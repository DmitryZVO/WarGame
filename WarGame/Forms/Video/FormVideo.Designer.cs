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
        ((System.ComponentModel.ISupportInitialize)pictureBoxMain).BeginInit();
        SuspendLayout();
        // 
        // pictureBoxMain
        // 
        pictureBoxMain.BackColor = Color.Black;
        pictureBoxMain.Location = new Point(0, -1);
        pictureBoxMain.Name = "pictureBoxMain";
        pictureBoxMain.Size = new Size(2560, 1440);
        pictureBoxMain.TabIndex = 0;
        pictureBoxMain.TabStop = false;
        // 
        // buttonBack
        // 
        buttonBack.Location = new Point(2473, 99);
        buttonBack.Name = "buttonBack";
        buttonBack.Size = new Size(75, 23);
        buttonBack.TabIndex = 1;
        buttonBack.Text = "КОРМА";
        buttonBack.UseVisualStyleBackColor = true;
        // 
        // buttonFrwd
        // 
        buttonFrwd.Location = new Point(2473, 70);
        buttonFrwd.Name = "buttonFrwd";
        buttonFrwd.Size = new Size(75, 23);
        buttonFrwd.TabIndex = 2;
        buttonFrwd.Text = "КУРСОВАЯ";
        buttonFrwd.UseVisualStyleBackColor = true;
        // 
        // buttonPtz
        // 
        buttonPtz.Location = new Point(2473, 12);
        buttonPtz.Name = "buttonPtz";
        buttonPtz.Size = new Size(75, 23);
        buttonPtz.TabIndex = 3;
        buttonPtz.Text = "PTZ";
        buttonPtz.UseVisualStyleBackColor = true;
        // 
        // buttonLeft
        // 
        buttonLeft.Location = new Point(2473, 128);
        buttonLeft.Name = "buttonLeft";
        buttonLeft.Size = new Size(75, 23);
        buttonLeft.TabIndex = 4;
        buttonLeft.Text = "Л БОРТ";
        buttonLeft.UseVisualStyleBackColor = true;
        // 
        // buttonRight
        // 
        buttonRight.Location = new Point(2473, 157);
        buttonRight.Name = "buttonRight";
        buttonRight.Size = new Size(75, 23);
        buttonRight.TabIndex = 5;
        buttonRight.Text = "П БОРТ";
        buttonRight.UseVisualStyleBackColor = true;
        // 
        // buttonWarm
        // 
        buttonWarm.Location = new Point(2473, 41);
        buttonWarm.Name = "buttonWarm";
        buttonWarm.Size = new Size(75, 23);
        buttonWarm.TabIndex = 6;
        buttonWarm.Text = "ТЕПЛО";
        buttonWarm.UseVisualStyleBackColor = true;
        // 
        // buttonPtzUp
        // 
        buttonPtzUp.Location = new Point(1360, 12);
        buttonPtzUp.Name = "buttonPtzUp";
        buttonPtzUp.Size = new Size(75, 23);
        buttonPtzUp.TabIndex = 7;
        buttonPtzUp.Text = "/\\";
        buttonPtzUp.UseVisualStyleBackColor = true;
        // 
        // buttonPtzDown
        // 
        buttonPtzDown.Location = new Point(1360, 41);
        buttonPtzDown.Name = "buttonPtzDown";
        buttonPtzDown.Size = new Size(75, 23);
        buttonPtzDown.TabIndex = 8;
        buttonPtzDown.Text = "\\/";
        buttonPtzDown.UseVisualStyleBackColor = true;
        // 
        // buttonPtzLeft
        // 
        buttonPtzLeft.Location = new Point(1279, 12);
        buttonPtzLeft.Name = "buttonPtzLeft";
        buttonPtzLeft.Size = new Size(75, 23);
        buttonPtzLeft.TabIndex = 9;
        buttonPtzLeft.Text = "<";
        buttonPtzLeft.UseVisualStyleBackColor = true;
        // 
        // buttonPtzRight
        // 
        buttonPtzRight.Location = new Point(1441, 12);
        buttonPtzRight.Name = "buttonPtzRight";
        buttonPtzRight.Size = new Size(75, 23);
        buttonPtzRight.TabIndex = 10;
        buttonPtzRight.Text = ">";
        buttonPtzRight.UseVisualStyleBackColor = true;
        // 
        // buttonPtzZoomIn
        // 
        buttonPtzZoomIn.Location = new Point(1279, 41);
        buttonPtzZoomIn.Name = "buttonPtzZoomIn";
        buttonPtzZoomIn.Size = new Size(75, 23);
        buttonPtzZoomIn.TabIndex = 11;
        buttonPtzZoomIn.Text = "Z+";
        buttonPtzZoomIn.UseVisualStyleBackColor = true;
        // 
        // buttonPtzZoomOut
        // 
        buttonPtzZoomOut.Location = new Point(1441, 41);
        buttonPtzZoomOut.Name = "buttonPtzZoomOut";
        buttonPtzZoomOut.Size = new Size(75, 23);
        buttonPtzZoomOut.TabIndex = 12;
        buttonPtzZoomOut.Text = "Z-";
        buttonPtzZoomOut.UseVisualStyleBackColor = true;
        // 
        // FormVideo
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(2560, 1440);
        ControlBox = false;
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
}
