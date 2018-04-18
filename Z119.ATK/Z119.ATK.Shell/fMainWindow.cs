using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z119.ATK.Common;
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
            this.FormClosed += fMainWindow_FormClosed;
            //fOxiloForm formtest = new fOxiloForm();
            //formtest.MdiParent = this;

            //formtest.Show();
            // End Menu project
        }

        void fMainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            //throw new NotImplementedException();
            frmPower.OffPowerAll();
            frmPower.Disconnect();
            frmTai.OffLoad();
        }

        void fMainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn lưu phiên làm việc hiện tại vào dự án?", "Thoát chương trình",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                saveEverything();
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
        private void createPowerForm()
        {
            frmPower = new fPower1();
            frmPower.FormClosing += FrmPower_FormClosing;
            frmPower.MdiParent = this;
            frmPower.LoadData();//todo here
            frmPower.WindowState = FormWindowState.Normal;
            frmPower.StartPosition = FormStartPosition.Manual;
            frmPower.Location = Common.Const.proConf.locationPower;
            if (frmPower.Location.Y < 0) frmPower.Location = new Point(50, 50);
            frmPower.Show();
        }
        private void createCheckForm()
        {
            fcheck = new fCheckForm(this);
            fcheck.FormClosing += Fcheck_FormClosing;
            fcheck.MdiParent = this;
            fcheck.LoadData();//todo here
            fcheck.WindowState = FormWindowState.Normal;
            fcheck.StartPosition = FormStartPosition.Manual;
            fcheck.Location = Common.Const.proConf.locationCheck;
            if (fcheck.Location.Y< 0) fcheck.Location = new Point(50, 50);
            fcheck.StartAll += fcheck_StartAll;
            fcheck.StopAll += fcheck_StopAll;
            fcheck.Show();
        }
        private void createSwitchForm()
        {
            frmSwitch = new fSwitchForm();
            frmSwitch.FormClosing += FrmSwitch_FormClosing;
            frmSwitch.MdiParent = this;
            frmSwitch.WindowState = FormWindowState.Normal;
            frmSwitch.StartPosition = FormStartPosition.Manual;
            frmSwitch.Location = Common.Const.proConf.locationSwitch;
            if (frmSwitch.Location.Y < 0) frmSwitch.Location = new Point(50, 50);
            frmSwitch.LoadData();//todo here
            frmSwitch.Show();
        }
        private void createLoadForm()
        {
            frmTai = new fLoadForm();
            frmTai.FormClosing += FrmTai_FormClosing;
            frmTai.MdiParent = this;
            frmTai.LoadData();//todo here
            frmTai.WindowState = FormWindowState.Normal;
            frmTai.StartPosition = FormStartPosition.Manual;

            frmTai.Location = Common.Const.proConf.locationLoad;

            if (frmTai.Location.Y < 0) frmTai.Location = new Point(50, 50);
            frmTai.Show();
        }
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
            
            createSwitchForm();
            createLoadForm();
            createPowerForm();
            createCheckForm();

            // newcode start
            frmPower.OffPowerAll();
            frmTai.OffLoad();
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
            DialogResult result = MessageBox.Show("Bạn có muốn lưu phiên làm việc hiện tại vào dự án?", "Thoát dự án hiện tại",
            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                saveEverything();
            }
            else if (result == DialogResult.Cancel) return;

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

        private void saveEverything()
        {
            Const.proConf.locationCheck = fcheck.Location;
            fcheck.SaveData();
            fScheme.SavePointList();
            Const.proConf.locationPower = frmPower.Location;
            frmPower.SaveData();
            Const.proConf.locationSwitch = frmSwitch.Location;
            frmSwitch.SaveData();
            Const.proConf.locationLoad = frmTai.Location;
            frmTai.SaveData();
            Z119.ATK.Common.Const.projMan.SaveProjectConfig();
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
                createSwitchForm();
                
            }
            else
            {
                fc.Show();
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
                fc.Show();
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
                fc.Show();
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
            /*
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
            }*/
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
                fc.Show();
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

            fHelpForm fhelp = Application.OpenForms["fHelpForm"] as fHelpForm;
            if (fhelp == null)
            {
                fhelp = new fHelpForm();
                fhelp.SetTitle("Thông tin về thiết bị:");
                fhelp.SetContent(@"Chức năng của thiết bị:
-	Cung cấp đầy đủ và chính xác các loại nguồn nuôi với các tham số đảm bảo theo yêu cầu để cấp cho các modul, mảng chức năng thuộc phân hệ làm việc bình thường;
-	Tạo ra các lệnh điều khiển các chế độ làm việc để cấp cho các phân khối,  modul, mảng chức năng;
-	Điều khiển từ xa, thu thập và xử lý kết quả thông qua các thiết bị đo lường chuyên dụng được kết nối.");
                fhelp.MdiParent = this;
                fhelp.Show();
            }
            else
            {
                fhelp.SetTitle("Thông tin về thiết bị:");
                fhelp.SetContent(@"Chức năng của thiết bị:
-	Cung cấp đầy đủ và chính xác các loại nguồn nuôi với các tham số đảm bảo theo yêu cầu để cấp cho các modul, mảng chức năng thuộc phân hệ làm việc bình thường;
-	Tạo ra các lệnh điều khiển các chế độ làm việc để cấp cho các phân khối,  modul, mảng chức năng;
-	Điều khiển từ xa, thu thập và xử lý kết quả thông qua các thiết bị đo lường chuyên dụng được kết nối.");
                fhelp.MdiParent = this;
                fhelp.BringToFront();
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
            //tsmenuItemControlAccessories.Enabled = true;
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
            
            Form fc = Application.OpenForms["FormConnectionManager"];
            if (fc == null)
            {
                FormConnectionManager connectionMan = new FormConnectionManager();
                
                connectionMan.MdiParent = this;
                connectionMan.Show();
            }
            else
            {
                fc.BringToFront();
            }
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void tsmenuItemControlCheck_Click_1(object sender, EventArgs e)
        {

        }


    }
}
