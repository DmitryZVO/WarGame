using OpenCvSharp;
using System.Text.Json;
using WarGame.Forms.Map;
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
        buttonFrwd.Click += ButtonFrwd_Click;
        buttonBack.Click += ButtonBack_Click;
        buttonLeft.Click += ButtonLeft_Click;
        buttonRight.Click += ButtonRight_Click;
        buttonPtz.Click += ButtonPtz_Click;
        buttonWarm.Click += ButtonWarm_Click;
        buttonFpv1.Click += ButtonFpv1_Click;
        buttonFpv2.Click += ButtonFpv2_Click;
        buttonFpv3.Click += ButtonFpv3_Click;
        buttonFpv4.Click += ButtonFpv4_Click;
        buttonPtzRight.MouseDown += ButtonPtzRight_MouseDown;
        buttonPtzRight.MouseUp += ButtonPtzStop;
        buttonPtzLeft.MouseDown += ButtonPtzLeft_MouseDown;
        buttonPtzLeft.MouseUp += ButtonPtzStop;
        buttonPtzUp.MouseDown += ButtonPtzUp_MouseDown;
        buttonPtzUp.MouseUp += ButtonPtzStop;
        buttonPtzDown.MouseDown += ButtonPtzDown_MouseDown;
        buttonPtzDown.MouseUp += ButtonPtzStop;
        buttonPtzZoomIn.MouseDown += ButtonPtzZoomIn_MouseDown;
        buttonPtzZoomIn.MouseUp += ButtonPtzStop;
        buttonPtzZoomOut.MouseDown += ButtonPtzZoomOut_MouseDown;
        buttonPtzZoomOut.MouseUp += ButtonPtzStop;
    }

    private async void ButtonPtzRight_MouseDown(object? sender, MouseEventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected);
        if (obj == null) return;

        await FormMap.ObjectsGame.UpdatePtzAsync(obj, 1);
    }
    private async void ButtonPtzLeft_MouseDown(object? sender, MouseEventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected);
        if (obj == null) return;

        await FormMap.ObjectsGame.UpdatePtzAsync(obj, 2);
    }
    private async void ButtonPtzUp_MouseDown(object? sender, MouseEventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected);
        if (obj == null) return;

        await FormMap.ObjectsGame.UpdatePtzAsync(obj, 4);
    }
    private async void ButtonPtzDown_MouseDown(object? sender, MouseEventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected);
        if (obj == null) return;

        await FormMap.ObjectsGame.UpdatePtzAsync(obj, 8);
    }
    private async void ButtonPtzZoomIn_MouseDown(object? sender, MouseEventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected);
        if (obj == null) return;

        await FormMap.ObjectsGame.UpdatePtzAsync(obj, 16);
    }
    private async void ButtonPtzZoomOut_MouseDown(object? sender, MouseEventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected);
        if (obj == null) return;

        await FormMap.ObjectsGame.UpdatePtzAsync(obj, 32);
    }

    private async void ButtonPtzStop(object? sender, MouseEventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected);
        if (obj == null) return;

        await FormMap.ObjectsGame.UpdatePtzAsync(obj, 0);
    }

    private async Task Timer100StartAsync(CancellationToken ct)
    {
        var count = 0;
        var fps = 0;
        var time = DateTime.Now;
        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(20, ct);

            var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected);
            if (obj == null)
            {
                _dx.CameraType = -1;
                ButtonCheck();
                continue;
            }
            if (_dx.CameraType == -1)
            {
                _dx.CameraType = 0;
                ButtonCheck();
            }

            var start = DateTime.Now;
            using var mat0 = await GetCameraAsync(obj.Id, _dx.CameraType, ct) ?? new();
            var ms = (DateTime.Now - start).TotalMilliseconds;
            lock (_dx)
            {
                if (mat0.Height <= 0 | mat0.Width <= 0) continue;
                count++;
                //mat0.Rectangle(new OpenCvSharp.Point(0, 30), new OpenCvSharp.Point((int)(mat0.Width * 0.24), (int)(mat0.Height  * 0.025+30)), Scalar.Gray, -1);
                //mat0.PutText($"CAM{obj.Id:0} FPS={fps:0}, ms={ms:0.00}", new OpenCvSharp.Point(5, (int)(mat0.Height * 0.02 + 30)), HersheyFonts.HersheyComplexSmall, 0.8d, Scalar.White);

                var curr = DateTime.Now;
                if ((curr - time).TotalMilliseconds > 1000)
                {
                    time = curr;
                    fps = count;
                    count = 0;
                }

                _dx.CameraFrame?.Dispose();
                _dx.CameraFrame = _dx.CreateDxBitmap(mat0);
            }
        }
    }

    private void ButtonPtz_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 0;
        ButtonCheck();
    }
    private void ButtonWarm_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 1;
        ButtonCheck();
    }
    private void ButtonFrwd_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 2;
        ButtonCheck();
    }

    private void ButtonBack_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 3;
        ButtonCheck();
    }
    private void ButtonLeft_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 4;
        ButtonCheck();
    }
    private void ButtonRight_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 5;
        ButtonCheck();
    }
    private void ButtonFpv1_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 6;
        ButtonCheck();
    }
    private void ButtonFpv2_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 7;
        ButtonCheck();
    }
    private void ButtonFpv3_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 8;
        ButtonCheck();
    }
    private void ButtonFpv4_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 9;
        ButtonCheck();
    }

    private void FormOnClosing(object? sender, EventArgs e)
    {
        _dx.Dispose();
        Core.FrmMap?.Close();
    }

    private void FormShown(object? sender, EventArgs e)
    {
        StartPosition = FormStartPosition.Manual;
        Location = _posFromDisplays;

        _ = _dx.StartAsync(default);
        _ = Timer100StartAsync(default);
        ButtonCheck();
    }

    private void ButtonCheck()
    {
        buttonPtz.BackColor = _dx.CameraType == 0 ? Color.LightGreen : Color.White;
        buttonWarm.BackColor = _dx.CameraType == 1 ? Color.LightGreen : Color.White;
        buttonFrwd.BackColor = _dx.CameraType == 2 ? Color.LightGreen : Color.White;
        buttonBack.BackColor = _dx.CameraType == 3 ? Color.LightGreen : Color.White;
        buttonLeft.BackColor = _dx.CameraType == 4 ? Color.LightGreen : Color.White;
        buttonRight.BackColor = _dx.CameraType == 5 ? Color.LightGreen : Color.White;
        buttonFpv1.BackColor = _dx.CameraType == 6 ? Color.LightGreen : Color.White;
        buttonFpv2.BackColor = _dx.CameraType == 7 ? Color.LightGreen : Color.White;
        buttonFpv3.BackColor = _dx.CameraType == 8 ? Color.LightGreen : Color.White;
        buttonFpv4.BackColor = _dx.CameraType == 9 ? Color.LightGreen : Color.White;
    }

    public static async Task<Mat?> GetCameraAsync(int id, int number, CancellationToken ct = default)
    {
        try
        {
            var start0 = DateTime.Now;
            using var web = new HttpClient();
            web.BaseAddress = new Uri(Core.Config.ServerUrl);
            using var answ = web.GetAsync($"GetCamera?id={id:0}&number={number:0}", ct).Result;
            var s0 = (DateTime.Now - start0).TotalMilliseconds; //210ms
            var start1 = DateTime.Now;
            var deserializedArray = JsonSerializer.Deserialize<byte[]>(await answ.Content.ReadAsStringAsync(ct));
            //var ret = 
            var s1 = (DateTime.Now - start1).TotalMilliseconds; //10ms
            return !answ.IsSuccessStatusCode ? null : Cv2.ImDecode(deserializedArray!, ImreadModes.Unchanged);
        }
        catch
        {
            return null;
        }
    }

}
