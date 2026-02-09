using SharpDX.Mathematics.Interop;
using WarGame.Model;
using WarGame.Resources;

namespace WarGame.Forms.Telem;

internal class SharpDxTelem(PictureBox surfacePtr, int fpsTarget) : SharpDx(surfacePtr, fpsTarget, new Sprites(), 2560)
{
    public bool NotActive { get; set; }

    protected sealed override void DrawUser()
    {
        lock (this)
        {
            Rt?.Clear(new RawColor4(0, 0, 0, 1));
            var joyRect = new RawRectangleF(BaseWidth * 0.9f, BaseHeight * 0.003f, BaseWidth * 0.998f, BaseHeight * 0.498f);
            var joyColor = Core.Joystick.Alive ? Brushes.RoiGreen03 : Brushes.RoiRed03;
            var yLine = (joyRect.Bottom - joyRect.Top) / Core.Joystick.Channels.Length;
            var xLine = joyRect.Right - joyRect.Left;
            for (var i = 0; i < Core.Joystick.Channels.Length; i++)
            {
                var rectLine = new RawRectangleF(joyRect.Left, joyRect.Top + (i * yLine) + 0.0f, joyRect.Right, joyRect.Top + +((i + 1) * yLine));
                Rt?.FillRectangle(new RawRectangleF(rectLine.Left, rectLine.Top, rectLine.Left + xLine * 0.5f * (Core.Joystick.Channels[i] + 1.0f), rectLine.Bottom), joyColor);
                Rt?.DrawRectangle(rectLine, joyColor, 3.0f);
                Rt?.DrawText($"CH{i + 1:00}", Brushes.SysText20, new RawRectangleF(rectLine.Left + xLine * 0.40f, rectLine.Top + yLine * 0.3f, rectLine.Right, rectLine.Bottom), Brushes.SysTextBrushYellow);
            }
            Rt?.DrawText(Core.ClientName, Brushes.SysText20, new RawRectangleF(joyRect.Left + xLine * 0.22f, BaseHeight * 0.98f, BaseWidth, BaseHeight), Brushes.SysTextBrushDarkGreen);
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
