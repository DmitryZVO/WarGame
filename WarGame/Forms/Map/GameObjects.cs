using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Markup;
using WarGame.Model;
using WarGame.Remote;

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

    public async void Init(SharpDx dx, CancellationToken ct = default)
    {
        RcSendAsync(ct);

        while (!ct.IsCancellationRequested)
        {
            if (BitmapTank == null)
            {
                var ret = await Files.GetSpriteAsync("Sprites", "Ship.png", ct);
                if (ret != null) BitmapTank = dx.CreateDxBitmap(ret);
            }

            await UpdateAsync(ct);
            await Task.Delay(50, ct); // 20гц

            if (Core.FrmMap!.Visible == false && Items.Count > 0) // Принудительное выделение объекта, если нет формы с картой
            {
                Items.First().Selected = true;
            }

            var obj = Items.Find(x => x.Selected);
            if (obj != null)
            {
                if (Core.FrmMap!.ObjectBlocked)
                {
                    Core.Config.Map.LonX = obj.LonX;
                    Core.Config.Map.LatY = obj.LatY;
                }
                await obj.UpdateTelemAsync(ct);
            }
        }
    }

    public async void RcSendAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(50, ct); // 20гЦ

            if (Core.FrmVideo!.ActiveControl == false) continue;

            var obj = Items.Find(x => x.Selected);
            if (obj != null)
            {
                var number = 0; // По умолчанию идет управление лодкой
                int camSel = Core.FrmVideo?.SelectedCamera ?? -1;
                switch (camSel)
                {
                    case 0: // Камера PTZ
                        number = 1;
                        break;
                    case 1: // Камера PTZ
                        number = 1;
                        break;
                    case 6: // Камера BOX1
                        number = 2;
                        break;
                    case 7: // Камера BOX2
                        number = 3;
                        break;
                    case 8: // Камера BOX3
                        number = 4;
                        break;
                    case 9: // Камера BOX4
                        number = 5;
                        break;
                    case -1: // Камера не выбрана, управляем лодкой
                    default:
                        number = 0;
                        break;
                }
                await obj.RewriteRcAsync(number, ct);
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
    public int Id { get; set; } // Уникальный номер объекта (4 байта)
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
        public ushort[] RcChannels { get; set; } = new ushort[8]; // Значения каналов управления
        public float MBitObjectIn { get; set; } // Прием данных от сервера в мегабитах (на объекте)
        public float MBitServerIn { get; set; } // Прием данных на сервер в мегабитах (на сервере)
        public float MBitObjectOut { get; set; } // Передача данных от объекта в мегабитах (на объекте)
        public float MBitServerOut { get; set; } // Передача данных от сервера в мегабитах (на сервере)
        public float PingToServer { get; set; } // Ping UDP до сервера и обратно
        public byte[] Relay { get; set; } = new byte[8]; // Значения каналов реле
        public byte[] RelayFrw { get; set; } = new byte[4]; // Значения каналов реле на носу
        public byte VideoFps { get; set; } // FPS видео с камер (0->5)
        public byte VideoQuality { get; set; } // Качество видео с камер (0->5)
        public byte CommandCount { get; set; } // Количество команд под исполнение
        public byte[] CanEngineBits { get; set; } = new byte[5];  // биты двигателя
        public ulong AliveCheck { get; set; } // статусы компонентов устройства
    }
    public class RcChannelsForWrite
    {
        public float[] Values { get; set; } = new float[8]; // Значения каналов управления
    }

    public static GameObject CreateFactory(GameObject o)
    {
        return o.Type switch
        {
            (0 | 1) => new GameObjShip()
            {
                Type = o.Type,
                Id = o.Id,
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
                Id = o.Id,
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

    public async Task<bool> SendCommandAsync(uint command, CancellationToken ct = default)
    {
        // Список команд для объекта
        //0xFFx00x00x0e - логирование на БЭКе, e-вкл(01)/выкл(00)
        //0xFEx00x0fx0q - изменить качество видео f-fps, q-качество
        //0x30x01x0nx0e - Управление реле 8каналов n-номер канала(0n), e-вкл(01)/выкл(00)
        //0x30x02x0nx0e - Управление реле 4канала n-номер канала(0n), e-вкл(01)/выкл(00)
        //0x0Fx00x00x00 - вернуть чеку на взрыватель
        //0x0Fx00x00x01 - снять чеку со взрывателя
        //0x0Fx00x00xFF - подрыв (только со снятой чекой)
        //0x11x0Nx00x00 - закрыть крышку N блока (нумерация с 0)
        //0x11x0Nx00x01 - открыть крышку N блока (нумерация с 0)
        //0x11x0Nx00x02 - остановить крышку N блока (нумерация с 0)
        //0x11x0Nx00x10 - выключить птицу N блока (нумерация с 0)
        //0x11x0Nx00x11 - включить птицу N блока (нумерация с 0)
        //0x22x0Nx00x00 - выключить мосфет N (нумерация с 0)
        //0x22x0Nx00x01 - включить мосфет N (нумерация с 0)

        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Core.Config.ServerUrl);
            using var answ = await web.GetAsync($"GameObjectCommand?id={Id:0}&command={command:0}", ct);
            return answ.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateTelemAsync(CancellationToken ct = default)
    {
        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Core.Config.ServerUrl);
            using var answ = await web.GetAsync($"GetGameObjectTelem?id={Id:0}", ct);
            var ret = !answ.IsSuccessStatusCode ? null : JsonSerializer.Deserialize<GameObject.GameObjectTelem>(await answ.Content.ReadAsStringAsync(ct));
            if (ret == null) return false;
            Telem = ret; // Перезаписываем телеметрию
        }
        catch
        {
            return false;
        }
        return true;
    }

    public async Task<bool> RewriteRcAsync(int number, CancellationToken ct = default)
    {
        if (!Core.Joystick.Alive) return false;
        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Core.Config.ServerUrl);
            RcChannelsForWrite rcNew = new()
            {
                Values = Core.Joystick.Channels
            };
            switch (number)
            {
                case 0: // это канал управления для лодки, нужно менять каналы местами
                    rcNew.Values = new float[8];
                    rcNew.Values[1] = Core.Joystick.Channels[2]; // LY CH2<-CH3
                    rcNew.Values[3] = Core.Joystick.Channels[3]; // LX CH4<-CH4
                    rcNew.Values[2] = Core.Joystick.Channels[1]; // RY CH3<-CH2
                    rcNew.Values[0] = Core.Joystick.Channels[0]; // RX CH1<-CH1
                    rcNew.Values[4] = Core.Joystick.Channels[4]; // ДЛ CH5<-CH5
                    rcNew.Values[5] = Core.Joystick.Channels[5]; // ДП CH6<-CH5
                    rcNew.Values[6] = Core.Joystick.Channels[6]; // БЛ CH7<-CH5
                    rcNew.Values[7] = Core.Joystick.Channels[7]; // БП CH8<-CH5
                    break;
                case 2: // это каналы управления птицами, нужно менять каналы местами
                case 3:
                case 4:
                case 5:
                    rcNew.Values = new float[8];
                    rcNew.Values[0] = Core.Joystick.Channels[2]; // LY CH1<-CH3
                    rcNew.Values[3] = Core.Joystick.Channels[3]; // LX CH4<-CH4
                    rcNew.Values[2] = Core.Joystick.Channels[1]; // RY CH3<-CH2
                    rcNew.Values[1] = Core.Joystick.Channels[0]; // RX CH2<-CH1
                    rcNew.Values[4] = Core.Joystick.Channels[4]; // ДЛ CH5<-CH5
                    rcNew.Values[5] = Core.Joystick.Channels[5]; // ДП CH6<-CH5
                    rcNew.Values[6] = Core.Joystick.Channels[6]; // БЛ CH7<-CH5
                    rcNew.Values[7] = Core.Joystick.Channels[7]; // БП CH8<-CH5
                    break;
                case 1: // это камера PTZ, изменения не нужны
                default:
                    break;
            }
            var jsonString = JsonSerializer.Serialize(rcNew);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            content.Headers.ContentLength = jsonString.Length;
            using var answ = await web.PostAsync($"SetGameObjectRcChannels?id={Id:0}&number={number:0}", content, ct);
            return answ.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
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
public class GameObjShip : GameObject // Type=1 (ровер)
{
    public GameObjShip()
    {
        ContextMenuEdit = new();
        //ContextMenuEdit.Items.Add("Изменить", null, (_, _) => { new FormObjEdit().ShowDialog(); });
        //ContextMenuEdit.Items.Add("Удалить объект", null, (_, _) => { FormMap.ObjectsStatic.ContextMenuDeleteStaticObj(); });
    }

    public override void Draw(SharpDx dx)
    {
        if (dx.Rt == null) return;
        //if (LonX <= 0) return;
        //if (LatY <= 0) return;
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