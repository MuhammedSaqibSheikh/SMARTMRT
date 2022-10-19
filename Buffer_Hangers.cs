using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Buffer_Hangers : Telerik.WinControls.UI.RadForm
    {
        public Buffer_Hangers()
        {
            InitializeComponent();
        }
        Database_Connection dc = new Database_Connection(); //Connection Class
        DataTable STN = new DataTable();  //datatable for buffer
        String controller_name = "";
        String theme = "";

        private void Buffer_Hangers_Load(object sender, EventArgs e)
        {
            dgvbuffer.MasterTemplate.SelectLastAddedRow = false;
            dgvbufferhangers.MasterTemplate.SelectLastAddedRow = false;
            dgvbufferout.MasterTemplate.SelectLastAddedRow = false;
            RadMessageBox.SetThemeName("FluentDark");  //set message box theme

            //disable the close buttons for search in grid
            dgvbuffer.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvbufferhangers.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvbufferout.MasterView.TableSearchRow.ShowCloseButton = false;

            this.CenterToScreen();  //keep form centered to the screen
            dc.OpenConnection();   //open connection
            select_controller();   //get the selected controller 

            //check if the user has selected the controller
            if (controller_name == "--SELECT--")
            {
                RadMessageBox.Show("Please Select a Controller", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                this.Close();
                return;
            }

            //get all the buffer stations
            SqlDataAdapter sda = new SqlDataAdapter("select I_OUTFEED_LINE_NO,I_STN_NO_OUTFEED,I_STN_ID from STATION_DATA where I_STATION_TYPE='3' ", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //get the count of hangers for that buffer station
                MySqlCommand cmd1 = new MySqlCommand("Select count(*) from bufferhangers where STN_ID='" + dt.Rows[i][2].ToString() + "'", dc.conn);
                int count = int.Parse(cmd1.ExecuteScalar() + "");

                cmd1 = new MySqlCommand("Select sum(COUNT) from buffercallout where STN_ID='" + dt.Rows[i][2].ToString() + "'", dc.conn);
                int count1 = 0;
                String temp = cmd1.ExecuteScalar() + "";
                if (temp != "")
                {
                    count1 = int.Parse(temp);
                }

                count = count - count1;
                //add to grid
                dgvbuffer.Rows.Add(dt.Rows[i][0].ToString() + "." + dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), count);
                Thread.Sleep(100);
            }

            dgvbufferout.Columns[0].ReadOnly = true;
            dgvbufferout.Columns[1].ReadOnly = true;
            dgvbufferout.Columns[2].ReadOnly = true;

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

            //Get Special field names
            SqlCommand cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF1' and V_ENABLED='TRUE'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user1 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //Get Special field names
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user2 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //Get Special field names
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user3 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //Get Special field names
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user4 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //Get Special field names
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user5 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //Get Special field names
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF6' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user6 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //Get Special field names
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF7' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user7 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //Get Special field names
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF8' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user8 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //Get Special field names
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF9' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user9 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //Get Special field names
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF10' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                user10 = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //Add columns to datatable 
            STN.Columns.Add("Hanger ID");
            STN.Columns.Add("MO No");
            STN.Columns.Add("MO Details");
            STN.Columns.Add("Color");
            STN.Columns.Add("Article ID");
            STN.Columns.Add("Size");
            STN.Columns.Add(user1);
            STN.Columns.Add(user2);
            STN.Columns.Add(user3);
            STN.Columns.Add(user4);
            STN.Columns.Add(user5);
            STN.Columns.Add(user6);
            STN.Columns.Add(user7);
            STN.Columns.Add(user8);
            STN.Columns.Add(user9);
            STN.Columns.Add(user10);
            dgvbufferhangers.DataSource = STN;

            //hide the specail fields which are not enabled
            dgvbuffer.Columns[1].IsVisible = false;
            if (user1 == "")
            {
                dgvbufferhangers.Columns[6].IsVisible = false;
            }

            if (user2 == "")
            {
                dgvbufferhangers.Columns[7].IsVisible = false;
            }

            if (user3 == "")
            {
                dgvbufferhangers.Columns[8].IsVisible = false;
            }

            if (user4 == "")
            {
                dgvbufferhangers.Columns[9].IsVisible = false;
            }

            if (user5 == "")
            {
                dgvbufferhangers.Columns[10].IsVisible = false;
            }

            if (user6 == "")
            {
                dgvbufferhangers.Columns[11].IsVisible = false;
            }

            if (user7 == "")
            {
                dgvbufferhangers.Columns[12].IsVisible = false;
            }

            if (user8 == "")
            {
                dgvbufferhangers.Columns[13].IsVisible = false;
            }

            if (user9 == "")
            {
                dgvbufferhangers.Columns[14].IsVisible = false;
            }

            if (user10 == "")
            {
                dgvbufferhangers.Columns[15].IsVisible = false;
            }

            Buffer_Data(0, 1);  //select the first row of grid
        }

        public void select_controller()
        {
            dc.OpenConnection();
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

            //get the ip address of the controller
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

            dc.Close_Connection();  //close the connection if open
            dc.OpenMYSQLConnection(ipaddress);  //open the connection
        }

        private void radLabel8_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            radPanel2.Visible = true;
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            MyTimer.Start();
        }

        System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radLabel8.Text = "";
            radPanel2.Visible = false;
            MyTimer.Stop();
        }

        public void Buffer_Data(int Row, int Col)
        {
            //check if controller is selected
            if (controller_name != "--SELECT--")
            {
                radLabel1.Visible = false;
                panel3.Visible = false;
                btnbuffercallout.Visible = false;
                btncancel.Visible = false;
                dgvbufferout.Visible = false;
                STN.Rows.Clear();

                //check if grid has data
                if (dgvbuffer.Rows.Count > 0)
                {
                    //get the station id
                    dgvbuffer.Rows[Row].IsCurrent = true;
                    dgvbuffer.Rows[Row].IsSelected = true;
                    String stnid = dgvbuffer.Rows[Row].Cells[Col].Value.ToString();

                    //get all the buffer hangers for that station
                    MySqlDataAdapter sda = new MySqlDataAdapter("Select MO_NO,MO_LINE,HANGER_ID from bufferhangers where STN_ID='" + stnid + "' order by TIME", dc.conn);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        radLabel1.Visible = true;
                        panel3.Visible = true;
                        btnbuffercallout.Visible = true;
                        btncancel.Visible = true;
                        dgvbufferout.Visible = true;
                        radLabel1.Text = "Hanger Count : " + dt.Rows.Count;

                        //mo details
                        String hangerID = dt.Rows[i][2].ToString();
                        String MO = dt.Rows[i][0].ToString();
                        String MOLINE = dt.Rows[i][1].ToString();
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

                        //get mo details for that hanger
                        SqlDataAdapter sda1 = new SqlDataAdapter("Select V_COLOR_ID,V_ARTICLE_ID,V_SIZE_ID,V_USER_DEF1,V_USER_DEF2,V_USER_DEF3,V_USER_DEF4,V_USER_DEF5,V_USER_DEF6,V_USER_DEF7,V_USER_DEF8,V_USER_DEF9,V_USER_DEF10 from MO_DETAILS where V_MO_NO='" + MO + "' and V_MO_LINE='" + MOLINE + "'", dc.con);
                        DataTable dt3 = new DataTable();
                        sda1.Fill(dt3);
                        for (int j = 0; j < dt3.Rows.Count; j++)
                        {
                            color = dt3.Rows[j][0].ToString();
                            article = dt3.Rows[j][1].ToString();
                            size = dt3.Rows[j][2].ToString();
                            user1 = dt3.Rows[j][3].ToString();
                            user2 = dt3.Rows[j][4].ToString();
                            user3 = dt3.Rows[j][5].ToString();
                            user4 = dt3.Rows[j][6].ToString();
                            user5 = dt3.Rows[j][7].ToString();
                            user6 = dt3.Rows[j][8].ToString();
                            user7 = dt3.Rows[j][9].ToString();
                            user8 = dt3.Rows[j][10].ToString();
                            user9 = dt3.Rows[j][11].ToString();
                            user10 = dt3.Rows[j][12].ToString();
                        }
                        sda1.Dispose();

                        //get master description
                        SqlCommand cmd = new SqlCommand("select V_COLOR_DESC from COLOR_DB where V_COLOR_ID='" + color + "'", dc.con);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            color = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get master description
                        cmd = new SqlCommand("select V_ARTICLE_DESC from ARTICLE_DB where V_ARTICLE_ID='" + article + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            article = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get master description
                        cmd = new SqlCommand("select V_SIZE_DESC from SIZE_DB where V_SIZE_ID='" + size + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            size = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get master description
                        cmd = new SqlCommand("select V_DESC from USER_DEF1_DB where V_USER_ID='" + user1 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user1 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get master description
                        cmd = new SqlCommand("select V_DESC from USER_DEF2_DB where V_USER_ID='" + user2 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user2 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get master description
                        cmd = new SqlCommand("select V_DESC from USER_DEF3_DB where V_USER_ID='" + user3 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user3 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get master description
                        cmd = new SqlCommand("select V_DESC from USER_DEF4_DB where V_USER_ID='" + user4 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user4 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get master description
                        cmd = new SqlCommand("select V_DESC from USER_DEF5_DB where V_USER_ID='" + user5 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user5 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get master description
                        cmd = new SqlCommand("select V_DESC from USER_DEF6_DB where V_USER_ID='" + user6 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user6 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get master description
                        cmd = new SqlCommand("select V_DESC from USER_DEF7_DB where V_USER_ID='" + user7 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user7 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get master description
                        cmd = new SqlCommand("select V_DESC from USER_DEF8_DB where V_USER_ID='" + user8 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user8 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get master description
                        cmd = new SqlCommand("select V_DESC from USER_DEF9_DB where V_USER_ID='" + user9 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user9 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //get master description
                        cmd = new SqlCommand("select V_DESC from USER_DEF10_DB where V_USER_ID='" + user10 + "'", dc.con);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            user10 = sdr.GetValue(0).ToString();
                        }
                        sdr.Close();

                        //add to grid
                        STN.Rows.Add(hangerID, MO, MOLINE, color, article, size, user1, user2, user3, user4, user5, user6, user7, user8, user9, user10);
                    }

                    dgvbufferhangers.DataSource = STN;
                    dgvbufferout.Rows.Clear();

                    //get the buffer hangers details for that station
                    sda = new MySqlDataAdapter("Select distinct MO_NO,MO_LINE from bufferhangers where STN_ID='" + stnid + "'", dc.conn);
                    dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //get the count of hangers in buffer for that mo in that station
                        MySqlCommand cmd = new MySqlCommand("Select count(*) from bufferhangers where MO_NO='" + dt.Rows[i][0].ToString() + "' and MO_LINE='" + dt.Rows[i][1].ToString() + "' and STN_ID='" + stnid + "'", dc.conn);
                        int count = int.Parse(cmd.ExecuteScalar() + "");

                        cmd = new MySqlCommand("Select sum(COUNT) from buffercallout where MO_NO='" + dt.Rows[i][0].ToString() + "' and MO_LINE='" + dt.Rows[i][1].ToString() + "' and STN_ID='" + stnid + "'", dc.conn);
                        int count1 = 0;
                        String temp = cmd.ExecuteScalar() + "";
                        if (temp != "")
                        {
                            count1 += int.Parse(temp);
                        }

                        count -= count1;
                        dgvbufferout.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), count, "1");
                    }
                }
            }
            else
            {
                radLabel8.Text = "Please Select a Controller";
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            dgvbufferhangers.Visible = false;
            dgvbufferout.Visible = false;
        }

        private void btnbuffercallout_Click(object sender, EventArgs e)
        {
            //check if controller is selected
            if (controller_name == "--SELECT--")
            {
                radLabel8.Text = "Please Select a Controller";
                return;
            }

            //buffer datatable
            DataTable buffer = new DataTable();
            buffer.Columns.Add("MO");
            buffer.Columns.Add("MOLINE");
            buffer.Columns.Add("STATION");

            String selectmo = "";
            int total = 0;

            //get total callout count for each mo
            for (int j = 0; j < dgvbufferout.Rows.Count; j++)
            {
                if (dgvbufferout.Rows[j].Cells[3].Value.ToString() != "")
                {
                    total = total + int.Parse(dgvbufferout.Rows[j].Cells[3].Value.ToString());
                    selectmo = selectmo + dgvbufferout.Rows[j].Cells[0].Value.ToString() + "*" + dgvbufferout.Rows[j].Cells[1].Value.ToString() + "*" + dgvbufferout.Rows[j].Cells[3].Value.ToString() + "+";
                }
            }

            selectmo = selectmo.Remove(selectmo.Length - 1, 1);
            String[] selectmo1 = selectmo.Split('+');
            int len = selectmo1.Length;

            while (total > 0)
            {
                //get hanger details for that station according to time
                MySqlDataAdapter sda1 = new MySqlDataAdapter("Select STN_ID,MO_NO,MO_LINE from bufferhangers where STN_ID='" + dgvbuffer.SelectedRows[0].Cells[1].Value.ToString() + "' order by time", dc.conn);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                sda1.Dispose();
                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    if (total == 0)
                    {
                        break;
                    }

                    String stationid = dt1.Rows[j][0].ToString();
                    String mo = dt1.Rows[j][1].ToString();
                    String moline = dt1.Rows[j][2].ToString();

                    //get all the mo callout
                    for (int i = 0; i < len; i++)
                    {
                        String[] selectmo2 = selectmo1[i].Split('*');
                        //check if the callout count > 0 and same MO  
                        if (mo == selectmo2[0] && moline == selectmo2[1] && int.Parse(selectmo2[2]) > 0)
                        {
                            int count = int.Parse(selectmo2[2]);
                            count = count - 1;
                            selectmo1[i] = selectmo2[0] + "*" + selectmo2[1] + "*" + count;
                            buffer.Rows.Add(mo, moline, stationid);
                            total = total - 1;
                        }
                    }
                }
            }

            int count1 = 1;
            buffer.Rows.Add("", "", "");
            //get callout details
            for (int i = 0; i < buffer.Rows.Count - 1; i++)
            {
                String cur_mo = buffer.Rows[i][0].ToString();
                String cur_molime = buffer.Rows[i][1].ToString();
                String cur_stn = buffer.Rows[i][2].ToString();
                String next_mo = buffer.Rows[i + 1][0].ToString();
                String next_molime = buffer.Rows[i + 1][1].ToString();
                String next_stn = buffer.Rows[i + 1][2].ToString();

                //check if current mo and station is same as previous
                if (cur_mo == next_mo && cur_molime == next_molime && cur_stn == next_stn)
                {
                    count1 = count1 + 1;
                }
                else
                {
                    //insert into buffercallout
                    MySqlCommand cmd = new MySqlCommand("insert into buffercallout (STN_ID,MO_NO,MO_LINE,COUNT) values('" + cur_stn + "','" + cur_mo + "','" + cur_molime + "','" + count1 + "')", dc.conn);
                    cmd.ExecuteNonQuery();

                    count1 = 1;
                }
            }

            radLabel1.Visible = false;
            panel3.Visible = false;
            btnbuffercallout.Visible = false;
            btncancel.Visible = false;
            dgvbufferout.Visible = false;
        }         

        private void Buffer_Hangers_Initialized(object sender, EventArgs e)
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

            //changer the theme for the grid
            GridTheme(theme);
        }

        //set the theme for the grid
        public void GridTheme(String theme)
        {
            dgvbuffer.ThemeName = theme;
            dgvbufferhangers.ThemeName = theme;
            dgvbufferout.ThemeName = theme;
        }

        private void dgvbuffer_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            //get selected buffer station hangers 
            if (e.RowIndex > 0)
            {
                Buffer_Data(e.RowIndex, 1);
            }
        }

        private void dgvbufferout_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            Regex r = new Regex("^[0-9]*$");

            //check if callout count is greater than 0
            int actualcount = int.Parse(dgvbufferout.Rows[e.RowIndex].Cells[2].Value.ToString());
            String c = dgvbufferout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (!(r.IsMatch(c)) || c == "0" || c == "")
            {
                radLabel8.Text = "Call Out Count should be greater than Zero";
                dgvbufferout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "1";
                return;
            }

            //check if callout count less that hanger count
            int count = int.Parse(c);
            if (count > actualcount)
            {
                radLabel8.Text = "Call Out Count is more than Anctual Hanger Count";
                dgvbufferout.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = actualcount;
            }
            else
            {
                btnbuffercallout.Enabled = true;
            }
        }

        private void dgvbufferhangers_CellClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {

        }


        private void dgvbuffer_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change the fore color of grids of these theme is selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvbuffer.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvbuffer.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvbuffer.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvbuffer.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvbufferout_ViewCellFormatting(object sender, CellFormattingEventArgs e)
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

        private void dgvbufferhangers_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change the fore color of grids of these theme is selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvbufferhangers.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvbufferhangers.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvbufferhangers.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvbufferhangers.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }
    }
}
