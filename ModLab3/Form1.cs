using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModLab3
{
    public partial class Form1 : Form
    {
        Queue<double> queue = new Queue<double>();
        int qLen = 10;

        List<double> list = new List<double>();
        Random rnd = new Random();
        int count=0;
        int count1 = 0;
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            timer1 = new System.Windows.Forms.Timer();
            timer2 = new System.Windows.Forms.Timer();
            timer3 = new System.Windows.Forms.Timer();

            timer1.Tick += new EventHandler(timer1_Tick);
            timer2.Tick += new EventHandler(timer2_Tick);
            timer3.Tick += new EventHandler(timer3_Tick);

            timer1.Start();
            timer2.Start();
            timer3.Start();

            Task.Run(() =>
            {   Stopwatch sw = new Stopwatch();
                sw.Start();
                while (sw.Elapsed.TotalSeconds < int.Parse(textBox1.Text)){int c = 0;c++;}
                sw.Stop();
                timer1.Stop();
                timer2.Stop();
                timer3 .Stop();
            });
        }

        public static int getPoisson(double lambda)
        {
            Random rnd = new Random();
            double L = Math.Exp(-lambda);
            double p = 1.0;
            int k = 0;
            do
            {
                k++;
                p *= rnd.NextDouble();
            } while (p > L);
            return k-1;
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = rnd.Next(300,500);
            queue.Enqueue(rnd.NextDouble());
            if (queue.Count > qLen)
            {
                qLen = queue.Count;
            }
            if (queue.Count != 0)
            {
                chart1.Series[0].Points.AddXY(count1, qLen);
                chart1.Series[1].Points.AddXY(count1, queue.Count);
                chart1.Series[2].Points.AddXY(count1, list.Count);
                count++;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = getPoisson(rnd.Next(400, 600));

            if (queue.Count != 0)
            {
                queue.Dequeue();
            }
            if (queue.Count > 10)
            {
                list.Add(1);
            }
        }

        private void Form1_Load(object sender, EventArgs e){
            comboBox1.Items.AddRange(new object[] { 100, 200, 500, 1000 });
        }
        private void progressBar1_Click(object sender, EventArgs e){}
        private void chart1_Click(object sender, EventArgs e) {}

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Interval = Convert.ToInt32(comboBox1.Text);
            if (queue.Count > qLen)
            {
                qLen = queue.Count;
            }
            if (queue.Count != 0)
            {
                chart2.Series[0].Points.AddXY(count, qLen);
                chart2.Series[1].Points.AddXY(count, queue.Count);
                chart2.Series[2].Points.AddXY(count, list.Count);
                count++;
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e){}
        private void textBox1_TextChanged(object sender, EventArgs e){}
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            queue.Clear();
            list.Clear();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();
        }
    }
}
