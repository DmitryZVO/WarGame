using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Mathematics.Interop;
using WarGame.Forms.Map;
using WarGame.Model;
using WarGame.Remote;
using WarGame.Resources;

namespace WarGame.Forms.Telem;

internal class SharpDxTelem(PictureBox surfacePtr, int fpsTarget) : SharpDx(surfacePtr, fpsTarget, new Sprites(), 1920)
{
    public bool NotActive { get; set; }
    public static SharpDX.Direct2D1.Bitmap? BitmapCompassLen { get; set; }

    protected sealed override async void DrawUser()
    {
        if (BitmapCompassLen == null)
        {
            var ret = await Files.GetSpriteAsync("Sprites", "CompassLen.png");
            if (ret != null) BitmapCompassLen = CreateDxBitmap(ret);
            return;
        }

        lock (this)
        {
            Rt?.Clear(new RawColor4(0, 0, 0, 1));
            var telemRect = new RawRectangleF(BaseWidth * 0.08f, BaseHeight * 0.01f, BaseWidth*2f, BaseHeight);
            var joyRect = new RawRectangleF(BaseWidth * 0.82f, BaseHeight * 0.01f, BaseWidth * 0.918f, BaseHeight * 0.25f);
            var joyColor = Core.Joystick.Alive ? Brushes.RoiGray03 : Brushes.RoiRed03;
            var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
            var yLine = (joyRect.Bottom - joyRect.Top) / Core.Joystick.Channels.Length;
            var xLine = joyRect.Right - joyRect.Left;
            int i;
            Core.FrmTelem!.ButtonVisibleChange(obj);
            if (obj != null) // Объект выбран
            {
                var sy = 0;
                var syT = 24.0f;
                Rt?.DrawText($"ЗАПИСЬ ЛОГОВ: {(obj.Telem.EnableCheck & 0x01)>0}", Brushes.SysText20, new RawRectangleF(telemRect.Left, telemRect.Top + (sy * syT), telemRect.Right, telemRect.Bottom), (obj.Telem.EnableCheck & 0x01) > 0 ? Brushes.SysTextBrushGreen : Brushes.SysTextBrushRed);
                sy = 2;
                syT = 30.0f;
                Rt?.DrawText($"ОБЪЕКТ: {obj.Name}", Brushes.SysText20, new RawRectangleF(telemRect.Left, telemRect.Top + (sy * syT), telemRect.Right, telemRect.Bottom), Brushes.SysTextBrushYellow); sy++;
                Rt?.DrawText($"LonX: {obj.LonX:0.000000}", Brushes.SysText20, new RawRectangleF(telemRect.Left, telemRect.Top + (sy * syT), telemRect.Right, telemRect.Bottom), Brushes.SysTextBrushYellow); sy++;
                Rt?.DrawText($"LatY: {obj.LatY:0.000000}", Brushes.SysText20, new RawRectangleF(telemRect.Left, telemRect.Top + (sy * syT), telemRect.Right, telemRect.Bottom), Brushes.SysTextBrushYellow); sy++;
                Rt?.DrawText($"PITCH: {obj.Telem.PitchGrad:0.00}°", Brushes.SysText20, new RawRectangleF(telemRect.Left, telemRect.Top + (sy * syT), telemRect.Right, telemRect.Bottom), Brushes.SysTextBrushYellow); sy++;
                Rt?.DrawText($"ROLL: {obj.Telem.RollGrad:0.00}°", Brushes.SysText20, new RawRectangleF(telemRect.Left, telemRect.Top + (sy * syT), telemRect.Right, telemRect.Bottom), Brushes.SysTextBrushYellow); sy++;
                Rt?.DrawText($"YAW: {obj.Telem.YawGrad:0.00}°", Brushes.SysText20, new RawRectangleF(telemRect.Left, telemRect.Top + (sy * syT), telemRect.Right, telemRect.Bottom), Brushes.SysTextBrushYellow); sy++;


                joyColor = Core.Joystick.Alive ? Brushes.RoiGreen03 : joyColor;
                for (i = 0; i < obj.Telem.RcChannels.Length; i++)
                {
                    var rectLine = new RawRectangleF(joyRect.Left, joyRect.Top + (i * yLine) + 0.0f, joyRect.Right, joyRect.Top + +((i + 1) * yLine));
                    Rt?.FillRectangle(new RawRectangleF(rectLine.Left, rectLine.Top, rectLine.Left + xLine * 0.5f * (((obj.Telem.RcChannels[i]-1500)/500.0f) + 1.0f), rectLine.Bottom), joyColor);
                    Rt?.DrawRectangle(rectLine, joyColor, 3.0f);
                    Rt?.DrawText($"CH{i + 1:00} [{obj.Telem.RcChannels[i]:0000}]", Brushes.SysText20, new RawRectangleF(rectLine.Left + xLine * 0.22f, rectLine.Top + yLine * 0.16f, rectLine.Right, rectLine.Bottom), Brushes.SysTextBrushYellow);
                }
                var colServOut = obj.Telem.MBitServerOut < 4.0f ? Brushes.SysTextBrushGreen : obj.Telem.MBitServerOut < 10.0f ? Brushes.SysTextBrushYellow : Brushes.SysTextBrushRed;
                Rt?.DrawText($"SERV_O ->: {obj.Telem.MBitServerOut:0.000} Mbit", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.11f, BaseHeight * 0.26f, BaseWidth, BaseHeight), colServOut);
                var colObjIn = obj.Telem.MBitObjectIn < 4.0f ? Brushes.SysTextBrushGreen : obj.Telem.MBitObjectIn < 10.0f ? Brushes.SysTextBrushYellow : Brushes.SysTextBrushRed;
                Rt?.DrawText($"OBJ_I  <-: {obj.Telem.MBitObjectIn:0.000} Mbit", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.18f, BaseHeight * 0.28f, BaseWidth, BaseHeight), colObjIn);
                var colObjOut = obj.Telem.MBitObjectOut < 4.0f ? Brushes.SysTextBrushGreen : obj.Telem.MBitObjectOut < 10.0f ? Brushes.SysTextBrushYellow : Brushes.SysTextBrushRed;
                Rt?.DrawText($"OBJ_O ->: {obj.Telem.MBitObjectOut:0.000} Mbit", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.16f, BaseHeight * 0.30f, BaseWidth, BaseHeight), colObjOut);
                var colServIn = obj.Telem.MBitServerIn < 4.0f ? Brushes.SysTextBrushGreen : obj.Telem.MBitServerIn < 10.0f ? Brushes.SysTextBrushYellow : Brushes.SysTextBrushRed;
                Rt?.DrawText($"SERV_I  <-: {obj.Telem.MBitServerIn:0.000} Mbit", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.12f, BaseHeight * 0.32f, BaseWidth, BaseHeight), colServIn);
                var colPing = obj.Telem.PingToServer < 10.0f ? Brushes.SysTextBrushGreen : obj.Telem.PingToServer < 100.0f ? Brushes.SysTextBrushYellow : Brushes.SysTextBrushRed;
                Rt?.DrawText($"PING SERV<>OBJ: {obj.Telem.PingToServer:###0} ms", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.08f, BaseHeight * 0.34f, BaseWidth, BaseHeight), colPing);
                var colQ = obj.Telem.CommandCount < 10 ? Brushes.SysTextBrushGreen : obj.Telem.CommandCount < 50 ? Brushes.SysTextBrushYellow : Brushes.SysTextBrushRed;
                Rt?.DrawText($"COM_QUEUE: {obj.Telem.CommandCount:0}", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.24f, BaseHeight * 0.36f, BaseWidth, BaseHeight), colQ);
                DrawCompass();
            }
            else // Объект не выбран, отображаем статус джойстика текущий
            {
                Rt?.DrawText($"ОБЪЕКТ НЕ ВЫБРАН", Brushes.SysText104, new RawRectangleF(BaseWidth * 0.25f, BaseHeight * 0.45f, BaseWidth, BaseHeight), Brushes.RoiYellow03);
            }
        }
    }

    private void DrawCompass()
    {
        var oneAngleLen = 0.049f * BaseWidth;
        var boxLw = BaseHeight * 0.30f;
        var boxLh = BaseHeight * 0.20f;
        var cX0 = BaseWidth / 2f;
        const int h = 100;
        var cY0 = BaseHeight / 2f;
        float startH = BaseHeight * 0.7f;
        var brush = Brushes.SysTextBrushDarkGreen;
        var brushCell = Brushes.RoiYellow03;
        var border = 6f;

        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj != null) // Объект выбран
        {
            // Отрисовка полоски компаса
            TransformSet(ZeroTransform);
            var rectOrig = new RawRectangleF(cX0 - boxLw - border, startH - border - h * 5, cX0 + boxLw + border, startH + h + border);
            Rt?.DrawRectangle(rectOrig, brushCell, 6.0f);
            Rt?.DrawLine(new RawVector2(cX0 - boxLw - border + 3f, startH - 4f), new RawVector2(cX0 + boxLw + border - 3f, startH - 4f), brushCell, 6.0f);
            Rt?.PushAxisAlignedClip(new RawRectangleF(cX0 - boxLw -2f, startH, cX0 + boxLw + 2f, startH + h), new AntialiasMode()); // Создание зоны отрисовки!
            TransformSet(Matrix3x2.Translation(-GeoMath.GradToRad(obj.Telem.YawGrad) * oneAngleLen, 0f)); // Масштабируем сцену на размер Scene_Zoom
            Rt?.DrawBitmap(BitmapCompassLen, new RawRectangleF(0, startH, BaseWidth, startH + h), 1.0f, BitmapInterpolationMode.Linear);
            Rt?.PopAxisAlignedClip(); // Возврат зоны отрисовки!
            TransformSet(ZeroTransform);
            var point0 = new RawVector2(cX0, startH - 1f);
            Rt?.DrawLine(point0, new RawVector2(cX0, startH + h - 35f), brushCell, 6.0f);
            Rt?.DrawLine(new RawVector2(cX0, startH + h - 10f), new RawVector2(cX0, startH + h + 3f), brushCell, 6.0f);

            // Отрисовка земли и неба
            TransformSet(ZeroTransform);
            var rectClip = new RawRectangleF(rectOrig.Left + 5f, rectOrig.Top + 5f, rectOrig.Right - 5f, rectOrig.Bottom - h - 12f);
            var center = new RawVector2(cX0, cY0 - 26f);
            Rt?.PushAxisAlignedClip(rectClip, new AntialiasMode()); // Создание зоны отрисовки!
            var a = Matrix3x2.Rotation(GeoMath.GradToRad(obj.Telem.RollGrad), center);
            var b = Matrix3x2.Translation(0f, GeoMath.GradToRad(obj.Telem.PitchGrad) * 300f); // Масштабируем сцену на размер Scene_Zoom
            TransformSet(a * b);
            Rt?.FillRectangle(new RawRectangleF(cX0 - boxLw - border - BaseWidth, startH - border - h * 5 - BaseHeight - h * 3.5f, cX0 + boxLw + border + BaseWidth, startH + h + border - h * 3.5f), Brushes.RoiGray02);
            Rt?.FillRectangle(new RawRectangleF(cX0 - boxLw - border - BaseWidth, startH - border - h * 5 + BaseHeight * 0.5f - h * 2.8f, cX0 + boxLw + border + BaseWidth, startH + h + border - h * 3.5f + BaseHeight), Brushes.RoiBlue02);
            Rt?.PopAxisAlignedClip(); // Возврат зоны отрисовки!
            TransformSet(ZeroTransform);
            Rt?.DrawLine(new RawVector2(center.X - 80f, center.Y), new RawVector2(center.X + 80f, center.Y), brushCell, 6.0f);
            Rt?.DrawLine(new RawVector2(center.X, center.Y - 40f), new RawVector2(center.X, center.Y - 3f), brushCell, 6.0f);
            Rt?.DrawText($"КРЕН: {obj.Telem.RollGrad:0.00}°", Brushes.SysText20, new RawRectangleF(point0.X - 320f, point0.Y - 30f, BaseWidth, BaseHeight), Brushes.RoiGreen03);
            Rt?.DrawText($"ТАНГАЖ: {obj.Telem.PitchGrad:0.00}°", Brushes.SysText20, new RawRectangleF(point0.X + 170f, point0.Y - 30f, BaseWidth, BaseHeight), Brushes.RoiGreen03);
            Rt?.DrawText($"РЫСКАНИЕ: {obj.Telem.YawGrad:0.00}°", Brushes.SysText20, new RawRectangleF(point0.X - 100f, point0.Y - 30f, BaseWidth, BaseHeight), Brushes.RoiGreen03);

            // Отрисовка статуса двигателя
            var rectEng = new RawRectangleF(cX0 - boxLw - 150f, startH - h * 4.7f, cX0 - boxLw - 20f, startH - 5f);
            Rt?.DrawRectangle(rectEng, brushCell, 6.0f);
            var maxOb = 6000; // Максимальные обороты двигателя
            var ob = (int)(obj.Telem.CanEngineBits[0] * GameObject.GameObjectTelem.KoeffEngineOb);
            var obF = Math.Max(0.0f, Math.Min(1.0f, ob / maxOb)); // Процент оборотов двигателя от максимума
            var rectOb = new RawRectangleF(rectEng.Left + 4f, rectEng.Bottom + 4f - (rectEng.Bottom - rectEng.Top) * obF, rectEng.Right - 4f, rectEng.Bottom - 4f);
            if (obF >= 0.02f)
            {
                Rt?.FillRectangle(rectOb, Brushes.SysTextBrushGreen);
                Rt?.FillRectangle(rectOb, Brushes.RoiNone);
                Rt?.FillRectangle(rectOb, Brushes.RoiNone);
            }
            var maxGear = 100.0f; // Максимальный газ
            var gr = (int)(obj.Telem.CanEngineBits[2] * GameObject.GameObjectTelem.KoeffEngineGear);
            var grF = Math.Max(0.0f, Math.Min(1.0f, gr / maxGear)); // Процент газа от максимума
            var rectGr = new RawRectangleF(rectEng.Left + 4f, rectEng.Bottom + 4f - (rectEng.Bottom - rectEng.Top) * grF, rectEng.Right - 4f, rectEng.Bottom - 4f);
            if (grF > 0.01f)
            {
                Rt?.DrawLine(new RawVector2(rectGr.Left,rectGr.Top), new RawVector2(rectGr.Right, rectGr.Top), brushCell, 6f);
            }
            Rt?.DrawText($"ДВИГАТЕЛЬ", Brushes.SysText20, new RawRectangleF(rectEng.Left + 8f, rectEng.Top - 35f, BaseWidth, BaseHeight), brushCell);
            Rt?.DrawText($"{ob:0}", Brushes.SysText34, new RawRectangleF(rectEng.Left + 25f - (ob.ToString().Length-4) * 10, rectEng.Bottom - 45f, BaseWidth, BaseHeight), Brushes.RoiGreen03);
            Rt?.DrawText(obj.Telem.CanEngineBits[4] == 2 ? "R" : obj.Telem.CanEngineBits[4] == 5 ? "N" : "D", Brushes.SysText74, new RawRectangleF(rectEng.Left + 35f, rectEng.Top + 5f, BaseWidth, BaseHeight), obj.Telem.CanEngineBits[4] == 2 ? Brushes.RoiRed03 : obj.Telem.CanEngineBits[4] == 5 ? Brushes.RoiGray03 : Brushes.RoiGreen03);
            Rt?.DrawText($"ТЕМП:{obj.Telem.CanEngineBits[3] * GameObject.GameObjectTelem.KoeffEngineTemp:0.00}°", Brushes.SysText20, new RawRectangleF(rectEng.Left + 6f, rectEng.Bottom + 10f, BaseWidth, BaseHeight), brushCell);
            Rt?.DrawText($"АКБ:{obj.Telem.CanEngineBits[1] * GameObject.GameObjectTelem.KoeffEngineAkb:0.00}V", Brushes.SysText20, new RawRectangleF(rectEng.Left + 10f, rectEng.Bottom + 40f, BaseWidth, BaseHeight), brushCell);
            Rt?.DrawText($"ГАЗ:{obj.Telem.CanEngineBits[2] * GameObject.GameObjectTelem.KoeffEngineGear:0}", Brushes.SysText20, new RawRectangleF(rectEng.Left + 30f, rectEng.Bottom + 70f, BaseWidth, BaseHeight), brushCell);

            // Отрисовка статуса бака
            var rectFuel = new RawRectangleF(cX0 + boxLw + 20f, startH - h * 4.7f, cX0 + boxLw + 150f, startH - 5f);
            Rt?.DrawRectangle(rectFuel, brushCell, 6.0f);
            var litres = obj.Telem.FuelVcurr;
            var litresF = Math.Max(0.0f, Math.Min(1.0f, obj.Telem.FuelLcurr / 255.0f)); // Процент оборотов двигателя от максимума
            var rectFuelCurr = new RawRectangleF(rectFuel.Left + 4f, rectFuel.Bottom + 4f - (rectFuel.Bottom - rectFuel.Top) * litresF, rectFuel.Right - 4f, rectFuel.Bottom - 4f);
            if (litresF >= 0.01f)
            {
                Rt?.FillRectangle(rectFuelCurr, Brushes.SysTextBrushOrange);
                Rt?.FillRectangle(rectFuelCurr, Brushes.RoiNone);
                Rt?.FillRectangle(rectFuelCurr, Brushes.RoiNone);
            }
            Rt?.DrawText($"БАК", Brushes.SysText20, new RawRectangleF(rectFuel.Left + 43f, rectFuel.Top - 35f, BaseWidth, BaseHeight), brushCell);
            Rt?.DrawText($"{litres:0}", Brushes.SysText34, new RawRectangleF(rectFuel.Left + 25f - (litres.ToString().Length - 4) * 10, rectFuel.Bottom - 45f, BaseWidth, BaseHeight), Brushes.RoiGreen03);
            Rt?.DrawText($"ТЕМП:{obj.Telem.FuelTemp:0.00}°", Brushes.SysText20, new RawRectangleF(rectFuel.Left + 6f, rectFuel.Bottom + 10f, BaseWidth, BaseHeight), brushCell);

            // Посветка кнопок
            Rt?.DrawText($"ГИРОСКОП", Brushes.SysText20, new RawRectangleF(cX0 - 192f, cY0 + 420f, BaseWidth, BaseHeight), Brushes.RoiGreen03);
            Rt?.DrawText($"КОМПАС", Brushes.SysText20, new RawRectangleF(cX0 - 50f, cY0 + 420f, BaseWidth, BaseHeight), Brushes.RoiGreen03);
            Rt?.DrawText($"ПОЗИЦИЯ", Brushes.SysText20, new RawRectangleF(cX0 + 75f, cY0 + 420f, BaseWidth, BaseHeight), Brushes.RoiGreen03);
        }
    }

    protected sealed override void DrawInfo()
    {
        lock (this)
        {
            DrawServerStatus();
        };
    }
}
