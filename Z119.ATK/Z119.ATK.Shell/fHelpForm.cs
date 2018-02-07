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
    public partial class fHelpForm : Form
    {
        public fHelpForm()
        {
            InitializeComponent();
            if (Const.isAdmin) richTextBox1.ReadOnly = false;
            else richTextBox1.ReadOnly = true;

        }

        
        public void SetTitle(string title)
        {
            labelTitle.Text = title;
        }
        public void  SetContent(string content)
        {
            richTextBox1.Text = content;
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (labelTitle.Text == "Qui trình kiểm tra:"&&Const.isAdmin)
            {
                Const.proConf.TEXT_Manual = richTextBox1.Text;
            }
        }
    }
}
