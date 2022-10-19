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
    public partial class Edit_Records : Telerik.WinControls.UI.RadForm
    {
        public Edit_Records()
        {
            InitializeComponent();
        }

        String empid = "";
        String empname = "";
        String id = "";
        Database_Connection dc = new Database_Connection();   //connection class

        private void Edit_Records_Load(object sender, EventArgs e)
        {
            dgveditrecords.MasterTemplate.SelectLastAddedRow = false;
            dgveditrecords.MasterView.TableSearchRow.ShowCloseButton = false;   //disable close button for search in grid
            this.CenterToScreen();
            txtempid.Text = empid;
            txtempname.Text = empname;

            //get all shifts
            SqlDataAdapter sda = new SqlDataAdapter("select V_SHIFT from SHIFTS", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbshift.Items.Add(dt.Rows[i][0].ToString());
            }

            //get all mo
            sda = new SqlDataAdapter("select V_MO_NO from MO", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbmono.Items.Add(dt.Rows[i][0].ToString());
            }

            RefereshGrid();   //refresh grid
            dtpdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }

        //get selected employee
        public void getData(String id, String name)
        {
            empid = id;
            empname = name;
        }

        private void radLabel1_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            panel1.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel1.Text = "";
            panel1.Visible = false;
            MyTimer.Stop();
        }

        String theme = "";
        private void Edit_Records_Initialized(object sender, EventArgs e)
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

            //chenge grid theme
            GridTheme(theme);
        }

        //set grid theme
        public void GridTheme(String theme)
        {
            dgveditrecords.ThemeName = theme;
        }

        private void cmbmono_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            cmbmoline.Items.Clear();
            cmboperation.Items.Clear();
            cmbmoline.Text = "--SELECT--";
            cmboperation.Text = "--SELECT--";

            //get all the mo details for the mo
            SqlDataAdapter sda = new SqlDataAdapter("select V_MO_LINE from MO_DETAILS where V_MO_NO='" + cmbmono.Text + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbmoline.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        private void cmbmoline_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            cmboperation.Items.Clear();
            cmboperation.Text = "--SELECT--";
            String article = "";

            //get the article id for the mo
            SqlDataAdapter sda = new SqlDataAdapter("select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + cmbmono.Text + "' and V_MO_LINE='" + cmbmoline.Text + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                article = dt.Rows[i][0].ToString();
            }

            //get all the operation for the article
            sda = new SqlDataAdapter("select OP.V_OPERATION_DESC from DESIGN_SEQUENCE DS,OPERATION_DB OP where DS.V_OPERATION_CODE=OP.V_OPERATION_CODE  and DS.V_ARTICLE_ID='" + article + "'", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmboperation.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        public void RefereshGrid()
        {
            dgveditrecords.Rows.Clear();

            //get edit records for the employee
            SqlDataAdapter da = new SqlDataAdapter("SELECT E.V_ID,E.V_MO_NO,E.V_MO_LINE,OP.V_OPERATION_DESC,E.D_DATETIME,E.V_SHIFT,E.I_PIECE_COUNT from EDIT_RECORDS E,OPERATION_DB OP where E.V_EMP_ID='" + empid + "' and OP.V_OPERATION_CODE=E.V_OPERATION_CODE", dc.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgveditrecords.Rows.Add(dt.Rows[i][0].ToString(),dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(), dt.Rows[i][6].ToString());
            }

            dgveditrecords.Columns[0].IsVisible = false;
            dgveditrecords.Visible = false;

            if (dgveditrecords.Rows.Count > 0)
            {
                dgveditrecords.Visible = true;
            }
        }

        private void btnedit_Click(object sender, EventArgs e)
        {
            RowSelected();   //get the selected edit records
        }

        public void RowSelected()
        {
            if (dgveditrecords.SelectedRows.Count > 0)
            {
                id = dgveditrecords.SelectedRows[0].Cells[0].Value + string.Empty;
                cmbmono.Text = dgveditrecords.SelectedRows[0].Cells[1].Value + string.Empty;
                cmbmoline.Text = dgveditrecords.SelectedRows[0].Cells[2].Value + string.Empty;
                cmboperation.Text = dgveditrecords.SelectedRows[0].Cells[3].Value + string.Empty;
                dtpdate.Text = dgveditrecords.SelectedRows[0].Cells[4].Value + string.Empty;
                cmbshift.Text = dgveditrecords.SelectedRows[0].Cells[5].Value + string.Empty;
                txttotalpiece.Text = dgveditrecords.SelectedRows[0].Cells[6].Value + string.Empty;
                btnsave.Text = "Update";
                btndelete.Enabled = true;
            }
        }

        private void dgveditrecords_DoubleClick(object sender, EventArgs e)
        {
            RowSelected();  //get the selected edit records
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            //delete edit records
            SqlCommand cmd = new SqlCommand("delete from EDIT_RECORDS where V_ID='" + id + "'", dc.con);
            cmd.ExecuteNonQuery();

            radLabel1.Text = "Records Deleted";
            RefereshGrid();

            btndelete.Enabled = false;
            btnsave.Text = "Save";
            ClearAll();    //clear the fields
        }

        //clear all the fields
        public void ClearAll()
        {
            cmbmono.Text = "--SELECT--";
            cmbmoline.Items.Clear();
            cmboperation.Items.Clear();
            cmbmoline.Text = "--SELECT--";
            cmboperation.Text = "--SELECT--";
            cmbshift.Text = "--SELECT--";
            txttotalpiece.Text = "";
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbmono.Text != "--SELECT--" && cmbmoline.Text != "--SELECT--" && cmboperation.Text != "--SELECT--" & cmbshift.Text != "--SELECT--" && txttotalpiece.Text != "")
                {
                    String article = "";

                    //get the operation id
                    SqlDataAdapter sda = new SqlDataAdapter("select V_OPERATION_CODE from OPERATION_DB where V_OPERATION_DESC='" + cmboperation.Text + "'", dc.con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        article = dt.Rows[i][0].ToString();
                    }

                    btndelete.Enabled = false;

                    if (btnsave.Text == "Save")
                    {
                        //insert into edit records
                        SqlCommand cmd = new SqlCommand("insert into EDIT_RECORDS values('" + cmbmono.Text + "','" + cmbmoline.Text + "','" + article + "','" + txttotalpiece.Text + "','" + dtpdate.Value.ToString("yyyy-MM-dd") + "','" + txtempid.Text + "','" + cmbshift.Text + "')", dc.con);
                        cmd.ExecuteNonQuery();

                        radLabel1.Text = "Records Saved";

                        RefereshGrid();   //refresh grid
                        ClearAll();     //clear fields
                    }

                    if (btnsave.Text == "Update")
                    {
                        //update edit records
                        SqlCommand cmd = new SqlCommand("Update EDIT_RECORDS set V_MO_NO='" + cmbmono.Text + "',V_MO_LINE='" + cmbmoline.Text + "',V_OPERATION_CODE='" + article + "',I_PIECE_COUNT='" + txttotalpiece.Text + "',D_DATETIME='" + dtpdate.Value.ToString("yyyy-MM-dd") + "',V_SHIFT='" + cmbshift.Text + "' where V_ID='" + id + "'", dc.con);
                        cmd.ExecuteNonQuery();

                        radLabel1.Text = "Records Updated";
                        btnsave.Text = "Save";

                        RefereshGrid();    //refresh grid                    
                        ClearAll();    //clear fields
                    }

                    id = "";
                }
                else
                {
                    radLabel1.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                radLabel1.Text = ex.Message;
            }
        }

        private void dgveditrecords_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //change fore color for grid if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgveditrecords.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgveditrecords.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgveditrecords.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgveditrecords.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }
    }
}
