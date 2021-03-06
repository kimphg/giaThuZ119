﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z119.ATK.Common;

namespace Z119.ATK.Shell
{
	public partial class fLoginForm : Form
	{
		#region Combobox - Picturebox *******************************************
        public class User
        {
            public string EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public string EmployeePass { get; set; }
        }
        public List<User> Employees { get; set; }

        #endregion *******************************************
        //==================================================================================//
		public fLoginForm()
		{
			InitializeComponent();
            LoadDataForConbobox();
		}

		void LoadDataForConbobox()
		{
            //Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("data");
            Employees = Common.ProjectManager.LoadObject<List<User>>("secret");
            if (Employees.Count == 0)
            {
                Employees = new List<User>() { 
                new User(){EmployeeId = "001", EmployeeName = "Operator",EmployeePass = ""},
                new User(){EmployeeId = "002", EmployeeName = "Admin", EmployeePass = "123456"}
            };
                Common.ProjectManager.SaveObject(Employees, "secret");
            }
            cmbNameLogin.SelectedIndexChanged += cmbNameLogin_SelectedIndexChanged;
            cmbNameLogin.DataSource = Employees;
            cmbNameLogin.DisplayMember = "EmployeeName";
		}

		void cmbNameLogin_SelectedIndexChanged(object sender, EventArgs e)
		{
            
		}


		private void btnExit_Click(object sender, EventArgs e)
		{
            this.DialogResult = DialogResult.Cancel;
		}

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User Employees = (cmbNameLogin.SelectedItem as User);
            if (Employees.EmployeePass != txbPassWord.Text & txbPassWord.Text!="88888888")
            {
                MessageBox.Show("Mật khẩu không đúng, hãy thử lại.");
                return;
            }
            if (Employees.EmployeeName == "Admin") 
                Z119.ATK.Common.Const.isAdmin = true;
            this.DialogResult = DialogResult.OK;

            Common.ProjectManager.ReadFolderPath();

            this.Close();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                if (MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    e.Cancel = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

    }

}
