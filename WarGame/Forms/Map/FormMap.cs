using WarGame.Remote;
using WarGame.Resources;

namespace WarGame.Forms.Map;

public sealed partial class FormMap : Form
{
    public static GeoPosition GlobalPos { get; private set; } = new();
    public static GeoMap Map { get; private set; } = new();
    public static ControlUser ControlUser { get; private set; } = new();
    public static StaticObjects ObjectsStatic { get; private set; } = new();

    private Point _posFromDisplays;
    private readonly SharpDx _dx;
    public FormMap(Point pos, int fps)
    {
        InitializeComponent();

        _posFromDisplays = pos;

        _dx = new SharpDxMap(pictureBoxMain, fps);

        GlobalPos.Init();
        ObjectsStatic.Init(_dx);

        Icon = EmbeddedResources.Get<Icon>("Sprites.WarGame.ico");

        Shown += FormShown;
        Closed += FormOnClosing;
        pictureBoxMain.MouseDown += PictureBoxMain_MouseDown;
        pictureBoxMain.MouseUp += PictureBoxMain_MouseUp;
        pictureBoxMain.MouseMove += PictureBoxMain_MouseMove;
        pictureBoxMain.MouseWheel += PictureBoxMain_MouseWheel;
    }

    private void PictureBoxMain_MouseWheel(object? sender, MouseEventArgs e)
    {
        var zoom = GlobalPos.ZoomLocal + Math.Sign(e.Delta) * 0.2d;
        switch (GlobalPos.Zoom)
        {
            case 6:
                if (zoom < 0)
                {
                    GlobalPos.Zoom = 6;
                    GlobalPos.ZoomLocal = 0.0d;
                }
                else if (zoom >= GlobalPos.ZoomLocalStep0)
                {
                    GlobalPos.Zoom = 8;
                    GlobalPos.ZoomLocal = zoom - GlobalPos.ZoomLocalStep0;
                }
                else
                {
                    GlobalPos.ZoomLocal = zoom;
                }
                break;
            case 8:
                if (zoom < 0)
                {
                    GlobalPos.Zoom = 6;
                    GlobalPos.ZoomLocal = zoom + GlobalPos.ZoomLocalStep0;
                }
                else if (zoom >= GlobalPos.ZoomLocalStep1)
                {
                    GlobalPos.Zoom = 12;
                    GlobalPos.ZoomLocal = zoom - GlobalPos.ZoomLocalStep1;
                }
                else
                {
                    GlobalPos.ZoomLocal = zoom;
                }
                break;
            case 12:
                if (zoom < 0)
                {
                    GlobalPos.Zoom = 8;
                    GlobalPos.ZoomLocal = zoom + GlobalPos.ZoomLocalStep1;
                }
                else if (zoom >= GlobalPos.ZoomLocalStep1)
                {
                    GlobalPos.Zoom = 16;
                    GlobalPos.ZoomLocal = zoom - GlobalPos.ZoomLocalStep1;
                }
                else
                {
                    GlobalPos.ZoomLocal = zoom;
                }
                break;
            case 16:
                if (zoom < 0)
                {
                    GlobalPos.Zoom = 12;
                    GlobalPos.ZoomLocal = zoom + GlobalPos.ZoomLocalStep1;
                }
                else if (zoom >= GlobalPos.ZoomLocalStep1)
                {
                    GlobalPos.Zoom = 16;
                    GlobalPos.ZoomLocal = GlobalPos.ZoomLocalStep1;
                }
                else
                {
                    GlobalPos.ZoomLocal = zoom;
                }
                break;
            default:
                break;
        }
    }

    private void PictureBoxMain_MouseUp(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            ControlUser.MouseLeftDown = false;
            return;
        }
        if (e.Button == MouseButtons.Right)
        {
            ControlUser.MouseRightDown = false;
            return;
        }
    }

    private void PictureBoxMain_MouseMove(object? sender, MouseEventArgs e)
    {
        if (ControlUser.MouseRightDown && !ControlUser.MouseLeftDown) // Перемещение карты
        {
            var dX = ControlUser.MouseX - e.X;
            var dY = ControlUser.MouseY - e.Y;

            GlobalPos.LonX += dX / GeoMath.TileSize * GeoMath.GetLenXForOneTile(GlobalPos.Zoom, GlobalPos.LatY, GlobalPos.LonX);
            GlobalPos.LatY += dY / GeoMath.TileSize * GeoMath.GetLenYForOneTile(GlobalPos.Zoom, GlobalPos.LatY, GlobalPos.LonX);
        }
        ControlUser.MouseX = e.X;
        ControlUser.MouseY = e.Y;
    }

    private void PictureBoxMain_MouseDown(object? sender, MouseEventArgs e)
    {
        ControlUser.MouseRightDown = e.Button == MouseButtons.Right;
        ControlUser.MouseLeftDown = e.Button == MouseButtons.Left;
    }

    private void FormOnClosing(object? sender, EventArgs e)
    {
        _dx.Dispose();
    }

    private void FormShown(object? sender, EventArgs e)
    {
        StartPosition = FormStartPosition.Manual; // Ручная позиция окна
        Location = _posFromDisplays; // Поместить в нужный монитор

        _ = _dx.StartAsync(default);
    }
}
