using SharpDX.Mathematics.Interop;
using WarGame.Forms.Map;
using WarGame.Model;
using WarGame.Resources;

namespace WarGame.Forms.Telem;

internal class SharpDxTelem(PictureBox surfacePtr, int fpsTarget) : SharpDx(surfacePtr, fpsTarget, new Sprites(), 1920)
{
    public bool NotActive { get; set; }

    protected sealed override void DrawUser()
    {
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
                Rt?.DrawText($"ВЕДЕТСЯ ЗАПИСЬ ТЕЛЕМЕТРИИ: {(obj.Telem.AliveCheck & 0x0000000080000000)>0}", Brushes.SysText20, new RawRectangleF(telemRect.Left, telemRect.Top + (sy * syT), telemRect.Right, telemRect.Bottom), (obj.Telem.AliveCheck & 0x0000000080000000) > 0 ? Brushes.SysTextBrushGreen : Brushes.SysTextBrushRed);
                sy = 2;
                syT = 30.0f;
                Rt?.DrawText($"ОБЪЕКТ: {obj.Name}", Brushes.SysText20, new RawRectangleF(telemRect.Left, telemRect.Top + (sy * syT), telemRect.Right, telemRect.Bottom), Brushes.SysTextBrushYellow); sy++;
                Rt?.DrawText($"GPS LonX: {obj.LonX:0.000000}", Brushes.SysText20, new RawRectangleF(telemRect.Left, telemRect.Top + (sy * syT), telemRect.Right, telemRect.Bottom), Brushes.SysTextBrushYellow); sy++;
                Rt?.DrawText($"GPS LatY: {obj.LatY:0.000000}", Brushes.SysText20, new RawRectangleF(telemRect.Left, telemRect.Top + (sy * syT), telemRect.Right, telemRect.Bottom), Brushes.SysTextBrushYellow); sy++;
                Rt?.DrawText($"КОМПАС: {obj.Angle:0.00} град", Brushes.SysText20, new RawRectangleF(telemRect.Left, telemRect.Top + (sy * syT), telemRect.Right, telemRect.Bottom), Brushes.SysTextBrushYellow); sy++;
                //Rt?.DrawText($"БИТЫ CAN_ENGINE: {Convert.ToString(obj.Telem.CanEngineBits[4], 2).PadLeft(8, '0')} {Convert.ToString(obj.Telem.CanEngineBits[3], 2).PadLeft(8, '0')} {Convert.ToString(obj.Telem.CanEngineBits[2], 2).PadLeft(8, '0')} {Convert.ToString(obj.Telem.CanEngineBits[1], 2).PadLeft(8, '0')} {Convert.ToString(obj.Telem.CanEngineBits[0], 2).PadLeft(8, '0')}", Brushes.SysText20, new RawRectangleF(telemRect.Left, telemRect.Top + (sy * syT), telemRect.Right, telemRect.Bottom), Brushes.SysTextBrushYellow); sy++;


                joyColor = Core.Joystick.Alive ? Brushes.RoiGreen03 : joyColor;
                for (i = 0; i < obj.Telem.RcChannels.Length; i++)
                {
                    var rectLine = new RawRectangleF(joyRect.Left, joyRect.Top + (i * yLine) + 0.0f, joyRect.Right, joyRect.Top + +((i + 1) * yLine));
                    Rt?.FillRectangle(new RawRectangleF(rectLine.Left, rectLine.Top, rectLine.Left + xLine * 0.5f * (((obj.Telem.RcChannels[i]-1500)/500.0f) + 1.0f), rectLine.Bottom), joyColor);
                    Rt?.DrawRectangle(rectLine, joyColor, 3.0f);
                    Rt?.DrawText($"CH{i + 1:00} [{obj.Telem.RcChannels[i]:0000}]", Brushes.SysText20, new RawRectangleF(rectLine.Left + xLine * 0.22f, rectLine.Top + yLine * 0.16f, rectLine.Right, rectLine.Bottom), Brushes.SysTextBrushYellow);
                }
                var colServOut = obj.Telem.MBitServerOut < 4.0f ? Brushes.SysTextBrushGreen : obj.Telem.MBitServerOut < 10.0f ? Brushes.SysTextBrushYellow : Brushes.SysTextBrushRed;
                Rt?.DrawText($"SERV_O ->: {obj.Telem.MBitServerOut:0.000000} Mbit", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.03f, BaseHeight * 0.26f, BaseWidth, BaseHeight), colServOut);
                var colObjIn = obj.Telem.MBitObjectIn < 4.0f ? Brushes.SysTextBrushGreen : obj.Telem.MBitObjectIn < 10.0f ? Brushes.SysTextBrushYellow : Brushes.SysTextBrushRed;
                Rt?.DrawText($"OBJ_I  <-: {obj.Telem.MBitObjectIn:0.000000} Mbit", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.10f, BaseHeight * 0.28f, BaseWidth, BaseHeight), colObjIn);
                var colObjOut = obj.Telem.MBitObjectOut < 4.0f ? Brushes.SysTextBrushGreen : obj.Telem.MBitObjectOut < 10.0f ? Brushes.SysTextBrushYellow : Brushes.SysTextBrushRed;
                Rt?.DrawText($"OBJ_O ->: {obj.Telem.MBitObjectOut:0.000000} Mbit", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.08f, BaseHeight * 0.30f, BaseWidth, BaseHeight), colObjOut);
                var colServIn = obj.Telem.MBitServerIn < 4.0f ? Brushes.SysTextBrushGreen : obj.Telem.MBitServerIn < 10.0f ? Brushes.SysTextBrushYellow : Brushes.SysTextBrushRed;
                Rt?.DrawText($"SERV_I  <-: {obj.Telem.MBitServerIn:0.000000} Mbit", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.04f, BaseHeight * 0.32f, BaseWidth, BaseHeight), colServIn);
                var colPing = obj.Telem.PingToServer < 10.0f ? Brushes.SysTextBrushGreen : obj.Telem.PingToServer < 100.0f ? Brushes.SysTextBrushYellow : Brushes.SysTextBrushRed;
                Rt?.DrawText($"PING SERV<>OBJ: {obj.Telem.PingToServer:###0.00} ms", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * -0.00f, BaseHeight * 0.34f, BaseWidth, BaseHeight), colPing);
                var colQ = obj.Telem.CommandCount < 10 ? Brushes.SysTextBrushGreen : obj.Telem.CommandCount < 50 ? Brushes.SysTextBrushYellow : Brushes.SysTextBrushRed;
                Rt?.DrawText($"COM_QUEUE: {obj.Telem.CommandCount:0}", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.18f, BaseHeight * 0.36f, BaseWidth, BaseHeight), colQ);
            }
            else // Объект не выбран, отображаем статус джойстика текущий
            {
                Rt?.DrawText($"ОБЪЕКТ НЕ ВЫБРАН", Brushes.SysText104, new RawRectangleF(BaseWidth * 0.25f, BaseHeight * 0.45f, BaseWidth, BaseHeight), Brushes.RoiYellow03);
            }
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
