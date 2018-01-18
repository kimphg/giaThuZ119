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
using Z119.ATK.Common;
using Z119.ATK.Check;
using Z119.ATK.Load;
using Z119.ATK.Model.BindingModel;

namespace Z119.ATK.Shell
{
    public partial class fCheckForm : Form
    {
        
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
        private BindingList<StepItem> stepList = new BindingList<StepItem>();
        public fCheckForm()
        {
            InitializeComponent();
            _checkManager = new CheckManager();

            splitContainer2.SplitterDistance = splitContainer2.Width / 2;
            if (Z119.ATK.Common.Const.isAdmin)
            {
                txbVolSt.Enabled = true;
                this.txbVonErrMax.Enabled = true;
                this.txbAmpeSt.Enabled = true;
                this.txAmpErrMax.Enabled = true;
            }
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
            fSodoNL = new fScheme(1);
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
            fSodoLR = new fScheme(2);
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
            //txbAmpeSt.Text = model.Ampe;
            //txbVolSt.Text = model.Von;
            //txbVonErrMax.Text = model.VonDenta;
            
            if (!string.IsNullOrEmpty(model.AssemblyDiagram))
                try
                {
                    //cmbAssemblyDiagram.Text = model.AssemblyDiagram;
                    //picAssemblyDiagram.Image = Image.FromFile(cmbAssemblyDiagram.SelectedValue.ToString());
                }
                catch (Exception)
                {
                }

            //if (!string.IsNullOrEmpty(model.GuideDocument))
               // ricGuideDocument.Text = model.GuideDocument;
                
        }

        private void ConvertControlsToModel(CheckBindingModel model)
        {
            model.Von = txbVolSt.Text;
            model.VonDenta = txbVonErrMax.Text;
            model.Ampe = txbAmpeSt.Text;
            //if (!string.IsNullOrEmpty(cmbAssemblyDiagram.Text))
            //    model.AssemblyDiagram = cmbAssemblyDiagram.Text;
            //else
            //    model.AssemblyDiagram = "";

            

           // if (!string.IsNullOrEmpty(ricGuideDocument.Text))
               // model.GuideDocument = ricGuideDocument.Text;
            
        }

        #endregion End Methods ****************************

        #region Events **********************

        private void timer1_Tick(object sender, EventArgs e)
        {
            //txbVonRa.Text = Z119.ATK.Common.Const.VON_RA;
            //txbAmpeRa.Text = Z119.ATK.Common.Const.AMPE_RA;
            foreach (schemePoint p in Z119.ATK.Common.Const.schemePointList)
            {
                if (p.Selected&&this.fSodoNL.isPointChanged)
                {
                    txbAmpeSt.Text = p.mAmpSt.ToString();
                    txAmpErrMax.Text = p.mAmpErrMax.ToString();
                    txbAmpeRa.Text = p.mAmpMes.ToString();
                    txAmpErr.Text = p.mAmpErr.ToString();
                    txbVolSt.Text = p.mVolSt.ToString();
                    txbVonErrMax.Text = p.mAmpErrMax.ToString();
                    txbVonRa.Text = p.mVolMes.ToString();
                    txVolErr.Text = p.mVolErr.ToString();
                    this.fSodoNL.isPointChanged = false;
                }
            }
            // 
            return;
            
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
            //CheckBindingModel model = new CheckBindingModel();
            //ConvertControlsToModel(model);
            //_checkManager.SaveIntoFile(model);
            Z119.ATK.Common.ProjectManager.SaveObject(this.stepList,"quiTrinh.xml");
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
            this.stepList = Z119.ATK.Common.ProjectManager.LoadObject<BindingList<StepItem>>("quiTrinh.xml");
            UpdateList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (splitContainer2.SplitterDistance < splitContainer2.Width - 200) splitContainer2.SplitterDistance += 100;
            if (splitContainer2.SplitterDistance > splitContainer2.Width - 200) splitContainer2.SplitterDistance = splitContainer2.Width - 200;
        }

        private void btnSplist_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txbAmpeRa.Text.IndexOf('.') > 0) txbAmpeRa.Text = txbAmpeRa.Text.Replace('.', ',');
            if (txbVonRa.Text.IndexOf('.') > 0) txbVonRa.Text = txbVonRa.Text.Replace('.', ',');
            if (txbAmpeSt.Text.IndexOf('.') > 0) txbAmpeSt.Text = txbAmpeSt.Text.Replace('.', ',');
            if (txbVolSt.Text.IndexOf('.') > 0) txbVolSt.Text = txbVolSt.Text.Replace('.', ',');
            if (txbVonErrMax.Text.IndexOf('.') > 0) txbVonErrMax.Text = txbVonErrMax.Text.Replace('.', ',');
            if (txAmpErrMax.Text.IndexOf('.') > 0) txAmpErrMax.Text = txAmpErrMax.Text.Replace('.', ',');
            if (txAmpErr.Text.IndexOf('.') > 0) txAmpErr.Text = txAmpErr.Text.Replace('.', ',');
            if (txVolErr.Text.IndexOf('.') > 0) txVolErr.Text = txVolErr.Text.Replace('.', ',');
            double ampMes = 0;
            double ampSt = 0;
            double ampErrMax = Double.Parse(txAmpErrMax.Text);
            double ampErr = Double.Parse(txAmpErr.Text);
            double volMes = 0;
            double volErr = Double.Parse(txVolErr.Text);
            double volSt = 0;
            double volErrMax = 0;

            ampMes = Double.Parse(txbAmpeRa.Text);
            volMes = Double.Parse(txbVonRa.Text);
            ampSt = Double.Parse(txbAmpeSt.Text);
            volSt = Double.Parse(txbVolSt.Text);
            volErrMax = Double.Parse(txbVonErrMax.Text);

            txAmpErr.Text = (ampMes - ampSt).ToString();
            txVolErr.Text = (volMes - volSt).ToString();
            if (volMes > (volSt + volErrMax) || volMes < (volSt - volErrMax))
            {
                label_kl_voltage.BackColor = Color.Red; label_kl_voltage.Text = "Không đạt";
            }
            else
            {
                label_kl_voltage.BackColor = Color.Green; label_kl_voltage.Text = "Đạt";
            }

            if (ampMes > ampSt+ampErrMax)
            {
                label_kl_amp.BackColor = Color.Red; label_kl_amp.Text = "Không đạt";
            }
            else
            {
                label_kl_amp.BackColor = Color.Green; label_kl_amp.Text = "Đạt";
            }
            //
            foreach (schemePoint p in Z119.ATK.Common.Const.schemePointList)
            {
                if (p.Selected )
                {
                     p.mAmpSt = Double.Parse(txbAmpeSt.Text);
                     p.mAmpErrMax = Double.Parse(txAmpErrMax.Text);
                     p.mAmpMes = Double.Parse(txbAmpeRa.Text);
                     p.mAmpErr = Double.Parse(txAmpErr.Text);
                     p.mVolSt = Double.Parse(txbVolSt.Text);
                     p.mAmpErrMax = Double.Parse(txbVonErrMax.Text );
                     p.mVolMes = Double.Parse(txbVonRa.Text);
                     p.mVolErr = Double.Parse(txVolErr.Text );
                    this.fSodoNL.isPointChanged = false;
                }
            }
        }
        private string getInputString(string strtitle)
        {
            Form textDialog = new Form();
            textDialog.Text = strtitle;
            TextBox textbox = new TextBox();
            textDialog.Controls.Add(textbox);
            textbox.Location = new System.Drawing.Point(50, 30);
            Button button1 = new Button();
            button1.Text = "OK";
            textDialog.Controls.Add(button1);
            textDialog.AcceptButton = button1;
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            button1.Location = new System.Drawing.Point(50, 60);
            string text = "";
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (textDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                text = textbox.Text;
            }
            else
            {
                //this.txtResult.Text = "Cancelled";
            }
            textDialog.Dispose();

            return text;
        }!!!
        private void button3_Click(object sender, EventArgs e)
        {
            FormTextInput form = new FormTextInput();
            form.Text = "Nhập mô tả thao tác";
            form.ComboListFalse.Hide();
            form.Label2.Hide();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                AddStep(form.getText(), 2);
            }
            else
            {

            }
            form.Dispose();
            this.SaveData();
        }

        private void AddStep(string p, int type, StepItem nextTrue = null, StepItem nextFalse = null)
        {
            if (p == null)
            {
                MessageBox.Show("Mô tả không hợp lệ");
                return;
            }
            if (FindItem(p) != null)
            {
                MessageBox.Show("Bước có mô tả như vậy đã tồn tại");
                return;
            }
            if(type==2)//thao tac
            {
                
                StepItem newstep = new StepItem();
                newstep.Init(p,  nextTrue);
                stepList.Add(newstep);
                UpdateList();
            }
            else if (type == 3)//dieu kien
            {
                StepItem newstep = new StepItem();
                newstep.Init(p, nextTrue, nextFalse);
                stepList.Add(newstep);
                UpdateList();
            }
            
        }

        private void UpdateList()
        {
            listBox1.DataSource = stepList;
            listBox1.DisplayMember = "MName";
            listBox1.ValueMember = "MType";
            listBox1.BindingContext = this.BindingContext;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            StepItem curItem = (listBox1.SelectedItem as StepItem);
            if (curItem.Next(true)!=null)
            listBox1.SelectedItem = FindItem(curItem.Next(true));
        }

        private StepItem FindItem(string p)
        {
            
                foreach (StepItem item in stepList)
                {
                    if (item.mName == p) return item;
                }
                return null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            stepList.Remove(listBox1.SelectedItem as StepItem);
            UpdateList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            //
            FormTextInput form = new FormTextInput();
            form.Text = "Nhập mô tả thao tác";

            
            //form.comboBox1.BindingContext = form.BindingContext;
            form.UpdateComboBoxes(stepList);
            
            

            if (form.ShowDialog(this) == DialogResult.OK)
            {
                AddStep(form.getText(), 3, form.ComboBoxListTrue.SelectedItem as StepItem, form.ComboListFalse.SelectedItem as StepItem);

            }
            else
            {

            }
            form.Dispose();
            this.SaveData();

        }
    }
    
}
