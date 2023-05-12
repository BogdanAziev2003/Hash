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

    public partial class Info : Form
    {
        ProcessStartInfo startInfo = new ProcessStartInfo();

        public Info()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "algoritms\\MD5.html";

                Process.Start(startInfo);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                startInfo.FileName = "algoritms\\SHA3.html";

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                startInfo.FileName = "algoritms\\CRC.html";

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
