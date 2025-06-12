
using CefSharp.OffScreen;
using System.Windows.Forms;
using WarGame.Other;
using WarGame.Resources;
using System.Drawing;

namespace WarGame;

public sealed partial class FormMain : Form
{
    private bool _formFullScreen = false;
    private Size _formSize = new Size();
    private Point _formLocation = new Point();
    private SharpDxMain _dx;
    public FormMain()
    {
        InitializeComponent();

        _dx = new SharpDxMain(pictureBoxMain, 100);

        Icon = EmbeddedResources.Get<Icon>("Sprites.WarGame.ico");

        pictureBoxMain.DoubleClick += FullScreen;
        Shown += FormShown;
        Closed += FormOnClosing;
    }

    private void FormOnClosing(object? sender, EventArgs e)
    {
        _dx.Dispose();
    }

    private async void FormShown(object? sender, EventArgs e)
    {
        var zoom = 19;
        var tileX0 = 316927;
        var tileY0 = 164368;
        var tileSize = 256;
        var topMenuHeight = 38;
        var lat = GeoMap.LatForTile(zoom, tileX0, tileY0);
        var lon = GeoMap.LonForTile(zoom, tileX0, tileY0);
        //var x = GeoMap.TileXForLon(19, lon);
        //var y = GeoMap.TileYForLat(19, lat);
        var completionSource = new TaskCompletionSource<Bitmap>();
        var wb = new ChromiumWebBrowser(string.Empty) { Size = new Size(tileSize * 51, tileSize * 51) };
        await wb.LoadUrlAsync($"https://bestmaps.ru/map/yandex/satellite/{zoom}/{lat.ToString().Replace(',', '.')}/{lon.ToString().Replace(',', '.')}");
        await Task.Delay(80000);

        using var bm = wb.ScreenshotOrNull(PopupBlending.Main);
        using var mat = OpenCvSharp.Extensions.BitmapConverter.ToMat(bm);
        for (var x = -25; x < 25; x++)
        {
            for (var y = -25; y < 25; y++)
            {
                using var mm = mat.SubMat(new OpenCvSharp.Rect(tileSize * x + mat.Width / 2, tileSize * y + mat.Height / 2 + topMenuHeight / 2, tileSize, tileSize));
                mm.SaveImage($"{zoom}_{tileX0 + x}_{tileY0 + y}.png");
            }
        }
        mat.SubMat(new OpenCvSharp.Rect(tileSize * -25 + mat.Width / 2, tileSize * -25 + mat.Height / 2 + topMenuHeight / 2, tileSize * 50, tileSize * 50)).SaveImage($"{zoom}_tiles_all.png");
        //bm.Save("test.png");
        _ = _dx.StartAsync(default);
    }

    private void FullScreen(object? sender, EventArgs e)
    {
        if (_formFullScreen)
        {
            ControlBox = true;
            FormBorderStyle = FormBorderStyle.Sizable;
            Location = new Point(_formLocation.X, _formLocation.Y);
            Size = new Size(_formSize.Width, _formSize.Height);
        }
        else
        {
            _formSize = new Size(Size.Width, Size.Height);
            _formLocation = new Point(Location.X, Location.Y);
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            Location = new Point(0, 0);
            Size = new Size(Screen.PrimaryScreen!.Bounds.Width, Screen.PrimaryScreen!.Bounds.Height);
        }
        _formFullScreen = !_formFullScreen;
    }
}
