namespace Z119.ATK.Shell
{
    partial class fMainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fMainWindow));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.tsmenuItemProject = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemProjectOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemProjectExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemControl = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemControlOxilo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemControlPower = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemControlSwitch = new System.Windows.Forms.ToolStripMenuItem();
            this.loadControllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemControlCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.menuMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmenuItemProject,
            this.tsmenuItemControl,
            this.tsmenuItemSystem,
            this.tsmenuItemHelp});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(880, 29);
            this.menuMain.TabIndex = 2;
            this.menuMain.Text = "menuStrip1";
            // 
            // tsmenuItemProject
            // 
            this.tsmenuItemProject.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmenuItemProjectOpen,
            this.saveToolStripMenuItem,
            this.tsmenuItemProjectExit});
            this.tsmenuItemProject.Name = "tsmenuItemProject";
            this.tsmenuItemProject.Size = new System.Drawing.Size(63, 25);
            this.tsmenuItemProject.Text = "Dự án";
            // 
            // tsmenuItemProjectOpen
            // 
            this.tsmenuItemProjectOpen.Name = "tsmenuItemProjectOpen";
            this.tsmenuItemProjectOpen.Size = new System.Drawing.Size(135, 26);
            this.tsmenuItemProjectOpen.Text = "Mở/Tạo";
            this.tsmenuItemProjectOpen.Click += new System.EventHandler(this.tsmenuItemProjectOpen_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(135, 26);
            this.saveToolStripMenuItem.Text = "Lưu";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click_1);
            // 
            // tsmenuItemProjectExit
            // 
            this.tsmenuItemProjectExit.Name = "tsmenuItemProjectExit";
            this.tsmenuItemProjectExit.Size = new System.Drawing.Size(135, 26);
            this.tsmenuItemProjectExit.Text = "Thoát";
            // 
            // tsmenuItemControl
            // 
            this.tsmenuItemControl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmenuItemControlOxilo,
            this.tsmenuItemControlPower,
            this.tsmenuItemControlSwitch,
            this.loadControllerToolStripMenuItem,
            this.tsmenuItemControlCheck});
            this.tsmenuItemControl.Name = "tsmenuItemControl";
            this.tsmenuItemControl.Size = new System.Drawing.Size(128, 25);
            this.tsmenuItemControl.Text = "Quản lý thiết bị";
            // 
            // tsmenuItemControlOxilo
            // 
            this.tsmenuItemControlOxilo.Name = "tsmenuItemControlOxilo";
            this.tsmenuItemControlOxilo.Size = new System.Drawing.Size(238, 26);
            this.tsmenuItemControlOxilo.Text = "Hiện sóng";
            this.tsmenuItemControlOxilo.Click += new System.EventHandler(this.tsmenuItemControlOxilo_Click);
            // 
            // tsmenuItemControlPower
            // 
            this.tsmenuItemControlPower.Name = "tsmenuItemControlPower";
            this.tsmenuItemControlPower.Size = new System.Drawing.Size(238, 26);
            this.tsmenuItemControlPower.Text = "Nguồn";
            this.tsmenuItemControlPower.Click += new System.EventHandler(this.tsmenuItemControlPower_Click);
            // 
            // tsmenuItemControlSwitch
            // 
            this.tsmenuItemControlSwitch.Name = "tsmenuItemControlSwitch";
            this.tsmenuItemControlSwitch.Size = new System.Drawing.Size(238, 26);
            this.tsmenuItemControlSwitch.Text = "Chuyển mạch";
            this.tsmenuItemControlSwitch.Click += new System.EventHandler(this.tsmenuItemControlSwitch_Click);
            // 
            // loadControllerToolStripMenuItem
            // 
            this.loadControllerToolStripMenuItem.Name = "loadControllerToolStripMenuItem";
            this.loadControllerToolStripMenuItem.Size = new System.Drawing.Size(238, 26);
            this.loadControllerToolStripMenuItem.Text = "Tải lập trình";
            this.loadControllerToolStripMenuItem.Click += new System.EventHandler(this.tảiToolStripMenuItem_Click);
            // 
            // tsmenuItemControlCheck
            // 
            this.tsmenuItemControlCheck.Name = "tsmenuItemControlCheck";
            this.tsmenuItemControlCheck.Size = new System.Drawing.Size(238, 26);
            this.tsmenuItemControlCheck.Text = "Chương trình kiểm  tra";
            this.tsmenuItemControlCheck.Click += new System.EventHandler(this.tsmenuItemControlCheck_Click_1);
            // 
            // tsmenuItemSystem
            // 
            this.tsmenuItemSystem.Name = "tsmenuItemSystem";
            this.tsmenuItemSystem.Size = new System.Drawing.Size(70, 25);
            this.tsmenuItemSystem.Text = "Kết nối";
            this.tsmenuItemSystem.Click += new System.EventHandler(this.tsmenuItemSystem_Click);
            // 
            // tsmenuItemHelp
            // 
            this.tsmenuItemHelp.Name = "tsmenuItemHelp";
            this.tsmenuItemHelp.Size = new System.Drawing.Size(81, 25);
            this.tsmenuItemHelp.Text = "Trợ giúp";
            this.tsmenuItemHelp.Click += new System.EventHandler(this.tsmenuItemHelp_Click);
            // 
            // fMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(880, 515);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "fMainWindow";
            this.Text = "GIÁ THỬ NGUỒN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemProject;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemProjectOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemControl;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemControlPower;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemControlSwitch;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemControlOxilo;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemSystem;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem loadControllerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemProjectExit;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemControlCheck;
    }
}

