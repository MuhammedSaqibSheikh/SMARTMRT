using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SMARTMRT
{
    public partial class Buffer_Sorting : Telerik.WinControls.UI.RadForm
    {       
        public Buffer_Sorting()
        {
            InitializeComponent();            
        }
        Database_Connection dc = new Database_Connection();  //connection class
        String theme = "";  

        private void Buffer_Sorting_Load(object sender, EventArgs e)
        {
            dgvbufferout.MasterTemplate.SelectLastAddedRow = false;
            dgvbufferout.MasterView.TableSearchRow.ShowCloseButton = false;  //disable the close buttons for search in grid
            this.CenterToScreen();  //keep form centered to screen

            radPanel2.Visible = false;
            dc.OpenConnection();   //open connection

            dgvbufferout.Columns[4].IsVisible = false;
            
            //make column read only
            dgvbufferout.Columns[0].ReadOnly = true;
            dgvbufferout.Columns[1].ReadOnly = true;
            dgvbufferout.Columns[2].ReadOnly = true;

            //get the mo details and hangers inthr buffer
            SqlDataAdapter sda = new SqlDataAdapter("Select V_MO_NO,V_MO_LINE,I_HANGER_COUNT,I_STATION_ID from SORT_CALL_OUT_SEQUENCE", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvbufferout.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), "1", dt.Rows[i][3].ToString());
            }
        }       

        private void radLabel8_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            radPanel2.Visible = true;
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel8.Text = "";
            radPanel2.Visible = false;
            MyTimer.Stop();
        }       

        private void btnmoveup_Click(object sender, EventArgs e)
        {

        }

        private void btnmovedown_Click(object sender, EventArgs e)
        {

        }

        private void btnbuffercallout_Click(object sender, EventArgs e)
        {
            //check if input quantity is integer
            Regex r = new Regex("^[0-9]*$");
            if (!(r.IsMatch(txtquantity.Text)) || txtquantity.Text == "0" || txtquantity.Text == "")
            {
                radLabel8.Text = "Invalid Piece Count";
                return;
            }

            //get all the buffer details
            for (int i = 0; i < dgvbufferout.Rows.Count; i++)
            {
                String MO = dgvbufferout.Rows[i].Cells[0].Value.ToString();
                String MOLINE = dgvbufferout.Rows[i].Cells[1].Value.ToString();
                int hanger = int.Parse(dgvbufferout.Rows[i].Cells[2].Value.ToString());
                int count = int.Parse(dgvbufferout.Rows[i].Cells[3].Value.ToString());
                int qty = int.Parse(txtquantity.Text);

                //check if buffer has enough hangers
                if (count * qty > hanger)
                {
                    radLabel8.Text = "Not Enough Hangers for the "+MO + "-" + MOLINE + " in the Buffer.";
                    dgvbufferout.Rows[i].IsSelected = true;

                    return;
                }
            }

            //delete the prevoius records
            SqlCommand cmd = new SqlCommand("Delete from SORT_CALL_OUT_SEQUENCE", dc.con);
            cmd.ExecuteNonQuery();

            //insert the new sort sequence
            for (int i = 0; i < dgvbufferout.Rows.Count; i++)
            {
                cmd = new SqlCommand("insert into SORT_CALL_OUT_SEQUENCE values('" + dgvbufferout.Rows[i].Cells[0].Value.ToString() + "','" + dgvbufferout.Rows[i].Cells[1].Value.ToString() + "','" + dgvbufferout.Rows[i].Cells[2].Value.ToString() + "','" + dgvbufferout.Rows[i].Cells[3].Value.ToString() + "','" + txtquantity.Text + "','" + dgvbufferout.Rows[i].Cells[4].Value.ToString() + "')", dc.con);
                cmd.ExecuteNonQuery();
            }

            radLabel8.Text = "Sort Sequence is Complete";
        }

        private void txtquantity_KeyDown(object sender, KeyEventArgs e)
        {
            //enter key press event
            if (e.KeyCode == Keys.Enter)
            {
                btnbuffercallout.PerformClick();
            }
        }

        private void Buffer_Sorting_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();  //open connection

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

            //set language for the form
            SqlDataAdapter sda = new SqlDataAdapter("select " + Lang + " from Language where Form='Buffer_Sort' order by Item_No", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                btnbuffercallout.Text = dt.Rows[0][0].ToString();
                lblqty.Text= dt.Rows[1][0].ToString();
            }

            //change grid theme
            GridTheme(theme);
        }

        //set grid theme
        public void GridTheme(String theme)
        {
            dgvbufferout.ThemeName = theme;
        }

        private void dgvbufferout_ViewCellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //change the fore color of grids of these theme is selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvbufferout.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvbufferout.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvbufferout.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvbufferout.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }
    }
}
