using WarGame.Model;
using WarGame.Resources;

namespace WarGame.Forms;
public sealed partial class FormMain : Form
{

    public FormMain()
    {
        InitializeComponent();
        Icon = EmbeddedResources.Get<Icon>("Sprites.WarGame.ico");

        Shown += FormShown;
    }
    private void FormShown(object? sender, EventArgs e)
    {
        Visible = false;
        CheckFormsAsync();
    }

    public async void CheckFormsAsync(CancellationToken ct = default)
    {
        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(100, ct);

            if (Core.Config.FormMap.Enable && !Core.FrmMap!.Visible) Close();
            if (Core.Config.FormVideo.Enable && !Core.FrmVideo!.Visible) Close();
            if (Core.Config.FormTelem.Enable && !Core.FrmTelem!.Visible) Close();
        }
    }
}
