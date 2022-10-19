using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SMARTMRT
{
    public partial class MO_Operation_Report : Telerik.WinControls.UI.RadForm
    {
        public MO_Operation_Report()
        {
            InitializeComponent();
        }

        String theme = "";
        Database_Connection dc = new Database_Connection();    //connection class
        DataTable MO = new DataTable();
        DataTable dt_op = new DataTable();
        DataTable dt_mo = new DataTable();
        String controller_name = "";

        private void MO_Operation_Report_Load(object sender, EventArgs e)
        {
            RadMessageBox.SetThemeName("FluentDark");   //set message box theme
            dgvmoline.MasterTemplate.SelectLastAddedRow = false;
            dgvoperation.MasterTemplate.SelectLastAddedRow = false;
            //disable close button on search in grid
            dgvmoline.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvoperation.MasterView.TableSearchRow.ShowCloseButton = false;

            select_controller();  //get the selected controller

            //check if the controller is selected
            if (controller_name == "")
            {
                RadMessageBox.Show("Please Select a controller.", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                this.Close();
            }

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

            //get special field names            
            SqlCommand cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF1' and V_ENABLED='TRUE'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user1 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field names   
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user2 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field names   
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user3 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field names   
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user4 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field names   
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user5 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field names   
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF6' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user6 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field names   
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF7' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user7 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field names   
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF8' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user8 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field names   
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF9' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user9 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field names   
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF10' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user10 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //add columns to MO table
            MO.Columns.Add("Select", System.Type.GetType("System.Boolean"));
            MO.Columns.Add("Color");
            MO.Columns.Add("Size");
            MO.Columns.Add("Article");
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

            dt_mo.Columns.Add("MO_NO");
            dt_mo.Columns.Add("MO_DETAILS");
            dt_mo.Columns.Add("COLOR");
            dt_mo.Columns.Add("ARTICLE");
            dt_mo.Columns.Add("SIZE");
            dt_mo.Columns.Add("USER1");
            dt_mo.Columns.Add("USER2");
            dt_mo.Columns.Add("USER3");
            dt_mo.Columns.Add("USER4");
            dt_mo.Columns.Add("USER5");
            dt_mo.Columns.Add("USER6");
            dt_mo.Columns.Add("USER7");
            dt_mo.Columns.Add("USER8");
            dt_mo.Columns.Add("USER9");
            dt_mo.Columns.Add("USER10");
            dt_mo.Columns.Add("u1");
            dt_mo.Columns.Add("u2");
            dt_mo.Columns.Add("u3");
            dt_mo.Columns.Add("u4");
            dt_mo.Columns.Add("u5");
            dt_mo.Columns.Add("u6");
            dt_mo.Columns.Add("u7");
            dt_mo.Columns.Add("u8");
            dt_mo.Columns.Add("u9");
            dt_mo.Columns.Add("u10");
            dt_mo.Columns.Add("QUANTITY");

            
            dt_op.Columns.Add("SEQ_NO");
            dt_op.Columns.Add("OPCODE");
            dt_op.Columns.Add("OPDESC");
            dt_op.Columns.Add("PIECE_RATE");
            dt_op.Columns.Add("OVERTIME_RATE");
            dt_op.Columns.Add("ALLOCATED_SAM");
            dt_op.Columns.Add("ACTUAL_SAM");
            dt_op.Columns.Add("ACTUAL_PROD_NORMAL");
            dt_op.Columns.Add("ACTUAL_PROD_OVERTIME");
            dt_op.Columns.Add("EFFICIENCY");
            dt_op.Columns.Add("NO_EMP");
            dt_op.Columns.Add("COST_NORMAL");
            dt_op.Columns.Add("COST_OVERTIME");
            dt_op.Columns.Add("COST_PER_PIECE");
            dt_op.Columns.Add("WORK_DURATION");            
            dt_op.Columns.Add("MO_NO");
            dt_op.Columns.Add("MO_DETAILS");

            ////get the last loaded mo
            //cmd = new SqlCommand("select V_MO_NO from LAST_SELECT_MO", dc.con);
            //sdr = cmd.ExecuteReader();
            //if (sdr.Read())
            //{
            //    txtmo.Text = sdr.GetValue(0).ToString();
            //    sdr.Close();
            //    btnsearch.PerformClick();
            //}
            //sdr.Close();

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

        public void select_controller()
        {
            try
            {
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

                //get the ip address
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

                dc.Close_Connection();  //close connection if open
                dc.OpenMYSQLConnection(ipaddress);   //open connection
            }
            catch (Exception ex)
            {
                radLabel1.Text = ex.Message;
            }
        }

        private void MO_Operation_Report_Initialized(object sender, EventArgs e)
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

            ////get all mo
            //SqlDataAdapter sda = new SqlDataAdapter("select V_MO_NO from MO", dc.con);
            //DataTable dt = new DataTable();
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
            dgvoperation.ThemeName = theme;
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            string strMO = "";
            strMO = cmbMO.Text.TrimStart().TrimEnd();

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
             txtpurchaseorder.Text = "";
             txtsalesorder.Text = "";
             txtshippingmode.Text = "";
             txtcustomer.Text = "";
             txtshipmentdest.Text = "";
             txtshippingdate.Text = "";




            //get the modetails for the mo
            //SqlDataAdapter sda = new SqlDataAdapter("SELECT V_MO_NO,V_COLOR_ID,V_SIZE_ID,V_ARTICLE_ID,I_ORDER_QTY,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10,V_MO_LINE,D_SHIPMENT_DATE,V_SHIPPING_DEST,V_SHIPPING_MODE,V_PURCHASE_ORDER,V_SALES_ORDER,D_LAST_UPDATED FROM MO_DETAILS where V_MO_NO='" + txtmo.Text + "' order by V_MO_LINE", dc.con);
            SqlDataAdapter sda = new SqlDataAdapter("SELECT V_MO_NO,V_COLOR_ID,V_SIZE_ID,V_ARTICLE_ID,I_ORDER_QTY,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10,V_MO_LINE,D_SHIPMENT_DATE,V_SHIPPING_DEST,V_SHIPPING_MODE,V_PURCHASE_ORDER,V_SALES_ORDER,D_LAST_UPDATED FROM MO_DETAILS where V_MO_NO='" + strMO + "' order by V_MO_LINE", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
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
                        txtshippingdate.Text = dt.Rows[i][16].ToString();
                        txtshipmentdest.Text = dt.Rows[i][17].ToString();
                        txtshippingmode.Text = dt.Rows[i][18].ToString();
                        txtpurchaseorder.Text = dt.Rows[i][19].ToString();
                        txtsalesorder.Text = dt.Rows[i][20].ToString();
                        String last_update = dt.Rows[i][21].ToString();

                        //get desc for masters
                        SqlCommand cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            color = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get desc for masters
                        cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + article + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            article = sdr.GetValue(0).ToString();
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

                        //get desc for masters
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
                        MO.Rows.Add(false, color, size, article, qty, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10, moline, last_update);
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
                    if (mo != strMO && MO.Rows.Count > 0)
                    {
                        cmd1 = new SqlCommand("update LAST_SELECT_MO set V_MO_NO='" + strMO + "'", dc.con);
                        cmd1.ExecuteNonQuery();
                    }

                    this.dgvmoline.Rows[0].IsSelected = true;
                    RowSelected();    //get the selected row
                }
            }
        }

        public void RowSelected()
        {
            try
            {
                dgvoperation.Rows.Clear();               
                dt_op.Rows.Clear();               
                dt_mo.Rows.Clear();               

                for (int k = 0; k < dgvmoline.RowCount; k++)
                {
                    if ((bool)dgvmoline.Rows[k].Cells[0].Value)
                    {
                        //String mo = txtmo.Text;
                        string mo = "";
                        mo = cmbMO.Text.TrimStart().TrimEnd();
                        String moline = dgvmoline.Rows[k].Cells[15].Value.ToString() + "";
                        String article = "";

                        dt_mo.Rows.Add(mo, moline, dgvmoline.Rows[k].Cells[1].Value.ToString(), dgvmoline.Rows[k].Cells[3].Value.ToString(), dgvmoline.Rows[k].Cells[2].Value.ToString(), dgvmoline.Rows[k].Cells[5].Value.ToString(), dgvmoline.Rows[k].Cells[6].Value.ToString(), dgvmoline.Rows[k].Cells[7].Value.ToString(), dgvmoline.Rows[k].Cells[8].Value.ToString(), dgvmoline.Rows[k].Cells[9].Value.ToString(), dgvmoline.Rows[k].Cells[10].Value.ToString(), dgvmoline.Rows[k].Cells[11].Value.ToString(), dgvmoline.Rows[k].Cells[12].Value.ToString(), dgvmoline.Rows[k].Cells[13].Value.ToString(), dgvmoline.Rows[k].Cells[14].Value.ToString(), dgvmoline.Columns[5].HeaderText, dgvmoline.Columns[6].HeaderText, dgvmoline.Columns[7].HeaderText, dgvmoline.Columns[8].HeaderText, dgvmoline.Columns[9].HeaderText, dgvmoline.Columns[10].HeaderText, dgvmoline.Columns[11].HeaderText, dgvmoline.Columns[12].HeaderText, dgvmoline.Columns[13].HeaderText, dgvmoline.Columns[14].HeaderText, dgvmoline.Rows[k].Cells[4].Value.ToString());

                        //get article id
                        SqlCommand cmd = new SqlCommand("select V_ARTICLE_ID from ARTICLE_DB where V_ARTICLE_DESC='" + dgvmoline.Rows[k].Cells[3].Value + "'", dc.con);
                        article = cmd.ExecuteScalar() + "";

                        int seq1 = 1;
                        int nextseq = 1;
                        int prevseq = 1;
                        int curseq = 1;

                        //get the sequence for the mo
                        SqlDataAdapter sda = new SqlDataAdapter("select ds.I_SEQUENCE_NO,ds.V_OPERATION_CODE,op.V_OPERATION_DESC,op.D_SAM,op.D_PIECERATE,op.D_OVERTIME_RATE from DESIGN_SEQUENCE ds,OPERATION_DB op where ds.V_ARTICLE_ID='" + article + "' and ds.V_OPERATION_CODE=op.V_OPERATION_CODE and ds.I_SEQUENCE_NO IN(select distinct I_SEQUENCE_NO from STATION_ASSIGN where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and I_STATION_ID!=0) order by ds.I_SEQUENCE_NO", dc.con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        sda.Dispose();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //reorder the sequence
                            prevseq = seq1;
                            seq1 = int.Parse(dt.Rows[i][0].ToString());
                            if (prevseq == seq1)
                            {
                                nextseq = curseq;
                            }
                            else
                            {
                                nextseq = nextseq + 1;
                            }
                            curseq = nextseq;

                            String opcode = dt.Rows[i][1].ToString();
                            String opdesc = dt.Rows[i][2].ToString();
                            int sam = int.Parse(dt.Rows[i][3].ToString());
                            decimal piecerate = Convert.ToDecimal(dt.Rows[i][4].ToString());
                            decimal overtime = Convert.ToDecimal(dt.Rows[i][5].ToString());

                            //get workduration
                            int workduration = 0;
                            string strSql = "select (datediff(SECOND, MIN(time), MAX(TIME))) AS TotalSec,CONVERT(VARCHAR(10), TIME, 111) from HANGER_HISTORY where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and SEQ_NO='" + curseq + "' group by CONVERT(VARCHAR(10), TIME, 111)";
                            sda = new SqlDataAdapter(strSql, dc.con);
                            DataTable dt1 = new DataTable();
                            sda.Fill(dt1);
                            sda.Dispose();
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            {
                                workduration += int.Parse(dt1.Rows[j][0].ToString());
                            }

                            //get normal time piece count
                            int normal_count = 0;
                            string strSqlNorm = "select sum(PC_COUNT) from HANGER_HISTORY where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and SEQ_NO='" + curseq + "' and WORKTYPE='0'";
                            cmd = new SqlCommand(strSqlNorm, dc.con);
                            String temp = cmd.ExecuteScalar() + "";
                            if (temp != "")
                            {
                                normal_count = int.Parse(temp);
                            }

                            //get overtime piece count
                            int overtime_count = 0;
                            string strSqlOT = "select sum(PC_COUNT) from HANGER_HISTORY where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and SEQ_NO='" + curseq + "' and WORKTYPE='1'";
                            cmd = new SqlCommand(strSqlOT, dc.con);
                            temp = cmd.ExecuteScalar() + "";
                            if (temp != "")
                            {
                                overtime_count = int.Parse(temp);
                            }

                            //Hanafi | Date:03 / 08 / 2021 | removed due to changed in data transfer process
                            //MySqlCommand cmd1 = new MySqlCommand("select sum(PC_COUNT) from hangerwip where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and SEQ_NO='" + curseq + "' and WORKTYPE='0'", dc.conn);
                            //temp = cmd1.ExecuteScalar() + "";
                            //if (temp != "")
                            //{
                            //    normal_count += int.Parse(temp);
                            //}

                            //Hanafi | Date:03 / 08 / 2021 | removed due to changed in data transfer process
                            ////get overtime piece count
                            //cmd1 = new MySqlCommand("select sum(PC_COUNT) from hangerwip where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and SEQ_NO='" + curseq + "' and WORKTYPE='1'", dc.conn);
                            //temp = cmd1.ExecuteScalar() + "";
                            //if (temp != "")
                            //{
                            //    overtime_count += int.Parse(temp);
                            //}

                            //get no of employees
                            cmd = new SqlCommand("select count(distinct EMP_ID) from HANGER_HISTORY where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and SEQ_NO='" + curseq + "'", dc.con);
                            int emp = int.Parse(cmd.ExecuteScalar() + "");

                            int flag = 0;
                            for (int j = 0; j < dgvoperation.RowCount; j++)
                            {
                                if (opcode == dgvoperation.Rows[j].Cells[1].Value.ToString())
                                {
                                    flag = 1;

                                    //if (int.Parse(dgvoperation.Rows[j].Cells[14].Value.ToString()) > workduration)
                                    //{
                                    workduration += int.Parse(dgvoperation.Rows[j].Cells[14].Value.ToString());
                                    //}

                                    //calculate cost
                                    decimal total_cost_normal = (decimal)(normal_count * piecerate);
                                    decimal total_cost_overtime = (decimal)(overtime_count * overtime); 
                                    int total = normal_count + overtime_count;

                                    total += int.Parse(dgvoperation.Rows[j].Cells[7].Value.ToString());
                                    total += int.Parse(dgvoperation.Rows[j].Cells[8].Value.ToString());
                                    total_cost_normal += Convert.ToDecimal(dgvoperation.Rows[j].Cells[11].Value.ToString());
                                    total_cost_overtime += Convert.ToDecimal(dgvoperation.Rows[j].Cells[12].Value.ToString());

                                    normal_count += int.Parse(dgvoperation.Rows[j].Cells[7].Value.ToString());
                                    overtime_count += int.Parse(dgvoperation.Rows[j].Cells[8].Value.ToString());
                                    emp += int.Parse(dgvoperation.Rows[j].Cells[10].Value.ToString());

                                    //calculate cost per piece
                                    decimal costperpiece = 0;
                                    if (total > 0)
                                    {
                                        costperpiece = ((decimal)total_cost_normal + (decimal)total_cost_overtime) / (decimal)total;
                                    }

                                    //calculate actual sam
                                    decimal actual_sam = 0;
                                    if (total > 0)
                                    {
                                        actual_sam = (decimal)workduration / (decimal)total;
                                    }

                                    //calculate efficiency
                                    decimal efficiency = 0;
                                    if (actual_sam > 0)
                                    {
                                        efficiency = (decimal)sam / (decimal)actual_sam * 100;
                                    }

                                    //update grid
                                    dgvoperation.Rows[j].Cells[7].Value = normal_count;
                                    dgvoperation.Rows[j].Cells[8].Value = overtime_count;
                                    dgvoperation.Rows[j].Cells[6].Value = actual_sam.ToString("0.##");
                                    dgvoperation.Rows[j].Cells[9].Value = efficiency.ToString("0.##");
                                    dgvoperation.Rows[j].Cells[10].Value = emp;
                                    dgvoperation.Rows[j].Cells[11].Value = total_cost_normal.ToString("0.##");
                                    dgvoperation.Rows[j].Cells[12].Value = total_cost_overtime.ToString("0.##");
                                    dgvoperation.Rows[j].Cells[13].Value = costperpiece.ToString("0.##");
                                    dgvoperation.Rows[j].Cells[14].Value = workduration / 60;

                                    dt_op.Rows[j][7] = normal_count;
                                    dt_op.Rows[j][8] = overtime_count;
                                    dt_op.Rows[j][6] = actual_sam.ToString("0.##");
                                    dt_op.Rows[j][9] = efficiency.ToString("0.##");
                                    dt_op.Rows[j][10] = emp;
                                    dt_op.Rows[j][11] = total_cost_normal.ToString("0.##");
                                    dt_op.Rows[j][12] = total_cost_overtime.ToString("0.##");
                                    dt_op.Rows[j][13] = costperpiece.ToString("0.##");
                                    dt_op.Rows[j][14] = workduration / 60;

                                    break;
                                }
                            }

                            if (flag == 0)
                            {
                                //calculate cost
                                decimal total_cost_normal = (decimal)(normal_count * piecerate);
                                decimal total_cost_overtime = (decimal)(overtime_count * overtime);
                                int total = normal_count + overtime_count;

                                //calculate cost per piece
                                decimal costperpiece = 0;
                                if (total > 0)
                                {
                                    costperpiece = ((decimal)total_cost_normal + (decimal)total_cost_overtime) / (decimal)total;
                                }

                                //calculate actual sam
                                decimal actual_sam = 0;
                                if (total > 0)
                                {
                                    actual_sam = (decimal)workduration / (decimal)total;
                                }

                                //calculate efficiency
                                decimal efficiency = 0;
                                if (actual_sam > 0)
                                {
                                    efficiency = (decimal)sam / (decimal)actual_sam * 100;
                                }

                                //add to grid
                                dgvoperation.Rows.Add(seq1, opcode, opdesc, piecerate, overtime, sam, actual_sam.ToString("0.##"), normal_count,overtime_count, efficiency.ToString("0.##") + "%", emp, total_cost_normal.ToString("0.##"), total_cost_overtime.ToString("0.##"), costperpiece.ToString("0.##"), workduration / 60);
                                dt_op.Rows.Add(seq1, opcode, opdesc, piecerate, overtime, sam, actual_sam.ToString("0.##"), normal_count, overtime_count, efficiency.ToString("0.##") + "%", emp, total_cost_normal.ToString("0.##"), total_cost_overtime.ToString("0.##"), costperpiece.ToString("0.##"), workduration / 60, mo, moline);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                radLabel1.Text = ex.Message;
            }
        }

        private void radLabel1_TextChanged(object sender, EventArgs e)
        {
            //hide the error message after 5 sec
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

        private void dgvmoline_Click(object sender, EventArgs e)
        {
            RowSelected();
        }

        private void dgvmoline_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            //check if mo is selected
            if ((bool)dgvmoline.Rows[e.RowIndex].Cells["Select"].Value)
            {
                dgvmoline.Rows[e.RowIndex].Cells["Select"].Value = false;
            }
            else
            {
                dgvmoline.Rows[e.RowIndex].Cells["Select"].Value = true;

                //check if selected mo is of same articles
                for (int i = 0; i < dgvmoline.Rows.Count; i++)
                {
                    if ((bool)dgvmoline.Rows[i].Cells[0].Value)
                    {
                        if (dgvmoline.Rows[i].Cells[3].Value.ToString() == dgvmoline.Rows[e.RowIndex].Cells[3].Value.ToString())
                        {
                            dgvmoline.Rows[e.RowIndex].Cells["Select"].Value = true;
                            RowSelected();
                        }
                        else
                        {
                            dgvmoline.Rows[e.RowIndex].Cells["Select"].Value = false;
                            radLabel1.Text = "Not a Same Article";
                            return;
                        }
                    }
                }
            }
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            //check if report button is clicked
            if (btnreport.Text == "Report View")
            {
                reportViewer1.Visible = true;
                DataView view = new DataView(dt_mo);
                DataView view1 = new DataView(dt_op);

                //get logo
                DataTable dt_image = new DataTable();
                dt_image.Columns.Add("image", typeof(byte[]));
                dt_image.Rows.Add(dc.GetImage());
                DataView dv_image = new DataView(dt_image);

                reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.MO_Operation_Report.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                //add views to dataset
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", view1));
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", dv_image));
                reportViewer1.RefreshReport();
                btnreport.Text = "Table View";
            }
            else
            {
                btnreport.Text = "Report View";
                reportViewer1.Visible = false;
            }
        }

        private void cmbMO_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            btnsearch.PerformClick();
        }
    }
}
