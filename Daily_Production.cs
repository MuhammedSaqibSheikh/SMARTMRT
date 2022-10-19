using Microsoft.Reporting.WinForms;
using Microsoft.SqlServer.Management.Smo;
using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Telerik.Charting;
using Telerik.WinControls;
using Telerik.WinControls.Data;
using Telerik.WinControls.UI;
//using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
//using Convert = System.Convert;

namespace SMARTMRT
{
    public partial class Daily_Production : RadForm
    {
        public Daily_Production()
        {
            InitializeComponent();

            //enable chart zoom
            chrtproduction.ShowPanZoom = true;
            ChartPanZoomController panZoomController = new ChartPanZoomController();
            panZoomController.PanZoomMode = ChartPanZoomMode.Horizontal;
            chrtproduction.Controllers.Add(panZoomController);
        }

        Database_Connection dc = new Database_Connection();  //connection class
        int updateflag = 0;  //update flag
        DataTable dttemp;
        String theme = "";
        String controller_name = "";
        String getallop = "";

        //report datatable
        DataTable data = new DataTable();
        DataTable data1 = new DataTable();
        DataTable data2 = new DataTable();
        DataTable data3 = new DataTable();
        DataTable data4 = new DataTable();
        DataTable data5 = new DataTable();
        DataTable data6 = new DataTable();
        DataTable data7 = new DataTable();
        DataTable data8 = new DataTable();

        private void Daily_Production_Load(object sender, EventArgs e)
        {
            RadMessageBox.SetThemeName("FluentDark");   //set message box theme
            dgvproduction.MasterTemplate.SelectLastAddedRow = false;
            //add columns for total production details
            dgvproduction.MasterView.TableSearchRow.ShowCloseButton = false;
            reportViewer1.Visible = false;

            select_controller();  //get the selected controller
            //check if the controller is selected
            if (controller_name == "")
            {
                RadMessageBox.Show("Please Select a controller.", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                this.Close();
            }

            data.Columns.Add("date");
            data.Columns.Add("total_target");
            data.Columns.Add("total_prod_normal");
            data.Columns.Add("total_repair_normal");
            data.Columns.Add("total_prod_over");
            data.Columns.Add("total_repair_over");
            data.Columns.Add("eff");
            data.Columns.Add("duration");
            data.Columns.Add("sam");
            data.Columns.Add("total_cost_normal");
            data.Columns.Add("total_cost_over");

            //add columns for daily permormance report
            data1.Columns.Add("date");
            data1.Columns.Add("total_target");
            data1.Columns.Add("total_prod_normal");
            data1.Columns.Add("total_repair_normal");
            data1.Columns.Add("total_prod_over");
            data1.Columns.Add("total_repair_over");
            data1.Columns.Add("total_eff");
            data1.Columns.Add("total_work_duration");
            data1.Columns.Add("total_average_sam");
            data1.Columns.Add("total_cost_normal");
            data1.Columns.Add("total_cost_over");
            data1.Columns.Add("mono");
            data1.Columns.Add("moline");
            data1.Columns.Add("target");
            data1.Columns.Add("prod_normal");
            data1.Columns.Add("repair_normal");
            data1.Columns.Add("prod_over");
            data1.Columns.Add("repair_over");
            data1.Columns.Add("eff");
            data1.Columns.Add("work_duration");
            data1.Columns.Add("total_sam");
            data1.Columns.Add("cost_normal");
            data1.Columns.Add("cost_over");

            //add columns for employee dialy performance report
            data2.Columns.Add("date");
            data2.Columns.Add("total_piece_count");
            data2.Columns.Add("total_repair_rework");
            data2.Columns.Add("empid");
            data2.Columns.Add("emp_name");
            data2.Columns.Add("piece_count");
            data2.Columns.Add("repair");

            //add columns for total production chart in report
            data3.Columns.Add("Date");
            data3.Columns.Add("Total_Production");

            //add columns for total cost chart in report
            data4.Columns.Add("Date");
            data4.Columns.Add("Total_Cost");

            //add columns for total repair chart in report
            data5.Columns.Add("Date");
            data5.Columns.Add("Total_Repair");

            data6.Columns.Add("MO_NO");
            data6.Columns.Add("MO_DETAILS");
            data6.Columns.Add("COLOR");
            data6.Columns.Add("ARTICLE");
            data6.Columns.Add("SIZE");
            data6.Columns.Add("USER1");
            data6.Columns.Add("USER2");
            data6.Columns.Add("USER3");
            data6.Columns.Add("USER4");
            data6.Columns.Add("USER5");
            data6.Columns.Add("USER6");
            data6.Columns.Add("USER7");
            data6.Columns.Add("USER8");
            data6.Columns.Add("USER9");
            data6.Columns.Add("USER10");
            data6.Columns.Add("u1");
            data6.Columns.Add("u2");
            data6.Columns.Add("u3");
            data6.Columns.Add("u4");
            data6.Columns.Add("u5");
            data6.Columns.Add("u6");
            data6.Columns.Add("u7");
            data6.Columns.Add("u8");
            data6.Columns.Add("u9");
            data6.Columns.Add("u10");
            data6.Columns.Add("date");
            data6.Columns.Add("opcode");
            data6.Columns.Add("opdesc");
            data6.Columns.Add("empid");
            data6.Columns.Add("empname");
            data6.Columns.Add("production", typeof(System.Int32));
            data6.Columns.Add("repair", typeof(System.Int32));
            data6.Columns.Add("seq");
            data6.Columns.Add("actual_sam", typeof(System.Int32));
            data6.Columns.Add("duration", typeof(System.Int32));
            data6.Columns.Add("efficiency", typeof(System.Int32));

            data7.Columns.Add("mono");
            data7.Columns.Add("moline");
            data7.Columns.Add("hour");
            data7.Columns.Add("loaded");
            data7.Columns.Add("unloaded");
            data7.Columns.Add("eff");
            data7.Columns.Add("rework");
            data7.Columns.Add("color");
            data7.Columns.Add("article");
            data7.Columns.Add("size");
            data7.Columns.Add("date");

            data8.Columns.Add("date");
            data8.Columns.Add("opcode");
            data8.Columns.Add("opdesc");
            data8.Columns.Add("empid");
            data8.Columns.Add("empname");
            data8.Columns.Add("production");
            data8.Columns.Add("repair");
            data8.Columns.Add("seq");
            data8.Columns.Add("hour");
            data8.Columns.Add("sam");
            data8.Columns.Add("actual_sam");
            data8.Columns.Add("efficiency");
            data8.Columns.Add("defect_rate");
            data8.Columns.Add("total");
            data8.Columns.Add("duration");
            data8.Columns.Add("hourlysam");

            dtpstart.Text = DateTime.Now.ToString();
            dtpend.Text = DateTime.Now.ToString();

            //calculate dialy performance
            Daily_Prod();

            // get all the production lines
            cmbline.Items.Clear();
            cmbline.Items.Add("All");
            cmbshift.Items.Add("All");
            cmbline.SelectedIndex = 0;
            SqlDataAdapter da = new SqlDataAdapter("SELECT distinct V_PROD_LINE FROM PROD_LINE_DB", dc.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                cmbline.Items.Add(dt.Rows[j][0].ToString());
            }

            da = new SqlDataAdapter("select V_SHIFT from SHIFTS", dc.con);
            dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                cmbshift.Items.Add(dt.Rows[j][0].ToString());
            }
        }

        public void select_controller()
        {
            try
            {
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

                //get the ip address
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

                dc.Close_Connection();  //close connection if open
                dc.OpenMYSQLConnection(ipaddress);   //open connection
            }
            catch (Exception ex)
            {
                radLabel1.Text = ex.Message;
            }
        }

        public void Daily_Prod()
        {
            try
            {
                toolTip1.SetToolTip(dgvproduction, null);
                btnreport.Text = "Report View";
                reportViewer1.Visible = false;
                updateflag = 0;
                dgvproduction.DataSource = null;
                cmbcharts.Text = "Total Production";

                //check if mo and employee checkbox is checked
                if (chkmono.Checked == true || chkemployee.Checked == true || chkmoemployee.Checked == true || chkhourlyperformance.Checked == true || chkempperformance.Checked == true || chkcumulativeperformance.Checked == true)
                {
                    if (chkemployee.Checked == true)
                    {
                        Include_EMP();  //calculate employee dialy perfomance
                    }
                    else if (chkmono.Checked == true)
                    {
                        Include_MO(); //calculate MO daily perfomance
                    }
                    else if (chkmoemployee.Checked == true)
                    {
                        Include_MO_EMP();   //calculate mo/emp dialy performance
                    }
                    else if (chkhourlyperformance.Checked == true)
                    {
                        Houly_Production();   //calculate hourly performance
                    }
                    else if (chkempperformance.Checked == true)
                    {
                        Hourly_MO_EMP();   //calculate hourly mo/emp performance
                    }
                    else if (chkcumulativeperformance.Checked == true)
                    {
                        Cumulative_Hourly_MO_EMP();   //calculate cumulative hourly mo/emp performance
                    }
                }
                else
                {
                    Day_Prod();   //calculate daily performance
                }
            }
            catch (Exception ex)
            {
                radLabel1.Text = ex.Message;
            }
        }

        public void Day_Prod()
        {
            try
            {
                //clear all the datatable
                data.Rows.Clear();
                data4.Rows.Clear();
                data5.Rows.Clear();
                data3.Rows.Clear();

                int total_target = 0;
                int total_production_normal = 0;
                int total_production_over = 0;
                int total_repair_normal = 0;
                int total_repair_over = 0;
                decimal total_avg_sam = 0;
                decimal total_cost_normal = 0;
                decimal total_cost_over = 0;
                int total_workduration = 0;
                int total_day = 0;
                decimal total_efficiency = 0;

                dttemp = new DataTable();

                //add columns for total dialy performance report
                DataTable dtprod = new DataTable();
                dtprod.Columns.Add("Date");
                dtprod.Columns.Add("Total Target");
                dtprod.Columns.Add("Total Production (Normal-Time)");
                dtprod.Columns.Add("Total Repair/Rework (Normal-Time)");
                dtprod.Columns.Add("Total Production (OverTime)");
                dtprod.Columns.Add("Total Repair/Rework (OverTime)");
                dtprod.Columns.Add("Total Average Efficiency");
                dtprod.Columns.Add("Total Work Duration (Min)");
                dtprod.Columns.Add("Total Average SAM (Min)");
                dtprod.Columns.Add("Total Cost (Normal-Time)");
                dtprod.Columns.Add("Total Cost (OverTime)");

                dtprod.Rows.Add("Total : ", "", "", "", "", "", "", "", "", "", "");

                DateTime startdate = Convert.ToDateTime(dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime enddate = Convert.ToDateTime(dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59");

                String shift_start = "";
                String shift_end = "";
                String overtime_end = "";

                //get shift details
                SqlCommand cmd1 = new SqlCommand("select T_SHIFT_START_TIME,T_SHIFT_END_TIME,T_OVERTIME_END_TIME from SHIFTS where V_SHIFT='" + cmbshift.Text + "'", dc.con);
                SqlDataReader sdr = cmd1.ExecuteReader();
                if (sdr.Read())
                {
                    shift_start = sdr.GetValue(1).ToString();
                    shift_end = sdr.GetValue(2).ToString();
                    overtime_end = sdr.GetValue(2).ToString();
                }
                sdr.Close();

                cmd1 = new SqlCommand("select HIDE_OVERTIME from SETUP", dc.con);
                String hide_overtime = cmd1.ExecuteScalar() + "";
                if (hide_overtime == "TRUE")
                {
                    shift_end = overtime_end;
                }

                //get details from first date to last date
                while (startdate < enddate)
                {
                    total_day += 1;
                    int totalrepair = 0;
                    int totaltarget = 0;
                    int totalunload = 0;

                    int totalrepair_normal = 0;
                    int totalunload_normal = 0;
                    int work_duration = 0;
                    decimal totalsam = 0;
                    int mocount = 0;
                    decimal totalcost_normal = 0;
                    decimal totalcost = 0;
                    decimal totaleff = 0;

                    String start = startdate.ToString("yyyy-MM-dd") + " 00:00:00";
                    String end = startdate.ToString("yyyy-MM-dd") + " 23:59:59";

                    if (cmbshift.Text != "All")
                    {
                        start = startdate.ToString("yyyy-MM-dd") + " " + shift_start;
                        end = startdate.ToString("yyyy-MM-dd") + " " + shift_end;
                    }

                    String sysUIFormat = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;

                    //check if date is enabled in hide day
                    SqlCommand cmd = new SqlCommand("select COUNT(*) from HIDEDAY_DB where CONVERT(nvarchar(10), '" + start + "', 120) in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                    mocount = int.Parse(cmd.ExecuteScalar() + "");
                    if (mocount > 0)
                    {
                        startdate = startdate.AddDays(1);
                        continue;
                    }

                    //get the first hanger and last hanger time
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT MIN(TIME), MAX(TIME) FROM HANGER_HISTORY WHERE TIME >= '" + start + "' and TIME <= '" + end + "'", dc.con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime date1 = DateTime.Now;
                        DateTime date2 = DateTime.Now;
                        if (dt.Rows[i][0].ToString() != "")
                        {
                            date1 = Convert.ToDateTime(dt.Rows[i][0].ToString());
                            date2 = Convert.ToDateTime(dt.Rows[i][1].ToString());
                        }

                        //calculate work duration
                        TimeSpan ts = date2 - date1;
                        work_duration += (int)ts.TotalSeconds;

                        cmd = new SqlCommand("select isnull(sum(I_BREAK_TIMESPAN), 0) from SHIFT_BREAKS where T_BREAK_TIME_START between '" + date1.ToString("HH:mm:ss") + "' and '" + date2.ToString("HH:mm:ss") + "' and T_BREAK_TIME_END between '" + date1.ToString("HH:mm:ss") + "' and '" + date2.ToString("HH:mm:ss") + "'", dc.con);
                        int breaks = int.Parse(cmd.ExecuteScalar() + "") * 60;
                        work_duration = work_duration - breaks;
                    }

                    total_workduration += work_duration;

                    //check if all the lines are selected
                    String query = "";
                    if (cmbline.Text == "All")
                    {
                        query = "select distinct MO_NO,MO_LINE from HANGER_HISTORY where  TIME >= '" + start + "' and TIME <= '" + end + "'";
                    }
                    else
                    {
                        query = "select distinct h.MO_NO,h.MO_LINE from HANGER_HISTORY h,STATION_DATA s where h.TIME >= '" + start + "' and h.TIME <= '" + end + "' and s.I_STN_ID=h.STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "'";
                    }

                    //get the mo used for the day
                    sda = new SqlDataAdapter(query, dc.con);
                    dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String mo = dt.Rows[i][0].ToString();
                        String moline = dt.Rows[i][1].ToString();
                        mocount += 1;
                        decimal sam = 0;

                        if (getallop == "TRUE")
                        {
                            //get the sum of sam for the article
                            cmd = new SqlCommand("select SUM(o.D_SAM) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "')", dc.con);
                            sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                if (sdr.GetValue(0).ToString() != "")
                                {
                                    sam = int.Parse(sdr.GetValue(0) + "");
                                }
                            }
                            sdr.Close();
                        }
                        else
                        {
                            //get the sum of sam for the article
                            cmd = new SqlCommand("select SUM(o.D_SAM) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "') and d.I_SEQUENCE_NO in(select s.I_SEQUENCE_NO from STATION_ASSIGN s where s.V_MO_NO='" + mo + "' and s.V_MO_LINE='" + moline + "' and s.I_STATION_ID!='0')", dc.con);
                            sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                if (sdr.GetValue(0).ToString() != "")
                                {
                                    sam = int.Parse(sdr.GetValue(0) + "");
                                }
                            }
                            sdr.Close();
                        }

                        //calculate total target production
                        if (sam > 0)
                        {
                            totaltarget += work_duration / (int)sam;
                        }
                    }

                    //check if all lines are selected
                    query = "";
                    if (cmbline.Text == "All")
                    {
                        query = "select SUM(I_QUANTITY),I_WORK_TYPE from QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' GROUP BY I_WORK_TYPE";
                    }
                    else
                    {
                        query = "select SUM(I_QUANTITY),I_WORK_TYPE from QC_HISTORY where I_STATION_ID like '" + cmbline.Text + ".%' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' GROUP BY I_WORK_TYPE";
                    }

                    //get sum of repair quantity for the day
                    sda = new SqlDataAdapter(query, dc.con);
                    dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int count = 0;
                        String temp = dt.Rows[i][0].ToString();
                        if (temp != "")
                        {
                            count = int.Parse(dt.Rows[i][0].ToString());
                        }
                        else
                        {
                            count = 0;
                        }

                        if (dt.Rows[i][1].ToString() == "1")
                        {
                            totalrepair = count;
                        }
                        else
                        {
                            totalrepair_normal = count;
                        }
                    }

                    query = "";
                    if (cmbline.Text == "All")
                    {
                        query = "SELECT MO_NO,MO_LINE,SUM(PC_COUNT),WORKTYPE FROM HANGER_HISTORY where   time>='" + start + "' and time<'" + end + "' and REMARKS='2' GROUP BY MO_NO,MO_LINE,WORKTYPE ORDER BY MO_NO,MO_LINE,WORKTYPE";
                    }
                    else
                    {
                        query = "SELECT h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT),h.WORKTYPE FROM HANGER_HISTORY h,STATION_DATA s where s.I_STN_ID=h.STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "' and time>='" + start + "' and time<'" + end + "' and REMARKS='2' GROUP BY MO_NO,MO_LINE,WORKTYPE ORDER BY MO_NO,MO_LINE,WORKTYPE";
                    }

                    //get mo wise total production
                    sda = new SqlDataAdapter(query, dc.con);
                    dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        String mo = dt.Rows[j][0].ToString();
                        String moline = dt.Rows[j][1].ToString();

                        decimal cost = 0;
                        decimal cost_normal = 0;
                        int count = 0;
                        int count_normal = 0;

                        if (getallop == "TRUE")
                        {
                            //get sum of piece rate and overtime time for the mo
                            cmd = new SqlCommand("select SUM(o.D_PIECERATE),SUM(o.D_OVERTIME_RATE) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "')", dc.con);
                            sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                if (sdr.GetValue(1) + "" != "")
                                {
                                    cost = Convert.ToDecimal(sdr.GetValue(1) + "");
                                }
                                if (sdr.GetValue(0) + "" != "")
                                {
                                    cost_normal = Convert.ToDecimal(sdr.GetValue(0) + "");
                                }
                            }
                            sdr.Close();
                        }
                        else
                        {
                            cmd = new SqlCommand("select SUM(o.D_PIECERATE),SUM(o.D_OVERTIME_RATE) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "') and d.I_SEQUENCE_NO in(select s.I_SEQUENCE_NO from STATION_ASSIGN s where s.V_MO_NO='" + mo + "' and s.V_MO_LINE='" + moline + "' and s.I_STATION_ID!='0')", dc.con);
                            sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                if (sdr.GetValue(1) + "" != "")
                                {
                                    cost = Convert.ToDecimal(sdr.GetValue(1) + "");
                                }
                                if (sdr.GetValue(0) + "" != "")
                                {
                                    cost_normal = Convert.ToDecimal(sdr.GetValue(0) + "");
                                }
                            }
                            sdr.Close();
                        }

                        String temp1 = dt.Rows[j][2].ToString();
                        if (temp1 != "")
                        {
                            count = int.Parse(dt.Rows[j][2].ToString());
                        }
                        else
                        {
                            count = 0;
                        }

                        //calculate total cost normal and overtime
                        if (dt.Rows[j][3].ToString() == "0")
                        {
                            count_normal = count;
                            count = 0;
                            totalunload_normal += count_normal;
                            totalcost_normal = totalcost_normal + (cost_normal * count_normal);
                        }
                        else
                        {
                            totalunload += count;
                            totalcost = totalcost + (cost * count);
                        }
                    }

                    //Hanafi | Date:03 / 08 / 2021 | removed due to the changes in data transfer process
                    //query = "";
                    //if (cmbline.Text == "All")
                    //{
                    //    query = "SELECT MO_NO,MO_LINE,SUM(PC_COUNT),WORKTYPE FROM hangerwip where time>='" + start + "' and time<'" + end + "' and REMARKS='2' GROUP BY MO_NO,MO_LINE,WORKTYPE ORDER BY MO_NO,MO_LINE,WORKTYPE";
                    //}
                    //else
                    //{
                    //    query = "SELECT h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT),h.WORKTYPE FROM hangerwip h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and time>='" + start + "' and time<'" + end + "' and REMARKS='2' GROUP BY MO_NO,MO_LINE,WORKTYPE ORDER BY MO_NO,MO_LINE,WORKTYPE";
                    //}

                    ////get mo wise total production
                    //MySqlDataAdapter sda1 = new MySqlDataAdapter(query, dc.conn);
                    //dt = new DataTable();
                    //sda1.Fill(dt);
                    //sda1.Dispose();
                    //for (int j = 0; j < dt.Rows.Count; j++)
                    //{
                    //    String mo = dt.Rows[j][0].ToString();
                    //    String moline = dt.Rows[j][1].ToString();

                    //    decimal cost = 0;
                    //    decimal cost_normal = 0;
                    //    int count = 0;
                    //    int count_normal = 0;

                    //    if (getallop == "TRUE")
                    //    {
                    //        //get sum of piece rate and overtime time for the mo
                    //        cmd = new SqlCommand("select SUM(o.D_PIECERATE),SUM(o.D_OVERTIME_RATE) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "')", dc.con);
                    //        sdr = cmd.ExecuteReader();
                    //        if (sdr.Read())
                    //        {
                    //            if (sdr.GetValue(1) + "" != "")
                    //            {
                    //                cost = Convert.ToDecimal(sdr.GetValue(1) + "");
                    //            }
                    //            if (sdr.GetValue(0) + "" != "")
                    //            {
                    //                cost_normal = Convert.ToDecimal(sdr.GetValue(0) + "");
                    //            }
                    //        }
                    //        sdr.Close();
                    //    }
                    //    else
                    //    {
                    //        cmd = new SqlCommand("select SUM(o.D_PIECERATE),SUM(o.D_OVERTIME_RATE) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "') and d.I_SEQUENCE_NO in(select s.I_SEQUENCE_NO from STATION_ASSIGN s where s.V_MO_NO='" + mo + "' and s.V_MO_LINE='" + moline + "' and s.I_STATION_ID!='0')", dc.con);
                    //        sdr = cmd.ExecuteReader();
                    //        if (sdr.Read())
                    //        {
                    //            if (sdr.GetValue(1) + "" != "")
                    //            {
                    //                cost = Convert.ToDecimal(sdr.GetValue(1) + "");
                    //            }
                    //            if (sdr.GetValue(0) + "" != "")
                    //            {
                    //                cost_normal = Convert.ToDecimal(sdr.GetValue(0) + "");
                    //            }
                    //        }
                    //        sdr.Close();
                    //    }

                    //    String temp1 = dt.Rows[j][2].ToString();
                    //    if (temp1 != "")
                    //    {
                    //        count = int.Parse(dt.Rows[j][2].ToString());
                    //    }
                    //    else
                    //    {
                    //        count = 0;
                    //    }

                    //    //calculate total cost normal and overtime
                    //    if (dt.Rows[j][3].ToString() == "0")
                    //    {
                    //        count_normal = count;
                    //        count = 0;
                    //        totalunload_normal += count_normal;
                    //        totalcost_normal = totalcost_normal + (cost_normal * count_normal);
                    //    }
                    //    else
                    //    {
                    //        totalunload += count;
                    //        totalcost = totalcost + (cost * count);
                    //    }
                    //}

                    //calculate total efficiency
                    if (totaltarget > 0)
                    {
                        totaleff = ((decimal)totalunload + (decimal)totalunload_normal) / (decimal)totaltarget * 100;
                    }

                    total_efficiency += totaleff;

                    //check if there is production for that day
                    if (totalunload_normal == 0 && totalunload == 0)
                    {
                        startdate = startdate.AddDays(1);
                        continue;
                    }

                    //calculate total actual sam
                    totalsam = work_duration / (totalunload + totalunload_normal);
                    totalsam /= 60;

                    //add to datatable
                    dtprod.Rows.Add(startdate.ToString("yyyy-MM-dd"), totaltarget, totalunload_normal, totalrepair_normal, totalunload, totalrepair, totaleff.ToString("0.##") + "%", work_duration / 60, totalsam.ToString("0.##"), totalcost_normal.ToString("0.##"), totalcost.ToString("0.##"));
                    data.Rows.Add(startdate.ToString("dd-MMM-yyyy"), totaltarget, totalunload_normal, totalrepair_normal, totalunload, totalrepair, totaleff.ToString("0.##") + "%", work_duration / 60, totalsam.ToString("0.##"), totalcost_normal.ToString("0.##"), totalcost.ToString("0.##"));

                    //add to datatable
                    String date = startdate.ToString("MMM - dd");
                    int count1 = totalunload + totalunload_normal;
                    data3.Rows.Add(date, count1);

                    // add to datatable
                    count1 = (int)totalcost + (int)totalcost_normal;
                    data4.Rows.Add(startdate.ToString("MMM - dd"), count1);

                    //add to datatable
                    count1 = totalrepair + totalrepair_normal;
                    data5.Rows.Add(startdate.ToString("MMM - dd"), count1);

                    //calculate total of total
                    total_target += totaltarget;
                    total_production_normal += totalunload_normal;
                    total_production_over += totalunload;
                    total_repair_normal += totalrepair_normal;
                    total_repair_over += totalrepair;
                    total_cost_normal += totalcost_normal;
                    total_cost_over += totalcost;
                    total_avg_sam += totalsam;
                    startdate = startdate.AddDays(1);
                }

                dttemp = dtprod.Copy();

                //calculate total average sam and efficiency
                total_avg_sam = total_avg_sam / total_day;
                total_efficiency = total_efficiency / total_day;
                dtprod.Rows[0][1] = total_target;
                dtprod.Rows[0][2] = total_production_normal;
                dtprod.Rows[0][3] = total_repair_normal;
                dtprod.Rows[0][4] = total_production_over;
                dtprod.Rows[0][5] = total_repair_over;
                dtprod.Rows[0][6] = total_efficiency.ToString("0.##") + "%";
                dtprod.Rows[0][7] = total_workduration / 60;
                dtprod.Rows[0][8] = total_avg_sam.ToString("0.##");
                dtprod.Rows[0][9] = total_cost_normal.ToString("0.##");
                dtprod.Rows[0][10] = total_cost_over.ToString("0.##");

                dgvproduction.DataSource = dtprod;
                dgvproduction.Rows[0].IsSelected = false;
                Production_Chart();  //update charts
            }
            catch (Exception ex)
            {
                radLabel1.Text = ex.Message;
                MessageBox.Show(ex + "");
            }
        }

        public void Include_MO()
        {
            try
            {
                data1.Rows.Clear();
                data3.Rows.Clear();
                data4.Rows.Clear();
                data5.Rows.Clear();

                //add columns for mo dialy production dtatable
                DataTable dtprod = new DataTable();
                dtprod.Columns.Add("Date");
                dtprod.Columns.Add("Total Target");
                dtprod.Columns.Add("Total Production (Normal-Time)");
                dtprod.Columns.Add("Total Repair/Rework (Normal-Time)");
                dtprod.Columns.Add("Total Production (OverTime)");
                dtprod.Columns.Add("Total Repair/Rework (OverTime)");
                dtprod.Columns.Add("Total Efficiency");
                dtprod.Columns.Add("Total Work Duration (Min)");
                dtprod.Columns.Add("Total Average SAM (Min)");
                dtprod.Columns.Add("Total Cost (Normal-Time)");
                dtprod.Columns.Add("Total Cost (OverTime)");
                dtprod.Columns.Add("MO No");
                dtprod.Columns.Add("MO Details");
                dtprod.Columns.Add("Target");
                dtprod.Columns.Add("Production (Normal-Time)");
                dtprod.Columns.Add("Repair/Rework (Normal-Time)");
                dtprod.Columns.Add("Production (OverTime)");
                dtprod.Columns.Add("Repair/Rework (OverTime)");
                dtprod.Columns.Add("Efficiency");
                dtprod.Columns.Add("Work Duration (Min)");
                dtprod.Columns.Add("Total SAM");
                dtprod.Columns.Add("Cost (Normal-Time)");
                dtprod.Columns.Add("Cost (OverTime)");

                DateTime startdate = Convert.ToDateTime(dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime enddate = Convert.ToDateTime(dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59");

                String shift_start = "";
                String shift_end = "";
                String overtime_end = "";

                //get shift details
                SqlCommand cmd1 = new SqlCommand("select T_SHIFT_START_TIME,T_SHIFT_END_TIME,T_OVERTIME_END_TIME from SHIFTS where V_SHIFT='" + cmbshift.Text + "'", dc.con);
                SqlDataReader sdr = cmd1.ExecuteReader();
                if (sdr.Read())
                {
                    //shift_start = sdr.GetValue(1).ToString();
                    //shift_end = sdr.GetValue(2).ToString();
                    //overtime_end = sdr.GetValue(2).ToString();

                    shift_start = sdr.GetValue(0).ToString();
                    shift_end = sdr.GetValue(1).ToString();
                    overtime_end = sdr.GetValue(2).ToString();
                }
                sdr.Close();

                cmd1 = new SqlCommand("select HIDE_OVERTIME from SETUP", dc.con);
                String hide_overtime = cmd1.ExecuteScalar() + "";
                if (hide_overtime == "TRUE")
                {
                    shift_end = overtime_end;
                }


                //get details of production for first date to last date
                while (startdate < enddate)
                {
                    //add columns to mo datatable
                    DataTable dtmo = new DataTable();
                    dtmo.Columns.Add("MO No");
                    dtmo.Columns.Add("MO Details");
                    dtmo.Columns.Add("Total Target");
                    dtmo.Columns.Add("Total Production (Normal-Time)");
                    dtmo.Columns.Add("Total Repair/Rework (Normal-Time)");
                    dtmo.Columns.Add("Total Production (OverTime)");
                    dtmo.Columns.Add("Total Repair/Rework (OverTime)");
                    dtmo.Columns.Add("Total Efficiency");
                    dtmo.Columns.Add("Work Duration");
                    dtmo.Columns.Add("Total SAM");
                    dtmo.Columns.Add("Cost (Normal-Time)");
                    dtmo.Columns.Add("Cost (OverTime)");

                    String start = startdate.ToString("yyyy-MM-dd") + " 00:00:00";
                    String end = startdate.ToString("yyyy-MM-dd") + " 23:59:59";

                    if (cmbshift.Text != "All")
                    {
                        start = startdate.ToString("yyyy-MM-dd") + " " + shift_start;
                        end = enddate.ToString("yyyy-MM-dd") + " " + shift_end;
                    }
                    //else
                    //{
                    //    if (cmbshift.Text == "3")
                    //    {
                    //        if (enddate > startdate)
                    //        {
                    //            enddate = enddate.AddDays(1);
                    //        }
                    //    }                        
                    //}

                    int totalrepair = 0;
                    int totaltarget = 0;
                    int totalunload = 0;
                    int mocount = 0;
                    int totalrepair_normal = 0;
                    int totalunload_normal = 0;
                    int work_duration = 0; //TOTAL DURATION (min)
                    decimal totalsam = 0;
                    decimal totalcost_normal = 0;
                    decimal totalcost = 0;

                    decimal totaleff = 0;                   

                    //check if date is enabled in hide day
                    SqlCommand cmd = new SqlCommand("select COUNT(*) from HIDEDAY_DB where CONVERT(nvarchar(10), '" + start + "', 120) in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                    mocount = int.Parse(cmd.ExecuteScalar() + "");
                    if (mocount > 0)
                    {
                        startdate = startdate.AddDays(1);
                        continue;
                    }

                    //get first hanger time and last hanger time
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT MIN(TIME), MAX(TIME) FROM HANGER_HISTORY WHERE TIME >= '" + start + "' and TIME <= '" + end + "'", dc.con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DateTime date1 = DateTime.Now;
                        DateTime date2 = DateTime.Now;
                        if (dt.Rows[i][0].ToString() != "")
                        {
                            date1 = Convert.ToDateTime(dt.Rows[i][0].ToString());
                            date2 = Convert.ToDateTime(dt.Rows[i][1].ToString());
                        }

                        //calculate workduration
                        TimeSpan ts = date2 - date1;
                        work_duration += (int)ts.TotalSeconds;

                        cmd = new SqlCommand("select isnull(sum(I_BREAK_TIMESPAN), 0) from SHIFT_BREAKS where T_BREAK_TIME_START between '" + date1.ToString("HH:mm:ss") + "' and '" + date2.ToString("HH:mm:ss") + "' and T_BREAK_TIME_END between '" + date1.ToString("HH:mm:ss") + "' and '" + date2.ToString("HH:mm:ss") + "'", dc.con);
                        int breaks = int.Parse(cmd.ExecuteScalar() + "") * 60;
                        work_duration = work_duration - breaks;
                    }

                    String query = "";
                    if (cmbline.Text == "All")
                    {
                        query = "select distinct MO_NO,MO_LINE from HANGER_HISTORY where  TIME >= '" + start + "' and TIME <= '" + end + "'";
                    }
                    else
                    {
                        query = "select distinct h.MO_NO,h.MO_LINE from HANGER_HISTORY h,STATION_DATA s where h.TIME >= '" + start + "' and h.TIME <= '" + end + "' and s.I_STN_ID=h.STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "'";
                    }

                    //get mo used for the day
                    sda = new SqlDataAdapter(query, dc.con);
                    dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String mo = dt.Rows[i][0].ToString();
                        String moline = dt.Rows[i][1].ToString();
                        decimal sam = 0;

                        if (getallop == "TRUE")
                        {
                            //get the sum of sam for the article 
                            cmd = new SqlCommand("select SUM(o.D_SAM) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "')", dc.con);
                            sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                if (sdr.GetValue(0).ToString() != "")
                                {
                                    sam = int.Parse(sdr.GetValue(0) + "");
                                }
                            }
                            sdr.Close();
                        }
                        else
                        {
                            //get the sum of sam for the article 
                            cmd = new SqlCommand("select SUM(o.D_SAM) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "') and d.I_SEQUENCE_NO in(select s.I_SEQUENCE_NO from STATION_ASSIGN s where s.V_MO_NO='" + mo + "' and s.V_MO_LINE='" + moline + "' and s.I_STATION_ID!='0')", dc.con);
                            sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                if (sdr.GetValue(0).ToString() != "")
                                {
                                    sam = int.Parse(sdr.GetValue(0) + "");
                                }
                            }
                            sdr.Close();
                        }
                    }

                    query = "";
                    if (cmbline.Text == "All")
                    {
                        query = "select V_MO_NO,V_MO_LINE, SUM(I_QUANTITY),I_WORK_TYPE from QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' GROUP BY  V_MO_NO,V_MO_LINE,I_WORK_TYPE ORDER BY V_MO_NO,V_MO_LINE";
                    }
                    else
                    {
                        query = "select V_MO_NO,V_MO_LINE, SUM(I_QUANTITY),I_WORK_TYPE from QC_HISTORY where I_STATION_ID like '" + cmbline.Text + ".%' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' GROUP BY  V_MO_NO,V_MO_LINE,I_WORK_TYPE ORDER BY V_MO_NO,V_MO_LINE";
                    }

                    //get repair quantity for the each mo
                    sda = new SqlDataAdapter(query, dc.con);
                    dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String mo = dt.Rows[i][0].ToString();
                        String moline = dt.Rows[i][1].ToString();
                        int count = 0;
                        String temp = dt.Rows[i][2].ToString();
                        if (temp != "")
                        {
                            count = int.Parse(dt.Rows[i][2].ToString());
                        }
                        else
                        {
                            count = 0;
                        }

                        //get total repair normal time and overtime
                        int normal = 0;
                        int overtime = 0;
                        if (dt.Rows[i][3].ToString() == "1")
                        {
                            totalrepair += count;
                            overtime = count;
                        }
                        else
                        {
                            normal = count;
                            totalrepair_normal += count;
                        }

                        //add to datatable
                        int flag = 0;
                        for (int j = 0; j < dtmo.Rows.Count; j++)
                        {
                            if (dtmo.Rows[j][0].ToString() == mo && dtmo.Rows[j][1].ToString() == moline)
                            {
                                int temp1 = int.Parse(dtmo.Rows[j][4].ToString());
                                temp1 += normal;
                                dtmo.Rows[j][4] = temp1;

                                temp1 = int.Parse(dtmo.Rows[j][6].ToString());
                                temp1 += overtime;
                                dtmo.Rows[j][6] = temp;
                                flag = 1;
                            }
                        }

                        if (flag == 0)
                        {
                            dtmo.Rows.Add(mo, moline, "0", "0", normal, "0", overtime, "0", "0", "0", "0", "0");
                        }
                    }

                    String prev_mo = "";
                    String prev_moline = "";
                    String cur_mo = "";
                    String cur_moline = "";
                    decimal eff = 0; //Fiza 051022
                    decimal moEff = 0; 

                    query = "";
                    if (cmbline.Text == "All")
                    {
                        query = "SELECT MO_NO,MO_LINE,SUM(PC_COUNT),WORKTYPE FROM HANGER_HISTORY where   time>='" + start + "' and time<'" + end + "' and REMARKS='2' GROUP BY MO_NO,MO_LINE,WORKTYPE ORDER BY MO_NO,MO_LINE,WORKTYPE";
                    }
                    else
                    {
                        query = "SELECT h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT),h.WORKTYPE FROM HANGER_HISTORY h,STATION_DATA s where s.I_STN_ID=h.STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "' and time>='" + start + "' and time<'" + end + "' and REMARKS='2' GROUP BY MO_NO,MO_LINE,WORKTYPE ORDER BY MO_NO,MO_LINE,WORKTYPE";
                    }

                    //get mo wise production
                    sda = new SqlDataAdapter(query, dc.con);
                    dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        prev_mo = cur_mo;
                        prev_moline = cur_moline;
                        String mo = dt.Rows[j][0].ToString();
                        String moline = dt.Rows[j][1].ToString();

                        cur_mo = mo;
                        cur_moline = moline;
                        decimal cost = 0;
                        decimal cost_normal = 0;
                        int count = 0;
                        int count_normal = 0;
                        decimal sam = 0;
                        int duration = 0;

                        string strSqlSAM = "";
                        if (getallop == "TRUE")
                        {
                            //get sum of sam , sum of piecerate and sum of overtime rate
                             strSqlSAM = "select SUM(o.D_SAM),SUM(o.D_PIECERATE),SUM(o.D_OVERTIME_RATE) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "')";
                            cmd = new SqlCommand(strSqlSAM, dc.con);
                            SqlDataReader sdr1 = cmd.ExecuteReader();
                            if (sdr1.Read())
                            {
                                if (sdr1.GetValue(0) + "" != "")
                                {
                                    sam = int.Parse(sdr1.GetValue(0) + "");
                                }
                                if (sdr1.GetValue(2) + "" != "")
                                {
                                    cost = Convert.ToDecimal(sdr1.GetValue(2) + "");
                                }
                                totalsam += sam;
                                if (sdr1.GetValue(1) + "" != "")
                                {
                                    cost_normal = Convert.ToDecimal(sdr1.GetValue(1) + "");
                                }
                            }
                            sdr1.Close();
                        }
                        else
                        {
                            //get sum of sam , sum of piecerate and sum of overtime rate
                            strSqlSAM = "select SUM(o.D_SAM),SUM(o.D_PIECERATE),SUM(o.D_OVERTIME_RATE) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE = o.V_OPERATION_CODE and d.V_ARTICLE_ID = (select V_ARTICLE_ID from MO_DETAILS where V_MO_NO = '" + mo + "' and V_MO_LINE = '" + moline + "') and d.I_SEQUENCE_NO in(select s.I_SEQUENCE_NO from STATION_ASSIGN s where s.V_MO_NO = '" + mo + "' and s.V_MO_LINE = '" + moline + "' and s.I_STATION_ID != '0')";
                            cmd = new SqlCommand(strSqlSAM, dc.con);
                            SqlDataReader sdr1 = cmd.ExecuteReader();
                            if (sdr1.Read())
                            {
                                if (sdr1.GetValue(0) + "" != "")
                                {
                                    sam = int.Parse(sdr1.GetValue(0) + "");
                                }
                                if (sdr1.GetValue(2) + "" != "")
                                {
                                    cost = Convert.ToDecimal(sdr1.GetValue(2) + "");
                                }
                                totalsam += sam;
                                if (sdr1.GetValue(1) + "" != "")
                                {
                                    cost_normal = Convert.ToDecimal(sdr1.GetValue(1) + "");
                                }

                                
                            }
                            sdr1.Close();
                        }

                        ////get the first hanger and last hanger time
                        //string strSql1 = "SELECT MIN(TIME), MAX(TIME) FROM HANGER_HISTORY WHERE TIME >= '" + start + "' and TIME <= '" + end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "'"; //MRT-GLOBALDB
                        //cmd = new SqlCommand(strSql1, dc.con);
                        //sdr = cmd.ExecuteReader();
                        //if (sdr.Read())
                        //{
                        //    DateTime date1 = DateTime.Now;
                        //    DateTime date2 = DateTime.Now;

                        //    if (sdr.GetValue(0).ToString() != "")
                        //    {
                        //        date1 = Convert.ToDateTime(sdr.GetValue(0).ToString());
                        //        date2 = Convert.ToDateTime(sdr.GetValue(1).ToString());
                        //    }


                        //    //calculate total duration
                        //    TimeSpan ts = date2 - date1;
                        //    duration = (int)ts.TotalSeconds;

                        //}
                        //sdr.Close();

                        //new calculation for work duration
                        duration = GetWorkDuration(start, end, mo, moline);

                        //Fiza 290922 - Start - Get the details of each sequence running in a mo
                        int seq1 = 1;
                        int op_sam = 0;
                        int op_id = 0;
                        int piece_count = 0;
                        int emp_duration = 0;
                        decimal emp_eff = 0;
                        decimal tot_empEff = 0;
                        decimal op_eff = 0;
                        decimal tot_opEff = 0;
                        int emp_count = 0;

                        String query1 = "";
                        String query2 = "";
                        String query3 = "";
                        String queryEmp = "";

                        query1 = "select ds.I_SEQUENCE_NO,ds.V_OPERATION_CODE,op.V_OPERATION_DESC,op.D_SAM, op.V_ID, ds.I_OPERATION_SEQUENCE_NO from DESIGN_SEQUENCE ds, OPERATION_DB op where ds.V_ARTICLE_ID = (select V_ARTICLE_ID from MO_DETAILS where V_MO_NO = '" + mo + "' and V_MO_LINE = '" + moline + "') and ds.V_OPERATION_CODE = op.V_OPERATION_CODE and ds.I_SEQUENCE_NO IN(select distinct S.I_SEQUENCE_NO from STATION_ASSIGN S, MO_DETAILS M where S.V_MO_NO= '" + mo + "' and S.V_MO_LINE= '" + moline + "' and S.I_STATION_ID!= 0 and M.V_ASSIGN_TYPE = S.V_ASSIGN_TYPE) order by ds.I_SEQUENCE_NO";
                        SqlDataAdapter sda1 = new SqlDataAdapter(query1, dc.con);
                        DataTable dt1 = new DataTable();
                        sda1.Fill(dt1);
                        sda1.Dispose();

                        for (int f = 0; f < dt1.Rows.Count; f++)
                        {
                            tot_empEff = 0;
                            seq1 = int.Parse(dt1.Rows[f][0].ToString());
                            //DebugLog("Include MO, Track 1 - SEQNO= "+seq1);
                            op_sam = int.Parse(dt1.Rows[f][3].ToString());
                            op_id = int.Parse(dt1.Rows[f][4].ToString());
                            //DebugLog("Include MO, Track 1 - OP_ID= " + op_id);

                            //get time spend, production amount for each employee of each operations
                            query2 = "SELECT OP_ID, SEQ_NO, EMP_ID, MIN(TIME), MAX(TIME), SUM(PC_COUNT) FROM HANGER_HISTORY where OP_ID='" + op_id + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and TIME>= '" + start + "' and TIME<'" + end + "' GROUP BY OP_ID, SEQ_NO, EMP_ID";
                            //query2 = "SELECT OP_ID, SEQ_NO, STN_ID, MIN(TIME), MAX(TIME), SUM(PC_COUNT) FROM HANGER_HISTORY where OP_ID='" + op_id + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and TIME>= '" + start + "' and TIME<'" + end + "' GROUP BY OP_ID, SEQ_NO, STN_ID";
                            SqlDataAdapter sda2 = new SqlDataAdapter(query2, dc.con);
                            //DebugLog("Include MO, Track 1 - "+query2);
                            DataTable dt2 = new DataTable();
                            sda2.Fill(dt2);
                            sda2.Dispose();
                            for(int y = 0; y<dt2.Rows.Count; y++)
                            {
                                //tot_empEff = 0;
                                //DebugLog("Include MO, Track 1");
                                DateTime seq_starttime = DateTime.Now;
                                DateTime seq_endtime = DateTime.Now;
                                String empd = "";

                                    empd = dt2.Rows[y][2].ToString();
                                    //DebugLog("Include MO, Track 2 - STNID = "+empd);
                                    seq_starttime = Convert.ToDateTime(dt2.Rows[y][3].ToString());
                                    seq_endtime = Convert.ToDateTime(dt2.Rows[y][4].ToString());
                                    //DebugLog("Include MO, Track 2 - Starttime " +seq_starttime);
                                    //DebugLog("Include MO, Track 2 - Endtime " + seq_endtime);
                                    piece_count = int.Parse(dt2.Rows[y][5].ToString());
                                    //DebugLog("Include MO, Track 2 - PRODUCTION = "+piece_count);
                                
                                //get Duration for each of Sequence
                                TimeSpan ts_seq_dur = seq_endtime - seq_starttime;
                                //DebugLog("Include MO, Track 3");
                                emp_duration = (int)ts_seq_dur.TotalSeconds;
                                //DebugLog("Include MO, Track 3 - Duration = "+ emp_duration);

                                cmd = new SqlCommand("select isnull(sum(I_BREAK_TIMESPAN), 0) from SHIFT_BREAKS where T_BREAK_TIME_START between '" + seq_starttime.ToString("HH:mm:ss") + "' and '" + seq_endtime.ToString("HH:mm:ss") + "' and T_BREAK_TIME_END between '" + seq_starttime.ToString("HH:mm:ss") + "' and '" + seq_endtime.ToString("HH:mm:ss") + "'", dc.con);
                                int breaks = int.Parse(cmd.ExecuteScalar() + "") * 60;
                                emp_duration = emp_duration - breaks;

                                //get Target for each of Operator of each sequence 041022
                                int emp_target = 0;
                                emp_target = emp_duration / (int)op_sam;
                                //DebugLog("Include MO, Track 3 - EMP_Target = " + emp_duration + "/" + op_sam);
                                //DebugLog("Include MO, Track 3 - EMP_Target = " + emp_target);

                                //get the Efficiency of each Operator
                                if (emp_target > 0) {
                                    emp_eff = (decimal)piece_count / (decimal)emp_target * 100;
                                    //DebugLog("Include MO, Track 3 - EMP_Efficiency = " + piece_count + "/" + emp_target);
                                    //DebugLog("Include MO, Track 3 - EMP_Efficiency = " + emp_eff);
                                }

                                //get Efficiency for each of Operation
                                tot_empEff += emp_eff;
                                //DebugLog("Include MO, Track 3 - TOTAL EMP EFF = " + tot_empEff);
                            }

                            //get Number of Operator
                            //queryEmp = "SELECT OP_ID, SEQ_NO, COUNT(DISTINCT EMP_ID) FROM HANGER_HISTORY WHERE MO_NO = '" + mo + "' AND MO_LINE = '" + moline + "' and TIME>= '" + start + "' and TIME<'" + end + "' AND OP_ID = '" + op_id + "' GROUP BY OP_ID, SEQ_NO";
                            queryEmp = "SELECT OP_ID, SEQ_NO, COUNT(DISTINCT STN_ID) FROM HANGER_HISTORY WHERE MO_NO = '" + mo + "' AND MO_LINE = '" + moline + "' and TIME>= '" + start + "' and TIME<'" + end + "' AND OP_ID = '" + op_id + "' GROUP BY OP_ID, SEQ_NO";
                            SqlCommand empcnt = new SqlCommand(queryEmp, dc.con);
                            //DebugLog("Include MO, Track 3 - FIND NUMOFOPERATORS " + queryEmp);
                            SqlDataReader sdr5 = empcnt.ExecuteReader();
                            if (sdr5.Read())
                            {
                                emp_count = int.Parse(sdr5.GetValue(2).ToString());
                                //DebugLog("Include MO, Track 3 - Num of OPERATOR = " + emp_count);
                            }
                            if (emp_count != 0)
                            {
                                op_eff = tot_empEff / emp_count;
                            }
                            //DebugLog("Include MO, Track 3 - OPERATION EFFICIENCY = " + tot_empEff + "/" + emp_count);
                            //DebugLog("Include MO, Track 3 - OPERATION EFFICIENCY = " + op_eff);

                            tot_opEff += op_eff;
                        }

                        //get Number of Sequence
                        int seq_count = 0;
                        query3 = "select count(d.I_SEQUENCE_NO) from DESIGN_SEQUENCE d, OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO = '" + mo + "' and V_MO_LINE = '" + moline + "') and d.I_SEQUENCE_NO in(select s.I_SEQUENCE_NO from STATION_ASSIGN s where s.V_MO_NO='" + mo + "' and s.V_MO_LINE='" + moline + "' and s.I_STATION_ID!='0')";
                        SqlCommand cmd3 = new SqlCommand(query3, dc.con);
                        //DebugLog("Include MO, Track 3 - FIND NUMOFSEQUENCE " + query3);
                        SqlDataReader sdr3 = cmd3.ExecuteReader();
                        if (sdr3.Read())
                        {
                            seq_count = int.Parse(sdr3.GetValue(0).ToString());
                            //DebugLog("Include MO, Track 3 - Num of SEQUENCE = " + seq_count);
                        }

                        
                        //Fiza 290922 - End - Get the details of each sequence running in a mo

                        String temp1 = dt.Rows[j][2].ToString();
                        if (temp1 != "")
                        {
                            count = int.Parse(dt.Rows[j][2].ToString());
                        }
                        else
                        {
                            count = 0;
                        }

                        //calculate cost for normal and overtime
                        if (dt.Rows[j][3].ToString() == "0")
                        {
                            count_normal = count;
                            count = 0;
                            cost = 0;
                            totalunload_normal += count_normal;
                            cost_normal *= count_normal;
                            totalcost_normal += cost_normal;
                        }
                        else
                        {
                            cost_normal = 0;
                            count_normal = 0;
                            totalunload += count;
                            cost *= count;
                            totalcost += cost;
                        }

                        //calculate target production
                        int target = 0;
                        if (sam > 0)
                        {
                            target = duration / (int)sam;
                        }

                        //calculate efficiency
                        //decimal eff = 0;
                        if (target > 0)
                        {
                            eff = ((decimal)count_normal + (decimal)count) / (decimal)target * 100; //original
                            //eff = (decimal)tot_opEff / (decimal)seq_count; //Average EFF% - Fiza 041022
                            //DebugLog("Include MO, Track 4 - AVG EFF% " + tot_opEff + "/"+ seq_count);
                            //DebugLog("Include MO, Track 4 - AVG EFF% " +eff);
                        }

                        int flag = 0;
                        for (int i = 0; i < dtmo.Rows.Count; i++)
                        {
                            if (dtmo.Rows[i][0].ToString() == mo && dtmo.Rows[i][1].ToString() == moline)
                            {
                                //if mo already present add normal count
                                int temp = int.Parse(dtmo.Rows[i][3].ToString());
                                temp += count_normal;
                                dtmo.Rows[i][3] = temp;

                                //if mo already present add overtime count
                                int temp2 = int.Parse(dtmo.Rows[i][5].ToString());
                                temp2 += count;
                                dtmo.Rows[i][5] = temp2;

                                //if mo already present add normal cost
                                decimal temp3 = Convert.ToDecimal(dtmo.Rows[i][10].ToString());
                                temp3 += cost_normal;
                                dtmo.Rows[i][10] = temp3.ToString("0.##");

                                //if mo already present add overtime cost
                                decimal temp4 = Convert.ToDecimal(dtmo.Rows[i][11].ToString());
                                temp4 += cost;
                                dtmo.Rows[i][11] = temp4.ToString("0.##");

                                dtmo.Rows[i][2] = target;

                                if (prev_mo != cur_mo && prev_moline != cur_moline)
                                {
                                    totaltarget += target;
                                }

                                //calculate efficiency
                                if (target > 0)
                                {
                                    eff = ((decimal)temp + (decimal)temp2) / (decimal)target * 100; //original
                                    //eff = (decimal)tot_opEff / (decimal)seq_count; //Average EFF% - Fiza 290922
                                }

                                totalsam -= sam;

                                //calculate sam
                                //if (temp > 0 || temp2 > 0)
                                //{
                                //    sam = duration / (temp + temp2);

                                //    //hanafi fix target on the report
                                //    target = duration / (int)sam;
                                //    eff = ((decimal)count_normal + (decimal)count) / (decimal)target * 100;
                                //    dtmo.Rows[i][2] = target;
                                //}

                                dtmo.Rows[i][7] = eff.ToString("0.##") + "%";
                                dtmo.Rows[i][8] = duration / 60;
                                dtmo.Rows[i][9] = sam.ToString("0.##");
                                flag = 1;
                            }
                        }
                        moEff += eff; //051022

                        if (flag == 0)
                        {
                            //calculate sam
                            //if (count > 0 || count_normal > 0)
                            //{
                            //    sam = duration / (count + count_normal);

                            //}
                            //add to datatable
                            totaltarget += target;
                            decimal decDur = 0;
                            decDur = duration / 60;
                            //dtmo.Rows.Add(mo, moline, target, count_normal, "0", count, "0", eff.ToString("0.##") + "%", duration / 60, sam.ToString("0.##"), cost_normal.ToString("0.##"), cost.ToString("0.##"));
                            dtmo.Rows.Add(mo, moline, target, count_normal, "0", count, "0", eff.ToString("0.##") + "%", decDur.ToString("0.##") , sam.ToString("0.##"), cost_normal.ToString("0.##"), cost.ToString("0.##"));
                        }
                    } //For each MO

                    /*
                    //Hanafi | Date:03/08/2021 | removed due to changed in data transfer process
                    //query = "";
                    //if (cmbline.Text == "All")
                    //{
                    //    query = "SELECT MO_NO,MO_LINE,SUM(PC_COUNT),WORKTYPE FROM hangerwip where   time>='" + start + "' and time<'" + end + "' and REMARKS='2' GROUP BY MO_NO,MO_LINE,WORKTYPE ORDER BY MO_NO,MO_LINE,WORKTYPE";
                    //}
                    //else
                    //{
                    //    query = "SELECT h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT),h.WORKTYPE FROM hangerwip h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and time>='" + start + "' and time<'" + end + "' and REMARKS='2' GROUP BY MO_NO,MO_LINE,WORKTYPE ORDER BY MO_NO,MO_LINE,WORKTYPE";
                    //}


                    ////get mo wise production
                    //MySqlDataAdapter sda1 = new MySqlDataAdapter(query, dc.conn);
                    //dt = new DataTable();
                    //sda1.Fill(dt);
                    //sda1.Dispose();
                    //for (int j = 0; j < dt.Rows.Count; j++)
                    //{
                    //    prev_mo = cur_mo;
                    //    prev_moline = cur_moline;
                    //    String mo = dt.Rows[j][0].ToString();
                    //    String moline = dt.Rows[j][1].ToString();

                    //    cur_mo = mo;
                    //    cur_moline = moline;
                    //    decimal cost = 0;
                    //    decimal cost_normal = 0;
                    //    int count = 0;
                    //    int count_normal = 0;
                    //    decimal sam = 0;
                    //    int duration = 0;

                    //    if (getallop == "TRUE")
                    //    {
                    //        //get sum of sam , sum of piecerate and sum of overtime rate
                    //        cmd = new SqlCommand("select SUM(o.D_SAM),SUM(o.D_PIECERATE),SUM(o.D_OVERTIME_RATE) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "')", dc.con);
                    //        SqlDataReader sdr1 = cmd.ExecuteReader();
                    //        if (sdr1.Read())
                    //        {
                    //            if (sdr1.GetValue(0) + "" != "")
                    //            {
                    //                sam = int.Parse(sdr1.GetValue(0) + "");
                    //            }
                    //            if (sdr1.GetValue(2) + "" != "")
                    //            {
                    //                cost = Convert.ToDecimal(sdr1.GetValue(2) + "");
                    //            }
                    //            totalsam += sam;
                    //            if (sdr1.GetValue(1) + "" != "")
                    //            {
                    //                cost_normal = Convert.ToDecimal(sdr1.GetValue(1) + "");
                    //            }
                    //        }
                    //        sdr1.Close();
                    //    }
                    //    else
                    //    {
                    //        //get sum of sam , sum of piecerate and sum of overtime rate
                    //        cmd = new SqlCommand("select SUM(o.D_SAM),SUM(o.D_PIECERATE),SUM(o.D_OVERTIME_RATE) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "') and d.I_SEQUENCE_NO in(select s.I_SEQUENCE_NO from STATION_ASSIGN s where s.V_MO_NO='" + mo + "' and s.V_MO_LINE='" + moline + "' and s.I_STATION_ID!='0')", dc.con);
                    //        SqlDataReader sdr1 = cmd.ExecuteReader();
                    //        if (sdr1.Read())
                    //        {
                    //            if (sdr1.GetValue(0) + "" != "")
                    //            {
                    //                sam = int.Parse(sdr1.GetValue(0) + "");
                    //            }
                    //            if (sdr1.GetValue(2) + "" != "")
                    //            {
                    //                cost = Convert.ToDecimal(sdr1.GetValue(2) + "");
                    //            }
                    //            totalsam += sam;
                    //            if (sdr1.GetValue(1) + "" != "")
                    //            {
                    //                cost_normal = Convert.ToDecimal(sdr1.GetValue(1) + "");
                    //            }
                    //        }
                    //        sdr1.Close();
                    //    }

                    //    //get the first hanger and last hanger time
                    //    cmd = new SqlCommand("SELECT MIN(TIME), MAX(TIME) FROM HANGER_HISTORY WHERE TIME >= '" + start + "' and TIME <= '" + end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "'", dc.con);
                    //    sdr = cmd.ExecuteReader();
                    //    if (sdr.Read())
                    //    {
                    //        DateTime date1 = DateTime.Now;
                    //        DateTime date2 = DateTime.Now;

                    //        if (sdr.GetValue(0).ToString() != "")
                    //        {
                    //            date1 = Convert.ToDateTime(sdr.GetValue(0).ToString());
                    //            date2 = Convert.ToDateTime(sdr.GetValue(1).ToString());
                    //        }

                    //        //calculate total duration
                    //        TimeSpan ts = date2 - date1;
                    //        duration = (int)ts.TotalSeconds;
                    //    }
                    //    sdr.Close();

                    //    String temp1 = dt.Rows[j][2].ToString();
                    //    if (temp1 != "")
                    //    {
                    //        count = int.Parse(dt.Rows[j][2].ToString());
                    //    }
                    //    else
                    //    {
                    //        count = 0;
                    //    }

                    //    //calculate cost for normal and overtime
                    //    if (dt.Rows[j][3].ToString() == "0")
                    //    {
                    //        count_normal = count;
                    //        count = 0;
                    //        cost = 0;
                    //        totalunload_normal += count_normal;
                    //        cost_normal *= count_normal;
                    //        totalcost_normal += cost_normal;
                    //    }
                    //    else
                    //    {
                    //        cost_normal = 0;
                    //        count_normal = 0;
                    //        totalunload += count;
                    //        cost *= count;
                    //        totalcost += cost;
                    //    }

                    //    //calculate target production
                    //    int target = 0;
                    //    if (sam > 0)
                    //    {
                    //        target = duration / (int)sam;
                    //    }

                    //    //calculate efficiency
                    //    decimal eff = 0;
                    //    if (target > 0)
                    //    {
                    //        eff = ((decimal)count_normal + (decimal)count) / (decimal)target * 100;
                    //    }

                    //    int flag = 0;
                    //    for (int i = 0; i < dtmo.Rows.Count; i++)
                    //    {
                    //        if (dtmo.Rows[i][0].ToString() == mo && dtmo.Rows[i][1].ToString() == moline)
                    //        {
                    //            //if mo already present add normal count
                    //            int temp = int.Parse(dtmo.Rows[i][3].ToString());
                    //            temp += count_normal;
                    //            dtmo.Rows[i][3] = temp;

                    //            //if mo already present add overtime count
                    //            int temp2 = int.Parse(dtmo.Rows[i][5].ToString());
                    //            temp2 += count;
                    //            dtmo.Rows[i][5] = temp2;

                    //            //if mo already present add normal cost
                    //            decimal temp3 = Convert.ToDecimal(dtmo.Rows[i][10].ToString());
                    //            temp3 += cost_normal;
                    //            dtmo.Rows[i][10] = temp3.ToString("0.##");

                    //            //if mo already present add overtime cost
                    //            decimal temp4 = Convert.ToDecimal(dtmo.Rows[i][11].ToString());
                    //            temp4 += cost;
                    //            dtmo.Rows[i][11] = temp4.ToString("0.##");

                    //            dtmo.Rows[i][2] = target;

                    //            if (prev_mo != cur_mo && prev_moline != cur_moline)
                    //            {
                    //                totaltarget += target;
                    //            }

                    //            //calcualate efficiency
                    //            if (target > 0)
                    //            {
                    //                eff = ((decimal)temp + (decimal)temp2) / (decimal)target * 100;
                    //            }

                    //            totalsam -= sam;

                    //            //calculate sam
                    //            if (temp > 0 || temp2 > 0)
                    //            {
                    //                sam = duration / (temp + temp2);
                    //            }

                    //            dtmo.Rows[i][7] = eff.ToString("0.##") + "%";
                    //            dtmo.Rows[i][8] = duration / 60;
                    //            dtmo.Rows[i][9] = sam.ToString("0.##");
                    //            flag = 1;
                    //        }
                    //    }

                    //    if (flag == 0)
                    //    {
                    //        //calculate sam
                    //        if (count > 0 || count_normal > 0)
                    //        {
                    //            sam = duration / (count + count_normal);
                    //        }
                    //        //add to datatable
                    //        totaltarget += target;
                    //        dtmo.Rows.Add(mo, moline, target, count_normal, "0", count, "0", eff.ToString("0.##") + "%", duration / 60, sam.ToString("0.##"), cost_normal.ToString("0.##"), cost.ToString("0.##"));
                    //    }
                    //}
                    */

                    //get Number of MO
                    int moCnt = 0;
                    String query4 = "";
                    //query4 = "SELECT COUNT(DISTINCT MO_NO) FROM HANGER_HISTORY WHERE TIME>= '"+start+"' and TIME<'"+end+"'"; //ORI FIZA 051022
                    if (cmbline.Text == "All")
                    {
                        query4 = "SELECT COUNT(DISTINCT MO_NO) FROM HANGER_HISTORY WHERE TIME>= '" + start + "' and TIME<'" + end + "'";
                    }
                    else { 
                        query4 = "SELECT COUNT(DISTINCT h.MO_NO) FROM HANGER_HISTORY h, STATION_DATA s WHERE h.TIME>= '"+start+"' and h.TIME<'"+end+"' AND s.I_STN_ID = h.STN_ID AND s.I_INFEED_LINE_NO = '"+ cmbline.Text + "'";
                    }
                    SqlCommand cmd4 = new SqlCommand(query4, dc.con);
                    //DebugLog("Include MO, Track 3 - FIND NUMOFSEQUENCE " + query4);
                    SqlDataReader sdr4 = cmd4.ExecuteReader();
                    if (sdr4.Read())
                    {
                        moCnt = int.Parse(sdr4.GetValue(0).ToString());
                        //DebugLog("Include MO, Track 3 - Num of MONO = " + moCnt);
                    }

                    //calculate total efficiency
                    if (totaltarget > 0)
                    {
                        int inttarget = 0;
                        totaltarget = 0;
                        for (int i = 0; i < dtmo.Rows.Count; i++)
                        {
                            inttarget = int.Parse(dtmo.Rows[i][2].ToString());
                            totaltarget += inttarget;
                        }

                        totaleff = ((decimal)totalunload + (decimal)totalunload_normal) / (decimal)totaltarget * 100;
                        //totaleff = moEff / moCnt; //Fiza 051022
                        //DebugLog("Include MO, Track 3 - TOTAL EFF% = " + moEff+ "/" +moCnt);
                        //DebugLog("Include MO, Track 3 - TOTAL EFF% = " +totaleff);
                    }

                    //calculate total actual sam
                    if (totalunload_normal > 0 || totalunload > 0)
                    {
                        totalsam = work_duration / (totalunload + totalunload_normal);
                    }

                    // add to datatable
                    totalsam /= 60;
                    for (int i = 0; i < dtmo.Rows.Count; i++)
                    {
                         dtprod.Rows.Add(startdate.ToString("yyyy-MM-dd"), totaltarget, totalunload_normal, totalrepair_normal, totalunload, totalrepair, totaleff.ToString("0.##") + "%", work_duration / 60, totalsam.ToString("0.##"), totalcost_normal.ToString("0.##"), totalcost.ToString("0.##"), dtmo.Rows[i][0].ToString(), dtmo.Rows[i][1].ToString(), dtmo.Rows[i][2].ToString(), dtmo.Rows[i][3].ToString(), dtmo.Rows[i][4].ToString(), dtmo.Rows[i][5].ToString(), dtmo.Rows[i][6].ToString(), dtmo.Rows[i][7].ToString(), dtmo.Rows[i][8].ToString(), (Convert.ToDecimal(dtmo.Rows[i][9].ToString()) / 60).ToString("0.##"), dtmo.Rows[i][10].ToString(), dtmo.Rows[i][11].ToString());
                        data1.Rows.Add(startdate.ToString("yyyy-MM-dd"), totaltarget, totalunload_normal, totalrepair_normal, totalunload, totalrepair, totaleff.ToString("0.##") + "%", work_duration / 60, totalsam.ToString("0.##"), totalcost_normal.ToString("0.##"), totalcost.ToString("0.##"), dtmo.Rows[i][0].ToString(), dtmo.Rows[i][1].ToString(), dtmo.Rows[i][2].ToString(), dtmo.Rows[i][3].ToString(), dtmo.Rows[i][4].ToString(), dtmo.Rows[i][5].ToString(), dtmo.Rows[i][6].ToString(), dtmo.Rows[i][7].ToString(), dtmo.Rows[i][8].ToString(), (Convert.ToDecimal(dtmo.Rows[i][9].ToString()) / 60).ToString("0.##"), dtmo.Rows[i][10].ToString(), dtmo.Rows[i][11].ToString());
                    }

                    //add to datatable
                    String date = startdate.ToString("MMM - dd");
                    int count1 = totalunload + totalunload_normal;
                    data3.Rows.Add(date, count1);

                    //add to datatable
                    count1 = (int)totalcost + (int)totalcost_normal;
                    data4.Rows.Add(date, count1);

                    //add to datatable
                    count1 = totalrepair + totalrepair_normal;
                    data5.Rows.Add(date, count1);

                    startdate = startdate.AddDays(1);
                }

                dgvproduction.DataSource = dtprod;

                Hide_MO();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                radLabel1.Text = ex.Message;
            }
        }

        public int GetWorkDuration(String start, String end, String mo, String moline)
        {
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Now;

            String strSql1 = "SELECT MIN(TIME), MAX(TIME) FROM HANGER_HISTORY WHERE TIME >= '" + start + "' and TIME <= '" + end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "'"; //MRT-GLOBALDB
            SqlCommand cmd = new SqlCommand(strSql1, dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetValue(0).ToString() != "")
                {
                    date1 = Convert.ToDateTime(sdr.GetValue(0).ToString());
                    date2 = Convert.ToDateTime(sdr.GetValue(1).ToString());
                }
            }
            sdr.Close();

            int duration = 0;
            cmd = new SqlCommand("select isnull(sum(I_BREAK_TIMESPAN), 0) from SHIFT_BREAKS where T_BREAK_TIME_START between '" + date1.ToString("HH:mm:ss") + "' and '" + date2.ToString("HH:mm:ss") + "' and T_BREAK_TIME_END between '" + date1.ToString("HH:mm:ss") + "' and '" + date2.ToString("HH:mm:ss") + "'", dc.con);
            int breaks = int.Parse(cmd.ExecuteScalar() + "") * 60;
            duration = duration - breaks;

            SqlDataAdapter sda = new SqlDataAdapter("select MO_NO, MO_LINE, Time from hanger_history where TIME >= '" + date1 + "' and TIME <= '" + date2 + "' order by time", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            if (dt.Rows.Count > 0)
            {
                date1 = Convert.ToDateTime(dt.Rows[0][2] + "");
            }
            for (int i = 1; i < dt.Rows.Count; i++)
            {               
                if (dt.Rows[i][0].ToString() == mo && dt.Rows[i][1].ToString() == moline)
                {                    
                    date2 = Convert.ToDateTime(dt.Rows[i][2] + "");
                    TimeSpan ts = date2 - date1;
                    duration += (int)ts.TotalSeconds;
                    date1 = date2;
                }
                else 
                {
                    date1 = Convert.ToDateTime(dt.Rows[i][2] + "");
                }
            }
            return duration;
        }

        public void Hide_MO()
        {
            if (hidetotals == "TRUE")
            {
                dgvproduction.Columns[1].IsVisible = false;
                dgvproduction.Columns[2].IsVisible = false;
                dgvproduction.Columns[3].IsVisible = false;
                dgvproduction.Columns[4].IsVisible = false;
                dgvproduction.Columns[5].IsVisible = false;
                dgvproduction.Columns[6].IsVisible = false;
                dgvproduction.Columns[7].IsVisible = false;
                dgvproduction.Columns[8].IsVisible = false;
                dgvproduction.Columns[9].IsVisible = false;
                dgvproduction.Columns[10].IsVisible = false;
            }
            else
            {
                GroupDescriptor descriptor1 = new GroupDescriptor();
                descriptor1.GroupNames.Add("Date", ListSortDirection.Ascending);
                descriptor1.GroupNames.Add("Total Target", ListSortDirection.Ascending);
                descriptor1.GroupNames.Add("Total Production (Normal-Time)", ListSortDirection.Ascending);
                descriptor1.GroupNames.Add("Total Repair/Rework (Normal-Time)", ListSortDirection.Ascending);
                descriptor1.GroupNames.Add("Total Production (OverTime)", ListSortDirection.Ascending);
                descriptor1.GroupNames.Add("Total Repair/Rework (OverTime)", ListSortDirection.Ascending);
                descriptor1.GroupNames.Add("Total Efficiency", ListSortDirection.Ascending);
                descriptor1.GroupNames.Add("Total Work Duration (Min)", ListSortDirection.Ascending);
                descriptor1.GroupNames.Add("Total Average SAM (Min)", ListSortDirection.Ascending);
                descriptor1.GroupNames.Add("Total Cost (Normal-Time)", ListSortDirection.Ascending);
                descriptor1.GroupNames.Add("Total Cost (OverTime)", ListSortDirection.Ascending);
                this.dgvproduction.GroupDescriptors.Add(descriptor1);
            }
        }

        public void Include_EMP()
        {
            try
            {
                //clear all the datatable
                data2.Rows.Clear();
                data3.Rows.Clear();
                data5.Rows.Clear();

                // add columns to employee datatable
                DataTable dtprod = new DataTable();
                dtprod.Columns.Add("Date");
                dtprod.Columns.Add("Total Piece Count");
                dtprod.Columns.Add("Total Repair/Rework");
                dtprod.Columns.Add("EMP ID");
                dtprod.Columns.Add("EMP Name");
                dtprod.Columns.Add("Piece Count");
                dtprod.Columns.Add("Repair/Rework");

                DateTime startdate = Convert.ToDateTime(dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime enddate = Convert.ToDateTime(dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59");

                String shift_start = "";
                String shift_end = "";
                String overtime_end = "";

                //get shift details
                SqlCommand cmd1 = new SqlCommand("select T_SHIFT_START_TIME,T_SHIFT_END_TIME,T_OVERTIME_END_TIME from SHIFTS where V_SHIFT='" + cmbshift.Text + "'", dc.con);
                SqlDataReader sdr = cmd1.ExecuteReader();
                if (sdr.Read())
                {
                    shift_start = sdr.GetValue(0).ToString();
                    shift_end = sdr.GetValue(1).ToString();
                    overtime_end = sdr.GetValue(2).ToString();
                }
                sdr.Close();

                cmd1 = new SqlCommand("select HIDE_OVERTIME from SETUP", dc.con);
                String overtime = cmd1.ExecuteScalar() + "";
                if (overtime == "TRUE")
                {
                    shift_end = overtime_end;
                }

                //get details for employee from first date to last date
                while (startdate < enddate)
                {
                    //add columns to employee datatable
                    DataTable dtmo = new DataTable();
                    dtmo.Columns.Add("EMP ID");
                    dtmo.Columns.Add("EMP Name");
                    dtmo.Columns.Add("Total Loaded");
                    dtmo.Columns.Add("Total Repair/Rework");

                    int totalrepair = 0;
                    int totalload = 0;
                    String temp1 = "";

                    String start = startdate.ToString("yyyy-MM-dd") + " 00:00:00";
                    String end = startdate.ToString("yyyy-MM-dd") + " 23:59:59";

                    if (cmbshift.Text != "All")
                    {
                        start = startdate.ToString("yyyy-MM-dd") + " " + shift_start;
                        end = enddate.ToString("yyyy-MM-dd") + " " + shift_end;
                    }
                    //else
                    //{
                    //    if (cmbshift.Text == "3")
                    //    {
                    //        if (enddate > startdate)
                    //        {
                    //            enddate = enddate.AddDays(1);
                    //        }
                    //    }
                    //}

                    //check if date is enabled in hide date
                    SqlCommand cmd = new SqlCommand("select COUNT(*) from HIDEDAY_DB where CONVERT(nvarchar(10), '" + start + "', 120) in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                    int mocount = int.Parse(cmd.ExecuteScalar() + "");
                    if (mocount > 0)
                    {
                        startdate = startdate.AddDays(1);
                        continue;
                    }

                    String query = "";
                    if (cmbline.Text == "All")
                    {
                        query = "select V_EMP_ID,V_EMP_NAME,SUM(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' GROUP BY V_EMP_ID,V_EMP_NAME ORDER BY V_EMP_ID,V_EMP_NAME";
                    }
                    else
                    {
                        query = "select V_EMP_ID,V_EMP_NAME,SUM(I_QUANTITY) from QC_HISTORY where I_STATION_ID like '" + cmbline.Text + ".%' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' GROUP BY V_EMP_ID,V_EMP_NAME ORDER BY V_EMP_ID,V_EMP_NAME ";
                    }

                    //get employee wise repair
                    SqlDataAdapter sda = new SqlDataAdapter(query, dc.con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String empid = dt.Rows[i][0].ToString();
                        String empname = dt.Rows[i][1].ToString();
                        int count = 0;
                        String temp = dt.Rows[i][2].ToString();
                        if (temp != "")
                        {
                            count = int.Parse(dt.Rows[i][2].ToString());
                        }
                        else
                        {
                            count = 0;
                        }
                        totalrepair += count;
                        dtmo.Rows.Add(empid, empname, "0", count);
                    }

                    query = "";
                    if (cmbline.Text == "All")
                    {
                        query = "SELECT EMP_ID,SUM(PC_COUNT) FROM HANGER_HISTORY where   time>='" + start + "' and time<'" + end + "' GROUP BY EMP_ID ORDER BY EMP_ID";
                    }
                    else
                    {
                        query = "SELECT h.EMP_ID,SUM(h.PC_COUNT) FROM HANGER_HISTORY h,STATION_DATA s where s.I_STN_ID=h.STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "' and time>='" + start + "' and time<'" + end + "' GROUP BY EMP_ID ORDER BY EMP_ID";
                    }

                    //get employee wise production
                    sda = new SqlDataAdapter(query, dc.con);
                    dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        String empid = dt.Rows[j][0].ToString();
                        String empname = "";

                        //get first name of the employee
                        cmd = new SqlCommand("select V_FIRST_NAME from EMPLOYEE where V_EMP_ID='" + empid + "'", dc.con);
                        SqlDataReader sdr1 = cmd.ExecuteReader();
                        if (sdr1.Read())
                        {
                            empname = sdr1.GetValue(0).ToString();
                        }
                        sdr1.Close();

                        //get group name
                        cmd = new SqlCommand("select V_GROUP_DESC from EMPLOYEE_GROUP_CATEGORY where V_GROUP_ID='" + empid + "'", dc.con);
                        sdr1 = cmd.ExecuteReader();
                        if (sdr1.Read())
                        {
                            empname = sdr1.GetValue(0).ToString();
                        }
                        sdr1.Close();

                        int count = 0;
                        temp1 = dt.Rows[j][1].ToString();
                        if (temp1 != "")
                        {
                            count = int.Parse(dt.Rows[j][1].ToString());
                        }
                        else
                        {
                            count = 0;
                        }

                        totalload += count;
                        //add to datatable
                        int flag = 0;
                        for (int i = 0; i < dtmo.Rows.Count; i++)
                        {
                            if (dtmo.Rows[i][0].ToString() == empid)
                            {
                                int temp = int.Parse(dtmo.Rows[i][2].ToString());
                                temp += count;
                                dtmo.Rows[i][2] = temp;
                                flag = 1;
                                break;
                            }
                        }
                        if (flag == 0)
                        {
                            dtmo.Rows.Add(empid, empname, count, "0");
                        }
                    }

                    //Hanafi | Date:03 / 08 / 2021 | removed due to changed in data transfer process
                    //query = "";
                    //if (cmbline.Text == "All")
                    //{
                    //    query = "SELECT EMP_ID,SUM(PC_COUNT) FROM hangerwip where   time>='" + start + "' and time<'" + end + "' GROUP BY EMP_ID ORDER BY EMP_ID";
                    //}
                    //else
                    //{
                    //    query = "SELECT h.EMP_ID,SUM(h.PC_COUNT) FROM hangerwip h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and time>='" + start + "' and time<'" + end + "' GROUP BY EMP_ID ORDER BY EMP_ID";
                    //}


                    ////get employee wise production
                    //MySqlDataAdapter sda1 = new MySqlDataAdapter(query, dc.conn);
                    //dt = new DataTable();
                    //sda1.Fill(dt);
                    //sda1.Dispose();
                    //for (int j = 0; j < dt.Rows.Count; j++)
                    //{
                    //    String empid = dt.Rows[j][0].ToString();
                    //    String empname = "";

                    //    //get first name of the employee
                    //    cmd = new SqlCommand("select V_FIRST_NAME from EMPLOYEE where V_EMP_ID='" + empid + "'", dc.con);
                    //    empname = cmd.ExecuteScalar() + "";

                    //    //get group name
                    //    if (empname == "")
                    //    {
                    //        cmd = new SqlCommand("select V_GROUP_DESC from EMPLOYEE_GROUP_CATEGORY where V_GROUP_ID='" + empid + "'", dc.con);
                    //        empname = cmd.ExecuteScalar() + "";
                    //    }

                    //    int count = 0;
                    //    temp1 = dt.Rows[j][1].ToString();
                    //    if (temp1 != "")
                    //    {
                    //        count = int.Parse(dt.Rows[j][1].ToString());
                    //    }
                    //    else
                    //    {
                    //        count = 0;
                    //    }

                    //    totalload += count;
                    //    //add to datatable
                    //    int flag = 0;
                    //    for (int i = 0; i < dtmo.Rows.Count; i++)
                    //    {
                    //        if (dtmo.Rows[i][0].ToString() == empid)
                    //        {
                    //            int temp = int.Parse(dtmo.Rows[i][2].ToString());
                    //            temp += count;
                    //            dtmo.Rows[i][2] = temp;
                    //            flag = 1;
                    //            break;
                    //        }
                    //    }
                    //    if (flag == 0)
                    //    {
                    //        dtmo.Rows.Add(empid, empname, count, "0");
                    //    }
                    //}

                    //add to datatable
                    for (int i = 0; i < dtmo.Rows.Count; i++)
                    {
                        dtprod.Rows.Add(startdate.ToString("yyyy-MM-dd"), totalload, totalrepair, dtmo.Rows[i][0].ToString(), dtmo.Rows[i][1].ToString(), dtmo.Rows[i][2].ToString(), dtmo.Rows[i][3].ToString());
                        data2.Rows.Add(startdate.ToString("yyyy-MM-dd"), totalload, totalrepair, dtmo.Rows[i][0].ToString(), dtmo.Rows[i][1].ToString(), dtmo.Rows[i][2].ToString(), dtmo.Rows[i][3].ToString());
                    }

                    String date = startdate.ToString("MMM - dd");
                    int count1 = totalload;
                    data3.Rows.Add(date, count1);

                    count1 = totalrepair;
                    data5.Rows.Add(date, count1);
                    startdate = startdate.AddDays(1);
                }

                dgvproduction.DataSource = dtprod;
            }
            catch (Exception ex)
            {
                radLabel1.Text = ex.Message;
            }
        }

        public void Include_MO_EMP()
        {
            try
            {
                data6.Rows.Clear();
                //add columns for mo dialy production dtatable
                DataTable dtprod = new DataTable();
                dtprod.Columns.Add("Date");
                dtprod.Columns.Add("MO No");
                dtprod.Columns.Add("MO Details");
                dtprod.Columns.Add("OP CODE");
                dtprod.Columns.Add("OP DESC");
                dtprod.Columns.Add("Employee ID");
                dtprod.Columns.Add("Employee Name");
                dtprod.Columns.Add("Production");
                dtprod.Columns.Add("Repair/Rework");

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
                    user1 = sdr.GetValue(0).ToString() + " : ";
                }
                sdr.Close();

                //get special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user2 = sdr.GetValue(0).ToString() + " : ";
                }
                sdr.Close();

                //get special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user3 = sdr.GetValue(0).ToString() + " : ";
                }
                sdr.Close();

                //get special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user4 = sdr.GetValue(0).ToString() + " : ";
                }
                sdr.Close();

                //get special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user5 = sdr.GetValue(0).ToString() + " : ";
                }
                sdr.Close();

                //get special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF6' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user6 = sdr.GetValue(0).ToString() + " : ";
                }
                sdr.Close();

                //get special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF7' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user7 = sdr.GetValue(0).ToString() + " : ";
                }
                sdr.Close();

                //get special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF8' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user8 = sdr.GetValue(0).ToString() + " : ";
                }
                sdr.Close();

                //get special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF9' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user9 = sdr.GetValue(0).ToString() + " : ";
                }
                sdr.Close();

                //get special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF10' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user10 = sdr.GetValue(0).ToString() + " : ";
                }
                sdr.Close();

                DateTime startdate = Convert.ToDateTime(dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime enddate = Convert.ToDateTime(dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59");

                String shift_start = "";
                String shift_end = "";
                String overtime_end = "";

                //get shift details
                SqlCommand cmd1 = new SqlCommand("select T_SHIFT_START_TIME,T_SHIFT_END_TIME,T_OVERTIME_END_TIME from SHIFTS where V_SHIFT='" + cmbshift.Text + "'", dc.con);
                sdr = cmd1.ExecuteReader();
                if (sdr.Read())
                {
                    shift_start = sdr.GetValue(0).ToString();
                    shift_end = sdr.GetValue(1).ToString();
                    overtime_end = sdr.GetValue(2).ToString();
                }
                sdr.Close();

                cmd1 = new SqlCommand("select HIDE_OVERTIME from SETUP", dc.con);
                String overtime = cmd1.ExecuteScalar() + "";
                if (overtime == "TRUE")
                {
                    shift_end = overtime_end;
                }               

                //get details of production for first date to last date
                while (startdate < enddate)
                {
                    int mocount = 0;

                    String start = startdate.ToString("yyyy-MM-dd") + " 00:00:00";
                    String end = startdate.ToString("yyyy-MM-dd") + " 23:59:59";

                    if (cmbshift.Text != "All")
                    {
                        start = startdate.ToString("yyyy-MM-dd") + " " + shift_start;
                        end = enddate.ToString("yyyy-MM-dd") + " " + shift_end;
                    }
                    //else
                    //{
                    //    if (cmbshift.Text == "3")
                    //    {
                    //        if (enddate > startdate)
                    //        {
                    //            enddate = enddate.AddDays(1);
                    //        }
                    //    }
                    //}

                    //check if date is enabled in hide day
                    cmd = new SqlCommand("select COUNT(*) from HIDEDAY_DB where CONVERT(nvarchar(10), '" + start + "', 120) in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                    mocount = int.Parse(cmd.ExecuteScalar() + "");
                    if (mocount > 0)
                    {
                        startdate = startdate.AddDays(1);
                        continue;
                    }

                    String query = "";
                    if (cmbline.Text == "All")
                    {
                        //MRT_GLOBALDB
                        query = "SELECT MO_NO,MO_LINE,SUM(PC_COUNT),EMP_ID,SEQ_NO, OP_ID FROM HANGER_HISTORY where time>='" + start + "' and time<'" + end + "' GROUP BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID ORDER BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID";
                        //mrt_local
                        //query = "SELECT MO_NO,MO_LINE,SUM(PC_COUNT),EMP_ID,SEQ_NO, OP_ID FROM stationhistory where time>='" + start + "' and time<'" + end + "' GROUP BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID ORDER BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID";
                    }
                    else
                    {
                        //MRT_GLOBALDB
                        query = "SELECT h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT),EMP_ID,SEQ_NO, h.OP_ID FROM HANGER_HISTORY h,STATION_DATA s where s.I_STN_ID = h.STN_ID and s.I_INFEED_LINE_NO = '" + cmbline.Text + "' and time>= '" + start + "' and time<'" + end + "' GROUP BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, h.OP_ID ORDER BY MO_NO, MO_LINE, EMP_ID, SEQ_NO, h.OP_ID;";
                        //mrt_local
                        //query = "SELECT h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT),EMP_ID,SEQ_NO, h.OP_ID FROM stationhistory h,stationdata s where s.STN_ID = h.STN_ID and s.INFEED_LINENO = '" + cmbline.Text + "' and time>= '" + start + "' and time<'" + end + "' GROUP BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, h.OP_ID ORDER BY MO_NO, MO_LINE, EMP_ID, SEQ_NO, h.OP_ID; ";
                    }

                    //get mo wise production
                     SqlDataAdapter Mysda = new SqlDataAdapter(query, dc.con);  //MRT_GLOBALDB
                    //MySqlDataAdapter Mysda = new MySqlDataAdapter(query, dc.conn);  //mrt_local
                    DataTable dt = new DataTable();
                    Mysda.Fill(dt);
                    Mysda.Dispose();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        String mo = dt.Rows[j][0].ToString();
                        String moline = dt.Rows[j][1].ToString();
                        String seq = dt.Rows[j][4].ToString();
                        String empid = dt.Rows[j][3].ToString();
                        String opid = dt.Rows[j][5].ToString();

                        cmd = new SqlCommand("select V_FIRST_NAME from EMPLOYEE where V_EMP_ID='" + empid + "'", dc.con);
                        String empname = cmd.ExecuteScalar() + "";

                        if (empname == "")
                        {
                            cmd = new SqlCommand("select V_GROUP_DESC from EMPLOYEE_GROUP_CATEGORY where V_GROUP_ID='" + empid + "'", dc.con);
                            empname = cmd.ExecuteScalar() + "";
                        }

                        int count = 0;

                        String temp1 = dt.Rows[j][2].ToString();
                        if (temp1 != "")
                        {
                            count = int.Parse(dt.Rows[j][2].ToString());
                        }
                        else
                        {
                            count = 0;
                        }
                        String articledesc = "";

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
                        String color = "";
                        String article = "";
                        String size = "";

                        SqlDataAdapter sda = new SqlDataAdapter("Select V_COLOR_ID,V_ARTICLE_ID,V_SIZE_ID,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10 from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);                      
                        DataTable dt3 = new DataTable();
                        sda.Fill(dt3);
                        for (int k = 0; k < dt3.Rows.Count; k++)
                        {
                            color = dt3.Rows[k][0].ToString();
                            article = dt3.Rows[k][1].ToString();
                            size = dt3.Rows[k][2].ToString();
                            u1 = dt3.Rows[k][3].ToString();
                            u2 = dt3.Rows[k][4].ToString();
                            u3 = dt3.Rows[k][5].ToString();
                            u4 = dt3.Rows[k][6].ToString();
                            u5 = dt3.Rows[k][7].ToString();
                            u6 = dt3.Rows[k][8].ToString();
                            u7 = dt3.Rows[k][9].ToString();
                            u8 = dt3.Rows[k][10].ToString();
                            u9 = dt3.Rows[k][11].ToString();
                            u10 = dt3.Rows[k][12].ToString();
                        }
                        sda.Dispose();

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
                            articledesc = sdr.GetValue(0).ToString();
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

                        ////get sequence for the mo
                        //sda = new SqlDataAdapter("select V_OPERATION_ID FROM SEQUENCE_OPERATION WHERE V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and V_SEQUENCE_NO='" + seq + "'", dc.con);
                        //DataTable dt1 = new DataTable();
                        //sda.Fill(dt1);
                        //sda.Dispose();
                        //for (int i = 0; i < dt1.Rows.Count; i++)
                        //{
                        //    String opid = dt1.Rows[i][0].ToString();

                        //    cmd = new SqlCommand("select V_OPERATION_CODE from OPERATION_DB where V_ID='" + opid + "'", dc.con);
                        //    String opcode = cmd.ExecuteScalar() + "";

                        //    cmd = new SqlCommand("select V_OPERATION_DESC from OPERATION_DB where V_ID='" + opid + "'", dc.con);
                        //    String opdesc = cmd.ExecuteScalar() + "";

                        //    if (cmbline.Text == "All")
                        //    {
                        //        query = "SELECT SUM(I_QUANTITY) FROM QC_HISTORY where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and V_OP_CODE='" + opcode + "' and V_EMP_ID='" + empid + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "'";
                        //    }
                        //    else
                        //    {
                        //        query = "SELECT SUM(I_QUANTITY) FROM QC_HISTORY where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and V_OP_CODE='" + opcode + "' and V_EMP_ID='" + empid + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and I_STATION_ID LIKE '" + cmbline.Text + ".%'";
                        //    }

                        //    int repair = 0;
                        //    cmd = new SqlCommand(query, dc.con);
                        //    String temp = cmd.ExecuteScalar() + "";
                        //    if (temp != "")
                        //    {
                        //        repair = int.Parse(temp);
                        //    }

                        //    dtprod.Rows.Add(startdate.ToString("yyyy-MM-dd"), mo, moline, opcode, opdesc, empid, empname, count, repair);
                        //    data6.Rows.Add(mo, moline, color, articledesc, size, u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, startdate.ToString("yyyy-MM-dd"), opcode, opdesc, empid, empname, count, repair, seq,1,30,80);
                        //}

                        //Hanafi | Date:21/10/2021 | Get the V_OPERATION_CODE and V_OPERATION_DESC directly from  OPERATION_DB table
                        String opcode = "";
                        String opdesc = "";
                        String strquery = "select V_OPERATION_CODE, V_OPERATION_DESC from OPERATION_DB where V_ID='" + opid + "'";
                        SqlCommand cmdQuery = new SqlCommand(strquery, dc.con);
                        sdr = cmdQuery.ExecuteReader();
                        if (sdr.Read())
                        {
                             opcode = sdr.GetValue(0).ToString();
                             opdesc = sdr.GetValue(1).ToString();
                        }

                        if (cmbline.Text == "All")
                            {
                                query = "SELECT SUM(I_QUANTITY) FROM QC_HISTORY where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and V_OP_CODE='" + opcode + "' and V_EMP_ID='" + empid + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "'";
                            }
                            else
                            {
                                query = "SELECT SUM(I_QUANTITY) FROM QC_HISTORY where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and V_OP_CODE='" + opcode + "' and V_EMP_ID='" + empid + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and I_STATION_ID LIKE '" + cmbline.Text + ".%'";
                            }

                            int repair = 0;
                            cmd = new SqlCommand(query, dc.con);
                            String temp = cmd.ExecuteScalar() + "";
                            if (temp != "")
                            {
                                repair = int.Parse(temp);
                            }

                            dtprod.Rows.Add(startdate.ToString("yyyy-MM-dd"), mo, moline, opcode, opdesc, empid, empname, count, repair);
                            data6.Rows.Add(mo, moline, color, articledesc, size, u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, startdate.ToString("yyyy-MM-dd"), opcode, opdesc, empid, empname, count, repair, seq, 1, 30, 80);
                        


                    }

                    //Hanafi | Date:03 / 08 / 2021 | removed due to changed in data transfer process
                    //query = "";
                    //if (cmbline.Text == "All")
                    //{
                    //    query = "SELECT MO_NO,MO_LINE,SUM(PC_COUNT),EMP_ID,SEQ_NO FROM hangerwip where   time>='" + start + "' and time<'" + end + "' GROUP BY MO_NO,MO_LINE,EMP_ID,SEQ_NO ORDER BY MO_NO,MO_LINE,EMP_ID,SEQ_NO";
                    //}
                    //else
                    //{
                    //    query = "SELECT h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT),EMP_ID,SEQ_NO FROM hangerwip h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and TIME>='" + start + "' and TIME<'" + end + "' GROUP BY MO_NO,MO_LINE,EMP_ID,SEQ_NO ORDER BY MO_NO,MO_LINE,EMP_ID,SEQ_NO";
                    //}

                    ////get mo wise production
                    //MySqlDataAdapter sda1 = new MySqlDataAdapter(query, dc.conn);
                    //dt = new DataTable();
                    //sda1.Fill(dt);
                    //sda1.Dispose();
                    //for (int j = 0; j < dt.Rows.Count; j++)
                    //{
                    //    String mo = dt.Rows[j][0].ToString();
                    //    String moline = dt.Rows[j][1].ToString();
                    //    String seq = dt.Rows[j][4].ToString();
                    //    String empid = dt.Rows[j][3].ToString();

                    //    cmd = new SqlCommand("select V_FIRST_NAME from EMPLOYEE where V_EMP_ID='" + empid + "'", dc.con);
                    //    String empname = cmd.ExecuteScalar() + "";

                    //    if (empname == "")
                    //    {
                    //        cmd = new SqlCommand("select V_GROUP_DESC from EMPLOYEE_GROUP_CATEGORY where V_GROUP_ID='" + empid + "'", dc.con);
                    //        empname = cmd.ExecuteScalar() + "";
                    //    }

                    //    int count = 0;

                    //    String temp1 = dt.Rows[j][2].ToString();
                    //    if (temp1 != "")
                    //    {
                    //        count = int.Parse(dt.Rows[j][2].ToString());
                    //    }
                    //    else
                    //    {
                    //        count = 0;
                    //    }

                    //    String u1 = "";
                    //    String u2 = "";
                    //    String u3 = "";
                    //    String u4 = "";
                    //    String u5 = "";
                    //    String u6 = "";
                    //    String u7 = "";
                    //    String u8 = "";
                    //    String u9 = "";
                    //    String u10 = "";
                    //    String color = "";
                    //    String article = "";
                    //    String articledesc = "";
                    //    String size = "";

                    //    sda = new SqlDataAdapter("Select V_COLOR_ID,V_ARTICLE_ID,V_SIZE_ID,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10 from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                    //    DataTable dt3 = new DataTable();
                    //    sda.Fill(dt3);
                    //    for (int k = 0; k < dt3.Rows.Count; k++)
                    //    {
                    //        color = dt3.Rows[k][0].ToString();
                    //        article = dt3.Rows[k][1].ToString();
                    //        size = dt3.Rows[k][2].ToString();
                    //        u1 = dt3.Rows[k][3].ToString();
                    //        u2 = dt3.Rows[k][4].ToString();
                    //        u3 = dt3.Rows[k][5].ToString();
                    //        u4 = dt3.Rows[k][6].ToString();
                    //        u5 = dt3.Rows[k][7].ToString();
                    //        u6 = dt3.Rows[k][8].ToString();
                    //        u7 = dt3.Rows[k][9].ToString();
                    //        u8 = dt3.Rows[k][10].ToString();
                    //        u9 = dt3.Rows[k][11].ToString();
                    //        u10 = dt3.Rows[k][12].ToString();
                    //    }
                    //    sda.Dispose();

                    //    //get desc
                    //    cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                    //    sdr = cmd.ExecuteReader();
                    //    if (sdr.Read())
                    //    {
                    //        color = sdr.GetValue(0).ToString();
                    //    }
                    //    sdr.Close();

                    //    cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + article + "'", dc.con);
                    //    sdr = cmd.ExecuteReader();
                    //    if (sdr.Read())
                    //    {
                    //        articledesc = sdr.GetValue(0).ToString();
                    //    }
                    //    sdr.Close();

                    //    cmd = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                    //    sdr = cmd.ExecuteReader();
                    //    if (sdr.Read())
                    //    {
                    //        size = sdr.GetValue(0).ToString();
                    //    }
                    //    sdr.Close();

                    //    cmd = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + u1 + "'", dc.con);
                    //    sdr = cmd.ExecuteReader();
                    //    if (sdr.Read())
                    //    {
                    //        u1 = sdr.GetValue(0).ToString();
                    //    }
                    //    sdr.Close();

                    //    cmd = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + u2 + "'", dc.con);
                    //    sdr = cmd.ExecuteReader();
                    //    if (sdr.Read())
                    //    {
                    //        u2 = sdr.GetValue(0).ToString();
                    //    }
                    //    sdr.Close();

                    //    cmd = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + u3 + "'", dc.con);
                    //    sdr = cmd.ExecuteReader();
                    //    if (sdr.Read())
                    //    {
                    //        u3 = sdr.GetValue(0).ToString();
                    //    }
                    //    sdr.Close();

                    //    cmd = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + u4 + "'", dc.con);
                    //    sdr = cmd.ExecuteReader();
                    //    if (sdr.Read())
                    //    {
                    //        u4 = sdr.GetValue(0).ToString();
                    //    }
                    //    sdr.Close();

                    //    cmd = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + u5 + "'", dc.con);
                    //    sdr = cmd.ExecuteReader();
                    //    if (sdr.Read())
                    //    {
                    //        u5 = sdr.GetValue(0).ToString();
                    //    }
                    //    sdr.Close();

                    //    cmd = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + u6 + "'", dc.con);
                    //    sdr = cmd.ExecuteReader();
                    //    if (sdr.Read())
                    //    {
                    //        u6 = sdr.GetValue(0).ToString();
                    //    }
                    //    sdr.Close();

                    //    cmd = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + u7 + "'", dc.con);
                    //    sdr = cmd.ExecuteReader();
                    //    if (sdr.Read())
                    //    {
                    //        u7 = sdr.GetValue(0).ToString();
                    //    }
                    //    sdr.Close();

                    //    cmd = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + u8 + "'", dc.con);
                    //    sdr = cmd.ExecuteReader();
                    //    if (sdr.Read())
                    //    {
                    //        u8 = sdr.GetValue(0).ToString();
                    //    }
                    //    sdr.Close();

                    //    cmd = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + u9 + "'", dc.con);
                    //    sdr = cmd.ExecuteReader();
                    //    if (sdr.Read())
                    //    {
                    //        u9 = sdr.GetValue(0).ToString();
                    //    }
                    //    sdr.Close();

                    //    cmd = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + u10 + "'", dc.con);
                    //    sdr = cmd.ExecuteReader();
                    //    if (sdr.Read())
                    //    {
                    //        u10 = sdr.GetValue(0).ToString();
                    //    }
                    //    sdr.Close();

                    //    //get sequence for the mo
                    //    sda = new SqlDataAdapter("select V_OPERATION_ID FROM SEQUENCE_OPERATION WHERE V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and V_SEQUENCE_NO='" + seq + "'", dc.con);
                    //    DataTable dt1 = new DataTable();
                    //    sda.Fill(dt1);
                    //    sda.Dispose();
                    //    for (int i = 0; i < dt1.Rows.Count; i++)
                    //    {
                    //        String opid = dt1.Rows[i][0].ToString();

                    //        cmd = new SqlCommand("select V_OPERATION_CODE from OPERATION_DB where V_ID='" + opid + "'", dc.con);
                    //        String opcode = cmd.ExecuteScalar() + "";

                    //        cmd = new SqlCommand("select V_OPERATION_DESC from OPERATION_DB where V_ID='" + opid + "'", dc.con);
                    //        String opdesc = cmd.ExecuteScalar() + "";

                    //        if (cmbline.Text == "All")
                    //        {
                    //            query = "SELECT SUM(I_QUANTITY) FROM QC_HISTORY where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and V_OP_CODE='" + opcode + "' and V_EMP_ID='" + empid + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "'";
                    //        }
                    //        else
                    //        {
                    //            query = "SELECT SUM(I_QUANTITY) FROM QC_HISTORY where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and V_OP_CODE='" + opcode + "' and V_EMP_ID='" + empid + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and I_STATION_ID LIKE '" + cmbline.Text + ".%'";
                    //        }

                    //        int repair = 0;
                    //        cmd = new SqlCommand(query, dc.con);
                    //        String temp = cmd.ExecuteScalar() + "";
                    //        if (temp != "")
                    //        {
                    //            repair = int.Parse(temp);
                    //        }

                    //        int flag = 0;
                    //        for (int k = 0; k < dtprod.Rows.Count; k++)
                    //        {
                    //            if (dtprod.Rows[k][1].ToString() == mo && dtprod.Rows[k][2].ToString() == moline && dtprod.Rows[k][3].ToString() == opcode && dtprod.Rows[k][5].ToString() == empid && dtprod.Rows[k][0].ToString() == startdate.ToString("yyyy-MM-dd"))
                    //            {
                    //                //if mo already present add overtime count
                    //                int temp2 = int.Parse(dtprod.Rows[k][7].ToString());
                    //                temp2 += count;
                    //                dtprod.Rows[k][7] = temp2;
                    //                data6.Rows[k]["production"] = temp2;

                    //                temp2 = 0;
                    //                temp2 = int.Parse(dtprod.Rows[k][8].ToString());
                    //                temp2 += repair;
                    //                dtprod.Rows[k][8] = temp2;
                    //                data6.Rows[k]["repair"] = temp2;
                    //                flag = 1;
                    //            }
                    //        }

                    //        if (flag == 0)
                    //        {
                    //            dtprod.Rows.Add(startdate.ToString("yyyy-MM-dd"), mo, moline, opcode, opdesc, empid, empname, count, repair);
                    //            data6.Rows.Add(mo, moline, color, articledesc, size, u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, startdate.ToString("yyyy-MM-dd"), opcode, opdesc, empid, empname, count, repair, seq, 1, 30, 80);
                    //        }
                    //    }
                    //}
                    startdate = startdate.AddDays(1);
                }

                dgvproduction.DataSource = dtprod;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                radLabel1.Text = ex.Message;
            }
        }

        public void Hourly_MO_EMP()
        {
            try
            {
                data8.Rows.Clear();
                //add columns for mo dialy production dtatable
                DataTable dtprod = new DataTable();
                dtprod.Columns.Add("Date");
                dtprod.Columns.Add("Hour");
                dtprod.Columns.Add("OP CODE");
                dtprod.Columns.Add("OP DESC");
                dtprod.Columns.Add("Employee ID");
                dtprod.Columns.Add("Employee Name");
                dtprod.Columns.Add("Allocated SAM");
                dtprod.Columns.Add("Earned Minutes");
                dtprod.Columns.Add("Production");
                dtprod.Columns.Add("Duration");
                dtprod.Columns.Add("Efficiency");
                dtprod.Columns.Add("Repair/Rework");
                dtprod.Columns.Add("Defect Rate");

                DateTime startdate = Convert.ToDateTime(dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime enddate = Convert.ToDateTime(dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59");

                String shift_start = "";
                String shift_end = "";
                String overtime_end = "";

                //get shift details
                SqlCommand cmd1 = new SqlCommand("select T_SHIFT_START_TIME,T_SHIFT_END_TIME,T_OVERTIME_END_TIME from SHIFTS where V_SHIFT='" + cmbshift.Text + "'", dc.con);
                SqlDataReader sdr = cmd1.ExecuteReader();
                if (sdr.Read())
                {
                    shift_start = sdr.GetValue(0).ToString();
                    shift_end = sdr.GetValue(1).ToString();
                    overtime_end = sdr.GetValue(2).ToString();
                }
                sdr.Close();
                DebugLog("Hourly_MO_EMP(), Track 1");
                cmd1 = new SqlCommand("select HIDE_OVERTIME from SETUP", dc.con);
                String hide_overtime = cmd1.ExecuteScalar() + "";
                if (hide_overtime == "TRUE")
                {
                    shift_end = overtime_end;
                }

                //get details of production for first date to last date
                while (startdate < enddate)
                {
                    int mocount = 0;

                    String start = startdate.ToString("yyyy-MM-dd") + " 00:00:00";
                    String end = startdate.ToString("yyyy-MM-dd") + " 23:59:59";

                    if (cmbshift.Text != "All")
                    {
                        start = startdate.ToString("yyyy-MM-dd") + " " + shift_start;
                        end = enddate.ToString("yyyy-MM-dd") + " " + shift_end;
                    }
                    //else
                    //{
                    //    if (cmbshift.Text == "3")
                    //    {
                    //        if (enddate > startdate)
                    //        {
                    //            enddate = enddate.AddDays(1);
                    //        }
                    //    }
                    //}

                    //check if date is enabled in hide day
                    SqlCommand cmd = new SqlCommand("select COUNT(*) from HIDEDAY_DB where CONVERT(nvarchar(10), '" + start + "', 120) in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                    mocount = int.Parse(cmd.ExecuteScalar() + "");
                    if (mocount > 0)
                    {
                        startdate = startdate.AddDays(1);
                        continue;
                    }
                    DebugLog("Hourly_MO_EMP(), Track 2");

                    String query = "";
                    if (cmbline.Text == "All")
                    {
                        //mrt_local
                        //query = "SELECT MO_NO,MO_LINE,SUM(PC_COUNT),EMP_ID,SEQ_NO,HOUR(TIME), OP_ID FROM stationhistory where   time>='" + start + "' and time<='" + end + "' GROUP BY MO_NO,MO_LINE,EMP_ID,SEQ_NO,HOUR(TIME), OP_ID ORDER BY HOUR(TIME),MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID";

                        //MRT_GLOBALDB
                        query = "SELECT MO_NO,MO_LINE,SUM(PC_COUNT),EMP_ID,SEQ_NO, DATEPART(HOUR, TIME), OP_ID FROM HANGER_HISTORY where   time>='" + start + "' and time<='" + end + "' GROUP BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, DATEPART(HOUR, TIME), OP_ID ORDER BY DATEPART(HOUR, TIME),MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID";
                    }
                    else
                    {
                        //mrt_local
                        //query = "SELECT h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT),EMP_ID,SEQ_NO,HOUR(TIME), h.OP_ID FROM stationhistory h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and time>='" + start + "' and time<='" + end + "' GROUP BY MO_NO,MO_LINE,EMP_ID,SEQ_NO,HOUR(TIME), OP_ID ORDER BY HOUR(TIME),MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID";

                        //MRT_GLOBALDB
                        query = "SELECT h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT),EMP_ID,SEQ_NO,DATEPART(HOUR, h.TIME), h.OP_ID FROM HANGER_HISTORY h,STATION_DATA s where s.I_STN_ID=h.STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "' and time>='" + start + "' and time<='" + end + "' GROUP BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, DATEPART(HOUR, h.TIME), OP_ID ORDER BY DATEPART(HOUR, h.TIME),MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID";
                    }

                    //get mo wise production
                    //MySqlDataAdapter sda1 = new MySqlDataAdapter(query, dc.conn); //mrt_local
                    SqlDataAdapter sda1 = new SqlDataAdapter(query, dc.con);  //MRT_GLOBALDB
                    DataTable dt = new DataTable();
                    sda1.Fill(dt);
                    sda1.Dispose();

                    DebugLog("Hourly_MO_EMP(), Track 3");
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        String mo = dt.Rows[j][0].ToString();
                        String moline = dt.Rows[j][1].ToString();
                        String seq = dt.Rows[j][4].ToString();
                        String empid = dt.Rows[j][3].ToString();
                        String hour = dt.Rows[j][5].ToString();
                        String opid = dt.Rows[j][6].ToString();
                        String total_prod = "";
                        String empname = "";
                        String temp1 = "";
                        decimal hourlysam = 0;
                        int count = 0;

                        if (empid == "0")
                        {
                            continue;
                        }

                        cmd = new SqlCommand("select V_FIRST_NAME from EMPLOYEE where V_EMP_ID='" + empid + "'", dc.con);
                        empname = cmd.ExecuteScalar() + "";

                        if (empname == "")
                        {
                            cmd = new SqlCommand("select V_GROUP_DESC from EMPLOYEE_GROUP_CATEGORY where V_GROUP_ID='" + empid + "'", dc.con);
                            empname = cmd.ExecuteScalar() + "";
                        }

                        temp1 = dt.Rows[j][2].ToString();
                        if (temp1 != "")
                        {
                            count = int.Parse(temp1);
                        }

                        if (cmbline.Text == "All")
                        {
                            query = "SELECT SUM(PC_COUNT) FROM stationhistory where   time>='" + start + "' and time<='" + end + "' and HOUR(TIME)='" + hour + "'";
                        }
                        else
                        {
                            query = "SELECT SUM(h.PC_COUNT) FROM stationhistory h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and time>='" + start + "' and time<='" + end + "' and HOUR(TIME)='" + hour + "'";
                        }

                        MySqlCommand cmd2 = new MySqlCommand(query, dc.conn);
                        total_prod = cmd2.ExecuteScalar() + "";

                        //if (cmbline.Text == "All")
                        //{
                        //    query = "SELECT distinct OP_ID,SAM FROM stationhistory s,sequenceoperations o where TIME>='" + start + "' AND TIME<='" + end + "' and HOUR(TIME)='" + hour + "' AND s.MO_NO=o.MO_NO AND s.MO_LINE=o.MO_LINE AND s.SEQ_NO=o.SEQ_NO";
                        //}
                        //else
                        //{
                        //    query = "SELECT distinct OP_ID,SAM FROM stationhistory s,sequenceoperations o,stationdata d where TIME>='" + start + "' AND TIME<='" + end + "' and HOUR(TIME)='" + hour + "' AND s.MO_NO=o.MO_NO AND s.MO_LINE=o.MO_LINE AND s.SEQ_NO=o.SEQ_NO AND d.STN_ID=s.STN_ID AND d.INFEED_LINENO='" + cmbline.Text + "'";
                        //}

                        //sda1 = new MySqlDataAdapter(query, dc.conn);
                        //DataTable dt3 = new DataTable();
                        //sda1.Fill(dt3);
                        //sda1.Dispose();
                        //for (int p = 0; p < dt3.Rows.Count; p++)
                        //{
                        //    hourlysam += Convert.ToDecimal(dt3.Rows[p][1].ToString());
                        //}

                        ////get sequence for the mo
                        //SqlDataAdapter sda = new SqlDataAdapter("select V_OPERATION_ID,D_SAM FROM SEQUENCE_OPERATION WHERE V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and V_SEQUENCE_NO='" + seq + "'", dc.con);
                        //DataTable dt1 = new DataTable();
                        //sda.Fill(dt1);
                        //sda.Dispose();

                        //for (int p = 0; p < dt1.Rows.Count; p++)
                        //{
                        //String opid = dt1.Rows[p][0].ToString();

                        //Hanafi | Date:21/10/2021 | Get the V_OPERATION_CODE and V_OPERATION_DESC directly from  OPERATION_DB table
                        String opcode = "";
                        String opdesc = "";
                        int sam = 0;
                        String strquery = "select V_OPERATION_CODE, V_OPERATION_DESC, D_SAM from OPERATION_DB where V_ID='" + opid + "'";
                        SqlCommand cmdQuery = new SqlCommand(strquery, dc.con);
                        sdr = cmdQuery.ExecuteReader();
                        if (sdr.Read())
                        {
                            opcode = sdr.GetValue(0).ToString();
                            opdesc = sdr.GetValue(1).ToString();
                            sam = int.Parse(sdr.GetValue(2).ToString());
                        }


                        //int sam = int.Parse(dt1.Rows[p][1].ToString());

                        //cmd = new SqlCommand("select V_OPERATION_CODE from OPERATION_DB where V_ID='" + opid + "'", dc.con);
                        //String opcode = cmd.ExecuteScalar() + "";

                        //cmd = new SqlCommand("select V_OPERATION_DESC from OPERATION_DB where V_ID='" + opid + "'", dc.con);
                        //String opdesc = cmd.ExecuteScalar() + "";

                        decimal efficiency = 0;
                        decimal actual_sam = 0;
                        int duration = 3600;

                        //if (cmbline.Text == "All")
                        //{
                        //    query = "SELECT MIN(TIME),MAX(TIME) FROM stationhistory where EMP_ID='" + empid + "' and TIME>='" + start + "' and time<'" + end + "' and HOUR(TIME)='" + hour + "'";
                        //}
                        //else
                        //{
                        //    query = "SELECT MIN(h.TIME),MAX(h.TIME) FROM stationhistory h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and h.EMP_ID='" + empid + "' and h.TIME>='" + start + " 00:00:00' and time<'" + end + "' and HOUR(h.TIME)='" + hour + "'";
                        //}


                        //MySqlDataAdapter sda2 = new MySqlDataAdapter(query, dc.conn);
                        //DataTable dt2 = new DataTable();
                        //sda2.Fill(dt2);
                        //sda2.Dispose();
                        //if (dt2.Rows.Count > 0)
                        //{
                        //    DateTime op_starttime = DateTime.Now;
                        //    DateTime op_endtime = DateTime.Now;

                        //    if (dt2.Rows[0][0].ToString() != "")
                        //    {
                        //        op_starttime = Convert.ToDateTime(dt2.Rows[0][0].ToString());
                        //    }

                        //    if (dt2.Rows[0][1].ToString() != "")
                        //    {
                        //        op_endtime = Convert.ToDateTime(dt2.Rows[0][1].ToString());
                        //    }

                        //calculate duration
                        //TimeSpan ts = new TimeSpan();
                        //ts = op_endtime - op_starttime;
                        //duration = (int)ts.TotalSeconds;
                        //}
                        //claculate actual sam
                        //if (count > 0)
                        //{
                        //    actual_sam = (decimal)duration / (decimal)count;
                        //}

                        //if (actual_sam > 0)
                        //{
                        //    efficiency = ((decimal)sam / (decimal)actual_sam) * 100;
                        //}

                        decimal earnedmin = sam * count;
                        efficiency = ((decimal)earnedmin / (decimal)duration) * 100;
                        efficiency = Math.Round(efficiency, 2);

                        if (cmbline.Text == "All")
                        {
                            query = "SELECT SUM(I_QUANTITY) FROM QC_HISTORY where V_OP_CODE='" + opcode + "' and V_EMP_ID='" + empid + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<='" + end + "' and DATEPART(hour,D_DATE_TIME)='" + hour + "'";
                        }
                        else
                        {
                            query = "SELECT SUM(I_QUANTITY) FROM QC_HISTORY where V_OP_CODE='" + opcode + "' and V_EMP_ID='" + empid + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<='" + end + "' and I_STATION_ID LIKE '" + cmbline.Text + ".%' and DATEPART(hour,D_DATE_TIME)='" + hour + "'";
                        }

                        int repair = 0;
                        cmd = new SqlCommand(query, dc.con);
                        String temp = cmd.ExecuteScalar() + "";

                        DebugLog("Hourly_MO_EMP(), Track 4");

                        if (temp != "")
                        {
                            repair = int.Parse(temp);
                        }

                        if (hour.Length == 1)
                        {
                            hour = "0" + hour;
                        }

                        decimal repair_eff = 0;
                        if (count > 0)
                        {
                            repair_eff = (decimal)repair / (decimal)count * 100;
                        }

                        int flag = 0;
                        for (int k = 0; k < dtprod.Rows.Count; k++)
                        {
                            if (dtprod.Rows[k][2].ToString() == opcode && dtprod.Rows[k][4].ToString() == empid && dtprod.Rows[k][0].ToString() == startdate.ToString("yyyy-MM-dd") && hour + ":00:00" == dtprod.Rows[k][1].ToString())
                            {
                                //if mo already present add overtime count
                                int temp2 = int.Parse(dtprod.Rows[k]["Production"].ToString());
                                count += temp2;
                                dtprod.Rows[k]["Production"] = count;
                                data8.Rows[k]["production"] = count;
                                flag = 1;

                                if (count > 0)
                                {
                                    repair_eff = (decimal)repair / (decimal)count * 100;
                                }
                                data8.Rows[k]["defect_rate"] = repair_eff;
                                dtprod.Rows[k]["Defect Rate"] = repair_eff + "%";

                                earnedmin = sam * count;
                                efficiency = ((decimal)earnedmin / (decimal)duration) * 100;
                                efficiency = Math.Round(efficiency, 2);
                                earnedmin /= 60;

                                data8.Rows[k]["actual_sam"] = earnedmin.ToString("0");
                                dtprod.Rows[k]["Earned Minutes"] = earnedmin.ToString("0");

                                data8.Rows[k]["efficiency"] = String.Format("{0:0.00}", efficiency); //efficiency.ToString("0");
                                dtprod.Rows[k]["Efficiency"] = String.Format("{0:0.00}", efficiency) + " %";  //efficiency.ToString("0") + "%";
                                break;
                            }
                        }
                        if (flag == 0)
                        {
                            if (hour.Length == 1)
                            {
                                hour = "0" + hour;
                            }

                            decimal sam1 = (decimal)sam / (decimal)60;
                            actual_sam /= 60;
                            earnedmin /= 60;
                            decimal duration1 = (decimal)duration / (decimal)60;

                            hourlysam /= 60;
                            DebugLog("Hourly_MO_EMP(), Track 5");
                            dtprod.Rows.Add(startdate.ToString("yyyy-MM-dd"), hour + ":00:00", opcode, opdesc, empid, empname, sam1.ToString("0.##"), earnedmin.ToString("0"), count, duration1.ToString("0"), String.Format("{0:0.00}", efficiency) + " %", repair, repair_eff.ToString("0") + "%");
                            data8.Rows.Add(startdate.ToString("yyyy-MM-dd"), opcode, opdesc, empid, empname, count, repair, seq, hour + ":00:00", sam1.ToString("0.##"), earnedmin.ToString("0"), String.Format("{0:0.00}", efficiency), repair_eff.ToString("0"), total_prod, duration1.ToString("0"), hourlysam.ToString("0"));
                            DebugLog("Hourly_MO_EMP(), Track 6");
                        }
                        // }
                    }
                    startdate = startdate.AddDays(1);
                }
                DebugLog("Hourly_MO_EMP(), Track 7");
                dgvproduction.DataSource = dtprod;
                DebugLog("Hourly_MO_EMP(), Track 8");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                radLabel1.Text = ex.Message;
            }
        }

        public void Cumulative_Hourly_MO_EMP()
        {
            try
            {
                data8.Rows.Clear();
                //add columns for mo dialy production dtatable
                DataTable dtprod = new DataTable();
                dtprod.Columns.Add("Date");
                dtprod.Columns.Add("Hour");
                dtprod.Columns.Add("OP CODE");
                dtprod.Columns.Add("OP DESC");
                dtprod.Columns.Add("Employee ID");
                dtprod.Columns.Add("Employee Name");
                dtprod.Columns.Add("Allocated SAM");
                dtprod.Columns.Add("Earned Minutes");
                dtprod.Columns.Add("Production");
                dtprod.Columns.Add("Duration");
                dtprod.Columns.Add("Efficiency");
                dtprod.Columns.Add("Repair/Rework");
                dtprod.Columns.Add("Defect Rate");

                DateTime startdate = Convert.ToDateTime(dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime enddate = Convert.ToDateTime(dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59");

                String shift_start = "";
                String shift_end = "";
                String overtime_end = "";

                //get shift details
                SqlCommand cmd1 = new SqlCommand("select T_SHIFT_START_TIME,T_SHIFT_END_TIME,T_OVERTIME_END_TIME from SHIFTS where V_SHIFT='" + cmbshift.Text + "'", dc.con);
                SqlDataReader sdr = cmd1.ExecuteReader();
                if (sdr.Read())
                {
                    shift_start = sdr.GetValue(0).ToString();
                    shift_end = sdr.GetValue(1).ToString();
                    overtime_end = sdr.GetValue(2).ToString();
                }
                sdr.Close();

                cmd1 = new SqlCommand("select HIDE_OVERTIME from SETUP", dc.con);
                String hide_overtime = cmd1.ExecuteScalar() + "";
                if (hide_overtime == "TRUE")
                {
                    shift_end = overtime_end;
                }

                //get details of production for first date to last date
                while (startdate < enddate)
                {
                    int mocount = 0;

                    String start = startdate.ToString("yyyy-MM-dd");
                    String end = startdate.ToString("yyyy-MM-dd");

                    if (cmbshift.Text != "All")
                    {
                        start = startdate.ToString("yyyy-MM-dd") + " " + shift_start;
                        end = enddate.ToString("yyyy-MM-dd") + " " + shift_end;
                    }
                    //else
                    //{
                    //    if (cmbshift.Text == "3")
                    //    {
                    //        if (enddate > startdate)
                    //        {
                    //            enddate = enddate.AddDays(1);
                    //        }
                    //    }
                    //}

                    //check if date is enabled in hide day
                    SqlCommand cmd = new SqlCommand("select COUNT(*) from HIDEDAY_DB where CONVERT(nvarchar(10), '" + start + "', 120) in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                    mocount = int.Parse(cmd.ExecuteScalar() + "");
                    if (mocount > 0)
                    {
                        startdate = startdate.AddDays(1);
                        continue;
                    }

                    String query = "";
                    if (cmbline.Text == "All")
                    {
                        //mrt_local
                        query = "SELECT distinct HOUR(TIME) FROM stationhistory where time>='" + start + " 00:00:00' and time<='" + end + " 23:59:59' ORDER BY HOUR(TIME)";

                        //MRT_GLOBALDB
                        query = "SELECT distinct DATEPART(HOUR, TIME) FROM HANGER_HISTORY where time>='" + start + " 00:00:00' and time<='" + end + " 23:59:59' ORDER BY DATEPART(HOUR, TIME)";
                    }
                    else
                    {
                        //mrt_local
                        query = "SELECT distinct HOUR(h.TIME) FROM stationhistory h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and time>='" + start + " 00:00:00' and time<='" + end + " 23:59:59' ORDER BY HOUR(h.TIME)";

                        //MRT_GLOBALDB
                        query = "SELECT distinct DATEPART(HOUR, h.TIME) FROM HANGER_HISTORY h,STATION_DATA s where s.I_STN_ID=h.STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "' and time>='" + start + " 00:00:00' and time<='" + end + " 23:59:59' ORDER BY DATEPART(HOUR, h.TIME)";

                    }

                    //MySqlDataAdapter sda1 = new MySqlDataAdapter(query, dc.conn);  //mrt_local
                    SqlDataAdapter sda1 = new SqlDataAdapter(query, dc.con);  //MRT_GLOBALDB
                    DataTable dt4 = new DataTable();
                    sda1.Fill(dt4);
                    sda1.Dispose();
                    for (int m = 0; m < dt4.Rows.Count; m++)
                    {
                        String start1 = dt4.Rows[0][0].ToString();
                        String end1 = dt4.Rows[m][0].ToString();
                        if (start1.Length == 1)
                        {
                            start1 = "0" + start1;
                        }
                        if (end1.Length == 1)
                        {
                            end1 = "0" + end1;
                        }

                        start = startdate.ToString("yyyy-MM-dd") + " " + start1 + ":00:00";
                        end = startdate.ToString("yyyy-MM-dd") + " " + end1 + ":59:59";

                        int breaktime = 0;
                        cmd = new SqlCommand("select SUM(s.I_BREAK_TIMESPAN) from SHIFT_BREAKS s where DATEPART(hour,s.T_BREAK_TIME_START)<='" + end1 + "' and s.V_SHIFT IN(SELECT T.V_SHIFT FROM SHIFTS T WHERE CAST(GETDATE() AS TIME) BETWEEN cast(T.T_SHIFT_START_TIME as TIME) AND cast(T.T_OVERTIME_END_TIME as TIME))", dc.con);
                        String temp1 = cmd.ExecuteScalar() + "";
                        if (temp1 != "")
                        {
                            breaktime = int.Parse(temp1);
                        }

                        //get cumulative total prod
                        String total_prod = "";
                        String hour = dt4.Rows[m][0].ToString();
                        if (cmbline.Text == "All")
                        {
                            //query = "SELECT SUM(PC_COUNT) FROM stationhistory where   time>='" + start + "' and time<'" + end + "' and HOUR(TIME)='" + hour + "'";
                            query = "SELECT SUM(PC_COUNT) FROM stationhistory where time>='" + start + "' and time<='" + end + "'";
                        }
                        else
                        {
                            //query = "SELECT SUM(h.PC_COUNT) FROM stationhistory h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and time>='" + start + "' and time<'" + end + "' and HOUR(TIME)='" + hour + "'";
                            query = "SELECT SUM(h.PC_COUNT) FROM stationhistory h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and time>='" + start + "' and time<='" + end + "'";
                        }
                        MySqlCommand cmd2 = new MySqlCommand(query, dc.conn);
                        total_prod = cmd2.ExecuteScalar() + "";

                        query = "";
                        if (cmbline.Text == "All")
                        {
                            //mrt_local
                            //query = "SELECT MO_NO,MO_LINE,SUM(PC_COUNT),EMP_ID,SEQ_NO, OP_ID FROM stationhistory where time>='" + start + "' and time<='" + end + "' GROUP BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID ORDER BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID";

                            //MRT_GLOBALDB
                            query = "SELECT MO_NO,MO_LINE,SUM(PC_COUNT),EMP_ID,SEQ_NO, OP_ID FROM HANGER_HISTORY where time>='" + start + "' and time<='" + end + "' GROUP BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID ORDER BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID";
                        }
                        else
                        {
                            //mrt_local
                            //query = "SELECT h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT),EMP_ID,SEQ_NO, h.OP_ID FROM stationhistory h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and time>='" + start + "' and time<='" + end + "' GROUP BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID ORDER BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID";

                            //MRT_GLOBALDB
                            query = "SELECT h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT),EMP_ID,SEQ_NO, h.OP_ID FROM HANGER_HISTORY h,STATION_DATA s where s.I_STN_ID=h.STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "' and time>='" + start + "' and time<='" + end + "' GROUP BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID ORDER BY MO_NO,MO_LINE,EMP_ID,SEQ_NO, OP_ID";
                        }

                        //get mo wise production
                        //sda1 = new MySqlDataAdapter(query, dc.conn); //mrt_local
                        sda1 = new SqlDataAdapter(query, dc.con);  //MRT_GLOBALDB
                        DataTable dt = new DataTable();
                        sda1.Fill(dt);
                        sda1.Dispose();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            String mo = dt.Rows[j][0].ToString();
                            String moline = dt.Rows[j][1].ToString();
                            String seq = dt.Rows[j][4].ToString();
                            String empid = dt.Rows[j][3].ToString();
                            //String hour = dt4.Rows[m][0].ToString();
                            String opid = dt.Rows[j][5].ToString();

                            String empname = "";
                            decimal hourlysam = 0;
                            int count = 0;

                            if (empid == "0")
                            {
                                continue;
                            }

                            cmd = new SqlCommand("select V_FIRST_NAME from EMPLOYEE where V_EMP_ID='" + empid + "'", dc.con);
                            empname = cmd.ExecuteScalar() + "";

                            if (empname == "")
                            {
                                cmd = new SqlCommand("select V_GROUP_DESC from EMPLOYEE_GROUP_CATEGORY where V_GROUP_ID='" + empid + "'", dc.con);
                                empname = cmd.ExecuteScalar() + "";
                            }

                            //get pc count
                            temp1 = dt.Rows[j][2].ToString();
                            if (temp1 != "")
                            {
                                count = int.Parse(temp1);
                            }

                            ////get sequence for the mo
                            //SqlDataAdapter sda = new SqlDataAdapter("select V_OPERATION_ID,D_SAM FROM SEQUENCE_OPERATION WHERE V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and V_SEQUENCE_NO='" + seq + "'", dc.con);
                            //DataTable dt1 = new DataTable();
                            //sda.Fill(dt1);
                            //sda.Dispose();

                            //for (int p = 0; p < dt1.Rows.Count; p++)
                            //{



                            //String opid = dt1.Rows[p][0].ToString();
                            //int sam = int.Parse(dt1.Rows[p][1].ToString());

                            //cmd = new SqlCommand("select V_OPERATION_CODE from OPERATION_DB where V_ID='" + opid + "'", dc.con);
                            //String opcode = cmd.ExecuteScalar() + "";

                            //cmd = new SqlCommand("select V_OPERATION_DESC from OPERATION_DB where V_ID='" + opid + "'", dc.con);
                            //String opdesc = cmd.ExecuteScalar() + "";

                            //Hanafi | Date:21/10/2021 | Get the V_OPERATION_CODE and V_OPERATION_DESC directly from  OPERATION_DB table
                            String opcode = "";
                            String opdesc = "";
                            int sam = 0;
                            String strquery = "select V_OPERATION_CODE, V_OPERATION_DESC, D_SAM from OPERATION_DB where V_ID='" + opid + "'";
                            SqlCommand cmdQuery = new SqlCommand(strquery, dc.con);
                            sdr = cmdQuery.ExecuteReader();
                            if (sdr.Read())
                            {
                                opcode = sdr.GetValue(0).ToString();
                                opdesc = sdr.GetValue(1).ToString();
                                sam = int.Parse( sdr.GetValue(2).ToString());
                            }

                            decimal efficiency = 0;
                                decimal actual_sam = 0;
                                int duration = 3600 * (m + 1);
                                duration -= breaktime * 60;

                                //if (count > 0)
                                //{
                                //    actual_sam = (decimal)duration / (decimal)count;
                                //}
                                decimal earnedmin = sam * count;
                                efficiency = ((decimal)earnedmin / (decimal)duration) * 100;
                                efficiency = Math.Round(efficiency, 2);

                                //string strEffc = String.Format("{0:0.00}", efficiency); //efficiency.ToString("0.##");

                                //get qc count
                                if (cmbline.Text == "All")
                                {
                                    //query = "SELECT SUM(I_QUANTITY) FROM QC_HISTORY where V_OP_CODE='" + opcode + "' and V_EMP_ID='" + empid + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and DATEPART(hour,D_DATE_TIME)='" + hour + "'";
                                    query = "SELECT SUM(I_QUANTITY) FROM QC_HISTORY where V_OP_CODE='" + opcode + "' and V_EMP_ID='" + empid + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<='" + end + "'"; //exclude DATEPART function
                                }
                                else
                                {
                                    //query = "SELECT SUM(I_QUANTITY) FROM QC_HISTORY where V_OP_CODE='" + opcode + "' and V_EMP_ID='" + empid + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and I_STATION_ID LIKE '" + cmbline.Text + ".%' and DATEPART(hour,D_DATE_TIME)='" + hour + "'";
                                    query = "SELECT SUM(I_QUANTITY) FROM QC_HISTORY where V_OP_CODE='" + opcode + "' and V_EMP_ID='" + empid + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<='" + end + "' and I_STATION_ID LIKE '" + cmbline.Text + ".%'"; //exclude DATEPART function
                                }

                                int repair = 0;
                                cmd = new SqlCommand(query, dc.con);
                                String temp = cmd.ExecuteScalar() + "";
                                if (temp != "")
                                {
                                    repair = int.Parse(temp);
                                }

                                if (hour.Length == 1)
                                {
                                    hour = "0" + hour;
                                }

                                decimal repair_eff = 0;
                                if (count > 0)
                                {
                                    repair_eff = (decimal)repair / (decimal)count * 100;
                                }

                                int flag = 0;
                                for (int k = 0; k < dtprod.Rows.Count; k++)
                                {
                                    if (dtprod.Rows[k][2].ToString() == opcode && dtprod.Rows[k][4].ToString() == empid && dtprod.Rows[k][0].ToString() == startdate.ToString("yyyy-MM-dd") && start1 + ":00:00 - " + end1 + ":59:59" == dtprod.Rows[k][1].ToString())
                                    {
                                        //if mo already present add overtime count
                                        int temp2 = int.Parse(dtprod.Rows[k]["Production"].ToString());
                                        count += temp2;
                                        dtprod.Rows[k]["Production"] = count;
                                        data8.Rows[k]["production"] = count;
                                        flag = 1;

                                        if (count > 0)
                                        {
                                            repair_eff = (decimal)repair / (decimal)count * 100;
                                        }
                                        data8.Rows[k]["defect_rate"] = repair_eff;
                                        dtprod.Rows[k]["Defect Rate"] = repair_eff + "%";

                                        earnedmin = sam * count;
                                        efficiency = ((decimal)earnedmin / (decimal)duration) * 100;
                                        efficiency = Math.Round(efficiency, 2);
                                        earnedmin /= 60;

                                        data8.Rows[k]["actual_sam"] = earnedmin.ToString("0");
                                        dtprod.Rows[k]["Earned Minutes"] = earnedmin.ToString("0");

                                        data8.Rows[k]["efficiency"] = String.Format("{0:0.00}", efficiency); //efficiency.ToString("0");
                                        dtprod.Rows[k]["Efficiency"] = String.Format("{0:0.00}", efficiency) + " %"; //efficiency.ToString("0") + "%";

                                        break;
                                    }
                                }

                                if (flag == 0)
                                {
                                    if (hour.Length == 1)
                                    {
                                        hour = "0" + hour;
                                    }

                                    decimal sam1 = (decimal)sam / (decimal)60;
                                    actual_sam /= 60;
                                    earnedmin /= 60;
                                    decimal duration1 = (decimal)duration / (decimal)60;

                                    hourlysam /= 60;

                                    dtprod.Rows.Add(startdate.ToString("yyyy-MM-dd"), start1 + ":00:00 - " + end1 + ":59:59", opcode, opdesc, empid, empname, sam1.ToString("0.##"), earnedmin.ToString("0"), count, duration1.ToString("0.##"), String.Format("{0:0.00}", efficiency) + " %", repair, repair_eff.ToString("0") + "%");
                                    data8.Rows.Add(startdate.ToString("yyyy-MM-dd"), opcode, opdesc, empid, empname, count, repair, seq, start1 + ":00:00 - " + end1 + ":59:59", sam1.ToString("0.##"), earnedmin.ToString("0"), String.Format("{0:0.00}", efficiency), repair_eff.ToString("0.##"), total_prod, duration1.ToString("0.##"), hourlysam.ToString("0.##"));
                                }
                            //}
                        }
                    }
                    startdate = startdate.AddDays(1);
                }
                dgvproduction.DataSource = dtprod;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
                radLabel1.Text = ex.Message;
            }
        }

        public void Houly_Production()
        {
            try
            {
                data7.Rows.Clear();

                DataTable dtprod = new DataTable();
                dtprod.Columns.Add("Date");
                dtprod.Columns.Add("Hour");
                dtprod.Columns.Add("MO No");
                dtprod.Columns.Add("MO Details");
                dtprod.Columns.Add("Loaded");
                dtprod.Columns.Add("Unloaded");
                dtprod.Columns.Add("Efficiency");
                dtprod.Columns.Add("Repair/Rework");

                DateTime startdate = Convert.ToDateTime(dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime enddate = Convert.ToDateTime(dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59");

                String shift_start = "";
                String shift_end = "";
                String overtime_end = "";

                //get shift details
                SqlCommand cmd1 = new SqlCommand("select T_SHIFT_START_TIME,T_SHIFT_END_TIME,T_OVERTIME_END_TIME from SHIFTS where V_SHIFT='" + cmbshift.Text + "'", dc.con);
                SqlDataReader sdr = cmd1.ExecuteReader();
                if (sdr.Read())
                {
                    shift_start = sdr.GetValue(0).ToString();
                    shift_end = sdr.GetValue(1).ToString();
                    overtime_end = sdr.GetValue(2).ToString();
                }
                sdr.Close();

                cmd1 = new SqlCommand("select HIDE_OVERTIME from SETUP", dc.con);
                String overtime = cmd1.ExecuteScalar() + "";
                if (overtime == "TRUE")
                {
                    shift_end = overtime_end;
                }

                while (startdate < enddate)
                {
                    int mocount = 0;

                    String date = startdate.ToString("yyyy-MM-dd");

                    String start = startdate.ToString("yyyy-MM-dd") + " 00:00:00";
                    String end = startdate.ToString("yyyy-MM-dd") + " 23:59:59";

                    if (cmbshift.Text != "All")
                    {
                        start = startdate.ToString("yyyy-MM-dd") + " " + shift_start;
                        end = enddate.ToString("yyyy-MM-dd") + " " + shift_end;
                    }
                    //else
                    //{
                    //    if (cmbshift.Text == "3")
                    //    {
                    //        if (enddate > startdate)
                    //        {
                    //            enddate = enddate.AddDays(1);
                    //        }
                    //    }
                    //}

                    //check if date is enabled in hide day
                    SqlCommand cmd = new SqlCommand("select COUNT(*) from HIDEDAY_DB where CONVERT(nvarchar(10), '" + startdate + "', 120) in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                    mocount = int.Parse(cmd.ExecuteScalar() + "");
                    if (mocount > 0)
                    {
                        startdate = startdate.AddDays(1);
                        continue;
                    }

                    DateTime op_starttime = DateTime.Now;
                    DateTime op_endtime = DateTime.Now;

                    String query = "";
                    if (cmbline.Text == "All")
                    {
                        //MRT_GLOBALDB
                        query = "SELECT DISTINCT MO_NO,MO_LINE FROM HANGER_HISTORY where time>='" + start + "' and time<'" + end + "' order by MO_NO,MO_LINE";
                        //mrt_local
                        //query = "SELECT DISTINCT MO_NO,MO_LINE FROM stationhistory where time>='" + start + "' and time<'" + end + "' order by MO_NO,MO_LINE";
                    }
                    else
                    {
                        //MRT_GLOBALDB
                        query = "SELECT DISTINCT h.MO_NO,h.MO_LINE FROM HANGER_HISTORY h,STATION_DATA s where s.I_STN_ID=h.STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "' and h.time>='" + start + "' and h.time<'" + end + "' order by h.MO_NO,h.MO_LINE";
                        //mrt_local
                        //query = "SELECT DISTINCT h.MO_NO,h.MO_LINE FROM stationhistory h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and h.time>='" + start + "' and h.time<'" + end + "' order by h.MO_NO,h.MO_LINE";
                    }

                    //get mo used for the day
                    SqlDataAdapter da = new SqlDataAdapter(query, dc.con);  //MRT_GLOBALDB
                    //MySqlDataAdapter da = new MySqlDataAdapter(query, dc.conn); //mrt_local
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        String mo = dt.Rows[j][0].ToString();
                        String moline = dt.Rows[j][1].ToString();

                        String color = "";
                        String article = "";
                        String size = "";

                        //get mo details
                        SqlDataAdapter sda = new SqlDataAdapter("select C.V_COLOR_DESC,A.V_ARTICLE_DESC,S.V_SIZE_DESC from MO_DETAILS M,COLOR_DB C,ARTICLE_DB A,SIZE_DB S where M.V_ARTICLE_ID=A.V_ARTICLE_ID and M.V_COLOR_ID=C.V_COLOR_ID and M.V_SIZE_ID=S.V_SIZE_ID and M.V_MO_NO='" + mo + "' and M.V_MO_LINE='" + moline + "'", dc.con);
                        DataTable dt5 = new DataTable();
                        sda.Fill(dt5);
                        sda.Dispose();
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {
                            color = dt5.Rows[i][0].ToString();
                            article = dt5.Rows[i][1].ToString();
                            size = dt5.Rows[i][2].ToString();
                        }

                        int load = 0;

                        if (cmbline.Text == "All")
                        {
                            //MRT_GLOBALDB
                            query = "SELECT DATEPART(HOUR, TIME),MO_NO,MO_LINE,SUM(PC_COUNT) FROM HANGER_HISTORY where time>='" + start + "' and time<'" + end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='1' GROUP BY DATEPART(HOUR, TIME),MO_NO,MO_LINE ORDER BY DATEPART(HOUR, TIME)";

                            //mrt_local
                            //query = "SELECT HOUR(TIME),MO_NO,MO_LINE,SUM(PC_COUNT) FROM stationhistory where time>='" + start + "' and time<'" + end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='1' GROUP BY HOUR(TIME),MO_NO,MO_LINE ORDER BY HOUR(TIME)";


                        }
                        else
                        {
                            //MRT_GLOBALDB
                            query = "SELECT DATEPART(HOUR, h.TIME),h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT) FROM HANGER_HISTORY h,STATION_DATA s where s.I_STN_ID=h.STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "' and h.time>='" + start + "' and h.time<'" + end + "' and h.MO_NO='" + mo + "' and h.MO_LINE='" + moline + "' and h.REMARKS='1' GROUP BY DATEPART(HOUR, h.TIME),h.MO_NO,h.MO_LINE ORDER BY DATEPART(HOUR, h.TIME)";

                            //mrt_local
                            //query = "SELECT HOUR(h.TIME),h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT) FROM stationhistory h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and h.time>='" + start + "' and h.time<'" + end + "' and h.MO_NO='" + mo + "' and h.MO_LINE='" + moline + "' and h.REMARKS='1' GROUP BY HOUR(h.TIME),h.MO_NO,h.MO_LINE ORDER BY HOUR(h.TIME)";
                        }

                        //get hourly mo loaded
                        //da = new MySqlDataAdapter(query, dc.conn); //mrt_local
                        da = new SqlDataAdapter(query, dc.con); //MRT_GLOBALDB
                        dt5 = new DataTable();
                        da.Fill(dt5);
                        da.Dispose();
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {
                            load = int.Parse(dt5.Rows[i][3].ToString());

                            int flag = 0;
                            for (int n = 0; n < data7.Rows.Count; n++)
                            {
                                if (data7.Rows[n][0].ToString() == mo && data7.Rows[n][1].ToString() == moline && data7.Rows[n][2].ToString() == dt5.Rows[i][0].ToString() + ":00:00" && date == data7.Rows[n]["date"].ToString())
                                {
                                    data7.Rows[n][3] = load;
                                    flag = 1;
                                }
                            }

                            if (flag == 0)
                            {
                                String hour = dt5.Rows[i][0].ToString();
                                if (hour.Length == 1)
                                {
                                    hour = "0" + hour;
                                }

                                data7.Rows.Add(mo, moline, hour + ":00:00", load, "0", "0", "0", color, article, size, date);
                            }
                        }

                        int unload = 0;

                        if (cmbline.Text == "All")
                        {
                            //mrt_local
                            //query = "SELECT HOUR(TIME),MO_NO,MO_LINE,SUM(PC_COUNT) FROM stationhistory where time>='" + start + "' and time<'" + end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='2' GROUP BY HOUR(TIME),MO_NO,MO_LINE ORDER BY HOUR(TIME)";

                            //MRT_GLOBALDB
                            query = "SELECT DATEPART(HOUR,TIME),MO_NO,MO_LINE,SUM(PC_COUNT) FROM HANGER_HISTORY where time>='" + start + "' and time<'" + end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='2' GROUP BY DATEPART(HOUR,TIME),MO_NO,MO_LINE ORDER BY DATEPART(HOUR,TIME)";
                        }
                        else
                        {
                            //mrt_local
                            //query = "SELECT HOUR(h.TIME),h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT) FROM stationhistory h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and h.time>='" + start + "' and h.time<'" + end + "' and h.MO_NO='" + mo + "' and h.MO_LINE='" + moline + "' and h.REMARKS='2' GROUP BY HOUR(h.TIME),h.MO_NO,h.MO_LINE ORDER BY HOUR(h.TIME)";

                            //MRT_GLOBALDB
                            query = "SELECT DATEPART(HOUR, h.TIME),h.MO_NO,h.MO_LINE,SUM(h.PC_COUNT) FROM HANGER_HISTORY h,STATION_DATA s where s.I_STN_ID=h.STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "' and h.time>='" + start + "' and h.time<'" + end + "' and h.MO_NO='" + mo + "' and h.MO_LINE='" + moline + "' and h.REMARKS='2' GROUP BY DATEPART(HOUR, h.TIME),h.MO_NO,h.MO_LINE ORDER BY DATEPART(HOUR, h.TIME)";
                        }

                        //get hourly mo unloaded
                        //da = new MySqlDataAdapter(query, dc.conn); //mrt_local
                        da = new SqlDataAdapter(query, dc.con); //MRT_GLOBALDB
                        dt5 = new DataTable();
                        da.Fill(dt5);
                        da.Dispose();
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {
                            unload = int.Parse(dt5.Rows[i][3].ToString());
                            int flag = 0;

                            String hour = dt5.Rows[i][0].ToString();
                            if (hour.Length == 1)
                            {
                                hour = "0" + hour;
                            }

                            for (int n = 0; n < data7.Rows.Count; n++)
                            {
                                if (data7.Rows[n][0].ToString() == mo && data7.Rows[n][1].ToString() == moline && data7.Rows[n][2].ToString() == hour + ":00:00" && date == data7.Rows[n]["date"].ToString())
                                {
                                    data7.Rows[n][4] = unload;
                                    flag = 1;
                                }
                            }

                            if (flag == 0)
                            {
                                data7.Rows.Add(mo, moline, hour + ":00:00", "0", unload, "0", "0", color, article, size, date);
                            }
                        }

                        //get article id
                        String articleid = "";
                        cmd = new SqlCommand("select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                        articleid = cmd.ExecuteScalar() + "";

                        int total_sam = 0;

                        //get sum of sam for the article
                        String temp = "";
                        if (getallop == "TRUE")
                        {
                            //get sum of sam , sum of piecerate and sum of overtime rate
                            cmd = new SqlCommand("select SUM(o.D_SAM) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "')", dc.con);
                            temp = cmd.ExecuteScalar().ToString();
                            if (temp != "")
                            {
                                total_sam = int.Parse(temp + "");
                            }
                            else
                            {
                                total_sam = 0;
                            }
                        }
                        else
                        {
                            //get sum of sam , sum of piecerate and sum of overtime rate
                            cmd = new SqlCommand("select SUM(o.D_SAM) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "') and d.I_SEQUENCE_NO in(select s.I_SEQUENCE_NO from STATION_ASSIGN s where s.V_MO_NO='" + mo + "' and s.V_MO_LINE='" + moline + "' and s.I_STATION_ID!='0')", dc.con);
                            temp = cmd.ExecuteScalar().ToString();
                            if (temp != "")
                            {
                                total_sam = int.Parse(temp + "");
                            }
                            else
                            {
                                total_sam = 0;
                            }
                        }

                        if (cmbline.Text == "All")
                        {
                            //mrt_local
                            //query = "select HOUR(TIME),SUM(PC_COUNT) from stationhistory where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='2' and TIME>='" + start + "' and time<'" + end + "' group by HOUR(TIME)";

                            //MRT_GLOBALDB
                            query = "select DATEPART(HOUR, TIME),SUM(PC_COUNT) from HANGER_HISTORY where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='2' and TIME>='" + start + "' and time<'" + end + "' group by DATEPART(HOUR, TIME)";
                        }
                        else
                        {
                            //mrt_local
                            //query = "select HOUR(h.TIME),SUM(h.PC_COUNT) from stationhistory h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and h.MO_NO='" + mo + "' and h.MO_LINE='" + moline + "' and REMARKS='2' and h.TIME>='" + start + "' and h.time<'" + end + "' group by HOUR(h.TIME)";

                            //MRT_GLOBALDB
                            query = "select DATEPART(HOUR, h.TIME),SUM(h.PC_COUNT) from HANGER_HISTORY h,STATION_DATA s where s.I_STN_ID=h.STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "' and h.MO_NO='" + mo + "' and h.MO_LINE='" + moline + "' and REMARKS='2' and h.TIME>='" + start + "' and h.time<'" + end + "' group by DATEPART(HOUR, h.TIME)";
                        }

                        //get hourly production
                        //da = new MySqlDataAdapter(query, dc.conn); //mrt_local
                        da = new SqlDataAdapter(query, dc.con); //MRT_GLOBALDB
                        dt5 = new DataTable();
                        da.Fill(dt5);
                        da.Dispose();
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {
                            //get first hanger and last hanger time

                            if (cmbline.Text == "All")
                            {
                                //mrt_local
                                //query = "SELECT MIN(TIME),MAX(TIME) FROM stationhistory where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and TIME>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' and HOUR(TIME)='" + dt5.Rows[i][0].ToString() + "'";

                                //MRT_GLOBALDB
                                query = "SELECT MIN(TIME),MAX(TIME) FROM HANGER_HISTORY where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and TIME>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' and DATEPART(HOUR, TIME)='" + dt5.Rows[i][0].ToString() + "'";
                            }
                            else
                            {
                                //mrt_local
                                //query = "SELECT MIN(h.TIME),MAX(h.TIME) FROM stationhistory h,stationdata s where s.STN_ID=h.STN_ID and s.INFEED_LINENO='" + cmbline.Text + "' and h.MO_NO='" + mo + "' and h.MO_LINE='" + moline + "' and h.TIME>='" + start + "' and time<'" + end + "' and HOUR(h.TIME)='" + dt5.Rows[i][0].ToString() + "'";

                                //MRT_GLOBALDB
                                query = "SELECT MIN(h.TIME),MAX(h.TIME) FROM HANGER_HISTORY h,STATION_DATA s where s.I_STN_ID=h.STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "' and h.MO_NO='" + mo + "' and h.MO_LINE='" + moline + "' and h.TIME>='" + start + "' and time<'" + end + "' and DATEPART(HOUR, TIME)='" + dt5.Rows[i][0].ToString() + "'";
                            }

                            //MySqlDataAdapter sda2 = new MySqlDataAdapter(query, dc.conn);  //mrt_local
                            SqlDataAdapter sda2 = new SqlDataAdapter(query, dc.con); //MRT_GLOBALDB
                            DataTable dt2 = new DataTable();
                            sda2.Fill(dt2);
                            sda2.Dispose();
                            if (dt2.Rows.Count > 0)
                            {
                                if (dt2.Rows[0][0].ToString() != "")
                                {
                                    op_starttime = Convert.ToDateTime(dt2.Rows[0][0].ToString());
                                }

                                if (dt2.Rows[0][1].ToString() != "")
                                {
                                    op_endtime = Convert.ToDateTime(dt2.Rows[0][1].ToString());
                                }

                                int actual_production = int.Parse(dt5.Rows[i][1].ToString());

                                //calculate duration
                                TimeSpan ts = new TimeSpan();
                                ts = op_endtime - op_starttime;
                                int duration = (int)ts.TotalSeconds;

                                //claculate actual sam
                                decimal actual_sam = 0;
                                if (actual_production > 0)
                                {
                                    actual_sam = (decimal)duration / (decimal)actual_production;
                                }

                                //calculate efficiency
                                decimal efficiency = 0;
                                if (actual_sam > 0)
                                {
                                    efficiency = (total_sam / actual_sam) * 100;
                                }

                                for (int n = 0; n < data7.Rows.Count; n++)
                                {
                                    if (data7.Rows[n][0].ToString() == mo && data7.Rows[n][1].ToString() == moline && data7.Rows[n][2].ToString() == dt5.Rows[i][0].ToString() + ":00:00" && date == data7.Rows[n]["date"].ToString())
                                    {
                                        data7.Rows[n][5] = (int)efficiency + "%";
                                    }
                                }
                            }
                        }

                        if (cmbline.Text != "All")
                        {
                            query = "select CONVERT(VARCHAR(2), D_DATE_TIME, 108),SUM(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and I_STATION_ID LIKE'" + cmbline.Text + ".%' group by CONVERT(VARCHAR(2), D_DATE_TIME, 108) ORDER BY CONVERT(VARCHAR(2), D_DATE_TIME, 108)";
                        }
                        else
                        {
                            query = "select CONVERT(VARCHAR(2), D_DATE_TIME, 108),SUM(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' group by CONVERT(VARCHAR(2), D_DATE_TIME, 108) ORDER BY CONVERT(VARCHAR(2), D_DATE_TIME, 108)";
                        }
                        //get hourly repair quantity
                        sda = new SqlDataAdapter(query, dc.con);
                        dt5 = new DataTable();
                        sda.Fill(dt5);
                        sda.Dispose();
                        for (int i = 0; i < dt5.Rows.Count; i++)
                        {
                            int count = 0;

                            temp = dt5.Rows[i][1].ToString();
                            if (temp != "")
                            {
                                count = int.Parse(dt5.Rows[i][1].ToString());
                            }
                            else
                            {
                                count = 0;
                            }

                            for (int n = 0; n < data7.Rows.Count; n++)
                            {
                                if (data7.Rows[n][0].ToString() == mo && data7.Rows[n][1].ToString() == moline && data7.Rows[n][2].ToString() == dt5.Rows[i][0].ToString() + ":00:00" && date == data7.Rows[n]["date"].ToString())
                                {
                                    data7.Rows[n][6] = count;
                                }
                            }
                        }
                    }

                    startdate = startdate.AddDays(1);
                }

                for (int i = 0; i < data7.Rows.Count; i++)
                {
                    dtprod.Rows.Add(data7.Rows[i]["date"].ToString(), data7.Rows[i]["hour"].ToString(), data7.Rows[i]["mono"].ToString(), data7.Rows[i]["moline"].ToString(), data7.Rows[i]["loaded"].ToString(), data7.Rows[i]["unloaded"].ToString(), data7.Rows[i]["eff"].ToString(), data7.Rows[i]["rework"].ToString());
                }

                dgvproduction.DataSource = dtprod;
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        String hidetotals = "FALSE";

        private void Daily_Production_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();  //open connection

            //get language and theme
            String Lang = "";
            SqlCommand cmd = new SqlCommand("SELECT Language,ThemeName,HIDE_TOTALS,GET_ALL_OPERATIONS FROM Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Lang = sdr.GetValue(0).ToString();
                theme = sdr.GetValue(1).ToString();
                hidetotals = sdr.GetValue(2).ToString();
                getallop = sdr.GetValue(3).ToString();
            }
            sdr.Close();

            //change grid theme
            GridTheme(theme);
        }

        //set grid theme
        public void GridTheme(String theme)
        {
            dgvproduction.ThemeName = theme;
        }

        private void radLabel1_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            panel1.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            panel1.Visible = false;
            radLabel1.Text = "";
            MyTimer.Stop();
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            updateflag = 1;
            Daily_Prod();  //calculate dialy performance
        }

        private void chkmono_CheckStateChanged(object sender, EventArgs e)
        {
            //check if mo checkbox is checked
            if (chkmono.Checked == true)
            {
                chkemployee.Checked = false;
                chkmoemployee.Checked = false;
                chkempperformance.Checked = false;
                chkhourlyperformance.Checked = false;
                chkcumulativeperformance.Checked = false;
            }

            if (updateflag == 0)
            {
                Daily_Prod();  //calculate dialy mo performance
            }
        }

        private void chkoperation_CheckStateChanged(object sender, EventArgs e)
        {
            //check if employee checkbox is checked
            if (chkemployee.Checked == true)
            {
                chkmono.Checked = false;
                chkmoemployee.Checked = false;
                chkempperformance.Checked = false;
                chkhourlyperformance.Checked = false;
                chkcumulativeperformance.Checked = false;
            }

            if (updateflag == 0)
            {
                Daily_Prod();  //calculate dialy employee performance
            }
        }

        private void radDropDownList1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //generate charts
            chrtproduction.Series.Clear();
            if (cmbcharts.Text == "Total Production")
            {
                Production_Chart();
            }
            else if (cmbcharts.Text == "Total Repair")
            {
                Repair_Chart();
            }
            else if (cmbcharts.Text == "Total Cost")
            {
                Cost_Chart();
            }
            else if (cmbcharts.Text == "Total Average SAM")
            {
                SAM_Chart();
            }
            else if (cmbcharts.Text == "Total Average Efficiency")
            {
                Efficiency_Chart();
            }
        }

        public void Production_Chart()
        {
            //generate total production chart
            chrtproduction.Series.Clear();
            LineSeries lineseries = new LineSeries();
            lineseries.LegendTitle = "Total Production";

            for (int i = 1; i < dttemp.Rows.Count; i++)
            {
                DateTime date = DateTime.ParseExact(dttemp.Rows[i][0].ToString(), "yyyy-MM-dd", null);
                int count1 = int.Parse(dttemp.Rows[i][2].ToString()) + int.Parse(dttemp.Rows[i][4].ToString());
                lineseries.DataPoints.Add(new CategoricalDataPoint(count1, date.ToString("MMM - dd")));
            }

            chrtproduction.Series.Add(lineseries);

            lineseries.ForeColor = Color.White;
            lineseries.ShowLabels = true;

            chrtproduction.LegendTitle = "Piece Count";
            chrtproduction.ShowLegend = true;
            chrtproduction.ShowSmartLabels = true;

            LinearAxis verticalAxis = chrtproduction.Axes[1] as LinearAxis;
            verticalAxis.LabelFitMode = AxisLabelFitMode.MultiLine;
            verticalAxis.ForeColor = Color.White;
            verticalAxis.BorderColor = Color.DodgerBlue;
            verticalAxis.ShowLabels = false;
            verticalAxis.Title = "Piece Count";

            CategoricalAxis ca = chrtproduction.Axes[0] as CategoricalAxis;
            ca.LabelFitMode = AxisLabelFitMode.MultiLine;
            ca.Title = "Date";
            ca.ForeColor = Color.White;
            ca.BorderColor = Color.DodgerBlue;
            ca.LabelFitMode = AxisLabelFitMode.Rotate;
            ca.LabelRotationAngle = 270;

            chrtproduction.ForeColor = Color.White;
            chrtproduction.ShowPanZoom = true;
            chrtproduction.Title = "Global Daily Production";
        }

        public void Repair_Chart()
        {
            //generate total repair chart
            LineSeries lineseries = new LineSeries();
            lineseries.LegendTitle = "Total Repairs/Reworks";

            for (int i = 1; i < dttemp.Rows.Count; i++)
            {
                DateTime date = DateTime.ParseExact(dttemp.Rows[i][0].ToString(), "yyyy-MM-dd", null);
                int count1 = int.Parse(dttemp.Rows[i][3].ToString()) + int.Parse(dttemp.Rows[i][5].ToString());
                lineseries.DataPoints.Add(new CategoricalDataPoint(count1, date.ToString("MMM - dd")));
            }

            chrtproduction.Series.Add(lineseries);

            lineseries.ForeColor = Color.White;
            lineseries.ShowLabels = true;

            chrtproduction.LegendTitle = "Piece Count";
            chrtproduction.ShowLegend = true;
            chrtproduction.ShowSmartLabels = true;

            LinearAxis verticalAxis = chrtproduction.Axes[1] as LinearAxis;
            verticalAxis.LabelFitMode = AxisLabelFitMode.MultiLine;
            verticalAxis.ForeColor = Color.White;
            verticalAxis.BorderColor = Color.DodgerBlue;
            verticalAxis.ShowLabels = false;
            verticalAxis.Title = "Piece Count";

            CategoricalAxis ca = chrtproduction.Axes[0] as CategoricalAxis;
            ca.LabelFitMode = AxisLabelFitMode.MultiLine;
            ca.Title = "Date";
            ca.ForeColor = Color.White;
            ca.BorderColor = Color.DodgerBlue;
            ca.LabelFitMode = AxisLabelFitMode.Rotate;
            ca.LabelRotationAngle = 270;

            chrtproduction.ForeColor = Color.White;
            chrtproduction.ShowPanZoom = true;
            chrtproduction.Title = "Global Daily Repair/Reworks";
        }

        public void Cost_Chart()
        {
            //generate total cost chart
            LineSeries lineseries = new LineSeries();
            lineseries.LegendTitle = "Total Cost";

            for (int i = 1; i < dttemp.Rows.Count; i++)
            {
                DateTime date = DateTime.ParseExact(dttemp.Rows[i][0].ToString(), "yyyy-MM-dd", null);
                String[] temp = dttemp.Rows[i][9].ToString().Split('.');
                String[] temp1 = dttemp.Rows[i][10].ToString().Split('.');
                int count1 = int.Parse(temp[0]) + int.Parse(temp1[0]);
                lineseries.DataPoints.Add(new CategoricalDataPoint(count1, date.ToString("MMM - dd")));
            }

            chrtproduction.Series.Add(lineseries);

            lineseries.ForeColor = Color.White;
            lineseries.ShowLabels = true;

            chrtproduction.LegendTitle = "Piece Rate";
            chrtproduction.ShowLegend = true;
            chrtproduction.ShowSmartLabels = true;

            LinearAxis verticalAxis = chrtproduction.Axes[1] as LinearAxis;
            verticalAxis.LabelFitMode = AxisLabelFitMode.MultiLine;
            verticalAxis.ForeColor = Color.White;
            verticalAxis.BorderColor = Color.DodgerBlue;
            verticalAxis.ShowLabels = false;
            verticalAxis.Title = "Piece Rate";

            CategoricalAxis ca = chrtproduction.Axes[0] as CategoricalAxis;
            ca.LabelFitMode = AxisLabelFitMode.MultiLine;
            ca.Title = "Date";
            ca.ForeColor = Color.White;
            ca.BorderColor = Color.DodgerBlue;
            ca.LabelFitMode = AxisLabelFitMode.Rotate;
            ca.LabelRotationAngle = 270;

            chrtproduction.ForeColor = Color.White;
            chrtproduction.ShowPanZoom = true;
            chrtproduction.Title = "Global Daily Cost";
        }

        public void SAM_Chart()
        {
            //generate total sam chart
            LineSeries lineseries = new LineSeries();
            lineseries.LegendTitle = "Total Average SAM";

            for (int i = 1; i < dttemp.Rows.Count; i++)
            {
                DateTime date = DateTime.ParseExact(dttemp.Rows[i][0].ToString(), "yyyy-MM-dd", null);
                String[] temp = dttemp.Rows[i][8].ToString().Split('.');
                int count1 = int.Parse(temp[0]);
                lineseries.DataPoints.Add(new CategoricalDataPoint(count1, date.ToString("MMM - dd")));
            }
            chrtproduction.Series.Add(lineseries);

            lineseries.ForeColor = Color.White;
            lineseries.ShowLabels = true;

            chrtproduction.LegendTitle = "Minutes";
            chrtproduction.ShowLegend = true;
            chrtproduction.ShowSmartLabels = true;

            LinearAxis verticalAxis = chrtproduction.Axes[1] as LinearAxis;
            verticalAxis.LabelFitMode = AxisLabelFitMode.MultiLine;
            verticalAxis.ForeColor = Color.White;
            verticalAxis.BorderColor = Color.DodgerBlue;
            verticalAxis.ShowLabels = false;
            verticalAxis.Title = "Minutes";

            CategoricalAxis ca = chrtproduction.Axes[0] as CategoricalAxis;
            ca.LabelFitMode = AxisLabelFitMode.MultiLine;
            ca.Title = "Date";
            ca.ForeColor = Color.White;
            ca.BorderColor = Color.DodgerBlue;
            ca.LabelFitMode = AxisLabelFitMode.Rotate;
            ca.LabelRotationAngle = 270;

            chrtproduction.ForeColor = Color.White;
            chrtproduction.ShowPanZoom = true;
            chrtproduction.Title = "Global Daily SAM";
        }

        public void Efficiency_Chart()
        {
            //generate total efficiency chart
            try
            {
                LineSeries lineseries = new LineSeries();
                lineseries.LegendTitle = "Total Efficeincy";

                for (int i = 1; i < dttemp.Rows.Count; i++)
                {
                    DateTime date = DateTime.ParseExact(dttemp.Rows[i][0].ToString(), "yyyy-MM-dd", null);
                    String[] temp = dttemp.Rows[i][6].ToString().Split('%');
                    String[] temp1 = temp[0].Split('.');
                    int count1 = int.Parse(temp1[0]);
                    lineseries.DataPoints.Add(new CategoricalDataPoint(count1, date.ToString("MMM - dd")));
                }
                chrtproduction.Series.Add(lineseries);

                lineseries.ForeColor = Color.White;
                lineseries.ShowLabels = true;

                chrtproduction.LegendTitle = "Percentage";
                chrtproduction.ShowLegend = true;
                chrtproduction.ShowSmartLabels = true;

                LinearAxis verticalAxis = chrtproduction.Axes[1] as LinearAxis;
                verticalAxis.LabelFitMode = AxisLabelFitMode.MultiLine;
                verticalAxis.ForeColor = Color.White;
                verticalAxis.BorderColor = Color.DodgerBlue;
                verticalAxis.ShowLabels = false;
                verticalAxis.Title = "Percentage";

                CategoricalAxis ca = chrtproduction.Axes[0] as CategoricalAxis;
                ca.LabelFitMode = AxisLabelFitMode.MultiLine;
                ca.Title = "Date";
                ca.ForeColor = Color.White;
                ca.BorderColor = Color.DodgerBlue;
                ca.LabelFitMode = AxisLabelFitMode.Rotate;
                ca.LabelRotationAngle = 270;

                chrtproduction.ForeColor = Color.White;
                chrtproduction.ShowPanZoom = true;
                chrtproduction.Title = "Global Daily Efficiency";
            }
            catch (Exception ex)
            {
                radLabel1.Text = ex.Message;
            }
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            //generate dialy performance report
            if (btnreport.Text == "Report View")
            {
                if (chkmono.Checked == true || chkemployee.Checked == true || chkmoemployee.Checked == true || chkhourlyperformance.Checked == true || chkempperformance.Checked == true || chkcumulativeperformance.Checked == true)
                {
                    if (chkemployee.Checked == true)
                    {
                        DataView view = new DataView(data2);
                        DataView view1 = new DataView(data3);
                        DataView view2 = new DataView(data4);
                        DataView view3 = new DataView(data5);

                        //get logo
                        DataTable dt_image = new DataTable();
                        dt_image.Columns.Add("image", typeof(byte[]));
                        dt_image.Rows.Add(dc.GetImage());
                        DataView dv_image = new DataView(dt_image);

                        reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Daily_Employee_Report.rdlc";
                        reportViewer1.LocalReport.DataSources.Clear();

                        //add view to datatset
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view)); //table
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", view1));//total Production
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", view2));//total Cost
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", view3));//total Repair
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", dv_image));
                        reportViewer1.RefreshReport();

                        btnreport.Text = "Table View";
                        reportViewer1.Visible = true;
                    }
                    else if (chkmono.Checked == true)
                    {
                        //genaret mo dialy pereformance report
                        DataView view = new DataView(data1);
                        DataView view1 = new DataView(data5);
                        DataView view2 = new DataView(data3);
                        DataView view3 = new DataView(data4);

                        //get logo
                        DataTable dt_image = new DataTable();
                        dt_image.Columns.Add("image", typeof(byte[]));
                        dt_image.Rows.Add(dc.GetImage());
                        DataView dv_image = new DataView(dt_image);

                        reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Daily_mono_report.rdlc";
                        reportViewer1.LocalReport.DataSources.Clear();

                        //add view to datatset
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view)); //table
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", view1));//total Production
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", view3));//total Cost
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", view2));//total Repair
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", dv_image));
                        reportViewer1.RefreshReport();

                        btnreport.Text = "Table View";
                        reportViewer1.Visible = true;
                    }
                    else if (chkmoemployee.Checked == true)
                    {
                        //genaret mo dialy pereformance report
                        DataView view = new DataView(data6);

                        //get logo
                        DataTable dt_image = new DataTable();
                        dt_image.Columns.Add("image", typeof(byte[]));
                        dt_image.Rows.Add(dc.GetImage());
                        DataView dv_image = new DataView(dt_image);

                        reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Daily_MO_EMP_Report.rdlc";
                        reportViewer1.LocalReport.DataSources.Clear();

                        //add view to datatset
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view)); //table
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                        reportViewer1.RefreshReport();
    
                        btnreport.Text = "Table View";
                        reportViewer1.Visible = true;
                    }
                    else if (chkhourlyperformance.Checked == true)
                    {
                        //generate report
                        DataView view = new DataView(data7);

                        //get logo
                        DataTable dt_image = new DataTable();
                        dt_image.Columns.Add("image", typeof(byte[]));
                        dt_image.Rows.Add(dc.GetImage());
                        DataView dv_image = new DataView(dt_image);

                        reportViewer1.Visible = true;
                        reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Hourly_Performance_Report.rdlc";
                        reportViewer1.LocalReport.DataSources.Clear();

                        //add views to datatset
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                        reportViewer1.RefreshReport();

                        btnreport.Text = "Table View";
                        reportViewer1.Visible = true;
                    }
                    else if (chkempperformance.Checked == true)
                    {
                        //genaret mo dialy pereformance report
                        DataView view = new DataView(data8);

                        //get logo
                        DataTable dt_image = new DataTable();
                        dt_image.Columns.Add("image", typeof(byte[]));
                        dt_image.Rows.Add(dc.GetImage());
                        DataView dv_image = new DataView(dt_image);

                        reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Houry_MO_EMP_Report.rdlc";
                        reportViewer1.LocalReport.DataSources.Clear();

                        //add view to datatset
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view)); //table
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                        reportViewer1.RefreshReport();

                        btnreport.Text = "Table View";
                        reportViewer1.Visible = true;
                    }
                    else if (chkcumulativeperformance.Checked == true)
                    {
                        //genaret mo dialy pereformance report
                        DataView view = new DataView(data8);

                        //get logo
                        DataTable dt_image = new DataTable();
                        dt_image.Columns.Add("image", typeof(byte[]));
                        dt_image.Rows.Add(dc.GetImage());
                        DataView dv_image = new DataView(dt_image);

                        reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Cumulative_EMP_Performance_Report.rdlc";
                        reportViewer1.LocalReport.DataSources.Clear();

                        //add view to datatset
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view)); //table
                        reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                        reportViewer1.RefreshReport();

                        btnreport.Text = "Table View";
                        reportViewer1.Visible = true;
                    }
                }
                else
                {
                    //generate employee dialy performance report
                    DataView view = new DataView(data);
                    DataView view1 = new DataView(data4);
                    DataView view2 = new DataView(data5);
                    DataView view3 = new DataView(data3);

                    //get logo
                    DataTable dt_image = new DataTable();
                    dt_image.Columns.Add("image", typeof(byte[]));
                    dt_image.Rows.Add(dc.GetImage());
                    DataView dv_image = new DataView(dt_image);

                    reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.daily_report.rdlc";
                    reportViewer1.LocalReport.DataSources.Clear();

                    //add view to datatset
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view)); //table
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", view1));//total Production
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", view2));//total Cost
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", view3));//total Repair
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", dv_image));//total Repair
                    reportViewer1.RefreshReport();

                    btnreport.Text = "Table View";
                    reportViewer1.Visible = true;
                }
            }
            else
            {
                btnreport.Text = "Report View";
                reportViewer1.Visible = false;
            }
        }

        private void cmbline_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Daily_Prod();  //calculate dialy performance
        }

        private void dgvproduction_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change for color of grid if these themes are selected 
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvproduction.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvproduction.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvproduction.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvproduction.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }

            //if (chkmono.Checked == true)
            //{
            //    int[] columnIndexes = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //    MergeVertically(dgvproduction, columnIndexes);
            //}
        }

        public void ProductionDetails_tooltip()
        {
            try
            {
                if (dgvproduction.SelectedRows.Count == 0)
                {
                    return;
                }

                if (chkmono.Checked == false && chkmoemployee.Checked == false && chkhourlyperformance.Checked == false)
                {
                    return;
                }

                //special fields
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

                //get the special field name
                SqlCommand cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF1' and V_ENABLED='TRUE'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    u1 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get the special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    u2 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get the special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    u3 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get the special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    u4 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get the special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    u5 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get the special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF6' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    u6 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get the special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF7' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    u7 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get the special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF8' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    u8 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get the special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF9' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    u9 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get the special field name
                cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF10' and V_ENABLED='TRUE'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    u10 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                String mo = "";
                String color = "";
                String size = "";
                String article = "";
                String qty = "";
                String moline = "";

                //get the mo from the modetails table which are assigned the station only
                SqlDataAdapter sda = new SqlDataAdapter(" SELECT DISTINCT MO.V_MO_NO, MO.V_COLOR_ID,MO.V_SIZE_ID,MO.V_ARTICLE_ID,MO.I_ORDER_QTY,MO.V_USER_DEF1,MO.V_USER_DEF2,MO.V_USER_DEF3,MO.V_USER_DEF4,MO.V_USER_DEF5,MO.V_USER_DEF6,MO.V_USER_DEF7,MO.V_USER_DEF8,MO.V_USER_DEF9,MO.V_USER_DEF10,MO.V_MO_LINE,MO.V_STATUS,MO.I_ID,MO.I_HANGER_COUNT,MO.V_PURCHASE_ORDER,MO.V_SALES_ORDER,MO.V_SHIPPING_DEST,MO.V_SHIPPING_MODE,c.V_CUSTOMER_NAME FROM MO_DETAILS MO, MO m ,CUSTOMER_DB c where m.V_MO_NO=MO.V_MO_NO and m.V_CUSTOMER_ID=c.V_CUSTOMER_ID and MO.V_MO_NO='" + dgvproduction.SelectedRows[0].Cells["MO No"].Value + "' and MO.V_MO_LINE='" + dgvproduction.SelectedRows[0].Cells["MO Details"].Value + "' and MO.V_STATUS!='COMP' order by MO.I_ID DESC", dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    mo = dt.Rows[i][0].ToString();
                    color = dt.Rows[i][1].ToString();
                    size = dt.Rows[i][2].ToString();
                    article = dt.Rows[i][3].ToString();
                    qty = dt.Rows[i][4].ToString();
                    String user1 = dt.Rows[i][5].ToString();
                    String user2 = dt.Rows[i][6].ToString();
                    String user3 = dt.Rows[i][7].ToString();
                    String user4 = dt.Rows[i][8].ToString();
                    String user5 = dt.Rows[i][9].ToString();
                    String user6 = dt.Rows[i][10].ToString();
                    String user7 = dt.Rows[i][11].ToString();
                    String user8 = dt.Rows[i][12].ToString();
                    String user9 = dt.Rows[i][13].ToString();
                    String user10 = dt.Rows[i][14].ToString();
                    moline = dt.Rows[i][15].ToString();
                    String status = dt.Rows[i][16].ToString();
                    String hanger_count = dt.Rows[i][18].ToString();
                    String purorder = dt.Rows[i][19].ToString();
                    String salesorder = dt.Rows[i][20].ToString();
                    String dest = dt.Rows[i][21].ToString();
                    String mode = dt.Rows[i][22].ToString();
                    String cust = dt.Rows[i][23].ToString();
                    String id = i.ToString();
                    //get the descriptions of the color,article etc

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

                    toolTip1.SetToolTip(dgvproduction, " MO NO : " + mo + "\n MO Details :" + moline + "\n Article : " + article + " \n Size : " + size + " \n Color : " + color + " \n " + u1 + " : " + user1 + " \n " + u2 + " : " + user2 + " \n " + u3 + " : " + user3 + " \n " + u4 + " : " + user4 + "\n Purchase Order :" + purorder + "\n Sales Order :" + salesorder + "\n Shipping Dest:" + dest + "\n Shipping Mode :" + mode + "\n Customer :" + cust);
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }



        private void chkmoemployee_CheckStateChanged(object sender, EventArgs e)
        {
            //check if employee checkbox is checked
            if (chkmoemployee.Checked == true)
            {
                chkmono.Checked = false;
                chkemployee.Checked = false;
                chkempperformance.Checked = false;
                chkhourlyperformance.Checked = false;
                chkcumulativeperformance.Checked = false;
            }

            if (updateflag == 0)
            {
                Daily_Prod();  //calculate dialy employee performance
            }
        }

        private void chkhourlyperformance_CheckStateChanged(object sender, EventArgs e)
        {
            //check if employee checkbox is checked
            if (chkhourlyperformance.Checked == true)
            {
                chkmono.Checked = false;
                chkemployee.Checked = false;
                chkmoemployee.Checked = false;
                chkempperformance.Checked = false;
                chkcumulativeperformance.Checked = false;
            }

            if (updateflag == 0)
            {
                Daily_Prod();  //calculate dialy employee performance
            }
        }

        private void chkempperformance_CheckStateChanged(object sender, EventArgs e)
        {
            //check if employee checkbox is checked
            if (chkempperformance.Checked == true)
            {
                chkmono.Checked = false;
                chkemployee.Checked = false;
                chkmoemployee.Checked = false;
                chkhourlyperformance.Checked = false;
                chkcumulativeperformance.Checked = false;
            }

            if (updateflag == 0)
            {
                Daily_Prod();  //calculate dialy employee performance
            }
        }

        private void chkcumulativeperformance_CheckStateChanged(object sender, EventArgs e)
        {
            //check if employee checkbox is checked
            if (chkcumulativeperformance.Checked == true)
            {
                chkmono.Checked = false;
                chkemployee.Checked = false;
                chkmoemployee.Checked = false;
                chkempperformance.Checked = false;
                chkhourlyperformance.Checked = false;
            }

            if (updateflag == 0)
            {
                Daily_Prod();  //calculate dialy employee performance
            }
        }

        private void dgvproduction_MouseClick(object sender, MouseEventArgs e)
        {
            ProductionDetails_tooltip();
        }

        private void cmbshift_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Daily_Prod();
        }

        public void DebugLog(string Message)
        {
            try
            {
                //string path = "C:\\SMARTMRT\\SmartMRT MGIS\\Debug\\" + DateTime.Now.ToString("MMMM yyyy");
                string path = Application.StartupPath + "\\Debug\\" + DateTime.Now.ToString("MMMM yyyy");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = path + "\\DebugLogs_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".txt";
                if (!File.Exists(filepath))
                {
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " : " + Message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " : " + Message);
                    }
                }
            }
            catch (Exception ex)
            {
                //WriteToExFile("Debug Logfile is in Use : " + ex.Message + " : " + ex);
            }
        }
    }
}
