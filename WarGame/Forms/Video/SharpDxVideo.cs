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

internal class SharpDxVideo(PictureBox surfacePtr, int fpsTarget) : SharpDx(surfacePtr, fpsTarget, new Sprites(), 2560)
{
    public bool NotActive { get; set; }
    public int CameraType { get; set; } = 0;
    public SharpDX.Direct2D1.Bitmap? Camera0 { get; set; }
    public Texture2D? Camera1Texture { get; set; }
    public SharpDX.Direct2D1.Bitmap? Camera1 { get; set; }
    public SharpDX.Direct2D1.Bitmap? Camera2 { get; set; }
    public SharpDX.Direct2D1.Bitmap? Camera3 { get; set; }
    public SharpDX.Direct2D1.Bitmap? Camera4 { get; set; }

    protected sealed override void DrawUser()
    {
        lock (this)
        {
            Rt?.Clear(new RawColor4(0.2f, 0, 0, 1));
            var rect = new RawRectangleF(BaseWidth * 0.33f, BaseHeight * 0.505f, BaseWidth * 0.763f, BaseHeight * 0.565f);
            Rt?.DrawText($"ИЗОБРАЖЕНИЕ С КАМЕРЫ", Brushes.SysText74, rect, Brushes.RoiYellow03);
            var fs = new RawRectangleF(0, 0, BaseWidth, BaseHeight);
            switch (CameraType)
            {
                default:
                case 0:// Вид 360
                    if (Camera1 == null) break;
                    //if (Camera2 == null) break;
                    //if (Camera3 == null) break;
                    if (Camera4 == null) break;

                    var fu = new RawRectangleF(BaseWidth * 0.25f, BaseHeight * 0.00f, BaseWidth * 0.75f, BaseHeight * 0.40f);
                    Rt?.DrawBitmap(Camera1, fu, 1.0f, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
                    //Rt?.DrawRectangle(fu, Brushes.SysTextBrushWhite);
                    var fd = new RawRectangleF(BaseWidth * 0.25f, BaseHeight * 0.60f, BaseWidth * 0.75f, BaseHeight * 1.00f);
                    Rt?.DrawBitmap(Camera4, fd, 1.0f, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
                    //Rt?.DrawRectangle(fd, Brushes.SysTextBrushWhite);
                    /*
                    var fu = new RawRectangleF(BaseWidth * 0.25f, BaseHeight * 0.00f, BaseWidth * 0.75f, BaseHeight * 0.25f);
                    var fl = new RawRectangleF(BaseWidth * 0.25f, BaseHeight * 0.25f, BaseWidth * 0.50f, BaseHeight * 0.75f);
                    var fr = new RawRectangleF(BaseWidth * 0.50f, BaseHeight * 0.25f, BaseWidth * 0.75f, BaseHeight * 0.75f);
                    var fd = new RawRectangleF(BaseWidth * 0.25f, BaseHeight * 0.75f, BaseWidth * 0.75f, BaseHeight * 1.00f);
                    Rt?.DrawBitmap(Camera1, fu, 1.0f, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
                    Rt?.DrawRectangle(fu, Brushes.SysTextBrushWhite);
                    Rt?.DrawBitmap(Camera4, fd, 1.0f, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
                    Rt?.DrawRectangle(fd, Brushes.SysTextBrushWhite);
                    Rt?.DrawBitmap(Camera2, fl, 1.0f, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
                    Rt?.DrawRectangle(fl, Brushes.SysTextBrushWhite);
                    Rt?.DrawBitmap(Camera3, fr, 1.0f, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
                    Rt?.DrawRectangle(fr, Brushes.SysTextBrushWhite);
                    */
                    break;
                case 1:// Курс
                    if (Camera1 == null) break;
                    Rt?.DrawBitmap(Camera1, fs, 1.0f, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
                    Rt?.DrawLine(new RawVector2(BaseWidth / 2, 0), new RawVector2(BaseWidth / 2, BaseHeight), Brushes.SysTextBrushYellow, 3);
                    Rt?.DrawLine(new RawVector2(0, BaseHeight / 2), new RawVector2(BaseWidth, BaseHeight / 2), Brushes.SysTextBrushYellow, 3);
                    break;
                case 2:// PTZ
                    if (Camera0 == null) break;
                    Rt?.DrawBitmap(Camera0, fs, 1.0f, SharpDX.Direct2D1.BitmapInterpolationMode.Linear);
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
