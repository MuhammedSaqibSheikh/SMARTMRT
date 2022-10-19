using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Open_MO : Telerik.WinControls.UI.RadForm
    {
        public Open_MO()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }

        Database_Connection dc = new Database_Connection();    //connection class
        DataTable MO = new DataTable();
        String theme = "";

        private void radButton1_Click(object sender, EventArgs e)
        {
            //search mo and get mo details
            for (int i = 0; i < dgvmo.Rows.Count; i++)
            {
                if (dgvmo.Rows[i].Cells[0].Value.ToString().Equals(txtmo.Text))
                {
                    dgvmo.Rows[i].IsSelected = true;
                    btneditmo.Enabled = true;
                    btndeletemo.Enabled = true;
                    btnstnassign.Enabled = true;

                    RowSelected();   //get selected mo 
                    MOSelected();    //get mo details

                    dgvmoline.Visible = true;
                    break;
                }
                else
                {
                    MO.Rows.Clear();
                    dgvmoline.DataSource = MO;
                    dgvmoline.Visible = false;
                    btneditmo.Enabled = false;
                    btndeletemo.Enabled = false;
                    btnstnassign.Enabled = false;
                }
            }
        }

        private void Open_MO_Load(object sender, EventArgs e)
        {
            dgvmo.MasterTemplate.SelectLastAddedRow = false;
            dgvmoline.MasterTemplate.SelectLastAddedRow = false;
            //disable close button on search in grid
            dgvmo.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvmoline.MasterView.TableSearchRow.ShowCloseButton = false;
            dc.OpenConnection();   //open connecion

            dgvmoline.Visible = false;
            radPanel2.Visible = false;
            RefreshGrid();

            btneditmo.Enabled = false;
            btndeletemo.Enabled = false;
            btnstnassign.Enabled = false;

            //special fields
            String user1 = "";
            String user2 = "";
            String user3 = "";
            String user4 = "";
            String user5 = "";
            String user6 = "";
            String user7 = "";
            String user8 = "";
            String user9 = "";
            String user10 = "";

            //get special field name
            SqlCommand cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF1' and V_ENABLED='TRUE'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user1 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user2 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user3 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user4 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user5 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF6' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user6 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF7' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user7 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF8' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user8 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF9' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user9 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF10' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user10 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            MO.Columns.Add("Color");
            MO.Columns.Add("Article ID");
            MO.Columns.Add("Article Desc.");
            MO.Columns.Add("Size");
            MO.Columns.Add(user1);
            MO.Columns.Add(user2);
            MO.Columns.Add(user3);
            MO.Columns.Add(user4);
            MO.Columns.Add(user5);
            MO.Columns.Add(user6);
            MO.Columns.Add(user7);
            MO.Columns.Add(user8);
            MO.Columns.Add(user9);
            MO.Columns.Add(user10);
            MO.Columns.Add("Quantity");
            MO.Columns.Add("Shipment Date");
            MO.Columns.Add("Shipment Dest");
            MO.Columns.Add("Shipment Mode");
            MO.Columns.Add("Purchase Order");
            MO.Columns.Add("Sales Order");
            MO.Columns.Add("MO Details");
            MO.Columns.Add("MO Created");
            MO.Columns.Add("Last Updated");
            dgvmoline.DataSource = MO;

            //hide columns which are not enabled
            if (user1 == "")
            {
                dgvmoline.Columns[4].IsVisible = false;
            }

            if (user2 == "")
            {
                dgvmoline.Columns[5].IsVisible = false;
            }

            if (user3 == "")
            {
                dgvmoline.Columns[6].IsVisible = false;
            }

            if (user4 == "")
            {
                dgvmoline.Columns[7].IsVisible = false;
            }

            if (user5 == "")
            {
                dgvmoline.Columns[8].IsVisible = false;
            }

            if (user6 == "")
            {
                dgvmoline.Columns[9].IsVisible = false;
            }

            if (user7 == "")
            {
                dgvmoline.Columns[10].IsVisible = false;
            }

            if (user8 == "")
            {
                dgvmoline.Columns[11].IsVisible = false;
            }

            if (user9 == "")
            {
                dgvmoline.Columns[12].IsVisible = false;
            }

            if (user10 == "")
            {
                dgvmoline.Columns[13].IsVisible = false;
            }
        }

        public void RefreshGrid()
        {
            //get all mo
            SqlDataAdapter da = new SqlDataAdapter("SELECT m.V_MO_NO,c.V_CUSTOMER_NAME FROM MO m, CUSTOMER_DB c where m.V_CUSTOMER_ID=c.V_CUSTOMER_ID", dc.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvmo.DataSource = dt;
            dgvmo.Columns["V_MO_NO"].HeaderText = "MONO";
            dgvmo.Columns["V_CUSTOMER_NAME"].HeaderText = "Customer ID";
        }

        private void radLabel15_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            radPanel2.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel15.Text = "";
            radPanel2.Visible = false;
            MyTimer.Stop();
        }

        private void txtmo_KeyDown(object sender, KeyEventArgs e)
        {
            //enter key press
            if (e.KeyCode == Keys.Enter)
            {
                btnsearch.PerformClick();
            }
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            //delete the mo
            SqlCommand cmd = new SqlCommand("delete from MO_DETAILS where V_MO_NO='" + txtmo.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            //delete the mo detials for the mo
            cmd = new SqlCommand("delete from MO where V_MO_NO='" + txtmo.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            RefreshGrid();   //get mo
            radLabel15.Text = "MO Deleted";
        }

        public void RowSelected()
        {
            if (dgvmo.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String mono = dgvmo.SelectedRows[0].Cells[0].Value + string.Empty;
                txtmo.Text = mono;
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            RowSelected();   //get mo details
            dgvmoline.Visible = true;
            btneditmo.Enabled = true;
            btndeletemo.Enabled = true;
            btnstnassign.Enabled = true;
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            EditMO();   //open edit mo
        }

        public void EditMO()
        {
            //check if there is modetails fro the mo
            if (dgvmoline.Rows.Count == 0)
            {
                Edit_MO em = new Edit_MO();
                if (ActiveMdiChild != null)
                {
                    ActiveMdiChild.Close();
                }

                em.MdiParent = this.ParentForm;
                em.Show();
                em.txtmo.Text = txtmo.Text;
                em.OpenMO();
                em.btnedit.PerformClick();
                this.Close();
            }
            else
            {
                Edit_MO em = new Edit_MO();
                if (ActiveMdiChild != null)
                {
                    ActiveMdiChild.Close();
                }

                em.MdiParent = this.ParentForm;
                em.Show();
                em.txtmo.Text = txtmo.Text;
                em.OpenMO(0);
                em.btnedit.PerformClick();
                this.Close();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            EditMO();   //open edit mo
        }

        private void Open_MO_FormClosed(object sender, FormClosedEventArgs e)
        {
            //em.Close();
        }              

        public void MOSelected()
        {
            String artDesc = "";
            dgvmoline.Visible = true;

            //get mo detials
            //SqlDataAdapter sda = new SqlDataAdapter("select V_MO_NO,V_COLOR_ID,I_ORDER_QTY,V_ARTICLE_ID,V_SIZE_ID,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10,D_SHIPMENT_DATE,V_SHIPPING_DEST,V_SHIPPING_MODE,V_PURCHASE_ORDER,V_SALES_ORDER,V_PROD_LINE,V_MO_LINE,D_CREATED,D_LAST_UPDATED from MO_DETAILS where V_MO_NO='" + txtmo.Text + "'", dc.con);
            SqlDataAdapter sda = new SqlDataAdapter("select V_MO_NO,V_COLOR_ID,I_ORDER_QTY,V_ARTICLE_ID,V_SIZE_ID,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10,FORMAT(D_SHIPMENT_DATE, 'yyyy-MM-dd') as D_SHIPMENT_DATE,V_SHIPPING_DEST,V_SHIPPING_MODE,V_PURCHASE_ORDER,V_SALES_ORDER,V_PROD_LINE,V_MO_LINE,D_CREATED,D_LAST_UPDATED from MO_DETAILS where V_MO_NO='" + txtmo.Text + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            MO.Rows.Clear();

            //special fields
            String user1 = "";
            String user2 = "";
            String user3 = "";
            String user4 = "";
            String user5 = "";
            String user6 = "";
            String user7 = "";
            String user8 = "";
            String user9 = "";
            String user10 = "";

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String color = dt.Rows[i][1].ToString();
                String qty = dt.Rows[i][2].ToString();
                String artID = dt.Rows[i][3].ToString();
                String size = dt.Rows[i][4].ToString();
                user1 = dt.Rows[i][5].ToString();
                user2 = dt.Rows[i][6].ToString();
                user3 = dt.Rows[i][7].ToString();
                user4 = dt.Rows[i][8].ToString();
                user5 = dt.Rows[i][9].ToString();
                user6 = dt.Rows[i][10].ToString();
                user7 = dt.Rows[i][11].ToString();
                user8 = dt.Rows[i][12].ToString();
                user9 = dt.Rows[i][13].ToString();
                user10 = dt.Rows[i][14].ToString();
                String ship_date = dt.Rows[i][15].ToString();
                String ship_dest = dt.Rows[i][16].ToString();
                String ship_mode = dt.Rows[i][17].ToString();
                String po = dt.Rows[i][18].ToString();
                String so = dt.Rows[i][19].ToString();
                String prodline = dt.Rows[i][20].ToString();
                String moline = dt.Rows[i][21].ToString();
                String created = dt.Rows[i][22].ToString();
                String last_update = dt.Rows[i][23].ToString();

                //get desc for masters
                SqlCommand cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    color = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for masters
                cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + artID + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    artDesc = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for masters
                cmd = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    size = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for masters
                cmd = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + user1 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user1 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for masters
                cmd = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + user2 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user2 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for masters
                cmd = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + user3 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user3 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for masters
                cmd = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + user4 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user4 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for masters
                cmd = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + user5 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user5 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for masters
                cmd = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + user6 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user6 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for masters
                cmd = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + user7 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user7 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for masters
                cmd = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + user8 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user8 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for masters
                cmd = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + user9 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user9 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for masters
                cmd = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + user10 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user10 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //add to grid
                MO.Rows.Add(color, artID, artDesc, size, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, qty, ship_date, ship_dest, ship_mode, po, so, moline, created, last_update);
            }
            dgvmoline.DataSource = MO;
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            //open edit mo
            if (dgvmoline.SelectedRows.Count > 0)
            {
                int i = dgvmoline.CurrentCell.RowIndex;
                Edit_MO em = new Edit_MO();
                if (ActiveMdiChild != null)
                {
                    ActiveMdiChild.Close();
                }

                em.MdiParent = this.ParentForm;
                em.Show();
                em.txtmo.Text = txtmo.Text;
                em.OpenMO(i);
                em.btnedit.PerformClick();
                this.Close();
            }            
        }

        private void Open_MO_Initialized(object sender, EventArgs e)
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

            //change language of the form
            SqlDataAdapter sda = new SqlDataAdapter("select " + Lang + " from Language where Form='OpenMO' order by Item_No", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            if (dt.Rows.Count > 0)
            {
                lblmo.Text = dt.Rows[0][0].ToString() + " :";
                btnsearch.Text = dt.Rows[1][0].ToString();
                btneditmo.Text = dt.Rows[2][0].ToString();
                btndeletemo.Text = dt.Rows[3][0].ToString();
                btnstnassign.Text= dt.Rows[4][0].ToString();
            }

            //get all the mo
            sda = new SqlDataAdapter("select V_MO_NO from MO", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txtmo.AutoCompleteCustomSource.Add(dt.Rows[i][0].ToString());
            }

            //chamge grid theme
            GridTheme(theme);
        }

        //set grid theme
        public void GridTheme(String theme)
        {
            dgvmo.ThemeName = theme;
            dgvmoline.ThemeName = theme;
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            //station assign
            Station_Assign em = new Station_Assign();
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            //update last select mo
            SqlCommand cmd = new SqlCommand("delete from LAST_SELECT_MO", dc.con);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand("insert into LAST_SELECT_MO values('" + txtmo.Text + "')", dc.con);
            cmd.ExecuteNonQuery();

            em.MdiParent = this.ParentForm;
            em.Show();
            this.Close();
        }

        private void dgvmo_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            MOSelected();   //get mo selected
        }

        private void dgvmo_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvmo.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvmoline_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvmoline.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvmoline.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvmoline.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvmoline.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }
    }
}
