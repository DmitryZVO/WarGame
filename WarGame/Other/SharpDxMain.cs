using SharpDX.Mathematics.Interop;
using WarGame.Resources;

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
            Rt?.Clear(new RawColor4(0.0f, 0.0f, 0.0f, 0.0f));
        }
    }

    protected sealed override void DrawInfo()
    {
        base.DrawInfo();
    }
}
