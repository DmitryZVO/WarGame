namespace GeoMapGrabber
{
    partial class FormMain
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
            chromiumWebBrowserMain = new CefSharp.WinForms.ChromiumWebBrowser();
            textBoxLon = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBoxLat = new TextBox();
            groupBoxGrabber = new GroupBox();
            labelInfo = new Label();
            buttonGrab = new Button();
            textBoxGrabTimeAll = new TextBox();
            label11 = new Label();
            textBoxGrabCyclesAll = new TextBox();
            label10 = new Label();
            textBoxGrabSizeAll = new TextBox();
            label9 = new Label();
            label8 = new Label();
            numericUpDownGrabKm = new NumericUpDown();
            textBoxOneCycleRadius = new TextBox();
            label7 = new Label();
            textBoxOneCycleTime = new TextBox();
            label6 = new Label();
            textBoxOneCycleTyles = new TextBox();
            label5 = new Label();
            textBoxTilesCount = new TextBox();
            label4 = new Label();
            comboBoxZoom = new ComboBox();
            label3 = new Label();
            progressBarGrab = new ProgressBar();
            groupBoxGeoMap = new GroupBox();
            groupBoxGrabber.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownGrabKm).BeginInit();
            groupBoxGeoMap.SuspendLayout();
            SuspendLayout();
            // 
            // chromiumWebBrowserMain
            // 
            chromiumWebBrowserMain.ActivateBrowserOnCreation = false;
            chromiumWebBrowserMain.Location = new Point(6, 52);
            chromiumWebBrowserMain.Name = "chromiumWebBrowserMain";
            chromiumWebBrowserMain.Size = new Size(835, 756);
            chromiumWebBrowserMain.TabIndex = 0;
            // 
            // textBoxLon
            // 
            textBoxLon.Location = new Point(466, 17);
            textBoxLon.Name = "textBoxLon";
            textBoxLon.Size = new Size(136, 23);
            textBoxLon.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(430, 20);
            label1.Name = "label1";
            label1.Size = new Size(30, 15);
            label1.TabIndex = 2;
            label1.Text = "Lon:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(252, 20);
            label2.Name = "label2";
            label2.Size = new Size(26, 15);
            label2.TabIndex = 4;
            label2.Text = "Lat:";
            // 
            // textBoxLat
            // 
            textBoxLat.Location = new Point(288, 17);
            textBoxLat.Name = "textBoxLat";
            textBoxLat.Size = new Size(136, 23);
            textBoxLat.TabIndex = 3;
            // 
            // groupBoxGrabber
            // 
            groupBoxGrabber.Controls.Add(labelInfo);
            groupBoxGrabber.Controls.Add(buttonGrab);
            groupBoxGrabber.Controls.Add(textBoxGrabTimeAll);
            groupBoxGrabber.Controls.Add(label11);
            groupBoxGrabber.Controls.Add(textBoxGrabCyclesAll);
            groupBoxGrabber.Controls.Add(label10);
            groupBoxGrabber.Controls.Add(textBoxGrabSizeAll);
            groupBoxGrabber.Controls.Add(label9);
            groupBoxGrabber.Controls.Add(label8);
            groupBoxGrabber.Controls.Add(numericUpDownGrabKm);
            groupBoxGrabber.Controls.Add(textBoxOneCycleRadius);
            groupBoxGrabber.Controls.Add(label7);
            groupBoxGrabber.Controls.Add(textBoxOneCycleTime);
            groupBoxGrabber.Controls.Add(label6);
            groupBoxGrabber.Controls.Add(textBoxOneCycleTyles);
            groupBoxGrabber.Controls.Add(label5);
            groupBoxGrabber.Controls.Add(textBoxTilesCount);
            groupBoxGrabber.Controls.Add(label4);
            groupBoxGrabber.Controls.Add(comboBoxZoom);
            groupBoxGrabber.Controls.Add(label3);
            groupBoxGrabber.Location = new Point(865, 12);
            groupBoxGrabber.Name = "groupBoxGrabber";
            groupBoxGrabber.Size = new Size(707, 814);
            groupBoxGrabber.TabIndex = 5;
            groupBoxGrabber.TabStop = false;
            groupBoxGrabber.Text = "Граббер карты";
            // 
            // labelInfo
            // 
            labelInfo.Location = new Point(8, 793);
            labelInfo.Name = "labelInfo";
            labelInfo.Size = new Size(693, 18);
            labelInfo.TabIndex = 19;
            // 
            // buttonGrab
            // 
            buttonGrab.Location = new Point(8, 214);
            buttonGrab.Name = "buttonGrab";
            buttonGrab.Size = new Size(693, 37);
            buttonGrab.TabIndex = 18;
            buttonGrab.Text = "НАЧАТЬ ЗАХВАТ";
            buttonGrab.UseVisualStyleBackColor = true;
            // 
            // textBoxGrabTimeAll
            // 
            textBoxGrabTimeAll.Location = new Point(515, 185);
            textBoxGrabTimeAll.Name = "textBoxGrabTimeAll";
            textBoxGrabTimeAll.ReadOnly = true;
            textBoxGrabTimeAll.Size = new Size(186, 23);
            textBoxGrabTimeAll.TabIndex = 17;
            textBoxGrabTimeAll.Text = "0";
            textBoxGrabTimeAll.TextAlign = HorizontalAlignment.Center;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(369, 188);
            label11.Name = "label11";
            label11.Size = new Size(140, 15);
            label11.TabIndex = 16;
            label11.Text = "Общее время (с/м/ч/д):";
            // 
            // textBoxGrabCyclesAll
            // 
            textBoxGrabCyclesAll.Location = new Point(264, 184);
            textBoxGrabCyclesAll.Name = "textBoxGrabCyclesAll";
            textBoxGrabCyclesAll.ReadOnly = true;
            textBoxGrabCyclesAll.Size = new Size(99, 23);
            textBoxGrabCyclesAll.TabIndex = 15;
            textBoxGrabCyclesAll.Text = "0";
            textBoxGrabCyclesAll.TextAlign = HorizontalAlignment.Center;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(212, 188);
            label10.Name = "label10";
            label10.Size = new Size(52, 15);
            label10.TabIndex = 14;
            label10.Text = "Циклов:";
            // 
            // textBoxGrabSizeAll
            // 
            textBoxGrabSizeAll.Location = new Point(109, 185);
            textBoxGrabSizeAll.Name = "textBoxGrabSizeAll";
            textBoxGrabSizeAll.ReadOnly = true;
            textBoxGrabSizeAll.Size = new Size(97, 23);
            textBoxGrabSizeAll.TabIndex = 13;
            textBoxGrabSizeAll.TextAlign = HorizontalAlignment.Center;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(8, 188);
            label9.Name = "label9";
            label9.Size = new Size(95, 15);
            label9.TabIndex = 12;
            label9.Text = "Захват в тайлах:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(8, 158);
            label8.Name = "label8";
            label8.Size = new Size(190, 15);
            label8.TabIndex = 11;
            label8.Text = "Желаемый радиус захвата (в км):";
            // 
            // numericUpDownGrabKm
            // 
            numericUpDownGrabKm.Location = new Point(204, 154);
            numericUpDownGrabKm.Maximum = new decimal(new int[] { 40075, 0, 0, 0 });
            numericUpDownGrabKm.Name = "numericUpDownGrabKm";
            numericUpDownGrabKm.Size = new Size(117, 23);
            numericUpDownGrabKm.TabIndex = 10;
            // 
            // textBoxOneCycleRadius
            // 
            textBoxOneCycleRadius.Location = new Point(196, 83);
            textBoxOneCycleRadius.Name = "textBoxOneCycleRadius";
            textBoxOneCycleRadius.ReadOnly = true;
            textBoxOneCycleRadius.Size = new Size(125, 23);
            textBoxOneCycleRadius.TabIndex = 9;
            textBoxOneCycleRadius.Text = "0";
            textBoxOneCycleRadius.TextAlign = HorizontalAlignment.Center;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(8, 87);
            label7.Name = "label7";
            label7.Size = new Size(182, 15);
            label7.TabIndex = 8;
            label7.Text = "Радиус захвата за 1 заход (в км):";
            // 
            // textBoxOneCycleTime
            // 
            textBoxOneCycleTime.Location = new Point(413, 52);
            textBoxOneCycleTime.Name = "textBoxOneCycleTime";
            textBoxOneCycleTime.ReadOnly = true;
            textBoxOneCycleTime.Size = new Size(80, 23);
            textBoxOneCycleTime.TabIndex = 7;
            textBoxOneCycleTime.Text = "80";
            textBoxOneCycleTime.TextAlign = HorizontalAlignment.Center;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(256, 55);
            label6.Name = "label6";
            label6.Size = new Size(151, 15);
            label6.TabIndex = 6;
            label6.Text = "Время на 1 заход (секунд):";
            // 
            // textBoxOneCycleTyles
            // 
            textBoxOneCycleTyles.Location = new Point(173, 51);
            textBoxOneCycleTyles.Name = "textBoxOneCycleTyles";
            textBoxOneCycleTyles.ReadOnly = true;
            textBoxOneCycleTyles.Size = new Size(63, 23);
            textBoxOneCycleTyles.TabIndex = 5;
            textBoxOneCycleTyles.Text = "50x50";
            textBoxOneCycleTyles.TextAlign = HorizontalAlignment.Center;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(8, 55);
            label5.Name = "label5";
            label5.Size = new Size(159, 15);
            label5.TabIndex = 4;
            label5.Text = "Захват за 1 заход (в тайлах):";
            // 
            // textBoxTilesCount
            // 
            textBoxTilesCount.Location = new Point(359, 22);
            textBoxTilesCount.Name = "textBoxTilesCount";
            textBoxTilesCount.ReadOnly = true;
            textBoxTilesCount.Size = new Size(134, 23);
            textBoxTilesCount.TabIndex = 3;
            textBoxTilesCount.TextAlign = HorizontalAlignment.Center;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(246, 25);
            label4.Name = "label4";
            label4.Size = new Size(107, 15);
            label4.TabIndex = 2;
            label4.Text = "Тайлов на уровне:";
            // 
            // comboBoxZoom
            // 
            comboBoxZoom.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxZoom.FormattingEnabled = true;
            comboBoxZoom.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" });
            comboBoxZoom.Location = new Point(108, 22);
            comboBoxZoom.Name = "comboBoxZoom";
            comboBoxZoom.Size = new Size(128, 23);
            comboBoxZoom.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(8, 25);
            label3.Name = "label3";
            label3.Size = new Size(94, 15);
            label3.TabIndex = 0;
            label3.Text = "ZOOM уровень:";
            // 
            // progressBarGrab
            // 
            progressBarGrab.Location = new Point(12, 832);
            progressBarGrab.Name = "progressBarGrab";
            progressBarGrab.Size = new Size(1560, 23);
            progressBarGrab.TabIndex = 6;
            // 
            // groupBoxGeoMap
            // 
            groupBoxGeoMap.Controls.Add(textBoxLat);
            groupBoxGeoMap.Controls.Add(chromiumWebBrowserMain);
            groupBoxGeoMap.Controls.Add(textBoxLon);
            groupBoxGeoMap.Controls.Add(label2);
            groupBoxGeoMap.Controls.Add(label1);
            groupBoxGeoMap.Location = new Point(12, 12);
            groupBoxGeoMap.Name = "groupBoxGeoMap";
            groupBoxGeoMap.Size = new Size(847, 814);
            groupBoxGeoMap.TabIndex = 7;
            groupBoxGeoMap.TabStop = false;
            groupBoxGeoMap.Text = "Карта";
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1584, 861);
            Controls.Add(groupBoxGeoMap);
            Controls.Add(progressBarGrab);
            Controls.Add(groupBoxGrabber);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FormMain";
            ShowIcon = false;
            Text = "Граббер карт Яндекс, ООО КБ ЦБС, г. Тула, 2025";
            groupBoxGrabber.ResumeLayout(false);
            groupBoxGrabber.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownGrabKm).EndInit();
            groupBoxGeoMap.ResumeLayout(false);
            groupBoxGeoMap.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowserMain;
        private TextBox textBoxLon;
        private Label label1;
        private Label label2;
        private TextBox textBoxLat;
        private GroupBox groupBoxGrabber;
        private ComboBox comboBoxZoom;
        private Label label3;
        private TextBox textBoxTilesCount;
        private Label label4;
        private TextBox textBoxOneCycleTyles;
        private Label label5;
        private TextBox textBoxOneCycleTime;
        private Label label6;
        private TextBox textBoxOneCycleRadius;
        private Label label7;
        private Label label8;
        private NumericUpDown numericUpDownGrabKm;
        private TextBox textBoxGrabSizeAll;
        private Label label9;
        private TextBox textBoxGrabTimeAll;
        private Label label11;
        private TextBox textBoxGrabCyclesAll;
        private Label label10;
        private Button buttonGrab;
        private ProgressBar progressBarGrab;
        private GroupBox groupBoxGeoMap;
        private Label labelInfo;
    }
}
