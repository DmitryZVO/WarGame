
using WarGame.Other;
using WarGame.Resources;
using WarGame.Core;

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

        _dx = new SharpDxMain(pictureBoxMain, 60);
        Core.Values.Map.Init(_dx);

        Icon = EmbeddedResources.Get<Icon>("Sprites.WarGame.ico");

        pictureBoxMain.DoubleClick += FullScreen;
        Shown += FormShown;
        Closed += FormOnClosing;
        pictureBoxMain.MouseDown += PictureBoxMain_MouseDown;
        pictureBoxMain.MouseUp += PictureBoxMain_MouseUp;
        pictureBoxMain.MouseMove += PictureBoxMain_MouseMove;
        pictureBoxMain.MouseWheel += PictureBoxMain_MouseWheel;
    }

    private void PictureBoxMain_MouseWheel(object? sender, MouseEventArgs e)
    {
        var zoom = Values.GlobalPos.ZoomLocal + Math.Sign(e.Delta)*0.2d;
        switch (Values.GlobalPos.Zoom)
        {
            case 6:
                if (zoom < 0)
                {
                    Values.GlobalPos.Zoom = 6;
                    Values.GlobalPos.ZoomLocal = 0.0d;
                }
                else if (zoom >= Values.GlobalPos.ZoomLocalStep0)
                {
                    Values.GlobalPos.Zoom = 8;
                    Values.GlobalPos.ZoomLocal = zoom - Values.GlobalPos.ZoomLocalStep0;
                }
                else
                {
                    Values.GlobalPos.ZoomLocal = zoom;
                }
                break;
            case 8:
                if (zoom < 0)
                {
                    Values.GlobalPos.Zoom = 6;
                    Values.GlobalPos.ZoomLocal = zoom + Values.GlobalPos.ZoomLocalStep0;
                }
                else if (zoom >= Values.GlobalPos.ZoomLocalStep1)
                {
                    Values.GlobalPos.Zoom = 12;
                    Values.GlobalPos.ZoomLocal = zoom - Values.GlobalPos.ZoomLocalStep1;
                }
                else
                {
                    Values.GlobalPos.ZoomLocal = zoom;
                }
                break;
            case 12:
                if (zoom < 0)
                {
                    Values.GlobalPos.Zoom = 8;
                    Values.GlobalPos.ZoomLocal = zoom + Values.GlobalPos.ZoomLocalStep1;
                }
                else if (zoom >= Values.GlobalPos.ZoomLocalStep1)
                {
                    Values.GlobalPos.Zoom = 16;
                    Values.GlobalPos.ZoomLocal = zoom - Values.GlobalPos.ZoomLocalStep1;
                }
                else
                {
                    Values.GlobalPos.ZoomLocal = zoom;
                }
                break;
            case 16:
                if (zoom < 0)
                {
                    Values.GlobalPos.Zoom = 12;
                    Values.GlobalPos.ZoomLocal = zoom + Values.GlobalPos.ZoomLocalStep1;
                }
                else if (zoom >= Values.GlobalPos.ZoomLocalStep1)
                {
                    Values.GlobalPos.Zoom = 16;
                    Values.GlobalPos.ZoomLocal = Values.GlobalPos.ZoomLocalStep1;
                }
                else
                {
                    Values.GlobalPos.ZoomLocal = zoom;
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
            Values.ControlUser.MouseLeftDown = false;
            return;
        }
        if (e.Button == MouseButtons.Right)
        {
            Values.ControlUser.MouseRightDown = false;
            return;
        }
    }

    private void PictureBoxMain_MouseMove(object? sender, MouseEventArgs e)
    {
        if (Values.ControlUser.MouseRightDown && !Values.ControlUser.MouseLeftDown) // Перемещение карты
        {
            var dX = Values.ControlUser.MouseX - e.X;
            var dY = Values.ControlUser.MouseY - e.Y;

            Values.GlobalPos.LonX += dX / GeoMath.TileSize * GeoMath.GetLenXForOneTile(Values.GlobalPos.Zoom, Values.GlobalPos.LatY, Values.GlobalPos.LonX);
            Values.GlobalPos.LatY += dY / GeoMath.TileSize * GeoMath.GetLenYForOneTile(Values.GlobalPos.Zoom, Values.GlobalPos.LatY, Values.GlobalPos.LonX);
        }
        Values.ControlUser.MouseX = e.X;
        Values.ControlUser.MouseY = e.Y;
    }

    private void PictureBoxMain_MouseDown(object? sender, MouseEventArgs e)
    {
        Values.ControlUser.MouseRightDown = e.Button == MouseButtons.Right;
        Values.ControlUser.MouseLeftDown = e.Button == MouseButtons.Left;
    }

    private void FormOnClosing(object? sender, EventArgs e)
    {
        _dx.Dispose();
    }

    private void FormShown(object? sender, EventArgs e)
    {
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
