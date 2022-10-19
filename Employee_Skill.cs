using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using Telerik.Charting;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Microsoft.Reporting.WinForms;

namespace SMARTMRT
{
    public partial class Employee_Skill : Telerik.WinControls.UI.RadForm
    {
        public Employee_Skill()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection(); //connection class
        DataTable MO = new DataTable();
        DataTable data1 = new DataTable();
        DataTable emp = new DataTable();

        private void Employee_Skill_Load(object sender, EventArgs e)
        {
            dgvemployee.MasterTemplate.SelectLastAddedRow = false;
            dgvoperations.MasterTemplate.SelectLastAddedRow = false;
            dgvemployee.MasterView.TableSearchRow.ShowCloseButton = false;    //disable close button for search in grid
            dgvoperations.MasterView.TableSearchRow.ShowCloseButton = false;  //disable close button for search in grid
            dgvoperations.Visible = false;

            MO.Columns.Add("OPCODE");
            MO.Columns.Add("OPDESC");
            MO.Columns.Add("Start Date");
            MO.Columns.Add("End Date");
            MO.Columns.Add("SAM");
            MO.Columns.Add("Actual SAM");
            MO.Columns.Add("Piece Count");
            MO.Columns.Add("Work Duration");
            MO.Columns.Add("Efficiency");

            data1.Columns.Add("OPCODE");
            data1.Columns.Add("OPDESC");
            data1.Columns.Add("STARTDATE");
            data1.Columns.Add("ENDDATE");
            data1.Columns.Add("SAM");
            data1.Columns.Add("ACTUALSAM");
            data1.Columns.Add("TOTALPEICECOUNT");
            data1.Columns.Add("TOTALWORKDURATION");
            data1.Columns.Add("EFFICIENCY");
            data1.Columns.Add("EMPID");
            data1.Columns.Add("EMPNAME");

            emp.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            emp.Columns.Add("Emp ID");
            emp.Columns.Add("Emp Name");

            //get emp first name and id
            SqlDataAdapter sda = new SqlDataAdapter("select V_EMP_ID,V_FIRST_NAME from EMPLOYEE", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                emp.Rows.Add(false, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
            }

            dgvemployee.DataSource = emp;
            dgvemployee.Columns[1].ReadOnly = true;
            dgvemployee.Columns[2].ReadOnly = true;

            //get all months
            sda = new SqlDataAdapter("SELECT distinct CONVERT(CHAR(2), TIME, 110) FROM HANGER_HISTORY", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbmonth.Items.Add(dt.Rows[i][0].ToString());
                cmbmonth.SelectedIndex = 0;
            }

            //get all years
            sda = new SqlDataAdapter("SELECT distinct CONVERT(CHAR(4), TIME, 120) FROM HANGER_HISTORY", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbyear.Items.Add(dt.Rows[i][0].ToString());
                cmbyear.SelectedIndex = 0;
            }

            dgvemployee.Columns[0].IsVisible = false;

            if (dgvemployee.Rows.Count > 0)
            {
                RowSelected(dgvemployee.Rows[0].Cells[1].Value.ToString(), dgvemployee.Rows[0].Cells[2].Value.ToString());
            }
        }

        private void Employee_Skill_FormClosed(object sender, FormClosedEventArgs e)
        {
            //dc.Close_Connection();
        }

        String theme = "";
        private void Employee_Skill_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();  //open connection
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
            dgvemployee.ThemeName = theme;
            dgvoperations.ThemeName = theme;
        }

        public void RowSelected(String empid, String EMPNAME)
        {
            try
            {                
                radChartView1.Series.Clear();
                radChartView1.Visible = true;
                data1.Rows.Clear();
                btnchart.Text = "Table View";
                reportViewer1.Visible = false;
                btnreport.Text = "Show Report";

                int breaktime_complete = 0;
                DateTime shift_start = Convert.ToDateTime("9:30:00");
                DateTime shift_end = Convert.ToDateTime("18:30:00");
                String shifts = "";

                //get shift details
                SqlCommand cmd = new SqlCommand("SELECT T.T_SHIFT_START_TIME,T.T_SHIFT_END_TIME,T.T_OVERTIME_END_TIME,T.V_SHIFT FROM SHIFTS T WHERE CAST(GETDATE() AS TIME) BETWEEN cast(T.T_SHIFT_START_TIME as TIME) AND cast(T.T_OVERTIME_END_TIME as TIME)", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    shift_start = Convert.ToDateTime(sdr.GetValue(0).ToString());
                    shift_end = Convert.ToDateTime(sdr.GetValue(1).ToString());
                    shifts = sdr.GetValue(2).ToString();
                }
                sdr.Close();

                //get the breakd time completed
                cmd = new SqlCommand("select I_BREAK_TIMESPAN from SHIFT_BREAKS where V_SHIFT<='" + shifts + "'", dc.con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    breaktime_complete = breaktime_complete + int.Parse(sdr.GetValue(0).ToString());
                }
                sdr.Close();

                //check if hime overtime in enabled
                String hide_ot = "";
                cmd = new SqlCommand("select HIDE_OVERTIME from Setup", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    hide_ot = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //calculate work duraion
                TimeSpan ts_workduration = shift_end - shift_start;
                int work_duration1 = (int)ts_workduration.TotalMinutes;
                work_duration1 = work_duration1 - breaktime_complete;
                work_duration1 = 1440 - work_duration1;

                dgvoperations.Rows.Clear();
                MO.Rows.Clear();
                //String empid = dgvemployee.Rows[e.RowIndex].Cells[0].Value.ToString();
                int workduration = 0;

                String sht_start = shift_start.ToString("HH:mm:ss");
                String sht_end = shift_end.ToString("HH:mm:ss");

                DateTime start_time = DateTime.Now;
                DateTime end_time = DateTime.Now;

                //get the production details for the employee
                String query = "select SUM(PC_COUNT) as COUNTS,SEQ_NO,MO_NO,MO_LINE from HANGER_HISTORY where EMP_ID = '" + empid + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10), D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE = 'TRUE') group by SEQ_NO,MO_NO,MO_LINE order by MO_NO, MO_LINE, SEQ_NO";
                SqlDataAdapter sda = new SqlDataAdapter(query, dc.con);
                DataTable dt3 = new DataTable();
                sda.Fill(dt3);
                sda.Dispose();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    String temp = dt3.Rows[i][0].ToString();
                    int count = 0;
                    if (temp != "")
                    {
                        count = int.Parse(dt3.Rows[i][0].ToString());
                    }

                    String seqno = dt3.Rows[i][1].ToString();
                    String mo = dt3.Rows[i][2].ToString();
                    String moline = dt3.Rows[i][3].ToString();
                    String article = "";

                    //get the article id 
                    cmd = new SqlCommand("select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        article = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //eget first hanger and last hanger time
                    sda = new SqlDataAdapter("select TIME from HANGER_HISTORY where EMP_ID='" + empid + "' and SEQ_NO='" + seqno + "' order by TIME", dc.con);
                    DataTable dt2 = new DataTable();
                    sda.Fill(dt2);
                    sda.Dispose();
                    if (dt2.Rows.Count > 0)
                    {
                        start_time = Convert.ToDateTime(dt2.Rows[0][0].ToString());
                        end_time = Convert.ToDateTime(dt2.Rows[dt2.Rows.Count - 1][0].ToString());
                    }

                    DateTime start = Convert.ToDateTime(start_time.ToString("yyyy-MM-dd") + " " + sht_start);
                    DateTime end = Convert.ToDateTime(end_time.ToString("yyyy-MM-dd") + " " + sht_end);

                    start = start_time;
                    end = end_time;

                    //get the operations for the article
                    sda = new SqlDataAdapter("select OP.V_OPERATION_CODE,OP.V_OPERATION_DESC,OP.D_SAM from DESIGN_SEQUENCE DS,OPERATION_DB OP where DS.V_OPERATION_CODE=OP.V_OPERATION_CODE  and DS.V_ARTICLE_ID='" + article + "' and I_SEQUENCE_NO='" + seqno + "'", dc.con);
                    DataTable dt1 = new DataTable();
                    sda.Fill(dt1);
                    sda.Dispose();
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        int flag = 0;
                        for (int k = 0; k < MO.Rows.Count; k++)
                        {
                            if (MO.Rows[k][0].ToString() == dt1.Rows[j][0].ToString())
                            {
                                int count1 = int.Parse(MO.Rows[k][6].ToString());
                                count1 = count1 + count;
                                MO.Rows[k][6] = count1;
                                flag = 1;
                                break;
                            }
                        }

                        if (flag == 0)
                        {
                            TimeSpan ts = new TimeSpan();
                            ts = end - start;
                            workduration = (int)ts.TotalMinutes;
                            MO.Rows.Add(dt1.Rows[j][0].ToString(), dt1.Rows[j][1].ToString(), start, end, dt1.Rows[j][2].ToString(), "0", count, workduration, "0");
                        }
                    }
                }

                //get all the employee groups for the employee
                //***************************************//
                sda = new SqlDataAdapter("select V_GROUP_ID from EMPLOYEE_GROUPS where V_EMP_ID='" + empid + "'", dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int p = 0; p < dt.Rows.Count; p++)
                {
                    String groupid = dt.Rows[p][0].ToString();

                    //get the production details for the group
                    query = "select SUM(PC_COUNT) as COUNTS,SEQ_NO,MO_NO,MO_LINE from HANGER_HISTORY where EMP_ID = '" + groupid + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10), D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE = 'TRUE') group by SEQ_NO,MO_NO,MO_LINE order by MO_NO, MO_LINE, SEQ_NO";
                    sda = new SqlDataAdapter(query, dc.con);
                    dt3 = new DataTable();
                    sda.Fill(dt3);
                    sda.Dispose();
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        String temp = dt3.Rows[i][0].ToString();
                        int count = 0;
                        if (temp != "")
                        {
                            count = int.Parse(dt3.Rows[i][0].ToString());
                        }

                        String seqno = dt3.Rows[i][1].ToString();
                        String mo = dt3.Rows[i][2].ToString();
                        String moline = dt3.Rows[i][3].ToString();
                        String article = "";

                        //get the article id
                        cmd = new SqlCommand("select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            article = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get the first hanger and last hanger time
                        sda = new SqlDataAdapter("select TIME from HANGER_HISTORY where EMP_ID='" + groupid + "' and SEQ_NO='" + seqno + "' order by TIME", dc.con);
                        DataTable dt2 = new DataTable();
                        sda.Fill(dt2);
                        sda.Dispose();
                        if (dt2.Rows.Count > 0)
                        {
                            start_time = Convert.ToDateTime(dt2.Rows[0][0].ToString());
                            end_time = Convert.ToDateTime(dt2.Rows[dt2.Rows.Count - 1][0].ToString());
                        }

                        DateTime start = Convert.ToDateTime(start_time.ToString("yyyy-MM-dd") + " " + sht_start);
                        DateTime end = Convert.ToDateTime(end_time.ToString("yyyy-MM-dd") + " " + sht_end);

                        start = start_time;
                        end = end_time;

                        //get the operation for the article
                        sda = new SqlDataAdapter("select OP.V_OPERATION_CODE,OP.V_OPERATION_DESC,OP.D_SAM from DESIGN_SEQUENCE DS,OPERATION_DB OP where DS.V_OPERATION_CODE=OP.V_OPERATION_CODE  and DS.V_ARTICLE_ID='" + article + "' and I_SEQUENCE_NO='" + seqno + "'", dc.con);
                        DataTable dt1 = new DataTable();
                        sda.Fill(dt1);
                        sda.Dispose();
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            int flag = 0;
                            for (int k = 0; k < MO.Rows.Count; k++)
                            {
                                if (MO.Rows[k][0].ToString() == dt1.Rows[j][0].ToString())
                                {
                                    int count1 = int.Parse(MO.Rows[k][6].ToString());
                                    count1 = count1 + count;
                                    MO.Rows[k][6] = count1;
                                    flag = 1;
                                    break;
                                }
                            }

                            if (flag == 0)
                            {
                                TimeSpan ts = new TimeSpan();
                                ts = end - start;
                                workduration = (int)ts.TotalMinutes;
                                MO.Rows.Add(dt1.Rows[j][0].ToString(), dt1.Rows[j][1].ToString(), start, end, dt1.Rows[j][2].ToString(), "0", count, workduration, "0");
                            }
                        }
                    }
                }

                //generate chart
                //********************************************//
                for (int i = 0; i < MO.Rows.Count; i++)
                {
                    BarSeries barSeries1 = new BarSeries("Performance", "RepresentativeName");
                    barSeries1.LegendTitle = "Efficiency";

                    int count = int.Parse(MO.Rows[i][6].ToString());
                    int work_duration = int.Parse(MO.Rows[i][7].ToString());
                    int work = int.Parse(MO.Rows[i][7].ToString());

                    DateTime start = Convert.ToDateTime(MO.Rows[i][2].ToString());
                    DateTime end = Convert.ToDateTime(MO.Rows[i][3].ToString());

                    TimeSpan ts = new TimeSpan();
                    ts = end - start;
                    int breaks = (int)ts.TotalDays;
                    String weekoff = "";

                    //get the week off 
                    cmd = new SqlCommand("select WEEK_OFF from Setup", dc.con);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        weekoff = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //check if the hideday is enabled for the day
                    int holiday = CountWeekEnds(start, end, weekoff);
                    cmd = new SqlCommand("select count(*) from HOLIDAY_DB where D_HOLIDAY BETWEEN '" + start.ToString("yyyy-MM-dd") + "' and '" + end.ToString("yyyy-MM-dd") + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        holiday = holiday + int.Parse(sdr.GetValue(0).ToString());
                    }
                    sdr.Close();

                    //get the breakstime
                    breaks = breaks + holiday;
                    breaks = work_duration1 * breaks;

                    work_duration = work_duration - breaks;

                    if (work_duration < 0)
                    {
                        work_duration *= -1;
                    }

                    //calculate the actual sam
                    decimal actual_sam = 0;
                    if (count > 0)
                    {
                        actual_sam = work_duration * 60 / count;
                    }

                    //calculate efficiency
                    decimal efficiency = 0;
                    if (actual_sam > 0)
                    {
                        efficiency = (int.Parse(MO.Rows[i][4].ToString()) * 100 / actual_sam);
                    }

                    dgvoperations.Rows.Add(MO.Rows[i][0].ToString(), MO.Rows[i][1].ToString(), MO.Rows[i][2].ToString(), MO.Rows[i][3].ToString(), MO.Rows[i][4].ToString(), actual_sam.ToString("0.##"), MO.Rows[i][6].ToString(), work_duration, efficiency.ToString("0.##") + "%"); ;
                    data1.Rows.Add(MO.Rows[i][0].ToString(), MO.Rows[i][1].ToString(), MO.Rows[i][2].ToString(), MO.Rows[i][3].ToString(), MO.Rows[i][4].ToString(), actual_sam.ToString("0.##"), MO.Rows[i][6].ToString(), work_duration, efficiency.ToString("0.##"), empid, EMPNAME);
                    
                    //generate chart
                    barSeries1.DataPoints.Add(new CategoricalDataPoint((int)efficiency, MO.Rows[i][0].ToString()));
                    barSeries1.ForeColor = Color.White;
                    radChartView1.Series.Add(barSeries1);
                    barSeries1.ShowLabels = true;
                    radChartView1.ForeColor = Color.White;

                    LinearAxis verticalAxis1 = radChartView1.Axes[1] as LinearAxis;
                    verticalAxis1.LabelFitMode = AxisLabelFitMode.MultiLine;
                    verticalAxis1.ForeColor = Color.White;
                    verticalAxis1.BorderColor = Color.DodgerBlue;
                    verticalAxis1.ShowLabels = false;
                    verticalAxis1.Title = "Efficiency";

                    CategoricalAxis ca1 = radChartView1.Axes[0] as CategoricalAxis;
                    ca1.LabelFitMode = AxisLabelFitMode.MultiLine;
                    ca1.Title = "Operations";
                    ca1.ForeColor = Color.White;
                    ca1.BorderColor = Color.DodgerBlue;

                    radChartView1.ForeColor = Color.White;
                }                
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
            }
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

        private void btnchart_Click(object sender, EventArgs e)
        {
            //show report
            btnreport.Text = "Show Report";
            if (btnchart.Text == "Chart View")
            {
                radChartView1.Visible = true;
                reportViewer1.Visible = false;
                dgvoperations.Visible = false;
                btnchart.Text = "Table View";
                return;
            }

            if (btnchart.Text == "Table View")
            {
                dgvoperations.Visible = true;
                reportViewer1.Visible = false;
                btnchart.Text = "Chart View";
                return;
            }
        }

        public int CountWeekEnds(DateTime startDate, DateTime endDate, String Day)
        {
            String[] dayoff = Day.Split(',');
            int weekEndCount = 0;
            if (startDate > endDate)
            {
                DateTime temp = startDate;
                startDate = endDate;
                endDate = temp;
            }

            //calculate week offs from start day to end day
            TimeSpan diff = endDate - startDate;
            int days = diff.Days;
            for (var i = 0; i <= days; i++)
            {
                var testDate = startDate.AddDays(i);

                for (int j = 0; j < dayoff.Length; j++)
                {
                    if (dayoff[j] == "Sunday")
                    {
                        if (testDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            weekEndCount += 1;
                        }
                    }

                    if (dayoff[j] == "Monday")
                    {
                        if (testDate.DayOfWeek == DayOfWeek.Monday)
                        {
                            weekEndCount += 1;
                        }
                    }

                    if (dayoff[j] == "Tuesday")
                    {
                        if (testDate.DayOfWeek == DayOfWeek.Tuesday)
                        {
                            weekEndCount += 1;
                        }
                    }

                    if (dayoff[j] == "Wednesday")
                    {
                        if (testDate.DayOfWeek == DayOfWeek.Wednesday)
                        {
                            weekEndCount += 1;
                        }
                    }

                    if (dayoff[j] == "Thursday")
                    {
                        if (testDate.DayOfWeek == DayOfWeek.Thursday)
                        {
                            weekEndCount += 1;
                        }
                    }

                    if (dayoff[j] == "Friday")
                    {
                        if (testDate.DayOfWeek == DayOfWeek.Friday)
                        {
                            weekEndCount += 1;
                        }
                    }

                    if (dayoff[j] == "Saturday")
                    {
                        if (testDate.DayOfWeek == DayOfWeek.Saturday)
                        {
                            weekEndCount += 1;
                        }
                    }
                }
            }

            return weekEndCount;
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            try
            {                
                radChartView1.Series.Clear();
                radChartView1.Visible = true;
                btnchart.Text = "Table View";

                int breaktime_complete = 0;
                DateTime shift_start = Convert.ToDateTime("9:30:00");
                DateTime shift_end = Convert.ToDateTime("18:30:00");

                DateTime current_time = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                String shifts = "";

                //get the shift details
                SqlCommand cmd = new SqlCommand("select T_SHIFT_START_TIME,T_SHIFT_END_TIME,V_SHIFT from SHIFTS where T_SHIFT_START_TIME<='" + current_time.ToString("HH:mm:ss") + "' and T_SHIFT_END_TIME>='" + current_time.ToString("HH:mm:ss") + "'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    shift_start = Convert.ToDateTime(sdr.GetValue(0).ToString());
                    shift_end = Convert.ToDateTime(sdr.GetValue(1).ToString());
                    shifts = sdr.GetValue(2).ToString();
                }
                sdr.Close();

                //get the break time completed
                cmd = new SqlCommand("select I_BREAK_TIMESPAN from SHIFT_BREAKS where V_SHIFT<='" + shifts + "'", dc.con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    breaktime_complete = breaktime_complete + int.Parse(sdr.GetValue(0).ToString());
                }
                sdr.Close();

                //calculate the work duration
                TimeSpan ts_workduration = shift_end - shift_start;
                int work_duration1 = (int)ts_workduration.TotalMinutes;
                work_duration1 = work_duration1 - breaktime_complete;
                work_duration1 = 1440 - work_duration1;

                dgvoperations.Rows.Clear();
                MO.Rows.Clear();
                if (dgvemployee.SelectedRows.Count < 0)
                {
                    return;
                }
                String empid = dgvemployee.SelectedRows[0].Cells[1].Value.ToString();
                int workduration = 0;

                String sht_start = shift_start.ToString("HH:mm:ss");
                String sht_end = shift_end.ToString("HH:mm:ss");

                DateTime start_time = DateTime.Now;
                DateTime end_time = DateTime.Now;

                String month = cmbmonth.Text;
                String year = cmbyear.Text;

                //get the production details for the employee for the month
                SqlDataAdapter sda = new SqlDataAdapter("select SUM(PC_COUNT) as COUNTS,SEQ_NO,MO_NO,MO_LINE from HANGER_HISTORY where EMP_ID='" + empid + "' and MONTH(TIME)=" + month + " and YEAR(TIME)=" + year + " and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') group by SEQ_NO,MO_NO,MO_LINE order by MO_NO,MO_LINE,SEQ_NO", dc.con);
                DataTable dt3 = new DataTable();
                sda.Fill(dt3);
                sda.Dispose();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    String temp = dt3.Rows[i][0].ToString();
                    int count = 0;
                    if (temp != "")
                    {
                        count = int.Parse(dt3.Rows[i][0].ToString());
                    }

                    String seqno = dt3.Rows[i][1].ToString();
                    String mo = dt3.Rows[i][2].ToString();
                    String moline = dt3.Rows[i][3].ToString();
                    String article = "";

                    //get the article id
                    cmd = new SqlCommand("select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        article = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the first hanger and last hanger time
                    SqlDataAdapter sda2 = new SqlDataAdapter("select TIME from HANGER_HISTORY where EMP_ID='" + empid + "' and MONTH(TIME)=" + month + " and YEAR(TIME)=" + year + " order by TIME", dc.con);
                    DataTable dt2 = new DataTable();
                    sda2.Fill(dt2);
                    sda2.Dispose();
                    if (dt2.Rows.Count > 0)
                    {
                        start_time = Convert.ToDateTime(dt2.Rows[0][0].ToString());
                        end_time = Convert.ToDateTime(dt2.Rows[dt2.Rows.Count - 1][0].ToString());
                    }

                    DateTime start = Convert.ToDateTime(start_time.ToString("yyyy-MM-dd") + " " + sht_start);
                    DateTime end = Convert.ToDateTime(end_time.ToString("yyyy-MM-dd") + " " + sht_end);

                    //get the operation for the article
                    SqlDataAdapter sda1 = new SqlDataAdapter("select OP.V_OPERATION_CODE,OP.V_OPERATION_DESC,OP.D_SAM from DESIGN_SEQUENCE DS,OPERATION_DB OP where DS.V_OPERATION_CODE=OP.V_OPERATION_CODE  and DS.V_ARTICLE_ID='" + article + "' and I_SEQUENCE_NO='" + seqno + "'", dc.con);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    sda1.Dispose();
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        int flag = 0;
                        for (int k = 0; k < MO.Rows.Count; k++)
                        {
                            if (MO.Rows[k][0].ToString() == dt1.Rows[j][0].ToString())
                            {
                                int count1 = int.Parse(MO.Rows[k][6].ToString());
                                count1 = count1 + count;
                                MO.Rows[k][6] = count1;
                                flag = 1;
                            }
                        }

                        if (flag == 0)
                        {
                            TimeSpan ts = new TimeSpan();
                            ts = end - start;
                            workduration = (int)ts.TotalMinutes;
                            MO.Rows.Add(dt1.Rows[j][0].ToString(), dt1.Rows[j][1].ToString(), start, end, dt1.Rows[j][2].ToString(), "0", count, workduration, "0");
                        }
                    }
                }

                //generate the chart
                for (int i = 0; i < MO.Rows.Count; i++)
                {
                    BarSeries barSeries1 = new BarSeries("Performance", "RepresentativeName");
                    barSeries1.LegendTitle = "Efficiency";

                    int count = int.Parse(MO.Rows[i][6].ToString());
                    int work_duration = int.Parse(MO.Rows[i][7].ToString());
                    int work = int.Parse(MO.Rows[i][7].ToString());

                    DateTime start = Convert.ToDateTime(MO.Rows[i][2].ToString());
                    DateTime end = Convert.ToDateTime(MO.Rows[i][3].ToString());
                    TimeSpan ts = new TimeSpan();
                    ts = end - start;
                    int breaks = (int)ts.TotalDays;

                    String weekoff = "";

                    //get the week offs
                    cmd = new SqlCommand("select WEEK_OFF from Setup", dc.con);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        weekoff = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    int holiday = CountWeekEnds(start, end, weekoff);

                    //check if the hideday is enabled for the day
                    cmd = new SqlCommand("select count(*) from HOLIDAY_DB where D_HOLIDAY BETWEEN '" + start.ToString("yyyy-MM-dd") + "' and '" + end.ToString("yyyy-MM-dd") + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        holiday = holiday + int.Parse(sdr.GetValue(0).ToString());
                    }
                    sdr.Close();

                    //calculate the break time
                    breaks = breaks + holiday;
                    breaks = work_duration1 * breaks;

                    work_duration = work_duration - breaks;

                    //calculate actual sam
                    decimal actual_sam = 0;
                    if (count > 0)
                    {
                        actual_sam = work_duration * 60 / count;
                    }

                    //calculate the efficiency
                    decimal efficiency = 0;
                    if (actual_sam > 0)
                    {
                        efficiency = (int.Parse(MO.Rows[i][4].ToString()) * 100 / actual_sam);
                    }

                    //add to grid
                    dgvoperations.Rows.Add(MO.Rows[i][0].ToString(), MO.Rows[i][1].ToString(), MO.Rows[i][2].ToString(), MO.Rows[i][3].ToString(), MO.Rows[i][4].ToString(), actual_sam.ToString("0.##"), MO.Rows[i][6].ToString(), work_duration, efficiency.ToString("0.##") + "%"); ;
                    
                    //generate the chart
                    barSeries1.DataPoints.Add(new CategoricalDataPoint((int)efficiency, MO.Rows[i][0].ToString()));

                    barSeries1.CombineMode = ChartSeriesCombineMode.Stack;
                    barSeries1.StackGroupKey = 1;
                    radChartView1.Series.Add(barSeries1);
                    barSeries1.ShowLabels = true;
                    radChartView1.ForeColor = Color.White;

                    LinearAxis verticalAxis1 = radChartView1.Axes[1] as LinearAxis;
                    verticalAxis1.LabelFitMode = AxisLabelFitMode.MultiLine;
                    verticalAxis1.ForeColor = Color.White;
                    verticalAxis1.BorderColor = Color.DodgerBlue;
                    verticalAxis1.ShowLabels = false;
                    verticalAxis1.Title = "Efficiency";

                    CategoricalAxis ca1 = radChartView1.Axes[0] as CategoricalAxis;
                    ca1.LabelFitMode = AxisLabelFitMode.MultiLine;
                    ca1.Title = "Operations";
                    ca1.ForeColor = Color.White;
                    ca1.BorderColor = Color.DodgerBlue;
                }
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
            }
        }
        
        private void btnreport_Click(object sender, EventArgs e)
        {
            //generate report
            if (btnreport.Text == "Show Report")
            {
                radChartView1.Visible = true;
                dgvoperations.Visible = true;
                reportViewer1.Visible = true;
                panel3.Visible = true;

                btnreport.Text = "Hide Report";
                DataView view = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Employee_Skill.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                //add views to dataset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer1.RefreshReport();
            }
            else
            {
                reportViewer1.Visible = false;
                btnreport.Text = "Show Report";
            }
        }

        private void dgvemployee_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            //get the selected employee id
            if ((bool)dgvemployee.Rows[e.RowIndex].Cells[0].Value == true)
            {
                dgvemployee.Rows[e.RowIndex].Cells[0].Value = false;
            }
            else
            {
                dgvemployee.Rows[e.RowIndex].Cells[0].Value = true;
            }

            //calculate the employee skill
            RowSelected(dgvemployee.Rows[e.RowIndex].Cells[1].Value.ToString(), dgvemployee.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void dgvemployee_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change the grid fore color if these themes are selected
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

        private void dgvoperations_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change the grid fore color if these themes are selected
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
