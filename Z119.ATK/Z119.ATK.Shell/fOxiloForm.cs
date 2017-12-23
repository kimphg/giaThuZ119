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
        private int[] dataArray = new int[800];
        private int[] dataArrayOld = new int[100];
        string dataLabel1 = "data";
        string dataLabel2 = "dataReference";
        public fOxiloForm()
        {
            
            InitializeComponent();

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

        int n = 0;
        private void getOscilloData()
        {
            listBoxDevices.Items.Clear();
            SharpVisaCLI.Program.List((inst) => { listBoxDevices.Items.Add(inst); });
            while (true)
            {
                Random r = new Random();


                for (int i = 0; i < 100; i++)
                {
                    dataArray[i] = r.Next(0, 1000);
                }

                if (chart1.IsHandleCreated)
                {
                    try
                    {
                        this.Invoke((MethodInvoker)delegate { UpdateCpuChart(); });
                    }
                    catch (Exception)
                    { }

                }
                else
                {

                }

                Thread.Sleep(100);
            }
        }

        private void UpdateCpuChart()
        {
            chart1.Series["Series1"].Points.Clear();
            for (int i = 0; i < dataArray.Length - 1; i++)
            {
                chart1.Series["Series1"].Points.AddY(dataArray[i]);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
                
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
