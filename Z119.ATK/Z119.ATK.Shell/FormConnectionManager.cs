using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z119.ATK.Common;

namespace Z119.ATK.Shell
{
    public partial class FormConnectionManager : Form
    {
        public FormConnectionManager()
        {
            InitializeComponent();
            foreach (string s in SerialPort.GetPortNames())
            {
               
                    listBox1.Items.Add(s);
                    comboBox_LoadControl.Items.Add(s);
                    comboBox_switchControl.Items.Add(s);
                    comboBox_LoadControl.Text = Const.proConf.COM_loadCtrl;
                    comboBox_switchControl.Text = Const.proConf.switchCtrl;
                
            }
            if (Z119.ATK.Common.Const.isPower1On) listBox3.Items.Add(Z119.ATK.Common.Const.PowerAddress1);
            if (Z119.ATK.Common.Const.isPower2On) listBox3.Items.Add(Z119.ATK.Common.Const.PowerAddress2);
            //comboBox_powerControl.DataSource = listBox1.Items;
            SharpVisaCLI.Program.List((inst) => { if (inst.Contains("DS"))listBox2.Items.Add(inst); });//USB0::0x1AB1::0x0588::DS1ET183710912::INSTR
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Z119.ATK.Common.Const.proConf.COM_loadCtrl   = comboBox_LoadControl.Text;
            //Z119.ATK.Common.Const.proConf.powerCtrl  = comboBox_powerControl.SelectedItem.ToString();
            Z119.ATK.Common.Const.proConf.switchCtrl = comboBox_switchControl.Text;
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Common.Const.proConf.oscilloCtrl =  listBox2.SelectedItem.ToString();
        }
    }
}
