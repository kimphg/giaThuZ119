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
    public partial class fAccessories : Form
    {
        List<Button> lstControls;
        List<MyControls.ElectronicAccessories> lstControlContent;
        public fAccessories()
        {
            InitializeComponent();

            lstControls = new List<Button>();
            lstControlContent = new List<MyControls.ElectronicAccessories>();

            LoadAccessories();

            splitContainer1.SplitterDistance = splitContainer1.Width - 195;
            
        }

        public void LoadAccessories()
        {
            string[] lstFiles = Directory.GetFiles(Const.PATH_ROOT + @"\" + Const.IMAGES_LIBLARY_NAME);
            if (lstFiles == null)
                return;

            int length = lstFiles.Length;

            for (int i = 0; i < length; i++)
            {
                Button btn = new Button() { BackgroundImage = Image.FromFile(lstFiles[i]), BackgroundImageLayout = ImageLayout.Stretch, Height = 100 };
                lstControls.Add(btn);
            }

            for (int i = 0; i < length; i++)
            {
                lstControls[i].Click += FAccessories_Click;
                flowLayoutPanel1.Controls.Add(lstControls[i]);
            }
        }

        private void FAccessories_Click(object sender, EventArgs e)
        {
            MyControls.ElectronicAccessories control = new MyControls.ElectronicAccessories() {
                BackgroundImage = (sender as Button).BackgroundImage,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            lstControlContent.Add(control);
            panelContent.Controls.Add(control);
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

            if (splitContainer1.SplitterDistance < splitContainer1.Width - 195)
                btnCloseOpenExpand.Text = ">>";
            else
                btnCloseOpenExpand.Text = "<<";
        }

        private void btnCloseOpenExpand_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "<<")
            {
                splitContainer1.SplitterDistance = splitContainer1.Width - 355;
                (sender as Button).Text = ">>";
            }
            else
            {
                splitContainer1.SplitterDistance = splitContainer1.Width - 195;
                (sender as Button).Text = "<<";
            }
        }
    }
}
