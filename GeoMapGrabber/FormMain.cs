

using CefSharp.OffScreen;
using GeoMapGrabber.Other;
using OpenCvSharp;
using System.Data;
using System.Data.SQLite;
namespace GeoMapGrabber;

public partial class FormMain : Form
{
    private readonly ImageEncodingParam _webp_par = new(ImwriteFlags.WebPQuality, 10);

    // ≈À‹ »ÕŒ: 54.151851, 37.542351
    private double _startLat = 54.151851d;
    private double _startLon = 37.542351d;
    private int _startZoom = 18;
    private readonly int _grabTylesBlocks = 50;
    private readonly int _grabTylesTimeSec = 80;
    private double _grabTylesRadiusKm = 20038;

    private readonly int _tileSize = 256;
    private readonly int _topMenuHeight = 38;

    public FormMain()
    {
        InitializeComponent();

        textBoxLon.Text = _startLon.ToString();
        textBoxLat.Text = _startLat.ToString();

        Shown += FrmShown;

        comboBoxZoom.SelectedIndex = _startZoom-1;
    }

    private void LatChanged(object? sender, EventArgs e)
    {
        if (double.TryParse(textBoxLat.Text.Replace('.',','), out double newLat) == false)
        {
            newLat = _startLat;
            textBoxLat.Text = _startLat.ToString();
        }
        _startLat = newLat;
        UpdateBrowserPageAsync();
    }

    private void LonChanged(object? sender, EventArgs e)
    {
        if (double.TryParse(textBoxLon.Text.Replace('.', ','), out double newLon) == false)
        {
            newLon = _startLon;
            textBoxLon.Text = _startLon.ToString();
        }
        _startLon = newLon;
        UpdateBrowserPageAsync();
    }

    private double GrabberOneTileKm()
    {
        return 40075.0d / Math.Pow(2, _startZoom);
    }

    private double GrabberRadiusOneCycle()
    {
        return Math.Min(Math.Pow(2, _startZoom) / 2, _grabTylesBlocks / 2) * GrabberOneTileKm();
    }

    private void UpdateInfo()
    {
        textBoxTilesCount.Text = Math.Pow(Math.Pow(2, _startZoom), 2).ToString();
        textBoxOneCycleTime.Text = _grabTylesTimeSec.ToString();
        textBoxOneCycleTyles.Text = $"{_grabTylesBlocks}x{_grabTylesBlocks}";
        textBoxOneCycleRadius.Text = GrabberRadiusOneCycle().ToString("0.00000");
        numericUpDownGrabKm.Value = (decimal)_grabTylesRadiusKm;
        var grabSize = (int)(_grabTylesRadiusKm / GrabberOneTileKm());
        textBoxGrabSizeAll.Text = $"{grabSize * 2}x{grabSize * 2}";
        var grabCycles = Math.Round((grabSize * 2.0d) * (grabSize * 2.0d) / (_grabTylesBlocks * _grabTylesBlocks), MidpointRounding.ToPositiveInfinity);
        textBoxGrabCyclesAll.Text = $"{grabCycles}";
        var GrabTimeSec = grabCycles * _grabTylesTimeSec;
        textBoxGrabTimeAll.Text = $"{GrabTimeSec}/{Math.Round(GrabTimeSec/60, MidpointRounding.ToZero)}/{Math.Round(GrabTimeSec / 3600, MidpointRounding.ToZero)}/{Math.Round(GrabTimeSec / 86400, MidpointRounding.ToZero)}";
    }

    private async void UpdateBrowserPageAsync()
    {
        await chromiumWebBrowserMain.LoadUrlAsync($"https://bestmaps.ru/map/yandex/satellite/{_startZoom}/{_startLat.ToString().Replace(',','.')}/{_startLon.ToString().Replace(',','.')}");
    }

    private void FrmShown(object? sender, EventArgs e)
    {
        UpdateBrowserPageAsync();
        UpdateInfo();

        textBoxLat.TextChanged += LatChanged;
        textBoxLon.TextChanged += LonChanged;
        comboBoxZoom.SelectedValueChanged += ZoomChanged;
        numericUpDownGrabKm.ValueChanged += GrabKmChanged;
        buttonGrab.Click += ButtonGrab_Click;
    }

    private async void ButtonGrab_Click(object? sender, EventArgs e)
    {
        if (MessageBox.Show("Õ‡˜‡Ú¸ „‡··ËÌ„?", "√–¿¡¡»Õ√  ¿–“€", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        groupBoxGrabber.Enabled = false;
        groupBoxGeoMap.Enabled = false;
        await GrabAsync();
        groupBoxGrabber.Enabled = true;
        groupBoxGeoMap.Enabled = true;
    }

    private async Task GrabAsync()
    {
        progressBarGrab.Value = 0;
        labelInfo.Text = "";
        Random r = new();

        using var sql = OpenTilesSqlDb();

        var xStart = GeoMap.TileXForLon(_startZoom, _startLon);
        var yStart = GeoMap.TileYForLat(_startZoom, _startLat);

        var lenGrabInTyles = (int)(_grabTylesRadiusKm / GrabberOneTileKm());
        var step = (int)Math.Round(lenGrabInTyles / (double)_grabTylesBlocks, MidpointRounding.ToZero);

        int n = 1;
        int t = 0;
        var maxStep = (step * 2 + 1) * (step * 2 + 1);
        var oneStep = 99.0d / maxStep;
        var timeStart = DateTime.Now;
        for (var xs = -step; xs <= step; xs++)
        {
            for (var ys = -step; ys <= step; ys++)
            {
                var x0 = xStart + xs * _grabTylesBlocks;
                var y0 = yStart + ys * _grabTylesBlocks;

                var lat = GeoMap.LatForTile(_startZoom, x0, y0);
                var lon = GeoMap.LonForTile(_startZoom, x0, y0);
                var completionSource = new TaskCompletionSource<Bitmap>();
                var s = _tileSize * (_grabTylesBlocks + 1);
                ChromiumWebBrowser? wb = null;
                Bitmap? bm = null;
                do
                {
                    if (wb != null)
                    {
                        wb.Dispose();
                        wb = null;
                    }
                    wb = new ChromiumWebBrowser(string.Empty) { Size = new System.Drawing.Size(s, s) };
                    await Task.Delay(1000);
                    await wb.LoadUrlAsync($"https://bestmaps.ru/map/yandex/satellite/{_startZoom}/{lat.ToString().Replace(',', '.')}/{lon.ToString().Replace(',', '.')}");
                    await Task.Delay(80000 - r.Next(0, 5000));
                    if (bm != null)
                    {
                        bm.Dispose();
                        bm = null;
                    }
                    bm = wb.ScreenshotOrNull(PopupBlending.Main);
                } 
                while (bm == null || bm.Width != s);
                var mat = OpenCvSharp.Extensions.BitmapConverter.ToMat(bm);
                bm.Dispose();

                for (var x = -_grabTylesBlocks / 2; x < _grabTylesBlocks / 2; x++)
                {
                    for (var y = -_grabTylesBlocks / 2; y < _grabTylesBlocks / 2; y++)
                    {
                        if (x0 + x < 0) continue;
                        if (y0 + y < 0) continue;
                        if (x0 + x >= Math.Pow(2, _startZoom)) continue;
                        if (y0 + y >= Math.Pow(2, _startZoom)) continue;

                        var mm = mat.SubMat(new OpenCvSharp.Rect(_tileSize * x + mat.Width / 2, _tileSize * y + mat.Height / 2 + _topMenuHeight / 2, _tileSize, _tileSize));
                        WriteTile(sql, mm, _startZoom, x0 + x, y0 + y);
                        mm.Dispose();
                        t++;

                    }
                }

                var sec = (DateTime.Now - timeStart).TotalSeconds;
                var min = Math.Round(sec / 60, MidpointRounding.ToZero);
                progressBarGrab.Value = (int)Math.Max(1.0d, Math.Min(99.0d, n * oneStep));
                labelInfo.Text = $"ÿ¿√ {n} ËÁ {maxStep}, ÒÓı‡ÌÂÌÓ {t} Ú‡ÈÎÓ‚, ¬–≈Ã≈Õ» ÔÓ¯ÎÓ: {min:0} ÏËÌ {sec - min * 60:0} ÒÂÍ";

                mat.Dispose();
                wb.Dispose();
                n++;
            }
        }
        progressBarGrab.Value = 0;
    }

    private SQLiteConnection OpenTilesSqlDb()
    {
        if (!Directory.Exists($"{Application.StartupPath}Maps"))
        {
            Directory.CreateDirectory($"{Application.StartupPath}Maps");
        }
        if (!File.Exists($"{Application.StartupPath}Maps\\tiles.db"))
        {
            var fs = File.Create($"{Application.StartupPath}Maps\\tiles.db");
            fs.Dispose();
        }
        var _sqlite = new SQLiteConnection($"Data Source={Application.StartupPath}Maps\\tiles.db;Version=3;");
        _sqlite.Open();
        using var cmd = _sqlite.CreateCommand();
        cmd.CommandText = "CREATE TABLE IF NOT EXISTS Tiles( " +
                          "zoom INTEGER NOT NULL, " +
                          "x INTEGER NOT NULL, " +
                          "y INTEGER NOT NULL, " +
                          "type INTEGER NOT NULL, " +
                          "blob BLOB NOT NULL, " +
                          "PRIMARY KEY (zoom, x, y)); ";
        cmd.ExecuteNonQuery();
        return _sqlite;
    }

    private void WriteTile(SQLiteConnection sql, Mat tile, int zoom, int x, int y, bool replace = false)
    {
        Cv2.ImEncode(".webp", tile, out var data, _webp_par);
        using var cmd = sql.CreateCommand();
        cmd.CommandText = "INSERT OR " + (replace ? "REPLACE" : "IGNORE") + " INTO Tiles VALUES (@zoom, @x, @y, @type, @blob);";
        cmd.Parameters.AddWithValue("@zoom", zoom);
        cmd.Parameters.AddWithValue("@x", x);
        cmd.Parameters.AddWithValue("@y", y);
        cmd.Parameters.AddWithValue("@type", 0); // ﬂÌ‰ÂÍÒ Ú‡ÈÎ
        cmd.Parameters.Add("@blob", DbType.Binary, data.Length).Value = data;
        cmd.ExecuteNonQuery();
    }

    private void GrabKmChanged(object? sender, EventArgs e)
    {
        _grabTylesRadiusKm = (double)numericUpDownGrabKm.Value;
        _startZoom = int.Parse(comboBoxZoom.SelectedItem!.ToString()!);
        UpdateInfo();
    }

    private void ZoomChanged(object? sender, EventArgs e)
    {
        _startZoom = int.Parse(comboBoxZoom.SelectedItem!.ToString()!);
        UpdateBrowserPageAsync();
        UpdateInfo();
    }
}
