using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SMARTMRT
{
    public partial class MessageBox_loading : Telerik.WinControls.UI.RadForm
    {
        public String MO = "";
        public String MOLINE = "";
        public String MO1 = "";
        public String MOLINE1 = "";
        public String Controller = "";

        Database_Connection dc = new Database_Connection();   //connection class

        public MessageBox_loading()
        {
            InitializeComponent();
        }

        private void MessageBox_loading_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();    //keep form centred to screen
            dc.OpenConnection();    //open connection

            //get cluster ip address
            String ipaddress = "";
            SqlCommand cmd = new SqlCommand("select V_CLUSTER_IP_ADDRESS from CLUSTER_DB where V_CLUSTER_ID='" + Controller + "'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                ipaddress = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            dc.OpenMYSQLConnection(ipaddress);   //open connection
        }

        private void btnchangeloading_Click(object sender, EventArgs e)
        {
            //check if change loading station button is clicked
            if (btnchangeloading.Text == "Change Loading Station")
            {
                lblloading.Visible = true;
                txtloading.Visible = true;
                btnchangeloading.Text = "Update";
                btnstoploading.Enabled = false;
            }
            //check if update button is clicked
            else if (btnchangeloading.Text == "Update")
            {
                try
                {
                    String station = txtloading.Text;
                    String stnID = "";

                    if (station.Contains("."))
                    {
                        String[] stn = station.Split('.');
                        String controller = "";

                        //get the controller ipaddress
                        SqlCommand cmd = new SqlCommand("select V_CONTROLLER from PROD_LINE_DB where V_PROD_LINE='" + stn[0] + "'", dc.con);
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        if (dataReader.Read())
                        {
                            controller = dataReader.GetValue(0).ToString();
                        }
                        dataReader.Close();

                        //get the station id
                        cmd = new SqlCommand("select I_STN_ID from STATION_DATA where I_INFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_INFEED='" + stn[1] + "' and V_CONTROLLER_ID='" + controller + "'", dc.con);
                        dataReader = cmd.ExecuteReader();
                        if (!(dataReader.Read()))
                        {
                            txtloading.Text = "";
                            radLabel15.Text = "There is no Station " + stn[1] + " in Line " + stn[0];
                            dataReader.Close();

                            return;
                        }
                        dataReader.Close();

                        //get the station type
                        cmd = new SqlCommand("select I_STATION_TYPE from STATION_DATA where I_INFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_INFEED='" + stn[1] + "' and V_CONTROLLER_ID='" + controller + "'", dc.con);
                        dataReader = cmd.ExecuteReader();
                        if (dataReader.Read())
                        {
                            String stntype = dataReader.GetValue(0).ToString();
                            //check if its a bridge station
                            if (stntype == "2")
                            {
                                txtloading.Text = "";
                                radLabel15.Text = "Its a Brigde Station";
                                dataReader.Close();

                                return;
                            }
                            //check if its a overload station
                            if (stntype == "4")
                            {
                                radLabel15.Text = "Its a Overload Station";
                                dataReader.Close();

                                return;
                            }
                        }
                        dataReader.Close();

                        //get station id
                        SqlCommand cmd1 = new SqlCommand("select I_STN_ID from STATION_DATA where I_INFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_INFEED='" + stn[1] + "' and V_CONTROLLER_ID='" + controller + "'", dc.con);
                        dataReader = cmd1.ExecuteReader();
                        if (dataReader.Read())
                        {
                            stnID = dataReader["I_STN_ID"].ToString();
                        }
                        //close Data Reader
                        dataReader.Close();

                        //update loading station in station assign
                        cmd = new SqlCommand("update STATION_ASSIGN set I_LINE_NO='" + stn[0] + "',D_STATION_NO='" + stn[1] + "',I_STATION_ID='" + stnID + "' where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "' and I_SEQUENCE_NO='1' and I_ROW_NO='1'", dc.con);
                        cmd.ExecuteNonQuery();

                        radLabel15.Text = "Loading Station Updated for " + MO + "-" + MOLINE;
                        btnchangeloading.Text = "Change Loading Station";
                        btnchangeloading.Enabled = false;
                    }
                    else
                    {
                        txtloading.Text = "";
                        radLabel15.Text = "Invalid Station Assign";
                    }
                }
                catch (Exception ex)
                {
                    radLabel15.Text = ex.Message;
                }
            }
        }

        private void btnstoploading_Click(object sender, EventArgs e)
        {
            //start loading
            MySqlCommand cmd = new MySqlCommand("update prod set IN_PROD='0' where MO_NO='" + MO1 + "' and MO_LINE='" + MOLINE1 + "'", dc.conn);
            cmd.ExecuteNonQuery();

            radLabel15.Text = MO1 + "-" + MOLINE1 + " Stopped Loading";
            btnchangeloading.Enabled = false;
            btnstoploading.Enabled = false;
        }

        private void radLabel15_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            radPanel2.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel15.Text = "";
            radPanel2.Visible = false;
            MyTimer.Stop();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();   //close the form
        }

        private void MessageBox_loading_FormClosed(object sender, FormClosedEventArgs e)
        {
            dc.Close_Connection();   //close the connection of form close
        }
    }
}
