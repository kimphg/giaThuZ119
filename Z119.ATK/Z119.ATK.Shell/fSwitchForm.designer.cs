namespace Z119.ATK.Shell
{
	partial class fSwitchForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fSwitchForm));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dgvSwitch = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNone = new System.Windows.Forms.Button();
            this.btnURA = new System.Windows.Forms.Button();
            this.btnU4 = new System.Windows.Forms.Button();
            this.btnDAT = new System.Windows.Forms.Button();
            this.btnU1 = new System.Windows.Forms.Button();
            this.btnU2 = new System.Windows.Forms.Button();
            this.btnU3 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnOnOff = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txbSoChan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.thưMụcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mởTậpTinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lưuTậpTinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thuNhỏToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pnlMain.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSwitch)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.panel3);
            this.pnlMain.Controls.Add(this.panel2);
            this.pnlMain.Controls.Add(this.menuStrip1);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(346, 721);
            this.pnlMain.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Location = new System.Drawing.Point(4, 142);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(329, 706);
            this.panel3.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.dgvSwitch);
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(180, 562);
            this.panel6.TabIndex = 0;
            // 
            // dgvSwitch
            // 
            this.dgvSwitch.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            this.dgvSwitch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvSwitch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSwitch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSwitch.Location = new System.Drawing.Point(0, 0);
            this.dgvSwitch.Name = "dgvSwitch";
            this.dgvSwitch.Size = new System.Drawing.Size(178, 560);
            this.dgvSwitch.TabIndex = 0;
            this.dgvSwitch.Click += new System.EventHandler(this.dgvSwitch_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNone);
            this.groupBox1.Controls.Add(this.btnURA);
            this.groupBox1.Controls.Add(this.btnU4);
            this.groupBox1.Controls.Add(this.btnU2);
            this.groupBox1.Controls.Add(this.btnDAT);
            this.groupBox1.Controls.Add(this.btnU1);
            this.groupBox1.Controls.Add(this.btnU3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(193, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 488);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lựa chọn";
            // 
            // btnNone
            // 
            this.btnNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnNone.Location = new System.Drawing.Point(6, 245);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(86, 30);
            this.btnNone.TabIndex = 21;
            this.btnNone.Text = "Bỏ chọn";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.btnU1Am_Click);
            // 
            // btnURA
            // 
            this.btnURA.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnURA.ForeColor = System.Drawing.Color.Red;
            this.btnURA.Location = new System.Drawing.Point(6, 101);
            this.btnURA.Name = "btnURA";
            this.btnURA.Size = new System.Drawing.Size(86, 30);
            this.btnURA.TabIndex = 20;
            this.btnURA.Text = "-U2";
            this.btnURA.UseVisualStyleBackColor = true;
            this.btnURA.Click += new System.EventHandler(this.btnU1Am_Click);
            // 
            // btnU4
            // 
            this.btnU4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnU4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnU4.Location = new System.Drawing.Point(6, 173);
            this.btnU4.Name = "btnU4";
            this.btnU4.Size = new System.Drawing.Size(86, 30);
            this.btnU4.TabIndex = 19;
            this.btnU4.Text = "U4";
            this.btnU4.UseVisualStyleBackColor = true;
            this.btnU4.Click += new System.EventHandler(this.btnU1Am_Click);
            // 
            // btnDAT
            // 
            this.btnDAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDAT.ForeColor = System.Drawing.Color.Olive;
            this.btnDAT.Location = new System.Drawing.Point(6, 209);
            this.btnDAT.Name = "btnDAT";
            this.btnDAT.Size = new System.Drawing.Size(86, 30);
            this.btnDAT.TabIndex = 18;
            this.btnDAT.Text = "Đất";
            this.btnDAT.UseVisualStyleBackColor = true;
            this.btnDAT.Click += new System.EventHandler(this.btnU1Am_Click);
            // 
            // btnU1
            // 
            this.btnU1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnU1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnU1.Location = new System.Drawing.Point(6, 25);
            this.btnU1.Name = "btnU1";
            this.btnU1.Size = new System.Drawing.Size(86, 30);
            this.btnU1.TabIndex = 17;
            this.btnU1.Text = "U1";
            this.btnU1.UseVisualStyleBackColor = true;
            this.btnU1.Click += new System.EventHandler(this.btnU1Am_Click);
            // 
            // btnU2
            // 
            this.btnU2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnU2.ForeColor = System.Drawing.Color.Red;
            this.btnU2.Location = new System.Drawing.Point(6, 65);
            this.btnU2.Name = "btnU2";
            this.btnU2.Size = new System.Drawing.Size(86, 30);
            this.btnU2.TabIndex = 16;
            this.btnU2.Text = "+U2";
            this.btnU2.UseVisualStyleBackColor = true;
            this.btnU2.Click += new System.EventHandler(this.btnU1Am_Click);
            // 
            // btnU3
            // 
            this.btnU3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnU3.ForeColor = System.Drawing.Color.Blue;
            this.btnU3.Location = new System.Drawing.Point(6, 137);
            this.btnU3.Name = "btnU3";
            this.btnU3.Size = new System.Drawing.Size(86, 30);
            this.btnU3.TabIndex = 15;
            this.btnU3.Text = "U3";
            this.btnU3.UseVisualStyleBackColor = true;
            this.btnU3.Click += new System.EventHandler(this.btnU1Am_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Location = new System.Drawing.Point(4, 29);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(329, 107);
            this.panel2.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.btnOnOff);
            this.panel4.Controls.Add(this.btnClear);
            this.panel4.Controls.Add(this.txbSoChan);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(296, 98);
            this.panel4.TabIndex = 0;
            // 
            // btnOnOff
            // 
            this.btnOnOff.Location = new System.Drawing.Point(203, 3);
            this.btnOnOff.Name = "btnOnOff";
            this.btnOnOff.Size = new System.Drawing.Size(48, 70);
            this.btnOnOff.TabIndex = 1;
            this.btnOnOff.UseVisualStyleBackColor = true;
            this.btnOnOff.Click += new System.EventHandler(this.btnOnOff_Click);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(3, 49);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txbSoChan
            // 
            this.txbSoChan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbSoChan.Location = new System.Drawing.Point(82, 10);
            this.txbSoChan.Name = "txbSoChan";
            this.txbSoChan.ReadOnly = true;
            this.txbSoChan.Size = new System.Drawing.Size(82, 26);
            this.txbSoChan.TabIndex = 0;
            this.txbSoChan.Text = "31";
            this.txbSoChan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 22);
            this.label1.TabIndex = 10;
            this.label1.Text = "Số chân:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thưMụcToolStripMenuItem,
            this.thuNhỏToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(344, 29);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // thưMụcToolStripMenuItem
            // 
            this.thưMụcToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mởTậpTinToolStripMenuItem,
            this.lưuTậpTinToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.thưMụcToolStripMenuItem.Name = "thưMụcToolStripMenuItem";
            this.thưMụcToolStripMenuItem.Size = new System.Drawing.Size(82, 25);
            this.thưMụcToolStripMenuItem.Text = "Thư mục";
            // 
            // mởTậpTinToolStripMenuItem
            // 
            this.mởTậpTinToolStripMenuItem.Name = "mởTậpTinToolStripMenuItem";
            this.mởTậpTinToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.mởTậpTinToolStripMenuItem.Text = "Mở tập tin";
            this.mởTậpTinToolStripMenuItem.Click += new System.EventHandler(this.mởTậpTinToolStripMenuItem_Click);
            // 
            // lưuTậpTinToolStripMenuItem
            // 
            this.lưuTậpTinToolStripMenuItem.Name = "lưuTậpTinToolStripMenuItem";
            this.lưuTậpTinToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.lưuTậpTinToolStripMenuItem.Text = "Lưu tập tin";
            this.lưuTậpTinToolStripMenuItem.Click += new System.EventHandler(this.lưuTậpTinToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(154, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // thuNhỏToolStripMenuItem
            // 
            this.thuNhỏToolStripMenuItem.Name = "thuNhỏToolStripMenuItem";
            this.thuNhỏToolStripMenuItem.Size = new System.Drawing.Size(79, 25);
            this.thuNhỏToolStripMenuItem.Text = "Thu nhỏ";
            this.thuNhỏToolStripMenuItem.Click += new System.EventHandler(this.thuNhỏToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "0.png");
            this.imageList1.Images.SetKeyName(1, "1.png");
            this.imageList1.Images.SetKeyName(2, "2.png");
            this.imageList1.Images.SetKeyName(3, "3.png");
            this.imageList1.Images.SetKeyName(4, "4.png");
            this.imageList1.Images.SetKeyName(5, "5.png");
            this.imageList1.Images.SetKeyName(6, "6.png");
            this.imageList1.Images.SetKeyName(7, "value1.png");
            this.imageList1.Images.SetKeyName(8, "value2.png");
            this.imageList1.Images.SetKeyName(9, "value3.png");
            this.imageList1.Images.SetKeyName(10, "value4.png");
            this.imageList1.Images.SetKeyName(11, "value5.png");
            this.imageList1.Images.SetKeyName(12, "value6.png");
            this.imageList1.Images.SetKeyName(13, "value0.png");
            // 
            // fSwitchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 721);
            this.Controls.Add(this.pnlMain);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "fSwitchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ĐK CHUYỂN MẠCH";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fSwitchForm_FormClosing);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSwitch)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txbSoChan;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.DataGridView dgvSwitch;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnURA;
		private System.Windows.Forms.Button btnU4;
		private System.Windows.Forms.Button btnDAT;
		private System.Windows.Forms.Button btnU1;
		private System.Windows.Forms.Button btnU2;
        private System.Windows.Forms.Button btnU3;
		private System.IO.Ports.SerialPort serialPort1;
		private System.Windows.Forms.Button btnNone;
        private System.Windows.Forms.Button btnOnOff;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem thưMụcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mởTậpTinToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lưuTậpTinToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem thuNhỏToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    }
}

