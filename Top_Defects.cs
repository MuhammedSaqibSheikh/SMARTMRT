using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Telerik.Charting;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Top_Defects : Telerik.WinControls.UI.RadForm
    {
        public Top_Defects()
        {
            InitializeComponent();
        }

        DataTable data1 = new DataTable();
        Database_Connection dc = new Database_Connection();   //connection class

        private void Top_Defects_Load(object sender, EventArgs e)
        {
            dgvtopdefects.MasterTemplate.SelectLastAddedRow = false;
            dgvtopdefects.MasterView.TableSearchRow.ShowCloseButton = false;    //disable close button of search in grid
            data1.Columns.Add("I_STATION_ID");
            data1.Columns.Add("V_MO_NO");
            data1.Columns.Add("V_MO_LINE");
            data1.Columns.Add("V_EMP_ID");
            data1.Columns.Add("V_EMP_NAME");
            data1.Columns.Add("V_COLOR");
            data1.Columns.Add("V_ARTICLE");
            data1.Columns.Add("V_SIZE");
            data1.Columns.Add("V_OP_CODE");
            data1.Columns.Add("V_OP_DESC");
            data1.Columns.Add("V_QC_MAIN_CODE");
            data1.Columns.Add("V_QC_MAIN_DESC");
            data1.Columns.Add("V_QC_SUB_CODE");
            data1.Columns.Add("V_QC_SUB_DESC");
            data1.Columns.Add("SUB_QUANTITY");
            data1.Columns.Add("D_DATE_TIME");
            data1.Columns.Add("TOTAL_QUANTITY");
            data1.Columns.Add("N");
            data1.Columns.Add("SUB_DEFECT_RATE");
            data1.Columns.Add("TOTAL_DEFECT_RATE");
            data1.Columns.Add("PRODUCTION");
            data1.Columns.Add("D_DATE_TIME1");

            WindowState = FormWindowState.Maximized;

            dtpstart.Text = DateTime.Now.ToString();
            dtpend.Text = DateTime.Now.ToString();
            Get_TopDefects();   //get top defects
            this.reportViewer1.RefreshReport();
        }

        String theme = "";
        private void Top_Defects_Initialized(object sender, EventArgs e)
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
            dgvtopdefects.ThemeName = theme;
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            Get_TopDefects();   //get top defects
        }

        public void Get_TopDefects()
        {
            try
            {
                data1.Rows.Clear();
                dgvtopdefects.Rows.Clear();

                reportViewer1.Visible = false;
                btnreport.Text = "Report View";

                btnchart.Visible = false;
                panel4.Visible = false;
                btnchart.Text = "Show Chart";

                String start = dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                String end = dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59";


                String query = "SELECT TOP(" + txttop.Text + ") I_STATION_ID,V_MO_NO,V_MO_LINE,V_EMP_ID,V_EMP_NAME, V_COLOR,V_ARTICLE, V_SIZE, V_OP_CODE, V_OP_DESC, V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC,sum(I_QUANTITY) AS QUANTITY FROM QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' GROUP BY I_STATION_ID,V_MO_NO,V_MO_LINE,V_EMP_ID,V_EMP_NAME, V_COLOR,V_ARTICLE, V_SIZE, V_OP_CODE, V_OP_DESC, V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC";
                if (chkmono.Checked == true || chkemployee.Checked == true || chkoperation.Checked == true || chkstation.Checked == true)
                {
                    btnchart.Visible = true;
                    radChartView3.Series.Clear();
                    if (chkmono.Checked == true)
                    {
                        //get mo wise top defects
                        radChartView3.Title = "Top Defects - MO";
                        query = "select TOP(" + txttop.Text + ") V_MO_NO,V_MO_LINE,sum(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' GROUP BY V_MO_NO,V_MO_LINE order by sum(I_QUANTITY) DESC";
                        
                        SqlDataAdapter sda1 = new SqlDataAdapter(query, dc.con);
                        DataTable dt2 = new DataTable();
                        sda1.Fill(dt2);
                        sda1.Dispose();
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            BarSeries barSeries1 = new BarSeries("Performance", "RepresentativeName");
                            
                            String mo = dt2.Rows[j][0].ToString();
                            String moline = dt2.Rows[j][1].ToString();
                            int qty = 0;
                            if (dt2.Rows[j][2].ToString() != "")
                            {
                                qty = int.Parse(dt2.Rows[j][2].ToString());
                            }

                            //get mo wise top defects
                            query = "SELECT V_MO_NO,V_MO_LINE,V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC,sum(I_QUANTITY) AS QUANTITY FROM QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' GROUP BY V_MO_NO,V_MO_LINE,V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC order by sum(I_QUANTITY) DESC";
                            sda1 = new SqlDataAdapter(query, dc.con);
                            DataTable dt1 = new DataTable();
                            sda1.Fill(dt1);
                            sda1.Dispose();
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                //get production details for the mo
                                SqlCommand cmd = new SqlCommand("Select SUM(PC_COUNT) from HANGER_HISTORY where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='2' and TIME>='" + start + "' and TIME<'" + end + "'", dc.con);
                                String temp = cmd.ExecuteScalar() + "";
                                
                                int count = 0;
                                if (temp != "")
                                {
                                    count = int.Parse(temp);
                                }
                                else
                                {
                                    count = 0;
                                }

                                //calculate sub qty
                                int subqty = 0;
                                if (dt1.Rows[i][6].ToString() != "")
                                {
                                    subqty = int.Parse(dt1.Rows[i][6].ToString());
                                }

                                //calculate sub efficiency
                                decimal sub_eff = 0;
                                if (qty != 0)
                                {
                                    sub_eff = ((decimal)subqty / (decimal)qty) * 100;
                                }

                                //calculate total efficiency
                                decimal tot_eff = 0;
                                if (count != 0)
                                {
                                    tot_eff = ((decimal)qty / (decimal)count) * 100;
                                }

                                dgvtopdefects.Rows.Add("", dt1.Rows[i][0].ToString(), dt1.Rows[i][1].ToString(), "", "", "", "", "", "", "", dt1.Rows[i][2].ToString(), dt1.Rows[i][3].ToString(), dt1.Rows[i][4].ToString(), dt1.Rows[i][5].ToString(), dt1.Rows[i][6].ToString(), sub_eff.ToString("0.##") + "%", qty, count, tot_eff.ToString("0.##") + "%");
                                data1.Rows.Add("", dt1.Rows[i][0].ToString(), dt1.Rows[i][1].ToString(), "", "", "", "", "", "", "", dt1.Rows[i][2].ToString(), dt1.Rows[i][3].ToString(), dt1.Rows[i][4].ToString(), dt1.Rows[i][5].ToString(), dt1.Rows[i][6].ToString(), start, qty, txttop.Text, sub_eff.ToString("0.##") + "%", tot_eff.ToString("0.##") + "%", count, end);
                            }

                            //generate chart
                            barSeries1.ForeColor = Color.White;
                            barSeries1.DataPoints.Add(new CategoricalDataPoint(qty, mo + "-" + moline));
                            radChartView3.Series.Add(barSeries1);
                            barSeries1.ShowLabels = true;

                            LinearAxis verticalAxis1 = radChartView3.Axes[1] as LinearAxis;
                            verticalAxis1.LabelFitMode = AxisLabelFitMode.MultiLine;
                            verticalAxis1.ForeColor = Color.White;
                            verticalAxis1.BorderColor = Color.DodgerBlue;
                            verticalAxis1.ShowLabels = false;
                            verticalAxis1.Title = "Quantity";

                            CategoricalAxis ca1 = radChartView3.Axes[0] as CategoricalAxis;
                            ca1.LabelFitMode = AxisLabelFitMode.MultiLine;
                            ca1.Title = "MO No";
                            ca1.ForeColor = Color.White;
                            ca1.BorderColor = Color.DodgerBlue;
                        }
                    }
                    else if (chkemployee.Checked == true)
                    {
                        radChartView3.Title = "Top Defects - Employee";

                        //get emp wise top defects
                        query = "select TOP(" + txttop.Text + ") V_EMP_ID,V_EMP_NAME,sum(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' GROUP BY V_EMP_ID,V_EMP_NAME order by sum(I_QUANTITY) DESC";
                        
                        SqlDataAdapter sda1 = new SqlDataAdapter(query, dc.con);
                        DataTable dt2 = new DataTable();
                        sda1.Fill(dt2);
                        sda1.Dispose();
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            BarSeries barSeries1 = new BarSeries("Performance", "RepresentativeName");
                            
                            String empid = dt2.Rows[j][0].ToString();
                            int qty = 0;
                            if (dt2.Rows[j][2].ToString() != "")
                            {
                                qty = int.Parse(dt2.Rows[j][2].ToString());
                            }

                            int count = 0;

                            //get production details for the mo
                            sda1 = new SqlDataAdapter("select MO_NO,MO_LINE,SUM(PC_COUNT) from HANGER_HISTORY where TIME>='" + start + "' and TIME<'" + end + "' and EMP_ID='" + empid + "' GROUP BY MO_NO,MO_LINE", dc.con);
                            DataTable dt1 = new DataTable();
                            sda1.Fill(dt1);
                            sda1.Dispose();
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                String temp = dt1.Rows[i][2].ToString();
                                if (temp != "")
                                {
                                    count = int.Parse(temp);
                                }
                                else
                                {
                                    count = 0;
                                }
                            }

                            //get employee wise top defects
                            query = "SELECT  V_EMP_ID,V_EMP_NAME,V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC,sum(I_QUANTITY) AS QUANTITY FROM QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and V_EMP_ID='" + empid + "' GROUP BY V_EMP_ID,V_EMP_NAME,V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC order by sum(I_QUANTITY) DESC";
                            sda1 = new SqlDataAdapter(query, dc.con);
                            dt1 = new DataTable();
                            sda1.Fill(dt1);
                            sda1.Dispose();
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                //calculate sub qty
                                int subqty = 0;
                                if (dt1.Rows[i][6].ToString() != "")
                                {
                                    subqty = int.Parse(dt1.Rows[i][6].ToString());
                                }

                                //calculate sub efficiency
                                decimal sub_eff = 0;
                                if (qty != 0)
                                {
                                    sub_eff = ((decimal)subqty / (decimal)qty) * 100;
                                }

                                //calculate total efficiency
                                decimal tot_eff = 0;
                                if (count != 0)
                                {
                                    tot_eff = ((decimal)qty / (decimal)count) * 100;
                                }

                                dgvtopdefects.Rows.Add("", "", "", dt1.Rows[i][0].ToString(), dt1.Rows[i][1].ToString(), "", "", "", "", "", dt1.Rows[i][2].ToString(), dt1.Rows[i][3].ToString(), dt1.Rows[i][4].ToString(), dt1.Rows[i][5].ToString(), dt1.Rows[i][6].ToString(), sub_eff.ToString("0.##") + "%", qty, count, tot_eff.ToString("0.##") + "%");
                                data1.Rows.Add("", "", "", dt1.Rows[i][0].ToString(), dt1.Rows[i][1].ToString(), "", "", "", "", "", dt1.Rows[i][2].ToString(), dt1.Rows[i][3].ToString(), dt1.Rows[i][4].ToString(), dt1.Rows[i][5].ToString(), dt1.Rows[i][6].ToString(), start, qty, txttop.Text, sub_eff.ToString("0.##") + "%", tot_eff.ToString("0.##") + "%", count, end);
                            }

                            //generate chart
                            barSeries1.ForeColor = Color.White;
                            barSeries1.DataPoints.Add(new CategoricalDataPoint(qty, dt2.Rows[j][1].ToString()));
                            radChartView3.Series.Add(barSeries1);
                            barSeries1.ShowLabels = true;

                            LinearAxis verticalAxis1 = radChartView3.Axes[1] as LinearAxis;
                            verticalAxis1.LabelFitMode = AxisLabelFitMode.MultiLine;
                            verticalAxis1.ForeColor = Color.White;
                            verticalAxis1.BorderColor = Color.DodgerBlue;
                            verticalAxis1.ShowLabels = false;
                            verticalAxis1.Title = "Quantity";

                            CategoricalAxis ca1 = radChartView3.Axes[0] as CategoricalAxis;
                            ca1.LabelFitMode = AxisLabelFitMode.MultiLine;
                            ca1.Title = "Employee";
                            ca1.ForeColor = Color.White;
                            ca1.BorderColor = Color.DodgerBlue;
                        }
                    }
                    else if (chkoperation.Checked == true)
                    {
                        radChartView3.Title = "Top Defects - Operation";

                        //get operation wise top defects
                        query = "select TOP(" + txttop.Text + ") V_OP_CODE,V_OP_DESC,sum(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' GROUP BY V_OP_CODE,V_OP_DESC order by sum(I_QUANTITY) DESC";
                        SqlDataAdapter sda1 = new SqlDataAdapter(query, dc.con);
                        DataTable dt2 = new DataTable();
                        sda1.Fill(dt2);
                        sda1.Dispose();
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            BarSeries barSeries1 = new BarSeries("Performance", "RepresentativeName");
                            String opcode = dt2.Rows[j][0].ToString();
                            int qty = 0;
                            if (dt2.Rows[j][2].ToString() != "")
                            {
                                qty = int.Parse(dt2.Rows[j][2].ToString());
                            }

                            int count = 0;

                            //get production details for the mo
                            sda1 = new SqlDataAdapter("select distinct V_MO_NO,V_MO_LINE,I_SEQUENCE_NO from QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and V_OP_CODE='" + opcode + "'", dc.con);
                            DataTable dt1 = new DataTable();
                            sda1.Fill(dt1);
                            sda1.Dispose();
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                sda1 = new SqlDataAdapter("select SUM(PC_COUNT) from HANGER_HISTORY where TIME>='" + start + "' and TIME<'" + end + "' and MO_NO='" + dt1.Rows[i][0].ToString() + "' and MO_LINE='" + dt1.Rows[i][1].ToString() + "' and SEQ_NO='" + dt1.Rows[i][2].ToString() + "'", dc.con);
                                DataTable dt3 = new DataTable();
                                sda1.Fill(dt3);
                                sda1.Dispose();
                                for (int k = 0; k < dt3.Rows.Count; k++)
                                {
                                    String temp = dt3.Rows[k][0].ToString();
                                    if (temp != "")
                                    {
                                        count = int.Parse(temp);
                                    }
                                    else
                                    {
                                        count = 0;
                                    }
                                }
                            }

                            //get operartion wise top defects
                            query = "SELECT V_OP_CODE,V_OP_DESC,V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC,sum(I_QUANTITY) AS QUANTITY FROM QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and V_OP_CODE='" + opcode + "' GROUP BY V_OP_CODE,V_OP_DESC,V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC order by sum(I_QUANTITY) DESC";
                            sda1 = new SqlDataAdapter(query, dc.con);
                            dt1 = new DataTable();
                            sda1.Fill(dt1);
                            sda1.Dispose();
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                //calculate sub qty
                                int subqty = 0;
                                if (dt1.Rows[i][6].ToString() != "")
                                {
                                    subqty = int.Parse(dt1.Rows[i][6].ToString());
                                }

                                //calculate sub efficiency
                                decimal sub_eff = 0;
                                if (qty != 0)
                                {
                                    sub_eff = ((decimal)subqty / (decimal)qty) * 100;
                                }

                                //calculate total efficiency
                                decimal tot_eff = 0;
                                if (count != 0)
                                {
                                    tot_eff = ((decimal)qty / (decimal)count) * 100;
                                }

                                dgvtopdefects.Rows.Add("", "", "", "", "", "", "", "", dt1.Rows[i][0].ToString(), dt1.Rows[i][1].ToString(), dt1.Rows[i][2].ToString(), dt1.Rows[i][3].ToString(), dt1.Rows[i][4].ToString(), dt1.Rows[i][5].ToString(), dt1.Rows[i][6].ToString(), sub_eff.ToString("0.##") + "%", qty, count, tot_eff.ToString("0.##") + "%");
                                data1.Rows.Add("", "", "", "", "", "", "", "", dt1.Rows[i][0].ToString(), dt1.Rows[i][1].ToString(), dt1.Rows[i][2].ToString(), dt1.Rows[i][3].ToString(), dt1.Rows[i][4].ToString(), dt1.Rows[i][5].ToString(), dt1.Rows[i][6].ToString(), start, qty, txttop.Text, sub_eff.ToString("0.##") + "%", tot_eff.ToString("0.##") + "%", count, end);
                            }

                            //generate chart
                            barSeries1.ForeColor = Color.White;
                            barSeries1.DataPoints.Add(new CategoricalDataPoint(qty, dt2.Rows[j][1].ToString()));
                            radChartView3.Series.Add(barSeries1);
                            barSeries1.ShowLabels = true;

                            LinearAxis verticalAxis1 = radChartView3.Axes[1] as LinearAxis;
                            verticalAxis1.LabelFitMode = AxisLabelFitMode.MultiLine;
                            verticalAxis1.ForeColor = Color.White;
                            verticalAxis1.BorderColor = Color.DodgerBlue;
                            verticalAxis1.ShowLabels = false;
                            verticalAxis1.Title = "Quantity";

                            CategoricalAxis ca1 = radChartView3.Axes[0] as CategoricalAxis;
                            ca1.LabelFitMode = AxisLabelFitMode.MultiLine;
                            ca1.Title = "Operations";
                            ca1.ForeColor = Color.White;
                            ca1.BorderColor = Color.DodgerBlue;
                        }
                    }
                    else if (chkstation.Checked == true)
                    {
                        radChartView3.Title = "Top Defects - Station";

                        //get station wise top defects
                        query = "select TOP(" + txttop.Text + ") I_STATION_ID,sum(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' GROUP BY I_STATION_ID order by sum(I_QUANTITY) DESC";
                        SqlDataAdapter sda1 = new SqlDataAdapter(query, dc.con);
                        DataTable dt2 = new DataTable();
                        sda1.Fill(dt2);
                        sda1.Dispose();
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            BarSeries barSeries1 = new BarSeries("Performance", "RepresentativeName");
                            
                            String stnid = dt2.Rows[j][0].ToString();
                            int qty = 0;
                            if (dt2.Rows[j][1].ToString() != "")
                            {
                                qty = int.Parse(dt2.Rows[j][1].ToString());
                            }

                            String[] temp = stnid.Split('.');
                            String stn = "";

                            //get station id
                            SqlCommand cmd1 = new SqlCommand("select I_STN_ID from STATION_DATA where I_INFEED_LINE_NO='" + temp[0] + "' and I_STN_NO_INFEED='" + temp[1] + "'", dc.con);
                            SqlDataReader sdr1 = cmd1.ExecuteReader();
                            if (sdr1.Read())
                            {
                                stn = sdr1.GetValue(0).ToString();
                            }
                            sdr1.Close();

                            int count = 0;

                            //get production details for the mo
                            sda1 = new SqlDataAdapter("select MO_NO,MO_LINE,SUM(PC_COUNT) from HANGER_HISTORY where TIME>='" + start + "' and TIME<'" + end + "' and STN_ID='" + stn + "' GROUP BY MO_NO,MO_LINE", dc.con);
                            DataTable dt1 = new DataTable();
                            sda1.Fill(dt1);
                            sda1.Dispose();
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                String temp1 = dt1.Rows[i][2].ToString();
                                if (temp1 != "")
                                {
                                    count = int.Parse(temp1);
                                }
                                else
                                {
                                    count = 0;
                                }
                            }

                            //get station wise top defects
                            query = "SELECT I_STATION_ID,V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC,sum(I_QUANTITY) AS QUANTITY FROM QC_HISTORY where D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and I_STATION_ID='" + stnid + "' GROUP BY I_STATION_ID,V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC order by sum(I_QUANTITY) DESC";
                            sda1 = new SqlDataAdapter(query, dc.con);
                            dt1 = new DataTable();
                            sda1.Fill(dt1);
                            sda1.Dispose();
                            for (int i = 0; i < dt1.Rows.Count; i++)
                            {
                                //calculate sub qty
                                int subqty = 0;
                                if (dt1.Rows[i][5].ToString() != "")
                                {
                                    subqty = int.Parse(dt1.Rows[i][5].ToString());
                                }

                                //calculate sub efficiency
                                decimal sub_eff = 0;
                                if (qty != 0)
                                {
                                    sub_eff = ((decimal)subqty / (decimal)qty) * 100;
                                }

                                //calculate total efficiency
                                decimal tot_eff = 0;
                                if (count != 0)
                                {
                                    tot_eff = ((decimal)qty / (decimal)count) * 100;
                                }

                                dgvtopdefects.Rows.Add(dt1.Rows[i][0].ToString(), "", "", "", "", "", "", "", "", "", dt1.Rows[i][1].ToString(), dt1.Rows[i][2].ToString(), dt1.Rows[i][3].ToString(), dt1.Rows[i][4].ToString(), dt1.Rows[i][5].ToString(), sub_eff.ToString("0.##") + "%", qty, count, tot_eff.ToString("0.##") + "%");
                                data1.Rows.Add(dt1.Rows[i][0].ToString(), "", "", "", "", "", "", "", "", "", dt1.Rows[i][1].ToString(), dt1.Rows[i][2].ToString(), dt1.Rows[i][3].ToString(), dt1.Rows[i][4].ToString(), dt1.Rows[i][5].ToString(), start, qty, txttop.Text, sub_eff.ToString("0.##") + "%", tot_eff.ToString("0.##") + "%", count, end);
                            }

                            //generate chart
                            barSeries1.ForeColor = Color.White;
                            barSeries1.DataPoints.Add(new CategoricalDataPoint(qty, stnid));
                            radChartView3.Series.Add(barSeries1);
                            barSeries1.ShowLabels = true;

                            LinearAxis verticalAxis1 = radChartView3.Axes[1] as LinearAxis;
                            verticalAxis1.LabelFitMode = AxisLabelFitMode.MultiLine;
                            verticalAxis1.ForeColor = Color.White;
                            verticalAxis1.BorderColor = Color.DodgerBlue;
                            verticalAxis1.ShowLabels = false;
                            verticalAxis1.Title = "Quantity";

                            CategoricalAxis ca1 = radChartView3.Axes[0] as CategoricalAxis;
                            ca1.LabelFitMode = AxisLabelFitMode.MultiLine;
                            ca1.Title = "Station";
                            ca1.ForeColor = Color.White;
                            ca1.BorderColor = Color.DodgerBlue;
                        }
                    }
                    return;
                }
                displayAll();
                dgvtopdefects.Columns[15].IsVisible = false;

                //get top defects
                SqlDataAdapter sda = new SqlDataAdapter(query, dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgvtopdefects.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString(), dt.Rows[i][7].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][9].ToString(), dt.Rows[i][10].ToString(), dt.Rows[i][11].ToString(), dt.Rows[i][12].ToString(), dt.Rows[i][13].ToString(), dt.Rows[i][14].ToString(), "");
                    data1.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString(), dt.Rows[i][7].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][9].ToString(), dt.Rows[i][10].ToString(), dt.Rows[i][11].ToString(), dt.Rows[i][12].ToString(), dt.Rows[i][13].ToString(), dt.Rows[i][14].ToString(), start, "", txttop.Text, end);
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void chkemployee_CheckStateChanged(object sender, EventArgs e)
        {
            displayAll();   //show all columns

            if (chkemployee.Checked == true)
            {
                chkmono.Checked = false;
                chkstation.Checked = false;
                chkoperation.Checked = false;

                dgvtopdefects.Columns[0].IsVisible = false;
                dgvtopdefects.Columns[1].IsVisible = false;
                dgvtopdefects.Columns[2].IsVisible = false;
                dgvtopdefects.Columns[5].IsVisible = false;
                dgvtopdefects.Columns[6].IsVisible = false;
                dgvtopdefects.Columns[7].IsVisible = false;
                dgvtopdefects.Columns[8].IsVisible = false;
                dgvtopdefects.Columns[9].IsVisible = false;
            }

            Get_TopDefects();   //get top defects
        }

        private void chkoperation_CheckStateChanged(object sender, EventArgs e)
        {
            displayAll();  //show all columns

            if (chkoperation.Checked == true)
            {
                chkmono.Checked = false;
                chkemployee.Checked = false;
                chkstation.Checked = false;

                dgvtopdefects.Columns[0].IsVisible = false;
                dgvtopdefects.Columns[1].IsVisible = false;
                dgvtopdefects.Columns[2].IsVisible = false;
                dgvtopdefects.Columns[5].IsVisible = false;
                dgvtopdefects.Columns[6].IsVisible = false;
                dgvtopdefects.Columns[7].IsVisible = false;
                dgvtopdefects.Columns[3].IsVisible = false;
                dgvtopdefects.Columns[4].IsVisible = false;
            }

            Get_TopDefects();    //get top defects
        }

        private void chkmono_CheckStateChanged(object sender, EventArgs e)
        {
            displayAll();   //show all columns

            if (chkmono.Checked == true)
            {
                chkstation.Checked = false;
                chkemployee.Checked = false;
                chkoperation.Checked = false;

                dgvtopdefects.Columns[0].IsVisible = false;
                dgvtopdefects.Columns[3].IsVisible = false;
                dgvtopdefects.Columns[5].IsVisible = false;
                dgvtopdefects.Columns[6].IsVisible = false;
                dgvtopdefects.Columns[7].IsVisible = false;
                dgvtopdefects.Columns[8].IsVisible = false;
                dgvtopdefects.Columns[9].IsVisible = false;
                dgvtopdefects.Columns[4].IsVisible = false;
            }

            Get_TopDefects();   //get top defects
        }

        private void chkstation_CheckStateChanged(object sender, EventArgs e)
        {
            displayAll();   //show all columns

            if (chkstation.Checked == true)
            {
                chkmono.Checked = false;
                chkemployee.Checked = false;
                chkoperation.Checked = false;

                dgvtopdefects.Columns[1].IsVisible = false;
                dgvtopdefects.Columns[3].IsVisible = false;
                dgvtopdefects.Columns[2].IsVisible = false;
                dgvtopdefects.Columns[5].IsVisible = false;
                dgvtopdefects.Columns[6].IsVisible = false;
                dgvtopdefects.Columns[7].IsVisible = false;
                dgvtopdefects.Columns[8].IsVisible = false;
                dgvtopdefects.Columns[9].IsVisible = false;
                dgvtopdefects.Columns[4].IsVisible = false;
            }

            Get_TopDefects();  //get top defects
        }

        public void displayAll()
        {
            //show all columns
            for (int i = 0; i < dgvtopdefects.Columns.Count; i++)
            {
                dgvtopdefects.Columns[i].IsVisible = true;
            }
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            //get logo
            DataTable dt_image = new DataTable();
            dt_image.Columns.Add("image", typeof(byte[]));
            dt_image.Rows.Add(dc.GetImage());
            DataView dv_image = new DataView(dt_image);

            if (btnreport.Text == "Report View")
            {
                if (chkmono.Checked == true)
                {
                    panel4.Visible = false;
                    btnchart.Text = "Show Chart";

                    reportViewer1.Visible = true;
                    btnreport.Text = "Table View";

                    DataView view1 = new DataView(data1);
                    reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.TOP_MONO.rdlc";
                    reportViewer1.LocalReport.DataSources.Clear();

                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view1));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                    reportViewer1.RefreshReport();
                }
                else if (chkemployee.Checked == true)
                {
                    panel4.Visible = false;
                    btnchart.Text = "Show Chart";

                    reportViewer1.Visible = true;
                    btnreport.Text = "Table View";

                    DataView view2 = new DataView(data1);

                    reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.TOP_EMPLOYEE.rdlc";
                    reportViewer1.LocalReport.DataSources.Clear();

                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view2));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                    reportViewer1.RefreshReport();
                }
                else if (chkoperation.Checked == true)
                {
                    panel4.Visible = false;
                    btnchart.Text = "Show Chart";

                    reportViewer1.Visible = true;
                    btnreport.Text = "Table View";

                    DataView view3 = new DataView(data1);

                    reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.TOP_OPERATION.rdlc";
                    reportViewer1.LocalReport.DataSources.Clear();

                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view3));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                    reportViewer1.RefreshReport();
                }
                else if (chkstation.Checked == true)
                {
                    panel4.Visible = false;
                    btnchart.Text = "Show Chart";

                    reportViewer1.Visible = true;
                    btnreport.Text = "Table View";

                    DataView view4 = new DataView(data1);

                    reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.TOP_STATION.rdlc";
                    reportViewer1.LocalReport.DataSources.Clear();

                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view4));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                    reportViewer1.RefreshReport();
                }
                else
                {
                    panel4.Visible = false;
                    btnchart.Text = "Show Chart";

                    reportViewer1.Visible = true;
                    btnreport.Text = "Table View";

                    DataView view = new DataView(data1);

                    reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Top_Defects.rdlc";
                    reportViewer1.LocalReport.DataSources.Clear();

                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                    reportViewer1.RefreshReport();
                }
            }
            else
            {
                reportViewer1.Visible = false;
                btnreport.Text = "Report View";
            }
        }        

        private void radButton2_Click(object sender, EventArgs e)
        {
            if (btnchart.Text == "Show Chart")
            {
                if (btnreport.Text == "Table View")
                {
                    btnreport.PerformClick();
                }

                panel4.Visible = true;
                btnchart.Text = "Hide Chart";
            }
            else
            {
                panel4.Visible = false;
                btnchart.Text = "Show Chart";
            }
        }

        private void dgvtopdefects_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvtopdefects.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvtopdefects.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvtopdefects.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvtopdefects.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void radLabel4_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            panel2.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel4.Text = "";
            panel2.Visible = false;
            MyTimer.Stop();
        }
    }
}
