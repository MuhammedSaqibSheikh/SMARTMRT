using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Telerik.WinControls;
using Microsoft.Reporting.WinForms;

namespace SMARTMRT
{
    public partial class QC_STATION : Telerik.WinControls.UI.RadForm
    {
        public QC_STATION()
        {
            InitializeComponent();
        }

        DataTable data1 = new DataTable();
        Database_Connection dc = new Database_Connection();   //connection class
        DataTable emp = new DataTable();

        private void QC_STATION_Load(object sender, EventArgs e)
        {
            dgvqc.MasterTemplate.SelectLastAddedRow = false;
            dgvstation.MasterTemplate.SelectLastAddedRow = false;
            RadMessageBox.SetThemeName("FluentDark");   //set grid theme
            dgvqc.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvstation.MasterView.TableSearchRow.ShowCloseButton = false;

            emp.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            emp.Columns.Add("Station ID");
            dgvstation.DataSource = emp;
            dgvstation.Columns[1].ReadOnly = true;            

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

            dtpstart.Text = DateTime.Now.ToString();
            dtpend.Text = DateTime.Now.ToString();

            radButton4.PerformClick();
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
                    data1.Rows.Add(dgvqc.Rows[j].Cells[1].Value.ToString(), dgvqc.Rows[j].Cells[2].Value.ToString(), dgvqc.Rows[j].Cells[0].Value.ToString(), dgvqc.Rows[j].Cells[4].Value.ToString(), dgvqc.Rows[j].Cells[3].Value.ToString(), dgvqc.Rows[j].Cells[5].Value.ToString(), dgvqc.Rows[j].Cells[6].Value.ToString(), dgvqc.Rows[j].Cells[7].Value.ToString(), dgvqc.Rows[j].Cells[8].Value.ToString(), dgvqc.Rows[j].Cells[9].Value.ToString(), dgvqc.Rows[j].Cells[10].Value.ToString(), dgvqc.Rows[j].Cells[11].Value.ToString(), start, "", "", dgvqc.Rows[j].Cells[12].Value.ToString(), dgvqc.Rows[j].Cells[14].Value.ToString(), dgvqc.Rows[j].Cells[13].Value.ToString(), dgvqc.Rows[j].Cells[16].Value.ToString(), dgvqc.Rows[j].Cells[15].Value.ToString(),end);
                }

                dgvqc.Visible = false;
                radButton3.Visible = true;

                DataView view = new DataView(data1);
                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer2.LocalReport.ReportEmbeddedResource = "SMARTMRT.QC_STATION.rdlc";

                //add views to dataset
                reportViewer2.LocalReport.DataSources.Clear();
                reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer2.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer2.RefreshReport();
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.ToString(), "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            try
            {
                String start = dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                String end = dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59";

                radButton3.Visible = true;
                dgvqc.Visible = true;
                radButton3.Text = "Report View";
                dgvqc.Rows.Clear();

                for (int i = 0; i < dgvstation.Rows.Count; i++)
                {
                    //get the repair details for the station
                    SqlDataAdapter da1 = new SqlDataAdapter("SELECT I_STATION_ID,V_MO_NO,V_MO_LINE, V_COLOR,V_ARTICLE,V_SIZE, V_OP_CODE, V_OP_DESC, V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC,sum(I_QUANTITY) FROM MRT_GLOBALDB.dbo.QC_HISTORY  where I_STATION_ID='" + dgvstation.Rows[i].Cells[1].Value.ToString() + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and CONVERT(nvarchar(10), D_DATE_TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') group by I_STATION_ID,V_MO_NO,V_MO_LINE, V_COLOR,V_ARTICLE,V_SIZE, V_OP_CODE, V_OP_DESC, V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC order by V_MO_NO,V_MO_LINE, V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC", dc.con);
                    DataTable dt = new DataTable();
                    da1.Fill(dt);
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        int piececount = 0;
                        int total_qty = 0;
                        decimal sub_eff = 0;
                        decimal total_eff = 0;

                      //get station id
                        String[] stn = dgvstation.Rows[i].Cells[1].Value.ToString().Split('.');
                        String stationid = "";
                        SqlCommand cmd1 = new SqlCommand("select s.I_STN_ID from STATION_DATA s where s.I_INFEED_LINE_NO='" + stn[0] + "' and s.I_STN_NO_INFEED='" + stn[1] + "'", dc.con);
                        SqlDataReader sdr1 = cmd1.ExecuteReader();
                        if (sdr1.Read())
                        {
                            stationid = sdr1.GetValue(0).ToString();
                        }
                        sdr1.Close();

                        //get the production details
                        SqlCommand cmd = new SqlCommand("SELECT SUM(PC_COUNT) as HANGER_ID  FROM HANGER_HISTORY where STN_ID = '" + stationid + "'  and TIME>='" + start + "' and TIME<'" + end + "' and MO_NO='" + dt.Rows[j][1].ToString() + "' and MO_LINE='" + dt.Rows[j][2].ToString() + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                        String temp = cmd.ExecuteScalar() + "";
                        if (temp != "")
                        {
                            piececount = int.Parse(temp);
                        }

                        //get the sum of repair for the station
                        total_qty = 0;
                        cmd = new SqlCommand("SELECT  SUM(I_QUANTITY) AS I_QUANTITY FROM MRT_GLOBALDB.dbo.QC_HISTORY QC_HISTORY where  I_STATION_ID = '" + dgvstation.Rows[i].Cells[1].Value.ToString() + "'  and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and V_MO_NO='" + dt.Rows[j][1].ToString() + "' and V_MO_LINE='" + dt.Rows[j][2].ToString() + "' and CONVERT(nvarchar(10), D_DATE_TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
                        temp = cmd.ExecuteScalar() + "";
                        if (temp != "")
                        {
                            total_qty = int.Parse(temp);
                        }

                        //calculate sub efficiency
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
                        dgvqc.Rows.Add(dgvstation.Rows[i].Cells[1].Value.ToString(), dt.Rows[j][1].ToString(), dt.Rows[j][2].ToString(), dt.Rows[j][3].ToString(), dt.Rows[j][4].ToString(), dt.Rows[j][5].ToString(), dt.Rows[j][6].ToString(), dt.Rows[j][7].ToString(), dt.Rows[j][8].ToString(), dt.Rows[j][9].ToString(), dt.Rows[j][10].ToString(), dt.Rows[j][11].ToString(), dt.Rows[j][12].ToString(), sub_eff.ToString("0.##") + "%", total_qty, piececount, total_eff.ToString("0.##") + "%");
                    }
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.ToString(), "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        public void QC_EMP_ROW_SELECTED()
        {
            try
            {
                String start = dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00";
                String end = dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59";

                dgvqc.Visible = true;
                radButton3.Text = "Report View";
                dgvqc.Rows.Clear();

                // get all the station
                for (int i = 0; i < dgvstation.Rows.Count; i++)
                {
                    //check if the station is selected
                    if ((bool)(dgvstation.Rows[i].Cells[0].Value) == true)
                    {
                        //get the repair details for the station
                        SqlDataAdapter da1 = new SqlDataAdapter("SELECT I_STATION_ID,V_MO_NO,V_MO_LINE, V_COLOR,V_ARTICLE,V_SIZE, V_OP_CODE, V_OP_DESC, V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC,sum(I_QUANTITY) FROM MRT_GLOBALDB.dbo.QC_HISTORY  where I_STATION_ID='" + dgvstation.Rows[i].Cells[1].Value.ToString() + "' and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and CONVERT(nvarchar(10), D_DATE_TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE') group by I_STATION_ID,V_MO_NO,V_MO_LINE, V_COLOR,V_ARTICLE,V_SIZE, V_OP_CODE, V_OP_DESC, V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC order by V_MO_NO,V_MO_LINE, V_QC_MAIN_CODE, V_QC_MAIN_DESC, V_QC_SUB_CODE, V_QC_SUB_DESC", dc.con);
                        DataTable dt = new DataTable();
                        da1.Fill(dt);
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            int piececount = 0;
                            int total_qty = 0;
                            decimal sub_eff = 0;
                            decimal total_eff = 0;

                            //get the station id
                            String[] stn = dgvstation.Rows[i].Cells[1].Value.ToString().Split('.');
                            String stationid = "";
                            SqlCommand cmd1 = new SqlCommand("select s.I_STN_ID from STATION_DATA s where s.I_INFEED_LINE_NO='" + stn[0] + "' and s.I_STN_NO_INFEED='" + stn[1] + "'", dc.con);
                            SqlDataReader sdr1 = cmd1.ExecuteReader();
                            if (sdr1.Read())
                            {
                                stationid = sdr1.GetValue(0).ToString();
                            }
                            sdr1.Close();

                            //get the production details
                            SqlDataAdapter da7 = new SqlDataAdapter("SELECT SUM(PC_COUNT) as HANGER_ID  FROM HANGER_HISTORY where STN_ID = '" + stationid + "'  and TIME>='" + start + "' and TIME<'" + end + "' and MO_NO='" + dt.Rows[j][1].ToString() + "' and MO_LINE='" + dt.Rows[j][2].ToString() + "' and CONVERT(nvarchar(10), TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
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

                            //get the sum of repair for the station
                            SqlDataAdapter da11 = new SqlDataAdapter("SELECT  SUM(I_QUANTITY) AS I_QUANTITY FROM MRT_GLOBALDB.dbo.QC_HISTORY QC_HISTORY where  I_STATION_ID = '" + dgvstation.Rows[i].Cells[1].Value.ToString() + "'  and D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "' and V_MO_NO='" + dt.Rows[j][1].ToString() + "' and V_MO_LINE='" + dt.Rows[j][2].ToString() + "' and CONVERT(nvarchar(10), D_DATE_TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
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

                            //calculate sub efficiency
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
                            dgvqc.Rows.Add(dgvstation.Rows[i].Cells[1].Value.ToString(), dt.Rows[j][1].ToString(), dt.Rows[j][2].ToString(), dt.Rows[j][3].ToString(), dt.Rows[j][4].ToString(), dt.Rows[j][5].ToString(), dt.Rows[j][6].ToString(), dt.Rows[j][7].ToString(), dt.Rows[j][8].ToString(), dt.Rows[j][9].ToString(), dt.Rows[j][10].ToString(), dt.Rows[j][11].ToString(), dt.Rows[j][12].ToString(), sub_eff.ToString("0.##") + "%", total_qty, piececount, total_eff.ToString("0.##") + "%");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex.ToString(), "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private void radCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            String start = dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00";
            String end = dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            emp.Rows.Clear();

            SqlDataAdapter da1 = new SqlDataAdapter("SELECT distinct I_STATION_ID from QC_HISTORY WHERE D_DATE_TIME>='" + start + "' and D_DATE_TIME<'" + end + "'  and CONVERT(nvarchar(10), D_DATE_TIME, 120) not in (SELECT CONVERT(nvarchar(10),D_HIDEDAY, 120) from HIDEDAY_DB where V_HIDE_ENABLE='TRUE')", dc.con);
            DataTable dt = new DataTable();
            da1.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                emp.Rows.Add(false, dt.Rows[i][0].ToString());
            }

            dgvstation.DataSource = emp;
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            //report button
            if (radButton3.Text == "Report View")
            {
                radButton2.PerformClick();
                radButton3.Text = "Table View";
                dgvqc.Visible = false;
            }
            else
            {
                dgvqc.Visible = true;
                radButton3.Text = "Report View";
            }
        }

        String theme = "";
        private void QC_STATION_Initialized(object sender, EventArgs e)
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
            dgvstation.ThemeName = theme;
            dgvqc.ThemeName = theme;
        }        

        private void QCGRID_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //get the station no
            if (e.RowIndex < 0)
            {
                return;
            }

            if ((bool)dgvstation.Rows[e.RowIndex].Cells[0].Value == true)
            {
                dgvstation.Rows[e.RowIndex].Cells[0].Value = false;
            }
            else
            {
                dgvstation.Rows[e.RowIndex].Cells[0].Value = true;
            }

            QC_EMP_ROW_SELECTED();   // calculate station qc report
        }

        private void dgvstation_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
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
    }
}
