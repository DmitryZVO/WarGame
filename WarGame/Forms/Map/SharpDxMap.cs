using WarGame.Resources;

namespace WarGame.Forms.Map;

internal class SharpDxMap(PictureBox surfacePtr, int fpsTarget) : SharpDx(surfacePtr, fpsTarget, new Sprites(), 2560)
{
    public bool NotActive { get; set; }

    protected sealed override void DrawUser()
    {
        lock (this)
        {
            FormMap.Map.Draw(this);
            FormMap.ObjectsStatic.Draw(this);
            /*
            Rt?.Clear(new RawColor4(0, 0, 0, 1));
            var rect = new RawRectangleF(BaseWidth * 0.38f, BaseHeight * 0.505f, BaseWidth * 0.663f, BaseHeight * 0.565f);
            Rt?.DrawText($"ДАННЫЕ ОТ РЛС", Brushes.SysText74, rect, Brushes.RoiYellow03);
             */
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
