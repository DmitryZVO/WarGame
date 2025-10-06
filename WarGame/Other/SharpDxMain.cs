using SharpDX.Mathematics.Interop;
using WarGame.Resources;
using WarGame.Core;

namespace WarGame.Other;

internal class SharpDxMain : SharpDx
{
    public bool NotActive { get; set; }

    public SharpDxMain(PictureBox surfacePtr, int fpsTarget) : base(surfacePtr, fpsTarget, new Sprites(), 2560)
    {
    }

    protected sealed override void DrawUser()
    {
        lock (this)
        {
            Values.Map.Draw(this);
            Rt?.DrawRectangle(new RawRectangleF(0, 0, BaseWidth, BaseHeight), Values.Server.Alive ? Brushes.RoiGreen03 : Brushes.RoiRed03, 8f);
            Values.ObjectsStatic.Draw(this);

            if (!Values.Server.Alive)
            {
                var rect = new RawRectangleF(BaseWidth * 0.33f, BaseHeight * 0.505f, BaseWidth * 0.663f, BaseHeight * 0.565f);
                Rt?.FillRectangle(rect, Brushes.RoiNone);
                Rt?.DrawRectangle(rect, Brushes.RoiRed03, 6f);
                Rt?.DrawText($"СЕРВЕР НЕ ДОСТУПЕН", Brushes.SysText74, rect, Brushes.RoiRed03);
            }
        }
    }

    protected sealed override void DrawInfo()
    {
        //base.DrawInfo();
    }
}
