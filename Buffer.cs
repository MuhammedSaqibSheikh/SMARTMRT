using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Buffer : Telerik.WinControls.UI.RadForm
    {
        public Buffer()
        {
            InitializeComponent();
        }
        Database_Connection dc = new Database_Connection();  //Connection class
        DataTable STN = new DataTable();
        DataTable STN1 = new DataTable();
        //String sort_enabled = "False";
        String theme = "";

        private void Buffer_Load(object sender, EventArgs e)
        {
            dgvbuffer.MasterTemplate.SelectLastAddedRow = false;
            dgvbufferout.MasterTemplate.SelectLastAddedRow = false;
            RadMessageBox.SetThemeName("FluentDark");  //Message theme

            dgvbuffer.MasterView.TableSearchRow.ShowCloseButton = false;   //disable close button ig grid search
            dgvbufferout.MasterView.TableSearchRow.ShowCloseButton = false;  //disable close button ig grid search
            btnsortout.Visible = false;
            btnbuffercallout.Enabled = false;
            radPanel2.Visible = false;

            btnsortout.Enabled = false;
            dc.OpenConnection();  //Open connection
                
            //Special fields
            String user1 = "";
            String user2 = "";
            String user3 = "";
            String user4 = "";
            String user5 = "";
            String user6 = "";
            String user7 = "";
            String user8 = "";
            String user9 = "";
            String user10 = "";

            //get the special fields
            SqlCommand cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF1' and V_ENABLED='TRUE'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user1 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the special fields
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user2 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the special fields
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user3 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the special fields
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user4 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the special fields
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user5 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the special fields
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF6' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user6 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the special fields
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF7' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user7 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the special fields
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF8' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user8 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the special fields
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF9' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user9 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the special fields
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF10' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user10 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //add columns for datatable
            STN.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            STN.Columns.Add("MO No");
            STN.Columns.Add("MO Details");
            STN.Columns.Add("Color");
            STN.Columns.Add("Article ID");
            STN.Columns.Add("Size");
            STN.Columns.Add(user1);
            STN.Columns.Add(user2);
            STN.Columns.Add(user3);
            STN.Columns.Add(user4);
            STN.Columns.Add(user5);
            STN.Columns.Add(user6);
            STN.Columns.Add(user7);
            STN.Columns.Add(user8);
            STN.Columns.Add(user9);
            STN.Columns.Add(user10);
            STN.Columns.Add("Hanger Count");
            STN.Columns.Add("Next Sequence");
            STN.Columns.Add("Next Station");
            STN.Columns.Add("Next Stn");
            STN.Columns.Add("Sorting");
            STN.Columns.Add("Sorting Station");
            STN.Columns.Add("Auto Buffer");

            dgvbuffer.DataSource = STN;
            //hide the columns which are not selected
            if (user1 == "")
            {
                dgvbuffer.Columns[6].IsVisible = false;
            }

            if (user2 == "")
            {
                dgvbuffer.Columns[7].IsVisible = false;
            }

            if (user3 == "")
            {
                dgvbuffer.Columns[8].IsVisible = false;
            }

            if (user4 == "")
            {
                dgvbuffer.Columns[9].IsVisible = false;
            }

            if (user5 == "")
            {
                dgvbuffer.Columns[10].IsVisible = false;
            }

            if (user6 == "")
            {
                dgvbuffer.Columns[11].IsVisible = false;
            }

            if (user7 == "")
            {
                dgvbuffer.Columns[12].IsVisible = false;
            }

            if (user8 == "")
            {
                dgvbuffer.Columns[13].IsVisible = false;
            }

            if (user9 == "")
            {
                dgvbuffer.Columns[14].IsVisible = false;
            }

            if (user10 == "")
            {
                dgvbuffer.Columns[15].IsVisible = false;
            }

            dgvbuffer.Columns[17].IsVisible = false;
            dgvbuffer.Columns[18].IsVisible = false;
            dgvbuffer.Columns[19].IsVisible = false;
            dgvbuffer.Columns[20].IsVisible = false;
            dgvbuffer.Columns[21].IsVisible = false;

            STN1.Columns.Add("MO No");
            STN1.Columns.Add("MO Details");
            STN1.Columns.Add("Color");
            STN1.Columns.Add("Article ID");
            STN1.Columns.Add("Size");
            STN1.Columns.Add(user1);
            STN1.Columns.Add(user2);
            STN1.Columns.Add(user3);
            STN1.Columns.Add(user4);
            STN1.Columns.Add(user5);
            STN1.Columns.Add(user6);
            STN1.Columns.Add(user7);
            STN1.Columns.Add(user8);
            STN1.Columns.Add(user9);
            STN1.Columns.Add(user10);
            STN1.Columns.Add("Hanger Count");
            STN1.Columns.Add("Call Out Count");
            STN1.Columns.Add("Next Sequence");
            STN1.Columns.Add("Next Station");
            STN1.Columns.Add("Next Stn");
            STN1.Columns.Add("Sorting");
            STN1.Columns.Add("Sorting Station");

            dgvbufferout.DataSource = STN1;

            if (user1 == "")
            {
                dgvbufferout.Columns[5].IsVisible = false;
            }

            if (user2 == "")
            {
                dgvbufferout.Columns[6].IsVisible = false;
            }

            if (user3 == "")
            {
                dgvbufferout.Columns[7].IsVisible = false;
            }

            if (user4 == "")
            {
                dgvbufferout.Columns[8].IsVisible = false;
            }

            if (user5 == "")
            {
                dgvbufferout.Columns[9].IsVisible = false;
            }

            if (user6 == "")
            {
                dgvbufferout.Columns[10].IsVisible = false;
            }

            if (user7 == "")
            {
                dgvbufferout.Columns[11].IsVisible = false;
            }

            if (user8 == "")
            {
                dgvbufferout.Columns[12].IsVisible = false;
            }

            if (user9 == "")
            {
                dgvbufferout.Columns[13].IsVisible = false;
            }

            if (user10 == "")
            {
                dgvbufferout.Columns[14].IsVisible = false;
            }

            dgvbufferout.Columns[17].IsVisible = false;
            dgvbufferout.Columns[19].IsVisible = false;
            dgvbufferout.Columns[20].IsVisible = false;
            dgvbufferout.Columns[21].IsVisible = false;

            //Read Only Columns
            dgvbufferout.Columns[0].ReadOnly = true;
            dgvbufferout.Columns[1].ReadOnly = true;
            dgvbufferout.Columns[2].ReadOnly = true;
            dgvbufferout.Columns[3].ReadOnly = true;
            dgvbufferout.Columns[4].ReadOnly = true;
            dgvbufferout.Columns[5].ReadOnly = true;
            dgvbufferout.Columns[6].ReadOnly = true;
            dgvbufferout.Columns[7].ReadOnly = true;
            dgvbufferout.Columns[8].ReadOnly = true;
            dgvbufferout.Columns[9].ReadOnly = true;
            dgvbufferout.Columns[10].ReadOnly = true;
            dgvbufferout.Columns[11].ReadOnly = true;
            dgvbufferout.Columns[12].ReadOnly = true;
            dgvbufferout.Columns[13].ReadOnly = true;
            dgvbufferout.Columns[14].ReadOnly = true;
            dgvbufferout.Columns[15].ReadOnly = true;

            //get all the buffer group
            SqlDataAdapter sda = new SqlDataAdapter("Select V_BUFFER_GROUP_DESC from BUFFER_GROUP", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                cmbbuffergroup.Items.Add(dt.Rows[j][0].ToString());
                cmbbuffergroup.SelectedIndex = 0;
            }
            sda.Dispose();

            //get the selected controller
            select_controller();
        }

        private void cmbcontroller_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            select_controller();
        }

        public void select_controller()
        {
            dc.OpenConnection();  //Open connection
            String ipaddress = "";
            String controller = "";

            //get the ip address and port number of the selected controller
            SqlCommand cmd = new SqlCommand("select V_CONTROLLER from Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                controller = sdr.GetValue(0).ToString();
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
                return;
            }
            
            dc.Close_Connection();  //close the connection if open
            dc.OpenMYSQLConnection(ipaddress);  //open connection

            btnrefresh.Visible = true;
            Buffer_Station();  //get the buffer hangers
        }

        public void Buffer_Station()
        {
            //clear datatables and grid
            STN.Rows.Clear();
            dgvbuffer.DataSource = STN;
            STN1.Rows.Clear();
            dgvbufferout.DataSource = STN1;

            //get the buffergroup id 
            String buffergroup = "";
            SqlCommand cmd = new SqlCommand("select V_BUFFER_GROUP_ID from BUFFER_GROUP where V_BUFFER_GROUP_DESC='" + cmbbuffergroup.Text + "'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                buffergroup= sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get all the stations for that buffer group
            String bufferstation = "";
            SqlDataAdapter sda = new SqlDataAdapter("Select V_BUFFER_STATION_ID from BUFFER_STATION where V_BUFFER_GROUP_ID='" + buffergroup + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                bufferstation += " STN_ID='" + dt.Rows[j][0].ToString() + "' or";
            }
            sda.Dispose();

            //check if buffer group has station assigned to it
            if (bufferstation.Length > 0)
            {
                bufferstation = bufferstation.Remove(bufferstation.Length - 1, 1);
                bufferstation = bufferstation.Remove(bufferstation.Length - 1, 1);
            }
            else
            {
                radLabel8.Text = "Create the Buffer Group and Assign Stations to that Group";
                return;
            }

            //get the MO from the buffer for the buffer group
            MySqlDataAdapter sda1 = new MySqlDataAdapter("Select distinct MO_NO,MO_LINE from bufferhangers where " + bufferstation, dc.conn);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            sda1.Dispose();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                String MO = dt1.Rows[i][0].ToString();
                String MOLINE = dt1.Rows[i][1].ToString();

                int count1 = 0;
                int count = 0;

                //get the count of the hanger for that station
                sda = new SqlDataAdapter("Select V_BUFFER_STATION_ID from BUFFER_STATION where V_BUFFER_GROUP_ID='" + buffergroup + "'", dc.con);
                dt = new DataTable();
                sda.Fill(dt);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    //get the count of hanger in the bufferhangers
                    MySqlCommand cmd2 = new MySqlCommand("Select count(*) from bufferhangers where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "' and STN_ID='" + dt.Rows[j][0].ToString() + "'", dc.conn);
                    count += int.Parse(cmd2.ExecuteScalar() + "");

                    //get the count of hanger in the buffercallout
                    cmd2 = new MySqlCommand("Select sum(COUNT) from buffercallout where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "' and STN_ID='" + dt.Rows[j][0].ToString() + "'", dc.conn);
                    String temp = cmd2.ExecuteScalar() + "";
                    if (temp != "")
                    {
                        count1 += int.Parse(temp);
                    }

                    Thread.Sleep(100);
                }
                sda.Dispose();

                //hanger count in buffer - hanger count in buffercallout
                count = count - count1;
                if (count < 0)
                {
                    count = 0;
                }

                //check if the MO is in auto buffer
                String autobuffer = "FALSE";
                MySqlCommand cmd1 = new MySqlCommand("Select COUNT(*) from autobufferlink where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
                int temp1 = int.Parse(cmd1.ExecuteScalar() + "");
                if (temp1 > 0)
                {
                    autobuffer = "TRUE";
                }

                //MO details
                String color = "";
                String article = "";
                String size = "";
                String user1 = "";
                String user2 = "";
                String user3 = "";
                String user4 = "";
                String user5 = "";
                String user6 = "";
                String user7 = "";
                String user8 = "";
                String user9 = "";
                String user10 = "";

                //get the MO details for that MO
                sda = new SqlDataAdapter("Select V_COLOR_ID,V_ARTICLE_ID,V_SIZE_ID,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10 from MO_DETAILS where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                DataTable dt3 = new DataTable();
                sda.Fill(dt3);
                for (int j = 0; j < dt3.Rows.Count; j++)
                {
                    color = dt3.Rows[j][0].ToString();
                    article = dt3.Rows[j][1].ToString();
                    size = dt3.Rows[j][2].ToString();
                    user1 = dt3.Rows[j][3].ToString();
                    user2 = dt3.Rows[j][4].ToString();
                    user3 = dt3.Rows[j][5].ToString();
                    user4 = dt3.Rows[j][6].ToString();
                    user5 = dt3.Rows[j][7].ToString();
                    user6 = dt3.Rows[j][8].ToString();
                    user7 = dt3.Rows[j][9].ToString();
                    user8 = dt3.Rows[j][10].ToString();
                    user9 = dt3.Rows[j][11].ToString();
                    user10 = dt3.Rows[j][12].ToString();
                }
                sda.Dispose();

                //Get the Description of the Masters
                cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    color = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //Get the Description of the Masters
                cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + article + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    article = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //Get the Description of the Masters
                cmd = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    size = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //Get the Description of the Masters
                cmd = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + user1 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user1 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //Get the Description of the Masters
                cmd = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + user2 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user2 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //Get the Description of the Masters
                cmd = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + user3 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user3 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //Get the Description of the Masters
                cmd = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + user4 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user4 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //Get the Description of the Masters
                cmd = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + user5 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user5 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //Get the Description of the Masters
                cmd = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + user6 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user6 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //Get the Description of the Masters
                cmd = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + user7 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user7 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //Get the Description of the Masters
                cmd = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + user8 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user8 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //Get the Description of the Masters
                cmd = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + user9 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user9 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //Get the Description of the Masters
                cmd = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + user10 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user10 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                String station = ",";
                String station1 = ",";
                String sorting_stn = ",";
                int seq1 = 0;
                
                String sort = "False";

                //get the sequence no for the buffer stations 
                MySqlDataAdapter cmd3 = new MySqlDataAdapter("SELECT s.STN_ID,s.SEQ_NO from sequencestations s,stationdata sd WHERE s.MO_NO='" + MO + "' AND s.MO_LINE='" + MOLINE + "' AND sd.STATIONTYPE='3' AND s.STN_ID=sd.STN_ID", dc.conn);
                DataTable d1 = new DataTable();
                cmd3.Fill(d1);
                cmd3.Dispose();
                for (int i1 = 0; i1 < d1.Rows.Count; i1++)
                {                
                    //check if the station contains in that buffer group
                    if (!bufferstation.Contains(d1.Rows[i1][0].ToString()))
                    {
                        continue;
                    }

                    station1 = "";
                    station = "";
                    int nextseq = int.Parse(d1.Rows[i1][1].ToString());
                    nextseq += 1;

                    //get the station id of the next station after buffer
                    MySqlDataAdapter sda3 = new MySqlDataAdapter("select STN_ID from sequencestations where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "' and SEQ_NO='" + nextseq + "'", dc.conn);
                    DataTable dt4 = new DataTable();
                    sda3.Fill(dt4);
                    sda3.Dispose();
                    for (int k = 0; k < dt4.Rows.Count; k++)
                    {
                        //get the station type of the station after buffer
                        SqlDataAdapter sequence = new SqlDataAdapter("select I_STATION_TYPE from  STATION_DATA where I_STN_ID='" + dt4.Rows[k][0].ToString() + "'", dc.con);
                        DataTable dtseq = new DataTable();
                        sequence.Fill(dtseq);
                        sequence.Dispose();
                        for (int q = 0; q < dtseq.Rows.Count; q++)
                        {
                            //check if the next station is sorting station
                            if (dtseq.Rows[q][0].ToString() == "7")
                            {
                                sort = "True";
                                seq1 = nextseq + 1;
                                sorting_stn = sorting_stn + dt4.Rows[k][0].ToString() + ",";

                                //get the next stations after the sorting station
                                sda3 = new MySqlDataAdapter("select STN_ID from sequencestations where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "' and SEQ_NO='" + seq1 + "'", dc.conn);
                                DataTable dt5 = new DataTable();
                                sda3.Fill(dt5);
                                sda3.Dispose();
                                for (int m = 0; m < dt5.Rows.Count; m++)
                                {
                                    station1 = station1 + dt5.Rows[m][0].ToString() + ",";

                                    //get the station no of the stations
                                    sequence = new SqlDataAdapter("select I_INFEED_LINE_NO,I_STN_NO_INFEED from  STATION_DATA where I_STN_ID='" + dt5.Rows[m][0].ToString() + "'", dc.con);
                                    DataTable dtseq1 = new DataTable();
                                    sequence.Fill(dtseq1);
                                    sequence.Dispose();
                                    for (int n = 0; n < dtseq1.Rows.Count; n++)
                                    {
                                        station = station + dtseq1.Rows[n][0].ToString() + "." + dtseq1.Rows[n][1].ToString() + ",";
                                    }
                                }
                            }
                            else
                            {
                                seq1 = nextseq;
                                station1 = station1 + dt4.Rows[k][0].ToString() + ",";

                                //get the station no of stations
                                sequence = new SqlDataAdapter("select I_INFEED_LINE_NO,I_STN_NO_INFEED from  STATION_DATA where I_STN_ID='" + dt4.Rows[k][0].ToString() + "'", dc.con);
                                DataTable dtseq1 = new DataTable();
                                sequence.Fill(dtseq1);
                                sequence.Dispose();
                                for (int n = 0; n < dtseq1.Rows.Count; n++)
                                {
                                    station = station + dtseq1.Rows[n][0].ToString() + "." + dtseq1.Rows[n][1].ToString() + ",";
                                }
                            }
                        }
                    }                    
                }

                if(station.Length>0)
                {
                    station = station.Remove(station.Length - 1, 1);
                    station1 = station1.Remove(station1.Length - 1, 1);
                    sorting_stn = sorting_stn.Remove(sorting_stn.Length - 1, 1);
                }

                //add to datatable
                STN.Rows.Add(false, MO, MOLINE, color, article, size, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, count, seq1, station, station1, sort, sorting_stn, autobuffer);
            }
            dgvbuffer.DataSource = STN;
            panel3.Visible = false;
            panel5.Visible = false;
            if (dgvbuffer.Rows.Count > 0)
            {
                RowSelected(0);
            }
        }
        
        private void radLabel8_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            radPanel2.Visible = true;
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
        }

        System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel8.Text = "";
            radPanel2.Visible = false;
            MyTimer.Stop();
        }        

        public void RowSelected(int e)
        {
            String sort = "False";
            if (dgvbuffer.SelectedRows.Count == 0)
            {
                return;
            }

            if (e < 0)
            {
                return;
            }

            //check if the mo is selected
            if ((bool)dgvbuffer.Rows[e].Cells["Select"].Value)
            {
                dgvbuffer.Rows[e].Cells["Select"].Value = false;

                for (int i = 0; i < dgvbufferout.Rows.Count; i++)
                {
                    if (dgvbufferout.Rows[i].Cells[0].Value == dgvbuffer.Rows[e].Cells[1].Value && dgvbufferout.Rows[i].Cells[1].Value == dgvbuffer.Rows[e].Cells[2].Value)
                    {
                        dgvbufferout.Rows.RemoveAt(i);
                    }
                }

                if (dgvbufferout.Rows.Count == 0)
                {
                    panel5.Visible = false;
                    panel3.Visible = false;
                    btnbuffercallout.Enabled = false;
                }
            }
            else
            {
                panel5.Visible = true;
                panel3.Visible = true;
                btnbuffercallout.Enabled = true;
                dgvbufferout.Visible = true;
                dgvbuffer.Rows[e].Cells["Select"].Value = true;

                //get the selected mo details
                String MO = dgvbuffer.Rows[e].Cells[1].Value.ToString();
                String MOLINE = dgvbuffer.Rows[e].Cells[2].Value.ToString();
                String color = dgvbuffer.Rows[e].Cells[3].Value.ToString();
                String article = dgvbuffer.Rows[e].Cells[4].Value.ToString();
                String size = dgvbuffer.Rows[e].Cells[5].Value.ToString();
                String user1 = dgvbuffer.Rows[e].Cells[6].Value.ToString();
                String user2 = dgvbuffer.Rows[e].Cells[7].Value.ToString();
                String user3 = dgvbuffer.Rows[e].Cells[8].Value.ToString();
                String user4 = dgvbuffer.Rows[e].Cells[9].Value.ToString();
                String user5 = dgvbuffer.Rows[e].Cells[10].Value.ToString();
                String user6 = dgvbuffer.Rows[e].Cells[11].Value.ToString();
                String user7 = dgvbuffer.Rows[e].Cells[12].Value.ToString();
                String user8 = dgvbuffer.Rows[e].Cells[13].Value.ToString();
                String user9 = dgvbuffer.Rows[e].Cells[14].Value.ToString();
                String user10 = dgvbuffer.Rows[e].Cells[15].Value.ToString();
                String count = dgvbuffer.Rows[e].Cells[16].Value.ToString();
                String nextseq = dgvbuffer.Rows[e].Cells[17].Value.ToString();
                String nextstation = dgvbuffer.Rows[e].Cells[18].Value.ToString();
                String nextstation1 = dgvbuffer.Rows[e].Cells[19].Value.ToString();
                sort = dgvbuffer.Rows[e].Cells[20].Value.ToString();
                String sort_station = dgvbuffer.Rows[e].Cells[21].Value.ToString();

                //add the selected mo to the next grid 
                STN1.Rows.Add(MO, MOLINE, color, article, size, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, count, "1", nextseq, nextstation, nextstation1, sort, sort_station);
                dgvbufferout.DataSource = STN1;

                int row = dgvbufferout.Rows.Count;
                //dgvbufferout.Rows[row - 1].Cells[18].Style = new DataGridViewCellStyle { BackColor = Color.SeaGreen };
            }
        }

        System.Windows.Forms.Timer bufferout = new System.Windows.Forms.Timer();

        private void bufferout_Tick(object sender, EventArgs e)
        {
            //get the count of hangers in buffercallout
            int count = 0;
            MySqlCommand cmd1 = new MySqlCommand("Select count(*) from buffercallout", dc.conn);
            count = int.Parse(cmd1.ExecuteScalar() + "");
            if (count == 0)
            {
                btnsortout.Enabled = true;
                //btnsortout.PerformClick();
                bufferout.Stop();
                btnrefresh.PerformClick();
            }
        }

        private void btnstoploading_Click(object sender, EventArgs e)
        {
            buffer_test();
            //bufferout.Interval = 5000; //5 Sec
            //bufferout.Tick += new EventHandler(bufferout_Tick);
            //bufferout.Start();
            //sort_enabled = "True";

            //get all the mo and callout count
            for (int i = 0; i < dgvbufferout.Rows.Count; i++)
            {
                String mo = dgvbufferout.Rows[i].Cells[0].Value.ToString();
                String moline = dgvbufferout.Rows[i].Cells[1].Value.ToString();
                String sequence = dgvbufferout.Rows[i].Cells[17].Value.ToString();
                String station = dgvbufferout.Rows[i].Cells[18].Value.ToString();
                String st = dgvbufferout.Rows[i].Cells[19].Value.ToString();

                if (station == "")
                {
                    radLabel8.Text = "No Next Station";
                    continue;
                }

                String[] stn2 = st.Split(',');
                String[] stn = station.Split(',');

                for (int j = 0; j < stn.Length; j++)
                {
                    String stn_id = stn[j];
                    String[] stn1 = stn_id.Split('.');
                    String stn3 = stn2[j];
                    String station_ID = "";

                    //get the station id
                    SqlCommand cmd = new SqlCommand("select I_STN_ID from STATION_DATA where I_INFEED_LINE_NO='" + stn1[0] + "' and I_STN_NO_INFEED='" + stn1[1] + "'", dc.con);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        station_ID = dataReader.GetValue(0).ToString();
                    }
                    dataReader.Close();

                    //update the station id for next station after buffer
                    MySqlCommand cmd1 = new MySqlCommand("update sequencestations set STN_ID='" + station_ID + "' where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and SEQ_NO='" + sequence + "' and STN_ID='" + stn3 + "'", dc.conn);
                    cmd1.ExecuteNonQuery();

                    //upadte the station id for next station after buffer
                    cmd = new SqlCommand("update STATION_ASSIGN set I_STATION_ID='" + station_ID + "',I_LINE_NO='" + stn1[0] + "',D_STATION_NO='" + stn1[1] + "' where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and I_SEQUENCE_NO='" + sequence + "' and I_STATION_ID='" + stn3 + "'", dc.con);
                    cmd.ExecuteNonQuery();
                }
            }
            radLabel8.Text = "Buffer Call Out Successful";
            Buffer_Station();  // get buffer details
            btnbuffercallout.Enabled = false;
        }

        public void buffer_test()
        {
            try
            {
                DataTable buffer = new DataTable();
                buffer.Columns.Add("MO");
                buffer.Columns.Add("MOLINE");
                buffer.Columns.Add("STATION");
                String selectmo = "";
                int total = 0;
                //SqlDataAdapter sda = new SqlDataAdapter("Select V_MO_NO,V_MO_LINE,I_CALL_OUT_COUNT,I_REPEAT_QUANTITY from SORT_CALL_OUT_SEQUENCE", dc.con);
                //DataTable dt = new DataTable();
                //sda.Fill(dt);
                //sda.Dispose();
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    String mo = dt.Rows[i][0].ToString();
                //    String moline = dt.Rows[i][1].ToString();
                //    int count = int.Parse(dt.Rows[i][2].ToString());
                //    int qty = int.Parse(dt.Rows[i][3].ToString());
                //    for (int j = 0; j < dgvbufferout.Rows.Count; j++)
                //    {
                //        if (dgvbufferout.Rows[j].Cells[0].Value.ToString() == mo && dgvbufferout.Rows[j].Cells[1].Value.ToString() == moline)
                //        {
                //            dgvbufferout.Rows[i].Cells[16].Value = count * qty;
                //        }
                //    }
                //}

                //get the buffer group id
                String buffergroup = "";
                SqlCommand cmd1 = new SqlCommand("select V_BUFFER_GROUP_ID from BUFFER_GROUP where V_BUFFER_GROUP_DESC='" + cmbbuffergroup.Text + "'", dc.con);
                SqlDataReader sdr = cmd1.ExecuteReader();
                if (sdr.Read())
                {
                    buffergroup = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get the stations for that buffer group
                String bufferstation = "";
                SqlDataAdapter sda = new SqlDataAdapter("Select V_BUFFER_STATION_ID from BUFFER_STATION where V_BUFFER_GROUP_ID='" + buffergroup + "'", dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    bufferstation += " STN_ID='" + dt.Rows[j][0].ToString() + "' or";
                }
                sda.Dispose();

                //check if buffer group has station assigned to it
                if (bufferstation.Length > 0)
                {
                    bufferstation = bufferstation.Remove(bufferstation.Length - 1, 1);
                    bufferstation = bufferstation.Remove(bufferstation.Length - 1, 1);
                }
                else
                {
                    radLabel8.Text = "Create the Buffer Group and Assign Stations to that Group";
                    return;
                }

                //get the total call cout count for each selected mo
                for (int j = 0; j < dgvbufferout.Rows.Count; j++)
                {
                    if (dgvbufferout.Rows[j].Cells[16].Value.ToString() != "")
                    {
                        total = total + int.Parse(dgvbufferout.Rows[j].Cells[16].Value.ToString());
                        selectmo = selectmo + dgvbufferout.Rows[j].Cells[0].Value.ToString() + "*" + dgvbufferout.Rows[j].Cells[1].Value.ToString() + "*" + dgvbufferout.Rows[j].Cells[16].Value.ToString() + "+";
                    }
                }

                selectmo = selectmo.Remove(selectmo.Length - 1, 1);
                String[] selectmo1 = selectmo.Split('+');
                int len = selectmo1.Length;
                while (total > 0)
                {
                    //get the station and mo details for that buffer group sort by time
                    MySqlDataAdapter sda1 = new MySqlDataAdapter("Select STN_ID,MO_NO,MO_LINE from bufferhangers where " + bufferstation + " order by time", dc.conn);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    sda1.Dispose();
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        if (total == 0)
                        {
                            break;
                        }

                        String stationid = dt1.Rows[j][0].ToString();
                        String mo = dt1.Rows[j][1].ToString();
                        String moline = dt1.Rows[j][2].ToString();

                        //get all the mo details selected
                        for (int i = 0; i < len; i++)
                        {
                            String[] selectmo2 = selectmo1[i].Split('*');
                            //check if the mo details and count is > than 0
                            if (mo == selectmo2[0] && moline == selectmo2[1] && int.Parse(selectmo2[2]) > 0)
                            {
                                int count = int.Parse(selectmo2[2]);
                                count = count - 1;
                                selectmo1[i] = selectmo2[0] + "*" + selectmo2[1] + "*" + count;
                                buffer.Rows.Add(mo, moline, stationid);
                                total = total - 1;
                            }
                        }
                    }
                }

                int count1 = 1;
                buffer.Rows.Add("", "", "");

                //get all the call out details
                for (int i = 0; i < buffer.Rows.Count - 1; i++)
                {
                    String cur_mo = buffer.Rows[i][0].ToString();
                    String cur_molime = buffer.Rows[i][1].ToString();
                    String cur_stn = buffer.Rows[i][2].ToString();
                    String next_mo = buffer.Rows[i + 1][0].ToString();
                    String next_molime = buffer.Rows[i + 1][1].ToString();
                    String next_stn = buffer.Rows[i + 1][2].ToString();

                    //check if the previous mo and station is same
                    if (cur_mo == next_mo && cur_molime == next_molime && cur_stn == next_stn)
                    {
                        count1 = count1 + 1;
                    }
                    else
                    {
                        //insert into buffercallout
                        MySqlCommand cmd = new MySqlCommand("insert into buffercallout (STN_ID,MO_NO,MO_LINE,COUNT) values('" + cur_stn + "','" + cur_mo + "','" + cur_molime + "','" + count1 + "')", dc.conn);
                        cmd.ExecuteNonQuery();
                        count1 = 1;
                    }
                }
            }
            catch(Exception ex)
            { 
                RadMessageBox.Show(ex+"", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }
               

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            panel3.Visible = false;
            Buffer_Station();
        }

        private void btnsortsequence_Click(object sender, EventArgs e)
        {
            //btnbuffercallout.Enabled = true;
            //delete from sort call out 
            SqlCommand cmd = new SqlCommand("Delete from SORT_CALL_OUT_SEQUENCE", dc.con);
            cmd.ExecuteNonQuery();

            for (int i = 0; i < dgvbufferout.Rows.Count; i++)
            {
                if (dgvbufferout.Rows[i].Cells[20].Value.ToString() == "True")
                {
                    String stnid = dgvbufferout.Rows[i].Cells[21].Value.ToString();

                    if (stnid.Contains(","))
                    {
                        String[] stnid1 = stnid.Split(',');

                        //insert into sort call out sequence
                        cmd = new SqlCommand("insert into SORT_CALL_OUT_SEQUENCE values('" + dgvbufferout.Rows[i].Cells[0].Value.ToString() + "','" + dgvbufferout.Rows[i].Cells[1].Value.ToString() + "','" + dgvbufferout.Rows[i].Cells[15].Value.ToString() + "','1','1','" + stnid1[0] + "')", dc.con);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd = new SqlCommand("insert into SORT_CALL_OUT_SEQUENCE values('" + dgvbufferout.Rows[i].Cells[0].Value.ToString() + "','" + dgvbufferout.Rows[i].Cells[1].Value.ToString() + "','" + dgvbufferout.Rows[i].Cells[15].Value.ToString() + "','1','1','" + stnid + "')", dc.con);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            //show sorting form
            Buffer_Sorting bs = new Buffer_Sorting();
            bs.Show();
        }

        private void btnsortout_Click(object sender, EventArgs e)
        {
            //sort_enabled = "False";

            //get the repeat quantity for sorting
            int qty = 0;
            SqlCommand cmd = new SqlCommand("SELECT I_REPEAT_QUANTITY FROM SORT_CALL_OUT_SEQUENCE", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                qty = int.Parse(sdr.GetValue(0).ToString());
            }
            sdr.Close();

            for (int j = 0; j < qty; j++)
            {
                SqlDataAdapter sda = new SqlDataAdapter("Select V_MO_NO,V_MO_LINE,I_CALL_OUT_COUNT,I_STATION_ID from SORT_CALL_OUT_SEQUENCE", dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String mo = dt.Rows[i][0].ToString();
                    String moline = dt.Rows[i][1].ToString();
                    String count = dt.Rows[i][2].ToString();
                    String stnid = dt.Rows[i][3].ToString();

                    //insert into sortingcallout
                    MySqlCommand cmd1 = new MySqlCommand("insert into sortingcallin (STN_ID,MO_NO,MO_LINE,COUNT) values('" + stnid + "','" + mo + "','" + moline + "','" + count + "')", dc.conn);
                    cmd1.ExecuteNonQuery();
                }
            }

            btnsortout.Enabled = false;

            //delete from SORT_CALL_OUT_SEQUENCE
            cmd = new SqlCommand("Delete from SORT_CALL_OUT_SEQUENCE", dc.con);
            cmd.ExecuteNonQuery();

            radLabel8.Text = "Buffer Call Out Successful";
            Buffer_Station();  //get buffer details
            btnbuffercallout.Enabled = false;
        }

        private void Buffer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (sort_enabled == "True")
            //{
            //    radLabel8.Text = "Sorting is Enabled";
            //    e.Cancel = true;
            //}
        }

        private void Buffer_FormClosed(object sender, FormClosedEventArgs e)
        {
            dc.Close_Connection();  //close connection on form close
            bufferout.Stop();  //stop timer
            this.Dispose();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnviewall_Click(object sender, EventArgs e)
        {
            //open buffer hangers
            Buffer_Hangers bh = new Buffer_Hangers();
            bh.Show();
        }

        private void Buffer_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();  //open connection
            String Lang = "";

            //get the language and theme
            SqlCommand cmd = new SqlCommand("SELECT Language,ThemeName FROM Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Lang = sdr.GetValue(0).ToString();
                theme = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //set the language for the form
            SqlDataAdapter sda = new SqlDataAdapter("select " + Lang + " from Language where Form='Buffer' order by Item_No", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                btnrefresh.Text = dt.Rows[0][0].ToString();
                btnadvanced.Text = dt.Rows[1][0].ToString();
                btnsortsequence.Text = dt.Rows[2][0].ToString();
                btnbuffercallout.Text = dt.Rows[3][0].ToString();
                btnsortout.Text = dt.Rows[4][0].ToString();
                radLabel1.Text = dt.Rows[5][0].ToString();
                radLabel2.Text = dt.Rows[6][0].ToString();
            }

            //change the grid theme
            GridTheme(theme);
        }

        //set the grid theme
        public void GridTheme(String theme)
        {
            dgvbuffer.ThemeName = theme;
            dgvbufferout.ThemeName = theme;
        }


        private void dgvbuffer_SelectionChanged(object sender, EventArgs e)
        {
            //if (dgvbuffer.Rows.Count > 0)
            //{
            //    if (dgvbuffer.CurrentCell.RowIndex > 0)
            //    {
            //        int row = dgvbuffer.CurrentCell.RowIndex;
            //        RowSelected(row);
            //    }
            //}
        }

        private void cmbbuffergroup_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            select_controller();  //get the selected controller
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //open auto buffer
            Auto_Buffer ab = new Auto_Buffer();
            ab.Show();
        }

        private void dgvbuffer_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            RowSelected(row);
            //int btnsort = 0;
            //for (int i = 0; i < dgvbufferout.Rows.Count; i++)
            //{
            //    if (dgvbufferout.Rows[i].Cells[20].Value.ToString() == "True")
            //    {
            //        btnsort = 1;
            //    }
            //}
            //if (btnsort == 1)
            //{
            //    btnsortsequence.Visible = true;
            //    btnbuffercallout.Enabled = false;
            //}
            //else
            //{
            //    btnsortsequence.Visible = false;
            //    btnbuffercallout.Enabled = true;
            //}
        }

        private void dgvbufferout_CellEndEdit(object sender, GridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 18)
            {
                try
                {
                    //change the stations for next sequence after buffer 
                    String station1 = dgvbufferout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    String sort = dgvbufferout.Rows[e.RowIndex].Cells[20].Value.ToString();
                    String[] station = station1.Split(',');

                    for (int i = 0; i < station.Length; i++)
                    {
                        if (station[i].Contains("."))
                        {
                            String[] stn = station[i].Split('.');

                            //get the station id
                            SqlCommand cmd = new SqlCommand("select I_STN_ID from STATION_DATA where I_INFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_INFEED='" + stn[1] + "'", dc.con);
                            SqlDataReader dataReader = cmd.ExecuteReader();
                            if (!(dataReader.Read()))
                            {
                                dgvbufferout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                                radLabel8.Text = "There is no Station " + stn[1] + " in Line " + stn[0];
                                dataReader.Close();
                                return;
                            }
                            dataReader.Close();

                            //get the station type
                            cmd = new SqlCommand("select I_STATION_TYPE from STATION_DATA where I_INFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_INFEED='" + stn[1] + "'", dc.con);
                            dataReader = cmd.ExecuteReader();
                            if (dataReader.Read())
                            {
                                //check if bridge station
                                String stntype = dataReader.GetValue(0).ToString();
                                if (stntype == "2")
                                {
                                    dgvbufferout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                                    radLabel8.Text = "Its a Brigde Station";
                                    dataReader.Close();
                                    return;
                                }

                                //check if overload station
                                if (stntype == "4")
                                {
                                    dgvbufferout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                                    radLabel8.Text = "Its a Overload Station";
                                    dataReader.Close();
                                    return;
                                }

                                //if (sort == "True")
                                //{
                                //    if (stntype != "7")
                                //    {
                                //        dgvbufferout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                                //        radLabel8.Text = "Its not a Sorting Station";
                                //    }
                                //}
                            }
                            dataReader.Close();
                        }
                        else
                        {
                            dgvbufferout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                            radLabel8.Text = "Invalid Station Assign";
                        }
                    }
                }
                catch (Exception ex)
                {
                    radLabel8.Text = ex.Message;
                    RadMessageBox.Show(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
            else
            {
                Regex r = new Regex("^[0-9]*$");

                //get the callout count and check if the its less than hanger count and greater than 0
                String c = dgvbufferout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (!(r.IsMatch(c)) || c == "0" || c == "")
                {
                    radLabel8.Text = "Call Out Count should be greater than Zero";
                    dgvbufferout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "1";
                    return;
                }

                int actualcount = int.Parse(dgvbufferout.Rows[e.RowIndex].Cells[15].Value.ToString());
                int count = int.Parse(c);

                if (count > actualcount)
                {
                    radLabel8.Text = "Call Out Count is more than Anctual Hanger Count";
                    dgvbufferout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = actualcount;
                }
                else
                {
                    btnbuffercallout.Enabled = true;
                }
            }
        }

        private void dgvbuffer_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change the fore color of the grid if these themes are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvbuffer.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvbuffer.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvbuffer.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvbuffer.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvbufferout_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change the fore color of the grid if these themes are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvbufferout.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvbufferout.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvbufferout.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvbufferout.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }
    }
}
