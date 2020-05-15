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
using System.Security.AccessControl;

namespace Folder_Lock
{
    public partial class lockFolder : Form
    {
        private void progressBarLoading()
        {
            progressBar.Value = 10;
            progressBar.Value = 20;
            progressBar.Value = 30;
            progressBar.Value = 40;
            progressBar.Value = 50;
            progressBar.Value = 60;
            progressBar.Value = 70;
            progressBar.Value = 80;
            progressBar.Value = 90;
            progressBar.Value = 100;
        }

        public lockFolder()
        {
            InitializeComponent();
            urlBox1.Text = "D:\\Folder";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Btn_CreateFolder_Click(object sender, EventArgs e)
        {
            /* Create Folder Button */
            /* The First Code is to Make a Folder */
            Directory.CreateDirectory(urlBox1.Text);
            urlBox2.Text = urlBox2.Text;

            /* Displaying ProgressBar Loading Effect */
            progressBarLoading();

            /* to Show Message */
            MessageBox.Show(
                "Folder Created & Ready to Lock",
                "Lock Folder",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            /* Reset ProgresBar*/
            progressBar.Value = 0;
        }

        private void Btn_Browse_Click(object sender, EventArgs e)
        {
            /* Browse Folder Button */
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                urlBox2.Text = folderBrowserDialog1.SelectedPath;
                progressBar.Value = 0;
            }
        }

        private void btn_Lock_Click(object sender, EventArgs e)
        {
            try
            {
                /* folder Path */
                String folderPath = urlBox2.Text;
                /* get Log on Windows Username */
                String adminUsername = Environment.UserName;

                /* get Access Control to path Folder*/
                DirectorySecurity dSecurity = Directory.GetAccessControl(folderPath);
                /* configure Access Control*/
                FileSystemAccessRule fSAR = new FileSystemAccessRule(
                    adminUsername, FileSystemRights.FullControl, AccessControlType.Deny);
                /* adding permission ACL(Access Control List) in directory */
                dSecurity.AddAccessRule(fSAR);
                /* set access control */
                Directory.SetAccessControl(folderPath, dSecurity);

                /* run progressBar */
                progressBarLoading();
                /* show Message */
                MessageBox.Show(
                    "Folder Successfully Locked", "Folder Lock",
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                    );
                /* reset progressbar */
                progressBar.Value = 0;

                /* adding Log Information*/
                logBox.Items.Add(urlBox2.Text + "Locked on : ");
                logBox.Items.Add(DateTime.Now.ToString());
                logBox.Items.Add("");

                /* Clear listBox */
                urlBox2.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Unlock_Click(object sender, EventArgs e)
        {
            try
            {
                string folderPath = urlBox2.Text;
                string adminUsername = Environment.UserName;
                DirectorySecurity dSecurity = Directory.GetAccessControl(folderPath);

                /* get Access Control to path Folder*/
                DirectorySecurity dSec = Directory.GetAccessControl(folderPath);
                /* configure Access Control*/
                FileSystemAccessRule fSAR = new FileSystemAccessRule(
                    adminUsername, FileSystemRights.FullControl, AccessControlType.Deny);
                /* remove access */
                dSec.RemoveAccessRule(fSAR);
                /* access control to folderpath */
                Directory.SetAccessControl(folderPath, dSec);
                /* loading progressbar */
                progressBarLoading();

                /* show Message */
                MessageBox.Show(
                    "Folder Successfully Unlocked", "Folder Lock",
                    MessageBoxButtons.OK, MessageBoxIcon.Information
                    );
                /* reset progressbar */
                progressBar.Value = 0;

                /* adding Log Information*/
                logBox.Items.Add(urlBox2.Text + "Unlocked on : ");
                logBox.Items.Add(DateTime.Now.ToString());
                logBox.Items.Add("");

                /* Clear listBox */
                urlBox2.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void createFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Btn_CreateFolder.PerformClick();
        }

        private void browseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Btn_Browse.PerformClick();
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logBox.Items.Clear();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Re-Developed by Hafiz Caniago", "About Dev", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lockFolder_Load(object sender, EventArgs e)
        {
            logBox.ForeColor = System.Drawing.Color.Blue;
            try
            {
                string[] items = File.ReadAllLines(@"C:\\logs.hafcod");
                logBox.Items.Clear();
                logBox.Items.AddRange(items);
                logBox.SelectedIndex = 0;
            }
            catch (System.Exception)
            {

            }
        }

        private void lockFolder_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\\logs.hafcod");
                foreach (object item in logBox.Items)
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
