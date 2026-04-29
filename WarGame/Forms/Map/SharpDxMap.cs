using WarGame.Remote;
using WarGame.Resources;

namespace WarGame.Forms.Map;

internal class SharpDxMap : SharpDx
{
    public SharpDX.Direct2D1.Bitmap BitmapNone;

    public SharpDxMap(PictureBox surfacePtr, int fpsTarget) : base(surfacePtr, fpsTarget, new Sprites(), 1920)
    {
        BitmapNone = CreateDxBitmap(EmbeddedResources.Get<Bitmap>("Sprites.None.png")!)!;
    }
    public bool NotActive { get; set; }

    protected sealed override async void DrawUser()
    {
        if (BitmapTank == null)
        {
            var ret = await Files.GetSpriteAsync("Sprites", "Drone0.png");
            if (ret != null) BitmapTank = CreateDxBitmap(ret);
            return;
        }

        lock (this)
        {
            FormMap.Map.Draw(this);
            FormMap.ObjectsStatic.Draw(this);
            FormMap.ObjectsGame.Draw(this);
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
