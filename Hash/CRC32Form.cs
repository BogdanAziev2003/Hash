using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hash
{
    public partial class CRC32Form : Form
    {
        CRC32 crc32 = new CRC32();

        public CRC32Form()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string toHash = textBox1.Text;
            byte[] data = Encoding.UTF8.GetBytes(toHash);
            string hash = Convert.ToString(crc32.ComputeChecksum(data));
            MessageBox.Show("Ваш хэш записан в текстовый файл hash.txt", "Оповещение");

            using (StreamWriter sw = new StreamWriter("hash.txt", true))
            {
                sw.WriteLine("CRC32");
                sw.WriteLine("Исходная строка: " + toHash);
                sw.WriteLine("Хэш: " + hash);
                sw.WriteLine();
            }
        }
    }
}
