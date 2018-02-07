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
        
        public fCheckForm(Form parent )
        {
            InitializeComponent();
            _checkManager = new CheckManager();
            this.MdiParent = parent;
            splitContainer2.SplitterDistance = splitContainer2.Width / 2;
            if (Z119.ATK.Common.Const.isAdmin)
            {
                txbVolSt.Enabled = true;
                this.txbVonErrMax.Enabled = true;
                this.txbAmpeSt.Enabled = true;
                this.txAmpErrMax.Enabled = true;
                comboBoxStepFail.Enabled = true;
                comboBoxStepNext.Enabled = true;
                comboBoxStepPoint.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
            }
            else
            {
                txbVolSt.Enabled = false;
                this.txbVonErrMax.Enabled = false;
                this.txbAmpeSt.Enabled = false;
                this.txAmpErrMax.Enabled = false;
                comboBoxStepFail.Enabled = false;
                comboBoxStepNext.Enabled = false;
                comboBoxStepPoint.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
 
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
            fSodoNL.MdiParent = this.MdiParent;
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
            fSodoLR.MdiParent = this.MdiParent;
            fSodoLR.Location = new Point(0, 0);
            fSodoLR.LoadScheme();
            this.panel14.Controls.Add(fSodoLR);
            fSodoLR.Size = this.panel13.Size;
            this.panel14.SizeChanged += panel14_SizeChanged;
            fSodoLR.Show();
            //
            listBox1.MouseDoubleClick += listBox1_MouseDoubleClick;
            
        }

        void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*foreach (KeyValuePair<string,StepItem> se in Const.stepList)
            {
                se.selected = false;
            }
            (listBox1.SelectedItem as StepItem).selected = true;
            EditStep();*/
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
                if (p.Selected&&(fScheme.isPointChanged))
                {
                    txbAmpeSt.Text = p.mAmpSt.ToString("0.00");
                    txAmpErrMax.Text = p.mAmpErrMax.ToString("0.00");
                    txbAmpeRa.Text = p.mAmpMes.ToString("0.00");
                    txAmpErr.Text = p.mAmpErr.ToString("0.00");
                    txbVolSt.Text = p.mVolSt.ToString("0.00");
                    txbVonErrMax.Text = p.mAmpErrMax.ToString("0.00");
                    txbVonRa.Text = p.mVolMes.ToString("0.00");
                    txVolErr.Text = p.mVolErr.ToString("0.00");
                    fScheme.isPointChanged = false;
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

        public void SaveData()
        {
            //CheckBindingModel model = new CheckBindingModel();
            //ConvertControlsToModel(model);
            //_checkManager.SaveIntoFile(model);
            List<StepItem> list = Const.stepList.Values.ToList();
            Z119.ATK.Common.ProjectManager.SaveObject(list, "quiTrinh.xml");
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

                //txbVonRa.Text = Z119.ATK.Common.Const.VON_RA;

                //txbVonRa.Text = Z119.ATK.Common.Const.AMPE_RA;

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
            List<StepItem> list = Z119.ATK.Common.ProjectManager.LoadObject<List<StepItem>>("quiTrinh.xml");
            Const.stepList = new SortedDictionary<string,StepItem>();
            foreach(StepItem si in list)
            {
                Const.stepList.Add(si.mName,si);
            }
            
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
            try
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
                double ampErr = 0;// Double.Parse(txAmpErr.Text);
                double volMes = 0;
                double volErr = 0;// Double.Parse(txVolErr.Text);
                double volSt = 0;
                double volErrMax = 0;

                ampMes = Double.Parse(txbAmpeRa.Text);
                volMes = Double.Parse(txbVonRa.Text);
                ampSt = Double.Parse(txbAmpeSt.Text);
                volSt = Double.Parse(txbVolSt.Text);
                volErrMax = Double.Parse(txbVonErrMax.Text);

                txAmpErr.Text = (ampMes - ampSt).ToString("0.00");
                txVolErr.Text = (volMes - volSt).ToString("0.00");
                if (volMes > (volSt + volErrMax) || volMes < (volSt - volErrMax))
                {
                    label_kl_voltage.BackColor = Color.Red; label_kl_voltage.Text = "Không đạt";
                }
                else
                {
                    label_kl_voltage.BackColor = Color.Green; label_kl_voltage.Text = "Đạt";
                }

                if (ampMes > ampSt + ampErrMax)
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
                    if (p.Selected)
                    {
                        p.mAmpSt = Double.Parse(txbAmpeSt.Text);
                        p.mAmpErrMax = Double.Parse(txAmpErrMax.Text);
                        p.mAmpMes = Double.Parse(txbAmpeRa.Text);
                        p.mAmpErr = Double.Parse(txAmpErr.Text);
                        p.mVolSt = Double.Parse(txbVolSt.Text);
                        p.mAmpErrMax = Double.Parse(txbVonErrMax.Text);
                        p.mVolMes = Double.Parse(txbVonRa.Text);
                        p.mVolErr = Double.Parse(txVolErr.Text);
                        //this.fSodoNL.isPointChanged = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
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
        }//!!!
        private void button3_Click(object sender, EventArgs e)
        {
            AddStep(getInputString("Nhập tên thao tác:"), 2);
            //UpdateList();
        }

        private void AddStep(string p, int type, StepItem nextTrue = null, StepItem nextFalse = null)
        {
            if (p == null)
            {
                MessageBox.Show("Tên không hợp lệ");
                return;
            }
            if (FindItem(p) != null)
            {
                MessageBox.Show("Tên như vậy đã tồn tại");
                return;
            }
            if(type==2)//thao tac
            {
                
                StepItem newstep = new StepItem();
                newstep.Init(p,  nextTrue);
                Const.stepList.Add(p,newstep);
                UpdateList();
            }
            else if (type == 3)//dieu kien
            {
                StepItem newstep = new StepItem();
                newstep.Init(p, nextTrue, nextFalse);
                Const.stepList.Add(p,newstep);
                UpdateList();
            }
            
        }

        private void UpdateList()
        {
            var bindingSource1 = new BindingSource();

            // Bind BindingSource1 to the list of states.
            bindingSource1.DataSource = Const.stepList;
            listBox1.DataSource = bindingSource1;
            listBox1.DisplayMember = "Key";
            listBox1.BindingContext = this.BindingContext;
            listBox1.Refresh();
            comboBoxStepFail.DataSource = Const.stepList.Keys.ToList();
            comboBoxStepNext.DataSource = Const.stepList.Keys.ToList();
            comboBoxStepPoint.DataSource = Const.schemePointList;
            comboBoxStepPoint.DisplayMember = "MName";
            //comboBoxStepPoint.ValueMember = "mName";
            //this.Invalidate();
        }
        void selectSchemePoint(string str)
        {
            foreach (schemePoint sp in Const.schemePointList)
            {
                if (sp.MName == str) sp.selected = true;
                else sp.selected = false;
            }
 
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (label_kl_voltage.Text == "Đạt" && label_kl_amp.Text == "Đạt")
            {
                int index = listBox1.FindString(comboBoxStepNext.Text);
                if (index > -1)
                    listBox1.SetSelected(index, true);
                else
                {
                    return;
                }
            }
            else
            {
                int index = listBox1.FindString(comboBoxStepFail.Text);
                if (index > -1)
                    listBox1.SetSelected(index, true);
                else
                {
                    return;
                }
            }
        }

        private StepItem FindItem(string p)
        {
            if (Const.stepList.ContainsKey(p))
            {
                return Const.stepList[p];
            }

            else
                return null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Const.stepList.Remove(listBox1.SelectedItem as StepItem);
            if (MessageBox.Show("Xóa bước đã chọn?", "Xóa bước", MessageBoxButtons.YesNo) == DialogResult.No) return;
            Const.stepList.Remove(this.textBoxStepName.Text);
            UpdateList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AddStep(getInputString("Nhập tên điều kiện:"),3);

            this.SaveData();

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string key = listBox1.GetItemText(listBox1.SelectedItem);
            if (Const.stepList.ContainsKey(key))
            {
                StepItem value = Const.stepList[key];
                this.textBoxStepName.Text = key;
                this.textBoxStepMota.Text = value.mDescription;
                this.comboBoxStepPoint.Text = value.mPoint;
                this.comboBoxStepNext.Text = value.mNextTrue;
                this.comboBoxStepFail.Text = value.mNextFalse;
                selectSchemePoint(value.mPoint);
            }
            //UpdateList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SaveStep();
        }

        private void SaveStep()
        {
            string key = listBox1.GetItemText(listBox1.SelectedItem);
            if (Const.stepList.ContainsKey(key))
            {
                StepItem value = Const.stepList[key];
                value.mDescription = textBoxStepMota.Text;
                value.mPoint = this.comboBoxStepPoint.Text;
                value.mNextTrue = comboBoxStepNext.Text;
                value.mNextFalse = comboBoxStepFail.Text;
            }
        }

        private void comboBoxStepNext_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SaveStep();
        }

        private void comboBoxStepFail_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SaveStep();
        }

        private void textBoxStepMota_TextChanged(object sender, EventArgs e)
        {
            //SaveStep();
        }

        private void comboBoxStepPoint_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SaveStep();
        }

        private void thưMụcToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.txbVonRa.Text = fOxiloForm.MesVmean.ToString();
            this.txbAmpeRa.Text = fOxiloForm.MesVpp.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
             this.txbVonRa.Text =  Z119.ATK.Common.Const.AMPE_RA;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == 0) return;
            string key = listBox1.GetItemText(listBox1.SelectedItem);
            string keynext = listBox1.GetItemText(listBox1.Items[listBox1.SelectedIndex - 1]);
            if (Const.stepList.ContainsKey(key))
            {
                StepItem temp = Const.stepList[key];
                Const.stepList[key] = Const.stepList[keynext];
                Const.stepList[keynext] = temp;
            }
            UpdateList();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == listBox1.Items.Count) return;
            string key = listBox1.GetItemText(listBox1.SelectedItem);
            string keynext = listBox1.GetItemText(listBox1.Items[listBox1.SelectedIndex + 1]);
            if (Const.stepList.ContainsKey(key))
            {
                StepItem temp = Const.stepList[key];
                Const.stepList[key] = Const.stepList[keynext];
                Const.stepList[keynext] = temp;
            }
            UpdateList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.txbVonRa.Text = Z119.ATK.Common.Const.VON_RA;
        }

        private void hướngDẫnQuiTrìnhToolStripMenuItem_Click(object sender, EventArgs e)
        {

            fHelpForm fhelp = Application.OpenForms["fHelpForm"] as fHelpForm;
            if (fhelp == null)
            {
                fhelp = new fHelpForm();
                fhelp.MdiParent = this.MdiParent;
                fhelp.SetTitle("Qui trình kiểm tra:");
                fhelp.SetContent(Const.proConf.TEXT_Manual);
                fhelp.Show();
            }
            else
            {
                fhelp.MdiParent = this.MdiParent;
                fhelp.SetTitle("Qui trình kiểm tra:");
                fhelp.SetContent(Const.proConf.TEXT_Manual);
                fhelp.BringToFront();
            }
        }

        
    }
    
}
