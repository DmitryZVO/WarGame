﻿using OpenCvSharp;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;
using WarGame.Model;
using Bitmap = SharpDX.Direct2D1.Bitmap;
using Rectangle = System.Drawing.Rectangle;

namespace WarGame.Forms;

public abstract class SharpDx : IDisposable
{
    protected int FpsTarget; // сжелаемое FPS
    protected int FpsScrC; // счетчик кадров экрана
    protected int FpsOcvC; // счетчик кадров техзрения

    protected readonly SharpDX.Direct3D11.Device Device;
    protected SwapChain? SwapChain;
    protected readonly Surface D2dSurface;
    protected readonly SharpDX.DXGI.Factory D2DFactory;
    protected readonly SharpDX.Direct2D1.Factory D2dFactory;
    public RenderTarget? Rt;
    protected readonly Texture2D BackBuffer;
    protected readonly RenderTargetView RenderView;
    protected readonly SharpDX.Direct3D11.DeviceContext Context;
    protected readonly SharpDX.DirectWrite.Factory DWf;
    protected readonly PixelFormat PixelFormat;
    protected readonly PictureBox FormTarget;
    public readonly DefBrushes Brushes;
    protected double FpsScr; // текущая FPS экрана
    protected double FpsOcv; // текущая FPS экрана
    protected readonly SpritesDb Sprites; // Спрайты
    protected Bitmap FrameVideo; // Видеокадр
    public int BaseWidth;
    public int BaseHeight;

    protected RawMatrix3x2 ZeroTransform = new(1, 0, 0, 1, 0, 0);
    private bool _closed;

    public virtual void FrameUpdate(Mat frame)
    {
        var temp = CreateDxBitmap(frame);
        if (temp is null) return;

        lock (this)
        {
            FrameVideo.Dispose();
            FrameVideo = temp;
        }

        FpsOcvC++;

        RenderCallback();
    }

    protected void TransformSet(RawMatrix3x2 matrix)
    {
        lock (this)
        {
            if (Rt != null) Rt.Transform = matrix;
        }
    }

    protected RawMatrix3x2 TransformGet()
    {
        lock (this)
        {
            return Rt?.Transform ?? new RawMatrix3x2();
        }
    }

    protected PathGeometry PathGeometryGet()
    {
        lock (this)
        {
            return new PathGeometry(D2dFactory);
        }
    }

    public class DefBrushes(SharpDx sdx)
    {
        public TextFormat SysText14 = new(sdx.DWf, "Arial", 14);
        public TextFormat SysText20 = new(sdx.DWf, "Arial", 20);
        public TextFormat SysText34 = new(sdx.DWf, "Arial", 34);
        public TextFormat SysText74 = new(sdx.DWf, "Arial", 74);
        public TextFormat SysText104 = new(sdx.DWf, "Arial", 104);
        public SharpDX.Direct2D1.Brush SysTextBrushBlue = new SolidColorBrush(sdx.Rt,
                new RawColor4(0.3F, 0.3F, 1.0F, 1.0F));
        public SharpDX.Direct2D1.Brush SysTextBrushOrange = new SolidColorBrush(sdx.Rt,
                new RawColor4(1.0F, 0.5F, 0.0F, 0.9F));
        public SharpDX.Direct2D1.Brush SysTextBrushYellow = new SolidColorBrush(sdx.Rt,
                new RawColor4(1.0F, 1.0F, 0.0F, 0.9F));
        public SharpDX.Direct2D1.Brush SysTextBrushRed = new SolidColorBrush(sdx.Rt,
                new RawColor4(1.0F, 0.0F, 0.0F, 0.9F));
        public SharpDX.Direct2D1.Brush SysTextBrushDarkGreen = new SolidColorBrush(sdx.Rt,
                new RawColor4(0.0F, 0.5F, 0.0F, 0.9F));
        public SharpDX.Direct2D1.Brush SysTextBrushGreen = new SolidColorBrush(sdx.Rt,
                new RawColor4(0.0F, 1.0F, 0.0F, 0.9F));
        public SharpDX.Direct2D1.Brush SysTextBrushWhite = new SolidColorBrush(sdx.Rt,
                new RawColor4(1.0F, 1.0F, 1.0F, 0.9F));
        public SharpDX.Direct2D1.Brush SysTextBrushGray = new SolidColorBrush(sdx.Rt,
                new RawColor4(0.3F, 0.3F, 0.3F, 0.9F));
        public SharpDX.Direct2D1.Brush SysTextBrushBlack = new SolidColorBrush(sdx.Rt,
                new RawColor4(0.0F, 0.0F, 0.0F, 0.9F));
        public SharpDX.Direct2D1.Brush RoiNone = new SolidColorBrush(sdx.Rt,
                new RawColor4(0.0F, 0.0F, 0.0F, 0.5F));
        public SharpDX.Direct2D1.Brush RoiHq = new SolidColorBrush(sdx.Rt, new RawColor4(1.0F, 1.0F, 0.0F, 0.1F));
        public SharpDX.Direct2D1.Brush RoiRed01 = new SolidColorBrush(sdx.Rt,
                new RawColor4(1.0F, 0.0F, 0.0F, 0.1F));
        public SharpDX.Direct2D1.Brush RoiGreen01 = new SolidColorBrush(sdx.Rt,
                new RawColor4(0.0F, 1.0F, 0.0F, 0.1F));
        public SharpDX.Direct2D1.Brush RoiGray01 = new SolidColorBrush(sdx.Rt,
                new RawColor4(1.0F, 1.0F, 1.0F, 0.1F));
        public SharpDX.Direct2D1.Brush RoiYellow01 = new SolidColorBrush(sdx.Rt,
                new RawColor4(1.0F, 1.0F, 0.0F, 0.1F));
        public SharpDX.Direct2D1.Brush RoiRed02 = new SolidColorBrush(sdx.Rt,
                new RawColor4(1.0F, 0.0F, 0.0F, 0.2F));
        public SharpDX.Direct2D1.Brush RoiGreen02 = new SolidColorBrush(sdx.Rt,
                new RawColor4(0.0F, 1.0F, 0.0F, 0.2F));
        public SharpDX.Direct2D1.Brush RoiGray02 = new SolidColorBrush(sdx.Rt,
                new RawColor4(1.0F, 1.0F, 1.0F, 0.2F));
        public SharpDX.Direct2D1.Brush RoiBlue02 = new SolidColorBrush(sdx.Rt,
                new RawColor4(0.0F, 0.0F, 1.0F, 0.2F));
        public SharpDX.Direct2D1.Brush RoiYellow02 = new SolidColorBrush(sdx.Rt,
                new RawColor4(1.0F, 1.0F, 0.0F, 0.2F));
        public SharpDX.Direct2D1.Brush RoiRed03 = new SolidColorBrush(sdx.Rt,
                new RawColor4(1.0F, 0.0F, 0.0F, 0.3F));
        public SharpDX.Direct2D1.Brush RoiGreen03 = new SolidColorBrush(sdx.Rt,
                new RawColor4(0.0F, 1.0F, 0.0F, 0.3F));
        public SharpDX.Direct2D1.Brush RoiGray03 = new SolidColorBrush(sdx.Rt,
                new RawColor4(1.0F, 1.0F, 1.0F, 0.3F));
        public SharpDX.Direct2D1.Brush RoiYellow03 = new SolidColorBrush(sdx.Rt,
                new RawColor4(1.0F, 1.0F, 0.0F, 0.3F));

        public void Dispose()
        {
            SysText14.Dispose();
            SysText20.Dispose();
            SysText34.Dispose();
            SysText74.Dispose();
            SysText104.Dispose();
            SysTextBrushBlue.Dispose();
            SysTextBrushOrange.Dispose();
            SysTextBrushRed.Dispose();
            SysTextBrushYellow.Dispose();
            SysTextBrushDarkGreen.Dispose();
            SysTextBrushGreen.Dispose();
            SysTextBrushWhite.Dispose();
            SysTextBrushGray.Dispose();
            SysTextBrushBlack.Dispose();
            RoiNone.Dispose();
            RoiHq.Dispose();
            RoiRed01.Dispose();
            RoiGreen01.Dispose();
            RoiGray01.Dispose();
            RoiYellow01.Dispose();
            RoiRed02.Dispose();
            RoiBlue02.Dispose();
            RoiGreen02.Dispose();
            RoiGray02.Dispose();
            RoiYellow02.Dispose();
            RoiRed03.Dispose();
            RoiGreen03.Dispose();
            RoiGray03.Dispose();
            RoiYellow03.Dispose();
        }
    }

    protected SharpDx(PictureBox surface, int fpsTarget, SpritesDb sprites, int widthVirtual) // Инициализация класса
    {
        var scale = widthVirtual / (float)surface.Size.Width;
        FormTarget = surface;
        FpsTarget = fpsTarget;
        BaseWidth = (int)(surface.Width * scale);
        BaseHeight = (int)(surface.Height * scale);
        PixelFormat = new PixelFormat(Format.R8G8B8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Ignore);

        ////////////////// Инициализация Direct3D
        var bufferDescription = new ModeDescription()
        {
            Width = BaseWidth, // Ширина
            Height = BaseHeight, // Высота
            RefreshRate = new Rational(FpsTarget, 0), // Частота обновления изображения
            Format = Format.R8G8B8A8_UNorm // Формат пикселей в буфере
        };
        var swapChainDesc = new SwapChainDescription() // Структура которая инициализирует DirectX 11, 
        {
            BufferCount = 1, // Количество буферов
            ModeDescription = bufferDescription, // Описание буфера
            IsWindowed = false, // Режим отображения окно/полный экран
            OutputHandle = surface.Handle, // Ссылка на заголовок формы рендеринга
            SampleDescription = new SampleDescription(1, 0), // ????
            SwapEffect = SwapEffect.Discard, // ????
            Usage = Usage.BackBuffer | Usage.RenderTargetOutput, // Куда выводить 
            Flags = SwapChainFlags.None, // Флаги ??
        };
        SharpDX.Direct3D11.Device.CreateWithSwapChain( // Инициализируем Direct3D11
            DriverType.Hardware, // Использовать ускорение видеодаптера
            DeviceCreationFlags.BgraSupport, // Поддержка DirectDraw
            swapChainDesc, // Структура описывающия инициализацию DirectX
            out Device, // Куда выводить
            out SwapChain); // Ссылка на буфер
        BackBuffer = SharpDX.Direct3D11.Resource.FromSwapChain<Texture2D>(SwapChain, 0);
        RenderView = new RenderTargetView(Device, BackBuffer);
        Context = Device.ImmediateContext;
        ////////////////// Инициализация Direct2D
        D2dFactory = new SharpDX.Direct2D1.Factory(); // Создаем фактуру Direct2D
        D2DFactory = SwapChain.GetParent<SharpDX.DXGI.Factory>();
        D2DFactory.MakeWindowAssociation(surface.Handle, WindowAssociationFlags.IgnoreAll);
        D2dSurface = BackBuffer.QueryInterface<Surface>();
        Rt = new RenderTarget(D2dFactory, D2dSurface,
            new RenderTargetProperties
            {
                MinLevel = SharpDX.Direct2D1.FeatureLevel.Level_DEFAULT,
                PixelFormat = PixelFormat,
                Type = RenderTargetType.Hardware,
                Usage = RenderTargetUsage.ForceBitmapRemoting
            });
        Context.OutputMerger.SetRenderTargets(RenderView);
        DWf = new SharpDX.DirectWrite.Factory();

        /////////////// ================ ////////////////////////
        Brushes = new DefBrushes(this);
        Sprites = sprites;
        Sprites.LoadBitmap(this);

        FrameVideo = new Bitmap(
            Rt,
            new Size2(BaseWidth, BaseHeight),
            new BitmapProperties
            { PixelFormat = PixelFormat });
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        _ = MathFpsAsync(cancellationToken);

        if (FpsTarget <= 0) return;

        while (!cancellationToken.IsCancellationRequested)
        {
            if (_closed) break;
            await Task.Delay(1000 / FpsTarget, cancellationToken);
            RenderCallback();
        }
    }

    private async Task MathFpsAsync(CancellationToken cancellationToken = default) // Таймер пересчета FPS
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            if (_closed) break;
            await Task.Delay(1000, cancellationToken);

            FpsScr = FpsScrC;
            FpsScrC = 0;
            FpsOcv = FpsOcvC;
            FpsOcvC = 0;
        }
    }

    public Bitmap? CreateDxBitmap(System.Drawing.Bitmap sbm)
    {
        lock (this)
        {
            if (Rt is null) return null;
            try
            {
                var bmpData = sbm.LockBits(new Rectangle(0, 0, sbm.Width, sbm.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                var stream = new DataStream(bmpData.Scan0, bmpData.Stride * bmpData.Height, true, false);
                var pFormat = new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied);
                var bmpProps = new BitmapProperties(pFormat);

                var result =
                    new Bitmap(
                        Rt,
                        new Size2(sbm.Width, sbm.Height),
                        stream,
                        bmpData.Stride,
                        bmpProps);

                sbm.UnlockBits(bmpData);
                stream.Dispose();
                return result;
            }
            catch
            {
                return new Bitmap(Rt, new Size2(sbm.Width, sbm.Height));
            }
        }
    }

    public Bitmap? CreateDxBitmap(Mat mat)
    {
        if (mat.Empty()) return null;

        lock (this)
        {
            if (Rt is null) return null;
            try
            {
                switch (mat.Channels())
                {
                    case 1:
                        Cv2.CvtColor(mat, mat, ColorConversionCodes.GRAY2RGBA);
                        break;
                    case 3:
                        Cv2.CvtColor(mat, mat, ColorConversionCodes.BGR2RGBA);
                        break;
                    case 4:
                        Cv2.CvtColor(mat, mat, ColorConversionCodes.BGRA2RGBA);
                        break;
                }

                using var stream = new DataStream(mat.Data, mat.Cols * mat.Rows * 4, true, false);
                var bmpProps = new BitmapProperties(PixelFormat);

                var result =
                    new Bitmap(
                        Rt,
                        new Size2(mat.Cols, mat.Rows),
                        stream,
                        mat.Cols * 4,
                        bmpProps);
                return result;
            }
            catch
            {
                return new Bitmap(Rt, new Size2(mat.Width, mat.Height));
            }
        }
    }

    public Mat GetBitmapFromSharpDxRender()
    {
        Mat ret;

        lock (this)
        {
            var desc = BackBuffer.Description;
            desc.CpuAccessFlags = CpuAccessFlags.Read;
            desc.Usage = ResourceUsage.Staging;
            desc.OptionFlags = ResourceOptionFlags.None;
            desc.BindFlags = BindFlags.None;

            using var texture = new Texture2D(Device, desc);
            Context.CopyResource(BackBuffer, texture);

            using var surface = texture.QueryInterface<Surface>();
            surface.Map(SharpDX.DXGI.MapFlags.Read, out var dataStream);

            using (var mat = Mat.FromPixelData(BaseHeight, BaseWidth, MatType.CV_8UC4, dataStream.DataPointer))
            {
                Cv2.CvtColor(mat, mat, ColorConversionCodes.RGBA2BGR);
                ret = mat.Clone();
            }

            surface.Unmap();
            dataStream.Dispose();
        }

        return ret;
    }

    public void Dispose() // Освобождение ресурсов
    {
        lock (this)
        {
            _closed = true;
            Brushes.Dispose();
            RenderView.Dispose();
            BackBuffer.Dispose();
            Device.Dispose();
            Context.Dispose();
            SwapChain?.Dispose();
            SwapChain = null;
            D2dSurface.Dispose();
            D2DFactory.Dispose();
            D2dFactory.Dispose();
            FrameVideo.Dispose();
            Sprites.Items.ToList().ForEach(x=>x.Value.Dispose());
            Rt?.Dispose();
            Rt = null;
            DWf.Dispose();
        }

        GC.SuppressFinalize(this);
    }

    protected virtual void FirstRun()
    {
    }

    public void RenderCallback() // Цикл отрисовки изображений в окне камеры
    {
        lock (this)
        {
            Rt?.BeginDraw();
            DrawUser(); // Вывод пользовательской графики
            DrawInfo(); // Вывод статистики
            Rt?.EndDraw();
            try
            {
                SwapChain?.Present(0, PresentFlags.None);
            }
            catch
            {
                //
            }
        }
        FpsScrC++;
    }

    protected virtual void DrawServerStatus()
    {
        Rt?.DrawRectangle(new RawRectangleF(0, 0, BaseWidth, BaseHeight), Core.Server.Alive ? Brushes.RoiGreen03 : Brushes.RoiRed03, 8f);
        if (!Core.Server.Alive)
        {
            Rt?.FillRectangle(new RawRectangleF(0, 0, BaseWidth, BaseHeight), Brushes.RoiRed01);
            var rect = new RawRectangleF(BaseWidth * 0.33f, BaseHeight * 0.505f, BaseWidth * 0.663f, BaseHeight * 0.565f);
            Rt?.FillRectangle(rect, Brushes.RoiNone);
            Rt?.DrawRectangle(rect, Brushes.RoiRed03, 6f);
            Rt?.DrawText($"СЕРВЕР НЕ ДОСТУПЕН", Brushes.SysText74, rect, Brushes.RoiRed03);
        }
    }

    protected virtual void DrawUser()
    {
        Rt?.Clear(new RawColor4(0, 0, 0, 1));
    }

    protected virtual void DrawInfo()
    {
        Rt?.DrawText(
            "[FPS] ЭКРАН: " + FpsScr.ToString("0") + ", ТЕХ. ЗРЕНИЕ: " + FpsOcv.ToString("0"),
            Brushes.SysText14,
            new RawRectangleF(10, 10, BaseWidth, BaseHeight),
            Brushes.SysTextBrushYellow);
    }
    public abstract class SpritesDb
    {
        public Dictionary<string, Bitmap> Items { get; set; } = [];

        public virtual void DisposeBitmap()
        {
            foreach (var i in Items)
            {
                i.Value.Dispose();
            }
        }

        public virtual void LoadBitmap(SharpDx sdx)
        {
        }
    }
}

