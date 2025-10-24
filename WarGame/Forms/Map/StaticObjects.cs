using OpenCvSharp;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WarGame.Model;
using WarGame.Remote;

namespace WarGame.Forms.Map;

public class StaticObjects
{
    public static SharpDX.Direct2D1.Bitmap? BitmapCity { get; set; }
    public static SharpDX.Direct2D1.Bitmap? BitmapFlag { get; set; }
    public static SharpDX.Direct2D1.Bitmap? BitmapAntenna { get; set; }
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
            var ret = !answ.IsSuccessStatusCode ? null : JsonSerializer.Deserialize<List<StaticObjJson>>(await answ.Content.ReadAsStringAsync(ct));
            if (ret == null) return false;

            lock (Items)
            {
                Items.Clear();
                ret.ForEach(x => Items.Add(StaticObject.CreateFactory(x)));
                Items.ForEach(x => x.ParametersFromJson());
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
            Items.ForEach(x => x.ParametersToJson());
            var jsonString = JsonSerializer.Serialize(this);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.ContentLength = jsonString.Length;
            using var answ = await web.PostAsync($"SetStaticObjects", content, ct);
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
            if (BitmapAntenna == null)
            {
                var ret = await Files.GetSpriteAsync("Sprites", "Antenna.png", ct);
                if (ret != null) BitmapAntenna = dx.CreateDxBitmap(ret);
            }
            if (BitmapFlag == null)
            {
                var ret = await Files.GetSpriteAsync("Sprites", "Flag.png", ct);
                if (ret != null) BitmapFlag = dx.CreateDxBitmap(ret);
            }

            await Task.Delay(100, ct);
        }
    }

    public void ContextMenuDeleteStaticObj()
    {
        Items.RemoveAll(x => x.Selected);
        FormMap.Map.EditNeedSave = true;
    }
}

public interface IDrawing // Доступна отрисовка объекта
{
    void Draw(SharpDx dx); // Отрисовка
}

public abstract class StaticObject : IDrawing
{
    [JsonIgnore] public ContextMenuStrip? ContextMenuEdit { get; set; }
    public int Type { get; set; } // Тип объекта
    public int Id { get; set; } // Номер (id) объекта
    public string Name { get; set; } = string.Empty; // Текстовое имя объекта
    public List<PolyVertex> Coords { get; set; } = []; // Список координат объекта
    public bool Lighting { get; set; } // объект выбран
    public bool Selected { get; set; } // объект выбран
    public string ParamsJsonString { get; set; } = string.Empty; // Дополнительные параметры объекта в JSON

    public static StaticObject CreateFactory(StaticObject o)
    {
        return o.Type switch
        {
            0 => new StaticObjCity()
            {
                Type = o.Type,
                Id = o.Id,
                Name = o.Name,
                Coords = o.Coords,
                Lighting = o.Lighting,
                Selected = o.Selected,
                ParamsJsonString = o.ParamsJsonString,
            },
            1 => new StaticObjPoint()
            {
                Type = o.Type,
                Id = o.Id,
                Name = o.Name,
                Coords = o.Coords,
                Lighting = o.Lighting,
                Selected = o.Selected,
                ParamsJsonString = o.ParamsJsonString,
            },
            2 => new StaticObjAntenna()
            {
                Type = o.Type,
                Id = o.Id,
                Name = o.Name,
                Coords = o.Coords,
                Lighting = o.Lighting,
                Selected = o.Selected,
                ParamsJsonString = o.ParamsJsonString,
            },
            10 => new StaticObjPoly()
            {
                Type = o.Type,
                Id = o.Id,
                Name = o.Name,
                Coords = o.Coords,
                Lighting = o.Lighting,
                Selected = o.Selected,
                ParamsJsonString = o.ParamsJsonString,
            },
            _ => new StaticObjJson() 
            {
                Type = o.Type,
                Id = o.Id,
                Name = o.Name,
                Coords = o.Coords,
                Lighting = o.Lighting,
                Selected = o.Selected,
                ParamsJsonString = o.ParamsJsonString,
            },
        };
    }

    public virtual void Draw(SharpDx dx) { } // Отрисовка объекта
    public virtual void Add(float lonX, float latY) { } // Добавление объекта на карту
    public virtual void ParametersToJson()  // Упаковка доп параметров в json-строку
    {
        ParamsJsonString = string.Empty;
    }
    public virtual void ParametersFromJson() { } // Распаковка доп параметров из json-строки

    public virtual void DrawSelecting(SharpDx dx, RawRectangleF rect)
    {
        if (!Selected) return;
        dx.Rt?.DrawRectangle(rect, dx.Brushes.RoiYellow03, 3.0f);
    }

    public virtual void DrawLighting(SharpDx dx, RawRectangleF rect)
    {
        Lighting = (Control.MousePosition.X - Core.FrmMap!.Location.X) <= rect.Right &&
                (Control.MousePosition.X - Core.FrmMap!.Location.X) >= rect.Left &&
                (Control.MousePosition.Y - Core.FrmMap!.Location.Y) <= rect.Bottom &&
                (Control.MousePosition.Y - Core.FrmMap!.Location.Y) >= rect.Top;
        if (!Lighting) return;
        dx.Rt?.DrawRectangle(rect, dx.Brushes.RoiGray03, 3.0f);
    }
}

public class PolyVertex
{
    public bool Lighting { get; set; } // подсветка
    public bool Selected { get; set; } // объект выбран
    public float X { get; set; }
    public float Y { get; set; }
    [JsonIgnore] public ContextMenuStrip? ContextMenuEdit { get; set; } // Контекстное меню

    public PolyVertex()
    {
        ContextMenuEdit = new();
        ContextMenuEdit.Items.Add("Добавить вершину", null, (_, _) => { Add(); });
        ContextMenuEdit.Items.Add("Удалить вершину", null, (_, _) => { Delete(); });
        ContextMenuEdit.Items.Add("Удалить ВЕСЬ полигон", null, (_, _) => { DeleteAll(); });
    }
    public virtual void DrawSelecting(SharpDx dx, RawRectangleF rect)
    {
        if (!Selected) return;
        dx.Rt?.DrawRectangle(rect, dx.Brushes.RoiYellow03, 3.0f);
    }
    public virtual void DrawLighting(SharpDx dx, RawRectangleF rect)
    {
        Lighting = (Control.MousePosition.X - Core.FrmMap!.Location.X) <= rect.Right &&
                (Control.MousePosition.X - Core.FrmMap!.Location.X) >= rect.Left &&
                (Control.MousePosition.Y - Core.FrmMap!.Location.Y) <= rect.Bottom &&
                (Control.MousePosition.Y - Core.FrmMap!.Location.Y) >= rect.Top;
        if (!Lighting) return;
        dx.Rt?.DrawRectangle(rect, dx.Brushes.RoiGray03, 3.0f);
    }
    private static void Delete()
    {
        var poly = FormMap.ObjectsStatic.Items.Find(x => x.Coords.Any(y => y.Selected));
        if (poly == null) return;
        if (poly.Coords.Count <= 3)
        {
            MessageBox.Show("Полигон не может содержать менее трех вершин!", "ОШИБКА", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        poly.Coords.RemoveAll(x => x.Selected);
        FormMap.Map.EditNeedSave = true;
    }

    private static void DeleteAll()
    {
        FormMap.ObjectsStatic.Items.RemoveAll(x => x.Coords.Any(y => y.Selected));
        FormMap.Map.EditNeedSave = true;
    }

    private static void Add()
    {
        var poly = FormMap.ObjectsStatic.Items.Find(x => x.Coords.Any(y => y.Selected));
        if (poly == null) return;
        var vert = poly.Coords.FindIndex(x => x.Selected);
        if (vert < 0) return;
        poly.Coords.ForEach(x => x.Selected = false);
        poly.Coords.ForEach(x => x.Lighting = false);
        poly.Coords.Insert(vert + 1, new PolyVertex() { Selected = true, Lighting = false, X = poly.Coords[vert].X, Y = poly.Coords[vert].Y });
        FormMap.Map.EditNeedSave = true;
    }

}

// Объект для десерелизации Json'а
public class StaticObjJson : StaticObject // Type=-1 (используется для десерелизации)
{
}

// Метка на карте
public class StaticObjPoint : StaticObject // Type=1
{
    public StaticObjPoint()
    {
        ContextMenuEdit = new();
        ContextMenuEdit.Items.Add("Изменить", null, (_, _) => { new FormObjEdit().ShowDialog(); });
        ContextMenuEdit.Items.Add("Удалить объект", null, (_, _) => { FormMap.ObjectsStatic.ContextMenuDeleteStaticObj(); });
    }

    public override void Add(float lonX, float latY)
    {
        var obj = FormMap.ObjectsStatic.Items.Find(x => x.Coords.Any(y => y.Selected));
        if (obj != null) return;
        var id = FormMap.ObjectsStatic.Items.Max(x => x.Id) + 1;
        FormMap.ObjectsStatic.Items.Add(new StaticObjPoint { Selected = false, Lighting = false, Name = $"Точка {id:0}", Type = 1, Id = id, Coords = [new PolyVertex() { X = lonX, Y = latY }] });
        FormMap.Map.EditNeedSave = true;
    }

    public override void Draw(SharpDx dx)
    {
        if (dx.Rt == null) return;
        if (Coords.Count <= 0) return;
        if (!GeoMath.TileIsVisible(Core.Config.Map.Zoom, Coords[0].X, Coords[0].Y)) return;
        var pos = GeoMath.GpsPositionToScreen(dx, Coords[0].X, Coords[0].Y);
        var radius = 20.0f;
        var rectBmp = new RawRectangleF(pos.X - radius, pos.Y - radius, pos.X + radius, pos.Y + radius);
        dx.Rt.DrawBitmap(StaticObjects.BitmapFlag ?? ((SharpDxMap)dx).BitmapNone, rectBmp, 0.8f, BitmapInterpolationMode.Linear);
        dx.Rt.DrawText($"{Name}", dx.Brushes.SysText20, new RawRectangleF(pos.X + radius, pos.Y - 0.5f * radius, pos.X + 100 * radius, pos.Y + 0.5f * radius), dx.Brushes.SysTextBrushYellow);
        DrawLighting(dx, rectBmp);
        DrawSelecting(dx, rectBmp);
    }
}

// Населенный пункт
public class StaticObjCity : StaticObject // Type=0
{
    public StaticObjCity()
    {
        ContextMenuEdit = new();
        ContextMenuEdit.Items.Add("Изменить", null, (_, _) => { new FormObjEdit().ShowDialog(); });
        ContextMenuEdit.Items.Add("Удалить объект", null, (_, _) => { FormMap.ObjectsStatic.ContextMenuDeleteStaticObj(); });
    }

    public override void Add(float lonX, float latY)
    {
        var obj = FormMap.ObjectsStatic.Items.Find(x => x.Coords.Any(y => y.Selected));
        if (obj != null) return;
        var id = FormMap.ObjectsStatic.Items.Max(x => x.Id) + 1;
        FormMap.ObjectsStatic.Items.Add(new StaticObjCity { Selected = false, Lighting = false, Name = $"Город {id:0}", Type = 0, Id = id, Coords = [new PolyVertex() { X = lonX, Y = latY }] });
        FormMap.Map.EditNeedSave = true;
    }

    public override void Draw(SharpDx dx)
    {
        if (dx.Rt == null) return;
        if (Coords.Count <= 0) return;
        if (!GeoMath.TileIsVisible(Core.Config.Map.Zoom, Coords[0].X, Coords[0].Y)) return;
        var pos = GeoMath.GpsPositionToScreen(dx, Coords[0].X, Coords[0].Y);
        var radius = 20.0f;
        var rectBmp = new RawRectangleF(pos.X - radius, pos.Y - radius, pos.X + radius, pos.Y + radius);
        dx.Rt.DrawBitmap(StaticObjects.BitmapCity ?? ((SharpDxMap)dx).BitmapNone, rectBmp, 0.8f, BitmapInterpolationMode.Linear);
        dx.Rt.DrawText($"{Name}", dx.Brushes.SysText20, new RawRectangleF(pos.X + radius, pos.Y - 0.5f * radius, pos.X + 100 * radius, pos.Y + 0.5f * radius), dx.Brushes.SysTextBrushYellow);
        DrawLighting(dx, rectBmp);
        DrawSelecting(dx, rectBmp);
    }
}

// Полигон на карте
public class StaticObjPoly : StaticObject // Type=10
{
    public StaticObjPoly()
    {
        ContextMenuEdit = new();
    }

    public override void Add(float lonX, float latY)
    {
        var obj = FormMap.ObjectsStatic.Items.Find(x => x.Coords.Any(y => y.Selected));
        if (obj != null) return;
        var sizeX = (float)(GeoMath.GetLenXForOneTile(Core.Config.Map.Zoom, Core.Config.Map.LonX, Core.Config.Map.LatY) / 10.0f);
        var sizeY = (float)(GeoMath.GetLenYForOneTile(Core.Config.Map.Zoom, Core.Config.Map.LonX, Core.Config.Map.LatY) / 15.0f);
        var id = FormMap.ObjectsStatic.Items.Max(x => x.Id) + 1;
        FormMap.ObjectsStatic.Items.Add(new StaticObjPoly { Selected = false, Lighting = false, Name = $"Полигон {id:0}", Type = 10, Id = id, Coords = [new PolyVertex() { X = lonX - sizeX, Y = latY + sizeY }, new PolyVertex() { X = lonX - sizeX, Y = latY - sizeY }, new PolyVertex() { X = lonX + sizeX, Y = latY - sizeY }, new PolyVertex() { X = lonX + sizeX, Y = latY + sizeY }] });
        FormMap.Map.EditNeedSave = true;
    }

    public override void Draw(SharpDx dx)
    {
        if (dx.Rt == null) return;
        if (Coords.Count <= 2) return;

        var radius = 8.0f;
        var vertex = new List<RawVector2>();
        var inDisplay = false;
        foreach (var c in Coords)
        {
            var v = GeoMath.GpsPositionToScreen(dx, c.X, c.Y);
            if (!inDisplay && GeoMath.TileIsVisible(Core.Config.Map.Zoom, c.X, c.Y)) inDisplay = true;
            vertex.Add(new RawVector2(v.X, v.Y));
            var rectVert = new RawRectangleF(v.X - radius, v.Y - radius, v.X + radius, v.Y + radius);
            c.DrawLighting(dx, rectVert);
            if (c.Selected) c.DrawSelecting(dx, rectVert);
        }
        if (!inDisplay) return;

        using var geo = new PathGeometry(dx.D2dFactory);
        var mainPolygon = geo.Open();
        mainPolygon.BeginFigure(vertex[0], new FigureBegin());
        for (var i = 1; i < vertex.Count; i++)
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
    }
}

// Статическая антенна на карте
public class StaticObjAntenna : StaticObject // Type=2
{
    public Param Parameters { get; set; } = new Param();

    public StaticObjAntenna()
    {
        ContextMenuEdit = new();
        ContextMenuEdit.Items.Add("Изменить", null, (_, _) => { new FormObjAntennaEdit().ShowDialog(); });
        ContextMenuEdit.Items.Add("Удалить объект", null, (_, _) => { FormMap.ObjectsStatic.ContextMenuDeleteStaticObj(); });
    }

    public override void ParametersToJson() // Упаковка доп параметров в json-строку
    {
        ParamsJsonString = JsonSerializer.Serialize(Parameters);
    }

    public override void ParametersFromJson() // Распаковка доп параметров из json-строки
    {
        try
        {
            Parameters = JsonSerializer.Deserialize<Param>(ParamsJsonString) ?? new Param();
        }
        catch
        {
            //
        }
    }

    public override void Add(float lonX, float latY)
    {
        var obj = FormMap.ObjectsStatic.Items.Find(x => x.Coords.Any(y => y.Selected));
        if (obj != null) return;
        var id = FormMap.ObjectsStatic.Items.Max(x => x.Id) + 1;
        FormMap.ObjectsStatic.Items.Add(new StaticObjAntenna { Selected = false, Lighting = false, Name = $"Антенна {id:0}", Type = 2, Id = id, Coords = [new PolyVertex() { X = lonX, Y = latY }], Parameters = new Param() { Angle = 0, DrawRadio = true, LenKm = 10, Width = (float)Math.PI/180.0f } });
        FormMap.Map.EditNeedSave = true;
    }

    public override void Draw(SharpDx dx)
    {
        if (dx.Rt == null) return;
        if (Coords.Count <= 0) return;
        var pos = GeoMath.GpsPositionToScreen(dx, Coords[0].X, Coords[0].Y);

        if (Parameters.DrawRadio)
        {
            var newGpsPoint = new Point2f(Coords[0].X, (float)(Coords[0].Y + (180.0f / Math.PI) * ((Parameters.LenKm * 1000.0f) / 6378137.0f)));
            var posNew = GeoMath.GpsPositionToScreen(dx, newGpsPoint.X, newGpsPoint.Y);
            var len = pos.Y - posNew.Y;

            using var geo = new PathGeometry(dx.D2dFactory);
            var mainPolygon = geo.Open();
            var px = (float)(len * Math.Sin(Parameters.Width / 2.0d));
            var py = (float)(len * Math.Cos(Parameters.Width / 2.0d));
            var pos1 = new RawVector2(pos.X - px, pos.Y - py);
            var pos2 = new RawVector2(pos.X + px, pos.Y - py);
            mainPolygon.BeginFigure(pos, new FigureBegin());
            mainPolygon.AddLine(pos1);
            mainPolygon.AddArc(new ArcSegment() { Point = pos2, Size = new SharpDX.Size2F(len, len), RotationAngle = 0.0f, ArcSize = ArcSize.Small, SweepDirection = SweepDirection.Clockwise });
            mainPolygon.AddLine(pos2);
            mainPolygon.AddLine(pos);
            mainPolygon.EndFigure(new FigureEnd());
            mainPolygon.Close();
            var mat = dx.TransformGet();
            dx.TransformSet(SharpDX.Matrix3x2.Multiply(mat, SharpDX.Matrix3x2.Rotation(Parameters.Angle, pos)));
            dx.Rt?.FillGeometry(geo, dx.Brushes.RoiGreen02);
            dx.Rt?.DrawGeometry(geo, dx.Brushes.RoiGreen03, 5.0f);
            dx.TransformSet(dx.ZeroTransform);
        }

        var size = 20.0f;
        var rectBmp = new RawRectangleF(pos.X - size, pos.Y - size, pos.X + size, pos.Y + size);
        dx.Rt?.DrawBitmap(StaticObjects.BitmapAntenna ?? ((SharpDxMap)dx).BitmapNone, rectBmp, 0.8f, BitmapInterpolationMode.Linear);
        dx.Rt?.DrawText($"{Name}", dx.Brushes.SysText20, new RawRectangleF(pos.X + size, pos.Y - 0.5f * size, pos.X + 100 * size, pos.Y + 0.5f * size), dx.Brushes.SysTextBrushYellow);
        DrawLighting(dx, rectBmp);
        DrawSelecting(dx, rectBmp);
    }
    public class Param
    {
        public float Angle { get; set; } // Угол поворота в радианах
        public float Width { get; set; } // Ширина луча в радианах
        public float LenKm { get; set; } // Длинна излучения в километрах
        public bool DrawRadio { get; set; } = true; // Рисовать луч или нет
    }
}

// Статическая камера на карте
public class StaticObjCamera : StaticObject // Type=3
{
    public StaticObjCamera()
    {
        ContextMenuEdit = new();
        ContextMenuEdit.Items.Add("Удалить объект", null, (_, _) => { FormMap.ObjectsStatic.ContextMenuDeleteStaticObj(); });
    }

    public override void Draw(SharpDx dx)
    {

    }
}

// Запрещенная зона на карте
public class StaticObjBlockZone : StaticObject // Type=4
{
    public StaticObjBlockZone()
    {
        ContextMenuEdit = new();
        ContextMenuEdit.Items.Add("Удалить объект", null, (_, _) => { FormMap.ObjectsStatic.ContextMenuDeleteStaticObj(); });
    }

    public override void Draw(SharpDx dx)
    {

    }
}
