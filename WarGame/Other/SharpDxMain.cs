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
            Values.ObjectsStatic.Draw(this);
        }
    }

    protected sealed override void DrawInfo()
    {
        //base.DrawInfo();
    }
}
