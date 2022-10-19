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
using Telerik.Charting;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Employee_Inspection : Telerik.WinControls.UI.RadForm
    {
        public Employee_Inspection()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();   //connection class
        DataTable EMP = new DataTable();
        String controller_name = "";
        DataTable data1 = new DataTable();
        DataTable data = new DataTable();
        DataTable data2 = new DataTable();

        private void Employee_Inspection_Load(object sender, EventArgs e)
        {
            dgvempreport.MasterTemplate.SelectLastAddedRow = false;
            dgvempreport.MasterView.TableSearchRow.ShowCloseButton = false;   //disable close button for search in grid 
            
            //add columns for datatable
            data1.Columns.Add("mono");
            data1.Columns.Add("modetails");
            data1.Columns.Add("color");
            data1.Columns.Add("article");
            data1.Columns.Add("size");
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
            data1.Columns.Add("station");
            data1.Columns.Add("sequence");
            data1.Columns.Add("peicecount");
            data1.Columns.Add("hangercount");
            data1.Columns.Add("rejects");

            data.Columns.Add("id");
            data.Columns.Add("name");
            data.Columns.Add("date");
            data.Columns.Add("shift");
            data.Columns.Add("totalpiececount");
            data.Columns.Add("averagesam");

            data2.Columns.Add("PIECE_COUNT");
            data2.Columns.Add("HOUR");
            data2.Columns.Add("OPDESC");

            radPanel2.Visible = false;
            dtpdate.Value = DateTime.Now;

            dc.OpenConnection();   //open connection
            select_controller();   //get the selected controller

            //get all the shifts
            SqlDataAdapter sda = new SqlDataAdapter("select V_SHIFT from SHIFTS", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            cmbshift.Items.Add("All");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbshift.Items.Add(dt.Rows[i][0].ToString());
            }

            //special fields
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

            //add columns for mo
            EMP.Columns.Add("MO No");
            EMP.Columns.Add("MO Details");
            EMP.Columns.Add("Color");
            EMP.Columns.Add("Article ID");
            EMP.Columns.Add("Size");
            EMP.Columns.Add(user1);
            EMP.Columns.Add(user2);
            EMP.Columns.Add(user3);
            EMP.Columns.Add(user4);
            EMP.Columns.Add(user5);
            EMP.Columns.Add(user6);
            EMP.Columns.Add(user7);
            EMP.Columns.Add(user8);
            EMP.Columns.Add(user9);
            EMP.Columns.Add(user10);
            EMP.Columns.Add("Station");
            EMP.Columns.Add("Sequence");
            EMP.Columns.Add("Piece Count");
            EMP.Columns.Add("Reject/Rework");
            EMP.Columns.Add("Hanger Count");
            dgvempreport.DataSource = EMP;
            dgvempreport.Columns[0].Width = 70;
            dgvempreport.Columns[1].Width = 60;
            dgvempreport.Columns[15].Width = 55;
            dgvempreport.Columns[16].Width = 65;
            dgvempreport.Columns[17].Width = 60;

            //hide the disbaled special fields
            if (user1 == "")
            {
                dgvempreport.Columns[5].IsVisible = false;
            }

            if (user2 == "")
            {
                dgvempreport.Columns[6].IsVisible = false;
            }

            if (user3 == "")
            {
                dgvempreport.Columns[7].IsVisible = false;
            }

            if (user4 == "")
            {
                dgvempreport.Columns[8].IsVisible = false;
            }

            if (user5 == "")
            {
                dgvempreport.Columns[9].IsVisible = false;
            }

            if (user6 == "")
            {
                dgvempreport.Columns[10].IsVisible = false;
            }

            if (user7 == "")
            {
                dgvempreport.Columns[11].IsVisible = false;
            }

            if (user8 == "")
            {
                dgvempreport.Columns[12].IsVisible = false;
            }

            if (user9 == "")
            {
                dgvempreport.Columns[13].IsVisible = false;
            }

            if (user10 == "")
            {
                dgvempreport.Columns[14].IsVisible = false;
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //check if controller is selected
                if (controller_name == "--SELECT--")
                {
                    radLabel8.Text = "Please Select a Controller";
                    return;
                }

                EMP.Rows.Clear();
                data1.Rows.Clear();
                data2.Rows.Clear();
                data.Rows.Clear();

                String MO = "";
                String MOLINE = "";
                String empid = "";
                SqlDataAdapter sda;

                double sam;
                double total = 0;

                panel3.Visible = true;
                panel4.Visible = true;
                panel7.Visible = true;
                btnreport.Text = "Report View";

                empid = txtempid.Text;

                //check if date is enabled in hide day
                SqlCommand cmd = new SqlCommand("select count(*) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE' and CONVERT(nvarchar(10),D_HIDEDAY, 120)='" + dtpdate.Value.ToString("yyyy-MM-dd") + "'", dc.con);
                int count2 = int.Parse(cmd.ExecuteScalar().ToString());
                if (count2 > 0)
                {
                    return;
                }

                DataTable hourly = new DataTable();
                hourly.Columns.Add("Hour");
                hourly.Columns.Add("Count");

                DateTime shift_start = Convert.ToDateTime("9:30:00");
                DateTime shift_end = Convert.ToDateTime("18:30:00");
                DateTime overtime_end = Convert.ToDateTime("19:30:00");

                //get the shift details
                cmd = new SqlCommand("select T_SHIFT_START_TIME,T_SHIFT_END_TIME,T_OVERTIME_END_TIME from SHIFTS where V_SHIFT='" + cmbshift.Text + "'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    shift_start = Convert.ToDateTime(sdr.GetValue(0).ToString());
                    shift_end = Convert.ToDateTime(sdr.GetValue(1).ToString());
                    overtime_end = Convert.ToDateTime(sdr.GetValue(2).ToString());
                }
                sdr.Close();

                //check if hide overtime is enabled
                String hide_ot = "";
                cmd = new SqlCommand("select HIDE_OVERTIME from Setup", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    hide_ot = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                String startime = shift_start.ToString("HH:mm:ss");
                String endtime = overtime_end.ToString("HH:mm:ss");

                String start_date = dtpdate.Value.ToString("yyyy-MM-dd");
                String end_date = dtpdate.Value.ToString("yyyy-MM-dd");
                if (shift_start > shift_end)
                {
                    start_date = dtpdate.Value.AddDays(-1).ToString("yyyy-MM-dd");
                }

                if (cmbshift.Text == "All")
                {
                    startime = "00:00:00";
                    endtime = "23:59:59";
                }

                if (hide_ot == "TRUE")
                {
                    endtime = shift_end.ToString("HH:mm:ss");
                }

                //get the production details for the employee
                MySqlDataAdapter da = new MySqlDataAdapter("Select EMP_ID,MO_NO,MO_LINE,SEQ_NO,STN_ID,SUM(PC_COUNT) from stationhistory where time>='" + start_date + " "+startime+"' and time<='" + end_date + " " + endtime + "' and emp_id='" + empid + "' GROUP BY EMP_ID,MO_NO,MO_LINE,SEQ_NO,STN_ID", dc.conn);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                da.Dispose();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    MO = dt1.Rows[i][1].ToString();
                    MOLINE = dt1.Rows[i][2].ToString();
                    String stn = dt1.Rows[i][4].ToString();
                    String stationno = "";
                    String lineno = "";
                    String sequence = "";
                    String temp = dt1.Rows[i][5].ToString();
                    int count = 0;

                    if (temp!="")
                    {
                        count = int.Parse(temp);
                    }   
                    
                    //get the station no of the employee 
                    SqlDataAdapter sda2 = new SqlDataAdapter("select I_INFEED_LINE_NO,I_STN_NO_INFEED from STATION_DATA where I_STN_ID='" + stn + "'", dc.con);
                    DataTable dt3 = new DataTable();
                    sda2.Fill(dt3);
                    for (int j = 0; j < dt3.Rows.Count; j++)
                    {
                        lineno = dt3.Rows[j][0].ToString();
                        stationno = dt3.Rows[j][1].ToString();
                    }
                    sda2.Dispose();

                    sequence = dt1.Rows[i][3].ToString();

                    //mo details
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

                    //get the mo details
                    sda = new SqlDataAdapter("Select V_COLOR_ID,V_ARTICLE_ID,V_SIZE_ID,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10 from MO_DETAILS where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                    DataTable dt2 = new DataTable();
                    sda.Fill(dt2);
                    for (int j = 0; j < dt2.Rows.Count; j++)
                    {
                        color = dt2.Rows[j][0].ToString();
                        article = dt2.Rows[j][1].ToString();
                        size = dt2.Rows[j][2].ToString();
                        user1 = dt2.Rows[j][3].ToString();
                        user2 = dt2.Rows[j][4].ToString();
                        user3 = dt2.Rows[j][5].ToString();
                        user4 = dt2.Rows[j][6].ToString();
                        user5 = dt2.Rows[j][7].ToString();
                        user6 = dt2.Rows[j][8].ToString();
                        user7 = dt2.Rows[j][9].ToString();
                        user8 = dt2.Rows[j][10].ToString();
                        user9 = dt2.Rows[j][11].ToString();
                        user10 = dt2.Rows[j][12].ToString();
                    }
                    sda.Dispose();

                    //get description for masters
                    cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        color = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get description for masters
                    cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + article + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        article = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get description for masters
                    cmd = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        size = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get description for masters
                    cmd = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + user1 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user1 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get description for masters
                    cmd = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + user2 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user2 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get description for masters
                    cmd = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + user3 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user3 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get description for masters
                    cmd = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + user4 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user4 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get description for masters
                    cmd = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + user5 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user5 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get description for masters
                    cmd = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + user6 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user6 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get description for masters
                    cmd = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + user7 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user7 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get description for masters
                    cmd = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + user8 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user8 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get description for masters
                    cmd = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + user9 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user9 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get description for masters
                    cmd = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + user10 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user10 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the hanger count for the mo
                    int hanger_count = 1;
                    SqlCommand cmd1 = new SqlCommand("select I_HANGER_COUNT,I_TARGET_DAY from MO_DETAILS where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                    SqlDataReader sdr1 = cmd1.ExecuteReader();
                    if (sdr1.Read())
                    {
                        hanger_count = int.Parse(sdr1.GetValue(0).ToString());
                    }
                    sdr1.Close();

                    //get repair quantity
                    cmd = new SqlCommand("Select sum(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + start_date + " " + startime + "' and D_DATE_TIME<='" + end_date + " " + endtime + "' and V_EMP_ID='" + empid + "' and I_SEQUENCE_NO='" + sequence + "' and V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                    String qc = cmd.ExecuteScalar() + "";

                    //add to datatable
                    EMP.Rows.Add(MO, MOLINE, color, article, size, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, lineno + "." + stationno, sequence, count, qc, hanger_count);
                    data1.Rows.Add(MO, MOLINE, color, article, size, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, lineno + "." + stationno, sequence, count, hanger_count, qc);
                }

                //get total work duration is seconds for the employee for the day
                da = new MySqlDataAdapter("Select TIME_TO_SEC(timediff(MAX(TIME),MIN(TIME)) ) AS ttime  FROM stationhistory  where time>='" + start_date + " " + startime + "' and time<='" + end_date + " " + endtime + "' and emp_id='" + empid + "' order by time desc", dc.conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() != "")
                    {
                        total = int.Parse(dt.Rows[i][0].ToString());
                    }
                    else
                    {
                        total = 0;
                    }
                }

                //get the group id for the employee 
                SqlDataAdapter sda1 = new SqlDataAdapter("select V_GROUP_ID from EMPLOYEE_GROUPS where V_EMP_ID='" + empid + "'", dc.con);
                dt = new DataTable();
                sda1.Fill(dt);
                sda1.Dispose();
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    String groupid = dt.Rows[k][0].ToString();

                    //get the production details for the group employee
                    da = new MySqlDataAdapter("Select EMP_ID,MO_NO,MO_LINE,SEQ_NO,STN_ID,SUM(PC_COUNT) from stationhistory where time>='" + start_date + " " + startime + "' and time<='" + end_date + " " + endtime + "' and emp_id='" + groupid + "' GROUP BY EMP_ID,MO_NO,MO_LINE,SEQ_NO,STN_ID", dc.conn);
                    dt1 = new DataTable();
                    da.Fill(dt1);
                    da.Dispose();
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        MO = dt1.Rows[i][1].ToString();
                        MOLINE = dt1.Rows[i][2].ToString();
                        String stn = dt1.Rows[i][4].ToString();
                        String stationno = "";
                        String lineno = "";
                        String sequence = "";
                        String temp = dt1.Rows[i][5].ToString();
                        int count = 0;

                        if (temp != "")
                        {
                            count = int.Parse(temp);
                        }

                        //get station no for employee
                        SqlDataAdapter sda2 = new SqlDataAdapter("select I_INFEED_LINE_NO,I_STN_NO_INFEED from STATION_DATA where I_STN_ID='" + stn + "'", dc.con);
                        DataTable dt3 = new DataTable();
                        sda2.Fill(dt3);
                        for (int j = 0; j < dt3.Rows.Count; j++)
                        {
                            lineno = dt3.Rows[j][0].ToString();
                            stationno = dt3.Rows[j][1].ToString();
                        }
                        sda2.Dispose();

                        sequence = dt1.Rows[i][3].ToString();
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

                        //get the mo details
                        sda = new SqlDataAdapter("Select V_COLOR_ID,V_ARTICLE_ID,V_SIZE_ID,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10 from MO_DETAILS where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                        DataTable dt2 = new DataTable();
                        sda.Fill(dt2);
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            color = dt2.Rows[j][0].ToString();
                            article = dt2.Rows[j][1].ToString();
                            size = dt2.Rows[j][2].ToString();
                            user1 = dt2.Rows[j][3].ToString();
                            user2 = dt2.Rows[j][4].ToString();
                            user3 = dt2.Rows[j][5].ToString();
                            user4 = dt2.Rows[j][6].ToString();
                            user5 = dt2.Rows[j][7].ToString();
                            user6 = dt2.Rows[j][8].ToString();
                            user7 = dt2.Rows[j][9].ToString();
                            user8 = dt2.Rows[j][10].ToString();
                            user9 = dt2.Rows[j][11].ToString();
                            user10 = dt2.Rows[j][12].ToString();
                        }
                        sda.Dispose();

                        //get description for masters
                        cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            color = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get description for masters
                        cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + article + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            article = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get description for masters
                        cmd = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            size = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get description for masters
                        cmd = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + user1 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user1 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get description for masters
                        cmd = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + user2 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user2 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get description for masters
                        cmd = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + user3 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user3 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get description for masters
                        cmd = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + user4 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user4 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get description for masters
                        cmd = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + user5 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user5 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get description for masters
                        cmd = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + user6 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user6 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get description for masters
                        cmd = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + user7 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user7 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get description for masters
                        cmd = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + user8 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user8 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get description for masters
                        cmd = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + user9 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user9 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get description for masters
                        cmd = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + user10 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user10 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get the hanger count for the mo
                        int hanger_count = 1;
                        SqlCommand cmd1 = new SqlCommand("select I_HANGER_COUNT,I_TARGET_DAY from MO_DETAILS where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                        SqlDataReader sdr1 = cmd1.ExecuteReader();
                        if (sdr1.Read())
                        {
                            hanger_count = int.Parse(sdr1.GetValue(0).ToString());
                        }
                        sdr1.Close();

                        //get repair quantity for the employee
                        cmd = new SqlCommand("Select sum(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + start_date + " " + startime + "' and D_DATE_TIME<='" + end_date + " " + endtime + "' and V_EMP_ID='" + empid + "' and I_SEQUENCE_NO='" + sequence + "' and V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                        String qc = cmd.ExecuteScalar() + "";

                        //add to grid
                        EMP.Rows.Add(MO, MOLINE, color, article, size, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, lineno + "." + stationno, sequence, count, qc, hanger_count);
                        data1.Rows.Add(MO, MOLINE, color, article, size, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, lineno + "." + stationno, sequence, count, hanger_count);
                    }

                    //get the total work duration for the employee for the day
                    da = new MySqlDataAdapter("Select TIME_TO_SEC(timediff(MAX(TIME),MIN(TIME)) ) AS ttime  FROM stationhistory  where time>='" + start_date + " " + startime + "' and time<='" + end_date + " " + endtime + "' and emp_id='" + groupid + "' order by time desc", dc.conn);
                    DataTable dt_time = new DataTable();
                    da.Fill(dt_time);
                    da.Dispose();
                    for (int i = 0; i < dt_time.Rows.Count; i++)
                    {
                        if (dt_time.Rows[i][0].ToString() != "")
                        {
                            total = total + int.Parse(dt_time.Rows[i][0].ToString());
                        }
                        else
                        {
                            total = total + 0;
                        }
                    }

                    //get the hourly production for the employee
                    da = new MySqlDataAdapter("SELECT HOUR(TIME),MO_NO,MO_LINE,SUM(PC_COUNT) FROM stationhistory where time>='" + start_date + " " + startime + "' and time<='" + end_date + " " + endtime + "' and emp_id='" + groupid + "' GROUP BY HOUR(TIME),MO_NO,MO_LINE ORDER BY HOUR(TIME)", dc.conn);
                    DataTable dt6 = new DataTable();
                    da.Fill(dt6);
                    da.Dispose();
                    for (int i = 0; i < dt6.Rows.Count; i++)
                    {
                        int flag = 0;
                        for (int j = 0; j < hourly.Rows.Count; j++)
                        {
                            if (dt6.Rows[i][0].ToString() == hourly.Rows[j][0].ToString())
                            {
                                flag = 1;
                                int total_count = int.Parse(hourly.Rows[j][1].ToString());
                                int count1 = 0;

                                if (dt6.Rows[i][3].ToString() != "")
                                {
                                    count1 = int.Parse(dt6.Rows[i][3].ToString());
                                }

                                total_count = total_count + count1;
                                hourly.Rows[j][1] = total_count;
                            }
                        }
                        if (flag == 0)
                        {
                            int count1 = 0;

                            if (dt6.Rows[i][3].ToString() != "")
                            {
                                count1 = int.Parse(dt6.Rows[i][3].ToString());
                            }

                            hourly.Rows.Add(dt6.Rows[i][0].ToString(), count1);
                        }
                    }
                }

                dgvempreport.DataSource = EMP;                

                SqlCommand cmd2 = new SqlCommand("delete from EMP_SEQ", dc.con);
                cmd2.ExecuteNonQuery();

                int total_piececount = 0;
                for (int i = 0; i < dgvempreport.Rows.Count; i++)
                {
                    total_piececount = total_piececount + int.Parse(dgvempreport.Rows[i].Cells[17].Value.ToString());
                    String seq = dgvempreport.Rows[i].Cells[16].Value.ToString();
                    int count = int.Parse(dgvempreport.Rows[i].Cells[17].Value.ToString());
                    String mo = dgvempreport.Rows[i].Cells[0].Value.ToString();
                    String moline = dgvempreport.Rows[i].Cells[1].Value.ToString();

                    //get the article id
                    SqlDataAdapter sda4 = new SqlDataAdapter("select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                    DataTable at = new DataTable();
                    sda4.Fill(at);
                    sda4.Dispose();
                    String article = "";
                    for (int j = 0; j < at.Rows.Count; j++)
                    {
                        article = at.Rows[j][0].ToString();
                    }

                    //get the operation for the sequence
                    sda4 = new SqlDataAdapter("select OP.V_OPERATION_DESC,OP.V_OPERATION_CODE from DESIGN_SEQUENCE DS,OPERATION_DB OP where DS.V_OPERATION_CODE=OP.V_OPERATION_CODE  and DS.V_ARTICLE_ID='" + article + "' and I_SEQUENCE_NO='" + seq + "'", dc.con);
                    at = new DataTable();
                    sda4.Fill(at);
                    sda4.Dispose();
                    for (int j = 0; j < at.Rows.Count; j++)
                    {
                        //insert into emp_seq
                        SqlCommand cmd1 = new SqlCommand("insert into EMP_SEQ values('" + at.Rows[j][0].ToString() + "','" + count + "')", dc.con);
                        cmd1.ExecuteNonQuery();
                    }

                    panel4.Visible = true;
                }

                //get first and last name of the employee
                txtpiececnt.Text = total_piececount.ToString();
                sda = new SqlDataAdapter("select V_FIRST_NAME,V_LAST_NAME from EMPLOYEE where V_EMP_ID='" + empid + "'", dc.con);
                DataTable dt4 = new DataTable();
                sda.Fill(dt4);
                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    txtempfirstname.Text = dt4.Rows[i][0].ToString();
                    txtemplastname.Text = dt4.Rows[i][1].ToString();
                }
                sda.Dispose();

                //calculate actual sam
                sam = total / total_piececount;
                txtsam.Text = sam.ToString("0.##");

                data.Rows.Add(empid, txtempfirstname.Text, dtpdate.Text, cmbshift.Text, txtpiececnt.Text, txtsam.Text);
                LineSeries lineSeries = new LineSeries();
                radChartView1.Series.Clear();               

                //get the hourly production for the employee
                da = new MySqlDataAdapter("SELECT HOUR(TIME),MO_NO,MO_LINE,SUM(PC_COUNT) FROM stationhistory where time>='" + start_date + " " + startime + "' and time<='" + end_date + " " + endtime + "' and emp_id='" + empid + "' GROUP BY HOUR(TIME),MO_NO,MO_LINE ORDER BY HOUR(TIME)", dc.conn);
                DataTable dt5 = new DataTable();
                da.Fill(dt5);
                da.Dispose();
                for (int i = 0; i < dt5.Rows.Count; i++)
                {
                    int flag = 0;
                    for (int j = 0; j < hourly.Rows.Count; j++)
                    {
                        if (dt5.Rows[i][0].ToString() == hourly.Rows[j][0].ToString())
                        {
                            flag = 1;
                            int total_count = int.Parse(hourly.Rows[j][1].ToString());
                            int count1 = 0;
                            if (dt5.Rows[i][3].ToString() != "")
                            {
                                count1 = int.Parse(dt5.Rows[i][3].ToString());
                            }
                            total_count = total_count + count1;                            
                            hourly.Rows[j][1] = total_count;
                        }
                    }
                    if (flag == 0)
                    {
                        int count1 = 0;
                        if (dt5.Rows[i][3].ToString() != "")
                        {
                            count1 = int.Parse(dt5.Rows[i][3].ToString());
                        }
                        hourly.Rows.Add(dt5.Rows[i][0].ToString(), count1);
                    }               
                }                

                //add to datatable
                for (int i = 0; i < hourly.Rows.Count; i++)
                {
                    int count = int.Parse(hourly.Rows[i][1].ToString());
                    lineSeries.DataPoints.Add(new CategoricalDataPoint(count, hourly.Rows[i][0].ToString() + ":00:00"));
                    data2.Rows.Add(count, hourly.Rows[i][0].ToString() + ":00:00", "");
                }

                //generate chart
                radChartView1.Series.Add(lineSeries);
                lineSeries.ShowLabels = true;

                LinearAxis verticalAxis = radChartView1.Axes[1] as LinearAxis;
                verticalAxis.ForeColor = Color.White;
                verticalAxis.BorderColor = Color.DodgerBlue;
                verticalAxis.ShowLabels = false;
                verticalAxis.Title = "Piece Count";

                CategoricalAxis ca = radChartView1.Axes[0] as CategoricalAxis;
                ca.LabelFitMode = AxisLabelFitMode.Rotate;
                ca.Title = "Time";
                ca.LabelRotationAngle = 270;
                ca.ForeColor = Color.White;
                ca.BorderColor = Color.DodgerBlue;

                radChartView1.Series[0].BackColor = Color.White;
                radChartView1.Series[0].BorderColor = Color.White;                

                mRT_GLOBALDBDataSet.Clear();
                //generate chart
                SqlDataAdapter sda5 = new SqlDataAdapter("select SUM(COUNT) as COUNT ,OP_DESC from EMP_SEQ GROUP BY OP_DESC", dc.con);
                sda5.Fill(this.mRT_GLOBALDBDataSet.EMP_SEQ);
                radChartView1.ForeColor = Color.White;
                radChartView2.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                radLabel8.Text = ex.Message;
                MessageBox.Show(ex+"");
            }
        }

        String theme = "";
        private void Employee_Inspection_Initialized(object sender, EventArgs e)
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

            //change the form language
            SqlDataAdapter sda = new SqlDataAdapter("select " + Lang + " from Language where Form='EmployeeInspection' order by Item_No", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblempid.Text = dt.Rows[0][0].ToString() + " :";
                lblfname.Text = dt.Rows[1][0].ToString() + " :";
                lbllname.Text = dt.Rows[2][0].ToString() + " :";
                lblpiecerate.Text = dt.Rows[6][0].ToString() + " :";
                lblsam.Text = dt.Rows[7][0].ToString() + " :";
                lbldate.Text = dt.Rows[8][0].ToString() + " :";
                btnsearch.Text = dt.Rows[10][0].ToString();
            }

            //get emp id and emp name for search in textbox
            sda = new SqlDataAdapter("select V_EMP_ID,V_FIRST_NAME from EMPLOYEE", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txtempid.AutoCompleteCustomSource.Add(dt.Rows[i][0].ToString());
                txtempname.AutoCompleteCustomSource.Add(dt.Rows[i][1].ToString());
            }

            //change grid theme
            GridTheme(theme);
        }

        //set grid theme
        public void GridTheme(String theme)
        {
            dgvempreport.ThemeName = theme;
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

        public void select_controller()
        {
            dc.OpenConnection();    //open connection

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

            //get ipadderess for the controller
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

            dc.Close_Connection();     //close connection if open
            dc.OpenMYSQLConnection(ipaddress);   //open connection
        }
        

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            //generate report
            if (btnreport.Text == "Report View")
            {
                panel3.Visible = false;
                panel4.Visible = false;
                panel7.Visible = false;
                btnreport.Text = "Table View";

                DataView view = new DataView(data1);
                DataView view1 = new DataView(data);
                DataView view2 = new DataView(data2);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.emp_report.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                //add views to dataset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", view1));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", view2));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", dv_image));
                reportViewer1.RefreshReport();
            }
            else
            {
                panel3.Visible = true;
                panel4.Visible = true;
                panel7.Visible = true;
                btnreport.Text = "Report View";
            }
        }

        private void txtempid_Leave(object sender, EventArgs e)
        {
            Get_EMP_Name();   //get the emp name for the emp id
        }

        private void txtempname_Leave(object sender, EventArgs e)
        {
            Get_EMP_ID();   //get the emp id for the emp name
        }

        public void Get_EMP_Name()
        {
            //DebugLog("Get_EMP_Name()");
            String trackno = "";
            try
            {
                trackno = "1";
                //get the employee first name
                SqlDataAdapter sda = new SqlDataAdapter("select V_FIRST_NAME from EMPLOYEE where V_EMP_ID ='" + txtempid.Text + "'", dc.con);
                trackno = "2";
                DataTable dt = new DataTable();
                trackno = "3";
                sda.Fill(dt);
                trackno = "4";
                sda.Dispose();
                if (dt != null)
                {
                    trackno = "5";
                    if (dt.Rows.Count > 0)
                    {
                        trackno = "6";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            trackno = "7";
                            txtempname.Text = dt.Rows[i][0].ToString();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                //DebugLog("Get_EMP_Name(), Trackno - " + trackno + ", Err -" + ex.Message);
            }



            
        }

        public void Get_EMP_ID()
        {
            //get the employee id
            SqlDataAdapter sda = new SqlDataAdapter("select V_EMP_ID  from EMPLOYEE where V_FIRST_NAME='" + txtempname.Text + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txtempid.Text = dt.Rows[i][0].ToString();
            }
        }

        private void txtempid_TextChanged(object sender, EventArgs e)
        {
            
            //Get_EMP_Name();  //get the emp name for the emp id
        }

        private void txtempname_TextChanged(object sender, EventArgs e)
        {
            Get_EMP_ID();   //get the emp id for the emp name
        }

        private void dgvempreport_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change the grid fore color if these themes are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvempreport.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvempreport.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvempreport.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvempreport.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
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
