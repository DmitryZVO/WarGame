namespace WarGame.Forms.Telem;

partial class FormTelem
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
        buttonRelay1 = new Button();
        buttonRelay2 = new Button();
        buttonRelay3 = new Button();
        buttonRelay4 = new Button();
        buttonRelay5 = new Button();
        buttonRelay6 = new Button();
        buttonRelay7 = new Button();
        buttonRelay8 = new Button();
        ((System.ComponentModel.ISupportInitialize)pictureBoxMain).BeginInit();
        SuspendLayout();
        // 
        // pictureBoxMain
        // 
        pictureBoxMain.BackColor = Color.Black;
        pictureBoxMain.Dock = DockStyle.Fill;
        pictureBoxMain.Location = new Point(0, 0);
        pictureBoxMain.Name = "pictureBoxMain";
        pictureBoxMain.Size = new Size(1920, 1080);
        pictureBoxMain.TabIndex = 0;
        pictureBoxMain.TabStop = false;
        // 
        // buttonRelay1
        // 
        buttonRelay1.Location = new Point(12, 12);
        buttonRelay1.Name = "buttonRelay1";
        buttonRelay1.Size = new Size(125, 23);
        buttonRelay1.TabIndex = 1;
        buttonRelay1.Text = "РЕЛЕ 1";
        buttonRelay1.UseVisualStyleBackColor = true;
        // 
        // buttonRelay2
        // 
        buttonRelay2.Location = new Point(12, 41);
        buttonRelay2.Name = "buttonRelay2";
        buttonRelay2.Size = new Size(125, 23);
        buttonRelay2.TabIndex = 2;
        buttonRelay2.Text = "РЕЛЕ 2";
        buttonRelay2.UseVisualStyleBackColor = true;
        // 
        // buttonRelay3
        // 
        buttonRelay3.Location = new Point(12, 70);
        buttonRelay3.Name = "buttonRelay3";
        buttonRelay3.Size = new Size(125, 23);
        buttonRelay3.TabIndex = 3;
        buttonRelay3.Text = "РЕЛЕ 3";
        buttonRelay3.UseVisualStyleBackColor = true;
        // 
        // buttonRelay4
        // 
        buttonRelay4.Location = new Point(12, 99);
        buttonRelay4.Name = "buttonRelay4";
        buttonRelay4.Size = new Size(125, 23);
        buttonRelay4.TabIndex = 4;
        buttonRelay4.Text = "РЕЛЕ 4";
        buttonRelay4.UseVisualStyleBackColor = true;
        // 
        // buttonRelay5
        // 
        buttonRelay5.Location = new Point(12, 128);
        buttonRelay5.Name = "buttonRelay5";
        buttonRelay5.Size = new Size(125, 23);
        buttonRelay5.TabIndex = 5;
        buttonRelay5.Text = "РЕЛЕ 5";
        buttonRelay5.UseVisualStyleBackColor = true;
        // 
        // buttonRelay6
        // 
        buttonRelay6.Location = new Point(12, 157);
        buttonRelay6.Name = "buttonRelay6";
        buttonRelay6.Size = new Size(125, 23);
        buttonRelay6.TabIndex = 6;
        buttonRelay6.Text = "РЕЛЕ 6";
        buttonRelay6.UseVisualStyleBackColor = true;
        // 
        // buttonRelay7
        // 
        buttonRelay7.Location = new Point(12, 186);
        buttonRelay7.Name = "buttonRelay7";
        buttonRelay7.Size = new Size(125, 23);
        buttonRelay7.TabIndex = 7;
        buttonRelay7.Text = "РЕЛЕ 7";
        buttonRelay7.UseVisualStyleBackColor = true;
        // 
        // buttonRelay8
        // 
        buttonRelay8.Location = new Point(12, 215);
        buttonRelay8.Name = "buttonRelay8";
        buttonRelay8.Size = new Size(125, 23);
        buttonRelay8.TabIndex = 8;
        buttonRelay8.Text = "РЕЛЕ 8";
        buttonRelay8.UseVisualStyleBackColor = true;
        // 
        // FormTelem
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1920, 1080);
        ControlBox = false;
        Controls.Add(buttonRelay8);
        Controls.Add(buttonRelay7);
        Controls.Add(buttonRelay6);
        Controls.Add(buttonRelay5);
        Controls.Add(buttonRelay4);
        Controls.Add(buttonRelay3);
        Controls.Add(buttonRelay2);
        Controls.Add(buttonRelay1);
        Controls.Add(pictureBoxMain);
        FormBorderStyle = FormBorderStyle.None;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FormTelem";
        ShowIcon = false;
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterScreen;
        Text = "РЛС";
        ((System.ComponentModel.ISupportInitialize)pictureBoxMain).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private PictureBox pictureBoxMain;
    private Button buttonRelay1;
    private Button buttonRelay2;
    private Button buttonRelay3;
    private Button buttonRelay4;
    private Button buttonRelay5;
    private Button buttonRelay6;
    private Button buttonRelay7;
    private Button buttonRelay8;
}
