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
    public partial class Machine_Report : Telerik.WinControls.UI.RadForm
    {
        public Machine_Report()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();    //connection class
        DataTable data1 = new DataTable();

        private void Machine_Report_Load(object sender, EventArgs e)
        {
            dgvmachine.MasterTemplate.SelectLastAddedRow = false;
            dgvmachine.MasterView.TableSearchRow.ShowCloseButton = false;   //disable close button on search in grid

            //get all the machines
            SqlDataAdapter sda = new SqlDataAdapter("select distinct V_MACHINE_DESC from MACHINE_DB", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            cmbmachine.Items.Add("All");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbmachine.Items.Add(dt.Rows[i][0].ToString());
            }

            data1.Columns.Add("machine");
            data1.Columns.Add("machine_id");
            data1.Columns.Add("machine_desc");
            data1.Columns.Add("model");
            data1.Columns.Add("available");
            data1.Columns.Add("inuse");
            data1.Columns.Add("balance");
            data1.Columns.Add("repair");

            Machine_Assign();    //get machine report
        }

        public void Machine_Assign()
        {
            data1.Rows.Clear();

            //check if all machines is selected
            String query = "";
            if (cmbmachine.Text == "All")
            {
                query = "select m.V_MACHINE_ID,m.V_MACHINE_DESC,m.V_MODEL from MACHINE_DB m";
            }
            else
            {
                query = "select m.V_MACHINE_ID,m.V_MACHINE_DESC,m.V_MODEL from MACHINE_DB m where m.V_MACHINE_DESC='" + cmbmachine.Text + "'";
            }

            dgvmachine.Rows.Clear();

            //get machine report
            SqlDataAdapter sda = new SqlDataAdapter(query, dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //get machine count which are not under repair
                SqlCommand cmd3 = new SqlCommand("select count(V_STATUS) from MACHINE_DETAILS MD where MD.V_MACHINE_ID='" + dt.Rows[i][0].ToString() + "' and  MD.V_STATUS!='REPAIR'", dc.con);
                int total = int.Parse(cmd3.ExecuteScalar().ToString());

                //get machine inuse count
                cmd3 = new SqlCommand("select count(V_STATUS) from MACHINE_DETAILS MD where MD.V_MACHINE_ID='" + dt.Rows[i][0].ToString() + "' and MD.V_STATUS='TRUE'", dc.con);
                int inuse = int.Parse(cmd3.ExecuteScalar().ToString());

                //get machine repair count
                cmd3 = new SqlCommand("select count(V_STATUS) from MACHINE_DETAILS MD where MD.V_MACHINE_ID='" + dt.Rows[i][0].ToString() + "' and MD.V_STATUS='REPAIR'", dc.con);
                int repair = int.Parse(cmd3.ExecuteScalar().ToString());

                //get balance machine
                int balance = total - inuse;

                //add to grid
                dgvmachine.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), total, inuse, balance, repair);
                data1.Rows.Add(cmbmachine.Text, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), total, inuse, balance, repair);
            }
        }

        String theme = "";
        private void Machine_Report_Initialized(object sender, EventArgs e)
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

        //get grid theme
        public void GridTheme(String theme)
        {
            dgvmachine.ThemeName = theme;
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            btnreport.Text = "Report View";
            reportViewer1.Visible = false;
            Machine_Assign();   //get machine report
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            //check if report button is clicked
            if (btnreport.Text == "Report View")
            {
                reportViewer1.Visible = true;
                DataView view = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.machine_report.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                //add views to dataset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer1.RefreshReport();
                btnreport.Text = "Table View";
            }
            else if (btnreport.Text == "Table View")
            {
                reportViewer1.Visible = false;
                btnreport.Text = "Report View";
            }
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
