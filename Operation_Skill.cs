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
using Telerik.Charting;
using Telerik.WinControls;
using Telerik.WinControls.UI;
using Microsoft.Reporting.WinForms;

namespace SMARTMRT
{
    public partial class Operation_Skill : Telerik.WinControls.UI.RadForm
    {
        public Operation_Skill()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();   //connection class
        DataTable MO = new DataTable();
        DataTable data1 = new DataTable();
        DataTable emp = new DataTable();

        private void Operation_Skill_Load(object sender, EventArgs e)
        {
            dgvemployee.MasterTemplate.SelectLastAddedRow = false;
            dgvoperation.MasterTemplate.SelectLastAddedRow = false;
            //disable close button on search in grid
            dgvemployee.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvoperation.MasterView.TableSearchRow.ShowCloseButton = false;

            dgvemployee.Visible = false;
            emp.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            emp.Columns.Add("OP CODE");
            emp.Columns.Add("OP DESC");

            //get all the operation
            SqlDataAdapter sda = new SqlDataAdapter("select V_OPERATION_CODE,V_OPERATION_DESC from OPERATION_DB", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                emp.Rows.Add(false, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
            }
            dgvoperation.DataSource = emp;

            dgvoperation.Columns[1].ReadOnly = true;
            dgvoperation.Columns[2].ReadOnly = true;

            MO.Columns.Add("opcode");
            MO.Columns.Add("opdesc");
            MO.Columns.Add("Start Date");
            MO.Columns.Add("End Date");
            MO.Columns.Add("Piece Count");
            MO.Columns.Add("Work Duration");
            MO.Columns.Add("Efficiency");
            MO.Columns.Add("EMPID");

            data1.Columns.Add("EMPID");
            data1.Columns.Add("EMPNAME");
            data1.Columns.Add("STARTDATE");
            data1.Columns.Add("ENDDATE");
            data1.Columns.Add("TOTALPEICECOUNT");
            data1.Columns.Add("TOTALWORKDURATION");
            data1.Columns.Add("EFFICIENCY");
            data1.Columns.Add("OPCODE");
            data1.Columns.Add("OPDESC");

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

            //get all year
            sda = new SqlDataAdapter("SELECT distinct CONVERT(CHAR(4), TIME, 120) FROM HANGER_HISTORY", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbyear.Items.Add(dt.Rows[i][0].ToString());
                cmbyear.SelectedIndex = 0;
            }

            dgvoperation.Columns[0].IsVisible = false;

            //get the first operation skill report
            if (dgvoperation.Rows.Count > 0)
            {
                RowSelected(dgvoperation.Rows[0].Cells[1].Value.ToString(), dgvoperation.Rows[0].Cells[2].Value.ToString());
            }
        }

        String theme = "";
        private void Operation_Skill_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();   //open connection

            //get the language and theme
            String Lang = "";
            SqlCommand cmd = new SqlCommand("SELECT Language,ThemeName FROM Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Lang = sdr.GetValue(0).ToString();
                theme = sdr.GetValue(1).ToString();
            }
            sdr.Close();
                     
            //change gtid theme
            GridTheme(theme);
        }

        //set grid theme
        public void GridTheme(String theme)
        {
            dgvemployee.ThemeName = theme;
            dgvoperation.ThemeName = theme;
        }

        private void Operation_Skill_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }       

        public void RowSelected(String op, String opdesc)
        {
            try
            {                
                radChartView1.Series.Clear();
                data1.Rows.Clear();

                radChartView1.Visible = true;
                btnchart.Text = "Table View";

                reportViewer1.Visible = false;
                btnreport.Text = "Show Report";                
                int sam = 0;

                int breaktime_complete = 0;
                DateTime shift_start = Convert.ToDateTime("9:30:00");
                DateTime shift_end = Convert.ToDateTime("18:30:00");
                String shifts = "";

                //get shift detials 
                SqlCommand cmd = new SqlCommand("SELECT T.T_SHIFT_START_TIME,T.T_SHIFT_END_TIME,T.T_OVERTIME_END_TIME,T.V_SHIFT FROM SHIFTS T WHERE CAST(GETDATE() AS TIME) BETWEEN cast(T.T_SHIFT_START_TIME as TIME) AND cast(T.T_OVERTIME_END_TIME as TIME)", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    shift_start = Convert.ToDateTime(sdr.GetValue(0).ToString());
                    shift_end = Convert.ToDateTime(sdr.GetValue(1).ToString());
                    shifts = sdr.GetValue(2).ToString();
                }
                sdr.Close();

                //get shift break completed
                cmd = new SqlCommand("select I_BREAK_TIMESPAN from SHIFT_BREAKS where V_SHIFT<='" + shifts + "'", dc.con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    breaktime_complete = breaktime_complete + int.Parse(sdr.GetValue(0).ToString());
                }
                sdr.Close();

                //get sam of the operation
                cmd = new SqlCommand("select D_SAM from OPERATION_DB where V_OPERATION_CODE='" + op + "'", dc.con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    sam = int.Parse(sdr.GetValue(0).ToString());
                }
                sdr.Close();

                //calculate work duration
                TimeSpan ts_workduration = shift_end - shift_start;
                int work_duration1 = (int)ts_workduration.TotalMinutes;
                work_duration1 = work_duration1 - breaktime_complete;
                work_duration1 = 1440 - work_duration1;

                dgvemployee.Rows.Clear();
                MO.Rows.Clear();

                int workduration = 0;

                DateTime start_time = DateTime.Now;
                DateTime end_time = DateTime.Now;
                String sht_start = shift_start.ToString("HH:mm:ss");
                String sht_end = shift_end.ToString("HH:mm:ss");

                //get production detial
                SqlDataAdapter sda = new SqlDataAdapter("select SUM(PC_COUNT) as COUNTS,SEQ_NO,MO_NO,MO_LINE,EMP_ID from HANGER_HISTORY where CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') group by SEQ_NO,MO_NO,MO_LINE,EMP_ID order by MO_NO,MO_LINE,SEQ_NO,EMP_ID", dc.con);
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

                    //get article id
                    cmd = new SqlCommand("select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        article = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    String empid = dt3.Rows[i][4].ToString();

                    //get first hanger and last hanger time
                    SqlDataAdapter sda2 = new SqlDataAdapter("select TIME from HANGER_HISTORY where EMP_ID='" + empid + "' and SEQ_NO='" + seqno + "' order by TIME", dc.con);
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

                    start = start_time;
                    end = end_time;

                    //get operation for the mo
                    SqlDataAdapter sda1 = new SqlDataAdapter("select OP.V_OPERATION_CODE,OP.V_OPERATION_DESC from DESIGN_SEQUENCE DS,OPERATION_DB OP where DS.V_OPERATION_CODE=OP.V_OPERATION_CODE  and DS.V_ARTICLE_ID='" + article + "' and I_SEQUENCE_NO='" + seqno + "'", dc.con);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    sda1.Dispose();
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j][0].ToString() == op)
                        {
                            int flag = 0;
                            for (int k = 0; k < MO.Rows.Count; k++)
                            {
                                if (MO.Rows[k][7].ToString() == empid)
                                {
                                    int count1 = int.Parse(MO.Rows[k][4].ToString());
                                    count1 = count1 + count;
                                    MO.Rows[k][4] = count1;
                                    flag = 1;
                                    break;
                                }
                            }

                            if (flag == 0)
                            {
                                TimeSpan ts = new TimeSpan();
                                ts = end - start;
                                workduration = (int)ts.TotalMinutes;
                                MO.Rows.Add(dt1.Rows[j][0].ToString(), dt1.Rows[j][1].ToString(), start, end, count, workduration, "0", empid);
                            }
                        }
                    }

                    //get employee groupid for the employee
                    //****************************//
                    sda = new SqlDataAdapter("select V_GROUP_ID from EMPLOYEE_GROUPS where V_EMP_ID='" + empid + "'", dc.con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int p = 0; p < dt.Rows.Count; p++)
                    {
                        String groupid = dt.Rows[p][0].ToString();

                        sda2 = new SqlDataAdapter("select TIME from HANGER_HISTORY where EMP_ID='" + groupid + "' and SEQ_NO='" + seqno + "' order by TIME", dc.con);
                        dt2 = new DataTable();
                        sda2.Fill(dt2);
                        sda2.Dispose();
                        if (dt2.Rows.Count > 0)
                        {
                            start_time = Convert.ToDateTime(dt2.Rows[0][0].ToString());
                            end_time = Convert.ToDateTime(dt2.Rows[dt2.Rows.Count - 1][0].ToString());
                        }

                        start = Convert.ToDateTime(start_time.ToString("yyyy-MM-dd") + " " + sht_start);
                        end = Convert.ToDateTime(end_time.ToString("yyyy-MM-dd") + " " + sht_end);

                        start = start_time;
                        end = end_time;

                        //get production detials
                        sda1 = new SqlDataAdapter("select OP.V_OPERATION_CODE,OP.V_OPERATION_DESC from DESIGN_SEQUENCE DS,OPERATION_DB OP where DS.V_OPERATION_CODE=OP.V_OPERATION_CODE  and DS.V_ARTICLE_ID='" + article + "' and I_SEQUENCE_NO='" + seqno + "'", dc.con);
                        dt1 = new DataTable();
                        sda1.Fill(dt1);
                        sda1.Dispose();
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            if (dt1.Rows[j][0].ToString() == op)
                            {
                                int flag = 0;
                                for (int k = 0; k < MO.Rows.Count; k++)
                                {
                                    if (MO.Rows[k][7].ToString() == empid)
                                    {
                                        int count1 = int.Parse(MO.Rows[k][4].ToString());
                                        count1 = count1 + count;
                                        MO.Rows[k][4] = count1;
                                        flag = 1;
                                        break;
                                    }
                                }

                                if (flag == 0)
                                {
                                    TimeSpan ts = new TimeSpan();
                                    ts = end - start;
                                    workduration = (int)ts.TotalMinutes;
                                    MO.Rows.Add(dt1.Rows[j][0].ToString(), dt1.Rows[j][1].ToString(), start, end, count, workduration, "0", empid);
                                }
                            }
                        }
                    }
                    //*****************************************//
                }


                for (int i = 0; i < MO.Rows.Count; i++)
                {
                    BarSeries barSeries1 = new BarSeries("Performance", "RepresentativeName");
                    barSeries1.LegendTitle = "Efficiency";

                    int count = int.Parse(MO.Rows[i][4].ToString());
                    int work_duration = int.Parse(MO.Rows[i][5].ToString());
                    int work = int.Parse(MO.Rows[i][5].ToString());

                    DateTime start = Convert.ToDateTime(MO.Rows[i][2].ToString());
                    DateTime end = Convert.ToDateTime(MO.Rows[i][3].ToString());
                    TimeSpan ts = new TimeSpan();
                    ts = end - start;
                    int breaks = (int)ts.TotalDays;

                    //get week offs
                    String weekoff = "";
                    cmd = new SqlCommand("select WEEK_OFF from Setup", dc.con);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        weekoff = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    int holiday = CountWeekEnds(start, end, weekoff);
                    //check if the hide day is enabled for the day
                    cmd = new SqlCommand("select count(*) from HOLIDAY_DB where D_HOLIDAY BETWEEN '" + start.ToString("yyyy-MM-dd") + "' and '" + end.ToString("yyyy-MM-dd") + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        holiday = holiday + int.Parse(sdr.GetValue(0).ToString());
                    }
                    sdr.Close();

                    //calculate work duration
                    breaks = breaks + holiday;
                    breaks = work_duration1 * breaks;
                    work_duration = work_duration - breaks;

                    if (work_duration < 0)
                    {
                        work_duration *= -1;
                    }

                    //calculate actual sam
                    decimal actual_sam = 0;
                    if (count > 0)
                    {
                        actual_sam = work_duration * 60 / count;
                    }

                    //calculate efficiency
                    decimal efficiency = 0;
                    if (actual_sam > 0)
                    {
                        efficiency = sam * 100 / actual_sam;
                    }

                    String empid = MO.Rows[i][7].ToString();

                    //get employee first name
                    cmd = new SqlCommand("select V_FIRST_NAME from EMPLOYEE where V_EMP_ID='" + empid + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    String empname = "";
                    while (sdr.Read())
                    {
                        empname = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //add to grid
                    dgvemployee.Rows.Add(empid, empname, MO.Rows[i][2].ToString(), MO.Rows[i][3].ToString(), MO.Rows[i][4].ToString(), work_duration, efficiency.ToString("0.##") + "%");
                    data1.Rows.Add(empid, empname, MO.Rows[i][2].ToString(), MO.Rows[i][3].ToString(), MO.Rows[i][4].ToString(), work_duration, efficiency.ToString("0.##"), op, opdesc);
                    
                    //generate chart
                    barSeries1.DataPoints.Add(new CategoricalDataPoint((int)efficiency, empid));
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
                    ca1.Title = "Employee ID";
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
            //check if report button is clicked
            btnreport.Text = "Show Report";
            if (btnchart.Text == "Chart View")
            {
                radChartView1.Visible = true;
                reportViewer1.Visible = false;
                dgvemployee.Visible = false;
                btnchart.Text = "Table View";
                return;
            }
            if (btnchart.Text == "Table View")
            {
                dgvemployee.Visible = true;
                reportViewer1.Visible = false;
                btnchart.Text = "Chart View";
                return;
            }
        }

        public int CountWeekEnds(DateTime startDate, DateTime endDate, String Day)
        {
            //caculate weekoffs from start to end date
            String[] dayoff = Day.Split(',');
            int weekEndCount = 0;
            if (startDate > endDate)
            {
                DateTime temp = startDate;
                startDate = endDate;
                endDate = temp;
            }

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
                BarSeries barSeries1 = new BarSeries("Performance", "RepresentativeName");
                barSeries1.LegendTitle = "Efficiency";
                radChartView1.Series.Clear();
                radChartView1.Visible = true;
                btnchart.Text = "Table View";

                if(dgvoperation.SelectedRows.Count<0)
                {
                    return;
                }
                String op = dgvoperation.SelectedRows[0].Cells[1].Value.ToString();
                int sam = 0;

                int breaktime_complete = 0;
                DateTime shift_start = Convert.ToDateTime("9:30:00");
                DateTime shift_end = Convert.ToDateTime("18:30:00");
                DateTime current_time = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                String shifts = "";

                //get shift details
                SqlCommand cmd = new SqlCommand("select T_SHIFT_START_TIME,T_SHIFT_END_TIME,V_SHIFT from SHIFTS where T_SHIFT_START_TIME<='" + current_time.ToString("HH:mm:ss") + "' and T_SHIFT_END_TIME>='" + current_time.ToString("HH:mm:ss") + "'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    shift_start = Convert.ToDateTime(sdr.GetValue(0).ToString());
                    shift_end = Convert.ToDateTime(sdr.GetValue(1).ToString());
                    shifts = sdr.GetValue(2).ToString();
                }
                sdr.Close();

                //get break complete for the shift
                cmd = new SqlCommand("select I_BREAK_TIMESPAN from SHIFT_BREAKS where V_SHIFT<='" + shifts + "'", dc.con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    breaktime_complete = breaktime_complete + int.Parse(sdr.GetValue(0).ToString());
                }
                sdr.Close();

                //get sam for the operation
                cmd = new SqlCommand("select D_SAM from OPERATION_DB where V_OPERATION_CODE='" + op + "'", dc.con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    sam = int.Parse(sdr.GetValue(0).ToString());
                }
                sdr.Close();

                //calculate work duration
                TimeSpan ts_workduration = shift_end - shift_start;
                int work_duration1 = (int)ts_workduration.TotalMinutes;
                work_duration1 = work_duration1 - breaktime_complete;
                work_duration1 = 1440 - work_duration1;

                dgvemployee.Rows.Clear();
                MO.Rows.Clear();

                int workduration = 0;

                DateTime start_time = DateTime.Now;
                DateTime end_time = DateTime.Now;
                String sht_start = shift_start.ToString("HH:mm:ss");
                String sht_end = shift_end.ToString("HH:mm:ss");
                String month = cmbmonth.Text;
                String year = cmbyear.Text;

                //get production details
                SqlDataAdapter sda = new SqlDataAdapter("select SUM(PC_COUNT) as COUNTS,SEQ_NO,MO_NO,MO_LINE,EMP_ID from HANGER_HISTORY where MONTH(TIME)=" + month + " and YEAR(TIME)=" + year + "  and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') group by SEQ_NO,MO_NO,MO_LINE,EMP_ID order by MO_NO,MO_LINE,SEQ_NO,EMP_ID", dc.con);
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
                    
                    //get article id
                    cmd = new SqlCommand("select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        article = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    String empid = dt3.Rows[i][4].ToString();

                    //get first an last hanger time
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

                    //get the operation for the mo
                    SqlDataAdapter sda1 = new SqlDataAdapter("select OP.V_OPERATION_CODE,OP.V_OPERATION_DESC from DESIGN_SEQUENCE DS,OPERATION_DB OP where DS.V_OPERATION_CODE=OP.V_OPERATION_CODE  and DS.V_ARTICLE_ID='" + article + "' and I_SEQUENCE_NO='" + seqno + "'", dc.con);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    sda1.Dispose();
                    
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        if (dt1.Rows[j][0].ToString() == op)
                        {
                            int flag = 0;
                            for (int k = 0; k < MO.Rows.Count; k++)
                            {
                                if (MO.Rows[k][7].ToString() == empid)
                                {
                                    int count1 = int.Parse(MO.Rows[k][4].ToString());
                                    count1 = count1 + count;
                                    MO.Rows[k][4] = count1;
                                    flag = 1;
                                }
                            }

                            if (flag == 0)
                            {
                                TimeSpan ts = new TimeSpan();
                                ts = end - start;
                                workduration = (int)ts.TotalMinutes;
                                MO.Rows.Add(dt1.Rows[j][0].ToString(), dt1.Rows[j][1].ToString(), start, end, count, workduration, "0", empid);
                            }
                        }
                    }
                }

                for (int i = 0; i < MO.Rows.Count; i++)
                {
                    int count = int.Parse(MO.Rows[i][4].ToString());
                    int work_duration = int.Parse(MO.Rows[i][5].ToString());
                    int work = int.Parse(MO.Rows[i][5].ToString());

                    DateTime start = Convert.ToDateTime(MO.Rows[i][2].ToString());
                    DateTime end = Convert.ToDateTime(MO.Rows[i][3].ToString());
                    TimeSpan ts = new TimeSpan();
                    ts = end - start;
                    int breaks = (int)ts.TotalDays;
                    String weekoff = "";

                    //get the weekoffs
                    cmd = new SqlCommand("select WEEK_OFF from Setup", dc.con);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        weekoff = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    int holiday = CountWeekEnds(start, end, weekoff);

                    //get all the holidays
                    cmd = new SqlCommand("select count(*) from HOLIDAY_DB where D_HOLIDAY BETWEEN '" + start.ToString("yyyy-MM-dd") + "' and '" + end.ToString("yyyy-MM-dd") + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        holiday = holiday + int.Parse(sdr.GetValue(0).ToString());
                    }
                    sdr.Close();

                    breaks = breaks + holiday;
                    breaks = work_duration1 * breaks;
                    work_duration = work_duration - breaks;
                    decimal actual_sam = 0;

                    //calculate actual sam
                    if (count > 0)
                    {
                        actual_sam = work_duration * 60 / count;
                    }

                    //calculate efficiency
                    decimal efficiency = 0;
                    if (actual_sam > 0)
                    {
                        efficiency = sam * 100 / actual_sam;
                    }

                    //get first name
                    String empid = MO.Rows[i][7].ToString();
                    cmd = new SqlCommand("select V_FIRST_NAME from EMPLOYEE where V_EMP_ID='" + empid + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    String empname = "";
                    while (sdr.Read())
                    {
                        empname = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //add to grid
                    dgvemployee.Rows.Add(empid, empname, MO.Rows[i][2].ToString(), MO.Rows[i][3].ToString(), MO.Rows[i][4].ToString(), work_duration, efficiency.ToString("0.##") + "%");
                    barSeries1.DataPoints.Add(new CategoricalDataPoint((int)efficiency, empid));
                }

                //generte chart
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
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
            }
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            //check if report button is clicked
            if (btnreport.Text == "Show Report")
            {
                dgvemployee.Visible = true;
                reportViewer1.Visible = true;
                btnreport.Text = "Hide Report";
                DataView view = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Operation_Skill.rdlc";
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

        private void dgvoperation_CellClick(object sender, GridViewCellEventArgs e)
        {
            //get selected operation 
            if (e.RowIndex < 0)
            {
                return;
            }
            if ((bool)dgvoperation.Rows[e.RowIndex].Cells[0].Value == true)
            {
                dgvoperation.Rows[e.RowIndex].Cells[0].Value = false;
            }
            else
            {
                dgvoperation.Rows[e.RowIndex].Cells[0].Value = true;
            }
            RowSelected(dgvoperation.Rows[e.RowIndex].Cells[1].Value.ToString(), dgvoperation.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void dgvoperation_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvoperation.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvoperation.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvoperation.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvoperation.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvemployee_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
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
    }
}
