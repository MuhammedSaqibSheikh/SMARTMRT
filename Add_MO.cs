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
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Add_MO : Telerik.WinControls.UI.RadForm
    {
        public Add_MO()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }
        Database_Connection dc = new Database_Connection(); //Connection Class
        DataTable MO = new DataTable(); //Datatable for MO
        int refresh = 0; //Refresh DropDownList Flag
        String theme = ""; //Grid Theme

        private void Add_MO_Load(object sender, EventArgs e)
        {
            dgvmo.MasterTemplate.SelectLastAddedRow = false;
            RadMessageBox.SetThemeName("FluentDark"); //Theme for MessageBox
            this.AutoScroll = true;
            dateshipment.Text = DateTime.Now.ToString(); //Current Date for Shipment Date

            //Special Fields
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

            dc.OpenConnection();    //Open Connection        
            dgvmo.Visible = false;
            radPanel2.Visible = false;

            //Get Special Field Name
            SqlCommand cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF1' and V_ENABLED='TRUE'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser1.Text = sdr.GetValue(0).ToString() + " :";
                user1 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel2.Visible = false;
            }
            sdr.Close();

            //Get Special Field Name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser2.Text = sdr.GetValue(0).ToString() + " :";
                user2 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel3.Visible = false;
            }
            sdr.Close();

            //Get Special Field Name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser3.Text = sdr.GetValue(0).ToString() + " :";
                user3 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel4.Visible = false;
            }
            sdr.Close();

            //Get Special Field Name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser4.Text = sdr.GetValue(0).ToString() + " :";
                user4 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel5.Visible = false;
            }
            sdr.Close();

            //Get Special Field Name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser5.Text = sdr.GetValue(0).ToString() + " :";
                user5 = sdr.GetValue(0).ToString();
            }
            else
            {
                tableLayoutPanel6.Visible = false;
            }
            sdr.Close();

            //Get Special Field Name
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

            //Get Special Field Name
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

            //Get Special Field Name
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

            //Get Special Field Name
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

            //Get Special Field Name
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

            Referesh(); //Refresh DropDownList and Get All the Masters

            //Add Columns to Grid
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
            MO.Columns.Add("Target Quantity Day");

            dgvmo.DataSource = MO;
            //Hide the Special Fields which are not Enabled
            if (user1 == "")
            {
                dgvmo.Columns[4].IsVisible = false;
            }

            if (user2 == "")
            {
                dgvmo.Columns[5].IsVisible = false;
            }

            if (user3 == "")
            {
                dgvmo.Columns[6].IsVisible = false;
            }

            if (user4 == "")
            {
                dgvmo.Columns[7].IsVisible = false;
            }

            if (user5 == "")
            {
                dgvmo.Columns[8].IsVisible = false;
            }

            if (user6 == "")
            {
                dgvmo.Columns[9].IsVisible = false;
            }

            if (user7 == "")
            {
                dgvmo.Columns[10].IsVisible = false;
            }

            if (user8 == "")
            {
                dgvmo.Columns[11].IsVisible = false;
            }

            if (user9 == "")
            {
                dgvmo.Columns[12].IsVisible = false;
            }

            if (user10 == "")
            {
                dgvmo.Columns[13].IsVisible = false;
            }
        }

        public void Referesh()
        {
            String artID = "";
            //Get the Select Masters
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

            //Clear out the DropDownList
            cmbcolor.Items.Clear();
            cmbarticle.Items.Clear();
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
            cmbsize.Items.Clear();

            //Get the Masters and Add to DropDownList
            SqlDataAdapter sda = new SqlDataAdapter("Select V_COLOR_DESC from COLOR_DB", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbcolor.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            ////Get the Masters and Add to DropDownList
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

            //Get the Masters and Add to DropDownList
            sda = new SqlDataAdapter("Select V_SIZE_DESC from SIZE_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbsize.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //Get the Masters and Add to DropDownList
            sda = new SqlDataAdapter("Select V_CUSTOMER_NAME from CUSTOMER_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbcustomer.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //Get the Masters and Add to DropDownList
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF1_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser1.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //Get the Masters and Add to DropDownList
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF2_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser2.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //Get the Masters and Add to DropDownList
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF3_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser3.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //Get the Masters and Add to DropDownList
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF4_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser4.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //Get the Masters and Add to DropDownList
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF5_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser5.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //Get the Masters and Add to DropDownList
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF6_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser6.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //Get the Masters and Add to DropDownList
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF7_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser7.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //Get the Masters and Add to DropDownList
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF8_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser8.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //Get the Masters and Add to DropDownList
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF9_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser9.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //Get the Masters and Add to DropDownList
            sda = new SqlDataAdapter("Select V_DESC from USER_DEF10_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbuser10.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //Get the Masters and Add to DropDownList
            sda = new SqlDataAdapter("Select V_PROD_LINE from PROD_LINE_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbprodline.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //Select the Masters which was Selected Before
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

        private void radButton1_Click(object sender, EventArgs e)
        {
            //Check if the Quantity is Integer if not then Show Error
            Regex r = new Regex("^[0-9]*$");
            if (!r.IsMatch(txtquantity.Text))
            {
                radLabel15.Text = "Invalid Quantity value. Example : 35";
                txtquantity.Text = "";
                return;
            }

            //Special Fields
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

            //Check if Color is Selected
            if (cmbcolor.Text == "--SELECT--")
            {
                radLabel15.Text = "Please Select the Color.";
                return;
            }

            //Check if Article is Selected
            if (cmbarticle.Text == "--SELECT--")
            {
                radLabel15.Text = "Please Select the Article.";
                return;
            }

            //Check if Size is Selected
            if (cmbsize.Text == "--SELECT--")
            {
                radLabel15.Text = "Please Select the Size.";
                return;
            }

            //Check if Quantity is Empty
            if (txtquantity.Text == "")
            {
                radLabel15.Text = "Please Enter the Quantity.";
                return;
            }

            //Check if MONO is Empty
            if (txtmo.Text == "")
            {
                radLabel15.Text = "Please Enter the MO No.";
                return;
            }

            //Check if Production Line is Empty
            if (cmbprodline.Text == "")
            {
                radLabel15.Text = "Please Enter the Production Line.";
                return;
            }

            //Check if Target Quantity is Empty
            if (txttarget.Text == "")
            {
                radLabel15.Text = "Please Enter the Target Quantity for a Day.";
                return;
            }

            //Check if Special Field is Selected
            if (cmbuser1.Text != "(Optional)")
            {
                user1 = cmbuser1.Text;
            }

            //Check if Special Field is Selected
            if (cmbuser2.Text != "(Optional)")
            {
                user2 = cmbuser2.Text;
            }

            //Check if Special Field is Selected
            if (cmbuser3.Text != "(Optional)")
            {
                user3 = cmbuser3.Text;
            }

            //Check if Special Field is Selected
            if (cmbuser4.Text != "(Optional)")
            {
                user4 = cmbuser4.Text;
            }

            //Check if Special Field is Selected
            if (cmbuser5.Text != "(Optional)")
            {
                user5 = cmbuser5.Text;
            }

            //Check if Special Field is Selected
            if (cmbuser6.Text != "(Optional)")
            {
                user6 = cmbuser6.Text;
            }

            //Check if Special Field is Selected
            if (cmbuser7.Text != "(Optional)")
            {
                user7 = cmbuser7.Text;
            }

            //Check if Special Field is Selected
            if (cmbuser8.Text != "(Optional)")
            {
                user8 = cmbuser8.Text;
            }

            //Check if Special Field is Selected
            if (cmbuser9.Text != "(Optional)")
            {
                user9 = cmbuser9.Text;
            }

            //Check if Special Field is Selected
            if (cmbuser10.Text != "(Optional)")
            {
                user10 = cmbuser10.Text;
            }

            //Check if the MO Details is Already Added with same Details
            dgvmo.Visible = true;
            for (int i = 0; i < dgvmo.Rows.Count; i++)
            {
                if (dgvmo.Rows[i].Cells[0].Value.ToString().Equals(cmbcolor.Text) && dgvmo.Rows[i].Cells[1].Value.ToString().Equals(cmbarticle.Text) && dgvmo.Rows[i].Cells[2].Value.ToString().Equals(cmbsize.Text) && dgvmo.Rows[i].Cells[3].Value.ToString().Equals(user1) && dgvmo.Rows[i].Cells[4].Value.ToString().Equals(user2) && dgvmo.Rows[i].Cells[5].Value.ToString().Equals(user3) && dgvmo.Rows[i].Cells[6].Value.ToString().Equals(user4) && dgvmo.Rows[i].Cells[7].Value.ToString().Equals(user5) && dgvmo.Rows[i].Cells[8].Value.ToString().Equals(user6) && dgvmo.Rows[i].Cells[9].Value.ToString().Equals(user7) && dgvmo.Rows[i].Cells[10].Value.ToString().Equals(user8) && dgvmo.Rows[i].Cells[11].Value.ToString().Equals(user9) && dgvmo.Rows[i].Cells[12].Value.ToString().Equals(user10))
                {
                    dgvmo.Rows[i].IsSelected = true;
                    radLabel15.Text = "Row Already Exists";
                    return;
                }
            }

            String ArtID = cmbarticle.SelectedValue.ToString(); //get article id 
            String ArtDesc = cmbarticle.SelectedItem.ToString(); //get article Desc



            //else Add the MO Details
            //MO.Rows.Add(cmbcolor.Text, cmbarticle.Text, cmbsize.Text, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, txtquantity.Text, dateshipment.Value.ToString("yyyy-MM-dd"), txtshipmentdest.Text, cmbshippingmode.Text, txtpurchaseorder.Text, txtsalesorder.Text, cmbprodline.Text,txttarget.Text);
            MO.Rows.Add(cmbcolor.Text, ArtID, ArtDesc, cmbsize.Text, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, txtquantity.Text, dateshipment.Value.ToString("yyyy-MM-dd"), txtshipmentdest.Text, cmbshippingmode.Text, txtpurchaseorder.Text, txtsalesorder.Text, cmbprodline.Text, txttarget.Text);
            dgvmo.DataSource = MO;
            btnsave.ForeColor = Color.Red;
            btnsave.Enabled = true;
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            //Check if User has Selected a Row
            if (dgvmo.SelectedRows.Count == 0)
            {
                radLabel15.Text = "Please Select a Row";
                return;
            }

            //Remove the Selected Row
            btnsave.ForeColor = Color.Red;
            dgvmo.Rows.RemoveAt(dgvmo.SelectedRows[0].Index);
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

        private void radButton4_Click(object sender, EventArgs e)
        {
            //Check if the Fields are Empty
            if (txtmo.Text != "" && cmbcustomer.Text != "--SELECT--" && dgvmo.Rows.Count > 0)
            {
                //Get the ID of the Masters
                String cust = "";
                SqlCommand cmd = new SqlCommand("select V_CUSTOMER_ID from CUSTOMER_DB where V_CUSTOMER_NAME='" + cmbcustomer.Text + "'", dc.con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    cust = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //Check if the MO Contains any of these Charecters *+'
                if (txtmo.Text.Contains("*") || txtmo.Text.Contains("+") || txtmo.Text.Contains("'"))
                {
                    radLabel15.Text = "Invalid Charecters in MO No";
                    return;
                }

                //Check if the MONO Already Exists
                cmd = new SqlCommand("Select count(*) from MO where V_MO_NO='" + txtmo.Text + "'", dc.con);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if (count != 0)
                {
                    radLabel15.Text = "MO No Already Exists";
                    return;
                }

                //Insert into MO
                cmd = new SqlCommand("insert into MO values('" + txtmo.Text + "','" + cust + "','NEW')", dc.con);
                cmd.ExecuteNonQuery();

                //Get all the MO Details
                for (int i = 0; i < dgvmo.Rows.Count; i++)
                {
                    String color = dgvmo.Rows[i].Cells[0].Value.ToString();
                    //String article = dgvmo.Rows[i].Cells[1].Value.ToString();
                    String artID = dgvmo.Rows[i].Cells[1].Value.ToString();
                    String size = dgvmo.Rows[i].Cells[3].Value.ToString();
                    String user1 = dgvmo.Rows[i].Cells[4].Value.ToString();
                    String user2 = dgvmo.Rows[i].Cells[5].Value.ToString();
                    String user3 = dgvmo.Rows[i].Cells[6].Value.ToString();
                    String user4 = dgvmo.Rows[i].Cells[7].Value.ToString();
                    String user5 = dgvmo.Rows[i].Cells[8].Value.ToString();
                    String user6 = dgvmo.Rows[i].Cells[9].Value.ToString();
                    String user7 = dgvmo.Rows[i].Cells[10].Value.ToString();
                    String user8 = dgvmo.Rows[i].Cells[11].Value.ToString();
                    String user9 = dgvmo.Rows[i].Cells[12].Value.ToString();
                    String user10 = dgvmo.Rows[i].Cells[13].Value.ToString();
                    String qty = dgvmo.Rows[i].Cells[14].Value.ToString();
                    String ship_date = dgvmo.Rows[i].Cells[15].Value.ToString();
                    String ship_dest = dgvmo.Rows[i].Cells[16].Value.ToString();
                    String ship_mode = dgvmo.Rows[i].Cells[17].Value.ToString();
                    String po = dgvmo.Rows[i].Cells[18].Value.ToString();
                    String so = dgvmo.Rows[i].Cells[19].Value.ToString();
                    String prodline = dgvmo.Rows[i].Cells[20].Value.ToString();
                    String target = dgvmo.Rows[i].Cells[21].Value.ToString();

                    //Get the ID of the Masters
                    cmd = new SqlCommand("select V_COLOR_ID from COLOR_DB where V_COLOR_DESC='" + color + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        color = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    ////Get the ID of the Masters
                    //cmd = new SqlCommand("select V_ARTICLE_ID from ARTICLE_DB where V_ARTICLE_DESC='" + article + "'", dc.con);
                    //sdr = cmd.ExecuteReader();
                    //if (sdr.Read())
                    //{
                    //    article = sdr.GetValue(0).ToString();
                    //}
                    //sdr.Close();

                    //Get the ID of the Masters
                    cmd = new SqlCommand("select V_SIZE_ID from SIZE_DB where V_SIZE_DESC='" + size + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        size = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //Get the ID of the Masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF1_DB where V_DESC='" + user1 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user1 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //Get the ID of the Masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF2_DB where V_DESC='" + user2 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user2 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //Get the ID of the Masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF3_DB where V_DESC='" + user3 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user3 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //Get the ID of the Masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF4_DB where V_DESC='" + user4 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user4 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //Get the ID of the Masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF5_DB where V_DESC='" + user5 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user5 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //Get the ID of the Masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF6_DB where V_DESC='" + user6 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user6 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //Get the ID of the Masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF7_DB where V_DESC='" + user7 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user7 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //Get the ID of the Masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF8_DB where V_DESC='" + user8 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user8 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //Get the ID of the Masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF9_DB where V_DESC='" + user9 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user9 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //Get the ID of the Masters
                    cmd = new SqlCommand("select V_USER_ID from USER_DEF10_DB where V_DESC='" + user10 + "'", dc.con);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        user10 = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    int n = i + 1;

                    //Insert into MO Details
                    //cmd = new SqlCommand("insert into MO_DETAILS values('" + txtmo.Text + "','" + color + "','" + n + "','" + n + "','" + qty + "','" + article + "','" + size + "','" + user1 + "','" + user2 + "','" + user3 + "','" + user4 + "','" + user5 + "','" + user6 + "','" + user7 + "','" + user8 + "','" + user9 + "','" + user10 + "','" + ship_date + "','" + ship_dest + "','" + ship_mode + "','" + po + "','" + so + "','NEW','" + prodline + "','" + target + "','1','VERSION-1','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", dc.con);
                    cmd = new SqlCommand("insert into MO_DETAILS values('" + txtmo.Text + "','" + color + "','" + n + "','" + n + "','" + qty + "','" + artID + "','" + size + "','" + user1 + "','" + user2 + "','" + user3 + "','" + user4 + "','" + user5 + "','" + user6 + "','" + user7 + "','" + user8 + "','" + user9 + "','" + user10 + "','" + ship_date + "','" + ship_dest + "','" + ship_mode + "','" + po + "','" + so + "','NEW','" + prodline + "','" + target + "','1','VERSION-1','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')", dc.con);
                    cmd.ExecuteNonQuery();
                }

                radLabel15.Text = "Records Updated";
                btnsave.ForeColor = Color.Lime;
                btnstnassign.Visible = true;
                this.Close();
            }
            else
            {
                radLabel15.Text = "Please All the Fields";
            }
        }

        private void btnaddcolor_Click(object sender, EventArgs e)
        {
            //Open Color Master
            Masters cm = new Masters();
            cm.Show();
            cm.Form_Location1("Color");

            refresh = 1;
        }

        private void Add_MO_MouseClick(object sender, MouseEventArgs e)
        {
            //Referesh();
        }

        private void btnaddarticle_Click(object sender, EventArgs e)
        {
            //Open Article Master
            Masters am = new Masters();
            am.Show();
            am.Form_Location1("Article");

            refresh = 1;
        }

        private void btnadduser1_Click(object sender, EventArgs e)
        {
            //Open User1 Master
            Masters us = new Masters();
            us.Show();
            us.Form_Location1("User1");

            refresh = 1;
        }

        private void btnadduser2_Click(object sender, EventArgs e)
        {
            //Open User2 Master
            Masters us = new Masters();
            us.Show();
            us.Form_Location1("User2");

            refresh = 1;
        }

        private void btnadduser3_Click(object sender, EventArgs e)
        {
            //Open User3 Master
            Masters us = new Masters();
            us.Show();
            us.Form_Location1("User3");

            refresh = 1;
        }

        private void btnadduser4_Click(object sender, EventArgs e)
        {
            //Open User4 Master
            Masters us = new Masters();
            us.Show();
            us.Form_Location1("User4");

            refresh = 1;
        }

        private void btnadduser5_Click(object sender, EventArgs e)
        {
            //Open User5 Master
            Masters us = new Masters();
            us.Show();
            us.Form_Location1("User5");

            refresh = 1;
        }

        private void addprodline_Click(object sender, EventArgs e)
        {
            //Open Prod Line Master
            Setup pm = new Setup();
            pm.Show();
            pm.Form_Location("ProdLine");

            refresh = 1;
        }

        private void radButton5_Click(object sender, EventArgs e)
        {
            //Clear all the Text Fields and DropDownList
            Clear();
        }

        public void Clear()
        {
            //Clear Everything
            cmbcolor.Text = "--SELECT--";
            cmbarticle.Text = "--SELECT--";
            cmbcustomer.Text = "--SELECT--";
            cmbsize.Text = "--SELECT--";

            cmbuser1.Text = "(Optional)";
            cmbuser2.Text = "(Optional)";
            cmbuser3.Text = "(Optional)";
            cmbuser4.Text = "(Optional)";
            cmbuser5.Text = "(Optional)";
            cmbuser6.Text = "(Optional)";
            cmbuser7.Text = "(Optional)";
            cmbuser8.Text = "(Optional)";
            cmbuser9.Text = "(Optional)";
            cmbuser10.Text = "(Optional)";

            txtmo.Text = "";
            txtpurchaseorder.Text = "";
            txtquantity.Text = "";
            txtsalesorder.Text = "";
            txtshipmentdest.Text = "";
        }

        private void btnaddsize_Click(object sender, EventArgs e)
        {
            //Open Size Master
            Masters sz = new Masters();
            sz.Show();
            sz.Form_Location1("Size");

            refresh = 1;
        }

        private void btnadduser6_Click(object sender, EventArgs e)
        {
            //Open User6 Master
            Masters us = new Masters();
            us.Show();
            us.Form_Location1("User6");

            refresh = 1;
        }

        private void btnadduser7_Click(object sender, EventArgs e)
        {
            //Open User7 Master
            Masters us = new Masters();
            us.Show();
            us.Form_Location1("User7");

            refresh = 1;
        }

        private void btnadduser8_Click(object sender, EventArgs e)
        {
            //Open User8 Master
            Masters us = new Masters();
            us.Show();
            us.Form_Location1("User8");

            refresh = 1;
        }

        private void btnadduser9_Click(object sender, EventArgs e)
        {
            //Open User9 Master
            Masters us = new Masters();
            us.Show();
            us.Form_Location1("User9");

            refresh = 1;
        }

        private void btnadduser10_Click(object sender, EventArgs e)
        {
            //Open User10 Master
            Masters us = new Masters();
            us.Show();
            us.Form_Location1("User10");

            refresh = 1;
        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            //Open Customer Master
            Masters cm = new Masters();
            cm.Show();
            cm.Form_Location1("Customer");

            refresh = 1;
        }

        private void Add_MO_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();

            //Get the Language and Theme
            String Lang = "";            
            SqlCommand cmd = new SqlCommand("SELECT Language,ThemeName FROM Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Lang = sdr.GetValue(0).ToString(); //Language
                theme = sdr.GetValue(1).ToString(); //Theme
            }
            sdr.Close();

            //Set Language for the Form
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
                btnsave.Text = dt.Rows[13][0].ToString();
                btnclose.Text = dt.Rows[14][0].ToString();
                lblqty.Text = dt.Rows[15][0].ToString() + " :";
                btnstnassign.Text = dt.Rows[17][0].ToString();
            }

            //Change Grid Theme
            GridTheme(theme);
        }

        public void GridTheme(String theme)
        {
            dgvmo.ThemeName = theme; //Set Grid Theme
        }

        private void btnstnassign_Click(object sender, EventArgs e)
        {
            //Delete the Last Selected MO
            SqlCommand cmd = new SqlCommand("delete from LAST_SELECT_MO", dc.con);
            cmd.ExecuteNonQuery();

            //Insert the new Selected MO
            cmd = new SqlCommand("insert into LAST_SELECT_MO values('" + txtmo.Text + "')", dc.con);
            cmd.ExecuteNonQuery();

            //Open Station Assign form
            Station_Assign em = new Station_Assign();
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
            em.MdiParent = this.ParentForm;
            em.Show();
            this.Close();
        }

        private void txtmo_TextChanged(object sender, EventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            if (txtmo.Text == "" && txtquantity.Text == "" && txtsalesorder.Text == "" && txtshipmentdest.Text == "" && txtpurchaseorder.Text == "" )
            {
                btnsave.ForeColor = Color.Lime;
            }
            else
            {
                btnsave.ForeColor = Color.Red;
            }
        }

        private void txtpurchaseorder_TextChanged(object sender, EventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            if (txtmo.Text == "" && txtquantity.Text == "" && txtsalesorder.Text == "" && txtshipmentdest.Text == "" && txtpurchaseorder.Text == "")
            {
                btnsave.ForeColor = Color.Lime;
            }
            else
            {
                btnsave.ForeColor = Color.Red;
            }
        }

        private void txtsalesorder_TextChanged(object sender, EventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            if (txtmo.Text == "" && txtquantity.Text == "" && txtsalesorder.Text == "" && txtshipmentdest.Text == "" && txtpurchaseorder.Text == "")
            {
                btnsave.ForeColor = Color.Lime;
            }
            else
            {
                btnsave.ForeColor = Color.Red;
            }
        }

        private void txtshipmentdest_TextChanged(object sender, EventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            if (txtmo.Text == "" && txtquantity.Text == "" && txtsalesorder.Text == "" && txtshipmentdest.Text == "" && txtpurchaseorder.Text == "")
            {
                btnsave.ForeColor = Color.Lime;
            }
            else
            {
                btnsave.ForeColor = Color.Red;
            }
        }

        private void txtquantity_TextChanged(object sender, EventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            if (txtmo.Text == "" && txtquantity.Text == "" && txtsalesorder.Text == "" && txtshipmentdest.Text == "" && txtpurchaseorder.Text == "")
            {
                btnsave.ForeColor = Color.Lime;
            }
            else
            {
                btnsave.ForeColor = Color.Red;
            }
        }

        private void cmbcolor_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbarticle_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbsize_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbuser1_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbuser2_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbuser3_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbuser4_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbuser5_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbuser6_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbuser7_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbuser8_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbuser9_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbuser10_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbcustomer_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbshippingmode_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void cmbprodline_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //If the Any Field has Changed then Change the Save Button Color
            btnsave.ForeColor = Color.Red;
        }

        private void dateshipment_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void Add_MO_FormClosing(object sender, FormClosingEventArgs e)
        {
            //If the Any Field has Changed then Show the Message Box
            if (btnsave.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved MO. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsave.PerformClick();
                    e.Cancel = true;
                }
            }
        }

        private void cmbcolor_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void cmbarticle_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void cmbsize_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void cmbuser1_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void cmbuser2_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void cmbuser3_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void cmbuser4_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void cmbuser5_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void cmbuser6_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void cmbuser7_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void cmbuser8_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void cmbuser9_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void cmbuser10_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void cmbcustomer_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void cmbprodline_Click(object sender, EventArgs e)
        {
            //if the Refresh Flag is 1 then Update the DropDownLists
            if (refresh == 1)
            {
                Referesh();
            }

            refresh = 0;
        }

        private void dgvmo_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //If any of this Theme is Selected then Change the Fore Color of Grid
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
