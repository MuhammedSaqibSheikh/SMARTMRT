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
    public partial class Machine_Repair_Report : Telerik.WinControls.UI.RadForm
    {
        public Machine_Repair_Report()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();   //connection class
        DataTable data1 = new DataTable();
        DataTable data2 = new DataTable();
        DataTable data3 = new DataTable();

        private void Machine_Repair_Report_Load(object sender, EventArgs e)
        {
            dgvmachine.MasterTemplate.SelectLastAddedRow = false;
            dgvmachine.MasterView.TableSearchRow.ShowCloseButton = false;   //disable close button on search in grid

            data1.Columns.Add("date");
            data1.Columns.Add("machine");
            data1.Columns.Add("machine_id");
            data1.Columns.Add("machine_desc");
            data1.Columns.Add("model");
            data1.Columns.Add("serialno");
            data1.Columns.Add("p_date");
            data1.Columns.Add("repairmainid");
            data1.Columns.Add("repair_main_desc");
            data1.Columns.Add("repairsub_id");
            data1.Columns.Add("sub_desc");
            data1.Columns.Add("datek");

            data3.Columns.Add("MACHINE_DESC");
            data3.Columns.Add("REPAIR_MAIN_ID");
            data3.Columns.Add("REPAIR_SUB_ID");

            data2.Columns.Add("MACHINE_DESC");
            data2.Columns.Add("MACHINE_REPAIR_COUNT");

            dtpstart.Text = DateTime.Now.ToString();
            dtpend.Text = DateTime.Now.ToString();

            //get all the machine
            SqlDataAdapter sda = new SqlDataAdapter("select distinct V_MACHINE_DESC from MACHINE_DB", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            cmbmachine.Items.Add("All");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbmachine.Items.Add(dt.Rows[i][0].ToString());
            }

            //get all machine details
            sda = new SqlDataAdapter("select count(m.V_MACHINE_DESC) as MACHINE_REPAIR_COUNT , m.V_MACHINE_DESC  from MACHINE_BREAKDOWN_HISTORY mb,MACHINE_DB m,MACHINE_DETAILS md where  mb.V_MACHINE_ID=md.V_MACHINE_ID and mb.V_MACHINE_ID=m.V_MACHINE_ID and mb.V_MACHINE_SERIAL_NO=md.V_MACHINE_SERIAL_NO   group by m.V_MACHINE_DESC", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data2.Rows.Add(dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString());
            }

            //get repair detials
            sda = new SqlDataAdapter("select count(mb.V_MB_MAIN_ID) as V_MB_MAIN_ID ,count(mb.V_MB_SUB_ID) as V_MB_SUB_ID, m.V_MACHINE_DESC  from MACHINE_BREAKDOWN_HISTORY mb,MACHINE_DB m,MACHINE_DETAILS md where  mb.V_MACHINE_ID=md.V_MACHINE_ID and mb.V_MACHINE_ID=m.V_MACHINE_ID and mb.V_MACHINE_SERIAL_NO=md.V_MACHINE_SERIAL_NO   group by m.V_MACHINE_DESC", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data3.Rows.Add(dt.Rows[i][2].ToString(), dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());
            }

            Machine_Assign();    //get machine repiar
        }

        public void Machine_Assign()
        {
            String start = dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00";
            String end = dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59";

            data1.Rows.Clear();
            String query = "";

            reportViewer1.Visible = false;
            btnreport.Text = "Report View";

            //check if all dates is selected
            if (chkalldates.Checked == true)
            {
                dgvmachine.Columns[9].IsVisible = true;

                //check if all machine is selected
                if (cmbmachine.Text == "All")
                {
                    query = "select m.V_MACHINE_ID,m.V_MACHINE_DESC,m.V_MODEL,md.V_MACHINE_SERIAL_NO,md.D_PURCHASE_DATE,mb.V_MB_MAIN_ID,mb.V_MB_MAIN_DESC,mb.V_MB_SUB_ID,mb.V_MB_SUB_DESC,mb.D_DATETIME from MACHINE_BREAKDOWN_HISTORY mb,MACHINE_DB m,MACHINE_DETAILS md where mb.V_MACHINE_ID=md.V_MACHINE_ID and mb.V_MACHINE_ID=m.V_MACHINE_ID and mb.V_MACHINE_SERIAL_NO=md.V_MACHINE_SERIAL_NO";
                }
                else
                {
                    query = "select m.V_MACHINE_ID,m.V_MACHINE_DESC,m.V_MODEL,md.V_MACHINE_SERIAL_NO,md.D_PURCHASE_DATE,mb.V_MB_MAIN_ID,mb.V_MB_MAIN_DESC,mb.V_MB_SUB_ID,mb.V_MB_SUB_DESC,mb.D_DATETIME from MACHINE_BREAKDOWN_HISTORY mb,MACHINE_DB m,MACHINE_DETAILS md where mb.V_MACHINE_ID=md.V_MACHINE_ID and mb.V_MACHINE_ID=m.V_MACHINE_ID and mb.V_MACHINE_SERIAL_NO=md.V_MACHINE_SERIAL_NO and m.V_MACHINE_DESC='" + cmbmachine.Text + "'";
                }
                dgvmachine.Rows.Clear();

                //get machine repair details
                SqlDataAdapter sda = new SqlDataAdapter(query, dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //add to grid
                    dgvmachine.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString(), dt.Rows[i][7].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][9].ToString());
                    data1.Rows.Add(dtpstart.Value, cmbmachine.Text, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString(), dt.Rows[i][7].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][9].ToString());
                }
            }
            else
            {
                dgvmachine.Columns[9].IsVisible = false;
                //check if all machine is selected
                if (cmbmachine.Text == "All")
                {
                    query = "select m.V_MACHINE_ID,m.V_MACHINE_DESC,m.V_MODEL,md.V_MACHINE_SERIAL_NO,md.D_PURCHASE_DATE,mb.V_MB_MAIN_ID,mb.V_MB_MAIN_DESC,mb.V_MB_SUB_ID,mb.V_MB_SUB_DESC from MACHINE_BREAKDOWN_HISTORY mb,MACHINE_DB m,MACHINE_DETAILS md where mb.V_MACHINE_ID=md.V_MACHINE_ID and mb.V_MACHINE_ID=m.V_MACHINE_ID and mb.V_MACHINE_SERIAL_NO=md.V_MACHINE_SERIAL_NO and mb.D_DATETIME>='" + start + "' and mb.D_DATETIME<'" + end + "'";
                }
                else
                {
                    query = "select m.V_MACHINE_ID,m.V_MACHINE_DESC,m.V_MODEL,md.V_MACHINE_SERIAL_NO,md.D_PURCHASE_DATE,mb.V_MB_MAIN_ID,mb.V_MB_MAIN_DESC,mb.V_MB_SUB_ID,mb.V_MB_SUB_DESC from MACHINE_BREAKDOWN_HISTORY mb,MACHINE_DB m,MACHINE_DETAILS md where mb.V_MACHINE_ID=md.V_MACHINE_ID and mb.V_MACHINE_ID=m.V_MACHINE_ID and mb.V_MACHINE_SERIAL_NO=md.V_MACHINE_SERIAL_NO and m.V_MACHINE_DESC='" + cmbmachine.Text + "' and mb.D_DATETIME>='" + start + "' and mb.D_DATETIME<'" + end + "'";
                }

                dgvmachine.Rows.Clear();

                //get machine repair details
                SqlDataAdapter sda = new SqlDataAdapter(query, dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //add to grid
                    dgvmachine.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString(), dt.Rows[i][7].ToString(), dt.Rows[i][8].ToString(), "");
                    data1.Rows.Add(dtpstart.Value, cmbmachine.Text, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString(), dt.Rows[i][7].ToString(), dt.Rows[i][8].ToString(), dtpstart.Value);
                }
            }
            
        }

        String theme = "";

        private void Machine_Repair_Report_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();     //open connection

            String Lang = "";
            //get language and theme
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
            dgvmachine.ThemeName = theme;
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            reportViewer1.Visible = false;
            btnreport.Text = "Report View";
            Machine_Assign();    //get machine repair details
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            //check if report button is clicked
            if (btnreport.Text == "Report View")
            {
                reportViewer1.Visible = true;
                DataView view = new DataView(data1);
                DataView view1 = new DataView(data2);
                DataView view2 = new DataView(data3);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.machine_repair.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                //add views to dataset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", view1));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", view2));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", dv_image));

                reportViewer1.RefreshReport();
                btnreport.Text = "Table View";
            }
            else if (btnreport.Text == "Table View")
            {
                reportViewer1.Visible = false;
                btnreport.Text = "Report View";
            }
        }

        private void cmbmachine_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            String start = dtpstart.Value.ToString("yyyy-MM-dd") + " 00:00:00";
            String end = dtpend.Value.ToString("yyyy-MM-dd") + " 23:59:59";
            data2.Rows.Clear();

            //get remaining machine details
            SqlDataAdapter sda1 = new SqlDataAdapter("select count(mb.V_MB_MAIN_ID) as VAL1 ,count(mb.V_MB_SUB_ID) as VAL2 ,  m.V_MACHINE_DESC from MACHINE_BREAKDOWN_HISTORY mb,MACHINE_DB m,MACHINE_DETAILS md where m.V_MACHINE_DESC='" + cmbmachine.Text + "' and mb.V_MACHINE_ID=md.V_MACHINE_ID and mb.V_MACHINE_ID=m.V_MACHINE_ID and mb.V_MACHINE_SERIAL_NO=md.V_MACHINE_SERIAL_NO and  mb.D_DATETIME>='" + start + "' and mb.D_DATETIME<'" + end + "' group by m.V_MACHINE_DESC", dc.con);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            sda1.Dispose();
            for (int j = 0; j < dt1.Rows.Count; j++)
            {
                int total = Int32.Parse(dt1.Rows[j][0].ToString()) + Int32.Parse(dt1.Rows[j][1].ToString());
                data2.Rows.Add(dt1.Rows[j][2].ToString(), total);
            }
        }

        private void chkalldates_CheckStateChanged(object sender, EventArgs e)
        {
            Machine_Assign();   //get machine repair details
        }

        private void dgvmachine_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //change grid fore color if these themes are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvmachine.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvmachine.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvmachine.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvmachine.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }
    }
}
