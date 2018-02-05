﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z119.ATK.Shell.Project;

namespace Z119.ATK.Shell
{
    public partial class fMainWindow : Form
    {

        #region Properties
        //List<Project.fOpen> lstForm = new List<Project.fOpen>();
        //Project.fOpen _project1;
        #endregion
        
        public 
            fMainWindow()
        {
            InitializeComponent();
            Z119.ATK.Common.Const.mainForm = this;
            Left = Top = 0;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            Init(null,null);
            this.FormClosing += fMainWindow_FormClosing;
            //fOxiloForm formtest = new fOxiloForm();
            //formtest.MdiParent = this;

            //formtest.Show();
            // End Menu project
        }

        void fMainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn lưu phiên làm việc hiện tại vào dự án?", "Thoát chương trình",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Z119.ATK.Common.Const.projMan.SaveProjectConfig();
                fcheck.SaveData();
                fScheme.SavePointList();
                 frmPower.SaveData();
                 frmSwitch.SaveData();
                 frmTai.SaveData();
                //Z119.ATK.Common.ProjectManager.SaveObject(this, Z119.ATK.Common.Const.FILE_MAINWINDOW);
            }
            else if (result == DialogResult.No)
            {
                
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        // *************************************************************************************
        // *************************************************************************************
        // *************************************************************************************

        #region Methods =========================================




        // Hàm khởi tạo đầu tiên
        void Initialize()
        {/*
            _project1 = new Project.fOpen();
            _project1.Tag = "Open";
            _project1.SelectedProject += frm_SelectedProject;
            lstForm.Add(_project1);*/
        }
        
        public void ShowForm(Form frm, string name)
        {
            if (name == "Open")
            {
                //KichBan.fMoKichBan frm = new KichBan.fMoKichBan();
                frm.MdiParent = this;
                frm.Show();
                frm.Location = new Point(0, 0);
                frm.Width = 554;
                frm.Height = this.Height - 75;
            }
            else if (name == "Create")
            {
                //KichBan.TaoMoiKichBan frm = new KichBan.TaoMoiKichBan();
                frm.MdiParent = this;
                //frm.FormClosed += frm_FormClosed;
                (frm as fCreate).CreatedNew += FMainWindow_CreatedNew;
                frm.Show();
                frm.Location = new Point(555, 200);
            }
        }

        private void FMainWindow_CreatedNew(object sender, EventArgs e)
        {
            if (IsExitsForm("Open"))
                ClearFormChildrent();

            /*
            // Dùng để Khi tạo xong thì load lại vào form mở project
            lstForm[0].Close();
            lstForm.Clear();

            Project.fOpen _project2 = new Project.fOpen();
            lstForm.Add(_project2);
            _project2.SelectedProject += frm_SelectedProject;
            ShowForm(lstForm[0], "Open");*/
        }

        public void ClearForm()
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm != null)
                    try
                    {
                        frm.Close();
                    }
                    catch (Exception)
                    { }
                   
            }
        }
        
        public bool IsExitsForm(string name)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if ((string)frm.Tag == name)
                    return true;
            }
            return false;
        }

        // Kiểm tra đã chọn project hay chưa
        public bool IsSelectedProject()
        {
            return !Z119.ATK.Common.Const.PATH_CURRENT.Equals(Z119.ATK.Common.Const.PATH_ROOT);
        }

        // Xóa tất cả các form Childrent
        void ClearFormChildrent()
        {
            foreach (var item in this.MdiChildren)
            {
                item.Close();
            }
        }

        #endregion End Methods =========================================

        // *************************************************************************************
        // *************************************************************************************
        // *************************************************************************************

        #region Events ====================================

        fPower1 frmPower;
        fSwitchForm frmSwitch;
        fLoadForm frmTai;
        fCheckForm fcheck;

        // Sau khi chon project thi bao len laf da chon
        void Init(object sender, EventArgs e)
        {
            //(sender as Project.fOpen).Close();
            
            
            
            if (frmPower != null)
                frmPower.Close();
            if (frmSwitch != null)
                frmSwitch.Close();
            if (frmTai != null)
                frmTai.Close();
            if (fcheck != null)
                fcheck.Close();
            
            //MessageBox.Show("Đã chọn dự án");

             frmPower = new fPower1();//Z119.ATK.Common.ProjectManager.LoadObject<fPower1>("fPower1"); 
            frmSwitch = new fSwitchForm();//frmSwitch = Z119.ATK.Common.ProjectManager.LoadObject<fSwitchForm>("fSwitchForm");//new fSwitchForm();
             //frmSwitch = new fSwitchForm();
             frmTai = new fLoadForm(); //frmTai = Z119.ATK.Common.ProjectManager.LoadObject<fLoadForm>("fLoadForm");
             fcheck = new fCheckForm(this); //fcheck = Z119.ATK.Common.ProjectManager.LoadObject<fCheckForm>("fCheckForm");

            frmPower.FormClosing += FrmPower_FormClosing;
            frmSwitch.FormClosing += FrmSwitch_FormClosing;
            frmTai.FormClosing += FrmTai_FormClosing;
            fcheck.FormClosing += Fcheck_FormClosing;

            frmPower.LoadData();
            frmSwitch.LoadData();//todo here
            frmTai.LoadData();
            fcheck.LoadData();

            frmPower.MdiParent = this;
            frmSwitch.MdiParent = this;
            frmTai.MdiParent = this;
            fcheck.MdiParent = this;

            frmPower.Show();
            frmPower.Location = new Point(0, 0);

            frmSwitch.WindowState = FormWindowState.Normal;
            frmSwitch.StartPosition = FormStartPosition.Manual;
            frmSwitch.Show();
            frmSwitch.Location = new Point(929, 0);

            frmTai.WindowState = FormWindowState.Normal;
            frmTai.StartPosition = FormStartPosition.Manual;
            frmTai.Show();
            frmTai.Location = new Point(1247, 0);

            fcheck.WindowState = FormWindowState.Normal;
            fcheck.StartPosition = FormStartPosition.Manual;
            fcheck.Show();
            fcheck.Location = new Point(100, 100);

            fcheck.StartAll += fcheck_StartAll;
            fcheck.StopAll += fcheck_StopAll;
            // newcode start
            
        }

        private void Fcheck_FormClosing(object sender, FormClosingEventArgs e)
        {
            tsmenuItemControlCheck.Enabled = true;
        }

        private void FrmTai_FormClosing(object sender, FormClosingEventArgs e)
        {
            loadControllerToolStripMenuItem.Enabled = true;
        }

        private void FrmSwitch_FormClosing(object sender, FormClosingEventArgs e)
        {
            tsmenuItemControlSwitch.Enabled = true;
        }

        private void FrmPower_FormClosing(object sender, FormClosingEventArgs e)
        {
            tsmenuItemControlPower.Enabled = true;
        }

        void fcheck_StartAll(object sender, EventArgs e)
        {
            frmPower.isRunAll = false;
            frmPower.RunOffFromServer();
            frmSwitch.isRunServer = false;
            frmSwitch.RunFromServer();
            frmTai.isRunServer = false;
            frmTai.RunFromServer();
        }

        void fcheck_StopAll(object sender, EventArgs e)
        {
            frmPower.isRunAll = true;
            frmPower.RunOffFromServer();
            frmSwitch.isRunServer = true;
            frmSwitch.RunFromServer();
            frmTai.isRunServer = true;
            frmTai.RunFromServer();
        }

        // Khi form tạo mới một project kết thúc thì roai event này lên để load lại folder cho combobox
        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (IsExitsForm("Open"))
                ClearFormChildrent();

            /*
            // Dùng để Khi tạo xong thì load lại vào form mở project
            lstForm[0].Close();
            lstForm.Clear();

            Project.fOpen _project2 = new Project.fOpen();
            lstForm.Add(_project2);
            _project2.SelectedProject += frm_SelectedProject;
            ShowForm(lstForm[0], "Open");*/
        }

        #endregion

        // *************************************************************************************
        // *************************************************************************************
        // *************************************************************************************

        #region MenuItem Project ===========================================
        private void tsmenuItemProjectOpen_Click(object sender, EventArgs e)
        {
            
            Z119.ATK.Common.Const.projMan.LoadProject();
            Init(null,null);
            //Application.Run(Z119.ATK.Common.ProjectManager.LoadObject<fMainWindow>(Z119.ATK.Common.Const.FILE_MAINWINDOW));
            /*if (IsExitsForm("Open"))
                return;
            Project.fOpen frm = new Project.fOpen();
            frm.SelectedProject += frm_SelectedProject;
            frm.Tag = "Open";
            ShowForm(frm, "Open");*/
        }
        //#endregion

        private void tsmenuItemProjectCreareNew_Click(object sender, EventArgs e)
        {
            if (IsExitsForm("Create"))
                return;
            Project.fCreate frm = new Project.fCreate();
            frm.Tag = "Create";
            ShowForm(frm, "Create");
        }
        
        private void tsmenuItemProjectExit_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        //#endregion

        #endregion End Menu project ===========================================

        // *************************************************************************************
        // *************************************************************************************
        // *************************************************************************************

        #region Another Menu  item Control============================================

        private void tsmenuItemControlPower_Click(object sender, EventArgs e)
        {
            //(sender as ToolStripMenuItem).Enabled = false;

            Form fc = Application.OpenForms["fPower1"];
            if (fc == null)
            {
                fPower1 fpower = new fPower1();
                frmPower.FormClosing += FrmPower_FormClosing;
                fpower.MdiParent = this;
                fpower.Show();
            }
            else
            {
                fc.BringToFront();
            }
        }

        private void tsmenuItemControlSwitch_Click(object sender, EventArgs e)
        {
            //(sender as ToolStripMenuItem).Enabled = false;

            Form fc = Application.OpenForms["fSwitchForm"];
            if (fc == null)
            {
                fSwitchForm fswitch = new fSwitchForm();
                frmSwitch.FormClosing += FrmSwitch_FormClosing;
                fswitch.MdiParent = this;
                fswitch.Show();
            }
            else
            {
                fc.BringToFront();
            }
        }

        private void tsmenuItemControlCheck_Click(object sender, EventArgs e)
        {
            //(sender as ToolStripMenuItem).Enabled = false;
            Form fc = Application.OpenForms["fCheckForm"];

            if (fc == null)
            {
                fCheckForm fcheck = new fCheckForm(this);
                fcheck.FormClosing += Fcheck_FormClosing;
                fcheck.MdiParent = this;
                fcheck.Show();
            }
            else
            {
                fc.BringToFront();
            }
        }

        private void tsmenuItemControlOxilo_Click(object sender, EventArgs e)
        {
            //(sender as ToolStripMenuItem).Enabled = false;
            Form fc = Application.OpenForms["fOxiloForm"];

            if (fc == null)
            {
                fOxiloForm foxilo = new fOxiloForm();
                foxilo.FormClosing += Foxilo_FormClosing;
                foxilo.MdiParent = this;
                foxilo.StartPosition = FormStartPosition.Manual;
                foxilo.Show();
            }
            else
            {
                fc.BringToFront();
            }
        }

        private void Foxilo_FormClosing(object sender, FormClosingEventArgs e)
        {
            tsmenuItemControlOxilo.Enabled = true;
        }

        private void tsmenuItemProgramEditer_Click(object sender, EventArgs e)
        {
            //(sender as ToolStripMenuItem).Enabled = false;

            Form fc = Application.OpenForms["fProgramForm"];
            if (fc == null)
            {
                fProgramForm fprogram = new fProgramForm();
                fprogram.MdiParent = this;
                fprogram.Show();
            }
            else
            {
                fc.BringToFront();
            }
        }

        private void tảiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Form fc = Application.OpenForms["fLoadForm"];
            if (fc == null)
            {
                fLoadForm frm = new fLoadForm();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                fc.BringToFront();
            }
        }

        private void tsmenuItemSystemEdit_Click(object sender, EventArgs e)
        {
            
            Form fc = Application.OpenForms["fSystemEditForm"];
            if (fc == null)
            {
                fSystemEditForm fsystemEdit = new fSystemEditForm();
                fsystemEdit.MdiParent = this;
                fsystemEdit.Done += FsystemEdit_Done;
                fsystemEdit.FormClosing += FsystemEdit_FormClosing;
                fsystemEdit.Show();
                fsystemEdit.Location = new Point(555, 20);
            }
            else
            {
                fc.BringToFront();
            }
        }

        private void FsystemEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            //tsmenuItemSystemEdit.Enabled = true;
        }

        private void FsystemEdit_Done(object sender, EventArgs e)
        {
            ClearForm();
            tsmenuItemProjectOpen_Click(null, null);
        }

        private void tsmenuItemSystemCheck_Click(object sender, EventArgs e)
        {

            Form fc = Application.OpenForms["fSystemCheckingForm"];
            if (fc == null)
            {
                fSystemCheckingForm fsystemChecking = new fSystemCheckingForm();
                fsystemChecking.MdiParent = this;
                fsystemChecking.Show();
            }
            else
            {
                fc.BringToFront();
            }
        }

        private void tsmenuItemHelp_Click(object sender, EventArgs e)
        {
            
            Form fc = Application.OpenForms["fHelpForm"];
            if (fc == null)
            {
                fHelpForm fhelp = new fHelpForm();
                fhelp.MdiParent = this;
                fhelp.Show();
            }
            else
            {
                fc.BringToFront();
            }
        }

        private void linhKiệnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (sender as ToolStripMenuItem).Enabled = false;
            fAccessories frmAcc = new fAccessories();
            frmAcc.FormClosing += FrmAcc_FormClosing;
            frmAcc.MdiParent = this;
            frmAcc.Show();
        }

        private void FrmAcc_FormClosing(object sender, FormClosingEventArgs e)
        {
            tsmenuItemControlAccessories.Enabled = true;
        }

        #endregion End Menu item Control ============================================

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Z119.ATK.Common.Const.projMan.SaveProjectConfig();
            //Z119.ATK.Common.ProjectManager.SaveObject(this, Z119.ATK.Common.Const.FILE_MAINWINDOW);

        }

        private void connectionManageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConnectionManager connectionMan = new FormConnectionManager();
            connectionMan.Show();
            
        }

        private void tsmenuItemSystem_Click(object sender, EventArgs e)
        {
            FormConnectionManager connectionMan = new FormConnectionManager();
            connectionMan.Show();
        }


    }
}
