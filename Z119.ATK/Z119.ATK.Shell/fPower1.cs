using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z119.ATK.Model.BindingModel;

namespace Z119.ATK.Shell
{
    public partial class fPower1 : Form
    {
        public StringBuilder strResult1 = new StringBuilder(); // Use as Buffer for Power Address 2
        public StringBuilder strResult2 = new StringBuilder(); // Use as Buffer for Power Address 1

        public int PowerSelected = 0; // Use for Power address 2
        public int Range = 0; // Use for power address 1
        public Z119.ATK.Power.ManagementPower _powerManager { get; set; }

        bool RunningTask1 = false;
        bool RunningTask2 = false;
        bool RunningTask3 = false;
        bool RunningTask4 = false;

        private event EventHandler _runRequiredFromServer;
        public event EventHandler RunRequiredFromServer
        {
            add { _runRequiredFromServer += value; }
            remove { _runRequiredFromServer -= value; }
        }

        public fPower1()
        {
            InitializeComponent();

            _powerManager = new Power.ManagementPower();

            Initialize();

            Innitialize1();
            btnSelectedPower1.BackColor =  Color.FromArgb(189, 227, 114);
            ChoosePower("NG2");
            if (_runRequiredFromServer != null)
                _runRequiredFromServer(this, null);
        }


        #region Power Address 2 **********************************************************

        #region Methods ==============================================

        public void ResetValuesPower1()
        {
            lblVonSetPower4.Text = "00.00";
            lblAmpeSetPower4.Text = "05.00";
            lblVonLimitPower4.Text = "22.00";
            lblAmpeLimitPower4.Text = "11.00";
            lblStatusRangPower4.Text = "Dải 1";
            lblDescriptionVonMaxPower4.Text = "20V";
            lblDescriptionAmpeMaxPower4.Text = "10A";

            _powerManager.PowerControlAddress1.ChangedRangeForPower1(1);
            _powerManager.PowerControlAddress1.StateRangForPower1 = Power.StateRang.RANG1;
            
            //_powerManager.PowerControlAddress1.DefaultReset();
        }

        public void ResertValuesPower2()
        {
            #region GUI ***************************
            lblVonSetPower1.Text = "00.00";
            lblAmpeSetPower1.Text = "03.00";
            lblVonLimitPower1.Text = "33.00";
            lblAmpeLimitPower1.Text = "03.30";
            lblVonSetPower2.Text = "00.00";
            lblAmpeSetPower2.Text = "03.00";
            lblVonLimitPower2.Text = "33.00";
            lblAmpeLimitPower2.Text = "03.30";
            lblVonSetPower3.Text = "00.00";
            lblAmpeSetPower3.Text = "03.00";
            lblVonLimitPower3.Text = "05.00";
            lblAmpeLimitPower3.Text = "03.00";
            #endregion EndGUI *************************

            _powerManager.PowerControlAddress2.DefaultReset();
        }

        /// <summary>
        /// Khởi tạo ban đầu
        /// </summary>
        public void Initialize()
        {
            pnlResultPowerAddress2.Visible = false;
            //panel3.BackColor = Color.FromArgb(186, 192, 190);
            btnOnOffPowerAll.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
            btnOnOffPowerAll.BackgroundImageLayout = ImageLayout.Stretch;
            {
                #region GUI ***********************

                btnOnOffPower1.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                btnOnOffPower1.BackgroundImageLayout = ImageLayout.Stretch;

                lblDescriptionNameOfPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblStatusPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpePower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionWoatPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionSetPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonSetPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeSetPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionLimitPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonLimitPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeLimitPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonMaxPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeMaxPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblRegion1Power1.BackColor = Color.FromArgb(43, 63, 54);
                lblRegion2Power1.BackColor = Color.FromArgb(43, 63, 54);
                lblRegion3Power1.BackColor = Color.FromArgb(43, 63, 54);

                lblStatusPower1.Text = "OFF";

                lblVonReceivePower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeReceivePower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblWoatReceivePower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonSetPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeSetPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonLimitPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeLimitPower1.ForeColor = Color.FromArgb(43, 63, 54);

                //lblVonSetPower1.Visible = true;
                //lblAmpeLimitPower4.Visible = true;

                IsOnPower1 = false;

                #endregion End GUI ***********************

                #region GUI *************************

                btnOnOffPower2.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                btnOnOffPower2.BackgroundImageLayout = ImageLayout.Stretch;

                lblDescriptionNameOfPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblStatusPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpePower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionWoatPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionSetPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonSetPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeSetPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionLimitPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonLimitPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeLimitPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonMaxPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeMaxPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblRegion1Power2.BackColor = Color.FromArgb(43, 63, 54);
                lblRegion2Power2.BackColor = Color.FromArgb(43, 63, 54);
                lblRegion3Power2.BackColor = Color.FromArgb(43, 63, 54);

                lblStatusPower2.Text = "OFF";

                lblVonReceivePower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeReceivePower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblWoatReceivePower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonSetPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeSetPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonLimitPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeLimitPower2.ForeColor = Color.FromArgb(43, 63, 54);

                IsOnPower2 = false;

                #endregion End GUI *************************

                #region GUI *********************

                btnOnOffPower3.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                btnOnOffPower3.BackgroundImageLayout = ImageLayout.Stretch;

                lblDescriptionNameOfPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblStatusPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpePower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionWoatPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionSetPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonSetPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeSetPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionLimitPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonLimitPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeLimitPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonMaxPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeMaxPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblRegion1Power3.BackColor = Color.FromArgb(43, 63, 54);
                lblRegion2Power3.BackColor = Color.FromArgb(43, 63, 54);
                lblRegion3Power3.BackColor = Color.FromArgb(43, 63, 54);

                lblStatusPower3.Text = "OFF";

                lblVonReceivePower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeReceivePower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblWoatReceivePower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonSetPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeSetPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonLimitPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeLimitPower3.ForeColor = Color.FromArgb(43, 63, 54);

                IsOnPower3 = false;

                #endregion EndGUI *********************

                #region GUI ********************************

                btnOnOffPower4.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                btnOnOffPower4.BackgroundImageLayout = ImageLayout.Stretch;

                lblDescriptionNameOfPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblStatusPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpePower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionWoatPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionSetPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonSetPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeSetPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionLimitPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonLimitPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeLimitPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonMaxPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeMaxPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblRegion1Power4.BackColor = Color.FromArgb(43, 63, 54);
                lblStatusRangPower4.ForeColor = Color.FromArgb(43, 63, 54);

                lblStatusPower4.Text = "OFF";

                lblVonReceivePower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeReceivePower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblWoatReceivePower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonSetPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeSetPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonLimitPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeLimitPower4.ForeColor = Color.FromArgb(43, 63, 54);

                IsOnPower4 = false;
                #endregion End GUI ********************************
            }


            {
                #region GUI *********************
                btnVonLimitPower1.BackColor = Color.White;
                lblStatusLimitVonPower1.Text = "OFF";
                lblStatusLimitVonPower1.ForeColor = Color.FromArgb(43, 63, 54);
                IsOnOffLimitVonPower1 = false;
                #endregion EndGUI *************************

                #region GUI *******************
                btnAmpeLimitPower1.BackColor = Color.White;
                lblStatusLimitAmpePower1.Text = "OFF";
                lblStatusLimitAmpePower1.ForeColor = Color.FromArgb(43, 63, 54);
                IsOnOffLimitAmpePower1 = false;
                #endregion EndGUI **************************

                #region GUI ************************
                btnVonLimitPower2.BackColor = Color.White;

                lblStatusLimitVonPower2.Text = "OFF";
                lblStatusLimitVonPower2.ForeColor = Color.FromArgb(43, 63, 54);
                IsOnOffLimitVonPower2 = false;
                #endregion EndGUI *****************************

                #region GUI ******************
                btnAmpeLimitPower2.BackColor = Color.White;
                lblStatusLimitAmpePower2.Text = "OFF";
                lblStatusLimitAmpePower2.ForeColor = Color.FromArgb(43, 63, 54);
                IsOnOffLimitAmpePower2 = false;
                #endregion EndGUI ***************************

                #region GUI **********************
                btnVonLimitPower3.BackColor = Color.White;

                lblStatusLimitVonPower3.Text = "OFF";
                lblStatusLimitVonPower3.ForeColor = Color.FromArgb(43, 63, 54);
                IsOnOffLimitVonPower3 = false;
                #endregion EndGUI *****************************

                #region GUI ***************
                btnAmpeLimitPower3.BackColor = Color.White;
                lblStatusLimitAmpePower3.Text = "OFF";
                lblStatusLimitAmpePower3.ForeColor = Color.FromArgb(43, 63, 54);
                IsOnOffLimitAmpePower3 = false;
                #endregion EndGUI ************************

                #region GUI **************
                btnVonLimitPower4.BackColor = Color.White;

                lblStatusLimitVonPower4.Text = "OFF";
                lblStatusLimitVonPower4.ForeColor = Color.FromArgb(43, 63, 54);
                IsOnOffLimitVonPower4 = false;
                #endregion EndGUI ******************

                #region GUI ********************
                btnAmpeLimitPower4.BackColor = Color.White;

                lblStatusLimitAmpePower4.Text = "OFF";
                lblStatusLimitAmpePower4.ForeColor = Color.FromArgb(43, 63, 54);
                IsOnOffLimitAmpePower4 = false;
                #endregion EndGUI ************************
            }

            ConnectionToAddress2();
        }

        /// <summary>
        /// Kiểm tra kết nối tới nguồn nếu kết nối không thành công thì đóng Button kết nối lại
        /// </summary>
        public void ConnectionToAddress2()
        {

            // Kết nối không thành công thì sẽ đóng button OnOFF Nguồn tới địa chỉ 2
            if (!_powerManager.ConnectionFromAddress2())
            {
                btnOnOffPower1.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ERROR);
                btnOnOffPower2.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ERROR);
                btnOnOffPower3.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ERROR);
                btnOnOffPowerAll.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ERROR);

                btnOnOffPower1.Enabled = false;
                btnOnOffPower2.Enabled = false;
                btnOnOffPower3.Enabled = false;
                btnOnOffPowerAll.Enabled = false;
            }

        }

        #region On or Off powwer *********************************

        Boolean IsOnPower1 = false;
        Boolean IsOnPower2 = false;
        Boolean IsOnPower3 = false;
        Boolean IsOnPowerAll = false;

        public void OnOffPower1()
        {
            if (IsOnPower1)
            {
                #region Handling ***********************
                try
                {
                    _powerManager.PowerControlAddress2.OffChanel(1);
                    _powerManager.PowerControlAddress2.StateChanel1 = Power.State.OFF;
                }
                catch (Exception)
                {
                    
                }

                #endregion EndHandling ***********************

                #region GUI ***********************

                btnOnOffPower1.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                btnOnOffPower1.BackgroundImageLayout = ImageLayout.Stretch;

                lblDescriptionNameOfPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblStatusPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpePower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionWoatPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionSetPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonSetPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeSetPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionLimitPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonLimitPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeLimitPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonMaxPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeMaxPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblRegion1Power1.BackColor = Color.FromArgb(43, 63, 54);
                lblRegion2Power1.BackColor = Color.FromArgb(43, 63, 54);
                lblRegion3Power1.BackColor = Color.FromArgb(43, 63, 54);

                lblStatusPower1.Text = "OFF";

                lblVonReceivePower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeReceivePower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblWoatReceivePower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonSetPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeSetPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonLimitPower1.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeLimitPower1.ForeColor = Color.FromArgb(43, 63, 54);

                IsOnPower1 = false;

                #endregion End GUI ***********************
            }

            else
            {
                #region Handling ***********************

                String vonLimit = lblVonLimitPower1.Text;
                String ampeLimit = lblAmpeLimitPower1.Text;
                _powerManager.PowerControlAddress2.SendLimit(vonLimit, ampeLimit, 2, 1);
                _powerManager.PowerControlAddress2.SendLimit(vonLimit, ampeLimit, 2, 2);

                String von = lblVonSetPower1.Text;
                String ampe = lblAmpeSetPower1.Text;
                _powerManager.PowerControlAddress2.OnChanel(1);
                _powerManager.PowerControlAddress2.StateChanel1 = Power.State.ON;
                _powerManager.PowerControlAddress2.SendPower(von, ampe, 2);

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
                                    _powerManager.PowerControlAddress2.PowerControl.Write(Z119.ATK.Common.Const.READALL + " CH1");

                                    string result = _powerManager.PowerControlAddress2.PowerControl.ReadString();

                                    string[] values = result.Split(',');
                                    if (Double.Parse(values[0].Replace('.', ',')) < 100)
                                    {
                                        string text = "000" + Double.Parse(values[0].Replace('.', ',')) + "000";
                                        if (text.IndexOf(',') < 0)
                                            text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                        if (text.IndexOf(',') > 0)
                                            lblVonReceivePower1.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');
                                    }
                                    if (Double.Parse(values[1].Replace('.', ',')) < 100)
                                    {
                                        string text = "000" + Double.Parse(values[1].Replace('.', ',')) + "000";
                                        if (text.IndexOf(',') < 0)
                                            text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                        if (text.IndexOf(',') > 0)
                                            lblAmpeReceivePower1.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');
                                        double amp = Double.Parse(values[1].Replace('.', ','));

                                        string maxAmp = lblAmpeLimitPower1.Text.Replace("A", "");

                                        maxAmp = maxAmp.Replace('.', ',');
                                        double ampMax = Double.Parse(maxAmp);
                                        if (amp > ampMax)
                                        {
                                            OffPowerAll();
                                            MessageBox.Show("Tắt nguồn do dòng điện quá mức cho phép.");
                                            return;
                                        }
                                    }
                                    if (Double.Parse(values[2].Replace('.', ',')) < 100)
                                    {
                                        string text = "000" + Double.Parse(values[2].Replace('.', ',')) + "000";
                                        if (text.IndexOf(',') < 0)
                                            text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                        if (text.IndexOf(',') > 0)
                                            lblWoatReceivePower1.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');
                                    }

                                    RunningTask1 = true;
                                }
                                catch (Exception)
                                {
                                    RunningTask1 = false;
                                    //MessageBox.Show("Lỗi kết nối nguồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }));

                            Thread.Sleep(100);
                            if (!RunningTask1)
                                break;
                        }
                        catch (Exception)
                        {
                            Thread.Sleep(100);
                            //MessageBox.Show("Lỗi kết nối nguồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                });

                    
                #endregion EndHandling ***********************

                #region GUI ***********************

                btnOnOffPower1.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ON);
                btnOnOffPower1.BackgroundImageLayout = ImageLayout.Stretch;

                lblDescriptionNameOfPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblStatusPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpePower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionWoatPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionSetPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonSetPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpeSetPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionLimitPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonLimitPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpeLimitPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonMaxPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpeMaxPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblRegion1Power1.BackColor = Color.FromArgb(0, 255, 0);
                lblRegion2Power1.BackColor = Color.FromArgb(0, 255, 0);
                lblRegion3Power1.BackColor = Color.FromArgb(0, 255, 0);

                lblStatusPower1.Text = "ON";

                lblVonReceivePower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblAmpeReceivePower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblWoatReceivePower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblVonSetPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblAmpeSetPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblVonLimitPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblAmpeLimitPower1.ForeColor = Color.FromArgb(0, 255, 0);
                IsOnPower1 = true;

                #endregion EndGUI ***********************
            }
        }

        public void OnOffPower2()
        {
            if (IsOnPower2)
            {
                #region Handling ************************
                try
                {
                    _powerManager.PowerControlAddress2.OffChanel(2);
                    _powerManager.PowerControlAddress2.StateChanel2 = Power.State.OFF;
                }
                catch (Exception)
                {
                    
                }
                

                #endregion EndHandling ************************

                #region GUI *************************

                btnOnOffPower2.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                btnOnOffPower2.BackgroundImageLayout = ImageLayout.Stretch;

                lblDescriptionNameOfPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblStatusPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpePower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionWoatPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionSetPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonSetPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeSetPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionLimitPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonLimitPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeLimitPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonMaxPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeMaxPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblRegion1Power2.BackColor = Color.FromArgb(43, 63, 54);
                lblRegion2Power2.BackColor = Color.FromArgb(43, 63, 54);
                lblRegion3Power2.BackColor = Color.FromArgb(43, 63, 54);

                lblStatusPower2.Text = "OFF";

                lblVonReceivePower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeReceivePower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblWoatReceivePower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonSetPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeSetPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonLimitPower2.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeLimitPower2.ForeColor = Color.FromArgb(43, 63, 54);

                IsOnPower2 = false;

                #endregion End GUI *************************
            }

            else
            {
                #region Handling *********************

                String vonLimit = lblVonLimitPower2.Text;
                String ampeLimit = lblAmpeLimitPower2.Text;
                _powerManager.PowerControlAddress2.SendLimit(vonLimit, ampeLimit, 3, 1);
                _powerManager.PowerControlAddress2.SendLimit(vonLimit, ampeLimit, 3, 2);

                String von = lblVonSetPower2.Text;
                String ampe = lblAmpeSetPower2.Text;
                _powerManager.PowerControlAddress2.OnChanel(2);
                _powerManager.PowerControlAddress2.StateChanel2 = Power.State.ON;
                _powerManager.PowerControlAddress2.SendPower(von, ampe, 3);


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
                                    _powerManager.PowerControlAddress2.PowerControl.Write(Z119.ATK.Common.Const.READALL + " CH2");

                                    string result = _powerManager.PowerControlAddress2.PowerControl.ReadString();

                                    string[] values = result.Split(',');
                                    if (Double.Parse(values[0].Replace('.', ',')) < 100)
                                    {
                                        string text = "000" + Double.Parse(values[0].Replace('.', ',')) + "000";
                                        if (text.IndexOf(',') < 0)
                                            text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                        if (text.IndexOf(',') > 0)
                                            lblVonReceivePower2.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');
                                    }
                                    if (Double.Parse(values[1].Replace('.', ',')) < 100)
                                    {
                                        string text = "000" + Double.Parse(values[1].Replace('.', ',')) + "000";
                                        if (text.IndexOf(',') < 0)
                                            text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                        if (text.IndexOf(',') > 0)
                                            lblAmpeReceivePower2.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');
                                        double amp = Double.Parse(values[1].Replace('.', ','));

                                        string maxAmp = lblAmpeLimitPower2.Text.Replace("A", "");

                                        maxAmp = maxAmp.Replace('.', ',');
                                        double ampMax = Double.Parse(maxAmp);
                                        if (amp > ampMax)
                                        {
                                            OffPowerAll();
                                            MessageBox.Show("Tắt nguồn do dòng điện quá mức cho phép.");
                                            return;
                                        }
                                    }
                                    if (Double.Parse(values[2].Replace('.', ',')) < 100)
                                    {
                                        string text = "000" + Double.Parse(values[2].Replace('.', ',')) + "000";
                                        if (text.IndexOf(',') < 0)
                                            text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                        if (text.IndexOf(',') > 0)
                                            lblWoatReceivePower2.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');
                                    }

                                    RunningTask2 = true;
                                }
                                catch (Exception)
                                {
                                    RunningTask2 = false;
                                    //MessageBox.Show("Lỗi kết nối nguồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }));

                            Thread.Sleep(100);
                            if (!RunningTask2)
                                break;
                        }
                        catch (Exception)
                        {
                            Thread.Sleep(100);
                            //MessageBox.Show("Lỗi kết nối nguồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                });

                #endregion EndHandling *********************

                #region GUI ***************************

                btnOnOffPower2.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ON);
                btnOnOffPower2.BackgroundImageLayout = ImageLayout.Stretch;

                lblDescriptionNameOfPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblStatusPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpePower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionWoatPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionSetPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonSetPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpeSetPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionLimitPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonLimitPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpeLimitPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonMaxPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpeMaxPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblRegion1Power2.BackColor = Color.FromArgb(0, 255, 0);
                lblRegion2Power2.BackColor = Color.FromArgb(0, 255, 0);
                lblRegion3Power2.BackColor = Color.FromArgb(0, 255, 0);

                lblStatusPower2.Text = "ON";

                lblVonReceivePower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblAmpeReceivePower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblWoatReceivePower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblVonSetPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblAmpeSetPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblVonLimitPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblAmpeLimitPower2.ForeColor = Color.FromArgb(0, 255, 0);

                IsOnPower2 = true;

                #endregion EndGUI *************************
            }
        }

        public void OnOffPower3()
        {
            if (IsOnPower3)
            {
                #region Handling *********************

                try
                {
                    _powerManager.PowerControlAddress2.OffChanel(3);
                    _powerManager.PowerControlAddress2.StateChanel3 = Power.State.OFF;
                }
                catch (Exception)
                {

                }
                

                #endregion EndHandling *********************

                #region GUI *********************

                btnOnOffPower3.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                btnOnOffPower3.BackgroundImageLayout = ImageLayout.Stretch;

                lblDescriptionNameOfPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblStatusPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpePower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionWoatPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionSetPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonSetPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeSetPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionLimitPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonLimitPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeLimitPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonMaxPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeMaxPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblRegion1Power3.BackColor = Color.FromArgb(43, 63, 54);
                lblRegion2Power3.BackColor = Color.FromArgb(43, 63, 54);
                lblRegion3Power3.BackColor = Color.FromArgb(43, 63, 54);

                lblStatusPower3.Text = "OFF";

                lblVonReceivePower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeReceivePower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblWoatReceivePower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonSetPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeSetPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonLimitPower3.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeLimitPower3.ForeColor = Color.FromArgb(43, 63, 54);

                IsOnPower3 = false;

                #endregion EndGUI *********************
            }

            else
            {
                #region Handling *********************

                String vonLimit = lblVonLimitPower3.Text;
                String ampeLimit = lblAmpeLimitPower3.Text;
                _powerManager.PowerControlAddress2.SendLimit(vonLimit, ampeLimit, 4, 1);
                _powerManager.PowerControlAddress2.SendLimit(vonLimit, ampeLimit, 4, 2);

                String von = lblVonSetPower3.Text;
                String ampe = lblAmpeSetPower3.Text;
                _powerManager.PowerControlAddress2.OnChanel(3);
                _powerManager.PowerControlAddress2.StateChanel3 = Power.State.ON;
                _powerManager.PowerControlAddress2.SendPower(von, ampe, 4);


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
                                    _powerManager.PowerControlAddress2.PowerControl.Write(Z119.ATK.Common.Const.READALL + " CH3");

                                    string result = _powerManager.PowerControlAddress2.PowerControl.ReadString();

                                    string[] values = result.Split(',');
                                    if (Double.Parse(values[0].Replace('.', ',')) < 100)
                                    {
                                        string text = "000" + Double.Parse(values[0].Replace('.', ',')) + "000";
                                        if (text.IndexOf(',') < 0)
                                            text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                        if (text.IndexOf(',') > 0)
                                            lblVonReceivePower3.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');
                                    }
                                    if (Double.Parse(values[1].Replace('.', ',')) < 100)
                                    {
                                        string text = "000" + Double.Parse(values[1].Replace('.', ',')) + "000";
                                        if (text.IndexOf(',') < 0)
                                            text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                        if (text.IndexOf(',') > 0)
                                            lblAmpeReceivePower3.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');
                                        double amp = Double.Parse(values[1].Replace('.', ','));

                                        string maxAmp = lblAmpeLimitPower3.Text.Replace("A", "");

                                        maxAmp = maxAmp.Replace('.', ',');
                                        double ampMax = Double.Parse(maxAmp);
                                        if (amp > ampMax)
                                        {
                                            OffPowerAll();
                                            MessageBox.Show("Tắt nguồn do dòng điện quá mức cho phép.");
                                            return;
                                        }
                                    }
                                    if (Double.Parse(values[2].Replace('.', ',')) < 100)
                                    {
                                        string text = "000" + Double.Parse(values[2].Replace('.', ',')) + "000";
                                        if (text.IndexOf(',') < 0)
                                            text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                        if (text.IndexOf(',') > 0)
                                            lblWoatReceivePower3.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');
                                    }

                                    RunningTask3 = true;
                                }
                                catch (Exception)
                                {
                                    RunningTask3 = false;
                                    //MessageBox.Show("Lỗi kết nối nguồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }));

                            Thread.Sleep(100);
                            if (!RunningTask3)
                                break;
                        }
                        catch (Exception)
                        {
                            Thread.Sleep(100);
                            //MessageBox.Show("Lỗi kết nối nguồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        
                    }
                });

                #endregion EndHandling *********************

                #region GUI **********************

                btnOnOffPower3.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ON);
                btnOnOffPower3.BackgroundImageLayout = ImageLayout.Stretch;

                lblDescriptionNameOfPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblStatusPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpePower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionWoatPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionSetPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonSetPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpeSetPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionLimitPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonLimitPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpeLimitPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonMaxPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpeMaxPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblRegion1Power3.BackColor = Color.FromArgb(0, 255, 0);
                lblRegion2Power3.BackColor = Color.FromArgb(0, 255, 0);
                lblRegion3Power3.BackColor = Color.FromArgb(0, 255, 0);

                lblStatusPower3.Text = "ON";

                lblVonReceivePower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblAmpeReceivePower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblWoatReceivePower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblVonSetPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblAmpeSetPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblVonLimitPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblAmpeLimitPower3.ForeColor = Color.FromArgb(0, 255, 0);

                IsOnPower3 = true;

                #endregion EndGUI ************************
            }
        }
        public void OffPowerAll()
        {

            try
            {
                _powerManager.PowerControlAddress1.OffChanel(1);
            }
            catch (Exception)
            { }

            try
            {
                _powerManager.PowerControlAddress2.OffChanel(1);
            }
            catch (Exception)
            { }

            try
            {
                _powerManager.PowerControlAddress2.OffChanel(2);
            }
            catch (Exception)
            { }

            try
            {
                _powerManager.PowerControlAddress2.OffChanel(3);
            }
            catch (Exception)
            { }

        }
        public void OnOffPowerAll()
        {
            if (IsOnPowerAll)
            {
                IsOnPower1 = false;
                IsOnPower2 = false;
                IsOnPower3 = false;
                IsOnPower4 = false;

                OnOffPower1();
                OnOffPower2();
                OnOffPower3();
                OnOffPower4();

                IsOnPowerAll = false;

                btnOnOffPowerAll.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ON);
                btnOnOffPowerAll.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                IsOnPower1 = true;
                IsOnPower2 = true;
                IsOnPower3 = true;
                IsOnPower4 = true;

                //if (!lblVonSetPower1.Text.Equals("00.00") || lblAmpeSetPower1.Text.Equals("00.00"))
                OnOffPower1();
                OnOffPower2();
                OnOffPower3();
                OnOffPower4();

                IsOnPowerAll = true;
                btnOnOffPowerAll.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                btnOnOffPowerAll.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        /// <summary>
        /// Selected power to set Von, Ampe, VonLimit and AmpeLimit
        /// </summary>
        public void ChoosePower(string chanel)
        {
            if (chanel.Equals("NG2"))
            {
                PowerSelected = 1;

                lblRegion2Power1.BackColor = Color.FromArgb(0, 255, 0);
                lblRegion3Power1.BackColor = Color.FromArgb(0, 255, 0);
                lblDescriptionSetPower1.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionLimitPower1.ForeColor = Color.FromArgb(0, 255, 0);

                lblLimitMinMaxPower.Text = "30V\n3A";

                if (!IsOnPower2) // if Power 2 is Off
                {
                    lblRegion2Power2.BackColor = Color.FromArgb(43, 63, 54);
                    lblRegion3Power2.BackColor = Color.FromArgb(43, 63, 54);
                    lblDescriptionSetPower2.ForeColor = Color.FromArgb(43, 63, 54);
                    lblDescriptionLimitPower2.ForeColor = Color.FromArgb(43, 63, 54);
                }

                if (!IsOnPower3) // if Power 3 is Off
                {
                    lblRegion2Power3.BackColor = Color.FromArgb(43, 63, 54);
                    lblRegion3Power3.BackColor = Color.FromArgb(43, 63, 54);
                    lblDescriptionSetPower3.ForeColor = Color.FromArgb(43, 63, 54);
                    lblDescriptionLimitPower3.ForeColor = Color.FromArgb(43, 63, 54);
                }
            }

            else if (chanel.Equals("NG3"))
            {
                PowerSelected = 2;

                lblRegion2Power2.BackColor = Color.FromArgb(0, 255, 0);
                lblRegion3Power2.BackColor = Color.FromArgb(0, 255, 0);
                lblDescriptionSetPower2.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionLimitPower2.ForeColor = Color.FromArgb(0, 255, 0);

                lblLimitMinMaxPower.Text = "30V\n3A";

                if (!IsOnPower1) // if Power 1 is Off
                {
                    lblRegion2Power1.BackColor = Color.FromArgb(43, 63, 54);
                    lblRegion3Power1.BackColor = Color.FromArgb(43, 63, 54);
                    lblDescriptionSetPower1.ForeColor = Color.FromArgb(43, 63, 54);
                    lblDescriptionLimitPower1.ForeColor = Color.FromArgb(43, 63, 54);
                }

                if (!IsOnPower3) // if Power 3 is Off
                {
                    lblRegion2Power3.BackColor = Color.FromArgb(43, 63, 54);
                    lblRegion3Power3.BackColor = Color.FromArgb(43, 63, 54);
                    lblDescriptionSetPower3.ForeColor = Color.FromArgb(43, 63, 54);
                    lblDescriptionLimitPower3.ForeColor = Color.FromArgb(43, 63, 54);
                }
            }
            else if (chanel.Equals("NG4"))
            {
                PowerSelected = 3;

                lblRegion2Power3.BackColor = Color.FromArgb(0, 255, 0);
                lblRegion3Power3.BackColor = Color.FromArgb(0, 255, 0);
                lblDescriptionSetPower3.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionLimitPower3.ForeColor = Color.FromArgb(0, 255, 0);

                lblLimitMinMaxPower.Text = "5V\n3A";

                if (!IsOnPower1) // if Power 2 is Off
                {
                    lblRegion2Power1.BackColor = Color.FromArgb(43, 63, 54);
                    lblRegion3Power1.BackColor = Color.FromArgb(43, 63, 54);
                    lblDescriptionSetPower1.ForeColor = Color.FromArgb(43, 63, 54);
                    lblDescriptionLimitPower1.ForeColor = Color.FromArgb(43, 63, 54);
                }

                if (!IsOnPower2) // if Power 3 is Off
                {
                    lblRegion2Power2.BackColor = Color.FromArgb(43, 63, 54);
                    lblRegion3Power2.BackColor = Color.FromArgb(43, 63, 54);
                    lblDescriptionSetPower2.ForeColor = Color.FromArgb(43, 63, 54);
                    lblDescriptionLimitPower2.ForeColor = Color.FromArgb(43, 63, 54);
                }
            }
        }

        Boolean IsOnOffLimitVonPower1 = false;
        Boolean IsOnOffLimitAmpePower1 = false;
        Boolean IsOnOffLimitVonPower2 = false;
        Boolean IsOnOffLimitAmpePower2 = false;
        Boolean IsOnOffLimitVonPower3 = false;
        Boolean IsOnOffLimitAmpePower3 = false;

        /// <summary>
        /// On Off Limit for Power
        /// </summary>
        /// <param name="text"></param>
        /// <param name="name"></param>
        public void OnOffLimit(string name)
        {

            // Power1 and Von
            if (name.Equals("btnVonLimitPower1"))
            {
                if (IsOnOffLimitVonPower1) // then OFF
                {
                    try
                    {
                        _powerManager.PowerControlAddress2.OffLimit(2, 2);
                    }
                    catch (Exception)
                    { }

                    #region GUI *********************
                    btnVonLimitPower1.BackColor = Color.White;
                    lblStatusLimitVonPower1.Text = "OFF";
                    lblStatusLimitVonPower1.ForeColor = Color.FromArgb(43, 63, 54);
                    IsOnOffLimitVonPower1 = false;
                    #endregion EndGUI *************************

                }
                else // then ON
                {
                    _powerManager.PowerControlAddress2.OnLimit(2, 2);

                    btnVonLimitPower1.BackColor = Color.FromArgb(189, 227, 114);

                    lblStatusLimitVonPower1.Text = "ON";
                    lblStatusLimitVonPower1.ForeColor = Color.FromArgb(0, 255, 0);
                    IsOnOffLimitVonPower1 = true;
                }
            }

            // Power1 and Ampe
            else if (name.Equals("btnAmpeLimitPower1"))
            {
                if (IsOnOffLimitAmpePower1) // then OFF
                {
                    try
                    {
                        _powerManager.PowerControlAddress2.OffLimit(2, 1);
                    }
                    catch (Exception)
                    { }

                    #region GUI *******************
                    btnAmpeLimitPower1.BackColor = Color.White;
                    lblStatusLimitAmpePower1.Text = "OFF";
                    lblStatusLimitAmpePower1.ForeColor = Color.FromArgb(43, 63, 54);
                    IsOnOffLimitAmpePower1 = false;
                    #endregion EndGUI **************************
                }
                else // then ON
                {
                    try
                    {
                        _powerManager.PowerControlAddress2.OnLimit(2, 1);
                    }
                    catch (Exception)
                    { }
                    

                    btnAmpeLimitPower1.BackColor = Color.FromArgb(189, 227, 114);

                    lblStatusLimitAmpePower1.Text = "ON";
                    lblStatusLimitAmpePower1.ForeColor = Color.FromArgb(0, 255, 0);
                    IsOnOffLimitAmpePower1 = true;
                }
            }

            // Power2 and Von
            if (name.Equals("btnVonLimitPower2"))
            {
                if (IsOnOffLimitVonPower2) // then OFF
                {
                    try
                    {
                        _powerManager.PowerControlAddress2.OffLimit(3, 2);
                    }
                    catch (Exception)
                    { }

                    #region GUI ************************
                    btnVonLimitPower2.BackColor = Color.White;

                    lblStatusLimitVonPower2.Text = "OFF";
                    lblStatusLimitVonPower2.ForeColor = Color.FromArgb(43, 63, 54);
                    IsOnOffLimitVonPower2 = false;
                    #endregion EndGUI *****************************
                }
                else // then ON
                {
                    try
                    {
                        _powerManager.PowerControlAddress2.OnLimit(3, 2);
                    }
                    catch (Exception)
                    { }
                    

                    btnVonLimitPower2.BackColor = Color.FromArgb(189, 227, 114);

                    lblStatusLimitVonPower2.Text = "ON";
                    lblStatusLimitVonPower2.ForeColor = Color.FromArgb(0, 255, 0);
                    IsOnOffLimitVonPower2 = true;
                }
            }

            // Power2 and Ampe
            else if (name.Equals("btnAmpeLimitPower2"))
            {
                if (IsOnOffLimitAmpePower2) // then OFF
                {
                    try
                    {
                        _powerManager.PowerControlAddress2.OffLimit(3, 1);
                    }
                    catch (Exception)
                    { }

                    #region GUI ******************
                    btnAmpeLimitPower2.BackColor = Color.White;
                    lblStatusLimitAmpePower2.Text = "OFF";
                    lblStatusLimitAmpePower2.ForeColor = Color.FromArgb(43, 63, 54);
                    IsOnOffLimitAmpePower2 = false;
                    #endregion EndGUI ***************************
                }
                else // then ON
                {
                    try
                    {
                        _powerManager.PowerControlAddress2.OnLimit(3, 1);
                    }
                    catch (Exception)
                    { }
                    

                    btnAmpeLimitPower2.BackColor = Color.FromArgb(189, 227, 114);

                    lblStatusLimitAmpePower2.Text = "ON";
                    lblStatusLimitAmpePower2.ForeColor = Color.FromArgb(0, 255, 0);
                    IsOnOffLimitAmpePower2 = true;
                }
            }

            // Power3 and Von
            if (name.Equals("btnVonLimitPower3"))
            {
                if (IsOnOffLimitVonPower3) // then OFF
                {
                    try
                    {
                        _powerManager.PowerControlAddress2.OffLimit(4, 2);
                    }
                    catch (Exception)
                    { }

                    #region GUI **********************
                    btnVonLimitPower3.BackColor = Color.White;

                    lblStatusLimitVonPower3.Text = "OFF";
                    lblStatusLimitVonPower3.ForeColor = Color.FromArgb(43, 63, 54);
                    IsOnOffLimitVonPower3 = false;
                    #endregion EndGUI *****************************
                }
                else // then ON
                {
                    try
                    {
                        _powerManager.PowerControlAddress2.OnLimit(4, 2);
                    }
                    catch (Exception)
                    { }
                    

                    btnVonLimitPower3.BackColor = Color.FromArgb(189, 227, 114);

                    lblStatusLimitVonPower3.Text = "ON";
                    lblStatusLimitVonPower3.ForeColor = Color.FromArgb(0, 255, 0);
                    IsOnOffLimitVonPower3 = true;
                }
            }

            // Power3 and Ampe
            else if (name.Equals("btnAmpeLimitPower3"))
            {
                if (IsOnOffLimitAmpePower3) // then OFF
                {
                    try
                    {
                        _powerManager.PowerControlAddress2.OffLimit(4, 1);
                    }
                    catch (Exception)
                    { }

                    #region GUI ***************
                    btnAmpeLimitPower3.BackColor = Color.White;
                    lblStatusLimitAmpePower3.Text = "OFF";
                    lblStatusLimitAmpePower3.ForeColor = Color.FromArgb(43, 63, 54);
                    IsOnOffLimitAmpePower3 = false;
                    #endregion EndGUI ************************
                }
                else // then ON
                {
                    try
                    {
                        _powerManager.PowerControlAddress2.OnLimit(4, 1);
                    }
                    catch (Exception)
                    { }
                    

                    btnAmpeLimitPower3.BackColor = Color.FromArgb(189, 227, 114);

                    lblStatusLimitAmpePower3.Text = "ON";
                    lblStatusLimitAmpePower3.ForeColor = Color.FromArgb(0, 255, 0);
                    IsOnOffLimitAmpePower3 = true;
                }
            }

        }

        #endregion End Gui On Off Power *********************************

        #endregion EndMethods ==============================================


        #region Events ==============================================

        /// <summary>
        /// Number Keyboard 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, EventArgs e)
        {
            pnlResultPowerAddress2.Visible = true;
            Button btn = (sender as Button);

            if (btn.Text != "V" && btn.Text != "A" && btn.Text != "V." && btn.Text != "A."
                && btn.Text != "0" && btn.Text != "." && btn.Text != "C")
            {
                strResult1.Append(btn.Text);
                pnlResultPowerAddress2.Visible = true;
            }

            else
                switch (btn.Text)
                {
                    case "C":
                        if (strResult1.Length > 0) { strResult1.Remove(strResult1.Length - 1, 1); }
                        break;
                    case "0":
                        if (strResult1.Length > 0) { strResult1.Append(btn.Text); }
                        break;
                    case ".":
                        if (strResult1.Length > 0) { if (strResult1.ToString().IndexOf('.') > 0) { return; } else { strResult1.Append(btn.Text); } } else { strResult1.Append("0").Append(btn.Text); }
                        break;
                    case "V":
                        if (txtResultPowerAddress2.TextLength > 0)
                        {
                            if (Double.Parse(txtResultPowerAddress2.Text) > 0)
                            {
                                // Check selected to set Von
                                if (PowerSelected == 1)
                                {
                                    #region GUI *****************
                                    string oldValue = lblVonSetPower1.Text ;
                                    string text = "000" + Double.Parse(txtResultPowerAddress2.Text.Replace('.', ',')) + "000";
                                    if (text.IndexOf(',') < 0)
                                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                    if (text.IndexOf(',') > 0)
                                        lblVonSetPower1.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                    txtResultPowerAddress2.Text = "";
                                    strResult1.Clear();
                                    pnlResultPowerAddress2.Visible = false;

                                    #endregion EndGUI *********************

                                    #region Handling **************
                                    
                                    string von = lblVonSetPower1.Text;
                                    string ampe = lblAmpeSetPower1.Text;
                                    string maxVon = lblDescriptionVonMaxPower1.Text;
                                    maxVon = maxVon.Replace("V", "");
                                    maxVon = maxVon.Replace('.', ',');

                                    if (Double.Parse(von.Replace('.', ',')) > Double.Parse(maxVon))
                                    {
                                        MessageBox.Show("Giá trị điện áp ngoài dải cho phép.");
                                        lblVonSetPower1.Text = oldValue;
                                        return;
                                    }
                                    if (_powerManager.PowerControlAddress2.StateChanel1 == Power.State.OFF)
                                        return;
                                    _powerManager.PowerControlAddress2.SendPower(von, ampe, 2);

                                    #endregion EndHandling *******************

                                    return;
                                }
                                else if (PowerSelected == 2)
                                {
                                    #region GUI ***********************
                                    string oldValue = lblVonSetPower2.Text;
                                    string text = "000" + Double.Parse(txtResultPowerAddress2.Text.Replace('.', ',')) + "000";
                                    if (text.IndexOf(',') < 0)
                                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                    if (text.IndexOf(',') > 0)
                                        lblVonSetPower2.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                    txtResultPowerAddress2.Text = "";
                                    strResult1.Clear();
                                    pnlResultPowerAddress2.Visible = false;

                                    #endregion EndGUI **********************

                                    #region Handling **************
                                    
                                    string von = lblVonSetPower2.Text;
                                    string ampe = lblAmpeSetPower2.Text;
                                    string maxVon = lblDescriptionVonMaxPower2.Text;
                                    maxVon = maxVon.Replace("V", "");
                                    maxVon = maxVon.Replace('.', ',');

                                    if (Double.Parse(von.Replace('.', ',')) > Double.Parse(maxVon))
                                    {
                                        MessageBox.Show("Giá trị điện áp ngoài dải cho phép.");
                                        lblVonSetPower2.Text = oldValue;
                                        return;
                                    }
                                    if (_powerManager.PowerControlAddress2.StateChanel2 == Power.State.OFF)
                                        return;
                                    _powerManager.PowerControlAddress2.SendPower(von, ampe, 3);

                                    #endregion EndHandling *******************

                                    return;
                                }

                                else if (PowerSelected == 3)
                                {
                                    #region GUI **********************
                                    string oldValue = lblVonSetPower3.Text;
                                    string text = "000" + Double.Parse(txtResultPowerAddress2.Text.Replace('.', ',')) + "000";
                                    if (text.IndexOf(',') < 0)
                                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                    if (text.IndexOf(',') > 0)
                                        lblVonSetPower3.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                    txtResultPowerAddress2.Text = "";
                                    strResult1.Clear();
                                    pnlResultPowerAddress2.Visible = false;

                                    #endregion EndGUI ***************************

                                    #region Handling **************
                                    

                                    string von = lblVonSetPower3.Text;
                                    string ampe = lblAmpeSetPower3.Text;
                                    string maxVon = lblDescriptionVonMaxPower3.Text;
                                    maxVon = maxVon.Replace("V", "");
                                    maxVon = maxVon.Replace('.', ',');

                                    if (Double.Parse(von.Replace('.', ',')) > Double.Parse(maxVon))
                                    {
                                        MessageBox.Show("Giá trị điện áp ngoài dải cho phép.");
                                        lblVonSetPower3.Text = oldValue;
                                        return;
                                    }
                                    if (_powerManager.PowerControlAddress2.StateChanel3 == Power.State.OFF)
                                        return;
                                    _powerManager.PowerControlAddress2.SendPower(von, ampe, 4);

                                    #endregion EndHandling *******************

                                    return;
                                }

                            }
                            else
                            {
                                lblVonSetPower1.Text = "00.00";
                                txtResultPowerAddress2.Text = "";
                                pnlResultPowerAddress2.Visible = false;
                            }
                        }
                        else
                        {
                            //lvlVonSetPower1.Text = "00.00";
                        }
                        break;
                    case "A":
                        if (txtResultPowerAddress2.TextLength > 0)
                        {
                            if (Double.Parse(txtResultPowerAddress2.Text) > 0)
                            {
                                // Check selected to set Ampe
                                if (PowerSelected == 1)
                                {
                                    #region GUI ********************

                                    string text = "000" + Double.Parse(txtResultPowerAddress2.Text.Replace('.', ',')) + "000";
                                    if (text.IndexOf(',') < 0)
                                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                    if (text.IndexOf(',') > 0)
                                        lblAmpeSetPower1.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                    txtResultPowerAddress2.Text = "";
                                    strResult1.Clear();
                                    pnlResultPowerAddress2.Visible = false;

                                    #endregion EndGUI *************************

                                    #region Handling **************
                                    if (_powerManager.PowerControlAddress2.StateChanel1 == Power.State.OFF)
                                        return;

                                    string von = lblVonSetPower1.Text;
                                    string ampe = lblAmpeSetPower1.Text;
                                    _powerManager.PowerControlAddress2.SendPower(von, ampe, 2);

                                    #endregion EndHandling *******************

                                    return;
                                }
                                else if (PowerSelected == 2)
                                {
                                    #region GUI *************************

                                    string text = "000" + Double.Parse(txtResultPowerAddress2.Text.Replace('.', ',')) + "000";
                                    if (text.IndexOf(',') < 0)
                                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                    if (text.IndexOf(',') > 0)
                                        lblAmpeSetPower2.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                    txtResultPowerAddress2.Text = "";
                                    strResult1.Clear();
                                    pnlResultPowerAddress2.Visible = false;

                                    #endregion EndGUI **********************

                                    #region Handling **************
                                    if (_powerManager.PowerControlAddress2.StateChanel2 == Power.State.OFF)
                                        return;

                                    string von = lblVonSetPower2.Text;
                                    string ampe = lblAmpeSetPower2.Text;
                                    _powerManager.PowerControlAddress2.SendPower(von, ampe, 3);

                                    #endregion EndHandling *******************

                                    return;
                                }

                                else if (PowerSelected == 3)
                                {
                                    #region GUI ****************************

                                    string text = "000" + Double.Parse(txtResultPowerAddress2.Text.Replace('.', ',')) + "000";
                                    if (text.IndexOf(',') < 0)
                                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                    if (text.IndexOf(',') > 0)
                                        lblAmpeSetPower3.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                    txtResultPowerAddress2.Text = "";
                                    strResult1.Clear();
                                    pnlResultPowerAddress2.Visible = false;

                                    #endregion EndGUI *********************

                                    #region Handling **************
                                    if (_powerManager.PowerControlAddress2.StateChanel3 == Power.State.OFF)
                                        return;

                                    string von = lblVonSetPower2.Text;
                                    string ampe = lblAmpeSetPower2.Text;
                                    _powerManager.PowerControlAddress2.SendPower(von, ampe, 4);

                                    #endregion EndHandling *******************

                                    return;
                                }

                            }
                            else
                            {
                                lblAmpeSetPower1.Text = "00.00";
                                txtResultPowerAddress2.Text = "";
                                pnlResultPowerAddress2.Visible = false;
                            }
                        }
                        else
                        {
                            //lvlAmpeSetPower1.Text = "00.00";
                        }
                        break;
                    case "V.":
                        if (txtResultPowerAddress2.TextLength > 0)
                        {
                            if (Double.Parse(txtResultPowerAddress2.Text) > 0)
                            {
                                // Check selected to set limit Von
                                if (PowerSelected == 1)
                                {
                                    #region GUI *****************

                                    string text = "000" + Double.Parse(txtResultPowerAddress2.Text.Replace('.', ',')) + "000";
                                    if (text.IndexOf(',') < 0)
                                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                    if (text.IndexOf(',') > 0)
                                        lblVonLimitPower1.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                    txtResultPowerAddress2.Text = "";
                                    strResult1.Clear();
                                    pnlResultPowerAddress2.Visible = false;

                                    #endregion End GUI *******************

                                    #region Handling ********************

                                    if (_powerManager.PowerControlAddress2.StateChanel1 == Power.State.OFF)
                                        return;
                                    string von = lblVonLimitPower1.Text;
                                    string ampe = lblAmpeLimitPower1.Text;
                                    _powerManager.PowerControlAddress2.SendLimit(von, ampe, 2, 2);

                                    #endregion End Handling ********************

                                    return;
                                }
                                else if (PowerSelected == 2)
                                {
                                    string text = "000" + Double.Parse(txtResultPowerAddress2.Text.Replace('.', ',')) + "000";
                                    if (text.IndexOf(',') < 0)
                                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                    if (text.IndexOf(',') > 0)
                                        lblVonLimitPower2.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                    txtResultPowerAddress2.Text = "";
                                    strResult1.Clear();
                                    pnlResultPowerAddress2.Visible = false;

                                    #region Handling ********************

                                    if (_powerManager.PowerControlAddress2.StateChanel2 == Power.State.OFF)
                                        return;
                                    string von = lblVonLimitPower2.Text;
                                    string ampe = lblAmpeLimitPower2.Text;
                                    _powerManager.PowerControlAddress2.SendLimit(von, ampe, 3, 2);

                                    #endregion End Handling ********************

                                    return;
                                }
                                else if (PowerSelected == 3)
                                {
                                    string text = "000" + Double.Parse(txtResultPowerAddress2.Text.Replace('.', ',')) + "000";
                                    if (text.IndexOf(',') < 0)
                                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                    if (text.IndexOf(',') > 0)
                                        lblVonLimitPower3.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                    txtResultPowerAddress2.Text = "";
                                    strResult1.Clear();
                                    pnlResultPowerAddress2.Visible = false;

                                    #region Handling ********************

                                    if (_powerManager.PowerControlAddress2.StateChanel3 == Power.State.OFF)
                                        return;
                                    string von = lblVonLimitPower3.Text;
                                    string ampe = lblAmpeLimitPower3.Text;
                                    _powerManager.PowerControlAddress2.SendLimit(von, ampe, 4, 2);

                                    #endregion End Handling ********************

                                    return;
                                }


                            }
                            else
                            {
                                lblVonLimitPower1.Text = "00.00";
                                txtResultPowerAddress2.Text = "";
                                pnlResultPowerAddress2.Visible = false;
                            }
                        }
                        else
                        {
                            //lvlVonLimitPower1.Text = "00.00";
                        }
                        break;
                    case "A.":
                        if (txtResultPowerAddress2.TextLength > 0)
                        {
                            if (Double.Parse(txtResultPowerAddress2.Text) > 0)
                            {
                                // Check selected to set limit Ampe

                                if (PowerSelected == 1)
                                {
                                    string text = "000" + Double.Parse(txtResultPowerAddress2.Text.Replace('.', ',')) + "000";
                                    if (text.IndexOf(',') < 0)
                                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                    if (text.IndexOf(',') > 0)
                                        lblAmpeLimitPower1.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                    txtResultPowerAddress2.Text = "";
                                    strResult1.Clear();
                                    pnlResultPowerAddress2.Visible = false;

                                    #region Handling ********************

                                    if (_powerManager.PowerControlAddress2.StateChanel1 == Power.State.OFF)
                                        return;
                                    string von = lblVonLimitPower1.Text;
                                    string ampe = lblAmpeLimitPower1.Text;
                                    _powerManager.PowerControlAddress2.SendLimit(von, ampe, 2, 1);

                                    #endregion End Handling ********************
                                    return;
                                }
                                else if (PowerSelected == 2)
                                {
                                    string text = "000" + Double.Parse(txtResultPowerAddress2.Text.Replace('.', ',')) + "000";
                                    if (text.IndexOf(',') < 0)
                                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                    if (text.IndexOf(',') > 0)
                                        lblAmpeLimitPower2.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                    txtResultPowerAddress2.Text = "";
                                    strResult1.Clear();
                                    pnlResultPowerAddress2.Visible = false;

                                    #region Handling ********************

                                    if (_powerManager.PowerControlAddress2.StateChanel2 == Power.State.OFF)
                                        return;
                                    string von = lblVonLimitPower2.Text;
                                    string ampe = lblAmpeLimitPower2.Text;
                                    _powerManager.PowerControlAddress2.SendLimit(von, ampe, 3, 1);

                                    #endregion End Handling ********************

                                    return;
                                }
                                else if (PowerSelected == 3)
                                {
                                    string text = "000" + Double.Parse(txtResultPowerAddress2.Text.Replace('.', ',')) + "000";
                                    if (text.IndexOf(',') < 0)
                                        text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                    if (text.IndexOf(',') > 0)
                                        lblAmpeLimitPower3.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                    txtResultPowerAddress2.Text = "";
                                    strResult1.Clear();
                                    pnlResultPowerAddress2.Visible = false;

                                    #region Handling ********************

                                    if (_powerManager.PowerControlAddress2.StateChanel3 == Power.State.OFF)
                                        return;
                                    string von = lblVonLimitPower3.Text;
                                    string ampe = lblAmpeLimitPower3.Text;
                                    _powerManager.PowerControlAddress2.SendLimit(von, ampe, 4, 1);

                                    #endregion End Handling ********************

                                    return;
                                }


                            }
                            else
                            {
                                lblAmpeLimitPower1.Text = "00.00";
                                txtResultPowerAddress2.Text = "";
                                pnlResultPowerAddress2.Visible = false;
                            }
                        }
                        else
                        {
                            //lvlAmpeLimitPower1.Text = "00.00";
                        }
                        break;
                    default:
                        break;
                }

            txtResultPowerAddress2.Clear();
            txtResultPowerAddress2.Text = strResult1.ToString();

        }


        /// <summary>
        /// Selected power to set Von And Ampe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectedPower_Click(object sender, EventArgs e)
        {
            btnSelectedPower1.BackColor = Color.White;
            btnSelectedPower2.BackColor = Color.White;
            btnSelectedPower3.BackColor = Color.White;
            Button btn = (sender as Button);
            btn.BackColor = Color.FromArgb(189, 227, 114);

            ChoosePower(btn.Text);

        }

        private void ButtonOnOffPower_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);//to do: check 

            if (btn.Name.Equals("btnOnOffPower1")) { OnOffPower1(); if (!IsOnPower1 && !IsOnPowerAll) { IsOnPowerAll = true; btnOnOffPowerAll.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF); } }
            else if (btn.Name.Equals("btnOnOffPower2")) { OnOffPower2(); if (!IsOnPower2 && !IsOnPowerAll) { IsOnPowerAll = true; btnOnOffPowerAll.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF); } }
            else if (btn.Name.Equals("btnOnOffPower3")) { OnOffPower3(); if (!IsOnPower3 && !IsOnPowerAll) { IsOnPowerAll = true; btnOnOffPowerAll.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF); } }
            else { OnOffPowerAll(); }
        }

        private void ButtonOnOffLimit_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);

            OnOffLimit(btn.Name);
        }

        private void btnResetLimitPowerAll_Click(object sender, EventArgs e)
        {
            ResertValuesPower2();
        }

        #endregion End Events ==============================================

        #endregion End Power Address 2 **********************************************************

        // **********************************************************************************************************************************************************************************************************//
        // **********************************************************************************************************************************************************************************************************//
        // **********************************************************************************************************************************************************************************************************//

        #region Power Address 1 **********************************************************

        #region Methods ===================================

        public void Innitialize1()
        {
            pnlResultPowerAddress1.Visible = false;

            ConnectionToAddress1();
        }

        public void ConnectionToAddress1()
        {
            // Kết nối không thành công thì sẽ đóng button OnOFF Nguồn tới địa chỉ 1
            if (!_powerManager.ConnectionFromAddress1())
            {
                btnOnOffPower4.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ERROR);
                btnOnOffPower4.BackgroundImageLayout = ImageLayout.Stretch;
                btnOnOffPower4.Enabled = false;
            }
        }

        Boolean IsOnPower4 = false;
        public void OnOffPower4()
        {
            if (IsOnPower4)
            {

                #region Handling ********************************
                try
                {
                    _powerManager.PowerControlAddress1.OffChanel(1);
                    _powerManager.PowerControlAddress1.StateChanel1 = Power.State.OFF;
                }
                catch (Exception)
                {
                    
                }

                
                #endregion End Handling ********************************

                #region GUI ********************************

                btnOnOffPower4.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                btnOnOffPower4.BackgroundImageLayout = ImageLayout.Stretch;

                lblDescriptionNameOfPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblStatusPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpePower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionWoatPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionSetPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonSetPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeSetPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionLimitPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonLimitPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeLimitPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionVonMaxPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblDescriptionAmpeMaxPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblRegion1Power4.BackColor = Color.FromArgb(43, 63, 54);
                lblStatusRangPower4.ForeColor = Color.FromArgb(43, 63, 54);

                lblStatusPower4.Text = "OFF";

                lblVonReceivePower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeReceivePower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblWoatReceivePower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonSetPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeSetPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblVonLimitPower4.ForeColor = Color.FromArgb(43, 63, 54);
                lblAmpeLimitPower4.ForeColor = Color.FromArgb(43, 63, 54);

                IsOnPower4 = false;
                #endregion End GUI ********************************

            }
            else
            {

                #region Handling ********************************

                String vonLimit = lblVonLimitPower4.Text;
                String ampeLimit = lblAmpeLimitPower4.Text;
                _powerManager.PowerControlAddress1.SendLimit(vonLimit, ampeLimit, 1, 1);
                _powerManager.PowerControlAddress1.SendLimit(vonLimit, ampeLimit, 1, 2);

                String von = lblVonSetPower4.Text;
                String ampe = lblAmpeSetPower4.Text;
                _powerManager.PowerControlAddress1.OnChanel(1);
                _powerManager.PowerControlAddress1.StateChanel1 = Power.State.ON;
                _powerManager.PowerControlAddress1.SendPower(von, ampe, 1);


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
                                    _powerManager.PowerControlAddress1.PowerControl.Write(Z119.ATK.Common.Const.READALL + " CH1");

                                    string result = _powerManager.PowerControlAddress1.PowerControl.ReadString();

                                    
                                    string[] values = result.Split(',');
                                    if (Double.Parse(values[0].Replace('.', ',')) < 100)
                                    {
                                        string text = "000" + Double.Parse(values[0].Replace('.', ',')) + "000";
                                        if (text.IndexOf(',') < 0)
                                            text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                        if (text.IndexOf(',') > 0)
                                            lblVonReceivePower4.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                        //lblVonReceivePower4.Text = values[0];
                                    }

                                    if (Double.Parse(values[1].Replace('.', ',')) < 100)
                                    {
                                        string text = "000" + Double.Parse(values[1].Replace('.', ',')) + "000";
                                        if (text.IndexOf(',') < 0)
                                            text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                        if (text.IndexOf(',') > 0)
                                            lblAmpeReceivePower4.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');
                                        double amp = Double.Parse(values[1].Replace('.', ','));

                                        string maxAmp = lblAmpeLimitPower4.Text.Replace("A", "");

                                        maxAmp = maxAmp.Replace('.', ',');
                                        double ampMax = Double.Parse(maxAmp);
                                        if (amp > ampMax)
                                        {
                                            OffPowerAll();
                                            MessageBox.Show("Tắt nguồn do dòng điện quá mức cho phép.");
                                            return;
                                        }

                                    }
                                        
                                    if (Double.Parse(values[2].Replace('.', ',')) < 100)
                                    {
                                        string text = "000" + Double.Parse(values[2].Replace('.', ',')) + "000";
                                        if (text.IndexOf(',') < 0)
                                            text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                        if (text.IndexOf(',') > 0)
                                            lblWoatReceivePower4.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');
                                    }

                                    RunningTask4 = true;
                                    

                                }
                                catch (Exception)
                                {
                                    RunningTask4 = false;
                                    //MessageBox.Show("Lỗi kết nối nguồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }));

                            Thread.Sleep(100);
                            if (!RunningTask4)
                                break;
                        }
                        catch (Exception)
                        {
                            Thread.Sleep(100);
                            //MessageBox.Show("Lỗi kết nối nguồn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        
                    }
                });
                #endregion End Handling ********************************

                #region GUI ********************************

                btnOnOffPower4.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ON);
                btnOnOffPower4.BackgroundImageLayout = ImageLayout.Stretch;

                lblDescriptionNameOfPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblStatusPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpePower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionWoatPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionSetPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonSetPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpeSetPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionLimitPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonLimitPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpeLimitPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionVonMaxPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblDescriptionAmpeMaxPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblRegion1Power4.BackColor = Color.FromArgb(0, 255, 0);
                lblStatusRangPower4.ForeColor = Color.FromArgb(0, 255, 0);

                lblStatusPower4.Text = "ON";

                lblVonReceivePower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblAmpeReceivePower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblWoatReceivePower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblVonSetPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblAmpeSetPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblVonLimitPower4.ForeColor = Color.FromArgb(0, 255, 0);
                lblAmpeLimitPower4.ForeColor = Color.FromArgb(0, 255, 0);

                IsOnPower4 = true;
                #endregion End GUI ********************************

            }
        }

        Boolean IsOnOffLimitVonPower4 = false;
        Boolean IsOnOffLimitAmpePower4 = false;
        public void OnOffLimitPower4(String name)
        {
            // Power1 and Von
            if (name.Equals("btnVonLimitPower4"))
            {
                if (IsOnOffLimitVonPower4) // then OFF
                {
                    #region GUI **************
                    btnVonLimitPower4.BackColor = Color.White;

                    lblStatusLimitVonPower4.Text = "OFF";
                    lblStatusLimitVonPower4.ForeColor = Color.FromArgb(43, 63, 54);
                    IsOnOffLimitVonPower4 = false;
                    #endregion EndGUI ******************

                    try
                    {
                        _powerManager.PowerControlAddress1.OffLimit(1, 2);
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                }
                else // then ON
                {
                    try
                    {
                        _powerManager.PowerControlAddress1.OnLimit(1, 2);
                    }
                    catch (Exception)
                    {
                        
                    }
                    

                    btnVonLimitPower4.BackColor = Color.FromArgb(189, 227, 114);

                    lblStatusLimitVonPower4.Text = "ON";
                    lblStatusLimitVonPower4.ForeColor = Color.FromArgb(0, 255, 0);
                    IsOnOffLimitVonPower4 = true;
                }
            }

            // Power1 and Ampe
            else if (name.Equals("btnAmpeLimitPower4"))
            {
                if (IsOnOffLimitAmpePower4) // then OFF
                {
                    try
                    {
                        _powerManager.PowerControlAddress1.OffLimit(1, 1);
                    }
                    catch (Exception)
                    {
                        
                    }

                    #region GUI ********************
                    btnAmpeLimitPower4.BackColor = Color.White;

                    lblStatusLimitAmpePower4.Text = "OFF";
                    lblStatusLimitAmpePower4.ForeColor = Color.FromArgb(43, 63, 54);
                    IsOnOffLimitAmpePower4 = false;
                    #endregion EndGUI ************************
                }
                else // then ON
                {
                    try
                    {
                        _powerManager.PowerControlAddress1.OnLimit(1, 1);
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                    btnAmpeLimitPower4.BackColor = Color.FromArgb(189, 227, 114);

                    lblStatusLimitAmpePower4.Text = "ON";
                    lblStatusLimitAmpePower4.ForeColor = Color.FromArgb(0, 255, 0);
                    IsOnOffLimitAmpePower4 = true;
                }
            }
        }


        #endregion End Methods ===============================

        #region Events =====================================

        private void BtnNumber_Click(object sender, EventArgs e)
        {
            pnlResultPowerAddress1.Visible = true;
            Button btn = (sender as Button);

            if (btn.Text != "V" && btn.Text != "A" && btn.Text != "V." && btn.Text != "A."
                && btn.Text != "0" && btn.Text != "." && btn.Text != "C")
            {
                strResult2.Append(btn.Text);
                pnlResultPowerAddress1.Visible = true;
            }

            else
                switch (btn.Text)
                {
                    case "C":
                        if (strResult2.Length > 0) { strResult2.Remove(strResult2.Length - 1, 1); }
                        break;
                    case "0":
                        if (strResult2.Length > 0) { strResult2.Append(btn.Text); }
                        break;
                    case ".":
                        if (strResult2.Length > 0) { if (strResult2.ToString().IndexOf('.') > 0) { return; } else { strResult2.Append(btn.Text); } } else { strResult2.Append("0").Append(btn.Text); }
                        break;
                    case "V":
                        if (txtResultPowerAddress1.TextLength > 0)
                        {
                            if (Double.Parse(txtResultPowerAddress1.Text) > 0)
                            {
                                // Check selected to set Von
                                #region GUI *****************
                                string oldValue = lblVonSetPower4.Text;
                                string text = "000" + Double.Parse(txtResultPowerAddress1.Text.Replace('.', ',')) + "000";
                                if (text.IndexOf(',') < 0)
                                    text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                if (text.IndexOf(',') > 0)
                                    lblVonSetPower4.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');
                                
                                txtResultPowerAddress1.Text = "";
                                strResult2.Clear();
                                pnlResultPowerAddress1.Visible = false;

                                #endregion EndGUI *********************

                                #region Handling **************
                                

                                string von = lblVonSetPower4.Text;
                                string ampe = lblAmpeSetPower4.Text;
                                string maxVon = lblDescriptionVonMaxPower4.Text;
                                maxVon = maxVon.Replace("V", "");
                                maxVon = maxVon.Replace('.', ',');

                                if (Double.Parse(von.Replace('.', ',')) > Double.Parse(maxVon))
                                {
                                    MessageBox.Show("Giá trị điện áp ngoài dải cho phép.");
                                    lblVonSetPower4.Text = oldValue;
                                    return;
                                }
                                if (_powerManager.PowerControlAddress1.StateChanel1 == Power.State.OFF)
                                    return;
                                _powerManager.PowerControlAddress1.SendPower(von, ampe, 1);

                                #endregion EndHandling *******************

                                return;

                            }
                            else
                            {
                                lblVonSetPower4.Text = "00.00";
                                txtResultPowerAddress1.Text = "";
                                pnlResultPowerAddress1.Visible = false;
                            }
                        }
                        else
                        {
                            //lvlVonSetPower1.Text = "00.00";
                        }
                        break;
                    case "A":
                        if (txtResultPowerAddress1.TextLength > 0)
                        {
                            if (Double.Parse(txtResultPowerAddress1.Text) > 0)
                            {
                                #region GUI ********************
                                string oldValue = lblAmpeSetPower4.Text;
                                string text = "000" + Double.Parse(txtResultPowerAddress1.Text.Replace('.', ',')) + "000";
                                if (text.IndexOf(',') < 0)
                                    text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                if (text.IndexOf(',') > 0)
                                    lblAmpeSetPower4.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                txtResultPowerAddress1.Text = "";
                                strResult2.Clear();
                                pnlResultPowerAddress1.Visible = false;

                                #endregion EndGUI *************************

                                #region Handling **************
                                if (_powerManager.PowerControlAddress1.StateChanel1 == Power.State.OFF)
                                    return;

                                string von = lblVonSetPower4.Text;
                                string ampe = lblAmpeSetPower4.Text;
                                
                                string maxAmp =this.lblDescriptionAmpeMaxPower4.Text;
                                maxAmp = Regex.Replace(maxAmp, "A", "");
                                maxAmp = maxAmp.Replace('.', ',');

                                if (Double.Parse(ampe.Replace('.', ',')) > Double.Parse(maxAmp))
                                {
                                    MessageBox.Show("Giá trị dòng điện ngoài dải cho phép.");
                                    lblAmpeSetPower4.Text = oldValue;
                                    return;
                                }
                                _powerManager.PowerControlAddress1.SendPower(von, ampe, 1);
                                #endregion EndHandling *******************

                                return;
                            }
                            else
                            {
                                lblAmpeSetPower4.Text = "00.00";
                                txtResultPowerAddress1.Text = "";
                                pnlResultPowerAddress1.Visible = false;
                            }
                        }
                        else
                        {
                            //lvlAmpeSetPower1.Text = "00.00";
                        }
                        break;
                    case "V.":
                        if (txtResultPowerAddress1.TextLength > 0)
                        {
                            if (Double.Parse(txtResultPowerAddress1.Text) > 0)
                            {

                                string text = "000" + Double.Parse(txtResultPowerAddress1.Text.Replace('.', ',')) + "000";
                                if (text.IndexOf(',') < 0)
                                    text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                if (text.IndexOf(',') > 0)
                                    lblVonLimitPower4.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                txtResultPowerAddress1.Text = "";
                                strResult2.Clear();
                                pnlResultPowerAddress1.Visible = false;

                                #region Handling ********************

                                if (_powerManager.PowerControlAddress1.StateChanel1 == Power.State.OFF)
                                    return;
                                string von = lblVonLimitPower4.Text;
                                string ampe = lblAmpeLimitPower4.Text;
                                _powerManager.PowerControlAddress1.SendLimit(von, ampe, 1, 2);

                                #endregion End Handling ********************

                                return;

                            }
                            else
                            {
                                lblVonLimitPower4.Text = "00.00";
                                txtResultPowerAddress1.Text = "";
                                pnlResultPowerAddress1.Visible = false;
                            }
                        }
                        else
                        {
                            //lvlVonLimitPower1.Text = "00.00";
                        }
                        break;
                    case "A.":
                        if (txtResultPowerAddress1.TextLength > 0)
                        {
                            if (Double.Parse(txtResultPowerAddress1.Text) > 0)
                            {
                                string text = "000" + Double.Parse(txtResultPowerAddress1.Text.Replace('.', ',')) + "000";
                                if (text.IndexOf(',') < 0)
                                    text = text.Remove(text.Length - 3, 3) + ",000"; ;
                                if (text.IndexOf(',') > 0)
                                    lblAmpeLimitPower4.Text = text.Substring(text.IndexOf(',') - 2, 5).Replace(',', '.');

                                txtResultPowerAddress1.Text = "";
                                strResult2.Clear();
                                pnlResultPowerAddress1.Visible = false;

                                #region Handling ********************

                                if (_powerManager.PowerControlAddress1.StateChanel1 == Power.State.OFF)
                                    return;
                                string von = lblVonLimitPower4.Text;
                                string ampe = lblAmpeLimitPower4.Text;
                                _powerManager.PowerControlAddress1.SendLimit(von, ampe, 1, 1);

                                #endregion End Handling ********************

                                return;
                            }
                            else
                            {
                                lblAmpeLimitPower4.Text = "00.00";
                                txtResultPowerAddress1.Text = "";
                                pnlResultPowerAddress1.Visible = false;
                            }
                        }
                        else
                        {
                            //lvlAmpeLimitPower1.Text = "00.00";
                        }
                        break;
                    default:
                        break;
                }

            txtResultPowerAddress1.Clear();
            txtResultPowerAddress1.Text = strResult2.ToString();
        }

        private void btnOnOffPower4_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            OnOffPower4();
            if (!IsOnPower4 && !IsOnPowerAll)
            {
                IsOnPowerAll = true;
                btnOnOffPowerAll.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
            }
        }
        private void btnOnOffLimitPower4(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            OnOffLimitPower4(btn.Name);
        }

        private void btnSelectedRang_Click(object sender, EventArgs e)
        {
            Button btn = (sender as Button);
            if (btn.Text.Equals("Dải 1"))
            {
                _powerManager.PowerControlAddress1.ChangedRangeForPower1(1);
                _powerManager.PowerControlAddress1.StateRangForPower1 = Power.StateRang.RANG1;
                lblStatusRangPower4.Text = "Dải 1";
                lblDescriptionVonMaxPower4.Text = "20V";
                lblDescriptionAmpeMaxPower4.Text = "10A";
                lblLimitMinMaxPower4.Text = "20V\n10A";
                lblVonLimitPower4.Text = "22V";
                lblAmpeLimitPower4.Text = "11A";
            }
            else
            {
                _powerManager.PowerControlAddress1.ChangedRangeForPower1(2);
                _powerManager.PowerControlAddress1.StateRangForPower1 = Power.StateRang.RANG2;
                lblStatusRangPower4.Text = "Dải 2";
                lblDescriptionVonMaxPower4.Text = "40V";
                lblDescriptionAmpeMaxPower4.Text = "5A";
                lblLimitMinMaxPower4.Text = "40V\n5A";
                lblVonLimitPower4.Text = "44V";
                lblAmpeLimitPower4.Text = "5.5A";
            }
        }

        #endregion End Events ======================================

        private void fPower1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            
        }

        #endregion End Power Address 1 **********************************************************

        private void btnResetLimitPower4_Click(object sender, EventArgs e)
        {
            ResetValuesPower1();
        }
        

        // ************************************************************//
        // ************************************************************//
        // ************************************************************//

        #region SaveFile ========================================

        public void SaveData()
        {
            PowerBindingModel model = new PowerBindingModel();
            LoadDataIntoModelFromControls(model);
            _powerManager.SaveIntoFile(model);
        }


        public void LoadDataIntoModelFromControls(PowerBindingModel model)
        {
            model.VonSetPower1 = lblVonSetPower4.Text;
            model.AmpeSetPower1 = lblAmpeSetPower4.Text;
            model.VonLimitPower1 = lblVonLimitPower4.Text;
            model.AmpeLimitPower1 = lblAmpeLimitPower4.Text;
            model.OnAmpeLimitPower1 = lblStatusLimitAmpePower4.Text == "ON" ? true : false;
            model.OnVonLimitPower1 = lblStatusLimitVonPower4.Text == "ON" ? true : false;
            model.RangePower1 = lblStatusRangPower4.Text == "Dải 1" ? true : false;

            model.VonSetPower2 = lblVonSetPower1.Text;
            model.AmpeSetPower2 = lblAmpeSetPower1.Text;
            model.VonLimitPower2 = lblVonLimitPower1.Text;
            model.AmpeLimitPower2 = lblAmpeLimitPower1.Text;
            model.OnAmpeLimitPower2 = lblStatusLimitAmpePower1.Text == "ON" ? true : false;
            model.OnVonLimitPower2 = lblStatusLimitVonPower1.Text == "ON" ? true : false;

            model.VonSetPower3 = lblVonSetPower2.Text;
            model.AmpeSetPower3 = lblAmpeSetPower2.Text;
            model.VonLimitPower3 = lblVonLimitPower2.Text;
            model.AmpeLimitPower3 = lblAmpeLimitPower2.Text;
            model.OnAmpeLimitPower3 = lblStatusLimitAmpePower2.Text == "ON" ? true : false;
            model.OnVonLimitPower3 = lblStatusLimitVonPower2.Text == "ON" ? true : false;

            model.VonSetPower4 = lblVonSetPower3.Text;
            model.AmpeSetPower4 = lblAmpeSetPower3.Text;
            model.VonLimitPower4 = lblVonLimitPower3.Text;
            model.AmpeLimitPower4 = lblAmpeLimitPower3.Text;
            model.OnAmpeLimitPower4 = lblStatusLimitAmpePower3.Text == "ON" ? true : false;
            model.OnVonLimitPower4 = lblStatusLimitVonPower3.Text == "ON" ? true : false;
        }

        private void tsmuItemSaveFile_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        public void LoadDataIntoControlsFromModel(PowerBindingModel model)
        {
            ResetValuesPower1();
            
            lblVonSetPower4.Text = model.VonSetPower1;
            lblAmpeSetPower4.Text = model.AmpeSetPower1;
            lblVonLimitPower4.Text = model.VonLimitPower1;
            if (lblVonLimitPower4.Text == "")
            { 
                ResertValuesPower2();
                
            }
            
            if (model.OnAmpeLimitPower1)
            {
                IsOnOffLimitAmpePower4 = false;
                OnOffLimitPower4("btnAmpeLimitPower4");
            }

            else
            {
                IsOnOffLimitAmpePower4 = true;
                OnOffLimitPower4("btnAmpeLimitPower4");
            }

            if (model.OnVonLimitPower1)
            {
                IsOnOffLimitVonPower4 = false;
                OnOffLimitPower4("btnVonLimitPower4");
            }
            else
            {
                IsOnOffLimitVonPower4 = true;
                OnOffLimitPower4("btnVonLimitPower4");
            }

            if (model.RangePower1)
            {
                _powerManager.PowerControlAddress1.ChangedRangeForPower1(1);
                _powerManager.PowerControlAddress1.StateRangForPower1 = Power.StateRang.RANG1;
                lblStatusRangPower4.Text = "Dải 1";
                lblDescriptionVonMaxPower4.Text = "20V";
                lblDescriptionAmpeMaxPower4.Text = "10A";
                lblLimitMinMaxPower4.Text = "20V\n10A";
            }
            else
            {
                _powerManager.PowerControlAddress1.ChangedRangeForPower1(2);
                _powerManager.PowerControlAddress1.StateRangForPower1 = Power.StateRang.RANG2;
                lblStatusRangPower4.Text = "Dải 2";
                lblDescriptionVonMaxPower4.Text = "40V";
                lblDescriptionAmpeMaxPower4.Text = "5A";
                lblLimitMinMaxPower4.Text = "40V\n5A";
            }

            lblVonSetPower1.Text = model.VonSetPower2;
            lblAmpeSetPower1.Text = model.AmpeSetPower2;
            lblVonLimitPower1.Text = model.VonLimitPower2;
            lblAmpeLimitPower1.Text = model.AmpeLimitPower2;
            lblAmpeLimitPower4.Text = model.AmpeLimitPower1;
            if (lblVonLimitPower1.Text == "")
                ResetValuesPower1();
            if (model.OnAmpeLimitPower2)
            {
                IsOnOffLimitAmpePower1 = false;
                OnOffLimit("btnAmpeLimitPower=1");
            }
            else
            {
                IsOnOffLimitAmpePower1 = true;
                OnOffLimit("btnAmpeLimitPower1");
            }

            if (model.OnVonLimitPower2)
            {
                IsOnOffLimitVonPower1 = false;
                OnOffLimit("btnVonLimitPower1");
            }
            else
            {
                IsOnOffLimitVonPower1 = true;
                OnOffLimit("btnVonLimitPower1");
            }

            lblVonSetPower2.Text = model.VonSetPower3;
            lblAmpeSetPower2.Text = model.AmpeSetPower3;
            lblVonLimitPower2.Text = model.VonLimitPower3;
            lblAmpeLimitPower2.Text = model.AmpeLimitPower3;

            if (model.OnAmpeLimitPower3)
            {
                IsOnOffLimitAmpePower2 = false;
                OnOffLimit("btnAmpeLimitPower2");
            }
            else
            {
                IsOnOffLimitAmpePower2 = true;
                OnOffLimit("btnAmpeLimitPower2");
            }
            
            if (model.OnVonLimitPower3)
            {
                IsOnOffLimitVonPower2 = false;
                OnOffLimit("btnVonLimitPower2");
            }
            else
            {
                IsOnOffLimitVonPower2 = true;
                OnOffLimit("btnVonLimitPower2");
            }

            lblVonSetPower3.Text = model.VonSetPower4;
            lblAmpeSetPower3.Text = model.AmpeSetPower4;
            lblVonLimitPower3.Text = model.VonLimitPower4;
            lblAmpeLimitPower3.Text = model.AmpeLimitPower4;

            if(model.OnAmpeLimitPower4)
            {
                IsOnOffLimitAmpePower3 = false;
                OnOffLimit("btnAmpeLimitPower3");
            }
            else
            {
                IsOnOffLimitAmpePower3 = true;
                OnOffLimit("btnAmpeLimitPower3");
            }

            if (model.OnVonLimitPower4)
            {
                IsOnOffLimitVonPower3 = false;
                OnOffLimit("btnVonLimitPower3");
            }
            else
            {
                IsOnOffLimitVonPower3 = true;
                OnOffLimit("btnVonLimitPower3");
            }

        }

        public void LoadData()
        {
            PowerBindingModel model = _powerManager.OpenFile();
            if (model != null)
                LoadDataIntoControlsFromModel(model);
            else
                ResertValuesPower2();
        }

        private void tsmuItemOpenFile_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        #endregion End SaveFile

        private void tsmuItemRun_Click(object sender, EventArgs e)
        {
            RunOffFromServer();
        }

        public bool isRunAll = false;

        public void RunOffFromServer()
        {
            IsOnPower1 = true;
            OnOffPower1();

            IsOnPower2 = true;
            OnOffPower2();

            IsOnPower3 = true;
            OnOffPower3();

            IsOnPower4 = true;
            OnOffPower4();

            if (isRunAll)
            {
                tsmuItemRun.Text = "Chạy";
                isRunAll = false;

            }
            else
            {
                tsmuItemRun.Text = "Dừng";

                if (lblVonSetPower1.Text != "00.00")
                {
                    IsOnPower1 = false;
                    OnOffPower1();
                }

                if (lblVonSetPower2.Text != "00.00")
                {
                    IsOnPower2 = false;
                    OnOffPower2();
                }

                if (lblVonSetPower3.Text != "00.00")
                {
                    IsOnPower3 = false;
                    OnOffPower3();
                }

                if (lblVonSetPower4.Text != "00.00")
                {
                    IsOnPower4 = false;
                    OnOffPower4();
                }
                isRunAll = true;
            }

        }

        private void thuNhỏToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Text == "Thu nhỏ")
            {
                this.Width = 600;
                (sender as ToolStripMenuItem).Text = "Mở rộng";
            }
            else
            {
                this.Width = 930;
                (sender as ToolStripMenuItem).Text = "Thu nhỏ";
            }
        }

        private void lblStatusRangPower4_Click(object sender, EventArgs e)
        {

        }
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        internal void Disconnect()
        {

            try
            {
                _powerManager.Disconnection(1);
            }
            catch (Exception)
            { }

            try
            {
                _powerManager.Disconnection(2);
            }
            catch (Exception)
            { }

            try
            {
                _powerManager.Disconnection(3);
            }
            catch (Exception)
            { }

            try
            {
                _powerManager.Disconnection(4);
            }
            catch (Exception)
            { }
        }

        private void lblVonReceivePower4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_powerManager.PowerControlAddress1.StateChanel1 == Power.State.ON)
            {
                string mVon = lblVonReceivePower4.Text;
                string maxVon = lblVonLimitPower4.Text;
                mVon = Regex.Replace(mVon, "V", "");
                maxVon = Regex.Replace(maxVon, "V", "");
                maxVon = maxVon.Replace('.', ',');
                mVon = mVon.Replace('.', ',');
                
                if (Double.Parse(mVon) > Double.Parse(maxVon))
                {
                    OnOffPower4();
                    MessageBox.Show("Điện áp vượt quá giá trị cho phép.");

                }

                string mAmp = this.lblAmpeReceivePower4.Text;
                string maxAmp = this.lblAmpeLimitPower4.Text;
                mAmp = Regex.Replace(mAmp, "A", "");
                maxAmp = Regex.Replace(maxAmp, "A", "");
                maxAmp = maxAmp.Replace('.', ',');
                mAmp = mAmp.Replace('.', ',');
                if (Double.Parse(mAmp) > Double.Parse(maxAmp))
                {
                    OnOffPower4();
                    MessageBox.Show("Điện áp vượt quá giá trị cho phép.");
                }
            }
            if (_powerManager.PowerControlAddress2.StateChanel1 == Power.State.ON)
            {
                string mVon = lblVonReceivePower1.Text;
                string maxVon = lblVonLimitPower1.Text;
                mVon = Regex.Replace(mVon, "V", "");
                maxVon = Regex.Replace(maxVon, "V", "");
                maxVon = maxVon.Replace('.', ',');
                mVon = mVon.Replace('.', ',');

                if (Double.Parse(mVon) > Double.Parse(maxVon))
                {
                    OnOffPower1();
                    MessageBox.Show("Điện áp vượt quá giá trị cho phép.");

                }

                string mAmp = this.lblAmpeReceivePower1.Text;
                string maxAmp = this.lblAmpeLimitPower1.Text;
                mAmp = Regex.Replace(mAmp, "A", "");
                maxAmp = Regex.Replace(maxAmp, "A", "");
                maxAmp = maxAmp.Replace('.', ',');
                mAmp = mAmp.Replace('.', ',');
                if (Double.Parse(mAmp) > Double.Parse(maxAmp))
                {
                    OnOffPower1();
                    MessageBox.Show("Điện áp vượt quá giá trị cho phép.");
                }
            }
            if (_powerManager.PowerControlAddress2.StateChanel2 == Power.State.ON)
            {
                string mVon = lblVonReceivePower2.Text;
                string maxVon = lblVonLimitPower2.Text;
                mVon = Regex.Replace(mVon, "V", "");
                maxVon = Regex.Replace(maxVon, "V", "");
                maxVon = maxVon.Replace('.', ',');
                mVon = mVon.Replace('.', ',');

                if (Double.Parse(mVon) > Double.Parse(maxVon))
                {
                    OnOffPower2();
                    MessageBox.Show("Điện áp vượt quá giá trị cho phép.");

                }

                string mAmp = this.lblAmpeReceivePower2.Text;
                string maxAmp = this.lblAmpeLimitPower2.Text;
                mAmp = Regex.Replace(mAmp, "A", "");
                maxAmp = Regex.Replace(maxAmp, "A", "");
                maxAmp = maxAmp.Replace('.', ',');
                mAmp = mAmp.Replace('.', ',');
                if (Double.Parse(mAmp) > Double.Parse(maxAmp))
                {
                    OnOffPower2();
                    MessageBox.Show("Điện áp vượt quá giá trị cho phép.");
                }
            }
            if (_powerManager.PowerControlAddress2.StateChanel3 == Power.State.ON)
            {
                string mVon = lblVonReceivePower3.Text;
                string maxVon = lblVonLimitPower3.Text;
                mVon = Regex.Replace(mVon, "V", "");
                maxVon = Regex.Replace(maxVon, "V", "");
                maxVon = maxVon.Replace('.', ',');
                mVon = mVon.Replace('.', ',');

                if (Double.Parse(mVon) > Double.Parse(maxVon))
                {
                    OnOffPower3();
                    MessageBox.Show("Điện áp vượt quá giá trị cho phép.");

                }

                string mAmp = this.lblAmpeReceivePower3.Text;
                string maxAmp = this.lblAmpeLimitPower3.Text;
                mAmp = Regex.Replace(mAmp, "A", "");
                maxAmp = Regex.Replace(maxAmp, "A", "");
                maxAmp = maxAmp.Replace('.', ',');
                mAmp = mAmp.Replace('.', ',');
                if (Double.Parse(mAmp) > Double.Parse(maxAmp))
                {
                    OnOffPower3();
                    MessageBox.Show("Điện áp vượt quá giá trị cho phép.");
                }
            }


        }

        private void lblVonLimitPower4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
