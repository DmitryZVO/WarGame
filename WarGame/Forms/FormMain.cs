
using WarGame.Resources;
using WarGame.Other;
using System.ComponentModel;

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

        Icon = EmbeddedResources.Get<Icon>("Sprites.WarGame.ico");

        pictureBoxMain.DoubleClick += FullScreen;
        Shown += FormShown;
        Closed += FormOnClosing;
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
