using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
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
    public partial class Employee_Logs : Telerik.WinControls.UI.RadForm
    {
        public Employee_Logs()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();  //connection class
        String controller_name = "";
        DataTable data1 = new DataTable();

        private void Employee_Logs_Load(object sender, EventArgs e)
        {
            dgvemployee.MasterTemplate.SelectLastAddedRow = false;
            dgvlogs.MasterTemplate.SelectLastAddedRow = false;
            dgvemployee.MasterView.TableSearchRow.ShowCloseButton = false;   //disable close button for search in grid
            dgvlogs.MasterView.TableSearchRow.ShowCloseButton = false;     //disable close button for search in grid
            data1.Columns.Add("EMPID");
            data1.Columns.Add("EMPNAME");
            data1.Columns.Add("STATIONID");
            data1.Columns.Add("LOGINTIME");
            select_controller();   //get the selected controller
            
            //get the employee id and first name
            SqlDataAdapter sda = new SqlDataAdapter("select V_EMP_ID,V_FIRST_NAME from EMPLOYEE where V_LOGIN_STATUS='Active'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvemployee.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
            }

            //get employee group id and group desc
            sda = new SqlDataAdapter("select V_GROUP_ID,V_GROUP_DESC from EMPLOYEE_GROUP_CATEGORY where V_STATUS='Active'", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvemployee.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
            }

            //get all prod line 
            sda = new SqlDataAdapter("select V_PROD_LINE from PROD_LINE_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbautologin.Items.Add(dt.Rows[i][0].ToString());
                cmbautologin.SelectedIndex = 0;
            }

            GetLogins();  //get login details
        }

        public void GetLogins()
        {
            //check if controller is selected
            if (controller_name == "--SELECT--" || controller_name == "")
            {
                radLabel15.Text = "Please Select a Controller";
                return;
            }

            dgvlogs.Rows.Clear();
            data1.Rows.Clear();

            //get the all the prod line
            //SqlDataAdapter sda1 = new SqlDataAdapter("select distinct V_PROD_LINE from PROD_LINE_DB", dc.con);
            //DataTable dt1 = new DataTable();
            //sda1.Fill(dt1);
            //sda1.Dispose();
            //for (int j = 0; j < dt1.Rows.Count; j++)
            //{
            //    //get the login details for each station for that line
                MySqlDataAdapter sda = new MySqlDataAdapter("SELECT EMP_ID,ACTION_ID,TIME,STN_ID,LINE_NO FROM (SELECT  EMP_ID,ACTION_ID,TIME,STN_ID,LINE_NO,row_number() over (partition BY STN_ID order by TIME DESC) row_num FROM mrt_local.employeeactions WHERE ACTION_ID = 2 OR ACTION_ID = 1) a WHERE a.row_num = 1 AND  ACTION_ID = 1 ORDER BY TIME ASC ;", dc.conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String empid = dt.Rows[i][0].ToString();
                    String action = dt.Rows[i][1].ToString();
                    String time = dt.Rows[i][2].ToString();
                    String stnid = dt.Rows[i][3].ToString();
                    String empname = "";

                    //get the employee first name 
                    SqlCommand cmd = new SqlCommand("select V_FIRST_NAME from EMPLOYEE where V_EMP_ID='" + empid + "'", dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        empname = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the group desc
                    cmd = new SqlCommand("select V_GROUP_DESC from EMPLOYEE_GROUP_CATEGORY where V_GROUP_ID='" + empid + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        empname = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the station no
                    cmd = new SqlCommand("Select  I_OUTFEED_LINE_NO, I_STN_NO_OUTFEED from STATION_DATA where I_STN_ID='" + stnid + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        stnid = sdr.GetValue(0).ToString() + "." + sdr.GetValue(1).ToString();
                    }
                    sdr.Close();

                    //check if its the login
                    if (action == "1")
                    {
                        dgvlogs.Rows.Add(empid, empname, stnid, time);
                        data1.Rows.Add(empid, empname, stnid, time);
                    }
                //}
            }
        }

        public void select_controller()
        {
            dc.OpenConnection();   //open connection
            String ipaddress = "";
            String controller = "";

            //get the ip address and port number of the selected controller
            SqlCommand cmd = new SqlCommand("select V_CONTROLLER from Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                controller = sdr.GetValue(0).ToString();
                controller_name = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the ipaddress for the controller
            cmd = new SqlCommand("select V_CLUSTER_IP_ADDRESS from CLUSTER_DB where V_CLUSTER_ID='" + controller + "'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                ipaddress = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //return if there is no ip address
            if (ipaddress == "")
            {
                controller_name = "--SELECT--";
                return;
            }

            dc.Close_Connection();   //close the connection if open
            dc.OpenMYSQLConnection(ipaddress);   //open connection
        }

        //check the head line status
        private String LineStatus(String IP, String PORT)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/Status";
                var data = "";
                data = webPostMethod(postData, URL);
                return data;
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
            }
            return ("");
        }

        //http post method
        public String webPostMethod(String postData, String URL)
        {
            String responseFromServer = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.Timeout = 2000;
            request.Credentials = CredentialCache.DefaultCredentials;

            ((HttpWebRequest)request).UserAgent ="Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)";
            request.Accept = "/";
            request.UseDefaultCredentials = true;
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);
            responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        private void radLabel15_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            panel2.Visible = true;
            MyTimer.Start();
        }

        System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel15.Text = "";
            panel2.Visible = false;
            MyTimer.Stop();
        }

        String theme = "";

        private void Employee_Logs_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();   //open connection

            //get the language and theme
            String Lang = "";
            SqlCommand cmd = new SqlCommand("SELECT Language,ThemeName FROM Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Lang = sdr.GetValue(0).ToString();
                theme = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //change grid theme
            GridTheme(theme);
        }

        //set grid theme
        public void GridTheme(String theme)
        {
            dgvemployee.ThemeName = theme;
            dgvlogs.ThemeName = theme;
        }

        private void Employee_Logs_FormClosed(object sender, FormClosedEventArgs e)
        {
            dc.Close_Connection();   //close connection on form close
        }
               

        public void RowSelected(String empid)
        {
            //get the selected employee details
            txtemployeeid.Text = empid;
            txtstationid.Text = "";
            for (int i = 0; i < dgvlogs.Rows.Count; i++)
            {
                if (dgvlogs.Rows[i].Cells[0].Value.ToString() == empid)
                {
                    txtstationid.Text = dgvlogs.Rows[i].Cells[2].Value.ToString();
                }
            }
        }

        private void dgvemployee_SelectionChanged(object sender, EventArgs e)
        {
            int row = dgvemployee.CurrentCell.RowIndex;

            if (row < 0)
            {
                return;
            }

            //check if the controller is selected
            if (controller_name == "--SELECT--" || controller_name == "")
            {
                radLabel15.Text = "Please Select a Controller";
                return;
            }

            //get the employee details
            RowSelected(dgvemployee.Rows[row].Cells[0].Value.ToString());
        }
               

        private void btnlogin_Click(object sender, EventArgs e)
        {
            //check if the controller is selected
            if (controller_name == "--SELECT--" || controller_name == "")
            {
                radLabel15.Text = "Please Select a Controller";
                return;
            }

            //check if the fields are empty
            if (txtemployeeid.Text != "" && txtstationid.Text != "")
            {             
                if (txtstationid.Text.Contains("."))
                {
                    String[] stnid = txtstationid.Text.Split('.');

                    //check if the station is autologin station
                    MySqlCommand cmd2 = new MySqlCommand("select AUTOLOGIN from stationdata where OUTFEED_LINENO='" + stnid[0] + "' and STN_NO_OUTFEED='" + stnid[1] + "'", dc.conn);
                    MySqlDataReader sdr = cmd2.ExecuteReader();
                    if (sdr.Read())
                    {
                        String autologin = sdr.GetValue(0).ToString();
                        if (autologin == "1")
                        {
                            radLabel15.Text = "Its an Auto Login Station";
                            return;
                        }
                    }
                    sdr.Close();

                    //check if the station is login station
                    cmd2 = new MySqlCommand("select AUTOLOGIN from stationdata where INFEED_LINENO='" + stnid[0] + "' and STN_NO_INFEED='" + stnid[1] + "'", dc.conn);
                    sdr = cmd2.ExecuteReader();
                    if (sdr.Read())
                    {
                        String autologin = sdr.GetValue(0).ToString();
                        if (autologin == "1")
                        {
                            radLabel15.Text = "Its an Auto Login Station";
                            return;
                        }
                    }
                    sdr.Close();

                    //check if sttaion is already logged in
                    for (int i = 0; i < dgvlogs.Rows.Count; i++)
                    {
                        if (dgvlogs.Rows[i].Cells[2].Value.ToString() == txtstationid.Text)
                        {
                            //confirm box if logout employee from the station
                            DialogResult result = MessageBox.Show("Employee " + dgvlogs.Rows[i].Cells[0].Value.ToString() + " is already Logged In in Station " + dgvlogs.Rows[i].Cells[2].Value.ToString() + ". Do you want to Logout Employee : " + dgvlogs.Rows[i].Cells[0].Value.ToString() + " and Login to Employee : " + txtemployeeid.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (result.Equals(DialogResult.Yes))
                            {
                                //logout employee
                                Logout(dgvlogs.Rows[i].Cells[0].Value.ToString(), dgvlogs.Rows[i].Cells[2].Value.ToString());
                            }
                            else
                            {
                                return;
                            }
                        }                      
                    }

                    //check if the employee is already loggen in other station
                    for (int i = 0; i < dgvlogs.Rows.Count; i++)
                    {
                        if (dgvlogs.Rows[i].Cells[0].Value.ToString() == txtemployeeid.Text)
                        {
                            //confirm box if logout the employee from the other station
                            DialogResult result = MessageBox.Show("Employee " + txtemployeeid.Text + " is already Logged In in Station " + dgvlogs.Rows[i].Cells[2].Value.ToString() + ". Do you want to Logout from Station " + dgvlogs.Rows[i].Cells[2].Value.ToString() + " and Login to Station " + txtstationid.Text, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (result.Equals(DialogResult.Yes))
                            {
                                Logout(txtemployeeid.Text, dgvlogs.Rows[i].Cells[2].Value.ToString());
                            }
                        }
                    }

                    //get the ipaddress and port
                    String ipaddress = "";
                    String port = "";                    
                    SqlCommand cmd1 = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_PROD_LINE='" + stnid[0] + "'", dc.con);
                    SqlDataReader dataReader = cmd1.ExecuteReader();
                    if (dataReader.Read())
                    {
                        ipaddress = dataReader.GetValue(0).ToString();
                        port = dataReader.GetValue(1).ToString();
                    }
                    dataReader.Close();

                    //check if ipaddress and port is empty
                    if (ipaddress == "" || port == "")
                    {
                        radLabel15.Text = "Station " + stnid[1] + " Does not Exists in Line " + stnid[0];
                        return;
                    }

                    //check if the headline is running before logging in
                    if (LineStatus(ipaddress, port).Contains("true"))
                    {
                        //login employee
                        String status = Login_Emp(ipaddress, port, stnid[1], txtemployeeid.Text);
                        if (status.Contains("true"))
                        {
                            radLabel15.Text = "Employee " + txtemployeeid.Text + " Logged In to Station " + txtstationid.Text;
                        }
                        else
                        {
                            radLabel15.Text = "Error while Logging in Employee " + txtemployeeid.Text + " to Station " + txtstationid.Text;
                        }
                    }
                    else
                    {
                        radLabel15.Text = "Please Start the HeadLine Before Logging In any Employee";
                        return;
                    }
                }
                else
                {
                    radLabel15.Text = "Invalid Station ID";
                }
            }
            else
            {
                radLabel15.Text = "Please Fill all the Fields";
            }

            Thread.Sleep(500);
            GetLogins();
        }

        //http request to login employee
        private String Login_Emp(String IP, String PORT, String StnID, String Empid)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/LoginEmp/" + StnID + "/" + Empid;
                var data = "";
                
                data = webPostMethod(postData, URL);
                return data;
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
            }
            return "";
        }

        //http request to logout employee
        private String Logout_Emp(String IP, String PORT, String StnID, String Empid)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/LogoutEmp/" + StnID + "/" + Empid;
                var data = "";

                data = webPostMethod(postData, URL);
                return data;
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
            }
            return "";
        }

        private void btnlogout_Click(object sender, EventArgs e)
        {
            Logout(txtemployeeid.Text, txtstationid.Text);   //logout selected employee from the station
        }

        public void Logout(String empid, String station_id)
        {
            //check if controller is selected
            if (controller_name == "--SELECT--" || controller_name == "")
            {
                radLabel15.Text = "Please Select a Controller";
                return;
            }

            //check if employee and station id is entered
            if (empid != "" && station_id != "")
            {
                if (station_id.Contains("."))
                {
                    String ipaddress = "";
                    String port = "";
                    String[] stnid = station_id.Split('.');

                    //check if the station is auto login station
                    MySqlCommand cmd2 = new MySqlCommand("select AUTOLOGIN from stationdata where OUTFEED_LINENO='" + stnid[0] + "' and STN_NO_OUTFEED='" + stnid[1] + "'", dc.conn);
                    MySqlDataReader sdr = cmd2.ExecuteReader();
                    if (sdr.Read())
                    {
                        String autologin = sdr.GetValue(0).ToString();
                        if (autologin == "1")
                        {
                            radLabel15.Text = "Its an Auto Login Station";
                            return;
                        }
                    }
                    sdr.Close();

                    //check if the station is auto login station
                    cmd2 = new MySqlCommand("select AUTOLOGIN from stationdata where INFEED_LINENO='" + stnid[0] + "' and STN_NO_INFEED='" + stnid[1] + "'", dc.conn);
                    sdr = cmd2.ExecuteReader();
                    if (sdr.Read())
                    {
                        String autologin = sdr.GetValue(0).ToString();
                        if (autologin == "1")
                        {
                            radLabel15.Text = "Its an Auto Login Station";
                            return;
                        }
                    }
                    sdr.Close();

                    //get the ipaddress and port
                    SqlCommand cmd1 = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_PROD_LINE='" + stnid[0] + "'", dc.con);
                    SqlDataReader dataReader = cmd1.ExecuteReader();
                    if (dataReader.Read())
                    {
                        ipaddress = dataReader.GetValue(0).ToString();
                        port = dataReader.GetValue(1).ToString();
                    }
                    dataReader.Close();

                    //check if the ipaddress anf port is empty
                    if (ipaddress == "" || port == "")
                    {
                        radLabel15.Text = "Station " + stnid[1] + " Does not Exists in Line " + stnid[0];
                        return;
                    }

                    //check if the headline is running before logging out any employee
                    if (LineStatus(ipaddress, port).Contains("true"))
                    {
                        //logout employee from the station
                        String status = Logout_Emp(ipaddress, port, stnid[1], empid);
                        if (status.Contains("true"))
                        {
                            radLabel15.Text = "Employee " + empid + " Logged Out from Station " + station_id;
                        }
                        else
                        {
                            radLabel15.Text = "Error while Logging Out Employee " + empid + " from Station " + station_id;
                        }
                    }
                    else
                    {
                        radLabel15.Text = "Please Start the HeadLine Before Logging Out any Employee";
                        return;
                    }
                }
                else
                {
                    radLabel15.Text = "Invalid Station ID";
                }
            }
            else
            {
                radLabel15.Text = "Please Fill all the Fields";
            }
            Thread.Sleep(500);
            GetLogins();  //get the login status
        }

        private void bntlogoutall_Click(object sender, EventArgs e)
        {
            //check if the controller is selected
            if (controller_name == "--SELECT--" || controller_name == "")
            {
                radLabel15.Text = "Please Select a Controller";
                return;
            }

            for (int i = 0; i < dgvlogs.Rows.Count; i++)
            {
                String ipaddress = "";
                String port = "";
                String[] stnid = dgvlogs.Rows[i].Cells[2].Value.ToString().Split('.');
                
                //get the ipaddress and port
                SqlCommand cmd1 = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_PROD_LINE='" + stnid[0] + "'", dc.con);
                SqlDataReader dataReader = cmd1.ExecuteReader();
                if (dataReader.Read())
                {
                    ipaddress = dataReader.GetValue(0).ToString();
                    port = dataReader.GetValue(1).ToString();
                }
                dataReader.Close();

                //check if ipaddress and port is empty
                if (ipaddress == "" || port == "")
                {
                    radLabel15.Text = "Station " + stnid[1] + " Does not Exists in Line " + stnid[0];
                    return;
                }

                //check if the headline is running before logging out any employee
                if (LineStatus(ipaddress, port).Contains("true"))
                {
                    //logout employee from the station
                    String status = Logout_Emp(ipaddress, port, stnid[1], dgvlogs.Rows[i].Cells[0].Value.ToString());
                    if (status.Contains("true"))
                    {
                        radLabel15.Text = "All Employees Logged Out";
                    }
                    else
                    {
                        radLabel15.Text = "Error while Logging Out All Employees ";
                    }
                    Thread.Sleep(1000);
                }
                else
                {
                    radLabel15.Text = "Please Start the HeadLine Before Logging Out any Employee";
                    return;
                }
            } 
            
            GetLogins();  //get the login details
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //generate report
            if (btnreport.Text == "Report View")
            {
                DataSet SET = new DataSet("SEQ");                
                DataView view = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.EMP_LOGIN.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                //add views to dataset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer1.RefreshReport();
                this.reportViewer1.RefreshReport();

                reportViewer1.Visible = true;
                btnreport.Text = "Table View";
            }
            else
            {
                reportViewer1.Visible = false;
                btnreport.Text = "Report View";
            }
        }

        private void btnautologin_Click(object sender, EventArgs e)
        {
            //check if the controller is selected
            if (controller_name == "--SELECT--" || controller_name == "")
            {
                radLabel15.Text = "Please Select a Controller";
                return;
            }

            String ipaddress = "";
            String port = "";
            //get the ipaddress and port
            SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_PROD_LINE='" + cmbautologin.Text + "'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                ipaddress = sdr.GetValue(0).ToString();
                port = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            AutoLogout(ipaddress, port);   //auto logout all emp

            //get station no for the line
            MySqlDataAdapter sda = new MySqlDataAdapter("select STN_NO_INFEED from stationdata where INFEED_LINENO='" + cmbautologin.Text + "' and AUTOLOGIN='0'", dc.conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Login_Emp(ipaddress, port, dt.Rows[i][0].ToString(), "999");   //auto login 
                Thread.Sleep(500);
            }

            radLabel15.Text = "Auto Login for All Station in Line : " + cmbautologin.Text;
            GetLogins();
        }

        public void AutoLogout(String ip, String port)
        {
            //get login details for the line
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT EMP_ID,ACTION_ID,TIME,STN_ID,LINE_NO FROM (SELECT  EMP_ID,ACTION_ID,TIME,STN_ID,LINE_NO,row_number() over (partition BY STN_ID order by TIME DESC) row_num FROM mrt_local.employeeactions WHERE ACTION_ID = 2 OR ACTION_ID = 1) a WHERE a.row_num = 1 AND  ACTION_ID = 1 AND LINE_NO = '" + cmbautologin.Text + "' ORDER BY TIME ASC", dc.conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String empid = dt.Rows[i][0].ToString();
                String stnid = "";

                //get station no
                MySqlCommand cmd = new MySqlCommand("select STN_NO_INFEED from stationdata where STN_ID='" + dt.Rows[i][3].ToString() + "'", dc.conn);
                if (cmd.ExecuteScalar() + "" != "")
                {
                    stnid = cmd.ExecuteScalar() + "";
                }

                if (stnid != "")
                {
                    Logout_Emp(ip, port, stnid, empid);   //logout emp
                    Thread.Sleep(500);
                }
            }
        }      

        private void dgvemployee_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            //check if the controller is selected
            if (controller_name == "--SELECT--" || controller_name == "")
            {
                radLabel15.Text = "Please Select a Controller";
                return;
            }

            //refresh grid
            RowSelected(dgvemployee.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void dgvlogs_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            txtemployeeid.Text = dgvlogs.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtstationid.Text = dgvlogs.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void dgvemployee_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //change grid fore color fi these themes are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvemployee.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvemployee.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvemployee.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvemployee.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvlogs_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //change grid fore color fi these themes are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvlogs.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvlogs.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvlogs.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvlogs.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            //check if the controller is selected
            if (controller_name == "--SELECT--" || controller_name == "")
            {
                radLabel15.Text = "Please Select a Controller";
                return;
            }

            String ipaddress = "";
            String port = "";
            //get the ipaddress and port
            SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_PROD_LINE='" + cmbautologin.Text + "'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                ipaddress = sdr.GetValue(0).ToString();
                port = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            AutoLogout(ipaddress, port);   //auto logout all emp
            GetLogins();
        }

        public void DebugLog(string Message)
        {
            try
            {
                //string path = "C:\\SMARTMRT\\SmartMRT MGIS\\Debug\\" + DateTime.Now.ToString("MMMM yyyy");
                string path = Application.StartupPath + "\\Debug\\" + DateTime.Now.ToString("MMMM yyyy");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = path + "\\DebugLogs_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".txt";
                if (!File.Exists(filepath))
                {
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " : " + Message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " : " + Message);
                    }
                }
            }
            catch (Exception ex)
            {
                //WriteToExFile("Debug Logfile is in Use : " + ex.Message + " : " + ex);
            }
        }
    }
}
