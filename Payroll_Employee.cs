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
using Microsoft.Reporting.WinForms;


namespace SMARTMRT
{
    public partial class Payroll_Employee : Telerik.WinControls.UI.RadForm
    {
        public Payroll_Employee()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();  //connection class
        DataTable MO = new DataTable();
        String empid = "";
        String empname = "";
        String line = "";
        DateTime start_time;
        DateTime end_time;
        int basic_pay = 0;
        String shift = "";
        DataTable data1 = new DataTable();

        private void Payroll_Employee_Load(object sender, EventArgs e)
        {
            select_controller();  //get the selected controller
            dgvemployee.MasterTemplate.SelectLastAddedRow = false;
            dgvemployee.MasterView.TableSearchRow.ShowCloseButton = false;   //disable close button on search in grid
            
            //get employee details
            txtempid.Text = empid;
            txtempname.Text = empname;
            txtshift.Text = shift;
            lblfrom.Text = start_time.ToString("yyyy-MM-dd");
            lblto.Text = end_time.ToString("yyyy-MM-dd");
            this.CenterToScreen();   //keep form centered to screen

            MO.Columns.Add("OPCODE");
            MO.Columns.Add("OPDESC");
            MO.Columns.Add("Start Date");
            MO.Columns.Add("End Date");
            MO.Columns.Add("SAM");
            MO.Columns.Add("Actual SAM");
            MO.Columns.Add("Piece Count");
            MO.Columns.Add("Work Duration");
            MO.Columns.Add("Efficiency");
            MO.Columns.Add("Piece rate");
            MO.Columns.Add("Overtime");
            MO.Columns.Add("Overtime Rate");
            MO.Columns.Add("MO");
            MO.Columns.Add("MOLINE");
            MO.Columns.Add("Seq");

            data1.Columns.Add("empid");
            data1.Columns.Add("empname");
            data1.Columns.Add("date1");
            data1.Columns.Add("date2");
            data1.Columns.Add("shift");
            data1.Columns.Add("totaldays");
            data1.Columns.Add("totalpiece");
            data1.Columns.Add("totalpay");
            data1.Columns.Add("netpay");
            data1.Columns.Add("mono");
            data1.Columns.Add("modetails");
            data1.Columns.Add("opcode");
            data1.Columns.Add("opdesc");
            data1.Columns.Add("sam");
            data1.Columns.Add("peicerate");
            data1.Columns.Add("ntpeicecount");
            data1.Columns.Add("oppeicecount");
            data1.Columns.Add("totalduration");
            data1.Columns.Add("targetproduction");
            data1.Columns.Add("efficiency");
            data1.Columns.Add("grosspay");
            data1.Columns.Add("image", typeof(byte[]));

            byte[] a = null;

            //get employee image
            SqlDataAdapter sda = new SqlDataAdapter("select IMG_IMAGE from EMPLOYEE where V_EMP_ID='" + empid + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                a = (byte[])dt.Rows[i][0];
            }

            //convert byte[] to image
            if (a != null)
            {
                MemoryStream ms = new MemoryStream(a);
                pictureBox1.Image = Image.FromStream(ms);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            }

            DataTable emp = Calculate_Payroll(empid).Copy();   //calculate payroll
            for (int i = 0; i < emp.Rows.Count; i++)
            {
                dgvemployee.Rows.Add(emp.Rows[i][0].ToString(), emp.Rows[i][1].ToString(), emp.Rows[i][2].ToString(), emp.Rows[i][3].ToString(), emp.Rows[i][4].ToString(), emp.Rows[i][5].ToString(), emp.Rows[i][6].ToString(), emp.Rows[i][7].ToString(), emp.Rows[i][8].ToString(), emp.Rows[i][9].ToString(), emp.Rows[i][10].ToString(), emp.Rows[i][11].ToString());
            }

            //check if net > basic
            txtnetpay.Text = basic_pay + "";
            if (int.Parse(txttotalpay.Text) > basic_pay)
            {
                txtnetpay.Text = txttotalpay.Text;
            }

            //add to datatable
            for (int i = 0; i < dgvemployee.Rows.Count; i++)
            {
                data1.Rows.Add(empid, empname, start_time, end_time, shift, txttotaldays.Text, txttotalpiece.Text, txttotalpay.Text, txtnetpay.Text, dgvemployee.Rows[i].Cells[0].Value.ToString(), dgvemployee.Rows[i].Cells[1].Value.ToString(), dgvemployee.Rows[i].Cells[2].Value.ToString(), dgvemployee.Rows[i].Cells[3].Value.ToString(), dgvemployee.Rows[i].Cells[4].Value.ToString(), dgvemployee.Rows[i].Cells[5].Value.ToString(), dgvemployee.Rows[i].Cells[6].Value.ToString(), dgvemployee.Rows[i].Cells[7].Value.ToString(), dgvemployee.Rows[i].Cells[8].Value.ToString(), dgvemployee.Rows[i].Cells[9].Value.ToString(), dgvemployee.Rows[i].Cells[10].Value.ToString(), dgvemployee.Rows[i].Cells[11].Value.ToString(), a);
            }
        }

        public DataTable Calculate_Payroll(String empid)
        {
            try
            {
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
                int totalpay = 0;
                int totalpiece = 0;

                DateTime shift_start = Convert.ToDateTime("9:30:00");
                DateTime shift_end = Convert.ToDateTime("18:30:00");
                DateTime overtime_end = Convert.ToDateTime("19:30:00");
                DateTime current_time = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));

                //get shift details
                SqlCommand cmd = new SqlCommand("select T_SHIFT_START_TIME,T_SHIFT_END_TIME,T_OVERTIME_END_TIME from SHIFTS where V_SHIFT='" + shift + "'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    shift_start = Convert.ToDateTime(sdr.GetValue(0).ToString());
                    shift_end = Convert.ToDateTime(sdr.GetValue(1).ToString());
                    overtime_end = Convert.ToDateTime(sdr.GetValue(2).ToString());
                }
                sdr.Close();

                //get shift break complete time
                cmd = new SqlCommand("select I_BREAK_TIMESPAN from SHIFT_BREAKS where V_SHIFT<='" + shift + "'", dc.con);
                sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    breaktime_complete = breaktime_complete + int.Parse(sdr.GetValue(0).ToString());
                }
                sdr.Close();

                //check if hide overtime is enabled
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

                //check all shift is selected
                int work_duration1 = 0;
                String query = "";
                if (shift == "All")
                {
                    sht_start = "00:00:00";
                    sht_end = "23:59:59";
                    dgvemployee.Columns[7].IsVisible = false;
                    dgvemployee.Columns[6].HeaderText = "Piece Count";
                    if (line == "All")
                    {
                        query = "SELECT h.MO_NO, h.MO_LINE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM,SUM(h.PC_COUNT) as COUNT,WORKTYPE FROM HANGER_HISTORY h,SEQUENCE_OPERATION s where h.EMP_ID = '" + empid + "' and h.TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and h.TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10), D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE = 'TRUE') and s.V_SEQUENCE_NO=h.SEQ_NO and s.V_MO_NO=h.MO_NO and s.V_MO_LINE=h.MO_LINE group by h.MO_NO,h.MO_LINE,h.WORKTYPE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM order by h.MO_NO, h.MO_LINE";
                    }
                    else
                    {
                        query = "SELECT h.MO_NO, h.MO_LINE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM,SUM(h.PC_COUNT) as COUNT,WORKTYPE FROM HANGER_HISTORY h,SEQUENCE_OPERATION s,STATION_DATA d where h.EMP_ID = '" + empid + "' and h.TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and h.TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' and h.STN_ID=d.I_STN_ID and d.I_INFEED_LINE_NO='" + line + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10), D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE = 'TRUE') and s.V_SEQUENCE_NO=h.SEQ_NO and s.V_MO_NO=h.MO_NO and s.V_MO_LINE=h.MO_LINE group by h.MO_NO,h.MO_LINE,h.WORKTYPE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM order by h.MO_NO, h.MO_LINE";
                    }
                }
                else
                {
                    dgvemployee.Columns[7].IsVisible = true;
                    dgvemployee.Columns[6].HeaderText = "NT Piece Count";
                    if (line == "All")
                    {
                        query = "SELECT h.MO_NO, h.MO_LINE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM,SUM(h.PC_COUNT) as COUNT,WORKTYPE FROM HANGER_HISTORY h,SEQUENCE_OPERATION s where h.EMP_ID = '" + empid + "' and h.TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and h.TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "'  and convert(char(5), TIME, 108) between(SELECT T.T_SHIFT_START_TIME FROM SHIFTS T WHERE V_SHIFT='" + shift + "') and (SELECT T.T_OVERTIME_END_TIME FROM SHIFTS T WHERE V_SHIFT='" + shift + "') and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10), D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE = 'TRUE') and s.V_SEQUENCE_NO=h.SEQ_NO and s.V_MO_NO=h.MO_NO and s.V_MO_LINE=h.MO_LINE group by h.MO_NO,h.MO_LINE,h.WORKTYPE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM order by h.MO_NO, h.MO_LINE";
                    }
                    else
                    {
                        query = "SELECT h.MO_NO, h.MO_LINE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM,SUM(h.PC_COUNT) as COUNT,WORKTYPE FROM HANGER_HISTORY h,SEQUENCE_OPERATION s,STATION_DATA d where h.EMP_ID = '" + empid + "' and h.TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and h.TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "'  and convert(char(5), TIME, 108) between(SELECT T.T_SHIFT_START_TIME FROM SHIFTS T WHERE V_SHIFT='" + shift + "') and (SELECT T.T_OVERTIME_END_TIME FROM SHIFTS T WHERE V_SHIFT='" + shift + "') and h.STN_ID=d.I_STN_ID and d.I_INFEED_LINE_NO='" + line + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10), D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE = 'TRUE') and s.V_SEQUENCE_NO=h.SEQ_NO and s.V_MO_NO=h.MO_NO and s.V_MO_LINE=h.MO_LINE group by h.MO_NO,h.MO_LINE,h.WORKTYPE,s.V_OPERATION_ID,s.D_PIECE_RATE,s.D_OVERTIME_RATE,s.D_SAM order by h.MO_NO, h.MO_LINE";
                    }
                }

                    //get production detials
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
                    String count = dt3.Rows[i][6].ToString();
                    String worktype = dt3.Rows[i][7].ToString();
                    if (hide_ot == "TRUE")
                    {
                        if (worktype == "1")
                        {
                            continue;
                        }
                    }

                    //get the operation id and desc
                    String opdesc = "";
                    cmd = new SqlCommand("select V_OPERATION_CODE,V_OPERATION_DESC from OPERATION_DB where V_ID='" + opcode + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        opcode = sdr.GetValue(0).ToString();
                        opdesc = sdr.GetValue(1).ToString();
                    }
                    sdr.Close();

                    String normal = "0";
                    String overtime = "0";
                    if (worktype == "1")
                    {
                        overtime = count;
                    }
                    else
                    {
                        normal = count;
                    }

                    //check all shift is selected
                    if (shift == "All")
                    {
                        overtime = "0";
                        normal = count;
                        int flag = 0;
                        for (int j = 0; j < MO.Rows.Count; j++)
                        {
                            if (MO.Rows[j][0].ToString() == mo && MO.Rows[j][1].ToString() == moline && MO.Rows[j][2].ToString() == opcode)
                            {
                                flag = 1;
                                int temp = int.Parse(MO.Rows[j][7].ToString());
                                temp += int.Parse(normal);
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
                                temp += int.Parse(normal);
                                MO.Rows[j][7] = temp;

                                temp = int.Parse(MO.Rows[j][8].ToString());
                                temp += int.Parse(overtime);
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

                //Hanafi | Date:03 / 08 / 2021 | removed due to the changes in data transfer process
                //Programmer: Hanafi | Date: 14 / 07 / 2021 | Ver:2.0.0.1 | Changes: add piece count from hangerwip in mrt_local db
                //get from hangerwip

        //        query = "SELECT h.MO_NO, h.MO_LINE,s.OP_ID,s.RATE,s.RATE_XTR,s.SAM,SUM(h.PC_COUNT) as COUNT,WORKTYPE FROM hangerwip h,sequenceoperations s " +
        //"where h.EMP_ID = '" + empid + "' " +
        //"and h.TIME >= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and h.TIME <= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' " +
        //"AND h.TIME BETWEEN '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' AND '" + start_time.ToString("yyyy-MM-dd") + " " + overtime_end.ToString("HH:mm:ss") + "' " +
        //"and s.SEQ_NO = h.SEQ_NO " +
        //"and s.MO_NO = h.MO_NO " +
        //"and s.MO_LINE = h.MO_LINE " +
        //"group by h.MO_NO,h.MO_LINE,h.WORKTYPE,s.OP_ID,s.RATE,s.RATE_XTR,s.SAM order by h.MO_NO, h.MO_LINE";

        //        MySqlDataAdapter sda1 = new MySqlDataAdapter(query, dc.conn);
        //        DataTable dtWip = new DataTable();
        //        sda1.Fill(dtWip);
        //        sda1.Dispose();
        //        for (int j = 0; j < dtWip.Rows.Count; j++)
        //        {
        //            String mo = dtWip.Rows[j][0].ToString();
        //            String moline = dtWip.Rows[j][1].ToString();
        //            String opcode = dtWip.Rows[j][2].ToString();
        //            String piecerate = dtWip.Rows[j][3].ToString();
        //            String overtimerate = dtWip.Rows[j][4].ToString();
        //            String sam = dtWip.Rows[j][5].ToString();
        //            int count = int.Parse(dtWip.Rows[j][6].ToString());
        //            String worktype = dtWip.Rows[j][7].ToString();

        //            //if (hide_ot == "TRUE")
        //            //{
        //            //    if (worktype == "1")
        //            //    {
        //            //        continue;
        //            //    }
        //            //}

        //            //operation id and desc
        //            String opdesc = "";
        //            cmd = new SqlCommand("select V_OPERATION_CODE,V_OPERATION_DESC from OPERATION_DB where V_ID='" + opcode + "'", dc.con);
        //            sdr = cmd.ExecuteReader();
        //            if (sdr.Read())
        //            {
        //                opcode = sdr.GetValue(0).ToString();
        //                opdesc = sdr.GetValue(1).ToString();
        //            }
        //            sdr.Close();

        //            //check if the production details is for overtime or normal time
        //            int normal = 0;
        //            int overtime = 0;
        //            if (worktype == "1")
        //            {
        //                overtime = count;
        //            }
        //            else
        //            {
        //                normal = count;
        //            }

        //            //check if all shift is selected
        //            if (shift == "All")
        //            {
        //                overtime = 0;
        //                normal = count;
        //                int flag = 0;
        //                for (int k = 0; k < MO.Rows.Count; k++)
        //                {
        //                    if (MO.Rows[k][0].ToString() == mo && MO.Rows[k][1].ToString() == moline && MO.Rows[k][2].ToString() == opcode)
        //                    {
        //                        flag = 1;
        //                        int temp = int.Parse(MO.Rows[k][7].ToString());
        //                        temp += normal;
        //                        MO.Rows[k][7] = temp;

        //                        break;
        //                    }
        //                }

        //                if (flag == 0)
        //                {
        //                    MO.Rows.Add(mo, moline, opcode, opdesc, piecerate, overtimerate, sam, normal, overtime);
        //                }
        //            }
        //            else
        //            {
        //                int flag = 0;
        //                for (int k = 0; k < MO.Rows.Count; k++)
        //                {
        //                    if (MO.Rows[k][0].ToString() == mo && MO.Rows[k][1].ToString() == moline && MO.Rows[k][2].ToString() == opcode)
        //                    {
        //                        flag = 1;
        //                        int temp = int.Parse(MO.Rows[k][7].ToString());
        //                        temp += normal;
        //                        MO.Rows[k][7] = temp;

        //                        temp = int.Parse(MO.Rows[k][8].ToString());
        //                        temp += overtime;
        //                        MO.Rows[k][8] = temp;

        //                        break;
        //                    }
        //                }

        //                if (flag == 0)
        //                {
        //                    MO.Rows.Add(mo, moline, opcode, opdesc, piecerate, overtimerate, sam, normal, overtime);
        //                }
        //            }
        //        }

                //--------------------------------------------------------------

                //check all shift is selected
                if (shift == "All")
                {
                    query = "SELECT E.V_MO_NO,E.V_MO_LINE,OP.V_OPERATION_CODE,OP.V_OPERATION_DESC,OP.D_PIECERATE,OP.D_OVERTIME_RATE,OP.D_SAM,E.I_PIECE_COUNT from EDIT_RECORDS E,OPERATION_DB OP where E.V_EMP_ID='" + empid + "' and OP.V_OPERATION_CODE=E.V_OPERATION_CODE and E.D_DATETIME>='" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and E.D_DATETIME<='" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' and CONVERT(nvarchar(10), E.D_DATETIME , 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')";
                }
                else
                {
                    query = "SELECT E.V_MO_NO,E.V_MO_LINE,OP.V_OPERATION_CODE,OP.V_OPERATION_DESC,OP.D_PIECERATE,OP.D_OVERTIME_RATE,OP.D_SAM,E.I_PIECE_COUNT from EDIT_RECORDS E,OPERATION_DB OP where E.V_EMP_ID='" + empid + "' and OP.V_OPERATION_CODE=E.V_OPERATION_CODE and E.V_SHIFT='" + shift + "' and E.D_DATETIME>='" + start_time.ToString("yyyy-MM-dd") + " 00:00:00' and E.D_DATETIME<='" + end_time.ToString("yyyy-MM-dd") + " 23:59:59' and CONVERT(nvarchar(10), E.D_DATETIME , 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')";
                }

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

                    //check all shift is selected
                    if (shift == "All")
                    {
                        if (line == "All")
                        {
                            query = "SELECT CONVERT(DATE,TIME),MIN(TIME),MAX(TIME) FROM HANGER_HISTORY WHERE EMP_ID='" + empid + "' AND TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') GROUP BY CONVERT(DATE,TIME) ORDER BY CONVERT(DATE,TIME)";
                        }
                        else
                        {
                            query = "SELECT CONVERT(DATE,TIME),MIN(TIME),MAX(TIME) FROM HANGER_HISTORY h,STATION_DATA d WHERE EMP_ID='" + empid + "' AND TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and h.STN_ID=d.I_STN_ID and d.I_INFEED_LINE_NO='" + line + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') GROUP BY CONVERT(DATE,TIME) ORDER BY CONVERT(DATE,TIME)";
                        }
                    }
                    else
                    {
                        if (line == "All")
                        {
                            query = "SELECT CONVERT(DATE,TIME),MIN(TIME),MAX(TIME) FROM HANGER_HISTORY WHERE EMP_ID='" + empid + "' AND TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "'  and convert(char(5), TIME, 108) between(SELECT T.T_SHIFT_START_TIME FROM SHIFTS T WHERE V_SHIFT='" + shift + "') and (SELECT T.T_OVERTIME_END_TIME FROM SHIFTS T WHERE V_SHIFT='" + shift + "') and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') GROUP BY CONVERT(DATE,TIME) ORDER BY CONVERT(DATE,TIME)";
                        }
                        else
                        {
                            query = "SELECT CONVERT(DATE,TIME),MIN(TIME),MAX(TIME) FROM HANGER_HISTORY h,STATION_DATA d WHERE EMP_ID='" + empid + "' AND TIME>= '" + start_time.ToString("yyyy-MM-dd") + " " + sht_start + "' and TIME<= '" + end_time.ToString("yyyy-MM-dd") + " " + sht_end + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and h.STN_ID=d.I_STN_ID and d.I_INFEED_LINE_NO='" + line + "' and convert(char(5), TIME, 108) between(SELECT T.T_SHIFT_START_TIME FROM SHIFTS T WHERE V_SHIFT='" + shift + "') and (SELECT T.T_OVERTIME_END_TIME FROM SHIFTS T WHERE V_SHIFT='" + shift + "') and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') GROUP BY CONVERT(DATE,TIME) ORDER BY CONVERT(DATE,TIME)";
                        }
                    }

                    //get first and last hanger time
                    work_duration1 = 0;
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

                    //calculate target production and gross pay
                    int target = (work_duration1 * 60 / sam);
                    decimal gross = (normal * piecerate) + (overtime * overtimerate);
                    decimal efficiency = 0;

                    //calculate efficiency
                    if (target > 0)
                    {
                        efficiency = (((decimal)normal + (decimal)overtime) / (decimal)target) * 100;
                    }

                    //add ti datatable
                    totalpay += (int)gross;
                    totalpiece += (normal + overtime);
                    EMP.Rows.Add(mo, moline, opcode, opdesc, piecerate, sam, normal, overtime, work_duration1, target, efficiency.ToString("0.##") + " %", gross);
                }

                txttotalpay.Text = totalpay.ToString("0.##");
                txttotalpiece.Text = totalpiece+"";
                TimeSpan ts_day = new TimeSpan();
                ts_day = end_time - start_time;
                txttotaldays.Text = ts_day.TotalDays.ToString();
                return EMP;
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public void getData(String id, String name, String pay, String start, String end,String shft,String lne)
        {
            //get employee details
            empid = id;
            empname = name;
            start_time = Convert.ToDateTime(start);
            end_time = Convert.ToDateTime(end);

            if (start_time > end_time)
            {
                start_time = Convert.ToDateTime(end);
                end_time = Convert.ToDateTime(start);
            }

            basic_pay = int.Parse(pay);
            shift = shft;
            line = lne;
        }

        String theme = "";

        private void Payroll_Employee_Initialized(object sender, EventArgs e)
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
            dgvemployee.ThemeName = theme;
        }

        private void radLabel15_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            panel4.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel15.Text = "";
            panel4.Visible = false;
            MyTimer.Stop();
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            //check if report button is clicked
            if (btnreport.Text == "Report View")
            {
                reportViewer1.Visible = true;
                btnreport.Text = "Table View";
                DataView view = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Payroll_Employee.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                //add views to dataset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer1.RefreshReport();
            }
            else
            {
                reportViewer1.Visible = false;
                btnreport.Text = "Report View";
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
