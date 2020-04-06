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

namespace timer
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		System.Threading.Timer timerpicbox1;
		System.Threading.Timer timerpicbox2;

		private void button1_Click(object sender, EventArgs e)
		{
			timerpicbox1.Dispose();
			timerpicbox2.Dispose();
			richTextBox1.AppendText("Остановка работы /n");
			richTextBox2.AppendText("Остановка работы /n");
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//попробуем потоки...
			//первая картинка
			TimerCallback datawrite = new TimerCallback(DataWrite);
			string data = "";

			//вторая картинка
			TimerCallback datawrite1 = new TimerCallback(DataWrite1);
			string data1 = "";

			//таймер timerpicbox1 создает новый поток для datawrite
			timerpicbox1 = new System.Threading.Timer(datawrite, data, 0, 2000);

			//таймер timerpicbox2 создает второй поток для datawrite2
			timerpicbox2 = new System.Threading.Timer(datawrite1, data1, 0, 5000);

			//классический формовский таймер, думал сделать через него, но не вышло
			//System.Windows.Forms.Timer tmr = new System.Windows.Forms.Timer();
			//tmr.Interval = 2000;
			//tmr.Tick += timer1_Tick;
			//tmr.Start();
		}

		public delegate void AddMessageDelegate(string message);

		public void DataWrite(object data)
		{
			Invoke(new AddMessageDelegate(firstpicbox), new object[] { data });
		}
		public void DataWrite1(object data)
		{
			Invoke(new AddMessageDelegate(secondpicbox), new object[] { data });
		}
		public void firstpicbox(object data)
		{
			pictureBox1.Visible = !pictureBox1.Visible;

			//это для нумерации строк в textbox'e... забей...
			//string[] buf = richTextBox1.Lines;
			//for (int i = 0; i < richTextBox1.Lines.Length; i++)
			//{
			//	buf[i] = (i).ToString() + ". " + buf[i];
			//}
			//richTextBox1.Lines = buf;

			richTextBox1.AppendText(">>> " + DateTime.Now.ToString("h:mm:ss.fff") + "\n");
		}

		public void secondpicbox(object data)
		{
			if (!pictureBox1.Visible)
			{
				pictureBox2.Visible = !pictureBox2.Visible;
				richTextBox2.AppendText(">>> " + DateTime.Now.ToString("h:mm:ss.fff") + "\n");
			}
			else
			{
				pictureBox2.Visible = !pictureBox2.Visible;
				richTextBox2.AppendText(">>> " + DateTime.Now.ToString("h:mm:ss.fff") + "\n");
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			//pictureBox1.BackColor = pictureBox1.BackColor == Color.Red ? Color.FromName("Control") : Color.Red;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Form1_Load(sender, e);
		}

		//public void kostil(object data)
		//{
		//	pictureBox1.Visible = false;
		//}
	}
}
