using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using Telerik.WinControls;
using MySql.Data.MySqlClient;
using System.IO;

namespace SMARTMRT
{
    public partial class Payroll : Telerik.WinControls.UI.RadForm
    {
        public Payroll()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();   //connection class
        DataTable MO = new DataTable();
        DataTable data1 = new DataTable();

        private void Payroll_Load(object sender, EventArgs e)
        {
            select_controller();  //get the selected controller
            dgvemployee.MasterTemplate.SelectLastAddedRow = false;
            dgvoperations.MasterTemplate.SelectLastAddedRow = false;
            //disable close button on search in grid
            dgvemployee.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvoperations.MasterView.TableSearchRow.ShowCloseButton = false;

            data1.Columns.Add("DATE1");
            data1.Columns.Add("DATE2");
            data1.Columns.Add("SHIFT");
            data1.Columns.Add("TOTALDAYS");
            data1.Columns.Add("TOTALEMPLOYEES");
            data1.Columns.Add("EMPID");
            data1.Columns.Add("EMPNAME");
            data1.Columns.Add("BASICPAY");
            data1.Columns.Add("PEICECNT");
            data1.Columns.Add("TOTALDURATION");
            data1.Columns.Add("TARGETPROD");
            data1.Columns.Add("EFFICIENCY");
            data1.Columns.Add("GROSSPAY");
            data1.Columns.Add("NETPAY");

            //get all shifts
            SqlDataAdapter sda = new SqlDataAdapter("select V_SHIFT from SHIFTS", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            cmbshift.Items.Add("All");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbshift.Items.Add(dt.Rows[i][0].ToString());
                cmbshift.SelectedIndex = 1;
            }

            cmbline.Items.Clear();
            cmbline.Items.Add("All");
            cmbline.SelectedIndex = 0;
            sda = new SqlDataAdapter("SELECT distinct V_PROD_LINE FROM PROD_LINE_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                cmbline.Items.Add(dt.Rows[j][0].ToString());
            }

            dtpstart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            dtpend.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        private void radLabel15_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            panel3.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel15.Text = "";
            panel3.Visible = false;
            MyTimer.Stop();
        }

        private void dtpstart_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void dtpend_ValueChanged(object sender, EventArgs e)
        {
            
        }     

        private void Payroll_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        String theme = "";
        private void Payroll_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();    //open connection

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
            dgvemployee.ThemeName = theme;
            dgvoperations.ThemeName = theme;
        }


        private void dgvemployee_DoubleClick(object sender, EventArgs e)
        {
            //show payroll for the employee
            if (dgvemployee.SelectedRows.Count > 0)
            {
                Payroll_Employee pe = new Payroll_Employee();
                pe.getData(dgvemployee.SelectedRows[0].Cells[0].Value.ToString(), dgvemployee.SelectedRows[0].Cells[1].Value.ToString(), dgvemployee.SelectedRows[0].Cells[2].Value.ToString(), dtpstart.Value.ToString("yyyy-MM-dd"), dtpend.Value.ToString("yyyy-MM-dd"), cmbshift.Text, cmbline.Text);
                pe.Show();
            }
        }

        private void cmbshift_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //Selected_Dates();
        }        

        private void btnupdatestn_Click(object sender, EventArgs e)
        {
            try
            {
                //get the all operation
                for (int i = 0; i < dgvoperations.Rows.Count; i++)
                {
                    String opcode = dgvoperations.Rows[i].Cells[2].Value.ToString() + "";
                    String piecerate = dgvoperations.Rows[i].Cells[4].Value.ToString() + "";
                    String sam = dgvoperations.Rows[i].Cells[5].Value.ToString() + "";

                    //check if sam is valid
                    Regex r = new Regex("^[0-9]*$");
                    if (!r.IsMatch(sam) || sam == "")
                    {
                        radLabel15.Text = "Invalid SAM value. Example : 15";
                        dgvoperations.Rows[i].IsSelected = true;
                        dgvoperations.Rows[i].Cells[3].Value = "";
                        return;
                    }

                    //check if the piecerate is valid 
                    r = new Regex("^[0-9]{1,4}([.][0-9]{1,4})?$");
                    if (!r.IsMatch(piecerate) || piecerate == "")
                    {
                        radLabel15.Text = "Invalid Piece Rate value.  Example : 1.2000";
                        dgvoperations.Rows[i].IsSelected = true;
                        dgvoperations.Rows[i].Cells[2].Value = "";
                        return;
                    }

                    //update operation db
                    SqlCommand cmd = new SqlCommand("update OPERATION_DB set D_SAM='" + sam + "',D_PIECERATE='" + piecerate + "' where V_OPERATION_CODE='" + opcode + "'", dc.con);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                radLabel15.Text = "Values for Piece Rate and Sam cannot be Empty. Please Fill with the appropriate values.";
                Console.WriteLine(ex);
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            try
            {
                panel6.Visible = false;

                DateTime start_time = Convert.ToDateTime(dtpstart.Value.ToString("yyyy-MM-dd"));
                DateTime end_time = Convert.ToDateTime(dtpend.Value.ToString("yyyy-MM-dd"));

                int totalpay = 0;
                dgvemployee.Rows.Clear();
                dgvoperations.Visible = false;

                //Individual
                String query = "";
                if (cmbline.Text == "All")
                {
                    query = "SELECT DISTINCT EMP_ID FROM HANGER_HISTORY WHERE TIME>='" + start_time.ToString("yyyy-MM-dd") + " 00:00:00' AND TIME<='" + end_time.ToString("yyyy-MM-dd") + " 23:59:59' and EMP_ID not in (select v_group_id from EMPLOYEE_GROUPS)";
                }
                else
                {
                    query = "SELECT DISTINCT h.EMP_ID FROM HANGER_HISTORY h,STATION_DATA s WHERE h.TIME>='" + start_time.ToString("yyyy-MM-dd") + " 00:00:00' AND h.TIME<='" + end_time.ToString("yyyy-MM-dd") + " 23:59:59' and h.STN_ID=s.I_STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "' and EMP_ID not in (select v_group_id from EMPLOYEE_GROUPS)";
                }


                //get the individual employee id
                SqlDataAdapter sda = new SqlDataAdapter(query, dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                //Group
                 query = "";
                if (cmbline.Text == "All")
                {
                    //query = "SELECT DISTINCT EMP_ID FROM HANGER_HISTORY WHERE TIME>='" + start_time.ToString("yyyy-MM-dd") + " 00:00:00' AND TIME<='" + end_time.ToString("yyyy-MM-dd") + " 23:59:59' and EMP_ID in (select v_group_id from EMPLOYEE_GROUPS)";
                    query = "select V_EMP_ID  as EMP_ID from EMPLOYEE_GROUPS where V_GROUP_ID in (SELECT DISTINCT EMP_ID FROM HANGER_HISTORY WHERE TIME >= '" + start_time.ToString("yyyy-MM-dd") + " 00:00:00' AND TIME<= '" + start_time.ToString("yyyy-MM-dd") + " 23:59:59' and EMP_ID in (select v_group_id from EMPLOYEE_GROUPS))";
                }
                else
                {
                  //  query = "SELECT DISTINCT h.EMP_ID FROM HANGER_HISTORY h,STATION_DATA s WHERE h.TIME>='" + start_time.ToString("yyyy-MM-dd") + " 00:00:00' AND h.TIME<='" + end_time.ToString("yyyy-MM-dd") + " 23:59:59' and h.STN_ID=s.I_STN_ID and s.I_INFEED_LINE_NO='" + cmbline.Text + "' and EMP_ID in (select v_group_id from EMPLOYEE_GROUPS)";
                  query = "select V_EMP_ID as EMP_ID from EMPLOYEE_GROUPS where V_GROUP_ID in (" +
                        "SELECT DISTINCT h.EMP_ID FROM HANGER_HISTORY h, STATION_DATA s WHERE h.TIME >= '" + start_time.ToString("yyyy-MM-dd") + " 00:00:00' AND h.TIME <= '" + start_time.ToString("yyyy-MM-dd") + " 23:59:59' and h.STN_ID = s.I_STN_ID and s.I_INFEED_LINE_NO = '" + cmbline.Text + "' " +
                        "and EMP_ID  in (select v_group_id from EMPLOYEE_GROUPS))";
                }

                //get the individual employee id
                SqlDataAdapter sda2 = new SqlDataAdapter(query, dc.con);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);

                DataRow dtRow;
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    dtRow = dt.NewRow();
                    dtRow["EMP_ID"] = dt2.Rows[j][0].ToString();
                    dt.Rows.Add(dtRow);
                }

                DataView view = new DataView(dt);
                DataTable dtEmpId = view.ToTable(true, "EMP_ID");


                //    //get the employee id
                //    SqlDataAdapter sda = new SqlDataAdapter(query, dc.con);
                //DataTable dt = new DataTable();
                //sda.Fill(dt);
                for (int j = 0; j < dtEmpId.Rows.Count; j++)
                {
                    int normal = 0;
                    int overtime = 0;
                    int work_duration1 = 0;
                    int target = 0;
                    decimal net = 0;
                    decimal efficiency = 0;
                    String empid = "";
                    empid = dtEmpId.Rows[j][0].ToString();
                    DataTable emp = Calculate_Payroll(empid);   //calculate payroll

                    //get totals
                    for (int i = 0; i < emp.Rows.Count; i++)
                    {
                        normal += int.Parse(emp.Rows[i][6].ToString());
                        overtime += int.Parse(emp.Rows[i][7].ToString());
                        work_duration1 += int.Parse(emp.Rows[i][8].ToString());
                        target += int.Parse(emp.Rows[i][9].ToString());
                        net += Convert.ToDecimal(emp.Rows[i][11].ToString());
                    }

                    //calculate effciency
                    if (target > 0)
                    {
                        efficiency = (((decimal)normal + (decimal)overtime) / (decimal)target) * 100;
                    }

                    totalpay += (int)net;
                    String empname = "";
                    String basicpay = "0";

                    //get first name of the employee
                    
                    String strSql = "";

                    query = "select V_FIRST_NAME,I_BASIC_PAY from EMPLOYEE where V_EMP_ID='" + empid + "'";

                    SqlCommand cmd = new SqlCommand(query, dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        empname = sdr.GetValue(0).ToString();
                        basicpay = sdr.GetValue(1).ToString();
                    }
                    sdr.Close();


                    ////get emp name in group
                    //if (empname == "")
                    //{
                    //    empname = "";
                    //    String tempEmpName = "";

                    //    query = "select EMPLOYEE.V_EMP_ID, EMPLOYEE.V_FIRST_NAME, EMPLOYEE.I_BASIC_PAY from EMPLOYEE_GROUPS " +
                    //    "inner join EMPLOYEE on EMPLOYEE.V_EMP_ID = EMPLOYEE_GROUPS.V_EMP_ID " +
                    //    "where V_GROUP_ID = '" + empid + "'";

                    //    sda = new SqlDataAdapter(query, dc.con);
                    //    DataTable dtG = new DataTable();
                    //    sda.Fill(dtG);
                    //    for (int k = 0; k < dtG.Rows.Count; k++)
                    //    {
                    //        tempEmpName = dtG.Rows[k][1].ToString();
                    //        basicpay = dtG.Rows[k][2].ToString();                           
                    //        empname = empname + "," + tempEmpName;
                    //    }
                        
                    //    empname = empname.Substring(1);
                    //}
          
                    //check if the net > than basis pay
                    String gross = basicpay;
                    if (net > int.Parse(basicpay))
                    {
                        //gross = net + "";

                        net = Math.Round(net, 2);
                        gross = net + "";
                    }

                    //add to grid
                    dgvemployee.Rows.Add(empid, empname, basicpay, normal, overtime, work_duration1, target, efficiency.ToString("0.##") + "%", net, gross);
                    data1.Rows.Add(start_time, end_time, cmbshift.Text, txttotaldays.Text, dtEmpId.Rows.Count, empid, empname, basicpay, normal, work_duration1, target, efficiency.ToString("0.##") + "%", net, gross);
                }

                txttotalpay.Text = totalpay.ToString("0.##");
                txttotalemp.Text = dgvemployee.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
            }
        }

        private void btneditrecords_Click(object sender, EventArgs e)
        {
            //open edit records
            if (dgvemployee.SelectedRows.Count > 0)
            {
                Edit_Records er = new Edit_Records();
                er.getData(dgvemployee.SelectedRows[0].Cells[0].Value.ToString(), dgvemployee.SelectedRows[0].Cells[1].Value.ToString());
                er.Show();
            }
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            //check if the report button clicked
            if (btnreport.Text == "Report View")
            {
                dgvoperations.Visible = true;
                reportViewer1.Visible = true;
                btnreport.Text = "Table View";

                DataView view = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Payroll.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                //add to views to dataset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer1.RefreshReport();
            }
            else
            {
                reportViewer1.Visible = false;
                dgvoperations.Visible = false;
                btnreport.Text = "Report View";
            }
        }

        private void dgvemployee_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            dgvoperations.Rows.Clear();
            dgvoperations.Visible = true;
            reportViewer1.Visible = false;
            String empid = dgvemployee.Rows[e.RowIndex].Cells[0].Value.ToString();

            DataTable emp = Calculate_Payroll(empid).Copy();   //calculate payroll
            for (int i = 0; i < emp.Rows.Count; i++)
            {
                dgvoperations.Rows.Add(emp.Rows[i][0].ToString(), emp.Rows[i][1].ToString(), emp.Rows[i][2].ToString(), emp.Rows[i][3].ToString(), emp.Rows[i][4].ToString(), emp.Rows[i][5].ToString(), emp.Rows[i][6].ToString(), emp.Rows[i][7].ToString(), emp.Rows[i][8].ToString(), emp.Rows[i][9].ToString(), emp.Rows[i][10].ToString() + "%", emp.Rows[i][11].ToString());
            }

            panel6.Visible = true;
        }

        public DataTable Calculate_Payroll(String empid)
        {
            try
            {

                DateTime start_time = Convert.ToDateTime(dtpstart.Value.ToString("yyyy-MM-dd"));
                DateTime end_time = Convert.ToDateTime(dtpend.Value.ToString("yyyy-MM-dd"));
                if (start_time > end_time)
                {
                    start_time = Convert.ToDateTime(dtpend.Value.ToString("yyyy-MM-dd"));
                    end_time = Convert.ToDateTime(dtpstart.Value.ToString("yyyy-MM-dd"));
                }

                TimeSpan ts_day = new TimeSpan();
                ts_day = end_time - start_time;
                txttotaldays.Text = ts_day.TotalDays.ToString();

                MO = new DataTable();
                MO.Columns.Add("MO");
                MO.Columns.Add("MOLINE");
                MO.Columns.Add("OPCODE");
                MO.Columns.Add("OPDESC");
                MO.Columns.Add("Piece rate");
                MO.Columns.Add("Overtime Rate");
                MO.Columns.Add("SAM");
                MO.Columns.Add("Normal Count");
                MO.Columns.Add("Overtime Count");

                DataTable EMP = new DataTable();
                EMP.Columns.Add("MO");
                EMP.Columns.Add("MOLINE");
                EMP.Columns.Add("OPCODE");
                EMP.Columns.Add("OPDESC");
                EMP.Columns.Add("Piece rate");
                EMP.Columns.Add("SAM");
                EMP.Columns.Add("Normal Count");
                EMP.Columns.Add("Overtime Count");
                EMP.Columns.Add("Total Duration");
                EMP.Columns.Add("Target");
                EMP.Columns.Add("Efficiency");
                EMP.Columns.Add("Gross Pay");

                String hide_ot = "";
                int breaktime_complete = 0;
                DateTime shift_start = Convert.ToDateTime("9:30:00");
                DateTime shift_end = Convert.ToDateTime("18:30:00");
                DateTime overtime_end = Convert.ToDateTime("19:30:00");
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

                //DateTime dtHideDay;
                //string HideDay;
                //string strSql = "SELECT CONVERT(nvarchar(10), D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE = 'TRUE'";
                //SqlCommand cmd2 = new SqlCommand(strSql, dc.con);
                // sdr = cmd.ExecuteReader();
                //if (sdr.Read())
                //{
                //    HideDay = sdr.GetValue(0).ToString();                  
                //    if (string.IsNullOrEmpty(HideDay))
                //      {
                //        dtHideDay = Convert.ToDateTime(HideDay);
                //      }         
                //}
                //sdr.Close();


                //get break complete time
                cmd = new SqlCommand("select I_BREAK_TIMESPAN from SHIFT_BREAKS where V_SHIFT<='" + cmbshift.Text + "'", dc.con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    breaktime_complete = breaktime_complete + int.Parse(sdr.GetValue(0).ToString());
                }
                sdr.Close();

                //check if hide overtime if enabled
                cmd = new SqlCommand("select HIDE_OVERTIME from Setup", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    hide_ot = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                String sht_start = shift_start.ToString("HH:mm:ss");
                String sht_end = overtime_end.ToString("HH:mm:ss");

                if (shift_start > shift_end)
                {
                    shift_start = shift_start.AddDays(-1);
                }

                int work_duration1 = 0;
                String query = "";

                //check if all shift is selected
                if (cmbshift.Text == "All")
                {
                    sht_start = "00:00:00";
                    sht_end = "23:59:59";
                    dgvemployee.Columns[4].IsVisible = false;
                    dgvemployee.Columns[3].HeaderText = "PIECE COUNT";

                    dgvoperations.Columns[7].IsVisible = false;
                    dgvoperations.Columns[6].HeaderText = "PIECE COUNT";
                    if (cmbline.Text == "All")
                    {
                        query = "SELECT h.MO_NO, h.MO_LINE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM,SUM(h.PC_COUNT) as COUNT,WORKTYPE FROM HANGER_HISTORY h,SEQUENCE_OPERATION s where h.EMP_ID = '" + empid + "' and h.TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and h.TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10), D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE = 'TRUE') and s.V_SEQUENCE_NO=h.SEQ_NO and s.V_MO_NO=h.MO_NO and s.V_MO_LINE=h.MO_LINE group by h.MO_NO,h.MO_LINE,h.WORKTYPE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM order by h.MO_NO, h.MO_LINE";
                    }
                    else
                    {
                        query = "SELECT h.MO_NO, h.MO_LINE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM,SUM(h.PC_COUNT) as COUNT,WORKTYPE FROM HANGER_HISTORY h,SEQUENCE_OPERATION s,STATION_DATA d where h.EMP_ID = '" + empid + "' and h.TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and h.TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' and h.STN_ID=d.I_STN_ID and d.I_INFEED_LINE_NO='" + cmbline.Text + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10), D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE = 'TRUE') and s.V_SEQUENCE_NO=h.SEQ_NO and s.V_MO_NO=h.MO_NO and s.V_MO_LINE=h.MO_LINE group by h.MO_NO,h.MO_LINE,h.WORKTYPE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM order by h.MO_NO, h.MO_LINE";
                    }
                }
                else
                {
                    dgvemployee.Columns[4].IsVisible = true;
                    dgvemployee.Columns[3].HeaderText = "NT PIECE COUNT";

                    dgvoperations.Columns[7].IsVisible = true;
                    dgvoperations.Columns[6].HeaderText = "NT PIECE COUNT";
                    if (cmbline.Text == "All")
                    {
                        query = "SELECT h.MO_NO, h.MO_LINE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM,SUM(h.PC_COUNT) as COUNT,WORKTYPE FROM HANGER_HISTORY h,SEQUENCE_OPERATION s where h.EMP_ID = '" + empid + "' and h.TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and h.TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "'  and convert(char(5), TIME, 108) between(SELECT T.T_SHIFT_START_TIME FROM SHIFTS T WHERE V_SHIFT='" + cmbshift.Text + "') and (SELECT T.T_OVERTIME_END_TIME FROM SHIFTS T WHERE V_SHIFT='" + cmbshift.Text + "') and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10), D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE = 'TRUE') and s.V_SEQUENCE_NO=h.SEQ_NO and s.V_MO_NO=h.MO_NO and s.V_MO_LINE=h.MO_LINE group by h.MO_NO,h.MO_LINE,h.WORKTYPE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM order by h.MO_NO, h.MO_LINE";
                    }
                    else
                    {
                        query = "SELECT h.MO_NO, h.MO_LINE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM,SUM(h.PC_COUNT) as COUNT,WORKTYPE FROM HANGER_HISTORY h,SEQUENCE_OPERATION s,STATION_DATA d where h.EMP_ID = '" + empid + "' and h.TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and h.TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "'  and convert(char(5), TIME, 108) between(SELECT T.T_SHIFT_START_TIME FROM SHIFTS T WHERE V_SHIFT='" + cmbshift.Text + "') and (SELECT T.T_OVERTIME_END_TIME FROM SHIFTS T WHERE V_SHIFT='" + cmbshift.Text + "') and h.STN_ID=d.I_STN_ID and d.I_INFEED_LINE_NO='" + cmbline.Text + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10), D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE = 'TRUE') and s.V_SEQUENCE_NO=h.SEQ_NO and s.V_MO_NO=h.MO_NO and s.V_MO_LINE=h.MO_LINE group by h.MO_NO,h.MO_LINE,h.WORKTYPE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM order by h.MO_NO, h.MO_LINE";
                    }
                }

                //get production details
                SqlDataAdapter sda = new SqlDataAdapter(query, dc.con);
                DataTable dt3 = new DataTable();
                sda.Fill(dt3);
                sda.Dispose();
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    String mo = dt3.Rows[i][0].ToString();
                    String moline = dt3.Rows[i][1].ToString();
                    String opcode = dt3.Rows[i][2].ToString();
                    String piecerate = dt3.Rows[i][3].ToString();
                    String overtimerate = dt3.Rows[i][4].ToString();
                    String sam = dt3.Rows[i][5].ToString();
                    int count = int.Parse( dt3.Rows[i][6].ToString());
                    String worktype = dt3.Rows[i][7].ToString();

                    if (hide_ot == "TRUE")
                    {
                        if (worktype == "1")
                        {
                            continue;
                        }
                    }

                    //operation id and desc
                    String opdesc = "";
                    cmd = new SqlCommand("select V_OPERATION_CODE,V_OPERATION_DESC from OPERATION_DB where V_ID='" + opcode + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        opcode = sdr.GetValue(0).ToString();
                        opdesc = sdr.GetValue(1).ToString();
                    }
                    sdr.Close();

                    //check if the production details is for overtime or normal time
                    int normal = 0;
                    int overtime = 0;
                    if (worktype == "1")
                    {
                        overtime = count;
                    }
                    else
                    {
                        normal = count;
                    }

                    

                    //get pc count for empid from hangerwip in mrt_local db
                    //int pcCnt = 0;
                   
                    //query = "SELECT IFNULL(SUM(PC_COUNT), 0) AS PC_Count " +
                    //    "FROM hangerwip " +
                    //    "where time>='" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and time<'" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' " +
                    //    "and EMP_ID = '" + empid + "' " +
                    //    "and worktype = "+ worktype +" " +
                    //    "AND MO_NO = '" + mo + "' " +
                    //    "AND MO_LINE = '" + moline + "'";

                    //MySqlCommand MySqlCmd = new MySqlCommand(query, dc.conn);
                    //MySqlDataReader myReader;
                    //myReader = MySqlCmd.ExecuteReader();
                    //if (myReader.Read())
                    //{
                    //    if (myReader["PC_COUNT"] != DBNull.Value)
                    //    {
                    //        pcCnt = int.Parse(myReader.GetValue(0).ToString());
                    //    }                                                
                    //}
                    //myReader.Close();
                  
                    //if (worktype == "1")
                    //{
                    //    overtime = overtime + pcCnt;
                    //}
                    //else
                    //{
                    //    normal = normal + pcCnt;
                    //}

                    //-------------

                    //check if all shift is selected
                    if (cmbshift.Text == "All")
                    {
                        overtime = 0;
                        normal = count;
                        int flag = 0;
                        for (int j = 0; j < MO.Rows.Count; j++)
                        {
                            if (MO.Rows[j][0].ToString() == mo && MO.Rows[j][1].ToString() == moline && MO.Rows[j][2].ToString() == opcode)
                            {
                                flag = 1;
                                int temp = int.Parse(MO.Rows[j][7].ToString());
                                temp += normal;
                                MO.Rows[j][7] = temp;

                                break;
                            }
                        }

                        if (flag == 0)
                        {
                            MO.Rows.Add(mo, moline, opcode, opdesc, piecerate, overtimerate, sam, normal, overtime);
                        }
                    }
                    else
                    {
                        int flag = 0;
                        for (int j = 0; j < MO.Rows.Count; j++)
                        {
                            if (MO.Rows[j][0].ToString() == mo && MO.Rows[j][1].ToString() == moline && MO.Rows[j][2].ToString() == opcode)
                            {
                                flag = 1;
                                int temp = int.Parse(MO.Rows[j][7].ToString());
                                temp += normal;
                                MO.Rows[j][7] = temp;

                                temp = int.Parse(MO.Rows[j][8].ToString());
                                temp += overtime;
                                MO.Rows[j][8] = temp;

                                break;
                            }
                        }

                        if (flag == 0)
                        {
                            MO.Rows.Add(mo, moline, opcode, opdesc, piecerate, overtimerate, sam, normal, overtime);
                        }
                    }
                }

//                //Programmer: Hanafi | Date: 14 / 07 / 2021 | Ver:2.0.0.1 | Changes: add piece count from hangerwip in mrt_local db
//                //get from hangerwip

//                query = "SELECT h.MO_NO, h.MO_LINE,s.OP_ID,s.RATE,s.RATE_XTR,s.SAM,SUM(h.PC_COUNT) as COUNT,WORKTYPE FROM hangerwip h,sequenceoperations s " +
//"where h.EMP_ID = '" + empid +"' " +
//"and h.TIME >= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and h.TIME <= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' " +
//"AND h.TIME BETWEEN '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' AND '" + start_time.ToString("yyyy-MM-dd") + " " + overtime_end.ToString("HH:mm:ss") + "' " +
//"and s.SEQ_NO = h.SEQ_NO " +
//"and s.MO_NO = h.MO_NO " +
//"and s.MO_LINE = h.MO_LINE " +
//"group by h.MO_NO,h.MO_LINE,h.WORKTYPE,s.OP_ID,s.RATE,s.RATE_XTR,s.SAM order by h.MO_NO, h.MO_LINE";

//                MySqlDataAdapter sda1 = new MySqlDataAdapter(query, dc.conn);
//                DataTable dtWip = new DataTable();
//                sda1.Fill(dtWip);
//                sda1.Dispose();
//                for (int j = 0; j < dtWip.Rows.Count; j++)
//                {
//                    String mo = dtWip.Rows[j][0].ToString();
//                    String moline = dtWip.Rows[j][1].ToString();
//                    String opcode = dtWip.Rows[j][2].ToString();
//                    String piecerate = dtWip.Rows[j][3].ToString();
//                    String overtimerate = dtWip.Rows[j][4].ToString();
//                    String sam = dtWip.Rows[j][5].ToString();
//                    int count = int.Parse(dtWip.Rows[j][6].ToString());
//                    String worktype = dtWip.Rows[j][7].ToString();

//                    //if (hide_ot == "TRUE")
//                    //{
//                    //    if (worktype == "1")
//                    //    {
//                    //        continue;
//                    //    }
//                    //}

//                    //operation id and desc
//                    String opdesc = "";
//                    cmd = new SqlCommand("select V_OPERATION_CODE,V_OPERATION_DESC from OPERATION_DB where V_ID='" + opcode + "'", dc.con);
//                    sdr = cmd.ExecuteReader();
//                    if (sdr.Read())
//                    {
//                        opcode = sdr.GetValue(0).ToString();
//                        opdesc = sdr.GetValue(1).ToString();
//                    }
//                    sdr.Close();

//                    //check if the production details is for overtime or normal time
//                    int normal = 0;
//                    int overtime = 0;
//                    if (worktype == "1")
//                    {
//                        overtime = count;
//                    }
//                    else
//                    {
//                        normal = count;
//                    }

//                    //check if all shift is selected
//                    if (cmbshift.Text == "All")
//                    {
//                        overtime = 0;
//                        normal = count;
//                        int flag = 0;
//                        for (int k = 0; k < MO.Rows.Count; k++)
//                        {
//                            if (MO.Rows[k][0].ToString() == mo && MO.Rows[k][1].ToString() == moline && MO.Rows[k][2].ToString() == opcode)
//                            {
//                                flag = 1;
//                                int temp = int.Parse(MO.Rows[k][7].ToString());
//                                temp += normal;
//                                MO.Rows[k][7] = temp;

//                                break;
//                            }
//                        }

//                        if (flag == 0)
//                        {
//                            MO.Rows.Add(mo, moline, opcode, opdesc, piecerate, overtimerate, sam, normal, overtime);
//                        }
//                    }
//                    else
//                    {
//                        int flag = 0;
//                        for (int k = 0; k < MO.Rows.Count; k++)
//                        {
//                            if (MO.Rows[k][0].ToString() == mo && MO.Rows[k][1].ToString() == moline && MO.Rows[k][2].ToString() == opcode)
//                            {
//                                flag = 1;
//                                int temp = int.Parse(MO.Rows[k][7].ToString());
//                                temp += normal;
//                                MO.Rows[k][7] = temp;

//                                temp = int.Parse(MO.Rows[k][8].ToString());
//                                temp += overtime;
//                                MO.Rows[k][8] = temp;

//                                break;
//                            }
//                        }

//                        if (flag == 0)
//                        {
//                            MO.Rows.Add(mo, moline, opcode, opdesc, piecerate, overtimerate, sam, normal, overtime);
//                        }
//                    }
//                }

                //----------------------------------------------------
                //check if all shift is selected
                if (cmbshift.Text == "All")
                {
                    query = "SELECT E.V_MO_NO,E.V_MO_LINE,OP.V_OPERATION_CODE,OP.V_OPERATION_DESC,OP.D_PIECERATE,OP.D_OVERTIME_RATE,OP.D_SAM,E.I_PIECE_COUNT from EDIT_RECORDS E,OPERATION_DB OP where E.V_EMP_ID='" + empid + "' and OP.V_OPERATION_CODE=E.V_OPERATION_CODE and E.D_DATETIME>='" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and E.D_DATETIME<='" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' and CONVERT(nvarchar(10), E.D_DATETIME , 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')";
                }
                else
                {
                    query = "SELECT E.V_MO_NO,E.V_MO_LINE,OP.V_OPERATION_CODE,OP.V_OPERATION_DESC,OP.D_PIECERATE,OP.D_OVERTIME_RATE,OP.D_SAM,E.I_PIECE_COUNT from EDIT_RECORDS E,OPERATION_DB OP where E.V_EMP_ID='" + empid + "' and OP.V_OPERATION_CODE=E.V_OPERATION_CODE and E.V_SHIFT='" + cmbshift.Text + "' and E.D_DATETIME>='" + start_time.ToString("yyyy-MM-dd") + " 00:00:00' and E.D_DATETIME<'" + end_time.ToString("yyyy-MM-dd") + " 23:59:59' and CONVERT(nvarchar(10), E.D_DATETIME , 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')";
                }

                //get production details from edit records
                sda = new SqlDataAdapter(query, dc.con);
                dt3 = new DataTable();
                sda.Fill(dt3);
                for (int i = 0; i < dt3.Rows.Count; i++)
                {
                    MO.Rows.Add(dt3.Rows[i][0].ToString(), dt3.Rows[i][1].ToString(), dt3.Rows[i][2].ToString(), dt3.Rows[i][3].ToString(), dt3.Rows[i][4].ToString(), dt3.Rows[i][5].ToString(), dt3.Rows[i][6].ToString(), dt3.Rows[i][7].ToString(), "0");
                }

                for (int i = 0; i < MO.Rows.Count; i++)
                {
                    String mo = MO.Rows[i][0].ToString();
                    String moline = MO.Rows[i][1].ToString();
                    String opcode = MO.Rows[i][2].ToString();
                    String opdesc = MO.Rows[i][3].ToString();
                    decimal piecerate = Convert.ToDecimal(MO.Rows[i][4].ToString());
                    decimal overtimerate = Convert.ToDecimal(MO.Rows[i][5].ToString());
                    int sam = int.Parse(MO.Rows[i][6].ToString());
                    int normal = int.Parse(MO.Rows[i][7].ToString());
                    int overtime = int.Parse(MO.Rows[i][8].ToString());

                    //check if the all shift is selected
                    if (cmbshift.Text == "All")
                    {
                        if (cmbline.Text == "All")
                        {
                            query = "SELECT CONVERT(DATE,TIME),MIN(TIME),MAX(TIME) FROM HANGER_HISTORY WHERE EMP_ID='" + empid + "' AND TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') GROUP BY CONVERT(DATE,TIME) ORDER BY CONVERT(DATE,TIME)";
                        }
                        else
                        {
                            query = "SELECT CONVERT(DATE,TIME),MIN(TIME),MAX(TIME) FROM HANGER_HISTORY h,STATION_DATA d WHERE EMP_ID='" + empid + "' AND TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and h.STN_ID=d.I_STN_ID and d.I_INFEED_LINE_NO='" + cmbline.Text + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') GROUP BY CONVERT(DATE,TIME) ORDER BY CONVERT(DATE,TIME)";
                        }
                    }
                    else
                    {
                        if (cmbline.Text == "All")
                        {
                            query = "SELECT CONVERT(DATE,TIME),MIN(TIME),MAX(TIME) FROM HANGER_HISTORY WHERE EMP_ID='" + empid + "' AND TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "'  and convert(char(5), TIME, 108) between(SELECT T.T_SHIFT_START_TIME FROM SHIFTS T WHERE V_SHIFT='" + cmbshift.Text + "') and (SELECT T.T_OVERTIME_END_TIME FROM SHIFTS T WHERE V_SHIFT='" + cmbshift.Text + "') and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') GROUP BY CONVERT(DATE,TIME) ORDER BY CONVERT(DATE,TIME)";
                        }
                        else
                        {
                            query = "SELECT CONVERT(DATE,TIME),MIN(TIME),MAX(TIME) FROM HANGER_HISTORY h,STATION_DATA d WHERE EMP_ID='" + empid + "' AND TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and h.STN_ID=d.I_STN_ID and d.I_INFEED_LINE_NO='" + cmbline.Text + "' and convert(char(5), TIME, 108) between(SELECT T.T_SHIFT_START_TIME FROM SHIFTS T WHERE V_SHIFT='" + cmbshift.Text + "') and (SELECT T.T_OVERTIME_END_TIME FROM SHIFTS T WHERE V_SHIFT='" + cmbshift.Text + "') and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') GROUP BY CONVERT(DATE,TIME) ORDER BY CONVERT(DATE,TIME)";
                        }
                    }

                    work_duration1 = 0;
                    //get first and last hanger time
                    sda = new SqlDataAdapter(query, dc.con);
                    dt3 = new DataTable();
                    sda.Fill(dt3);
                    for (int j = 0; j < dt3.Rows.Count; j++)
                    {
                        DateTime date1 = Convert.ToDateTime(dt3.Rows[j][1].ToString());
                        DateTime date2 = Convert.ToDateTime(dt3.Rows[j][2].ToString());
                        TimeSpan ts = date2 - date1;
                        work_duration1 += (int)ts.TotalMinutes;
                    }

                    //calculate target production
                    int target = (work_duration1 * 60 / sam);
                    decimal gross = (normal * piecerate) + (overtime * overtimerate);
                    decimal efficiency = 0;

                    //calculate efficiency
                    if (target > 0)
                    {
                        efficiency =(((decimal)normal + (decimal)overtime) / (decimal)target) * 100;
                    }

                    //add to datatable
                    EMP.Rows.Add(mo, moline, opcode, opdesc, piecerate, sam, normal, overtime, work_duration1, target, efficiency.ToString("0.##"), gross);
                }
                return EMP;
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
                DataTable dt = new DataTable();
                return dt;
            }
        }

        private void dgvemployee_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
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


        public void select_controller()
        {
            try
            {
                String ipaddress = "";
                String controller = "";
                string controller_name = "";

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
                radLabel15.Text = ex.Message;
            }
        }

    }
}
