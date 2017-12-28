namespace Z119.ATK.Shell
{
    partial class fOxiloForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnStart = new System.Windows.Forms.Button();
            this.listBoxDevices = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_xscale = new System.Windows.Forms.ComboBox();
            this.label_timeScale = new System.Windows.Forms.Label();
            this.comboBox_yscale = new System.Windows.Forms.ComboBox();
            this.label_yScale = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            chartArea2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            chartArea2.BorderColor = System.Drawing.Color.White;
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.BackColor = System.Drawing.Color.Silver;
            legend2.BackSecondaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            legend2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(15, 11);
            this.chart1.Margin = new System.Windows.Forms.Padding(2);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Yellow;
            series4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(5)));
            series4.Legend = "Legend1";
            series4.Name = "Data";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.Red;
            series5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(5)));
            series5.Legend = "Legend1";
            series5.Name = "Reference data";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Legend = "Legend1";
            series6.Name = "Old data";
            this.chart1.Series.Add(series4);
            this.chart1.Series.Add(series5);
            this.chart1.Series.Add(series6);
            this.chart1.Size = new System.Drawing.Size(596, 280);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.Location = new System.Drawing.Point(2, 77);
            this.btnStart.Margin = new System.Windows.Forms.Padding(2);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(186, 27);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Cập nhật";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // listBoxDevices
            // 
            this.listBoxDevices.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listBoxDevices.FormattingEnabled = true;
            this.listBoxDevices.Location = new System.Drawing.Point(3, 16);
            this.listBoxDevices.Name = "listBoxDevices";
            this.listBoxDevices.Size = new System.Drawing.Size(185, 56);
            this.listBoxDevices.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Chọn thiết bị";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.listBoxDevices);
            this.flowLayoutPanel1.Controls.Add(this.btnStart);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(11, 296);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(219, 118);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.label5);
            this.flowLayoutPanel2.Controls.Add(this.comboBox_xscale);
            this.flowLayoutPanel2.Controls.Add(this.label_timeScale);
            this.flowLayoutPanel2.Controls.Add(this.label4);
            this.flowLayoutPanel2.Controls.Add(this.comboBox_yscale);
            this.flowLayoutPanel2.Controls.Add(this.label_yScale);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(256, 297);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(355, 118);
            this.flowLayoutPanel2.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 5;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // comboBox_xscale
            // 
            this.comboBox_xscale.FormattingEnabled = true;
            this.comboBox_xscale.Items.AddRange(new object[] {
            "5 s",
            "2 s",
            "1 s",
            "500 ms",
            "200 ms",
            "100 ms",
            "50 ms",
            "20 ms",
            "10 ms",
            "5 ms",
            "2 ms",
            "1 ms",
            "500 us",
            "200 us"});
            this.comboBox_xscale.Location = new System.Drawing.Point(109, 3);
            this.comboBox_xscale.Name = "comboBox_xscale";
            this.comboBox_xscale.Size = new System.Drawing.Size(113, 21);
            this.comboBox_xscale.TabIndex = 6;
            this.comboBox_xscale.SelectedIndexChanged += new System.EventHandler(this.comboBox_xscale_SelectedIndexChanged);
            // 
            // label_timeScale
            // 
            this.label_timeScale.AutoSize = true;
            this.label_timeScale.Location = new System.Drawing.Point(228, 0);
            this.label_timeScale.MinimumSize = new System.Drawing.Size(100, 20);
            this.label_timeScale.Name = "label_timeScale";
            this.label_timeScale.Size = new System.Drawing.Size(100, 20);
            this.label_timeScale.TabIndex = 7;
            this.label_timeScale.Text = "--";
            // 
            // comboBox_yscale
            // 
            this.comboBox_yscale.FormattingEnabled = true;
            this.comboBox_yscale.Items.AddRange(new object[] {
            "20 V",
            "10 V",
            "5 V",
            "2 V",
            "1 V",
            "500 mV",
            "200 mV",
            "100 mV",
            "50mV"});
            this.comboBox_yscale.Location = new System.Drawing.Point(109, 30);
            this.comboBox_yscale.Name = "comboBox_yscale";
            this.comboBox_yscale.Size = new System.Drawing.Size(113, 21);
            this.comboBox_yscale.TabIndex = 6;
            this.comboBox_yscale.SelectedIndexChanged += new System.EventHandler(this.comboBox_yscale_SelectedIndexChanged);
            // 
            // label_yScale
            // 
            this.label_yScale.AutoSize = true;
            this.label_yScale.Location = new System.Drawing.Point(228, 27);
            this.label_yScale.MinimumSize = new System.Drawing.Size(100, 20);
            this.label_yScale.Name = "label_yScale";
            this.label_yScale.Size = new System.Drawing.Size(100, 20);
            this.label_yScale.TabIndex = 8;
            this.label_yScale.Text = "--";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 27);
            this.label4.MinimumSize = new System.Drawing.Size(100, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 9;
            this.label4.Text = "Thang biên độ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.MinimumSize = new System.Drawing.Size(100, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Thang thời gian";
            // 
            // fOxiloForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 427);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "fOxiloForm";
            this.Text = "Oscilloscope";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox listBoxDevices;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_xscale;
        private System.Windows.Forms.Label label_timeScale;
        private System.Windows.Forms.ComboBox comboBox_yscale;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_yScale;
    }
}