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

namespace Z119.ATK.Shell
{
    public partial class fOxiloForm : Form
    {
        private Thread oscilloThread;
        private int[] dataArray = new int[600];
        private int[] dataArrayOld = new int[600];
        string deviceName;
        string dataLabel1 = "Data";
        string dataLabel2 = "Reference data";
        public fOxiloForm()
        {
            
            InitializeComponent();
            chart1.ChartAreas[0].AxisY.Maximum = 125;
            chart1.ChartAreas[0].AxisY.Minimum = -125;
            listBoxDevices.Items.Clear();
            SharpVisaCLI.Program.List((inst) => { listBoxDevices.Items.Add(inst); });
            listBoxDevices.SetSelected(0,true);
            StartConnection();
            //chart1.Series["dataReference"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            //Random r = new Random();
            //for (int i = 0; i < 100; i++)
            //{
            //    dataArrayOld[i] = r.Next(0, 1000);
            //}

            //chart1.Series["Series2"].Points.Clear();
            //for (int i = 0; i < dataArrayOld.Length - 1; i++)
            //{
            //    chart1.Series["Series2"].Points.AddY(dataArrayOld[i]);
            //}
            
        }

        
        private void getOscilloData()
        {
            
            var req = ":waveform:data?";
            
            while (true)
            {
                SharpVisaCLI.Program.Send(deviceName, req, (res) =>
                {
                    DrawData(res);
                });
            }
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
            for (int i = 0; i < dataArray.Length; i++)
            {
                chart1.Series[dataLabel1].Points.AddY(dataArray[i]);
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
    }
}
