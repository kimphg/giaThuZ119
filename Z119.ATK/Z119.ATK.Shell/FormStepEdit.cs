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

namespace Z119.ATK.Shell
{
    public partial class FormStepEdit : Form
    {
        private Common.StepItem se;
        BindingSource bindingSource1;
        public FormStepEdit()
        {
            InitializeComponent();
        }

        public FormStepEdit(Common.StepItem ste)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.se = ste;
            textBoxName.Text = se.mName;//se.mName;
            textBoxDescription.Text = se.mDescription;
            
            bindingSource1 = new BindingSource();

            // Bind BindingSource1 to the list of states.
            //List<StepItem> copy1 = new List<StepItem>();

            bindingSource1.DataSource = Const.stepList;
            comboListFalse.DataSource =  bindingSource1;
            comboListFalse.DisplayMember = "Key";
            comboListTrue.DataSource = bindingSource1;
            comboListTrue.DisplayMember = "Key";

        }
        public string getText()
        {
            return this.textBoxDescription.Text;
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

            comboListTrue.DataSource = stepList;
            comboListTrue.DisplayMember = "MName";
            comboListTrue.ValueMember = "MType";
            comboListTrue.BindingContext = this.BindingContext;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
