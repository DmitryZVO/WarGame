//using OpenCvSharp;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using SharpDX.WIC;
using WarGame.Model;
using WarGame.Resources;

namespace WarGame.Forms.Video;
public sealed partial class FormVideo : Form
{

    private System.Drawing.Point _posFromDisplays;
    private readonly SharpDxVideo _dx;
    public FormVideo(System.Drawing.Point pos, int fps)
    {
        InitializeComponent();

        _posFromDisplays = pos;

        _dx = new SharpDxVideo(pictureBoxMain, fps);
        Icon = EmbeddedResources.Get<Icon>("Sprites.WarGame.ico");

        Shown += FormShown;
        Closed += FormOnClosing;
        button360.Click += Button360_Click;
        buttonFrwd.Click += ButtonFrwd_Click;
        buttonPtz.Click += ButtonPtz_Click;
        //trackBarAngle.Value = 178;
    }

    private async Task Timer100StartAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(1000, ct);

            using var mat0 = GetCameraAsync(0, ct).Result ?? new(); // PTZ
            lock (_dx)
            {
                _dx.Camera0?.Dispose();
                _dx.Camera0 = _dx.CreateDxBitmap(mat0);
            }

            using var mat1 = GetCameraAsync(1, ct).Result ?? new(); // Forward
            Point2f[] srcPoints1 = [new Point2f(0, 100), new Point2f(1280, 100), new Point2f(0, 720), new Point2f(1280, 720)];
            Point2f[] dstPoints1 = [new Point2f(0, 0), new Point2f(1280, 0), new Point2f(500, 720), new Point2f(780, 720)];
            var matAff1 = Cv2.GetPerspectiveTransform(srcPoints1, dstPoints1);
            using var mat11 = new Mat();
            Cv2.WarpPerspective(mat1, mat11, matAff1, mat11.Size());

            lock (_dx)
            {
                _dx.Camera1?.Dispose();
                _dx.Camera1 = _dx.CreateDxBitmap(mat11);
            }

            using var mat2 = GetCameraAsync(2, ct).Result ?? new(); // Left
            lock (_dx)
            {
                Cv2.Rotate(mat2, mat2, RotateFlags.Rotate90Counterclockwise);
                _dx.Camera2?.Dispose();
                _dx.Camera2 = _dx.CreateDxBitmap(mat2);
            }

            using var mat3 = GetCameraAsync(3, ct).Result ?? new(); // Right
            lock (_dx)
            {
                Cv2.Rotate(mat3, mat3, RotateFlags.Rotate90Clockwise);
                _dx.Camera3?.Dispose();
                _dx.Camera3 = _dx.CreateDxBitmap(mat3);
            }

            using var mat4 = GetCameraAsync(4, ct).Result ?? new();
            Point2f[] srcPoints4 = [new Point2f(0, 100), new Point2f(1280, 100), new Point2f(0, 720), new Point2f(1280, 720)];
            Point2f[] dstPoints4 = [new Point2f(0, 0), new Point2f(1280, 0), new Point2f(500, 720), new Point2f(780, 720)];
            var matAff4 = Cv2.GetPerspectiveTransform(srcPoints4, dstPoints4);
            using var mat44 = new Mat();
            Cv2.WarpPerspective(mat4, mat44, matAff4, new OpenCvSharp.Size(1280, 720));
            lock (_dx)
            {
                Cv2.Rotate(mat44, mat44, RotateFlags.Rotate180);
                _dx.Camera4?.Dispose();
                _dx.Camera4 = _dx.CreateDxBitmap(mat44);
            }
        }
    }

    private void ButtonPtz_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 2;
        ButtonCheck();
    }

    private void ButtonFrwd_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 1;
        ButtonCheck();
    }

    private void Button360_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 0;
        ButtonCheck();
    }

    private void FormOnClosing(object? sender, EventArgs e)
    {
        _dx.Dispose();
        Core.FrmMap?.Close();
    }

    private void FormShown(object? sender, EventArgs e)
    {
        StartPosition = FormStartPosition.Manual; // Ручная позиция окна
        Location = _posFromDisplays; // Поместить в нужный монитор

        _ = _dx.StartAsync(default);
        _ = Timer100StartAsync(default);
        ButtonCheck();
    }

    private void ButtonCheck()
    {
        button360.BackColor = _dx.CameraType == 0 ? Color.LightGreen : Color.White;
        buttonFrwd.BackColor = _dx.CameraType == 1 ? Color.LightGreen : Color.White;
        buttonPtz.BackColor = _dx.CameraType == 2 ? Color.LightGreen : Color.White;
    }

    public static async Task<OpenCvSharp.Mat?> GetCameraAsync(int number, CancellationToken ct = default)
    {
        try
        {
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Core.Config.ServerUrl);
            using var answ = web.GetAsync($"GetCamera?number={number:0}", ct).Result;
            return !answ.IsSuccessStatusCode ? null : OpenCvSharp.Cv2.ImDecode(Convert.FromBase64String(await answ.Content.ReadAsStringAsync(ct)), OpenCvSharp.ImreadModes.Unchanged);
        }
        catch
        {
            return null;
        }
    }

}
