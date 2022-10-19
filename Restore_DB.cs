using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SMARTMRT
{
    public partial class Restore_DB : Telerik.WinControls.UI.RadForm
    {
        public Restore_DB()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();     //connection class
        String path = "";

        private void Restore_DB_Load(object sender, EventArgs e)
        {
            RadMessageBox.SetThemeName("FluentDark");   //set theme for message theme
            this.CenterToScreen();

            //set grid color
            dgvbackup.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            this.dgvbackup.DefaultCellStyle.ForeColor = Color.Black;
            dgvbackup.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgvbackup.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvbackup.EnableHeadersVisualStyles = false;

            //get all years
            SqlDataAdapter sda = new SqlDataAdapter("select distinct V_YEAR from BACKUP_FILES", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbyear.Items.Add(dt.Rows[i][0].ToString());
            }

            cmbyear.Text = "--SELECT--";
        }

        private void Restore_DB_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();   //open connection
        }

        private void cmbyear_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            cmbmonth.Items.Clear();

            //get all month for the year
            SqlDataAdapter sda = new SqlDataAdapter("select distinct V_MONTH from BACKUP_FILES where V_YEAR='" + cmbyear.Text + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbmonth.Items.Add(dt.Rows[i][0].ToString());
            }

            cmbmonth.Text = "--SELECT--";
        }

        private void cmbmonth_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            dgvbackup.Rows.Clear();

            //get all the for that month
            SqlDataAdapter sda = new SqlDataAdapter("select V_FILE from BACKUP_FILES where V_YEAR='" + cmbyear.Text + "' and V_MONTH='" + cmbmonth.Text + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvbackup.Rows.Add(dt.Rows[i][0].ToString());
            }
        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            //confirm box restoring db 
            DialogResult result = MessageBox.Show("Applying Changes will Restart the GUI?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (!result.Equals(DialogResult.OK))
            {
                return;
            }
            if (dgvbackup.SelectedRows.Count <= 0)
            {
                radLabel2.Text = "Select a file";
            }

            //start restore
            String temp = Restore(cmbyear.Text, cmbmonth.Text, dgvbackup.SelectedRows[0].Cells[0].Value.ToString());
            if (temp == "TRUE")
            {
                radLabel2.Text = "Restore Complete";
                Application.Restart();
            }
            else
            {
                radLabel2.Text = "Error on Restore";
            }
        }

        //http request to restore db
        public String Restore(String year, String month, String file)
        {
            try
            {
                string postData = "";
                string URL = "http://" + Database_Connection.GET_SERVER_IP + ":8091/RESTORE_DB/" + year + "/" + month + "/" + file;
                var data = "";

                data = webGetMethod(postData, URL);
                if (data.Contains("TRUE"))
                {
                    return ("TRUE");
                }
                else
                {
                    return ("FALSE");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ("");
        }

        public String webGetMethod(String postData, String URL)
        {
            try
            {
                //GET Method
                string html = string.Empty;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return "";
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //get the backup file 
            if (dgvbackup.SelectedRows.Count <= 0)
            {
                radLabel2.Text = "Select a file";
            }

            try
            {
                //select folder to save the download file
                path = "";
                FolderBrowserDialog folderDlg = new FolderBrowserDialog();
                folderDlg.ShowNewFolderButton = true;

                // Show the FolderBrowserDialog.  
                DialogResult result = folderDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    path = folderDlg.SelectedPath;
                    Environment.SpecialFolder root = folderDlg.RootFolder;
                }

                if (path != "")
                {
                    //thread to download backfile 
                    Thread clock = new Thread(backupthread);
                    clock.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //thread to download backup file
        public void backupthread()
        {
            GETFILE(cmbyear.Text, cmbmonth.Text, dgvbackup.SelectedRows[0].Cells[0].Value.ToString(), path);
        }

        //http request to download the backup file from the pms server
        public void GETFILE(String year, String month, String file, String path)
        {
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create("http://" + Database_Connection.GET_SERVER_IP + ":8091/GET_BACKUPFILE/" + year + "/" + month + "/" + file);
                httpRequest.Method = WebRequestMethods.Http.Get;

                HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();

                Stream httpResponseStream = httpResponse.GetResponseStream();

                //convert stream to file
                int bufferSize = 10240;
                byte[] buffer = new byte[bufferSize];
                int bytesRead = 0;

                FileStream fileStream = File.Create(path + "\\" + file);
                while ((bytesRead = httpResponseStream.Read(buffer, 0, bufferSize)) != 0)
                {
                    fileStream.Write(buffer, 0, bytesRead);
                }

                RadMessageBox.Show("Backup File Saved At : " + path + "\\" + file, "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Info);
            }
            catch(Exception ex)
            {
                RadMessageBox.Show(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void radLabel2_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            panel1.Visible = true;
            MyTimer.Start();
        }

        System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel2.Text = "";
            panel1.Visible = false;
            MyTimer.Stop();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            //download via browser
            if (dgvbackup.SelectedRows.Count <= 0)
            {
                radLabel2.Text = "Select a file";
            }
            System.Diagnostics.Process.Start("http://" + Database_Connection.GET_SERVER_IP + ":8091/GET_BACKUPFILE/" + cmbyear.Text + "/" + cmbmonth.Text + "/" + dgvbackup.SelectedRows[0].Cells[0].Value.ToString() + "");
        }
    }
}
