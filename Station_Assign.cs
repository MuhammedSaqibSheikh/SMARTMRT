using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Station_Assign : Telerik.WinControls.UI.RadForm
    {
        public Station_Assign()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();   //connection class
        String edit = "";
        String view = "";
        DataTable MO = new DataTable();

        private void radButton1_Click(object sender, EventArgs e)
        {
            string strMO = "";
            //strMO = cmbMO.SelectedText.TrimStart().TrimEnd();
            strMO = cmbMO.Text.TrimStart().TrimEnd();
            MO.Rows.Clear();
            dgvsequence.Rows.Clear();
            dgvstationassign.Rows.Clear();
            dgvstationassign.Columns.Clear();
            panel5.Visible = false;


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
 
            //get the modetails for the mo
            //SqlDataAdapter sda = new SqlDataAdapter("SELECT V_MO_NO,V_COLOR_ID,V_SIZE_ID,V_ARTICLE_ID,I_ORDER_QTY,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10,V_MO_LINE,D_SHIPMENT_DATE,V_SHIPPING_DEST,V_SHIPPING_MODE,V_PURCHASE_ORDER,V_SALES_ORDER,D_LAST_UPDATED FROM MO_DETAILS where V_MO_NO='" + txtmo.Text + "' order by V_MO_LINE", dc.con);
            //String strSql = "SELECT V_MO_NO, V_COLOR_ID, V_SIZE_ID, V_ARTICLE_ID, (select ARTICLE_DB.V_ARTICLE_DESC from ARTICLE_DB where ARTICLE_DB.V_ARTICLE_ID = MO_DETAILS.V_ARTICLE_ID) as V_ARTICLE_DESC, I_ORDER_QTY, V_USER_DEF1, V_USER_DEF2, V_USER_DEF3, V_USER_DEF4, V_USER_DEF5, V_USER_DEF6, V_USER_DEF7, V_USER_DEF8, V_USER_DEF9, V_USER_DEF10, V_MO_LINE, D_SHIPMENT_DATE, V_SHIPPING_DEST, V_SHIPPING_MODE, V_PURCHASE_ORDER, V_SALES_ORDER, D_LAST_UPDATED FROM MO_DETAILS where V_MO_NO = '" + txtmo.Text + "' order by V_MO_LINE";
            String strSql = "SELECT V_MO_NO, V_COLOR_ID, V_SIZE_ID, V_ARTICLE_ID, (select ARTICLE_DB.V_ARTICLE_DESC from ARTICLE_DB where ARTICLE_DB.V_ARTICLE_ID = MO_DETAILS.V_ARTICLE_ID) as V_ARTICLE_DESC, I_ORDER_QTY, V_USER_DEF1, V_USER_DEF2, V_USER_DEF3, V_USER_DEF4, V_USER_DEF5, V_USER_DEF6, V_USER_DEF7, V_USER_DEF8, V_USER_DEF9, V_USER_DEF10, V_MO_LINE, D_SHIPMENT_DATE, V_SHIPPING_DEST, V_SHIPPING_MODE, V_PURCHASE_ORDER, V_SALES_ORDER, D_LAST_UPDATED FROM MO_DETAILS where V_MO_NO = '" + strMO + "' order by V_MO_LINE";
            SqlDataAdapter sda = new SqlDataAdapter(strSql, dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String color = dt.Rows[i][1].ToString();
                String size = dt.Rows[i][2].ToString();
                //String article = dt.Rows[i][3].ToString();
                String artID = dt.Rows[i][3].ToString();
                String artDesc = dt.Rows[i][4].ToString();
                String qty = dt.Rows[i][5].ToString();
                user1 = dt.Rows[i][6].ToString();
                user2 = dt.Rows[i][7].ToString();
                user3 = dt.Rows[i][8].ToString();
                user4 = dt.Rows[i][9].ToString();
                user5 = dt.Rows[i][10].ToString();
                user6 = dt.Rows[i][11].ToString();
                user7 = dt.Rows[i][12].ToString();
                user8 = dt.Rows[i][13].ToString();
                user9 = dt.Rows[i][14].ToString();
                user10 = dt.Rows[i][15].ToString();
                String moline = dt.Rows[i][16].ToString();
                txtshippingdate.Text = dt.Rows[i][17].ToString();
                txtshipmentdest.Text = dt.Rows[i][18].ToString();
                txtshippingmode.Text = dt.Rows[i][19].ToString();
                txtpurchaseorder.Text = dt.Rows[i][20].ToString();
                txtsalesorder.Text = dt.Rows[i][21].ToString();
                String last_update= dt.Rows[i][22].ToString();
                //get the description of the color,article etc

                SqlCommand cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    color = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + article + "'", dc.con);
                //sdr = cmd.ExecuteReader();
                //if (sdr.Read())
                //{
                //    article = sdr.GetValue(0).ToString();
                //}
                //sdr.Close();

                cmd = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    size = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + user1 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user1 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + user2 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user2 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + user3 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user3 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + user4 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user4 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + user5 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user5 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + user6 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user6 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + user7 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user7 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + user8 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user8 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + user9 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user9 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                cmd = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + user10 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user10 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //sda = new SqlDataAdapter("Select C.V_CUSTOMER_NAME from CUSTOMER_DB C,MO M where M.V_MO_NO='" + txtmo.Text + "' and  C.V_CUSTOMER_ID=M.V_CUSTOMER_ID", dc.con);
                sda = new SqlDataAdapter("Select C.V_CUSTOMER_NAME from CUSTOMER_DB C,MO M where M.V_MO_NO='" + strMO + "' and  C.V_CUSTOMER_ID=M.V_CUSTOMER_ID", dc.con);
                DataTable dt1 = new DataTable();
                sda.Fill(dt1);
                sda.Dispose();
                for (int ii = 0; ii < dt1.Rows.Count; ii++)
                {
                    txtcustomer.Text = dt1.Rows[ii][0].ToString();
                }
                //add the rows to the mo grid view
                MO.Rows.Add(color, size, artID, artDesc, qty, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, moline, last_update);
            }

            dgvmoline.DataSource = MO;
            String mo = "";

            //get the last searched mo
            SqlCommand cmd1 = new SqlCommand("select V_MO_NO from LAST_SELECT_MO where V_ID=(select MAX(V_ID) from LAST_SELECT_MO)", dc.con);
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            if (sdr1.Read())
            {
                mo = sdr1.GetValue(0).ToString();
            }
            sdr1.Close();

            //if its not same then insert to last loaded mo
            //if (mo != txtmo.Text && MO.Rows.Count > 0)
            //{
            //    //cmd1 = new SqlCommand("update LAST_SELECT_MO set V_MO_NO='" + txtmo.Text + "'", dc.con);
            //    cmd1 = new SqlCommand("update LAST_SELECT_MO set V_MO_NO='" + strMO + "'", dc.con);
            //    cmd1.ExecuteNonQuery();
            //}

            if (mo != strMO && MO.Rows.Count > 0)
            {
                //cmd1 = new SqlCommand("update LAST_SELECT_MO set V_MO_NO='" + txtmo.Text + "'", dc.con);
                cmd1 = new SqlCommand("update LAST_SELECT_MO set V_MO_NO='" + strMO + "'", dc.con);
                cmd1.ExecuteNonQuery();
            }

            if (dgvmoline.Rows.Count > 0)
            {
                this.dgvmoline.Rows[0].IsSelected = true;
                this.dgvmoline.Rows[0].IsCurrent = true;
            }
            RowSelected();
        }

        private void Station_Assign_Load(object sender, EventArgs e)
        {
            dgvmoline.MasterTemplate.SelectLastAddedRow = false;
            RadMessageBox.SetThemeName("FluentDark");
            dc.OpenConnection();
            dgvmoline.MasterView.TableSearchRow.ShowCloseButton = false;

            dgvstationassign.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgvstationassign.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvstationassign.EnableHeadersVisualStyles = false;
            dgvsequence.Visible = false;

            dgvsequence.DefaultCellStyle.SelectionBackColor = Color.Yellow;
            dgvsequence.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvsequence.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgvsequence.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvsequence.DefaultCellStyle.ForeColor = Color.Black;
            dgvsequence.EnableHeadersVisualStyles = false;

            //dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.Bisque;
            this.dgvstationassign.DefaultCellStyle.ForeColor = Color.Black;
            dgvstationassign.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
            radPanel2.Visible = false;

            WindowState = FormWindowState.Maximized;
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

            //get the user defined field names            
            SqlCommand cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF1' and V_ENABLED='TRUE'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user1 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user2 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user3 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user4 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user5 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF6' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user6 = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF7' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user7 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF8' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user8 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF9' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user9 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF10' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user10 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //add columns to MO table
            MO.Columns.Add("Color");
            MO.Columns.Add("Size");
            MO.Columns.Add("Article ID");
            MO.Columns.Add("Article Desc.");
            MO.Columns.Add("Quantity");
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
            MO.Columns.Add("MO Details");
            MO.Columns.Add("Last Updated");
            dgvmoline.DataSource = MO;

            //hide the columns which are not enabled
            if (user1 == "")
            {
                dgvmoline.Columns[5].IsVisible = false;
            }

            if (user2 == "")
            {
                dgvmoline.Columns[6].IsVisible = false;
            }

            if (user3 == "")
            {
                dgvmoline.Columns[7].IsVisible = false;
            }

            if (user4 == "")
            {
                dgvmoline.Columns[8].IsVisible = false;
            }

            if (user5 == "")
            {
                dgvmoline.Columns[9].IsVisible = false;
            }

            if (user6 == "")
            {
                dgvmoline.Columns[10].IsVisible = false;
            }

            if (user7 == "")
            {
                dgvmoline.Columns[11].IsVisible = false;
            }

            if (user8 == "")
            {
                dgvmoline.Columns[12].IsVisible = false;
            }

            if (user9 == "")
            {
                dgvmoline.Columns[13].IsVisible = false;
            }

            if (user10 == "")
            {
                dgvmoline.Columns[14].IsVisible = false;
            }

            //get the last loaded mo
            string lastSelMO = "";
            cmd = new SqlCommand("select V_MO_NO from LAST_SELECT_MO", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                //txtmo.Text = sdr.GetValue(0).ToString();
                lastSelMO = sdr.GetValue(0).ToString();
                sdr.Close();
                //btnsearch.PerformClick();
            }
            sdr.Close();


            // cmbMO.SelectedIndex = 0;
            SqlDataAdapter daMO = new SqlDataAdapter("select V_MO_NO from MO", dc.con);
            DataTable dtMO = new DataTable();
            daMO.Fill(dtMO);
            daMO.Dispose();
            for (int j = 0; j < dtMO.Rows.Count; j++)
            {
                cmbMO.Items.Add(dtMO.Rows[j][0].ToString());
            }

            if (lastSelMO != "")
            {
                cmbMO.SelectedText = lastSelMO;
            }
            btnsearch.PerformClick();

        }

        private void txtmo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                btnsearch.PerformClick();
            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            if (dgvstationassign.Columns.Count > 0)
            {
                dgvstationassign.Rows.Add("");
            }
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            //delete the last row from the station assign grid view
            int i = dgvstationassign.Rows.Count - 1;
            if (i >= 1)
            {
                dgvstationassign.Rows.RemoveAt(i);
            }
        }

        private void radLabel15_TextChanged(object sender, EventArgs e)
        {
            //hide the error message after 5 sec
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

        private void dataGridView3_KeyDown(object sender, KeyEventArgs e)
        {
            //add and delete row from station assign gridview when insert or delete is clicked
            if (e.KeyCode.Equals(Keys.Insert))
            {
                btnaddrow.PerformClick();
            }

            if (e.KeyCode.Equals(Keys.Delete))
            {
                btnremoverow.PerformClick();
            }
        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            try
            {
                //String MO = txtmo.Text;
                String MO = cmbMO.Text .TrimStart().TrimEnd();
                String MOLINE = dgvmoline.SelectedRows[0].Cells[15].Value.ToString();
                //String article = dgvmoline.SelectedRows[0].Cells[2].Value.ToString();
                String artID = dgvmoline.SelectedRows[0].Cells[2].Value.ToString();
                //delete from station assign table

                SqlCommand cmd = new SqlCommand("delete from STATION_ASSIGN where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "' and V_ASSIGN_TYPE='" + cmbstationassign.Text + "'", dc.con);
                cmd.ExecuteNonQuery();
                //get the article id

                //cmd = new SqlCommand("select V_ARTICLE_ID from ARTICLE_DB where V_ARTICLE_DESC='" + article + "'", dc.con);
                //SqlDataReader sdr = cmd.ExecuteReader();
                //if (sdr.Read())
                //{
                //    article = sdr.GetValue(0).ToString();
                //}
                //sdr.Close();

                for (int i = 0; i < dgvstationassign.Columns.Count; i++)
                {
                    int seq = i + 1;
                    for (int j = 0; j < dgvstationassign.Rows.Count; j++)
                    {
                        String station = dgvstationassign.Rows[j].Cells[i].Value + string.Empty;
                        String[] stn = new String[2];
                        int n = j + 1;
                        // if (station != "")
                        // {                            
                        String stnID = "";
                        if (station.Contains("."))
                        {
                            stn = station.Split('.');

                            //get the station id from the station data table
                            SqlCommand cmd1 = new SqlCommand("select I_STN_ID from STATION_DATA where I_INFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_INFEED='" + stn[1] + "'", dc.con);
                            SqlDataReader dataReader = cmd1.ExecuteReader();
                            if (dataReader.Read())
                            {
                                stnID = dataReader["I_STN_ID"].ToString();
                            }
                            dataReader.Close();

                            if (stnID == "")
                            {
                                cmd1 = new SqlCommand("select I_STN_ID from STATION_DATA where I_OUTFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_OUTFEED='" + stn[1] + "'", dc.con);
                                dataReader = cmd1.ExecuteReader();
                                if (dataReader.Read())
                                {
                                    stnID = dataReader["I_STN_ID"].ToString();
                                }
                                //close Data Reader
                                dataReader.Close();
                            }
                        }

                        if (stn[0] == "")
                        {
                            stn[0] = "0";
                            stn[1] = "0";
                        }

                        //insert into station assign table
                        cmd = new SqlCommand("insert into STATION_ASSIGN values('" + MO + "','" + MOLINE + "','" + seq + "','" + stn[1] + "','" + n + "','" + stnID + "','" + artID + "','" + stn[0] + "','" + cmbstationassign.Text + "')", dc.con);
                        cmd.ExecuteNonQuery();
                    }
                }
                btnsave.ForeColor = Color.Lime;
                radLabel15.Text = "Records Saved";
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
            }
        }        

        private void radButton6_Click(object sender, EventArgs e)
        {
            //display all the sequence
            for (int i = 0; i < dgvstationassign.Columns.Count; i++)
            {
                dgvstationassign.Columns[i].Visible = true;
                lblmode.Text = view;
                lblmode.ForeColor = Color.DodgerBlue;
            }
            dgvsequence.ClearSelection();
        }


        public void RowSelected()
        {

            string strMO = "";
            strMO = cmbMO.Text.TrimStart().TrimEnd();
            try
            {
                if (dgvmoline.SelectedRows.Count > 0) // make sure user select at least 1 row 
                {
                    txtmodetails.Visible = false;
                    btncopy.Visible = false;
                    panel5.Visible = true;

                    dgvsequence.Rows.Clear();
                    dgvstationassign.Rows.Clear();
                    dgvstationassign.Columns.Clear();

                    dgvsequence.Visible = true;
                    // String article = dgvmoline.SelectedRows[0].Cells[2].Value + string.Empty;
                    String artID = dgvmoline.SelectedRows[0].Cells[2].Value + string.Empty;

                    ////get the article id
                    //SqlCommand cmd = new SqlCommand("select V_ARTICLE_ID from ARTICLE_DB where V_ARTICLE_DESC='" + article + "'", dc.con);
                    //SqlDataReader sdr = cmd.ExecuteReader();
                    //if (sdr.Read())
                    //{
                    //    article = sdr.GetValue(0).ToString();
                    //}
                    //sdr.Close();

                    //get the operation and sequence number
                    SqlDataAdapter da = new SqlDataAdapter("SELECT V_OPERATION_CODE,I_SEQUENCE_NO,I_OPERATION_SEQUENCE_NO FROM DESIGN_SEQUENCE WHERE V_ARTICLE_ID='" + artID + "' ORDER BY I_SEQUENCE_NO", dc.con);
                    DataTable dt2 = new DataTable();
                    da.Fill(dt2);
                    da.Dispose();
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        String opcode = dt2.Rows[i][0].ToString();
                        String opdesc = "";
                        String op_seqno = dt2.Rows[i][2].ToString(); ;
                        String seqno = dt2.Rows[i][1].ToString();

                        //get the operation desc
                        da = new SqlDataAdapter("SELECT V_OPERATION_DESC FROM OPERATION_DB WHERE V_OPERATION_CODE='" + opcode + "'", dc.con);
                        DataTable dt1 = new DataTable();
                        da.Fill(dt1);
                        da.Dispose();
                        for (int k = 0; k < dt1.Rows.Count; k++)
                        {
                            opdesc = dt1.Rows[k][0].ToString();
                        }

                        dgvsequence.Rows.Add(op_seqno, opcode, opdesc, seqno);
                        dgvsequence.Visible = true;
                    }

                    //get the distinct sequence number
                    da = new SqlDataAdapter("Select distinct I_SEQUENCE_NO from DESIGN_SEQUENCE where V_ARTICLE_ID='" + artID + "' ORDER BY I_SEQUENCE_NO", dc.con);
                    DataTable dt3 = new DataTable();
                    da.Fill(dt3);
                    for (int i = 0; i < dt3.Rows.Count; i++)
                    {
                        //add columns to station assign
                        dgvstationassign.Columns.Add("", "Seq-" + dt3.Rows[i][0].ToString());
                        dgvstationassign.Columns[i].Visible = false;
                        dgvstationassign.Columns[i].Width = 50;
                    }

                    //get the distinct row no
                    //da = new SqlDataAdapter("select distinct I_ROW_NO from STATION_ASSIGN where V_MO_NO='" + txtmo.Text + "' and V_MO_LINE='" + dgvmoline.SelectedRows[0].Cells[15].Value.ToString() + "' and V_ASSIGN_TYPE='" + cmbstationassign.Text + "'", dc.con);
                    da = new SqlDataAdapter("select distinct I_ROW_NO from STATION_ASSIGN where V_MO_NO='" + strMO + "' and V_MO_LINE='" + dgvmoline.SelectedRows[0].Cells[15].Value.ToString() + "' and V_ASSIGN_TYPE='" + cmbstationassign.Text + "'", dc.con);
                    DataTable dt4 = new DataTable();
                    da.Fill(dt4);
                    for (int i = 0; i < dt4.Rows.Count; i++)
                    {
                        dgvstationassign.Rows.Add("");

                        //get the station number and line number from the station assign table
                        //da = new SqlDataAdapter("select D_STATION_NO,I_LINE_NO from STATION_ASSIGN where V_MO_NO='" + txtmo.Text + "' and V_MO_LINE='" + dgvmoline.SelectedRows[0].Cells[15].Value.ToString() + "' and V_ASSIGN_TYPE='" + cmbstationassign.Text + "' and I_ROW_NO='" + dt4.Rows[i][0].ToString() + "'  ORDER BY I_SEQUENCE_NO", dc.con);
                        da = new SqlDataAdapter("select D_STATION_NO,I_LINE_NO from STATION_ASSIGN where V_MO_NO='" + strMO + "' and V_MO_LINE='" + dgvmoline.SelectedRows[0].Cells[15].Value.ToString() + "' and V_ASSIGN_TYPE='" + cmbstationassign.Text + "' and I_ROW_NO='" + dt4.Rows[i][0].ToString() + "'  ORDER BY I_SEQUENCE_NO", dc.con);
                        DataTable dt5 = new DataTable();
                        da.Fill(dt5);
                        for (int j = 0; j < dt5.Rows.Count; j++)
                        {
                            dgvstationassign.Rows[i].Cells[j].Value = dt5.Rows[j][1].ToString() + "." + dt5.Rows[j][0].ToString();
                            dgvstationassign.Columns[j].Visible = true;
                            String station = dgvstationassign.Rows[i].Cells[j].Value + string.Empty;
                            if (station.Contains("."))
                            {
                                int infeed = 0;
                                String[] stn = station.Split('.');

                                //get the station type of the station id
                                SqlCommand cmd = new SqlCommand("select I_STATION_TYPE from STATION_DATA where I_INFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_INFEED='" + stn[1] + "'", dc.con);
                                SqlDataReader dataReader = cmd.ExecuteReader();
                                if (dataReader.Read())
                                {
                                    infeed = 1;
                                    String stntype = dataReader.GetValue(0).ToString();
                                    //check if the station is normal
                                    if (stntype == "1")
                                    {
                                        dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.SeaGreen };
                                    }

                                    //check if the station is bridge station
                                    if (stntype == "2")
                                    {
                                        dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.Violet };
                                        //dgvstationassign.Rows[i].Cells[j].Value = "";
                                    }

                                    //check if the station is buffer station
                                    if (stntype == "3")
                                    {
                                        dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.Yellow };
                                    }

                                    //check if the station is overload station
                                    if (stntype == "4")
                                    {
                                        dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.Firebrick };
                                    }

                                    //check if the station is auto collection station
                                    if (stntype == "5")
                                    {
                                        dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.Thistle };
                                    }

                                    //check if the station is buffer internal
                                    if (stntype == "6")
                                    {
                                        dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.Violet };
                                    }

                                    //check if the station is sorting station
                                    if (stntype == "7")
                                    {
                                        dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.PaleTurquoise };
                                    }

                                    //check if the station is sorting station
                                    if (stntype == "8")
                                    {
                                        dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.MediumSlateBlue };
                                    }
                                }
                                dataReader.Close();

                                if (infeed == 0)
                                {
                                    cmd = new SqlCommand("select I_STATION_TYPE from STATION_DATA where I_OUTFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_OUTFEED='" + stn[1] + "'", dc.con);
                                    dataReader = cmd.ExecuteReader();
                                    if (dataReader.Read())
                                    {
                                        String stntype = dataReader.GetValue(0).ToString();

                                        //check if the station is normal
                                        if (stntype == "1")
                                        {
                                            dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.SeaGreen };
                                        }

                                        //check if the station is bridge station
                                        if (stntype == "2")
                                        {
                                            dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.Violet };
                                        }

                                        //check if the station is buffer station
                                        if (stntype == "3")
                                        {
                                            dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.Yellow };
                                        }

                                        //check if the station is overload station
                                        if (stntype == "4")
                                        {
                                            dgvstationassign.Rows[i].Cells[j].Value = "";
                                            //radLabel15.Text = "Its a Overload Station";
                                        }

                                        //check if the station is auto collection station
                                        if (stntype == "5")
                                        {
                                            dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.Thistle };
                                        }

                                        //check if the station is buffer internal
                                        if (stntype == "6")
                                        {
                                            dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.Violet };
                                        }

                                        //check if the station is sorting station
                                        if (stntype == "7")
                                        {
                                            dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.PaleTurquoise };
                                        }

                                        //check if the station is sorting station
                                        if (stntype == "8")
                                        {
                                            dgvstationassign.Rows[i].Cells[j].Style = new DataGridViewCellStyle { BackColor = Color.MediumSlateBlue };
                                        }
                                    }
                                    dataReader.Close();
                                }

                                //if there is no station assign show empty
                                if (dgvstationassign.Rows[i].Cells[j].Value.ToString() == "0.0")
                                {
                                    dgvstationassign.Rows[i].Cells[j].Value = "";
                                }
                            }
                        }
                    }

                    if (dgvstationassign.Rows.Count == 0)
                    {
                        dgvstationassign.Rows.Add("");
                    }
                    lblmode.Text = view;
                    lblmode.ForeColor = Color.DodgerBlue;
                    btnviewall.PerformClick();                    
                }
            }
            catch (Exception ex)
            {
                radLabel15.Text = "No Sequence for the Article";
                Console.WriteLine(ex);
            }
        }

        private void radButton5_Click(object sender, EventArgs e)
        {
            dgvstationassign.Rows.Clear();
        }

        private void dataGridView3_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                String station = dgvstationassign.SelectedCells[0].Value + string.Empty;
                btnsave.ForeColor = Color.Red;
                
                String cur_station = dgvstationassign.SelectedCells[0].Value + string.Empty;
                if (cur_station.Contains("."))
                {
                    String[] cur_stn = cur_station.Split('.');
                    String cur = "";
                    String cur_group = "";

                    //buffer group 
                    SqlCommand cmd = new SqlCommand("select V_BUFFER_GROUP_ID from BUFFER_STATION where V_BUFFER_STATION_NO='" + cur_station + "'", dc.con);
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        cur_group = dataReader.GetValue(0).ToString();
                    }
                    dataReader.Close();

                    //get station type
                    cmd = new SqlCommand("select I_STATION_TYPE from STATION_DATA where I_INFEED_LINE_NO='" + cur_stn[0] + "' and I_STN_NO_INFEED='" + cur_stn[1] + "'", dc.con);
                    dataReader = cmd.ExecuteReader();
                    if (dataReader.Read())
                    {
                        cur = dataReader.GetValue(0).ToString();
                        dataReader.Close();
                        if (cur == "3")
                        {
                            if (cur_group != "")
                            {
                                for (int i = 0; i < dgvstationassign.RowCount; i++)
                                {
                                    for (int j = 0; j < dgvstationassign.ColumnCount; j++)
                                    {
                                        if (j != e.ColumnIndex)
                                        {
                                            String prev_station = dgvstationassign.Rows[i].Cells[j].Value + string.Empty;
                                            if (prev_station.Contains(".") && cur_station.Contains("."))
                                            {
                                                String[] prev_stn = prev_station.Split('.');
                                                String prev_group = "";

                                                //get the station id of the station 
                                                cmd = new SqlCommand("select V_BUFFER_GROUP_ID from BUFFER_STATION where V_BUFFER_STATION_NO='" + prev_station + "'", dc.con);
                                                dataReader = cmd.ExecuteReader();
                                                if (dataReader.Read())
                                                {
                                                    prev_group = dataReader.GetValue(0).ToString();
                                                }
                                                dataReader.Close();

                                                //get station type
                                                cmd = new SqlCommand("select I_STATION_TYPE from STATION_DATA where I_INFEED_LINE_NO='" + prev_stn[0] + "' and I_STN_NO_INFEED='" + prev_stn[1] + "'", dc.con);
                                                dataReader = cmd.ExecuteReader();
                                                if (dataReader.Read())
                                                {
                                                    String stntype1 = dataReader.GetValue(0).ToString();
                                                    if (stntype1 == "3")
                                                    {
                                                        if (prev_group == cur_group)
                                                        {
                                                            dgvstationassign.SelectedCells[0].Value = "";
                                                            radLabel15.Text = "Cannot use the Same Buffer Group in Two Different Sequence";
                                                            
                                                            return;
                                                        }
                                                    }
                                                }
                                                dataReader.Close();
                                            }
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                dgvstationassign.SelectedCells[0].Value = "";
                                radLabel15.Text = "Create the Buffer Group and Assign Stations to that Group";
                                
                                return;
                            }
                        }
                    }
                    dataReader.Close();

                    for (int i = 0; i < dgvstationassign.Rows.Count; i++)
                    {
                        String prev_station = dgvstationassign.Rows[i].Cells[e.ColumnIndex].Value + string.Empty;

                        if (prev_station.Contains(".") && cur_station.Contains("."))
                        {
                            String[] prev_stn = prev_station.Split('.');

                            String prev = "";
                            String prev_group = "";

                            //get the station id of the station 
                            cmd = new SqlCommand("select I_STATION_TYPE from STATION_DATA where I_INFEED_LINE_NO='" + prev_stn[0] + "' and I_STN_NO_INFEED='" + prev_stn[1] + "'", dc.con);
                            dataReader = cmd.ExecuteReader();
                            if (dataReader.Read())
                            {
                                prev = dataReader.GetValue(0).ToString();
                            }
                            dataReader.Close();

                            //get buffer group id
                            cmd = new SqlCommand("select V_BUFFER_GROUP_ID from BUFFER_STATION where V_BUFFER_STATION_NO='" + prev_station + "'", dc.con);
                            dataReader = cmd.ExecuteReader();
                            if (dataReader.Read())
                            {
                                prev_group = dataReader.GetValue(0).ToString();
                            }
                            dataReader.Close();

                            if (prev != cur)
                            {
                                dgvstationassign.SelectedCells[0].Value = "";
                                radLabel15.Text = "Station type does not match";
                                return;
                            }

                            if (prev_group != cur_group)
                            {
                                dgvstationassign.SelectedCells[0].Value = "";
                                radLabel15.Text = "Buffer Group does not match";
                                return;
                            }
                        }
                    }
                    if (station.Contains("."))
                    {
                        String[] stn = station.Split('.');

                        int INFEED = 0;
                        int OUTFEED = 0;

                        String cur_type = "";

                        //get the station id of the station 
                        cmd = new SqlCommand("select I_STATION_TYPE from STATION_DATA where I_INFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_INFEED='" + stn[1] + "'", dc.con);
                        dataReader = cmd.ExecuteReader();
                        if (dataReader.Read())
                        {
                            cur_type = dataReader.GetValue(0).ToString();
                            if (dataReader.GetValue(0).ToString() == "2")
                            {
                                INFEED = 2;
                            }
                            else
                            {
                                INFEED = 0;
                            }
                        }
                        else
                        {
                            INFEED = 1;
                        }
                        dataReader.Close();

                        //get station type
                        cmd = new SqlCommand("select I_STATION_TYPE from STATION_DATA where I_OUTFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_OUTFEED='" + stn[1] + "'", dc.con);
                        dataReader = cmd.ExecuteReader();
                        if (dataReader.Read())
                        {
                            cur_type = dataReader.GetValue(0).ToString();
                            if (cur_type == "2")
                            {
                                INFEED = 1;
                            }
                            else
                            {
                                INFEED = 0;
                            }
                        }
                        else
                        {
                            OUTFEED = 1;
                        }
                        dataReader.Close();

                        if (INFEED == 2)
                        {
                            dgvstationassign.SelectedCells[0].Value = "";
                            radLabel15.Text = "There is no Station " + stn[1] + " in Line " + stn[0];
                            return;
                        }

                        if (INFEED == 1 && OUTFEED == 1)
                        {
                            dgvstationassign.SelectedCells[0].Value = "";
                            radLabel15.Text = "There is no Station " + stn[1] + " in Line " + stn[0];
                            return;
                        }

                        if (INFEED == 1)
                        {
                            for (int i = 0; i < dgvstationassign.RowCount; i++)
                            {
                                for (int j = 0; j < dgvstationassign.ColumnCount; j++)
                                {
                                    String prev_station = dgvstationassign.Rows[i].Cells[j].Value + string.Empty;
                                    if (prev_station.Contains(".") && cur_station.Contains("."))
                                    {
                                        String[] prev_stn = prev_station.Split('.');
                                        String prev = "";

                                        //get station type
                                        cmd = new SqlCommand("select I_STATION_TYPE from STATION_DATA where I_INFEED_LINE_NO='" + prev_stn[0] + "' and I_STN_NO_INFEED='" + prev_stn[1] + "'", dc.con);
                                        dataReader = cmd.ExecuteReader();
                                        if (dataReader.Read())
                                        {
                                            prev = dataReader.GetValue(0).ToString();
                                        }
                                        dataReader.Close();

                                        if (prev == "2")
                                        {
                                            if (j < e.ColumnIndex)
                                            {
                                                dgvstationassign.SelectedCells[0].Value = "";
                                                radLabel15.Text = "Brigde Station can Only be Used for Loading";
                                                return;
                                            }
                                            else if (j > e.ColumnIndex)
                                            {
                                                dgvstationassign.Rows[i].Cells[j].Value = "";
                                                radLabel15.Text = "Brigde Station can Only be Used for Loading";
                                                return;
                                            }
                                        }

                                        if (cur_type == "2")
                                        {
                                            if (j < e.ColumnIndex)
                                            {
                                                dgvstationassign.SelectedCells[0].Value = "";
                                                radLabel15.Text = "Brigde Station can Only be Used for Loading";
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        //get the station type of the station
                        if (INFEED == 0)
                        {
                            cmd = new SqlCommand("select I_STATION_TYPE from STATION_DATA where I_INFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_INFEED='" + stn[1] + "'", dc.con);
                            dataReader = cmd.ExecuteReader();
                            if (dataReader.Read())
                            {
                                String stntype = dataReader.GetValue(0).ToString();
                                //check if the station is normal station
                                if (stntype == "1")
                                {
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.SeaGreen };
                                }

                                //check if the station is buffer station
                                if (stntype == "3")
                                {
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.Yellow };
                                }

                                //check if the station is overload station
                                if (stntype == "4")
                                {
                                    //dgvstationassign.SelectedCells[0].Value = "";
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.Firebrick };
                                    radLabel15.Text = "Its a Overload Station";
                                }

                                //check if the station is auto collection station
                                if (stntype == "5")
                                {
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.Thistle };
                                }

                                //check if the station is buffer internal station
                                if (stntype == "6")
                                {
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.Violet };
                                }

                                //check if the station is sorting station
                                if (stntype == "7")
                                {
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.PaleTurquoise };
                                }

                                //check if the station is qc station
                                if (stntype == "8")
                                {
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.MediumSlateBlue };
                                }
                            }
                            dataReader.Close();
                        }

                        if (INFEED == 1)
                        {
                            cmd = new SqlCommand("select I_STATION_TYPE from STATION_DATA where I_OUTFEED_LINE_NO='" + stn[0] + "' and I_STN_NO_OUTFEED='" + stn[1] + "'", dc.con);
                            dataReader = cmd.ExecuteReader();
                            if (dataReader.Read())
                            {
                                String stntype = dataReader.GetValue(0).ToString();
                                //check if the station is normal station
                                if (stntype == "1")
                                {
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.SeaGreen };
                                }

                                //check if the station is bridge station
                                if (stntype == "2")
                                {
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.Violet };
                                    radLabel15.Text = "Brigde Station can Only be Used for Loading";
                                }

                                //check if the station is buffer station
                                if (stntype == "3")
                                {
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.Yellow };
                                }

                                //check if the station is overload station
                                if (stntype == "4")
                                {
                                    //dgvstationassign.SelectedCells[0].Value = "";
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.Firebrick };
                                    radLabel15.Text = "Its a Overload Station";
                                }

                                //check if the station is auto collection station
                                if (stntype == "5")
                                {
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.Thistle };
                                }

                                //check if the station is buffer internal station
                                if (stntype == "6")
                                {
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.Violet };
                                }

                                //check if the station is sorting station
                                if (stntype == "7")
                                {
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.PaleTurquoise };
                                }

                                //check if the station is qc station
                                if (stntype == "8")
                                {
                                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.MediumSlateBlue };
                                }
                            }
                            dataReader.Close();
                        }
                        btnsave.ForeColor = Color.Red;
                    }
                    //check if the station assign is empty
                    else if (station == "")
                    {
                        dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.White };
                        btnsave.ForeColor = Color.Red;
                    }
                    else
                    {
                        dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.White };
                        dgvstationassign.SelectedCells[0].Value = "";
                        radLabel15.Text = "Invalid Station Assign";
                    }
                }
                else
                {
                    dgvstationassign.SelectedCells[0].Style = new DataGridViewCellStyle { BackColor = Color.White };
                }
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
            }
        }

        String theme = "";

        private void Station_Assign_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();

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

            //get the text according to language
            SqlDataAdapter sda = new SqlDataAdapter("select " + Lang + " from Language where Form='StationAssign' order by Item_No", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                lblmono.Text = dt.Rows[0][0].ToString() + " :";
                lblpo.Text = dt.Rows[1][0].ToString() + " :";
                lblcustomer.Text = dt.Rows[2][0].ToString() + " :";
                lblso.Text = dt.Rows[3][0].ToString() + " :";
                lblshipdate.Text = dt.Rows[4][0].ToString() + " :";
                lblshipdest.Text = dt.Rows[5][0].ToString() + " :";
                lblshipmode.Text = dt.Rows[6][0].ToString() + " :";
                btnaddrow.Text = dt.Rows[7][0].ToString();
                btnsave.Text = dt.Rows[8][0].ToString();
                btncancel.Text = dt.Rows[9][0].ToString();
                btnremoverow.Text = dt.Rows[10][0].ToString();
                btnviewall.Text = dt.Rows[11][0].ToString();
                btnsearch.Text = dt.Rows[12][0].ToString();
                btnaddproduction.Text = dt.Rows[15][0].ToString();
                view = dt.Rows[13][0].ToString();
                edit = dt.Rows[14][0].ToString();
            }
            lblmode.Text = view;

            ////get last select mo
            //sda = new SqlDataAdapter("select V_MO_NO from MO", dc.con);
            //dt = new DataTable();
            //sda.Fill(dt);
            //sda.Dispose();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    txtmo.AutoCompleteCustomSource.Add(dt.Rows[i][0].ToString());
            //}

            //change grid theme
            GridTheme(theme);
        }

        //set grid theme
        public void GridTheme(String theme)
        {
            dgvmoline.ThemeName = theme;
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            //open add to production
            Add_Production em = new Add_Production();
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            em.MdiParent = this.ParentForm;
            this.Close();
            em.Show();
        }

        private void btneditsequence_Click(object sender, EventArgs e)
        {
            if (dgvmoline.SelectedRows.Count > 0)
            {
                Design_Sequence ds = new Design_Sequence();
                //String articledesc = dgvmoline.SelectedRows[0].Cells[2].Value + string.Empty;
                //String articleid = "";

                String articleid = dgvmoline.SelectedRows[0].Cells[2].Value.ToString();
                String articledesc = dgvmoline.SelectedRows[0].Cells[3].Value.ToString();
                String stnAsg = cmbstationassign.Text;

                //get the article id to edit design sequence
                SqlCommand cmd = new SqlCommand("select V_ARTICLE_ID from ARTICLE_DB where V_ARTICLE_DESC='" + articledesc + "'", dc.con);
                //SqlDataReader sdr = cmd.ExecuteReader();
                //if (sdr.Read())
                //{
                //    articleid = sdr.GetValue(0).ToString();
                //}
                //sdr.Close();

                ds.txtarticleid.Text = articleid;
                ds.txtarticledesc.Text = articledesc;
                ds.txtStnAsgn.Text = stnAsg;
                ds.Show();
                
            }
        }

        private void radButton1_Click_2(object sender, EventArgs e)
        {
            //go to setup form
            Setup em = new Setup();
            em.Form_Location("Station Assign");
            em.Show();
        }

        private void Station_Assign_FormClosing(object sender, FormClosingEventArgs e)
        {
            //check if the station is saved or not 
            if (btnsave.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Station Assign. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsave.PerformClick();
                    e.Cancel = true;
                }
            }
        }

        private void Station_Assign_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void radButton2_Click_1(object sender, EventArgs e)
        {
            //open sequence report
            Sequence_Report sr = new Sequence_Report();
            sr.MdiParent = this.ParentForm;
            sr.Show();
            this.Close();
        }

        private void radTextBox1_Enter(object sender, EventArgs e)
        {
            txtmodetails.Text = "";
        }

        private void btncopystation_Click(object sender, EventArgs e)
        {
            txtmodetails.Visible = true;
            btncopy.Visible = true;
        }

        private void btncopy_Click(object sender, EventArgs e)
        {
            try
            {
                String artID = dgvmoline.SelectedRows[0].Cells[2].Value + string.Empty;
                String artDesc = dgvmoline.SelectedRows[0].Cells[3].Value + string.Empty;
                //String MO = txtmo.Text;
                String MO = cmbMO.SelectedText.TrimStart().TrimEnd();
                String MOLINE = dgvmoline.SelectedRows[0].Cells[15].Value.ToString();

                ////get the article id
                //SqlCommand cmd = new SqlCommand("select V_ARTICLE_ID from ARTICLE_DB where V_ARTICLE_DESC='" + article + "'", dc.con);
                //SqlDataReader sdr = cmd.ExecuteReader();
                //if (sdr.Read())
                //{
                //    artDesc = sdr.GetValue(0).ToString();
                //}
                //sdr.Close();

                String article1 = "";
                //SqlCommand cmd = new SqlCommand("select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + txtmo.Text + "' and V_MO_LINE='" + txtmodetails.Text + "'", dc.con);
                SqlCommand cmd = new SqlCommand("select V_ARTICLE_ID from MO_DETAILS where V_MO_NO='" + MO + "' and V_MO_LINE='" + txtmodetails.Text + "'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    article1 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                if (artID == article1)
                {
                    //delete
                    cmd = new SqlCommand("delete from STATION_ASSIGN where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "' and V_ASSIGN_TYPE='" + cmbstationassign.Text + "'", dc.con);
                    cmd.ExecuteNonQuery();

                    //select from station assign
                    //SqlDataAdapter sda = new SqlDataAdapter("select * from STATION_ASSIGN where V_MO_NO='" + txtmo.Text + "' and V_MO_LINE='" + txtmodetails.Text + "' and V_ASSIGN_TYPE='" + cmbstationassign.Text + "' order by I_ROW_NO", dc.con);
                    SqlDataAdapter sda = new SqlDataAdapter("select * from STATION_ASSIGN where V_MO_NO='" + MO + "' and V_MO_LINE='" + txtmodetails.Text + "' and V_ASSIGN_TYPE='" + cmbstationassign.Text + "' order by I_ROW_NO", dc.con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //insert
                        cmd = new SqlCommand("insert into STATION_ASSIGN values('" + MO + "','" + MOLINE + "','" + dt.Rows[i][3].ToString() + "','" + dt.Rows[i][4].ToString() + "','" + dt.Rows[i][5].ToString() + "','" + dt.Rows[i][6].ToString() + "','" + dt.Rows[i][7].ToString() + "','" + dt.Rows[i][8].ToString() + "','" + cmbstationassign.Text + "')", dc.con);
                        cmd.ExecuteNonQuery();
                    }

                    //radLabel15.Text = "Station Assign Copied from MO :" + txtmo.Text + " MO Details : " + txtmodetails.Text;
                    radLabel15.Text = "Station Assign Copied from MO :" + MO + " MO Details : " + txtmodetails.Text;
                    RowSelected();
                    txtmodetails.Text = "MO Details";
                }
                else
                {
                    //radLabel15.Text = "MO : " + txtmo.Text + " MO Details : " + txtmodetails.Text + " has different Article ID cannot Copy the Station Assign";
                    radLabel15.Text = "MO : " + MO + " MO Details : " + txtmodetails.Text + " has different Article ID cannot Copy the Station Assign";
                }
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
            }
        }


        private void dgvstationassign_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //change the back color for each type of station
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                {
                    return;
                }

                String[] seq = dgvstationassign.Columns[e.ColumnIndex].HeaderText.ToString().Split('-');
                for (int i = 0; i < dgvsequence.Rows.Count; i++)
                {
                    if (dgvsequence.Rows[i].Cells[3].Value.ToString() == seq[1])
                    {
                        dgvsequence.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else
                    {
                        dgvsequence.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
            }
        }

        private void dgvstationassign_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //change the back color for each type of station
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                {
                    return;
                }

                String[] seq = dgvstationassign.Columns[e.ColumnIndex].HeaderText.ToString().Split('-');
                for (int i = 0; i < dgvsequence.Rows.Count; i++)
                {
                    if (dgvsequence.Rows[i].Cells[3].Value.ToString() == seq[1])
                    {
                        dgvsequence.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        dgvsequence.FirstDisplayedScrollingRowIndex = i;
                    }
                    else
                    {
                        dgvsequence.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                radLabel15.Text = ex.Message;
            }
        }

        private void cmbstationassign_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            RowSelected();   //get selected row
        }

        private void dgvmoline_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //check if unsaved station assign
            if (btnsave.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Station Assign. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsave.PerformClick();
                }
                else
                {
                    btnsave.ForeColor = Color.Lime;
                }
            }

            RowSelected();   //get selected row
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

        private void dgvsequence_Click(object sender, EventArgs e)
        {
            try
            {
                //get the sequence number of the operation clicked
                for (int i = 0; i < dgvstationassign.Columns.Count; i++)
                {
                    String col = dgvstationassign.Columns[i].HeaderText.ToString();
                    String seq = dgvsequence.SelectedRows[0].Cells[3].Value.ToString();
                    String[] Line = col.Split('-');
                    if (Line[1] != seq)
                    {
                        //hide the other sequence
                        dgvstationassign.Columns[i].Visible = false;
                    }
                    else
                    {
                        //show the sequence
                        dgvstationassign.Columns[i].Visible = true;
                    }
                    lblmode.Text = edit;
                    lblmode.ForeColor = Color.MediumSpringGreen;
                }

                String seq1 = dgvsequence.SelectedRows[0].Cells[3].Value.ToString();
                for (int i = 0; i < dgvsequence.Rows.Count; i++)
                {
                    if (dgvsequence.Rows[i].Cells[3].Value.ToString() == seq1)
                    {
                        DataGridViewRow row = dgvsequence.Rows[i];
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else
                    {
                        DataGridViewRow row = dgvsequence.Rows[i];
                        row.DefaultCellStyle.BackColor = Color.White;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void dgvsequence_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //merge the row with same data
            if (e.RowIndex == 0)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        bool IsTheSameCellValue(int column, int row)
        {
            //merge the row with same data
            DataGridViewCell cell1 = dgvsequence[column, row];
            DataGridViewCell cell2 = dgvsequence[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }

        private void dgvsequence_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //merge the row with same data
            if (e.RowIndex != -1)
            {
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                if (e.RowIndex < 1 || e.ColumnIndex < 0)
                    return;
                if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
                {
                    e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                }
                else
                {
                    e.AdvancedBorderStyle.Top = dgvsequence.AdvancedCellBorderStyle.Top;
                }
            }
        }

        private void cmbMO_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            btnsearch.PerformClick();
        }
    }        
}
