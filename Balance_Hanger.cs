using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Telerik.Charting;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Balance_Hanger : Telerik.WinControls.UI.RadForm
    {
        public Balance_Hanger()
        {
            InitializeComponent();
        }
        Database_Connection dc = new Database_Connection();
        String controller_name = "";

        private void Balance_Hanger_Load(object sender, EventArgs e)
        {
            timer1.Interval = int.Parse(cmbautorefresh.Text) * 1000; //auto refresh timer

            select_controller(); //get the selected controller

            //get the Production lines
            SqlDataAdapter sda = new SqlDataAdapter("select distinct V_PROD_LINE from PROD_LINE_DB", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {              
                
                cmbline.Items.Add(dt.Rows[i][0].ToString());
                cmbline.SelectedIndex = 0;
                //DebugLog("Track 1 - Balance_Hangers.cs(Balance_Hanger_Load()), Line -  " + dt.Rows[i][0].ToString());
            }
            
            //check if the controller is selected
            if (controller_name != "--SELECT--" || controller_name != "")
            {
                
                Balance_Hangers();
            }
        }

        public void Balance_Hangers()
        {
            
            radChartView3.Series.Clear();
            int total = 0;
            
            //get the station no of the station
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT sd.STN_NO_INFEED,COUNT(sh.HANGER_ID) FROM  balancehangers sh,stationdata sd WHERE sh.STN_ID=sd.STN_ID AND sd.INFEED_LINENO=" + cmbline.Text + " GROUP BY sd.STN_NO_INFEED ORDER BY sd.STN_NO_INFEED", dc.conn);
            //DebugLog("Track 1 - Balance_Hangers.cs(Balance_Hangers()), SQL - SELECT sd.STN_NO_INFEED,COUNT(sh.HANGER_ID) FROM  balancehangers sh,stationdata sd WHERE sh.STN_ID=sd.STN_ID AND sd.INFEED_LINENO=" + cmbline.Text + " GROUP BY sd.STN_NO_INFEED ORDER BY sd.STN_NO_INFEED");
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //bar chart for station wip
                BarSeries barSeries1 = new BarSeries("Performance", "RepresentativeName");

                String stnid = dt.Rows[i][0].ToString();
                int count = int.Parse(dt.Rows[i][1].ToString());
                total += count;

                //add the datapoints to the bar chart
                barSeries1.DataPoints.Add(new CategoricalDataPoint(count, stnid));
                barSeries1.ForeColor = Color.White;
                radChartView3.Series.Add(barSeries1);
                barSeries1.ShowLabels = true;

                //Set the verticle axis properties
                LinearAxis verticalAxis1 = radChartView3.Axes[1] as LinearAxis;
                verticalAxis1.LabelFitMode = AxisLabelFitMode.MultiLine;
                verticalAxis1.ForeColor = Color.White;
                verticalAxis1.BorderColor = Color.DodgerBlue;
                verticalAxis1.ShowLabels = false;
                verticalAxis1.Title = "Hangers";

                //set the horizontal axis properties
                CategoricalAxis ca1 = radChartView3.Axes[0] as CategoricalAxis;
                ca1.LabelFitMode = AxisLabelFitMode.MultiLine;
                ca1.Title = "Stations";
                ca1.ForeColor = Color.White;
                ca1.BorderColor = Color.DodgerBlue;
            }

            lbltotalwip.Text = "Total WIP : " + total;
        }

        public void select_controller()
        {
            dc.OpenConnection(); //Open Connection
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

            //get the IP address of the selected controller
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
                radLabel5.Text = "Please Select a Controller";
                return;
            }

            dc.Close_Connection();  //close the connection if open
            dc.OpenMYSQLConnection(ipaddress);  //open connection
            timer1.Start();  //start the auto refresh timer
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Balance_Hangers();  //get the station wip
        }

        private void radLabel5_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //10 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            panel1.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            panel1.Visible = false;
            radLabel5.Text = "";
            MyTimer.Stop();
        }

        private void Balance_Hanger_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();  //open connection
        }

        private void cmbline_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Balance_Hangers();  //get the station wip
        }

        private void cmbautorefresh_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            timer1.Stop();
            timer1.Interval = int.Parse(cmbautorefresh.Text) * 1000;
            timer1.Start();
        }

        private void Balance_Hanger_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
        }

        //public void DebugLog(string Message)
        //{
        //    try
        //    {
        //        //string path = "C:\\SMARTMRT\\SmartMRT MGIS\\Debug\\" + DateTime.Now.ToString("MMMM yyyy");
        //        string path = Application.StartupPath + "\\Debug\\" + DateTime.Now.ToString("MMMM yyyy");
        //        if (!Directory.Exists(path))
        //        {
        //            Directory.CreateDirectory(path);
        //        }
        //        string filepath = path + "\\DebugLogs_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".txt";
        //        if (!File.Exists(filepath))
        //        {
        //            using (StreamWriter sw = File.CreateText(filepath))
        //            {
        //                sw.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " : " + Message);
        //            }
        //        }
        //        else
        //        {
        //            using (StreamWriter sw = File.AppendText(filepath))
        //            {
        //                sw.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " : " + Message);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //WriteToExFile("Debug Logfile is in Use : " + ex.Message + " : " + ex);
        //    }
        //}
    }
}
