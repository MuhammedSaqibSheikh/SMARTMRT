using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Telerik.Charting;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Line_Balancing : RadForm
    {
        public Line_Balancing()
        {
            InitializeComponent();
        }

        String theme = "";
        Database_Connection dc = new Database_Connection();   //connection class
        String controller_name = "";
        DataTable data1 = new DataTable();
        DataTable data2 = new DataTable();

        private void Line_Balancing_Load(object sender, EventArgs e)
        {
            dgvmo.MasterTemplate.SelectLastAddedRow = false;
            dgvstation.MasterTemplate.SelectLastAddedRow = false;
            RadMessageBox.SetThemeName("FluentDark");   //set message box theme

            select_controller();  //get the selected controller

            //check if the controller is selected
            if (controller_name == "")
            {
                RadMessageBox.Show("Please Select a controller.", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                this.Close();
            }

            dtpdate.Value = DateTime.Now;
            dgvmo.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvstation.MasterView.TableSearchRow.ShowCloseButton = false;

            data1.Columns.Add("MO_NO");
            data1.Columns.Add("STN_ID");
            data1.Columns.Add("SAM");
            data1.Columns.Add("MO_DETAILS");

            data2.Columns.Add("stn");
            data2.Columns.Add("first_op");
            data2.Columns.Add("last_op");
            data2.Columns.Add("sec");
            data2.Columns.Add("pc_count");
            data2.Columns.Add("actual_sam");
            data2.Columns.Add("avg");
            data2.Columns.Add("opcode");
            data2.Columns.Add("opdesc");
            data2.Columns.Add("empid");

            Refresh_Balancing();   //calculate line balancing
            InitTimer();    //enable timer
            timer1.Enabled = true;
        }

        private void dgvstation_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these themes are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvstation.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvstation.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvstation.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvstation.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvmo_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these themes are selected
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

        private void Line_Balancing_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();   //open connection
            String Lang = "";

            //get the language and theme
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
            dgvstation.ThemeName = theme;
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
                RadMessageBox.Show(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void dgvmo_CellClick(object sender, GridViewCellEventArgs e)
        {
            Refresh_Balancing();   //calculate line balancing
            ProductionDetails_tooltip();   //generate toop tips
        }

        public void calculate_chart()
        {
            data1.Rows.Clear();
            data2.Rows.Clear();
            reportViewer1.Visible = false;

            try
            {
                double avg_sam = 0;
                dgvstation.Rows.Clear();

                if (dgvmo.SelectedRows.Count >= 0)
                {
                    String mo = dgvmo.SelectedRows[0].Cells[0].Value.ToString();
                    String moline = dgvmo.SelectedRows[0].Cells[1].Value.ToString();

                    //get the station id and sttaion no
                    MySqlDataAdapter sda2 = new MySqlDataAdapter("select s.STN_ID,h.INFEED_LINENO,h.STN_NO_INFEED,s.SEQ_NO FROM sequencestations s,stationdata h where s.MO_NO='" + mo + "' AND s.MO_LINE='" + moline + "' and s.STN_ID=h.STN_ID order by s.SEQ_NO", dc.conn);
                    DataTable dt2 = new DataTable();
                    sda2.Fill(dt2);
                    sda2.Dispose();
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        String STNID = dt2.Rows[i][0].ToString();
                        String stnno = dt2.Rows[i][1] + "." + dt2.Rows[i][2];
                        String seqno = dt2.Rows[i][3].ToString();

                        //get the first hanger and last hanger time and production detials 
                        sda2 = new MySqlDataAdapter("select MIN(TIME),MAX(TIME),SUM(PC_COUNT),EMP_ID FROM stationhistory where MO_NO='" + mo + "' AND MO_LINE='" + moline + "' AND STN_ID='" + STNID + "' and TIME>='" + dtpdate.Value.ToString("yyyy-MM-dd") + " 00:00:00' and TIME<='" + dtpdate.Value.ToString("yyyy-MM-dd") + " 23:59:59' GROUP BY EMP_ID", dc.conn);
                        DataTable dt = new DataTable();
                        sda2.Fill(dt);
                        sda2.Dispose();
                        for (int J = 0; J < dt.Rows.Count; J++)
                        {
                            if (dt.Rows[J][1].ToString() != "" && dt.Rows[J][0].ToString() != "")
                            {
                                //calculate actual sam 
                                TimeSpan ts = Convert.ToDateTime(dt.Rows[J][1].ToString()) - Convert.ToDateTime(dt.Rows[J][0].ToString());
                                int op_completed = (int)ts.TotalSeconds;
                                int pc_count = int.Parse(dt.Rows[J][2].ToString());
                                double actual_sam = (double)op_completed / (double)pc_count;
                                avg_sam += actual_sam;

                                String opcode = "";
                                String opdesc = "";

                                //get the operations
                                sda2 = new MySqlDataAdapter("select OP_ID FROM sequenceoperations where MO_NO='" + mo + "' AND MO_LINE='" + moline + "' AND SEQ_NO='" + seqno + "'", dc.conn);
                                DataTable dt1 = new DataTable();
                                sda2.Fill(dt1);
                                sda2.Dispose();
                                for (int k = 0; k < dt1.Rows.Count; k++)
                                {
                                    //get the openration desc
                                    SqlDataAdapter sd1 = new SqlDataAdapter("Select V_OPERATION_CODE ,V_OPERATION_DESC from OPERATION_DB where V_ID='" + dt1.Rows[k][0].ToString() + "'", dc.con);
                                    DataTable dt8 = new DataTable();
                                    sd1.Fill(dt8);
                                    sd1.Dispose();
                                    for (int d = 0; d < dt8.Rows.Count; d++)
                                    {
                                        opcode += dt8.Rows[d][0] + ",";
                                        opdesc += dt8.Rows[d][1] + ",";
                                    }
                                }

                                //add to grid
                                dgvstation.Rows.Add(stnno, dt.Rows[J][0].ToString(), dt.Rows[J][1].ToString(), op_completed, pc_count, actual_sam.ToString("0.##"), "0", opcode, opdesc, dt.Rows[J][3].ToString());
                            }
                        }
                    }

                    //calculate average actual sam
                    if (Convert.ToDecimal(dgvstation.RowCount) > 0)
                    {
                        avg_sam /= Convert.ToDouble(dgvstation.RowCount);
                    }

                    for (int i = 0; i < dgvstation.RowCount; i++)
                    {
                        //calculate the diffrential sam
                        double actual_sam = Convert.ToDouble(dgvstation.Rows[i].Cells[5].Value.ToString());
                        double ac_avg = actual_sam - avg_sam;

                        if (ac_avg < 0)
                        {
                            ac_avg = ac_avg * -1;
                        }
                        else if (ac_avg > 0)
                        {
                            ac_avg = ac_avg * -1;
                        }

                        //roond off the avrage sam
                        double roundoff = Convert.ToDouble(txtroundoff.Text);

                        if (ac_avg < roundoff && ac_avg > -roundoff)
                        {
                            ac_avg = 0;
                        }

                        //add to grid
                        dgvstation.Rows[i].Cells[6].Value = ac_avg.ToString("0.##");
                        data1.Rows.Add(mo, "Station ID :" + dgvstation.Rows[i].Cells[0].Value + " \nOP Code :" + dgvstation.Rows[i].Cells[7].Value + "\nEmp ID :" + dgvstation.Rows[i].Cells[9].Value, ac_avg, moline);
                        data2.Rows.Add(dgvstation.Rows[i].Cells[0].Value, dgvstation.Rows[i].Cells[1].Value, dgvstation.Rows[i].Cells[2].Value, dgvstation.Rows[i].Cells[3].Value, dgvstation.Rows[i].Cells[4].Value, dgvstation.Rows[i].Cells[5].Value, dgvstation.Rows[i].Cells[6].Value, dgvstation.Rows[i].Cells[7].Value, dgvstation.Rows[i].Cells[8].Value, dgvstation.Rows[i].Cells[9].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void radChartView1_LabelFormatting(object sender, ChartViewLabelFormattingEventArgs e)
        {
            e.LabelElement.ForeColor = Color.White;
            e.LabelElement.BorderColor = Color.DodgerBlue;
        }

        private void radDropDownList1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Refresh_Balancing();   //calculate the diffrential line balancing
        }

        public void Refresh_Balancing()
        {
            if (dgvmo.Rows.Count == 0)
            {
                return;
            }

            //check if the round off value is integer
            Regex r = new Regex("^[0-9]{1,3}([.][0-9]{1,2})?$");
            if (!r.IsMatch(txtroundoff.Text))
            {
                RadMessageBox.Show("Invalid Round Off value.  Example : 1.20", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                txtroundoff.Text = "0.5";
            }

            calculate_chart();   //generate chart

            //check what is selected
            if (cmbselect.Text == "Chart View")
            {
                chartdata();
            }
            else if (cmbselect.Text == "Report View")
            {
                report();
            }
            else if (cmbselect.Text == "Table View")
            {
                reportViewer1.Visible = false;
                chrtline.Visible = false;
                dgvstation.Visible = true;
            }
        }

        private void dtpdate_ValueChanged(object sender, EventArgs e)
        {
            dgvmo.Rows.Clear();

            //get all the mo used for day
            MySqlDataAdapter sda2 = new MySqlDataAdapter("select distinct MO_NO, MO_LINE FROM stationhistory where TIME>='" + dtpdate.Value.ToString("yyyy-MM-dd") + " 00:00:00' and TIME<='" + dtpdate.Value.ToString("yyyy-MM-dd") + " 23:59:59'", dc.conn);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            sda2.Dispose();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {                
                dgvmo.Rows.Add(dt2.Rows[i][0].ToString(), dt2.Rows[i][1].ToString());
                dgvmo.Rows[0].IsSelected = true;
            }

            cmbselect.Visible = true;
        }

        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 60000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Refresh_Balancing();   //calculate the diffrential line balancing
        }

        public void report()
        {
            reportViewer1.Visible = true;
            dgvstation.Visible = true;
            chrtline.Visible = false;

            DataView view = new DataView(data1);
            DataView view1 = new DataView(data2);

            //get logo
            DataTable dt_image = new DataTable();
            dt_image.Columns.Add("image", typeof(byte[]));
            dt_image.Rows.Add(dc.GetImage());
            DataView dv_image = new DataView(dt_image);

            reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Line_Balancing.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();

            //add views to dataset
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", view1));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", dv_image));
            reportViewer1.RefreshReport();
        }

        public void chartdata()
        {
            try
            {
                //generate chart
                reportViewer1.Visible = false;
                chrtline.Visible = true;
                chrtline.Series.Clear();
                for (int i = 0; i < dgvstation.RowCount; i++)
                {
                    CartesianArea area = this.chrtline.GetArea<CartesianArea>();
                    area.ShowGrid = true;

                    CartesianGrid grid = area.GetGrid<CartesianGrid>();
                    grid.DrawHorizontalStripes = true;
                    grid.DrawVerticalStripes = true;                    

                    BarSeries barSeries1 = new BarSeries();
                    barSeries1.DataPoints.Add(new CategoricalDataPoint(Convert.ToDouble(dgvstation.Rows[i].Cells[6].Value), dgvstation.Rows[i].Cells[0].Value + " \n OP Code : " + dgvstation.Rows[i].Cells[7].Value + " \n OP Desc : " + dgvstation.Rows[i].Cells[8].Value + " \n Emp ID : " + dgvstation.Rows[i].Cells[9].Value));
                    barSeries1.ShowLabels = true;

                    chrtline.BackColor = Color.FromArgb(43, 43, 43);
                    chrtline.ChartElement.TitleElement.ForeColor = Color.White;
                    chrtline.Series.Add(barSeries1);

                    LinearAxis verticalAxis1 = chrtline.Axes[1] as LinearAxis;
                    verticalAxis1.LabelFitMode = AxisLabelFitMode.MultiLine;
                    verticalAxis1.ShowLabels = true;
                    verticalAxis1.ForeColor = Color.White;
                    verticalAxis1.BorderColor = Color.DodgerBlue;
                    verticalAxis1.Title = "DIFFERENTIAL ACTUAL SAM";

                    CategoricalAxis ca1 = chrtline.Axes[0] as CategoricalAxis;
                    ca1.LabelFitMode = AxisLabelFitMode.MultiLine;
                    ca1.Title = "STATIONS";
                    ca1.ForeColor = Color.White;
                    ca1.BorderColor = Color.DodgerBlue;
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void cmbautorefresh_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //change the auto refresh timer interval
            timer1.Stop();
            int interval = int.Parse(cmbautorefresh.Text);
            interval = interval * 1000;
            timer1.Interval = interval;
            timer1.Start();
        }

        private void Line_Balancing_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
        }

        public void ProductionDetails_tooltip()
        {           
            try
            {
                if (dgvmo.SelectedRows.Count == 0)
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
                SqlDataAdapter sda = new SqlDataAdapter(" SELECT DISTINCT MO.V_MO_NO, MO.V_COLOR_ID,MO.V_SIZE_ID,MO.V_ARTICLE_ID,MO.I_ORDER_QTY,MO.V_USER_DEF1,MO.V_USER_DEF2,MO.V_USER_DEF3,MO.V_USER_DEF4,MO.V_USER_DEF5,MO.V_USER_DEF6,MO.V_USER_DEF7,MO.V_USER_DEF8,MO.V_USER_DEF9,MO.V_USER_DEF10,MO.V_MO_LINE,MO.V_STATUS,MO.I_ID,MO.I_HANGER_COUNT,MO.V_PURCHASE_ORDER,MO.V_SALES_ORDER,MO.V_SHIPPING_DEST,MO.V_SHIPPING_MODE,c.V_CUSTOMER_NAME FROM MO_DETAILS MO, MO m ,CUSTOMER_DB c where m.V_MO_NO=MO.V_MO_NO and m.V_CUSTOMER_ID=c.V_CUSTOMER_ID and MO.V_MO_NO='" + dgvmo.SelectedRows[0].Cells[0].Value + "' and MO.V_MO_LINE='" + dgvmo.SelectedRows[0].Cells[1].Value + "' and MO.V_STATUS!='COMP' order by MO.I_ID DESC", dc.con);
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

                    toolTip1.SetToolTip(dgvmo, " MO NO : " + mo + "\n MO Details :" + moline + "\n Article : " + article + " \n Size : " + size + " \n Color : " + color + " \n " + u1 + " : " + user1 + " \n " + u2 + " : " + user2 + " \n " + u3 + " : " + user3 + " \n " + u4 + " : " + user4 + "\n Purchase Order :" + purorder + "\n Sales Order :" + salesorder + "\n Shipping Dest:" + dest + "\n Shipping Mode :" + mode + "\n Customer :" + cust);
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            Refresh_Balancing();   //calculate diffrential line balancing
        }
    }
}
