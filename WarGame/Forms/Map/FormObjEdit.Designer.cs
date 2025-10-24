namespace WarGame.Forms.Map
{
    partial class FormObjEdit
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
            SuspendLayout();
            // 
            // textBoxId
            // 
            textBoxId.BorderStyle = BorderStyle.FixedSingle;
            textBoxId.Location = new Point(62, 12);
            textBoxId.Name = "textBoxId";
            textBoxId.ReadOnly = true;
            textBoxId.Size = new Size(306, 23);
            textBoxId.TabIndex = 0;
            // 
            // label1
            // 
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(44, 26);
            label1.TabIndex = 1;
            label1.Text = "ID:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.Location = new Point(12, 38);
            label2.Name = "label2";
            label2.Size = new Size(44, 26);
            label2.TabIndex = 3;
            label2.Text = "ИМЯ:";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBoxName
            // 
            textBoxName.BorderStyle = BorderStyle.FixedSingle;
            textBoxName.Location = new Point(62, 41);
            textBoxName.Name = "textBoxName";
            textBoxName.Size = new Size(306, 23);
            textBoxName.TabIndex = 2;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(192, 255, 192);
            button2.Location = new Point(266, 72);
            button2.Name = "button2";
            button2.Size = new Size(102, 23);
            button2.TabIndex = 5;
            button2.Text = "СОХРАНИТЬ";
            button2.UseVisualStyleBackColor = false;
            // 
            // FormObjEdit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(378, 107);
            Controls.Add(button2);
            Controls.Add(label2);
            Controls.Add(textBoxName);
            Controls.Add(label1);
            Controls.Add(textBoxId);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormObjEdit";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Свойства статического объекта";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxId;
        private Label label1;
        private Label label2;
        private TextBox textBoxName;
        private Button button2;
    }
}