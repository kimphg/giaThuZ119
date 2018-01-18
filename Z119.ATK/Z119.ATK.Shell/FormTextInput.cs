using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Z119.ATK.Shell
{
    public partial class FormTextInput : Form
    {
        public FormTextInput()
        {
            InitializeComponent();
        }
        public string getText()
        {
            return this.textBox1.Text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        internal void UpdateComboBoxes(BindingList<Common.StepItem> stepList)
        {
            comboListFalse.DataSource = stepList;
            comboListFalse.DisplayMember = "MName";
            comboListFalse.ValueMember = "MType";
            comboListFalse.BindingContext = this.BindingContext;

            comboBoxListTrue.DataSource = stepList;
            comboBoxListTrue.DisplayMember = "MName";
            comboBoxListTrue.ValueMember = "MType";
            comboBoxListTrue.BindingContext = this.BindingContext;
        }
    }
}
