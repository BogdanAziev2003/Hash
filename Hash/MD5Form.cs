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
    public partial class MD5Form : Form
    {
        MD5 hasher = new MD5();
        public MD5Form()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string toHash = textBox1.Text;
            string hash = hasher.Hash(toHash);
            MessageBox.Show("Ваш хэш записан в текстовый файл hash.txt", "Оповещение");

            using (StreamWriter sw = new StreamWriter("hash.txt", true))
            {
                sw.WriteLine("MD5");
                sw.WriteLine("Исходная строка: " + toHash);
                sw.WriteLine("Хэш: " + hash);
                sw.WriteLine();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
