using System;
using System.Windows.Forms;

namespace LiveChartTest
{
    public partial class Form1 : Form
    {
        private float[] cpuData = new float[20];
        private float[] ramData = new float[20];
        private float threshold = 5.0f;

        public Form1()
        {
            InitializeComponent();
            timer1.Tick += Timer1_Tick;
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
            cpuData[19] = cpuCounter.NextValue();
            ramData[19] = ramCounter.NextValue();
            chart1.Series[0].Points.DataBindY(cpuData);
            chart1.Series[1].Points.DataBindY(ramData);
            chart1.Series[0].LegendText = String.Format("CPU: {0:00.00}%", cpuData[19]);
            chart1.Series[1].LegendText = String.Format("RAM: {0:00.00}%", ramData[19]);

            if (cpuData[19] > threshold)
            {
                textBox1.AppendText(String.Format("[{0:r}] CPU threshold exceeded: {1:00.00}%\n", System.DateTime.Now, cpuData[19]));
            }
        }
    }
}
