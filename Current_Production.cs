using Microsoft.Reporting.WinForms;
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
using System.Windows.Forms;
using Telerik.Charting;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Current_Production : Telerik.WinControls.UI.RadForm
    {
        public Current_Production()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();  //connection class
        DataTable MO = new DataTable();
        String controller_name = "";
        DataTable data1 = new DataTable();
        DataTable data = new DataTable();
        String old_sam = "1";

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

        DataTable dthourly;
        DataTable dtmoload;
        DataTable dtmoprod;
        DataTable dtmoeff;
        DataTable dtmorepair;
        DataTable dttotal;

        String theme = "";

        private void Current_Production_Load(object sender, EventArgs e)
        {
            dgvmoline.MasterTemplate.SelectLastAddedRow = false;
            RadMessageBox.SetThemeName("FluentDark");  //set message theme
            dgvmoline.MasterView.TableSearchRow.ShowCloseButton = false;  //disable close button of grid search

            //add columns for report
            DataSet SET = new DataSet("SEQ");
            data1.Columns.Add("SEQ_NO");
            data1.Columns.Add("OPCODE");
            data1.Columns.Add("OPDESC");
            data1.Columns.Add("START_TIME");
            data1.Columns.Add("END_TIME");
            data1.Columns.Add("DURATION");
            data1.Columns.Add("TIME_REMAINING");
            data1.Columns.Add("TARGET_PROD");
            data1.Columns.Add("ACTUAL_PROD");
            data1.Columns.Add("REPAIR");
            data1.Columns.Add("POTENTIAL_SAM");
            data1.Columns.Add("ACTUAL_SAM");
            data1.Columns.Add("STATUS");
            data1.Columns.Add("EFFICIENCY");
            data1.Columns.Add("BALANCE_POTENTIAL");
            data1.Columns.Add("TOTAL_P");
            data1.Columns.Add("NO_EMP");
            data1.Columns.Add("TARGET_FOR_DAY");
            data1.Columns.Add("REQ_EMP");
            data1.Columns.Add("MO_NO");
            data1.Columns.Add("MO_LINE");
            data1.Columns.Add("color");
            data1.Columns.Add("size");
            data1.Columns.Add("article");
            data1.Columns.Add("qty");
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
            data1.Columns.Add("U1");
            data1.Columns.Add("U2");
            data1.Columns.Add("U3");
            data1.Columns.Add("U4");
            data1.Columns.Add("U5");
            data1.Columns.Add("U6");
            data1.Columns.Add("U7");
            data1.Columns.Add("U8");
            data1.Columns.Add("U9");
            data1.Columns.Add("U10");

            SET.Tables.Add(data1);

            data.Columns.Add("ACTUAL_PRODUCTION");
            data.Columns.Add("BALANCE_POTENTIAL");
            data.Columns.Add("OPCODE");
            SET.Tables.Add(data);

            dc.OpenConnection();  //open connection
            select_controller();  //get the selected controller

            //set the grid colors
            dgvoperation.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            this.dgvoperation.DefaultCellStyle.ForeColor = Color.Black;
            dgvoperation.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgvoperation.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvoperation.EnableHeadersVisualStyles = false;

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

            //add columns to MO table
            MO.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            MO.Columns.Add("MO No");
            MO.Columns.Add("MO Details");
            MO.Columns.Add("Color");
            MO.Columns.Add("Size");
            MO.Columns.Add("Article");
            MO.Columns.Add("Quantity");
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

            dgvmoline.DataSource = MO;
            //hide the columns which are not enabled
            if (user1 == "")
            {
                dgvmoline.Columns[7].IsVisible = false;
            }

            if (user2 == "")
            {
                dgvmoline.Columns[8].IsVisible = false;
            }

            if (user3 == "")
            {
                dgvmoline.Columns[9].IsVisible = false;
            }

            if (user4 == "")
            {
                dgvmoline.Columns[10].IsVisible = false;
            }

            if (user5 == "")
            {
                dgvmoline.Columns[11].IsVisible = false;
            }

            if (user6 == "")
            {
                dgvmoline.Columns[12].IsVisible = false;
            }

            if (user7 == "")
            {
                dgvmoline.Columns[13].IsVisible = false;
            }

            if (user8 == "")
            {
                dgvmoline.Columns[14].IsVisible = false;
            }

            if (user9 == "")
            {
                dgvmoline.Columns[15].IsVisible = false;
            }

            if (user10 == "")
            {
                dgvmoline.Columns[16].IsVisible = false;
            }

            //get current shift
            cmd = new SqlCommand("SELECT T.V_SHIFT FROM SHIFTS T WHERE CAST(GETDATE() AS TIME) BETWEEN cast(T.T_SHIFT_START_TIME as TIME) AND cast(T.T_OVERTIME_END_TIME as TIME)", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                cmbshift.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            dtpcurrent.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dgvmoline.Columns[0].IsVisible = false;
        }

        public void RowSelected1()
        {
            try
            {
                //clear grids and datatable
                dgvoperation.Rows.Clear();
                data1.Rows.Clear();
                radDropDownList1.Text = "Current Production";

                String article = dgvmoline.SelectedRows[0].Cells[5].Value.ToString();
                int breaktime_remain = 0;
                int breaktime_complete = 0;

                DateTime shift_start = Convert.ToDateTime("00:00:01");
                DateTime shift_end = Convert.ToDateTime("00:00:01");
                DateTime overtime_end = Convert.ToDateTime("00:00:01");
                DateTime current_time = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));

                //get shift details
                SqlCommand cmd = new SqlCommand("select T_SHIFT_START_TIME,T_SHIFT_END_TIME,T_OVERTIME_END_TIME from SHIFTS where V_SHIFT='" + cmbshift.Text + "'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    shift_start = Convert.ToDateTime(sdr.GetValue(0).ToString());
                    shift_end = Convert.ToDateTime(sdr.GetValue(1).ToString());
                    overtime_end = Convert.ToDateTime(sdr.GetValue(2).ToString());
                }
                sdr.Close();

                //get break details for the shift completed
                cmd = new SqlCommand("select I_BREAK_TIMESPAN from SHIFT_BREAKS where CAST(T_BREAK_TIME_END AS TIME) < '" + current_time.ToString("HH:mm:ss") + "' and V_SHIFT='" + cmbshift.Text + "'", dc.con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    breaktime_complete = breaktime_complete + int.Parse(sdr.GetValue(0).ToString());
                }
                sdr.Close();

                //get break details for the shift remaining
                cmd = new SqlCommand("select I_BREAK_TIMESPAN from SHIFT_BREAKS where CAST(T_BREAK_TIME_END AS TIME) > '" + current_time.ToString("HH:mm:ss") + "' and V_SHIFT='" + cmbshift.Text + "'", dc.con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    breaktime_remain = breaktime_remain + int.Parse(sdr.GetValue(0).ToString());
                }
                sdr.Close();

                //check if hide overtime enabled
                String hide_ot = "";
                cmd = new SqlCommand("select HIDE_OVERTIME from Setup", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    hide_ot = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //check if current time > shift end time
                if (current_time > shift_end)
                {
                    current_time = shift_end;
                }

                //calculate the work duration and remain time for the shift
                TimeSpan ts_workduration = shift_end - shift_start;
                TimeSpan ts_timecompleted = current_time - shift_start;
                TimeSpan ts_timeremian = shift_end - current_time;

                int workduration = (int)ts_workduration.TotalMinutes;
                int timecompleted = (int)ts_timecompleted.TotalMinutes;
                int timeremaining = (int)ts_timeremian.TotalMinutes;
                workduration = workduration - (breaktime_complete + breaktime_remain);


                timecompleted = timecompleted - breaktime_complete;
                timeremaining = timeremaining - breaktime_remain;

                if (timecompleted < 0)
                {
                    timecompleted *= -1;
                }

                //if selected date is not current date then time remaining = 0
                if (dtpcurrent.Value.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd"))
                {
                    timeremaining = 0;
                }

                String start_date = dtpcurrent.Value.ToString("yyyy-MM-dd");
                String end_date = dtpcurrent.Value.ToString("yyyy-MM-dd");

                if (shift_start > shift_end)
                {
                    start_date = dtpcurrent.Value.AddDays(-1).ToString("yyyy-MM-dd");
                }

                //check if all shift is selected
                if (cmbshift.Text == "All")
                {
                    shift_start = Convert.ToDateTime("00:00:00");
                    overtime_end = Convert.ToDateTime("23:59:59");
                }

                if (hide_ot == "TRUE")
                {
                    overtime_end = shift_end;
                }

                //get the article id of the mo
                cmd = new SqlCommand("select V_ARTICLE_ID from ARTICLE_DB where V_ARTICLE_DESC='" + article + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    article = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //bar chart 
                BarSeries barSeries1 = new BarSeries("Performance", "RepresentativeName");
                barSeries1.LegendTitle = "Target Production";

                BarSeries barSeries2 = new BarSeries("Performance", "RepresentativeName");
                barSeries2.LegendTitle = "Actual Production";

                BarSeries barSeries3 = new BarSeries("Performance", "RepresentativeName");
                barSeries3.LegendTitle = "Target Production Day";

                BarSeries barSeries4 = new BarSeries("Performance", "RepresentativeName");
                barSeries4.LegendTitle = "Balance Potential";

                //clear chart data
                radChartView1.Series.Clear();
                radChartView2.Series.Clear();

                String mo1 = dgvmoline.SelectedRows[0].Cells[1].Value.ToString();
                String moline1 = dgvmoline.SelectedRows[0].Cells[2].Value.ToString();

                LineSeries lineSeries = new LineSeries();
                lineSeries.LegendTitle = "Piece Count";

                int hanger_count = 1;
                int seq = 0;
                int prev_seq = 0;
                int cur_sam = 0;
                int total_sam = 0;
                int same_seq = 0;

                int seq1 = 1;
                int nextseq = 1;
                int prevseq = 1;
                int curseq = 1;

                //check if same article checkbox is checked
                String query = "";
                if (chksamearticle.Checked == true)
                {
                    query = "select ds.I_SEQUENCE_NO,ds.V_OPERATION_CODE,op.V_OPERATION_DESC,op.D_SAM,ds.I_OPERATION_SEQUENCE_NO from DESIGN_SEQUENCE ds,OPERATION_DB op where ds.V_ARTICLE_ID='" + article + "' and ds.V_OPERATION_CODE=op.V_OPERATION_CODE order by ds.I_SEQUENCE_NO";
                }
                else
                {
                    query = "select ds.I_SEQUENCE_NO,ds.V_OPERATION_CODE,op.V_OPERATION_DESC,op.D_SAM,ds.I_OPERATION_SEQUENCE_NO from DESIGN_SEQUENCE ds, OPERATION_DB op where ds.V_ARTICLE_ID = '" + article + "' and ds.V_OPERATION_CODE = op.V_OPERATION_CODE and ds.I_SEQUENCE_NO IN(select distinct S.I_SEQUENCE_NO from STATION_ASSIGN S, MO_DETAILS M where S.V_MO_NO= '" + mo1 + "' and S.V_MO_LINE= '" + moline1 + "' and S.I_STATION_ID!= 0 and M.V_ASSIGN_TYPE = S.V_ASSIGN_TYPE) order by ds.I_SEQUENCE_NO";
                }

                //get the sequence for the mo
                SqlDataAdapter sda = new SqlDataAdapter(query, dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //reset the sequence 
                    prev_seq = seq;
                    prevseq = seq1;
                    seq1 = int.Parse(dt.Rows[i][0].ToString());

                    if (prevseq == seq1)
                    {
                        nextseq = curseq;
                    }
                    else
                    {
                        nextseq = nextseq + 1;
                    }
                    curseq = nextseq;

                    seq = curseq;
                    //seq = dt.Rows[i][0].ToString();
                    int op_seq = int.Parse(dt.Rows[i][4].ToString());
                    cur_sam = int.Parse(dt.Rows[i][3].ToString());

                    //btnchart.Visible = true;
                    radChartView1.Visible = false;
                    panel5.Visible = true;
                    panel2.Visible = true;

                    decimal target = 0;
                    int qc = 0;
                    int actual_production = 0;
                    int op_completed = 0;
                    int emp = 0;

                    DateTime op_starttime = current_time;
                    DateTime op_endtime = current_time;
                    DateTime prev_opstart;

                    decimal avg_hanger = 0;
                    int mo_count = 0;
                    DateTime prev_opend;

                    //get all the mo used for the date
                    for (int j = 0; j < dgvmoline.Rows.Count; j++)
                    {
                        //check if mo is selected
                        if ((bool)dgvmoline.Rows[j].Cells[0].Value)
                        {
                            String mo = dgvmoline.Rows[j].Cells[1].Value.ToString();
                            String moline = dgvmoline.Rows[j].Cells[2].Value.ToString();

                            //get the hanger count anf target fo the day
                            SqlCommand cmd1 = new SqlCommand("select I_HANGER_COUNT,I_TARGET_DAY from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                            SqlDataReader sdr1 = cmd1.ExecuteReader();
                            if (sdr1.Read())
                            {
                                hanger_count = int.Parse(sdr1.GetValue(0).ToString());
                                target = target + Convert.ToDecimal(sdr1.GetValue(1).ToString());
                            }
                            sdr1.Close();

                            prev_opstart = op_starttime;
                            prev_opend = op_endtime;

                            //get first hanger and the last hanger time and piece count
                            MySqlDataAdapter sda2 = new MySqlDataAdapter("SELECT MIN(TIME),MAX(TIME),SUM(PC_COUNT) FROM stationhistory where SEQ_NO='" + seq + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and TIME>='" + start_date + " " + shift_start.ToString("HH:mm:ss") + "' and TIME<'" + end_date + " " + overtime_end.ToString("HH:mm:ss") + "' order by TIME", dc.conn);
                            DataTable dt2 = new DataTable();
                            sda2.Fill(dt2);
                            sda2.Dispose();
                            int piece_count = 0;
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

                                if (dt2.Rows[0][2].ToString() != "")
                                {
                                    piece_count = int.Parse(dt2.Rows[0][2].ToString());
                                }
                            }

                            //the employee used for the operation
                            sda2 = new MySqlDataAdapter("SELECT distinct EMP_ID FROM stationhistory where SEQ_NO='" + seq + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and TIME>='" + start_date + " " + shift_start.ToString("HH:mm:ss") + "' and TIME<'" + end_date + " " + overtime_end.ToString("HH:mm:ss") + "'", dc.conn);
                            dt2 = new DataTable();
                            sda2.Fill(dt2);
                            sda2.Dispose();
                            for (int m = 0; m < dt2.Rows.Count; m++)
                            {
                                cmd = new SqlCommand("Select count(*) from EMPLOYEE_GROUPS where V_GROUP_ID='" + dt2.Rows[m][0].ToString() + "'", dc.con);
                                int group = int.Parse(cmd.ExecuteScalar() + "");

                                if (group == 0)
                                {
                                    emp = emp + 1;
                                }
                                else
                                {
                                    emp = emp + group;
                                }
                            }

                            //get the qc count for that operation
                            cmd = new SqlCommand("Select sum(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + start_date + " " + shift_start.ToString("HH:mm:ss") + "' and D_DATE_TIME<'" + end_date + " " + overtime_end.ToString("HH:mm:ss") + "' and V_OP_CODE='" + dt.Rows[i][1].ToString() + "' and V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                            String temp = cmd.ExecuteScalar().ToString();
                            if (temp != "")
                            {
                                qc = qc + int.Parse(temp + "");
                            }
                            else
                            {
                                qc = qc + 0;
                            }

                            //add the production
                            actual_production = actual_production + piece_count;

                            avg_hanger = avg_hanger + hanger_count;
                            mo_count = mo_count + 1;
                        }
                    }
                        
                    if (mo_count > 0)
                    {
                        avg_hanger = avg_hanger / mo_count; 
                    }

                    //get total time taken for the operation
                    TimeSpan ts_op_completed = Convert.ToDateTime(op_endtime.ToString("HH:mm:ss")) - Convert.ToDateTime(op_starttime.ToString("HH:mm:ss"));
                    op_completed = (int)ts_op_completed.TotalSeconds;
                    decimal sam = Convert.ToDecimal(dt.Rows[i][3].ToString());

                    decimal target_production = 0;
                    if (sam >= 0)
                    {
                        target_production = op_completed / sam;
                    }

                    //calculated actual sam
                    decimal actual_sam = 0;
                    if (actual_production > 0)
                    {
                        actual_sam = (decimal)op_completed / (decimal)actual_production;
                    }

                    //calculate the propotional sam for each sam
                    actual_sam = Convert.ToDecimal(actual_sam.ToString("0.##"));
                    if (prev_seq == seq)
                    {
                        same_seq = same_seq + 1;
                        //get sum of sam for the operation
                        SqlCommand cmd3 = new SqlCommand("select sum(op.D_SAM) from OPERATION_DB op,DESIGN_SEQUENCE ds  where ds.V_ARTICLE_ID='" + article + "' and ds.V_OPERATION_CODE=op.V_OPERATION_CODE and ds.I_OPERATION_SEQUENCE_NO<='" + op_seq + "' and ds.I_SEQUENCE_NO='" + seq + "'", dc.con);
                        total_sam = int.Parse(cmd3.ExecuteScalar().ToString());

                        //calculate the propotional actual sam

                        decimal propotional_sam = 0;
                        if (total_sam > 0)
                        {
                            propotional_sam = actual_sam / total_sam;
                        }
                        actual_sam = cur_sam * propotional_sam;

                        for (int m = 0; m < same_seq; m++)
                        {
                            //update the calculation
                            int n = m + 1;
                            decimal potentail_rem1 = 0;
                            decimal efficiency1 = 0;

                            //calculate the target production with the propotional sam
                            int sam1 = int.Parse(dgvoperation.Rows[i - n].Cells[11].Value.ToString());
                            decimal actual_sam1 = sam1 * propotional_sam;
                            actual_sam1 = Convert.ToDecimal(actual_sam1.ToString("0.##"));

                            decimal target_production1 = 0;
                            if (sam1 > 0)
                            {
                                target_production1 = op_completed / sam1;
                            }

                            //calculate efficeincy and potential production
                            if (actual_sam1 > 0)
                            {
                                efficiency1 = (sam1 / actual_sam1) * 100;
                                potentail_rem1 = timeremaining * 60 / actual_sam1;
                            }

                            //calculate potential production for the day
                            int potential_day1 = (int)potentail_rem1 + actual_production;
                            //target production for the day
                            decimal target_day1 = 0;
                            if (sam > 0)
                            {
                                target_day1 = workduration / sam;
                            }

                            //required employees to reach the target
                            decimal req_emp1 = 0;
                            if (target > 0)
                            {
                                req_emp1 = potential_day1 / target * 100;
                                req_emp1 = 100 / req_emp1;
                            }

                            //calculate hourly average production 
                            decimal average_production1 = 0;
                            decimal hour1 = (decimal)op_completed / 60;
                            if (hour1 != 0)
                            {
                                average_production1 = actual_production / hour1;
                            }

                            //get the qc count for the operation
                            String query1 = "Select sum(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + start_date + " " + shift_start.ToString("HH:mm:ss") + "' and D_DATE_TIME<'" + end_date + " " + overtime_end.ToString("HH:mm:ss") + "' and V_OP_CODE='" + dt.Rows[i - n][1].ToString() + "' and V_MO_NO='" + mo1 + "' and V_MO_LINE='" + moline1 + "'";
                            if (chksamearticle.Checked == true && mo_count > 1)
                            {
                                query1 = "Select sum(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + start_date + " " + shift_start.ToString("HH:mm:ss") + "' and D_DATE_TIME<'" + end_date + " " + overtime_end.ToString("HH:mm:ss") + "' and V_OP_CODE='" + dt.Rows[i - n][1].ToString() + "'";
                            }

                            cmd = new SqlCommand(query1, dc.con);
                            String temp = cmd.ExecuteScalar().ToString();
                            if (temp != "")
                            {
                                qc = qc + int.Parse(temp + "");
                            }
                            else
                            {
                                qc = qc + 0;
                            }

                            //update the grid and chart
                            dgvoperation.Rows.RemoveAt(i - n);
                            dgvoperation.Rows.Add(seq1, dt.Rows[i - n][1].ToString(), dt.Rows[i - n][2].ToString(), op_starttime.ToString("HH:mm:ss"), op_endtime.ToString("HH:mm:ss"), (int)op_completed / 60, timeremaining, (int)target_production1, actual_production, "", qc, dt.Rows[i - n][3].ToString(), actual_sam1.ToString("0.##"), average_production1.ToString("0.##"), efficiency1.ToString("0.##"), "", (int)potentail_rem1, potential_day1, emp, target, req_emp1.ToString("0.##"));

                            barSeries1.DataPoints.RemoveAt(i - n);
                            barSeries2.DataPoints.RemoveAt(i - n);
                            barSeries3.DataPoints.RemoveAt(i - n);
                            barSeries4.DataPoints.RemoveAt(i - n);

                            target_day1 = target_day1 - target_production1;
                            potential_day1 = potential_day1 - actual_production;

                            barSeries1.DataPoints.Add(new CategoricalDataPoint((int)target_production1, dt.Rows[i - n][2].ToString()));
                            barSeries3.DataPoints.Add(new CategoricalDataPoint((int)target_day1, dt.Rows[i - n][2].ToString()));
                            barSeries2.DataPoints.Add(new CategoricalDataPoint(actual_production, dt.Rows[i - n][2].ToString()));
                            barSeries4.DataPoints.Add(new CategoricalDataPoint(potential_day1, dt.Rows[i - n][2].ToString()));
                        }
                    }
                    else
                    {
                        same_seq = 0;
                    }


                    decimal potentail_rem = 0;
                    decimal efficiency = 0;

                    //calculate efficiency and potential production
                    if (actual_sam > 0)
                    {
                        efficiency = (sam / actual_sam) * 100;
                        potentail_rem = timeremaining * 60 / actual_sam;
                    }

                    //calculate potential production for the day and target for the day
                    int potential_day = (int)potentail_rem + actual_production;
                    decimal target_day = 0;
                    if (sam > 0)
                    {
                        target_day = workduration / sam;
                    }

                    //calculate employees required to reach target
                    decimal req_emp = 0;
                    if (target > 0)
                    {
                        req_emp = potential_day / target * 100;
                        if (req_emp > 0)
                        {
                            req_emp = 100 / req_emp;
                        }
                    }

                    //calculate hourly average production
                    decimal average_production = 0;
                    decimal hour = (decimal)op_completed / 3600;
                    if (hour > 0)
                    {
                        average_production = actual_production / hour;
                    }

                    //add to grid
                    op_completed = op_completed / 60;
                    dgvoperation.Rows.Add(seq1, dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), op_starttime.ToString("HH:mm:ss"), op_endtime.ToString("HH:mm:ss"), op_completed, timeremaining, (int)target_production, actual_production, "", qc, dt.Rows[i][3].ToString(), actual_sam.ToString("0.##"), average_production.ToString("0.##"), efficiency.ToString("0.##"), "", (int)potentail_rem, potential_day, emp, target, req_emp.ToString("0.##"));

                    dgvoperation.Rows[i].Cells[0].ToolTipText = seq1.ToString();

                    target_day = target_day - target_production;
                    potential_day = potential_day - actual_production;

                    barSeries1.DataPoints.Add(new CategoricalDataPoint((int)target_production, dt.Rows[i][2].ToString()));
                    barSeries3.DataPoints.Add(new CategoricalDataPoint((int)target_day, dt.Rows[i][2].ToString()));
                    barSeries2.DataPoints.Add(new CategoricalDataPoint(actual_production, dt.Rows[i][2].ToString()));
                    barSeries4.DataPoints.Add(new CategoricalDataPoint(potential_day, dt.Rows[i][2].ToString()));
                }

                //get the operation
                for (int i = 0; i < dgvoperation.Rows.Count; i++)
                {
                    //change the color of cells for efficiency and target
                    decimal eff = Convert.ToDecimal(dgvoperation.Rows[i].Cells[14].Value.ToString());
                    String clr = setcolor((int)eff);
                    String[] clr1 = clr.Split(',');
                    int red = int.Parse(clr1[0]);
                    int green = int.Parse(clr1[1]);

                    decimal target_production = Convert.ToDecimal(dgvoperation.Rows[i].Cells[7].Value.ToString());
                    int actual_production = int.Parse(dgvoperation.Rows[i].Cells[8].Value.ToString());

                    dgvoperation.Rows[i].Cells[15].Style = new DataGridViewCellStyle { BackColor = Color.FromArgb(red, green, 0) };
                    dgvoperation.Rows[i].Cells[14].Value = eff.ToString("0.##") + "%";

                    if (target_production > actual_production)
                    {
                        dgvoperation.Rows[i].Cells[9].Style = new DataGridViewCellStyle { BackColor = Color.Red };
                    }
                    else if (target_production < actual_production)
                    {
                        dgvoperation.Rows[i].Cells[9].Style = new DataGridViewCellStyle { BackColor = Color.FromArgb(54, 255, 0) };
                    }
                    else
                    {
                        dgvoperation.Rows[i].Cells[9].Style = new DataGridViewCellStyle { BackColor = Color.Yellow };
                    }
                }

                //remove the rows with no production
                for (int i = 0; i < dgvoperation.Rows.Count; i++)
                {
                    if (dgvoperation.Rows[i].Cells[3].Value.ToString() == dgvoperation.Rows[i].Cells[4].Value.ToString())
                    {
                        dgvoperation.Rows.RemoveAt(i);
                        i = 0;
                    }
                }

                //chart properties
                barSeries1.CombineMode = ChartSeriesCombineMode.Stack;
                barSeries2.CombineMode = ChartSeriesCombineMode.Stack;
                barSeries3.CombineMode = ChartSeriesCombineMode.Stack;
                barSeries4.CombineMode = ChartSeriesCombineMode.Stack;

                barSeries1.StackGroupKey = 1;
                barSeries3.StackGroupKey = 1;
                barSeries2.StackGroupKey = 2;
                barSeries4.StackGroupKey = 2;


                barSeries1.ForeColor = Color.White;
                barSeries3.ForeColor = Color.White;
                barSeries2.ForeColor = Color.White;
                barSeries4.ForeColor = Color.White;

                //radChartView1.Series.Add(barSeries1);
                radChartView1.Series.Add(barSeries2);
                //radChartView1.Series.Add(barSeries3);
                radChartView1.Series.Add(barSeries4);

                barSeries2.ShowLabels = true;
                barSeries4.ShowLabels = true;

                //set vertical axis propeties
                LinearAxis verticalAxis1 = radChartView1.Axes[1] as LinearAxis;
                verticalAxis1.LabelFitMode = AxisLabelFitMode.MultiLine;
                verticalAxis1.ForeColor = Color.White;
                verticalAxis1.BorderColor = Color.DodgerBlue;
                verticalAxis1.ShowLabels = false;
                verticalAxis1.Title = "Piece Count";

                //set horizontal axis properties
                CategoricalAxis ca1 = radChartView1.Axes[0] as CategoricalAxis;
                ca1.LabelFitMode = AxisLabelFitMode.MultiLine;
                ca1.Title = "Operations";
                ca1.ForeColor = Color.White;
                ca1.BorderColor = Color.DodgerBlue;

                radChartView1.ForeColor = Color.White;

                //get all the mo details
                for (int i = 0; i < dgvmoline.Rows.Count; i++)
                {
                    //check if the mo is selected
                    if ((bool)dgvmoline.Rows[i].Cells[0].Value)
                    {
                        String mo = dgvmoline.Rows[i].Cells[1].Value.ToString();
                        String moline = dgvmoline.Rows[i].Cells[2].Value.ToString();

                        //get the hourly production
                        MySqlDataAdapter sda5 = new MySqlDataAdapter("SELECT HOUR(TIME),COUNT(HANGER_ID) FROM stationhistory  where TIME>='" + dtpcurrent.Value.ToString("yyyy-MM-dd") + " " + shift_start.ToString("HH:mm:ss") + "' and TIME<'" + dtpcurrent.Value.ToString("yyyy-MM-dd") + " " + overtime_end.ToString("HH:mm:ss") + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='2' GROUP BY HOUR(TIME) ORDER BY HOUR(TIME)", dc.conn);
                        DataTable dt5 = new DataTable();
                        sda5.Fill(dt5);
                        sda5.Dispose();
                        for (int p = 0; p < dt5.Rows.Count; p++)
                        {
                            panel5.Visible = true;
                            int count = int.Parse(dt5.Rows[p][1].ToString());
                            lineSeries.DataPoints.Add(new CategoricalDataPoint(count, dt5.Rows[p][0].ToString() + ":00:00"));
                        }
                    }
                }

                radChartView2.Series.Add(lineSeries);
                lineSeries.ShowLabels = true;

                //set vertical axis properties
                LinearAxis verticalAxis = radChartView2.Axes[1] as LinearAxis;
                verticalAxis.ForeColor = Color.White;
                verticalAxis.BorderColor = Color.DodgerBlue;
                verticalAxis.ShowLabels = false;
                verticalAxis.Title = "Piece Count";

                //set horizontal axis properties
                CategoricalAxis ca = radChartView2.Axes[0] as CategoricalAxis;
                ca.LabelFitMode = AxisLabelFitMode.Rotate;
                ca.Title = "Time";
                ca.LabelRotationAngle = 270;
                ca.ForeColor = Color.White;
                ca.BorderColor = Color.DodgerBlue;

                radChartView2.Series[0].BackColor = Color.White;
                radChartView2.Series[0].BorderColor = Color.White;
            }
            catch (Exception ex)
            {
                radLabel8.Text = ex.Message;
            }
        }

        public void Report()
        {
            try
            {
                //clear the datatables
                data1.Rows.Clear();
                data.Rows.Clear();

                String article = dgvmoline.SelectedRows[0].Cells[5].Value.ToString();
                int breaktime_remain = 0;
                int breaktime_complete = 0;

                DateTime shift_start = Convert.ToDateTime("00:00:01");
                DateTime shift_end = Convert.ToDateTime("00:00:01");
                DateTime overtime_end = Convert.ToDateTime("00:00:01");
                DateTime current_time = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));

                //get the shift details
                SqlCommand cmd = new SqlCommand("select T_SHIFT_START_TIME,T_SHIFT_END_TIME,T_OVERTIME_END_TIME from SHIFTS where V_SHIFT='" + cmbshift.Text + "'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    shift_start = Convert.ToDateTime(sdr.GetValue(0).ToString());
                    shift_end = Convert.ToDateTime(sdr.GetValue(1).ToString());
                    overtime_end = Convert.ToDateTime(sdr.GetValue(2).ToString());
                }
                sdr.Close();

                //get the shift time completed
                cmd = new SqlCommand("select I_BREAK_TIMESPAN from SHIFT_BREAKS where CAST(T_BREAK_TIME_END AS TIME) < '" + current_time.ToString("HH:mm:ss") + "'", dc.con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    breaktime_complete = breaktime_complete + int.Parse(sdr.GetValue(0).ToString());
                }
                sdr.Close();

                //get shift time remaining
                cmd = new SqlCommand("select I_BREAK_TIMESPAN from SHIFT_BREAKS where CAST(T_BREAK_TIME_END AS TIME) > '" + current_time.ToString("HH:mm:ss") + "'", dc.con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    breaktime_remain = breaktime_remain + int.Parse(sdr.GetValue(0).ToString());
                }
                sdr.Close();

                //check if the hide overtime is enabled
                String hide_ot = "";
                cmd = new SqlCommand("select HIDE_OVERTIME from Setup", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    hide_ot = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                if (current_time > shift_end)
                {
                    current_time = shift_end;
                }

                //calculate the work duration and time remaining
                TimeSpan ts_workduration = shift_end - shift_start;
                TimeSpan ts_timecompleted = current_time - shift_start;
                TimeSpan ts_timeremian = shift_end - current_time;

                int workduration = (int)ts_workduration.TotalMinutes;
                int timecompleted = (int)ts_timecompleted.TotalMinutes;
                int timeremaining = (int)ts_timeremian.TotalMinutes;
                workduration = workduration - (breaktime_complete + breaktime_remain);

                timecompleted = timecompleted - breaktime_complete;
                timeremaining = timeremaining - breaktime_remain;

                if (timecompleted < 0)
                {
                    timecompleted *= -1;
                }

                if (timeremaining < 0)
                {
                    timeremaining = 0;
                }

                String start_date = dtpcurrent.Value.ToString("yyyy-MM-dd");
                String end_date = dtpcurrent.Value.ToString("yyyy-MM-dd");
                if (shift_start > shift_end)
                {
                    start_date = dtpcurrent.Value.AddDays(-1).ToString("yyyy-MM-dd");
                }

                //check if the all shift is selected
                if (cmbshift.Text == "All")
                {
                    shift_start = Convert.ToDateTime("00:00:00");
                    overtime_end = Convert.ToDateTime("23:59:59");
                }

                //check if hide overtime is selected
                if (hide_ot == "TRUE")
                {
                    overtime_end = shift_end;
                }

                //get the article id for the mo
                cmd = new SqlCommand("select V_ARTICLE_ID from ARTICLE_DB where V_ARTICLE_DESC='" + article + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    article = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get the all the mo
                for (int j = 0; j < dgvmoline.Rows.Count; j++)
                {
                    //check if mo is selected
                    if ((bool)dgvmoline.Rows[j].Cells[0].Value)
                    {
                        String mo = dgvmoline.Rows[j].Cells[1].Value.ToString();
                        String moline = dgvmoline.Rows[j].Cells[2].Value.ToString();
                        String query = "select ds.I_SEQUENCE_NO,ds.V_OPERATION_CODE,op.V_OPERATION_DESC,op.D_SAM,ds.I_OPERATION_SEQUENCE_NO from DESIGN_SEQUENCE ds, OPERATION_DB op where ds.V_ARTICLE_ID = '" + article + "' and ds.V_OPERATION_CODE = op.V_OPERATION_CODE and ds.I_SEQUENCE_NO IN(select distinct S.I_SEQUENCE_NO from STATION_ASSIGN S, MO_DETAILS M where S.V_MO_NO= '" + mo + "' and S.V_MO_LINE= '" + moline + "' and S.I_STATION_ID!= 0 and M.V_ASSIGN_TYPE = S.V_ASSIGN_TYPE) order by ds.I_SEQUENCE_NO";

                        int seq = 0;
                        int prev_seq = 0;
                        int cur_sam = 0;
                        int total_sam = 0;
                        int same_seq = 0;

                        int seq1 = 1;
                        int nextseq = 1;
                        int prevseq = 1;
                        int curseq = 1;

                        //get the sequences for the mo
                        SqlDataAdapter sda = new SqlDataAdapter(query, dc.con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        sda.Dispose();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            prev_seq = seq;
                            //reset the sequence
                            prevseq = seq1;
                            seq1 = int.Parse(dt.Rows[i][0].ToString());
                            if (prevseq == seq1)
                            {
                                nextseq = curseq;
                            }
                            else
                            {
                                nextseq = nextseq + 1;
                            }
                            curseq = nextseq;

                            seq = curseq;
                            int op_seq = int.Parse(dt.Rows[i][4].ToString());
                            cur_sam = int.Parse(dt.Rows[i][3].ToString());

                            decimal target = 0;
                            int qc = 0;
                            int actual_production = 0;
                            int op_completed = 0;
                            int emp = 0;

                            DateTime op_starttime = current_time;
                            DateTime op_endtime = current_time;
                            DateTime prev_opstart;
                            DateTime prev_opend;

                            //get the hanger count and target for the day
                            SqlCommand cmd1 = new SqlCommand("select I_HANGER_COUNT,I_TARGET_DAY from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                            SqlDataReader sdr1 = cmd1.ExecuteReader();
                            if (sdr1.Read())
                            {
                                target = target + Convert.ToDecimal(sdr1.GetValue(1).ToString());
                            }
                            sdr1.Close();

                            prev_opstart = op_starttime;
                            prev_opend = op_endtime;

                            //get the first hanger and last hanger and production 
                            MySqlDataAdapter sda2 = new MySqlDataAdapter("SELECT MIN(TIME),MAX(TIME),SUM(PC_COUNT) FROM stationhistory where SEQ_NO='" + seq + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and TIME>='" + start_date + " " + shift_start.ToString("HH:mm:ss") + "' and TIME<'" + end_date + " " + overtime_end.ToString("HH:mm:ss") + "' order by TIME", dc.conn);
                            DataTable dt2 = new DataTable();
                            sda2.Fill(dt2);
                            sda2.Dispose();
                            int piece_count = 0;
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

                                if (dt2.Rows[0][2].ToString() != "")
                                {
                                    piece_count = int.Parse(dt2.Rows[0][2].ToString());
                                }
                            }

                            //get employees used for the operation
                            sda2 = new MySqlDataAdapter("SELECT distinct EMP_ID FROM stationhistory where SEQ_NO='" + seq + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and TIME>='" + start_date + " " + shift_start.ToString("HH:mm:ss") + "' and TIME<'" + end_date + " " + overtime_end.ToString("HH:mm:ss") + "'", dc.conn);
                            dt2 = new DataTable();
                            sda2.Fill(dt2);
                            sda2.Dispose();
                            for (int m = 0; m < dt2.Rows.Count; m++)
                            {
                                cmd = new SqlCommand("Select count(*) from EMPLOYEE_GROUPS where V_GROUP_ID='" + dt2.Rows[m][0].ToString() + "'", dc.con);
                                int group = int.Parse(cmd.ExecuteScalar() + "");
                                if (group == 0)
                                {
                                    emp = emp + 1;
                                }
                                else
                                {
                                    emp = emp + group;
                                }
                            }

                            //get the count of qc for the operation
                            cmd = new SqlCommand("Select sum(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + start_date + " " + shift_start.ToString("HH:mm:ss") + "' and D_DATE_TIME<'" + end_date + " " + overtime_end.ToString("HH:mm:ss") + "' and V_OP_CODE='" + dt.Rows[i][1].ToString() + "' and V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                            String temp = cmd.ExecuteScalar().ToString();
                            if (temp != "")
                            {
                                qc = qc + int.Parse(temp + "");
                            }
                            else
                            {
                                qc = qc + 0;
                            }

                            actual_production = actual_production + piece_count;

                            //calculate target production and time taken for the operation
                            TimeSpan ts_op_completed = Convert.ToDateTime(op_endtime.ToString("HH:mm:ss")) - Convert.ToDateTime(op_starttime.ToString("HH:mm:ss"));
                            op_completed = (int)ts_op_completed.TotalSeconds;
                            decimal sam = Convert.ToDecimal(dt.Rows[i][3].ToString());
                            decimal target_production = 0;
                            if (sam > 0)
                            {
                                target_production = op_completed / sam;
                            }

                            decimal actual_sam = 0;
                            if (actual_production > 0)
                            {
                                actual_sam = (decimal)op_completed / (decimal)actual_production;
                            }

                            actual_sam = Convert.ToDecimal(actual_sam.ToString("0.##"));

                            if (prev_seq == seq)
                            {
                                //get the sum of sam for the operation
                                same_seq = same_seq + 1;
                                SqlCommand cmd3 = new SqlCommand("select sum(op.D_SAM) from OPERATION_DB op,DESIGN_SEQUENCE ds  where ds.V_ARTICLE_ID='" + article + "' and ds.V_OPERATION_CODE=op.V_OPERATION_CODE and ds.I_OPERATION_SEQUENCE_NO<='" + op_seq + "' and ds.I_SEQUENCE_NO='" + seq + "'", dc.con);
                                total_sam = int.Parse(cmd3.ExecuteScalar().ToString());

                                //calculate the propotional sam for the operation
                                decimal propotional_sam = 0;
                                if (total_sam > 0)
                                {
                                    propotional_sam = actual_sam / total_sam;
                                }
                                actual_sam = cur_sam * propotional_sam;

                                for (int m = 0; m < same_seq; m++)
                                {
                                    int n = m + 1;
                                    decimal potentail_rem1 = 0;
                                    decimal efficiency1 = 0;

                                    //calculate the target production
                                    int sam1 = int.Parse(dgvoperation.Rows[i - n].Cells[11].Value.ToString());
                                    decimal actual_sam1 = sam1 * propotional_sam;
                                    actual_sam1 = Convert.ToDecimal(actual_sam1.ToString("0.##"));
                                    decimal target_production1 = 0;
                                    if (sam1 > 0)
                                    {
                                        target_production1 = op_completed / sam1;
                                    }

                                    //calculate efficiency and potential production
                                    if (actual_sam1 > 0)
                                    {
                                        efficiency1 = (sam1 / actual_sam1) * 100;
                                        potentail_rem1 = timeremaining * 60 / actual_sam1;
                                    }
                                    //calculate target for the day and potential for the day
                                    int potential_day1 = (int)potentail_rem1 + actual_production;
                                    decimal target_day1 = 0;
                                    if (sam > 0)
                                    {
                                        target_day1 = workduration / sam;
                                    }

                                    //calcualate required employees to reach the target
                                    decimal req_emp1 = 0;
                                    if (target > 0)
                                    {
                                        req_emp1 = potential_day1 / target * 100;
                                        req_emp1 = 100 / req_emp1;
                                    }

                                    //calculate average hourly production
                                    decimal average_production1 = 0;
                                    decimal hour1 = (decimal)op_completed / 60;
                                    if (hour1 != 0)
                                    {
                                        average_production1 = actual_production / hour1;
                                    }

                                    data1.Rows.RemoveAt(i - n);
                                    data.Rows.RemoveAt(i - n);
                                    data1.Rows.Add(seq1, dt.Rows[i - n][1].ToString(), dt.Rows[i - n][2].ToString(), op_starttime.ToString("HH:mm:ss"), op_endtime.ToString("HH:mm:ss"), (int)op_completed / 60, timeremaining, (int)target_production1, actual_production, qc, dt.Rows[i - n][3].ToString(), actual_sam1.ToString("0.##"), average_production1.ToString("0.##"), efficiency1.ToString("0.##") + "%", (int)potentail_rem1, potential_day1, emp, target, req_emp1.ToString("0.##"), mo, moline, dgvmoline.Rows[j].Cells[3].Value.ToString(), dgvmoline.Rows[j].Cells[4].Value.ToString(), dgvmoline.Rows[j].Cells[5].Value.ToString(), dgvmoline.Rows[j].Cells[6].Value.ToString(), dgvmoline.Rows[j].Cells[7].Value.ToString(), dgvmoline.Rows[j].Cells[8].Value.ToString(), dgvmoline.Rows[j].Cells[9].Value.ToString(), dgvmoline.Rows[j].Cells[10].Value.ToString(), dgvmoline.Rows[j].Cells[11].Value.ToString(), dgvmoline.Rows[j].Cells[12].Value.ToString(), dgvmoline.Rows[j].Cells[13].Value.ToString(), dgvmoline.Rows[j].Cells[14].Value.ToString(), dgvmoline.Rows[j].Cells[15].Value.ToString(), dgvmoline.Rows[j].Cells[16].Value.ToString(), user1, user2, user3, user4, user5, user6, user7, user8, user9, user10);
                                    data.Rows.Add(actual_production, (int)potentail_rem1, dt.Rows[i - n][2].ToString());
                                }
                            }
                            else
                            {
                                same_seq = 0;
                            }

                            decimal potentail_rem = 0;
                            decimal efficiency = 0;

                            //calculate efficiency and potential production
                            if (actual_sam > 0)
                            {
                                efficiency = (sam / actual_sam) * 100;
                                potentail_rem = timeremaining * 60 / actual_sam;
                            }

                            //calculate potential for the day and target for the day
                            int potential_day = (int)potentail_rem + actual_production;
                            decimal target_day = 0;
                            if (sam > 0)
                            {
                                target_day = workduration / sam;
                            }

                            //calculate require employees to reach target
                            decimal req_emp = 0;
                            if (target > 0)
                            {
                                req_emp = potential_day / target * 100;
                                req_emp = 100 / req_emp;
                            }

                            //calculate average hourly production
                            decimal average_production = 0;
                            decimal hour = (decimal)op_completed / 3600;
                            if (hour != 0)
                            {
                                average_production = actual_production / hour;
                            }

                            op_completed = op_completed / 60;

                            //add to grid
                            data.Rows.Add(actual_production, (int)potentail_rem, dt.Rows[i][2].ToString());
                            data1.Rows.Add(seq1, dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), op_starttime.ToString("HH:mm:ss"), op_endtime.ToString("HH:mm:ss"), op_completed, timeremaining, (int)target_production, actual_production, qc, dt.Rows[i][3].ToString(), actual_sam.ToString("0.##"), average_production.ToString("0.##"), efficiency.ToString("0.##") + "%", (int)potentail_rem, potential_day, emp, target, req_emp.ToString("0.##"), mo, moline, dgvmoline.Rows[j].Cells[3].Value.ToString(), dgvmoline.Rows[j].Cells[4].Value.ToString(), dgvmoline.Rows[j].Cells[5].Value.ToString(), dgvmoline.Rows[j].Cells[6].Value.ToString(), dgvmoline.Rows[j].Cells[7].Value.ToString(), dgvmoline.Rows[j].Cells[8].Value.ToString(), dgvmoline.Rows[j].Cells[9].Value.ToString(), dgvmoline.Rows[j].Cells[10].Value.ToString(), dgvmoline.Rows[j].Cells[11].Value.ToString(), dgvmoline.Rows[j].Cells[12].Value.ToString(), dgvmoline.Rows[j].Cells[13].Value.ToString(), dgvmoline.Rows[j].Cells[14].Value.ToString(), dgvmoline.Rows[j].Cells[15].Value.ToString(), dgvmoline.Rows[j].Cells[16].Value.ToString(), user1, user2, user3, user4, user5, user6, user7, user8, user9, user10);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                radLabel8.Text = ex.Message;
            }
        }

        private void btnchart_Click(object sender, EventArgs e)
        {
            if (btnchart.Text == "Chart View")
            {
                radChartView1.Visible = true;
                btnchart.Text = "Table View";
                reportViewer1.Visible = false;
                return;
            }

            if (btnchart.Text == "Table View")
            {
                radChartView1.Visible = false;
                btnchart.Text = "Chart View";
                reportViewer1.Visible = false;
                return;
            }

            tmrautorefresh.Start();
        }

        private void dtpcurrent_ValueChanged(object sender, EventArgs e)
        {
            //get the selected controller
            if (controller_name == "--SELECT--")
            {
                radLabel8.Text = "Please Select a Controller";
                return;
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

            MO.Rows.Clear();
            btnchart.Visible = false;
            panel5.Visible = false;
            panel2.Visible = false;
            radChartView1.Visible = false;

            //check if the date is in hide date
            SqlCommand cmd = new SqlCommand("select count(*) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE' and CONVERT(nvarchar(10),D_HIDEDAY, 120)='" + dtpcurrent.Value.ToString("yyyy-MM-dd") + "'", dc.con);
            int count = int.Parse(cmd.ExecuteScalar().ToString());
            if (count > 0)
            {
                return;
            }

            //get the mo used for the day
            MySqlDataAdapter sda = new MySqlDataAdapter("select distinct MO_NO,MO_LINE from stationhistory where TIME>='" + dtpcurrent.Value.ToString("yyyy-MM-dd") + " 00:00:00' and TIME<'" + dtpcurrent.Value.ToString("yyyy-MM-dd") + " 23:59:59'", dc.conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //get the mo details
                SqlDataAdapter sda1 = new SqlDataAdapter("SELECT V_MO_NO, V_COLOR_ID, V_SIZE_ID, V_ARTICLE_ID, I_ORDER_QTY, V_USER_DEF1, V_USER_DEF2, V_USER_DEF3, V_USER_DEF4, V_USER_DEF5, V_USER_DEF6, V_USER_DEF7, V_USER_DEF8, V_USER_DEF9, V_USER_DEF10 FROM MO_DETAILS where V_MO_NO = '" + dt.Rows[i][0].ToString() + "' and V_MO_LINE='" + dt.Rows[i][1].ToString() + "'", dc.con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                sda1.Dispose();
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    String color = dt1.Rows[j][1].ToString();
                    String size = dt1.Rows[j][2].ToString();
                    String article = dt1.Rows[j][3].ToString();
                    String qty = dt1.Rows[j][4].ToString();
                    user1 = dt1.Rows[j][5].ToString();
                    user2 = dt1.Rows[j][6].ToString();
                    user3 = dt1.Rows[j][7].ToString();
                    user4 = dt1.Rows[j][8].ToString();
                    user5 = dt1.Rows[j][9].ToString();
                    user6 = dt1.Rows[j][10].ToString();
                    user7 = dt1.Rows[j][11].ToString();
                    user8 = dt1.Rows[j][12].ToString();
                    user9 = dt1.Rows[j][13].ToString();
                    user10 = dt1.Rows[j][14].ToString();

                    //get the description of the master
                    cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        color = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the description of the master
                    cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + article + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        article = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the description of the master
                    cmd = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        size = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the description of the master
                    cmd = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + user1 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user1 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the description of the master
                    cmd = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + user2 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user2 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the description of the master
                    cmd = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + user3 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user3 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the description of the master
                    cmd = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + user4 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user4 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the description of the master
                    cmd = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + user5 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user5 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the description of the master
                    cmd = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + user6 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user6 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the description of the master
                    cmd = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + user7 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user7 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the description of the master
                    cmd = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + user8 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user8 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the description of the master
                    cmd = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + user9 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user9 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get the description of the master
                    cmd = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + user10 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user10 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //add the rows to the mo grid view
                    MO.Rows.Add(false, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), color, size, article, qty, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10);
                }

                dgvmoline.DataSource = MO;

                //check if mo is selected
                if (dgvmoline.SelectedRows.Count > 0)
                {
                    dgvmoline.SelectedRows[0].Cells[0].Value = true;

                    btnchart.Visible = false;
                    panel5.Visible = false;
                    panel2.Visible = false;
                    radChartView1.Visible = false;
                    btnhourlyproduction.Visible = false;

                    btnhourlyproduction.Text = "Hourly Production";
                    btnchart.Text = "Chart View";
                    panel1.Visible = false;

                    RowSelected1();
                }
            }
        }

        private void radLabel8_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            radPanel2.Visible = true;
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
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
            dc.OpenConnection();  //open connection
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

            //get the ipaddess
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
            dc.OpenMYSQLConnection(ipaddress);  //open connection
        }

        private void Current_Production_FormClosed(object sender, FormClosedEventArgs e)
        {
            //close connection on form close
            dc.Close_Connection();
            tmrautorefresh.Stop();
        }

        public String setcolor(int i)
        {
            //set color of the cell
            int red = 0;
            int green = 0;

            if (i > 100)
            {
                red = 54;
                green = 255;
            }
            else if (i == 100)
            {
                red = 209;
                green = 255;
            }
            else if (i >= 95)
            {
                red = 251;
                green = 255;
            }
            else if (i >= 90)
            {
                red = 255;
                green = 228;
            }
            else if (i >= 85)
            {
                red = 255;
                green = 207;
            }
            else if (i >= 80)
            {
                red = 255;
                green = 166;
            }
            else if (i >= 75)
            {
                red = 255;
                green = 135;
            }
            else if (i >= 70)
            {
                red = 255;
                green = 104;
            }
            else if (i >= 65)
            {
                red = 255;
                green = 73;
            }
            else if (i >= 60)
            {
                red = 255;
                green = 47;
            }
            else if (i >= 55)
            {
                red = 255;
                green = 18;
            }
            else
            {
                red = 255;
                green = 0;
            }

            return (red + "," + green);
        }

        private void btnhourlyproduction_Click(object sender, EventArgs e)
        {
            if (btnhourlyproduction.Text == "Hourly Production")
            {
                radChartView1.Visible = true;
                btnchart.Text = "Table View";
                panel1.Visible = true;
                reportViewer1.Visible = false;
                btnhourlyproduction.Text = "Current Production";
            }
            else
            {
                panel1.Visible = false;
                reportViewer1.Visible = false;
                btnhourlyproduction.Text = "Hourly Production";
            }

            tmrautorefresh.Start();
        }

        private void radCheckBox1_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (dgvmoline.RowCount > 0)
            {
                //check if same combine article checkbox is checked
                if (chksamearticle.Checked == true)
                {
                    dgvmoline.Columns[0].IsVisible = true;
                }
                else
                {
                    //uncheck all the mo
                    for (int i = 0; i < dgvmoline.Rows.Count; i++)
                    {
                        dgvmoline.Rows[i].Cells["Select"].Value = false;
                    }

                    //get the select mo only
                    int j = dgvmoline.CurrentCell.RowIndex;
                    dgvmoline.Rows[j].Cells["Select"].Value = true;

                    dgvmoline.Columns[0].IsVisible = false;
                    btnchart.Visible = false;
                    panel5.Visible = false;
                    panel2.Visible = false;
                    radChartView1.Visible = false;
                    btnhourlyproduction.Visible = false;

                    btnhourlyproduction.Text = "Hourly Production";
                    btnchart.Text = "Chart View";
                    panel1.Visible = false;

                    //calculate current production details
                    RowSelected1();
                }
            }
        }

        private void dtpcurrent_Initialized(object sender, EventArgs e)
        {

        }

        private void cmbshift_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            btnchart.Visible = false;
            panel5.Visible = false;
            panel2.Visible = false;
            radChartView1.Visible = false;
            btnhourlyproduction.Visible = false;

            btnhourlyproduction.Text = "Hourly Production";
            btnchart.Text = "Chart View";

            panel1.Visible = false;
            //calculate current production
            RowSelected1();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //generate report
            Report();
            radChartView1.Visible = true;
            panel1.Visible = true;
            reportViewer1.Visible = true;

            DataView view = new DataView(data1);
            DataView view1 = new DataView(data);

            DataTable dt_image = new DataTable();
            dt_image.Columns.Add("image", typeof(byte[]));
            dt_image.Rows.Add(dc.GetImage());
            DataView dv_image = new DataView(dt_image);

            reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Cur_prod.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", view1));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", dv_image));
            reportViewer1.RefreshReport();
            tmrautorefresh.Stop();
        }

        private void tmrautorefresh_Tick(object sender, EventArgs e)
        {
            RowSelected1();
        }

        private void radDropDownList1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //set auto refresh timer
            tmrautorefresh.Stop();
            int interval = int.Parse(cmbautorefresh.Text);
            interval = interval * 1000;
            tmrautorefresh.Interval = interval;
            tmrautorefresh.Start();
        }

        private void btnupdatestn_Click(object sender, EventArgs e)
        {
            //update the sam for each operation
            for (int i = 0; i < dgvoperation.Rows.Count; i++)
            {
                String opcode = dgvoperation.Rows[i].Cells[1].Value.ToString();
                String sam = dgvoperation.Rows[i].Cells[11].Value.ToString();

                SqlCommand cmd = new SqlCommand("update OPERATION_DB set D_SAM='" + sam + "' where V_OPERATION_CODE='" + opcode + "'", dc.con);
                cmd.ExecuteNonQuery();
            }

            radLabel8.Text = "SAM Updated";
            //calculate the current production
            RowSelected1();
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            //generate current production
            dthourly = new DataTable();
            dthourly.Columns.Add("hour");
            dthourly.Columns.Add("count");
            dthourly.Columns.Add("mo");

            dtmoload = new DataTable();
            dtmoload.Columns.Add("mo");
            dtmoload.Columns.Add("count");

            dtmoprod = new DataTable();
            dtmoprod.Columns.Add("mo");
            dtmoprod.Columns.Add("count");

            dtmoeff = new DataTable();
            dtmoeff.Columns.Add("mo");
            dtmoeff.Columns.Add("count");

            dtmorepair = new DataTable();
            dtmorepair.Columns.Add("mo");
            dtmorepair.Columns.Add("count");

            dttotal = new DataTable();
            dttotal.Columns.Add("TOTAL_LOADED");
            dttotal.Columns.Add("TOTAL_UNLOADED");
            dttotal.Columns.Add("TOTAL_REPAIR_REWORK");

            DataTable data = new DataTable();
            data.Columns.Add("mono");
            data.Columns.Add("moline");
            data.Columns.Add("hour");
            data.Columns.Add("loaded");
            data.Columns.Add("unloaded");
            data.Columns.Add("eff");
            data.Columns.Add("rework");
            data.Columns.Add("color");
            data.Columns.Add("article");
            data.Columns.Add("size");
            data.Columns.Add("date");

            int totalload = 0;
            int totalunload = 0;
            int totalrepair = 0;

            String date = DateTime.Now.ToString("yyyy-MM-dd");
            date = dtpcurrent.Value.ToString("yyyy-MM-dd");
            DateTime op_starttime = DateTime.Now;
            DateTime op_endtime = DateTime.Now;

            String mo = dgvmoline.SelectedRows[0].Cells[1].Value.ToString();
            String moline = dgvmoline.SelectedRows[0].Cells[2].Value.ToString();

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

            //get hourly production loading
            int load = 0;
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT HOUR(TIME),MO_NO,MO_LINE,SUM(PC_COUNT) FROM stationhistory where time>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='1' GROUP BY HOUR(TIME),MO_NO,MO_LINE ORDER BY HOUR(TIME)", dc.conn);
            dt5 = new DataTable();
            da.Fill(dt5);
            da.Dispose();
            for (int i = 0; i < dt5.Rows.Count; i++)
            {
                if (dt5.Rows[i][3].ToString() != "")
                {
                    load = int.Parse(dt5.Rows[i][3].ToString());
                }

                String hour = dt5.Rows[i][0].ToString();
                if (hour.Length == 1)
                {
                    hour = "0" + hour;
                }

                //add to moload datatable
                dtmoload.Rows.Add(mo + "-" + moline, load);
                totalload += load;
                data.Rows.Add(mo, moline, hour + ":00:00", load, "0", "0", "0", color, article, size, date);
            }

            //get hourly production unloading
            int unload = 0;
            da = new MySqlDataAdapter("SELECT HOUR(TIME),MO_NO,MO_LINE,SUM(PC_COUNT) FROM stationhistory where time>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='2' GROUP BY HOUR(TIME),MO_NO,MO_LINE ORDER BY HOUR(TIME)", dc.conn);
            dt5 = new DataTable();
            da.Fill(dt5);
            da.Dispose();
            for (int i = 0; i < dt5.Rows.Count; i++)
            {
                if (dt5.Rows[i][3].ToString() != "")
                {
                    unload = int.Parse(dt5.Rows[i][3].ToString());
                }

                int flag = 0;

                String hour = dt5.Rows[i][0].ToString();
                if (hour.Length == 1)
                {
                    hour = "0" + hour;
                }

                //add to mounload datatable
                totalunload += unload;
                dtmoprod.Rows.Add(mo + "-" + moline, unload);

                for (int n = 0; n < data.Rows.Count; n++)
                {
                    if (data.Rows[n][0].ToString() == mo && data.Rows[n][1].ToString() == moline && data.Rows[n][2].ToString() == hour + ":00:00")
                    {
                        data.Rows[n][4] = unload;
                        flag = 1;
                    }
                }

                if (flag == 0)
                {
                    data.Rows.Add(mo, moline, hour + ":00:00", "0", unload, "0", "0", color, article, size, date);
                }
            }

            //get article id
            String articleid = "";
            SqlCommand cmd = new SqlCommand("select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                articleid = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get sum of sam for the article
            int total_sam = 0;
            if (getallop == "TRUE")
            {
                cmd = new SqlCommand("select sum(op.D_SAM) from DESIGN_SEQUENCE ds,OPERATION_DB op where ds.V_ARTICLE_ID='" + articleid + "' and ds.V_OPERATION_CODE=op.V_OPERATION_CODE", dc.con);
                total_sam = int.Parse(cmd.ExecuteScalar() + "");
            }
            else
            {
                cmd = new SqlCommand("select SUM(o.D_SAM) from DESIGN_SEQUENCE d,OPERATION_DB o where d.V_OPERATION_CODE=o.V_OPERATION_CODE and d.V_ARTICLE_ID=(select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "') and d.I_SEQUENCE_NO in(select s.I_SEQUENCE_NO from STATION_ASSIGN s where s.V_MO_NO='" + mo + "' and s.V_MO_LINE='" + moline + "' and s.I_STATION_ID!='0')", dc.con);
                total_sam = int.Parse(cmd.ExecuteScalar() + "");
            }

            //get hourly production for the mo
            da = new MySqlDataAdapter("select HOUR(TIME),SUM(PC_COUNT) from stationhistory where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='2' and TIME>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' group by HOUR(TIME)", dc.conn);
            dt5 = new DataTable();
            da.Fill(dt5);
            da.Dispose();
            for (int i = 0; i < dt5.Rows.Count; i++)
            {
                //get the first hanger and last hanger time
                MySqlDataAdapter sda2 = new MySqlDataAdapter("SELECT MIN(TIME),MAX(TIME) FROM stationhistory where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and TIME>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' and HOUR(TIME)='" + dt5.Rows[i][0].ToString() + "'", dc.conn);
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

                    int actual_production = 0;
                    if (dt5.Rows[i][1].ToString() != "")
                    {
                        actual_production = int.Parse(dt5.Rows[i][1].ToString());
                    }

                    //calculate actual sam for the mo
                    TimeSpan ts = new TimeSpan();
                    ts = op_endtime - op_starttime;
                    int duration = (int)ts.TotalSeconds;

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

                    //add to efficiency datatable
                    dtmoeff.Rows.Add(mo + "-" + moline, (int)efficiency);
                    for (int n = 0; n < data.Rows.Count; n++)
                    {
                        if (data.Rows[n][0].ToString() == mo && data.Rows[n][1].ToString() == moline && data.Rows[n][2].ToString() == dt5.Rows[i][0].ToString() + ":00:00")
                        {
                            data.Rows[n][5] = (int)efficiency + "%";
                        }
                    }
                }
            }

            //get hourly repair quantity for the mo
            sda = new SqlDataAdapter("select CONVERT(VARCHAR(2), D_DATE_TIME, 108),SUM(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + date + " 00:00:00' and D_DATE_TIME<'" + date + " 23:59:59' and V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' group by CONVERT(VARCHAR(2), D_DATE_TIME, 108) ORDER BY CONVERT(VARCHAR(2), D_DATE_TIME, 108)", dc.con);
            dt5 = new DataTable();
            sda.Fill(dt5);
            sda.Dispose();
            for (int i = 0; i < dt5.Rows.Count; i++)
            {
                int count = 0;
                String temp = dt5.Rows[i][1].ToString();

                if (temp != "")
                {
                    count = int.Parse(dt5.Rows[i][1].ToString());
                }
                else
                {
                    count = 0;
                }

                //add to morepiar datatable
                totalrepair += count;
                dtmorepair.Rows.Add(mo + "-" + moline, count);

                for (int n = 0; n < data.Rows.Count; n++)
                {
                    if (data.Rows[n][0].ToString() == mo && data.Rows[n][1].ToString() == moline && data.Rows[n][2].ToString() == dt5.Rows[i][0].ToString() + ":00:00")
                    {
                        data.Rows[n][6] = count;
                    }
                }
            }

            dttotal.Rows.Add(totalload, totalunload, totalrepair);
            radChartView1.Visible = true;
            reportViewer1.Visible = true;
            panel1.Visible = true;

            //add datatable to datatview
            DataView view = new DataView(data);
            DataView view1 = new DataView(dthourly);
            DataView view2 = new DataView(dtmoprod);
            DataView view3 = new DataView(dtmoload);
            DataView view4 = new DataView(dtmoeff);
            DataView view5 = new DataView(dtmorepair);
            DataView view6 = new DataView(dttotal);

            //get logo
            DataTable dt_image = new DataTable();
            dt_image.Columns.Add("image", typeof(byte[]));
            dt_image.Rows.Add(dc.GetImage());
            DataView dv_image = new DataView(dt_image);

            reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.HOURLY_PRODUCTION.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();

            //add views to datatset
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", view1));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", view2));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", view3));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", view4));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", view5));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet7", view6));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet8", dv_image));

            //refresh reportviewer
            reportViewer1.RefreshReport();
            tmrautorefresh.Stop();
        }

        private void radDropDownList1_SelectedIndexChanged_1(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //show different reports and chart
            if (radDropDownList1.Text == "Current Production")
            {
                btnchart.Text = "Table View";
                btnchart.PerformClick();
            }
            else if (radDropDownList1.Text == "Current Production Report")
            {
                btncurrentreport.PerformClick();
            }
            else if (radDropDownList1.Text == "Current Production Chart")
            {
                btnhourlyproduction.Text = "Current Production";
                btnchart.Text = "Chart View";
                btnchart.PerformClick();
                btnhourlyproduction.PerformClick();
            }
            else if (radDropDownList1.Text == "Hourly Production")
            {

                btnhourlyproduction.Text = "Hourly Production";
                btnhourlyproduction.PerformClick();
            }
            else if (radDropDownList1.Text == "Hourly Production Report")
            {
                btnhourlyreport.PerformClick();
            }
            else if (radDropDownList1.Text == "Hourly MO Production Report")
            {
                Hourly_MO();
            }
        }

        private void dgvmoline_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            //check if combine article checkbox is checked
            if (chksamearticle.Checked == false)
            {
                //deselect all the mo
                for (int i = 0; i < dgvmoline.Rows.Count; i++)
                {
                    dgvmoline.Rows[i].Cells["Select"].Value = false;
                }
                dgvmoline.Rows[e.RowIndex].Cells["Select"].Value = true;
            }
            else
            {
                //check if mo is selected
                if ((bool)dgvmoline.Rows[e.RowIndex].Cells["Select"].Value)
                {
                    dgvmoline.Rows[e.RowIndex].Cells["Select"].Value = false;
                }
                else
                {
                    dgvmoline.Rows[e.RowIndex].Cells["Select"].Value = true;

                    //check if select mo is of same article
                    for (int i = 0; i < dgvmoline.Rows.Count; i++)
                    {
                        if ((bool)dgvmoline.Rows[i].Cells[0].Value)
                        {
                            if (dgvmoline.Rows[i].Cells[5].Value.ToString() == dgvmoline.Rows[e.RowIndex].Cells[5].Value.ToString())
                            {
                                dgvmoline.Rows[e.RowIndex].Cells["Select"].Value = true;
                            }
                            else
                            {
                                dgvmoline.Rows[e.RowIndex].Cells["Select"].Value = false;
                                radLabel8.Text = "Not a Same Article";
                                return;
                            }
                        }
                    }
                }
            }

            btnchart.Visible = false;
            panel5.Visible = false;
            panel2.Visible = false;
            radChartView1.Visible = false;
            btnhourlyproduction.Visible = false;

            btnhourlyproduction.Text = "Hourly Production";
            btnchart.Text = "Chart View";
            panel1.Visible = false;

            //calculate current production
            RowSelected1();
        }

        String getallop = "";

        private void Current_Production_Initialized(object sender, EventArgs e)
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
                getallop = sdr.GetValue(3).ToString();
            }
            sdr.Close();

            //change grid theme
            GridTheme(theme);
        }

        //set grid theme
        public void GridTheme(String theme)
        {
            dgvmoline.ThemeName = theme;
        }

        public void Hourly_MO()
        {
            try
            {
                radChartView1.Visible = true;
                reportViewer1.Visible = true;
                panel1.Visible = true;

                DataTable dtmo = new DataTable();
                dtmo.Columns.Add("mono");
                dtmo.Columns.Add("moline");
                dtmo.Columns.Add("hour");
                dtmo.Columns.Add("loaded");
                dtmo.Columns.Add("eff");
                dtmo.Columns.Add("rework");
                dtmo.Columns.Add("color");
                dtmo.Columns.Add("article");
                dtmo.Columns.Add("size");
                dtmo.Columns.Add("date");
                dtmo.Columns.Add("opcode");
                dtmo.Columns.Add("opdesc");

                String date = DateTime.Now.ToString("yyyy-MM-dd");
                date = dtpcurrent.Value.ToString("yyyy-MM-dd");
                DateTime op_starttime = DateTime.Now;
                DateTime op_endtime = DateTime.Now;

                //get mo used for the day
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT MO_NO,MO_LINE FROM stationhistory where time>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' order by MO_NO,MO_LINE", dc.conn);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                da.Dispose();
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    String mo = dt1.Rows[j][0].ToString();
                    String moline = dt1.Rows[j][1].ToString();
                    String article = "";
                    int hanger_count = 1;

                    String color = "";
                    String articleDesc = "";
                    String size = "";

                    //get mo details
                    SqlDataAdapter sda = new SqlDataAdapter("select C.V_COLOR_DESC,A.V_ARTICLE_DESC,S.V_SIZE_DESC from MO_DETAILS M,COLOR_DB C,ARTICLE_DB A,SIZE_DB S where M.V_ARTICLE_ID=A.V_ARTICLE_ID and M.V_COLOR_ID=C.V_COLOR_ID and M.V_SIZE_ID=S.V_SIZE_ID and M.V_MO_NO='" + mo + "' and M.V_MO_LINE='" + moline + "'", dc.con);
                    DataTable dt5 = new DataTable();
                    sda.Fill(dt5);
                    sda.Dispose();
                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {
                        color = dt5.Rows[i][0].ToString();
                        articleDesc = dt5.Rows[i][1].ToString();
                        size = dt5.Rows[i][2].ToString();
                    }

                    //get article id
                    SqlCommand cmd = new SqlCommand("select V_ARTICLE_ID from ARTICLE_DB where V_ARTICLE_DESC='" + articleDesc + "'", dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        article = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get hanger count
                    cmd = new SqlCommand("select I_HANGER_COUNT from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        hanger_count = int.Parse(sdr.GetValue(0).ToString());
                    }
                    sdr.Close();

                    int seq1 = 1;
                    int nextseq = 1;
                    int prevseq = 1;
                    int curseq = 1;

                    //get all the sequence fro the mo
                    sda = new SqlDataAdapter("select ds.I_SEQUENCE_NO,ds.V_OPERATION_CODE,op.V_OPERATION_DESC,op.D_SAM from DESIGN_SEQUENCE ds,OPERATION_DB op where ds.V_ARTICLE_ID='" + article + "' and ds.V_OPERATION_CODE=op.V_OPERATION_CODE and ds.I_SEQUENCE_NO IN(select distinct I_SEQUENCE_NO from STATION_ASSIGN where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and I_STATION_ID!=0) order by ds.I_SEQUENCE_NO", dc.con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //reset the sequence
                        prevseq = seq1;
                        seq1 = int.Parse(dt.Rows[i][0].ToString());
                        if (prevseq == seq1)
                        {
                            nextseq = curseq;
                        }
                        else
                        {
                            nextseq = nextseq + 1;
                        }
                        curseq = nextseq;

                        String opcode = dt.Rows[i][1].ToString();
                        String opdesc = dt.Rows[i][2].ToString();
                        int sam = int.Parse(dt.Rows[i][3].ToString());

                        //get hourly production for each sequence
                        MySqlDataAdapter sda2 = new MySqlDataAdapter("SELECT HOUR(TIME),COUNT(HANGER_ID),MIN(TIME),MAX(TIME) FROM stationhistory where SEQ_NO='" + curseq + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and TIME>='" + date + " 00:00:00' and TIME<'" + date + " 23:59:59' group by HOUR(TIME) order by HOUR(TIME)", dc.conn);
                        DataTable dt2 = new DataTable();
                        sda2.Fill(dt2);
                        sda2.Dispose();
                        int loaded = 0;
                        for (int k = 0; k < dt2.Rows.Count; k++)
                        {
                            String hour = dt2.Rows[k][0].ToString();
                            int count = 0;

                            if (dt2.Rows[k][2].ToString() != "")
                            {
                                op_starttime = Convert.ToDateTime(dt2.Rows[k][2].ToString());
                            }

                            if (dt2.Rows[0][3].ToString() != "")
                            {
                                op_endtime = Convert.ToDateTime(dt2.Rows[k][3].ToString());
                            }

                            loaded = int.Parse(dt2.Rows[k][1].ToString());
                            loaded = loaded * hanger_count;

                            TimeSpan ts_op_completed = Convert.ToDateTime(op_endtime.ToString("HH:mm:ss")) - Convert.ToDateTime(op_starttime.ToString("HH:mm:ss"));
                            int op_completed = (int)ts_op_completed.TotalSeconds;

                            //calculate actual sam for each sequence
                            decimal actual_sam = 0;
                            if (loaded > 0)
                            {
                                actual_sam = (decimal)op_completed / (decimal)loaded;
                            }

                            //calculate efficiency
                            decimal efficiency = 0;
                            if (actual_sam > 0)
                            {
                                efficiency = (sam / actual_sam) * 100;
                            }

                            //get repair quantity for each operation
                            sda = new SqlDataAdapter("select SUM(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + date + " 00:00:00' and D_DATE_TIME<'" + date + " 23:59:59' and V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and CONVERT(VARCHAR(2), D_DATE_TIME, 108)='" + hour + "' and V_OP_CODE='" + opcode + "'", dc.con);
                            dt5 = new DataTable();
                            sda.Fill(dt5);
                            sda.Dispose();
                            for (int p = 0; p < dt5.Rows.Count; p++)
                            {

                                String temp = dt5.Rows[p][0].ToString();
                                if (temp != "")
                                {
                                    count = int.Parse(dt5.Rows[p][0].ToString());
                                }
                                else
                                {
                                    count = 0;
                                }
                            }

                            if (hour.Length == 1)
                            {
                                hour = "0" + hour;
                            }

                            //add to mo datatable
                            dtmo.Rows.Add(mo, moline, hour, loaded, efficiency.ToString("0.##"), count, color, articleDesc, size, date, opcode, opdesc);
                        }
                    }
                }

                reportViewer1.Visible = true;
                DataView view = new DataView(dtmo);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.MO_OP.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                //add view to datatset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.ToString(), "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void dgvmoline_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these themes are selected
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

        private void dgvoperation_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            String sam = dgvoperation.Rows[e.RowIndex].Cells[11].Value.ToString();
            Regex r = new Regex("^[0-9]*$");
            if (!r.IsMatch(sam))
            {
                radLabel8.Text = "Invalid SAM value. Example : 35";
                dgvoperation.Rows[e.RowIndex].Cells[11].Value = old_sam;
            }
        }

        private void dgvoperation_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            old_sam = dgvoperation.Rows[e.RowIndex].Cells[11].Value.ToString();
        }
    }
}
