using Microsoft.Reporting.WinForms;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualBasic;
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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Add_Production : Telerik.WinControls.UI.RadForm
    {
        public Add_Production()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection(); //Connection Class
        DataTable data1 = new DataTable();  //Datatable for Reports
        DataTable MO2 = new DataTable(); //Datatable for MO
        String theme = "";

        private void Add_Production_Load(object sender, EventArgs e)
        {
            dgvmo.MasterTemplate.SelectLastAddedRow = false;
            RadMessageBox.SetThemeName("FluentDark"); //Message Box Theme
            dgvmo.MasterView.TableSearchRow.ShowCloseButton = false; //Disable Close Button for Grid Search
            reportViewer1.Visible = false; //Set Visible False by Default

            //Add Columns for Reports
            DataSet SET = new DataSet("SEQ");
            data1.Columns.Add("MONO");
            data1.Columns.Add("MODETAILS");
            data1.Columns.Add("COLOR");
            data1.Columns.Add("SIZE");
            data1.Columns.Add("ARTICLE");
            data1.Columns.Add("USER1");
            data1.Columns.Add("USER2");
            data1.Columns.Add("USER3");
            data1.Columns.Add("USER4");
            data1.Columns.Add("USER5");
            data1.Columns.Add("USER6");
            data1.Columns.Add("USER7");
            data1.Columns.Add("USER8");
            data1.Columns.Add("USER9");
            data1.Columns.Add("USER10");
            data1.Columns.Add("QUANTITY");
            data1.Columns.Add("LOADING_STATION");

            SET.Tables.Add(data1);

            dc.OpenConnection();      //Open Connection     
            radPanel2.Visible = false;

            RefereshGrid(); //Refresh the Grid
            select_controller(); //Get the Selected Controller IP address
        }

        public void RefereshGrid()
        {
            //Clear Datatable
            MO2.Rows.Clear();
            MO2.Columns.Clear();

            //Special Fields
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

            //get the Special Field names 
            SqlCommand cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF1' and V_ENABLED='TRUE'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user1 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the Special Field names 
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user2 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the Special Field names 
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user3 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the Special Field names 
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user4 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the Special Field names 
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user5 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the Special Field names 
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF6' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user6 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the Special Field names 
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF7' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user7 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the Special Field names 
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF8' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user8 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the Special Field names 
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF9' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user9 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get the Special Field names 
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF10' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user10 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //add columns to grid view
            DataTable MO = new DataTable();
            MO.Columns.Add("MO No");
            MO.Columns.Add("MO Details");
            MO.Columns.Add("Color");
            MO.Columns.Add("Size");
            MO.Columns.Add("Article ID");
            MO.Columns.Add("Article");
            MO.Columns.Add(user1);
            MO.Columns.Add(user2);
            MO.Columns.Add(user3);
            MO.Columns.Add(user4);
            MO.Columns.Add(user5);
            MO.Columns.Add(user6);
            MO.Columns.Add(user7);
            MO.Columns.Add(user8);
            MO.Columns.Add(user9);
            MO.Columns.Add(user10);
            MO.Columns.Add("Quantity");
            MO.Columns.Add("Status");
            MO.Columns.Add("Loading Station");
            MO.Columns.Add("ID");
            MO.Columns.Add("Hanger Count");
            MO.Columns.Add("Last Updated");

            MO2.Columns.Add("MO No");
            MO2.Columns.Add("MO Details");
            MO2.Columns.Add("Color");
            MO2.Columns.Add("Size");
            MO2.Columns.Add("Article ID");
            MO2.Columns.Add("Article");
            MO2.Columns.Add(user1);
            MO2.Columns.Add(user2);
            MO2.Columns.Add(user3);
            MO2.Columns.Add(user4);
            MO2.Columns.Add(user5);
            MO2.Columns.Add(user6);
            MO2.Columns.Add(user7);
            MO2.Columns.Add(user8);
            MO2.Columns.Add(user9);
            MO2.Columns.Add(user10);
            MO2.Columns.Add("Quantity");
            MO2.Columns.Add("Status");
            MO2.Columns.Add("Loading Station");
            MO2.Columns.Add("ID");
            MO2.Columns.Add("Hanger Count");
            MO2.Columns.Add("Last Updated");

            dgvmo.DataSource = MO;

            dgvmo.Columns[0].Width = 70;
            dgvmo.Columns[1].Width = 70;
            dgvmo.Columns[17].Width = 70;
            dgvmo.Columns[19].Width = 70;
            dgvmo.Columns[21].Width = 70;

            //hide the user defined names if they are not enabled
            dgvmo.Columns[17].IsVisible = false;
            dgvmo.Columns[19].IsVisible = false;
            if (user1 == "")
            {
                dgvmo.Columns[6].IsVisible = false;
            }

            if (user2 == "")
            {
                dgvmo.Columns[7].IsVisible = false;
            }

            if (user3 == "")
            {
                dgvmo.Columns[8].IsVisible = false;
            }

            if (user4 == "")
            {
                dgvmo.Columns[9].IsVisible = false;
            }

            if (user5 == "")
            {
                dgvmo.Columns[10].IsVisible = false;
            }

            if (user6 == "")
            {
                dgvmo.Columns[11].IsVisible = false;
            }

            if (user7 == "")
            {
                dgvmo.Columns[12].IsVisible = false;
            }

            if (user8 == "")
            {
                dgvmo.Columns[13].IsVisible = false;
            }

            if (user9 == "")
            {
                dgvmo.Columns[14].IsVisible = false;
            }

            if (user10 == "")
            {
                dgvmo.Columns[15].IsVisible = false;
            }

            //get the mo from the modetails table which are assigned the station only
            SqlDataAdapter sda = new SqlDataAdapter("SELECT DISTINCT MO.V_MO_NO,MO.V_COLOR_ID,MO.V_SIZE_ID,MO.V_ARTICLE_ID,MO.I_ORDER_QTY,MO.V_USER_DEF1,MO.V_USER_DEF2,MO.V_USER_DEF3,MO.V_USER_DEF4,MO.V_USER_DEF5,MO.V_USER_DEF6,MO.V_USER_DEF7,MO.V_USER_DEF8,MO.V_USER_DEF9,MO.V_USER_DEF10,MO.V_MO_LINE,MO.V_STATUS,MO.I_ID,MO.I_HANGER_COUNT,MO.D_LAST_UPDATED FROM MO_DETAILS MO ,STATION_ASSIGN SA where MO.V_MO_NO=SA.V_MO_NO and MO.V_MO_LINE=SA.V_MO_LINE and MO.V_STATUS!='COMP' order by MO.I_ID DESC", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String articleDesc = "";
                String mo = dt.Rows[i][0].ToString();
                String color = dt.Rows[i][1].ToString();
                String size = dt.Rows[i][2].ToString();
                String articleID = dt.Rows[i][3].ToString();
                String qty = dt.Rows[i][4].ToString();
                user1 = dt.Rows[i][5].ToString();
                user2 = dt.Rows[i][6].ToString();
                user3 = dt.Rows[i][7].ToString();
                user4 = dt.Rows[i][8].ToString();
                user5 = dt.Rows[i][9].ToString();
                user6 = dt.Rows[i][10].ToString();
                user7 = dt.Rows[i][11].ToString();
                user8 = dt.Rows[i][12].ToString();
                user9 = dt.Rows[i][13].ToString();
                user10 = dt.Rows[i][14].ToString();
                String moline = dt.Rows[i][15].ToString();
                String status = dt.Rows[i][16].ToString();
                String hanger_count = dt.Rows[i][18].ToString();
                String id = i.ToString();
                String last_update = dt.Rows[i][19].ToString();
                //get the descriptions of the color,article etc

                cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    color = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + articleID + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    articleDesc = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    size = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + user1 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user1 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + user2 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user2 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + user3 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user3 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + user4 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user4 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + user5 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user5 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + user6 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user6 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + user7 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user7 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + user8 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user8 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + user9 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user9 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + user10 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user10 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                String STATIONID = "";

                cmd = new SqlCommand("select I_SEQUENCE_NO from STATION_ASSIGN where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and I_STATION_ID!='0' and V_ASSIGN_TYPE=(select V_ASSIGN_TYPE from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "') ORDER BY I_SEQUENCE_NO", dc.con);
                String prev_seq = cmd.ExecuteScalar() + "";

                //Get the Loading Stations
                SqlDataAdapter sequence = new SqlDataAdapter("select I_LINE_NO,D_STATION_NO from STATION_ASSIGN where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and I_STATION_ID!='0' and I_SEQUENCE_NO='" + prev_seq + "' and V_ASSIGN_TYPE=(select V_ASSIGN_TYPE from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "')", dc.con);
                DataTable dtseq = new DataTable();
                sequence.Fill(dtseq);
                sequence.Dispose();
                for (int j = 0; j < dtseq.Rows.Count; j++)
                {
                    STATIONID += dtseq.Rows[j][0].ToString() + "." + dtseq.Rows[j][1].ToString() + ",";
                }

                //if (dtseq.Rows.Count > 0)
                //{
                //    int j = dtseq.Rows.Count - 1;
                //    STATIONID += dtseq.Rows[j][0].ToString() + "." + dtseq.Rows[j][1].ToString();
                //}


                if (STATIONID.Length > 0)
                {
                    STATIONID = STATIONID.Remove(STATIONID.Length - 1, 1);
                }

                //Add the MO Details into Grid and Report
                MO.Rows.Add(mo, moline, color, size, articleID, articleDesc, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, qty, status, STATIONID, id, hanger_count, last_update);
                data1.Rows.Add(mo, moline, color, size,  user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, qty, STATIONID);
            }
            dgvmo.DataSource = MO;

            //Get the Station Assign Version 
            if (dgvmo.Rows.Count > 0)
            {
                cmbstationassign.Items.Clear();
                sda = new SqlDataAdapter("select distinct V_ASSIGN_TYPE from STATION_ASSIGN where V_MO_NO='" + dgvmo.Rows[0].Cells[0].Value.ToString() + "' and V_MO_LINE='" + dgvmo.Rows[0].Cells[1].Value.ToString() + "'", dc.con);
                dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbstationassign.Items.Add(dt.Rows[i][0].ToString());
                }

                sda = new SqlDataAdapter("select V_ASSIGN_TYPE from MO_DETAILS where V_MO_NO='" + dgvmo.Rows[0].Cells[0].Value.ToString() + "' and V_MO_LINE='" + dgvmo.Rows[0].Cells[1].Value.ToString() + "'", dc.con);
                dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbstationassign.Text = dt.Rows[i][0].ToString();
                }
            }
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            try
            {
                //Check if the Version is selected
                if (cmbstationassign.Text == "")
                {
                    radLabel4.Text = "Please Select a Version";
                    return;
                }

                //Get the Selected MO Details
                if (dgvmo.SelectedRows.Count > 0)
                {                    
                    String MO = dgvmo.SelectedRows[0].Cells[0].Value.ToString();
                    String MOLINE = dgvmo.SelectedRows[0].Cells[1].Value.ToString();
                    String color= dgvmo.SelectedRows[0].Cells[2].Value.ToString();
                    String Size = dgvmo.SelectedRows[0].Cells[3].Value.ToString();
                    String article = dgvmo.SelectedRows[0].Cells[5].Value.ToString();
                    String User1 = dgvmo.SelectedRows[0].Cells[6].Value.ToString();
                    String User2 = dgvmo.SelectedRows[0].Cells[7].Value.ToString();
                    String User3 = dgvmo.SelectedRows[0].Cells[8].Value.ToString();
                    String User4 = dgvmo.SelectedRows[0].Cells[9].Value.ToString();
                    String User5 = dgvmo.SelectedRows[0].Cells[10].Value.ToString();
                    String User6 = dgvmo.SelectedRows[0].Cells[11].Value.ToString();
                    String User7 = dgvmo.SelectedRows[0].Cells[12].Value.ToString();
                    String User8 = dgvmo.SelectedRows[0].Cells[13].Value.ToString();
                    String User9 = dgvmo.SelectedRows[0].Cells[14].Value.ToString();
                    String User10 = dgvmo.SelectedRows[0].Cells[15].Value.ToString();
                    String Qty = dgvmo.SelectedRows[0].Cells[16].Value.ToString();

                    //check if the controller is selected or not
                    if (cmbcontroller.Text == "--SELECT--" || cmbcontroller.Text == "")
                    {
                        radLabel4.Text = "Please Select the Controller";
                        return;
                    }

                    //Update the MO Status to PROD
                    SqlCommand cmd1 = new SqlCommand("update MO_DETAILS set V_STATUS='PROD',V_ASSIGN_TYPE='" + cmbstationassign.Text + "' where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                    cmd1.ExecuteNonQuery();

                    //get count of rows from the prod table with the mo selected
                    MySqlCommand cmd = new MySqlCommand("Select count(*) from prod where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
                    int count = int.Parse(cmd.ExecuteScalar() + "");
                    if (count != 0)
                    {
                        //if the mo exits in prod table show message
                        radLabel4.Text = MO + "-" + MOLINE + " Already in Production";
                        return;
                    }

                    //insert into prod table
                    cmd = new MySqlCommand("insert into prod (MO_NO,MO_LINE,IN_PROD,CUR_COUNT,PC_COUNT,MAX_COUNT) values('" + MO + "','" + MOLINE + "','0','0','1','" + Qty + "')", dc.conn);
                    cmd.ExecuteNonQuery();
                    
                    //insert into modetails table
                    cmd = new MySqlCommand("insert into modetails (MO_NO,MO_LINE,COLOR,ARTICLE,SIZE,USER1,USER2,USER3,USER4,USER5,USER6,USER7,USER8,USER9,USER10,QUANTITY) values('" + MO + "','" + MOLINE + "','" + color + "','" + article + "','" + Size + "','" + User1 + "','" + User2 + "','" + User3 + "','" + User4 + "','" + User5 + "','" + User6 + "','" + User7 + "','" + User8 + "','" + User9 + "','" + User10 + "','" + Qty + "')", dc.conn);
                    cmd.ExecuteNonQuery();

                    //Update Station Assign Button Click
                    btnupdatestn.PerformClick();

                    radLabel4.Text = MO + "-" + MOLINE + " is in Production";
                    dgvmo.SelectedRows[0].Cells[17].Value = "PROD";
                }
                else
                {
                    radLabel4.Text = "Please Select a Row";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void radTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //Enter Key Press event
            if (e.KeyCode == Keys.Enter)
            {
                btnsearch.PerformClick();
            }
        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            try
            {
                btnshowall.PerformClick();
                //search button select the row
                for (int i = 0; i < dgvmo.Rows.Count; i++)
                {
                    if (dgvmo.Rows[i].Cells[0].Value.ToString().Equals(txtmo.Text))
                    {
                        dgvmo.Rows[i].IsVisible = true;
                    }
                    else
                    {
                        //Suspend the Grid Datasource and Resume
                        CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dgvmo.DataSource];
                        currencyManager1.SuspendBinding();
                        dgvmo.Rows[i].IsVisible = false;
                        currencyManager1.ResumeBinding();
                    }
                }
            }
            catch(Exception ex)
            {
                RadMessageBox.Show(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void radDropDownList1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //Enable the Buttons if the Version is Selected
            btnaddproduction.Enabled = true;
            btnstartloading.Enabled = true;
            btnstoploading.Enabled = true;
            btnupdatestn.Enabled = true;
        }

        public void select_controller()
        {            
            dc.OpenConnection(); //Open the Connection
            String ipaddress = "";
            String controller = "";
            //get the ip address and port number of the selected controller

            //Get the Selected Controller Name
            SqlCommand cmd = new SqlCommand("select V_CONTROLLER from Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                controller = sdr.GetValue(0).ToString();
                cmbcontroller.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            
            //Get the IP Address of the Selected Controller
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
                cmbcontroller.Text = "--SELECT--";
                return;               
            }

            dc.Close_Connection(); //Close the Connection if is Open
            dc.OpenMYSQLConnection(ipaddress); //Open the Connection

            //Get the MO 
            for (int i = 0; i < dgvmo.Rows.Count; i++)
            {
                String MO1 = dgvmo.Rows[i].Cells[0].Value.ToString();
                String MOLINE1 = dgvmo.Rows[i].Cells[1].Value.ToString();
                String status = "";

                //get the mo which are loading
                MySqlCommand cmd1 = new MySqlCommand("Select IN_PROD from prod where MO_NO='" + MO1 + "' and MO_LINE='" + MOLINE1 + "'", dc.conn);
                MySqlDataReader sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    status = sdr1.GetValue(0).ToString();
                    //if loading then change the row header color to green
                    if (status == "1")
                    {
                        dgvmo.Rows[i].Cells[17].Value = "LOADING";
                    }
                    else
                    {
                        dgvmo.Rows[i].Cells[17].Value = "PROD";
                    }
                }
                else
                {
                    dgvmo.Rows[i].Cells[17].Value = "ZZZZZ";
                }
                sdr1.Close();

                MO2.Rows.Add(dgvmo.Rows[i].Cells[0].Value.ToString(), dgvmo.Rows[i].Cells[1].Value.ToString(), dgvmo.Rows[i].Cells[2].Value.ToString(), dgvmo.Rows[i].Cells[3].Value.ToString(), dgvmo.Rows[i].Cells[4].Value.ToString(), dgvmo.Rows[i].Cells[5].Value.ToString(), dgvmo.Rows[i].Cells[6].Value.ToString(), dgvmo.Rows[i].Cells[7].Value.ToString(), dgvmo.Rows[i].Cells[8].Value.ToString(), dgvmo.Rows[i].Cells[9].Value.ToString(), dgvmo.Rows[i].Cells[10].Value.ToString(), dgvmo.Rows[i].Cells[11].Value.ToString(), dgvmo.Rows[i].Cells[12].Value.ToString(), dgvmo.Rows[i].Cells[13].Value.ToString(), dgvmo.Rows[i].Cells[14].Value.ToString(), dgvmo.Rows[i].Cells[15].Value.ToString(), dgvmo.Rows[i].Cells[16].Value.ToString(), dgvmo.Rows[i].Cells[17].Value.ToString(), dgvmo.Rows[i].Cells[18].Value.ToString(), dgvmo.Rows[i].Cells[19].Value.ToString(), dgvmo.Rows[i].Cells[20].Value.ToString(), dgvmo.Rows[i].Cells[21].Value.ToString());
            }

            //Sort On MO which is Loading
            DataView dv = new DataView(MO2);
            dv.Sort = "Status ASC,ID DESC";
            dgvmo.DataSource = dv;

            if (dgvmo.RowCount > 0)
            {
                dgvmo.Rows[0].IsSelected = true;
            }
        }

        private void radLabel4_TextChanged(object sender, EventArgs e)
        {
            //message to disable after 5 sec 
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            radPanel2.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel4.Text = "";
            radPanel2.Visible = false;
            MyTimer.Stop();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            String MO = dgvmo.SelectedRows[0].Cells[0].Value.ToString();
            String MOLINE = dgvmo.SelectedRows[0].Cells[1].Value.ToString();
            String STATIONID = dgvmo.SelectedRows[0].Cells[18].Value.ToString() + ",";

            //Check if Controller is selected or not
            if (cmbcontroller.Text == "--SELECT--" || cmbcontroller.Text == "")
            {
                radLabel4.Text = "Please Select the Controller";
                return;
            }

            //See if the MO has same loading Station,if same show Message to Change Loading Station of this MO or Stop Laoding of the that MO
            MySqlDataAdapter loading = new MySqlDataAdapter("SELECT p.MO_NO,p.MO_LINE,s.STN_ID FROM prod p,sequencestations s WHERE s.MO_NO=p.MO_NO AND s.MO_LINE=p.MO_LINE AND s.SEQ_NO='1' AND p.IN_PROD='1'", dc.conn);
            DataTable dtload = new DataTable();
            loading.Fill(dtload);
            loading.Dispose();
            for (int i = 0; i < dtload.Rows.Count; i++)
            {
                String MO1 = dtload.Rows[i][0].ToString();
                String MOLINE1 = dtload.Rows[i][1].ToString();
                String STATIONID1 = dtload.Rows[i][2].ToString();

                //Get the Station no
                SqlCommand cmd = new SqlCommand("select I_INFEED_LINE_NO,I_STN_NO_INFEED from STATION_DATA where I_STN_ID='" + STATIONID1 + "'", dc.con);
                SqlDataReader dataReader = cmd.ExecuteReader();
                if (dataReader.Read())
                {
                    STATIONID1 = dataReader.GetValue(0).ToString() + "." + dataReader.GetValue(1).ToString() + ",";
                }
                dataReader.Close();

                //Check if the Station No is Already Used for Another MO for Loading
                if (STATIONID.Contains(STATIONID1))
                {
                    //If the MO is Not Same
                    if (!(MO == MO1 && MOLINE == MOLINE1))
                    {
                        //Show Dialog Box to Change the Loading station or Stop the Loading of the MO
                        MessageBox_loading ms = new MessageBox_loading();
                        ms.lblmessage1.Text = MO1 + "-" + MOLINE1 + " has same Loading Station as this.";
                        ms.lblmessage2.Text = "Please Stop Loading MO : " + MO1 + "-" + MOLINE1 + " or Change the Loading Station for : " + MO + "-" + MOLINE;
                        ms.MO = MO;
                        ms.MOLINE = MOLINE;
                        ms.MO1 = MO1;
                        ms.MOLINE1 = MOLINE1;
                        ms.Controller = cmbcontroller.Text;
                        ms.BringToFront();
                        ms.Show();
                        //radLabel4.Text = "MO : " + MO1 + " MO LINE : " + MOLINE1 + " has same Loading Station as this .Please Stop Loading that MO or Change the Loading Station for this MO";
                        return;
                    }
                }
            }

            lblhangercount.Visible = true;
            txthangercount.Visible = true;
            btnaddhangercount.Visible = true;
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvmo.SelectedRows.Count > 0)
                {
                    //check if the controller is selected
                    if (cmbcontroller.Text == "--SELECT--" || cmbcontroller.Text == "")
                    {
                        radLabel4.Text = "Please Select the Controller";
                        return;
                    }

                    String MO = dgvmo.SelectedRows[0].Cells[0].Value.ToString();
                    String MOLINE = dgvmo.SelectedRows[0].Cells[1].Value.ToString();
                    //check if the mo is added
                    MySqlCommand cmd = new MySqlCommand("Select count(*) from prod where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
                    int count = int.Parse(cmd.ExecuteScalar() + "");
                    //if not then show message
                    if (count == 0)
                    {
                        radLabel4.Text = "Add to MO to Production to Stop Loading";
                        return;
                    }

                    //stop loading the mo
                    cmd = new MySqlCommand("update prod set IN_PROD='0' where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
                    cmd.ExecuteNonQuery();

                    radLabel4.Text = MO + "-" + MOLINE + " Stopped Loading";
                    dgvmo.SelectedRows[0].Cells[17].Value = "PROD";
                }
                else
                {
                    radLabel4.Text = "Please Select a Row";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void Add_Production_Initialized(object sender, EventArgs e)
        {
            //connect to local database
            dc.OpenConnection();

            String Lang = "";
            //get the language and theme from the setup table
            SqlCommand cmd = new SqlCommand("SELECT Language,ThemeName FROM Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Lang = sdr.GetValue(0).ToString();
                theme = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //change the language of the form 
            SqlDataAdapter sda = new SqlDataAdapter("select " + Lang + " from Language where Form='AddProduction' order by Item_No", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblmono.Text = dt.Rows[0][0].ToString() + " :";
                btnsearch.Text = dt.Rows[2][0].ToString();
                btnaddproduction.Text = dt.Rows[3][0].ToString();
                btnstartloading.Text = dt.Rows[4][0].ToString();
                btnstoploading.Text = dt.Rows[5][0].ToString();
                btnupdatestn.Text = dt.Rows[6][0].ToString();
            }

            //Get all the MO for autoComplete
            sda = new SqlDataAdapter("select V_MO_NO from MO", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txtmo.AutoCompleteCustomSource.Add(dt.Rows[i][0].ToString());
            }

            GridTheme(theme); //Change the Grid Theme
        }

        public void GridTheme(String theme)
        {
            dgvmo.ThemeName = theme; //Set the grid Theme
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dgvmo.SelectedRows.Count > 0)
                {
                    String MO = dgvmo.SelectedRows[0].Cells[0].Value.ToString() + String.Empty;
                    String MOLINE = dgvmo.SelectedRows[0].Cells[1].Value.ToString() + String.Empty;
                    //check the controller is selected
                    if (cmbcontroller.Text == "--SELECT--" || cmbcontroller.Text == "")
                    {
                        radLabel4.Text = "Please Select the Controller";
                        return;
                    }

                    //check if the mo it currently loading before confirm production
                    MySqlCommand cmd = new MySqlCommand("Select count(*) from prod where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "' and IN_PROD='1'", dc.conn);
                    int count = int.Parse(cmd.ExecuteScalar() + "");
                    //if yes the show message
                    if (count != 0)
                    {
                        radLabel4.Text = MO + "-" + MOLINE + " is Currently in Production, Stop Loading and Try Again ";
                        return;
                    }

                    //else update the mo status to COMP
                    SqlCommand cmd1 = new SqlCommand("update MO_DETAILS set V_STATUS='COMP' where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                    cmd1.ExecuteNonQuery();

                    dgvmo.Rows.RemoveAt(dgvmo.CurrentCell.RowIndex);
                    radLabel4.Text = MO + "-" + MOLINE + " Production Completed";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void radButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dgvmo.SelectedRows.Count > 0)
                {                    
                    String MO = dgvmo.SelectedRows[0].Cells[0].Value.ToString();
                    String MOLINE = dgvmo.SelectedRows[0].Cells[1].Value.ToString();
                    String articleID = dgvmo.SelectedRows[0].Cells[4].Value.ToString();

                    if (cmbcontroller.Text == "--SELECT--" || cmbcontroller.Text == "")
                    {
                        radLabel4.Text = "Please Select the Controller";
                        return;
                    }

                    ////get the article id
                    //SqlCommand cmd1 = new SqlCommand("select V_ARTICLE_ID  from ARTICLE_DB where V_ARTICLE_DESC='" + article + "'", dc.con);
                    //SqlDataReader sdr = cmd1.ExecuteReader();
                    //if (sdr.Read())
                    //{
                    //    article = sdr.GetValue(0).ToString();
                    //}
                    //sdr.Close();

                    //Check if the MO is Added to Production
                    MySqlCommand cmd = new MySqlCommand("Select count(*) from prod where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
                    int count = int.Parse(cmd.ExecuteScalar() + "");
                    if (count == 0)
                    {
                        radLabel4.Text = MO + "-" + MOLINE + " is not added to Production";
                        return;
                    }
                    
                    //delete the operations from the sequenceoperations table
                    cmd = new MySqlCommand("Delete from sequenceoperations where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
                    cmd.ExecuteNonQuery();

                    SqlCommand cmd1 = new SqlCommand("Delete from SEQUENCE_OPERATION where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                    //DebugLog("Add_Production.cs(radButton2_Click_1), Track 1, SQL - Delete from SEQUENCE_OPERATION where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'");
                    cmd1.ExecuteNonQuery();

                    //delete the stations from the sequencestations table
                    cmd = new MySqlCommand("Delete from sequencestations where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
                    cmd.ExecuteNonQuery();

                    //Update the Station Assign Version
                    cmd1 = new SqlCommand("update MO_DETAILS set V_ASSIGN_TYPE='" + cmbstationassign.Text + "' where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                    cmd1.ExecuteNonQuery();

                    //get the Sequence and Station Id of the MO
                    SqlDataAdapter sda = new SqlDataAdapter("select I_SEQUENCE_NO,I_STATION_ID from STATION_ASSIGN where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "' and I_STATION_ID!='0' and V_ASSIGN_TYPE='" + cmbstationassign.Text + "' order by I_SEQUENCE_NO", dc.con);
                    //DebugLog("Add_Production.cs(radButton2_Click_1), Track 2, SQL - select I_SEQUENCE_NO,I_STATION_ID from STATION_ASSIGN where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "' and I_STATION_ID!='0' and V_ASSIGN_TYPE='" + cmbstationassign.Text + "' order by I_SEQUENCE_NO");
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    int seq = 1;
                    int nextseq = 1;
                    int prevseq = 1;
                    int curseq = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {             
                        //re-order the sequence if there no station_assign for any of the sequence                        
                        prevseq = seq;
                        seq = int.Parse(dt.Rows[i][0].ToString());
                        if (prevseq == seq)
                        {
                            nextseq = curseq;
                        }
                        else
                        {
                            nextseq = nextseq + 1;
                        }
                        curseq = nextseq;

                        String stnid = dt.Rows[i][1].ToString();
                        //get the operations for the sequence
                        sda = new SqlDataAdapter("select O.V_ID,D.V_OPERATION_CODE from DESIGN_SEQUENCE D,OPERATION_DB O where D.V_ARTICLE_ID='" + articleID + "' and D.I_SEQUENCE_NO='" + seq + "' and O.V_OPERATION_CODE=D.V_OPERATION_CODE", dc.con);
                        //DebugLog("Add_Production.cs(radButton2_Click_1), Track 3, SQL - select O.V_ID,D.V_OPERATION_CODE from DESIGN_SEQUENCE D,OPERATION_DB O where D.V_ARTICLE_ID='" + articleID + "' and D.I_SEQUENCE_NO='" + seq + "' and O.V_OPERATION_CODE=D.V_OPERATION_CODE");
                        DataTable dt1 = new DataTable();
                        sda.Fill(dt1);
                        sda.Dispose();                        
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            String op = dt1.Rows[j][1].ToString();
                            String sam = "1";
                            String piecerate = "1";
                            String overtime_rate = "1";
                            String opcode = dt1.Rows[j][0].ToString();

                            //get the count of rows from sequenceoperations table
                            cmd1 = new SqlCommand("select D_SAM,D_PIECERATE,D_OVERTIME_RATE from OPERATION_DB where V_OPERATION_CODE='" + op + "'", dc.con);
                            SqlDataReader sdr = cmd1.ExecuteReader();
                            if (sdr.Read())
                            {
                                sam = sdr.GetValue(0).ToString();
                                piecerate = sdr.GetValue(1).ToString();
                                overtime_rate = sdr.GetValue(2).ToString();
                            }
                            sdr.Close();

                            //check if the operationID already exists for the mo
                            cmd = new MySqlCommand("Select count(*) from sequenceoperations where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "' and SEQ_NO='" + curseq + "' and OP_ID='" + opcode + "'", dc.conn);
                            int sequenceoperations1 = int.Parse(cmd.ExecuteScalar() + "");
                            if (sequenceoperations1 == 0)
                            {
                                //else insert into sequenceoperations table
                                cmd = new MySqlCommand("insert into sequenceoperations (MO_NO,MO_LINE,SEQ_NO,OP_ID,SAM,RATE,RATE_XTR) values ('" + MO + "','" + MOLINE + "','" + curseq + "','" + opcode + "','" + sam + "','" + piecerate + "','" + overtime_rate + "')", dc.conn);
                                cmd.ExecuteNonQuery();

                                //Insert into local Sequence Operation
                                string query = "insert into SEQUENCE_OPERATION  values ('" + MO + "','" + MOLINE + "','" + curseq + "','" + opcode + "','" + sam + "','" + piecerate + "','" + overtime_rate + "')";
                                //DebugLog("Add_Production.cs(radButton2_Click_1), Track 4, SQL - insert into SEQUENCE_OPERATION  values ('" + MO + "','" + MOLINE + "','" + curseq + "','" + opcode + "','" + sam + "','" + piecerate + "','" + overtime_rate + "')");
                                cmd1 = new SqlCommand(query, dc.con);
                                cmd1.ExecuteNonQuery();

                                
                            }
                        }

                        //insrt into sequencestations table
                        cmd = new MySqlCommand("insert into sequencestations (MO_NO,MO_LINE,SEQ_NO,STN_ID) values ('" + MO + "','" + MOLINE + "','" + curseq + "','" + stnid + "')", dc.conn);
                        cmd.ExecuteNonQuery();
                    }
                    radLabel4.Text = MO + "-" + MOLINE + " Station Assign is Updated";
                }
                else
                {
                    radLabel4.Text = "Please Select a Row";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }        

        private void cmbcontroller_TextChanged(object sender, EventArgs e)
        {
            btnaddproduction.Enabled = true;
            btnstartloading.Enabled = true;
            btnstoploading.Enabled = true;
            btnupdatestn.Enabled = true;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnaddhangercount_Click(object sender, EventArgs e)
        {
            try
            {
                //Get the Selected MO Details
                if (dgvmo.SelectedRows.Count > 0)
                {
                    String MO = dgvmo.SelectedRows[0].Cells[0].Value.ToString();
                    String MOLINE = dgvmo.SelectedRows[0].Cells[1].Value.ToString();
                    String color = dgvmo.SelectedRows[0].Cells[2].Value.ToString();
                    String Size = dgvmo.SelectedRows[0].Cells[3].Value.ToString();
                    String article = dgvmo.SelectedRows[0].Cells[5].Value.ToString();
                    String User1 = dgvmo.SelectedRows[0].Cells[6].Value.ToString();
                    String User2 = dgvmo.SelectedRows[0].Cells[7].Value.ToString();
                    String User3 = dgvmo.SelectedRows[0].Cells[8].Value.ToString();
                    String User4 = dgvmo.SelectedRows[0].Cells[9].Value.ToString();
                    String User5 = dgvmo.SelectedRows[0].Cells[10].Value.ToString();
                    String User6 = dgvmo.SelectedRows[0].Cells[11].Value.ToString();
                    String User7 = dgvmo.SelectedRows[0].Cells[12].Value.ToString();
                    String User8 = dgvmo.SelectedRows[0].Cells[13].Value.ToString();
                    String User9 = dgvmo.SelectedRows[0].Cells[14].Value.ToString();
                    String User10 = dgvmo.SelectedRows[0].Cells[15].Value.ToString();
                    String Qty = dgvmo.SelectedRows[0].Cells[16].Value.ToString();                                       

                    //Check if the piecePerHanger is Integer
                    String piece_per_hanger = txthangercount.Text;
                    Regex r = new Regex("^[0-9]*$");
                    if (!(r.IsMatch(piece_per_hanger)) || piece_per_hanger == "0")
                    {
                        radLabel4.Text = "Invalid Piece Count";
                        return;
                    }
                    else
                    {
                        lblhangercount.Visible = false;
                        txthangercount.Visible = false;
                        btnaddhangercount.Visible = false;
                    }

                    //Update the Hanger Count of MO Details
                    SqlCommand cmd1 = new SqlCommand("update MO_DETAILS set I_HANGER_COUNT='" + piece_per_hanger + "',V_ASSIGN_TYPE='" + cmbstationassign.Text + "',V_STATUS='PROD' where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                    cmd1.ExecuteNonQuery();

                    //Check if the MO is Already in the Controller
                    MySqlCommand cmd = new MySqlCommand("Select count(*) from prod where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
                    int count = int.Parse(cmd.ExecuteScalar() + "");
                    if (count != 0)
                    {
                        //if its in the prod table update IN_PROD=1
                        cmd = new MySqlCommand("update prod set IN_PROD='1',PC_COUNT='" + piece_per_hanger + "' where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = MO + "-" + MOLINE + " Started Loading";
                        dgvmo.SelectedRows[0].Cells[17].Value = "LOADING";
                        return;
                    }

                    //insert into prod table with IN_PROD=1
                    cmd = new MySqlCommand("insert into prod (MO_NO,MO_LINE,IN_PROD,CUR_COUNT,PC_COUNT,MAX_COUNT) values('" + MO + "','" + MOLINE + "','1','0','" + piece_per_hanger + "','" + Qty + "')", dc.conn);
                    cmd.ExecuteNonQuery();

                    //insert into modetails table
                    cmd = new MySqlCommand("insert into modetails (MO_NO,MO_LINE,COLOR,ARTICLE,SIZE,USER1,USER2,USER3,USER4,USER5,USER6,USER7,USER8,USER9,USER10,QUANTITY) values('" + MO + "','" + MOLINE + "','" + color + "','" + article + "','" + Size + "','" + User1 + "','" + User2 + "','" + User3 + "','" + User4 + "','" + User5 + "','" + User6 + "','" + User7 + "','" + User8 + "','" + User9 + "','" + User10 + "','" + Qty + "')", dc.conn);
                    cmd.ExecuteNonQuery();

                    //Perform Update station Assign
                    btnupdatestn.PerformClick();

                    radLabel4.Text = MO + "-" + MOLINE + " Started Loading";
                    dgvmo.SelectedRows[0].Cells[17].Value = "LOADING";
                }
                else
                {
                    radLabel4.Text = "Please Select a Row";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void txthangercount_KeyDown(object sender, KeyEventArgs e)
        {
            //Enter Key Press for PeicesPerHanger
            if (e.KeyCode == Keys.Enter)
            {
                btnaddhangercount.PerformClick();
            }
        }
               
        private void Add_Production_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Close the connection on Form Close
            dc.Close_Connection();
        }        

        

        private void radButton2_Click_3(object sender, EventArgs e)
        {
            //Report for Add to production
            if (btnreport.Text == "Report View")
            {
                reportViewer3.Visible = true;

                //Get the LOGO 
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                DataView view = new DataView(data1);

                reportViewer3.LocalReport.ReportEmbeddedResource = "SMARTMRT.Add_prod.rdlc";
                reportViewer3.LocalReport.DataSources.Clear();
                
                //Add dataset for the report
                reportViewer3.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer3.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer3.RefreshReport();
                btnreport.Text = "Table View";
            }
            else
            {
                reportViewer3.Visible = false;
                btnreport.Text = "Report View";
            }
        }

        private void radButton2_Click_4(object sender, EventArgs e)
        {
            //show all MO
            for (int i = 0; i < dgvmo.Rows.Count; i++)
            {
                dgvmo.Rows[i].IsVisible = true;
            }

        }

        private void dgvmo_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            lblhangercount.Visible = false;
            txthangercount.Visible = false;
            btnaddhangercount.Visible = false;
            cmbstationassign.Items.Clear();

            //Get all the Versions which has station assign for the MO
            SqlDataAdapter sda = new SqlDataAdapter("select distinct V_ASSIGN_TYPE from STATION_ASSIGN where V_MO_NO='" + dgvmo.Rows[e.RowIndex].Cells[0].Value.ToString() + "' and V_MO_LINE='" + dgvmo.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbstationassign.Items.Add(dt.Rows[i][0].ToString());
            }

            //Get the Last Updated Version for that MO
            sda = new SqlDataAdapter("select V_ASSIGN_TYPE from MO_DETAILS where V_MO_NO='" + dgvmo.Rows[e.RowIndex].Cells[0].Value.ToString() + "' and V_MO_LINE='" + dgvmo.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbstationassign.Text = dt.Rows[i][0].ToString();
            }

            //Get the Loading station for that Version of Station Assign for that MO
            String stationid = "";
            sda = new SqlDataAdapter("select I_LINE_NO,D_STATION_NO,I_SEQUENCE_NO from STATION_ASSIGN where V_MO_NO='" + dgvmo.Rows[e.RowIndex].Cells[0].Value.ToString() + "' and V_MO_LINE='" + dgvmo.Rows[e.RowIndex].Cells[1].Value.ToString() + "' and V_ASSIGN_TYPE='" + cmbstationassign.Text + "' and I_STATION_ID!='0' and I_SEQUENCE_NO='1'", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                stationid += dt.Rows[i][0].ToString() + "." + dt.Rows[i][1].ToString() + ",";
            }

            if (stationid.Length > 0)
            {
                stationid = stationid.Remove(stationid.Length - 1, 1);
                dgvmo.Rows[e.RowIndex].Cells[17].Value = stationid;
            }
        }

        private void MasterTemplate_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            //change the Row color if the mo is Loading or Added to Production or Not added to Production
            if (e.RowElement.RowInfo.Cells[17].Value.ToString() == "LOADING")
            {
                e.RowElement.DrawFill = true;
                e.RowElement.GradientStyle = GradientStyles.Solid;
                e.RowElement.BackColor = Color.ForestGreen;
            }
            else if (e.RowElement.RowInfo.Cells[17].Value.ToString() == "PROD")
            {
                e.RowElement.DrawFill = true;
                e.RowElement.GradientStyle = GradientStyles.Solid;
                e.RowElement.BackColor = ColorTranslator.FromHtml("#e5d07f");
            }
            else
            {
                e.RowElement.DrawFill = true;
                e.RowElement.GradientStyle = GradientStyles.Solid;
                e.RowElement.BackColor = ColorTranslator.FromHtml("#e35961");
            }
        }

        private void dgvmo_SelectionChanged(object sender, EventArgs e)
        {
            lblhangercount.Visible = false;
            txthangercount.Visible = false;
            btnaddhangercount.Visible = false;
        }

        private void dgvmo_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change the grid fore color for these Themes
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {                
                e.CellElement.ForeColor = Color.Black;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
                if (e.CellElement is GridHeaderCellElement || e.CellElement is GridGroupContentCellElement)
                {
                    e.CellElement.ForeColor = Color.White;
                }
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
                if (e.CellElement is GridHeaderCellElement || e.CellElement is GridGroupContentCellElement)
                {
                    e.CellElement.ForeColor = Color.Black;
                }
            }            
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
