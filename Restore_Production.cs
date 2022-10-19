using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Restore_Production : Telerik.WinControls.UI.RadForm
    {
        public Restore_Production()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();   //connection class
        DataTable data1 = new DataTable();

        private void Restore_Production_Load(object sender, EventArgs e)
        {
            dgvmo.MasterTemplate.SelectLastAddedRow = false;
            dgvmo.MasterView.TableSearchRow.ShowCloseButton = false;   //disable close button on search in grid

            data1.Columns.Add("MONO");
            data1.Columns.Add("Color");
            data1.Columns.Add("Size");
            data1.Columns.Add("Article_ID");
            data1.Columns.Add("USER1");
            data1.Columns.Add("USER2");
            data1.Columns.Add("USER3");
            data1.Columns.Add("USER4");
            data1.Columns.Add("USER5");
            data1.Columns.Add("USER6");
            data1.Columns.Add("USER7");
            data1.Columns.Add("USER8");
            data1.Columns.Add("USER9");
            data1.Columns.Add("USER10");
            data1.Columns.Add("qty");
            data1.Columns.Add("MOLINE");

            RefereshGrid();  //get restore production 
        }

        public void RefereshGrid()
        {
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

            DataTable MO = new DataTable();
            MO.Columns.Add("Select", Type.GetType("System.Boolean"));
            MO.Columns.Add("MO No");
            MO.Columns.Add("MO Details");
            MO.Columns.Add("Color");
            MO.Columns.Add("Size");
            MO.Columns.Add("Article");
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
            dgvmo.DataSource = MO;

            //hide the columns which are disabled
            if (user1 == "")
            {
                dgvmo.Columns[6].IsVisible = false;
            }

            if (user2 == "")
            {
                dgvmo.Columns[7].IsVisible = false;
            }

            if (user3 == "")
            {
                dgvmo.Columns[8].IsVisible = false;
            }

            if (user4 == "")
            {
                dgvmo.Columns[9].IsVisible = false;
            }

            if (user5 == "")
            {
                dgvmo.Columns[10].IsVisible = false;
            }

            if (user6 == "")
            {
                dgvmo.Columns[11].IsVisible = false;
            }

            if (user7 == "")
            {
                dgvmo.Columns[12].IsVisible = false;
            }

            if (user8 == "")
            {
                dgvmo.Columns[13].IsVisible = false;
            }

            if (user9 == "")
            {
                dgvmo.Columns[14].IsVisible = false;
            }

            if (user10 == "")
            {
                dgvmo.Columns[15].IsVisible = false;
            }

            dgvmo.Columns[2].IsVisible = false;

            //get the restore mo details
            SqlDataAdapter sda = new SqlDataAdapter("SELECT DISTINCT MO.V_MO_NO,MO.V_COLOR_ID,MO.V_SIZE_ID,MO.V_ARTICLE_ID,MO.I_ORDER_QTY,MO.V_USER_DEF1,MO.V_USER_DEF2,MO.V_USER_DEF3,MO.V_USER_DEF4,MO.V_USER_DEF5,MO.V_USER_DEF6,MO.V_USER_DEF7,MO.V_USER_DEF8,MO.V_USER_DEF9,MO.V_USER_DEF10,MO.V_MO_LINE,MO.V_STATUS FROM MO_DETAILS MO ,STATION_ASSIGN SA where MO.V_MO_NO=SA.V_MO_NO and MO.V_MO_LINE=SA.V_MO_LINE and MO.V_STATUS='COMP'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String mo = dt.Rows[i][0].ToString();
                String color = dt.Rows[i][1].ToString();
                String size = dt.Rows[i][2].ToString();
                String article = dt.Rows[i][3].ToString();
                String qty = dt.Rows[i][4].ToString();
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
                String moline = dt.Rows[i][15].ToString();

                //get desc for the masters
                cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    color = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for the masters
                cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + article + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    article = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for the masters
                cmd = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    size = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + user1 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user1 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + user2 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user2 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + user3 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user3 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + user4 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user4 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + user5 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user5 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + user6 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user6 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + user7 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user7 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + user8 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user8 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + user9 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user9 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get desc for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + user10 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user10 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //add to grid
                MO.Rows.Add(false, mo, moline, color, size, article, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, qty);
                data1.Rows.Add(mo, color, size, article, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, qty, moline);
                dgvmo.DataSource = MO;
                
                int row = MO.Rows.Count - 1;
            }
        }
       
        private void radButton1_Click(object sender, EventArgs e)
        {
            //restore all the selected mo
            for (int i = 0; i < dgvmo.Rows.Count; i++)
            {
                if ((bool)dgvmo.Rows[i].Cells["Select"].Value)
                {
                    String MO = dgvmo.Rows[i].Cells[1].Value.ToString();
                    String MOLINE = dgvmo.Rows[i].Cells[2].Value.ToString();

                    //update mo status to prod
                    SqlCommand cmd1 = new SqlCommand("update MO_DETAILS set V_STATUS='PROD' where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                    cmd1.ExecuteNonQuery();

                    RefereshGrid();
                    radLabel4.Text = "MO No Restored";
                }
            }
        }

        private void radLabel4_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            radPanel2.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel4.Text = "";
            radPanel2.Visible = false;
            MyTimer.Stop();
        }

        String theme = "";
        private void Restore_Production_Initialized(object sender, EventArgs e)
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

            //change language of the form
            SqlDataAdapter sda = new SqlDataAdapter("select " + Lang + " from Language where Form='Restore_Prod' order by Item_No", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {               
                btnrestore.Text= dt.Rows[2][0].ToString();
                btncancel.Text= dt.Rows[3][0].ToString();
            }

            //change grid theme
            GridTheme(theme);
        }

        //set grid theme
        public void GridTheme(String theme)
        {
            dgvmo.ThemeName = theme;
        }                    

        private void btnreport_Click(object sender, EventArgs e)
        {
            if(btnreport.Text=="Report View")
            {                
                DataView view = new DataView(data1);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Restore.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                //add views to dataset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                reportViewer1.RefreshReport();

                btnreport.Text = "Table View";
                reportViewer1.Visible = true;
            }
            else
            {
                btnreport.Text = "Report View";
                reportViewer1.Visible = false;
            }
        }

        private void dgvmo_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //get the selected mo
            if (e.RowIndex < 0)
            {
                return;
            }

            if ((bool)dgvmo.Rows[e.RowIndex].Cells["Select"].Value)
            {
                dgvmo.Rows[e.RowIndex].Cells["Select"].Value = false;
            }
            else
            {
                dgvmo.Rows[e.RowIndex].Cells["Select"].Value = true;
            }
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
    }
}
