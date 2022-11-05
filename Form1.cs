using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LSAApp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.Add("Seç");
            comboBox1.Items.Add("Finans");
            comboBox1.Items.Add("Spor");
            comboBox1.Items.Add("Kültür");
            comboBox1.Items.Add("Ozet");
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Lütfen bir değer seçiniz");
                return;
            }

            string testveri = label2.Text;
            int count = testveri.Split('.').ToList().Count();
            Document doc = new Document(testveri, count);
            label1.Text = LSA.LSACalculate(doc);
            MessageBox.Show("İşlem tamam");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    label2.Text = Values.Finans;
                    break;
                case 2:
                    label2.Text = Values.Spor;
                    break;
                case 3:
                    label2.Text = Values.Kultur;
                    break;
                case 4:
                    label2.Text = Values.Ozet1;
                    break;
                default:
                    break;
            }
        }
    }

}
