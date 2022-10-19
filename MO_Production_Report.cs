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

namespace SMARTMRT
{
    public partial class MO_Production_Report : Telerik.WinControls.UI.RadForm
    {
        public MO_Production_Report()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();  //connection class
        DataTable MO = new DataTable();
        DataTable data1 = new DataTable();

        String controller_name = "";
        String USER1 = "";
        String USER2 = "";
        String USER3 = "";
        String USER4 = "";
        String USER5 = "";
        String USER6 = "";
        String USER7 = "";
        String USER8 = "";
        String USER9 = "";
        String USER10 = "";

        private void MO_Production_Report_Load(object sender, EventArgs e)
        {
            dgvmoline.MasterTemplate.SelectLastAddedRow = false;
            dgvmoline.MasterView.TableSearchRow.ShowCloseButton = false;    //disable close button on search in grid

            data1.Columns.Add("COLOR");
            data1.Columns.Add("SIZE");
            data1.Columns.Add("ARTICLE");
            data1.Columns.Add("QUANTITY");
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
            data1.Columns.Add("TOTAL_UNLOADED");
            data1.Columns.Add("BALANCE_QTY");

            data1.Columns.Add("mono");
            data1.Columns.Add("p_order");
            data1.Columns.Add("s_order");
            data1.Columns.Add("customer");
            data1.Columns.Add("dest");
            data1.Columns.Add("mode");
            data1.Columns.Add("date");
            data1.Columns.Add("moline");
            data1.Columns.Add("user1");
            data1.Columns.Add("user2");
            data1.Columns.Add("user3");
            data1.Columns.Add("user4");
            data1.Columns.Add("user5");
            data1.Columns.Add("user6");
            data1.Columns.Add("user7");
            data1.Columns.Add("user8");
            data1.Columns.Add("user9");
            data1.Columns.Add("user10");
            data1.Columns.Add("loaded");

            //get special field name
            SqlCommand cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF1' and V_ENABLED='TRUE'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                USER1 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                USER2 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                USER3 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                USER4 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                USER5 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF6' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                USER6 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF7' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                USER7 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF8' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                USER8 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF9' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                USER9 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF10' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                USER10 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            MO.Columns.Add("Color");
            MO.Columns.Add("Size");
            MO.Columns.Add("Article");
            MO.Columns.Add("Quantity");
            MO.Columns.Add(USER1);
            MO.Columns.Add(USER2);
            MO.Columns.Add(USER3);
            MO.Columns.Add(USER4);
            MO.Columns.Add(USER5);
            MO.Columns.Add(USER6);
            MO.Columns.Add(USER7);
            MO.Columns.Add(USER8);
            MO.Columns.Add(USER9);
            MO.Columns.Add(USER10);
            MO.Columns.Add("Total Loaded");
            MO.Columns.Add("Total Produced");
            MO.Columns.Add("Balance Quantity");

            dgvmoline.DataSource = MO;
            //hide the columns which are not enabled
            if (USER1 == "")
            {
                dgvmoline.Columns[4].IsVisible = false;
            }

            if (USER2 == "")
            {
                dgvmoline.Columns[5].IsVisible = false;
            }

            if (USER3 == "")
            {
                dgvmoline.Columns[6].IsVisible = false;
            }

            if (USER4 == "")
            {
                dgvmoline.Columns[7].IsVisible = false;
            }

            if (USER5 == "")
            {
                dgvmoline.Columns[8].IsVisible = false;
            }

            if (USER6 == "")
            {
                dgvmoline.Columns[9].IsVisible = false;
            }

            if (USER7 == "")
            {
                dgvmoline.Columns[10].IsVisible = false;
            }

            if (USER8 == "")
            {
                dgvmoline.Columns[11].IsVisible = false;
            }

            if (USER9 == "")
            {
                dgvmoline.Columns[12].IsVisible = false;
            }

            if (USER10 == "")
            {
                dgvmoline.Columns[13].IsVisible = false;
            }
            //get the last loaded mo
            select_controller();

            //cmd = new SqlCommand("select V_MO_NO from LAST_SELECT_MO", dc.con);
            //sdr = cmd.ExecuteReader();
            //if (sdr.Read())
            //{
            //    txtmo.Text = sdr.GetValue(0).ToString();
            //    sdr.Close();
            //    btnsearch.PerformClick();
            //}
            //sdr.Close();

            //get the last loaded mo
            string lastSelMO = "";
            cmd = new SqlCommand("select V_MO_NO from LAST_SELECT_MO", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                //txtmo.Text = sdr.GetValue(0).ToString();
                lastSelMO = sdr.GetValue(0).ToString();
                sdr.Close();
                //btnsearch.PerformClick();
            }
            sdr.Close();

            SqlDataAdapter daMO = new SqlDataAdapter("select V_MO_NO from MO", dc.con);
            DataTable dtMO = new DataTable();
            daMO.Fill(dtMO);
            daMO.Dispose();
            for (int j = 0; j < dtMO.Rows.Count; j++)
            {
                cmbMO.Items.Add(dtMO.Rows[j][0].ToString());
            }

            if (lastSelMO != "")
            {
                cmbMO.SelectedText = lastSelMO;
            }
            btnsearch.PerformClick();
        }

        String theme = "";
        private void MO_Production_Report_Initialized(object sender, EventArgs e)
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

            ////get all the mo
            //SqlDataAdapter sda = new SqlDataAdapter("select V_MO_NO from MO", dc.con);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //sda.Dispose();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    txtmo.AutoCompleteCustomSource.Add(dt.Rows[i][0].ToString());
            //}

            //change grid theme
            GridTheme(theme);
        }

        //set grid theme
        public void GridTheme(String theme)
        {
            dgvmoline.ThemeName = theme;
        }


        private void radLabel15_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            panel2.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel15.Text = "";
            panel2.Visible = false;
            MyTimer.Stop();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            //check if controller is selected
            if (controller_name == "--SELECT--" || controller_name == "")
            {
                radLabel15.Text = "Please Select a Controller";
                return;
            }

            try
            {
                string strMO = "";
                strMO = cmbMO.Text.TrimStart().TrimEnd();
                data1.Rows.Clear();
                reportViewer1.Visible = false;
                btnreport.Text = "Report View";
                MO.Rows.Clear();

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

                //get the modetails for the mo
                //SqlDataAdapter sda = new SqlDataAdapter("SELECT V_MO_NO,V_COLOR_ID,V_SIZE_ID,V_ARTICLE_ID,I_ORDER_QTY,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10,V_MO_LINE,D_SHIPMENT_DATE,V_SHIPPING_DEST,V_SHIPPING_MODE,V_PURCHASE_ORDER,V_SALES_ORDER FROM MO_DETAILS where V_MO_NO='" + txtmo.Text + "'", dc.con);
                SqlDataAdapter sda = new SqlDataAdapter("SELECT V_MO_NO,V_COLOR_ID,V_SIZE_ID,V_ARTICLE_ID,I_ORDER_QTY,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10,V_MO_LINE,D_SHIPMENT_DATE,V_SHIPPING_DEST,V_SHIPPING_MODE,V_PURCHASE_ORDER,V_SALES_ORDER FROM MO_DETAILS where V_MO_NO='" + strMO + "'", dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String color = dt.Rows[i][1].ToString();
                    String size = dt.Rows[i][2].ToString();
                    String article = dt.Rows[i][3].ToString();
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
                    txtshippingdate.Text = dt.Rows[i][16].ToString();
                    txtshipmentdest.Text = dt.Rows[i][17].ToString();
                    txtshippingmode.Text = dt.Rows[i][18].ToString();
                    txtpurchaseorder.Text = dt.Rows[i][19].ToString();
                    txtsalesorder.Text = dt.Rows[i][20].ToString();
                    
                    //get desc for master
                    SqlCommand cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        color = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + article + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        article = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        size = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + user1 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user1 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + user2 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user2 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + user3 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user3 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + user4 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user4 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + user5 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user5 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + user6 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user6 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + user7 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user7 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + user8 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user8 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + user9 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user9 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    cmd = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + user10 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user10 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get desc for master
                    sda = new SqlDataAdapter("Select C.V_CUSTOMER_NAME from CUSTOMER_DB C,MO M where M.V_MO_NO='" + strMO + "' and  C.V_CUSTOMER_ID=M.V_CUSTOMER_ID", dc.con);
                    DataTable dt1 = new DataTable();
                    sda.Fill(dt1);
                    sda.Dispose();
                    for (int ii = 0; ii < dt1.Rows.Count; ii++)
                    {
                        txtcustomer.Text = dt1.Rows[ii][0].ToString();
                    }

                    //get total unloaded for the mo
                    int produced = 0;
                    MySqlCommand cmd2 = new MySqlCommand("select SUM(PC_COUNT) from stationhistory where MO_NO='" + strMO + "' and MO_LINE='" + moline + "' and REMARKS='2'", dc.conn);
                    String temp = cmd2.ExecuteScalar() + "";
                    if (temp != "")
                    {
                        produced = int.Parse(temp);
                    }

                    //get total loaded for the mo
                    int loaded = 0;
                    cmd2 = new MySqlCommand("select SUM(PC_COUNT) from stationhistory where MO_NO='" + strMO + "' and MO_LINE='" + moline + "' and REMARKS='1'", dc.conn);
                    temp = cmd2.ExecuteScalar() + "";
                    if (temp != "")
                    {
                        loaded = int.Parse(temp);
                    }

                    int balance = 0;

                    //calculate balance quantity
                    balance = int.Parse(qty) - produced;

                    if (balance < 0)
                    {
                        balance = 0;
                    }

                    //add to grid
                    MO.Rows.Add(color, size, article, qty, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, loaded, produced, balance);
                    data1.Rows.Add(color, size, article, qty, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, produced, balance, strMO, txtpurchaseorder.Text, txtsalesorder.Text, txtcustomer.Text, txtshipmentdest.Text, txtshippingmode.Text, txtshippingdate.Text, moline, USER1, USER2, USER3, USER4, USER5, USER6, USER7, USER8, USER9, USER10, loaded);
                }
                dgvmoline.DataSource = MO;
                String mo = "";

                //get the last searched mo
                SqlCommand cmd1 = new SqlCommand("select V_MO_NO from LAST_SELECT_MO where V_ID=(select MAX(V_ID) from LAST_SELECT_MO)", dc.con);
                SqlDataReader sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    mo = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();

                //if its not same then insert to last loaded mo
                if (mo != strMO && MO.Rows.Count > 0)
                {
                    cmd1 = new SqlCommand("update LAST_SELECT_MO set V_MO_NO='" + strMO + "'", dc.con);
                    cmd1.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                radLabel15.Text = ex.Message;
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
            dc.Close_Connection();   //close connection if open
            dc.OpenMYSQLConnection(ipaddress);   //open connection
        }       

        private void MO_Production_Report_FormClosed(object sender, FormClosedEventArgs e)
        {
            dc.Close_Connection();   //close connection on form close
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //clech if report button is clicked
            if (btnreport.Text == "Report View")
            {
                reportViewer1.Visible = true;
                DataView view = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.MO_PROD.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                //add views to dataset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer1.RefreshReport();
                btnreport.Text = "Table View";
            }
            else
            {
                btnreport.Text = "Report View";
                reportViewer1.Visible = false;
            }
        }

        private void dgvmoline_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvmoline.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvmoline.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvmoline.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvmoline.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void cmbMO_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            btnsearch.PerformClick();
        }
    }
}
