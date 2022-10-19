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
    public partial class Station_Idle : Telerik.WinControls.UI.RadForm
    {
        public Station_Idle()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();   //connection class
        DataTable EMP = new DataTable();
        String controller_name = "";
        DataTable data1 = new DataTable();
        DataTable data = new DataTable();
        DataTable data2 = new DataTable();

        String theme = "";
       

        public void GridTheme(String theme)
        {
            dgvempreport.ThemeName = theme;
        }

        

        private void dgvempreport_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
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

     
        public void select_controller()
        {
            dc.OpenConnection();  //Open Connection
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

            //get the IP address of the selected Controller
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

            dc.Close_Connection(); //close the connection if Open
            dc.OpenMYSQLConnection(ipaddress); //Open connection
        }

        private void btnRptView_Click(object sender, EventArgs e)
        {
            //generate report
            if (btnRptView.Text == "Report View")
            {
                pnlDgv.Visible = false;
                btnRptView.Text = "Table View";

                DataView view = new DataView(data1);
                DataView view1 = new DataView(data2);
                //DataView view2 = new DataView(data2);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.station_function.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                //add views to dataset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsAction", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("dsStnFunc", view1));
                //reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", view2));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", dv_image));
                reportViewer1.RefreshReport();
            }
            else
            {
                pnlDgv.Visible = true;
                btnRptView.Text = "Report View";
            }
        }

        private void cmbLineNo_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //get all the production line no
            string strSql = "";
            if (cmbLineNo.SelectedItem.Text == "All")
            {
                cmbStnId.Items.Clear();
                cmbStnId.Items.Add("All");
            }
            else
            {
                int intLineNo = int.Parse(cmbLineNo.SelectedItem.Text);
                strSql = "SELECT I_STN_ID FROM STATION_DATA WHERE I_INFEED_LINE_NO = " + intLineNo;
                SqlDataAdapter sda = new SqlDataAdapter(strSql, dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                cmbStnId.Items.Clear();
                cmbStnId.Items.Add("All");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbStnId.Items.Add(dt.Rows[i][0].ToString());
                }
            }
        }

        private void Station_Idle_Load(object sender, EventArgs e)
        {
            dgvempreport.MasterTemplate.SelectLastAddedRow = false;
            dgvempreport.MasterView.TableSearchRow.ShowCloseButton = false;   //disable close button for search in grid 

            EMP.Columns.Add("Line No");
            EMP.Columns.Add("Station Id");
            EMP.Columns.Add("Emp. Id");
            EMP.Columns.Add("Emp. Name");
            EMP.Columns.Add("Status");
            EMP.Columns.Add("Start Time ");
            EMP.Columns.Add("End Time");
            EMP.Columns.Add("Duration (minute)");

            data1.Columns.Add("LINE_NO");
            data1.Columns.Add("STN_ID");
            data1.Columns.Add("EMP_ID");
            data1.Columns.Add("EMP_NAME");
            data1.Columns.Add("ACTION_START");
            data1.Columns.Add("TIME_START");
            data1.Columns.Add("TIME_END");
            data1.Columns.Add("DURATION");

            data2.Columns.Add("LINE_NO");
            data2.Columns.Add("STN_ID");
            data2.Columns.Add("DATE");


            //dgvempreport.Columns[0].Width = 50;
            //dgvempreport.Columns[1].Width = 50;
            //dgvempreport.Columns[2].Width = 50;
            //dgvempreport.Columns[3].Width = 50;

            dgvempreport.DataSource = EMP;

            dtpFrom.Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
        }

        private void Station_Idle_Initialized(object sender, EventArgs e)
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
            select_controller();

            //get all the production line no
            SqlDataAdapter sda = new SqlDataAdapter("select V_PROD_LINE from PROD_LINE_DB;", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            cmbLineNo.Items.Add("All");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbLineNo.Items.Add(dt.Rows[i][0].ToString());
            }

            //change grid theme
            GridTheme(theme);
        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            if (cmbLineNo.Text == "")
            {
                return;
            }

            if (cmbStnId.Text == "")
            {
                return;
            }

            string strMySql = "";

            dc.OpenConnection(); //Open the Connection
            try
            {
                EMP.Rows.Clear();
                data1.Rows.Clear();
                data2.Rows.Clear();
                data.Rows.Clear();

                //btnreport.Text = "Report View";

                String start_date = dtpFrom.Value.ToString("yyyy-MM-dd");
                String end_date = dtpTo.Value.ToString("yyyy-MM-dd");

                string strFilter = "";

                string strLineNo = cmbLineNo.Text;
                if (strLineNo != "All")
                {
                    int intLineNo = int.Parse(strLineNo);
                    strFilter = " AND LINE_NO = " + intLineNo + "";
                }

                string strStnId = cmbStnId.Text;
                if (strStnId != "All")
                {
                    int intStnId = int.Parse(strStnId);
                    strFilter += " AND STN_ID = " + intStnId + "";
                }
            
                strMySql = "SELECT LINE_NO, STN_ID, EMP_ID, ACTIONID_START, TIME_START, ACTIONID_END, TIME_END " +
"FROM stationstatus WHERE TIME_START > '" + start_date + " 00:00:00' AND TIME_START< '" + end_date + " 23:59:59' AND ACTIONID_START IN (3, 4, 5, 6, 7, 8, 9, 10) " + strFilter;

                MySqlDataAdapter daEmpStn = new MySqlDataAdapter(strMySql, dc.conn);
                DataTable dtEmpStn = new DataTable();
                daEmpStn.Fill(dtEmpStn);
                daEmpStn.Dispose();

                dtEmpStn.Columns.Add("EmpName", typeof(String));
                for (int i = 0; i < dtEmpStn.Rows.Count; i++)
                {
                    int intLineNo = int.Parse(dtEmpStn.Rows[i]["LINE_NO"].ToString());
                    int intStnId = int.Parse(dtEmpStn.Rows[i]["STN_ID"].ToString());
                    int intEmpId = int.Parse(dtEmpStn.Rows[i]["EMP_ID"].ToString());
                    string strEmpName = "";
                    int intActionId_start = int.Parse(dtEmpStn.Rows[i]["ACTIONID_START"].ToString());

                    DateTime dtTimeStart = DateTime.Parse(dtEmpStn.Rows[i]["TIME_START"].ToString());
                    string strTimeStart = dtTimeStart.ToString("dd/MM/yyyy HH:mm:ss");

                    //int intActionId_end = int.Parse(dtEmpStn.Rows[i]["ACTIONID_END"].ToString());

                    DateTime dtTimeEnd = DateTime.Parse(dtEmpStn.Rows[i]["TIME_END"].ToString());
                    string strTimeEnd = dtTimeEnd.ToString("dd/MM/yyyy HH:mm:ss");


                    string strAction_start = "";


                    switch (intActionId_start)
                    {
                        case 3:
                            strAction_start = "PRAYER";
                            break;

                        case 5:
                            strAction_start = "BREAK"; ;
                            break;

                        case 7:
                            strAction_start = "REPAIR";
                            break;

                        case 9:
                            strAction_start = "IDLE";
                            break;
                    }



                    TimeSpan duration = (dtTimeEnd - dtTimeStart);
                    double dblDuration = Math.Round(duration.TotalMinutes, 2);

                    string strSql = "SELECT V_FIRST_NAME, V_LAST_NAME FROM EMPLOYEE WHERE V_EMP_ID = '" + intEmpId + "'";
                    //get the station no of the employee 
                    SqlDataAdapter sda = new SqlDataAdapter(strSql, dc.con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        strEmpName = dt.Rows[j]["V_FIRST_NAME"].ToString();
                    }
                    sda.Dispose();

                    EMP.Rows.Add(intLineNo, intStnId, intEmpId, strEmpName, strAction_start, strTimeStart, strTimeEnd, dblDuration);
                    data1.Rows.Add(intLineNo, intStnId, intEmpId, strEmpName, strAction_start, strTimeStart, strTimeEnd, dblDuration);
                    
                dgvempreport.DataSource = EMP;

            }

                string strDate = "";
                if (start_date == end_date)
                {
                    DateTime dtStartDate = DateTime.Parse(start_date);
                    start_date = dtStartDate.ToString("dd/MM/yyyy");

                    strDate = start_date;
                }
                else
                {
                    DateTime dtStartDate = DateTime.Parse(start_date);
                    start_date = dtStartDate.ToString("dd/MM/yyyy");

                    DateTime dtEndDateDate = DateTime.Parse(end_date);
                    end_date = dtEndDateDate.ToString("dd/MM/yyyy");

                    strDate = start_date + " - " + end_date;
                }


                data2.Rows.Add(strLineNo, strStnId, strDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }
        }
    }
}
