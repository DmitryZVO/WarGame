using WarGame.Forms.Map;
using WarGame.Model;
using WarGame.Resources;
using static WarGame.Forms.Map.GameObject;

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

    public void ButtonVisibleChange(GameObject? obj)
    {
        buttonRelay1.Visible = (obj != null);
        buttonRelay2.Visible = (obj != null);
        buttonRelay3.Visible = (obj != null);
        buttonRelay4.Visible = (obj != null);
        buttonRelay5.Visible = (obj != null);
        buttonRelay6.Visible = (obj != null);
        buttonRelay7.Visible = (obj != null);
        buttonRelay8.Visible = (obj != null);
        if (obj == null) return;
        buttonRelay1.BackColor = obj.Telem.Relay[0] == 0 ? Color.White : obj.Telem.Relay[0] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelay2.BackColor = obj.Telem.Relay[1] == 0 ? Color.White : obj.Telem.Relay[1] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelay3.BackColor = obj.Telem.Relay[2] == 0 ? Color.White : obj.Telem.Relay[2] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelay4.BackColor = obj.Telem.Relay[3] == 0 ? Color.White : obj.Telem.Relay[3] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelay5.BackColor = obj.Telem.Relay[4] == 0 ? Color.White : obj.Telem.Relay[4] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelay6.BackColor = obj.Telem.Relay[5] == 0 ? Color.White : obj.Telem.Relay[5] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelay7.BackColor = obj.Telem.Relay[6] == 0 ? Color.White : obj.Telem.Relay[6] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelay8.BackColor = obj.Telem.Relay[7] == 0 ? Color.White : obj.Telem.Relay[7] == 1 ? Color.LightGreen : Color.LightGray;
    }

    private void FormShown(object? sender, EventArgs e)
    {
        StartPosition = FormStartPosition.Manual; // Ручная позиция окна
        Location = _posFromDisplays; // Поместить в нужный монитор

        buttonRelay1.Click += ButtonRelay1_Click;
        buttonRelay2.Click += ButtonRelay2_Click;
        buttonRelay3.Click += ButtonRelay3_Click;
        buttonRelay4.Click += ButtonRelay4_Click;
        buttonRelay5.Click += ButtonRelay5_Click;
        buttonRelay6.Click += ButtonRelay6_Click;
        buttonRelay7.Click += ButtonRelay7_Click;
        buttonRelay8.Click += ButtonRelay8_Click;

        _ = _dx.StartAsync(default);
    }
    private async void ButtonRelay1_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var relNew = new RelaysForWrite();
        relNew.Values[0] = buttonRelay1.BackColor == Color.LightGreen ? 0.0f : 1.0f;
        relNew.Values[1] = obj.Telem.Relay[1] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[2] = obj.Telem.Relay[2] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[3] = obj.Telem.Relay[3] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[4] = obj.Telem.Relay[4] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[5] = obj.Telem.Relay[5] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[6] = obj.Telem.Relay[6] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[7] = obj.Telem.Relay[7] == 1.0f ? 1.0f : 0.0f;
        await FormMap.ObjectsGame.RewriteRelayAsync(obj, relNew);
    }

    private async void ButtonRelay2_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var relNew = new RelaysForWrite();
        relNew.Values[0] = obj.Telem.Relay[0] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[1] = buttonRelay2.BackColor == Color.LightGreen ? 0.0f : 1.0f;
        relNew.Values[2] = obj.Telem.Relay[2] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[3] = obj.Telem.Relay[3] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[4] = obj.Telem.Relay[4] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[5] = obj.Telem.Relay[5] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[6] = obj.Telem.Relay[6] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[7] = obj.Telem.Relay[7] == 1.0f ? 1.0f : 0.0f;
        await FormMap.ObjectsGame.RewriteRelayAsync(obj, relNew);
    }
    private async void ButtonRelay3_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var relNew = new RelaysForWrite();
        relNew.Values[0] = obj.Telem.Relay[0] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[1] = obj.Telem.Relay[1] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[2] = buttonRelay3.BackColor == Color.LightGreen ? 0.0f : 1.0f;
        relNew.Values[3] = obj.Telem.Relay[3] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[4] = obj.Telem.Relay[4] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[5] = obj.Telem.Relay[5] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[6] = obj.Telem.Relay[6] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[7] = obj.Telem.Relay[7] == 1.0f ? 1.0f : 0.0f;
        await FormMap.ObjectsGame.RewriteRelayAsync(obj, relNew);
    }
    private async void ButtonRelay4_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var relNew = new RelaysForWrite();
        relNew.Values[0] = obj.Telem.Relay[0] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[1] = obj.Telem.Relay[1] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[2] = obj.Telem.Relay[2] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[3] = buttonRelay4.BackColor == Color.LightGreen ? 0.0f : 1.0f;
        relNew.Values[4] = obj.Telem.Relay[4] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[5] = obj.Telem.Relay[5] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[6] = obj.Telem.Relay[6] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[7] = obj.Telem.Relay[7] == 1.0f ? 1.0f : 0.0f;
        await FormMap.ObjectsGame.RewriteRelayAsync(obj, relNew);
    }
    private async void ButtonRelay5_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var relNew = new RelaysForWrite();
        relNew.Values[0] = obj.Telem.Relay[0] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[1] = obj.Telem.Relay[1] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[2] = obj.Telem.Relay[2] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[3] = obj.Telem.Relay[3] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[4] = buttonRelay5.BackColor == Color.LightGreen ? 0.0f : 1.0f;
        relNew.Values[5] = obj.Telem.Relay[5] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[6] = obj.Telem.Relay[6] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[7] = obj.Telem.Relay[7] == 1.0f ? 1.0f : 0.0f;
        await FormMap.ObjectsGame.RewriteRelayAsync(obj, relNew);
    }
    private async void ButtonRelay6_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var relNew = new RelaysForWrite();
        relNew.Values[0] = obj.Telem.Relay[0] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[1] = obj.Telem.Relay[1] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[2] = obj.Telem.Relay[2] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[3] = obj.Telem.Relay[3] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[4] = obj.Telem.Relay[4] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[5] = buttonRelay6.BackColor == Color.LightGreen ? 0.0f : 1.0f;
        relNew.Values[6] = obj.Telem.Relay[6] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[7] = obj.Telem.Relay[7] == 1.0f ? 1.0f : 0.0f;
        await FormMap.ObjectsGame.RewriteRelayAsync(obj, relNew);
    }
    private async void ButtonRelay7_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var relNew = new RelaysForWrite();
        relNew.Values[0] = obj.Telem.Relay[0] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[1] = obj.Telem.Relay[1] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[2] = obj.Telem.Relay[2] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[3] = obj.Telem.Relay[3] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[4] = obj.Telem.Relay[4] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[5] = obj.Telem.Relay[5] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[6] = buttonRelay7.BackColor == Color.LightGreen ? 0.0f : 1.0f;
        relNew.Values[7] = obj.Telem.Relay[7] == 1.0f ? 1.0f : 0.0f;
        await FormMap.ObjectsGame.RewriteRelayAsync(obj, relNew);
    }
    private async void ButtonRelay8_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var relNew = new RelaysForWrite();
        relNew.Values[0] = obj.Telem.Relay[0] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[1] = obj.Telem.Relay[1] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[2] = obj.Telem.Relay[2] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[3] = obj.Telem.Relay[3] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[4] = obj.Telem.Relay[4] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[5] = obj.Telem.Relay[5] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[6] = obj.Telem.Relay[6] == 1.0f ? 1.0f : 0.0f;
        relNew.Values[7] = buttonRelay8.BackColor == Color.LightGreen ? 0.0f : 1.0f;
        await FormMap.ObjectsGame.RewriteRelayAsync(obj, relNew);
    }
}
