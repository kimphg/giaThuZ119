using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z119.ATK.Common;

namespace Z119.ATK.Shell.Project
{
    public partial class fOpen : Form
    {

        #region Khi tạo xong một kịch bản thì roai even lên cho main biết
        private event EventHandler _selectedProject;
        public event EventHandler SelectedProject
        {
            add
            {
                _selectedProject += value;
            }
            remove
            {
                _selectedProject -= value;
            }
        }
        #endregion

        public fOpen()
        {
            InitializeComponent();

            Initialize();
            LoadFolderToCombobox();
        }

        #region Methods ==============

        public void Initialize()
        {
            DirectoryInfo dir = new DirectoryInfo(Z119.ATK.Common.Const.PATH_ROOT);
            TreeNode root = new TreeNode(Z119.ATK.Common.Const.PATH_ROOT) { Tag = Z119.ATK.Common.Const.PATH_ROOT, Text = Z119.ATK.Common.Const.PROJECT_NAME };
            root.ExpandAll();

            treeView1.Nodes.Add(root);

            AddTreeNode(ref root, Z119.ATK.Common.Const.PATH_ROOT);
        }

        public void AddTreeNode(ref TreeNode root, string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            try
            {
                foreach (var item in dir.GetDirectories())
                {
                    if (item.Name == Const.IMAGES_LIBLARY_NAME)
                        continue;
                    TreeNode newNode = new TreeNode(item.Name) { Tag = item.FullName };

                    AddTreeNode(ref newNode, item.FullName);
                    root.Nodes.Add(newNode);
                }
            }
            catch (Exception e) { }
        }

        public void LoadFolderToCombobox()
        {
            List<Tuple<string, string>> lstFolder = new List<Tuple<string, string>>();

            DirectoryInfo dir = new DirectoryInfo(Z119.ATK.Common.Const.PATH_ROOT);
            try
            {
                foreach (var item in dir.GetDirectories())
                {
                    if (item.Name == Const.IMAGES_LIBLARY_NAME)
                        continue;
                    string folder = item.Name;
                    lstFolder.Add(new Tuple<string, string>(item.Name, item.FullName));
                }
            }
            catch (Exception ex) { }

            cmbFolder.DataSource = lstFolder;
            cmbFolder.DisplayMember = "Item1";
        }

        public void LoadProjectIntoCombobox(string path)
        {
            List<Tuple<string, string>> lstFolder = new List<Tuple<string, string>>();

            DirectoryInfo dir = new DirectoryInfo(path);
            try
            {
                foreach (var item in dir.GetDirectories())
                {
                    string folder = item.Name;
                    lstFolder.Add(new Tuple<string, string>(item.Name, item.FullName));
                }
            }
            catch (Exception ex) { }

            cmbProject.DataSource = lstFolder;
            cmbProject.DisplayMember = "Item1";
            cmbProject.ValueMember = "Item2";
        }

        #endregion


        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            cmbFolder.Text = e.Node.Text;
        }

        // Sau khi chon folder thì load prọect của folder đó vào trong combobox project
        private void cmbFolder_SelectedIndexChanged(object sender, EventArgs e)
        {
            String path = ((sender as ComboBox).SelectedValue as Tuple<string, string>).Item2.ToString();
            LoadProjectIntoCombobox(path);
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbFolder.Text) || string.IsNullOrEmpty(cmbProject.Text))
            {
                MessageBox.Show("Chưa có thư mục hoặc dự án! \nHãy tạo mới một dự án ...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String path = (cmbFolder.SelectedValue as Tuple<string, string>).Item2.ToString()+ @"\" +cmbProject.Text;
            
            // Cập nhật lại path current
            Z119.ATK.Common.Const.PATH_CUREENT = path;
            // Tạo 3 Folder trong path current
            string fdNguon = Z119.ATK.Common.Const.PATH_CUREENT + @"\" + Z119.ATK.Common.Const.FD_NGUON;
            string fdChuyenMach = Z119.ATK.Common.Const.PATH_CUREENT + @"\" + Z119.ATK.Common.Const.FD_CHUYENMACH;
            string fdHienThi = Z119.ATK.Common.Const.PATH_CUREENT + @"\" + Z119.ATK.Common.Const.FD_HIENTHI;
            
            string fdTai = Z119.ATK.Common.Const.PATH_CUREENT + @"\" + Z119.ATK.Common.Const.FD_Tai;

            string fdHinhAnh = Z119.ATK.Common.Const.PATH_CUREENT + @"\" + Z119.ATK.Common.Const.FD_HIENTHI + @"\HinhAnh";
            string fSoDoNguyenLy = fdHinhAnh + @"\SoDoNguyenLy";
            string fSoDoLapRap = fdHinhAnh + @"\SoDoLapRap";

            // Kiểm tra tồn tại và tạo mới
            if (!Directory.Exists(fdNguon))
                Directory.CreateDirectory(fdNguon);
            if (!Directory.Exists(fdChuyenMach))
                Directory.CreateDirectory(fdChuyenMach);
            if (!Directory.Exists(fdHienThi))
                Directory.CreateDirectory(fdHienThi);

            if (!Directory.Exists(fdTai))
                Directory.CreateDirectory(fdTai);

            if (!Directory.Exists(fdHinhAnh))
                Directory.CreateDirectory(fdHinhAnh);
            if (!Directory.Exists(fSoDoNguyenLy))
                Directory.CreateDirectory(fSoDoNguyenLy);
            if (!Directory.Exists(fSoDoLapRap))
                Directory.CreateDirectory(fSoDoLapRap);

            // Chọn xong prọect và tạo các folder xong
            // Tạo xong rồi thì phải roai một cái event lên để thông báo cho fmain
            if (_selectedProject != null)
            {
                _selectedProject(this, new EventArgs());
            }
        }

    }
}
