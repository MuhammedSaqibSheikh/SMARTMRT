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
    public partial class Machine_Assigned_Report : Telerik.WinControls.UI.RadForm
    {
        public Machine_Assigned_Report()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();
        DataTable data1 = new DataTable();
        String theme = "";

        private void Machine_Assigned_Report_Load(object sender, EventArgs e)
        {
            dgvmachine.MasterTemplate.SelectLastAddedRow = false;
            dgvmachine.MasterView.TableSearchRow.ShowCloseButton = false;    //disable close button on search in grid

            //get all the prod line
            SqlDataAdapter sda = new SqlDataAdapter("select distinct V_PROD_LINE from PROD_LINE_DB", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            cmbline.Items.Add("All");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbline.Items.Add(dt.Rows[i][0].ToString());
            }

            //get all the machines
            sda = new SqlDataAdapter("select distinct V_MACHINE_DESC from MACHINE_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            cmbmachine.Items.Add("All");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbmachine.Items.Add(dt.Rows[i][0].ToString());
            }

            data1.Columns.Add("line");
            data1.Columns.Add("machine");
            data1.Columns.Add("machine_id");
            data1.Columns.Add("machine_desc");
            data1.Columns.Add("model");
            data1.Columns.Add("serialno");
            data1.Columns.Add("date");
            data1.Columns.Add("stationNo");

            Machine_Assign();    //get machine assign
        }

        public void Machine_Assign()
        {
            data1.Rows.Clear();
            String query = "";
            //check if all lines and all machines are selected
            if (cmbline.Text == "All" && cmbmachine.Text == "All")
            {
                query = "select m.V_MACHINE_ID,m.V_MACHINE_DESC,m.V_MODEL,md.V_MACHINE_SERIAL_NO,md.D_PURCHASE_DATE,ma.V_STATION_ID from MACHINE_ASSIGN ma,MACHINE_DB m,MACHINE_DETAILS md where ma.V_MACHINE_ID=md.V_MACHINE_ID and ma.V_MACHINE_ID=m.V_MACHINE_ID and ma.V_MACHINE_SERIAL_NO=md.V_MACHINE_SERIAL_NO ";
            }
            //check if only all lines is selected
            else if (cmbline.Text == "All")
            {
                query = "select m.V_MACHINE_ID,m.V_MACHINE_DESC,m.V_MODEL,md.V_MACHINE_SERIAL_NO,md.D_PURCHASE_DATE,ma.V_STATION_ID from MACHINE_ASSIGN ma,MACHINE_DB m,MACHINE_DETAILS md where ma.V_MACHINE_ID=md.V_MACHINE_ID and ma.V_MACHINE_ID=m.V_MACHINE_ID and ma.V_MACHINE_SERIAL_NO=md.V_MACHINE_SERIAL_NO and m.V_MACHINE_DESC='" + cmbmachine.Text + "'";
            }
            //check if only all machines is selected
            else if (cmbmachine.Text == "All")
            {
                query = "select m.V_MACHINE_ID,m.V_MACHINE_DESC,m.V_MODEL,md.V_MACHINE_SERIAL_NO,md.D_PURCHASE_DATE,ma.V_STATION_ID from MACHINE_ASSIGN ma,MACHINE_DB m,MACHINE_DETAILS md where ma.V_MACHINE_ID=md.V_MACHINE_ID and ma.V_MACHINE_ID=m.V_MACHINE_ID and ma.V_MACHINE_SERIAL_NO=md.V_MACHINE_SERIAL_NO  and ma.V_STATION_ID LIKE'" + cmbline.Text + ".%'";
            }
            else
            {
                query = "select m.V_MACHINE_ID,m.V_MACHINE_DESC,m.V_MODEL,md.V_MACHINE_SERIAL_NO,md.D_PURCHASE_DATE,ma.V_STATION_ID from MACHINE_ASSIGN ma,MACHINE_DB m,MACHINE_DETAILS md where ma.V_MACHINE_ID=md.V_MACHINE_ID and ma.V_MACHINE_ID=m.V_MACHINE_ID and ma.V_MACHINE_SERIAL_NO=md.V_MACHINE_SERIAL_NO and m.V_MACHINE_DESC='" + cmbmachine.Text + "' and ma.V_STATION_ID LIKE'" + cmbline.Text + ".%' ";
            }

            dgvmachine.Rows.Clear();

            //get machine assign details
            SqlDataAdapter sda = new SqlDataAdapter(query, dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //add to grid
                dgvmachine.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
                data1.Rows.Add(cmbline.Text, cmbmachine.Text, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString());
            }
        }
        
        private void Machine_Assigned_Report_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();    //open connection

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
            Machine_Assign();   //get machine assign details
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

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.machine_assign.rdlc";
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
            //change grid fore color is these themes are selected
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
