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
        var visible = (obj != null);

        buttonRelay1.Visible = visible;
        buttonRelay2.Visible = visible;
        buttonRelay3.Visible = visible;
        buttonRelay4.Visible = visible;
        buttonRelay5.Visible = visible;
        buttonRelay6.Visible = visible;
        buttonRelay7.Visible = visible;
        buttonRelay8.Visible = visible;
        buttonRelayF1.Visible = visible;
        buttonRelayF2.Visible = visible;
        buttonRelayF3.Visible = visible;
        buttonRelayF4.Visible = visible;

        buttonBoomCheck.Visible = visible;
        buttonBoom.Visible = visible;

        buttonPower1On.Visible = visible;
        buttonPower1Off.Visible = visible;
        buttonPower2On.Visible = visible;
        buttonPower2Off.Visible = visible;
        buttonPower3On.Visible = visible;
        buttonPower3Off.Visible = visible;
        buttonPower4On.Visible = visible;
        buttonPower4Off.Visible = visible;

        buttonFpv1Open.Visible = visible;
        buttonFpv1Close.Visible = visible;
        buttonFpv1Stop.Visible = visible;
        buttonFpv1On.Visible = visible;
        buttonFpv1Off.Visible = visible;
        buttonFpv2Open.Visible = visible;
        buttonFpv2Close.Visible = visible;
        buttonFpv2Stop.Visible = visible;
        buttonFpv2On.Visible = visible;
        buttonFpv2Off.Visible = visible;
        buttonFpv3Open.Visible = visible;
        buttonFpv3Close.Visible = visible;
        buttonFpv3Stop.Visible = visible;
        buttonFpv3On.Visible = visible;
        buttonFpv3Off.Visible = visible;
        buttonFpv4Open.Visible = visible;
        buttonFpv4Close.Visible = visible;
        buttonFpv4Stop.Visible = visible;
        buttonFpv4On.Visible = visible;
        buttonFpv4Off.Visible = visible;
        buttonBoom.Enabled = !(buttonBoomCheck.BackColor == Color.White);
        buttonBoom.BackColor = buttonBoomCheck.BackColor == Color.White ? Color.LightGray : Color.White;

        if (obj == null)
        {
            buttonBoomCheck.BackColor = Color.White;
            return;
        }

        buttonRelay1.BackColor = obj.Telem.Relay[0] == 0 ? Color.White : obj.Telem.Relay[0] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelay2.BackColor = obj.Telem.Relay[1] == 0 ? Color.White : obj.Telem.Relay[1] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelay3.BackColor = obj.Telem.Relay[2] == 0 ? Color.White : obj.Telem.Relay[2] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelay4.BackColor = obj.Telem.Relay[3] == 0 ? Color.White : obj.Telem.Relay[3] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelay5.BackColor = obj.Telem.Relay[4] == 0 ? Color.White : obj.Telem.Relay[4] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelay6.BackColor = obj.Telem.Relay[5] == 0 ? Color.White : obj.Telem.Relay[5] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelay7.BackColor = obj.Telem.Relay[6] == 0 ? Color.White : obj.Telem.Relay[6] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelay8.BackColor = obj.Telem.Relay[7] == 0 ? Color.White : obj.Telem.Relay[7] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelayF1.BackColor = obj.Telem.RelayFrw[0] == 0 ? Color.White : obj.Telem.RelayFrw[0] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelayF2.BackColor = obj.Telem.RelayFrw[1] == 0 ? Color.White : obj.Telem.RelayFrw[1] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelayF3.BackColor = obj.Telem.RelayFrw[2] == 0 ? Color.White : obj.Telem.RelayFrw[2] == 1 ? Color.LightGreen : Color.LightGray;
        buttonRelayF4.BackColor = obj.Telem.RelayFrw[3] == 0 ? Color.White : obj.Telem.RelayFrw[3] == 1 ? Color.LightGreen : Color.LightGray;
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
        buttonRelayF1.Click += ButtonRelayF1_Click;
        buttonRelayF2.Click += ButtonRelayF2_Click;
        buttonRelayF3.Click += ButtonRelayF3_Click;
        buttonRelayF4.Click += ButtonRelayF4_Click;

        buttonFpv1Open.Click += ButtonFpv1Open_Click;
        buttonFpv1Close.Click += ButtonFpv1Close_Click;
        buttonFpv1Stop.Click += ButtonFpv1Stop_Click;
        buttonFpv1On.Click += ButtonFpv1On_Click;
        buttonFpv1Off.Click += ButtonFpv1Off_Click;
        buttonFpv2Open.Click += ButtonFpv2Open_Click;
        buttonFpv2Close.Click += ButtonFpv2Close_Click;
        buttonFpv2Stop.Click += ButtonFpv2Stop_Click;
        buttonFpv2On.Click += ButtonFpv2On_Click;
        buttonFpv2Off.Click += ButtonFpv2Off_Click;
        buttonFpv3Open.Click += ButtonFpv3Open_Click;
        buttonFpv3Close.Click += ButtonFpv3Close_Click;
        buttonFpv3Stop.Click += ButtonFpv3Stop_Click;
        buttonFpv3On.Click += ButtonFpv3On_Click;
        buttonFpv3Off.Click += ButtonFpv3Off_Click;
        buttonFpv4Open.Click += ButtonFpv4Open_Click;
        buttonFpv4Close.Click += ButtonFpv4Close_Click;
        buttonFpv4Stop.Click += ButtonFpv4Stop_Click;
        buttonFpv4On.Click += ButtonFpv4On_Click;
        buttonFpv4Off.Click += ButtonFpv4Off_Click;

        buttonPower1Off.Click += ButtonPower1Off_Click;
        buttonPower1On.Click += ButtonPower1On_Click;
        buttonPower2Off.Click += ButtonPower2Off_Click;
        buttonPower2On.Click += ButtonPower2On_Click;
        buttonPower3Off.Click += ButtonPower3Off_Click;
        buttonPower3On.Click += ButtonPower3On_Click;
        buttonPower4Off.Click += ButtonPower4Off_Click;
        buttonPower4On.Click += ButtonPower4On_Click;

        buttonBoomCheck.Click += ButtonBoomCheck_Click;
        buttonBoom.Click += ButtonBoom_Click;

        buttonBoomCheck.BackColor = Color.White;
        buttonBoom.Enabled = !(buttonBoomCheck.BackColor == Color.White);
        buttonBoom.BackColor = buttonBoomCheck.BackColor == Color.White ? Color.LightGray : Color.White;

        _ = _dx.StartAsync(default);
    }

    private async void ButtonBoom_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync( 0x0F0000FF);
    }

    private async void ButtonBoomCheck_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;

        if (buttonBoomCheck.BackColor == Color.White)
        {
            await obj.SendCommandAsync(0x0F000001);
            buttonBoomCheck.BackColor = Color.LightPink;
            return;
        }

        await obj.SendCommandAsync(0x0F000000);
        buttonBoomCheck.BackColor = Color.White;
    }

    private async void ButtonPower1Off_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x22000000);
    }
    private async void ButtonPower1On_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x22000001);
    }
    private async void ButtonPower2Off_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x22010000);
    }
    private async void ButtonPower2On_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x22010001);
    }
    private async void ButtonPower3Off_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x22020000);
    }
    private async void ButtonPower3On_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x22020001);
    }
    private async void ButtonPower4Off_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x22030000);
    }
    private async void ButtonPower4On_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x22030001);
    }

    private async void ButtonFpv1Open_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11000001);
    }
    private async void ButtonFpv1Close_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11000000);
    }
    private async void ButtonFpv1Stop_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11000002);
    }
    private async void ButtonFpv1Off_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11000010);
    }
    private async void ButtonFpv1On_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11000011);
    }
    private async void ButtonFpv2Open_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11010001);
    }
    private async void ButtonFpv2Close_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11010000);
    }
    private async void ButtonFpv2Stop_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11010002);
    }
    private async void ButtonFpv2Off_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11010010);
    }
    private async void ButtonFpv2On_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11010011);
    }
    private async void ButtonFpv3Open_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11020001);
    }
    private async void ButtonFpv3Close_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11020000);
    }
    private async void ButtonFpv3Stop_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11020002);
    }
    private async void ButtonFpv3Off_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11020010);
    }
    private async void ButtonFpv3On_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11020011);
    }
    private async void ButtonFpv4Open_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11030001);
    }
    private async void ButtonFpv4Close_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11030000);
    }
    private async void ButtonFpv4Stop_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11030002);
    }
    private async void ButtonFpv4Off_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11030010);
    }
    private async void ButtonFpv4On_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync(0x11030011);
    }

    private async void ButtonRelay1_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var relNew = new RelaysForWrite
        {
            Values =
            {
                [0] = buttonRelay1.BackColor == Color.LightGreen ? 0.0f : 1.0f,
                [1] = obj.Telem.Relay[1].Equals(1.0f) ? 1.0f : 0.0f,
                [2] = obj.Telem.Relay[2].Equals(1.0f) ? 1.0f : 0.0f,
                [3] = obj.Telem.Relay[3].Equals(1.0f) ? 1.0f : 0.0f,
                [4] = obj.Telem.Relay[4].Equals(1.0f) ? 1.0f : 0.0f,
                [5] = obj.Telem.Relay[5].Equals(1.0f) ? 1.0f : 0.0f,
                [6] = obj.Telem.Relay[6].Equals(1.0f) ? 1.0f : 0.0f,
                [7] = obj.Telem.Relay[7].Equals(1.0f) ? 1.0f : 0.0f
            },
            ValuesFrw =
            {
                [0] = obj.Telem.RelayFrw[0].Equals(1.0f) ? 1.0f : 0.0f,
                [1] = obj.Telem.RelayFrw[1].Equals(1.0f) ? 1.0f : 0.0f,
                [2] = obj.Telem.RelayFrw[2].Equals(1.0f) ? 1.0f : 0.0f,
                [3] = obj.Telem.RelayFrw[3].Equals(1.0f) ? 1.0f : 0.0f
            }
        };
        await obj.RewriteRelayAsync(relNew);
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
        relNew.ValuesFrw[0] = obj.Telem.RelayFrw[0] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[1] = obj.Telem.RelayFrw[1] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[2] = obj.Telem.RelayFrw[2] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[3] = obj.Telem.RelayFrw[3] == 1.0f ? 1.0f : 0.0f;
        await obj.RewriteRelayAsync(relNew);
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
        relNew.ValuesFrw[0] = obj.Telem.RelayFrw[0] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[1] = obj.Telem.RelayFrw[1] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[2] = obj.Telem.RelayFrw[2] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[3] = obj.Telem.RelayFrw[3] == 1.0f ? 1.0f : 0.0f;
        await obj.RewriteRelayAsync(relNew);
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
        relNew.ValuesFrw[0] = obj.Telem.RelayFrw[0] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[1] = obj.Telem.RelayFrw[1] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[2] = obj.Telem.RelayFrw[2] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[3] = obj.Telem.RelayFrw[3] == 1.0f ? 1.0f : 0.0f;
        await obj.RewriteRelayAsync(relNew);
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
        relNew.ValuesFrw[0] = obj.Telem.RelayFrw[0] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[1] = obj.Telem.RelayFrw[1] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[2] = obj.Telem.RelayFrw[2] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[3] = obj.Telem.RelayFrw[3] == 1.0f ? 1.0f : 0.0f;
        await obj.RewriteRelayAsync(relNew);
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
        relNew.ValuesFrw[0] = obj.Telem.RelayFrw[0] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[1] = obj.Telem.RelayFrw[1] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[2] = obj.Telem.RelayFrw[2] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[3] = obj.Telem.RelayFrw[3] == 1.0f ? 1.0f : 0.0f;
        await obj.RewriteRelayAsync(relNew);
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
        relNew.ValuesFrw[0] = obj.Telem.RelayFrw[0] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[1] = obj.Telem.RelayFrw[1] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[2] = obj.Telem.RelayFrw[2] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[3] = obj.Telem.RelayFrw[3] == 1.0f ? 1.0f : 0.0f;
        await obj.RewriteRelayAsync(relNew);
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
        relNew.ValuesFrw[0] = obj.Telem.RelayFrw[0] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[1] = obj.Telem.RelayFrw[1] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[2] = obj.Telem.RelayFrw[2] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[3] = obj.Telem.RelayFrw[3] == 1.0f ? 1.0f : 0.0f;
        await obj.RewriteRelayAsync(relNew);
    }
    private async void ButtonRelayF1_Click(object? sender, EventArgs e)
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
        relNew.Values[7] = obj.Telem.Relay[7] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[0] = buttonRelayF1.BackColor == Color.LightGreen ? 0.0f : 1.0f;
        relNew.ValuesFrw[1] = obj.Telem.RelayFrw[1] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[2] = obj.Telem.RelayFrw[2] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[3] = obj.Telem.RelayFrw[3] == 1.0f ? 1.0f : 0.0f;
        await obj.RewriteRelayAsync(relNew);
    }
    private async void ButtonRelayF2_Click(object? sender, EventArgs e)
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
        relNew.Values[7] = obj.Telem.Relay[7] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[0] = obj.Telem.RelayFrw[0] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[1] = buttonRelayF2.BackColor == Color.LightGreen ? 0.0f : 1.0f;
        relNew.ValuesFrw[2] = obj.Telem.RelayFrw[2] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[3] = obj.Telem.RelayFrw[3] == 1.0f ? 1.0f : 0.0f;
        await obj.RewriteRelayAsync(relNew);
    }
    private async void ButtonRelayF3_Click(object? sender, EventArgs e)
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
        relNew.Values[7] = obj.Telem.Relay[7] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[0] = obj.Telem.RelayFrw[0] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[1] = obj.Telem.RelayFrw[1] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[2] = buttonRelayF3.BackColor == Color.LightGreen ? 0.0f : 1.0f;
        relNew.ValuesFrw[3] = obj.Telem.RelayFrw[3] == 1.0f ? 1.0f : 0.0f;
        await obj.RewriteRelayAsync(relNew);
    }
    private async void ButtonRelayF4_Click(object? sender, EventArgs e)
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
        relNew.Values[7] = obj.Telem.Relay[7] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[0] = obj.Telem.RelayFrw[0] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[1] = obj.Telem.RelayFrw[1] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[2] = obj.Telem.RelayFrw[2] == 1.0f ? 1.0f : 0.0f;
        relNew.ValuesFrw[3] = buttonRelayF4.BackColor == Color.LightGreen ? 0.0f : 1.0f;
        await obj.RewriteRelayAsync(relNew);
    }
}
