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
        button360 = new Button();
        buttonFrwd = new Button();
        buttonPtz = new Button();
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
        // button360
        // 
        button360.Location = new Point(2473, 12);
        button360.Name = "button360";
        button360.Size = new Size(75, 23);
        button360.TabIndex = 1;
        button360.Text = "ОБЗОР 360";
        button360.UseVisualStyleBackColor = true;
        // 
        // buttonFrwd
        // 
        buttonFrwd.Location = new Point(2392, 12);
        buttonFrwd.Name = "buttonFrwd";
        buttonFrwd.Size = new Size(75, 23);
        buttonFrwd.TabIndex = 2;
        buttonFrwd.Text = "КУРСОВАЯ";
        buttonFrwd.UseVisualStyleBackColor = true;
        // 
        // buttonPtz
        // 
        buttonPtz.Location = new Point(2311, 12);
        buttonPtz.Name = "buttonPtz";
        buttonPtz.Size = new Size(75, 23);
        buttonPtz.TabIndex = 3;
        buttonPtz.Text = "PTZ";
        buttonPtz.UseVisualStyleBackColor = true;
        // 
        // FormVideo
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(2560, 1440);
        ControlBox = false;
        Controls.Add(buttonPtz);
        Controls.Add(buttonFrwd);
        Controls.Add(button360);
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
    private Button button360;
    private Button buttonFrwd;
    private Button buttonPtz;
}
