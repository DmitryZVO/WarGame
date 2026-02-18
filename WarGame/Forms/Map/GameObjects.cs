using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WarGame.Model;
using WarGame.Remote;
using static WarGame.Forms.Map.GameObject;

namespace WarGame.Forms.Map;

public class GameObjects
{
    public static SharpDX.Direct2D1.Bitmap? BitmapTank { get; set; }
    public long TimeStamp { get; set; } = 0;
    public List<GameObject> Items { get; set; } = [];

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
            using var answ = await web.GetAsync($"GetGameObjectsList", ct);
            var ret = !answ.IsSuccessStatusCode ? null : JsonSerializer.Deserialize<List<GameObjJson>>(await answ.Content.ReadAsStringAsync(ct));
            if (ret == null) return false;

            foreach (var i in ret)
            {
                var x = Items.Find(x => x.Name.Equals(i.Name));
                if (x == null)
                {
                    lock (Items)
                    {
                        x = GameObject.CreateFactory(i);
                        Items.Add(x);
                        x.ParametersFromJson();
                    }
                }
                x.Angle = i.Angle;
                x.LatY = i.LatY;
                x.LonX = i.LonX;
                x.Z = i.Z;
            }
        }
        catch
        {
            return false;
        }
        return true;
    }

    public async Task<bool> UpdateTelemAsync(GameObject obj, CancellationToken ct = default)
    {
        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Core.Config.ServerUrl);
            using var answ = await web.GetAsync($"GetGameObjectTelem?name={obj.Name}", ct);
            var ret = !answ.IsSuccessStatusCode ? null : JsonSerializer.Deserialize<GameObject.GameObjectTelem>(await answ.Content.ReadAsStringAsync(ct));
            if (ret == null) return false;
            obj.Telem = ret; // Перезаписываем телеметрию
        }
        catch
        {
            return false;
        }
        return true;
    }

    public async Task<bool> RewriteRcAsync(GameObject obj, CancellationToken ct = default)
    {
        if (!Core.Joystick.Alive) return false;
        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Core.Config.ServerUrl);
            var rcNew = new RcChannelsForWrite();
            rcNew.Values = Core.Joystick.Channels;
            var jsonString = JsonSerializer.Serialize(rcNew);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.ContentLength = jsonString.Length;
            using var answ = await web.PostAsync($"SetGameObjectRcChannels?name={obj.Name}", content, ct);
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
            if (BitmapTank == null)
            {
                var ret = await Files.GetSpriteAsync("Sprites", "Tank.png", ct);
                if (ret != null) BitmapTank = dx.CreateDxBitmap(ret);
            }

            await UpdateAsync(ct);
            await Task.Delay(100, ct);
            var obj = Items.Find(x => x.Selected);
            if (obj != null)
            {
                await UpdateTelemAsync(obj, ct);
                await RewriteRcAsync(obj, ct);
            }
        }
    }

    public void ContextMenuDeleteStaticObj()
    {
        Items.RemoveAll(x => x.Selected);
        FormMap.Map.EditNeedSave = true;
    }
}

public abstract class GameObject : IDrawing
{
    [JsonIgnore] public ContextMenuStrip? ContextMenuEdit { get; set; }
    public int Type { get; set; } // Тип объекта
    public string Name { get; set; } = string.Empty; // Текстовое имя объекта
    public float LonX { get; set; } // Позиция по X
    public float LatY { get; set; } // Позиция по Y
    public float Z { get; set; } // Позиция по Z
    public float Angle { get; set; } // Угол поворота
    public bool Lighting { get; set; } // объект подсвечен
    public bool Selected { get; set; } // объект выбран
    [JsonIgnore] public GameObjectTelem Telem { get; set; } = new(); // Телеметрия объекта
    public class GameObjectTelem // Параметры телеметрии
    {
        public float[] Servos { get; set; } = new float[8]; // Значения сервоприводов
        public float[] RcChannels { get; set; } = new float[16]; // Значения каналов управления
        public float MBitObjectIn { get; set; } // Прием данных от сервера в мегабитах (на объекте)
        public float MBitServerIn { get; set; } // Прием данных на сервер в мегабитах (на сервере)
        public int MBitServerInBytesCounter { get; set; } // Счетчик приема данных в байтах
    }
    public class RcChannelsForWrite
    {
        public float[] Values { get; set; } = new float[16]; // Значения каналов управления
    }

    public static GameObject CreateFactory(GameObject o)
    {
        return o.Type switch
        {
            1 => new GameObjTank()
            {
                Type = o.Type,
                Name = o.Name,
                LonX = o.LonX,
                LatY = o.LatY,
                Z = o.Z,
                Angle = o.Angle,
                Lighting = o.Lighting,
                Selected = o.Selected,
            },
            _ => new GameObjJson()
            {
                Type = o.Type,
                Name = o.Name,
                Lighting = o.Lighting,
                Selected = o.Selected,
                LonX = o.LonX,
                LatY = o.LatY,
                Z = o.Z,
                Angle = o.Angle,
            }

        };
    }

    public virtual void Draw(SharpDx dx) { } // Отрисовка объекта
    public virtual void Add(float lonX, float latY) { } // Добавление объекта на карту
    public virtual void ParametersToJson() { }  // Упаковка доп параметров в json-строку
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


// Объект для десерелизации Json'а
public class GameObjJson : GameObject // Type=-1 (используется для десерелизации)
{
}

// Ровер (борщевик)
public class GameObjTank : GameObject // Type=1 (ровер)
{
    public GameObjTank()
    {
        ContextMenuEdit = new();
        //ContextMenuEdit.Items.Add("Изменить", null, (_, _) => { new FormObjEdit().ShowDialog(); });
        //ContextMenuEdit.Items.Add("Удалить объект", null, (_, _) => { FormMap.ObjectsStatic.ContextMenuDeleteStaticObj(); });
    }

    public override void Draw(SharpDx dx)
    {
        if (dx.Rt == null) return;
        if (LonX <= 0) return;
        if (LatY <= 0) return;
        if (!GeoMath.TileIsVisible(Core.Config.Map.Zoom, LonX, LatY)) return;
        var pos = GeoMath.GpsPositionToScreen(dx, LonX, LatY);
        var radius = 20.0f;
        var rectBmp = new RawRectangleF(pos.X - radius, pos.Y - radius, pos.X + radius, pos.Y + radius);
        var ellips = new Ellipse(new RawVector2(pos.X, pos.Y), radius * 1.2f, radius * 1.2f);
        var rectText = new RawRectangleF(pos.X + radius * 1.2f, pos.Y - 0.6f * radius, pos.X + radius * 5.0f, pos.Y + 0.6f * radius);
        var posText = new RawRectangleF(rectText.Left * 1.005f, rectText.Top * 1.001f, rectText.Right, rectText.Bottom);
        dx.Rt.FillRectangle(rectText, dx.Brushes.RoiYellow03);
        dx.Rt.DrawRectangle(rectText, dx.Brushes.SysTextBrushRed, 2.0f);
        dx.Rt.FillEllipse(ellips, dx.Brushes.RoiYellow03);
        dx.Rt.DrawEllipse(ellips, dx.Brushes.SysTextBrushRed, 2.0f);
        dx.Rt.DrawText($"{Name}", dx.Brushes.SysText20, posText, dx.Brushes.SysTextBrushRed);
        var mat = dx.TransformGet();
        dx.TransformSet(SharpDX.Matrix3x2.Multiply(mat, SharpDX.Matrix3x2.Rotation(GeoMath.GradToRad(Angle), pos)));
        dx.Rt.DrawBitmap(GameObjects.BitmapTank ?? ((SharpDxMap)dx).BitmapNone, rectBmp, 0.8f, BitmapInterpolationMode.Linear);
        dx.Rt.DrawLine(new RawVector2(pos.X, pos.Y), new RawVector2(pos.X, pos.Y - 100.0f), dx.Brushes.RoiGray03, 5.0f);
        dx.TransformSet(dx.ZeroTransform);

        DrawLighting(dx, rectBmp);
        DrawSelecting(dx, rectBmp);
    }
}