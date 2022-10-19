using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Telerik.WinControls;
using Microsoft.Reporting.WinForms;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Production_Planning : Telerik.WinControls.UI.RadForm
    {
        public Production_Planning()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();   //connection class
        DataTable data1 = new DataTable();

        private void Production_Planning_Load(object sender, EventArgs e)
        {
            try
            {
                dgvsequence.MasterTemplate.SelectLastAddedRow = false;
                dgvsequence.MasterView.TableSearchRow.ShowCloseButton = false;   //disable close button on search in grid
                
                //get article id
                SqlDataAdapter sda = new SqlDataAdapter("Select V_ARTICLE_ID from ARTICLE_DB", dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbarticleid.Items.Add(dt.Rows[i][0].ToString());
                }

                DateTime shift_start = Convert.ToDateTime("9:30:00");
                DateTime shift_end = Convert.ToDateTime("18:30:00");

                DateTime current_time = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                String shift = "";
                int breaktime = 0;

                //get shift details
                sda = new SqlDataAdapter("SELECT T.T_SHIFT_START_TIME,T.T_SHIFT_END_TIME,T.T_OVERTIME_END_TIME,T.V_SHIFT FROM SHIFTS T WHERE CAST(GETDATE() AS TIME) BETWEEN cast(T.T_SHIFT_START_TIME as TIME) AND cast(T.T_OVERTIME_END_TIME as TIME)", dc.con);
                dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    shift_start = Convert.ToDateTime(dt.Rows[i][0].ToString());
                    shift_end = Convert.ToDateTime(dt.Rows[i][1].ToString());
                    shift = dt.Rows[i][3].ToString();
                }

                //get shift break complete time
                SqlCommand  cmd = new SqlCommand("select I_BREAK_TIMESPAN from SHIFT_BREAKS where V_SHIFT='" + shift + "'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    breaktime = breaktime + int.Parse(sdr.GetValue(0).ToString());
                }
                sdr.Close();

                //get workduration
                TimeSpan ts_workduration = shift_end - shift_start;
                int workduration = (int)ts_workduration.TotalMinutes;
                workduration = workduration - breaktime;
                txtworkdurarion.Text = workduration.ToString();

                data1.Columns.Add("seqno");
                data1.Columns.Add("opcode");
                data1.Columns.Add("opdesc");
                data1.Columns.Add("SAM");
                data1.Columns.Add("operators");
                data1.Columns.Add("Machine");
                data1.Columns.Add("station_unit");
                data1.Columns.Add("availabel");
                data1.Columns.Add("article");
                data1.Columns.Add("articledesc");
                data1.Columns.Add("totalprod");
                data1.Columns.Add("targetprod");
                data1.Columns.Add("duration");
                data1.Columns.Add("estimationdays");
                data1.Columns.Add("estimateunits");
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
            }
        }

        private void radLabel15_TextChanged(object sender, EventArgs e)
        {
            //hide the error message after 5 sec
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            panel2.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel15.Text = "";
            panel2.Visible = false;
            MyTimer.Stop();
        }

        String theme = "";
        private void Production_Planning_Initialized(object sender, EventArgs e)
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
            dgvsequence.ThemeName = theme;
        }


        private void cmbarticleid_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                //get article desc
                SqlDataAdapter sda = new SqlDataAdapter("Select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + cmbarticleid.Text + "'", dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    txtatricledesc.Text = dt.Rows[i][0].ToString();
                }
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
            }
        }

        private void btncalculate_Click(object sender, EventArgs e)
        {
            //check if the field inserted
            if (txttargetproduction.Text == "0" || txttotalproduction.Text == "0")
            {
                radLabel15.Text = "Invalid Total Production/Target Production value. Example : 5000";
                return;
            }

            try
            {
                //check if the total production is valid
                Regex r = new Regex("^[0-9]*$");
                if (!r.IsMatch(txttotalproduction.Text))
                {
                    radLabel15.Text = "Invalid Total Production value. Example : 5000";
                    txttotalproduction.Text = "";
                    return;
                }

                //check if target production is valid
                if (!r.IsMatch(txttargetproduction.Text))
                {
                    radLabel15.Text = "Invalid Target Production value. Example : 5000";
                    txttargetproduction.Text = "";
                    return;
                }

                dgvsequence.Rows.Clear();
                data1.Rows.Clear();
                reportViewer1.Visible = false;
                btnreport.Text = "Report View";

                //get all design sequence for the article
                SqlDataAdapter sda = new SqlDataAdapter("select ds.I_SEQUENCE_NO,ds.V_OPERATION_CODE,op.V_OPERATION_DESC,op.D_SAM,mc.V_MACHINE_DESC,count(mc.V_MACHINE_ID) from DESIGN_SEQUENCE ds,OPERATION_DB op,MACHINE_DB mc where ds.V_ARTICLE_ID='" + cmbarticleid.Text + "' and ds.V_OPERATION_CODE=op.V_OPERATION_CODE and op.V_MACHINE_ID=mc.V_MACHINE_ID  group by ds.I_SEQUENCE_NO,ds.V_OPERATION_CODE,op.V_OPERATION_DESC,op.D_SAM,mc.V_MACHINE_DESC order by ds.I_SEQUENCE_NO", dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    String seqno = dt.Rows[i][0].ToString();
                    String opcode = dt.Rows[i][1].ToString();
                    String opdesc = dt.Rows[i][2].ToString();
                    String machine = dt.Rows[i][4].ToString();
                    int sam = int.Parse(dt.Rows[i][3].ToString());

                    //calculate piece per day ,machines needed
                    decimal pieces_day = int.Parse(txtworkdurarion.Text) * 60 / sam;
                    decimal op = int.Parse(txttargetproduction.Text) / pieces_day;
                    decimal unit = decimal.Round(op);

                    if (unit == 0)
                    {
                        unit = 1;
                    }

                    int count = 0;

                    //get the available machines
                    SqlCommand cmd = new SqlCommand("select count(MD.V_MACHINE_SERIAL_NO) from MACHINE_DETAILS MD,MACHINE_DB MA where MD.V_MACHINE_ID=MA.V_MACHINE_ID and MD.V_STATUS!='REPAIR' and MA.V_MACHINE_DESC='" + machine + "'", dc.con);
                    count = int.Parse(cmd.ExecuteScalar() + "");  
                    
                    //add to grid
                    dgvsequence.Rows.Add(seqno, opcode, opdesc, sam, op.ToString("0.##"), machine, unit, count);                    
                }

                int total_sam = 0;
                decimal total_op = 0;
                int total_unit = 0;

                //get totals
                for (int i = 0; i < dgvsequence.Rows.Count; i++)
                {
                    total_sam = total_sam + int.Parse(dgvsequence.Rows[i].Cells[3].Value.ToString());
                    total_op = total_op + Convert.ToDecimal(dgvsequence.Rows[i].Cells[4].Value.ToString());
                    total_unit = total_unit + int.Parse(dgvsequence.Rows[i].Cells[6].Value.ToString());
                }

                //add to grid
                txtunitestimate.Text = total_unit.ToString();
                txtproductionestimate.Text = Convert.ToDecimal(int.Parse(txttotalproduction.Text) / int.Parse(txttargetproduction.Text)).ToString();
                dgvsequence.Rows.Add(" ", " ", "Total : ", total_sam, total_op, " ", total_unit," ");

                for (int i = 0; i < dgvsequence.Rows.Count; i++)
                {
                    data1.Rows.Add(dgvsequence.Rows[i].Cells[0].Value.ToString(), dgvsequence.Rows[i].Cells[1].Value.ToString(), dgvsequence.Rows[i].Cells[2].Value.ToString(), dgvsequence.Rows[i].Cells[3].Value.ToString(), dgvsequence.Rows[i].Cells[4].Value.ToString(), dgvsequence.Rows[i].Cells[5].Value.ToString(), dgvsequence.Rows[i].Cells[6].Value.ToString(), dgvsequence.Rows[i].Cells[7].Value.ToString(), cmbarticleid.Text, txtatricledesc.Text, txttotalproduction.Text, txttargetproduction.Text, txtworkdurarion.Text, txtproductionestimate.Text, txtunitestimate.Text);
                }
                                
                dgvsequence.Rows[dgvsequence.Rows.Count - 1].Cells[2].Style.ForeColor = Color.Red;
                dgvsequence.Rows[dgvsequence.Rows.Count - 1].Cells[3].Style.ForeColor = Color.Red;
                dgvsequence.Rows[dgvsequence.Rows.Count - 1].Cells[4].Style.ForeColor = Color.Red;
                dgvsequence.Rows[dgvsequence.Rows.Count - 1].Cells[6].Style.ForeColor = Color.Red;                
            }

            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
                MessageBox.Show(ex + "");
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //check if the report button is clicked
            if (btnreport.Text == "Report View")
            {
                reportViewer1.Visible = true;
                btnreport.Text = "Table View";
                DataView view = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Production_Planning.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                //add views to dataset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer1.RefreshReport();
            }
            else
            {
                reportViewer1.Visible = false;
                btnreport.Text = "Report View";
            }
        }

        private void dgvsequence_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvsequence.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvsequence.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvsequence.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvsequence.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }
    }
}
