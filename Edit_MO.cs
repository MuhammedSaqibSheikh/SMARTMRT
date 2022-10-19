using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Edit_MO : Telerik.WinControls.UI.RadForm
    {
        public Edit_MO()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            Location = new System.Drawing.Point(178, 94);
        }

        Database_Connection dc = new Database_Connection();   //Connection class
        DataTable MO = new DataTable();
        String save = "";
        String update = "";
        String controller_name = "";

        int refresh = 0;  //refresh flag
        private void Edit_MO_Load(object sender, EventArgs e)
        {
            dgvmo.MasterTemplate.SelectLastAddedRow = false;
            dgvmo.MasterView.TableSearchRow.ShowCloseButton = false;   //disable close button for search in grid

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

            dc.OpenConnection();        //open connection     
            radPanel2.Visible = false;

            //get special field name
            SqlCommand cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF1' and V_ENABLED='TRUE'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser1.Text = sdr.GetValue(0).ToString();
                user1 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel3.Visible = false;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser2.Text = sdr.GetValue(0).ToString();
                user2 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel4.Visible = false;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser3.Text = sdr.GetValue(0).ToString();
                user3 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel5.Visible = false;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser4.Text = sdr.GetValue(0).ToString();
                user4 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel6.Visible = false;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser5.Text = sdr.GetValue(0).ToString();
                user5 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel7.Visible = false;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF6' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser6.Text = sdr.GetValue(0).ToString() + " :";
                user6 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel9.Visible = false;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF7' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser7.Text = sdr.GetValue(0).ToString() + " :";
                user7 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel10.Visible = false;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF8' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser8.Text = sdr.GetValue(0).ToString() + " :";
                user8 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel11.Visible = false;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF9' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser9.Text = sdr.GetValue(0).ToString() + " :";
                user9 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel12.Visible = false;
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF10' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser10.Text = sdr.GetValue(0).ToString() + " :";
                user10 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel13.Visible = false;
            }
            sdr.Close();

            Referesh();  //refresh masters

            //add columns to mo datatable
            MO.Columns.Add("MoDtID");
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
            MO.Columns.Add("Production Line");
            MO.Columns.Add("MO Details");
            MO.Columns.Add("Target Quantity For Day");

            //hide special fields which are not enabled
            dgvmo.DataSource = MO;
            dgvmo.Columns[0].IsVisible = false;
            if (user1 == "")
            {
                dgvmo.Columns[5].IsVisible = false;
            }

            if (user2 == "")
            {
                dgvmo.Columns[6].IsVisible = false;
            }

            if (user3 == "")
            {
                dgvmo.Columns[7].IsVisible = false;
            }

            if (user4 == "")
            {
                dgvmo.Columns[8].IsVisible = false;
            }

            if (user5 == "")
            {
                dgvmo.Columns[9].IsVisible = false;
            }

            if (user6 == "")
            {
                dgvmo.Columns[10].IsVisible = false;
            }

            if (user7 == "")
            {
                dgvmo.Columns[11].IsVisible = false;
            }

            if (user8 == "")
            {
                dgvmo.Columns[12].IsVisible = false;
            }

            if (user9 == "")
            {
                dgvmo.Columns[13].IsVisible = false;
            }

            if (user10 == "")
            {
                dgvmo.Columns[14].IsVisible = false;
            }

            select_controller();   //get the seleted controller
        }


        public void OpenMO(int k)
        {
            //get customers details for the mo
            SqlCommand cmd = new SqlCommand("select V_CUSTOMER_ID from MO where V_MO_NO='" + txtmo.Text + "'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                cmbcustomer.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get customer name
            cmd = new SqlCommand("select V_CUSTOMER_NAME from CUSTOMER_DB where V_CUSTOMER_ID='" + cmbcustomer.Text + "'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                cmbcustomer.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get mo details
            //String strSql = "select V_MO_NO,V_COLOR_ID,I_ORDER_QTY,V_ARTICLE_ID,V_SIZE_ID,V_USER_DEF1," +
            //    "V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9," +
            //    "V_USER_DEF10,D_SHIPMENT_DATE,V_SHIPPING_DEST,V_SHIPPING_MODE,V_PURCHASE_ORDER,V_SALES_ORDER," +
            //    "V_PROD_LINE,V_MO_LINE,I_TARGET_DAY from MO_DETAILS where V_MO_NO='" + txtmo.Text + "'";

            String strSql = "select I_ID, V_MO_NO,V_COLOR_ID,I_ORDER_QTY,V_ARTICLE_ID, (select ARTICLE_DB.V_ARTICLE_DESC from ARTICLE_DB where ARTICLE_DB.V_ARTICLE_ID = MO_DETAILS.V_ARTICLE_ID) as V_ARTICLE_DESC,V_SIZE_ID,V_USER_DEF1," +
                "V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9," +
                "V_USER_DEF10,FORMAT(D_SHIPMENT_DATE, 'yyyy-MM-dd') as D_SHIPMENT_DATE,V_SHIPPING_DEST,V_SHIPPING_MODE,V_PURCHASE_ORDER,V_SALES_ORDER," +
                "V_PROD_LINE,V_MO_LINE,I_TARGET_DAY from MO_DETAILS where V_MO_NO = '" + txtmo.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(strSql, dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
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
                Int32 MoDtID = int.Parse(dt.Rows[i][0].ToString());
                String color = dt.Rows[i][2].ToString();
                String qty = dt.Rows[i][3].ToString();
                String artID = dt.Rows[i][4].ToString();
                String artDesc = dt.Rows[i][5].ToString();
                String size = dt.Rows[i][6].ToString();
                user1 = dt.Rows[i][7].ToString();
                user2 = dt.Rows[i][8].ToString();
                user3 = dt.Rows[i][9].ToString();
                user4 = dt.Rows[i][10].ToString();
                user5 = dt.Rows[i][11].ToString();
                user6 = dt.Rows[i][12].ToString();
                user7 = dt.Rows[i][13].ToString();
                user8 = dt.Rows[i][14].ToString();
                user9 = dt.Rows[i][15].ToString();
                user10 = dt.Rows[i][16].ToString();
                
                String ship_date = dt.Rows[i][17].ToString();
                String ship_dest = dt.Rows[i][18].ToString();
                String ship_mode = dt.Rows[i][19].ToString();
                String po = dt.Rows[i][20].ToString();
                String so = dt.Rows[i][21].ToString();
                String prodline = dt.Rows[i][22].ToString();
                String moline = dt.Rows[i][23].ToString();
                String target = dt.Rows[i][24].ToString();

                //get description for the master
                cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    color = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                ////get description for the master
                //cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + article + "'", dc.con);
                //sdr = cmd.ExecuteReader();
                //if (sdr.Read())
                //{
                //    article = sdr.GetValue(0).ToString();
                //}
                //sdr.Close();

                //get description for the master
                cmd = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    size = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the master
                cmd = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + user1 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user1 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the master
                cmd = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + user2 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user2 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the master
                cmd = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + user3 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user3 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the master
                cmd = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + user4 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user4 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the master
                cmd = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + user5 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user5 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the master
                cmd = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + user6 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user6 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the master
                cmd = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + user7 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user7 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the master
                cmd = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + user8 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user8 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the master
                cmd = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + user9 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user9 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the master
                cmd = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + user10 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user10 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //add to grid
                MO.Rows.Add(MoDtID, color, artID, artDesc, size, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, qty, ship_date, ship_dest, ship_mode, po, so, prodline, moline, target);
            }
            dgvmo.DataSource = MO;
            dgvmo.Rows[k].IsSelected = true;
            dgvmo.Rows[k].IsCurrent = true;
        }

        public void Referesh()
        {
            String artID = "";
            //get the selected mo details
            String color = cmbcolor.Text;
            //String article = cmbarticle.Text;

            if (cmbarticle.SelectedValue == null)
            {
                artID = "0";
            }
            else
            {
                artID = cmbarticle.SelectedValue.ToString();
            }

            String size = cmbsize.Text;
            String user1 = cmbuser1.Text;
            String user2 = cmbuser2.Text;
            String user3 = cmbuser3.Text;
            String user4 = cmbuser4.Text;
            String user5 = cmbuser5.Text;
            String user6 = cmbuser6.Text;
            String user7 = cmbuser7.Text;
            String user8 = cmbuser8.Text;
            String user9 = cmbuser9.Text;
            String user10 = cmbuser10.Text;
            String prodline = cmbprodline.Text;
            String cust = cmbcustomer.Text;

            //clear dropdownlist
            cmbcolor.Items.Clear();
            cmbarticle.Items.Clear();
            cmbsize.Items.Clear();
            cmbprodline.Items.Clear();
            cmbuser1.Items.Clear();
            cmbuser2.Items.Clear();
            cmbuser3.Items.Clear();
            cmbuser4.Items.Clear();
            cmbuser5.Items.Clear();
            cmbuser6.Items.Clear();
            cmbuser7.Items.Clear();
            cmbuser8.Items.Clear();
            cmbuser9.Items.Clear();
            cmbuser10.Items.Clear();
            cmbcustomer.Items.Clear();

            //get all masters
            SqlDataAdapter sda = new SqlDataAdapter("Select V_COLOR_DESC from COLOR_DB", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbcolor.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            ////get all masters
            //sda = new SqlDataAdapter("Select V_ARTICLE_DESC from ARTICLE_DB", dc.con);
            //dt = new DataTable();
            //sda.Fill(dt);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    cmbarticle.Items.Add(dt.Rows[i][0].ToString());
            //}
            //sda.Dispose();

            //Hanafi|21/7/2021|Changes - populate article ID and Desc
            try
            {
                //cmbdesignarticle.Items.Clear();
                //SqlDataAdapter da2 = new SqlDataAdapter("SELECT V_ARTICLE_ID, (+'['+ V_ARTICLE_ID + '] ' + V_ARTICLE_DESC) as ArtDesc FROM ARTICLE_DB", dc.con);
                SqlDataAdapter da2 = new SqlDataAdapter("SELECT V_ARTICLE_ID, (V_ARTICLE_ID + ' : ' + V_ARTICLE_DESC) as ArtDesc FROM ARTICLE_DB ORDER BY V_ARTICLE_ID", dc.con);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "ARTICLE_DB");
                DataTable dt2 = ds2.Tables["ARTICLE_DB"];
                DataRow row = dt2.NewRow();
                //dt.Rows.Add(0, 0, "--SELECT--");

                row["V_ARTICLE_ID"] = 0;
                row["ArtDesc"] = "--SELECT--";
                dt2.Rows.InsertAt(row, 0);

                cmbarticle.DataSource = dt2;
                cmbarticle.DisplayMember = "ArtDesc";
                cmbarticle.ValueMember = "V_ARTICLE_ID";
            }
            catch (Exception ex)
            {
                //lblmsg.Text = ex.Message;
            }

            //get all masters
            sda = new SqlDataAdapter("Select V_SIZE_DESC from SIZE_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbsize.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //get all masters
            sda = new SqlDataAdapter("Select V_CUSTOMER_NAME from CUSTOMER_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbcustomer.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //get all masters
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF1_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser1.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //get all masters
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF2_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser2.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //get all masters
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF3_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser3.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //get all masters
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF4_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser4.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //get all masters
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF5_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser5.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //get all masters
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF6_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser6.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //get all masters
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF7_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser7.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //get all masters
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF8_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser8.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //get all masters
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF9_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser9.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //get all masters
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF10_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser10.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //get all masters
            sda = new SqlDataAdapter("Select V_PROD_LINE from PROD_LINE_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbprodline.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //re-assing the masters to dropdownlist
            cmbcolor.Text = color;
            //cmbarticle.Text = article;
            cmbarticle.SelectedValue = artID;
            cmbcustomer.Text = cust;
            cmbsize.Text = size;
            cmbuser1.Text = user1;
            cmbuser2.Text = user2;
            cmbuser3.Text = user3;
            cmbuser4.Text = user4;
            cmbuser5.Text = user5;
            cmbuser6.Text = user6;
            cmbuser7.Text = user7;
            cmbuser8.Text = user8;
            cmbuser9.Text = user9;
            cmbuser10.Text = user10;
            cmbprodline.Text = prodline;
        }

        private void Edit_MO_Shown(object sender, EventArgs e)
        {
            //OpenMO(0);
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            RowSelected();   //get the selected mo details
        }
        public void RowSelected()
        {
            if (dgvmo.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String color = dgvmo.SelectedRows[0].Cells[1].Value.ToString();
                //String article = dgvmo.SelectedRows[0].Cells[1].Value.ToString();
                String ArtID = dgvmo.SelectedRows[0].Cells[2].Value.ToString();
                String size = dgvmo.SelectedRows[0].Cells[4].Value.ToString();
                String user1 = dgvmo.SelectedRows[0].Cells[5].Value.ToString();
                String user2 = dgvmo.SelectedRows[0].Cells[6].Value.ToString();
                String user3 = dgvmo.SelectedRows[0].Cells[7].Value.ToString();
                String user4 = dgvmo.SelectedRows[0].Cells[8].Value.ToString();
                String user5 = dgvmo.SelectedRows[0].Cells[9].Value.ToString();
                String user6 = dgvmo.SelectedRows[0].Cells[10].Value.ToString();
                String user7 = dgvmo.SelectedRows[0].Cells[11].Value.ToString();
                String user8 = dgvmo.SelectedRows[0].Cells[12].Value.ToString();
                String user9 = dgvmo.SelectedRows[0].Cells[13].Value.ToString();
                String user10 = dgvmo.SelectedRows[0].Cells[14].Value.ToString();
                String qty = dgvmo.SelectedRows[0].Cells[15].Value.ToString();
                String ship_date = dgvmo.SelectedRows[0].Cells[16].Value.ToString();
                String ship_dest = dgvmo.SelectedRows[0].Cells[17].Value.ToString();
                String ship_mode = dgvmo.SelectedRows[0].Cells[18].Value.ToString();
                String po = dgvmo.SelectedRows[0].Cells[19].Value.ToString();
                String so = dgvmo.SelectedRows[0].Cells[20].Value.ToString();
                String prodline = dgvmo.SelectedRows[0].Cells[21].Value.ToString();
                String target = dgvmo.SelectedRows[0].Cells[23].Value.ToString();

                cmbcolor.Text = color;
                //cmbarticle.Text = article;
                cmbarticle.SelectedValue = ArtID;
                cmbsize.Text = size;
                cmbuser1.Text = user1;
                cmbuser2.Text = user2;
                cmbuser3.Text = user3;
                cmbuser4.Text = user4;
                cmbuser5.Text = user5;
                cmbuser6.Text = user6;
                cmbuser7.Text = user7;
                cmbuser8.Text = user8;
                cmbuser9.Text = user9;
                cmbuser10.Text = user10;
                txtquantity.Text = qty;
                dateshipment.Text = ship_date;
                txtshipmentdest.Text = ship_dest;
                cmbshippingmode.Text = ship_mode;
                txtpurchaseorder.Text = po;
                txtsalesorder.Text = so;
                cmbprodline.Text = prodline;
                txttarget.Text = target;
                //radButton4.Enabled = false;
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //check if quantity is integer
            Regex r = new Regex("^[0-9]*$");
            if (!r.IsMatch(txtquantity.Text))
            {
                radLabel15.Text = "Invalid Quantity value. Example : 35";
                txtquantity.Text = "";
                return;
            }

            //check if target quantity is integer
            if (!r.IsMatch(txttarget.Text))
            {
                radLabel15.Text = "Invalid Target Quantity value. Example : 35";
                txttarget.Text = "";
                return;
            }

            if (txtpurchaseorder.Text == "" || txtsalesorder.Text == "" || cmbprodline.Text == "" || cmbprodline.Text == "--SELECT-" || txtshipmentdest.Text == "")
            {
                radLabel15.Text = "Please Fill all the Fields";
                return;
            }

            //special fields
            String u1 = "";
            String u2 = "";
            String u3 = "";
            String u4 = "";
            String u5 = "";
            String u6 = "";
            String u7 = "";
            String u8 = "";
            String u9 = "";
            String u10 = "";

            //check if color is selected
            if (cmbcolor.Text == "--SELECT--")
            {
                radLabel15.Text = "Please Select the Color.";
                return;
            }

            //check if article is selected
            if (cmbarticle.Text == "--SELECT--")
            {
                radLabel15.Text = "Please Select the Color.";
                return;
            }

            //check if size is selected
            if (cmbsize.Text == "--SELECT--")
            {
                radLabel15.Text = "Please Select the Size.";
                return;
            }

            //check if quantity is entered
            if (txtquantity.Text == "")
            {
                radLabel15.Text = "Please Enter the Quantity.";
                return;
            }

            //check if special field are selected
            if (cmbuser1.Text != "(Optional)")
            {
                u1 = cmbuser1.Text;
            }

            if (cmbuser2.Text != "(Optional)")
            {
                u2 = cmbuser2.Text;
            }

            if (cmbuser3.Text != "(Optional)")
            {
                u3 = cmbuser3.Text;
            }

            if (cmbuser4.Text != "(Optional)")
            {
                u4 = cmbuser4.Text;
            }

            if (cmbuser4.Text != "(Optional)")
            {
                u4 = cmbuser4.Text;
            }

            if (cmbuser5.Text != "(Optional)")
            {
                u5 = cmbuser5.Text;
            }

            if (cmbuser6.Text != "(Optional)")
            {
               
                u6 = cmbuser6.Text;
            }
            if (cmbuser7.Text != "(Optional)")
            {
                u7 = cmbuser7.Text;
            }

            if (cmbuser8.Text != "(Optional)")
            {
                u8 = cmbuser8.Text;
            }

            if (cmbuser9.Text != "(Optional)")
            {
                u9 = cmbuser9.Text;
            }

            if (cmbuser10.Text != "(Optional)")
            {
                u10 = cmbuser10.Text;
            }

            Int32 n = 0;

            //check if mo details already exists
            for (int i = 0; i < dgvmo.Rows.Count; i++)
            {
                if (dgvmo.Rows[i].Cells[0].Value.ToString().Equals(cmbcolor.Text) && dgvmo.Rows[i].Cells[1].Value.ToString().Equals(cmbarticle.Text) && dgvmo.Rows[i].Cells[2].Value.ToString().Equals(cmbsize.Text) && dgvmo.Rows[i].Cells[3].Value.ToString().Equals(u1) && dgvmo.Rows[i].Cells[4].Value.ToString().Equals(u2) && dgvmo.Rows[i].Cells[5].Value.ToString().Equals(u3) && dgvmo.Rows[i].Cells[6].Value.ToString().Equals(u4) && dgvmo.Rows[i].Cells[7].Value.ToString().Equals(u5) && dgvmo.Rows[i].Cells[8].Value.ToString().Equals(u6) && dgvmo.Rows[i].Cells[9].Value.ToString().Equals(u7) && dgvmo.Rows[i].Cells[10].Value.ToString().Equals(u8) && dgvmo.Rows[i].Cells[11].Value.ToString().Equals(u9) && dgvmo.Rows[i].Cells[12].Value.ToString().Equals(u10) && dgvmo.Rows[i].Cells[13].Value.ToString().Equals(txtquantity.Text))
                {
                    dgvmo.Rows[i].IsSelected = true;
                    radLabel15.Text = "Row Already Exists";
                    return;
                }
            }

            //get max of sequence for the mo
            SqlCommand cmd = new SqlCommand("select MAX(I_SEQ_NO) from MO_DETAILS where V_MO_NO='" + txtmo.Text + "'", dc.con);
            if (cmd.ExecuteScalar() + "" != "")
            {
                n = int.Parse(cmd.ExecuteScalar().ToString());
            }
            n += 1;

            String artID = cmbarticle.SelectedValue.ToString();
            String artDesc = "";

            //get article description
            cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID = '" + artID + "'", dc.con);
            SqlDataReader sdr1 = cmd.ExecuteReader();
            if (sdr1.Read())
            {
                artDesc = sdr1.GetValue(0).ToString();
            }
            sdr1.Close();

            ////add the next moline
            //MO.Rows.Add(cmbcolor.Text, artID, artDesc, cmbsize.Text, u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, txtquantity.Text, dateshipment.Value.ToString("yyyy-MM-dd"), txtshipmentdest.Text, cmbshippingmode.Text, txtpurchaseorder.Text, txtsalesorder.Text, cmbprodline.Text, n, txttarget.Text);
            //dgvmo.DataSource = MO;

            if (txtmo.Text != "" && cmbcustomer.Text != "--SELECT--")
            {
                //get customer id
                String cust = "";
                cmd = new SqlCommand("select V_CUSTOMER_ID from CUSTOMER_DB where V_CUSTOMER_NAME='" + cmbcustomer.Text + "'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cust = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //update customer details for the mo
                cmd = new SqlCommand("update MO set V_CUSTOMER_ID='" + cust + "' where V_MO_NO='" + txtmo.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                String color = "";
                String article = "";
                String size = "";
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
                String qty = "";
                String ship_date = "";
                String ship_dest = "";
                String ship_mode = "";
                String po = "";
                String so = "";
                String prodline = "";

                color = cmbcolor.Text;
                article = cmbarticle.Text;
                size = cmbsize.Text;
                user1 = cmbuser1.Text;
                user2 = cmbuser2.Text;
                user3 = cmbuser3.Text;
                user4 = cmbuser4.Text;
                user5 = cmbuser5.Text;
                user6 = cmbuser6.Text;
                user7 = cmbuser7.Text;
                user8 = cmbuser8.Text;
                user9 = cmbuser9.Text;
                user10 = cmbuser10.Text;
                qty = txtquantity.Text;
                ship_date = dateshipment.Value.ToString("yyyy-MM-dd");
                ship_dest = txtshipmentdest.Text;
                ship_mode = cmbshippingmode.Text;
                po = txtpurchaseorder.Text;
                so = txtsalesorder.Text;
                prodline = cmbprodline.Text;
                String target = txttarget.Text;

                //get id for the masters
                cmd = new SqlCommand("select V_COLOR_ID from COLOR_DB where V_COLOR_DESC='" + color + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    color = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                ////get id for the masters
                //cmd = new SqlCommand("select V_ARTICLE_ID from ARTICLE_DB where V_ARTICLE_DESC='" + article + "'", dc.con);
                //sdr = cmd.ExecuteReader();
                //if (sdr.Read())
                //{
                //    article = sdr.GetValue(0).ToString();
                //}
                //sdr.Close();

                //get id for the masters
                cmd = new SqlCommand("select V_SIZE_ID from SIZE_DB where V_SIZE_DESC='" + size + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    size = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get id for the masters
                cmd = new SqlCommand("select V_USER_ID from USER_DEF1_DB where V_DESC='" + user1 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user1 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get id for the masters
                cmd = new SqlCommand("select V_USER_ID from USER_DEF2_DB where V_DESC='" + user2 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user2 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get id for the masters
                cmd = new SqlCommand("select V_USER_ID from USER_DEF3_DB where V_DESC='" + user3 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user3 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get id for the masters
                cmd = new SqlCommand("select V_USER_ID from USER_DEF4_DB where V_DESC='" + user4 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user4 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get id for the masters
                cmd = new SqlCommand("select V_USER_ID from USER_DEF5_DB where V_DESC='" + user5 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user5 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get id for the masters
                cmd = new SqlCommand("select V_USER_ID from USER_DEF6_DB where V_DESC='" + user6 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user6 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get id for the masters
                cmd = new SqlCommand("select V_USER_ID from USER_DEF7_DB where V_DESC='" + user7 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user7 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get id for the masters
                cmd = new SqlCommand("select V_USER_ID from USER_DEF8_DB where V_DESC='" + user8 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user8 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get id for the masters
                cmd = new SqlCommand("select V_USER_ID from USER_DEF9_DB where V_DESC='" + user9 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user9 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get id for the masters
                cmd = new SqlCommand("select V_USER_ID from USER_DEF10_DB where V_DESC='" + user10 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user10 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //insert into mo details
                cmd = new SqlCommand("insert into MO_DETAILS values('" + txtmo.Text + "','" + color + "','" + n + "','" + n + "','" + qty + "','" + artID + "','" + size + "','" + user1 + "','" + user2 + "','" + user3 + "','" + user4 + "','" + user5 + "','" + user6 + "','" + user7 + "','" + user8 + "','" + user9 + "','" + user10 + "','" + ship_date + "','" + ship_dest + "','" + ship_mode + "','" + po + "','" + so + "','NEW','" + prodline + "','" + target + "','1','VERSION-1','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", dc.con);
                cmd.ExecuteNonQuery();

                OpenMO();
                radLabel15.Text = "Records Updated";
                //ClearData();
            }
            else
            {
                radLabel15.Text = "Please All the Fields";
            }
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            DialogResult result = RadMessageBox.Show("Are you sure to delete this record?", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
            if (result.Equals(DialogResult.No))
            {
                return;
            }

            try
            {
                //check if user has selected a moline
                String moline = "";
                if (dgvmo.SelectedRows.Count > 0) // make sure user select at least 1 row 
                {
                    int idx = dgvmo.SelectedRows[0].Index;
                    moline = dgvmo.SelectedRows[0].Cells[22].Value.ToString();
                    Int32 modtID = int.Parse(dgvmo.SelectedRows[0].Cells[0].Value.ToString());
                    SqlCommand cmd = new SqlCommand("select count(*) from HANGER_HISTORY where MO_NO='" + txtmo.Text + "' and MO_LINE='" + moline + "'", dc.con);
                    int count = int.Parse(cmd.ExecuteScalar() + "");

                    if (count == 0)
                    {
                        //cmd = new SqlCommand("delete from MO_DETAILS where V_MO_NO='" + txtmo.Text + "' and V_MO_LINE='" + moline + "'", dc.con);
                        cmd = new SqlCommand("delete from MO_DETAILS where I_ID=" + modtID, dc.con);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        radLabel15.Text = "MO : " + txtmo.Text + " MO DETAILS : " + moline + " Already used for Production";
                        return;
                    }

                    //int idx = dgvmo.CurrentCell.RowIndex;
                    MO.Rows[idx].Delete();
                    dgvmo.DataSource = MO;

                    ClearData();
                    radLabel15.Text = "Record successfully deleted!";

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
                
            }



                    
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            RowSelected();   //get selected modetails
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

        private void btnaddarticle_Click(object sender, EventArgs e)
        {
            //open master
            Masters am = new Masters();
            am.Show();
            am.Form_Location1("Article");

            refresh = 1;
        }

        private void btnaddcolor_Click(object sender, EventArgs e)
        {
            //open master
            Masters cm = new Masters();
            cm.Show();
            cm.Form_Location1("Color");

            refresh = 1;
        }

        private void btnaddsize_Click(object sender, EventArgs e)
        {
            //open master
            Masters sm = new Masters();
            sm.Show();
            sm.Form_Location1("Size");

            refresh = 1;
        }

        private void btnadduser1_Click(object sender, EventArgs e)
        {
            //open master
            Masters ud = new Masters();
            ud.Show();
            ud.Form_Location1("User1");
            refresh = 1;
        }

        private void btnadduser2_Click(object sender, EventArgs e)
        {
            //open master
            Masters ud = new Masters();
            ud.Show();
            ud.Form_Location1("User2");

            refresh = 1;
        }

        private void btnadduser3_Click(object sender, EventArgs e)
        {
            //open master
            Masters ud = new Masters();
            ud.Show();
            ud.Form_Location1("User3");

            refresh = 1;
        }

        private void btnadduser4_Click(object sender, EventArgs e)
        {
            //open master
            Masters ud = new Masters();
            ud.Show();
            ud.Form_Location1("User4");

            refresh = 1;
        }

        private void btnadduser5_Click(object sender, EventArgs e)
        {
            //open master
            Masters ud = new Masters();
            ud.Show();
            ud.Form_Location1("User5");

            refresh = 1;
        }

        private void btnadduser6_Click(object sender, EventArgs e)
        {
            //open master
            Masters ud = new Masters();
            ud.Show();
            ud.Form_Location1("User6");

            refresh = 1;
        }

        private void btnadduser7_Click(object sender, EventArgs e)
        {
            //open master
            Masters ud = new Masters();
            ud.Show();
            ud.Form_Location1("User7");

            refresh = 1;
        }

        private void btnadduser8_Click(object sender, EventArgs e)
        {
            //open master
            Masters ud = new Masters();
            ud.Show();
            ud.Form_Location1("User8");

            refresh = 1;
        }

        private void btnadduser9_Click(object sender, EventArgs e)
        {
            //open master
            Masters ud = new Masters();
            ud.Show();
            ud.Form_Location1("User9");

            refresh = 1;
        }

        private void btnadduser10_Click(object sender, EventArgs e)
        {
            //open master
            Masters ud = new Masters();
            ud.Show();
            ud.Form_Location1("User10");

            refresh = 1;
        }

        private void addprodline_Click(object sender, EventArgs e)
        {
            //open master
            Setup pm = new Setup();
            pm.Show();
            pm.Form_Location("ProdLine");

            refresh = 1;
        }

        private void radButton4_Click_1(object sender, EventArgs e)
        {
            //open master
            Masters cm = new Masters();
            cm.Show();
            cm.Form_Location1("Customer");

            refresh = 1;
        }

        private void Edit_MO_Click(object sender, EventArgs e)
        {
            Referesh();  //refresh dropdownlist
        }

        String theme = "";
        private void Edit_MO_Initialized(object sender, EventArgs e)
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

            //change form language
            SqlDataAdapter sda = new SqlDataAdapter("select " + Lang + " from Language where Form='AddMO' order by Item_No", dc.con);
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
                lblprodline.Text = dt.Rows[7][0].ToString() + " :";
                lblcolor.Text = dt.Rows[8][0].ToString() + " :";
                lblarticle.Text = dt.Rows[9][0].ToString() + " :";
                lblsize.Text = dt.Rows[10][0].ToString() + " :";
                btnadd.Text = dt.Rows[11][0].ToString();
                btndelete.Text = dt.Rows[12][0].ToString();
                lblqty.Text = dt.Rows[15][0].ToString() + " :";
                btnedit.Text = dt.Rows[16][0].ToString();
                btnstnassign.Text = dt.Rows[17][0].ToString();
                save = dt.Rows[11][0].ToString();
                update = dt.Rows[18][0].ToString();
            }

            //change grid theme
            GridTheme(theme);
        }

        //set grid theme
        public void GridTheme(String theme)
        {
            dgvmo.ThemeName = theme;
        }

        public void OpenMO()
        {
            //get customer details for the mo
            SqlCommand cmd = new SqlCommand("select V_CUSTOMER_ID from MO where V_MO_NO='" + txtmo.Text + "'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                cmbcustomer.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get customer name
            cmd = new SqlCommand("select V_CUSTOMER_NAME from CUSTOMER_DB where V_CUSTOMER_ID='" + cmbcustomer.Text + "'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                cmbcustomer.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get mo details
            SqlDataAdapter sda = new SqlDataAdapter("select I_ID,V_MO_NO,V_COLOR_ID,I_ORDER_QTY,V_ARTICLE_ID,(select ARTICLE_DB.V_ARTICLE_DESC from ARTICLE_DB where ARTICLE_DB.V_ARTICLE_ID = MO_DETAILS.V_ARTICLE_ID) as V_ARTICLE_DESC,V_SIZE_ID,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10,FORMAT(D_SHIPMENT_DATE, 'yyyy-MM-dd') as D_SHIPMENT_DATE,V_SHIPPING_DEST,V_SHIPPING_MODE,V_PURCHASE_ORDER,V_SALES_ORDER,V_PROD_LINE,V_MO_LINE,I_TARGET_DAY from MO_DETAILS where V_MO_NO='" + txtmo.Text + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MO.Rows.Clear();

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
                //Int32 MoDtID = int.Parse(dt.Rows[i][0].ToString());
                //String color = dt.Rows[i][2].ToString();
                //String qty = dt.Rows[i][3].ToString();
                //String article = dt.Rows[i][4].ToString();
                //String size = dt.Rows[i][5].ToString();
                //user1 = dt.Rows[i][6].ToString();
                //user2 = dt.Rows[i][7].ToString();
                //user3 = dt.Rows[i][8].ToString();
                //user4 = dt.Rows[i][9].ToString();
                //user5 = dt.Rows[i][10].ToString();
                //user6 = dt.Rows[i][11].ToString();
                //user7 = dt.Rows[i][12].ToString();
                //user8 = dt.Rows[i][13].ToString();
                //user9 = dt.Rows[i][14].ToString();
                //user10 = dt.Rows[i][15].ToString();

                //String ship_date = dt.Rows[i][16].ToString();
                //DateTime DTship_date = DateTime.ParseExact(ship_date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                //String ship_dest = dt.Rows[i][17].ToString();
                //String ship_mode = dt.Rows[i][18].ToString();
                //String po = dt.Rows[i][19].ToString();
                //String so = dt.Rows[i][20].ToString();
                //String prodline = dt.Rows[i][21].ToString();
                //String moline = dt.Rows[i][22].ToString();
                //String target = dt.Rows[i][23].ToString();

                Int32 MoDtID = int.Parse(dt.Rows[i][0].ToString());
                String color = dt.Rows[i][2].ToString();
                String qty = dt.Rows[i][3].ToString();
                String artID = dt.Rows[i][4].ToString();
                String artDesc = dt.Rows[i][5].ToString();
                String size = dt.Rows[i][6].ToString();
                user1 = dt.Rows[i][7].ToString();
                user2 = dt.Rows[i][8].ToString();
                user3 = dt.Rows[i][9].ToString();
                user4 = dt.Rows[i][10].ToString();
                user5 = dt.Rows[i][11].ToString();
                user6 = dt.Rows[i][12].ToString();
                user7 = dt.Rows[i][13].ToString();
                user8 = dt.Rows[i][14].ToString();
                user9 = dt.Rows[i][15].ToString();
                user10 = dt.Rows[i][16].ToString();

                String ship_date = dt.Rows[i][17].ToString();
                String ship_dest = dt.Rows[i][18].ToString();
                String ship_mode = dt.Rows[i][19].ToString();
                String po = dt.Rows[i][20].ToString();
                String so = dt.Rows[i][21].ToString();
                String prodline = dt.Rows[i][22].ToString();
                String moline = dt.Rows[i][23].ToString();
                String target = dt.Rows[i][24].ToString();

                //get description for the masters
                cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    color = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                ////get description for the masters
                //cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + article + "'", dc.con);
                //sdr = cmd.ExecuteReader();
                //if (sdr.Read())
                //{
                //    article = sdr.GetValue(0).ToString();
                //}
                //sdr.Close();

                //get description for the masters
                cmd = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    size = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + user1 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user1 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + user2 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user2 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + user3 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user3 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + user4 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user4 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + user5 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user5 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + user6 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user6 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + user7 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user7 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + user8 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user8 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + user9 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user9 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //get description for the masters
                cmd = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + user10 + "'", dc.con);
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    user10 = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //add to grid
                MO.Rows.Add(MoDtID,color, artID, artDesc, size, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, qty, ship_date, ship_dest, ship_mode, po, so, prodline, moline, target);
            }
            dgvmo.DataSource = MO;
        }

        private void btnstnassign_Click(object sender, EventArgs e)
        {
            //goto station assign
            Station_Assign em = new Station_Assign();
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            //clear last select mo
            SqlCommand cmd = new SqlCommand("delete from LAST_SELECT_MO", dc.con);
            cmd.ExecuteNonQuery();

            //insert into last_select_mo
            cmd = new SqlCommand("insert into LAST_SELECT_MO values('" + txtmo.Text + "')", dc.con);
            cmd.ExecuteNonQuery();

            //open station assign form
            em.MdiParent = this.ParentForm;
            em.Show();
            this.Close();
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            //check if user has selected controller
            if (controller_name != "--SELECT--")
            {
                //check if quantity is integer
                Regex r = new Regex("^[0-9]*$");
                if (!r.IsMatch(txtquantity.Text))
                {
                    radLabel15.Text = "Invalid Quantity value. Example : 35";
                    txtquantity.Text = "";
                    return;
                }

                //check if target is integer
                if (!r.IsMatch(txttarget.Text))
                {
                    radLabel15.Text = "Invalid Target For Day value. Example : 35";
                    txttarget.Text = "";
                    return;
                }

                //special fields
                String u1 = "";
                String u2 = "";
                String u3 = "";
                String u4 = "";
                String u5 = "";
                String u6 = "";
                String u7 = "";
                String u8 = "";
                String u9 = "";
                String u10 = "";

                //check if color selected
                if (cmbcolor.Text == "--SELECT--")
                {
                    radLabel15.Text = "Please Select the Color.";
                    return;
                }

                //check article selected
                if (cmbarticle.Text == "--SELECT--")
                {
                    radLabel15.Text = "Please Select the Color.";
                    return;
                }

                //check if size is selected
                if (cmbsize.Text == "--SELECT--")
                {
                    radLabel15.Text = "Please Select the Size.";
                    return;
                }

                //check if quantity is entered
                if (txtquantity.Text == "")
                {
                    radLabel15.Text = "Please Enter the Quantity.";
                    return;
                }

                //check if special fields are selected
                if (cmbuser1.Text != "(Optional)")
                {
                    u1 = cmbuser1.Text;
                }

                if (cmbuser2.Text != "(Optional)")
                {
                    u2 = cmbuser2.Text;
                }

                if (cmbuser3.Text != "(Optional)")
                {
                    u3 = cmbuser3.Text;
                }

                if (cmbuser4.Text != "(Optional)")
                {
                    u4 = cmbuser4.Text;
                }

                if (cmbuser4.Text != "(Optional)")
                {
                    u4 = cmbuser4.Text;
                }

                if (cmbuser5.Text != "(Optional)")
                {
                    u5 = cmbuser5.Text;
                }

                if (cmbuser6.Text != "(Optional)")
                {
                    u6 = cmbuser6.Text;
                }

                if (cmbuser7.Text != "(Optional)")
                {
                    u7 = cmbuser7.Text;
                }

                if (cmbuser8.Text != "(Optional)")
                {
                    u8 = cmbuser8.Text;
                }

                if (cmbuser9.Text != "(Optional)")
                {
                    u9 = cmbuser9.Text;
                }

                if (cmbuser10.Text != "(Optional)")
                {
                    u10 = cmbuser10.Text;
                }


                Int32 n = 0;
                n = n + 1;

                if (txtmo.Text != "" && cmbcustomer.Text != "--SELECT--")
                {
                    String cust = "";

                    //get the selected customer id
                    SqlCommand cmd = new SqlCommand("select V_CUSTOMER_ID from CUSTOMER_DB where V_CUSTOMER_NAME='" + cmbcustomer.Text + "'", dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        cust = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //update mo
                    cmd = new SqlCommand("update MO set V_CUSTOMER_ID='" + cust + "' where V_MO_NO='" + txtmo.Text + "'", dc.con);
                    cmd.ExecuteNonQuery();

                    String color = "";
                    // String article = "";
                    String artID = "";
                    String artDesc = "";
                    String size = "";
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
                    String qty = "";
                    String ship_date = "";
                    String ship_dest = "";
                    String ship_mode = "";
                    String po = "";
                    String so = "";
                    String prodline = "";
                    String moline = "";
                   
                    color = cmbcolor.Text;
                    //article = cmbarticle.Text;
                    artID = cmbarticle.SelectedValue.ToString();
                    size = cmbsize.Text;
                    user1 = cmbuser1.Text;
                    user2 = cmbuser2.Text;
                    user3 = cmbuser3.Text;
                    user4 = cmbuser4.Text;
                    user5 = cmbuser5.Text;
                    user6 = cmbuser6.Text;
                    user7 = cmbuser7.Text;
                    user8 = cmbuser8.Text;
                    user9 = cmbuser9.Text;
                    user10 = cmbuser10.Text;
                    qty = txtquantity.Text;
                    ship_date = dateshipment.Value.ToString("yyyy-MM-dd");
                    ship_dest = txtshipmentdest.Text;
                    ship_mode = cmbshippingmode.Text;
                    po = txtpurchaseorder.Text;
                    so = txtsalesorder.Text;
                    prodline = cmbprodline.Text;
                    String target = txttarget.Text;

                    //get id for the masters
                    cmd = new SqlCommand("select V_COLOR_ID from COLOR_DB where V_COLOR_DESC='" + color + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        color = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get id for the masters
                    cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + artID + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        artDesc = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get id for the masters
                    cmd = new SqlCommand("select V_SIZE_ID from SIZE_DB where V_SIZE_DESC='" + size + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        size = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get id for the masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF1_DB where V_DESC='" + user1 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user1 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get id for the masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF2_DB where V_DESC='" + user2 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user2 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get id for the masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF3_DB where V_DESC='" + user3 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user3 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get id for the masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF4_DB where V_DESC='" + user4 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user4 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get id for the masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF5_DB where V_DESC='" + user5 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user5 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get id for the masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF6_DB where V_DESC='" + user6 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user6 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get id for the masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF7_DB where V_DESC='" + user7 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user7 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get id for the masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF8_DB where V_DESC='" + user8 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user8 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get id for the masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF9_DB where V_DESC='" + user9 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user9 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //get id for the masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF10_DB where V_DESC='" + user10 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user10 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    ////update mo details
                    //String strSql = "update MO_DETAILS set I_ORDER_QTY='" + qty + "',D_SHIPMENT_DATE='" + ship_date + "',V_SHIPPING_DEST='" + ship_dest + "',V_SHIPPING_MODE='" + ship_mode + "',V_PURCHASE_ORDER='" + po + "',V_SALES_ORDER='" + so + "',V_PROD_LINE='" + prodline + "' where V_MO_NO='" + txtmo.Text + "' and V_MO_LINE='" + moline + "'";
                    //cmd = new SqlCommand(strSql, dc.con);
                    //cmd.ExecuteNonQuery();

                    //String moline = "";
                    if (dgvmo.SelectedRows.Count > 0) // make sure user select at least 1 row 
                    {
                        moline = dgvmo.SelectedRows[0].Cells[22].Value.ToString();
                        Int32 modtid = int.Parse(dgvmo.SelectedRows[0].Cells[0].Value.ToString());  //get mo detail id

                        //add to datatable
                        //int idx = dgvmo.CurrentCell.RowIndex;
                        int idx = dgvmo.SelectedRows[0].Index;
                        MO.Rows[idx].Delete();
                        //MO.Rows.Add(cmbcolor.Text, artID, artDesc, cmbsize.Text, u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, txtquantity.Text, dateshipment.Text, txtshipmentdest.Text, cmbshippingmode.Text, txtpurchaseorder.Text, txtsalesorder.Text, cmbprodline.Text, moline, target);
                        

                        //update mo details
                        // string strSql2 = "update MO_DETAILS set V_COLOR_ID='" + color + "' , V_ARTICLE_ID='" + artID + "' ,V_SIZE_ID='" + size + "', V_USER_DEF1='" + user1 + "' , V_USER_DEF2='" + user2 + "' , V_USER_DEF3='" + user3 + "' , V_USER_DEF4='" + user4 + "' , V_USER_DEF5='" + user5 + "', V_USER_DEF6='" + user6 + "' , V_USER_DEF7='" + user7 + "' , V_USER_DEF8='" + user8 + "' , V_USER_DEF9='" + user9 + "' , V_USER_DEF10='" + user10 + "',I_ORDER_QTY='" + qty + "',D_SHIPMENT_DATE='" + ship_date + "',V_SHIPPING_DEST='" + ship_dest + "',V_SHIPPING_MODE='" + ship_mode + "',V_PURCHASE_ORDER='" + po + "',V_SALES_ORDER='" + so + "',V_PROD_LINE='" + prodline + "',I_TARGET_DAY='" + target + "',D_LAST_UPDATED='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where V_MO_NO='" + txtmo.Text + "' and V_MO_LINE='" + moline + "'";
                        string strSql2 = "update MO_DETAILS set V_COLOR_ID='" + color + "' , V_ARTICLE_ID='" + artID + "' ,V_SIZE_ID='" + size + "', V_USER_DEF1='" + user1 + "' , V_USER_DEF2='" + user2 + "' , V_USER_DEF3='" + user3 + "' , V_USER_DEF4='" + user4 + "' , V_USER_DEF5='" + user5 + "', V_USER_DEF6='" + user6 + "' , V_USER_DEF7='" + user7 + "' , V_USER_DEF8='" + user8 + "' , V_USER_DEF9='" + user9 + "' , V_USER_DEF10='" + user10 + "',I_ORDER_QTY='" + qty + "',D_SHIPMENT_DATE='" + ship_date + "',V_SHIPPING_DEST='" + ship_dest + "',V_SHIPPING_MODE='" + ship_mode + "',V_PURCHASE_ORDER='" + po + "',V_SALES_ORDER='" + so + "',V_PROD_LINE='" + prodline + "',I_TARGET_DAY='" + target + "',D_LAST_UPDATED='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where I_ID = " + modtid;
                        cmd = new SqlCommand(strSql2, dc.con);
                        cmd.ExecuteNonQuery();

                        //update quantity in controller
                        MySqlCommand cmd1 = new MySqlCommand("update prod set MAX_COUNT='" + qty + "' where MO_NO='" + txtmo.Text + "' and MO_LINE='" + moline + "'", dc.conn);
                        cmd1.ExecuteNonQuery();

                        MO.Rows.Add(modtid,cmbcolor.Text, artID, artDesc, cmbsize.Text, u1, u2, u3, u4, u5, u6, u7, u8, u9, u10, txtquantity.Text, ship_date, txtshipmentdest.Text, cmbshippingmode.Text, txtpurchaseorder.Text, txtsalesorder.Text, cmbprodline.Text, moline, target);
                        dgvmo.DataSource = MO;
                    }
                    radLabel15.Text = "Records Updated";
                    ClearData();
                }
                else
                {
                    radLabel15.Text = "Please All the Fields";
                }
            }
            else
            {
                radLabel15.Text = "Please Select a Controller";
            }
            //radButton4.Enabled = true;
        }

        //clear selected values
        public void ClearData()
        {
            cmbcolor.Text = "--SELECT--";
            cmbarticle.Text = "--SELECT--";
            cmbsize.Text = "--SELECT--";
            cmbuser1.Text = "--SELECT--";
            cmbuser2.Text = "--SELECT--";
            cmbuser3.Text = "--SELECT--";
            cmbuser4.Text = "--SELECT--";
            cmbuser5.Text = "--SELECT--";
            cmbuser6.Text = "--SELECT--";
            cmbuser7.Text = "--SELECT--";
            cmbuser8.Text = "--SELECT--";
            cmbuser9.Text = "--SELECT--";
            cmbuser10.Text = "--SELECT--";
            txtquantity.Text = "";
            txttarget.Text = "";
        }

        //refresh dropdownlist
        private void cmbcolor_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        //refresh dropdownlist
        private void cmbarticle_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        //refresh dropdownlist
        private void cmbsize_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        //refresh dropdownlist
        private void cmbuser1_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        //refresh dropdownlist
        private void cmbuser2_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        //refresh dropdownlist
        private void cmbuser3_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        //refresh dropdownlist
        private void cmbuser4_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        //refresh dropdownlist
        private void cmbuser5_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        //refresh dropdownlist
        private void cmbuser6_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        //refresh dropdownlist
        private void cmbuser7_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        //refresh dropdownlist
        private void cmbuser8_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        //refresh dropdownlist
        private void cmbuser9_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        //refresh dropdownlist
        private void cmbuser10_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        //refresh dropdownlist
        private void cmbcustomer_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        //refresh dropdownlist
        private void cmbprodline_Click(object sender, EventArgs e)
        {
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        public void select_controller()
        {
            dc.OpenConnection();   //open connection
            String ipaddress = "";
            String controller = "";

            //get the ip address and port number of the selected controller
            SqlCommand cmd = new SqlCommand("select V_CONTROLLER from Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                controller = sdr.GetValue(0).ToString();
                controller_name = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get ip address for the controller
            cmd = new SqlCommand("select V_CLUSTER_IP_ADDRESS from CLUSTER_DB where V_CLUSTER_ID='" + controller + "'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                ipaddress = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //return if there is no ip address
            if (ipaddress == "")
            {
                controller_name = "--SELECT--";
                return;
            }

            dc.Close_Connection();    //close connection if open
            dc.OpenMYSQLConnection(ipaddress);   //open connection
        }

        private void Edit_MO_FormClosed(object sender, FormClosedEventArgs e)
        {
            dc.Close_Connection();   //close connection on form close
        }

        private void dgvmo_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these themes are selected
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
