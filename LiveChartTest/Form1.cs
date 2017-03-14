using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace LiveChartTest
{
    public partial class Form1 : Form
    {
        private float[] cpuData = new float[20];
        private float[] ramData = new float[20];
        private PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private PerformanceCounter ramCounter = new PerformanceCounter("Memory", "Available MBytes");

        public Form1()
        {
            InitializeComponent();
            timer1.Tick += new EventHandler(Timer1_Tick);
            chart1.Series[0].Points.DataBindY(cpuData);
            chart1.Series[1].Points.DataBindY(ramData);
        }

        private void Timer1_Tick(object Sender, EventArgs e)
        {
            //shift old data to the left.
            for (int i = 0; i < 19; i++)
            {
                cpuData[i] = cpuData[i + 1];
                ramData[i] = ramData[i + 1];
            }

            cpuCounter.NextValue();
            ramCounter.NextValue();
            Thread.Sleep(500);
            cpuData[19] = cpuCounter.NextValue();
            ramData[19] = ramCounter.NextValue();
            
            
            
            chart1.Series[0].Points.DataBindY(cpuData);
            chart1.Series[1].Points.DataBindY(ramData);
        }
    }
}
