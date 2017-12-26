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
        public static int[] dataArray = new int[600];
        public static int[] dataArrayOld = new int[600];
        public static int[] dataArrayRef = new int[600];
        string deviceName;
        string dataLabel1 = "Data";
        string dataLabel2 = "Reference data";
        private string dataLabel3 ="Old data";
        
        public fOxiloForm()
        {
            
            InitializeComponent();
            chart1.ChartAreas[0].AxisY.Maximum = 125;
            chart1.ChartAreas[0].AxisY.Minimum = -125;
            listBoxDevices.Items.Clear();
            SharpVisaCLI.Program.List((inst) => { listBoxDevices.Items.Add(inst); });
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
    }
}
