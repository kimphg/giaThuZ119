using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.VisaNS;

namespace Z119.ATK.Shell
{
    public partial class fPowerForm : Form
    {
        public Z119.ATK.Power.ManagementPower _powerManager { get; set; }
        public fPowerForm()
        {
            InitializeComponent();

            Initialize();
            ConnectionBothAdress();
            TurnOnRangForPower1();
        }

        #region Methods

        public void Initialize()
        {
            _powerManager = new Power.ManagementPower();
            // Nguon 1

            btnOn_OffPower1.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
            btnOn_OffPower1.BackgroundImageLayout = ImageLayout.Stretch;

            // Nguon2 - 3 - 4

            // Nguuồn 2 (Nguồn 2 kênh 1)
            btnOn_OffPower2.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
            btnOn_OffPower2.BackgroundImageLayout = ImageLayout.Stretch;


            // Nguồn 3 (Nguồn 2 kênh 2)
            btnOn_OffPower3.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
            btnOn_OffPower3.BackgroundImageLayout = ImageLayout.Stretch;

            // Nguồn 4 (Nguồn 2 kênh 3)
            btnOn_OffPower4.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
            btnOn_OffPower4.BackgroundImageLayout = ImageLayout.Stretch;
        }

        public void ConnectionBothAdress()
        {

            if (!_powerManager.ConnectionFromAddress1())
            {
                btnOn_OffPower1.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ERROR);
                btnOn_OffPower1.Enabled = false;
            }
                
            if (!_powerManager.ConnectionFromAddress2())
            {
                btnOn_OffPower2.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ERROR);
                btnOn_OffPower3.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ERROR);
                btnOn_OffPower4.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ERROR);

                btnOn_OffPower2.Enabled = false;
                btnOn_OffPower3.Enabled = false;
                btnOn_OffPower4.Enabled = false;
            }
            
        }

        public void TurnOnRangForPower1()
        {
            if (_powerManager.PowerControlAddress1.StateRangForPower1 == Power.StateRang.RANG1)
                btnPower1Rang1_Click(null, null);
            else
                btnPower1Rang2_Click(null, null);
        }

        #endregion

        #region Controls power
        private void btnOn_OffPower1_Click(object sender, EventArgs e)
        {
            if (_powerManager.PowerControlAddress1.StateChanel1 == Power.State.OFF)
            {
                string von = nmVonPower1.Value.ToString().Replace(',', '.');
                string ampe = nmAmpePower1.Value.ToString().Replace(',', '.');
                _powerManager.PowerControlAddress1.OnChanel(1);
                _powerManager.PowerControlAddress1.StateChanel1 = Power.State.ON;
                btnOn_OffPower1.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ON);
                _powerManager.PowerControlAddress1.SendPower(von, ampe, 1);
            }
            else
            {
                _powerManager.PowerControlAddress1.OffChanel(1);
                _powerManager.PowerControlAddress1.StateChanel1 = Power.State.OFF;
                btnOn_OffPower1.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
            }

            bool isKill = false;

             Task.Run(() =>
             {
                 while (true)
                 {
                     try
                     {
                         this.Invoke(new MethodInvoker(() =>
                         {
                             try
                             {
                                 _powerManager.PowerControlAddress1.PowerControl.Write(Z119.ATK.Common.Const.READVON + " CH1");
                                 txbReceiseVonPower1.Text = _powerManager.PowerControlAddress1.PowerControl.ReadString();

                                 _powerManager.PowerControlAddress1.PowerControl.Write(Z119.ATK.Common.Const.READAMPE + " CH1");
                                 txbReceiseAmpePower1.Text = Math.Round(Double.Parse(_powerManager.PowerControlAddress1.PowerControl.ReadString())/100, 2, MidpointRounding.AwayFromZero).ToString();
                             }
                             catch (Exception)
                             {
                                 MessageBox.Show("Lỗi kết nối nguồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                 isKill = true;
                                 
                             }
                         }));
                     }
                     catch (Exception)
                     {
                         MessageBox.Show("............!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                         isKill = true;
                     }
                     

                     Thread.Sleep(100);
                     if (isKill)
                         break;
                 }
             });
            
        }


        private void btnPower1Rang1_Click(object sender, EventArgs e)
        {
            try
            {
                _powerManager.PowerControlAddress1.ChangedRangeForPower1(1);
                _powerManager.PowerControlAddress1.StateRangForPower1 = Power.StateRang.RANG1;
                btnPower1Rang1.BackColor = Color.Lime;
                btnPower1Rang2.BackColor = Color.Gray;
                

                lblUmaxPower1.Text = "Umax = 20 V";
                lblImaxPower1.Text = "Imax = 10 A";

                nmVonPower1.Maximum = 20;
                nmAmpePower1.Maximum = 10;
            }
            catch (Exception)
            {
            }
            
        }

        private void btnPower1Rang2_Click(object sender, EventArgs e)
        {
            try
            {
                _powerManager.PowerControlAddress1.ChangedRangeForPower1(2);
                _powerManager.PowerControlAddress1.StateRangForPower1 = Power.StateRang.RANG2;
                btnPower1Rang1.BackColor = Color.Gray;
                btnPower1Rang2.BackColor = Color.Lime;
                
                lblUmaxPower1.Text = "Umax = 40 V";
                lblImaxPower1.Text = "Imax = 5 A";

                nmVonPower1.Maximum = 40;
                nmAmpePower1.Maximum = 5;
            }
            catch (Exception)
            {
            }
            
        }


        private void btnOn_OffPower2_Click(object sender, EventArgs e)
        {
            if (_powerManager.PowerControlAddress2.StateChanel1 == Power.State.OFF)
            {
                string von = nmVonPower2.Value.ToString();
                string ampe = nmAmpePower2.Value.ToString().Replace(',', '.');
                _powerManager.PowerControlAddress2.OnChanel(1);
                _powerManager.PowerControlAddress2.StateChanel1 = Power.State.ON;
                btnOn_OffPower2.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ON);
                _powerManager.PowerControlAddress2.SendPower(von, ampe, 2);
            }
            else
            {
                _powerManager.PowerControlAddress2.OffChanel(1);
                _powerManager.PowerControlAddress2.StateChanel1 = Power.State.OFF;
                btnOn_OffPower2.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
            }

            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        
                        this.Invoke(new MethodInvoker(() =>
                        {
                            try
                            {
                                _powerManager.PowerControlAddress2.PowerControl.Write(Z119.ATK.Common.Const.READVON + " CH1");
                                txbReceiseVonPower2.Text = _powerManager.PowerControlAddress2.PowerControl.ReadString();

                                _powerManager.PowerControlAddress2.PowerControl.Write(Z119.ATK.Common.Const.READAMPE + " CH1");
                                txbReceiseAmpePower2.Text = Math.Round(Double.Parse(_powerManager.PowerControlAddress2.PowerControl.ReadString()), 2, MidpointRounding.AwayFromZero).ToString();

                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Lỗi kết nối nguồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            
                        }));
                    }
                    catch (Exception)
                    {

                    }


                    Thread.Sleep(100);
                }
            });
        }

        private void btnOn_OffPower3_Click(object sender, EventArgs e)
        {
            if (_powerManager.PowerControlAddress2.StateChanel2 == Power.State.OFF)
            {
                string von = nmVonPower3.Value.ToString();
                string ampe = nmAmpePower3.Value.ToString().Replace(',', '.');
                _powerManager.PowerControlAddress2.OnChanel(2);
                _powerManager.PowerControlAddress2.StateChanel2 = Power.State.ON;
                btnOn_OffPower3.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ON);
                _powerManager.PowerControlAddress2.SendPower(von, ampe, 3);
            }
            else
            {
                _powerManager.PowerControlAddress2.OffChanel(2);
                _powerManager.PowerControlAddress2.StateChanel2 = Power.State.OFF;
                btnOn_OffPower3.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
            }

            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            try
                            {
                                _powerManager.PowerControlAddress2.PowerControl.Write(Z119.ATK.Common.Const.READVON + " CH2");
                                txbReceiseVonPower3.Text = _powerManager.PowerControlAddress2.PowerControl.ReadString();

                                _powerManager.PowerControlAddress2.PowerControl.Write(Z119.ATK.Common.Const.READAMPE + " CH2");
                                txbReceiseAmpePower3.Text = Math.Round(Double.Parse(_powerManager.PowerControlAddress2.PowerControl.ReadString()), 2, MidpointRounding.AwayFromZero).ToString();

                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Lỗi kết nối nguồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            
                        }));
                    }
                    catch (Exception)
                    {

                    }


                    Thread.Sleep(100);
                }
            });
        }

        private void btnOn_OffPower4_Click(object sender, EventArgs e)
        {
            if (_powerManager.PowerControlAddress2.StateChanel3 == Power.State.OFF)
            {
                string von = nmVonPower4.Value.ToString();
                string ampe = nmAmpePower4.Value.ToString().Replace(',', '.');
                _powerManager.PowerControlAddress2.OnChanel(3);
                _powerManager.PowerControlAddress2.StateChanel3 = Power.State.ON;
                btnOn_OffPower4.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ON);
                _powerManager.PowerControlAddress2.SendPower(von, ampe, 4);
                //_powerManager.PowerControlAddress2.PowerControl.Write(":APPL CH3,5,2");
            }
            else
            {
                _powerManager.PowerControlAddress2.OffChanel(3);
                _powerManager.PowerControlAddress2.StateChanel3 = Power.State.OFF;
                btnOn_OffPower4.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
            }

            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        this.Invoke(new MethodInvoker(() =>
                        {
                            try
                            {
                                _powerManager.PowerControlAddress2.PowerControl.Write(Z119.ATK.Common.Const.READVON + " CH3");
                                txbReceiseVonPower4.Text = _powerManager.PowerControlAddress2.PowerControl.ReadString();

                                _powerManager.PowerControlAddress2.PowerControl.Write(Z119.ATK.Common.Const.READAMPE + " CH3");
                                txbReceiseAmpePower4.Text = Math.Round(Double.Parse(_powerManager.PowerControlAddress2.PowerControl.ReadString()), 2, MidpointRounding.AwayFromZero).ToString();

                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Lỗi kết nối nguồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                           
                        }));
                    }
                    catch (Exception)
                    {

                    }


                    Thread.Sleep(100);
                }
            });
        }

        private void nmVonPower1_ValueChanged(object sender, EventArgs e)
        {
            if (_powerManager.PowerControlAddress1.StateChanel1 == Power.State.OFF)
                return;

            string von = nmVonPower1.Value.ToString();
            string ampe = nmAmpePower1.Value.ToString().Replace(',', '.');
            _powerManager.PowerControlAddress1.SendPower(von, ampe, 1);
        }

        private void nmAmpePower1_ValueChanged(object sender, EventArgs e)
        {
            if (_powerManager.PowerControlAddress1.StateChanel1 == Power.State.OFF)
                return;

            string von = nmVonPower1.Value.ToString();
            string ampe = nmAmpePower1.Value.ToString().Replace(',', '.');
            _powerManager.PowerControlAddress1.SendPower(von, ampe, 1);
        }

        private void nmVonPower2_ValueChanged(object sender, EventArgs e)
        {
            if (_powerManager.PowerControlAddress2.StateChanel1 == Power.State.OFF)
                return;

            string von = nmVonPower2.Value.ToString();
            string ampe = nmAmpePower2.Value.ToString().Replace(',', '.');
            _powerManager.PowerControlAddress2.SendPower(von, ampe, 2);
        }

        private void nmAmpePower2_ValueChanged(object sender, EventArgs e)
        {
            if (_powerManager.PowerControlAddress2.StateChanel1 == Power.State.OFF)
                return;

            string von = nmVonPower2.Value.ToString();
            string ampe = nmAmpePower2.Value.ToString().Replace(',', '.');
            _powerManager.PowerControlAddress2.SendPower(von, ampe, 2);
        }

        private void nmVonPower3_ValueChanged(object sender, EventArgs e)
        {
            if (_powerManager.PowerControlAddress2.StateChanel2 == Power.State.OFF)
                return;

            string von = nmVonPower3.Value.ToString();
            string ampe = nmAmpePower3.Value.ToString().Replace(',', '.');
            _powerManager.PowerControlAddress2.SendPower(von, ampe, 3);
        }

        private void nmAmpePower3_ValueChanged(object sender, EventArgs e)
        {
            if (_powerManager.PowerControlAddress2.StateChanel2 == Power.State.OFF)
                return;

            string von = nmVonPower3.Value.ToString();
            string ampe = nmAmpePower3.Value.ToString().Replace(',', '.');
            _powerManager.PowerControlAddress2.SendPower(von, ampe, 3);
        }

        private void nmVonPower4_ValueChanged(object sender, EventArgs e)
        {
            if (_powerManager.PowerControlAddress2.StateChanel3 == Power.State.OFF)
                return;

            string von = nmVonPower4.Value.ToString();
            string ampe = nmAmpePower4.Value.ToString().Replace(',', '.');
            _powerManager.PowerControlAddress2.SendPower(von, ampe, 4);
        }

        private void nmAmpePower4_ValueChanged(object sender, EventArgs e)
        {
            if (_powerManager.PowerControlAddress2.StateChanel3 == Power.State.OFF)
                return;

            string von = nmVonPower4.Value.ToString();
            string ampe = nmAmpePower4.Value.ToString().Replace(',', '.');
            _powerManager.PowerControlAddress2.SendPower(von, ampe, 4);
        }

        private void fPowerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _powerManager.PowerControlAddress1.OffChanel(1);
            _powerManager.PowerControlAddress2.OffChanel(1);
            _powerManager.PowerControlAddress2.OffChanel(2);
            _powerManager.PowerControlAddress2.OffChanel(3);
            _powerManager.Disconnection(1);
            _powerManager.Disconnection(2);
            _powerManager.Disconnection(3);
            _powerManager.Disconnection(4);
        }

        #endregion End Control power



        // =========================================================================================================

        #region FILE


       /// <summary>
       /// Đưa dữ liệu từ controls vào model
       /// </summary>
        public void LoadDataIntoModelFromControls(Z119.ATK.Model.BindingModel.PowerBindingModel model)
        {
            
            

        }

        /// <summary>
        /// Đưa dữ liệu từ model vào controls
        /// </summary>
        public void LoadDataIntoControlsFromModel(Z119.ATK.Model.BindingModel.PowerBindingModel model)
        {
            
        }

        public void SaveData()
        {
            Z119.ATK.Model.BindingModel.PowerBindingModel model = new Model.BindingModel.PowerBindingModel();
            LoadDataIntoModelFromControls(model);
            _powerManager.SaveIntoFile(model);
        }

        public void LoadData()
        {
            Z119.ATK.Model.BindingModel.PowerBindingModel model = new Model.BindingModel.PowerBindingModel();
            model = _powerManager.OpenFile();
            if (model != null)
                LoadDataIntoControlsFromModel(model);
        }


        private void tsMenuFileOpen_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void tsMenuFileSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        #endregion

        private void tsMenuTurnONOFF_Click(object sender, EventArgs e)
        {
            if (tsMenuTurnONOFF.Text == "Bật")
            {
                if (nmAmpePower1.Value != 0 && nmVonPower1.Value != 0)
                {
                    btnOn_OffPower1_Click(null, null);
                    tsMenuTurnONOFF.Text = "Tắt";
                }


                if (nmAmpePower2.Value != 0 && nmVonPower2.Value != 0)
                {
                    btnOn_OffPower2_Click(null, null);
                    tsMenuTurnONOFF.Text = "Tắt";
                }


                if (nmAmpePower3.Value != 0 && nmVonPower3.Value != 0)
                {
                    btnOn_OffPower3_Click(null, null);
                    tsMenuTurnONOFF.Text = "Tắt";
                }


                if (nmAmpePower4.Value != 0 && nmVonPower4.Value != 0)
                {
                    btnOn_OffPower4_Click(null, null);
                    tsMenuTurnONOFF.Text = "Tắt";
                }
            }
            else
            {
                _powerManager.PowerControlAddress1.StateChanel1 = Power.State.ON;
                _powerManager.PowerControlAddress2.StateChanel1 = Power.State.ON;
                _powerManager.PowerControlAddress2.StateChanel2 = Power.State.ON;
                _powerManager.PowerControlAddress2.StateChanel3 = Power.State.ON;

                btnOn_OffPower1_Click(null, null);
                btnOn_OffPower2_Click(null, null);
                btnOn_OffPower3_Click(null, null);
                btnOn_OffPower4_Click(null, null);
                tsMenuTurnONOFF.Text = "Bật";
            }                

        }
    }
}
