using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace CS031MereniAlgoritmuProhozeni
{
    public partial class MereniAlgoritmuProhozeniForm : Form
    {
        public MereniAlgoritmuProhozeniForm()
        {
            InitializeComponent();
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            StopkyTestDateTime();
            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(Environment.NewLine);
            StopkyTestHighResolutionDateTime();
            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(Environment.NewLine);
            StopkyTestStopwatchHashSet();
            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(Environment.NewLine);
            StopkyTestStopwatch();
            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(Environment.NewLine);
            StopkyTestStopwatchDrift();
            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(Environment.NewLine);
        }

        private void StopkyTestDateTime() {

            vystupTextBox.AppendText("Testing DateTime for 1 seconds...");

            var distinctValues = new HashSet<DateTime>();
            var sw = Stopwatch.StartNew();

            while (sw.Elapsed.TotalSeconds < 1)
            {
                distinctValues.Add(DateTime.UtcNow);
            }

            sw.Stop();

            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(
                string.Format(
                    "Precision: {0:0.000000} ms ({1} samples)",
                    sw.Elapsed.TotalMilliseconds / distinctValues.Count,
                    distinctValues.Count));

        }

        private void StopkyTestHighResolutionDateTime()
        {

            vystupTextBox.AppendText("Testing High Resolution DateTime for 1 seconds...");

            var distinctValues = new HashSet<DateTime>();
            var sw = Stopwatch.StartNew();

            while (sw.Elapsed.TotalSeconds < 1)
            {
                distinctValues.Add(HighResolutionDateTime.UtcNow);
            }

            sw.Stop();

            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(
                string.Format(
                    "Precision: {0:0.000000} ms ({1} samples)",
                    sw.Elapsed.TotalMilliseconds / distinctValues.Count,
                    distinctValues.Count));

        }

        private void StopkyTestStopwatchHashSet()
        {

            vystupTextBox.AppendText("Testing Stopwatch for 1 seconds...");

            var distinctValues = new HashSet<long>();
            var sw = Stopwatch.StartNew();

            while (sw.Elapsed.TotalSeconds < 1)
            {
                distinctValues.Add(sw.ElapsedTicks);
            }

            sw.Stop();

            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(
                string.Format(
                    "Resolution: {0:0.000000} ms ({1} samples)",
                    sw.Elapsed.TotalMilliseconds / distinctValues.Count,
                    distinctValues.Count));

        }

        private void StopkyTestStopwatch()
        {

            vystupTextBox.AppendText("Testing Stopwatch for 1 seconds...");

            var sw = Stopwatch.StartNew();

            while (sw.Elapsed.TotalSeconds < 1)
            {
                
            }

            sw.Stop();

            vystupTextBox.AppendText(Environment.NewLine);
            vystupTextBox.AppendText(
                string.Format(
                    "Resolution: {0:0.000000} ms ({1} samples)",
                    sw.Elapsed.TotalMilliseconds / sw.ElapsedTicks,
                    sw.ElapsedTicks));

        }

        private void StopkyTestStopwatchDrift() {
            var start = HighResolutionDateTime.UtcNow;
            var sw = Stopwatch.StartNew();

            while (sw.Elapsed.TotalSeconds < 10)
            {
                DateTime nowBasedOnStopwatch = start + sw.Elapsed;
                TimeSpan diff = HighResolutionDateTime.UtcNow - nowBasedOnStopwatch;

                vystupTextBox.AppendText(string.Format("Stopwatch to UTC drift: {0:0.000000} ms", diff.TotalMilliseconds));
                vystupTextBox.AppendText(Environment.NewLine);

                Thread.Sleep(1000);
            }
        }
        private void ProhoditPromenna<T>(ref T a, ref T b)
        {
            T pom = a;
            a = b;
            b = pom;
        }
        private void ProhoditPromenna(ref int a, ref int b)
        {
            int pom = a;
            a = b;
            b = pom; 
        }
        public delegate void ProceduraProhozeni(ref int a, ref int b);

        private long OpakovatProhozeni(ProceduraProhozeni proceduraProhozeni, int n)
        {
            var sw = Stopwatch.StartNew();
            int a = 1;
            int b = 2;
            for (int i = 0; i< n; i++)
            {
                proceduraProhozeni(ref a, ref b);
            }
            return sw.ElapsedMilliseconds;
        }
        private void MeritProhozeni(int max)
        {
            VytizitProcesor();
            string vypis = "Prohození s pomocnou proměnnou {1}x: {0:0.000000} ms";
            vypis = "{0};{1:0.000000};{2:0.000000}";
            for (int i = 1; i < max; i *= 10)
            {





                vystupTextBox.AppendText(
                    string.Format(
                        vypis,
                        i,
                         OpakovatProhozeni(ProhoditPromenna, i),
                         OpakovatProhozeni(ProhoditPromenna<int>, i)));
              

                vystupTextBox.AppendText(Environment.NewLine);

            }
        }

        private void meritProhozeniButton_Click(object sender, EventArgs e)
        {
            MeritProhozeni(100000000);
        }
        private void VytizitProcesor()
        {
            OpakovatProhozeni(ProhoditPromenna,100000000);
        }
    }
}
