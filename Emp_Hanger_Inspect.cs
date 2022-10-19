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
    public partial class Emp_Hanger_Inspect : Telerik.WinControls.UI.RadForm
    {
        public Emp_Hanger_Inspect()
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
        private void Emp_Hanger_Inspect_Initialized(object sender, EventArgs e)
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

            ////change the form language
            //SqlDataAdapter sda = new SqlDataAdapter("select " + Lang + " from Language where Form='EmployeeInspection' order by Item_No", dc.con);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //if (dt.Rows.Count > 0)
            //{
            //    lblempid.Text = dt.Rows[0][0].ToString() + " :";
            //    lblfname.Text = dt.Rows[1][0].ToString() + " :";
            //    lbllname.Text = dt.Rows[2][0].ToString() + " :";
            //    lblpiecerate.Text = dt.Rows[6][0].ToString() + " :";
            //    lblsam.Text = dt.Rows[7][0].ToString() + " :";
            //    lbldate.Text = dt.Rows[8][0].ToString() + " :";
            //    btnsearch.Text = dt.Rows[10][0].ToString();
            //}



            //change grid theme
            GridTheme(theme);
        }

        public void GridTheme(String theme)
        {
            dgvempreport.ThemeName = theme;
        }

        private void Emp_Hanger_Inspect_Load(object sender, EventArgs e)
        {
            dgvempreport.MasterTemplate.SelectLastAddedRow = false;
            dgvempreport.MasterView.TableSearchRow.ShowCloseButton = false;   //disable close button for search in grid 

            data1.Columns.Add("HANGER_ID");
            data1.Columns.Add("PC_COUNT");
            data1.Columns.Add("MO_NO");
            data1.Columns.Add("MO_LINE");
            data1.Columns.Add("V_ARTICLE_DESC");
            data1.Columns.Add("V_COLOR_DESC");
            data1.Columns.Add("V_SIZE_DESC");
            data1.Columns.Add("STN_ID");
            data1.Columns.Add("V_PROD_LINE");
            data1.Columns.Add("TIME");

            EMP.Columns.Add("Hanger ID");
            EMP.Columns.Add("PC Count");
            EMP.Columns.Add("MO");
            EMP.Columns.Add("MO Line");
            EMP.Columns.Add("Article");
            EMP.Columns.Add("Color");
            EMP.Columns.Add("Size");
            EMP.Columns.Add("Station");
            EMP.Columns.Add("Line No");
            EMP.Columns.Add("Time");

            data.Columns.Add("id");
            data.Columns.Add("name");
            data.Columns.Add("date");
            data.Columns.Add("shift");
            data.Columns.Add("totalpiececount");
            data.Columns.Add("averagesam");

            data2.Columns.Add("PIECE_COUNT");
            data2.Columns.Add("HOUR");
            data2.Columns.Add("OPDESC");

            dgvempreport.DataSource = EMP;
            //dgvempreport.Columns[0].Width = 70;
            //dgvempreport.Columns[1].Width = 60;
            //dgvempreport.Columns[2].Width = 55;
            //dgvempreport.Columns[3].Width = 65;
            //dgvempreport.Columns[4].Width = 60;

            dtpFrom .Value = DateTime.Now;
            dtpTo.Value = DateTime.Now;
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

        private void btnsearch_Click(object sender, EventArgs e)
        {
            if (txtempid.Text == "")
            {
                MessageBox.Show("Please enter Employee ID!");
                return;
            }


            DateTime dtStart= DateTime.Parse(dtpFrom.Value.ToString("yyyy-MM-dd"));
            DateTime dtEnd = DateTime.Parse(dtpTo.Value.ToString("yyyy-MM-dd"));
            if (dtStart > dtEnd)
            {
                MessageBox.Show("Start Date must be earlier than End Date!");
                return;
            }

                try
            {
                EMP.Rows.Clear();
                data1.Rows.Clear();
                data2.Rows.Clear();
                data.Rows.Clear();

               //btnreport.Text = "Report View";

                string empid = txtempid.Text;

                DateTime shift_start = Convert.ToDateTime("9:30:00");
                DateTime shift_end = Convert.ToDateTime("18:30:00");
                DateTime overtime_end = Convert.ToDateTime("19:30:00");

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

                //check if hide overtime is enabled
                String hide_ot = "";
                cmd = new SqlCommand("select HIDE_OVERTIME from Setup", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    hide_ot = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                String startime = shift_start.ToString("HH:mm:ss");
                String endtime = overtime_end.ToString("HH:mm:ss");

                String start_date = dtpFrom.Value.ToString("yyyy-MM-dd");
                String end_date = dtpTo.Value.ToString("yyyy-MM-dd");
                if (shift_start > shift_end)
                {
                    start_date = dtpFrom.Value.AddDays(-1).ToString("yyyy-MM-dd");
                }

                if (cmbshift.Text == "All")
                {
                    startime = "00:00:00";
                    endtime = "23:59:59";
                }

                if (hide_ot == "TRUE")
                {
                    endtime = shift_end.ToString("HH:mm:ss");
                }

                string strFirstName = "";
                string strLastName = "";
                string strSql = "SELECT V_FIRST_NAME, V_LAST_NAME FROM EMPLOYEE WHERE V_EMP_ID = '" + empid + "'";
                //get the station no of the employee 
                SqlDataAdapter sda = new SqlDataAdapter(strSql, dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strFirstName = dt.Rows[i]["V_FIRST_NAME"].ToString();
                    strLastName = dt.Rows[i]["V_LAST_NAME"].ToString();

                   txtempfirstname.Text = strFirstName;
                   txtemplastname.Text = strLastName;
                }
                sda.Dispose();

                //find hanger records

                string strSql2 = "SELECT HANGER_ID, PC_COUNT, MO_NO, MO_LINE, " +
                "ARTICLE_DB.V_ARTICLE_DESC, " +
                "SIZE_DB.V_SIZE_DESC , " +
                "COLOR_DB.V_COLOR_DESC, " +
                "MO_DETAILS.V_PROD_LINE, " +
                "STN_ID, FORMAT(TIME, 'dd-MM-yyyy HH:mm:ss') as TIME " +
                "from HANGER_HISTORY " +
                "INNER JOIN MO_DETAILS ON MO_DETAILS.V_MO_NO = HANGER_HISTORY.MO_NO and MO_DETAILS.V_MO_LINE = HANGER_HISTORY.MO_LINE " +
                "INNER JOIN ARTICLE_DB ON ARTICLE_DB.V_ARTICLE_ID = MO_DETAILS.V_ARTICLE_ID " +
                "INNER JOIN SIZE_DB ON SIZE_DB.V_SIZE_ID = MO_DETAILS.V_SIZE_ID " +
                "INNER JOIN COLOR_DB ON COLOR_DB.V_COLOR_ID = MO_DETAILS.V_COLOR_ID " +
                "where HANGER_HISTORY.EMP_ID = '" + empid + "' and (HANGER_HISTORY.time > '" + start_date + " " + startime + "' and HANGER_HISTORY.time < '" + end_date + " " + endtime + "') order by time asc;";

                //get the station no of the employee 
                SqlDataAdapter sda2 = new SqlDataAdapter(strSql2, dc.con);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);

                int intPcCnt = 0;
                
                if (dt2.Rows.Count > 0)
                {
                    intPcCnt = dt2.Rows.Count;

                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                    string Hanger_ID = dt2.Rows[i]["HANGER_ID"].ToString();
                    string PC_Count = dt2.Rows[i]["PC_COUNT"].ToString();
                    string MO = dt2.Rows[i]["MO_NO"].ToString();
                    string MO_Line = dt2.Rows[i]["MO_LINE"].ToString();
                    string Article = dt2.Rows[i]["V_ARTICLE_DESC"].ToString();
                    string Color = dt2.Rows[i]["V_COLOR_DESC"].ToString();
                    string Size = dt2.Rows[i]["V_SIZE_DESC"].ToString();
                    string Station = dt2.Rows[i]["STN_ID"].ToString();
                    string Line_No = dt2.Rows[i]["V_PROD_LINE"].ToString();
                    string Time = dt2.Rows[i]["TIME"].ToString();

                    EMP.Rows.Add(Hanger_ID, PC_Count, MO, MO_Line, Article, Color, Size, Station, Line_No, Time);
                    data1.Rows.Add(Hanger_ID, PC_Count, MO, MO_Line, Article, Color, Size, Station, Line_No, Time);
                    }
                sda2.Dispose();
                dgvempreport.DataSource = EMP;

                }

                txtpiececnt.Text = Convert.ToString(intPcCnt);

                //---------------------------------------------------

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



                data.Rows.Add(empid, strFirstName, strDate, cmbshift.Text, intPcCnt, 0);

                //---------------------------------------------------------


                
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex + "");
            }
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            //generate report
            if (btnreport.Text == "Report View")
            {
                //panel3.Visible = false;
                panel4.Visible = false;
                //panel7.Visible = false;
                btnreport.Text = "Table View";

                DataView view = new DataView(data1);
                DataView view1 = new DataView(data);
                DataView view2 = new DataView(data2);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.hanger_report.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                //add views to dataset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", view1));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", view2));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", dv_image));
                reportViewer1.RefreshReport();
            }
            else
            {

                panel4.Visible = true;
                btnreport.Text = "Report View";
            }
        }
    }
}
