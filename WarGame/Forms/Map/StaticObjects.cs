using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System.Text;
using System.Text.Json;
using WarGame.Model;
using WarGame.Remote;

namespace WarGame.Forms.Map;

public class StaticObjects
{
    public static SharpDX.Direct2D1.Bitmap? BitmapCity { get; set; }
    public static SharpDX.Direct2D1.Bitmap? BitmapFlag { get; set; }
    public long TimeStamp { get; set; } = 0;
    public List<StaticObject> Items { get; set; } = [];

    public void Draw(SharpDx dx)
    {
        if (dx.Rt == null) return;

        lock (dx.Rt!)
        {
            lock (Items)
            {
                Items.ForEach(i => i.Draw(dx));
            }
        }
    }

    public async Task<bool> UpdateAsync(CancellationToken ct = default)
    {
        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Core.Config.ServerUrl);
            using var answ = await web.GetAsync($"GetStaticObjects", ct);
            var ret = !answ.IsSuccessStatusCode ? null : JsonSerializer.Deserialize<List<StaticObject>>(await answ.Content.ReadAsStringAsync(ct));
            if (ret == null) return false;

            lock (Items)
            {
                Items = ret;
            };
        }
        catch
        {
            return false;
        }
        return true;
    }

    public async Task<bool> ChangeAsync(CancellationToken ct = default)
    {
        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Core.Config.ServerUrl);
            var jsonString = JsonSerializer.Serialize(this);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.ContentLength = jsonString.Length;
            using var answ = await web.PostAsync($"SetStaticObjects?id=0", content, ct);
            return answ.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async void Init(SharpDx dx, CancellationToken ct = default)
    {
        while (!ct.IsCancellationRequested)
        {
            if (!TimeStamp.Equals(Core.Server.TimeStampStaticObjects))
            {
                if (await UpdateAsync(ct)) 
                {
                    TimeStamp = Core.Server.TimeStampStaticObjects;
                }
            }

            if (BitmapCity == null)
            {
                var ret = await Files.GetSpriteAsync("Sprites", "City.png", ct);
                if (ret != null) BitmapCity = dx.CreateDxBitmap(ret);
            }
            if (BitmapFlag == null)
            {
                var ret = await Files.GetSpriteAsync("Sprites", "Flag.png", ct);
                if (ret != null) BitmapFlag = dx.CreateDxBitmap(ret);
            }

            await Task.Delay(100, ct);
        }
    }

}

public class StaticObject
{
    public class Coord
    {
        public float X { get; set; }
        public float Y { get; set; }
        public bool Lighting { get; set; } // подсветка
        public bool Selected { get; set; } // объект выбран
        public void CheckSelecting(SharpDx dx, RawRectangleF rect)
        {
            if (!Selected) return;
            dx.Rt?.DrawRectangle(rect, dx.Brushes.RoiYellow03, 3.0f);
        }
        public void CheckLighting(SharpDx dx, RawRectangleF rect)
        {
            Lighting = Control.MousePosition.X <= rect.Right &&
                    Control.MousePosition.X >= rect.Left &&
                    Control.MousePosition.Y <= rect.Bottom &&
                    Control.MousePosition.Y >= rect.Top;
            if (!Lighting) return;
            dx.Rt?.DrawRectangle(rect, dx.Brushes.RoiGray03, 3.0f);
        }
    }

    public void CheckSelecting(SharpDx dx, RawRectangleF rect)
    {
        if (!Selected) return;
        dx.Rt?.DrawRectangle(rect, dx.Brushes.RoiYellow03, 3.0f);
    }

    public bool Lighting { get; set; } // подсветка
    public bool Selected { get; set; } // объект выбран
    public int Id { get; set; }
    public int Type { get; set; }
    public List<Coord> Coords { get; set; } = [];
    public bool Visible { get; set; }
    public string Name { get; set; } = string.Empty;

    public void CheckLighting(SharpDx dx, RawRectangleF rect)
    {
        Lighting = Control.MousePosition.X <= rect.Right &&
                Control.MousePosition.X >= rect.Left &&
                Control.MousePosition.Y <= rect.Bottom &&
                Control.MousePosition.Y >= rect.Top;
        if (!Lighting) return;
        dx.Rt?.DrawRectangle(rect, dx.Brushes.RoiGray03, 3.0f);
    }

    public void Draw(SharpDx dx)
    {
        if (dx.Rt == null) return;

        if (Type < 10) // Одиночные объекты
        {
            if (Coords.Count <= 0) return;
            if (!GeoMath.TileIsVisible(Core.Config.Map.Zoom, Coords[0].X, Coords[0].Y)) return;
            var pos = GeoMath.GpsPositionToScreen(dx, Coords[0].X, Coords[0].Y);
            switch (Type)
            {
                case 0: // город
                    {
                        var radius = 20.0f;
                        var rectBmp = new RawRectangleF(pos.X - radius, pos.Y - radius, pos.X + radius, pos.Y + radius);
                        dx.Rt.DrawBitmap(StaticObjects.BitmapCity ?? ((SharpDxMap)dx).BitmapNone, rectBmp, 0.8f, BitmapInterpolationMode.Linear);
                        dx.Rt.DrawText($"{Name}", dx.Brushes.SysText20, new RawRectangleF(pos.X + radius, pos.Y - 0.5f * radius, pos.X + 100 * radius, pos.Y + 0.5f * radius), dx.Brushes.SysTextBrushYellow);
                        CheckLighting(dx, rectBmp);
                        CheckSelecting(dx, rectBmp);
                        break;
                    }
                case 1: // метка
                    {
                        var radius = 20.0f;
                        var rectBmp = new RawRectangleF(pos.X - radius, pos.Y - radius, pos.X + radius, pos.Y + radius);
                        dx.Rt.DrawBitmap(StaticObjects.BitmapFlag ?? ((SharpDxMap)dx).BitmapNone, rectBmp, 0.8f,    BitmapInterpolationMode.Linear);
                        dx.Rt.DrawText($"{Name}", dx.Brushes.SysText20, new RawRectangleF(pos.X + radius, pos.Y - 0.5f * radius, pos.X + 100 * radius, pos.Y + 0.5f * radius), dx.Brushes.SysTextBrushYellow);
                        CheckLighting(dx, rectBmp);
                        CheckSelecting(dx, rectBmp);
                        break;
                    }
            }
            return;
        }
        if (Type <= 10) // Полигоны
        {
            var radius = 8.0f;
            if (Coords.Count <= 2) return;
            var vertex = new List<RawVector2>();
            var inDisplay = false;
            foreach (var c in Coords)
            {
                var v = GeoMath.GpsPositionToScreen(dx, c.X, c.Y);
                if (!inDisplay && GeoMath.TileIsVisible(Core.Config.Map.Zoom, c.X, c.Y)) inDisplay = true;
                vertex.Add(new RawVector2(v.X, v.Y));
                var rectVert = new RawRectangleF(v.X - radius, v.Y - radius, v.X + radius, v.Y + radius);
                c.CheckLighting(dx, rectVert);
                if (c.Selected) c.CheckSelecting(dx, rectVert);
            }
            if (!inDisplay) return;

            switch (Type)
            {
                case 10: // разрешенный рабочий полигон
                    {
                        using var geo = new PathGeometry(dx.D2dFactory);
                        var mainPolygon = geo.Open();
                        mainPolygon.BeginFigure(vertex[0], new FigureBegin());
                        for (var i=1;i<vertex.Count;i++)
                        {
                            mainPolygon.AddLine(vertex[i]);
                        }
                        mainPolygon.AddLine(vertex[0]);
                        mainPolygon.EndFigure(new FigureEnd());
                        mainPolygon.Close();
                        dx.Rt?.FillGeometry(geo, dx.Brushes.RoiBlue02);
                        dx.Rt?.DrawGeometry(geo, dx.Brushes.RoiGreen03, 5.0f);

                        foreach (var v in vertex)
                        {
                            dx.Rt?.FillEllipse(new Ellipse(v, radius, radius), dx.Brushes.RoiGreen03);
                        }
                        break;
                    }
            }
            return;
        }
    }
}
