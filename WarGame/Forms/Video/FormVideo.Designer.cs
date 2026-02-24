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
        buttonForward = new Button();
        buttonPtz = new Button();
        buttonLeft = new Button();
        buttonRight = new Button();
        buttonWarm = new Button();
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
        // buttonForward
        // 
        buttonForward.Location = new Point(2473, 70);
        buttonForward.Name = "buttonForward";
        buttonForward.Size = new Size(75, 23);
        buttonForward.TabIndex = 2;
        buttonForward.Text = "КУРСОВАЯ";
        buttonForward.UseVisualStyleBackColor = true;
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
        // FormVideo
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(2560, 1440);
        ControlBox = false;
        Controls.Add(buttonWarm);
        Controls.Add(buttonRight);
        Controls.Add(buttonLeft);
        Controls.Add(buttonPtz);
        Controls.Add(buttonForward);
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
    private Button buttonForward;
    private Button buttonPtz;
    private Button buttonLeft;
    private Button buttonRight;
    private Button buttonWarm;
}
