using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z119.ATK.Check;
using Z119.ATK.Load;
using Z119.ATK.Model.BindingModel;

namespace Z119.ATK.Shell
{
    public partial class fCheckForm : Form
    {
        #region Expend

        Image imgOriginalAssemblyDiagram;
        int zoomAssemblyDiagram = 0;
        Image imgOriginalPrincipleDiagram;
        int zoomPrincipleDiagram = 0;

        private Image Zoom(Image img, Size size)
        {

            Bitmap bmp = new Bitmap(img, img.Width + (img.Width * size.Width / 100), img.Height + (img.Height * size.Height / 100));
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }

        #endregion End Expend

        CheckManager _checkManager;

        private event EventHandler _startAll;
        public event EventHandler StartAll
        {
            add { _startAll += value; }
            remove { _startAll -= value; }
        }

        private event EventHandler _stopAll;
        private fScheme fSodoNL,fSodoLR;
        public event EventHandler StopAll
        {
            add { _stopAll += value; }
            remove { _stopAll -= value; }
        }

        public fCheckForm()
        {
            InitializeComponent();
            _checkManager = new CheckManager();

            splitContainer2.SplitterDistance = splitContainer2.Width / 2;

            LoadPrincipleDiagram(Z119.ATK.Common.Const.PATH_CURRENT + @"\" + Z119.ATK.Common.Const.FD_HIENTHI + @"\HinhAnh\SoDoNguyenLy");
            LoadAssemblyDiagram(Z119.ATK.Common.Const.PATH_CURRENT + @"\" + Z119.ATK.Common.Const.FD_HIENTHI + @"\HinhAnh\SoDoLapRap");

            //if (!string.IsNullOrEmpty(cmbAssemblyDiagram.Text))
             //   cmbAssemblyDiagram_SelectionChangeCommitted(null, null);
            //if (!string.IsNullOrEmpty(cmbPrincipleDiagram.Text))
            //    cmbPrincipleDiagram_SelectionChangeCommitted(null, null);


            splitContainer1.SplitterDistance = 400;

            // Mouse scroll for picture
           // panel16.MouseWheel += Panel16_MouseWheel;
            //panel17.MouseWheel += Panel17_MouseWheel;
            //new code
            fSodoNL = new fScheme(0);
            //fSodo.WindowState = FormWindowState.Normal;
            fSodoNL.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            fSodoNL.StartPosition = FormStartPosition.Manual;
            fSodoNL.TopLevel = false;
            fSodoNL.Location = new Point(0, 0);
            fSodoNL.LoadScheme();
            this.panel13.Controls.Add(fSodoNL);
            fSodoNL.Size = this.panel13.Size;
            this.panel13.SizeChanged += panel13_SizeChanged;
            fSodoNL.Show();
            //
            fSodoLR = new fScheme(1);
            //fSodo.WindowState = FormWindowState.Normal;
            fSodoLR.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            fSodoLR.StartPosition = FormStartPosition.Manual;
            fSodoLR.TopLevel = false;
            fSodoLR.Location = new Point(0, 0);
            fSodoLR.LoadScheme();
            this.panel14.Controls.Add(fSodoLR);
            fSodoLR.Size = this.panel13.Size;
            this.panel14.SizeChanged += panel14_SizeChanged;
            fSodoLR.Show();
        }

        private void panel14_SizeChanged(object sender, EventArgs e)
        {
            fSodoLR.Size = this.panel14.Size;
        }

        void panel13_SizeChanged(object sender, EventArgs e)
        {
            fSodoNL.Size = this.panel13.Size;
        }


        // Mouse scroll for picture PrincipleDiagram
       
        


        #region Methods ****************************
        public void LoadAssemblyDiagram(string path)
        {
            List<Tuple<string, string>> lstFolder = new List<Tuple<string, string>>();

            DirectoryInfo dir = new DirectoryInfo(path);
            try
            {
                foreach (var item in dir.GetFiles())
                {
                    string file = item.Name;
                    lstFolder.Add(new Tuple<string, string>(item.Name, item.FullName));
                }
            }
            catch (Exception ex) { }

            //cmbAssemblyDiagram.DataSource = lstFolder;
            //cmbAssemblyDiagram.DisplayMember = "Item1";
            //cmbAssemblyDiagram.ValueMember = "Item2";
        }

        public void LoadPrincipleDiagram(string path)
        {
            List<Tuple<string, string>> lstFolder = new List<Tuple<string, string>>();

            DirectoryInfo dir = new DirectoryInfo(path);
            try
            {
                foreach (var item in dir.GetFiles())
                {
                    string file = item.Name;
                    lstFolder.Add(new Tuple<string, string>(item.Name, item.FullName));
                }
            }
            catch (Exception ex) { }

        }

        private void ConvertModelToControl(CheckBindingModel model)
        {
            txbAmpeDD.Text = model.Ampe;
            txbVonDD1.Text = model.Von;
            txbVonDD2.Text = model.VonDenta;
            
            if (!string.IsNullOrEmpty(model.AssemblyDiagram))
                try
                {
                    //cmbAssemblyDiagram.Text = model.AssemblyDiagram;
                    //picAssemblyDiagram.Image = Image.FromFile(cmbAssemblyDiagram.SelectedValue.ToString());
                }
                catch (Exception)
                {
                }

            if (!string.IsNullOrEmpty(model.GuideDocument))
                ricGuideDocument.Text = model.GuideDocument;
                
        }

        private void ConvertControlsToModel(CheckBindingModel model)
        {
            model.Von = txbVonDD1.Text;
            model.VonDenta = txbVonDD2.Text;
            model.Ampe = txbAmpeDD.Text;
            //if (!string.IsNullOrEmpty(cmbAssemblyDiagram.Text))
            //    model.AssemblyDiagram = cmbAssemblyDiagram.Text;
            //else
            //    model.AssemblyDiagram = "";

            

            if (!string.IsNullOrEmpty(ricGuideDocument.Text))
                model.GuideDocument = ricGuideDocument.Text;
            
        }

        #endregion End Methods ****************************

        #region Events **********************

        private void timer1_Tick(object sender, EventArgs e)
        {
            txbVonRa.Text = Z119.ATK.Common.Const.VON_RA;
            txbAmpeRa.Text = Z119.ATK.Common.Const.AMPE_RA;

            // 
            double ampeRa = 0;
            double vonRa = 0;
            double ampe = 0;
            double von1 = 0;
            double von2 = 0;

            ampeRa = Double.Parse(txbAmpeRa.Text);
            vonRa = Double.Parse(txbVonRa.Text);
            ampe = Double.Parse(txbAmpeDD.Text);
            von1 = Double.Parse(txbVonDD1.Text);
            von2 = Double.Parse(txbVonDD1.Text);

            if (txbAmpeRa.Text.IndexOf('.') > 0)
                ampeRa = Double.Parse(txbAmpeRa.Text.Replace('.', ','));
            if (txbVonRa.Text.IndexOf('.') > 0)
                vonRa = Double.Parse(txbVonRa.Text.Replace('.', ','));
            if (txbAmpeDD.Text.IndexOf('.') > 0)
                ampe = Double.Parse(txbAmpeDD.Text.Replace('.', ','));
            if (txbVonDD1.Text.IndexOf('.') > 0)
                von1 = Double.Parse(txbVonDD1.Text.Replace('.', ','));
            if (txbVonDD2.Text.IndexOf('.') > 0)
                von2 = Double.Parse(txbVonDD2.Text.Replace('.', ','));

            if (vonRa > (von1 + von2) || vonRa < (von1 - von2))
            {
                txbVonRa.BackColor = Color.Red;
            }
            else
            {
                txbVonRa.BackColor = Color.White;
            }

            if (ampeRa > ampe)
            {
                txbAmpeRa.BackColor = Color.Red;
            }
            else
            {
                txbAmpeRa.BackColor = Color.White;
            }
        }

        private void mởToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            CheckBindingModel model = new CheckBindingModel();
            ConvertControlsToModel(model);
            _checkManager.SaveIntoFile(model);
        }

        private void cmbPrincipleDiagram_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //picPrincipleDiagram.Image = Image.FromFile(cmbPrincipleDiagram.SelectedValue.ToString());
            //imgOriginalPrincipleDiagram = picPrincipleDiagram.Image;
        }

        

        #endregion End Events ************************

        private void chạyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (chạyToolStripMenuItem.Text == "Kiểm tra")
            {
                if (_startAll != null)
                    _startAll(this, null);
                chạyToolStripMenuItem.Text = "Dừng";

                timer1.Enabled = true;
            }
            else
            {
                if (_stopAll != null)
                    _stopAll(this, null);

                timer1.Enabled = false; 

                txbVonRa.Text = Z119.ATK.Common.Const.VON_RA;
                txbVonRa.Text = Z119.ATK.Common.Const.AMPE_RA;

                chạyToolStripMenuItem.Text = "Kiểm tra";
               
            }
        }

        



        

        private void btnCloseOpenExpand2_Click(object sender, EventArgs e)
        {
            if (splitContainer2.SplitterDistance > 200) splitContainer2.SplitterDistance -= 100;
            if (splitContainer2.SplitterDistance < 200) splitContainer2.SplitterDistance = 200;
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
           
        }

        private void fCheckForm_Resize(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 400;
        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        internal void LoadData()
        {
            CheckBindingModel model = _checkManager.OpenFile();
            if (model != null)
                ConvertModelToControl(model);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (splitContainer2.SplitterDistance < splitContainer2.Width - 200) splitContainer2.SplitterDistance += 100;
            if (splitContainer2.SplitterDistance > splitContainer2.Width - 200) splitContainer2.SplitterDistance = splitContainer2.Width - 200;
        }
    }
}
