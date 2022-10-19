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
    public partial class Spare_Report : Telerik.WinControls.UI.RadForm
    {
        public Spare_Report()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();   //connection class
        DataTable data1 = new DataTable();
        String theme = "";

        private void Spare_Report_Load(object sender, EventArgs e)
        {
            dgvmachine.MasterTemplate.SelectLastAddedRow = false;
            dgvmachine.MasterView.TableSearchRow.ShowCloseButton = false;   //disable close button of search in grid

            //get desc
            SqlDataAdapter sda = new SqlDataAdapter("select distinct V_SPARE_MAIN_DESC from SPARE_MAIN_CATEGORY", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            cmbmachine.Items.Add("All");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbmachine.Items.Add(dt.Rows[i][0].ToString());
            }

            data1.Columns.Add("spare_main_desc");
            data1.Columns.Add("spare_sub_id");
            data1.Columns.Add("spare_sub_desc");
            data1.Columns.Add("quantity");
            data1.Columns.Add("machine_id");
            data1.Columns.Add("model");
            data1.Columns.Add("serialno");
            data1.Columns.Add("used");
            data1.Columns.Add("balance");
            data1.Columns.Add("stationNo");

            Machine_Spare();  // get spare details
        }

        public void Machine_Spare()
        {
            data1.Rows.Clear();
            dgvmachine.Rows.Clear();

            //check if all machine is selected
            String query = "";
            if (cmbmachine.Text == "All")
            {
                query = "select a.V_SPARE_SUB_ID,s.V_SPARE_SUB_DESC,s.I_QUANTITY,a.V_MACHINE_ID,m.V_MODEL,a.V_MACHINE_SERIAL_NO,a.I_QUANTITY from SPARE_SUB_CATEGORY s,SPARE_ASSIGN a,MACHINE_DB m where s.V_SPARE_MAIN_ID=a.V_SPARE_MAIN_ID and s.V_SPARE_SUB_ID=a.V_SPARE_SUB_ID and a.V_MACHINE_ID=m.V_MACHINE_ID";
            }
            else
            {
                query = "select a.V_SPARE_SUB_ID,s.V_SPARE_SUB_DESC,s.I_QUANTITY,a.V_MACHINE_ID,m.V_MODEL,a.V_MACHINE_SERIAL_NO,a.I_QUANTITY from SPARE_SUB_CATEGORY s, SPARE_MAIN_CATEGORY sm,SPARE_ASSIGN a,MACHINE_DB m where s.V_SPARE_MAIN_ID=a.V_SPARE_MAIN_ID and s.V_SPARE_SUB_ID=a.V_SPARE_SUB_ID and a.V_MACHINE_ID=m.V_MACHINE_ID and sm.V_SPARE_MAIN_DESC='" + cmbmachine.Text + "'";
            }

            // get spare details
            SqlDataAdapter sda = new SqlDataAdapter(query, dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String spareid = dt.Rows[i][0].ToString();
                String sparedesc = dt.Rows[i][1].ToString();
                int quantity = int.Parse(dt.Rows[i][2].ToString());
                String machine_id = dt.Rows[i][3].ToString();
                String model = dt.Rows[i][4].ToString();
                String serial_no = dt.Rows[i][5].ToString();
                int used = int.Parse(dt.Rows[i][6].ToString());

                int balance = 0;
                String station_id = "Not Assigned";

                //get used count
                int total_used = 0;
                SqlCommand cmd = new SqlCommand("select sum(I_QUANTITY) from SPARE_ASSIGN where V_SPARE_SUB_ID='" + spareid + "'", dc.con);
                String temp = cmd.ExecuteScalar() + "";
                if (temp != "")
                {
                    total_used = int.Parse(temp);
                }

                //calculate balance
                balance = quantity - total_used;

                //get station id
                sda = new SqlDataAdapter("select V_STATION_ID from MACHINE_ASSIGN where V_MACHINE_ID='" + machine_id + "' and V_MACHINE_SERIAL_NO='" + serial_no + "'", dc.con);
                DataTable dt2 = new DataTable();
                sda.Fill(dt2);
                sda.Dispose();
                for (int k = 0; k < dt2.Rows.Count; k++)
                {
                    station_id = dt2.Rows[k][0].ToString();
                }

                dgvmachine.Rows.Add(spareid, sparedesc, quantity, machine_id, model, serial_no, used, balance, station_id);
                data1.Rows.Add(cmbmachine.Text, spareid, sparedesc, quantity, machine_id, model, serial_no, used, balance, station_id);
            }
        }

        private void Spare_Report_Initialized(object sender, EventArgs e)
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
            dgvmachine.ThemeName = theme;
        }

        private void cmbmachine_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Machine_Spare();   // get spare details
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            Machine_Spare();    // get spare details
        }

        private void dgvmachine_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
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

        private void btnreport_Click(object sender, EventArgs e)
        {
            if (btnreport.Text == "Report View")
            {
                reportViewer1.Visible = true;
                DataView view = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.SpareReport.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

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
    }
}
