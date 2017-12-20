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
    public partial class fCreate : Form
    {
        private event EventHandler _createdNew;
        public event EventHandler CreatedNew
        {
            add { _createdNew += value; }
            remove { _createdNew -= value; }
        }
        public fCreate()
        {
            InitializeComponent();
            LoadFolderIntoCombobox();
        }

        public void LoadFolderIntoCombobox()
        {
            DirectoryInfo dir = new DirectoryInfo(Z119.ATK.Common.Const.PATH_ROOT);
            List<string> lstFolderName = new List<string>();
            try
            {
                foreach (var item in dir.GetDirectories())
                {
                    if (item.Name == Const.IMAGES_LIBLARY_NAME)
                        continue;
                    string folderName = item.Name;
                    lstFolderName.Add(folderName);
                }

                cmbListFolder.DataSource = lstFolderName;
            }
            catch (Exception e) { }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(cmbListFolder.Text) || String.IsNullOrWhiteSpace(txtProjectName.Text))
            {
                MessageBox.Show("Chưa có thư mục hoặc tên dự án!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string path = Z119.ATK.Common.Const.PATH_ROOT + @"\" + cmbListFolder.Text + @"\" + txtProjectName.Text;
            if (Directory.Exists(path))
            {
                MessageBox.Show("Dự án đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Directory.CreateDirectory(path);

            if (_createdNew != null)
                _createdNew(this, new EventArgs());

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
