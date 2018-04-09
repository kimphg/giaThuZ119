using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z119.ATK.Common;
using Z119.ATK.Load;
using Z119.ATK.Model.BindingModel;

namespace Z119.ATK.Shell
{
    public partial class fLoadForm : Form
    {
        int SelectedValueSet = 1;
        StringBuilder strResult = new StringBuilder();
        //bool RunningTask = false;

        LoadManager _loadmanager = new LoadManager();

        public fLoadForm()
        {
            InitializeComponent();

            //ConnectCOMPort();

            btnOnOff.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
            btnOnOff.BackgroundImageLayout = ImageLayout.Stretch;

            #region GUI OFF
            lblReceiveVon.ForeColor = Color.Gray;
            label4.ForeColor = Color.Gray;
            lblReceiveWoat.ForeColor = Color.Gray;
            label6.ForeColor = Color.Gray;
            lblReceiveAmpe.ForeColor = Color.Gray;
            label5.ForeColor = Color.Gray;
            #endregion End GUI OFF
        }

        public void ConnectCOMPort()
        {
            try
            {
                serialPort1.PortName = Z119.ATK.Common.Const.proConf.COM_loadCtrl;
                serialPort1.Open();
                n = 0;
            }
            catch
            {
                //MessageBox.Show("Không thể mở cổng điều khiển tải " + serialPort1.PortName, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //btnOnOff.Enabled = false;
            }

        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        private void btnButtonNumberClick(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            if (btn.Text != "Ok" && btn.Text != "C" && btn.Text != "." && btn.Text != "0")
            {
                strResult.Append(btn.Text);
            }
            else
                switch (btn.Text)
                {
                    case "C":
                        if (strResult.Length > 0) { strResult.Remove(strResult.Length - 1, 1); }
                        break;

                    case "0":
                        if (strResult.Length > 0) { strResult.Append(btn.Text); }
                        break;

                    case ".":
                        if (strResult.Length > 0) { if (strResult.ToString().IndexOf('.') > 0) { return; } else { strResult.Append(btn.Text); } } else { strResult.Append("0").Append(btn.Text); }
                        break;

                    default:
                        break;
                }

            if (SelectedValueSet == 1)
            {
                txbAValue.Text = strResult.ToString();
            }
            else if (SelectedValueSet == 2)
            {
                txbBValue.Text = strResult.ToString();
            }
        }

        private void txbAValue_Click(object sender, EventArgs e)
        {
            SelectedValueSet = 1;
            strResult.Clear();
            (sender as TextBox).Clear();
        }

        private void txbBValue_Click(object sender, EventArgs e)
        {
            SelectedValueSet = 2;
            strResult.Clear();
            (sender as TextBox).Clear();
        }

        private void txbBValue_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        bool IsOn = false;
        public async void OnOffPower()
        
        {
            if (!serialPort1.IsOpen) return;
            try
            {
                if (IsOn )
                {
                    #region GUI OFF
                    lblReceiveVon.ForeColor = Color.Gray;
                    label4.ForeColor = Color.Gray;
                    lblReceiveWoat.ForeColor = Color.Gray;
                    label6.ForeColor = Color.Gray;
                    lblReceiveAmpe.ForeColor = Color.Gray;
                    label5.ForeColor = Color.Gray;
                    #endregion End GUI OFF

                    lblReceiveVon.Text = "00.00";
                    lblReceiveAmpe.Text = "00.00";
                    lblReceiveWoat.Text = "00.00";

                    Z119.ATK.Common.Const.VON_RA = "0.00";
                    Z119.ATK.Common.Const.AMPE_RA = "0.00";

                    IsOn = false;

                    btnOnOff.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                    btnOnOff.BackgroundImageLayout = ImageLayout.Stretch;

                    serialPort1.WriteLine(Z119.ATK.Common.Const.LOAD_OFFTAI);
                    timer1.Enabled = false;

                }
                else 
                {
                    #region GUI ON
                    lblReceiveVon.ForeColor = Color.Yellow;
                    label4.ForeColor = Color.Yellow;
                    lblReceiveWoat.ForeColor = Color.Yellow;
                    label6.ForeColor = Color.Yellow;
                    lblReceiveAmpe.ForeColor = Color.Yellow;
                    label5.ForeColor = Color.Yellow;
                    #endregion End GUI ON

                    btnOnOff.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ON);
                    btnOnOff.BackgroundImageLayout = ImageLayout.Stretch;
                    IsOn = true;

                    serialPort1.WriteLine(Z119.ATK.Common.Const.LOAD_MODE_CC);
                    serialPort1.WriteLine(Z119.ATK.Common.Const.LOAD_CURR_VA + " " + (txbAValue.Text == "" ? "0" : txbAValue.Text));
                    serialPort1.WriteLine(Z119.ATK.Common.Const.LOAD_CURR_VB + " " + (txbBValue.Text == "" ? "0" : txbBValue.Text));
                    serialPort1.WriteLine(Z119.ATK.Common.Const.LOAD_ONTAI);

                    timer1.Enabled = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void btnOnOff_Click(object sender, EventArgs e)
        {
            OnOffPower();
        }

        private void btnDefaultReset_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine(Z119.ATK.Common.Const.LOAD_DEFAULT_RESET);
        }

        Action<String> SerialPortReceiveAction;
        private void SeriportReceive(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPortReceiveAction = serialPortReceive;
            try
            {
                this.BeginInvoke(SerialPortReceiveAction, serialPort1.ReadExisting());
            }
            catch { }
        }

        //Xu ly nhan du lieu
        /*--------------------------------------------------------------------------------*/
        string str_Receiver = "";
        int n = 0;
        private void serialPortReceive(String input)
        {
            n++;
            str_Receiver = input;
            textBox2.Text = input;
            try
            {
                if (n == 1)
                {
                    string text = "000" + Double.Parse(input.Replace('.', ',')) + "000";
                    if (text.IndexOf('.') < 0)
                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                    if (text.IndexOf('.') > 0)
                        lblReceiveVon.Text = text.Substring(text.IndexOf('.') - 2, 5).Replace('.', ',');

                    Z119.ATK.Common.Const.VON_RA = Double.Parse(lblReceiveVon.Text).ToString();
                }

                else if (n == 2)
                {
                    string text = "000" + Double.Parse(input.Replace('.', ',')) + "000";
                    if (text.IndexOf(',') < 0)
                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                    if (text.IndexOf(',') > 0)
                        lblReceiveAmpe.Text = text.Substring(text.IndexOf(',') - 2, 6).Replace(',', '.');

                    Z119.ATK.Common.Const.AMPE_RA = lblReceiveAmpe.Text;
                }
                else 
                {
                    string text = "000" + Double.Parse(input.Replace('.', ',')) + "000";
                    if (text.IndexOf(',') < 0)
                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                    if (text.IndexOf(',') > 0)
                        lblReceiveWoat.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                    n = 0;
                }
                
            }
            catch (Exception ex)
            {
                return;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            try
            {
                label1.Text = "Trạng thái kết nối: " + serialPort1.PortName + " "+serialPort1.IsOpen.ToString();
                
                if (Const.proConf.COM_loadCtrl != serialPort1.PortName)
                {
                    if(serialPort1.IsOpen)serialPort1.Close();
                    if (Const.proConf.COM_loadCtrl == null) Const.proConf.COM_loadCtrl = "COM3";
                    serialPort1.PortName = Const.proConf.COM_loadCtrl;
                    return;
                    
                }
                else if (!serialPort1.IsOpen)
                {
                    serialPort1.PortName = Const.proConf.COM_loadCtrl;
                    ConnectCOMPort();
                    //serialPort1.Open();
                }
                serialPort1.WriteLine(Z119.ATK.Common.Const.RECEIVE_VON_TAI);
                serialPort1.WriteLine(Z119.ATK.Common.Const.RECEIVE_AMPE_TAI);
                serialPort1.WriteLine(Z119.ATK.Common.Const.RECEIVE_WOAT_TAI);
                
            }
            catch (Exception)
            { }
            
        }

        private void fTaiForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            OffLoad();
        }
        public void OffLoad()
        {
            timer1.Enabled = false;
            try
            {
                serialPort1.WriteLine(Z119.ATK.Common.Const.LOAD_OFFTAI);
            }
            catch (Exception)
            { }

            IsOn = false;
        }
        private void lưuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
            
        }

        public void SaveData()
        {
            LoadBindingModel model = new LoadBindingModel();
            ConvertDataFromControlToModel(model);
            _loadmanager.SaveIntoFile(model);
        }

        public void ConvertDataFromControlToModel(LoadBindingModel model)
        {
            model.CCAValue = txbAValue.Text;
            model.CCBValue = txbBValue.Text;
        }

        private void mởToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData();
            
        }

        public void ConvertDataFromModelToControls(LoadBindingModel model)
        {
            txbAValue.Text = model.CCAValue;
            txbBValue.Text = model.CCBValue;
        }

        public bool isRunServer = false;
        public void RunFromServer()
        {
            if (isRunServer)
            {
                IsOn = true;
                OnOffPower();
                isRunServer = false;
            }
            else
            {
                IsOn = false;
                OnOffPower();
                isRunServer = true;

            }
        }

        private void thuNhỏToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Text == "Thu nhỏ")
            {
                this.Height = 385;
                (sender as ToolStripMenuItem).Text = "Mở rộng";
            }
            else
            {
                this.Height = 560;
                (sender as ToolStripMenuItem).Text = "Thu nhỏ";
            }
        }

        internal void LoadData()
        {
            LoadBindingModel model = _loadmanager.OpenFile();
            if (model != null)
                ConvertDataFromModelToControls(model);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Write(textBox1.Text);
        }

        
    }
}
