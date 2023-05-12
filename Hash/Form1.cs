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

namespace Hash
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Info infoForm = new Info();
            this.Hide();
            infoForm.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MD5Form md5 = new MD5Form();
            this.Hide();
            md5.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SHA3Form sha3 = new SHA3Form();
            this.Hide();
            sha3.ShowDialog();
            this.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CRC32Form crc32 = new CRC32Form();
            this.Hide();
            crc32.ShowDialog();
            this.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            try
            {

                startInfo.FileName = "hash.txt";

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
