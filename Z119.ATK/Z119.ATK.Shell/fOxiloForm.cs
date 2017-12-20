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
        private Thread cpuThread;
        private int[] cpuArray = new int[100];
        private int[] cpuArray1 = new int[100];
        public fOxiloForm()
        {
            InitializeComponent();

            chart1.Series["Series1"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                cpuArray1[i] = r.Next(0, 1000);
            }

            chart1.Series["Series2"].Points.Clear();
            for (int i = 0; i < cpuArray1.Length - 1; i++)
            {
                chart1.Series["Series2"].Points.AddY(cpuArray1[i]);
            }
        }

        int n = 0;
        private void getPerformanceCounters()
        {
            //var cpuCounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
            while (true)
            {
                Random r = new Random();


                for (int i = 0; i < 100; i++)
                {
                    cpuArray[i] = r.Next(0, 1000);
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
            for (int i = 0; i < cpuArray.Length - 1; i++)
            {
                chart1.Series["Series1"].Points.AddY(cpuArray[i]);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
                
            cpuThread = new Thread(new ThreadStart(getPerformanceCounters));
            cpuThread.IsBackground = true;
            cpuThread.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            cpuThread.Abort();
        }
    }
}
