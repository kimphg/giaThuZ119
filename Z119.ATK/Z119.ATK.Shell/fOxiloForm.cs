using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Z119.ATK.Common;

namespace Z119.ATK.Shell
{
    public partial class fOxiloForm : Form
    {
        private const int xRes = 600;
        private const int yRes = 256;
        private Thread oscilloThread;
        public static int[] dataArray = new int[xRes];
        public static int[] dataArrayOld = new int[xRes];
        public static int[] dataArrayRef = new int[xRes];
        static string deviceName;
        string dataLabel1 = "Data";
        string dataLabel2 = "Reference data";
        private string dataLabel3 ="Old data";
        static int currentChanel = 1;
        volatile bool continueRead = false;
        private static string strXScale;
        private static string strYScale;
        private static volatile  bool paraChanged =  false;
        private bool manualMode = false;
        public static double MesVpp;
        public static double MesVmean;
        public fOxiloForm()
        {
            
            InitializeComponent();
            // set value range 
            chart1.ChartAreas[0].AxisY.Maximum = 127;
            chart1.ChartAreas[0].AxisY.Minimum = -127;
            //hide number labels
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
            //grid
            Grid xGrid = new Grid();
            xGrid.Interval=(xRes/12);
            chart1.ChartAreas[0].AxisX.MajorGrid = xGrid;
            Grid yGrid = new Grid();
            yGrid.Interval = (yRes / 8);
            chart1.ChartAreas[0].AxisY.MajorGrid = yGrid;
            //
            this.MdiParent = Const.mainForm;
            //listBoxDevices.Items.Clear();
            //SharpVisaCLI.Program.List((inst) => { if (inst.Contains("USB"))listBoxDevices.Items.Add(inst); });
            //listBoxDevices.SetSelected(0,true);
            StartConnection();
            this.LocationChanged+=fOxiloForm_LocationChanged;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = Z119.ATK.Common.Const.proConf.fOsciloLocation;
            this.TopMost = false;
            this.FormClosing += fOxiloForm_Onclosing;
            setProbe(1);
            SetOffset(0, 1);
            SetOffset(0, 2);
            UnlockControl();
        }

        private void fOxiloForm_LocationChanged(object sender, EventArgs e)
        {
            Z119.ATK.Common.Const.proConf.fOsciloLocation = this.Location;
        }
        private void  fOxiloForm_Onclosing(object sender, EventArgs e)
        {
            StopConnection();
        }
        public static void setReference(int[] data)
        {
            dataArrayRef = data;
        }
        public static int[] getData()
        {
           return dataArray;
        }
        
        private void getOscilloData()
        {
            var req = ":waveform:data?";
            int frameCounter = 0;
            continueRead = true;
            while(continueRead)
            {
                try
                {
                    if (manualMode)
                    {
                        SharpVisaCLI.Program.Send(deviceName, req, (res) =>
                        {
                            UpdateParamFromOscillo();
                            UnlockControl();
                            this.Invoke((MethodInvoker)delegate { Blink(2); });
                            DrawData(res);
                            Thread.Sleep(3000);
                            this.Invoke((MethodInvoker)delegate { Blink(3); });
                            Thread.Sleep(3000);
                        });
                    }
                    else
                    {
                        //this.Invoke((MethodInvoker)delegate { Blink(1); });
                        SharpVisaCLI.Program.Send(deviceName, req, (res) =>
                        {
                            DrawData(res);
                        });
                        frameCounter++;
                        if (paraChanged)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                comboBox_xscale.Text = strXScale;
                                comboBox_yscale.Text = strYScale;
                            });
                            paraChanged = false;
                            UpdateXscale();
                            UpdateYscale();
                        }
                        if (frameCounter >= 10)
                        {
                            try
                            {
                                this.Invoke((MethodInvoker)delegate { UpdateParamFromOscillo(); });
                                frameCounter = 0;
                            }
                            catch (Exception e)
                            {
                                return;
                            }

                        }
                    }
                }
                catch (Exception e)
                {
                    return;
                }
                
                

            }
            UnlockControl();
        }

        private void setOscilloParam()
        {
            
        }

        private void Blink(int p)
        {
            if (p==3)
            {
                button2.BackColor = Color.Orange;
            }
            else if (p == 2)
            {
                button2.BackColor = Color.Yellow;

            }
            else 
            {
                button2.BackColor = Color.Transparent;
            }
        }

        private void UpdateParamFromOscillo()
        {
            
            getOscilloScaleX();
            getOscilloScaleY();
            getoscilloIDN();
            Measure();
            if (!manualMode)
            {
                try
                {
                    strXScale = comboBox_xscale.Text;
                    strYScale = comboBox_yscale.Text;
                }
                catch (Exception e)
                {
                    return;
                }
            }
            
        }

        private void Measure()
        {
            SharpVisaCLI.Program.Send(deviceName, ":MEAS:VPP? CHAN1", (res) =>
            {
                if (res.ElementAt(0) == '#') return;
                //label2.Text = res.Substring(0, res.Length - 1);
                Regex rgx = new Regex("[^a-zA-Z0-9 . -]");
                res = rgx.Replace(res, "");
                if (res == "99e366")
                {
                    textBoxMes1.Text = "Thang đo không đúng";
                }
                else 
                {
                    res = res.Substring(0, res.Length - 1);
                    textBoxMes1.Text = res + " V";
                    try
                    {

                        MesVpp = Double.Parse(res, System.Globalization.NumberStyles.Float, new CultureInfo("en-US"));
                    }
                    catch (FormatException)
                    {
                    }
                }
                
                //strXScale = comboBox_xscale.Text;
            });
            SharpVisaCLI.Program.Send(deviceName, ":MEAS:VAV?", (res) =>
            {

                if (res.ElementAt(0) == '#') return;
                Regex rgx = new Regex("[^a-zA-Z0-9 . -]");
                res = rgx.Replace(res, "");
                if (res == "99e366")
                {
                    textBoxMes2.Text = "Thang đo không đúng";
                }
                else
                {
                    res = res.Substring(0, res.Length - 1);
                    textBoxMes2.Text = res + " V";
                    try
                    {
                        MesVmean = Double.Parse(res, System.Globalization.NumberStyles.Float, new CultureInfo("en-US"));
                    }
                    catch (FormatException)
                    { 
                    }
                }
                
            });
        }

        private void getoscilloIDN()
        {
            SharpVisaCLI.Program.Send(deviceName, "*IDN?", (res) =>
            {
                if (res.ElementAt(0) == '#') label2.Text = "Error: "+res;
                else label2.Text = res.Substring(0, res.Length - 1);
                //strXScale = comboBox_xscale.Text;
            });
        }
        private void getOscilloScaleX()
        {
            try
            {
                SharpVisaCLI.Program.Send(deviceName, ":timebase:scale?", (res) =>
                {
                    if (res.ElementAt(0) == '#') return;
                    Regex rgx = new Regex("[^a-zA-Z0-9 . -]");
                    res = rgx.Replace(res, "");
                    string input = res.Substring(0, res.Length - 1);
                    try
                    {
                        double dbScaleX = Double.Parse(input, System.Globalization.NumberStyles.Float, new CultureInfo("en-US"));
                        string unitX = "s";
                        if (dbScaleX < 1.0) { dbScaleX *= 1000.0; unitX = "ms"; }
                        if (dbScaleX < 1.0) { dbScaleX *= 1000.0; unitX = "us"; }
                        this.Invoke((MethodInvoker)delegate
                        {
                            comboBox_xscale.Text = dbScaleX.ToString() + " " + unitX;
                        });
                    }
                    catch (FormatException)
                    {
                    }
                    
                });
            }
            catch (Exception e)
            { 
            }
            
        }
        private void getOscilloScaleY()
        {

            SharpVisaCLI.Program.Send(deviceName, ":CHAN"+currentChanel.ToString()+":SCAL?", (res) =>
            {
                if (res.ElementAt(0) == '#') return;
                Regex rgx = new Regex("[^a-zA-Z0-9 . -]");
                res = rgx.Replace(res, "");
                try
                {
                    string input = res.Substring(0, res.Length - 1);
                    double dbScaleY = Double.Parse(input, System.Globalization.NumberStyles.Float, new CultureInfo("en-US"));
                    string unitY = "V";
                    if (dbScaleY < 1.0) { dbScaleY *= 1000.0; unitY = "mV"; }
                    if (dbScaleY < 1.0) { dbScaleY *= 1000.0; unitY = "uV"; }
                    this.Invoke((MethodInvoker)delegate
                    {
                        comboBox_yscale.Text = dbScaleY.ToString() + " " + unitY;
                    });
                }
                catch (FormatException)
                {
                }
                 
            });

        }

        private void DrawData(string res)
        {
            byte[] data = Encoding.ASCII.GetBytes(res);
            if (data.Length >= dataArray.Length+10)
            for (int i = 0; i < dataArray.Length; i++)
            {
                dataArray[i] = data[i+10]-127;
            }
            try
            {
                this.Invoke((MethodInvoker)delegate { UpdateChart(); });
            }
            catch (Exception)
            { }
        }

        private void UpdateChart()
        {
            chart1.Series[dataLabel1].Points.Clear();
            chart1.Series[dataLabel2].Points.Clear();
            chart1.Series[dataLabel3].Points.Clear();
            for (int i = 0; i < dataArray.Length; i++)
            {
                chart1.Series[dataLabel1].Points.AddY(dataArray[i]);
                chart1.Series[dataLabel2].Points.AddY(dataArrayRef[i]);
                chart1.Series[dataLabel3].Points.AddY(dataArrayOld[i]);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            StartConnection();
        }

        private void StartConnection()
        {

            deviceName = Common.Const.proConf.oscilloCtrl;
            oscilloThread = new Thread(new ThreadStart(getOscilloData));
            oscilloThread.IsBackground = true;
            oscilloThread.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            oscilloThread.Abort();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public static void setScaleX(string scale)
        {
            strXScale = scale;
            paraChanged = true;
        }
        public static void setScaleY(string scale)
        {
            strYScale = scale;
            paraChanged = true;
        }
        public static void sendScaleX(string str)
        {
            string req = ":TIM" + ":SCAL " + str;
            SharpVisaCLI.Program.Send(deviceName, req, null);
        }
        public static string getScaleX()
        {
            return strXScale;
        }
        public static string getScaleY()
        {
            return strYScale;
        }
        public static void sendScaleY(string str)
        {
            string chanel = currentChanel.ToString();
            string req = ":CHAN" + chanel + ":SCAL " + str;
            SharpVisaCLI.Program.Send(deviceName, req, null);
        }
        static void setProbe(int mult)
        {
           // :CHAN2:PROB 10 
            string chanel = currentChanel.ToString();
            string req = ":CHAN" + chanel + ":PROB " + mult.ToString();
            SharpVisaCLI.Program.Send(deviceName, req, null);
        }
        private void comboBox_xscale_SelectedIndexChanged(object sender, EventArgs e)
        {
            strXScale = comboBox_xscale.Text;
            UpdateXscale(); ;
        }

        private void UpdateXscale()
        {
            try
            {
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";
                if (strXScale.Split(' ').Count() < 2) strXScale = "100 us";
                double val = Double.Parse(strXScale.Split(' ')[0]);
                string unit = strXScale.Split(' ')[1];
                if (unit == "ms") val /= 1000.0;
                else if (unit == "us") val /= 1000000.0;
                sendScaleX(val.ToString(nfi));
            }
            catch (FormatException)
            {
            }
        }
        private void UpdateYscale()
        {
            try
            {
                NumberFormatInfo nfi = new NumberFormatInfo();
                nfi.NumberDecimalSeparator = ".";
                if (strYScale.Split(' ').Count() < 2) strYScale = "1 V";
                double val = Double.Parse(strYScale.Split(' ')[0]);
                string unit = strYScale.Split(' ')[1];
                if (unit == "mV") val /= 1000.0;
                sendScaleY(val.ToString(nfi));
            }
            catch (FormatException)
            {
            }
        }
        private void comboBox_yscale_SelectedIndexChanged(object sender, EventArgs e)
        {
            strYScale = comboBox_yscale.Text;
            UpdateYscale();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            manualMode = true;
            //StopConnection();
        }

        private void LockControl()
        {
            string req = ":KEY:LOCK DISABLE";
            SharpVisaCLI.Program.Send(deviceName, req, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            manualMode = false;
            Blink(1);
            UpdateParamFromOscillo();
            //UnlockControl();
            //StartConnection();
        }
        private void SetOffset(double value,int chanel)
        {
            string val = value.ToString();
            string req = ":CHAN" + chanel.ToString() + ":OFFS " + val;
            SharpVisaCLI.Program.Send(deviceName, req, null);
             
        }
        private void UnlockControl()
        {
            string req = ":KEY:LOCK DISABLE";
            SharpVisaCLI.Program.Send(deviceName, req, null);
        }

        private void StopConnection()
        {
            continueRead = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string req = textBox1.Text;
            SharpVisaCLI.Program.Send(deviceName, req, (res) =>
            {

                this.Invoke((MethodInvoker)delegate
                {
                    textBox2.Text = res;
                });
                
            });
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        
    }
}
