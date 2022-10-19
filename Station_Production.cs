using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using System;
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
    public partial class Station_Production : Telerik.WinControls.UI.RadForm
    {
        public Station_Production()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();   //connection class
        String controller_name = "";
        DataTable data1 = new DataTable();
        DataTable emp = new DataTable();

        //special fields
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
        private void Station_Production_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dgvmo.MasterTemplate.SelectLastAddedRow = false;
            dgvoperations.MasterTemplate.SelectLastAddedRow = false;
            //disable close button of search in grid
            dgvmo.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvoperations.MasterView.TableSearchRow.ShowCloseButton = false;

            emp.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            emp.Columns.Add("MO No");
            emp.Columns.Add("MO Details");
            emp.Columns.Add("Color");
            emp.Columns.Add("Article");
            emp.Columns.Add("Size");

            dgvmo.DataSource = emp;
            dgvmo.Columns[0].IsVisible = false;
            reportViewer1.Visible = false;

            data1.Columns.Add("Station_No");
            data1.Columns.Add("Sequence_no");
            data1.Columns.Add("Employee_No");
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
            data1.Columns.Add("Login_time");
            data1.Columns.Add("Mo_line");
            data1.Columns.Add("Opcode");
            data1.Columns.Add("Opdesc");
            data1.Columns.Add("Peice_Count");
            data1.Columns.Add("qty");
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

            select_controller();    //get the selected controller

            radPanel2.Visible = false;

            //get special field name
            SqlCommand cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF1' and V_ENABLED='TRUE'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser1.Text = sdr.GetValue(0).ToString() + " :";
                USER1 = sdr.GetValue(0).ToString() + " :";
                lbluser1.Visible = true;
                txtuser1.Visible = true;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser2.Text = sdr.GetValue(0).ToString() + " :";
                USER2 = sdr.GetValue(0).ToString() + " :";
                lbluser2.Visible = true;
                txtuser2.Visible = true;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser3.Text = sdr.GetValue(0).ToString() + " :";
                USER3 = sdr.GetValue(0).ToString() + " :";
                lbluser3.Visible = true;
                txtuser3.Visible = true;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser4.Text = sdr.GetValue(0).ToString() + " :";
                USER4 = sdr.GetValue(0).ToString() + " :";
                lbluser4.Visible = true;
                txtuser4.Visible = true;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser5.Text = sdr.GetValue(0).ToString() + " :";
                USER5 = sdr.GetValue(0).ToString() + " :";
                lbluser5.Visible = true;
                txtuser5.Visible = true;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF6' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser6.Text = sdr.GetValue(0).ToString() + " :";
                USER6 = sdr.GetValue(0).ToString() + " :";
                lbluser6.Visible = true;
                txtuser6.Visible = true;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF7' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser7.Text = sdr.GetValue(0).ToString() + " :";
                USER7 = sdr.GetValue(0).ToString() + " :";
                lbluser7.Visible = true;
                txtuser7.Visible = true;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF8' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser8.Text = sdr.GetValue(0).ToString() + " :";
                USER8 = sdr.GetValue(0).ToString() + " :";
                lbluser8.Visible = true;
                txtuser8.Visible = true;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF9' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser9.Text = sdr.GetValue(0).ToString() + " :";
                USER9 = sdr.GetValue(0).ToString() + " :";
                lbluser9.Visible = true;
                txtuser9.Visible = true;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF10' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser10.Text = sdr.GetValue(0).ToString() + " :";
                USER10 = sdr.GetValue(0).ToString() + " :";
                lbluser10.Visible = true;
                txtuser10.Visible = true;
            }
            sdr.Close();

            //get mo details
            SqlDataAdapter sda = new SqlDataAdapter("SELECT DISTINCT MO.V_MO_NO,MO.V_COLOR_ID,MO.V_SIZE_ID,MO.V_ARTICLE_ID,MO.V_MO_LINE FROM MO_DETAILS MO ,STATION_ASSIGN SA where MO.V_MO_NO=SA.V_MO_NO and MO.V_MO_LINE=SA.V_MO_LINE and MO.V_STATUS!='COMP' order by mo.V_MO_NO,mo.V_MO_LINE", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String mo = dt.Rows[i][0].ToString();
                String color = dt.Rows[i][1].ToString();
                String size = dt.Rows[i][2].ToString();
                String article = dt.Rows[i][3].ToString();
                String moline = dt.Rows[i][4].ToString();

                //get desc
                cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                sdr = cmd.ExecuteReader();
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

                emp.Rows.Add(false, mo, moline, color, article, size);
            }
            dgvmo.DataSource = emp;
        }
        

        public void RowSelected(String mo, String moline)
        {
            try
            {
                data1.Rows.Clear();
                dgvoperations.Rows.Clear();
                txtmo1.Text = mo;
                txtcolor.Text = "";
                txtarticle.Text = "";
                txtsize.Text = "";
                txtqty.Text = "";
                txtuser1.Text = "";
                txtuser2.Text = "";
                txtuser3.Text = "";
                txtuser4.Text = "";
                txtuser5.Text = "";
                txtuser6.Text = "";
                txtuser7.Text = "";
                txtuser8.Text = "";
                txtuser9.Text = "";
                txtuser10.Text = "";

                //get mo details
                SqlDataAdapter sda = new SqlDataAdapter("select V_MO_NO,V_COLOR_ID,I_ORDER_QTY,V_ARTICLE_ID,V_SIZE_ID,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10 from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
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
                String color = "";
                String qty = "";
                String article = "";
                String size = "";
                String article1 = "";

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    color = dt.Rows[i][1].ToString();
                    qty = dt.Rows[i][2].ToString();
                    article = dt.Rows[i][3].ToString();
                    size = dt.Rows[i][4].ToString();
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
                    article1 = article;

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
                }

                txtmo1.Text = mo;
                txtcolor.Text = color;
                txtarticle.Text = article;
                txtsize.Text = size;
                txtqty.Text = qty;
                txtuser1.Text = user1;
                txtuser2.Text = user2;
                txtuser3.Text = user3;
                txtuser4.Text = user4;
                txtuser5.Text = user5;
                txtuser6.Text = user6;
                txtuser7.Text = user7;
                txtuser8.Text = user8;
                txtuser9.Text = user9;
                txtuser10.Text = user10;                               

                //get all sequence
                sda = new SqlDataAdapter("select I_SEQUENCE_NO,I_LINE_NO,D_STATION_NO,I_STATION_ID from STATION_ASSIGN where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and I_STATION_ID!='0' and I_SEQUENCE_NO IN(select distinct S.I_SEQUENCE_NO from STATION_ASSIGN S,MO_DETAILS M where S.V_MO_NO='" + mo + "' and S.V_MO_LINE='" + moline + "' and S.I_STATION_ID!=0 and M.V_ASSIGN_TYPE=S.V_ASSIGN_TYPE) and V_ASSIGN_TYPE=(select V_ASSIGN_TYPE from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "') order by I_SEQUENCE_NO", dc.con);
                dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String seqno = dt.Rows[i][0].ToString();
                    String line = dt.Rows[i][1].ToString();
                    String station_no = dt.Rows[i][2].ToString();
                    String station_id = dt.Rows[i][3].ToString();
                    String opcode = "";
                    String opdesc = "";
                    String empid = "";
                    String empname = "";
                    String logintime = "";

                    //get operation code
                    SqlCommand cmd = new SqlCommand("select op.V_OPERATION_CODE,op.V_OPERATION_DESC from DESIGN_SEQUENCE ds,OPERATION_DB op where ds.V_ARTICLE_ID='" + article1 + "' and ds.V_OPERATION_CODE=op.V_OPERATION_CODE and ds.I_SEQUENCE_NO='" + seqno + "'", dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        opcode = sdr.GetValue(0).ToString();
                        opdesc = sdr.GetValue(1).ToString();
                    }
                    sdr.Close();

                    //get employee login time
                    MySqlDataAdapter sda1 = new MySqlDataAdapter("SELECT EMP_ID,ACTION_ID,TIME,STN_ID,LINE_NO FROM (SELECT  EMP_ID,ACTION_ID,TIME,STN_ID,LINE_NO,row_number() over (partition BY STN_ID order by TIME DESC) row_num FROM mrt_local.employeeactions WHERE ACTION_ID = 2 OR ACTION_ID = 1) a WHERE a.row_num = 1 AND  ACTION_ID = 1 AND LINE_NO = '" + line + "' and STN_ID='" + station_id + "' ORDER BY TIME ASC", dc.conn);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    sda1.Dispose();
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        empid = dt1.Rows[j][0].ToString();
                        logintime = dt1.Rows[j][2].ToString();
                    }

                    //get employee name
                    cmd = new SqlCommand("select V_FIRST_NAME from EMPLOYEE where V_EMP_ID='" + empid + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        empname = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    int piece_count = 0;

                    //get production details for the employee
                    sda1 = new MySqlDataAdapter("select DATE(TIME),SUM(PC_COUNT) from stationhistory where EMP_ID='" + empid + "' and STN_ID='" + station_id + "' and SEQ_NO='" + seqno + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' GROUP BY DATE(TIME) ORDER BY DATE(TIME)", dc.conn);
                    dt1 = new DataTable();
                    sda1.Fill(dt1);
                    sda1.Dispose();
                    for (int k = 0; k < dt1.Rows.Count; k++)
                    {
                        DateTime hideday = Convert.ToDateTime(dt1.Rows[k][0].ToString());
                        cmd = new SqlCommand("select count(*) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE' and CONVERT(nvarchar(10),D_HIDEDAY, 120)='" + hideday.ToString("yyyy-MM-dd") + "'", dc.con);
                        int count = int.Parse(cmd.ExecuteScalar().ToString());
                        if (count > 0)
                        {
                            continue;
                        }

                        String temp = dt1.Rows[k][1].ToString();
                        if (temp != "")
                        {
                            piece_count = piece_count + int.Parse(dt1.Rows[k][1].ToString());
                        }                       
                    }

                    dgvoperations.Rows.Add(seqno, opcode, opdesc, empid, empname, line + "." + station_no, piece_count, logintime);
                    data1.Rows.Add(line + "." + station_no, seqno, empid, empname, mo, color, article, size, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, logintime, moline, opcode, opdesc, piece_count, qty, USER1, USER2, USER3, USER4, USER5, USER6, USER7, USER8, USER9, USER10);
                }

                reportViewer1.Visible = false;
                btnreport.Text = "Report View";
            }
            catch (Exception ex)
            {
                radLabel8.Text = ex.Message;
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
            dc.Close_Connection();   //close connection
            dc.OpenMYSQLConnection(ipaddress);   //open connection
        }        

        String theme = "";

        private void Station_Production_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();   //open connection
            String Lang = "";

            //get language and theme
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
            dgvmo.ThemeName = theme;
            dgvoperations.ThemeName = theme;
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

        private void radButton1_Click(object sender, EventArgs e)
        {
            if(btnreport.Text=="Report View")
            {
                DataView view = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Station_Prod.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer1.RefreshReport();

                reportViewer1.Visible = true;
                btnreport.Text = "Table View";
            }
            else
            {
                reportViewer1.Visible = false;
                btnreport.Text = "Report View";
            }
        }

        private void dgvmo_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //get selected mo
            if (e.RowIndex < 0)
            {
                return;
            }

            if (controller_name == "--SELECT--" || controller_name == "")
            {
                radLabel8.Text = "Please Select a Controller";
                return;
            }

            if ((bool)dgvmo.Rows[e.RowIndex].Cells[0].Value == true)
            {
                dgvmo.Rows[e.RowIndex].Cells[0].Value = false;
            }
            else
            {
                dgvmo.Rows[e.RowIndex].Cells[0].Value = true;
            }

            RowSelected(dgvmo.Rows[e.RowIndex].Cells[1].Value.ToString(), dgvmo.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void dgvmo_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvoperations_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvoperations.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvoperations.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvoperations.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvoperations.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }
    }
}
