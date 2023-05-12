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
using System.Security.Cryptography;


namespace Hash
{
    public partial class SHA3Form : Form
    {
        public SHA3Form()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string toHash = textBox1.Text;
            byte[] data = Encoding.UTF8.GetBytes(toHash);
            byte[] hashBytes = SHA3.ComputeHash(data);
            string hash = hashBytes.ToHexString();
            MessageBox.Show("Ваш хэш записан в текстовый файл hash.txt", "Оповещение");

            using (StreamWriter sw = new StreamWriter("hash.txt", true))
            {
                sw.WriteLine("SHA3");
                sw.WriteLine("Исходная строка: " + toHash);
                sw.WriteLine("Хэш: " + hash);
                sw.WriteLine();
            }
        }
    }
}
