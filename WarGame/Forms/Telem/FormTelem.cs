using WarGame.Forms.Map;
using WarGame.Model;
using WarGame.Resources;

namespace WarGame.Forms.Telem;
public sealed partial class FormTelem : Form
{

    private Point _posFromDisplays;
    private readonly SharpDx _dx;
    private readonly Color _grayColor = Color.FromArgb(0xFF, 0xA8, 0xA8, 0xA8);

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

        buttonMosfet1.Visible = visible;
        buttonMosfet2.Visible = visible;
        buttonMosfet3.Visible = visible;
        buttonMosfet4.Visible = visible;
        buttonBox1.Visible = visible;
        buttonBox2.Visible = visible;
        buttonBox3.Visible = visible;
        buttonBox4.Visible = visible;
        buttonFpv1.Visible = visible;
        buttonFpv2.Visible = visible;
        buttonFpv3.Visible = visible;
        buttonFpv4.Visible = visible;

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

        buttonUseGyroCompas.Visible = visible;
        buttonUseGyroCubic.Visible = visible;
        buttonUseGyroInert.Visible = visible;
        buttonUseCompasCompas.Visible = visible;
        buttonUseCompasInert.Visible = visible;
        buttonUsePosInert.Visible = visible;
        buttonUsePosGpsF.Visible = visible;
        buttonUsePosGpsB.Visible = visible;

        buttonBoom.Enabled = buttonBoomCheck.BackColor == Color.LightPink;
        buttonBoom.BackColor = buttonBoomCheck.BackColor == Color.LightPink ? Color.White : _grayColor;

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
        buttonWifi58In.BackColor = ((obj.Telem.AliveCheck &      0b0000000000000000000000000000000010000000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonWifi58Out.BackColor = ((obj.Telem.AliveCheck &     0b0000000000000000000000000000000100000000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonWifiMeshIn.BackColor = ((obj.Telem.AliveCheck &    0b0000000000000000000000000000001000000000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonWifiMeshOut.BackColor = ((obj.Telem.AliveCheck &   0b0000000000000000000000000000010000000000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonWifiCbsIn.BackColor = ((obj.Telem.AliveCheck &     0b0000000000000000000000000000100000000000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;
        buttonWifiCbsOut.BackColor = ((obj.Telem.AliveCheck &    0b0000000000000000000000000001000000000000000000000000000000000000) > 0) ? Color.LightGreen : Color.LightPink;

        buttonLogEnable.BackColor = ((obj.Telem.EnableCheck &                                                                     0b0000000000000000000000000000000000000000000000000000000000000001) > 0) ? Color.Yellow : Color.White;
        buttonRelay1.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? _grayColor :     (obj.Telem.EnableCheck &        0b0000000000000000000000000000000000000000000000000000000000000010) == 0 ? Color.White : Color.LightGreen;
        buttonRelay2.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? _grayColor :     (obj.Telem.EnableCheck &        0b0000000000000000000000000000000000000000000000000000000000000100) == 0 ? Color.White : Color.LightGreen;
        buttonRelay3.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? _grayColor :     (obj.Telem.EnableCheck &        0b0000000000000000000000000000000000000000000000000000000000001000) == 0 ? Color.White : Color.LightGreen;
        buttonRelay4.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? _grayColor :     (obj.Telem.EnableCheck &        0b0000000000000000000000000000000000000000000000000000000000010000) == 0 ? Color.White : Color.LightGreen;
        buttonRelay5.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? _grayColor :     (obj.Telem.EnableCheck &        0b0000000000000000000000000000000000000000000000000000000000100000) == 0 ? Color.White : Color.LightGreen;
        buttonRelay6.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? _grayColor :     (obj.Telem.EnableCheck &        0b0000000000000000000000000000000000000000000000000000000001000000) == 0 ? Color.White : Color.LightGreen;
        buttonRelay7.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? _grayColor :     (obj.Telem.EnableCheck &        0b0000000000000000000000000000000000000000000000000000000010000000) == 0 ? Color.White : Color.LightGreen;
        buttonRelay8.BackColor = buttonAliveRelay.BackColor == Color.LightPink ? _grayColor :     (obj.Telem.EnableCheck &        0b0000000000000000000000000000000000000000000000000000000100000000) == 0 ? Color.White : Color.LightGreen;
        buttonRelayF1.BackColor = buttonAliveRelayFrw.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &        0b0000000000000000000000000000000000000000000000000000001000000000) == 0 ? Color.White : Color.LightGreen;
        buttonRelayF2.BackColor = buttonAliveRelayFrw.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &        0b0000000000000000000000000000000000000000000000000000010000000000) == 0 ? Color.White : Color.LightGreen;
        buttonRelayF3.BackColor = buttonAliveRelayFrw.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &        0b0000000000000000000000000000000000000000000000000000100000000000) == 0 ? Color.White : Color.LightGreen;
        buttonRelayF4.BackColor = buttonAliveRelayFrw.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &        0b0000000000000000000000000000000000000000000000000001000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonMosfet1.BackColor = buttonAliveMosfets.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &         0b0000000000000000000000000000000000000000000000000010000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonMosfet2.BackColor = buttonAliveMosfets.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &         0b0000000000000000000000000000000000000000000000000100000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonMosfet3.BackColor = buttonAliveMosfets.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &         0b0000000000000000000000000000000000000000000000001000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonMosfet4.BackColor = buttonAliveMosfets.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &         0b0000000000000000000000000000000000000000000000010000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonBox1.BackColor = buttonAliveBox1.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &               0b0000000000000000000000000000000000000000000000100000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonBox2.BackColor = buttonAliveBox2.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &               0b0000000000000000000000000000000000000000000001000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonBox3.BackColor = buttonAliveBox3.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &               0b0000000000000000000000000000000000000000000010000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonBox4.BackColor = buttonAliveBox4.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &               0b0000000000000000000000000000000000000000000100000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonFpv1.BackColor = buttonAliveBoxCrsf1.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &           0b0000000000000000000000000000000000000000001000000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonFpv2.BackColor = buttonAliveBoxCrsf2.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &           0b0000000000000000000000000000000000000000010000000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonFpv3.BackColor = buttonAliveBoxCrsf3.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &           0b0000000000000000000000000000000000000000100000000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonFpv4.BackColor = buttonAliveBoxCrsf4.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &           0b0000000000000000000000000000000000000001000000000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonBoomCheck.BackColor = buttonAliveBoom.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &          0b0000000000000000000000000000000000000010000000000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonUseGyroCompas.BackColor = buttonAliveCompas.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &    0b0000000000000000000000000000000000000100000000000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonUseGyroInert.BackColor = buttonAliveInertial.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &   0b0000000000000000000000000000000000001000000000000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonUseGyroCubic.BackColor = buttonAliveCubic.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &      0b0000000000000000000000000000000000010000000000000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonUseCompasCompas.BackColor = buttonAliveCompas.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &  0b0000000000000000000000000000000000100000000000000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonUseCompasInert.BackColor = buttonAliveInertial.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck & 0b0000000000000000000000000000000001000000000000000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonUsePosInert.BackColor = buttonAliveInertial.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &    0b0000000000000000000000000000000010000000000000000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonUsePosGpsF.BackColor = buttonAliveGpsF.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &         0b0000000000000000000000000000000100000000000000000000000000000000) == 0 ? Color.White : Color.LightGreen;
        buttonUsePosGpsB.BackColor = buttonAliveGpsB.BackColor == Color.LightPink ? _grayColor : (obj.Telem.EnableCheck &         0b0000000000000000000000000000001000000000000000000000000000000000) == 0 ? Color.White : Color.LightGreen;

        buttonRelay1.Enabled = buttonRelay1.BackColor != _grayColor;
        buttonRelay2.Enabled = buttonRelay2.BackColor != _grayColor;
        buttonRelay3.Enabled = buttonRelay3.BackColor != _grayColor;
        buttonRelay4.Enabled = buttonRelay4.BackColor != _grayColor;
        buttonRelay5.Enabled = buttonRelay5.BackColor != _grayColor;
        buttonRelay6.Enabled = buttonRelay6.BackColor != _grayColor;
        buttonRelay7.Enabled = buttonRelay7.BackColor != _grayColor;
        buttonRelay8.Enabled = buttonRelay8.BackColor != _grayColor;
        buttonRelayF1.Enabled = buttonRelayF1.BackColor != _grayColor;
        buttonRelayF2.Enabled = buttonRelayF2.BackColor != _grayColor;
        buttonRelayF3.Enabled = buttonRelayF3.BackColor != _grayColor;
        buttonRelayF4.Enabled = buttonRelayF4.BackColor != _grayColor;
        buttonMosfet1.Enabled = buttonMosfet1.BackColor != _grayColor;
        buttonMosfet2.Enabled = buttonMosfet2.BackColor != _grayColor;
        buttonMosfet3.Enabled = buttonMosfet3.BackColor != _grayColor;
        buttonMosfet4.Enabled = buttonMosfet4.BackColor != _grayColor;
        buttonBox1.Enabled = buttonBox1.BackColor != _grayColor;
        buttonBox2.Enabled = buttonBox2.BackColor != _grayColor;
        buttonBox3.Enabled = buttonBox3.BackColor != _grayColor;
        buttonBox4.Enabled = buttonBox4.BackColor != _grayColor;
        buttonFpv1.Enabled = buttonFpv1.BackColor != _grayColor;
        buttonFpv2.Enabled = buttonFpv2.BackColor != _grayColor;
        buttonFpv3.Enabled = buttonFpv3.BackColor != _grayColor;
        buttonFpv4.Enabled = buttonFpv4.BackColor != _grayColor;
        buttonUseGyroCompas.Enabled = buttonUseGyroCompas.BackColor != _grayColor;
        buttonUseGyroInert.Enabled = buttonUseGyroInert.BackColor != _grayColor;
        buttonUseGyroCubic.Enabled = buttonUseGyroCubic.BackColor != _grayColor;
        buttonUseCompasCompas.Enabled = buttonUseCompasCompas.BackColor != _grayColor;
        buttonUseCompasInert.Enabled = buttonUseCompasInert.BackColor != _grayColor;
        buttonUsePosInert.Enabled = buttonUsePosInert.BackColor != _grayColor;
        buttonUsePosGpsF.Enabled = buttonUsePosGpsF.BackColor != _grayColor;
        buttonUsePosGpsB.Enabled = buttonUsePosGpsB.BackColor != _grayColor;
        buttonBoomCheck.Enabled = buttonBoomCheck.BackColor != _grayColor;

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

        buttonBox1.Click += ButtonBox1_Click;
        buttonBox2.Click += ButtonBox2_Click;
        buttonBox3.Click += ButtonBox3_Click;
        buttonBox4.Click += ButtonBox4_Click;
        buttonFpv1.Click += ButtonFpv1_Click;
        buttonFpv2.Click += ButtonFpv2_Click;
        buttonFpv3.Click += ButtonFpv3_Click;
        buttonFpv4.Click += ButtonFpv4_Click;

        buttonMosfet1.Click += ButtonMosfet1_Click;
        buttonMosfet2.Click += ButtonMosfet2_Click;
        buttonMosfet3.Click += ButtonMosfet3_Click;
        buttonMosfet4.Click += ButtonMosfet4_Click;

        buttonBoomCheck.Click += ButtonBoomCheck_Click;
        buttonBoom.Click += ButtonBoom_Click;
        buttonLogEnable.Click += ButtonLogEnable_Click;

        buttonUseGyroCompas.Click += ButtonUseGyroCompas_Click;
        buttonUseGyroInert.Click += ButtonUseGyroInert_Click;
        buttonUseGyroCubic.Click += ButtonUseGyroCubic_Click;
        buttonUseCompasCompas.Click += ButtonUseCompasCompas_Click;
        buttonUseCompasInert.Click += ButtonUseCompasInert_Click;
        buttonUsePosInert.Click += ButtonUsePosInert_Click;
        buttonUsePosGpsF.Click += ButtonUsePosGpsF_Click;
        buttonUsePosGpsB.Click += ButtonUsePosGpsB_Click;

        buttonBoomCheck.BackColor = _grayColor;
        buttonBoom.Enabled = buttonBoomCheck.BackColor == Color.LightPink;
        buttonBoom.BackColor = buttonBoomCheck.BackColor == Color.LightPink ? Color.White : _grayColor;

        _ = _dx.StartAsync(default);
    }

    private async void ButtonUsePosGpsB_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var bits = 0;
        bits += buttonUsePosInert.BackColor == Color.LightGreen ? 0b00000001 : 0b00000000;
        bits += buttonUsePosGpsF.BackColor == Color.LightGreen ? 0b00000010 : 0b00000000;
        bits += buttonUsePosGpsB.BackColor == Color.LightGreen ? 0 : 0b00000100;
        if (bits == 0) return; // Все выключится, не даем выключить
        await obj.SendCommandAsync((uint)(0xEE020000 + bits));
    }

    private async void ButtonUsePosGpsF_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var bits = 0;
        bits += buttonUsePosInert.BackColor == Color.LightGreen ? 0b00000001 : 0b00000000;
        bits += buttonUsePosGpsF.BackColor == Color.LightGreen ? 0 : 0b00000010;
        bits += buttonUsePosGpsB.BackColor == Color.LightGreen ? 0b00000100 : 0b00000000;
        if (bits == 0) return; // Все выключится, не даем выключить
        await obj.SendCommandAsync((uint)(0xEE020000 + bits));
    }

    private async void ButtonUsePosInert_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var bits = 0;
        bits += buttonUsePosInert.BackColor == Color.LightGreen ? 0 : 0b00000001;
        bits += buttonUsePosGpsF.BackColor == Color.LightGreen ? 0b00000010 : 0b00000000;
        bits += buttonUsePosGpsB.BackColor == Color.LightGreen ? 0b00000100 : 0b00000000;
        if (bits == 0) return; // Все выключится, не даем выключить
        await obj.SendCommandAsync((uint)(0xEE020000 + bits));
    }

    private async void ButtonUseCompasInert_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var bits = 0;
        bits += buttonUseCompasCompas.BackColor == Color.LightGreen ? 0b00000001 : 0b00000000;
        bits += buttonUseCompasInert.BackColor == Color.LightGreen ? 0 : 0b00000010;
        if (bits == 0) return; // Все выключится, не даем выключить
        await obj.SendCommandAsync((uint)(0xEE010000 + bits));
    }

    private async void ButtonUseCompasCompas_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var bits = 0;
        bits += buttonUseCompasCompas.BackColor == Color.LightGreen ? 0 : 0b00000001;
        bits += buttonUseCompasInert.BackColor == Color.LightGreen ? 0b00000010 : 0b00000000;
        if (bits == 0) return; // Все выключится, не даем выключить
        await obj.SendCommandAsync((uint)(0xEE010000 + bits));
    }

    private async void ButtonUseGyroCubic_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var bits = 0;
        bits += buttonUseGyroCompas.BackColor == Color.LightGreen ? 0b00000001 : 0b00000000;
        bits += buttonUseGyroInert.BackColor == Color.LightGreen ? 0b00000010 : 0b00000000;
        bits += buttonUseGyroCubic.BackColor == Color.LightGreen ? 0 : 0b00000100;
        if (bits == 0) return; // Все выключится, не даем выключить
        await obj.SendCommandAsync((uint)(0xEE000000 + bits));
    }

    private async void ButtonUseGyroInert_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var bits = 0;
        bits += buttonUseGyroCompas.BackColor == Color.LightGreen ? 0b00000001 : 0b00000000;
        bits += buttonUseGyroInert.BackColor == Color.LightGreen ? 0 : 0b00000010; 
        bits += buttonUseGyroCubic.BackColor == Color.LightGreen ? 0b00000100 : 0b00000000;
        if (bits == 0) return; // Все выключится, не даем выключить
        await obj.SendCommandAsync((uint)(0xEE000000 + bits));
    }

    private async void ButtonUseGyroCompas_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        var bits = 0;
        bits += buttonUseGyroCompas.BackColor == Color.LightGreen ? 0 : 0b00000001;
        bits += buttonUseGyroInert.BackColor == Color.LightGreen ? 0b00000010 : 0b00000000;
        bits += buttonUseGyroCubic.BackColor == Color.LightGreen ? 0b00000100 : 0b00000000;
        if (bits == 0) return; // Все выключится, не даем выключить
        await obj.SendCommandAsync((uint)(0xEE000000 + bits));
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
        await obj.SendCommandAsync((uint)(buttonBoomCheck.BackColor == Color.White ? 0x0F000001 : 0x0F000000));
    }

    private async void ButtonMosfet1_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonMosfet1.BackColor == Color.White ? 0x22000001 : 0x22000000));
    }
    private async void ButtonMosfet2_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonMosfet2.BackColor == Color.White ? 0x22010001 : 0x22010000));
    }
    private async void ButtonMosfet3_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonMosfet3.BackColor == Color.White ? 0x22020001 : 0x22020000));
    }
    private async void ButtonMosfet4_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonMosfet4.BackColor == Color.White ? 0x22030001 : 0x22030000));
    }

    private async void ButtonBox1_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonBox1.BackColor == Color.White ? 0x11000001 : 0x11000000));
    }
    private async void ButtonBox2_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonBox2.BackColor == Color.White ? 0x11010001 : 0x11010000));
    }
    private async void ButtonBox3_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonBox3.BackColor == Color.White ? 0x11020001 : 0x11020000));
    }
    private async void ButtonBox4_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonBox4.BackColor == Color.White ? 0x11030001 : 0x11030000));
    }
    private async void ButtonFpv1_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonFpv1.BackColor == Color.White ? 0x11000011 : 0x11000010));
    }
    private async void ButtonFpv2_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonFpv2.BackColor == Color.White ? 0x11010011 : 0x11010010));
    }
    private async void ButtonFpv3_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonFpv2.BackColor == Color.White ? 0x11020011 : 0x11020010));
    }
    private async void ButtonFpv4_Click(object? sender, EventArgs e)
    {
        var obj = FormMap.ObjectsGame.Items.Find(x => x.Selected); // Есть ли выбранный игровой объект?
        if (obj == null) return;
        await obj.SendCommandAsync((uint)(buttonFpv2.BackColor == Color.White ? 0x11030011 : 0x11030010));
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
