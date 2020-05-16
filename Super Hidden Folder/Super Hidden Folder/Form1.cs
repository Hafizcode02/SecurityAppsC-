using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Super_Hidden_Folder
{
    public partial class Form1 : Form
    {
        private void progressbarloading()
        {
            LoadingBar.Value = 5;
            LoadingBar.Value = 10;
            LoadingBar.Value = 15;
            LoadingBar.Value = 20;
            LoadingBar.Value = 25;
            LoadingBar.Value = 30;
            LoadingBar.Value = 35;
            LoadingBar.Value = 40;
            LoadingBar.Value = 45;
            LoadingBar.Value = 50;
            LoadingBar.Value = 55;
            LoadingBar.Value = 60;
            LoadingBar.Value = 65;
            LoadingBar.Value = 70;
            LoadingBar.Value = 75;
            LoadingBar.Value = 80;
            LoadingBar.Value = 85;
            LoadingBar.Value = 90;
            LoadingBar.Value = 95;
            LoadingBar.Value = 100;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listboxlog.ForeColor = System.Drawing.Color.Red;
            try
            {
                string[] items = File.ReadAllLines(@"C:\\sfhlogs.hafcode");
                listboxlog.Items.Clear();
                listboxlog.Items.AddRange(items);
                listboxlog.SelectedIndex = 0;
            }
            catch (System.Exception)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textboxPath2.Text == string.Empty)
            {
                MessageBox.Show("textbox can not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                //membuat suatu perintah dengan bantuan script bat
                string[] lines = { "attrib +h +s " + textboxPath2.Text };

                //merekap perintah diatas dan menyimpannya menjadi sebuah file extensi .bat
                System.IO.File.WriteAllLines(@"C:\Hide.bat", lines);

                //mengeksekusi file yang telah dibuat di drive c
                Process.Start("C:\\Hide.bat");

                //menampilkan loading progressbar
                progressbarloading();

                //menampilkan kotak pesan
                MessageBox.Show("Hide Folder in " + textBoxPath1.Text + "Success", "Super Folder Hidden", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //merekap informasi dan menyimpannya kedalam listbox
                listboxlog.Items.Add(textboxPath2.Text + " Hidden on " + DateTime.Now.ToString());

                //Menghapus file bat yang berada di drive c
                System.IO.File.Delete(@"C:\Hide.bat");
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void btn_browse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPath1.Text = folderBrowserDialog1.SelectedPath;
                textboxPath2.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btn_unhide_Click(object sender, EventArgs e)
        {
            //membuat perintah diatas dengan bantuan script .bat, perintah attrib digunakan untuk menampilkan atau memodifikasi sebuah attribut file
            string[] lines = { "attrib -h -s" + textboxPath2.Text };

            //merekap perintah diatas dan menyimpannya menjadi sebuah file dengan ektensi .bat untuk dieksekusi lebih lanjut
            System.IO.File.WriteAllLines(@"C:\unHide.bat", lines);

            //mengekseksi file bat yang telah dibuat di drive c
            Process.Start("C:\\unHide.bat");

            //menampilkan progressbar loading
            progressbarloading();

            //tampilkan kotak pesan
            MessageBox.Show("Unhide Folder in " + textboxPath2.Text + "Success", "Super Folder Hidden", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //merekap informasi menyimpannya ke dalam listbox
            listboxlog.Items.Add(textboxPath2.Text + "un Hidden on" + DateTime.Now.ToString());

            //menghapus file bat yang berada di drive c
            System.IO.File.Delete(@"C:\unHide.bat");
            System.IO.File.Delete(@"C:\Hide.bat");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\\sfhlogs.hafcode");
                foreach (object item in listboxlog.Items)
                {
                    sw.WriteLine(item.ToString());
                    sw.Close();
                }
            }
            catch (System.Exception)
            {

            }
        }
    }
}
