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
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Station_Status : Telerik.WinControls.UI.RadForm
    {
        public Station_Status()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();    //connection class
        DataTable STN = new DataTable();
        String controller_name = "";
        DataTable data1 = new DataTable();
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

        private void Station_Status_Load(object sender, EventArgs e)
        {
            dgvstationreport.MasterTemplate.SelectLastAddedRow = false;
            dgvstationreport.MasterView.TableSearchRow.ShowCloseButton = false;    //disable close button of search in grid
            data1.Columns.Add("Station_No");
            data1.Columns.Add("Sequence_No");
            data1.Columns.Add("Employee_ID");
            data1.Columns.Add("Employee_Name");
            data1.Columns.Add("MONO");
            data1.Columns.Add("Color");
            data1.Columns.Add("Article_ID");
            data1.Columns.Add("Size");
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
            data1.Columns.Add("Peice_Count");
            data1.Columns.Add("u1");
            data1.Columns.Add("u2");
            data1.Columns.Add("u3");
            data1.Columns.Add("u4");
            data1.Columns.Add("u5");
            data1.Columns.Add("u6");
            data1.Columns.Add("u7");
            data1.Columns.Add("u8");
            data1.Columns.Add("u9");
            data1.Columns.Add("u10");

            dc.OpenConnection();   //open connection

            //get special field name
            SqlCommand cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF1' and V_ENABLED='TRUE'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user1 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user2 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user3 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user4 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user5 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF6' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user6 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF7' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user7 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF8' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user8 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF9' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user9 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF10' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user10 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            STN.Columns.Add("Station No");
            STN.Columns.Add("Sequence No");
            STN.Columns.Add("Employee ID");
            STN.Columns.Add("Employee Name");
            STN.Columns.Add("MO No");
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
            STN.Columns.Add("Piece Count");

            //hide columns which are disabled
            dgvstationreport.DataSource = STN;
            if (user1 == "")
            {
                dgvstationreport.Columns[8].IsVisible = false;
            }

            if (user2 == "")
            {
                dgvstationreport.Columns[9].IsVisible = false;
            }

            if (user3 == "")
            {
                dgvstationreport.Columns[10].IsVisible = false;
            }

            if (user4 == "")
            {
                dgvstationreport.Columns[11].IsVisible = false;
            }

            if (user5 == "")
            {
                dgvstationreport.Columns[12].IsVisible = false;
            }

            if (user6 == "")
            {
                dgvstationreport.Columns[13].IsVisible = false;
            }

            if (user7 == "")
            {
                dgvstationreport.Columns[14].IsVisible = false;
            }

            if (user8 == "")
            {
                dgvstationreport.Columns[15].IsVisible = false;
            }

            if (user9 == "")
            {
                dgvstationreport.Columns[16].IsVisible = false;
            }

            if (user10 == "")
            {
                dgvstationreport.Columns[17].IsVisible = false;
            }

            select_controller();   //get the selected controller
            Station_inspection();   //get station details

            this.dgvstationreport.AutoSizeColumnsMode = GridViewAutoSizeColumnsMode.Fill;
        }

        public void Station_inspection()
        {
            try
            {
                //check if controller is selected
                if (controller_name == "--SELECT--" || controller_name == "")
                {
                    radLabel8.Text = "Please Select a Controller";
                    return;
                }
                STN.Rows.Clear();
                data1.Rows.Clear();

                //get station no
                SqlDataAdapter sda1 = new SqlDataAdapter("Select I_STN_ID,I_INFEED_LINE_NO,I_STN_NO_INFEED from STATION_DATA", dc.con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                sda1.Dispose();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    String stnid = dt1.Rows[i][0].ToString();
                    String lineno = dt1.Rows[i][1].ToString();
                    String stationno = dt1.Rows[i][2].ToString();
                    String MO = "";
                    String MOLINE = "";
                    String SEQNO = "";
                    String EMPID = "";
                    String empname = "";
                    String color = "";
                    String article = "";
                    String size = "";

                    String u1 = "";
                    String u2 = "";
                    String u3 = "";
                    String u4 = "";
                    String u5 = "";
                    String u6 = "";
                    String u7 = "";
                    String u8 = "";
                    String u9 = "";
                    String u10 = "";

                    //get mo details
                    int piece_count = 0;
                    MySqlDataAdapter sda2 = new MySqlDataAdapter("SELECT MO_NO,MO_LINE,SEQ_NO,EMP_ID FROM hangerwip WHERE stn_id='" + stnid + "' ORDER BY TIME", dc.conn);
                    DataTable dt2 = new DataTable();
                    sda2.Fill(dt2);
                    sda2.Dispose();
                    if (dt2.Rows.Count > 0)
                    {
                        int j = dt2.Rows.Count - 1;
                        MO = dt2.Rows[j][0].ToString();
                        MOLINE = dt2.Rows[j][1].ToString();
                        SEQNO = dt2.Rows[j][2].ToString();
                        EMPID = dt2.Rows[j][3].ToString();

                        //gte mo details
                        SqlDataAdapter sda = new SqlDataAdapter("Select V_COLOR_ID,V_ARTICLE_ID,V_SIZE_ID,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10 from MO_DETAILS where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                        DataTable dt3 = new DataTable();
                        sda.Fill(dt3);
                        for (j = 0; j < dt3.Rows.Count; j++)
                        {
                            color = dt3.Rows[j][0].ToString();
                            article = dt3.Rows[j][1].ToString();
                            size = dt3.Rows[j][2].ToString();
                            u1 = dt3.Rows[j][3].ToString();
                            u2 = dt3.Rows[j][4].ToString();
                            u3 = dt3.Rows[j][5].ToString();
                            u4 = dt3.Rows[j][6].ToString();
                            u5 = dt3.Rows[j][7].ToString();
                            u6 = dt3.Rows[j][8].ToString();
                            u7 = dt3.Rows[j][9].ToString();
                            u8 = dt3.Rows[j][10].ToString();
                            u9 = dt3.Rows[j][11].ToString();
                            u10 = dt3.Rows[j][12].ToString();
                        }
                        sda.Dispose();

                        //get desc
                        SqlCommand cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            color = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + article + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            article = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        cmd = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            size = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        cmd = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + u1 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            u1 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        cmd = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + u2 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            u2 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        cmd = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + u3 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            u3 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        cmd = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + u4 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            u4 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        cmd = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + u5 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            u5 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        cmd = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + u6 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            u6 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        cmd = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + u7 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            u7 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        cmd = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + u8 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            u8 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        cmd = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + u9 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            u9 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        cmd = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + u10 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            u10 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get emp name
                        cmd = new SqlCommand("select V_FIRST_NAME from EMPLOYEE where V_EMP_ID='" + EMPID + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            empname = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get production details
                        MySqlCommand cmd1 = new MySqlCommand("select SUM(PC_COUNT) from stationhistory where stn_id='" + stnid + "' and time>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
                        String temp = cmd1.ExecuteScalar() + "";
                        if (temp != "")
                        {
                            piece_count = int.Parse(temp);
                        }
                    }

                    if (lineno != "0")
                    {
                        STN.Rows.Add(lineno + "." + stationno, SEQNO, EMPID, empname, MO, color, article, size, u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, piece_count);
                        data1.Rows.Add(lineno + "." + stationno, SEQNO, EMPID, empname, MO, color, article, size, u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, piece_count, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10);
                    }
                }
                DataView dv = STN.DefaultView;
                dv.Sort = "Station No ASC";
                dgvstationreport.DataSource = dv;
            }
            catch (Exception ex)
            {
                radLabel8.Text = ex.Message;
            }
        }

        String theme = "";

        private void Station_Status_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();   //open connection


            //get language and theme
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
            dgvstationreport.ThemeName = theme;
        }

        private void radLabel8_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            radPanel2.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel8.Text = "";
            radPanel2.Visible = false;
            MyTimer.Stop();
        }

        private void Station_Status_FormClosed(object sender, FormClosedEventArgs e)
        {
            dc.Close_Connection();
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

            dc.Close_Connection();   //close connection
            dc.OpenMYSQLConnection(ipaddress);    //open connection
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            if(btnreport.Text=="Report View")
            {
                DataView view = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer2.LocalReport.ReportEmbeddedResource = "SMARTMRT.Station.rdlc";
                reportViewer2.LocalReport.DataSources.Clear();

                reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer2.RefreshReport();

                btnreport.Text = "Table View";
                reportViewer2.Visible = true;
            }
            else
            {
                btnreport.Text = "Report View";
                reportViewer2.Visible = false;
            }
        }

        private void dgvstationreport_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvstationreport.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvstationreport.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvstationreport.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvstationreport.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }
    }
}
