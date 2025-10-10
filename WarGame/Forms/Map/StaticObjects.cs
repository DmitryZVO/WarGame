using System.Text.Json;
using WarGame.Model;
using WarGame.Remote;

namespace WarGame.Forms.Map;

public class StaticObjects
{
    public static SharpDX.Direct2D1.Bitmap? BitmapCity { get; set; }
    public static SharpDX.Direct2D1.Bitmap? BitmapFlag { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.MinValue;
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
    public int Id { get; set; }
    public int Type { get; set; }
    public double LonX { get; set; }
    public double LatY { get; set; }
    public bool Visible { get; set; }
    public string Name { get; set; } = string.Empty;

    public void Draw(SharpDx dx)
    {
        if (dx.Rt == null) return;

        if (!GeoMath.TileIsVisible(FormMap.GlobalPos.Zoom, LonX, LatY)) return;
        var tileSize = (int)(GeoMath.TileSize + FormMap.GlobalPos.ZoomLocal * GeoMath.TileSize);
        var tileObjectX = GeoMath.TileXForLon(FormMap.GlobalPos.Zoom, LonX);
        var tileObjectY = GeoMath.TileYForLat(FormMap.GlobalPos.Zoom, LatY);
        var tileCenterX = GeoMath.TileXForLon(FormMap.GlobalPos.Zoom, FormMap.GlobalPos.LonX);
        var tileCenterY = GeoMath.TileYForLat(FormMap.GlobalPos.Zoom, FormMap.GlobalPos.LatY);
        var posX = (tileObjectX - tileCenterX) * tileSize;
        var posY = (tileObjectY - tileCenterY) * tileSize;
        var tileCenter = GeoMath.GetTileCoord(FormMap.GlobalPos.Zoom, FormMap.GlobalPos.LonX, FormMap.GlobalPos.LatY);
        var deltaSx = tileCenter.Left - FormMap.GlobalPos.LonX;
        var deltaSy = tileCenter.Top - FormMap.GlobalPos.LatY;
        var sx = deltaSx / GeoMath.GetLenXForOneTile(FormMap.GlobalPos.Zoom, FormMap.GlobalPos.LatY, FormMap.GlobalPos.LonX) * tileSize;
        var sy = deltaSy / GeoMath.GetLenYForOneTile(FormMap.GlobalPos.Zoom, FormMap.GlobalPos.LatY, FormMap.GlobalPos.LonX) * tileSize;

        var tileObject = GeoMath.GetTileCoord(FormMap.GlobalPos.Zoom, LonX, LatY);
        var objectSx = LonX - tileObject.Left;
        var objectSy = LatY - tileObject.Top;
        var ox = objectSx / GeoMath.GetLenXForOneTile(FormMap.GlobalPos.Zoom, LatY, LonX) * tileSize;
        var oy = objectSy / GeoMath.GetLenYForOneTile(FormMap.GlobalPos.Zoom, LatY, LonX) * tileSize;

        var pos = new SharpDX.Mathematics.Interop.RawVector2(dx.BaseWidth / 2.0f + posX + (float)sx + (float)ox, dx.BaseHeight / 2.0f + posY + (float)sy + (float)oy);
        switch (Type)
        {
            case 0: // город
                {
                    var radius = 20.0f;
                    dx.Rt.DrawBitmap(StaticObjects.BitmapCity ?? ((SharpDxMap)dx).BitmapNone, new SharpDX.Mathematics.Interop.RawRectangleF(pos.X - radius, pos.Y - radius, pos.X + radius, pos.Y + radius), 0.8f, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
                    dx.Rt.DrawText($"{Name}", dx.Brushes.SysText20, new SharpDX.Mathematics.Interop.RawRectangleF(pos.X + radius, pos.Y - 0.5f * radius, pos.X + 100 * radius, pos.Y + 0.5f * radius), dx.Brushes.SysTextBrushYellow);
                    break;
                }
            case 1: // метка
                {
                    var radius = 20.0f;
                    dx.Rt.DrawBitmap(StaticObjects.BitmapFlag ?? ((SharpDxMap)dx).BitmapNone, new SharpDX.Mathematics.Interop.RawRectangleF(pos.X, pos.Y - 2 * radius, pos.X + 2 * radius, pos.Y - 0), 0.8f, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
                    dx.Rt.DrawText($"{Name}", dx.Brushes.SysText20, new SharpDX.Mathematics.Interop.RawRectangleF(pos.X + 2*radius, pos.Y - 1.5f * radius, pos.X + 100 * radius, pos.Y + 0.5f * radius), dx.Brushes.SysTextBrushYellow);
                    break;
                }
        };
    }
}
