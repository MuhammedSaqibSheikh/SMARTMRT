using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SMARTMRT
{
    public partial class QC : Telerik.WinControls.UI.RadForm
    {
        public QC()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();
        DataTable emp = new DataTable();
        DataTable data1 = new DataTable();

        private void QC_Load(object sender, EventArgs e)
        {
            dgvemployee.MasterTemplate.SelectLastAddedRow = false;
            dgvqc.MasterTemplate.SelectLastAddedRow = false;
            //disable close button on search in grid
            dgvemployee.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvqc.MasterView.TableSearchRow.ShowCloseButton = false;

            data1.Columns.Add("V_MO_NO");
            data1.Columns.Add("V_MO_LINE");
            data1.Columns.Add("I_STATION_ID");
            data1.Columns.Add("V_ARTICLE");
            data1.Columns.Add("V_COLOR");
            data1.Columns.Add("V_SIZE");
            data1.Columns.Add("V_OP_CODE");
            data1.Columns.Add("V_OP_DESC");
            data1.Columns.Add("V_QC_MAIN_CODE");
            data1.Columns.Add("V_QC_MAIN_DESC");
            data1.Columns.Add("V_QC_SUB_CODE");
            data1.Columns.Add("V_QC_SUB_DESC");
            data1.Columns.Add("D_DATE_TIME");
            data1.Columns.Add("V_EMP_ID");
            data1.Columns.Add("V_EMP_NAME");
            data1.Columns.Add("I_QUANTITY");
            data1.Columns.Add("totalqty");
            data1.Columns.Add("subeff");
            data1.Columns.Add("totaleff");
            data1.Columns.Add("production");
            data1.Columns.Add("D_DATE_TIME1");

            emp.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            emp.Columns.Add("Emp ID");
            emp.Columns.Add("Emp Name");
            dgvemployee.DataSource = emp;

            dc.OpenConnection();   //open connection

            dtpstart.Text = DateTime.Now.ToString();
            dtpend.Text = DateTime.Now.ToString();

            dgvemployee.Columns[1].ReadOnly = true;
            dgvemployee.Columns[2].ReadOnly = true;

            radButton4.PerformClick();
        }
        public void QC_EMP_ROW_SELECTED()
        {
            try
            {
                String start = dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                String end = dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59";
                dgvqc.Rows.Clear();

                //get all the employees
                for (int i = 0; i < dgvemployee.Rows.Count; i++)
                {
                    //check if the employee is selected
                    if ((bool)(dgvemployee.Rows[i].Cells[0].Value) == true)
                    {
                        //get repair details
                        SqlDataAdapter da1 = new SqlDataAdapter("SELECT V_MO_NO,V_MO_LINE,I_STATION_ID, V_COLOR,V_ARTICLE,V_SIZE, V_OP_CODE, V_OP_DESC, V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC,sum(I_QUANTITY) FROM MRT_GLOBALDB.dbo.QC_HISTORY  where V_EMP_ID='" + dgvemployee.Rows[i].Cells[1].Value.ToString() + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and CONVERT(nvarchar(10), D_DATE_TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') group by V_MO_NO,V_MO_LINE,I_STATION_ID, V_COLOR,V_ARTICLE,V_SIZE, V_OP_CODE, V_OP_DESC, V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC order by V_MO_NO,V_MO_LINE, V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC", dc.con);
                        DataTable dt = new DataTable();
                        da1.Fill(dt);
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            int piececount = 0;
                            int total_qty = 0;
                            decimal sub_eff = 0;
                            decimal total_eff = 0;

                            //get production details
                            SqlCommand cmd = new SqlCommand("SELECT SUM(PC_COUNT) as HANGER_ID  FROM HANGER_HISTORY where EMP_ID = '" + dgvemployee.Rows[i].Cells[1].Value.ToString() + "'  and TIME>='" + start + "' and TIME<'" + end + "' and MO_NO='" + dt.Rows[j][0].ToString() + "' and MO_LINE='" + dt.Rows[j][1].ToString() + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                            String temp = cmd.ExecuteScalar() + "";
                            if (temp != "")
                            {
                                piececount = int.Parse(temp);
                            }

                            //get the group id for the employee
                            SqlDataAdapter sda = new SqlDataAdapter("SELECT DISTINCT V_GROUP_ID FROM EMPLOYEE_GROUPS WHERE V_EMP_ID='" + dgvemployee.Rows[i].Cells[1].Value.ToString() + "' ", dc.con);
                            DataTable dt1 = new DataTable();
                            sda.Fill(dt1);
                            for (int k = 0; k < dt1.Rows.Count; k++)
                            {
                                //get the production details
                                cmd = new SqlCommand("SELECT SUM(PC_COUNT) as HANGER_ID  FROM HANGER_HISTORY where EMP_ID = '" + dt1.Rows[k][0] + "'  and TIME>='" + start + "' and TIME<'" + end + "' and MO_NO='" + dt.Rows[j][0].ToString() + "' and MO_LINE='" + dt.Rows[j][1].ToString() + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                                temp = cmd.ExecuteScalar() + "";
                                if (temp != "")
                                {
                                    piececount += int.Parse(temp);
                                }
                            }

                            //get sum of repair for the employee
                            total_qty = 0;
                            cmd = new SqlCommand("SELECT  SUM(I_QUANTITY) AS I_QUANTITY FROM MRT_GLOBALDB.dbo.QC_HISTORY QC_HISTORY where  V_EMP_ID = '" + dgvemployee.Rows[i].Cells[1].Value.ToString() + "'  and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and V_MO_NO='" + dt.Rows[j][0].ToString() + "' and V_MO_LINE='" + dt.Rows[j][1].ToString() + "' and CONVERT(nvarchar(10), D_DATE_TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                            temp = cmd.ExecuteScalar() + "";                           
                            if (temp != "")
                            {
                                total_qty = int.Parse(temp);
                            }

                            //calculate defect efficiency
                            if (total_qty > 0)
                            {
                                sub_eff = (Convert.ToDecimal(dt.Rows[j][12].ToString()) / (decimal)total_qty) * 100;
                            }

                            //calculate production efficiency
                            if (piececount > 0)
                            {
                                total_eff = (Convert.ToDecimal(total_qty) / (decimal)piececount) * 100;
                            }

                            //add to grid
                            dgvqc.Rows.Add(dgvemployee.Rows[i].Cells[1].Value.ToString(), dt.Rows[j][0].ToString(), dt.Rows[j][1].ToString(), dt.Rows[j][2].ToString(), dt.Rows[j][3].ToString(), dt.Rows[j][4].ToString(), dt.Rows[j][5].ToString(), dt.Rows[j][6].ToString(), dt.Rows[j][7].ToString(), dt.Rows[j][8].ToString(), dt.Rows[j][9].ToString(), dt.Rows[j][10].ToString(), dt.Rows[j][11].ToString(), dt.Rows[j][12].ToString(), sub_eff.ToString("0.##") + "%", total_qty, piececount, total_eff.ToString("0.##") + "%", dgvemployee.Rows[i].Cells[2].Value.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                radLabel1.Text = ex.Message;
            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            try
            {
                String start = dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                String end = dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59";
                data1.Rows.Clear();
                for (int j = 0; j < dgvqc.Rows.Count; j++)
                {
                    data1.Rows.Add(dgvqc.Rows[j].Cells[1].Value.ToString(), dgvqc.Rows[j].Cells[2].Value.ToString(), dgvqc.Rows[j].Cells[3].Value.ToString(), dgvqc.Rows[j].Cells[4].Value.ToString(), dgvqc.Rows[j].Cells[5].Value.ToString(), dgvqc.Rows[j].Cells[6].Value.ToString(), dgvqc.Rows[j].Cells[7].Value.ToString(), dgvqc.Rows[j].Cells[8].Value.ToString(), dgvqc.Rows[j].Cells[9].Value.ToString(), dgvqc.Rows[j].Cells[10].Value.ToString(), dgvqc.Rows[j].Cells[11].Value.ToString(), dgvqc.Rows[j].Cells[12].Value.ToString(), start, dgvqc.Rows[j].Cells[0].Value.ToString(), dgvqc.Rows[j].Cells[18].Value.ToString(), dgvqc.Rows[j].Cells[13].Value.ToString(), dgvqc.Rows[j].Cells[15].Value.ToString(), dgvqc.Rows[j].Cells[14].Value.ToString(), dgvqc.Rows[j].Cells[17].Value.ToString(), dgvqc.Rows[j].Cells[16].Value.ToString(),end);
                }

                reportViewer2.Visible = true;
                radButton3.Visible = true;
                DataView view = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer2.LocalReport.ReportEmbeddedResource = "SMARTMRT.QC.rdlc";
                reportViewer2.LocalReport.DataSources.Clear();

                //add view to dataset
                reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer2.RefreshReport();
            }
            catch (Exception ex)
            {
                radLabel1.Text = ex.Message;
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            try
            {
                String start = dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                String end = dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59";

                reportViewer2.Visible = false;
                radButton3.Visible = true;
                radButton3.Text = "Report View";
                dgvqc.Rows.Clear();

                for (int i = 0; i < dgvemployee.Rows.Count; i++)
                {
                    //get repair details
                    SqlDataAdapter da1 = new SqlDataAdapter("SELECT V_MO_NO,V_MO_LINE,I_STATION_ID, V_COLOR,V_ARTICLE,V_SIZE, V_OP_CODE, V_OP_DESC, V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC,sum(I_QUANTITY) FROM MRT_GLOBALDB.dbo.QC_HISTORY  where V_EMP_ID='" + dgvemployee.Rows[i].Cells[1].Value.ToString() + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and CONVERT(nvarchar(10), D_DATE_TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') group by V_MO_NO,V_MO_LINE,I_STATION_ID, V_COLOR,V_ARTICLE,V_SIZE, V_OP_CODE, V_OP_DESC, V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC order by V_MO_NO,V_MO_LINE, V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC", dc.con);
                    DataTable dt = new DataTable();
                    da1.Fill(dt);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        int piececount = 0;
                        int total_qty = 0;
                        decimal sub_eff = 0;
                        decimal total_eff = 0;

                        //get the production details
                        SqlDataAdapter da7 = new SqlDataAdapter("SELECT SUM(PC_COUNT) as HANGER_ID  FROM HANGER_HISTORY where EMP_ID = '" + dgvemployee.Rows[i].Cells[1].Value.ToString() + "'  and TIME>='" + start + "' and TIME<'" + end + "' and MO_NO='" + dt.Rows[j][0].ToString() + "' and MO_LINE='" + dt.Rows[j][1].ToString() + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                        DataTable dt7 = new DataTable();
                        da7.Fill(dt7);
                        for (int k = 0; k < dt7.Rows.Count; k++)
                        {
                            String temp = dt7.Rows[k]["HANGER_ID"].ToString();
                            if (temp != "")
                            {
                                piececount = Int32.Parse(dt7.Rows[k]["HANGER_ID"].ToString());
                            }
                        }

                        //get sum og repair for the employee
                        SqlDataAdapter da11 = new SqlDataAdapter("SELECT  SUM(I_QUANTITY) AS I_QUANTITY FROM MRT_GLOBALDB.dbo.QC_HISTORY QC_HISTORY where  V_EMP_ID = '" + dgvemployee.Rows[i].Cells[1].Value.ToString() + "'  and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and V_MO_NO='" + dt.Rows[j][0].ToString() + "' and V_MO_LINE='" + dt.Rows[j][1].ToString() + "' and CONVERT(nvarchar(10), D_DATE_TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                        DataTable dt11 = new DataTable();
                        da11.Fill(dt11);
                        for (int k = 0; k < dt11.Rows.Count; k++)
                        {
                            String temp = dt11.Rows[k]["I_QUANTITY"].ToString();
                            total_qty = 0;
                            if (temp != "")
                            {
                                total_qty = int.Parse(temp);
                            }
                        }

                        //calculate sun efficiency
                        if (total_qty > 0)
                        {
                            sub_eff = (Convert.ToDecimal(dt.Rows[j][12].ToString()) / (decimal)total_qty) * 100;
                        }

                        //calculate total efficiency
                        if (piececount > 0)
                        {
                            total_eff = (Convert.ToDecimal(total_qty) / (decimal)piececount) * 100;
                        }

                        //add to grid
                        dgvqc.Rows.Add(dgvemployee.Rows[i].Cells[1].Value.ToString(), dt.Rows[j][0].ToString(), dt.Rows[j][1].ToString(), dt.Rows[j][2].ToString(), dt.Rows[j][3].ToString(), dt.Rows[j][4].ToString(), dt.Rows[j][5].ToString(), dt.Rows[j][6].ToString(), dt.Rows[j][7].ToString(), dt.Rows[j][8].ToString(), dt.Rows[j][9].ToString(), dt.Rows[j][10].ToString(), dt.Rows[j][11].ToString(), dt.Rows[j][12].ToString(), sub_eff.ToString("0.##") + "%", total_qty, piececount, total_eff.ToString("0.##") + "%", dgvemployee.Rows[i].Cells[2].Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                radLabel1.Text = ex.Message;
            }
        }

        private void radCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            String start = dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00";
            String end = dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            //txt1.Visible = false;
            emp.Rows.Clear();

            //get all employees
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT distinct V_EMP_ID,V_EMP_NAME from QC_HISTORY WHERE D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "'  and CONVERT(nvarchar(10), D_DATE_TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
            DataTable dt = new DataTable();
            da1.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                emp.Rows.Add(false, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
            }

            dgvemployee.DataSource = emp;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            reportViewer2.Visible = false;
        }

        private void QCGRID_Click(object sender, EventArgs e)
        {
            //txt1.Visible = false;
        }

        private void radLabel1_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            panel4.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel1.Text = "";
            panel4.Visible = false;
            MyTimer.Stop();
        }

        String theme = "";
        private void QC_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();  //open connection

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
            dgvqc.ThemeName = theme;
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            //report
            if (radButton3.Text == "Report View")
            {
                radButton2.PerformClick();
                radButton3.Text = "Table View";
            }
            else
            {
                reportViewer2.Visible = false;
                radButton3.Text = "Report View";
            }
        }       

        private void QCGRID_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //get the selected employee 
            if (e.RowIndex < 0)
            {
                return;
            }

            if ((bool)dgvemployee.Rows[e.RowIndex].Cells[0].Value == true)
            {
                dgvemployee.Rows[e.RowIndex].Cells[0].Value = false;
            }

            else
            {
                dgvemployee.Rows[e.RowIndex].Cells[0].Value = true;
            }

            radButton3.Text = "Report View";
            reportViewer2.Visible = false;
            QC_EMP_ROW_SELECTED();   //calculate qc for the employee
            radButton3.Visible = true;
        }

        private void dgvqc_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvqc.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvqc.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvqc.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvqc.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
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
    }
}
