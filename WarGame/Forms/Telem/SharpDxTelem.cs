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
            var joyRect = new RawRectangleF(BaseWidth * 0.9f, BaseHeight * 0.003f, BaseWidth * 0.998f, BaseHeight * 0.498f);
            var joyColor = Core.Joystick.Alive ? Brushes.RoiGray03 : Brushes.RoiRed03;
            var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
            var yLine = (joyRect.Bottom - joyRect.Top) / Core.Joystick.Channels.Length;
            var xLine = joyRect.Right - joyRect.Left;
            int i;
            Core.FrmTelem!.ButtonVisibleChange(obj);
            if (obj != null) // Объект выбрат
            {
                joyColor = Core.Joystick.Alive ? Brushes.RoiGreen03 : joyColor;
                for (i = 0; i < obj.Telem.RcChannels.Length; i++)
                {
                    var rectLine = new RawRectangleF(joyRect.Left, joyRect.Top + (i * yLine) + 0.0f, joyRect.Right, joyRect.Top + +((i + 1) * yLine));
                    Rt?.FillRectangle(new RawRectangleF(rectLine.Left, rectLine.Top, rectLine.Left + xLine * 0.5f * (obj.Telem.RcChannels[i] + 1.0f), rectLine.Bottom), joyColor);
                    Rt?.DrawRectangle(rectLine, joyColor, 3.0f);
                    Rt?.DrawText($"CH{i + 1:00} [{obj.Telem.RcChannels[i]*500+1500:0000}]", Brushes.SysText20, new RawRectangleF(rectLine.Left + xLine * 0.22f, rectLine.Top + yLine * 0.3f, rectLine.Right, rectLine.Bottom), Brushes.SysTextBrushYellow);
                }

                for (i = 16; i < obj.Telem.Servos.Length + 16; i++)
                {
                    var rectLine = new RawRectangleF(joyRect.Left, joyRect.Top + (i * yLine) + 0.0f, joyRect.Right, joyRect.Top + +((i + 1) * yLine));
                    Rt?.FillRectangle(new RawRectangleF(rectLine.Left, rectLine.Top, rectLine.Left + xLine * (obj.Telem.Servos[i - 16]), rectLine.Bottom), Brushes.RoiYellow03);
                    Rt?.DrawRectangle(rectLine, Brushes.RoiYellow03, 3.0f);
                    Rt?.DrawText($"SERVO {i - 15:00}", Brushes.SysText20, new RawRectangleF(rectLine.Left + xLine * 0.26f, rectLine.Top + yLine * 0.3f, rectLine.Right, rectLine.Bottom), Brushes.SysTextBrushYellow);
                }

                Rt?.DrawText($"OBJ_IN: {obj.Telem.MBitObjectIn:0.000000} Mbit", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.06f, BaseHeight * 0.90f, BaseWidth, BaseHeight), Brushes.SysTextBrushRed);
                Rt?.DrawText($"SERV_IN: {obj.Telem.MBitServerIn:0.000000} Mbit", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.01f, BaseHeight * 0.92f, BaseWidth, BaseHeight), Brushes.SysTextBrushYellow);
                Rt?.DrawText($"PING<>UDP: {obj.Telem.PingToServer:0.00} ms", Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.06f, BaseHeight * 0.94f, BaseWidth, BaseHeight), Brushes.SysTextBrushWhite);
            }
            else // Объект не выбран, отображаем статус джойстика текущий

            {
                for (i = 0; i < Core.Joystick.Channels.Length; i++)
                {
                    var rectLine = new RawRectangleF(joyRect.Left, joyRect.Top + (i * yLine) + 0.0f, joyRect.Right, joyRect.Top + +((i + 1) * yLine));
                    Rt?.FillRectangle(new RawRectangleF(rectLine.Left, rectLine.Top, rectLine.Left + xLine * 0.5f * (Core.Joystick.Channels[i] + 1.0f), rectLine.Bottom), joyColor);
                    Rt?.DrawRectangle(rectLine, joyColor, 3.0f);
                    Rt?.DrawText($"CH{i + 1:00}", Brushes.SysText20, new RawRectangleF(rectLine.Left + xLine * 0.40f, rectLine.Top + yLine * 0.3f, rectLine.Right, rectLine.Bottom), Brushes.SysTextBrushYellow);
                }
            }
            Rt?.DrawText(Core.ClientName, Brushes.SysText14, new RawRectangleF(joyRect.Left + xLine * 0.08f, BaseHeight * 0.97f, BaseWidth, BaseHeight), Brushes.SysTextBrushDarkGreen);
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
