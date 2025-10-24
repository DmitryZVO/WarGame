namespace WarGame.Forms.Map
{
    partial class FormObjAntennaEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBoxId = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBoxName = new TextBox();
            button2 = new Button();
            label3 = new Label();
            trackBarAngle = new TrackBar();
            label4 = new Label();
            trackBarWidth = new TrackBar();
            label5 = new Label();
            trackBarLenKm = new TrackBar();
            textBoxAngle = new TextBox();
            textBoxWidth = new TextBox();
            textBoxLenKm = new TextBox();
            checkBoxDrawRadio = new CheckBox();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)trackBarAngle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLenKm).BeginInit();
            SuspendLayout();
            // 
            // textBoxId
            // 
            textBoxId.BorderStyle = BorderStyle.FixedSingle;
            textBoxId.Location = new Point(150, 12);
            textBoxId.Name = "textBoxId";
            textBoxId.ReadOnly = true;
            textBoxId.Size = new Size(492, 23);
            textBoxId.TabIndex = 0;
            // 
            // label1
            // 
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(132, 23);
            label1.TabIndex = 1;
            label1.Text = "ID:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Location = new Point(12, 41);
            label2.Name = "label2";
            label2.Size = new Size(132, 23);
            label2.TabIndex = 3;
            label2.Text = "ИМЯ:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBoxName
            // 
            textBoxName.BorderStyle = BorderStyle.FixedSingle;
            textBoxName.Location = new Point(150, 41);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(492, 23);
            textBoxName.TabIndex = 2;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(192, 255, 192);
            button2.Location = new Point(540, 160);
            button2.Name = "button2";
            button2.Size = new Size(102, 23);
            button2.TabIndex = 5;
            button2.Text = "СОХРАНИТЬ";
            button2.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            label3.Location = new Point(12, 70);
            label3.Name = "label3";
            label3.Size = new Size(132, 23);
            label3.TabIndex = 6;
            label3.Text = "ПОВОРОТ (ГРАД):";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // trackBarAngle
            // 
            trackBarAngle.AutoSize = false;
            trackBarAngle.Location = new Point(150, 72);
            trackBarAngle.Maximum = 359;
            trackBarAngle.Name = "trackBarAngle";
            trackBarAngle.Size = new Size(418, 23);
            trackBarAngle.TabIndex = 7;
            // 
            // label4
            // 
            label4.Location = new Point(12, 99);
            label4.Name = "label4";
            label4.Size = new Size(132, 23);
            label4.TabIndex = 8;
            label4.Text = "РАСКРЫТИЕ (ГРАД):";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // trackBarWidth
            // 
            trackBarWidth.AutoSize = false;
            trackBarWidth.Location = new Point(150, 101);
            trackBarWidth.Maximum = 179;
            trackBarWidth.Minimum = 1;
            trackBarWidth.Name = "trackBarWidth";
            trackBarWidth.Size = new Size(418, 23);
            trackBarWidth.TabIndex = 9;
            trackBarWidth.Value = 1;
            // 
            // label5
            // 
            label5.Location = new Point(12, 128);
            label5.Name = "label5";
            label5.Size = new Size(132, 23);
            label5.TabIndex = 10;
            label5.Text = "ДАЛЬНОСТЬ (КМ):";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // trackBarLenKm
            // 
            trackBarLenKm.AutoSize = false;
            trackBarLenKm.Location = new Point(150, 130);
            trackBarLenKm.Maximum = 500;
            trackBarLenKm.Minimum = 1;
            trackBarLenKm.Name = "trackBarLenKm";
            trackBarLenKm.Size = new Size(418, 23);
            trackBarLenKm.TabIndex = 11;
            trackBarLenKm.Value = 1;
            // 
            // textBoxAngle
            // 
            textBoxAngle.BorderStyle = BorderStyle.FixedSingle;
            textBoxAngle.Location = new Point(574, 72);
            textBoxAngle.Name = "textBoxAngle";
            textBoxAngle.ReadOnly = true;
            textBoxAngle.Size = new Size(68, 23);
            textBoxAngle.TabIndex = 12;
            textBoxAngle.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxWidth
            // 
            textBoxWidth.BorderStyle = BorderStyle.FixedSingle;
            textBoxWidth.Location = new Point(574, 101);
            textBoxWidth.Name = "textBoxWidth";
            textBoxWidth.ReadOnly = true;
            textBoxWidth.Size = new Size(68, 23);
            textBoxWidth.TabIndex = 13;
            textBoxWidth.TextAlign = HorizontalAlignment.Center;
            // 
            // textBoxLenKm
            // 
            textBoxLenKm.BorderStyle = BorderStyle.FixedSingle;
            textBoxLenKm.Location = new Point(574, 130);
            textBoxLenKm.Name = "textBoxLenKm";
            textBoxLenKm.ReadOnly = true;
            textBoxLenKm.Size = new Size(68, 23);
            textBoxLenKm.TabIndex = 14;
            textBoxLenKm.TextAlign = HorizontalAlignment.Center;
            // 
            // checkBoxDrawRadio
            // 
            checkBoxDrawRadio.Location = new Point(150, 160);
            checkBoxDrawRadio.Name = "checkBoxDrawRadio";
            checkBoxDrawRadio.Size = new Size(22, 23);
            checkBoxDrawRadio.TabIndex = 15;
            checkBoxDrawRadio.TextAlign = ContentAlignment.MiddleCenter;
            checkBoxDrawRadio.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.Location = new Point(12, 160);
            label6.Name = "label6";
            label6.Size = new Size(132, 23);
            label6.TabIndex = 16;
            label6.Text = "РИСОВАТЬ ЛУЧ:";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FormObjAntennaEdit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(654, 189);
            Controls.Add(label6);
            Controls.Add(checkBoxDrawRadio);
            Controls.Add(textBoxLenKm);
            Controls.Add(textBoxWidth);
            Controls.Add(textBoxAngle);
            Controls.Add(trackBarLenKm);
            Controls.Add(label5);
            Controls.Add(trackBarWidth);
            Controls.Add(label4);
            Controls.Add(trackBarAngle);
            Controls.Add(label3);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(textBoxName);
            Controls.Add(label1);
            Controls.Add(textBoxId);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormObjAntennaEdit";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Свойства статического объекта";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)trackBarAngle).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarLenKm).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxId;
        private Label label1;
        private Label label2;
        private TextBox textBoxName;
        private Button button2;
        private Label label3;
        private TrackBar trackBarAngle;
        private Label label4;
        private TrackBar trackBarWidth;
        private Label label5;
        private TrackBar trackBarLenKm;
        private TextBox textBoxAngle;
        private TextBox textBoxWidth;
        private TextBox textBoxLenKm;
        private CheckBox checkBoxDrawRadio;
        private Label label6;
    }
}