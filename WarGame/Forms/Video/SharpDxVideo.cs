using OpenCvSharp;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using System.Drawing.Imaging;
using WarGame.Forms.Map;
using WarGame.Resources;

namespace WarGame.Forms.Video;

internal class SharpDxVideo(PictureBox surfacePtr, int fpsTarget) : SharpDx(surfacePtr, fpsTarget, new Sprites(), 1920)
{
    public bool NotActive { get; set; }
    public int CameraType { get; set; } = 0;
    public SharpDX.Direct2D1.Bitmap? CameraFrame { get; set; }

    protected sealed override void DrawUser()
    {
        lock (this)
        {
            Rt?.Clear(new RawColor4(0.0f, 0, 0, 1));
            var fs = new RawRectangleF(0, 0, BaseWidth, BaseHeight);
            var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
            if (obj == null)
            {
                Rt?.DrawText($"ОБЪЕКТ НЕ ВЫБРАН", Brushes.SysText104, new RawRectangleF(BaseWidth * 0.25f, BaseHeight * 0.45f, BaseWidth, BaseHeight), Brushes.RoiGreen03);
                return;
            }
            if (CameraFrame == null) return;
            switch (CameraType)
            {
                case -1:
                    break;
                default:
                    Rt?.DrawBitmap(CameraFrame, fs, 1.0f, BitmapInterpolationMode.Linear);
                    break;
            }
        }
    }

    public Texture2D CreateTexture2DFromBitmap(System.Drawing.Bitmap bitmap)
    {
        // 1. Lock Bitmap Bits
        BitmapData bitmapData = bitmap.LockBits(
            new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
            ImageLockMode.ReadOnly,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb // Assuming ARGB format for simplicity
        );

        try
        {
            // 2. Create DataStream
            DataStream dataStream = new(
                bitmapData.Scan0,
                bitmapData.Stride * bitmap.Height, // Total size in bytes
                true, // Can read
                false // Cannot write
            );

            // 3. Create Texture2D Description
            Texture2DDescription textureDesc = new()
            {
                Width = bitmap.Width,
                Height = bitmap.Height,
                MipLevels = 1,
                ArraySize = 1,
                Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm, // Corresponds to Format32bppArgb
                SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                Usage = ResourceUsage.Immutable, // Or Dynamic, depending on your needs
                BindFlags = BindFlags.ShaderResource,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None
            };

            // 4. Create Texture2D
            Texture2D texture = new(Device, textureDesc, new DataRectangle(dataStream.DataPointer, bitmapData.Stride));

            return texture;
        }
        finally
        {
            // 5. Unlock Bitmap Bits
            bitmap.UnlockBits(bitmapData);
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
