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
        buttonPtzUp = new Button();
        buttonPtzDown = new Button();
        buttonPtzLeft = new Button();
        buttonPtzRight = new Button();
        buttonPtzZoomIn = new Button();
        buttonPtzZoomOut = new Button();
        buttonFpv1 = new Button();
        buttonFpv2 = new Button();
        buttonFpv3 = new Button();
        buttonFpv4 = new Button();
        buttonVideoQH = new Button();
        buttonVideoQM = new Button();
        buttonVideoQL = new Button();
        buttonVideoQEL = new Button();
        ((System.ComponentModel.ISupportInitialize)pictureBoxMain).BeginInit();
        SuspendLayout();
        // 
        // pictureBoxMain
        // 
        pictureBoxMain.BackColor = Color.Black;
        pictureBoxMain.Location = new Point(0, -1);
        pictureBoxMain.Name = "pictureBoxMain";
        pictureBoxMain.Size = new Size(1920, 1080);
        pictureBoxMain.TabIndex = 0;
        pictureBoxMain.TabStop = false;
        // 
        // buttonBack
        // 
        buttonBack.Location = new Point(1833, 99);
        buttonBack.Name = "buttonBack";
        buttonBack.Size = new Size(75, 23);
        buttonBack.TabIndex = 1;
        buttonBack.Text = "КОРМА";
        buttonBack.UseVisualStyleBackColor = true;
        // 
        // buttonFrwd
        // 
        buttonFrwd.Location = new Point(1833, 12);
        buttonFrwd.Name = "buttonFrwd";
        buttonFrwd.Size = new Size(75, 23);
        buttonFrwd.TabIndex = 2;
        buttonFrwd.Text = "КУРСОВАЯ";
        buttonFrwd.UseVisualStyleBackColor = true;
        // 
        // buttonPtz
        // 
        buttonPtz.Location = new Point(1833, 41);
        buttonPtz.Name = "buttonPtz";
        buttonPtz.Size = new Size(75, 23);
        buttonPtz.TabIndex = 3;
        buttonPtz.Text = "PTZ";
        buttonPtz.UseVisualStyleBackColor = true;
        // 
        // buttonLeft
        // 
        buttonLeft.Location = new Point(1833, 128);
        buttonLeft.Name = "buttonLeft";
        buttonLeft.Size = new Size(75, 23);
        buttonLeft.TabIndex = 4;
        buttonLeft.Text = "Л БОРТ";
        buttonLeft.UseVisualStyleBackColor = true;
        // 
        // buttonRight
        // 
        buttonRight.Location = new Point(1833, 157);
        buttonRight.Name = "buttonRight";
        buttonRight.Size = new Size(75, 23);
        buttonRight.TabIndex = 5;
        buttonRight.Text = "П БОРТ";
        buttonRight.UseVisualStyleBackColor = true;
        // 
        // buttonWarm
        // 
        buttonWarm.Location = new Point(1833, 70);
        buttonWarm.Name = "buttonWarm";
        buttonWarm.Size = new Size(75, 23);
        buttonWarm.TabIndex = 6;
        buttonWarm.Text = "ТЕПЛО";
        buttonWarm.UseVisualStyleBackColor = true;
        // 
        // buttonPtzUp
        // 
        buttonPtzUp.Location = new Point(860, 12);
        buttonPtzUp.Name = "buttonPtzUp";
        buttonPtzUp.Size = new Size(75, 23);
        buttonPtzUp.TabIndex = 7;
        buttonPtzUp.Text = "/\\";
        buttonPtzUp.UseVisualStyleBackColor = true;
        // 
        // buttonPtzDown
        // 
        buttonPtzDown.Location = new Point(860, 41);
        buttonPtzDown.Name = "buttonPtzDown";
        buttonPtzDown.Size = new Size(75, 23);
        buttonPtzDown.TabIndex = 8;
        buttonPtzDown.Text = "\\/";
        buttonPtzDown.UseVisualStyleBackColor = true;
        // 
        // buttonPtzLeft
        // 
        buttonPtzLeft.Location = new Point(779, 12);
        buttonPtzLeft.Name = "buttonPtzLeft";
        buttonPtzLeft.Size = new Size(75, 23);
        buttonPtzLeft.TabIndex = 9;
        buttonPtzLeft.Text = "<";
        buttonPtzLeft.UseVisualStyleBackColor = true;
        // 
        // buttonPtzRight
        // 
        buttonPtzRight.Location = new Point(941, 12);
        buttonPtzRight.Name = "buttonPtzRight";
        buttonPtzRight.Size = new Size(75, 23);
        buttonPtzRight.TabIndex = 10;
        buttonPtzRight.Text = ">";
        buttonPtzRight.UseVisualStyleBackColor = true;
        // 
        // buttonPtzZoomIn
        // 
        buttonPtzZoomIn.Location = new Point(779, 41);
        buttonPtzZoomIn.Name = "buttonPtzZoomIn";
        buttonPtzZoomIn.Size = new Size(75, 23);
        buttonPtzZoomIn.TabIndex = 11;
        buttonPtzZoomIn.Text = "Z+";
        buttonPtzZoomIn.UseVisualStyleBackColor = true;
        // 
        // buttonPtzZoomOut
        // 
        buttonPtzZoomOut.Location = new Point(941, 41);
        buttonPtzZoomOut.Name = "buttonPtzZoomOut";
        buttonPtzZoomOut.Size = new Size(75, 23);
        buttonPtzZoomOut.TabIndex = 12;
        buttonPtzZoomOut.Text = "Z-";
        buttonPtzZoomOut.UseVisualStyleBackColor = true;
        // 
        // buttonFpv1
        // 
        buttonFpv1.Location = new Point(1833, 215);
        buttonFpv1.Name = "buttonFpv1";
        buttonFpv1.Size = new Size(75, 23);
        buttonFpv1.TabIndex = 13;
        buttonFpv1.Text = "БПЛА 1";
        buttonFpv1.UseVisualStyleBackColor = true;
        // 
        // buttonFpv2
        // 
        buttonFpv2.Location = new Point(1833, 244);
        buttonFpv2.Name = "buttonFpv2";
        buttonFpv2.Size = new Size(75, 23);
        buttonFpv2.TabIndex = 14;
        buttonFpv2.Text = "БПЛА 2";
        buttonFpv2.UseVisualStyleBackColor = true;
        // 
        // buttonFpv3
        // 
        buttonFpv3.Location = new Point(1833, 273);
        buttonFpv3.Name = "buttonFpv3";
        buttonFpv3.Size = new Size(75, 23);
        buttonFpv3.TabIndex = 15;
        buttonFpv3.Text = "БПЛА 3";
        buttonFpv3.UseVisualStyleBackColor = true;
        // 
        // buttonFpv4
        // 
        buttonFpv4.Location = new Point(1833, 302);
        buttonFpv4.Name = "buttonFpv4";
        buttonFpv4.Size = new Size(75, 23);
        buttonFpv4.TabIndex = 16;
        buttonFpv4.Text = "БПЛА 4";
        buttonFpv4.UseVisualStyleBackColor = true;
        // 
        // buttonVideoQH
        // 
        buttonVideoQH.Location = new Point(1833, 440);
        buttonVideoQH.Name = "buttonVideoQH";
        buttonVideoQH.Size = new Size(75, 23);
        buttonVideoQH.TabIndex = 18;
        buttonVideoQH.Text = "HIGH";
        buttonVideoQH.UseVisualStyleBackColor = true;
        // 
        // buttonVideoQM
        // 
        buttonVideoQM.Location = new Point(1833, 469);
        buttonVideoQM.Name = "buttonVideoQM";
        buttonVideoQM.Size = new Size(75, 23);
        buttonVideoQM.TabIndex = 19;
        buttonVideoQM.Text = "MEDIUM";
        buttonVideoQM.UseVisualStyleBackColor = true;
        // 
        // buttonVideoQL
        // 
        buttonVideoQL.Location = new Point(1833, 498);
        buttonVideoQL.Name = "buttonVideoQL";
        buttonVideoQL.Size = new Size(75, 23);
        buttonVideoQL.TabIndex = 20;
        buttonVideoQL.Text = "LOW";
        buttonVideoQL.UseVisualStyleBackColor = true;
        // 
        // buttonVideoQEL
        // 
        buttonVideoQEL.Location = new Point(1833, 527);
        buttonVideoQEL.Name = "buttonVideoQEL";
        buttonVideoQEL.Size = new Size(75, 23);
        buttonVideoQEL.TabIndex = 21;
        buttonVideoQEL.Text = "EXT_LOW";
        buttonVideoQEL.UseVisualStyleBackColor = true;
        // 
        // FormVideo
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1920, 1080);
        ControlBox = false;
        Controls.Add(buttonVideoQEL);
        Controls.Add(buttonVideoQL);
        Controls.Add(buttonVideoQM);
        Controls.Add(buttonVideoQH);
        Controls.Add(buttonFpv4);
        Controls.Add(buttonFpv3);
        Controls.Add(buttonFpv2);
        Controls.Add(buttonFpv1);
        Controls.Add(buttonPtzZoomOut);
        Controls.Add(buttonPtzZoomIn);
        Controls.Add(buttonPtzRight);
        Controls.Add(buttonPtzLeft);
        Controls.Add(buttonPtzDown);
        Controls.Add(buttonPtzUp);
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
    private Button buttonPtzUp;
    private Button buttonPtzDown;
    private Button buttonPtzLeft;
    private Button buttonPtzRight;
    private Button buttonPtzZoomIn;
    private Button buttonPtzZoomOut;
    private Button buttonFpv1;
    private Button buttonFpv2;
    private Button buttonFpv3;
    private Button buttonFpv4;
    private Button buttonVideoQH;
    private Button buttonVideoQM;
    private Button buttonVideoQL;
    private Button buttonVideoQEL;
}
