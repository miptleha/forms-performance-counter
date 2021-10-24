using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace performance_counter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        PerformanceCounter pcMem;
        PerformanceCounter pcMem2;
        PerformanceCounter pcCpu;
        Timer timer;

        private void Form1_Load(object sender, EventArgs e)
        {
            pcCpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            pcMem = new PerformanceCounter("Memory", "Available MBytes");
            pcMem2 = new PerformanceCounter("Memory", "% Committed Bytes In Use");

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Enabled = true;
        }

        double total = 0;

        private void Timer_Tick(object sender, EventArgs e)
        {
            var sw = Stopwatch.StartNew();

            int cpu = (int)Math.Round((decimal)pcCpu.NextValue());
            float mem = pcMem.NextValue();
            int mem2 = (int)Math.Round((decimal)pcMem2.NextValue());
            
            sw.Stop();

            lCpu.Text = $"{cpu} %";
            pbCpu.Value = cpu;

            lMem.Text = $"{mem} MBytes";
            lMem2.Text = $"{mem2}%";

            double ms = sw.Elapsed.TotalMilliseconds;
            total += ms;
            lStatus.Text = $"Done in {ms.ToString("0.###")} ms. Total: {total.ToString("0.###")} ms";
        }
    }
}
