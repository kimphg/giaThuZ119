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
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.tsmenuItemProject = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemProjectCreareNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemProjectOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemProjectExit = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemProgram = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemProgramEditer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemControl = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemControlPower = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemControlSwitch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemControlCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.loadControllerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemControlOxilo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemControlAccessories = new System.Windows.Forms.ToolStripMenuItem();
            this.connectionManageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemSystemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmenuItemSystemCheck = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tsmenuItemProgram,
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
            this.tsmenuItemProjectCreareNew,
            this.tsmenuItemProjectOpen,
            this.tsmenuItemProjectExit,
            this.saveToolStripMenuItem});
            this.tsmenuItemProject.Name = "tsmenuItemProject";
            this.tsmenuItemProject.Size = new System.Drawing.Size(63, 25);
            this.tsmenuItemProject.Text = "Dự án";
            // 
            // tsmenuItemProjectCreareNew
            // 
            this.tsmenuItemProjectCreareNew.Name = "tsmenuItemProjectCreareNew";
            this.tsmenuItemProjectCreareNew.Size = new System.Drawing.Size(137, 26);
            this.tsmenuItemProjectCreareNew.Text = "Tạo mới";
            this.tsmenuItemProjectCreareNew.Click += new System.EventHandler(this.tsmenuItemProjectCreareNew_Click);
            // 
            // tsmenuItemProjectOpen
            // 
            this.tsmenuItemProjectOpen.Name = "tsmenuItemProjectOpen";
            this.tsmenuItemProjectOpen.Size = new System.Drawing.Size(137, 26);
            this.tsmenuItemProjectOpen.Text = "Mở";
            this.tsmenuItemProjectOpen.Click += new System.EventHandler(this.tsmenuItemProjectOpen_Click);
            // 
            // tsmenuItemProjectExit
            // 
            this.tsmenuItemProjectExit.Name = "tsmenuItemProjectExit";
            this.tsmenuItemProjectExit.Size = new System.Drawing.Size(137, 26);
            this.tsmenuItemProjectExit.Text = "Thoát";
            this.tsmenuItemProjectExit.Click += new System.EventHandler(this.tsmenuItemProjectExit_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // tsmenuItemProgram
            // 
            this.tsmenuItemProgram.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmenuItemProgramEditer});
            this.tsmenuItemProgram.Name = "tsmenuItemProgram";
            this.tsmenuItemProgram.Size = new System.Drawing.Size(115, 25);
            this.tsmenuItemProgram.Text = "Chương trình";
            // 
            // tsmenuItemProgramEditer
            // 
            this.tsmenuItemProgramEditer.Name = "tsmenuItemProgramEditer";
            this.tsmenuItemProgramEditer.Size = new System.Drawing.Size(150, 26);
            this.tsmenuItemProgramEditer.Text = "Soạn thảo";
            this.tsmenuItemProgramEditer.Click += new System.EventHandler(this.tsmenuItemProgramEditer_Click);
            // 
            // tsmenuItemControl
            // 
            this.tsmenuItemControl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmenuItemControlPower,
            this.tsmenuItemControlSwitch,
            this.tsmenuItemControlCheck,
            this.loadControllerToolStripMenuItem,
            this.tsmenuItemControlOxilo,
            this.tsmenuItemControlAccessories,
            this.connectionManageToolStripMenuItem});
            this.tsmenuItemControl.Name = "tsmenuItemControl";
            this.tsmenuItemControl.Size = new System.Drawing.Size(96, 25);
            this.tsmenuItemControl.Text = "Điều khiển";
            // 
            // tsmenuItemControlPower
            // 
            this.tsmenuItemControlPower.Name = "tsmenuItemControlPower";
            this.tsmenuItemControlPower.Size = new System.Drawing.Size(219, 26);
            this.tsmenuItemControlPower.Text = "Nguồn";
            this.tsmenuItemControlPower.Click += new System.EventHandler(this.tsmenuItemControlPower_Click);
            // 
            // tsmenuItemControlSwitch
            // 
            this.tsmenuItemControlSwitch.Name = "tsmenuItemControlSwitch";
            this.tsmenuItemControlSwitch.Size = new System.Drawing.Size(219, 26);
            this.tsmenuItemControlSwitch.Text = "Chuyển mạch";
            this.tsmenuItemControlSwitch.Click += new System.EventHandler(this.tsmenuItemControlSwitch_Click);
            // 
            // tsmenuItemControlCheck
            // 
            this.tsmenuItemControlCheck.Name = "tsmenuItemControlCheck";
            this.tsmenuItemControlCheck.Size = new System.Drawing.Size(219, 26);
            this.tsmenuItemControlCheck.Text = "Kiểm  tra";
            this.tsmenuItemControlCheck.Click += new System.EventHandler(this.tsmenuItemControlCheck_Click);
            // 
            // loadControllerToolStripMenuItem
            // 
            this.loadControllerToolStripMenuItem.Name = "loadControllerToolStripMenuItem";
            this.loadControllerToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.loadControllerToolStripMenuItem.Text = "Tải";
            this.loadControllerToolStripMenuItem.Click += new System.EventHandler(this.tảiToolStripMenuItem_Click);
            // 
            // tsmenuItemControlOxilo
            // 
            this.tsmenuItemControlOxilo.Name = "tsmenuItemControlOxilo";
            this.tsmenuItemControlOxilo.Size = new System.Drawing.Size(219, 26);
            this.tsmenuItemControlOxilo.Text = "Hiện sóng";
            this.tsmenuItemControlOxilo.Click += new System.EventHandler(this.tsmenuItemControlOxilo_Click);
            // 
            // tsmenuItemControlAccessories
            // 
            this.tsmenuItemControlAccessories.Name = "tsmenuItemControlAccessories";
            this.tsmenuItemControlAccessories.Size = new System.Drawing.Size(219, 26);
            this.tsmenuItemControlAccessories.Text = "Linh kiện";
            this.tsmenuItemControlAccessories.Click += new System.EventHandler(this.linhKiệnToolStripMenuItem_Click);
            // 
            // connectionManageToolStripMenuItem
            // 
            this.connectionManageToolStripMenuItem.Name = "connectionManageToolStripMenuItem";
            this.connectionManageToolStripMenuItem.Size = new System.Drawing.Size(185, 26);
            this.connectionManageToolStripMenuItem.Text = "Quản lý kết nối";
            this.connectionManageToolStripMenuItem.Click += new System.EventHandler(this.connectionManageToolStripMenuItem_Click);
            // 
            // tsmenuItemSystem
            // 
            this.tsmenuItemSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmenuItemSystemEdit,
            this.tsmenuItemSystemCheck});
            this.tsmenuItemSystem.Name = "tsmenuItemSystem";
            this.tsmenuItemSystem.Size = new System.Drawing.Size(86, 25);
            this.tsmenuItemSystem.Text = "Hệ thống";
            // 
            // tsmenuItemSystemEdit
            // 
            this.tsmenuItemSystemEdit.Name = "tsmenuItemSystemEdit";
            this.tsmenuItemSystemEdit.Size = new System.Drawing.Size(152, 26);
            this.tsmenuItemSystemEdit.Text = "Tùy chỉnh";
            this.tsmenuItemSystemEdit.Click += new System.EventHandler(this.tsmenuItemSystemEdit_Click);
            // 
            // tsmenuItemSystemCheck
            // 
            this.tsmenuItemSystemCheck.Name = "tsmenuItemSystemCheck";
            this.tsmenuItemSystemCheck.Size = new System.Drawing.Size(152, 26);
            this.tsmenuItemSystemCheck.Text = "Kiểm tra";
            this.tsmenuItemSystemCheck.Click += new System.EventHandler(this.tsmenuItemSystemCheck_Click);
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
            this.IsMdiContainer = true;
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
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemProjectCreareNew;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemProjectOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemProjectExit;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemProgram;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemProgramEditer;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemControl;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemControlPower;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemControlSwitch;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemControlCheck;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemControlOxilo;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemSystem;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemSystemEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemSystemCheck;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem loadControllerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmenuItemControlAccessories;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectionManageToolStripMenuItem;
    }
}

