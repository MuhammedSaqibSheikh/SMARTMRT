using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using Microsoft.Reporting.WinForms;
using System.Diagnostics;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Setup : Telerik.WinControls.UI.RadForm
    {
        public Setup()
        {
            InitializeComponent();
        }

        Database_Connection dc = new Database_Connection();   //connection class

        String breaks = "";
        String Shifts = "";
        String routeID = "";
        String stnID = "";
        String lineNo = "";
        String holiday_id = "";
        String hideday_id = "";

        String save = "";
        String update = "";
        String shift_start = "";
        String shift_end = "";
        String overtime = "";
        String buffergroup = "";
        String buffergroupid = "";
        String station = "";
        int flg = 0;
        int autologin = 0;
        String theme = "";

        public void Form_Location(String Form_Name)
        {
            //get form location
            if (Form_Name == "Home")
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(0, 0);
                vpagesetup.SelectedPage = pagespecial;
            }
            else if (Form_Name == "Station Assign")
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(350, 150);
                this.Size = new Size(900, 485);

                vpagesetup.SelectedPage = pagecontroller1;
                vpagecontroller.SelectedPage = pagehangerlimit;
                pageuseraccount.Item.Visibility = ElementVisibility.Collapsed;
                pagespecial.Item.Visibility = ElementVisibility.Collapsed;
                pagestation1.Item.Visibility = ElementVisibility.Collapsed;
                pageroute.Item.Visibility = ElementVisibility.Collapsed;
                pagerouting1.Item.Visibility = ElementVisibility.Collapsed;
                pagepusher.Item.Visibility = ElementVisibility.Collapsed;
                pagegeneral.Item.Visibility = ElementVisibility.Collapsed;
                pageaddcontroller.Item.Visibility = ElementVisibility.Collapsed;
                pageproductionline.Item.Visibility = ElementVisibility.Collapsed;
                pagekeypad.Item.Visibility = ElementVisibility.Collapsed;
                pageaddclusterdb.Item.Visibility = ElementVisibility.Collapsed;
                pageshifts.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "ProdLine")
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(350, 150);
                this.Size = new Size(900, 485);

                vpagesetup.SelectedPage = pagecontroller1;
                vpagecontroller.SelectedPage = pageproductionline;
                pageuseraccount.Item.Visibility = ElementVisibility.Collapsed;
                pagespecial.Item.Visibility = ElementVisibility.Collapsed;
                pagestation1.Item.Visibility = ElementVisibility.Collapsed;
                pageroute.Item.Visibility = ElementVisibility.Collapsed;
                pagerouting1.Item.Visibility = ElementVisibility.Collapsed;
                pagepusher.Item.Visibility = ElementVisibility.Collapsed;
                pagegeneral.Item.Visibility = ElementVisibility.Collapsed;
                pageaddcontroller.Item.Visibility = ElementVisibility.Collapsed;
                pagekeypad.Item.Visibility = ElementVisibility.Collapsed;
                pageaddclusterdb.Item.Visibility = ElementVisibility.Collapsed;
                pageshifts.Item.Visibility = ElementVisibility.Collapsed;
                pagehangerlimit.Item.Visibility = ElementVisibility.Collapsed;
            }
        }

        private void Setup_Load(object sender, EventArgs e)
        {
            PMSCLIENT_Version();   //get pms version
            RadMessageBox.SetThemeName("FluentDark");    //set messagebox theme
            flg = 0;
            clnholiday.SelectedDate = DateTime.Now;
            radPanel1.Visible = false;

            //get all user group
            SqlDataAdapter da2 = new SqlDataAdapter("select distinct V_USERGROUP FROM USER_GROUP_NAMES  ", dc.con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                cmbUsergroup.Items.Add(dt2.Rows[i]["V_USERGROUP"].ToString());
                cmdusergroupaccessprivilage.Items.Add(dt2.Rows[i]["V_USERGROUP"].ToString());
            }
            da2.Dispose();

            //get all the language columns
            da2 = new SqlDataAdapter("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Language' and COLUMN_NAME!='Item' and COLUMN_NAME!='Form' and COLUMN_NAME!='Item_No' ORDER BY ORDINAL_POSITION", dc.con);
            dt2 = new DataTable();
            da2.Fill(dt2);
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                cmblanguage.Items.Add(dt2.Rows[i][0].ToString());
            }
            da2.Dispose();

            //set to current date
            tpshiftstart.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            tpshiftend.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            tpovertimeend.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            tpbreakstart.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
            tpbreakend.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));

            pageproduction.Item.Visibility = ElementVisibility.Collapsed;
            pagehideday.Item.Visibility = ElementVisibility.Collapsed;

            RefereshGrid_Buffer();    //get buffer groups

            btndeletebufferstation.Enabled = false;
            RefereshGrid_BufferStation();   //get buffer group stations

            //chkfollowemployee.Checked = false;
            //chkfollowemployee.Visible = true;
            //chkfollowemployee.Checked = false;
            //radLabel11.Visible = true;
            //get logo
            byte[] logo = null;
            btndeletecontroller.Enabled = false;
            SqlCommand cmd = new SqlCommand("SELECT Language,ThemeName,COMPANY_LOGO,HIDE_TOTALS,GET_ALL_OPERATIONS,MULTI_LOGIN FROM Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                cmblanguage.Text = sdr.GetValue(0).ToString();
                theme = sdr.GetValue(1).ToString();
                if (sdr.GetValue(2).ToString() != "")
                {
                    logo = (byte[])sdr.GetValue(2);
                }

                if (sdr.GetValue(3).ToString() == "TRUE")
                {
                    chkhidetotals.Checked = true;
                }

                if (sdr.GetValue(4).ToString() == "TRUE")
                {
                    chkgetallop.Checked = true;
                }

                if (sdr.GetValue(5).ToString() == "TRUE")
                {
                    chkmultilogin.Checked = true;
                }

                //if (sdr.GetValue(6).ToString() == "TRUE")
                //{
                //    chkfollowemployee.Checked = true;
                //}
            }
            sdr.Close();

            //convert byte[] to image
            if (logo != null)
            {
                MemoryStream ms = new MemoryStream(logo);
                pictureBox1.Image = Image.FromStream(ms);

                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            }

            //get special field details
            cmd = new SqlCommand("SELECT V_USER,V_ENABLED FROM USER_COLUMN_NAMES where V_MRT='USER_DEF1'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                txtuser1.Text = sdr.GetValue(0).ToString();
                chkboxuser1.Text = sdr.GetValue(0).ToString();
                if (sdr.GetValue(1).ToString() == "TRUE")
                {
                    chkuser1.Checked = true;
                    chkboxuser1.Visible = true;
                }
                else
                {
                    chkuser1.Checked = false;
                    chkboxuser1.Visible = false;
                }
            }
            sdr.Close();

            //get special field details
            cmd = new SqlCommand("SELECT V_USER,V_ENABLED FROM USER_COLUMN_NAMES where V_MRT='USER_DEF2'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                txtuser2.Text = sdr.GetValue(0).ToString();
                chkboxuser2.Text = sdr.GetValue(0).ToString();

                if (sdr.GetValue(1).ToString() == "TRUE")
                {
                    chkuser2.Checked = true;
                    chkboxuser2.Visible = true;
                }
                else
                {
                    chkuser2.Checked = false;
                    chkboxuser2.Visible = false;
                }
            }
            sdr.Close();

            //get special field details
            cmd = new SqlCommand("SELECT V_USER,V_ENABLED FROM USER_COLUMN_NAMES where V_MRT='USER_DEF3'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                txtuser3.Text = sdr.GetValue(0).ToString();
                chkboxuser3.Text = sdr.GetValue(0).ToString();

                if (sdr.GetValue(1).ToString() == "TRUE")
                {
                    chkuser3.Checked = true;
                    chkboxuser3.Visible = true;
                }
                else
                {
                    chkuser3.Checked = false;
                    chkboxuser3.Visible = false;
                }
            }
            sdr.Close();

            //get special field details
            cmd = new SqlCommand("SELECT V_USER,V_ENABLED FROM USER_COLUMN_NAMES where V_MRT='USER_DEF4'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                txtuser4.Text = sdr.GetValue(0).ToString();
                chkboxuser4.Text = sdr.GetValue(0).ToString();

                if (sdr.GetValue(1).ToString() == "TRUE")
                {
                    chkuser4.Checked = true;
                    chkboxuser4.Visible = true;
                }
                else
                {
                    chkuser4.Checked = false;
                    chkboxuser4.Visible = false;
                }
            }
            sdr.Close();

            //get special field details
            cmd = new SqlCommand("SELECT V_USER,V_ENABLED FROM USER_COLUMN_NAMES where V_MRT='USER_DEF5'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                txtuser5.Text = sdr.GetValue(0).ToString();
                chkboxuser5.Text = sdr.GetValue(0).ToString();

                if (sdr.GetValue(1).ToString() == "TRUE")
                {
                    chkuser5.Checked = true;
                    chkboxuser5.Visible = true;
                }
                else
                {
                    chkuser5.Checked = false;
                    chkboxuser5.Visible = false;
                }
            }
            sdr.Close();

            //get special field details
            cmd = new SqlCommand("SELECT V_USER,V_ENABLED FROM USER_COLUMN_NAMES where V_MRT='USER_DEF6'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                txtuser6.Text = sdr.GetValue(0).ToString();
                chkboxuser6.Text = sdr.GetValue(0).ToString();

                if (sdr.GetValue(1).ToString() == "TRUE")
                {
                    chkuser6.Checked = true;
                    chkboxuser6.Visible = true;
                }
                else
                {
                    chkuser6.Checked = false;
                    chkboxuser6.Visible = false;
                }
            }
            sdr.Close();

            //get special field details
            cmd = new SqlCommand("SELECT V_USER,V_ENABLED FROM USER_COLUMN_NAMES where V_MRT='USER_DEF7'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                txtuser7.Text = sdr.GetValue(0).ToString();
                chkboxuser7.Text = sdr.GetValue(0).ToString();

                if (sdr.GetValue(1).ToString() == "TRUE")
                {
                    chkuser7.Checked = true;
                    chkboxuser7.Visible = true;
                }
                else
                {
                    chkuser7.Checked = false;
                    chkboxuser7.Visible = false;
                }
            }
            sdr.Close();

            //get special field details
            cmd = new SqlCommand("SELECT V_USER,V_ENABLED FROM USER_COLUMN_NAMES where V_MRT='USER_DEF8'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                txtuser8.Text = sdr.GetValue(0).ToString();
                chkboxuser8.Text = sdr.GetValue(0).ToString();

                if (sdr.GetValue(1).ToString() == "TRUE")
                {
                    chkuser8.Checked = true;
                    chkboxuser8.Visible = true;
                }
                else
                {
                    chkuser8.Checked = false;
                    chkboxuser8.Visible = false;
                }
            }
            sdr.Close();

            //get special field details
            cmd = new SqlCommand("SELECT V_USER,V_ENABLED FROM USER_COLUMN_NAMES where V_MRT='USER_DEF9'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                txtuser9.Text = sdr.GetValue(0).ToString();
                chkboxuser9.Text = sdr.GetValue(0).ToString();

                if (sdr.GetValue(1).ToString() == "TRUE")
                {
                    chkuser9.Checked = true;
                    chkboxuser9.Visible = true;
                }
                else
                {
                    chkuser9.Checked = false;
                    chkboxuser9.Visible = false;
                }
            }
            sdr.Close();

            //get special field details
            cmd = new SqlCommand("SELECT V_USER,V_ENABLED FROM USER_COLUMN_NAMES where V_MRT='USER_DEF10'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                txtuser10.Text = sdr.GetValue(0).ToString();
                chkboxuser10.Text = sdr.GetValue(0).ToString();

                if (sdr.GetValue(1).ToString() == "TRUE")
                {
                    chkuser10.Checked = true;
                    chkboxuser10.Visible = true;
                }
                else
                {
                    chkuser10.Checked = false;
                    chkboxuser10.Visible = false;
                }
            }
            sdr.Close();


            btneditstation.Enabled = false;
            btndeletestation.Enabled = false;
            btnsavestation.Enabled = false;
            btndeleterouting.Enabled = false;
            btneditrouting.Enabled = false;
            btnsaverouting.Enabled = false;
            btndeleteroute.Enabled = false;
            btneditroute.Enabled = false;
            btnsaveroute.Enabled = false;
            btndeleteproduction.Enabled = false;
            btnsavepusher.Enabled = false;
            btndeletepusher.Enabled = false;
            btneditpusher.Enabled = false;

            RefereshGrid_ProdLine();   //get production lines
            RefereshGrid();    //get controller details
            RefereshUser();    //get user details
            RefereshUserGroup();    //get user group details
            RefereshShifts();   //get shift details
            RefereshBreaks();   //get shift break details
            RefereshHoliday();   //get holiday details
            RefereshHideday();   //get hide day details
            RefereshWeekoffs();   //get weekoff detais
            select_controller();    //get the selected controller
            RefereshCluster();   //get cluster details
            KeypadSetup();    //get keypad details
            cmbtheme.Text = theme;   //get theme
            GridTheme();      //change grid theme
        }

        public void PMSCLIENT_Version()
        {
            //check if basic version of pms client is enabled
            if (Database_Connection.SET_PMSCIENT == "1")
            {
                chkqcmain.Enabled = false;
                chkqcsub.Enabled = false;
                chkmbmain.Enabled = false;
                chkmbsub.Enabled = false;
                chksparemain.Enabled = false;
                chksparesub.Enabled = false;

                chkproductionplanning.Enabled = false;
                chkbuffer.Enabled = false;
                chkstationwip.Enabled = false;
                chklinebalancing.Enabled = false;

                chkperfomance.Enabled = false;
                chkemployeeqcreport.Enabled = false;
                chkmoqcreport.Enabled = false;
                chkoperationqcreport.Enabled = false;
                chkstationqcreport.Enabled = false;
                chktopdefects.Enabled = false;
                chkpayrollreport.Enabled = false;
                chkmachineassign.Enabled = false;
                chkmachinerepair.Enabled = false;
                chkmachinereport.Enabled = false;
                chkspareinventory.Enabled = false;
                chksparereport.Enabled = false;

                chkskill.Enabled = false;
                chkemployeeskill.Enabled = false;
                chkoperationskill.Enabled = false;
            }
        }

        //set grid theme
        public void GridTheme()
        {
            dgvuser.ThemeName = theme;
            dgvusergroup.ThemeName = theme;
            dgvcluster.ThemeName = theme;
            dgvcontroller.ThemeName = theme;
            dgvprodline.ThemeName = theme;
            dgvpusher.ThemeName = theme;
            dgvroute.ThemeName = theme;
            dgvrouting.ThemeName = theme;
            dgvstation.ThemeName = theme;
            dgvhanger.ThemeName = theme;
            dgvbuffergroup.ThemeName = theme;
            dgvbufferstation.ThemeName = theme;
            dgvshifts.ThemeName = theme;
            dgvbreaks.ThemeName = theme;
            dgvholiday.ThemeName = theme;
            dgvhideday.ThemeName = theme;

            //disable close button on search in grid
            dgvuser.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvusergroup.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvcluster.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvcontroller.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvprodline.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvpusher.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvroute.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvrouting.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvstation.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvhanger.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvbuffergroup.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvbufferstation.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvshifts.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvbreaks.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvholiday.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvhideday.MasterView.TableSearchRow.ShowCloseButton = false;

            dgvuser.MasterTemplate.SelectLastAddedRow = false;
            dgvusergroup.MasterTemplate.SelectLastAddedRow = false;
            dgvcluster.MasterTemplate.SelectLastAddedRow = false;
            dgvcontroller.MasterTemplate.SelectLastAddedRow = false;
            dgvprodline.MasterTemplate.SelectLastAddedRow = false;
            dgvpusher.MasterTemplate.SelectLastAddedRow = false;
            dgvroute.MasterTemplate.SelectLastAddedRow = false;
            dgvrouting.MasterTemplate.SelectLastAddedRow = false;
            dgvstation.MasterTemplate.SelectLastAddedRow = false;
            dgvhanger.MasterTemplate.SelectLastAddedRow = false;
            dgvbuffergroup.MasterTemplate.SelectLastAddedRow = false;
            dgvbufferstation.MasterTemplate.SelectLastAddedRow = false;
            dgvshifts.MasterTemplate.SelectLastAddedRow = false;
            dgvbreaks.MasterTemplate.SelectLastAddedRow = false;
            dgvholiday.MasterTemplate.SelectLastAddedRow = false;
            dgvhideday.MasterTemplate.SelectLastAddedRow = false;
        }

        public void RefereshGrid_Buffer()
        {
            //get buffer group details
            SqlDataAdapter sda = new SqlDataAdapter("Select V_BUFFER_GROUP_ID,V_BUFFER_GROUP_DESC from BUFFER_GROUP", dc.con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "BUFFER_GROUP");
            dgvbuffergroup.DataSource = ds.Tables["BUFFER_GROUP"].DefaultView;
            dgvbuffergroup.Columns["V_BUFFER_GROUP_ID"].HeaderText = "Buffer Group ID";
            dgvbuffergroup.Columns["V_BUFFER_GROUP_DESC"].HeaderText = "Buffer Group Desc";
            dgvbuffergroup.Visible = false;

            if (dgvbuffergroup.Rows.Count > 0)
            {
                dgvbuffergroup.Visible = true;
            }
        }

        public void RefereshGrid_BufferStation()
        {
            for (int i = 0; i < dgvbufferstation.Rows.Count; i++)
            {
                dgvbufferstation.Rows[i].IsVisible = true;
            }
            dgvbufferstation.Rows.Clear();

            //get buffer group stations
            SqlDataAdapter da = new SqlDataAdapter("select G.V_BUFFER_GROUP_DESC,S.V_BUFFER_STATION_NO,S.V_ID from BUFFER_STATION S, BUFFER_GROUP G where G.V_BUFFER_GROUP_ID=S.V_BUFFER_GROUP_ID", dc.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvbufferstation.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
            }

            dgvbufferstation.Visible = false;
            if (dgvbufferstation.Rows.Count > 0)
            {
                dgvbufferstation.Visible = true;
            }

            //get all the buffer groups
            da = new SqlDataAdapter("select distinct V_BUFFER_GROUP_DESC from BUFFER_GROUP", dc.con);
            dt = new DataTable();
            da.Fill(dt);
            cmbbuffergroup.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbbuffergroup.Items.Add(dt.Rows[i][0].ToString());
            }
            cmbbuffergroup.Text = "--SELECT--";

            //get station no
            da = new SqlDataAdapter("select s.I_INFEED_LINE_NO,s.I_STN_NO_INFEED from STATION_DATA s where s.I_STATION_TYPE=3", dc.con);
            dt = new DataTable();
            da.Fill(dt);
            cmbbufferstation.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbbufferstation.Items.Add(dt.Rows[i][0].ToString() + "." + dt.Rows[i][1].ToString());
            }

            cmbbufferstation.Text = "--SELECT--";
        }

        public void RefereshBreaks()
        {
            dgvbreaks.Rows.Clear();

            //get the shift break details
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_SHIFT,V_BREAKS,T_BREAK_TIME_START,T_BREAK_TIME_END from SHIFT_BREAKS", dc.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvbreaks.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
            }

            dgvshifts.Visible = false;
            if (dgvshifts.Rows.Count > 0)
            {
                dgvshifts.Visible = true;
            }
            da.Dispose();
        }

        public void RefereshShifts()
        {
            //get shift details
            SqlDataAdapter da = new SqlDataAdapter("select  s.V_SHIFT,convert(varchar, s.T_SHIFT_START_TIME, 108) as T_SHIFT_START_TIME,convert(varchar, s.T_SHIFT_END_TIME, 108)as T_SHIFT_END_TIME,convert(varchar, s.T_OVERTIME_END_TIME, 108)as T_OVERTIME_END_TIME from SHIFTS s", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "SHIFTS");
            dgvshifts.DataSource = ds.Tables["SHIFTS"].DefaultView;
            dgvshifts.Columns["V_SHIFT"].HeaderText = "Shift";
            dgvshifts.Columns["T_SHIFT_START_TIME"].HeaderText = "Shift Start Time";
            dgvshifts.Columns["T_SHIFT_END_TIME"].HeaderText = "Shift End Time";
            dgvshifts.Columns["T_OVERTIME_END_TIME"].HeaderText = "OverTime End Time";
            dgvshifts.Visible = false;

            if (dgvshifts.Rows.Count > 0)
            {
                dgvshifts.Visible = true;
            }
            da.Dispose();
        }

        public void RefereshCluster()
        {
            //get cluster details
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_CLUSTER_ID,V_CLUSTER_IP_ADDRESS from CLUSTER_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "CLUSTER_DB");
            dgvcluster.DataSource = ds.Tables["CLUSTER_DB"].DefaultView;
            dgvcluster.Columns["V_CLUSTER_ID"].HeaderText = "Cluster ID";
            dgvcluster.Columns["V_CLUSTER_IP_ADDRESS"].HeaderText = "Cluster IP Address";
            dgvcluster.Visible = false;

            if (dgvcluster.Rows.Count > 0)
            {
                dgvcluster.Visible = true;
            }
            da.Dispose();
        }

        public void RefereshHoliday()
        {
            //get holiday details
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_ID,D_HOLIDAY,V_HOLIDAY_DESC from HOLIDAY_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "HOLIDAY_DB");
            dgvholiday.DataSource = ds.Tables["HOLIDAY_DB"].DefaultView;
            dgvholiday.Columns["D_HOLIDAY"].HeaderText = "Date";
            dgvholiday.Columns["V_HOLIDAY_DESC"].HeaderText = "Description";
            dgvholiday.Visible = false;

            dgvholiday.Columns[0].IsVisible = false;
            if (dgvholiday.Rows.Count > 0)
            {
                dgvholiday.Visible = true;
            }
            da.Dispose();
        }

        public void RefereshHideday()
        {
            //get hide day details
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_ID,D_HIDEDAY,V_HIDEDAY_DESC,V_HIDE_ENABLE from HIDEDAY_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "HIDEDAY_DB");
            dgvhideday.DataSource = ds.Tables["HIDEDAY_DB"].DefaultView;
            dgvhideday.Columns["D_HIDEDAY"].HeaderText = "Date";
            dgvhideday.Columns["V_HIDEDAY_DESC"].HeaderText = "Description";
            dgvhideday.Columns["V_HIDE_ENABLE"].HeaderText = "Enabled";
            dgvhideday.Visible = false;
            dgvhideday.Columns[0].IsVisible = false;

            if (dgvhideday.Rows.Count > 0)
            {
                dgvhideday.Visible = true;
            }
            da.Dispose();
        }

        public void RefereshWeekoffs()
        {
            //get week offs details
            String weekoff = "";
            SqlCommand cmd = new SqlCommand("select WEEK_OFF from Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                weekoff = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            String[] dayoff = weekoff.Split(',');
            for (int i = 0; i < dayoff.Length; i++)
            {
                if (dayoff[i] == "Sunday")
                {
                    chksunday.Checked = true;
                }

                if (dayoff[i] == "Monday")
                {
                    chkmonday.Checked = true;
                }

                if (dayoff[i] == "Tuesday")
                {
                    chktuesday.Checked = true;
                }

                if (dayoff[i] == "Wednesday")
                {
                    chkwednesday.Checked = true;
                }

                if (dayoff[i] == "Thursday")
                {
                    chkthursday.Checked = true;
                }

                if (dayoff[i] == "Friday")
                {
                    chkfriday.Checked = true;
                }

                if (dayoff[i] == "Saturday")
                {
                    chksaturday.Checked = true;
                }
            }
        }

        public void RefereshUser()
        {
            //get user details
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_USERNAME,V_PASSWORD,V_USER_GROUP FROM USER_LOGIN", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "USER_LOGIN");
            dgvuser.DataSource = ds.Tables["USER_LOGIN"].DefaultView;
            dgvuser.Columns["V_USERNAME"].HeaderText = "Username";
            dgvuser.Columns["V_PASSWORD"].HeaderText = "Password";
            dgvuser.Columns["V_USER_GROUP"].HeaderText = "User Group";
            dgvuser.Visible = false;

            if (dgvuser.Rows.Count > 0)
            {
                dgvuser.Visible = true;
            }
            dgvuser.Columns[1].IsVisible = false;
            da.Dispose();
        }

        public void RefereshUserGroup()
        {
            //get user group details
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_USERGROUP,V_DESCRIPTION FROM USER_GROUP_NAMES", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "USER_GROUP_NAMES");
            dgvusergroup.DataSource = ds.Tables["USER_GROUP_NAMES"].DefaultView;
            dgvusergroup.Columns["V_USERGROUP"].HeaderText = "User Groups";
            dgvusergroup.Columns["V_DESCRIPTION"].HeaderText = "Description";
            dgvusergroup.Visible = false;

            if (dgvusergroup.Rows.Count > 0)
            {
                dgvusergroup.Visible = true;
            }
            da.Dispose();
        }

        public void RefereshGrid()
        {
            //get controller details
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_CONTROLLER_ID,V_ENABLED FROM CONTROLLER_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "CONTROLLER_DB");
            dgvcontroller.DataSource = ds.Tables["CONTROLLER_DB"].DefaultView;
            dgvcontroller.Columns["V_CONTROLLER_ID"].HeaderText = "Controller ID";
            dgvcontroller.Columns["V_ENABLED"].HeaderText = "Enabled";
            dgvcontroller.Visible = false;

            if (dgvcontroller.Rows.Count > 0)
            {
                dgvcontroller.Visible = true;
            }
            da.Dispose();
        }

        public void RefreshGrid()
        {
            //get station details
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT STN_ID,INFEED_LINENO,OUTFEED_LINENO,STN_NO_INFEED,STN_NO_OUTFEED,INFEEDOFFSET,INFEEDCHAINOFFSET,OUTFEEDOFFSET,OUTFEEDCHAINOFFSET,STATIONTYPE,AUTOLOGIN FROM stationdata", dc.conn);
            DataSet ds = new DataSet();
            sda.Fill(ds, "stationdata");
            dgvstation.DataSource = ds.Tables["stationdata"].DefaultView;
            dgvstation.Columns["STN_ID"].HeaderText = "Station ID";
            dgvstation.Columns["INFEED_LINENO"].HeaderText = "Infeed Line No";
            dgvstation.Columns["OUTFEED_LINENO"].HeaderText = "Outfeed Line No";
            dgvstation.Columns["STN_NO_INFEED"].HeaderText = "Station No Infeed";
            dgvstation.Columns["STN_NO_OUTFEED"].HeaderText = "Station No Outfeed";
            dgvstation.Columns["INFEEDOFFSET"].HeaderText = "Infeed Offset";
            dgvstation.Columns["INFEEDCHAINOFFSET"].HeaderText = "Infeed Chain Offset";
            dgvstation.Columns["OUTFEEDOFFSET"].HeaderText = "Outfeed Offset";
            dgvstation.Columns["OUTFEEDCHAINOFFSET"].HeaderText = "Outfeed Chain Offset";
            dgvstation.Columns["STATIONTYPE"].HeaderText = "Station Type";
            dgvstation.Columns["AUTOLOGIN"].HeaderText = "Auto Login";
            btneditstation.Enabled = true;
            btnsavestation.Enabled = true;
            dgvstation.Visible = false;

            if (dgvstation.Rows.Count > 0)
            {
                dgvstation.Visible = true;
            }
            sda.Dispose();
        }

        public void RowSelected()
        {
            if (dgvcontroller.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String Id = dgvcontroller.SelectedRows[0].Cells[0].Value + string.Empty;
                String enabled = dgvcontroller.SelectedRows[0].Cells[1].Value + string.Empty;
                txtcontrollerid.Text = Id;

                if (enabled == "TRUE")
                {
                    chkcontrollerenable.Checked = true;
                }
                else
                {
                    chkcontrollerenable.Checked = false;
                }

                txtcontrollerid.Enabled = false;
                btnsavecontroller.Text = update;
                btndeletecontroller.Enabled = true;
            }
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
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

            //check if special field is enabled
            if (chkuser1.Checked == true)
            {
                u1 = "TRUE";
            }
            else
            {
                u1 = "FALSE";
            }

            //check if special field is enabled
            if (chkuser2.Checked == true)
            {
                u2 = "TRUE";
            }
            else
            {
                u2 = "FALSE";
            }

            //check if special field is enabled
            if (chkuser3.Checked == true)
            {
                u3 = "TRUE";
            }
            else
            {
                u3 = "FALSE";
            }

            //check if special field is enabled
            if (chkuser4.Checked == true)
            {
                u4 = "TRUE";
            }
            else
            {
                u4 = "FALSE";
            }

            //check if special field is enabled
            if (chkuser5.Checked == true)
            {
                u5 = "TRUE";
            }
            else
            {
                u5 = "FALSE";
            }

            //check if special field is enabled
            if (chkuser6.Checked == true)
            {
                u6 = "TRUE";
            }
            else
            {
                u6 = "FALSE";
            }

            //check if special field is enabled
            if (chkuser7.Checked == true)
            {
                u7 = "TRUE";
            }
            else
            {
                u7 = "FALSE";
            }

            //check if special field is enabled
            if (chkuser8.Checked == true)
            {
                u8 = "TRUE";
            }
            else
            {
                u8 = "FALSE";
            }

            //check if special field is enabled
            if (chkuser9.Checked == true)
            {
                u9 = "TRUE";
            }
            else
            {
                u9 = "FALSE";
            }

            //check if special field is enabled
            if (chkuser10.Checked == true)
            {
                u10 = "TRUE";
            }
            else
            {
                u10 = "FALSE";
            }

            //check if special field is empty
            if (txtuser1.Text == "")
            {
                txtuser1.Text = "Special_Field1";
            }

            if (txtuser2.Text == "")
            {
                txtuser2.Text = "Special_Field2";
            }

            if (txtuser3.Text == "")
            {
                txtuser3.Text = "Special_Field3";
            }

            if (txtuser4.Text == "")
            {
                txtuser4.Text = "Special_Field4";
            }

            if (txtuser5.Text == "")
            {
                txtuser5.Text = "Special_Field5";
            }

            if (txtuser6.Text == "")
            {
                txtuser6.Text = "Special_Field6";
            }

            if (txtuser7.Text == "")
            {
                txtuser7.Text = "Special_Field7";
            }

            if (txtuser8.Text == "")
            {
                txtuser8.Text = "Special_Field8";
            }

            if (txtuser9.Text == "")
            {
                txtuser9.Text = "Special_Field9";
            }

            if (txtuser10.Text == "")
            {
                txtuser10.Text = "Special_Field10";
            }

            //trim empty spaces
            txtuser1.Text = txtuser1.Text.Trim();
            txtuser2.Text = txtuser2.Text.Trim();
            txtuser3.Text = txtuser3.Text.Trim();
            txtuser4.Text = txtuser4.Text.Trim();
            txtuser5.Text = txtuser5.Text.Trim();
            txtuser6.Text = txtuser6.Text.Trim();
            txtuser7.Text = txtuser7.Text.Trim();
            txtuser8.Text = txtuser8.Text.Trim();
            txtuser9.Text = txtuser9.Text.Trim();
            txtuser10.Text = txtuser10.Text.Trim();

            //check if special field is valid
            Regex r = new Regex("^[a-zA-Z0-9_]*$");
            if (!(r.IsMatch(txtuser1.Text)))
            {
                radLabel4.Text = "Invalid Charecters in User Defined 1";
                return;
            }

            //check if special field is valid
            if (!(r.IsMatch(txtuser2.Text)))
            {
                radLabel4.Text = "Invalid Charecters in User Defined 2";
                return;
            }

            //check if special field is valid
            if (!(r.IsMatch(txtuser3.Text)))
            {
                radLabel4.Text = "Invalid Charecters in User Defined 3";
                return;
            }

            //check if special field is valid
            if (!(r.IsMatch(txtuser4.Text)))
            {
                radLabel4.Text = "Invalid Charecters in User Defined 4";
                return;
            }

            //check if special field is valid
            if (!(r.IsMatch(txtuser5.Text)))
            {
                radLabel4.Text = "Invalid Charecters in User Defined 5";
                return;
            }

            //check if special field is valid
            if (!(r.IsMatch(txtuser6.Text)))
            {
                radLabel4.Text = "Invalid Charecters in User Defined 6";
                return;
            }

            //check if special field is valid
            if (!(r.IsMatch(txtuser7.Text)))
            {
                radLabel4.Text = "Invalid Charecters in User Defined 7";
                return;
            }

            //check if special field is valid
            if (!(r.IsMatch(txtuser8.Text)))
            {
                radLabel4.Text = "Invalid Charecters in User Defined 8";
                return;
            }

            //check if special field is valid
            if (!(r.IsMatch(txtuser9.Text)))
            {
                radLabel4.Text = "Invalid Charecters in User Defined 9";
                return;
            }

            //check if special field is valid
            if (!(r.IsMatch(txtuser10.Text)))
            {
                radLabel4.Text = "Invalid Charecters in User Defined 10";
                return;
            }

            //delete if any
            SqlCommand cmd = new SqlCommand("DELETE FROM USER_COLUMN_NAMES", dc.con);
            cmd.ExecuteNonQuery();

            //insert into user_column_names
            cmd = new SqlCommand("insert into USER_COLUMN_NAMES values('USER_DEF1',N'" + txtuser1.Text + "','" + u1 + "')", dc.con);
            cmd.ExecuteNonQuery();

            //insert into user_column_names
            cmd = new SqlCommand("insert into USER_COLUMN_NAMES values('USER_DEF2',N'" + txtuser2.Text + "','" + u2 + "')", dc.con);
            cmd.ExecuteNonQuery();

            //insert into user_column_names
            cmd = new SqlCommand("insert into USER_COLUMN_NAMES values('USER_DEF3',N'" + txtuser3.Text + "','" + u3 + "')", dc.con);
            cmd.ExecuteNonQuery();

            //insert into user_column_names
            cmd = new SqlCommand("insert into USER_COLUMN_NAMES values('USER_DEF4',N'" + txtuser4.Text + "','" + u4 + "')", dc.con);
            cmd.ExecuteNonQuery();

            //insert into user_column_names
            cmd = new SqlCommand("insert into USER_COLUMN_NAMES values('USER_DEF5',N'" + txtuser5.Text + "','" + u5 + "')", dc.con);
            cmd.ExecuteNonQuery();

            //insert into user_column_names
            cmd = new SqlCommand("insert into USER_COLUMN_NAMES values('USER_DEF6',N'" + txtuser6.Text + "','" + u6 + "')", dc.con);
            cmd.ExecuteNonQuery();

            //insert into user_column_names
            cmd = new SqlCommand("insert into USER_COLUMN_NAMES values('USER_DEF7',N'" + txtuser7.Text + "','" + u7 + "')", dc.con);
            cmd.ExecuteNonQuery();

            //insert into user_column_names
            cmd = new SqlCommand("insert into USER_COLUMN_NAMES values('USER_DEF8',N'" + txtuser8.Text + "','" + u8 + "')", dc.con);
            cmd.ExecuteNonQuery();

            //insert into user_column_names
            cmd = new SqlCommand("insert into USER_COLUMN_NAMES values('USER_DEF9',N'" + txtuser9.Text + "','" + u9 + "')", dc.con);
            cmd.ExecuteNonQuery();

            //insert into user_column_names
            cmd = new SqlCommand("insert into USER_COLUMN_NAMES values('USER_DEF10',N'" + txtuser10.Text + "','" + u10 + "')", dc.con);
            cmd.ExecuteNonQuery();

            radLabel4.Text = "Record Updated";
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

        private void radLabel1_Click(object sender, EventArgs e)
        {

        }

        private void radButton2_Click(object sender, EventArgs e)
        {
            //conrfirm box before changing the language
            DialogResult result = RadMessageBox.Show("Applying Changes will Restart the GUI?", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
            if (result.Equals(DialogResult.Yes))
            {
                SqlCommand cmd = new SqlCommand("update Setup set Language='" + cmblanguage.Text + "'", dc.con);
                cmd.ExecuteNonQuery();
                Application.Restart();
            }

            btnapplylanguage.ForeColor = Color.Lime;
        }

        private void Setup_Initialized(object sender, EventArgs e)
        {
            flg = 1;
            dc.OpenConnection();   //open connection

            String Lang = "";
            String skill = "";
            String backup = "";
            String overtime = "";

            //get setup details
            SqlCommand cmd = new SqlCommand("SELECT Language,SKILL_EFFICIENCY,BACKUP_PATH,BACKUP_TIME,BACKUP_ENABLE,HIDE_OVERTIME FROM Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Lang = sdr.GetValue(0).ToString();
                skill = sdr.GetValue(1).ToString();
                txtbackuppath.Text = sdr.GetValue(2).ToString();
                cmbbackuptimer.Text = sdr.GetValue(3).ToString();
                backup = sdr.GetValue(4).ToString();
                overtime = sdr.GetValue(5).ToString();
            }
            sdr.Close();

            if (skill == "TRUE")
            {
                chkskillrate.Checked = true;
            }
            else
            {
                chkskillrate.Checked = false;
            }

            if (backup == "TRUE")
            {
                chktimerenable.Checked = true;
            }
            else
            {
                chktimerenable.Checked = false;
            }

            if (overtime == "TRUE")
            {
                chkhideot.Checked = true;
            }
            else
            {
                chkhideot.Checked = false;
            }

            //radDropDownList2.Text = controller;
            btnupdategeneral.ForeColor = Color.Lime;

            //change form language
            SqlDataAdapter sda = new SqlDataAdapter("select " + Lang + " from Language where Form='Setup' order by Item_No", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                pagecontroller1.Text = dt.Rows[0][0].ToString();
                pageaddcontroller.Text = dt.Rows[25][0].ToString();
                lblcontrollerid.Text = dt.Rows[1][0].ToString() + " :";
                lblcontrollerenabled.Text = dt.Rows[2][0].ToString() + " :";
                btndeletecontroller.Text = dt.Rows[3][0].ToString();
                btnsavecontroller.Text = dt.Rows[4][0].ToString();

                pagespecial.Text = dt.Rows[5][0].ToString();
                lblsf1.Text = dt.Rows[6][0].ToString() + "1" + " :";
                lblsf2.Text = dt.Rows[6][0].ToString() + "2" + " :";
                lblsf3.Text = dt.Rows[6][0].ToString() + "3" + " :";
                lblsf4.Text = dt.Rows[6][0].ToString() + "4" + " :";
                lblsf5.Text = dt.Rows[6][0].ToString() + "5" + " :";
                lblsf6.Text = dt.Rows[6][0].ToString() + "6" + " :";
                lblsf7.Text = dt.Rows[6][0].ToString() + "7" + " :";
                lblsf8.Text = dt.Rows[6][0].ToString() + "8" + " :";
                lblsf9.Text = dt.Rows[6][0].ToString() + "9" + " :";
                lblsf10.Text = dt.Rows[6][0].ToString() + "10" + " :";
                lblsfenabled.Text = dt.Rows[2][0].ToString();
                btnsvaespecial.Text = dt.Rows[4][0].ToString();

                pagegeneral.Text = dt.Rows[7][0].ToString();
                lbllang.Text = dt.Rows[8][0].ToString() + " :";
                btnapplylanguage.Text = dt.Rows[9][0].ToString();

                pagestation1.Text = dt.Rows[10][0].ToString();
                lblstnid.Text = dt.Rows[11][0].ToString() + " :";
                lblinfeedlineno.Text = dt.Rows[12][0].ToString() + " :";
                lbloutfeedlineno.Text = dt.Rows[13][0].ToString() + " :";
                lblstationnoinfeed.Text = dt.Rows[14][0].ToString() + " :";
                lblstationnooutfeed.Text = dt.Rows[15][0].ToString() + " :";
                lblinfeedoffset.Text = dt.Rows[16][0].ToString() + " :";
                lbloutfeedoffset.Text = dt.Rows[17][0].ToString() + " :";
                lblinfeedchainoffset.Text = dt.Rows[18][0].ToString() + " :";
                lbloutfeedchainoffset.Text = dt.Rows[19][0].ToString() + " :";
                lblstntype.Text = dt.Rows[20][0].ToString() + " :";
                btndeletestation.Text = dt.Rows[3][0].ToString();
                btnsavestation.Text = dt.Rows[4][0].ToString();
                btnsyncstationdata.Text = dt.Rows[24][0].ToString();

                pagerouting1.Text = dt.Rows[21][0].ToString();
                lblroutingid.Text = dt.Rows[22][0].ToString() + " :";
                lblstep.Text = dt.Rows[23][0].ToString() + " :";
                lblroutingstnid.Text = dt.Rows[11][0].ToString() + " :";
                btndeleterouting.Text = dt.Rows[3][0].ToString();
                btnsaverouting.Text = dt.Rows[4][0].ToString();

                pageroute.Text = dt.Rows[26][0].ToString();
                lblrouteid.Text = dt.Rows[22][0].ToString() + " :";
                lblsourceline.Text = dt.Rows[28][0].ToString() + " :";
                lbldestline.Text = dt.Rows[29][0].ToString() + " :";
                btndeleteroute.Text = dt.Rows[3][0].ToString();
                btnsaveroute.Text = dt.Rows[4][0].ToString();

                pagepusher.Text = dt.Rows[27][0].ToString();
                lbllineno.Text = dt.Rows[30][0].ToString() + " :";
                lblpushercount.Text = dt.Rows[31][0].ToString() + " :";
                lblchaincount.Text = dt.Rows[32][0].ToString() + " :";
                btndeletepusher.Text = dt.Rows[3][0].ToString();
                btnsavepusher.Text = dt.Rows[4][0].ToString();

                save = dt.Rows[4][0].ToString();
                update = dt.Rows[33][0].ToString();
                btnsavehanger.Text = save;

                lblhangerenable.Text = dt.Rows[2][0].ToString() + " :";

                pageuseraccount.Text = dt.Rows[34][0].ToString();
                pagecreateuser.Text = dt.Rows[35][0].ToString();
                pageallowaccessprv.Text = dt.Rows[36][0].ToString();
                pageadddeletegruop.Text = dt.Rows[37][0].ToString();

                lblusername.Text = dt.Rows[38][0].ToString() + " :";
                lblpassword.Text = dt.Rows[39][0].ToString() + " :";
                lblconfirmpass.Text = dt.Rows[40][0].ToString() + " :";

                lblusergroup.Text = dt.Rows[41][0].ToString() + " :";
                lblselectusergruop.Text = dt.Rows[42][0].ToString() + " :";
                lblusergroupname.Text = dt.Rows[43][0].ToString() + " :";
                lblgroupdesc.Text = dt.Rows[44][0].ToString() + " :";

                lblhangerlimit.Text = dt.Rows[45][0].ToString() + " :";
                pagehangerlimit.Text = dt.Rows[45][0].ToString();

                btnsaveshift.Text = save;
                btndeleteshift.Text = dt.Rows[3][0].ToString();
            }
            sda.Dispose();

            sda = new SqlDataAdapter("select " + Lang + " from Language where Form='Prodline' order by Item_No", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            if (dt.Rows.Count > 0)
            {
                lblprodline.Text = dt.Rows[0][0].ToString() + " :";
                lblfactoryname.Text = dt.Rows[1][0].ToString() + " :";
                lblfactoryno.Text = dt.Rows[2][0].ToString() + " :";
                lblbuildingno.Text = dt.Rows[3][0].ToString() + " :";
                lblfloorno.Text = dt.Rows[4][0].ToString() + " :";
                lblsectionno.Text = dt.Rows[5][0].ToString() + " :";
                lblipaddress.Text = dt.Rows[6][0].ToString() + " :";
                lblport.Text = dt.Rows[7][0].ToString() + " :";
                lblprodcontroller.Text = dt.Rows[8][0].ToString() + " :";
                btndeleteprodline.Text = dt.Rows[9][0].ToString();
                btnsaveprodline.Text = dt.Rows[10][0].ToString();
            }
        }        

        //http post method
        public String webPostMethod(String postData, String URL)
        {
            String responseFromServer = "";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Method = "POST";
            request.Timeout = 2000;
            request.Credentials = CredentialCache.DefaultCredentials;

            ((HttpWebRequest)request).UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)";
            request.Accept = "/";
            request.UseDefaultCredentials = true;
            request.Proxy.Credentials = CredentialCache.DefaultCredentials;

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);
            responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        //3-des decrypt method
        public static string DecryptPassword(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            AppSettingsReader settingsReader = new AppSettingsReader();
            string key = "WETHEPEOPLEOFINDIAHAVING";
            //key = "GNIVAHAIDNIFOELPOEPEHTEW";

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;

            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        //3-des decrypt method
        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            AppSettingsReader settingsReader = new AppSettingsReader();
            string key = "WETHEPEOPLEOFINDIAHAVING";
            key = "GNIVAHAIDNIFOELPOEPEHTEW";

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            return UTF8Encoding.UTF8.GetString(resultArray);
        }
               

        public void RowSelected1()
        {
            try
            {
                if (dgvstation.SelectedRows.Count > 0) // make sure user select at least 1 row 
                {
                    txtstationid.Text = dgvstation.SelectedRows[0].Cells[0].Value + string.Empty;
                    txtinfeedlineno.Text = dgvstation.SelectedRows[0].Cells[1].Value + string.Empty;
                    txtoutfeedlineno.Text = dgvstation.SelectedRows[0].Cells[2].Value + string.Empty;
                    txtstationnoinfeed.Text = dgvstation.SelectedRows[0].Cells[3].Value + string.Empty;
                    txtstationnooutfeed.Text = dgvstation.SelectedRows[0].Cells[4].Value + string.Empty;
                    txtinfeedoffset.Text = dgvstation.SelectedRows[0].Cells[5].Value + string.Empty;
                    txtinfeedchainoffset.Text = dgvstation.SelectedRows[0].Cells[6].Value + string.Empty;
                    txtoutfeedoffset.Text = dgvstation.SelectedRows[0].Cells[7].Value + string.Empty;
                    txtoutfeedchainoffset.Text = dgvstation.SelectedRows[0].Cells[8].Value + string.Empty;
                    String stn = dgvstation.SelectedRows[0].Cells[9].Value + string.Empty;

                    if (dgvstation.SelectedRows[0].Cells[10].Value + string.Empty == "0")
                    {
                        chkautologin.Checked = false;
                    }
                    else
                    {
                        chkautologin.Checked = true;
                    }

                    MySqlDataAdapter sda1 = new MySqlDataAdapter("Select TYPE from stationtype where ID='" + stn + "'", dc.conn);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    //cmbstationtype.Items.Clear();
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        cmbstationtype.Text = dt1.Rows[i][0].ToString();
                    }

                    btnsavestation.Text = update;
                    btndeletestation.Enabled = true;
                    lineNo = txtinfeedlineno.Text;
                    sda1.Dispose();
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void radButton6_Click(object sender, EventArgs e)
        {
            try
            {
                //delete selected row
                MySqlCommand cmd = new MySqlCommand("Delete from stationdata where STN_ID='" + txtstationid.Text + "'", dc.conn);
                cmd.ExecuteNonQuery();

                //delete selected row
                SqlCommand cmd1 = new SqlCommand("Delete from STATION_DATA where I_STN_ID='" + txtstationid.Text + "'", dc.con);
                cmd1.ExecuteNonQuery();

                radLabel4.Text = "Record Deleted";
                RefreshGrid();    //get the master

                txtstationid.Enabled = true;
                btnsavestation.Text = save;
                ClearData();    //clear all fields

                btndeletestation.Enabled = false;
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        //clear all fields
        public void ClearData()
        {
            txtstationid.Text = "";
            txtinfeedlineno.Text = "";
            txtoutfeedlineno.Text = "";
            txtstationnoinfeed.Text = "";
            txtstationnooutfeed.Text = "";
            txtinfeedoffset.Text = "";
            txtinfeedchainoffset.Text = "";
            txtoutfeedoffset.Text = "";
            txtoutfeedchainoffset.Text = "";
            cmbstationtype.Text = "--SELECT--";
            btnsavestation.ForeColor = Color.Lime;
        }

        public void RefreshGrid_Routing()
        {
            //get routing details
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT ID,ROUTE_ID,STEP,STN_ID from routingdata", dc.conn);
            DataSet ds = new DataSet();
            sda.Fill(ds, "routingdata");
            dgvrouting.DataSource = ds.Tables["routingdata"].DefaultView;
            dgvrouting.Columns["ID"].HeaderText = "ID";
            dgvrouting.Columns["ROUTE_ID"].HeaderText = "Route ID";
            dgvrouting.Columns["STEP"].HeaderText = "Step";
            dgvrouting.Columns["STN_ID"].HeaderText = "Staion ID";
            btneditrouting.Enabled = true;
            btnsaverouting.Enabled = true;
            dgvrouting.Visible = false;

            if (dgvrouting.Rows.Count > 0)
            {
                dgvrouting.Visible = true;
            }
            sda.Dispose();
        }

        public void RowSelected2()
        {
            if (dgvrouting.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                routeID = dgvrouting.SelectedRows[0].Cells[0].Value + string.Empty;
                cmbroutingid.Text = dgvrouting.SelectedRows[0].Cells[1].Value + string.Empty;
                txtstep.Text = dgvrouting.SelectedRows[0].Cells[2].Value + string.Empty;
                cmbstationid.Text = dgvrouting.SelectedRows[0].Cells[3].Value + string.Empty;
                btnsaverouting.Text = update;
                btndeleterouting.Enabled = true;
            }
        }

        //clear all fields
        public void ClearData_Routing()
        {
            cmbroutingid.Text = "";
            cmbroutingid.Text = "";
            txtstep.Text = "";
            btnsaverouting.ForeColor = Color.Lime;
        }

        private void radDropDownList4_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //get the line ipaddress
            String IP = "";
            SqlDataAdapter sda = new SqlDataAdapter("Select distinct V_IP_ADDRESS from PROD_LINE_DB where V_CONTROLLER='" + cmbprodcontroller.Text + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IP = dt.Rows[i][0].ToString();
            }
            sda.Dispose();

            if (IP == "")
            {
                return;
            }

            dc.OpenMYSQLConnection(IP);   //open connection
            RefreshGrid_Production();   //get the master
        }

        public void RefreshGrid_Production()
        {
            //get the production line details
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT MO_NO,MO_LINE,IN_PROD,CUR_COUNT from prod", dc.conn);
            DataSet ds = new DataSet();
            sda.Fill(ds, "routingdata");
            dgvproduction.DataSource = ds.Tables["routingdata"].DefaultView;
            dgvproduction.Columns["MO_NO"].HeaderText = "MO No";
            dgvproduction.Columns["MO_LINE"].HeaderText = "MO Line";
            dgvproduction.Columns["IN_PROD"].HeaderText = "In Production";
            dgvproduction.Columns["CUR_COUNT"].HeaderText = "Piece Count";
            dgvproduction.Visible = false;
            btndeleteproduction.Enabled = true;

            if (dgvproduction.Rows.Count > 0)
            {
                dgvproduction.Visible = true;
            }
            sda.Dispose();
        }

        private void radButton12_Click(object sender, EventArgs e)
        {
            //if (dgvproduction.SelectedRows.Count > 0)
            //{
            //    String MO = dgvproduction.SelectedRows[0].Cells[0].Value.ToString();
            //    String MOLINE = dgvproduction.SelectedRows[0].Cells[1].Value.ToString();

            //    MySqlCommand cmd = new MySqlCommand("Select count(*) from hangerhistory where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
            //    int count = int.Parse(cmd.ExecuteScalar() + "");

            //    cmd = new MySqlCommand("Select count(*) from hangerwip where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
            //    int count1 = int.Parse(cmd.ExecuteScalar() + "");
            //    if (count == 0 & count1 == 0)
            //    {
            //        cmd = new MySqlCommand("Delete from prod where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
            //        cmd.ExecuteNonQuery();

            //        radLabel4.Text = "Record Deleted";
            //        RefreshGrid_Production();

            //        return;
            //    }
            //    else
            //    {
            //        DialogResult result = RadMessageBox.Show("Deleting the MO will also Delete all the Hanger History for this MO", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
            //        if (result.Equals(DialogResult.Yes))
            //        {
            //            cmd = new MySqlCommand("Delete from hangerhistory where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
            //            cmd.ExecuteNonQuery();

            //            cmd = new MySqlCommand("Delete from hangerwip where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
            //            cmd.ExecuteNonQuery();

            //            cmd = new MySqlCommand("Delete from prod where MO_NO='" + MO + "' and MO_LINE='" + MOLINE + "'", dc.conn);
            //            cmd.ExecuteNonQuery();

            //            radLabel4.Text = "Record Deleted";
            //        }
            //    }
            //}
        }

        private void radPageView1_SelectedPageChanged(object sender, EventArgs e)
        {
            ClearAllData();   //clear all fields
            //select cluster setup by default
            if (vpagesetup.SelectedPage == pagecontroller1)
            {
                vpagecontroller.SelectedPage = pageaddclusterdb;
            }
        }

        private void btneditcontroller_Click(object sender, EventArgs e)
        {
            RowSelected();   //get the selected row
        }

        private void dgvcontroller_DoubleClick(object sender, EventArgs e)
        {
            RowSelected();    //get the selected row
        }

        private void btnsavecontroller_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtcontrollerid.Text != "")
                {
                    String enabled = "";
                    if (chkcontrollerenable.Checked == true)
                    {
                        enabled = "TRUE";
                    }
                    else
                    {
                        enabled = "FALSE";
                    }

                    btndeletecontroller.Enabled = false;
                    if (btnsavecontroller.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from CONTROLLER_DB where V_CONTROLLER_ID='" + txtcontrollerid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //check if id adlready exists
                        if (i == 0)
                        {
                            //insert
                            SqlCommand cmd = new SqlCommand("insert into CONTROLLER_DB values('" + txtcontrollerid.Text + "','" + enabled + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            radLabel4.Text = "Records Saved";
                            RefereshGrid();   //get the master

                            txtcontrollerid.Enabled = true;
                            txtcontrollerid.Text = "";
                            chkcontrollerenable.Checked = false;
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvcontroller.Rows.Count; j++)
                            {
                                if (dgvcontroller.Rows[j].Cells[0].Value.ToString().Equals(txtcontrollerid.Text))
                                {
                                    dgvcontroller.Rows[j].IsSelected = true;
                                    radLabel4.Text = "Controller Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsavecontroller.Text == update)
                    {
                        //update
                        SqlCommand cmd = new SqlCommand("Update CONTROLLER_DB set V_ENABLED='" + enabled + "' where V_CONTROLLER_ID='" + txtcontrollerid.Text + "'", dc.con);
                        cmd.ExecuteNonQuery();

                        //update
                        cmd = new SqlCommand("update PROD_LINE_DB set V_CONTROLLER_ENABLED='" + enabled + "' where V_CONTROLLER='" + txtcontrollerid.Text + "'", dc.con);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = "Records Updated";
                        RefereshGrid();   //get the master

                        txtcontrollerid.Enabled = true;
                        btnsavecontroller.Text = save;
                        txtcontrollerid.Text = "";
                        chkcontrollerenable.Checked = false;
                    }
                    btnsavecontroller.ForeColor = Color.Lime;
                }
                else
                {
                    radLabel4.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btndeletecontroller_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from CONTROLLER_DB where V_CONTROLLER_ID='" + txtcontrollerid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Record Deleted";
                radPanel2.Visible = true;
                RefereshGrid();    //get the master

                txtcontrollerid.Enabled = true;
                btnsavecontroller.Text = save;
                btndeletecontroller.Enabled = false;
                txtcontrollerid.Text = "";
                chkcontrollerenable.Checked = false;
                btnsavecontroller.ForeColor = Color.Lime;
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void cmbstncontroller_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            select_controller();   //get the selected controller
        }

        private void btnsyncstationdata_Click_1(object sender, EventArgs e)
        {
            try
            {
                String IP = "";

                //get ipaddress of the controller
                SqlDataAdapter sda = new SqlDataAdapter("Select distinct V_IP_ADDRESS,V_CONTROLLER from PROD_LINE_DB where V_CONTROLLER_ENABLED='TRUE'", dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IP = dt.Rows[i][0].ToString();
                    if (IP == "")
                    {
                        return;
                    }

                    dc.OpenMYSQLConnection(IP);   //open connection

                    MySqlDataAdapter sda1 = new MySqlDataAdapter("SELECT STN_ID,INFEED_LINENO,OUTFEED_LINENO,STN_NO_INFEED,STN_NO_OUTFEED,INFEEDOFFSET,INFEEDCHAINOFFSET,OUTFEEDOFFSET,OUTFEEDCHAINOFFSET,STATIONTYPE FROM stationdata", dc.conn);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    sda1.Dispose();
                    if (dt1.Rows.Count > 0)
                    {
                        //delete from the station data
                        SqlCommand cmd1 = new SqlCommand("Delete from STATION_DATA", dc.con);
                        cmd1.ExecuteNonQuery();
                    }

                    //insert
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        SqlCommand cmd3 = new SqlCommand("insert into STATION_DATA values('" + dt1.Rows[j][0].ToString() + "','" + dt1.Rows[j][1].ToString() + "','" + dt1.Rows[j][2].ToString() + "','" + dt1.Rows[j][3].ToString() + "','" + dt1.Rows[j][4].ToString() + "','" + dt1.Rows[j][5].ToString() + "','" + dt1.Rows[j][6].ToString() + "','" + dt1.Rows[j][7].ToString() + "','" + dt1.Rows[j][8].ToString() + "','" + dt1.Rows[j][9].ToString() + "','" + dt.Rows[i][1].ToString() + "')", dc.con);
                        cmd3.ExecuteNonQuery();
                        radLabel4.Text = "Synchronization Completed";
                    }

                    dc.Close_Connection();  //close connection
                }

                dc.Close_Connection();   //close connection
                select_controller();   //get the selected controller
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btnsavestation_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtstationid.Text != "" || txtinfeedlineno.Text != "" || txtoutfeedlineno.Text != "" || txtstationnoinfeed.Text != "" || txtstationnooutfeed.Text != "" || txtinfeedoffset.Text != "" || txtinfeedchainoffset.Text != "" || txtoutfeedoffset.Text != "" || txtoutfeedchainoffset.Text != "" || cmbstationtype.Text != "" || cmbstationtype.Text != "--SELECT--")
                {
                    //check if controller is selected
                    if (cmbstncontroller.Text == "--SELECT--" || cmbstncontroller.Text == "")
                    {
                        radLabel4.Text = "Select the Controller";
                        return;
                    }

                    //check if station is valid
                    int n;
                    if (!(int.TryParse(txtstationid.Text, out n)))
                    {
                        radLabel4.Text = "Invalid Station Id";
                        return;
                    }

                    //check if station infeed in valid
                    if (!(int.TryParse(txtstationnoinfeed.Text, out n)))
                    {
                        radLabel4.Text = "Invalid Station No Infeed";
                        return;
                    }

                    //check if station outfeed is valid
                    if (!(int.TryParse(txtstationnooutfeed.Text, out n)))
                    {
                        radLabel4.Text = "Invalid Station No OutFeed";
                        return;
                    }

                    btndeletestation.Enabled = false;
                    String stn = "";

                    //get the id
                    MySqlDataAdapter sda1 = new MySqlDataAdapter("Select ID from stationtype where TYPE='" + cmbstationtype.Text + "'", dc.conn);
                    DataTable dt1 = new DataTable();
                    sda1.Fill(dt1);
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        stn = dt1.Rows[i][0].ToString();
                    }
                    sda1.Dispose();

                    if (btnsavestation.Text == save)
                    {
                        //get id count
                        MySqlCommand cmd1 = new MySqlCommand("select count(*) from stationdata where STN_ID='" + txtstationid.Text + "'", dc.conn);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //check if the id exists
                        if (i == 0)
                        {
                            //insert
                            MySqlCommand cmd = new MySqlCommand("insert into stationdata (STN_ID,INFEED_LINENO,OUTFEED_LINENO,STN_NO_INFEED,STN_NO_OUTFEED,INFEEDOFFSET,INFEEDCHAINOFFSET,OUTFEEDOFFSET,OUTFEEDCHAINOFFSET,STATIONTYPE,LIMIT_ENABLED,HANGER_LIMIT,INFEED_ELEVATOR,OUTFEED_ELEVATOR,JEFF_ISSUE,AUTOLOGIN) values('" + txtstationid.Text + "','" + txtinfeedlineno.Text + "','" + txtoutfeedlineno.Text + "','" + txtstationnoinfeed.Text + "','" + txtstationnooutfeed.Text + "','" + txtinfeedoffset.Text + "','" + txtinfeedchainoffset.Text + "','" + txtoutfeedoffset.Text + "','" + txtoutfeedchainoffset.Text + "','" + stn + "','0','25','0','0','0','" + autologin + "')", dc.conn);
                            cmd.ExecuteNonQuery();

                            //insert
                            SqlCommand cmd3 = new SqlCommand("insert into STATION_DATA values('" + txtstationid.Text + "','" + txtinfeedlineno.Text + "','" + txtoutfeedlineno.Text + "','" + txtstationnoinfeed.Text + "','" + txtstationnooutfeed.Text + "','" + txtinfeedoffset.Text + "','" + txtinfeedchainoffset.Text + "','" + txtoutfeedoffset.Text + "','" + txtoutfeedchainoffset.Text + "','" + stn + "','" + cmbstncontroller.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            radLabel4.Text = "Records Saved";
                            RefreshGrid();   //get the master

                            txtstationid.Enabled = true;
                            ClearData();   //clear all fields
                        }
                        else
                        {
                            radLabel4.Text = "Station ID Already Exists";
                        }
                    }
                    else if (btnsavestation.Text == update)
                    {
                        //update
                        MySqlCommand cmd = new MySqlCommand("update stationdata set infeed_lineno='" + txtinfeedlineno.Text + "',outfeed_lineno='" + txtoutfeedlineno.Text + "',stn_no_infeed='" + txtstationnoinfeed.Text + "',stn_no_outfeed='" + txtstationnooutfeed.Text + "',infeedoffset='" + txtinfeedoffset.Text + "',infeedchainoffset='" + txtinfeedchainoffset.Text + "',outfeedoffset='" + txtoutfeedoffset.Text + "',outfeedchainoffset='" + txtoutfeedchainoffset.Text + "',stationtype='" + stn + "',AUTOLOGIN='" + autologin + "' where stn_id='" + txtstationid.Text + "'", dc.conn);
                        cmd.ExecuteNonQuery();

                        //update
                        SqlCommand cmd1 = new SqlCommand("update STATION_DATA set I_INFEED_LINE_NO='" + txtinfeedlineno.Text + "',I_OUTFEED_LINE_NO='" + txtoutfeedlineno.Text + "',I_STN_NO_INFEED='" + txtstationnoinfeed.Text + "',I_STN_NO_OUTFEED='" + txtstationnooutfeed.Text + "',I_INFEED_OFFSET='" + txtinfeedoffset.Text + "',I_INFEED_CHAIN_OFFSET='" + txtinfeedchainoffset.Text + "',I_OUTFEED_OFFSET='" + txtoutfeedoffset.Text + "',I_OUTFEED_CHAIN_OFFSET='" + txtoutfeedchainoffset.Text + "',I_STATION_TYPE='" + stn + "' where I_STN_ID='" + txtstationid.Text + "'", dc.con);
                        cmd1.ExecuteNonQuery();

                        radLabel4.Text = "Records Updated";
                        RefreshGrid();   //get the master
                        txtstationid.Enabled = true;

                        ClearData();    //clear all fields
                        btnsavestation.Text = save;
                    }
                    String ip = "";
                    String port = "";

                    //get the ip address of the prod line
                    SqlDataAdapter sda = new SqlDataAdapter("select I_PORT,V_IP_ADDRESS from PROD_LINE_DB where V_PROD_LINE='" + lineNo + "'", dc.con);
                    dt1 = new DataTable();
                    sda.Fill(dt1);
                    sda.Dispose();
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        port = dt1.Rows[i][0].ToString();
                        ip = dt1.Rows[i][1].ToString();
                    }

                    //update station master
                    //String up = StationUpdate(ip, port);
                }
                else
                {
                    radLabel4.Text = "Fill all the fields";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btndeletestation_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                MySqlCommand cmd = new MySqlCommand("Delete from stationdata where STN_ID='" + txtstationid.Text + "'", dc.conn);
                cmd.ExecuteNonQuery();

                //delete the selected row
                SqlCommand cmd1 = new SqlCommand("Delete from STATION_DATA where I_STN_ID='" + txtstationid.Text + "'", dc.con);
                cmd1.ExecuteNonQuery();

                radLabel4.Text = "Record Deleted";
                RefreshGrid();   //get the master

                txtstationid.Enabled = true;
                btnsavestation.Text = save;

                ClearData();    //clear all fields
                btndeletestation.Enabled = false;
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btneditstation_Click(object sender, EventArgs e)
        {
            RowSelected1();    //get the selected row
        }

        private void dgvstation_DoubleClick(object sender, EventArgs e)
        {
            RowSelected1();     //get the selected row
        }

        private void cmbroutecontroller_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            select_controller();   //get the selected controller
        }

        private void btnsaveroute_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (cmbroutingid.Text != "" || cmbstationid.Text != "" || txtstep.Text != "")
                {
                    //chekc if step is valid
                    int n;
                    if (!(int.TryParse(txtstep.Text, out n)))
                    {
                        radLabel4.Text = "Invalid Step";
                        return;
                    }

                    btndeleterouting.Enabled = false;
                    if (btnsaverouting.Text == save)
                    {
                        //insert
                        MySqlCommand cmd = new MySqlCommand("insert into routingdata (ROUTE_ID, STEP, STN_ID) values('" + cmbroutingid.Text + "','" + txtstep.Text + "','" + cmbstationid.Text + "')", dc.conn);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = "Records Saved";
                        RefreshGrid_Routing();    //get the master

                        txtstationid.Enabled = true;
                        ClearData_Routing();    //clear all fields
                    }
                    else if (btnsaverouting.Text == update)
                    {
                        //update
                        MySqlCommand cmd = new MySqlCommand("update routingdata set route_id='" + cmbroutingid.Text + "',step='" + txtstep.Text + "',stn_id='" + cmbstationid.Text + "' where id='" + routeID + "'", dc.conn);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = "Records Updated";
                        RefreshGrid_Routing();     //get the master

                        ClearData_Routing();    //clear all fields
                        btnsaverouting.Text = save;
                    }
                }
                else
                {
                    radLabel4.Text = "Fill all the fields";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btndeleteroute_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                MySqlCommand cmd = new MySqlCommand("Delete from routingdata where ID='" + routeID + "'", dc.conn);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Record Deleted";
                RefreshGrid_Routing();    //get the master

                btnsaverouting.Text = save;
                ClearData_Routing();    //clear all fields

                btndeleterouting.Enabled = false;
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btneditroute_Click(object sender, EventArgs e)
        {
            RowSelected2();     //get the selected row
        }

        private void dgvroute_DoubleClick(object sender, EventArgs e)
        {
            RowSelected2();     //get the selected row
        }

        private void cmbroutecontroller_SelectedIndexChanged_1(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            select_controller();    //get the selected controller
        }

        public void RefreshGrid_Route()
        {
            //get route details
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT ROUTE_ID,LINE_SOURCE,LINE_DESTINATION from routing", dc.conn);
            DataSet ds = new DataSet();
            sda.Fill(ds, "routing");
            dgvroute.DataSource = ds.Tables["routing"].DefaultView;
            dgvroute.Columns["ROUTE_ID"].HeaderText = "Route ID";
            dgvroute.Columns["LINE_SOURCE"].HeaderText = "Source Line";
            dgvroute.Columns["LINE_DESTINATION"].HeaderText = "Destination Line";
            btneditroute.Enabled = true;
            btnsaveroute.Enabled = true;
            dgvroute.Visible = false;

            if (dgvroute.Rows.Count > 0)
            {
                dgvroute.Visible = true;
            }
            sda.Dispose();
        }

        private void btnsaveroute_Click_1(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtrouteid.Text != "" || cmbdestline.Text != "" || cmbsourceline.Text != "")
                {
                    //check if controller is selected
                    if (cmbroutecontroller.Text == "--SELECT--" || cmbroutecontroller.Text == "")
                    {
                        radLabel4.Text = "Select the Controller";
                        return;
                    }

                    //check if route id is valid
                    int n;
                    if (!(int.TryParse(txtrouteid.Text, out n)))
                    {
                        radLabel4.Text = "Invalid Route ID";
                        return;
                    }

                    btndeleteroute.Enabled = false;
                    if (btnsaveroute.Text == save)
                    {
                        //get id count
                        MySqlCommand cmd2 = new MySqlCommand("select count(*) from routing where ROUTE_ID='" + txtrouteid.Text + "'", dc.conn);
                        Int32 route = int.Parse(cmd2.ExecuteScalar().ToString());
                        if (route != 0)
                        {
                            radLabel4.Text = "Route Id Already Exists";
                            return;
                        }

                        //insert
                        MySqlCommand cmd = new MySqlCommand("insert into routing (ROUTE_ID, LINE_SOURCE, LINE_DESTINATION) values('" + txtrouteid.Text + "','" + cmbsourceline.Text + "','" + cmbdestline.Text + "')", dc.conn);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = "Records Saved";
                        RefreshGrid_Route();    //get the master

                        txtrouteid.Enabled = true;
                        ClearData_Route();      //clear all fields
                    }
                    else if (btnsaveroute.Text == update)
                    {
                        MySqlCommand cmd = new MySqlCommand("update routing set LINE_SOURCE='" + cmbsourceline.Text + "',LINE_DESTINATION='" + cmbdestline.Text + "' where ROUTE_ID='" + txtrouteid.Text + "'", dc.conn);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = "Records Updated";
                        RefreshGrid_Route();    //get the master

                        ClearData_Route();   //clear all fields
                        btnsaveroute.Text = save;
                    }
                }
                else
                {
                    radLabel4.Text = "Fill all the fields";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        //clear all fields
        public void ClearData_Route()
        {
            txtrouteid.Text = "";
            cmbsourceline.Text = "";
            cmbdestline.Text = "";
            btnsaveroute.ForeColor = Color.Lime;
        }

        private void btndeleteroute_Click_1(object sender, EventArgs e)
        {
            try
            {
                //get id count
                MySqlCommand cmd2 = new MySqlCommand("select count(*) from routingdata where ROUTE_ID='" + txtrouteid.Text + "'", dc.conn);
                Int32 route = int.Parse(cmd2.ExecuteScalar().ToString());
                if (route != 0)
                {
                    radLabel4.Text = "Route Id is in Use";
                    return;
                }

                //delete the selected row
                MySqlCommand cmd = new MySqlCommand("Delete from routing where ROUTE_ID='" + txtrouteid.Text + "'", dc.conn);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Record Deleted";
                RefreshGrid_Route();   //get the master

                btnsaveroute.Text = save;
                ClearData_Route();    //clear all fields

                btndeleteroute.Enabled = false;
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btneditroute_Click_1(object sender, EventArgs e)
        {
            RowSelected3();     //get the selected row
        }

        public void RowSelected3()
        {
            if (dgvroute.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                txtrouteid.Text = dgvroute.SelectedRows[0].Cells[0].Value + string.Empty;
                cmbsourceline.Text = dgvroute.SelectedRows[0].Cells[1].Value + string.Empty;
                cmbdestline.Text = dgvroute.SelectedRows[0].Cells[2].Value + string.Empty;

                btnsaveroute.Text = update;
                btndeleteroute.Enabled = true;
            }
        }

        private void dgvroute_DoubleClick_1(object sender, EventArgs e)
        {
            RowSelected3();     //get the selected row
        }

        private void radPageView1_PageIndexChanged(object sender, Telerik.WinControls.UI.RadPageViewIndexChangedEventArgs e)
        {

        }

        private void radPageView1_SelectedPageChanged_1(object sender, EventArgs e)
        {
            ClearAllData();    //clear all fields
        }

        public void ClearAllData()
        {
            //confirm box to save changes
            if (btnsavecontroller.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Controller. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavecontroller.PerformClick();
                }
            }
            else if (btnsavestation.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Station. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavestation.PerformClick();
                }
            }
            else if (btnsaveroute.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Route. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveroute.PerformClick();
                }
            }
            else if (btnsaverouting.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Routing. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaverouting.PerformClick();
                }
            }
            else if (btnsavepusher.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Pusher. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavepusher.PerformClick();
                }
            }
            else if (btnsavecluster.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Cluster. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavecluster.PerformClick();
                }
            }
            else if (btnsaveprodline.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Production Line. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveprodline.PerformClick();
                }
            }
            else if (btnsavehanger.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Hanger Limit. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavehanger.PerformClick();
                }
            }
            else if (btnsavebuffer.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Buffer Group. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavebuffer.PerformClick();
                }
            }
            else if (btnsavebufferstation.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Buffer Stations. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavebufferstation.PerformClick();
                }
            }

            dc.Close_Connection();   //close connection
            dc.OpenConnection();   //open connection

            //get the controller details
            SqlDataAdapter sda = new SqlDataAdapter("Select V_CONTROLLER_ID from CONTROLLER_DB where V_ENABLED='TRUE'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            //clear all fields
            cmbdestline.Items.Clear();
            cmbsourceline.Items.Clear();
            cmbstncontroller.Items.Clear();
            cmbroutecontroller.Items.Clear();
            cmbprodcontroller.Items.Clear();
            cmbcontroller.Items.Clear();
            cmbroutingcontroller.Items.Clear();
            cmbpushercontroller.Items.Clear();
            cmblineno.Items.Clear();
            cmbhangercontroller.Items.Clear();
            txtcontrollerid.Text = "";
            chkcontrollerenable.Checked = false;

            //clear all fields
            ClearData();   
            ClearData_Pusher();
            ClearData_Route();
            ClearData_Routing();
            Clear_Hanger();

            dgvroute.DataSource = null;
            dgvrouting.DataSource = null;
            dgvstation.DataSource = null;
            dgvpusher.DataSource = null;
            dgvhanger.DataSource = null;

            cmbdestline.Text = "--SELECT--";
            cmblineno.Text = "--SELECT--";
            cmbsourceline.Text = "--SELECT--";
            cmbroutingid.Text = "--SELECT--";

            btneditstation.Enabled = false;
            btndeletestation.Enabled = false;
            btnsavestation.Enabled = false;
            btndeleterouting.Enabled = false;
            btneditrouting.Enabled = false;
            btnsaverouting.Enabled = false;
            btndeleteroute.Enabled = false;
            btneditroute.Enabled = false;
            btnsaveroute.Enabled = false;
            btndeleteproduction.Enabled = false;
            btnsavepusher.Enabled = false;
            btndeletepusher.Enabled = false;
            btneditpusher.Enabled = false;
            btnedithanger.Enabled = false;
            btnsavehanger.Enabled = false;

            btnsavecontroller.Text = save;
            btnsavestation.Text = save;
            btnsaveroute.Text = save;
            btnsaverouting.Text = save;
            btnsavepusher.Text = save;
            btnsavehanger.Text = save;

            //add controller
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbstncontroller.Items.Add(dt.Rows[i][0].ToString());
                cmbroutingcontroller.Items.Add(dt.Rows[i][0].ToString());
                cmbroutecontroller.Items.Add(dt.Rows[i][0].ToString());
                cmbprodcontroller.Items.Add(dt.Rows[i][0].ToString());
                cmbpushercontroller.Items.Add(dt.Rows[i][0].ToString());
                cmbhangercontroller.Items.Add(dt.Rows[i][0].ToString());
                cmbcontroller.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //get all the production lines
            sda = new SqlDataAdapter("Select V_PROD_LINE from PROD_LINE_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbsourceline.Items.Add(dt.Rows[i][0].ToString());
                cmbdestline.Items.Add(dt.Rows[i][0].ToString());
                cmblineno.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            //get cluster details
            cmbcluster.Items.Clear();
            sda = new SqlDataAdapter("Select V_CLUSTER_ID from CLUSTER_DB", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbcluster.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            cmbcluster.Text = "--SELECT--";
            select_controller();   //get the selected controller
        }

        private void cmbpushercontroller_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            select_controller();   //get the selected controller
        }

        public void RefreshGrid_Pusher()
        {
            //get pusher details
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT LINE_NO,PUSHER_COUNT,CHAIN_COUNT,PUSHER_TIMING,IP_ADDRESS,PORT,OVERLOAD_ROUNDS from pusherinfo", dc.conn);
            DataSet ds = new DataSet();
            sda.Fill(ds, "pusherinfo");
            dgvpusher.DataSource = ds.Tables["pusherinfo"].DefaultView;
            dgvpusher.Columns["LINE_NO"].HeaderText = "Line No";
            dgvpusher.Columns["PUSHER_COUNT"].HeaderText = "Pusher Count";
            dgvpusher.Columns["CHAIN_COUNT"].HeaderText = "Chain Count";
            dgvpusher.Columns["PUSHER_TIMING"].HeaderText = "Pusher Timing";
            dgvpusher.Columns["IP_ADDRESS"].HeaderText = "IP Address";
            dgvpusher.Columns["PORT"].HeaderText = "Port";
            dgvpusher.Columns["OVERLOAD_ROUNDS"].HeaderText = "Overload Rounds";

            btneditpusher.Enabled = true;
            btnsavepusher.Enabled = true;
            dgvpusher.Visible = false;

            if (dgvpusher.Rows.Count > 0)
            {
                dgvpusher.Visible = true;
            }
            sda.Dispose();
        }

        private void btnsavepusher_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (cmblineno.Text != "" || txtpushercount.Text != "" || txtchaincount.Text != "")
                {
                    //check if pusher count is valid
                    int n;
                    if (!(int.TryParse(txtpushercount.Text, out n)))
                    {
                        radLabel4.Text = "Invalid Pusher Count";
                        return;
                    }

                    //check if chain count is valid
                    if (!(int.TryParse(txtchaincount.Text, out n)))
                    {
                        radLabel4.Text = "Invalid Pusher Count";
                        return;
                    }

                    //check if pusher timing is valid
                    if (!(int.TryParse(txtpushertiming.Text, out n)))
                    {
                        radLabel4.Text = "Invalid Pusher Timing";
                        return;
                    }

                    //check if pusher port is valid
                    if (!(int.TryParse(txtpusherport.Text, out n)))
                    {
                        radLabel4.Text = "Invalid Port";
                        return;
                    }

                    //check if overload round is valid
                    if (!(int.TryParse(txtoverloadrounds.Text, out n)))
                    {
                        radLabel4.Text = "Invalid Overload Rounds";
                        return;
                    }

                    //check if ipaddress is valid
                    IPAddress ip;
                    bool ValidateIP = IPAddress.TryParse(txtpusherip.Text, out ip);
                    if (!ValidateIP)
                    {
                        radLabel4.Text = "Invalid IP Address";
                        return;
                    }

                    btndeletepusher.Enabled = false;
                    if (btnsavepusher.Text == save)
                    {
                        //get id count
                        MySqlCommand cmd2 = new MySqlCommand("select count(*) from pusherinfo where LINE_NO='" + cmblineno.Text + "'", dc.conn);
                        Int32 route = int.Parse(cmd2.ExecuteScalar().ToString());
                        if (route != 0)
                        {
                            radLabel4.Text = "Pusher Count and Chain Count for this Line Already Exists";
                            return;
                        }

                        //insert
                        MySqlCommand cmd = new MySqlCommand("insert into pusherinfo (LINE_NO, PUSHER_COUNT, CHAIN_COUNT, PUSHER_TIMING, CURRENT_PUSHER, IP_ADDRESS, PORT, OVERLOAD_ROUNDS) values('" + cmblineno.Text + "','" + txtpushercount.Text + "','" + txtchaincount.Text + "','" + txtpushertiming.Text + "','0','" + txtpusherip.Text + "','" + txtpusherport.Text + "','" + txtoverloadrounds.Text + "')", dc.conn);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = "Records Saved";
                        RefreshGrid_Pusher();   //get the master

                        cmblineno.Enabled = true;
                        ClearData_Pusher();    //clear all fields
                    }

                    else if (btnsavepusher.Text == update)
                    {
                        //update
                        MySqlCommand cmd = new MySqlCommand("update pusherinfo set PUSHER_COUNT='" + txtpushercount.Text + "', CHAIN_COUNT='" + txtchaincount.Text + "',PUSHER_TIMING='" + txtpushertiming.Text + "',IP_ADDRESS='" + txtpusherip.Text + "',PORT='" + txtpusherport.Text + "',OVERLOAD_ROUNDS='" + txtoverloadrounds.Text + "' where LINE_NO='" + cmblineno.Text + "'", dc.conn);
                        cmd.ExecuteNonQuery();   

                        radLabel4.Text = "Records Updated";
                        RefreshGrid_Pusher();    //get the master

                        ClearData_Pusher();     //clear all fields
                        btnsavepusher.Text = save;
                    }
                }
                else
                {
                    radLabel4.Text = "Fill all the fields";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        //clear all fields
        public void ClearData_Pusher()
        {
            cmblineno.Text = "";
            txtpushercount.Text = "";
            txtchaincount.Text = "";
            btnsavepusher.ForeColor = Color.Lime;
        }

        private void btndeletepusher_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                MySqlCommand cmd = new MySqlCommand("Delete from pusherinfo where LINE_NO='" + cmblineno.Text + "'", dc.conn);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Record Deleted";
                RefreshGrid_Pusher();   //get the master

                btnsavepusher.Text = save;
                ClearData_Pusher();    //clear all fields

                btndeletepusher.Enabled = false;
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btneditpusher_Click(object sender, EventArgs e)
        {
            RowSelected4();   //select row if exists
        }

        public void RowSelected4()
        {
            if (dgvpusher.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                cmblineno.Text = dgvpusher.SelectedRows[0].Cells[0].Value + string.Empty;
                txtpushercount.Text = dgvpusher.SelectedRows[0].Cells[1].Value + string.Empty;
                txtchaincount.Text = dgvpusher.SelectedRows[0].Cells[2].Value + string.Empty;
                txtpushertiming.Text = dgvpusher.SelectedRows[0].Cells[3].Value + string.Empty;
                txtpusherip.Text = dgvpusher.SelectedRows[0].Cells[4].Value + string.Empty;
                txtpusherport.Text = dgvpusher.SelectedRows[0].Cells[5].Value + string.Empty;
                txtoverloadrounds.Text = dgvpusher.SelectedRows[0].Cells[6].Value + string.Empty;

                btnsavepusher.Text = update;
                btndeletepusher.Enabled = true;
            }
        }

        private void dgvpusher_DoubleClick(object sender, EventArgs e)
        {
            RowSelected4();    //select row if exists
        }

        private void cmblanguage_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            btnapplylanguage.ForeColor = Color.Red;
        }

        private void txtcontrollerid_TextChanged(object sender, EventArgs e)
        {
            //check if fields is changed
            if (txtcontrollerid.Text == "" && chkcontrollerenable.Checked == true)
            {
                btnsavecontroller.ForeColor = Color.Lime;
            }
            else
            {
                btnsavecontroller.ForeColor = Color.Red;
            }
        }

        private void txtstationid_TextChanged(object sender, EventArgs e)
        {
            //check if fields is changed
            if (txtstationid.Text == "" && txtinfeedlineno.Text == "" && txtoutfeedlineno.Text == "" && txtstationnoinfeed.Text == "" && txtstationnooutfeed.Text == "" && txtinfeedoffset.Text == "" && txtinfeedchainoffset.Text == "" && txtoutfeedoffset.Text == "" && txtoutfeedchainoffset.Text == "")
            {
                btnsavestation.ForeColor = Color.Lime;
            }
            else
            {
                btnsavestation.ForeColor = Color.Red;
            }
        }

        private void txtstationnoinfeed_TextChanged(object sender, EventArgs e)
        {
            //check if fields is changed
            if (txtstationid.Text == "" && txtinfeedlineno.Text == "" && txtoutfeedlineno.Text == "" && txtstationnoinfeed.Text == "" && txtstationnooutfeed.Text == "" && txtinfeedoffset.Text == "" && txtinfeedchainoffset.Text == "" && txtoutfeedoffset.Text == "" && txtoutfeedchainoffset.Text == "")
            {
                btnsavestation.ForeColor = Color.Lime;
            }
            else
            {
                btnsavestation.ForeColor = Color.Red;
            }
        }

        private void txtstationnooutfeed_TextChanged(object sender, EventArgs e)
        {
            //check if fields is changed
            if (txtstationid.Text == "" && txtinfeedlineno.Text == "" && txtoutfeedlineno.Text == "" && txtstationnoinfeed.Text == "" && txtstationnooutfeed.Text == "" && txtinfeedoffset.Text == "" && txtinfeedchainoffset.Text == "" && txtoutfeedoffset.Text == "" && txtoutfeedchainoffset.Text == "")
            {
                btnsavestation.ForeColor = Color.Lime;
            }
            else
            {
                btnsavestation.ForeColor = Color.Red;
            }
        }
              

        private void txtrouteid_TextChanged(object sender, EventArgs e)
        {
            //check if fields is changed
            if (txtrouteid.Text == "")
            {
                btnsaveroute.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveroute.ForeColor = Color.Red;
            }
        }

        private void cmbstationtype_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            //check if fields is changed
            btnsavestation.ForeColor = Color.Red;
        }

        private void cmbsourceline_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            btnsaveroute.ForeColor = Color.Red;   //check if fields is changed
        }

        private void cmbdestline_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            btnsaveroute.ForeColor = Color.Red;   //check if fields is changed
        }

        private void cmbroutingid_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            btnsaverouting.ForeColor = Color.Red;   //check if fields is changed
        }

        private void cmbstationid_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            btnsaverouting.ForeColor = Color.Red;   //check if fields is changed
        }

        private void cmblineno_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            btnsavepusher.ForeColor = Color.Red;   //check if fields is changed
        }

        private void txtpushercount_TextChanged(object sender, EventArgs e)
        {
            //check if fields is changed
            if (txtpushercount.Text == "" && txtchaincount.Text == "")
            {
                btnsavepusher.ForeColor = Color.Lime;
            }
            else
            {
                btnsavepusher.ForeColor = Color.Red;
            }
        }

        private void txtchaincount_TextChanged(object sender, EventArgs e)
        {
            //check if fields is changed
            if (txtpushercount.Text == "" && txtchaincount.Text == "")
            {
                btnsavepusher.ForeColor = Color.Lime;
            }
            else
            {
                btnsavepusher.ForeColor = Color.Red;
            }
        }

        private void txtstep_TextChanged(object sender, EventArgs e)
        {
            //check if fields is changed
            if (txtstep.Text == "")
            {
                btnsaverouting.ForeColor = Color.Lime;
            }
            else
            {
                btnsaverouting.ForeColor = Color.Red;
            }
        }

        private void Setup_FormClosing(object sender, FormClosingEventArgs e)
        {
            //check if fields is changed
            if (btnupdategeneral.ForeColor == Color.Red)
            {
                vpagesetup.SelectedPage = pagegeneral;

                DialogResult result = RadMessageBox.Show("Unsaved General Setup. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnupdategeneral.PerformClick();
                    e.Cancel = true;
                }
            }
        }

        private void btncreateaccount_Click_1(object sender, EventArgs e)
        {
            //check if username is valid
            if (!Regex.IsMatch(txtusername.Text, "^[a-zA-Z]"))
            {
                radLabel4.Text = "Enter All the Details Properly( Only Alphabetical Characters are Allowed for User Name)";
            }

            //check if passwords match
            else if (txtpassword.Text != txtconfirmPass.Text)
            {
                radLabel4.Text = "Password Doesn't match";
            }

            else if (cmbUsergroup.Text != "--SELECT--")
            {
                //excrypt pssword
                String password = EncryptPassword(txtpassword.Text, false);
                if (btncreateaccount.Text == save)
                {
                    //get id count
                    SqlCommand cmd = new SqlCommand("Select count(*) from USER_LOGIN where V_USERNAME='" + txtusername.Text + "'", dc.con);
                    int count = int.Parse(cmd.ExecuteScalar() + "");
                    if (count != 0)
                    {
                        radLabel4.Text = "Username Already Exists";
                        return;
                    }

                    //insert
                    cmd = new SqlCommand("insert into USER_LOGIN values('" + txtusername.Text + "','" + password + "','" + cmbUsergroup.SelectedItem + "')", dc.con);
                    cmd.ExecuteNonQuery();

                    radLabel4.Text = "Account created";
                }
                else if (btncreateaccount.Text == update)
                {
                    //update
                    SqlCommand cmd = new SqlCommand("update USER_LOGIN set V_PASSWORD='" + password + "',V_USER_GROUP='" + cmbUsergroup.Text + "' where V_USERNAME='" + txtusername.Text + "'", dc.con);
                    cmd.ExecuteNonQuery();

                    radLabel4.Text = "Records Updated";
                }

                btncreateaccount.Text = save;
                btncreateaccount.ForeColor = Color.Lime;
                txtpassword.Text = "";
                txtconfirmPass.Text = "";
                txtusername.Text = "";

                txtusername.Enabled = true;
                RefreshComboBox();   //get the master
                RefereshUser();    //clear all fields
            }
            else
            {
                radLabel4.Text = "Select the User Group";
            }
        }

        private void radButton2_Click_1(object sender, EventArgs e)
        {
            //check if user group is valid
            if (string.IsNullOrEmpty(txtusergroupname.Text))
            {
                radLabel4.Text = "Enter User Details Properly";
            }
            //check if group desc is valid
            else if (string.IsNullOrEmpty(txtusergroupdescription.Text))
            {
                radLabel4.Text = "Enter User Details Properly";
            }
            //check if group name valid
            else if (!Regex.IsMatch(txtusergroupname.Text, "^[a-zA-Z]"))
            {
                radLabel4.Text = "Only Alphabetical Characters are Allowed for User Group and  Description";
                txtusergroupdescription.Text.Remove(txtusergroupdescription.Text.Length - 1);
                txtusergroupname.Text.Remove(txtusergroupname.Text.Length - 1);

            }
            else
            {
                try
                {
                    if (btnsaveusergroup.Text == save)
                    {
                        //get id count
                        SqlCommand cmd = new SqlCommand("Select count(*) from USER_GROUP_NAMES where V_USERGROUP='" + txtusergroupname.Text + "'", dc.con);
                        int count = int.Parse(cmd.ExecuteScalar() + "");
                        if (count != 0)
                        {
                            radLabel4.Text = "User Group Already Exists";
                            return;
                        }

                        //insert
                        cmd = new SqlCommand("INSERT INTO USER_GROUP_NAMES values('" + txtusergroupname.Text + "', '" + txtusergroupdescription.Text + "') ;", dc.con);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = "User Group Created";
                    }
                    else if (btnsaveusergroup.Text == update)
                    {
                        //update
                        SqlCommand cmd = new SqlCommand("update USER_GROUP_NAMES set V_DESCRIPTION='" + txtusergroupdescription.Text + "' WHERE V_USERGROUP = '" + txtusergroupname.Text + "'", dc.con);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = "Records Updated";
                    }

                    btnsaveusergroup.Text = save;
                    btnsaveusergroup.ForeColor = Color.Lime;
                    txtusergroupdescription.Text = "";
                    txtusergroupname.Text = "";
                    txtusergroupname.Enabled = true;

                    RefreshComboBox();    //get the master
                    RefereshUserGroup();   //get the master
                }
                catch (Exception ex)
                {
                    radLabel4.Text = ex.Message;
                }
            }
        }

        public void RefreshComboBox()
        {
            txtusergroupname.Text = "";
            txtusergroupdescription.Text = "";
            cmbUsergroup.Items.Clear();
            cmdusergroupaccessprivilage.Items.Clear();

            //get all the user group
            SqlDataAdapter da2 = new SqlDataAdapter("select distinct V_USERGROUP FROM USER_GROUP_NAMES  ", dc.con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                cmbUsergroup.Items.Add(dt2.Rows[i]["V_USERGROUP"].ToString());
                cmdusergroupaccessprivilage.Items.Add(dt2.Rows[i]["V_USERGROUP"].ToString());
            }

            cmbUsergroup.Text = "--SELECT--";
            cmdusergroupaccessprivilage.Text = "--SELECT--";
            da2.Dispose();
        }

        private void radButton6_Click_1(object sender, EventArgs e)
        {
            //delete the selected row
            SqlCommand cmd = new SqlCommand("DELETE from USER_GROUP_NAMES  where V_USERGROUP='" + txtusergroupname.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            txtusergroupname.Text = "";
            txtusergroupdescription.Text = "";
            txtusergroupname.Enabled = true;

            radLabel4.Text = "The User Group has been deleted";

            RefreshComboBox();   //get the master
            RefereshUserGroup();   //get the master
        }

        private void chkboxmasters_CheckStateChanged(object sender, EventArgs e)
        {
            //check all the check box fo master
            chkboxcolor.Checked = true;
            chkboxarticle.Checked = true;
            chkboxsize.Checked = true;
            chkboxemployee.Checked = true;            
            chkboxcontractor.Checked = true;
            chkboxcustomer.Checked = true;
            chkboxoperation.Checked = true;
            chkqcmain.Checked = true;
            chkqcsub.Checked = true;
            chkemployeegroupcategory.Checked = true;
            chkemployeegroups.Checked = true;
            chkmachines.Checked = true;
            chkmachinedetails.Checked = true;
            chkemployeeskilllevel.Checked = true;
            chkmbmain.Checked = true;
            chkmbsub.Checked = true;
            chkdesignsequence.Checked = true;
            chksparemain.Checked = true;
            chksparesub.Checked = true;
            chkemployeelogs.Checked = true;

            //uncheck all check box for master
            if (chkboxmasters.Checked == false)
            {
                chkboxemployee.Checked = false;
                chkboxcolor.Checked = false;
                chkboxcontractor.Checked = false;
                chkboxarticle.Checked = false;
                chkboxsize.Checked = false;
                chkboxcustomer.Checked = false;
                chkboxoperation.Checked = false;
                chkqcmain.Checked = false;
                chkqcsub.Checked = false;
                chkemployeegroupcategory.Checked = false;
                chkemployeegroups.Checked = false;
                chkmachines.Checked = false;
                chkmachinedetails.Checked = false;
                chkemployeeskilllevel.Checked = false;
                chkmbmain.Checked = false;
                chkmbsub.Checked = false;
                chkdesignsequence.Checked = false;
                chksparemain.Checked = false;
                chksparesub.Checked = false;
                chkemployeelogs.Checked = false;
            }

            //check if basic version of pms server is installed
            if (Database_Connection.SET_PMSCIENT == "1")
            {
                chkqcmain.Checked = false;
                chkqcsub.Checked = false;
                chkmbmain.Checked = false;
                chkmbsub.Checked = false;
                chksparemain.Checked = false;
                chksparesub.Checked = false;
                chkmachines.Checked = false;
                chkmachinedetails.Checked = false;
            }
        }
        private void chkboxspecialfields_CheckStateChanged(object sender, EventArgs e)
        {
            //check all checkbox for special fields
            chkboxuser1.Checked = true;
            chkboxuser2.Checked = true;
            chkboxuser3.Checked = true;
            chkboxuser4.Checked = true;
            chkboxuser5.Checked = true;
            chkboxuser6.Checked = true;
            chkboxuser7.Checked = true;
            chkboxuser8.Checked = true;
            chkboxuser9.Checked = true;
            chkboxuser10.Checked = true;

            //unckeck all check box for specil field
            if (chkboxspecialfields.Checked == false)
            {
                chkboxuser1.Checked = false;
                chkboxuser2.Checked = false;
                chkboxuser3.Checked = false;
                chkboxuser4.Checked = false;
                chkboxuser5.Checked = false;
                chkboxuser6.Checked = false;
                chkboxuser7.Checked = false;
                chkboxuser8.Checked = false;
                chkboxuser9.Checked = false;
                chkboxuser10.Checked = false;
            }
        }
        private void chkreports_CheckStateChanged(object sender, EventArgs e)
        {
            //check all checkbox for reports
            chkboxemployeereport.Checked = true;
            chkboxstationreport.Checked = true;
            chkmoreport.Checked = true;
            chkstationproductionreport.Checked = true;
            chkemployeeqcreport.Checked = true;
            chkoperationqcreport.Checked = true;            
            chkmoqcreport.Checked = true;
            chkstationqcreport.Checked = true;
            chkpayrollreport.Checked = true;
            chkmachinereport.Checked = true;
            chkmachineassign.Checked = true;
            chkmachinerepair.Checked = true;
            chktopdefects.Checked = true;
            chksparereport.Checked = true;
            chkspareinventory.Checked = true;
            chkperfomance.Checked = true;

            //uncheck all checkbox for reports
            if (chkreports.Checked == false)
            {
                chkboxemployeereport.Checked = false;
                chkboxstationreport.Checked = false;
                chkmoreport.Checked = false;
                chkstationproductionreport.Checked = false;
                chkemployeeqcreport.Checked = false;
                chkoperationqcreport.Checked = false;
                chkpayrollreport.Checked = false;
                chkmoqcreport.Checked = false;
                chkstationqcreport.Checked = false;
                chkmachinereport.Checked = false;
                chkmachineassign.Checked = false;
                chkmachinerepair.Checked = false;
                chktopdefects.Checked = false;
                chksparereport.Checked = false;
                chkspareinventory.Checked = false;
                chkperfomance.Checked = false;
            }

            //check is basic version of pms server is enabled
            if (Database_Connection.SET_PMSCIENT == "1")
            {
                chkemployeeqcreport.Checked = false;
                chkoperationqcreport.Checked = false;
                chkmoqcreport.Checked = false;
                chkstationqcreport.Checked = false;
                chkmachinereport.Checked = false;
                chkmachineassign.Checked = false;
                chkmachinerepair.Checked = false;
                chktopdefects.Checked = false;
                chksparereport.Checked = false;
                chkspareinventory.Checked = false;
                chkperfomance.Checked = false;
                chkpayrollreport.Checked = false;
            }
        }
        private void chkboxmo_CheckStateChanged(object sender, EventArgs e)
        {
            //check all checkbox for skills
            chkboxnewmo.Checked = true;
            chkboxopenmo.Checked = true;

            //uncheck all checkbox for skills
            if (chkboxmo.Checked == false)
            {
                chkboxnewmo.Checked = false;
                chkboxopenmo.Checked = false;
            }
        }
        private void chkboxhome_CheckStateChanged(object sender, EventArgs e)
        {
            //check all checkbox for home
            chkboxsetup.Checked = true;
            chkemployeelogs.Checked = true;

            //uncheck all checkbox for home
            if (chkboxhome.Checked == false)
            {
                chkboxsetup.Checked = false;
                chkemployeelogs.Checked = false;
            }
        }

        private void chkboxproduction_CheckStateChanged(object sender, EventArgs e)
        {
            //check all checkbox for production
            chkboxaddproduction.Checked = true;
            chkboxstationassign.Checked = true;
            chkproductionplanning.Checked = true;
            chkbuffer.Checked = true;
            chkrestoreproduction.Checked = true;
            chkcurrentproduction.Checked = true;
            chkstationwip.Checked = true;
            chklinebalancing.Checked = true;

            //uncheck all checkbox for production
            if (chkboxproduction.Checked == false)
            {
                chkboxaddproduction.Checked = false;
                chkboxstationassign.Checked = false;
                chkproductionplanning.Checked = false;
                chkbuffer.Checked = false;
                chkrestoreproduction.Checked = false;
                chkcurrentproduction.Checked = false;
                chklinebalancing.Checked = false;
                chkstationwip.Checked = false;
            }

            //check is basic version of pms server is enabled
            if (Database_Connection.SET_PMSCIENT == "1")
            {
                chkproductionplanning.Checked = false;
                chkbuffer.Checked = false;
                chklinebalancing.Checked = false;
                chkstationwip.Checked = false;
            }
        }

        private void radButton3_Click(object sender, EventArgs e)
        {
            //get access previlages
            String home = "";
            String setup = "";
            String mono = "";
            String addmo = "";
            String openmo = "";
            String production = "";
            String stationassign = "";
            String addtoproduction = "";
            String masters = "";
            String color = "";
            String article = "";
            String size = "";
            String emp = "";
            String contractor = "";
            String customer = "";
            String operation = "";
            String special = "";
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
            String reports = "";
            String empreport = "";
            String stationreport = "";
            String logout = "";
            String buffer = "";
            String restoreprod = "";
            String qcmain = "";
            String qcsub = "";
            String groupcategory = "";
            String empgroup = "";
            String machines = "";
            String machinedetails = "";
            String mbmain = "";
            String mbsub = "";
            String empskill_level = "";
            String production_plan = "";
            String current_prod = "";
            String skill = "";
            String empskill = "";
            String opskill = "";
            String moreport = "";
            String stn_prod_report = "";
            String empqcreport = "";
            String opqcreport = "";
            String payrollreport = "";
            String emplogs = "";
            String moqcreport = "";
            String stationqcreport = "";
            String machinereport = "";
            String machineassign = "";
            String machinerepair = "";
            String topdefects = "";
            String designsequence = "";
            String stationwip = "";
            String linebalancing = "";
            String performancereport = "";
            String sparereport = "";
            String spareinventory = "";
            String sparemain = "";
            String sparesub = "";
            String moopreport = "";

            //check if checkbox is checked
            if (chkbuffer.Checked == true)
            {
                buffer = "Y";
            }
            else
            {
                buffer = "N";
            }

            if (chkrestoreproduction.Checked == true)
            {
                restoreprod = "Y";
            }
            else
            {
                restoreprod = "N";
            }

            if (chkboxhome.Checked == true)
            {
                home = "Y";
            }
            else
            {
                home = "N";
            }

            if (chkboxsetup.Checked == true)
            {
                setup = "Y";
            }
            else
            {
                setup = "N";
            }

            if (chkboxmo.Checked == true)
            {
                mono = "Y";
            }
            else
            {
                mono = "N";
            }

            if (chkboxnewmo.Checked == true)
            {
                addmo = "Y";
            }
            else
            {
                addmo = "N";
            }

            if (chkboxopenmo.Checked == true)
            {
                openmo = "Y";
            }
            else
            {
                openmo = "N";
            }

            if (chkboxproduction.Checked == true)
            {
                production = "Y";
            }
            else
            {
                production = "N";
            }

            if (chkboxaddproduction.Checked == true)
            {
                addtoproduction = "Y";
            }
            else
            {
                addtoproduction = "N";
            }

            if (chkboxstationassign.Checked == true)
            {
                stationassign = "Y";
            }
            else
            {
                stationassign = "N";
            }

            if (chkreports.Checked == true)
            {
                reports = "Y";
            }
            else
            {
                reports = "N";
            }

            if (chkboxemployeereport.Checked == true)
            {
                empreport = "Y";
            }
            else
            {
                empreport = "N";
            }

            if (chkboxstationreport.Checked == true)
            {
                stationreport = "Y";
            }
            else
            {
                stationreport = "N";
            }

            if (chkboxmasters.Checked == true)
            {
                masters = "Y";
            }
            else
            {
                masters = "N";
            }

            if (chkboxcolor.Checked == true)
            {
                color = "Y";
            }
            else
            {
                color = "N";
            }

            if (chkboxarticle.Checked == true)
            {
                article = "Y";
            }
            else
            {
                article = "N";
            }

            if (chkboxsize.Checked == true)
            {
                size = "Y";
            }
            else
            {
                size = "N";
            }

            if (chkboxemployee.Checked == true)
            {
                emp = "Y";
            }
            else
            {
                emp = "N";
            }

            if (chkboxcontractor.Checked == true)
            {
                contractor = "Y";
            }
            else
            {
                contractor = "N";
            }

            if (chkboxcustomer.Checked == true)
            {
                customer = "Y";
            }
            else
            {
                customer = "N";
            }

            if (chkboxoperation.Checked == true)
            {
                operation = "Y";
            }
            else
            {
                operation = "N";
            }

            if (chkboxspecialfields.Checked == true)
            {
                special = "Y";
            }
            else
            {
                special = "N";
            }

            if (chkboxuser1.Checked == true)
            {
                user1 = "Y";
            }
            else
            {
                user1 = "N";
            }

            if (chkboxuser2.Checked == true)
            {
                user2 = "Y";
            }
            else
            {
                user2 = "N";
            }

            if (chkboxuser3.Checked == true)
            {
                user3 = "Y";
            }
            else
            {
                user3 = "N";
            }

            if (chkboxuser4.Checked == true)
            {
                user4 = "Y";
            }
            else
            {
                user4 = "N";
            }

            if (chkboxuser5.Checked == true)
            {
                user5 = "Y";
            }
            else
            {
                user5 = "N";
            }

            if (chkboxuser6.Checked == true)
            {
                user6 = "Y";
            }
            else
            {
                user6 = "N";
            }
            if (chkboxuser7.Checked == true)
            {
                user7 = "Y";
            }
            else
            {
                user7 = "N";
            }

            if (chkboxuser8.Checked == true)
            {
                user8 = "Y";
            }
            else
            {
                user8 = "N";
            }

            if (chkboxuser9.Checked == true)
            {
                user9 = "Y";
            }
            else
            {
                user9 = "N";
            }

            if (chkboxuser10.Checked == true)
            {
                user10 = "Y";
            }
            else
            {
                user10 = "N";
            }

            if (chklogout.Checked == true)
            {
                logout = "Y";
            }
            else
            {
                logout = "N";
            }

            if (chkqcmain.Checked == true)
            {
                qcmain = "Y";
            }
            else
            {
                qcmain = "N";
            }

            if (chkqcsub.Checked == true)
            {
                qcsub = "Y";
            }
            else
            {
                qcsub = "N";
            }

            if (chkemployeegroupcategory.Checked == true)
            {
                groupcategory = "Y";
            }
            else
            {
                groupcategory = "N";
            }

            if (chkemployeegroups.Checked == true)
            {
                empgroup = "Y";
            }
            else
            {
                empgroup = "N";
            }

            if (chkemployeeskilllevel.Checked == true)
            {
                empskill_level = "Y";
            }
            else
            {
                empskill_level = "N";
            }

            if (chkmachines.Checked == true)
            {
                machines = "Y";
            }
            else
            {
                machines = "N";
            }

            if (chkmachinedetails.Checked == true)
            {
                machinedetails = "Y";
            }
            else
            {
                machinedetails = "N";
            }

            if (chkmbmain.Checked == true)
            {
                mbmain = "Y";
            }
            else
            {
                mbmain = "N";
            }

            if (chkmbsub.Checked == true)
            {
                mbsub = "Y";
            }
            else
            {
                mbsub = "N";
            }

            if (chkskill.Checked == true)
            {
                skill = "Y";
            }
            else
            {
                skill = "N";
            }

            if (chkemployeeskill.Checked == true)
            {
                empskill = "Y";
            }
            else
            {
                empskill = "N";
            }

            if (chkoperationskill.Checked == true)
            {
                opskill = "Y";
            }
            else
            {
                opskill = "N";
            }

            if (chkmoreport.Checked == true)
            {
                moreport = "Y";
            }
            else
            {
                moreport = "N";
            }

            if (chkstationproductionreport.Checked == true)
            {
                stn_prod_report = "Y";
            }
            else
            {
                stn_prod_report = "N";
            }

            if (chkemployeeqcreport.Checked == true)
            {
                empqcreport = "Y";
            }
            else
            {
                empqcreport = "N";
            }

            if (chkoperationqcreport.Checked == true)
            {
                opqcreport = "Y";
            }
            else
            {
                opqcreport = "N";
            }

            if (chkpayrollreport.Checked == true)
            {
                payrollreport = "Y";
            }
            else
            {
                payrollreport = "N";
            }

            if (chkemployeelogs.Checked == true)
            {
                emplogs = "Y";
            }
            else
            {
                emplogs = "N";
            }

            if (chkproductionplanning.Checked == true)
            {
                production_plan = "Y";
            }
            else
            {
                production_plan = "N";
            }

            if (chkcurrentproduction.Checked == true)
            {
                current_prod = "Y";
            }
            else
            {
                current_prod = "N";
            }

            if (chkmoqcreport.Checked == true)
            {
                moqcreport = "Y";
            }
            else
            {
                moqcreport = "N";
            }

            if (chkstationqcreport.Checked == true)
            {
                stationqcreport = "Y";
            }
            else
            {
                stationqcreport = "N";
            }

            if (chkmachinereport.Checked == true)
            {
                machinereport = "Y";
            }
            else
            {
                machinereport = "N";
            }

            if (chkmachineassign.Checked == true)
            {
                machineassign = "Y";
            }
            else
            {
                machineassign = "N";
            }

            if (chkmachinerepair.Checked == true)
            {
                machinerepair = "Y";
            }
            else
            {
                machinerepair = "N";
            }

            if (chktopdefects.Checked == true)
            {
                topdefects = "Y";
            }
            else
            {
                topdefects = "N";
            }

            if (chkdesignsequence.Checked == true)
            {
                designsequence = "Y";
            }
            else
            {
                designsequence = "N";
            }

            if (chkstationwip.Checked == true)
            {
                stationwip = "Y";
            }
            else
            {
                stationwip = "N";
            }

            if (chklinebalancing.Checked == true)
            {
                linebalancing = "Y";
            }
            else
            {
                linebalancing = "N";
            }

            if (chkperfomance.Checked == true)
            {
                performancereport = "Y";
            }
            else
            {
                performancereport = "N";
            }

            if (chksparereport.Checked == true)
            {
                sparereport = "Y";
            }
            else
            {
                sparereport = "N";
            }

            if (chkspareinventory.Checked == true)
            {
                spareinventory = "Y";
            }
            else
            {
                spareinventory = "N";
            }

            if (chksparemain.Checked == true)
            {
                sparemain = "Y";
            }
            else
            {
                sparemain = "N";
            }

            if (chksparesub.Checked == true)
            {
                sparesub = "Y";
            }
            else
            {
                sparesub = "N";
            }

            if (chkmoopreport.Checked == true)
            {
                moopreport = "Y";
            }
            else
            {
                moopreport = "N";
            }

            //delete if the access previlages already exists for the user group
            SqlCommand cmd = new SqlCommand("delete from USER_LOGIN_DETAILS where USER_GROUP='" + cmdusergroupaccessprivilage.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            //insert
            cmd = new SqlCommand("insert into USER_LOGIN_DETAILS values('" + home + "','" + setup + "','" + production + "','" + addtoproduction + "','" + stationassign + "','" + mono + "','" + addmo + "','" + openmo + "','" + masters + "','" + color + "','" + size + "','" + article + "','" + customer + "','" + emp + "','" + contractor + "','Y','" + operation + "','" + special + "','" + user1 + "','" + user2 + "','" + user3 + "','" + user4 + "','" + user5 + "','" + user6 + "','" + user7 + "','" + user8 + "','" + user9 + "','" + user10 + "','" + reports + "','" + empreport + "','" + stationreport + "','" + logout + "','" + cmdusergroupaccessprivilage.Text + "','" + buffer + "','" + restoreprod + "','" + qcmain + "','" + qcsub + "','" + groupcategory + "','" + empgroup + "','" + empskill_level + "','" + machines + "','" + machinedetails + "','" + mbmain + "','" + mbsub + "','" + production_plan + "','" + current_prod + "','" + skill + "','" + empskill + "','" + opskill + "','" + moreport + "','" + stn_prod_report + "','" + empqcreport + "','" + opqcreport + "','" + payrollreport + "','" + emplogs + "','" + moqcreport + "','" + stationqcreport + "','" + machinereport + "','" + machineassign + "','" + machinerepair + "','" + topdefects + "','" + designsequence + "','" + stationwip + "','" + linebalancing + "','" + performancereport + "','" + sparereport + "','" + spareinventory + "','" + sparemain + "','" + sparesub + "','" + moopreport + "')", dc.con);
            cmd.ExecuteNonQuery();

            radLabel4.Text = "Records Inserted Successfully";
        }

        private void cmdusergroupaccessprivilage_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            radPanel1.Visible = true;

            //get access previlages
            String home = "";
            String setup = "";
            String mono = "";
            String addmo = "";
            String openmo = "";
            String production = "";
            String stationassign = "";
            String addtoproduction = "";
            String masters = "";
            String color = "";
            String article = "";
            String size = "";
            String prodline = "";
            String emp = "";
            String contractor = "";
            String customer = "";
            String operation = "";
            String special = "";
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
            String reports = "";
            String empreport = "";
            String stationreport = "";
            String logout = "";
            String buffer = "";
            String restoreprod = "";
            String qcmain = "";
            String qcsub = "";
            String groupcategory = "";
            String empgroup = "";
            String machines = "";
            String machinedetails = "";
            String mbmain = "";
            String mbsub = "";
            String empskill_level = "";
            String production_plan = "";
            String current_prod = "";
            String skill = "";
            String empskill = "";
            String opskill = "";
            String moreport = "";
            String stn_prod_report = "";
            String empqcreport = "";
            String opqcreport = "";
            String payrollreport = "";
            String emplogs = "";
            String moqcreport = "";
            String stationqcreport = "";
            String machinereport = "";
            String machineassign = "";
            String machinerepair = "";
            String topdefects = "";
            String designsequence = "";
            String stationwip = "";
            String linebalancing = "";
            String performancereport = "";
            String sparereport = "";
            String spareinventory = "";
            String sparemain = "";
            String sparesub = "";
            String moopreport = "";

            //get the access previlages for the user group
            SqlDataAdapter sda = new SqlDataAdapter("select * from USER_LOGIN_DETAILS where USER_GROUP='" + cmdusergroupaccessprivilage.Text + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                home = dt.Rows[i][0].ToString();
                setup = dt.Rows[i][1].ToString();
                production = dt.Rows[i][2].ToString();
                addtoproduction = dt.Rows[i][3].ToString();
                stationassign = dt.Rows[i][4].ToString();
                mono = dt.Rows[i][5].ToString();
                addmo = dt.Rows[i][6].ToString();
                openmo = dt.Rows[i][7].ToString();
                masters = dt.Rows[i][8].ToString();
                color = dt.Rows[i][9].ToString();
                size = dt.Rows[i][10].ToString();
                article = dt.Rows[i][11].ToString();
                customer = dt.Rows[i][12].ToString();
                emp = dt.Rows[i][13].ToString();
                contractor = dt.Rows[i][14].ToString();
                prodline = dt.Rows[i][15].ToString();
                operation = dt.Rows[i][16].ToString();
                special = dt.Rows[i][17].ToString();
                user1 = dt.Rows[i][18].ToString();
                user2 = dt.Rows[i][19].ToString();
                user3 = dt.Rows[i][20].ToString();
                user4 = dt.Rows[i][21].ToString();
                user5 = dt.Rows[i][22].ToString();
                user6 = dt.Rows[i][23].ToString();
                user7 = dt.Rows[i][24].ToString();
                user8 = dt.Rows[i][25].ToString();
                user9 = dt.Rows[i][26].ToString();
                user10 = dt.Rows[i][27].ToString();
                reports = dt.Rows[i][28].ToString();
                empreport = dt.Rows[i][29].ToString();
                stationreport = dt.Rows[i][30].ToString();
                logout = dt.Rows[i][31].ToString();
                buffer = dt.Rows[i][33].ToString();
                restoreprod = dt.Rows[i][34].ToString();
                qcmain = dt.Rows[i][35].ToString();
                qcsub = dt.Rows[i][36].ToString();
                groupcategory = dt.Rows[i][37].ToString();
                empgroup = dt.Rows[i][38].ToString();
                empskill_level = dt.Rows[i][39].ToString();
                machines = dt.Rows[i][40].ToString();
                machinedetails = dt.Rows[i][41].ToString();
                mbmain = dt.Rows[i][42].ToString();
                mbsub = dt.Rows[i][43].ToString();
                production_plan = dt.Rows[i][44].ToString();
                current_prod = dt.Rows[i][45].ToString();
                skill = dt.Rows[i][46].ToString();
                empskill = dt.Rows[i][47].ToString();
                opskill = dt.Rows[i][48].ToString();
                moreport = dt.Rows[i][49].ToString();
                stn_prod_report = dt.Rows[i][50].ToString();
                empqcreport = dt.Rows[i][51].ToString();
                opqcreport = dt.Rows[i][52].ToString();
                payrollreport = dt.Rows[i][53].ToString();
                emplogs = dt.Rows[i][54].ToString();
                moqcreport = dt.Rows[i][55].ToString();
                stationqcreport = dt.Rows[i][56].ToString();
                machinereport = dt.Rows[i][57].ToString();
                machineassign = dt.Rows[i][58].ToString();
                machinerepair = dt.Rows[i][59].ToString();
                topdefects = dt.Rows[i][60].ToString();
                designsequence = dt.Rows[i][61].ToString();
                stationwip = dt.Rows[i][62].ToString();
                linebalancing = dt.Rows[i][63].ToString();
                performancereport = dt.Rows[i][64].ToString();
                sparereport = dt.Rows[i][65].ToString();
                spareinventory = dt.Rows[i][66].ToString();
                sparemain = dt.Rows[i][67].ToString();
                sparesub = dt.Rows[i][68].ToString();
                moopreport = dt.Rows[i][69].ToString();
            }

            //chekc if menu is enabled
            if (home == "Y")
            {
                chkboxhome.Checked = true;
            }
            else
            {
                chkboxhome.Checked = false;
            }

            if (reports == "Y")
            {
                chkreports.Checked = true;
            }
            else
            {
                chkreports.Checked = false;
            }

            if (skill == "Y")
            {
                chkskill.Checked = true;
            }
            else
            {
                chkskill.Checked = false;
            }

            if (production == "Y")
            {
                chkboxproduction.Checked = true;
            }
            else
            {
                chkboxproduction.Checked = false;
            }

            if (masters == "Y")
            {
                chkboxmasters.Checked = true;
            }
            else
            {
                chkboxmasters.Checked = false;
            }

            if (mono == "Y")
            {
                chkboxmo.Checked = true;
            }
            else
            {
                chkboxmo.Checked = false;
            }

            if (buffer == "Y")
            {
                chkbuffer.Checked = true;
            }
            else
            {
                chkbuffer.Checked = false;
            }

            if (restoreprod == "Y")
            {
                chkrestoreproduction.Checked = true;
            }
            else
            {
                chkrestoreproduction.Checked = false;
            }

            if (setup == "Y")
            {
                chkboxsetup.Checked = true;
            }
            else
            {
                chkboxsetup.Checked = false;
            }           

            if (addmo == "Y")
            {
                chkboxnewmo.Checked = true;
            }
            else
            {
                chkboxnewmo.Checked = false;
            }            

            if (addtoproduction == "Y")
            {
                chkboxaddproduction.Checked = true;
            }
            else
            {
                chkboxaddproduction.Checked = false;
            }

            if (stationassign == "Y")
            {
                chkboxstationassign.Checked = true;
            }
            else
            {
                chkboxstationassign.Checked = false;
            }

            if (stationassign == "Y")
            {
                chkboxstationassign.Checked = true;
            }
            else
            {
                chkboxstationassign.Checked = false;
            }

            if (openmo == "Y")
            {
                chkboxopenmo.Checked = true;
            }
            else
            {
                chkboxopenmo.Checked = false;
            }            

            if (color == "Y")
            {
                chkboxcolor.Checked = true;
            }
            else
            {
                chkboxcolor.Checked = false;
            }

            if (size == "Y")
            {
                chkboxsize.Checked = true;
            }
            else
            {
                chkboxsize.Checked = false;
            }

            if (article == "Y")
            {
                chkboxarticle.Checked = true;
            }
            else
            {
                chkboxarticle.Checked = false;
            }

            if (customer == "Y")
            {
                chkboxcustomer.Checked = true;
            }
            else
            {
                chkboxcustomer.Checked = false;
            }

            if (emp == "Y")
            {
                chkboxemployee.Checked = true;
            }
            else
            {
                chkboxemployee.Checked = false;
            }

            if (contractor == "Y")
            {
                chkboxcontractor.Checked = true;
            }
            else
            {
                chkboxcontractor.Checked = false;
            }

            if (operation == "Y")
            {
                chkboxoperation.Checked = true;
            }
            else
            {
                chkboxoperation.Checked = false;
            }

            if (special == "Y")
            {
                chkboxspecialfields.Checked = true;
            }
            else
            {
                chkboxspecialfields.Checked = false;
            }

            if (user1 == "Y")
            {
                chkboxuser1.Checked = true;
            }
            else
            {
                chkboxuser1.Checked = false;
            }

            if (user2 == "Y")
            {
                chkboxuser2.Checked = true;
            }
            else
            {
                chkboxuser2.Checked = false;
            }

            if (user3 == "Y")
            {
                chkboxuser3.Checked = true;
            }
            else
            {
                chkboxuser3.Checked = false;
            }

            if (user4 == "Y")
            {
                chkboxuser4.Checked = true;
            }
            else
            {
                chkboxuser4.Checked = false;
            }

            if (user5 == "Y")
            {
                chkboxuser5.Checked = true;
            }
            else
            {
                chkboxuser5.Checked = false;
            }

            if (user6 == "Y")
            {
                chkboxuser6.Checked = true;
            }
            else
            {
                chkboxuser6.Checked = false;
            }

            if (user7 == "Y")
            {
                chkboxuser7.Checked = true;
            }
            else
            {
                chkboxuser7.Checked = false;
            }

            if (user8 == "Y")
            {
                chkboxuser8.Checked = true;
            }
            else
            {
                chkboxuser8.Checked = false;
            }

            if (user9 == "Y")
            {
                chkboxuser9.Checked = true;
            }
            else
            {
                chkboxuser9.Checked = false;
            }

            if (user10 == "Y")
            {
                chkboxuser10.Checked = true;
            }
            else
            {
                chkboxuser10.Checked = false;
            }

            if (logout == "Y")
            {
                chklogout.Checked = true;
            }
            else
            {
                chklogout.Checked = false;
            }            

            if (stationreport == "Y")
            {
                chkboxstationreport.Checked = true;
            }
            else
            {
                chkboxstationreport.Checked = false;
            }

            if (empreport == "Y")
            {
                chkboxemployeereport.Checked = true;
            }
            else
            {
                chkboxemployeereport.Checked = false;
            }

            if (qcmain == "Y")
            {
                chkqcmain.Checked = true;
            }
            else
            {
                chkqcmain.Checked = false;
            }

            if (qcsub == "Y")
            {
                chkqcsub.Checked = true;
            }
            else
            {
                chkqcsub.Checked = false;
            }

            if (groupcategory == "Y")
            {
                chkemployeegroupcategory.Checked = true;
            }
            else
            {
                chkemployeegroupcategory.Checked = false;
            }

            if (empgroup == "Y")
            {
                chkemployeegroups.Checked = true;
            }
            else
            {
                chkemployeegroups.Checked = false;
            }

            if (empskill_level == "Y")
            {
                chkemployeeskilllevel.Checked = true;
            }
            else
            {
                chkemployeeskilllevel.Checked = false;
            }

            if (machines == "Y")
            {
                chkmachines.Checked = true;
            }
            else
            {
                chkmachines.Checked = false;
            }

            if (machinedetails == "Y")
            {
                chkmachinedetails.Checked = true;
            }
            else
            {
                chkmachinedetails.Checked = false;
            }

            if (mbmain == "Y")
            {
                chkmbmain.Checked = true;
            }
            else
            {
                chkmbmain.Checked = false;
            }

            if (mbsub == "Y")
            {
                chkmbsub.Checked = true;
            }
            else
            {
                chkmbsub.Checked = false;
            }

            if (production_plan == "Y")
            {
                chkproductionplanning.Checked = true;
            }
            else
            {
                chkproductionplanning.Checked = false;
            }

            if (current_prod == "Y")
            {
                chkcurrentproduction.Checked = true;
            }
            else
            {
                chkcurrentproduction.Checked = false;
            }            

            if (empskill == "Y")
            {
                chkemployeeskill.Checked = true;
            }
            else
            {
                chkemployeeskill.Checked = false;
            }

            if (opskill == "Y")
            {
                chkoperationskill.Checked = true;
            }
            else
            {
                chkoperationskill.Checked = false;
            }

            if (moreport == "Y")
            {
                chkmoreport.Checked = true;
            }
            else
            {
                chkmoreport.Checked = false;
            }

            if (stn_prod_report == "Y")
            {
                chkstationproductionreport.Checked = true;
            }
            else
            {
                chkstationproductionreport.Checked = false;
            }

            if (empqcreport == "Y")
            {
                chkemployeeqcreport.Checked = true;
            }
            else
            {
                chkemployeeqcreport.Checked = false;
            }

            if (opqcreport == "Y")
            {
                chkoperationqcreport.Checked = true;
            }
            else
            {
                chkoperationqcreport.Checked = false;
            }

            if (payrollreport == "Y")
            {
                chkpayrollreport.Checked = true;
            }
            else
            {
                chkpayrollreport.Checked = false;
            }

            if (emplogs == "Y")
            {
                chkemployeelogs.Checked = true;
            }
            else
            {
                chkemployeelogs.Checked = false;
            }

            if (moqcreport == "Y")
            {
                chkmoqcreport.Checked = true;
            }
            else
            {
                chkmoqcreport.Checked = false;
            }

            if (stationqcreport == "Y")
            {
                chkstationqcreport.Checked = true;
            }
            else
            {
                chkstationqcreport.Checked = false;
            }

            if (machinereport == "Y")
            {
                chkmachinereport.Checked = true;
            }
            else
            {
                chkmachinereport.Checked = false;
            }

            if (machineassign == "Y")
            {
                chkmachineassign.Checked = true;
            }
            else
            {
                chkmachineassign.Checked = false;
            }

            if (machinerepair == "Y")
            {
                chkmachinerepair.Checked = true;
            }
            else
            {
                chkmachinerepair.Checked = false;
            }

            if (topdefects == "Y")
            {
                chktopdefects.Checked = true;
            }
            else
            {
                chktopdefects.Checked = false;
            }

            if (designsequence == "Y")
            {
                chkdesignsequence.Checked = true;
            }
            else
            {
                chkdesignsequence.Checked = false;
            }

            if (stationwip == "Y")
            {
                chkstationwip.Checked = true;
            }
            else
            {
                chkstationwip.Checked = false;
            }

            if (linebalancing == "Y")
            {
                chklinebalancing.Checked = true;
            }
            else
            {
                chklinebalancing.Checked = false;
            }

            if (performancereport == "Y")
            {
                chkperfomance.Checked = true;
            }
            else
            {
                chkperfomance.Checked = false;
            }

            if (sparereport == "Y")
            {
                chksparereport.Checked = true;
            }
            else
            {
                chksparereport.Checked = false;
            }

            if (spareinventory == "Y")
            {
                chkspareinventory.Checked = true;
            }
            else
            {
                chkspareinventory.Checked = false;
            }

            if (sparemain == "Y")
            {
                chksparemain.Checked = true;
            }
            else
            {
                chksparemain.Checked = false;
            }

            if (sparesub == "Y")
            {
                chksparesub.Checked = true;
            }
            else
            {
                chksparesub.Checked = false;
            }

            if (moopreport == "Y")
            {
                chkmoopreport.Checked = true;
            }
            else
            {
                chkmoopreport.Checked = false;
            }
        }

        //3-des encrypt
        public static string EncryptPassword(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            AppSettingsReader settingsReader = new AppSettingsReader();
            string key = "WETHEPEOPLEOFINDIAHAVING";

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();

            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            tdes.Clear();

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        private void cmbhangercontroller_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            select_controller();   //get the selected controller
            btnsavehanger.Enabled = false;
        }
        public void RefreshGrid_Hanger()
        {
            //get hanger limit details
            MySqlDataAdapter sda = new MySqlDataAdapter("SELECT STN_ID,INFEED_LINENO,STN_NO_INFEED,LIMIT_ENABLED,HANGER_LIMIT from stationdata", dc.conn);
            DataSet ds = new DataSet();
            sda.Fill(ds, "stationdata");
            dgvhanger.DataSource = ds.Tables["stationdata"].DefaultView;
            dgvhanger.Columns["STN_ID"].HeaderText = "Statio ID";
            dgvhanger.Columns["INFEED_LINENO"].HeaderText = "Line No";
            dgvhanger.Columns["STN_NO_INFEED"].HeaderText = "Station No";
            dgvhanger.Columns["LIMIT_ENABLED"].HeaderText = "Limit Enabled";
            dgvhanger.Columns["HANGER_LIMIT"].HeaderText = "Hanger Limit";
            dgvhanger.Columns[0].IsVisible = false;
            dgvhanger.Visible = false;

            if (dgvhanger.Rows.Count > 0)
            {
                dgvhanger.Visible = true;
            }
            sda.Dispose();
        }

        private void btnedithanger_Click(object sender, EventArgs e)
        {
            RowSelected_Hanger();    //get the selected row
        }

        public void RowSelected_Hanger()
        {
            if (dgvhanger.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                stnID = dgvhanger.SelectedRows[0].Cells[0].Value + string.Empty;
                lineNo = dgvhanger.SelectedRows[0].Cells[1].Value + string.Empty;
                txthangerlimit.Text = dgvhanger.SelectedRows[0].Cells[4].Value + string.Empty;
                String enabled = dgvhanger.SelectedRows[0].Cells[3].Value + string.Empty;

                if (enabled == "0")
                {
                    chkhangerlimit.Checked = false;
                }
                else
                {
                    chkhangerlimit.Checked = true;
                }

                btnsavehanger.Enabled = true;
                btnsavehanger.ForeColor = Color.Red;
            }
        }

        private void dgvhanger_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Hanger();    //get the selected row
        }

        private void btnsavehanger_Click(object sender, EventArgs e)
        {
            //check if all the fields are inserted
            if (stnID != "" && txthangerlimit.Text != "")
            {
                String enable = "0";

                if (chkhangerlimit.Checked == true)
                {
                    enable = "1";
                }
                else
                {
                    enable = "0";
                }

                //update
                MySqlCommand cmd = new MySqlCommand("update stationdata set LIMIT_ENABLED='" + enable + "',HANGER_LIMIT='" + txthangerlimit.Text + "' where STN_ID='" + stnID + "'", dc.conn);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Records Updated";
                btnsavehanger.ForeColor = Color.Lime;
                RefreshGrid_Hanger();    //get the master
                Clear_Hanger();    //clear all fields
            }
        }

        //clear all fields
        public void Clear_Hanger()
        {
            txthangerlimit.Text = "";
            chkhangerlimit.Checked = false;
        }

        private void radButton5_Click(object sender, EventArgs e)
        { 
            RowSelected_User();      //get the selected row
        }
        public void RowSelected_User()
        {
            if (dgvuser.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                txtusername.Text = dgvuser.SelectedRows[0].Cells[0].Value + string.Empty;
                txtpassword.Text = DecryptPassword(dgvuser.SelectedRows[0].Cells[1].Value.ToString(), false);
                txtconfirmPass.Text = DecryptPassword(dgvuser.SelectedRows[0].Cells[1].Value.ToString(), false);
                cmbUsergroup.Text = dgvuser.SelectedRows[0].Cells[2].Value + string.Empty;
                btndeleteuser1.Enabled = true;
                btncreateaccount.Text = update;
                btncreateaccount.ForeColor = Color.Red;
                txtusername.Enabled = false;

                RefereshUser();
            }
        }

        private void dgvuser_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_User();     //get the selected row
        }

        private void btndeleteuser1_Click(object sender, EventArgs e)
        {
            //delete the selected rows
            SqlCommand cmd = new SqlCommand("DELETE from USER_LOGIN  where V_USERNAME='" + txtusername.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            radLabel4.Text = "The User has been Deleted";
            txtpassword.Text = "";
            btncreateaccount.Text = save;
            txtusername.Enabled = true;
            txtconfirmPass.Text = "";
            txtusername.Text = "";

            RefreshComboBox();    //get the master
            RefereshUser();   //get the master
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            RowSelected_UserGroup();     //get the selected row
        }

        public void RowSelected_UserGroup()
        {
            if (dgvusergroup.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                txtusergroupname.Text = dgvusergroup.SelectedRows[0].Cells[0].Value + string.Empty;
                txtusergroupdescription.Text = dgvusergroup.SelectedRows[0].Cells[1].Value.ToString();

                btndeleteusergroup.Enabled = true;
                btnsaveusergroup.Text = update;
                txtusergroupname.Enabled = false;
                btnsaveusergroup.ForeColor = Color.Red;

                RefereshUserGroup();    //get the master
            }
        }

        private void dgvusergroup_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_UserGroup();     //get the selected row
        }

        private void Setup_FormClosed(object sender, FormClosedEventArgs e)
        {
            dc.Close_Connection();   //close connection
        }

        public void select_controller()
        {
            try
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
                    cmbstncontroller.Text = controller;
                    cmbhangercontroller.Text = controller;
                    cmbprodcontroller.Text = controller;
                    cmbpushercontroller.Text = controller;
                    cmbroutecontroller.Text = controller;
                    cmbroutingcontroller.Text = controller;
                    cmbkeypad.Text = controller;
                }
                sdr.Close();

                //get cluster ipaddress
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
                    radLabel4.Text = "Controller " + controller + " is not Connected";
                    cmbstncontroller.Text = "--SELECT--";
                    cmbhangercontroller.Text = "--SELECT--";
                    cmbprodcontroller.Text = "--SELECT--";
                    cmbpushercontroller.Text = "--SELECT--";
                    cmbroutecontroller.Text = "--SELECT--";
                    cmbroutingcontroller.Text = "--SELECT--";
                    cmbkeypad.Text = "--SELECT--";
                    btneditstation.Enabled = false;
                    btndeletestation.Enabled = false;
                    btnsavestation.Enabled = false;
                    btndeleterouting.Enabled = false;
                    btneditrouting.Enabled = false;
                    btnsaverouting.Enabled = false;
                    btndeleteroute.Enabled = false;
                    btneditroute.Enabled = false;
                    btnsaveroute.Enabled = false;
                    btndeleteproduction.Enabled = false;
                    btnsavepusher.Enabled = false;
                    btndeletepusher.Enabled = false;
                    btneditpusher.Enabled = false;
                    btnedithanger.Enabled = false;
                    btnsavehanger.Enabled = false;
                    return;
                }

                //check if controller is selected
                if (controller == "--SELECT--" || controller == "")
                {
                    return;
                }

                dc.Close_Connection();   //close connection
                dc.OpenMYSQLConnection(ipaddress);   //open connection

                //get all station types
                MySqlDataAdapter sda1 = new MySqlDataAdapter("Select TYPE from stationtype", dc.conn);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbstationtype.Items.Clear();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    cmbstationtype.Items.Add(dt1.Rows[i][0].ToString());
                }

                btneditstation.Enabled = true;
                btnsavestation.Enabled = true;
                btneditrouting.Enabled = true;
                btnsaverouting.Enabled = true;
                btneditroute.Enabled = true;
                btnsaveroute.Enabled = true;
                btnsavepusher.Enabled = true;
                btneditpusher.Enabled = true;
                btnedithanger.Enabled = true;
                btnsavehanger.Enabled = true;

                RefreshGrid_Route();    //get the master
                RefreshGrid();     //get the master
                RefreshGrid_Hanger();    //get the master
                RefreshGrid_Pusher();    //get the master

                //get all route id
                sda1 = new MySqlDataAdapter("Select ROUTE_ID from routing", dc.conn);
                dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbroutingid.Items.Clear();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    cmbroutingid.Items.Add(dt1.Rows[i][0].ToString());
                }
                sda1.Dispose();

                //get all station id
                sda1 = new MySqlDataAdapter("Select STN_ID from stationdata", dc.conn);
                dt1 = new DataTable();
                sda1.Fill(dt1);
                cmbstationid.Items.Clear();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    cmbstationid.Items.Add(dt1.Rows[i][0].ToString());
                }
                sda1.Dispose();

                RefreshGrid_Routing();   //get the master
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        public void KeypadSetup()
        {
            //check if controller is selected
            if (cmbkeypad.Text == "--SELECT--" || cmbkeypad.Text == "")
            {
                return;
            }

            //get description of the keypad
            MySqlDataAdapter sda1 = new MySqlDataAdapter("Select DESCRIPTION from displaydata where SPECIAL='0'", dc.conn);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                cmbrow1position1.Items.Add(dt1.Rows[i][0].ToString());
                cmbrow1position2.Items.Add(dt1.Rows[i][0].ToString());
                cmbrow1position3.Items.Add(dt1.Rows[i][0].ToString());
                cmbrow2position1.Items.Add(dt1.Rows[i][0].ToString());
                cmbrow2position3.Items.Add(dt1.Rows[i][0].ToString());
                cmbrow3position1.Items.Add(dt1.Rows[i][0].ToString());
                cmbrow3position3.Items.Add(dt1.Rows[i][0].ToString());
                cmbrow4position1.Items.Add(dt1.Rows[i][0].ToString());
                cmbrow4position2.Items.Add(dt1.Rows[i][0].ToString());
                cmbrow4position3.Items.Add(dt1.Rows[i][0].ToString());
            }
            sda1.Dispose();

            //get the desc
            sda1 = new MySqlDataAdapter("Select DESCRIPTION from displaydata where SPECIAL='1'", dc.conn);
            dt1 = new DataTable();
            sda1.Fill(dt1);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                cmbrow2position2.Items.Add(dt1.Rows[i][0].ToString());
                cmbrow3position2.Items.Add(dt1.Rows[i][0].ToString());
            }
            sda1.Dispose();

            //get keypad 1st row info
            sda1 = new MySqlDataAdapter("Select POS_1,POS_2,POS_3 from displayconfig where ROW_ID='1'", dc.conn);
            dt1 = new DataTable();
            sda1.Fill(dt1);
            sda1.Dispose();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                String pos1 = dt1.Rows[i][0].ToString();
                String pos2 = dt1.Rows[i][1].ToString();
                String pos3 = dt1.Rows[i][2].ToString();

                //get desc
                MySqlCommand cmd1 = new MySqlCommand("select DESCRIPTION from displaydata where ID='" + pos1 + "'", dc.conn);
                MySqlDataReader sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    cmbrow1position1.Text = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();

                //get desc
                cmd1 = new MySqlCommand("select DESCRIPTION from displaydata where ID='" + pos2 + "'", dc.conn);
                sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    cmbrow1position2.Text = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();

                //get desc
                cmd1 = new MySqlCommand("select DESCRIPTION from displaydata where ID='" + pos3 + "'", dc.conn);
                sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    cmbrow1position3.Text = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();
            }

            //get keypad 2nd row info
            sda1 = new MySqlDataAdapter("Select POS_1,POS_2,POS_3 from displayconfig where ROW_ID='2'", dc.conn);
            dt1 = new DataTable();
            sda1.Fill(dt1);
            sda1.Dispose();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                String pos1 = dt1.Rows[i][0].ToString();
                String pos2 = dt1.Rows[i][1].ToString();
                String pos3 = dt1.Rows[i][2].ToString();

                //get desc
                MySqlCommand cmd1 = new MySqlCommand("select DESCRIPTION from displaydata where ID='" + pos1 + "'", dc.conn);
                MySqlDataReader sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    cmbrow2position1.Text = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();

                //get desc
                cmd1 = new MySqlCommand("select DESCRIPTION from displaydata where ID='" + pos2 + "'", dc.conn);
                sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    cmbrow2position2.Text = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();

                //get desc
                cmd1 = new MySqlCommand("select DESCRIPTION from displaydata where ID='" + pos3 + "'", dc.conn);
                sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    cmbrow2position3.Text = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();
            }

            //get keypad 3rd row info
            sda1 = new MySqlDataAdapter("Select POS_1,POS_2,POS_3 from displayconfig where ROW_ID='3'", dc.conn);
            dt1 = new DataTable();
            sda1.Fill(dt1);
            sda1.Dispose();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                String pos1 = dt1.Rows[i][0].ToString();
                String pos2 = dt1.Rows[i][1].ToString();
                String pos3 = dt1.Rows[i][2].ToString();

                //get desc
                MySqlCommand cmd1 = new MySqlCommand("select DESCRIPTION from displaydata where ID='" + pos1 + "'", dc.conn);
                MySqlDataReader sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    cmbrow3position1.Text = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();

                //get desc
                cmd1 = new MySqlCommand("select DESCRIPTION from displaydata where ID='" + pos2 + "'", dc.conn);
                sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    cmbrow3position2.Text = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();

                //get desc
                cmd1 = new MySqlCommand("select DESCRIPTION from displaydata where ID='" + pos3 + "'", dc.conn);
                sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    cmbrow3position3.Text = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();
            }

            //get keypad 4th row info
            sda1 = new MySqlDataAdapter("Select POS_1,POS_2,POS_3 from displayconfig where ROW_ID='4'", dc.conn);
            dt1 = new DataTable();
            sda1.Fill(dt1);
            sda1.Dispose();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                String pos1 = dt1.Rows[i][0].ToString();
                String pos2 = dt1.Rows[i][1].ToString();
                String pos3 = dt1.Rows[i][2].ToString();

                //get decs
                MySqlCommand cmd1 = new MySqlCommand("select DESCRIPTION from displaydata where ID='" + pos1 + "'", dc.conn);
                MySqlDataReader sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    cmbrow4position1.Text = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();

                //get desc
                cmd1 = new MySqlCommand("select DESCRIPTION from displaydata where ID='" + pos2 + "'", dc.conn);
                sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    cmbrow4position2.Text = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();

                //get desc
                cmd1 = new MySqlCommand("select DESCRIPTION from displaydata where ID='" + pos3 + "'", dc.conn);
                sdr1 = cmd1.ExecuteReader();
                if (sdr1.Read())
                {
                    cmbrow4position3.Text = sdr1.GetValue(0).ToString();
                }
                sdr1.Close();
            }

            String row1 = "0";
            String row2 = "0";
            String row3 = "0";
            String row4 = "0";

            //check if row is enabled
            MySqlCommand cmd2 = new MySqlCommand("select ROW_1,ROW_2,ROW_3,ROW_4 from displayrow", dc.conn);
            MySqlDataReader sdr2 = cmd2.ExecuteReader();
            if (sdr2.Read())
            {
                row1 = sdr2.GetValue(0).ToString();
                row2 = sdr2.GetValue(1).ToString();
                row3 = sdr2.GetValue(2).ToString();
                row4 = sdr2.GetValue(3).ToString();
            }
            sdr2.Close();

            if (row1 == "1")
            {
                chkrow1.Checked = true;
            }
            else
            {
                chkrow1.Checked = false;
            }

            if (row2 == "1")
            {
                chkrow2.Checked = true;
            }
            else
            {
                chkrow2.Checked = false;
            }

            if (row3 == "1")
            {
                chkrow3.Checked = true;
            }
            else
            {
                chkrow3.Checked = false;
            }

            if (row4 == "1")
            {
                chkrow4.Checked = true;
            }
            else
            {
                chkrow4.Checked = false;
            }
        }

        private void Setup_Shown(object sender, EventArgs e)
        {
            select_controller();   //get the selected controller
        }

        private void radPageView1_SelectedPageChanged_2(object sender, EventArgs e)
        {
            dc.OpenConnection();   //oprn connection
            cmbshift.Items.Clear();

            //check if unsaved tabs
            if (btnsaveshift.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Shitfs. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveshift.PerformClick();
                }
            }
            else if (btnsavebreaks.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Shift Breaks. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavebreaks.PerformClick();
                }
            }
            else if (btnsavehideday.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Holidays. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavehideday.PerformClick();
                }
            }
            else if (btnsaveweekoff.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved Week Offs. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveweekoff.PerformClick();
                }
            }

            SqlDataAdapter sda = new SqlDataAdapter("select V_SHIFT from SHIFTS", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbshift.Items.Add(dt.Rows[i][0].ToString());
            }
            sda.Dispose();

            RefereshBreaks();   //get the master
        }

        private void btneditshift_Click(object sender, EventArgs e)
        {
            RowSelected_Shifts();     //get the selected row
        }

        public void RowSelected_Shifts()
        {
            if (dgvshifts.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                txtshifts.Text = dgvshifts.SelectedRows[0].Cells[0].Value + string.Empty;
                tpshiftstart.Value = Convert.ToDateTime(dgvshifts.SelectedRows[0].Cells[1].Value + string.Empty);
                tpshiftend.Value = Convert.ToDateTime(dgvshifts.SelectedRows[0].Cells[2].Value + string.Empty);
                tpovertimeend.Value = Convert.ToDateTime(dgvshifts.SelectedRows[0].Cells[3].Value + string.Empty);

                txtshifts.Enabled = false;
                btnsaveshift.Text = update;
                btndeleteshift.Enabled = true;
                btnsaveshift.ForeColor = Color.Red;

                shift_start = tpshiftstart.Value.ToString();
                shift_end = tpshiftend.Value.ToString();
                overtime = tpovertimeend.Value.ToString();
            }
        }

        private void dgvshifts_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Shifts();    //get the selected row
        }

        private void btndeleteshift_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from SHIFTS where V_SHIFT='" + txtshifts.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                //delete the selected row
                cmd = new SqlCommand("DELETE FROM SHIFT_BREAKS WHERE V_SHIFT NOT IN(SELECT D.V_SHIFT FROM SHIFTS D)", dc.con);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Record Deleted";
                RefereshShifts();  //get the master

                txtshifts.Enabled = true;
                btnsaveshift.Text = save;
                btndeleteshift.Enabled = false;
                txtshifts.Text = "";

                tpshiftstart.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                tpshiftend.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                tpovertimeend.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                btnsavecontroller.ForeColor = Color.Lime;
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btnsaveshift_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtshifts.Text != "")
                {
                    btndeleteshift.Enabled = false;
                    if (btnsaveshift.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from SHIFTS where V_SHIFT='" + txtshifts.Text + "'", dc.con);
                        Int32 j = int.Parse(cmd1.ExecuteScalar().ToString());

                        //check if id adlready exists
                        if (j == 0)
                        {
                            //get shift details
                            SqlDataAdapter sda = new SqlDataAdapter("select T_SHIFT_START_TIME,T_OVERTIME_END_TIME from SHIFTS", dc.con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            sda.Dispose();
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                DateTime date1 = Convert.ToDateTime(dt.Rows[i][0].ToString());
                                DateTime date2 = Convert.ToDateTime(dt.Rows[i][1].ToString());
                                if (date1 > date2)
                                {
                                    date2 = date2.AddDays(1);
                                }

                                //check if shifts overlap
                                cmd1 = new SqlCommand("SELECT count(*) FROM SHIFTS T WHERE CONVERT(DATETIME, '" + tpshiftstart.Value + "', 0) BETWEEN '" + date1.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + date2.ToString("yyyy-MM-dd HH:mm:ss") + "'", dc.con);
                                int k = int.Parse(cmd1.ExecuteScalar().ToString());
                                if (k > 0)
                                {
                                    radLabel4.Text = "Shift Already Exists or Shift Timings Overlapa with Other Shift";
                                    return;
                                }

                                //check if shifts overlap
                                cmd1 = new SqlCommand("SELECT count(*) FROM SHIFTS T WHERE CONVERT(DATETIME, '" + tpovertimeend.Value + "', 0) BETWEEN '" + date1.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + date2.ToString("yyyy-MM-dd HH:mm:ss") + "'", dc.con);
                                k = int.Parse(cmd1.ExecuteScalar().ToString());
                                if (k > 0)
                                {
                                    radLabel4.Text = "Shift Already Exists or Shift Timings Overlapa with Other Shift";
                                    return;
                                }
                            }
                            
                            //insert
                            SqlCommand cmd = new SqlCommand("insert into SHIFTS values('" + txtshifts.Text + "','" + tpshiftstart.Value + "','" + tpshiftend.Value + "','" + tpovertimeend.Value + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            radLabel4.Text = "Records Saved";
                            RefereshShifts();   //get the master

                            txtshifts.Enabled = true;
                            txtshifts.Text = "";

                            tpshiftstart.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                            tpshiftend.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                            tpovertimeend.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                        }
                    }
                    if (btnsaveshift.Text == update)
                    {
                        //get the shift details
                        SqlDataAdapter sda = new SqlDataAdapter("select T_SHIFT_START_TIME,T_OVERTIME_END_TIME from SHIFTS where V_SHIFT!='" + txtshifts.Text + "'", dc.con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        sda.Dispose();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            DateTime date1 = Convert.ToDateTime(dt.Rows[i][0].ToString());
                            DateTime date2 = Convert.ToDateTime(dt.Rows[i][1].ToString());
                            if (date1 > date2)
                            {
                                date2 = date2.AddDays(1);
                            }

                            //check if shifts overlap
                            SqlCommand cmd1 = new SqlCommand("SELECT count(*) FROM SHIFTS T WHERE CONVERT(DATETIME, '" + tpshiftstart.Value + "', 0) BETWEEN '" + date1.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + date2.ToString("yyyy-MM-dd HH:mm:ss") + "' and V_SHIFT!='" + txtshifts.Text + "'", dc.con);
                            int k = int.Parse(cmd1.ExecuteScalar().ToString());
                            if (k > 0)
                            {
                                radLabel4.Text = "Shift Already Exists or Shift Timings Overlapa with Other Shift";
                                return;
                            }

                            //check if shifts overlap
                            cmd1 = new SqlCommand("SELECT count(*) FROM SHIFTS T WHERE CONVERT(DATETIME, '" + tpovertimeend.Value + "', 0) BETWEEN '" + date1.ToString("yyyy-MM-dd HH:mm:ss") + "' AND '" + date2.ToString("yyyy-MM-dd HH:mm:ss") + "' and V_SHIFT!='" + txtshifts.Text + "'", dc.con);
                            k = int.Parse(cmd1.ExecuteScalar().ToString());
                            if (k > 0)
                            {
                                radLabel4.Text = "Shift Already Exists or Shift Timings Overlapa with Other Shift";
                                return;
                            }
                        }

                        //update
                        SqlCommand cmd = new SqlCommand("Update SHIFTS set T_SHIFT_START_TIME='" + tpshiftstart.Value + "',T_SHIFT_END_TIME='" + tpshiftend.Value + "',T_OVERTIME_END_TIME='" + tpovertimeend.Value + "' where V_SHIFT='" + txtshifts.Text + "'", dc.con);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = "Records Updated";
                        RefereshShifts();   //get the master

                        txtshifts.Enabled = true;
                        btnsaveshift.Text = save;
                        txtshifts.Text = "";

                        tpshiftstart.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                        tpshiftend.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                        tpovertimeend.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                    }
                    btnsaveshift.ForeColor = Color.Lime;
                }
                else
                {
                    radLabel4.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void cmbshift_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            HideSelected();     //get the selected row
        }

        private void btneditbreaks_Click(object sender, EventArgs e)
        {
            RowSelected_Breaks();    //get the selected row
        }

        public void RowSelected_Breaks()
        {
            if (dgvbreaks.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                cmbshift.Text = dgvbreaks.SelectedRows[0].Cells[0].Value + string.Empty;
                tpbreakstart.Value = Convert.ToDateTime(dgvbreaks.SelectedRows[0].Cells[2].Value + string.Empty);
                tpbreakend.Value = Convert.ToDateTime(dgvbreaks.SelectedRows[0].Cells[3].Value + string.Empty);

                breaks = dgvbreaks.SelectedRows[0].Cells[1].Value + string.Empty;
                Shifts = cmbshift.Text;
                btnsavebreaks.Text = update;
                btndeletebreaks.Enabled = true;
                btnsavebreaks.ForeColor = Color.Red;

                HideSelected();
            }
        }

        private void dgvbreaks_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Breaks();    //get the selected row
        }

        private void btndeletebreaks_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("DELETE FROM SHIFT_BREAKS WHERE V_BREAKS='" + breaks + "'", dc.con);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Record Deleted";

                //get shift breaks
                SqlDataAdapter sda = new SqlDataAdapter("select V_BREAKS from SHIFT_BREAKS where V_SHIFT='" + Shifts + "' order by V_BREAKS", dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                int j = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //update
                    cmd = new SqlCommand("update SHIFT_BREAKS set V_BREAKS='" + j + "' where V_BREAKS='" + dt.Rows[i][0].ToString() + "' and V_SHIFT='" + Shifts + "'", dc.con);
                    cmd.ExecuteNonQuery();
                    j = j + 1;
                }

                RefereshBreaks();      //get the master 

                btnsavebreaks.Text = save;
                btndeletebreaks.Enabled = false;
                cmbshift.Text = "";
                tpbreakstart.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                tpbreakend.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                btnsavebreaks.ForeColor = Color.Lime;
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btnsavebreaks_Click(object sender, EventArgs e)
        {
            DateTime starttime1 = (DateTime)tpbreakstart.Value;
            DateTime endtime1 = (DateTime)tpbreakend.Value;
            String starttime = starttime1.ToString("HH:mm") + ":00";
            String endtime = endtime1.ToString("HH:mm") + ":00";

            //check if end time less than start time
            if (starttime1 > endtime1)
            {
                radLabel4.Text = "Shift End Time should be greater than Shift Start Time";
                return;
            }
            try
            {
                //check if all the fields are inserted
                if (cmbshift.Text != "" || cmbshift.Text == "--SELECT--")
                {
                    btndeletebreaks.Enabled = false;
                    if (btnsavebreaks.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from SHIFT_BREAKS where T_BREAK_TIME_START='" + starttime + "' and T_BREAK_TIME_END='" + endtime + "' and V_SHIFT='" + cmbshift.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //check if id adlready exists
                        if (i == 0)
                        {
                            //get the break count
                            cmd1 = new SqlCommand("select count(*) from SHIFT_BREAKS where V_SHIFT='" + cmbshift.Text + "'", dc.con);
                            Int32 count = int.Parse(cmd1.ExecuteScalar().ToString());

                            //calculate timespan for break
                            count = count + 1;
                            TimeSpan tp_breaks = (TimeSpan)(tpbreakend.Value - tpbreakstart.Value);
                            int breaktime = (int)tp_breaks.TotalMinutes;

                            //insert
                            SqlCommand cmd = new SqlCommand("insert into SHIFT_BREAKS values('" + cmbshift.Text + "','" + count + "','" + tpbreakstart.Value + "','" + tpbreakend.Value + "','" + breaktime + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            radLabel4.Text = "Records Saved";
                            RefereshBreaks();   //get the master

                            cmbshift.Text = "";
                            tpbreakend.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                            tpbreakstart.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvbreaks.Rows.Count; j++)
                            {
                                if (dgvbreaks.Rows[j].Cells[2].Value.ToString().Equals(starttime.ToString()) && dgvbreaks.Rows[j].Cells[3].Value.ToString().Equals(endtime.ToString()))
                                {
                                    dgvbreaks.Rows[j].IsSelected = true;
                                    radLabel4.Text = "Shift Break Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsavebreaks.Text == update)
                    {
                        //calculate timespan for the break
                        TimeSpan tp_breaks = (TimeSpan)(tpbreakend.Value - tpbreakstart.Value);
                        int breaktime = (int)tp_breaks.TotalMinutes;

                        //update
                        SqlCommand cmd = new SqlCommand("Update SHIFT_BREAKS set T_BREAK_TIME_START='" + tpbreakstart.Value + "',T_BREAK_TIME_END='" + tpbreakend.Value + "',V_SHIFT='" + cmbshift.Text + "',I_BREAK_TIMESPAN='" + breaktime + "' where V_BREAKS='" + breaks + "'", dc.con);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = "Records Updated";
                        RefereshBreaks();   //get the master

                        btnsavebreaks.Text = save;
                        cmbshift.Text = "";

                        tpbreakend.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                        tpbreakstart.Value = Convert.ToDateTime(DateTime.Now.ToString("HH:mm"));
                    }
                    btnsavebreaks.ForeColor = Color.Lime;
                }
                else
                {
                    radLabel4.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        public void HideSelected()
        {
            try
            {
                //unhide all rows
                for (int i = 0; i < dgvbreaks.Rows.Count; i++)
                {
                    dgvbreaks.Rows[i].IsVisible = true;
                }

                //hide all the rows other than the selected shift
                for (int i = 0; i < dgvbreaks.Rows.Count; i++)
                {
                    if (dgvbreaks.Rows[i].Cells[0].Value.ToString() != cmbshift.Text && cmbshift.Text != "")
                    {
                        dgvbreaks.Rows[i].IsVisible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }


        private void btneditholiday_Click(object sender, EventArgs e)
        {
            RowSelected_Holiday();    //get the selected row
        }

        public void RowSelected_Holiday()
        {
            if (dgvholiday.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                holiday_id = dgvholiday.SelectedRows[0].Cells[0].Value + string.Empty;
                txtholiday.Text = dgvholiday.SelectedRows[0].Cells[1].Value + string.Empty;
                txtholidaydesc.Text = dgvholiday.SelectedRows[0].Cells[2].Value + string.Empty;

                clnholiday.SelectedDate = Convert.ToDateTime(txtholiday.Text);
                btnsaveholiday.Text = update;
                btndeleteholiday.Enabled = true;
                btnsaveholiday.ForeColor = Color.Red;
            }
        }

        private void dgvholiday_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Holiday();    //get the selected row
        }

        private void btndeleteholiday_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from HOLIDAY_DB where D_HOLIDAY='" + txtholiday.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Record Deleted";
                RefereshHoliday();   //get the master

                btnsaveholiday.Text = save;
                btndeleteholiday.Enabled = false;
                txtholiday.Text = "";
                txtholidaydesc.Text = "";
                btnsaveholiday.ForeColor = Color.Lime;
                holiday_id = "";
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btnsaveholiday_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtholiday.Text != "" && txtholidaydesc.Text != "")
                {
                    btndeleteholiday.Enabled = false;
                    if (btnsaveholiday.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from HOLIDAY_DB where D_HOLIDAY='" + txtholiday.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //check if id adlready exists
                        if (i == 0)
                        {
                            //insert
                            SqlCommand cmd = new SqlCommand("insert into HOLIDAY_DB values('" + txtholiday.Text + "','" + txtholidaydesc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            radLabel4.Text = "Records Saved";
                            RefereshHoliday();   //get the master

                            txtholiday.Text = "";
                            txtholidaydesc.Text = "";
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvholiday.Rows.Count; j++)
                            {
                                if (dgvholiday.Rows[j].Cells[0].Value.ToString().Equals(txtholiday.Text))
                                {
                                    dgvholiday.Rows[j].IsSelected = true;
                                    radLabel4.Text = "Holiday Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsaveholiday.Text == update)
                    {
                        //update
                        SqlCommand cmd = new SqlCommand("Update HOLIDAY_DB set D_HOLIDAY='" + txtholiday.Text + "',V_HOLIDAY_DESC='" + txtholidaydesc.Text + "' where V_ID='" + holiday_id + "'", dc.con);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = "Records Updated";
                        RefereshHoliday();   //get the master

                        btnsaveholiday.Text = save;
                        txtholiday.Text = "";
                        txtholidaydesc.Text = "";
                    }
                    btnsaveholiday.ForeColor = Color.Lime;
                    holiday_id = "";
                }
                else
                {
                    radLabel4.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btnsaveweekoff_Click(object sender, EventArgs e)
        {
            try
            {
                //get weekoff details
                String weekoff = "";
                if (chkmonday.Checked == true)
                {
                    weekoff = weekoff + "Monday,";
                }

                if (chktuesday.Checked == true)
                {
                    weekoff = weekoff + "Tuesday,";
                }

                if (chkwednesday.Checked == true)
                {
                    weekoff = weekoff + "Wednesday,";
                }

                if (chkthursday.Checked == true)
                {
                    weekoff = weekoff + "Thursday,";
                }

                if (chkfriday.Checked == true)
                {
                    weekoff = weekoff + "Friday,";
                }

                if (chksaturday.Checked == true)
                {
                    weekoff = weekoff + "Saturday,";
                }

                if (chksunday.Checked == true)
                {
                    weekoff = weekoff + "Sunday,";
                }

                if (weekoff.Length > 0)
                {
                    weekoff = weekoff.Remove(weekoff.Length - 1, 1);
                }

                //update
                SqlCommand cmd = new SqlCommand("update Setup set WEEK_OFF='" + weekoff + "'", dc.con);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Records Updated";
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void clnholiday_SelectionChanged(object sender, EventArgs e)
        {
            txtholiday.Text = clnholiday.SelectedDate.ToString("yyyy-MM-dd");   //get selected date
        }

        private void radCheckBox1_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            //check ig skill rate is selected
            if (chkskillrate.Checked == true)
            {
                chkskillrate.Text = "Enabled";
            }
            else
            {
                chkskillrate.Text = "Disabled";
            }

            btnupdategeneral.ForeColor = Color.Red;
        }

        private void btnupdategeneral_Click(object sender, EventArgs e)
        {
            //check if skill is selected
            String skill = "";
            if (chkskillrate.Checked == true)
            {
                skill = "TRUE";
            }
            else
            {
                skill = "FALSE";
            }

            //check if backup is enabled
            String backup = "";
            if (chktimerenable.Checked == true)
            {
                backup = "TRUE";
            }
            else
            {
                backup = "FALSE";
            }

            String hidetotals = "FALSE";
            if (chkhidetotals.Checked == true)
            {
                hidetotals = "TRUE";
            }

            String getallop = "FALSE";
            if (chkgetallop.Checked == true)
            {
                getallop = "TRUE";
            }

            String multilogin = "FALSE";
            if (chkmultilogin.Checked == true)
            {
                multilogin = "TRUE";
            }

            //String followemp = "FALSE";
            //if (chkfollowemployee.Checked == true)
            //{
            //    followemp = "TRUE";
            //}

            //convert image to byte[]
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);

            byte[] photo_aray = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(photo_aray, 0, photo_aray.Length);

            //update
            SqlCommand cmd = new SqlCommand("update Setup set SKILL_EFFICIENCY=@skill,BACKUP_PATH=@path,BACKUP_ENABLE=@enable,BACKUP_TIME=@time,ThemeName=@theme,COMPANY_LOGO=@logo,HIDE_TOTALS=@hidetotals,GET_ALL_OPERATIONS=@op,MULTI_LOGIN=@multi", dc.con);
            cmd.Parameters.AddWithValue("@skill", skill);
            cmd.Parameters.AddWithValue("@path", txtbackuppath.Text);
            cmd.Parameters.AddWithValue("@enable", backup);
            cmd.Parameters.AddWithValue("@time", cmbbackuptimer.Text);
            cmd.Parameters.AddWithValue("@theme", cmbtheme.Text);
            cmd.Parameters.AddWithValue("@logo", photo_aray);
            cmd.Parameters.AddWithValue("@hidetotals", hidetotals);
            cmd.Parameters.AddWithValue("@op", getallop);
            cmd.Parameters.AddWithValue("@multi", multilogin);
            //cmd.Parameters.AddWithValue("@follow", followemp);
            cmd.ExecuteNonQuery();


            //Update setup table in mrt_local db for Controller
            //update
            try
            {
                MySqlCommand cmd2 = new MySqlCommand("UPDATE setup SET multi_login = " + multilogin + " WHERE ID = 1;", dc.conn);
                cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           




            radLabel4.Text = "Records Updated";
            btnupdategeneral.ForeColor = Color.Lime;
            theme = cmbtheme.Text;

            GridTheme();   //change grid theme
        }

        private void vpagesetup_KeyDown(object sender, KeyEventArgs e)
        {
            //special keys for hide day
            if (e.KeyData.ToString() == "D3, Shift")
            {
                pagehideday.Item.Visibility = ElementVisibility.Visible;
                return;
            }

            if (e.KeyData.ToString() == "Escape")
            {
                radTextBox2.Text = "";
                pagehideday.Item.Visibility = ElementVisibility.Collapsed;
                vpagesetup.SelectedPage = pagespecial;
                return;
            }

            radTextBox2.Text += e.KeyData.ToString();
        }

        private void radTextBox2_TextChanged(object sender, EventArgs e)
        {
            ////special keys to open hide day
            //if (radTextBox2.Text == "ShiftKey, ShiftD1D2D3D4D5D6D7D8")
            //{
            //    pagehideday.Item.Visibility = ElementVisibility.Visible;
            //    //DebugLog("Setup.cs(radTextBox2_TextChanged), Text = " + radTextBox2.Text);
            //}
        }

        private void btnedithideday_Click(object sender, EventArgs e)
        {
            RowSelected_Hideday();    //get the selected row
        }

        public void RowSelected_Hideday()
        {
            if (dgvhideday.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                hideday_id = dgvhideday.SelectedRows[0].Cells[0].Value + string.Empty;
                txthidedate.Text = dgvhideday.SelectedRows[0].Cells[1].Value + string.Empty;
                txthidedesc.Text = dgvhideday.SelectedRows[0].Cells[2].Value + string.Empty;

                clnhideday.SelectedDate = Convert.ToDateTime(txthidedate.Text);
                String hideday = dgvhideday.SelectedRows[0].Cells[3].Value + string.Empty;

                if (hideday == "TRUE")
                {
                    chkhideday.Checked = true;
                }
                else
                {
                    chkhideday.Checked = false;
                }

                btnsavehideday.Text = update;
                btndeletehideday.Enabled = true;
                btnsavehideday.ForeColor = Color.Red;
            }
        }

        private void dgvhideday_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Hideday();    //get the selected row
        }

        private void btndeletehideday_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from HIDEDAY_DB where D_HIDEDAY='" + txthidedate.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Record Deleted";
                RefereshHideday();    //get the master

                btnsavehideday.Text = save;
                btndeletehideday.Enabled = false;
                txthidedate.Text = "";
                txthidedesc.Text = "";
                btnsavehideday.ForeColor = Color.Lime;
                hideday_id = "";
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btnsavehideday_Click(object sender, EventArgs e)
        {
            try
            {
                String hideday = "";
                if (chkhideday.Checked == true)
                {
                    hideday = "TRUE";
                }
                else
                {
                    hideday = "FALSE";
                }

                if (txthidedate.Text != "" && txthidedesc.Text != "")
                {
                    btndeletehideday.Enabled = false;
                    if (btnsavehideday.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from HIDEDAY_DB where D_HIDEDAY='" + txthidedate.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //check if id adlready exists
                        if (i == 0)
                        {
                            //insert
                            SqlCommand cmd = new SqlCommand("insert into HIDEDAY_DB values('" + txthidedate.Text + "','" + txthidedesc.Text + "','" + hideday + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            radLabel4.Text = "Records Saved";
                            RefereshHideday();   //get the master

                            txthidedate.Text = "";
                            txthidedesc.Text = "";
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvhideday.Rows.Count; j++)
                            {
                                if (dgvhideday.Rows[j].Cells[0].Value.ToString().Equals(txthidedate.Text))
                                {
                                    dgvhideday.Rows[j].IsSelected = true;
                                    radLabel4.Text = "Hideday Already Exists";
                                    return;
                                }
                            }
                        }
                    }

                    if (btnsavehideday.Text == update)
                    {
                        //update
                        SqlCommand cmd = new SqlCommand("Update HIDEDAY_DB set D_HIDEDAY='" + txthidedate.Text + "',V_HIDEDAY_DESC='" + txthidedesc.Text + "',V_HIDE_ENABLE='" + hideday + "' where V_ID='" + hideday_id + "'", dc.con);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = "Records Updated";
                        RefereshHideday();   //get the master

                        btnsavehideday.Text = save;
                        txthidedate.Text = "";
                        txthidedesc.Text = "";
                    }

                    btnsavehideday.ForeColor = Color.Lime;
                    hideday_id = "";
                }
                else
                {
                    radLabel4.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void clnhideday_SelectionChanged(object sender, EventArgs e)
        {
            txthidedate.Text = clnhideday.SelectedDate.ToString("yyyy-MM-dd");   //get the selected date
        }

        private void chkhideday_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkhideday.Checked == true)
            {
                chkhideday.Text = "Enabled";
            }
            else
            {
                chkhideday.Text = "Disabled";
            }
        }

        private void radCheckBox1_ToggleStateChanged_1(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkhideall.Checked == true)
            {
                //update
                SqlCommand cmd = new SqlCommand("Update HIDEDAY_DB set V_HIDE_ENABLE='TRUE'", dc.con);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "All Hide Dates Enabled";
                RefereshHideday();   //get the master

                chkhideall.Text = "Enabled";
            }
            else
            {
                //update
                SqlCommand cmd = new SqlCommand("Update HIDEDAY_DB set V_HIDE_ENABLE='FALSE'", dc.con);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "All Hide Dates Disabled";
                RefereshHideday();   //get the master

                chkhideall.Text = "Disabled";
            }
        }

        private void btnkeypadupdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbkeypad.Text == "--SELECT--" || cmbkeypad.Text == "")
                {
                    radLabel4.Text = "Select the Controller";
                    return;
                }

                String row1pos1 = "0";
                String row1pos2 = "0";
                String row1pos3 = "0";
                String row1enable = "0";

                String row2pos1 = "0";
                String row2pos2 = "0";
                String row2pos3 = "0";
                String row2enable = "0";

                String row3pos1 = "0";
                String row3pos2 = "0";
                String row3pos3 = "0";
                String row3enable = "0";

                String row4pos1 = "0";
                String row4pos2 = "0";
                String row4pos3 = "0";
                String row4enable = "0";

                //get desc
                MySqlDataAdapter sda = new MySqlDataAdapter("select ID,DESCRIPTION from displaydata", dc.conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (cmbrow1position1.Text == dt.Rows[i][1].ToString())
                    {
                        row1pos1 = dt.Rows[i][0].ToString();
                    }

                    if (cmbrow1position2.Text == dt.Rows[i][1].ToString())
                    {
                        row1pos2 = dt.Rows[i][0].ToString();
                    }

                    if (cmbrow1position3.Text == dt.Rows[i][1].ToString())
                    {
                        row1pos3 = dt.Rows[i][0].ToString();
                    }

                    if (cmbrow2position1.Text == dt.Rows[i][1].ToString())
                    {
                        row2pos1 = dt.Rows[i][0].ToString();
                    }

                    if (cmbrow2position2.Text == dt.Rows[i][1].ToString())
                    {
                        row2pos2 = dt.Rows[i][0].ToString();
                    }

                    if (cmbrow2position3.Text == dt.Rows[i][1].ToString())
                    {
                        row2pos3 = dt.Rows[i][0].ToString();
                    }

                    if (cmbrow3position1.Text == dt.Rows[i][1].ToString())
                    {
                        row3pos1 = dt.Rows[i][0].ToString();
                    }

                    if (cmbrow3position2.Text == dt.Rows[i][1].ToString())
                    {
                        row3pos2 = dt.Rows[i][0].ToString();
                    }

                    if (cmbrow3position3.Text == dt.Rows[i][1].ToString())
                    {
                        row3pos3 = dt.Rows[i][0].ToString();
                    }

                    if (cmbrow4position1.Text == dt.Rows[i][1].ToString())
                    {
                        row4pos1 = dt.Rows[i][0].ToString();
                    }

                    if (cmbrow4position2.Text == dt.Rows[i][1].ToString())
                    {
                        row4pos2 = dt.Rows[i][0].ToString();
                    }

                    if (cmbrow4position3.Text == dt.Rows[i][1].ToString())
                    {
                        row4pos3 = dt.Rows[i][0].ToString();
                    }
                }

                if (chkrow1.Checked == true)
                {
                    row1enable = "1";
                }
                if (chkrow2.Checked == true)
                {
                    row2enable = "1";
                }
                if (chkrow3.Checked == true)
                {
                    row3enable = "1";
                }
                if (chkrow4.Checked == true)
                {
                    row4enable = "1";
                }

                //update
                MySqlCommand cmd = new MySqlCommand("update displayconfig set POS_1='" + row1pos1 + "',POS_2='" + row1pos2 + "',POS_3='" + row1pos3 + "' where ROW_ID='1'", dc.conn);
                cmd.ExecuteNonQuery();

                //update
                cmd = new MySqlCommand("update displayconfig set POS_1='" + row2pos1 + "',POS_2='" + row2pos2 + "',POS_3='" + row2pos3 + "' where ROW_ID='2'", dc.conn);
                cmd.ExecuteNonQuery();

                //update
                cmd = new MySqlCommand("update displayconfig set POS_1='" + row3pos1 + "',POS_2='" + row3pos2 + "',POS_3='" + row3pos3 + "' where ROW_ID='3'", dc.conn);
                cmd.ExecuteNonQuery();

                //update
                cmd = new MySqlCommand("update displayconfig set POS_1='" + row4pos1 + "',POS_2='" + row4pos2 + "',POS_3='" + row4pos3 + "' where ROW_ID='4'", dc.conn);
                cmd.ExecuteNonQuery();

                //update
                cmd = new MySqlCommand("update displayrow set ROW_1='" + row1enable + "',ROW_2='" + row2enable + "',ROW_3='" + row3enable + "',ROW_4='" + row4enable + "'", dc.conn);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Records Updated";
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void cmbkeypad_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            select_controller();    //get the selected controller
        }

        private void chkrow1_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkrow1.Checked == true)
            {
                chkrow1.Text = "Enabled";
            }
            else
            {
                chkrow1.Text = "Disabled";
            }
        }

        private void chkrow2_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkrow2.Checked == true)
            {
                chkrow2.Text = "Enabled";
            }
            else
            {
                chkrow2.Text = "Disabled";
            }
        }

        private void chkrow3_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkrow3.Checked == true)
            {
                chkrow3.Text = "Enabled";
            }
            else
            {
                chkrow3.Text = "Disabled";
            }
        }

        private void chkrow4_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chkrow4.Checked == true)
            {
                chkrow4.Text = "Enabled";
            }
            else
            {
                chkrow4.Text = "Disabled";
            }
        }

        //http request to take backup
        public String Backup()
        {
            try 
            {
                string postData = "";
                string URL = "http://" + Database_Connection.GET_SERVER_IP + ":8091/BACKUP_DB";
                var data = "";
                data = webGetMethod(postData, URL);
                if (data.Contains("TRUE"))
                {
                    return ("TRUE");
                }
                else
                {
                    return ("FALSE");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ("");
        }

        //http get method
        public String webGetMethod(String postData, String URL)
        {
            try
            {
                //GET Method
                string html = string.Empty;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                return html;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return "";
        }

        private void radButton1_Click_2(object sender, EventArgs e)
        {
            //get selected folder path
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;

            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                String path = folderDlg.SelectedPath + "\\";
                path = path.Replace("\\\\", "\\");
                txtbackuppath.Text = path;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }

        private void txtbackuppath_TextChanged(object sender, EventArgs e)
        {
            btnupdategeneral.ForeColor = Color.Red;
        }

        private void radButton2_Click_2(object sender, EventArgs e)
        {
            try
            {
                String temp = Backup();   //take backup
                if (temp == "TRUE")
                {
                    radLabel4.Text = "Backup Completed";
                }
                else
                {
                    radLabel4.Text = "Error On Backup";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void cmbbackuptimer_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            btnupdategeneral.ForeColor = Color.Red;
        }

        private void radButton4_Click(object sender, EventArgs e)
        {
            //open restore db
            Restore_DB db = new Restore_DB();
            db.Show();
        }

        private void chktimerenable_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (chktimerenable.Checked == true)
            {
                chktimerenable.Text = "Enabled";
            }
            else
            {
                chktimerenable.Text = "Disabled";
            }

            btnupdategeneral.ForeColor = Color.Red;
        }

        private void radButton5_Click_1(object sender, EventArgs e)
        {
            //reset db
            DialogResult result = RadMessageBox.Show("Applying the Reset Database will Clear all the Data from Database and will Restart the GUI?", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
            if (result.Equals(DialogResult.Yes))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("delete from MO_DETAILS", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from MO", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from MO_DETAILS", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from DESIGN_SEQUENCE", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from ARTICLE_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from PROD_LINE_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from CUSTOMER_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from EMPLOYEE", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from CONTRACTOR_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from COLOR_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from CONTROLLER_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from EDIT_RECORDS", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from EMP_SEQ", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from HANGER_HISTORY", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from HIDEDAY_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from HOLIDAY_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from LAST_SELECT_MO", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from MACHINE_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from OPERATION_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from QC_HISTORY", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from QC_MAIN_CATEGORY", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from QC_SUB_CATEGORY", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from SHIFT_BREAKS", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from SHIFTS", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from SIZE_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from SORT_CALL_OUT_SEQUENCE", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from STATION_ASSIGN", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from STATION_DATA", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from USER_DEF1_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from USER_DEF2_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from USER_DEF3_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from USER_DEF4_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from USER_DEF5_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from USER_DEF6_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from USER_DEF7_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from USER_DEF8_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from USER_DEF9_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from USER_DEF10_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from EMPLOYEE_GROUPS", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from EMPLOYEE_GROUP_CATEGORY", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from MACHINE_ASSIGN", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from MACHINE_BREAKDOWN_HISTORY", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from MACHINE_DETAILS", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from MB_MAIN_CATEGORY", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from MB_SUB_CATEGORY", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from CLUSTER_DB", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from SKILL_RATE", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from Setup", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from BUFFER_GROUP", dc.con);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("delete from BUFFER_STATION", dc.con);
                    cmd.ExecuteNonQuery();

                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);

                    byte[] photo_aray = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(photo_aray, 0, photo_aray.Length);

                    cmd = new SqlCommand("insert into MRT_GLOBALDB.dbo.Setup values(@lang,@timer,@hanger,@cluster,@emp,@weekends,@skill,@backuppath,@backuptimer,@backupenable,@hideovertime,@activationkey,@theme,@logo,@hidetotals,@getallop,@multi)", dc.con);
                    cmd.Parameters.AddWithValue("@lang", "English");
                    cmd.Parameters.AddWithValue("@timer", "60000");
                    cmd.Parameters.AddWithValue("@hanger", "10");
                    cmd.Parameters.AddWithValue("@cluster", "--SELECT--");
                    cmd.Parameters.AddWithValue("@emp", "TRUE");
                    cmd.Parameters.AddWithValue("@weekends", "Sunday");
                    cmd.Parameters.AddWithValue("@skill", "FALSE");
                    cmd.Parameters.AddWithValue("@backuppath", "D:\\");
                    cmd.Parameters.AddWithValue("@backuptimer", "15");
                    cmd.Parameters.AddWithValue("@backupenable", "FALSE");
                    cmd.Parameters.AddWithValue("@hideovertime", "FALSE");
                    cmd.Parameters.AddWithValue("@activationkey", "FDkr/Q6UDfxm+OJ23DLTgmwy3BbQ6/bI");
                    cmd.Parameters.AddWithValue("@theme", "Office2010Blue");
                    cmd.Parameters.AddWithValue("@logo", photo_aray);
                    cmd.Parameters.AddWithValue("@hidetotals", "FALSE");
                    cmd.Parameters.AddWithValue("@getallop", "FALSE");
                    cmd.Parameters.AddWithValue("@multi", "TRUE");
                    //cmd.Parameters.AddWithValue("@followemp", "TRUE");
                    cmd.ExecuteNonQuery();

                    Application.Restart();
                }
                catch (Exception ex)
                {
                    radLabel4.Text = ex.Message;
                }
            }
        }

        private void chkhideot_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (flg == 1)
            {
                return;
            }

            if (chkhideot.Checked == true)
            {
                //update
                SqlCommand cmd = new SqlCommand("Update Setup set HIDE_OVERTIME='TRUE'", dc.con);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Hide OverTime Enabled";
                chkhideot.Text = "Enabled";
            }
            else
            {
                //update
                SqlCommand cmd = new SqlCommand("Update Setup set HIDE_OVERTIME='FALSE'", dc.con);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Hide OverTime Dissbled";
                chkhideot.Text = "Disabled";
            }
        }

        private void radButton6_Click_2(object sender, EventArgs e)
        {
            //check if no copies is valid
            Regex r = new Regex("^[0-9]*$");
            if (!r.IsMatch(txtcopies.Text) || txtcopies.Text == "")
            {
                radLabel4.Text = "Invalid Copies value. Example : 5";
                txtcopies.Text = "";
                return;
            }

            panel25.Visible = true;

            DataTable dt1 = new DataTable();
            dt1.Columns.Add("ROW_NO");
            dt1.Columns.Add("ROW1_POS1");
            dt1.Columns.Add("ROW1_POS2");
            dt1.Columns.Add("ROW1_POS3");
            dt1.Columns.Add("ROW2_POS1");
            dt1.Columns.Add("ROW2_POS2");
            dt1.Columns.Add("ROW2_POS3");
            dt1.Columns.Add("ROW3_POS1");
            dt1.Columns.Add("ROW3_POS2");
            dt1.Columns.Add("ROW3_POS3");
            dt1.Columns.Add("ROW4_POS1");
            dt1.Columns.Add("ROW4_POS2");
            dt1.Columns.Add("ROW4_POS3");

            int count = int.Parse(txtcopies.Text);
            count *= 6;
            String row1pos1 = "0";
            String row1pos2 = "0";
            String row1pos3 = "0";

            String row2pos1 = "0";
            String row2pos2 = "0";
            String row2pos3 = "0";

            String row3pos1 = "0";
            String row3pos2 = "0";
            String row3pos3 = "0";

            String row4pos1 = "0";
            String row4pos2 = "0";
            String row4pos3 = "0";

            //get short desc
            MySqlDataAdapter sda = new MySqlDataAdapter("select SHORT_DESC,DESCRIPTION from displaydata", dc.conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (cmbrow1position1.Text == dt.Rows[i][1].ToString())
                {
                    row1pos1 = dt.Rows[i][0].ToString();
                }

                if (cmbrow1position2.Text == dt.Rows[i][1].ToString())
                {
                    row1pos2 = dt.Rows[i][0].ToString();
                }

                if (cmbrow1position3.Text == dt.Rows[i][1].ToString())
                {
                    row1pos3 = dt.Rows[i][0].ToString();
                }

                if (cmbrow2position1.Text == dt.Rows[i][1].ToString())
                {
                    row2pos1 = dt.Rows[i][0].ToString();
                }

                if (cmbrow2position2.Text == dt.Rows[i][1].ToString())
                {
                    row2pos2 = dt.Rows[i][0].ToString();
                }

                if (cmbrow2position3.Text == dt.Rows[i][1].ToString())
                {
                    row2pos3 = dt.Rows[i][0].ToString();
                }

                if (cmbrow3position1.Text == dt.Rows[i][1].ToString())
                {
                    row3pos1 = dt.Rows[i][0].ToString();
                }

                if (cmbrow3position2.Text == dt.Rows[i][1].ToString())
                {
                    row3pos2 = dt.Rows[i][0].ToString();
                }

                if (cmbrow3position3.Text == dt.Rows[i][1].ToString())
                {
                    row3pos3 = dt.Rows[i][0].ToString();
                }

                if (cmbrow4position1.Text == dt.Rows[i][1].ToString())
                {
                    row4pos1 = dt.Rows[i][0].ToString();
                }

                if (cmbrow4position2.Text == dt.Rows[i][1].ToString())
                {
                    row4pos2 = dt.Rows[i][0].ToString();
                }

                if (cmbrow4position3.Text == dt.Rows[i][1].ToString())
                {
                    row4pos3 = dt.Rows[i][0].ToString();
                }
            }

            //add to datatable
            for (int i = 1; i <= count; i++)
            {
                dt1.Rows.Add(i, row1pos1, row1pos2, row1pos3, row2pos1, row2pos2, row2pos3, row3pos1, row3pos2, row3pos3, row4pos1, row4pos2, row4pos3);
            }

            DataView view = new DataView(dt1);

            reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Keypad.rdlc";
            reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
            reportViewer1.RefreshReport();
        }

        private void chkskill_CheckStateChanged(object sender, EventArgs e)
        {
            chkemployeeskill.Checked = true;
            chkoperationskill.Checked = true;
            if (chkskill.Checked == false)
            {
                chkemployeeskill.Checked = false;
                chkoperationskill.Checked = false;
            }
        }

        private void btneditprodline_Click(object sender, EventArgs e)
        {
            RowSelected_ProdLine();    //get the selected row
        }

        public void RowSelected_ProdLine()
        {
            if (dgvprodline.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                txtprodline.Text = dgvprodline.SelectedRows[0].Cells[0].Value + string.Empty;
                txtfactoryname.Text = dgvprodline.SelectedRows[0].Cells[1].Value + string.Empty;
                txtfactoryno.Text = dgvprodline.SelectedRows[0].Cells[2].Value + string.Empty;
                txtbuildingno.Text = dgvprodline.SelectedRows[0].Cells[3].Value + string.Empty;
                txtfloorno.Text = dgvprodline.SelectedRows[0].Cells[4].Value + string.Empty;
                txtsectionno.Text = dgvprodline.SelectedRows[0].Cells[5].Value + string.Empty;
                txtipaddress.Text = dgvprodline.SelectedRows[0].Cells[6].Value + string.Empty;
                txtport.Text = dgvprodline.SelectedRows[0].Cells[7].Value + string.Empty;
                cmbcontroller.Text = dgvprodline.SelectedRows[0].Cells[8].Value + string.Empty;
                cmbcluster.Text = dgvprodline.SelectedRows[0].Cells[9].Value + string.Empty;

                txtprodline.Enabled = false;
                btnsaveprodline.Text = update;
                btndeleteprodline.Enabled = true;
                btnsaveprodline.ForeColor = Color.Red;
            }
        }

        private void dgvprodline_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_ProdLine();     //get the selected row
        }

        private void btndeleteprodline_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from PROD_LINE_DB where V_PROD_LINE='" + txtprodline.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Record Deleted";
                RefereshGrid_ProdLine();   //get the master

                txtprodline.Enabled = true;
                btnsaveprodline.Text = save;
                ClearData_ProdLine();    //clear all fields

                btndeleteprodline.Enabled = false;
            }
            catch (Exception ex)
            {
                radLabel4.Text = "Production Line is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        public void RefereshGrid_ProdLine()
        {
            //get prodline details
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_PROD_LINE,V_FACTORY_NAME,V_FACTORY_NO,V_BUILDING_NO,V_FLOOR_NO,V_SECTION_NO,V_IP_ADDRESS,I_PORT,V_CONTROLLER,V_CLUSTER_DB FROM PROD_LINE_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "PROD_LINE_DB");
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();

            dgvprodline.DataSource = ds.Tables["PROD_LINE_DB"].DefaultView;
            dgvprodline.Columns["V_PROD_LINE"].HeaderText = "Production Line";
            dgvprodline.Columns["V_FACTORY_NAME"].HeaderText = "Factory Name";
            dgvprodline.Columns["V_FACTORY_NO"].HeaderText = "Factory No";
            dgvprodline.Columns["V_BUILDING_NO"].HeaderText = "Building No";
            dgvprodline.Columns["V_FLOOR_NO"].HeaderText = "Floor No";
            dgvprodline.Columns["V_SECTION_NO"].HeaderText = "Section No";
            dgvprodline.Columns["V_IP_ADDRESS"].HeaderText = "IP Address";
            dgvprodline.Columns["I_PORT"].HeaderText = "Port No";
            dgvprodline.Columns["V_CONTROLLER"].HeaderText = "Controller";
            dgvprodline.Columns["V_CLUSTER_DB"].HeaderText = "Cluster DB";
            dgvprodline.Visible = false;

            if (dgvprodline.Rows.Count > 0)
            {
                dgvprodline.Visible = true;
            }

            txtinfeedlineno.Items.Clear();
            txtoutfeedlineno.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txtinfeedlineno.Items.Add(dt.Rows[i][0].ToString());
                txtoutfeedlineno.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        //clear all fields
        public void ClearData_ProdLine()
        {
            txtprodline.Text = "";
            txtfactoryname.Text = "";
            txtfactoryno.Text = "";
            txtbuildingno.Text = "";
            txtfloorno.Text = "";
            txtsectionno.Text = "";
            txtipaddress.Text = "";
            txtport.Text = "";
            cmbcontroller.Text = "--SELECT--";
            cmbcluster.Text = "--SELECT--";
            btnsaveprodline.ForeColor = Color.Lime;
        }

        private void btnsaveprodline_Click(object sender, EventArgs e)
        {
            //check if ipaddress is valid
            IPAddress ip;
            bool ValidateIP = IPAddress.TryParse(txtipaddress.Text, out ip);
            if (!ValidateIP)
            {
                radLabel4.Text = "Invalid IP Address";
                return;
            }

            //check if prod line is valid
            Regex r = new Regex("^[0-9]*$");
            if (!r.IsMatch(txtprodline.Text))
            {
                radLabel4.Text = "Invalid Production Line No value. Example : 3";
                txtprodline.Text = "";
                return;
            }

            //check if port is valid
            r = new Regex("^[0-9]{4}?$");
            if (!r.IsMatch(txtport.Text))
            {
                radLabel4.Text = "Invalid Port No. Example : 8081";
                txtport.Text = "";
                return;
            }

            //check if controller is enabled
            String enabled = "";
            SqlCommand cmd2 = new SqlCommand("SELECT V_ENABLED FROM CONTROLLER_DB where V_CONTROLLER_ID='" + cmbcontroller.Text + "'", dc.con);
            SqlDataReader sdr = cmd2.ExecuteReader();
            if (sdr.Read())
            {
                enabled = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            try
            {
                //check if all the fields are inserted
                if (txtprodline.Text != "" && txtfactoryname.Text != "" && txtfactoryno.Text != "" && txtbuildingno.Text != "" && txtfloorno.Text != "" && txtsectionno.Text != "")
                {
                    btndeleteprodline.Enabled = false;
                    if (btnsaveprodline.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from PROD_LINE_DB where V_PROD_LINE='" + txtprodline.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //check if id adlready exists
                        if (i == 0)
                        {
                            //insert
                            SqlCommand cmd = new SqlCommand("insert into PROD_LINE_DB values('" + txtprodline.Text + "','" + txtfactoryname.Text + "','" + txtfactoryno.Text + "','" + txtbuildingno.Text + "','" + txtfloorno.Text + "','" + txtsectionno.Text + "','" + txtipaddress.Text + "','" + txtport.Text + "','" + cmbcontroller.Text + "','" + enabled + "','FALSE','" + cmbcluster.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            radLabel4.Text = "Records Saved";
                            RefereshGrid_ProdLine();   //get the master

                            txtprodline.Enabled = true;
                            ClearData_ProdLine();    //clear all fields
                        }
                        else
                        {
                            radLabel4.Text = "Production Line Already Exists";
                        }
                    }

                    if (btnsaveprodline.Text == update)
                    {
                        //update
                        SqlCommand cmd = new SqlCommand("Update PROD_LINE_DB set V_FACTORY_NAME='" + txtfactoryname.Text + "',V_FACTORY_NO='" + txtfactoryno.Text + "',V_BUILDING_NO='" + txtbuildingno.Text + "',V_FLOOR_NO='" + txtfloorno.Text + "',V_SECTION_NO='" + txtsectionno.Text + "',V_IP_ADDRESS='" + txtipaddress.Text + "',I_PORT='" + txtport.Text + "',V_CONTROLLER='" + cmbcontroller.Text + "',V_CONTROLLER_ENABLED='" + enabled + "',V_CLUSTER_DB='" + cmbcluster.Text + "' where V_PROD_LINE='" + txtprodline.Text + "'", dc.con);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = "Records Updated";
                        RefereshGrid_ProdLine();    //get the master

                        txtprodline.Enabled = true;
                        btnsaveprodline.Text = save;
                        ClearData_ProdLine();   //clear all fields
                    }
                    btnsaveprodline.ForeColor = Color.Lime;
                }
                else
                {
                    radLabel4.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btneditcluster_Click(object sender, EventArgs e)
        {
            RowSelected_Cluster();     //get the selected row
        }

        public void RowSelected_Cluster()
        {
            try
            {
                if (dgvcluster.SelectedRows.Count > 0) // make sure user select at least 1 row 
                {
                    txtclusterid.Text = dgvcluster.SelectedRows[0].Cells[0].Value + string.Empty;
                    txtclusterip.Text = dgvcluster.SelectedRows[0].Cells[1].Value + string.Empty;
                    txtclusterid.Enabled = false;
                    btnsavecluster.Text = update;
                    btndeletecluster.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void dgvcluster_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Cluster();     //get the selected row
        }

        private void btndeletecluster_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from CLUSTER_DB where V_CLUSTER_ID='" + txtclusterid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Record Deleted";
                RefereshCluster();   //get the master

                txtclusterid.Enabled = true;
                btnsavecluster.Text = save;
                btndeletecluster.Enabled = false;
                txtclusterid.Text = "";
                txtclusterip.Text = "";
                btnsavecluster.ForeColor = Color.Lime;
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btnsavecluster_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtclusterid.Text != "" || txtclusterip.Text != "")
                {
                    //check if ipaddress is valid
                    IPAddress ip;
                    bool ValidateIP = IPAddress.TryParse(txtclusterip.Text, out ip);
                    if (!ValidateIP)
                    {
                        radLabel4.Text = "Invalid IP Address";
                        return;
                    }

                    //update system dsn
                    btndeletecluster.Enabled = false;
                    Process.Start("cmd.exe", $"/c ODBCCONF.exe CONFIGSYSDSN \"MySQL ODBC 8.0 ANSI Driver\" \"DSN = CONTROLLER | SERVER = " + txtclusterip.Text + "\"");

                    if (btnsavecluster.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from CLUSTER_DB where V_CLUSTER_ID='" + txtclusterid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //check if the id exists
                        if (i == 0)
                        {
                            //insert
                            SqlCommand cmd = new SqlCommand("insert into CLUSTER_DB values('" + txtclusterid.Text + "','" + txtclusterip.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            radLabel4.Text = "Records Saved";
                            RefereshCluster();   //get the master

                            txtclusterid.Enabled = true;
                            txtclusterid.Text = "";
                            txtclusterip.Text = "";
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvcluster.Rows.Count; j++)
                            {
                                if (dgvcluster.Rows[j].Cells[0].Value.ToString().Equals(txtclusterid.Text))
                                {
                                    dgvcluster.Rows[j].IsSelected = true;
                                    radLabel4.Text = "Controller Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsavecluster.Text == update)
                    {
                        //update
                        SqlCommand cmd = new SqlCommand("Update CLUSTER_DB set V_CLUSTER_IP_ADDRESS='" + txtclusterip.Text + "' where V_CLUSTER_ID='" + txtclusterid.Text + "'", dc.con);
                        cmd.ExecuteNonQuery();

                        radLabel4.Text = "Records Updated";
                        RefereshCluster();   //get the master

                        txtclusterid.Enabled = true;
                        txtclusterid.Text = "";
                        txtclusterip.Text = "";
                        btnsavecluster.Text = save;
                    }
                    btnsavecluster.ForeColor = Color.Lime;
                }
                else
                {
                    radLabel4.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void radPageView2_KeyDown(object sender, KeyEventArgs e)
        {
            //special keys for hide day
            if (e.KeyData.ToString() == "D3, Shift")
            {
                return;
            }

            if (e.KeyData.ToString() == "Escape")
            {
                radTextBox2.Text = "";
                pagehideday.Item.Visibility = ElementVisibility.Collapsed;
                vpagesetup.SelectedPage = pagespecial;
                return;
            }

            radTextBox2.Text += e.KeyData.ToString();
        }

        private void vpagecontroller_KeyDown(object sender, KeyEventArgs e)
        {
            //special keys to open hide day
            if (e.KeyData.ToString() == "D3, Shift")
            {
                return;
            }

            if (e.KeyData.ToString() == "Escape")
            {
                radTextBox2.Text = "";
                pagehideday.Item.Visibility = ElementVisibility.Collapsed;
                vpagesetup.SelectedPage = pagespecial;
                return;
            }

            radTextBox2.Text += e.KeyData.ToString();
        }

        private void cmbcontroller_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

        }

        private void btneditbuffer_Click(object sender, EventArgs e)
        {
            RowSelected_Buffer();    //get the selected row
        }

        public void RowSelected_Buffer()
        {
            if (dgvbuffergroup.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String mainId = dgvbuffergroup.SelectedRows[0].Cells[0].Value + string.Empty;
                String mainDesc = dgvbuffergroup.SelectedRows[0].Cells[1].Value + string.Empty;
                txtbufferid.Text = mainId;
                txtbufferdesc.Text = mainDesc;
                txtbufferid.ReadOnly = true;
                btnsavebuffer.Text = update;
                btndeletebuffer.Enabled = true;
                btnsavebuffer.ForeColor = Color.Red;
                buffergroup = mainDesc;
            }
        }

        private void dgvbuffergroup_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Buffer();     //get the selected row
        }

        private void btndeletebuffer_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from BUFFER_GROUP where V_BUFFER_GROUP_ID='" + txtbufferid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                //delete the selected row
                cmd = new SqlCommand("DELETE FROM BUFFER_STATION WHERE V_BUFFER_GROUP_ID NOT IN(SELECT D.V_BUFFER_GROUP_ID FROM BUFFER_GROUP D)", dc.con);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Record Deleted";
                RefereshGrid_Buffer();   //get the master

                txtbufferid.ReadOnly = false;
                btnsavebuffer.Text = save;
                ClearData_Buffer();    //clear all fields

                btndeletebuffer.Enabled = false;
                RefereshGrid_BufferStation();   //get the master
            }
            catch (Exception ex)
            {
                radLabel4.Text = "Buffer Group is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        //clear all fields
        public void ClearData_Buffer()
        {
            txtbufferid.Text = "";
            txtbufferdesc.Text = "";
            btnsavebuffer.ForeColor = Color.Lime;
        }

        private void btnsavebuffer_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtbufferid.Text != "" && txtbufferdesc.Text != "")
                {
                    btndeletebuffer.Enabled = false;
                    if (btnsavebuffer.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from BUFFER_GROUP where V_BUFFER_GROUP_ID='" + txtbufferid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from BUFFER_GROUP where V_BUFFER_GROUP_DESC='" + txtbufferdesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if the id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //insert
                            SqlCommand cmd = new SqlCommand("insert into BUFFER_GROUP values('" + txtbufferid.Text + "','" + txtbufferdesc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            radLabel4.Text = "Records Saved";
                            RefereshGrid_Buffer();   //get the master

                            txtbufferid.ReadOnly = false;
                            ClearData_Buffer();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvbuffergroup.Rows.Count; j++)
                            {
                                if (dgvbuffergroup.Rows[j].Cells[0].Value.ToString().Equals(txtbufferid.Text) || dgvbuffergroup.Rows[j].Cells[1].Value.ToString().Equals(txtbufferdesc.Text))
                                {
                                    dgvbuffergroup.Rows[j].IsSelected = true;
                                    radLabel4.Text = "Buffer Group Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsavebuffer.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from BUFFER_GROUP where V_BUFFER_GROUP_DESC='" + txtbufferdesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if the desc exists
                        if (k == 0 || buffergroup == txtbufferdesc.Text)
                        {
                            //update
                            SqlCommand cmd = new SqlCommand("Update BUFFER_GROUP set V_BUFFER_GROUP_DESC='" + txtbufferdesc.Text + "' where V_BUFFER_GROUP_ID='" + txtbufferid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            radLabel4.Text = "Records Updated";
                            RefereshGrid_Buffer();   //get the master

                            txtbufferid.ReadOnly = false;
                            btnsavebuffer.Text = save;
                            ClearData_Buffer();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvbuffergroup.Rows.Count; j++)
                            {
                                if (dgvbuffergroup.Rows[j].Cells[1].Value.ToString().Equals(txtbufferdesc.Text))
                                {
                                    dgvbuffergroup.Rows[j].IsSelected = true;
                                    radLabel4.Text = "Buffer Group Already Exists";
                                    return;
                                }
                            }
                        }
                    }

                    btnsavebuffer.ForeColor = Color.Lime;
                    RefereshGrid_BufferStation();   //get the master
                }
                else
                {
                    radLabel4.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void btneditbufferstation_Click(object sender, EventArgs e)
        {
            RowSelected_BufferStation();   //get the selected row
        }

        public void RowSelected_BufferStation()
        {
            if (dgvbufferstation.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String mainId = dgvbufferstation.SelectedRows[0].Cells[0].Value + string.Empty;
                String mainDesc = dgvbufferstation.SelectedRows[0].Cells[1].Value + string.Empty;
                buffergroupid = dgvbufferstation.SelectedRows[0].Cells[2].Value + string.Empty;

                cmbbuffergroup.Text = mainId;
                cmbbufferstation.Text = mainDesc;

                btnsavebufferstation.Text = update;
                btndeletebufferstation.Enabled = true;
                btnsavebufferstation.ForeColor = Color.Red;
                station = mainDesc;
            }
        }

        private void dgvbufferstation_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_BufferStation();     //get the selected row
        }

        private void btndeletebufferstation_Click(object sender, EventArgs e)
        {
            try
            {
                String[] stn = cmbbufferstation.Text.Split('.');

                //get station id
                SqlCommand cmd = new SqlCommand("select I_STN_ID from STATION_DATA s where s.I_INFEED_LINE_NO='" + stn[0] + "' and s.I_STN_NO_INFEED='" + stn[1] + "'", dc.con);
                String stationid = "";
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    stationid = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //check if there is any hangers in the buffer
                MySqlCommand cmd1 = new MySqlCommand("select count(*) from bufferhangers where STN_ID='" + stationid + "'", dc.conn);
                Int32 k = int.Parse(cmd1.ExecuteScalar().ToString());
                if (k != 0)
                {
                    radLabel4.Text = "Station ID : " + cmbbufferstation.Text + " in not Empty.";
                    return;
                }

                //get buffer group id
                cmd = new SqlCommand("select V_BUFFER_GROUP_ID from BUFFER_GROUP where V_BUFFER_GROUP_DESC='" + cmbbuffergroup.Text + "'", dc.con);
                String maincode = "";
                sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    maincode = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //delete the selected row
                cmd = new SqlCommand("Delete from BUFFER_STATION where V_BUFFER_GROUP_ID='" + maincode + "' and V_BUFFER_STATION_NO='" + cmbbufferstation.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                radLabel4.Text = "Record Deleted";
                RefereshGrid_BufferStation();   //get the master

                btnsavebufferstation.Text = save;
                cmbbuffergroup.Text = "--SELECT--";
                cmbbufferstation.Text = "--SELECT--";
                btndeletebufferstation.Enabled = false;
            }
            catch (Exception ex)
            {
                radLabel4.Text = "Buffer Station is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        private void btnsavebufferstation_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (cmbbuffergroup.Text != "--SELECT--" && cmbbufferstation.Text != "--SELECT--")
                {
                    //get the buffer group id
                    SqlCommand cmd = new SqlCommand("select V_BUFFER_GROUP_ID from BUFFER_GROUP where V_BUFFER_GROUP_DESC='" + cmbbuffergroup.Text + "'", dc.con);
                    String maincode = "";
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        maincode = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    String[] stn = cmbbufferstation.Text.Split('.');

                    //get station id
                    cmd = new SqlCommand("select I_STN_ID from STATION_DATA s where s.I_INFEED_LINE_NO='" + stn[0] + "' and s.I_STN_NO_INFEED='" + stn[1] + "'", dc.con);
                    String stationid = "";
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        stationid = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    //check if buffer station is already used
                    MySqlCommand cmd1 = new MySqlCommand("select count(*) from bufferhangers where STN_ID='" + stationid + "'", dc.conn);
                    Int32 m = int.Parse(cmd1.ExecuteScalar().ToString());
                    if (m != 0)
                    {
                        radLabel4.Text = "Station ID : " + cmbbufferstation.Text + " in not Empty.";
                        //return;
                    }

                    btndeletebufferstation.Enabled = false;
                    if (btnsavebufferstation.Text == save)
                    {
                        //get id count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from BUFFER_STATION where V_BUFFER_STATION_NO='" + cmbbufferstation.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id already exists
                        if (k == 0 || cmbbufferstation.Text == station)
                        {
                            //insert
                            cmd = new SqlCommand("insert into BUFFER_STATION values('" + maincode + "','" + stationid + "','" + cmbbufferstation.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            radLabel4.Text = "Records Saved"; 
                            RefereshGrid_BufferStation();    //get the master

                            cmbbuffergroup.Text = "--SELECT--";
                            cmbbufferstation.Text = "--SELECT--";
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvbufferstation.Rows.Count; j++)
                            {
                                if (dgvbufferstation.Rows[j].Cells[1].Value.ToString().Equals(cmbbufferstation.Text))
                                {
                                    dgvbufferstation.Rows[j].IsSelected = true;
                                    radLabel4.Text = "Buffer Station Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsavebufferstation.Text == update)
                    {
                        //get id count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from BUFFER_STATION where V_BUFFER_STATION_NO='" + cmbbufferstation.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if the id exists
                        if (k == 0 || cmbbufferstation.Text == station)
                        {
                            //update
                            cmd = new SqlCommand("Update BUFFER_STATION set V_BUFFER_STATION_NO='" + cmbbufferstation.Text + "',V_BUFFER_STATION_ID='" + stationid + "',V_BUFFER_GROUP_ID='" + maincode + "' where V_ID='" + buffergroupid + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            radLabel4.Text = "Records Updated";
                            RefereshGrid_BufferStation();   //get the master

                            btnsavebufferstation.Text = save;
                            cmbbuffergroup.Text = "--SELECT--";
                            cmbbufferstation.Text = "--SELECT--";
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvbufferstation.Rows.Count; j++)
                            {
                                if (dgvbufferstation.Rows[j].Cells[1].Value.ToString().Equals(cmbbufferstation.Text))
                                {
                                    dgvbufferstation.Rows[j].IsSelected = true;
                                    radLabel4.Text = "Buffer Station Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    btnsavebufferstation.ForeColor = Color.Lime;
                }
                else
                {
                    radLabel4.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void txtinfeedlineno_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Infeed_Line_Pusher();    //get infeed line pusher
        }

        private void txtoutfeedlineno_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Outfeed_Line_Pusher();   //get outfeed line pusher
        }

        private void txtinfeedlineno_TextChanged(object sender, EventArgs e)
        {
            Infeed_Line_Pusher();    //get infeed line pusher
        }

        private void txtoutfeedlineno_TextChanged(object sender, EventArgs e)
        {
            Outfeed_Line_Pusher();   // get outfeed line pusher
        }

        public void Infeed_Line_Pusher()
        {
            try
            {
                //get the selected controller
                if (cmbstncontroller.Text == "--SELECT--" || cmbstncontroller.Text == "")
                {
                    radLabel4.Text = "Select the Controller";
                    return;
                }

                txtinfeedoffset.Items.Clear();
                txtinfeedchainoffset.Items.Clear();

                //get pusher count and chain count
                MySqlDataAdapter sda = new MySqlDataAdapter("select PUSHER_COUNT,CHAIN_COUNT from pusherinfo where LINE_NO='" + txtinfeedlineno.Text + "'", dc.conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                if (dt.Rows.Count > 0)
                {
                    int pusher = int.Parse(dt.Rows[0][0].ToString());
                    int chain = int.Parse(dt.Rows[0][1].ToString());

                    for (int i = 0; i <= pusher; i++)
                    {
                        txtinfeedoffset.Items.Add(i.ToString());
                    }

                    for (int i = 0; i <= chain; i++)
                    {
                        txtinfeedchainoffset.Items.Add(i.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        public void Outfeed_Line_Pusher()
        {
            try
            {
                //get the selected controller
                if (cmbstncontroller.Text == "--SELECT--" || cmbstncontroller.Text == "")
                {
                    radLabel4.Text = "Select the Controller";
                    return;
                }

                txtoutfeedoffset.Items.Clear();
                txtoutfeedchainoffset.Items.Clear();

                //get pusher count and chain count
                MySqlDataAdapter sda = new MySqlDataAdapter("select PUSHER_COUNT,CHAIN_COUNT from pusherinfo where LINE_NO='" + txtinfeedlineno.Text + "'", dc.conn);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                if (dt.Rows.Count > 0)
                {
                    int pusher = int.Parse(dt.Rows[0][0].ToString());
                    int chain = int.Parse(dt.Rows[0][1].ToString());

                    for (int i = 0; i <= pusher; i++)
                    {
                        txtoutfeedoffset.Items.Add(i.ToString());
                    }

                    for (int i = 0; i <= chain; i++)
                    {
                        txtoutfeedchainoffset.Items.Add(i.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                radLabel4.Text = ex.Message;
            }
        }

        private void chkautologin_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkautologin.Checked == true)
            {
                chkautologin.Text = "Enabled";
                autologin = 1;
            }
            else
            {
                chkautologin.Text = "Disabled";
                autologin = 0;
            }
        }

        private void dgvuser_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvuser.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvuser.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvuser.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvuser.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvusergroup_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvusergroup.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvusergroup.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvusergroup.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvusergroup.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvcluster_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvcluster.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvcluster.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvcluster.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvcluster.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvcontroller_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvcontroller.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvcontroller.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvcontroller.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvcontroller.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvprodline_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvprodline.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvprodline.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvprodline.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvprodline.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvpusher_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvpusher.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvpusher.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvpusher.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvpusher.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvroute_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvroute.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvroute.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvroute.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvroute.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvrouting_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvrouting.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvrouting.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvrouting.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvrouting.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvstation_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvstation.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvstation.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvstation.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvstation.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvhanger_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvhanger.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvhanger.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvhanger.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvhanger.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvbuffer_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvbuffergroup.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvbuffergroup.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvbuffergroup.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvbuffergroup.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvbufferstation_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvbufferstation.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvbufferstation.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvbufferstation.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvbufferstation.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvbreaks_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvbreaks.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvbreaks.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvbreaks.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvbreaks.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvholiday_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvholiday.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvholiday.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvholiday.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvholiday.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvhideday_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvhideday.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvhideday.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvhideday.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvhideday.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvshifts_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvshifts.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvshifts.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvshifts.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvshifts.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void radButton6_Click_3(object sender, EventArgs e)
        {
            //open files
            OpenFileDialog img = new OpenFileDialog();
            img.InitialDirectory = "C:/Picture/";
            img.Filter = "All Files|*.*|JPEGs|*.jpg|Bitmaps|*.bmp|GIFs|*.gif";
            img.FilterIndex = 2;

            if (img.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(img.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void radPageView2_SelectedPageChanged(object sender, EventArgs e)
        {
            //check if unsaved tabs
            if (btnsaveusergroup.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved User Group. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveusergroup.PerformClick();
                }
                btnsaveusergroup.ForeColor = Color.Lime;
            }
            else if (btncreateaccount.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved User Login. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btncreateaccount.PerformClick();
                }
                btncreateaccount.ForeColor = Color.Lime;
            }
            else if (btnsaveaccesspriv.ForeColor == Color.Red)
            {
                DialogResult result = RadMessageBox.Show("Unsaved User Access Previlages. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveaccesspriv.PerformClick();
                }
                btnsaveaccesspriv.ForeColor = Color.Lime;
            }
        }

        private void chkhidetotals_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkhidetotals.Checked == true)
            {
                chkhidetotals.Text = "Enabled";
            }
            else
            {
                chkhidetotals.Text = "Disabled";
            }
        }

        private void chkgetallop_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkgetallop.Checked == true)
            {
                chkgetallop.Text = "Enabled";
            }
            else
            {
                chkgetallop.Text = "Disabled";
            }
        }

        private void chkmultilogin_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            
        }

        private void chkfollowemployee_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (chkfollowemployee.Checked == true)
            {
                chkfollowemployee.Text = "Enabled";
            }
            else
            {
                chkfollowemployee.Text = "Disabled";
            }
        }

        private void chkmultilogin_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkmultilogin.Checked == true)
            {
                chkmultilogin.Text = "Enabled";
                //chkfollowemployee.Visible = false;
                //radLabel11.Visible = false;
            }
            else
            {
                chkmultilogin.Text = "Disabled";
                //chkfollowemployee.Visible = true;
                //chkfollowemployee.Checked = false;
                //radLabel11.Visible = true;
            }
        }

        public void DebugLog(string Message)
        {
            try
            {
                //string path = "C:\\SMARTMRT\\SmartMRT MGIS\\Debug\\" + DateTime.Now.ToString("MMMM yyyy");
                string path = Application.StartupPath + "\\Debug\\" + DateTime.Now.ToString("MMMM yyyy");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = path + "\\DebugLogs_" + DateTime.Now.Date.ToString("yyyy-MM-dd") + ".txt";
                if (!File.Exists(filepath))
                {
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " : " + Message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " : " + Message);
                    }
                }
            }
            catch (Exception ex)
            {
                //WriteToExFile("Debug Logfile is in Use : " + ex.Message + " : " + ex);
            }
        }
    }
}
