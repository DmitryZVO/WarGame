using SharpDX.Mathematics.Interop;
using WarGame.Resources;

namespace WarGame.Forms.Video;

internal class SharpDxVideo(PictureBox surfacePtr, int fpsTarget) : SharpDx(surfacePtr, fpsTarget, new Sprites(), 2560)
{
    public bool NotActive { get; set; }

    protected sealed override void DrawUser()
    {
        lock (this)
        {
            Rt?.Clear(new RawColor4(0, 0, 0, 1));
            var rect = new RawRectangleF(BaseWidth * 0.33f, BaseHeight * 0.505f, BaseWidth * 0.763f, BaseHeight * 0.565f);
            Rt?.DrawText($"ИЗОБРАЖЕНИЕ С КАМЕРЫ", Brushes.SysText74, rect, Brushes.RoiYellow03);

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
