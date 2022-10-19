using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
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
    public partial class Dashboard : Telerik.WinControls.UI.RadForm
    {
        public Dashboard()
        {
            InitializeComponent();
            radChartView1.ShowPanZoom = true;

            //enable chart zoom
            ChartPanZoomController panZoomController = new ChartPanZoomController();
            panZoomController.PanZoomMode = ChartPanZoomMode.Horizontal;

            radChartView1.Controllers.Add(panZoomController);
        }

        Database_Connection dc = new Database_Connection();  //connection class
        String controller_name = "";
        DataTable dthourly;
        DataTable dtmoload;
        DataTable dtmoprod;
        DataTable dtmoeff;
        DataTable dtmorepair;
        DataTable data4 = new DataTable();

        private void Dashboard_Load(object sender, EventArgs e)
        {
            RadMessageBox.SetThemeName("FluentDark");  //set message theme

            //add columns for totals
            data4.Columns.Add("TOTAL_LOADED");
            data4.Columns.Add("TOTAL_UNLOADED");
            data4.Columns.Add("TOTAL_REPAIR_REWORK");

            dc.OpenConnection();  //open connection
            select_controller();  //get controller ip address

            //check if controller is selected
            if (controller_name == "--SELECT--" || controller_name == "")
            {
                RadMessageBox.Show("Please Select a Controller", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                timer1.Stop();
                this.Close();
            }

            //span first row
            tableLayoutPanel2.SetCellPosition(radChartView1, new TableLayoutPanelCellPosition(0, 0));
            tableLayoutPanel2.SetColumnSpan(radChartView1, 2);

            radDateTimePicker1.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));            

            GET_MO();  //get mo wise customer details
        }

        public void GET_MO()
        {
            //clear dropdownlist
            cmbcustomer.Items.Clear();
            cmbcustomer.Items.Add("All");

            String date = DateTime.Now.ToString("yyyy-MM-dd");
            date = radDateTimePicker1.Value.ToString("yyyy-MM-dd");

            //get distinct mo used for the day
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT distinct MO_NO FROM stationhistory where time>='" + date + " 00:00:00' and time<'" + date + " 23:59:59'", dc.conn);
            DataTable dt1 = new DataTable();
            da.Fill(dt1);
            da.Dispose();
            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                String cust = "";
                //get customer details for the mo
                SqlDataAdapter sda = new SqlDataAdapter("select c.V_CUSTOMER_NAME from CUSTOMER_DB c,MO m where m.V_CUSTOMER_ID=c.V_CUSTOMER_ID and m.V_MO_NO='" + dt1.Rows[j][0] + "'", dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cust = dt.Rows[i][0].ToString();
                }

                int flag = 0;
                for (int i = 0; i < cmbcustomer.Items.Count; i++)
                {
                    if (cmbcustomer.Items[i].Text == cust)
                    {
                        flag = 1;
                        break;
                    }
                }

                if (flag == 0)
                {
                    cmbcustomer.Items.Add(cust);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Dashboard_Refresh();  //generate dashboard
        }

        public void Dashboard_Refresh()
        {
            try
            {
                String date = DateTime.Now.ToString("yyyy-MM-dd");
                date = radDateTimePicker1.Value.ToString("yyyy-MM-dd");

                DateTime op_starttime = DateTime.Now;
                DateTime op_endtime = DateTime.Now;

                int total_sam = 0;
                int actual_production = 0;

                //add columns for hourly production
                dthourly = new DataTable();
                dthourly.Columns.Add("hour");
                dthourly.Columns.Add("count");
                dthourly.Columns.Add("mo");

                //add columns for mo load
                dtmoload = new DataTable();
                dtmoload.Columns.Add("mo");
                dtmoload.Columns.Add("count");

                //add columns for mo unload
                dtmoprod = new DataTable();
                dtmoprod.Columns.Add("mo");
                dtmoprod.Columns.Add("count");

                //add columns for mo efficiency
                dtmoeff = new DataTable();
                dtmoeff.Columns.Add("mo");
                dtmoeff.Columns.Add("count");

                //add columns for mo repair
                dtmorepair = new DataTable();
                dtmorepair.Columns.Add("mo");
                dtmorepair.Columns.Add("count");

                //check if date is enabled in hide day
                String start = radDateTimePicker1.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                SqlCommand cmd = new SqlCommand("select COUNT(*) from HIDEDAY_DB where CONVERT(nvarchar(10), '" + start + "', 120) in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                int mocount = int.Parse(cmd.ExecuteScalar() + "");
                if (mocount > 0)
                {
                    return;
                }

                //get the customer details of the mo
                SqlDataAdapter sa = new SqlDataAdapter("select M.V_MO_NO from MO M, CUSTOMER_DB C where C.V_CUSTOMER_ID=M.V_CUSTOMER_ID and C.V_CUSTOMER_NAME='" + cmbcustomer.Text + "'", dc.con);
                DataTable cust = new DataTable();
                sa.Fill(cust);
                sa.Dispose();

                radChartView1.Series.Clear();
                int same_cust = 0;

                //get hourly production for the mo
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT MO_NO,MO_LINE,MIN(HOUR(TIME)) FROM stationhistory where time>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' and REMARKS='2' group by MO_NO,MO_LINE order by MIN(HOUR(TIME))", dc.conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    String mo = dt.Rows[j][0].ToString();
                    String moline = dt.Rows[j][1].ToString();

                    LineSeries lineSeries = new LineSeries();
                    int flag = 0;
                    //check if all customer is selected
                    if (cmbcustomer.Text != "All")
                    {
                        for (int k = 0; k < cust.Rows.Count; k++)
                        {
                            if (cust.Rows[k][0].ToString() == mo)
                            {
                                flag = 1;
                            }
                        }
                    }
                    else
                    {
                        flag = 1;
                    }

                    if (flag != 1)
                    {
                        same_cust += 1;
                        continue;
                    }
                    int count = 0;

                    //get hourly production fro the mo
                    da = new MySqlDataAdapter("SELECT HOUR(TIME),MO_NO,MO_LINE,SUM(PC_COUNT) FROM stationhistory where time>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='2' GROUP BY HOUR(TIME),MO_NO,MO_LINE ORDER BY HOUR(TIME)", dc.conn);
                    DataTable dt5 = new DataTable();
                    da.Fill(dt5);
                    da.Dispose();
                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {
                        String temp = dt5.Rows[i][3].ToString();
                        if (temp != "")
                        {
                            count = int.Parse(temp);
                        }
                        else
                        {
                            count = 0;
                        }

                        //add to chart and report
                        lineSeries.DataPoints.Add(new CategoricalDataPoint(count, dt5.Rows[i][0].ToString() + ":00:00"));
                        dthourly.Rows.Add(dt5.Rows[i][0].ToString(), count, mo + "  " + moline);
                    }

                    //generate chart
                    lineSeries.ShowLabels = true;
                    radChartView1.Series.Add(lineSeries);
                    lineSeries.LegendTitle = mo + "-" + moline;

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

                    radChartView1.Series[j - same_cust].ForeColor = Color.White;
                    radChartView1.ForeColor = Color.White;
                    this.radChartView1.ShowLegend = true;
                }

                radChartView1.ShowSmartLabels = true;

                this.radChartView2.AreaType = ChartAreaType.Pie;
                PieSeries series = new PieSeries();

                radChartView2.SelectionMode = ChartSelectionMode.SingleDataPoint;
                radChartView2.Series.Clear();

                int totalrepair = 0;

                //get mo wise repiar quantity
                SqlDataAdapter sda = new SqlDataAdapter("select V_MO_NO,V_MO_LINE,SUM(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + date + " 00:00:00' and D_DATE_TIME<'" + date + " 23:59:59' group by V_MO_NO,V_MO_LINE ORDER BY V_MO_NO,V_MO_LINE", dc.con);
                dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String mo = dt.Rows[i][0].ToString();
                    String moline = dt.Rows[i][1].ToString();

                    int flag = 0;
                    //check if all customer is selected
                    if (cmbcustomer.Text != "All")
                    {
                        for (int k = 0; k < cust.Rows.Count; k++)
                        {
                            if (cust.Rows[k][0].ToString() == mo)
                            {
                                flag = 1;
                            }
                        }
                    }
                    else
                    {
                        flag = 1;
                    }

                    if (flag != 1)
                    {
                        continue;
                    }

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

                    //add to chart and reports
                    totalrepair += count;
                    series.DataPoints.Add(new PieDataPoint(count, mo + "-" + moline + "  " + count));
                    dtmorepair.Rows.Add(mo + "-" + moline, count);
                }

                //generate chart
                series.ShowLabels = true;
                this.radChartView2.Series.Add(series);
                radChartView2.Series[0].ForeColor = Color.White;
                radChartView2.ForeColor = Color.White;

                this.radChartView3.AreaType = ChartAreaType.Pie;

                PieSeries series1 = new PieSeries();
                radChartView3.Series.Clear();
                int totalunload = 0;

                //get mo wise unloading
                da = new MySqlDataAdapter("SELECT MO_NO,MO_LINE,SUM(PC_COUNT) FROM stationhistory where time>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' and REMARKS='2' GROUP BY MO_NO,MO_LINE ORDER BY MO_NO,MO_LINE", dc.conn);
                dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    String mo = dt.Rows[j][0].ToString();
                    String moline = dt.Rows[j][1].ToString();
                    int flag = 0;

                    //check if all customer is selected
                    if (cmbcustomer.Text != "All")
                    {
                        for (int k = 0; k < cust.Rows.Count; k++)
                        {
                            if (cust.Rows[k][0].ToString() == mo)
                            {
                                flag = 1;
                            }
                        }
                    }
                    else
                    {
                        flag = 1;
                    }

                    if (flag != 1)
                    {
                        continue;
                    }

                    int count = 0;
                    String temp = dt.Rows[j][2].ToString();
                    if (temp != "")
                    {
                        count = int.Parse(dt.Rows[j][2].ToString());
                    }
                    else
                    {
                        count = 0;
                    }

                    // add to chart and report
                    totalunload += count;
                    series1.DataPoints.Add(new PieDataPoint(count, mo + "-" + moline + "  " + count));
                    dtmoprod.Rows.Add( mo + "-" + moline, count);
                }

                //generate chart
                series1.ShowLabels = true;
                this.radChartView3.Series.Add(series1);
                radChartView3.Series[0].ForeColor = Color.White;
                radChartView3.ForeColor = Color.White;


                this.radChartView4.AreaType = ChartAreaType.Pie;

                PieSeries series2 = new PieSeries();
                radChartView4.Series.Clear();
                int totalload = 0;

                //get mo wise loading
                da = new MySqlDataAdapter("SELECT MO_NO,MO_LINE,SUM(PC_COUNT) FROM stationhistory where time>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' and REMARKS='1' GROUP BY MO_NO,MO_LINE ORDER BY MO_NO,MO_LINE", dc.conn);
                dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    String mo = dt.Rows[j][0].ToString();
                    String moline = dt.Rows[j][1].ToString();

                    int flag = 0;
                    //check if all customer is selected
                    if (cmbcustomer.Text != "All")
                    {
                        for (int k = 0; k < cust.Rows.Count; k++)
                        {
                            if (cust.Rows[k][0].ToString() == mo)
                            {
                                flag = 1;
                            }
                        }
                    }
                    else
                    {
                        flag = 1;
                    }

                    if (flag != 1)
                    {
                        continue;
                    }

                    int count = 0;
                    String temp = dt.Rows[j][2].ToString();
                    if (temp != "")
                    {
                        count = int.Parse(dt.Rows[j][2].ToString());
                    }
                    else
                    {
                        count = 0;
                    }

                    //add to chart and report
                    totalload += count;
                    series2.DataPoints.Add(new PieDataPoint(count, mo + "-" + moline + "  " + count));
                    dtmoload.Rows.Add(mo + "   " + moline, count);
                }

                //generate chart
                series2.ShowLabels = true;
                this.radChartView4.Series.Add(series2);
                radChartView4.Series[0].ForeColor = Color.White;
                radChartView4.ForeColor = Color.White;

                this.radChartView5.AreaType = ChartAreaType.Pie;
                PieSeries series3 = new PieSeries();
                radChartView5.Series.Clear();

                //get mo wise efficiency
                da = new MySqlDataAdapter("select distinct MO_NO, MO_LINE from stationhistory where time>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' ORDER BY MO_NO,MO_LINE", dc.conn);
                dt = new DataTable();
                da.Fill(dt);
                da.Dispose();
                for (int p = 0; p < dt.Rows.Count; p++)
                {
                    String mo = dt.Rows[p][0].ToString();
                    String moline = dt.Rows[p][1].ToString();
                    String article = "";

                    int flag = 0;
                    //check if all employee is selected
                    if (cmbcustomer.Text != "All")
                    {
                        for (int k = 0; k < cust.Rows.Count; k++)
                        {
                            if (cust.Rows[k][0].ToString() == mo)
                            {
                                flag = 1;
                            }
                        }
                    }
                    else
                    {
                        flag = 1;
                    }

                    if (flag != 1)
                    {
                        continue;
                    }

                    //get article id
                    cmd = new SqlCommand("select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "'", dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        article = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

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

                    //get sum production
                    MySqlCommand cmd1 = new MySqlCommand("select SUM(PC_COUNT) from stationhistory where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='2' and TIME>='" + date + " 00:00:00' and time<'" + date + " 23:59:59'", dc.conn);
                    temp = cmd1.ExecuteScalar().ToString();
                    if (temp != "")
                    {
                        actual_production = int.Parse(temp + "");
                    }
                    else
                    {
                        actual_production = 0;
                    }

                    //get first hanger and last hanger time
                    MySqlDataAdapter sda2 = new MySqlDataAdapter("SELECT MIN(TIME),MAX(TIME) FROM stationhistory where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and TIME>='" + date + " 00:00:00' and time<'" + date + " 23:59:59'", dc.conn);
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
                    }

                    //calculate duration
                    TimeSpan ts = new TimeSpan();
                    ts = op_endtime - op_starttime;
                    int duration = (int)ts.TotalSeconds;

                    //calculate actual sam
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

                    //add to chart and report
                    series3.DataPoints.Add(new PieDataPoint((int)efficiency, mo + "-" + moline + "  " + (int)efficiency + "%"));
                    dtmoeff.Rows.Add(mo + "-" + moline, efficiency.ToString("0.##"));
                }

                //generate chart
                series3.ShowLabels = true;
                this.radChartView5.Series.Add(series3);
                radChartView5.Series[0].ForeColor = Color.White;
                radChartView5.ForeColor = Color.White;
                radChartView5.ForeColor = Color.White;

                data4.Rows.Clear();
                lbltotalloaded.Text = "Total Loaded : " + totalload;
                lbltotalunloaded.Text = "Total Production : " + totalunload;
                lbltotalrepair.Text = "Total Repair/Rework : " + totalrepair;
               
                if (totalload >= 0)
                {
                    if (totalunload >= 0)
                    {
                        if (totalrepair >= 0)
                        {
                            data4.Rows.Add(totalload, totalunload, totalrepair);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                timer1.Stop();
                timer1.Enabled = false;
            }
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

            //get ipaddress
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
            timer2.Enabled = true;
            timer1.Start();  //start timer
        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            dc.Close_Connection();  //close connection of form close
        }

        private void radDateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            GET_MO();  //get mo wise customer 
        }

        private void cmbcustomer_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Dashboard_Refresh();  //generate dashboard
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Dashboard_Refresh();  //generate dashboard
            timer2.Enabled = false;
        }

        public void Houly_Production()
        {
            try
            {
                //add columns for datatable
                DataTable data1 = new DataTable();
                data1.Columns.Add("mono");
                data1.Columns.Add("moline");
                data1.Columns.Add("hour");
                data1.Columns.Add("loaded");
                data1.Columns.Add("unloaded");
                data1.Columns.Add("eff");
                data1.Columns.Add("rework");
                data1.Columns.Add("color");
                data1.Columns.Add("article");
                data1.Columns.Add("size");
                data1.Columns.Add("date");

                String date = DateTime.Now.ToString("yyyy-MM-dd");
                date = radDateTimePicker1.Value.ToString("yyyy-MM-dd");
                DateTime op_starttime = DateTime.Now;
                DateTime op_endtime = DateTime.Now;

                //get mo used for the day
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT DISTINCT MO_NO,MO_LINE FROM stationhistory where time>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' order by MO_NO,MO_LINE", dc.conn);
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

                    //get hourly mo loaded
                    da = new MySqlDataAdapter("SELECT HOUR(TIME),MO_NO,MO_LINE,SUM(PC_COUNT) FROM stationhistory where time>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='1' GROUP BY HOUR(TIME),MO_NO,MO_LINE ORDER BY HOUR(TIME)", dc.conn);
                    dt5 = new DataTable();
                    da.Fill(dt5);
                    da.Dispose();
                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {
                        load = int.Parse(dt5.Rows[i][3].ToString());

                        int flag = 0;
                        for (int n = 0; n < data1.Rows.Count; n++)
                        {
                            if (data1.Rows[n][0].ToString() == mo && data1.Rows[n][1].ToString() == moline && data1.Rows[n][2].ToString() == dt5.Rows[i][0].ToString() + ":00:00")
                            {
                                data1.Rows[n][3] = load;
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

                            data1.Rows.Add(mo, moline, hour + ":00:00", load, "0", "0", "0", color, article, size, date);
                        }
                    }

                    int unload = 0;

                    //get hourly mo unloaded
                    da = new MySqlDataAdapter("SELECT HOUR(TIME),MO_NO,MO_LINE,SUM(PC_COUNT) FROM stationhistory where time>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='2' GROUP BY HOUR(TIME),MO_NO,MO_LINE ORDER BY HOUR(TIME)", dc.conn);
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

                        for (int n = 0; n < data1.Rows.Count; n++)
                        {
                            if (data1.Rows[n][0].ToString() == mo && data1.Rows[n][1].ToString() == moline && data1.Rows[n][2].ToString() == hour + ":00:00")
                            {
                                data1.Rows[n][4] = unload;
                                flag = 1;
                            }
                        }

                        if (flag == 0)
                        {
                            data1.Rows.Add(mo, moline, hour + ":00:00", "0", unload, "0", "0", color, article, size, date);
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

                    //get hourly production
                    da = new MySqlDataAdapter("select HOUR(TIME),SUM(PC_COUNT) from stationhistory where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and REMARKS='2' and TIME>='" + date + " 00:00:00' and time<'" + date + " 23:59:59' group by HOUR(TIME)", dc.conn);
                    dt5 = new DataTable();
                    da.Fill(dt5);
                    da.Dispose();
                    for (int i = 0; i < dt5.Rows.Count; i++)
                    {
                        //get first hanger and last hanger time
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

                            for (int n = 0; n < data1.Rows.Count; n++)
                            {
                                if (data1.Rows[n][0].ToString() == mo && data1.Rows[n][1].ToString() == moline && data1.Rows[n][2].ToString() == dt5.Rows[i][0].ToString() + ":00:00")
                                {
                                    data1.Rows[n][5] = (int)efficiency + "%";
                                }
                            }
                        }
                    }

                    //get hourly repair quantity
                    sda = new SqlDataAdapter("select CONVERT(VARCHAR(2), D_DATE_TIME, 108),SUM(I_QUANTITY) from QC_HISTORY where D_DATE_TIME>='" + date + " 00:00:00' and D_DATE_TIME<'" + date + " 23:59:59' and V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' group by CONVERT(VARCHAR(2), D_DATE_TIME, 108) ORDER BY CONVERT(VARCHAR(2), D_DATE_TIME, 108)", dc.con);
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

                        for (int n = 0; n < data1.Rows.Count; n++)
                        {
                            if (data1.Rows[n][0].ToString() == mo && data1.Rows[n][1].ToString() == moline && data1.Rows[n][2].ToString() == dt5.Rows[i][0].ToString() + ":00:00")
                            {
                                data1.Rows[n][6] = count;
                            }
                        }
                    }
                }

                //generate report
                DataView view = new DataView(data1);
                DataView view1 = new DataView(dthourly);
                DataView view2 = new DataView(dtmoprod);
                DataView view3 = new DataView(dtmoload);
                DataView view4 = new DataView(dtmoeff);
                DataView view5 = new DataView(dtmorepair);
                DataView view6 = new DataView(data4);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.Visible = true;
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
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        public void Hourly_Operation(String Report)
        {
            try
            {
                //add columns to datatable
                DataTable data1 = new DataTable();
                data1.Columns.Add("mono");
                data1.Columns.Add("moline");
                data1.Columns.Add("hour");
                data1.Columns.Add("loaded");
                data1.Columns.Add("eff");
                data1.Columns.Add("rework");
                data1.Columns.Add("color");
                data1.Columns.Add("article");
                data1.Columns.Add("size");
                data1.Columns.Add("date");
                data1.Columns.Add("opcode");
                data1.Columns.Add("opdesc");

                String date = DateTime.Now.ToString("yyyy-MM-dd");
                date = radDateTimePicker1.Value.ToString("yyyy-MM-dd");
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

                    int seq1 = 1;
                    int nextseq = 1;
                    int prevseq = 1;
                    int curseq = 1;

                    //get sequence for the mo
                    sda = new SqlDataAdapter("select ds.I_SEQUENCE_NO,ds.V_OPERATION_CODE,op.V_OPERATION_DESC,op.D_SAM from DESIGN_SEQUENCE ds,OPERATION_DB op where ds.V_ARTICLE_ID='" + article + "' and ds.V_OPERATION_CODE=op.V_OPERATION_CODE and ds.I_SEQUENCE_NO IN(select distinct I_SEQUENCE_NO from STATION_ASSIGN where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and I_STATION_ID!=0) order by ds.I_SEQUENCE_NO", dc.con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //reset sequence
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

                        //get hourly production for the sequence
                        MySqlDataAdapter sda2 = new MySqlDataAdapter("SELECT HOUR(TIME),SUM(PC_COUNT),MIN(TIME),MAX(TIME) FROM stationhistory where SEQ_NO='" + curseq + "' and MO_NO='" + mo + "' and MO_LINE='" + moline + "' and TIME>='" + date + " 00:00:00' and TIME<'" + date + " 23:59:59' group by HOUR(TIME) order by HOUR(TIME)", dc.conn);
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

                            //calculate duration
                            TimeSpan ts_op_completed = Convert.ToDateTime(op_endtime.ToString("HH:mm:ss")) - Convert.ToDateTime(op_starttime.ToString("HH:mm:ss"));
                            int op_completed = (int)ts_op_completed.TotalSeconds;

                            //calculate actual sam
                            decimal actual_sam = 0;
                            if (loaded > 0)
                            {
                                actual_sam = (decimal)op_completed / (decimal)loaded;
                            }

                            //calculate efficeincy
                            decimal efficiency = 0;
                            if (actual_sam > 0)
                            {
                                efficiency = (sam / actual_sam) * 100;
                            }

                            //get sum of repair for the operation
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

                            //add to datatable
                            data1.Rows.Add(mo, moline, hour, loaded, efficiency.ToString("0.##"), count, color, articleDesc, size, date, opcode, opdesc);
                        }
                    }
                }

                reportViewer1.Visible = true;
                DataView view = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                //check if opertaion report is selected
                if (Report == "Operation")
                {
                    reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.OPERATION_HOURLY.rdlc";
                }
                else if (Report == "MO")
                {
                    reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.MO_OP.rdlc";
                }

                reportViewer1.LocalReport.DataSources.Clear();
                //add views to dataset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.ToString(), "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void cmbselect_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            panel3.Visible = false;

            //check which report is selected
            if (cmbselect.Text == "Hourly Production Report")
            {
                Houly_Production();
            }
            else if (cmbselect.Text == "Hourly Operation Report")
            {
                Hourly_Operation("Operation");
            }
            else if (cmbselect.Text == "Hourly MO Operation Report")
            {
                Hourly_Operation("MO");
            }
            else if (cmbselect.Text == "Dash Board")
            {
                reportViewer1.Visible = false;
                panel3.Visible = true;
                panel2.Visible = true;
            }
        }

        String getallop = "";

        private void Dashboard_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();

            String Lang = "";
            SqlCommand cmd = new SqlCommand("SELECT Language,ThemeName,HIDE_TOTALS,GET_ALL_OPERATIONS FROM Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Lang = sdr.GetValue(0).ToString();
                getallop = sdr.GetValue(3).ToString();
            }
            sdr.Close();
        }
    }
}
