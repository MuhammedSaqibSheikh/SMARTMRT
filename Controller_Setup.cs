using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SMARTMRT
{
    public partial class Controller_Setup : Telerik.WinControls.UI.RadForm
    {
        public Controller_Setup()
        {
            InitializeComponent();
        }
        Database_Connection dc = new Database_Connection();  //connection class
        private void Controller_Setup_Load(object sender, EventArgs e)
        {
            RadMessageBox.SetThemeName("FluentDark");  //set message theme
            this.CenterToScreen();  //keep form centered to screen
            dc.OpenConnection();  //open connection

            cmbcontroller.Items.Add("--SELECT--");

            //get all the controllers
            SqlDataAdapter sda = new SqlDataAdapter("Select distinct V_CONTROLLER_ID from CONTROLLER_DB", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbcontroller.Items.Add(dt.Rows[i][0].ToString());
                cmbcontroller.SelectedIndex = 0;
            }            
        }

        public void GetStationTypes()
        {
            //clear station dropdownlist
            cmbstationtype.Items.Clear();

            //check if user has selected the controller
            if (cmbcontroller.Text == "" || cmbcontroller.Text == "--SELECT--")
            {
                radLabel5.Text = "Please Select a Controller";
                return;
            }

            //get station type from stationtype
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT TYPE FROM stationtype WHERE ID=1 OR ID=4 OR ID=5 OR ID=7", dc.conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbstationtype.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        private void radLabel5_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
            radPanel2.Visible = true;
        }

        System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radPanel2.Visible = false;
            radLabel5.Text = "";
            MyTimer.Stop();
        }

        public void select_controller()
        {
            dc.OpenConnection();  //open connection
            String ipaddress = "";

            //get the ip address and port number of the selected controller
            SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS from PROD_LINE_DB where V_CONTROLLER='" + cmbcontroller.Text + "'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                ipaddress = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //return if there is no ip address
            if (ipaddress == "")
            {
                cmbcontroller.SelectedIndex = 0;
                cmbcontroller.Text = "--SELECT--";
                cchkprodline.Items.Clear();
                cchkstationid.Items.Clear();
                cmbstationtype.Items.Clear();
                return;
            }

            dc.Close_Connection();  //close connection if open
            String status = dc.OpenMYSQLConnection(ipaddress);  //open connection
            //check connection status
            if (status == "UNABLE")
            {
                cmbcontroller.SelectedIndex = 0;
                cmbcontroller.Text = "--SELECT--";
                radLabel5.Text = "Controller Offline";
                cchkprodline.Items.Clear();
                cchkstationid.Items.Clear();
                cmbstationtype.Items.Clear();
            }
        }

        private void btnreboot_Click(object sender, EventArgs e)
        {
            try
            {
                //check id user has selected the controller
                if (cmbcontroller.Text == "" || cmbcontroller.Text == "--SELECT--")
                {
                    radLabel5.Text = "Please Select a Controller";
                    return;
                }
                String ipaddress = "";

                //get ip address of selected controller
                SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_CONTROLLER='" + cmbcontroller.Text + "'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    ipaddress = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //reboot the controller
                if (Reboot(ipaddress) == "TRUE")
                {
                    radLabel5.Text = "Controller " + cmbcontroller.Text + " is Rebooting";
                }
                else
                {
                    radLabel5.Text = "Error while Rebooting Controller " + cmbcontroller.Text + "";
                }
            }
            catch (Exception ex)
            {
                radLabel5.Text = ex.Message;
            }
        }

        //reboot controller
        private String Reboot(String IP)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":1880/E200001B291802132650C322/REBOOT";
                var data = "";
                data = webGetMethod(postData, URL);
                if (data == "")
                {
                    return ("FALSE");
                }
                if (data != "")
                {
                    return ("TRUE");
                }
            }
            catch (Exception ex)
            {
                radLabel5.Text = ex.Message;
            }
            return ("");
        }

        //shutdown controller
        private String Shutdown(String IP)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":1880/E200001B291802132650C322/SHUTDOWN";
                var data = "";
                data = webGetMethod(postData, URL);
                if (data == "")
                {
                    return ("FALSE");
                }
                if (data != "")
                {
                    return ("TRUE");
                }
            }
            catch (Exception ex)
            {
                radLabel5.Text = ex.Message;
            }
            return ("");
        }

        //start the line controller
        private String Start(String IP, String LINE)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":1880/E200001B291802132650C322/CONTROLLER/?LINE=" + LINE + "&CONT=ON";
                var data = "";
                data = webGetMethod(postData, URL);
                Thread.Sleep(1000);
                if (data == "")
                {
                    return ("FALSE");
                }
                if (data != "")
                {
                    return ("TRUE");
                }
            }
            catch (Exception ex)
            {
                radLabel5.Text = ex.Message;
            }
            return ("");
        }

        //stop the line controller
        private String Stop(String IP, String LINE)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":1880/E200001B291802132650C322/CONTROLLER/?LINE=" + LINE + "&CONT=OFF";
                var data = "";
                data = webGetMethod(postData, URL);
                Thread.Sleep(1000);
                if (data == "")
                {
                    return ("FALSE");
                }
                if (data != "")
                {
                    return ("TRUE");
                }
            }
            catch (Exception ex)
            {
                radLabel5.Text = ex.Message;
            }
            return ("");
        }

        //http get request with login credentials
        public String webGetMethod(String postData, String URL)
        {
            try
            {
                //GET Method
                string html = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);

                string authInfo = "smrt:betapass"; //login credential
                authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

                request.Headers["Authorization"] = "Basic " + authInfo;
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
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                }
                else
                {
                    radLabel5.Text = ex.Message;
                }
            }

            return "";
        }

        private void btnshutdown_Click(object sender, EventArgs e)
        {
            try
            {
                //check if user selected controller
                String ipaddress = "";
                if (cmbcontroller.Text == "" || cmbcontroller.Text == "--SELECT--")
                {
                    radLabel5.Text = "Please Select a Controller";
                    return;
                }

                //get the ip address of the controller
                SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_CONTROLLER='" + cmbcontroller.Text + "'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    ipaddress = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //shutdown controller
                if (Shutdown(ipaddress) == "TRUE")
                {
                    radLabel5.Text = "Controller " + cmbcontroller.Text + " is Shutting down";
                }
                else
                {
                    radLabel5.Text = "Error while Shutting down Controller " + cmbcontroller.Text + "";
                }
            }
            catch (Exception ex)
            {
                radLabel5.Text = ex.Message;
            }
        }

        private void btnstart_Click(object sender, EventArgs e)
        {
            //thread to start the headline
            Thread startcontroller = new Thread(StartController);
            startcontroller.Start();
        }

        public void StartController()
        {
            //check if user selected controller
            if (cmbcontroller.Text == "" || cmbcontroller.Text == "--SELECT--")
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = "Please Select a Controller"));
                }
                else
                {
                    radLabel5.Text = "Please Select a Controller";
                }
                return;
            }

            //get all production lines
            for (int i = 0; i < cchkprodline.Items.Count; i++)
            {
                //check if user selected prod line
                if (cchkprodline.Items[i].Checked == true)
                {
                    try
                    {
                        String ipaddress = "";

                        //get ip address and port no of prod line
                        SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_CONTROLLER='" + cmbcontroller.Text + "'", dc.con);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            ipaddress = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //strat the head line
                        String status = Start(ipaddress, cchkprodline.Items[i].Text);
                        if (status == "TRUE")
                        {
                            //check if funtion is running on diffrent thread other than the main thread
                            if (radLabel5.InvokeRequired)
                            {
                                radLabel5.Invoke((Action)(() => radLabel5.Text = "Line controller is Started for " + cchkprodline.Items[i].Text));
                            }
                            else
                            {
                                radLabel5.Text = "Line controller is Started for " + cchkprodline.Items[i].Text;
                            }
                        }
                        else
                        {
                            //check if funtion is running on diffrent thread other than the main thread
                            if (radLabel5.InvokeRequired)
                            {
                                radLabel5.Invoke((Action)(() => radLabel5.Text = "Error while Starting Line controller for " + cchkprodline.Items[i].Text + " "));
                            }
                            else
                            {
                                radLabel5.Text = "Error while Starting Line controller for " + cchkprodline.Items[i].Text + " ";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //check if funtion is running on diffrent thread other than the main thread
                        if (radLabel5.InvokeRequired)
                        {
                            radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                        }
                        else
                        {
                            radLabel5.Text = ex.Message;
                        }
                    }
                }
            }
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            //thread to stop the headline
            Thread stopcontroller = new Thread(StopController);
            stopcontroller.Start();
        }

        public void StopController()
        {
            if (cmbcontroller.Text == "" || cmbcontroller.Text == "--SELECT--")
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = "Please Select a Controller"));
                }
                else
                {
                    radLabel5.Text = "Please Select a Controller";
                }
                return;
            }

            //get all the prod line
            for (int i = 0; i < cchkprodline.Items.Count; i++)
            {
                //check if selected
                if (cchkprodline.Items[i].Checked == true)
                {
                    try
                    {
                        String ipaddress = "";

                        //get the ipaddress and port of the production line
                        SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_CONTROLLER='" + cmbcontroller.Text + "'", dc.con);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            ipaddress = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //stop the headline
                        String status = Stop(ipaddress, cchkprodline.Items[i].Text);
                        if (status == "TRUE")
                        {
                            //check if funtion is running on diffrent thread other than the main thread
                            if (radLabel5.InvokeRequired)
                            {
                                radLabel5.Invoke((Action)(() => radLabel5.Text = "Line controller is Stopped for " + cchkprodline.Items[i].Text));
                            }
                            else
                            {
                                radLabel5.Text = "Line controller is Stopped for " + cchkprodline.Items[i].Text;
                            }
                        }
                        else
                        {
                            //check if funtion is running on diffrent thread other than the main thread
                            if (radLabel5.InvokeRequired)
                            {
                                radLabel5.Invoke((Action)(() => radLabel5.Text = "Error while Stopping Line controller for " + cchkprodline.Items[i].Text + " "));
                            }
                            else
                            {
                                radLabel5.Text = "Error while Stopping Line controller for " + cchkprodline.Items[i].Text + " ";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //check if funtion is running on diffrent thread other than the main thread
                        if (radLabel5.InvokeRequired)
                        {
                            radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                        }
                        else
                        {
                            radLabel5.Text = ex.Message;
                        }
                    }
                }
            }
        }

        private void cchkcontroller_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            select_controller();  //connect to controller

            //check if user selected controller
            if (cmbcontroller.Text == "" || cmbcontroller.Text == "--SELECT--")
            {
                radLabel5.Text = "Controller Offline";
                return;
            }

            GetStationTypes();  //get station type
            //clear the dropdownlist
            cchkprodline.Items.Clear();
            cchkstationid.Items.Clear();

            //get all the production lines for that controller
            SqlDataAdapter sda = new SqlDataAdapter("Select V_PROD_LINE from PROD_LINE_DB where V_CONTROLLER='" + cmbcontroller.Text + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                cchkprodline.Items.Add(dt.Rows[j][0].ToString());
            }
        }

        private void btnstart_Click_1(object sender, EventArgs e)
        {
            //thread to start line controller
            Thread startline = new Thread(StartLines);
            startline.Start();
        }

        public void StartLines()
        {
            String ipaddress = "";
            String port = "";

            //check if funtion is running on diffrent thread other than the main thread
            if (radLabel5.InvokeRequired)
            {
                radLabel5.Invoke((Action)(() => radLabel5.Text = "Command sent to Start Headlines"));
            }
            else
            {
                radLabel5.Text = "Command sent to Start Headlines";
            }

            //get all the prod lines
            for (int i = 0; i < cchkprodline.Items.Count; i++)
            {
                //check prod line is selected
                if (cchkprodline.Items[i].Checked == true)
                {
                    //get the ip address and port for that prodline
                    SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_PROD_LINE='" + cchkprodline.Items[i].ToString() + "' order by V_CONTROLLER", dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        ipaddress = sdr.GetValue(0).ToString();
                        port = sdr.GetValue(1).ToString();
                    }
                    sdr.Close();

                    //start the line controller
                    StartLine(ipaddress, port);
                }
            }
        }

        //start the line controller
        private void StartLine(String IP, String PORT)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/Start";
                var data = "";
                data = webGetMethod(postData, URL);
                Thread.Sleep(1000);

                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = "Head Line Started"));
                }
                else
                {
                    radLabel5.Text = "Head Line Started";
                }

            }
            catch (Exception ex)
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                }
                else
                {
                    radLabel5.Text = ex.Message;
                }
            }
        }

        //stop line controller
        private void StopLine(String IP, String PORT)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/Stop";
                var data = "";
                data = webGetMethod(postData, URL);
                Thread.Sleep(1000);

                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = "Head Line Stopped"));
                }
                else
                {
                    radLabel5.Text = "Head Line Stopped";
                }

            }
            catch (Exception ex)
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                }
                else
                {
                    radLabel5.Text = ex.Message;
                }
            }
        }

        private void btnstop_Click_1(object sender, EventArgs e)
        {
            //confirm box for shutdown headlines
            DialogResult result = RadMessageBox.Show("Are you sure to Shutdown the Lines?", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
            if (result.Equals(DialogResult.Yes))
            {
                //thread to stop headlines
                Thread stopline = new Thread(StopLines);
                stopline.Start();
            }
        }

        //stop headlines
        public void StopLines()
        {
            String ipaddress = "";
            String port = "";

            //check if funtion is running on diffrent thread other than the main thread
            if (radLabel5.InvokeRequired)
            {
                radLabel5.Invoke((Action)(() => radLabel5.Text = "Command sent to Stop Headlines"));
            }
            else
            {
                radLabel5.Text = "Command sent to Stop Headlines";
            }

            //get all the prod lines
            for (int i = 0; i < cchkprodline.Items.Count; i++)
            {
                //check if prodline is selected
                if (cchkprodline.Items[i].Checked == true)
                {
                    //get ipaddress and port for that prod line
                    SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_PROD_LINE='" + cchkprodline.Items[i].ToString() + "' order by V_CONTROLLER", dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        ipaddress = sdr.GetValue(0).ToString();
                        port = sdr.GetValue(1).ToString();
                    }
                    sdr.Close();

                    //stop the headlines
                    StopLine(ipaddress, port);
                }
            }
        }

        private void cchkprodline_ItemCheckedChanged(object sender, Telerik.WinControls.UI.RadCheckedListDataItemEventArgs e)
        {
            //clear the station dropdownlist
            cchkstationid.Items.Clear();

            //get all the pro lines
            for (int i = 0; i < cchkprodline.Items.Count; i++)
            {
                //check if prodline is selected
                if (cchkprodline.Items[i].Checked == true)
                {
                    //get the infeed station no
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT CONCAT(s.I_INFEED_LINE_NO,'.',s.I_STN_NO_INFEED) FROM STATION_DATA s WHERE s.I_INFEED_LINE_NO='" + cchkprodline.Items[i].Text + "' ORDER BY CONCAT(s.I_INFEED_LINE_NO,'.',s.I_STN_NO_INFEED)", dc.con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        cchkstationid.Items.Add(dt.Rows[j][0].ToString());
                    }

                    //get the outfeed station no
                    sda = new SqlDataAdapter("SELECT CONCAT(s.I_OUTFEED_LINE_NO,'.',s.I_STN_NO_OUTFEED) FROM STATION_DATA s WHERE s.I_OUTFEED_LINE_NO='" + cchkprodline.Items[i].Text + "' ORDER BY CONCAT(s.I_OUTFEED_LINE_NO,'.',s.I_STN_NO_OUTFEED)", dc.con);
                    dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        int flag = 0;
                        //check if the station no is already added
                        for (int k = 0; k < cchkstationid.Items.Count; k++)
                        {
                            if (cchkstationid.Items[k].Text == dt.Rows[j][0].ToString())
                            {
                                flag = 1;
                                break;
                            }
                        }
                        if (flag == 0)
                        {
                            cchkstationid.Items.Add(dt.Rows[j][0].ToString());
                        }
                    }
                }
            }
        }

        private void btninfeed_Click(object sender, EventArgs e)
        {
            //thread to fire the infeed
            Thread infeed = new Thread(StationInfeed);
            infeed.Start();
        }

        private void btnoutfeed_Click(object sender, EventArgs e)
        {
            //thread to fire the outfeed
            Thread outfeed = new Thread(StationOutfeed);
            outfeed.Start();
        }

        private void btnelevator_Click(object sender, EventArgs e)
        {
            //thread to fire the elevator
            Thread elevator = new Thread(StationElevator);
            elevator.Start();
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            //thread to reset the station
            Thread reset = new Thread(StationReset);
            reset.Start();
        }

        //fire infeed
        public void StationInfeed()
        {
            //get all the station
            for (int i = 0; i < cchkstationid.Items.Count; i++)
            {
                //check if station is selected
                if (cchkstationid.Items[i].Checked == true)
                {
                    String[] stn = cchkstationid.Items[i].Text.Split('.');
                    String ipaddress = "";
                    String port = "";

                    //get the line ipaddress and port
                    SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_PROD_LINE='" + stn[0] + "' order by V_CONTROLLER", dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        ipaddress = sdr.GetValue(0).ToString();
                        port = sdr.GetValue(1).ToString();
                    }
                    sdr.Close();

                    //fire infeed
                    Infeed(ipaddress, port, stn[1]);
                    //check if funtion is running on diffrent thread other than the main thread
                    if (radLabel5.InvokeRequired)
                    {
                        radLabel5.Invoke((Action)(() => radLabel5.Text = "Infeed Fired for Station " + cchkstationid.Items[i].Text));
                    }
                    else
                    {
                        radLabel5.Text = "Infeed Fired for Station " + cchkstationid.Items[i].Text;
                    }
                }
            }
        }

        //fire infeed
        private void Infeed(String IP, String PORT, String STATIONID)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/Infeed/" + STATIONID;
                var data = "";

                data = webGetMethod(postData, URL);
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                }
                else
                {
                    radLabel5.Text = ex.Message;
                }
            }
        }

        //fire out feed
        private void Outfeed(String IP, String PORT, String STATIONID)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/Outfeed/" + STATIONID;
                var data = "";

                data = webGetMethod(postData, URL);
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                }
                else
                {
                    radLabel5.Text = ex.Message;
                }
            }
        }

        //fire out feed
        public void StationOutfeed()
        {
            //get all the station
            for (int i = 0; i < cchkstationid.Items.Count; i++)
            {
                //check if station is selected
                if (cchkstationid.Items[i].Checked == true)
                {
                    String[] stn = cchkstationid.Items[i].Text.Split('.');
                    String ipaddress = "";
                    String port = "";

                    //get the ipaddress and port
                    SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_PROD_LINE='" + stn[0] + "' order by V_CONTROLLER", dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        ipaddress = sdr.GetValue(0).ToString();
                        port = sdr.GetValue(1).ToString();
                    }
                    sdr.Close();

                    //fire outfeed
                    Outfeed(ipaddress, port, stn[1]);
                    //check if funtion is running on diffrent thread other than the main thread
                    if (radLabel5.InvokeRequired)
                    {
                        radLabel5.Invoke((Action)(() => radLabel5.Text = "Outfeed Fired for Station " + cchkstationid.Items[i].Text));
                    }
                    else
                    {
                        radLabel5.Text = "Outfeed Fired for Station " + cchkstationid.Items[i].Text;
                    }
                }
            }
        }

        //fire elevator
        public void StationElevator()
        {
            //get all the station
            for (int i = 0; i < cchkstationid.Items.Count; i++)
            {
                //check if station is selected
                if (cchkstationid.Items[i].Checked == true)
                {
                    String[] stn = cchkstationid.Items[i].Text.Split('.');
                    String ipaddress = "";
                    String port = "";

                    //get the ipaddress and port
                    SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_PROD_LINE='" + stn[0] + "' order by V_CONTROLLER", dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        ipaddress = sdr.GetValue(0).ToString();
                        port = sdr.GetValue(1).ToString();
                    }
                    sdr.Close();

                    //fire ellevator
                    Elevator(ipaddress, port, stn[1]);

                    //check if funtion is running on diffrent thread other than the main thread
                    if (radLabel5.InvokeRequired)
                    {
                        radLabel5.Invoke((Action)(() => radLabel5.Text = "Elevator Fired for Station " + cchkstationid.Items[i].Text));
                    }
                    else
                    {
                        radLabel5.Text = "Elevator Fired for Station " + cchkstationid.Items[i].Text;
                    }
                }
            }
        }

        //fire elevator
        private void Elevator(String IP, String PORT, String STATIONID)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/Elevator/" + STATIONID;
                var data = "";

                data = webGetMethod(postData, URL);
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                }
                else
                {
                    radLabel5.Text = ex.Message;
                }
            }
        }

        //reset station
        public void StationReset()
        {
            Logout(); //logout all employees before reset

            //get all the station
            for (int i = 0; i < cchkstationid.Items.Count; i++)
            {
                //check if station is selected
                if (cchkstationid.Items[i].Checked == true)
                {
                    String[] stn = cchkstationid.Items[i].Text.Split('.');
                    String ipaddress = "";
                    String port = "";

                    //get the ipaddress and port
                    SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_PROD_LINE='" + stn[0] + "' order by V_CONTROLLER", dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        ipaddress = sdr.GetValue(0).ToString();
                        port = sdr.GetValue(1).ToString();
                    }
                    sdr.Close();

                    //reset station
                    Reset(ipaddress, port, stn[1]);
                    //check if funtion is running on diffrent thread other than the main thread
                    if (radLabel5.InvokeRequired)
                    {
                        radLabel5.Invoke((Action)(() => radLabel5.Text = "Reset Fired for Station " + cchkstationid.Items[i].Text));
                    }
                    else
                    {
                        radLabel5.Text = "Reset Fired for Station " + cchkstationid.Items[i].Text;
                    }
                }
            }
        }

        //reset station
        private void Reset(String IP, String PORT, String STATIONID)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/Reset/" + STATIONID;
                var data = "";

                data = webGetMethod(postData, URL);
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                }
                else
                {
                    radLabel5.Text = ex.Message;
                }
            }
        }

        //logout employees
        public void Logout()
        {
            //check if user selected controller
            if (cmbcontroller.Text == "--SELECT--" || cmbcontroller.Text == "")
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = "Please Select a Controller"));
                }
                else
                {
                    radLabel5.Text = "Please Select a Controller";
                }
                return;
            }

            //get all the station
            for (int k = 0; k < cchkstationid.Items.Count; k++)
            {
                //check if station is selected
                if (cchkstationid.Items[k].Checked == true)
                {
                    String ipaddress = "";
                    String port = "";
                    String[] stnid = cchkstationid.Items[k].Text.Split('.');
                    String empid = "0";

                    //check if autologin is enabled
                    MySqlCommand cmd2 = new MySqlCommand("select AUTOLOGIN from stationdata where OUTFEED_LINENO='" + stnid[0] + "' and STN_NO_OUTFEED='" + stnid[1] + "'", dc.conn);
                    MySqlDataReader sdr = cmd2.ExecuteReader();
                    if (sdr.Read())
                    {
                        String autologin = sdr.GetValue(0).ToString();
                        //if autologin station return
                        if (autologin == "1")
                        {
                            //check if funtion is running on diffrent thread other than the main thread
                            if (radLabel5.InvokeRequired)
                            {
                                radLabel5.Invoke((Action)(() => radLabel5.Text = "Its an Auto Login Station"));
                            }
                            else
                            {
                                radLabel5.Text = "Its an Auto Login Station";
                            }
                            return;
                        }
                    }
                    sdr.Close();

                    //check if autologin is enabled
                    cmd2 = new MySqlCommand("select AUTOLOGIN from stationdata where INFEED_LINENO='" + stnid[0] + "' and STN_NO_INFEED='" + stnid[1] + "'", dc.conn);
                    sdr = cmd2.ExecuteReader();
                    if (sdr.Read())
                    {
                        String autologin = sdr.GetValue(0).ToString();
                        //if autologin station return
                        if (autologin == "1")
                        {
                            //check if funtion is running on diffrent thread other than the main thread
                            if (radLabel5.InvokeRequired)
                            {
                                radLabel5.Invoke((Action)(() => radLabel5.Text = "Its an Auto Login Station"));
                            }
                            else
                            {
                                radLabel5.Text = "Its an Auto Login Station";
                            }
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

                    //check if headline is on before logging out employees
                    if (LineStatus(ipaddress, port).Contains("true"))
                    {
                        //if headline on logut employees
                        String status = Logout_Emp(ipaddress, port, stnid[1], empid);
                        if (status.Contains("true"))
                        {
                            //check if funtion is running on diffrent thread other than the main thread
                            if (radLabel5.InvokeRequired)
                            {
                                radLabel5.Invoke((Action)(() => radLabel5.Text = "Employee Logged Out from Station " + cchkstationid.Items[k].Text));
                            }
                            else
                            {
                                radLabel5.Text = "Employee Logged Out from Station " + cchkstationid.Items[k].Text;
                            }
                        }
                        else
                        {
                            //check if funtion is running on diffrent thread other than the main thread
                            if (radLabel5.InvokeRequired)
                            {
                                radLabel5.Invoke((Action)(() => radLabel5.Text = "Error while Logging Out Employee from Station " + cchkstationid.Items[k].Text));
                            }
                            else
                            {
                                radLabel5.Text = "Error while Logging Out Employee from Station " + cchkstationid.Items[k].Text;
                            }
                        }
                    }
                    else
                    {
                        //check if funtion is running on diffrent thread other than the main thread
                        if (radLabel5.InvokeRequired)
                        {
                            radLabel5.Invoke((Action)(() => radLabel5.Text = "Please Start the HeadLine Before Logging Out any Employee"));
                        }
                        else
                        {
                            radLabel5.Text = "Please Start the HeadLine Before Logging Out any Employee";
                        }
                        return;
                    }

                    Thread.Sleep(1000);
                }
            }
        }

        //logout employees
        private String Logout_Emp(String IP, String PORT, String StnID, String Empid)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/LogoutEmp/" + StnID + "/" + Empid;
                var data = "";

                data = webGetMethod(postData, URL);
                return data;
            }
            catch (Exception ex)
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                }
                else
                {
                    radLabel5.Text = ex.Message;
                }
            }
            return "";
        }

        //check line status
        private String LineStatus(String IP, String PORT)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/Status";
                var data = "";

                data = webGetMethod(postData, URL);
                return data;
            }
            catch (Exception ex)
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                }
                else
                {
                    radLabel5.Text = ex.Message;
                }
            }
            return ("");
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //thread to logout employees
            Thread logout = new Thread(Logout);
            logout.Start();
        }

        private void Controller_Setup_FormClosed(object sender, FormClosedEventArgs e)
        {
            dc.Close_Connection();  //close the connection on form close
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            //check if controller is selected
            if (cmbcontroller.Text == "" || cmbcontroller.Text == "--SELECT--")
            {
                radLabel5.Text = "Please Select a Controller";
                return;
            }

            //check if station type is selected
            if (cmbstationtype.Text == "--SELECT--")
            {
                radLabel5.Text = "Please Select a Station Type";
                return;
            }

            //thread to update the station type
            Thread update = new Thread(UpdateStationType);
            update.Start();
        }

        public void UpdateStationType()
        {
            try
            {
                //set the station type
                String stationtype = "1";
                if (cmbstationtype.Text == "NORMAL_STATION")
                {
                    stationtype = "1";
                }
                else if (cmbstationtype.Text == "OVERLOAD_STATION")
                {
                    stationtype = "4";
                }
                else if (cmbstationtype.Text == "AUTO_COLLECTION_STATION")
                {
                    stationtype = "5";
                }
                else if (cmbstationtype.Text == "SORTING_STATION")
                {
                    stationtype = "7";
                }

                //get all the station
                for (int i = 0; i < cchkstationid.Items.Count; i++)
                {
                    //check if station is selected
                    if (cchkstationid.Items[i].Checked == true)
                    {
                        String[] stn = cchkstationid.Items[i].Text.Split('.');

                        //update the station type
                        MySqlCommand cmd = new MySqlCommand("update stationdata set STATIONTYPE='" + stationtype + "' where INFEED_LINENO='" + stn[0] + "' and STN_NO_INFEED='" + stn[1] + "'", dc.conn);
                        cmd.ExecuteNonQuery();

                        cmd = new MySqlCommand("update stationdata set STATIONTYPE='" + stationtype + "' where OUTFEED_LINENO='" + stn[0] + "' and STN_NO_OUTFEED='" + stn[1] + "'", dc.conn);
                        cmd.ExecuteNonQuery();

                        SqlCommand cmd1 = new SqlCommand("update STATION_DATA set I_STATION_TYPE='" + stationtype + "' where I_INFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_INFEED='" + stn[1] + "'", dc.con);
                        cmd1.ExecuteNonQuery();

                        cmd1 = new SqlCommand("update STATION_DATA set I_STATION_TYPE='" + stationtype + "' where I_OUTFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_OUTFEED='" + stn[1] + "'", dc.con);
                        cmd1.ExecuteNonQuery();
                    }
                }

                //get all the prod line
                for (int i = 0; i < cchkprodline.Items.Count; i++)
                {
                    //check if prodline is selected
                    if (cchkprodline.Items[i].Checked == true)
                    {
                        String ipaddress = "";
                        String port = "";

                        //get the ipaddress and port
                        SqlCommand cmd2 = new SqlCommand("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_PROD_LINE='" + cchkprodline.Items[i].Text + "' order by V_CONTROLLER", dc.con);
                        SqlDataReader sdr2 = cmd2.ExecuteReader();
                        if (sdr2.Read())
                        {
                            ipaddress = sdr2.GetValue(0).ToString();
                            port = sdr2.GetValue(1).ToString();
                        }
                        sdr2.Close();

                        //update station type
                        //StationUpdate(ipaddress, port);
                    }
                }

                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = "Station Update Completed"));
                }
                else
                {
                    radLabel5.Text = "Station Update Completed";
                }
            }
            catch(Exception ex)
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                }
                else
                {
                    radLabel5.Text = ex.Message;
                }
            }
        }

        private String StationUpdate(String IP, String PORT)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/StationUpdate";
                var data = "";
                data = webGetMethod(postData, URL);
            }
            catch (Exception ex)
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                }
                else
                {
                    radLabel5.Text = ex.Message;
                }
            }
            return ("");
        }
    }
}
