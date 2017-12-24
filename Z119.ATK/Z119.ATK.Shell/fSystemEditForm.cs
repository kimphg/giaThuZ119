using Microsoft.WindowsAPICodePack.Dialogs;
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

namespace Z119.ATK.Shell
{
    public partial class fSystemEditForm : Form
    {
        public fSystemEditForm()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtPathRoot.Text = fbd.SelectedPath;
                }
            }

            //var dlg = new CommonOpenFileDialog();
            //dlg.Title = "My Title";
            //dlg.IsFolderPicker = true;

            //dlg.AddToMostRecentlyUsedList = false;
            //dlg.AllowNonFileSystemItems = false;
            //dlg.EnsureFileExists = true;
            //dlg.EnsurePathExists = true;
            //dlg.EnsureReadOnly = false;
            //dlg.EnsureValidNames = true;
            //dlg.Multiselect = false;
            //dlg.ShowPlacesList = true;

            //if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            //{
            //    var v = dlg.FileName;
            //    txtPathRoot.Text = v.ToString() + @"\";
            //}
        }

        private event EventHandler _done;
        public event EventHandler Done
        {
            add { _done += value; }
            remove { _done -= value; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtPathRoot.Text))
            {
                MessageBox.Show("Bạn chưa chọn đường dẫn để lưu!");
                return;
            }

            string path = txtPathRoot.Text;
            File.WriteAllText(Const.FILE_NAME_PATH, path);
            
            Common.ProjectManager.ReadFolderPath();
            
            if (_done != null)
                _done(null, null);
        }
    }
}
