using WarGame.Forms.Map;
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
        
        buttonAliveServer.Visible = visible;
        buttonAliveCamFrwd.Visible = visible;
        buttonAliveCamBack.Visible = visible;
        buttonAliveCamLeft.Visible = visible;
        buttonAliveCamRight.Visible = visible;
        buttonAliveCamPtz.Visible = visible;
        buttonAliveCamWarm.Visible = visible;
        buttonAliveCamFpv1.Visible = visible;
        buttonAliveCamFpv2.Visible = visible;
        buttonAliveCamFpv3.Visible = visible;
        buttonAliveCamFpv4.Visible = visible;
        buttonAliveCompas.Visible = visible;
        buttonAliveCubic.Visible = visible;
        buttonAliveGpsF.Visible = visible;
        buttonAliveGpsB.Visible = visible;
        buttonAliveRelay.Visible = visible;
        buttonAliveRelayFrw.Visible = visible;
        buttonAliveFuel.Visible = visible;
        buttonAliveInertial.Visible = visible;
        buttonAliveEngine.Visible = visible;
        buttonAliveBox1.Visible = visible;
        buttonAliveBox2.Visible = visible;
        buttonAliveBox3.Visible = visible;
        buttonAliveBox4.Visible = visible;
        buttonAliveBoxCrsf1.Visible = visible;
        buttonAliveBoxCrsf2.Visible = visible;
        buttonAliveBoxCrsf3.Visible = visible;
        buttonAliveBoxCrsf4.Visible = visible;
        buttonAlivePtzRs485.Visible = visible;
        buttonAliveMosfets.Visible = visible;
        buttonAliveBoom.Visible = visible;
        buttonLogEnable.Visible = visible;
        buttonWifi58In.Visible = visible;
        buttonWifi58Out.Visible = visible;
        buttonWifiMeshIn.Visible = visible;
        buttonWifiMeshOut.Visible = visible;
        buttonWifiCbsIn.Visible = visible;
        buttonWifiCbsOut.Visible = visible;

        buttonBoom.Enabled = !(buttonBoomCheck.BackColor == Color.White);
        buttonBoom.BackColor = buttonBoomCheck.BackColor == Color.White ? Color.LightGray : Color.White;

        if (obj == null)
        {
            buttonBoomCheck.BackColor = Color.White;
            return;
        }

        buttonAliveServer.BackColor = ((obj.Telem.AliveCheck &   0b0000000000000000000000000000000000000000000000000000000000000001) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveCamFrwd.BackColor = ((obj.Telem.AliveCheck &  0b0000000000000000000000000000000000000000000000000000000000000010) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveCamBack.BackColor = ((obj.Telem.AliveCheck &  0b0000000000000000000000000000000000000000000000000000000000000100) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveCamLeft.BackColor = ((obj.Telem.AliveCheck &  0b0000000000000000000000000000000000000000000000000000000000001000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveCamRight.BackColor = ((obj.Telem.AliveCheck & 0b0000000000000000000000000000000000000000000000000000000000010000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveCamPtz.BackColor = ((obj.Telem.AliveCheck &   0b0000000000000000000000000000000000000000000000000000000000100000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveCamWarm.BackColor = ((obj.Telem.AliveCheck &  0b0000000000000000000000000000000000000000000000000000000001000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveCamFpv1.BackColor = ((obj.Telem.AliveCheck &  0b0000000000000000000000000000000000000000000000000000000010000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveCamFpv2.BackColor = ((obj.Telem.AliveCheck &  0b0000000000000000000000000000000000000000000000000000000100000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveCamFpv3.BackColor = ((obj.Telem.AliveCheck &  0b0000000000000000000000000000000000000000000000000000001000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveCamFpv4.BackColor = ((obj.Telem.AliveCheck &  0b0000000000000000000000000000000000000000000000000000010000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveCompas.BackColor = ((obj.Telem.AliveCheck &   0b0000000000000000000000000000000000000000000000000000100000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveCubic.BackColor = ((obj.Telem.AliveCheck &    0b0000000000000000000000000000000000000000000000000001000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveGpsF.BackColor = ((obj.Telem.AliveCheck &     0b0000000000000000000000000000000000000000000000000010000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveGpsB.BackColor = ((obj.Telem.AliveCheck &     0b0000000000000000000000000000000000000000000000000100000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveRelay.BackColor = ((obj.Telem.AliveCheck &    0b0000000000000000000000000000000000000000000000001000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveRelayFrw.BackColor = ((obj.Telem.AliveCheck & 0b0000000000000000000000000000000000000000000000010000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveFuel.BackColor = ((obj.Telem.AliveCheck &     0b0000000000000000000000000000000000000000000000100000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveInertial.BackColor = ((obj.Telem.AliveCheck & 0b0000000000000000000000000000000000000000000001000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveEngine.BackColor = ((obj.Telem.AliveCheck &   0b0000000000000000000000000000000000000000000010000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveBox1.BackColor = ((obj.Telem.AliveCheck &     0b0000000000000000000000000000000000000000000100000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveBox2.BackColor = ((obj.Telem.AliveCheck &     0b0000000000000000000000000000000000000000001000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveBox3.BackColor = ((obj.Telem.AliveCheck &     0b0000000000000000000000000000000000000000010000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveBox4.BackColor = ((obj.Telem.AliveCheck &     0b0000000000000000000000000000000000000000100000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveBoxCrsf1.BackColor = ((obj.Telem.AliveCheck & 0b0000000000000000000000000000000000000001000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveBoxCrsf2.BackColor = ((obj.Telem.AliveCheck & 0b0000000000000000000000000000000000000010000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveBoxCrsf3.BackColor = ((obj.Telem.AliveCheck & 0b0000000000000000000000000000000000000100000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveBoxCrsf4.BackColor = ((obj.Telem.AliveCheck & 0b0000000000000000000000000000000000001000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAlivePtzRs485.BackColor = ((obj.Telem.AliveCheck & 0b0000000000000000000000000000000000010000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveMosfets.BackColor = ((obj.Telem.AliveCheck &  0b0000000000000000000000000000000000100000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonAliveBoom.BackColor = ((obj.Telem.AliveCheck &     0b0000000000000000000000000000000001000000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonLogEnable.BackColor = ((obj.Telem.AliveCheck &     0b0000000000000000000000000000000010000000000000000000000000000000) > 0) ? Color.Yellow : Color.White;
        buttonWifi58In.BackColor = ((obj.Telem.AliveCheck &      0b0000000000000000000000000000000100000000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonWifi58Out.BackColor = ((obj.Telem.AliveCheck &     0b0000000000000000000000000000001000000000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonWifiMeshIn.BackColor = ((obj.Telem.AliveCheck &    0b0000000000000000000000000000010000000000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonWifiMeshOut.BackColor = ((obj.Telem.AliveCheck &   0b0000000000000000000000000000100000000000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonWifiCbsIn.BackColor = ((obj.Telem.AliveCheck &     0b0000000000000000000000000001000000000000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonWifiCbsOut.BackColor = ((obj.Telem.AliveCheck &    0b0000000000000000000000000010000000000000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;

        buttonRelay1.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? Color.LightGray : obj.Telem.Relay[0] == 0 ? Color.White : Color.LightGreen;
        buttonRelay2.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? Color.LightGray : obj.Telem.Relay[1] == 0 ? Color.White : Color.LightGreen;
        buttonRelay3.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? Color.LightGray : obj.Telem.Relay[2] == 0 ? Color.White : Color.LightGreen;
        buttonRelay4.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? Color.LightGray : obj.Telem.Relay[3] == 0 ? Color.White : Color.LightGreen;
        buttonRelay5.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? Color.LightGray : obj.Telem.Relay[4] == 0 ? Color.White : Color.LightGreen;
        buttonRelay6.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? Color.LightGray : obj.Telem.Relay[5] == 0 ? Color.White : Color.LightGreen;
        buttonRelay7.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? Color.LightGray : obj.Telem.Relay[6] == 0 ? Color.White : Color.LightGreen;
        buttonRelay8.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? Color.LightGray : obj.Telem.Relay[7] == 0 ? Color.White : Color.LightGreen;
        buttonRelayF1.BackColor = buttonAliveRelayFrw.BackColor == Color.LightPink ? Color.LightGray : obj.Telem.RelayFrw[0] == 0 ? Color.White : Color.LightGreen;
        buttonRelayF2.BackColor = buttonAliveRelayFrw.BackColor == Color.LightPink ? Color.LightGray : obj.Telem.RelayFrw[1] == 0 ? Color.White : Color.LightGreen;
        buttonRelayF3.BackColor = buttonAliveRelayFrw.BackColor == Color.LightPink ? Color.LightGray : obj.Telem.RelayFrw[2] == 0 ? Color.White : Color.LightGreen;
        buttonRelayF4.BackColor = buttonAliveRelayFrw.BackColor == Color.LightPink ? Color.LightGray : obj.Telem.RelayFrw[3] == 0 ? Color.White : Color.LightGreen;
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
        buttonLogEnable.Click += ButtonLogEnable_Click;
        
        buttonBoomCheck.BackColor = Color.White;
        buttonBoom.Enabled = !(buttonBoomCheck.BackColor == Color.White);
        buttonBoom.BackColor = buttonBoomCheck.BackColor == Color.White ? Color.LightGray : Color.White;

        _ = _dx.StartAsync(default);
    }

    private async void ButtonLogEnable_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonLogEnable.BackColor == Color.Yellow ? 0xFF000000 : 0xFF000001));
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
        await obj.SendCommandAsync((uint)(buttonRelay1.BackColor == Color.LightGreen ? 0x30010100 : 0x30010101));
    }

    private async void ButtonRelay2_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonRelay2.BackColor == Color.LightGreen ? 0x30010200 : 0x30010201));
    }
    private async void ButtonRelay3_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonRelay3.BackColor == Color.LightGreen ? 0x30010300 : 0x30010301));
    }
    private async void ButtonRelay4_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonRelay4.BackColor == Color.LightGreen ? 0x30010400 : 0x30010401));
    }
    private async void ButtonRelay5_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonRelay5.BackColor == Color.LightGreen ? 0x30010500 : 0x30010501));
    }
    private async void ButtonRelay6_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonRelay6.BackColor == Color.LightGreen ? 0x30010600 : 0x30010601));
    }
    private async void ButtonRelay7_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonRelay7.BackColor == Color.LightGreen ? 0x30010700 : 0x30010701));
    }
    private async void ButtonRelay8_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonRelay8.BackColor == Color.LightGreen ? 0x30010800 : 0x30010801));
    }
    private async void ButtonRelayF1_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonRelayF1.BackColor == Color.LightGreen ? 0x30020100 : 0x30020101));
    }
    private async void ButtonRelayF2_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonRelayF2.BackColor == Color.LightGreen ? 0x30020200 : 0x30020201));
    }
    private async void ButtonRelayF3_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonRelayF3.BackColor == Color.LightGreen ? 0x30020300 : 0x30020301));
    }
    private async void ButtonRelayF4_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonRelayF4.BackColor == Color.LightGreen ? 0x30020400 : 0x30020401));
    }
}
