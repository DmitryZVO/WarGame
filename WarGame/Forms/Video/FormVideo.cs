using OpenCvSharp;
using System.Text.Json;
using WarGame.Forms.Map;
using WarGame.Model;
using WarGame.Resources;

namespace WarGame.Forms.Video;
public sealed partial class FormVideo : Form
{

    private readonly System.Drawing.Point _posFromDisplays;
    private readonly SharpDxVideo _dx;
    public int SelectedCamera => _dx.CameraType;

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
        buttonVideoQH.Click += ButtonVideoQH_Click;
        buttonVideoQM.Click += ButtonVideoQM_Click;
        buttonVideoQL.Click += ButtonVideoQL_Click;
        buttonVideoQEL.Click += ButtonVideoQEL_Click;
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

    private async void ButtonVideoQH_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected);
        if (obj == null) return;
        await FormMap.ObjectsGame.SetQualityVideo(obj, 3);
        buttonVideoQH.Enabled = false;
        await Task.Delay(1000);
        buttonVideoQH.Enabled = true;
    }

    private async void ButtonVideoQM_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected);
        if (obj == null) return;
        await FormMap.ObjectsGame.SetQualityVideo(obj, 2);
        buttonVideoQM.Enabled = false;
        await Task.Delay(1000);
        buttonVideoQM.Enabled = true;
    }

    private async void ButtonVideoQL_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected);
        if (obj == null) return;
        await FormMap.ObjectsGame.SetQualityVideo(obj, 1);
        buttonVideoQL.Enabled = false;
        await Task.Delay(1000);
        buttonVideoQL.Enabled = true;
    }

    private async void ButtonVideoQEL_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected);
        if (obj == null) return;
        await FormMap.ObjectsGame.SetQualityVideo(obj, 0);
        buttonVideoQEL.Enabled = false;
        await Task.Delay(1000);
        buttonVideoQEL.Enabled = true;
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

            ButtonCheck();

            var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected);
            if (obj == null)
            {
                _dx.CameraType = -1;
                continue;
            }
            if (_dx.CameraType == -1)
            {
                _dx.CameraType = 2;
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

    private async void ButtonPtz_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 0;
        ButtonCheck();
        buttonPtz.Enabled = false;
        await Task.Delay(1000);
        buttonPtz.Enabled = true;
    }
    private async void ButtonWarm_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 1;
        ButtonCheck();
        buttonWarm.Enabled = false;
        await Task.Delay(1000);
        buttonWarm.Enabled = true;
    }
    private async void ButtonFrwd_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 2;
        ButtonCheck();
        buttonFrwd.Enabled = false;
        await Task.Delay(1000);
        buttonFrwd.Enabled = true;
    }

    private async void ButtonBack_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 3;
        ButtonCheck();
        buttonBack.Enabled = false;
        await Task.Delay(1000);
        buttonBack.Enabled = true;
    }
    private async void ButtonLeft_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 4;
        ButtonCheck();
        buttonLeft.Enabled = false;
        await Task.Delay(1000);
        buttonLeft.Enabled = true;
    }
    private async void ButtonRight_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 5;
        ButtonCheck();
        buttonRight.Enabled = false;
        await Task.Delay(1000);
        buttonRight.Enabled = true;
    }
    private async void ButtonFpv1_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 6;
        ButtonCheck();
        buttonFpv1.Enabled = false;
        await Task.Delay(1000);
        buttonFpv1.Enabled = true;
    }
    private async void ButtonFpv2_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 7;
        ButtonCheck();
        buttonFpv2.Enabled = false;
        await Task.Delay(1000);
        buttonFpv2.Enabled = true;
    }
    private async void ButtonFpv3_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 8;
        ButtonCheck();
        buttonFpv3.Enabled = false;
        await Task.Delay(1000);
        buttonFpv3.Enabled = true;
    }
    private async void ButtonFpv4_Click(object? sender, EventArgs e)
    {
        _dx.CameraType = 9;
        ButtonCheck();
        buttonFpv4.Enabled = false;
        await Task.Delay(1000);
        buttonFpv4.Enabled = true;
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
        buttonPtzDown.Visible = _dx.CameraType < 2;
        buttonPtzUp.Visible = _dx.CameraType < 2;
        buttonPtzLeft.Visible = _dx.CameraType < 2;
        buttonPtzRight.Visible = _dx.CameraType < 2;
        buttonPtzZoomIn.Visible = _dx.CameraType < 2;
        buttonPtzZoomOut.Visible = _dx.CameraType < 2;

        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected);

        buttonVideoQH.BackColor = obj == null ? Color.White : obj.VideoQuality == 3 ? Color.LightGreen : Color.White;
        buttonVideoQM.BackColor = obj == null ? Color.White : obj.VideoQuality == 2 ? Color.LightGreen : Color.White;
        buttonVideoQL.BackColor = obj == null ? Color.White : obj.VideoQuality == 1 ? Color.LightGreen : Color.White;
        buttonVideoQEL.BackColor = obj == null ? Color.White : obj.VideoQuality == 0 ? Color.LightGreen : Color.White;
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
