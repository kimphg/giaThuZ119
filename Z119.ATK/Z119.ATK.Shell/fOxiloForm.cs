﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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
        string deviceName;
        string dataLabel1 = "Data";
        string dataLabel2 = "Reference data";
        private string dataLabel3 ="Old data";
        int currentChanel = 1;
        public fOxiloForm()
        {
            
            InitializeComponent();
            chart1.ChartAreas[0].AxisY.Maximum = 127;
            chart1.ChartAreas[0].AxisY.Minimum = -127;
            //grid
            Grid xGrid = new Grid();
            xGrid.Interval=(xRes/12);
            chart1.ChartAreas[0].AxisX.MajorGrid = xGrid;
            Grid yGrid = new Grid();
            yGrid.Interval = (yRes / 8);
            chart1.ChartAreas[0].AxisY.MajorGrid = yGrid;
            //
            listBoxDevices.Items.Clear();
            SharpVisaCLI.Program.List((inst) => { if (inst.Contains("USB"))listBoxDevices.Items.Add(inst); });
            listBoxDevices.SetSelected(0,true);
            StartConnection();
            this.LocationChanged+=fOxiloForm_LocationChanged;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = Z119.ATK.Common.Const.proConf.fOsciloLocation;
            this.TopMost = true;
        }

        private void fOxiloForm_LocationChanged(object sender, EventArgs e)
        {
            Z119.ATK.Common.Const.proConf.fOsciloLocation = this.Location;
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
            while (true)
            {
                SharpVisaCLI.Program.Send(deviceName, req, (res) =>
                {
                    DrawData(res);
                });
                frameCounter++;
                if (frameCounter >= 10)
                {
                    
                    this.Invoke((MethodInvoker)delegate { UpdateData(); });
                }

            }
        }

        private void UpdateData()
        {
            getOscilloScaleX();
            getOscilloScaleY();
        }
        private void getOscilloScaleX()
        {

            SharpVisaCLI.Program.Send(deviceName, ":timebase:scale?", (res) =>
            {
                label_timeScale.Text = res;
            });
            
        }
        private void getOscilloScaleY()
        {

            SharpVisaCLI.Program.Send(deviceName, ":CHAN"+currentChanel.ToString()+":SCAL?", (res) =>
            {
                label_yScale.Text = res;
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
            deviceName = (string)listBoxDevices.SelectedItem;
            StartConnection();
        }

        private void StartConnection()
        {
            
            deviceName = (string)listBoxDevices.SelectedItem;
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

        private void comboBox_xscale_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = comboBox_xscale.SelectedItem.ToString();
            double val = Double.Parse(str.Split(' ')[0]);
            string unit = str.Split(' ')[1];
            if (unit == "ms") val /= 1000.0;
            else if (unit == "us") val /= 1000000.0;
            string req = ":TIM"  + ":SCAL: " + val.ToString("e");
            SharpVisaCLI.Program.Send(deviceName, req, null);
            
        }

        private void comboBox_yscale_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = comboBox_xscale.SelectedItem.ToString();
            double val = Double.Parse(str.Split(' ')[0]);
            string unit = str.Split(' ')[1];
            if (unit == "mV") val /= 1000.0;
            string chanel = currentChanel.ToString();
            string req = ":CHAN" + chanel + ":SCAL: " + val.ToString("e");
            SharpVisaCLI.Program.Send(deviceName, req, null);
        }
    }
}
