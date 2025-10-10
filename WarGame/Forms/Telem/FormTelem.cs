using WarGame.Model;
using WarGame.Resources;

namespace WarGame.Forms.Telem;
public sealed partial class FormTelem : Form
{

    private Point _posFromDisplays;
    private readonly SharpDx _dx;
    public FormTelem(Point pos, int fps)
    {
        InitializeComponent();

        _posFromDisplays = pos;

        _dx = new SharpDxTelem(pictureBoxMain, fps);
        Icon = EmbeddedResources.Get<Icon>("Sprites.WarGame.ico");

        Shown += FormShown;
        Closed += FormOnClosing;
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
    }
}
