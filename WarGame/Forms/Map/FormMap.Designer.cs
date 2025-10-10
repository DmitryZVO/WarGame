namespace WarGame.Forms.Map;

partial class FormMap
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
        ((System.ComponentModel.ISupportInitialize)pictureBoxMain).BeginInit();
        SuspendLayout();
        // 
        // pictureBoxMain
        // 
        pictureBoxMain.BackColor = Color.Black;
        pictureBoxMain.Dock = DockStyle.Fill;
        pictureBoxMain.Location = new Point(0, 0);
        pictureBoxMain.Name = "pictureBoxMain";
        pictureBoxMain.Size = new Size(2560, 1440);
        pictureBoxMain.TabIndex = 0;
        pictureBoxMain.TabStop = false;
        // 
        // FormMap
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(2560, 1440);
        ControlBox = false;
        Controls.Add(pictureBoxMain);
        FormBorderStyle = FormBorderStyle.None;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormMap";
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Глобальная карта";
        ((System.ComponentModel.ISupportInitialize)pictureBoxMain).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private PictureBox pictureBoxMain;
}
