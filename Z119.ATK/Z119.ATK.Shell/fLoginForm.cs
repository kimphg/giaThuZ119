using System;
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
        public class Employee
        {
            public string EmployeeId { get; set; }
            public string EmployeeName { get; set; }
        }
        public List<Employee> Employees { get; set; }

        #endregion *******************************************
        //==================================================================================//
		public fLoginForm()
		{
			InitializeComponent();
            LoadDataForConbobox();
		}

		void LoadDataForConbobox()
		{
            Employees = new List<Employee>() { 
                new Employee(){EmployeeId = "001", EmployeeName = "SuperAdmin"},
                new Employee(){EmployeeId = "002", EmployeeName = "Admin"},
                new Employee(){EmployeeId = "003", EmployeeName = "User"}
            };

            cmbNameLogin.SelectedIndexChanged += cmbNameLogin_SelectedIndexChanged;
            cmbNameLogin.DataSource = Employees;
            cmbNameLogin.DisplayMember = "EmployeeName";
		}

		void cmbNameLogin_SelectedIndexChanged(object sender, EventArgs e)
		{
            switch ((cmbNameLogin.SelectedItem as Employee).EmployeeId)
	        {
                case "001" : picEmployee.Image = Image.FromFile(@"..\..\Resources\Administrator-icon.png");
                    break;
                case "002": picEmployee.Image = Image.FromFile(@"..\..\Resources\Customer-service-icon.png");
                    break;
                case "003": picEmployee.Image = Image.FromFile(@"..\..\Resources\Man-icon.png");
                    break;
		        default:
                    break;
	        }
                
		}


		private void btnExit_Click(object sender, EventArgs e)
		{
            this.DialogResult = DialogResult.Cancel;
		}

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            Common.CommonFile.ReadFolderPath();

            this.Close();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                if (MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    e.Cancel = true;
        }

    }

}
