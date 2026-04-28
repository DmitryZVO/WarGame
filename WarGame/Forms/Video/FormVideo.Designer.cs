namespace WarGame.Forms.Video;

partial class FormVideo
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        pictureBoxMain = new PictureBox();
        buttonBack = new Button();
        buttonFrwd = new Button();
        buttonPtz = new Button();
        buttonLeft = new Button();
        buttonRight = new Button();
        buttonWarm = new Button();
        buttonFpv1 = new Button();
        buttonFpv2 = new Button();
        buttonFpv3 = new Button();
        buttonFpv4 = new Button();
        buttonVideoQH = new Button();
        buttonVideoQM = new Button();
        buttonVideoQL = new Button();
        buttonVideoQEL = new Button();
        buttonCamNone = new Button();
        buttonJoystickSend = new Button();
        buttonCamFps1 = new Button();
        buttonCamFps5 = new Button();
        buttonCamFps10 = new Button();
        buttonCamFps25 = new Button();
        ((System.ComponentModel.ISupportInitialize)pictureBoxMain).BeginInit();
        SuspendLayout();
        // 
        // pictureBoxMain
        // 
        pictureBoxMain.BackColor = Color.Black;
        pictureBoxMain.Location = new Point(0, 0);
        pictureBoxMain.Name = "pictureBoxMain";
        pictureBoxMain.Size = new Size(1920, 1080);
        pictureBoxMain.TabIndex = 0;
        pictureBoxMain.TabStop = false;
        // 
        // buttonBack
        // 
        buttonBack.Location = new Point(1833, 70);
        buttonBack.Name = "buttonBack";
        buttonBack.Size = new Size(75, 23);
        buttonBack.TabIndex = 1;
        buttonBack.Text = "КОРМА";
        buttonBack.UseVisualStyleBackColor = true;
        // 
        // buttonFrwd
        // 
        buttonFrwd.Location = new Point(1833, 41);
        buttonFrwd.Name = "buttonFrwd";
        buttonFrwd.Size = new Size(75, 23);
        buttonFrwd.TabIndex = 2;
        buttonFrwd.Text = "КУРСОВАЯ";
        buttonFrwd.UseVisualStyleBackColor = true;
        // 
        // buttonPtz
        // 
        buttonPtz.Location = new Point(1833, 193);
        buttonPtz.Name = "buttonPtz";
        buttonPtz.Size = new Size(75, 23);
        buttonPtz.TabIndex = 3;
        buttonPtz.Text = "PTZ";
        buttonPtz.UseVisualStyleBackColor = true;
        // 
        // buttonLeft
        // 
        buttonLeft.Location = new Point(1833, 99);
        buttonLeft.Name = "buttonLeft";
        buttonLeft.Size = new Size(75, 23);
        buttonLeft.TabIndex = 4;
        buttonLeft.Text = "Л БОРТ";
        buttonLeft.UseVisualStyleBackColor = true;
        // 
        // buttonRight
        // 
        buttonRight.Location = new Point(1833, 128);
        buttonRight.Name = "buttonRight";
        buttonRight.Size = new Size(75, 23);
        buttonRight.TabIndex = 5;
        buttonRight.Text = "П БОРТ";
        buttonRight.UseVisualStyleBackColor = true;
        // 
        // buttonWarm
        // 
        buttonWarm.Location = new Point(1833, 222);
        buttonWarm.Name = "buttonWarm";
        buttonWarm.Size = new Size(75, 23);
        buttonWarm.TabIndex = 6;
        buttonWarm.Text = "ТЕПЛО";
        buttonWarm.UseVisualStyleBackColor = true;
        // 
        // buttonFpv1
        // 
        buttonFpv1.Location = new Point(1833, 294);
        buttonFpv1.Name = "buttonFpv1";
        buttonFpv1.Size = new Size(75, 23);
        buttonFpv1.TabIndex = 13;
        buttonFpv1.Text = "БПЛА 1";
        buttonFpv1.UseVisualStyleBackColor = true;
        // 
        // buttonFpv2
        // 
        buttonFpv2.Location = new Point(1833, 323);
        buttonFpv2.Name = "buttonFpv2";
        buttonFpv2.Size = new Size(75, 23);
        buttonFpv2.TabIndex = 14;
        buttonFpv2.Text = "БПЛА 2";
        buttonFpv2.UseVisualStyleBackColor = true;
        // 
        // buttonFpv3
        // 
        buttonFpv3.Location = new Point(1833, 352);
        buttonFpv3.Name = "buttonFpv3";
        buttonFpv3.Size = new Size(75, 23);
        buttonFpv3.TabIndex = 15;
        buttonFpv3.Text = "БПЛА 3";
        buttonFpv3.UseVisualStyleBackColor = true;
        // 
        // buttonFpv4
        // 
        buttonFpv4.Location = new Point(1833, 381);
        buttonFpv4.Name = "buttonFpv4";
        buttonFpv4.Size = new Size(75, 23);
        buttonFpv4.TabIndex = 16;
        buttonFpv4.Text = "БПЛА 4";
        buttonFpv4.UseVisualStyleBackColor = true;
        // 
        // buttonVideoQH
        // 
        buttonVideoQH.Location = new Point(1833, 958);
        buttonVideoQH.Name = "buttonVideoQH";
        buttonVideoQH.Size = new Size(75, 23);
        buttonVideoQH.TabIndex = 18;
        buttonVideoQH.Text = "HIGH";
        buttonVideoQH.UseVisualStyleBackColor = true;
        // 
        // buttonVideoQM
        // 
        buttonVideoQM.Location = new Point(1833, 987);
        buttonVideoQM.Name = "buttonVideoQM";
        buttonVideoQM.Size = new Size(75, 23);
        buttonVideoQM.TabIndex = 19;
        buttonVideoQM.Text = "MEDIUM";
        buttonVideoQM.UseVisualStyleBackColor = true;
        // 
        // buttonVideoQL
        // 
        buttonVideoQL.Location = new Point(1833, 1016);
        buttonVideoQL.Name = "buttonVideoQL";
        buttonVideoQL.Size = new Size(75, 23);
        buttonVideoQL.TabIndex = 20;
        buttonVideoQL.Text = "LOW";
        buttonVideoQL.UseVisualStyleBackColor = true;
        // 
        // buttonVideoQEL
        // 
        buttonVideoQEL.Location = new Point(1833, 1045);
        buttonVideoQEL.Name = "buttonVideoQEL";
        buttonVideoQEL.Size = new Size(75, 23);
        buttonVideoQEL.TabIndex = 21;
        buttonVideoQEL.Text = "EXT_LOW";
        buttonVideoQEL.UseVisualStyleBackColor = true;
        // 
        // buttonCamNone
        // 
        buttonCamNone.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
        buttonCamNone.Location = new Point(1833, 12);
        buttonCamNone.Name = "buttonCamNone";
        buttonCamNone.Size = new Size(75, 23);
        buttonCamNone.TabIndex = 22;
        buttonCamNone.Text = "БЕЗ КАМЕР";
        buttonCamNone.UseVisualStyleBackColor = true;
        // 
        // buttonJoystickSend
        // 
        buttonJoystickSend.Location = new Point(730, 1045);
        buttonJoystickSend.Name = "buttonJoystickSend";
        buttonJoystickSend.Size = new Size(447, 23);
        buttonJoystickSend.TabIndex = 23;
        buttonJoystickSend.Text = "ЗАХВАТИТЬ УПРАВЛЕНИЕ";
        buttonJoystickSend.UseVisualStyleBackColor = true;
        // 
        // buttonCamFps1
        // 
        buttonCamFps1.Location = new Point(1752, 1045);
        buttonCamFps1.Name = "buttonCamFps1";
        buttonCamFps1.Size = new Size(75, 23);
        buttonCamFps1.TabIndex = 24;
        buttonCamFps1.Text = "FPS=1";
        buttonCamFps1.UseVisualStyleBackColor = true;
        // 
        // buttonCamFps5
        // 
        buttonCamFps5.Location = new Point(1752, 1016);
        buttonCamFps5.Name = "buttonCamFps5";
        buttonCamFps5.Size = new Size(75, 23);
        buttonCamFps5.TabIndex = 25;
        buttonCamFps5.Text = "FPS=5";
        buttonCamFps5.UseVisualStyleBackColor = true;
        // 
        // buttonCamFps10
        // 
        buttonCamFps10.Location = new Point(1752, 987);
        buttonCamFps10.Name = "buttonCamFps10";
        buttonCamFps10.Size = new Size(75, 23);
        buttonCamFps10.TabIndex = 26;
        buttonCamFps10.Text = "FPS=10";
        buttonCamFps10.UseVisualStyleBackColor = true;
        // 
        // buttonCamFps25
        // 
        buttonCamFps25.Location = new Point(1752, 958);
        buttonCamFps25.Name = "buttonCamFps25";
        buttonCamFps25.Size = new Size(75, 23);
        buttonCamFps25.TabIndex = 27;
        buttonCamFps25.Text = "FPS=25";
        buttonCamFps25.UseVisualStyleBackColor = true;
        // 
        // FormVideo
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1920, 1080);
        ControlBox = false;
        Controls.Add(buttonCamFps25);
        Controls.Add(buttonCamFps10);
        Controls.Add(buttonCamFps5);
        Controls.Add(buttonCamFps1);
        Controls.Add(buttonJoystickSend);
        Controls.Add(buttonCamNone);
        Controls.Add(buttonVideoQEL);
        Controls.Add(buttonVideoQL);
        Controls.Add(buttonVideoQM);
        Controls.Add(buttonVideoQH);
        Controls.Add(buttonFpv4);
        Controls.Add(buttonFpv3);
        Controls.Add(buttonFpv2);
        Controls.Add(buttonFpv1);
        Controls.Add(buttonWarm);
        Controls.Add(buttonRight);
        Controls.Add(buttonLeft);
        Controls.Add(buttonPtz);
        Controls.Add(buttonFrwd);
        Controls.Add(buttonBack);
        Controls.Add(pictureBoxMain);
        FormBorderStyle = FormBorderStyle.None;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormVideo";
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "РЛС";
        ((System.ComponentModel.ISupportInitialize)pictureBoxMain).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private PictureBox pictureBoxMain;
    private Button buttonBack;
    private Button buttonFrwd;
    private Button buttonPtz;
    private Button buttonLeft;
    private Button buttonRight;
    private Button buttonWarm;
    private Button buttonFpv1;
    private Button buttonFpv2;
    private Button buttonFpv3;
    private Button buttonFpv4;
    private Button buttonVideoQH;
    private Button buttonVideoQM;
    private Button buttonVideoQL;
    private Button buttonVideoQEL;
    private Button buttonCamNone;
    private Button buttonJoystickSend;
    private Button buttonCamFps1;
    private Button buttonCamFps5;
    private Button buttonCamFps10;
    private Button buttonCamFps25;
}
