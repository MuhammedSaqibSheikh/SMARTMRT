using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic;
using System.Configuration;
using System.Security.Cryptography;
using System.Threading;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Diagnostics;

namespace SMARTMRT
{
    public partial class Home : Telerik.WinControls.UI.RadForm
    {
        public Home()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
        }

        Database_Connection dc = new Database_Connection();  //connection class
        MySqlConnection conn1;    //mysql connection object
        int i = 0;
        int led_flag = 0;     //led flag
        int production_flag = 0;   //production flag
        int shift_flag = 0;    //shift flag
        int reconnect_flag = 0;   //reconnect flag
        String Key = "";

        Bitmap img = new Bitmap(Properties.Resources.ok_16px);     //image for line is on
        Bitmap offline = new Bitmap(Properties.Resources.wifi_off_16px);   //image for line is offine
        Bitmap off = new Bitmap(Properties.Resources.lineoff);    //image for linr is off
        Bitmap emer = new Bitmap(Properties.Resources.emergency_stop_button_24px);    //image for linr is off

        private void Home_Load(object sender, EventArgs e)
        {
            RadMessageBox.SetThemeName("FluentDark");   //set message box theme
            lblusergroup.Text = Database_Connection.SET_USER;   //get user group

            //check if basis version of pms enabled
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion;
            this.Text += "v" + version;

            if (Database_Connection.SET_PMSCIENT=="1")
            {
                this.Text += " Basic";
            }
            else
            {
                this.Text += " Advanced";
            }
            
            //reconnect to pms server timer
            Reconnect_Timer.Interval = 2000;
            Reconnect_Timer.Tick += new EventHandler(Reconnect_Timer_Tick);
            Reconnect_Timer.Enabled = true;

            //production grid colors
            dgvproduction.AlternatingRowsDefaultCellStyle.BackColor = Color.SlateGray;
            this.dgvproduction.DefaultCellStyle.ForeColor = Color.White;
            dgvproduction.ColumnHeadersDefaultCellStyle.BackColor = Color.DimGray;
            dgvproduction.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvproduction.EnableHeadersVisualStyles = false;
            dgvproduction.DefaultCellStyle.SelectionBackColor = Color.Gray;

            //tooltips for the shortcut images
            ToolTip ToolTip1 = new ToolTip();
            ToolTip1.SetToolTip(this.picsetup, "Setup");
            ToolTip1.SetToolTip(this.picnewmo, "New MO");
            ToolTip1.SetToolTip(this.picopenmo, "Open MO");
            ToolTip1.SetToolTip(this.picstationassign, "Station Assign");
            ToolTip1.SetToolTip(this.picaddproduction, "Add to Production");
            ToolTip1.SetToolTip(this.picempreports, "Employee Report");
            ToolTip1.SetToolTip(this.picstationreports, "Station Report By Production");
            ToolTip1.SetToolTip(this.pictureBox2, "Refresh Production Line Status");
            ToolTip1.SetToolTip(this.pictureBox1, "SmartMRT");
            ToolTip1.SetToolTip(this.pictureBox3, "Buffer");
            ToolTip1.SetToolTip(this.pictureBox4, "Controller Setup");
            ToolTip1.SetToolTip(this.picrefreshcontroller, "Refresh Controller Status");
            ToolTip1.SetToolTip(this.pictureBox5, "Current Production");

            tmrclock.Enabled = true;
            lbltime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");
            
            Edit_MO em = new Edit_MO();
            em.MdiParent = this;

            //disable all the menu
            String home = "N";
            String setup = "N";
            String mono = "N";
            String addmo = "N";
            String openmo = "N";
            String production = "N";
            String stationassign = "N";
            String addtoproduction = "N";
            String masters = "N";
            String color = "N";
            String article = "N";
            String size = "N";
            String prodline = "N";
            String emp = "N";
            String contractor = "N";
            String customer = "N";
            String operation = "N";
            String special = "N";
            String user1 = "N";
            String user2 = "N";
            String user3 = "N";
            String user4 = "N";
            String user5 = "N";
            String user6 = "N";
            String user7 = "N";
            String user8 = "N";
            String user9 = "N";
            String user10 = "N";
            String reports = "N";
            String empreport = "N";
            String stationreport = "N";
            String buffer = "N";
            String restoreprod = "N";
            String qcmain = "N";
            String qcsub = "N";
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

            //get the user access previlages
            SqlDataAdapter sda = new SqlDataAdapter("select * from USER_LOGIN_DETAILS where USER_GROUP='" + lblusergroup.Text + "'", dc.con);
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

            //enable all the menu if super user is logged in
            if (lblusergroup.Text == "Super User")
            {
                home = "Y";
                setup = "Y";
                mono = "Y";
                addmo = "Y";
                openmo = "Y";
                production = "Y";
                stationassign = "Y";
                addtoproduction = "Y";
                masters = "Y";
                color = "Y";
                article = "Y";
                size = "Y";
                prodline = "Y";
                emp = "Y";
                contractor = "Y";
                customer = "Y";
                operation = "Y";
                special = "Y";
                user1 = "Y";
                user2 = "Y";
                user3 = "Y";
                user4 = "Y";
                user5 = "Y";
                user6 = "Y";
                user7 = "Y";
                user8 = "Y";
                user9 = "Y";
                user10 = "Y";
                reports = "Y";
                empreport = "Y";
                stationreport = "Y";
                buffer = "Y";
                restoreprod = "Y";
                qcmain = "Y";
                qcsub = "Y";
                groupcategory = "Y";
                empgroup = "Y";
                machines = "Y";
                machinedetails = "Y";
                mbmain = "Y";
                mbsub = "Y";
                empskill_level = "Y";
                production_plan = "Y";
                current_prod = "Y";
                skill = "Y";
                empskill = "Y";
                opskill = "Y";
                moreport = "Y";
                stn_prod_report = "Y";
                empqcreport = "Y";
                opqcreport = "Y";
                payrollreport = "Y";
                emplogs = "Y";
                moqcreport = "Y";
                stationqcreport = "Y";
                machinereport = "Y";
                machineassign = "Y";
                machinerepair = "Y";
                topdefects = "Y";
                designsequence = "Y";
                stationwip = "Y";
                linebalancing = "Y";
                performancereport = "Y";
                sparereport = "Y";
                spareinventory = "Y";
                sparemain = "Y";
                sparesub = "Y";
                moopreport = "Y";
            }

            //check if the basic version of pms client is enabled
            if (Database_Connection.SET_PMSCIENT == "1")
            {
                qcmain = "N";
                qcsub = "N";
                //machines = "N";
                //machinedetails = "N";
                mbmain = "N";
                mbsub = "N";
                sparemain = "N";
                sparesub = "N";

                stationwip = "N";
                linebalancing = "N";
                buffer = "N";
                production_plan = "N";

                skill = "N";
                empskill = "N";
                opskill = "N";

                moqcreport = "N";
                stationqcreport = "N";
                machinereport = "N";
                machineassign = "N";
                machinerepair = "N";
                topdefects = "N";
                empqcreport = "N";
                opqcreport = "N";
                payrollreport = "N";
                performancereport = "N";
                sparereport = "N";
                spareinventory = "N";
            }

            //check if the menu is enabled
            if (home == "Y")
            {
                menuhome.Visibility = ElementVisibility.Visible;
                menulogout.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuhome.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (buffer == "Y")
            {
                menuproduction.Visibility = ElementVisibility.Visible;
                menubuffer.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menubuffer.Visibility = ElementVisibility.Collapsed;
                pictureBox3.Enabled = false;
            }

            //check if the menu is enabled
            if (restoreprod == "Y")
            {
                menuproduction.Visibility = ElementVisibility.Visible;
                menurestoreproduction.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menurestoreproduction.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (setup == "Y")
            {
                menuhome.Visibility = ElementVisibility.Visible;
                menusetup.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menusetup.Visibility = ElementVisibility.Collapsed;
                picsetup.Enabled = false;
            }

            //check if the menu is enabled
            if (masters == "Y")
            {
                menumasters.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menumasters.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (color == "Y")
            {
                menumasters.Visibility = ElementVisibility.Visible;
                menucolormaster.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menucolormaster.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (article == "Y")
            {
                menumasters.Visibility = ElementVisibility.Visible;
                menuarticlemaster.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuarticlemaster.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (size == "Y")
            {
                menumasters.Visibility = ElementVisibility.Visible;
                menusizemaster.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menusizemaster.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (customer == "Y")
            {
                menumasters.Visibility = ElementVisibility.Visible;
                menucustomermaster.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menucustomermaster.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (operation == "Y")
            {
                menumasters.Visibility = ElementVisibility.Visible;
                menuoperationmaster.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuoperationmaster.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (emp == "Y")
            {
                menuemp.Visibility = ElementVisibility.Visible;
                menuempmaster.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuempmaster.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (contractor == "Y")
            {
                menuemp.Visibility = ElementVisibility.Visible;
                menucontractormaster.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menucontractormaster.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (mono == "Y")
            {
                menumo.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menumo.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (addmo == "Y")
            {
                menumo.Visibility = ElementVisibility.Visible;
                menunewmo.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menunewmo.Visibility = ElementVisibility.Collapsed;
                picnewmo.Enabled = false;
            }

            //check if the menu is enabled
            if (openmo == "Y")
            {
                menumo.Visibility = ElementVisibility.Visible;
                menuopenmo.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuopenmo.Visibility = ElementVisibility.Collapsed;
                picopenmo.Enabled = false;
            }

            //check if the menu is enabled
            if (production == "Y")
            {
                menuproduction.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuproduction.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (addtoproduction == "Y")
            {
                menuproduction.Visibility = ElementVisibility.Visible;
                menuaddproduction.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuaddproduction.Visibility = ElementVisibility.Collapsed;
                picaddproduction.Enabled = false;
            }

            //check if the menu is enabled
            if (stationassign == "Y")
            {
                menuproduction.Visibility = ElementVisibility.Visible;
                menustationassign.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menustationassign.Visibility = ElementVisibility.Collapsed;
                picstationassign.Enabled = false;
            }

            //check if the menu is enabled
            if (reports == "Y")
            {
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menureports.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (empreport == "Y")
            {
                menureports.Visibility = ElementVisibility.Visible;
                menuempreports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuempreports.Visibility = ElementVisibility.Collapsed;
                picempreports.Enabled = false;
            }

            //check if the menu is enabled
            if (stationreport == "Y")
            {
                menureports.Visibility = ElementVisibility.Visible;
                menustationreport.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menustationreport.Visibility = ElementVisibility.Collapsed;
                picstationreports.Enabled = false;
            }

            //check if the menu is enabled
            if (special == "Y")
            {
                menuspecial.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuspecial.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (user1 == "Y")
            {
                menuspecial.Visibility = ElementVisibility.Visible;
                menuuser1.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuuser1.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (user2 == "Y")
            {
                menuspecial.Visibility = ElementVisibility.Visible;
                menuuser2.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuuser2.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (user3 == "Y")
            {
                menuspecial.Visibility = ElementVisibility.Visible;
                menuuser3.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuuser3.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (user4 == "Y")
            {
                menuspecial.Visibility = ElementVisibility.Visible;
                menuuser4.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuuser4.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (user5 == "Y")
            {
                menuspecial.Visibility = ElementVisibility.Visible;
                menuuser5.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuuser5.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (user6 == "Y")
            {
                menuspecial.Visibility = ElementVisibility.Visible;
                menuuser6.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuuser6.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (user7 == "Y")
            {
                menuspecial.Visibility = ElementVisibility.Visible;
                menuuser7.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuuser7.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (user8 == "Y")
            {
                menuspecial.Visibility = ElementVisibility.Visible;
                menuuser8.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuuser8.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (user9 == "Y")
            {
                menuspecial.Visibility = ElementVisibility.Visible;
                menuuser9.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuuser9.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (user10 == "Y")
            {
                menuspecial.Visibility = ElementVisibility.Visible;
                menuuser10.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuuser10.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (qcmain == "Y")
            {
                menuqcmain.Visibility = ElementVisibility.Visible;
                menumasters.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuqcmain.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (qcsub == "Y")
            {
                menuqcsub.Visibility = ElementVisibility.Visible;
                menumasters.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuqcsub.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (sparemain == "Y")
            {
                menusparemain.Visibility = ElementVisibility.Visible;
                menumasters.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menusparemain.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (sparesub == "Y")
            {
                menusparesub.Visibility = ElementVisibility.Visible;
                menumasters.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menusparesub.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (groupcategory == "Y")
            {
                menuempgroupcategory.Visibility = ElementVisibility.Visible;
                menuemp.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuempgroupcategory.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (empgroup == "Y")
            {
                menuempgroups.Visibility = ElementVisibility.Visible;
                menuemp.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuempgroups.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (empskill_level == "Y")
            {
                menuskill.Visibility = ElementVisibility.Visible;
                menuemp.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuskill.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (machines == "Y")
            {
                menumachine.Visibility = ElementVisibility.Visible;
                menumasters.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menumachine.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (machinedetails == "Y")
            {
                menumachinedetails.Visibility = ElementVisibility.Visible;
                menumasters.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menumachinedetails.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (mbmain == "Y")
            {
                menumbmain.Visibility = ElementVisibility.Visible;
                menumasters.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menumbmain.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (mbsub == "Y")
            {
                menumbsub.Visibility = ElementVisibility.Visible;
                menumasters.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menumbsub.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (emplogs == "Y")
            {
                menuemployeelogin.Visibility = ElementVisibility.Visible;
                menuemp.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuemployeelogin.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (production_plan == "Y")
            {
                menuproductionplanning.Visibility = ElementVisibility.Visible;
                menuproduction.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuproductionplanning.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (current_prod == "Y")
            {
                menucurrentproduction.Visibility = ElementVisibility.Visible;
                menuproduction.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menucurrentproduction.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (skill == "Y")
            {
                menuskills.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuskills.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (empskill == "Y")
            {
                menuemployeeskill.Visibility = ElementVisibility.Visible;
                menuskills.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuemployeeskill.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (opskill == "Y")
            {
                menuoperationskill.Visibility = ElementVisibility.Visible;
                menuskills.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuoperationskill.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (moreport == "Y")
            {
                menumoproduction.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menumoproduction.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (stn_prod_report == "Y")
            {
                menustationproduction.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menustationproduction.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (empqcreport == "Y")
            {
                menuempqcreports.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuempqcreports.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (opqcreport == "Y")
            {
                menuopqcreports.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuopqcreports.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (payrollreport == "Y")
            {
                menupayroll.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menupayroll.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (moqcreport == "Y")
            {
                menuqcmoreport.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuqcmoreport.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (stationqcreport == "Y")
            {
                menuqcstationreport.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuqcstationreport.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (machinereport == "Y")
            {
                menumachinereport.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menumachinereport.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (machineassign == "Y")
            {
                menumachineassignreport.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menumachineassignreport.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (machinerepair == "Y")
            {
                menumachinerepairreport.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menumachinerepairreport.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (topdefects == "Y")
            {
                menutopdefects.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menutopdefects.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (sparereport == "Y")
            {
                menusparereport.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menusparereport.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (spareinventory == "Y")
            {
                menuspareinventory.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menuspareinventory.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (designsequence == "Y")
            {
                menudesignsequence.Visibility = ElementVisibility.Visible;
                menumasters.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menudesignsequence.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (stationwip == "Y")
            {
                menubalancehangers.Visibility = ElementVisibility.Visible;
                menuproduction.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menubalancehangers.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (linebalancing == "Y")
            {
                menulinebalancing.Visibility = ElementVisibility.Visible;
                menuproduction.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menulinebalancing.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (performancereport == "Y")
            {
                menudailyproduction.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menudailyproduction.Visibility = ElementVisibility.Collapsed;
            }

            //check if the menu is enabled
            if (moopreport == "Y")
            {
                menumoopreport.Visibility = ElementVisibility.Visible;
                menureports.Visibility = ElementVisibility.Visible;
            }
            else
            {
                menumoopreport.Visibility = ElementVisibility.Collapsed;
            }

            //check if basic version of pms client is enabled
            if (Database_Connection.SET_PMSCIENT == "1")
            {
                radMenuSeparatorItem2.Visibility = ElementVisibility.Collapsed;
                radMenuSeparatorItem3.Visibility = ElementVisibility.Collapsed;
                radMenuSeparatorItem4.Visibility = ElementVisibility.Collapsed;
                radMenuSeparatorItem5.Visibility = ElementVisibility.Collapsed;
                radMenuSeparatorItem6.Visibility = ElementVisibility.Collapsed;
                radMenuSeparatorItem7.Visibility = ElementVisibility.Collapsed;
            }

            Refresh_Controller();    //refresh the controller
            HideUserMenu();   //the special field menu
        }

        public void Refresh_Controller()
        {
            //get all the clusters
            SqlDataAdapter sda = new SqlDataAdapter("Select V_CLUSTER_ID from CLUSTER_DB", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            cmbcontroller.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbcontroller.Items.Add(dt.Rows[i][0].ToString());
                cmbcontroller.SelectedIndex = 0;
            }

            //get the controller
            sda = new SqlDataAdapter("Select V_CONTROLLER from Setup", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbcontroller.Text = dt.Rows[i][0].ToString();
            }

            select_controller();  //get the selected controller
            piccontroller.Image = null;
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
        }

        public void HideUserMenu()
        {
            int i = 0;

            //get the special field name
            SqlCommand cmd = new SqlCommand("SELECT V_ENABLED,V_USER FROM USER_COLUMN_NAMES where V_MRT='USER_DEF1'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetValue(0).ToString() == "FALSE")
                {
                    menuuser1.Visibility = ElementVisibility.Collapsed;
                    ++i;
                }
                menuuser1.Text = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //get the special field name
            cmd = new SqlCommand("SELECT V_ENABLED,V_USER FROM USER_COLUMN_NAMES where V_MRT='USER_DEF2'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetValue(0).ToString() == "FALSE")
                {
                    menuuser2.Visibility = ElementVisibility.Collapsed;
                    ++i;
                }
                menuuser2.Text = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //get the special field name
            cmd = new SqlCommand("SELECT V_ENABLED,V_USER FROM USER_COLUMN_NAMES where V_MRT='USER_DEF3'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetValue(0).ToString() == "FALSE")
                {
                    menuuser3.Visibility = ElementVisibility.Collapsed;
                    ++i;
                }
                menuuser3.Text = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //get the special field name
            cmd = new SqlCommand("SELECT V_ENABLED,V_USER FROM USER_COLUMN_NAMES where V_MRT='USER_DEF4'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetValue(0).ToString() == "FALSE")
                {
                    menuuser4.Visibility = ElementVisibility.Collapsed;
                    ++i;
                }
                menuuser4.Text = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //get the special field name
            cmd = new SqlCommand("SELECT V_ENABLED,V_USER FROM USER_COLUMN_NAMES where V_MRT='USER_DEF5'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetValue(0).ToString() == "FALSE")
                {
                    menuuser5.Visibility = ElementVisibility.Collapsed;
                    ++i;
                }
                menuuser5.Text = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //get the special field name
            cmd = new SqlCommand("SELECT V_ENABLED,V_USER FROM USER_COLUMN_NAMES where V_MRT='USER_DEF6'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetValue(0).ToString() == "FALSE")
                {
                    menuuser6.Visibility = ElementVisibility.Collapsed;
                    ++i;
                }
                menuuser6.Text = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //get the special field name
            cmd = new SqlCommand("SELECT V_ENABLED,V_USER FROM USER_COLUMN_NAMES where V_MRT='USER_DEF7'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetValue(0).ToString() == "FALSE")
                {
                    menuuser7.Visibility = ElementVisibility.Collapsed;
                    ++i;
                }
                menuuser7.Text = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //get the special field name
            cmd = new SqlCommand("SELECT V_ENABLED,V_USER FROM USER_COLUMN_NAMES where V_MRT='USER_DEF8'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetValue(0).ToString() == "FALSE")
                {
                    menuuser8.Visibility = ElementVisibility.Collapsed;
                    ++i;
                }
                menuuser8.Text = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //get the special field name
            cmd = new SqlCommand("SELECT V_ENABLED,V_USER FROM USER_COLUMN_NAMES where V_MRT='USER_DEF9'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetValue(0).ToString() == "FALSE")
                {
                    menuuser9.Visibility = ElementVisibility.Collapsed;
                    ++i;
                }
                menuuser9.Text = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //get the special field name
            cmd = new SqlCommand("SELECT V_ENABLED,V_USER FROM USER_COLUMN_NAMES where V_MRT='USER_DEF10'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                if (sdr.GetValue(0).ToString() == "FALSE")
                {
                    menuuser10.Visibility = ElementVisibility.Collapsed;
                    ++i;
                }
                menuuser10.Text = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //check if any of the special field is enabled
            if (i == 10)
            {
                menuspecial.Visibility = ElementVisibility.Collapsed;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //thread to start the pms server backup
            Thread clock = new Thread(BackUp_Clock);
            clock.Start();

            String temp = EmergencyStop();
            String[] emergency = temp.Split(',');
            if (emergency[0] == "1")
            {
                //DebugLog("Emergency Stop. Line : " + emergency[1] + " is Stopped.");
                this.radDesktopAlert1.ContentImage = emer;
                this.radDesktopAlert1.Popup.AlertElement.CaptionElement.TextAndButtonsElement.TextElement.Font = new Font("Microsoft Sans Serif", 22f);
                this.radDesktopAlert1.CaptionText = "Emergency Stop";
                this.radDesktopAlert1.ContentText = "Line : " + emergency[1] + " is Stopped.";
                this.radDesktopAlert1.Popup.AlertElement.ContentElement.Font = new Font("Microsoft Sans Serif", 15f);
                this.radDesktopAlert1.Popup.AlertElement.BorderColor = Color.Red;
                this.radDesktopAlert1.Show();
            }
        }

        public void BackUp_Clock()
        {
            try
            {
                //check if the backup is already taking place
                if (shift_flag == 1)
                {
                    return;
                }

                shift_flag = 1;
                DateTime current_time1 = Convert.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));
                String current_time = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");

                lbltime.Text = current_time;
                DateTime shift_start = Convert.ToDateTime("9:30:00");
                DateTime shift_end = Convert.ToDateTime("18:30:00");
                DateTime overtime_end = Convert.ToDateTime("19:30:00");
                String shift = "";

                //get the shift details
                SqlDataAdapter sda = new SqlDataAdapter("SELECT T.T_SHIFT_START_TIME,T.T_SHIFT_END_TIME,T.T_OVERTIME_END_TIME,T.V_SHIFT FROM SHIFTS T WHERE CAST(GETDATE() AS TIME) BETWEEN cast(T.T_SHIFT_START_TIME as TIME) AND cast(T.T_OVERTIME_END_TIME as TIME)", dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    shift_start = Convert.ToDateTime(dt.Rows[i][0].ToString());
                    shift_end = Convert.ToDateTime(dt.Rows[i][1].ToString());
                    overtime_end = Convert.ToDateTime(dt.Rows[i][2].ToString());
                    shift = dt.Rows[i][3].ToString();
                }

                //check if the backup timer is enabled
                String backup_enable = "";
                sda = new SqlDataAdapter("SELECT BACKUP_ENABLE FROM Setup", dc.con);
                dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    backup_enable = dt.Rows[i][0].ToString();
                }

                //check if the backup timer is enabled
                if (backup_enable == "TRUE")
                {
                    lblbackup.Visibility = ElementVisibility.Collapsed;
                }
                else
                {
                    lblbackup.Text = "        Backup Timer Disabled";
                    lblbackup.Visibility = ElementVisibility.Visible;
                }

                //calculate the work duration
                TimeSpan ts_workduration = shift_end - shift_start;
                TimeSpan ts_timecompleted = current_time1 - shift_start;
                int workduration = (int)ts_workduration.TotalMinutes;
                int timecompleted = (int)ts_timecompleted.TotalMinutes;
                int timeremaining = workduration - timecompleted;

                if (timeremaining < 0)
                {
                    timeremaining = 0;
                }

                txtshift.Text = "Current Shift : " + shift + "           Time Completed : " + timecompleted + "           Time Remaining : " + timeremaining;
                
                //check id overtime is running
                if (current_time1 >= shift_end && current_time1 <= overtime_end)
                {
                    txtnormal.Text = "        OverTime";
                }
                else
                {
                    txtnormal.Text = "";
                }               

                shift_flag = 0;
            }
            catch (Exception ex)
            {
                shift_flag = 0;

                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                }
                else
                {
                    radLabel5.Text = ex.Message;
                }                
            }
        }

        private void Home_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();   //open connection
            String Lang = "";
            radLabel5.Text = "";

            //get the language
            SqlCommand cmd = new SqlCommand("SELECT Language FROM Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Lang = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //change the form language
            SqlDataAdapter sda = new SqlDataAdapter("select " + Lang + " from Language where Form='Home' order by Item_No", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                menusetup.Text = dt.Rows[0][0].ToString();
                menulogout.Text = dt.Rows[1][0].ToString();
                menuexit.Text = dt.Rows[2][0].ToString();
                menuhome.Text = dt.Rows[3][0].ToString();
                menumasters.Text = dt.Rows[4][0].ToString();
                menucolormaster.Text = dt.Rows[5][0].ToString();
                menuarticlemaster.Text = dt.Rows[6][0].ToString();
                menucustomermaster.Text = dt.Rows[7][0].ToString();
                menucontractormaster.Text = dt.Rows[8][0].ToString();
                menuempmaster.Text = dt.Rows[10][0].ToString();
                menuemp.Text = dt.Rows[10][0].ToString();
                menunewmo.Text = dt.Rows[11][0].ToString();
                menuopenmo.Text = dt.Rows[12][0].ToString();
                menumo.Text = dt.Rows[13][0].ToString();
                menusizemaster.Text = dt.Rows[14][0].ToString();
                menuoperationmaster.Text = dt.Rows[15][0].ToString();
                menuspecial.Text = dt.Rows[16][0].ToString();
                menuproduction.Text = dt.Rows[17][0].ToString();
                menustationassign.Text = dt.Rows[18][0].ToString();
                menuhelp.Text = dt.Rows[19][0].ToString();
                radMenuItem32.Text = dt.Rows[20][0].ToString();
                menuaddproduction.Text = dt.Rows[21][0].ToString();
                menureports.Text = dt.Rows[22][0].ToString();
                menuempreports.Text = dt.Rows[23][0].ToString();
                menustationreport.Text = dt.Rows[24][0].ToString();
                btnstartline.Text = dt.Rows[25][0].ToString();
                btnstopline.Text = dt.Rows[26][0].ToString();
                lblprodline.Text = dt.Rows[27][0].ToString();
                lblheadline.Text = dt.Rows[28][0].ToString();
                menuqcmain.Text = dt.Rows[29][0].ToString();
                menuqcsub.Text = dt.Rows[30][0].ToString();
                menubuffer.Text = dt.Rows[31][0].ToString();
                menurestoreproduction.Text = dt.Rows[32][0].ToString();
            }
            sda.Dispose();

            //thread to refresh controller
            Thread thr_Controller = new Thread(ControllerRefresh);
            thr_Controller.Start();
        }

        public void ControllerRefresh()
        {
            try
            {
                Thread.Sleep(2000);

                //check if funtion is running on diffrent thread other than the main thread
                if (cchkprodline.InvokeRequired)
                {
                    cchkprodline.Invoke((Action)(() => cchkprodline.Items.Clear()));
                }
                else
                {
                    cchkprodline.Items.Clear();
                }

                //get all the prod line
                SqlDataAdapter sda = new SqlDataAdapter("select V_PROD_LINE from PROD_LINE_DB where V_CONTROLLER_ENABLED='TRUE'", dc.con);
                DataTable dt1 = new DataTable();
                sda.Fill(dt1);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    //check if funtion is running on diffrent thread other than the main thread
                    if (cchkprodline.InvokeRequired)
                    {
                        cchkprodline.Invoke((Action)(() => cchkprodline.Items.Add(dt1.Rows[i][0].ToString())));
                    }
                    else
                    {
                        cchkprodline.Items.Add(dt1.Rows[i][0].ToString());
                    }
                }
                sda.Dispose();

                for (int i = 0; i < cchkprodline.Items.Count; i++)
                {
                    String ipaddress = "";
                    String port = "";
                    String controller = "";
                    String cluster = "--SELECT--";

                    //get the ipaddress and port
                    sda = new SqlDataAdapter("select V_IP_ADDRESS,I_PORT,V_PROD_LINE from PROD_LINE_DB where V_PROD_LINE='" + cchkprodline.Items[i].ToString() + "'", dc.con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        ipaddress = dt.Rows[j][0].ToString();
                        port = dt.Rows[j][1].ToString();
                        controller = dt.Rows[j][2].ToString();
                    }

                    //get cluster ipaddress
                    sda = new SqlDataAdapter("select V_CLUSTER_ID from CLUSTER_DB", dc.con);
                    dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        cluster = dt.Rows[j][0].ToString();
                    }

                    //check if headline is running
                    if (LineStatus(ipaddress, port) == "TRUE")
                    {
                        //check if funtion is running on diffrent thread other than the main thread
                        if (cchkprodline.InvokeRequired)
                        {
                            cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Image = img));
                            cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight));
                            cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Checked = true));
                        }
                        else
                        {
                            cchkprodline.Items[i].Image = img;
                            cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight;
                            cchkprodline.Items[i].Checked = true;
                        }
                        continue;
                    }

                    //check if headline is not running
                    if (LineStatus(ipaddress, port) == "FALSE")
                    {
                        //check if funtion is running on diffrent thread other than the main thread
                        if (cchkprodline.InvokeRequired)
                        {
                            cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Image = off));
                            cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight));
                        }
                        else
                        {
                            cchkprodline.Items[i].Image = off;
                            cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight;
                        }
                        continue;
                    }

                    //check if funtion is running on diffrent thread other than the main thread
                    if (cchkprodline.InvokeRequired)
                    {
                        cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Image = offline));
                        cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight));
                    }
                    else
                    {
                        cchkprodline.Items[i].Image = offline;
                        cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight;
                    }

                    //check if funtion is running on diffrent thread other than the main thread
                    if (radLabel5.InvokeRequired)
                    {
                        radLabel5.Invoke((Action)(() => radLabel5.Text = "Line " + controller + " Controller is not Connected"));
                    }
                    else
                    {
                        radLabel5.Text = "Line " + controller + " Controller is not Connected";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        public void ControllerRefresh1()
        {
            try
            {
                Thread.Sleep(1000);
                for (int i = 0; i < cchkprodline.Items.Count; i++)
                {
                    String ipaddress = "";
                    String port = "";
                    String controller = "";
                    String cluster = "--SELECT--";

                    //get the ipaddress and port
                    SqlDataAdapter sda = new SqlDataAdapter("select V_IP_ADDRESS,I_PORT,V_PROD_LINE from PROD_LINE_DB where V_PROD_LINE='" + cchkprodline.Items[i].ToString() + "'", dc.con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        ipaddress = dt.Rows[j][0].ToString();
                        port = dt.Rows[j][1].ToString();
                        controller = dt.Rows[j][2].ToString();
                    }

                    //get cluster ipaddress
                    sda = new SqlDataAdapter("select V_CLUSTER_ID from CLUSTER_DB", dc.con);
                    dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        cluster = dt.Rows[j][0].ToString();
                    }

                    //check if headline is running
                    if (LineStatus(ipaddress, port) == "TRUE")
                    {
                        //check if funtion is running on diffrent thread other than the main thread
                        if (cchkprodline.InvokeRequired)
                        {
                            cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Image = img));
                            cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight));
                            cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Checked = true));
                        }
                        else
                        {
                            cchkprodline.Items[i].Image = img;
                            cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight;
                            cchkprodline.Items[i].Checked = true;
                        }
                        continue;
                    }

                    //check if headline is not running
                    if (LineStatus(ipaddress, port) == "FALSE")
                    {
                        //check if funtion is running on diffrent thread other than the main thread
                        if (cchkprodline.InvokeRequired)
                        {
                            cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Image = off));
                            cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight));
                        }
                        else
                        {
                            cchkprodline.Items[i].Image = off;
                            cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight;
                        }
                        continue;
                    }

                    //check if funtion is running on diffrent thread other than the main thread
                    if (cchkprodline.InvokeRequired)
                    {
                        cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Image = offline));
                        cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight));
                    }
                    else
                    {
                        cchkprodline.Items[i].Image = offline;
                        cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex + "", "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }


        private void radLabel5_TextChanged(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            radPanel2.Visible = true;
            MyTimer.Start();
        }

        System.Windows.Forms.Timer MyTimer = new System.Windows.Forms.Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            radPanel2.Visible = false;
            radLabel5.Text = "";
            MyTimer.Stop();
        }

        //http request start the headline
        private void StartLine(String IP, String PORT)
        {
            try
            {
               // DebugLog("Home.cs(StartLine),IP - " + IP + ", PORT - "  +PORT);
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/Start";
                var data = "";
                data = webPostMethod(postData, URL);
                Thread.Sleep(1000);

                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = "Head Line Started"));
                }
                else
                {
                    radLabel5.Text = "Head Line Started";
                }

                i = 1;
            }
            catch (Exception ex)
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                }
                else
                {
                    radLabel5.Text = ex.Message;
                }
            }
        }

        //http request to stop the headline
        private void StopLine(String IP, String PORT)
        {
            try
            {
               // DebugLog("Home.cs(StopLine), IP - " + IP + ", PORT - " + PORT);
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/Stop";
                var data = "";
                data = webPostMethod(postData, URL);
                Thread.Sleep(1000);

                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = "Head Line Stopped"));
                }
                else
                {
                    radLabel5.Text = "Head Line Stopped";
                }

                i = 0;
            }
            catch (Exception ex)
            {
                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = ex.Message));
                }
                else
                {
                    radLabel5.Text = ex.Message;
                }
            }
        }

        //http request to get headline status
        private String LineStatus(String IP, String PORT)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":" + PORT + "/Status";
                var data = "";
                data = webPostMethod(postData, URL);
                Thread.Sleep(1000);

                if (data.Contains("\"Motor_On\":false"))
                {
                    return ("FALSE");
                }
                if (data.Contains("\"Motor_On\":true"))
                {
                    return ("TRUE");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ("");
        }

        private String EmergencyStop()
        {
            String data = "";
            try
            {
                string postData = "";
                string URL = "http://" + Database_Connection.GET_SERVER_IP + ":8091/EmergencyStop/";
                data = webGetMethod(postData, URL);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return (data);
        }

        private String EmergencyFlag()
        {
            radDesktopAlert1.Hide();
            String data = "";
            try
            {
                string postData = "";
                string URL = "http://" + Database_Connection.GET_SERVER_IP + ":8091/EmergencyFlag/";
                data = webGetMethod(postData, URL);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return (data);
        }

        //http post method
        public String webPostMethod(String postData, String URL)
        {
            try
            {
                String responseFromServer = "";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "POST";
                request.Timeout = 500;
                request.Credentials = CredentialCache.DefaultCredentials;

                ((HttpWebRequest)request).UserAgent ="Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 7.1; Trident/5.0)";
                request.Accept = "/";
                request.UseDefaultCredentials = true;
                request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        //open add mo
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Add_MO am = new Add_MO();
            am.MdiParent = this;
            am.Show();
        }

        //open open mo
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Open_MO om = new Open_MO();
            om.MdiParent = this;
            om.Show();
        }

        //open setup
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //check if super is loggeg in
            if (lblusergroup.Text != "Super User")
            {
                if (Key == "")
                {
                    Key = Inputbox();
                }

                //get the login password
                if (checkpassword(Key) == "False")
                {
                    Key = "";
                    radLabel5.Text = "Wrong Password or Paswword Expired.";
                    return;
                }
            }

            //open setup
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Setup st = new Setup();
            st.lbluser.Text = lblusergroup.Text;

            st.MdiParent = this;
            st.Show();
            st.Form_Location("Home");
        }

        private void radCheckedDropDownList1_Click(object sender, EventArgs e)
        {

        }

        //open station assign
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Station_Assign sa = new Station_Assign();
            sa.MdiParent = this;
            sa.Show();
        }

        private void radButton1_Click(object sender, EventArgs e)
        {
            //thread to start the headline
           // DebugLog("Home.cs(radButton1_Click), StartLines thread started.");
            Thread startline = new Thread(StartLines);
            startline.Start();
        }

        public void StartLines()
        {
            try
            {
                String ipaddress = "";
                String port = "";
                String controller = "";

                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = "Command sent to Start Headlines"));
                }
                else
                {
                    radLabel5.Text = "Command sent to Start Headlines";
                }

                for (int i = 0; i < cchkprodline.Items.Count; i++)
                {
                    if (cchkprodline.Items[i].Checked == true)
                    {
                        //get the ipaddress and port
                        SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS,I_PORT,V_CONTROLLER from PROD_LINE_DB where V_PROD_LINE='" + cchkprodline.Items[i].ToString() + "' order by V_CONTROLLER", dc.con);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            ipaddress = sdr.GetValue(0).ToString();
                            port = sdr.GetValue(1).ToString();
                            controller = sdr.GetValue(2).ToString();
                        }
                        sdr.Close();

                        //check the headline status
                        if (LineStatus(ipaddress, port) == "TRUE")
                        {
                            //check if funtion is running on diffrent thread other than the main thread
                            if (cchkprodline.InvokeRequired)
                            {
                                cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Image = img));
                                cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight));
                                cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Checked = true));
                            }
                            else
                            {
                                cchkprodline.Items[i].Image = img;
                                cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight;
                                cchkprodline.Items[i].Checked = true;
                            }
                            continue;
                        }

                        //check the headline status
                        if (LineStatus(ipaddress, port) == "FALSE")
                        {
                            //check if funtion is running on diffrent thread other than the main thread
                            StartLine(ipaddress, port);
                            if (cchkprodline.InvokeRequired)
                            {
                                cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Image = img));
                                cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight));
                            }
                            else
                            {
                                cchkprodline.Items[i].Image = img;
                                cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight;
                            }
                            continue;
                        }

                        //check if funtion is running on diffrent thread other than the main thread
                        if (cchkprodline.InvokeRequired)
                        {
                            cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Image = offline));
                            cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight));
                        }
                        else
                        {
                            cchkprodline.Items[i].Image = offline;
                            cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                RadMessageBox.Show(ex + "");
            }
        }

        private void radButton2_Click(object sender, EventArgs e)
        {            
            //confirm box to shut down headline
            DialogResult result = RadMessageBox.Show("Are you sure to Shutdown the Lines?", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
            if (result.Equals(DialogResult.Yes))
            {
              //  DebugLog("Home.cs(radButton2_Click), StopLines thread started.");
                //thread to stop headline
                Thread stopline = new Thread(StopLines);
                stopline.Start();
            }
        }

        public void StopLines()
        {
            try
            {
                String ipaddress = "";
                String port = "";
                String controller = "";

                //check if funtion is running on diffrent thread other than the main thread
                if (radLabel5.InvokeRequired)
                {
                    radLabel5.Invoke((Action)(() => radLabel5.Text = "Command sent to Stop Headlines"));
                }
                else
                {
                    radLabel5.Text = "Command sent to Stop Headlines";
                }

                for (int i = 0; i < cchkprodline.Items.Count; i++)
                {
                    if (cchkprodline.Items[i].Checked == true)
                    {
                        String previous = controller;
                        //get the ipaddress and port
                        SqlCommand cmd = new SqlCommand("select V_IP_ADDRESS,I_PORT,V_CONTROLLER from PROD_LINE_DB where V_PROD_LINE='" + cchkprodline.Items[i].ToString() + "' order by V_CONTROLLER", dc.con);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            ipaddress = sdr.GetValue(0).ToString();
                            port = sdr.GetValue(1).ToString();
                            controller = sdr.GetValue(2).ToString();
                        }
                        sdr.Close();

                        if (previous != controller && previous != "")
                        {
                            Thread.Sleep(2000);
                        }

                        //get the headline status
                        if (LineStatus(ipaddress, port) == "TRUE")
                        {
                            //stop the headline
                            StopLine(ipaddress, port);
                            //check if funtion is running on diffrent thread other than the main thread
                            if (cchkprodline.InvokeRequired)
                            {
                                cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Image = off));
                                cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight));
                            }
                            else
                            {
                                cchkprodline.Items[i].Image = off;
                                cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight;
                            }
                            continue;
                        }

                        //get the status of the headline
                        if (LineStatus(ipaddress, port) == "FALSE")
                        {
                            //check if funtion is running on diffrent thread other than the main thread
                            if (cchkprodline.InvokeRequired)
                            {
                                cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Image = off));
                                cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight));
                            }
                            else
                            {
                                cchkprodline.Items[i].Image = off;
                                cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight;
                            }
                            continue;
                        }

                        //check if funtion is running on diffrent thread other than the main thread
                        if (cchkprodline.InvokeRequired)
                        {
                            cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].Image = offline));
                            cchkprodline.Invoke((Action)(() => cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight));
                        }
                        else
                        {
                            cchkprodline.Items[i].Image = offline;
                            cchkprodline.Items[i].ImageAlignment = ContentAlignment.TopRight;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex + "");
            }
        }

        private void radMenuItem8_Click_1(object sender, EventArgs e)
        {
            //check if the super user is logged in
            if (lblusergroup.Text != "Super User")
            {
                if (Key == "")
                {
                    Key = Inputbox();
                }

                //get the login password
                if (checkpassword(Key) == "False")
                {
                    Key = "";
                    radLabel5.Text = "Wrong Password or Paswword Expired.";
                    return;               
                }
            }

            //open setup
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Setup st = new Setup();
            st.MdiParent = this;
            st.Show();
            st.Form_Location("Home");
        }

        //input box to get the login password
        public String Inputbox()
        {
            string message, title;
            String defaultValue = "";
            string myValue;

            // Set prompt.
            message = "Enter Password";
            // Set title.
            title = "SmartMRT";

            // Set default value.//Display message, title, and default value.
            myValue = Interaction.InputBox(message, title, defaultValue, 100, 100);// If user has clicked Cancel, set myValue to defaultValue
            if (myValue != "")
            {               
                return (myValue);
            }

            return "";
        }

        public String checkpassword(String key)
        {
            //get the current date time
            String date = DateTime.Now.ToString("MMddyyyyhhtt");
            String pass = "";

            if (key.Length == 24)
            {
                try
                {
                    //decrypt the password
                    pass = Decrypt(key, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //check if the password match the time
            if (date == pass)
            {
                return "True";
            }
            else
            {
                return "False";
            }
        }

        //triple des algorithm decrypt
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

        //exit the application
        private void radMenuItem10_Click_1(object sender, EventArgs e)
        {
          //  DebugLog("radMenuItem10_Click_1 - closed by Exit btn");
            Application.Exit();
            
        }

        //open masters
        private void radMenuItem11_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Color");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem12_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Article");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem13_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Size");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem14_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Customer");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem16_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Operation");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem17_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Employee");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem18_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Contractor");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem19_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("User1");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem20_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("User2");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem21_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("User3");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem22_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("User4");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem23_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("User5");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem24_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("User6");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem25_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("User7");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem26_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("User8");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem27_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("User9");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void radMenuItem28_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("User10");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open add mo
        private void radMenuItem29_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Add_MO am = new Add_MO();
            am.MdiParent = this;
            am.Show();
        }

        //open open mo
        private void radMenuItem30_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Open_MO om = new Open_MO();
            om.MdiParent = this;
            om.Show();
        }

        //open station assign
        private void radMenuItem31_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Station_Assign sa = new Station_Assign();
            sa.MdiParent = this;
            sa.Show();
        }

        //open about
        private void radMenuItem32_Click_1(object sender, EventArgs e)
        {
            About sa = new About();
            sa.Show();
        }

        //hide the special fields
        private void radMenuItem4_Click_1(object sender, EventArgs e)
        {
            HideUserMenu();
        }

        //open employee report
        private void radMenuItem34_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Employee_Inspection ei = new Employee_Inspection();
            ei.MdiParent = this;
            ei.Show();
        }

        //open station report
        private void radMenuItem35_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Station_Status ss = new Station_Status();
            ss.MdiParent = this;
            ss.Show();
        }

        //open add to production
        private void radMenuItem36_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Add_Production ap = new Add_Production();
            ap.MdiParent = this;
            ap.Show();
        }

        //open add to production
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Add_Production ap = new Add_Production();
            ap.MdiParent = this;
            ap.Show();
        }

        //open employee report
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Employee_Inspection ei = new Employee_Inspection();
            ei.MdiParent = this;
            ei.Show();
        }

        //open station production report
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Station_Production ss = new Station_Production();
            ss.MdiParent = this;
            ss.Show();
        }

        private void radPanel1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            //thread to refresh controller
            Thread thr_Controller = new Thread(ControllerRefresh);
            thr_Controller.Start();
            led_flag = 0;
        }

        //open smartmrt website
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("www.smartmrt.com");
        }

        //logout user
        private void menulogout_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        //open buffer
        private void menubuffer_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Buffer ss = new Buffer();
            ss.MdiParent = this;
            ss.Show();
        }

        //open restore production
        private void menurestoreproduction_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Restore_Production ss = new Restore_Production();
            ss.MdiParent = this;
            ss.Show();
        }

        private void tmrproduction_Tick(object sender, EventArgs e)
        {
            dgvproduction.Visible = true;
            if (production_flag == 0)
            {
                dgvproduction.Rows.Clear();

                //get the prod line
                SqlDataAdapter sda = new SqlDataAdapter("select distinct V_PROD_LINE from PROD_LINE_DB where V_CONTROLLER_ENABLED='TRUE' Order by V_PROD_LINE", dc.con);
                DataTable dt1 = new DataTable();
                sda.Fill(dt1);
                sda.Dispose();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    dgvproduction.Rows.Add(dt1.Rows[i][0].ToString(), "0", "0", "0", "0");
                }

                dgvproduction.Rows.Add("Total", "0", "0", "0", "0");
            }

            production_flag = 1;

            //thread to get the production details
            Thread Th_Prod = new Thread(ProductionThread);
            Th_Prod.Start();
        }

        public void ProductionThread()
        {
            try
            {
                String[] load = Total_Load();   //get the production details  for loading
                String[] unload = Total_Unload();   //get the producion details for unloading
                String[] wip = Total_WIP();   //get the station wip

                for (int i = 0; i < load.Length; i++)
                {
                    String load1 = load[i];
                    String unload1 = unload[i];
                    String wip1 = wip[i];

                    if (wip1.Contains("-") && unload1.Contains("-") && load1.Contains("-"))
                    {
                        String[] load2 = load1.Split('-');
                        String[] unload2 = unload1.Split('-');
                        String[] wip2 = wip1.Split('-');

                        int wip_1 = int.Parse(load2[1]) - int.Parse(unload2[1]);
                        if (wip_1 < 0)
                        {
                            wip_1 = 0;
                        }

                        //check if funtion is running on diffrent thread other than the main thread
                        if (dgvproduction.InvokeRequired)
                        {
                            dgvproduction.Invoke((Action)(() => dgvproduction.Rows[i].Cells[1].Value = load2[1]));
                            dgvproduction.Invoke((Action)(() => dgvproduction.Rows[i].Cells[2].Value = wip_1));
                            dgvproduction.Invoke((Action)(() => dgvproduction.Rows[i].Cells[3].Value = unload2[1]));
                            dgvproduction.Invoke((Action)(() => dgvproduction.Rows[i].Cells[4].Value = wip2[1]));
                        }
                        else
                        {
                            dgvproduction.Rows[i].Cells[1].Value = load2[1];
                            dgvproduction.Rows[i].Cells[2].Value = wip_1;
                            dgvproduction.Rows[i].Cells[3].Value = unload2[1];
                            dgvproduction.Rows[i].Cells[4].Value = wip2[1];
                        }
                    }
                }

                int total_load = 0;
                int total_unload = 0;
                int total_wip = 0;
                int total_wip1 = 0;

                //get totals
                for (int i = 0; i < dgvproduction.Rows.Count - 1; i++)
                {
                    total_load = total_load + int.Parse(dgvproduction.Rows[i].Cells[1].Value.ToString() + "");
                    total_unload = total_unload + int.Parse(dgvproduction.Rows[i].Cells[2].Value.ToString() + "");
                    total_wip1 = total_wip1 + int.Parse(dgvproduction.Rows[i].Cells[3].Value.ToString() + "");
                    total_wip = total_wip + int.Parse(dgvproduction.Rows[i].Cells[4].Value.ToString() + "");
                }

                //check if funtion is running on diffrent thread other than the main thread
                if (dgvproduction.InvokeRequired)
                {
                    dgvproduction.Invoke((Action)(() => dgvproduction.Rows[dgvproduction.Rows.Count - 1].Cells[1].Value = total_load));
                    dgvproduction.Invoke((Action)(() => dgvproduction.Rows[dgvproduction.Rows.Count - 1].Cells[2].Value = total_unload));
                    dgvproduction.Invoke((Action)(() => dgvproduction.Rows[dgvproduction.Rows.Count - 1].Cells[3].Value = total_wip1));
                    dgvproduction.Invoke((Action)(() => dgvproduction.Rows[dgvproduction.Rows.Count - 1].Cells[4].Value = total_wip));
                }
                else
                {
                    dgvproduction.Rows[dgvproduction.Rows.Count - 1].Cells[1].Value = total_load;
                    dgvproduction.Rows[dgvproduction.Rows.Count - 1].Cells[2].Value = total_unload;
                    dgvproduction.Rows[dgvproduction.Rows.Count - 1].Cells[3].Value = total_wip1;
                    dgvproduction.Rows[dgvproduction.Rows.Count - 1].Cells[4].Value = total_wip;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public String[] Total_Load()
        {
            try
            {
                //get the prod line
                SqlDataAdapter sda1 = new SqlDataAdapter("select distinct V_PROD_LINE from PROD_LINE_DB where V_CONTROLLER_ENABLED='TRUE' Order by V_PROD_LINE", dc.con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                sda1.Dispose();

                String prodline = "";
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    prodline = prodline + dt1.Rows[i][0].ToString() + "-0,";
                }

                if (prodline.Length > 0)
                {
                    prodline = prodline.Remove(prodline.Length - 1, 1);
                }

                //get the cluster ip address
                String[] prod = prodline.Split(',');
                sda1 = new SqlDataAdapter("select distinct C.V_CLUSTER_IP_ADDRESS from PROD_LINE_DB P, CLUSTER_DB C where P.V_CLUSTER_DB=C.V_CLUSTER_ID", dc.con);
                dt1 = new DataTable();
                sda1.Fill(dt1);
                sda1.Dispose();
                for (int p = 0; p < dt1.Rows.Count; p++)
                {
                    String status = OpenMYSQLConnection(dt1.Rows[i][0].ToString());   //open connection
                    if (status == "UNABLE")
                    {
                        tmrproduction.Enabled = false;

                        //update the setup
                        SqlCommand cmd = new SqlCommand("update Setup set V_CONTROLLER = '--SELECT--'", dc.con);
                        cmd.ExecuteNonQuery();

                        cmbcontroller.Items.Clear();
                        cmbcontroller.Text = "--SELECT--";
                        break;
                    }

                    //get the production details for each line
                    MySqlDataAdapter sda = new MySqlDataAdapter("select sd.INFEED_LINENO,SUM(sh.PC_COUNT),sh.MO_NO,sh.MO_LINE from stationhistory sh,stationdata sd where sh.REMARKS='1' and sh.TIME>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and sh.STN_ID=sd.STN_ID group by sd.INFEED_LINENO,sh.MO_NO,sh.MO_LINE", conn1);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String stnid = dt.Rows[i][0].ToString();
                        int count = int.Parse(dt.Rows[i][1].ToString());
                        String mo = dt.Rows[i][2].ToString();
                        String moline = dt.Rows[i][3].ToString();

                        for (int k = 0; k < prod.Length; k++)
                        {
                            if (prod[k].Contains("-"))
                            {
                                String[] load = prod[k].Split('-');

                                if (stnid == load[0])
                                {
                                    int load1 = int.Parse(load[1]);
                                    load1 = load1 + count;
                                    prod[k] = load[0] + "-" + load1;
                                }
                            }
                        }
                    }
                    Close_Connection();
                }

                return (prod);
            }
            catch (Exception ex)
            {
                radLabel5.Text = "Total Loading : " + ex.Message;
                tmrproduction.Stop();
                tmrproduction.Enabled = false;
            }

            String[] ex1 = { "" };

            return ex1;
        }

        public String[] Total_Unload()
        {
            try
            {
                //get the prod line
                SqlDataAdapter sda1 = new SqlDataAdapter("select distinct V_PROD_LINE from PROD_LINE_DB where V_CONTROLLER_ENABLED='TRUE'  Order by V_PROD_LINE", dc.con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                sda1.Dispose();
                String prodline = "";
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    prodline = prodline + dt1.Rows[i][0].ToString() + "-0,";
                }

                if (prodline.Length > 0)
                {
                    prodline = prodline.Remove(prodline.Length - 1, 1);
                }

                String[] prod = prodline.Split(',');

                //get the cluster ip address
                sda1 = new SqlDataAdapter("select distinct C.V_CLUSTER_IP_ADDRESS from PROD_LINE_DB P, CLUSTER_DB C where P.V_CLUSTER_DB=C.V_CLUSTER_ID", dc.con);
                dt1 = new DataTable();
                sda1.Fill(dt1);
                sda1.Dispose();
                for (int p = 0; p < dt1.Rows.Count; p++)
                {
                    String status = OpenMYSQLConnection(dt1.Rows[i][0].ToString());  //open connection

                    if (status == "UNABLE")
                    {
                        tmrproduction.Enabled = false;
                        SqlCommand cmd = new SqlCommand("update Setup set V_CONTROLLER = '--SELECT--'", dc.con);
                        cmd.ExecuteNonQuery();
                        cmbcontroller.Items.Clear();
                        cmbcontroller.Text = "--SELECT--";
                        break;
                    }

                    //get the production details for each line
                    MySqlDataAdapter sda = new MySqlDataAdapter("select sd.INFEED_LINENO,sum(sh.PC_COUNT),sh.MO_NO,sh.MO_LINE from stationhistory sh,stationdata sd where sh.REMARKS='2' and sh.TIME>='" + DateTime.Now.ToString("yyyy-MM-dd") + "' and sh.STN_ID=sd.STN_ID group by sd.INFEED_LINENO,sh.MO_NO,sh.MO_LINE", conn1);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String stnid = dt.Rows[i][0].ToString();
                        int count = int.Parse(dt.Rows[i][1].ToString());
                        String mo = dt.Rows[i][2].ToString();
                        String moline = dt.Rows[i][3].ToString();
                        
                        for (int k = 0; k < prod.Length; k++)
                        {
                            if (prod[k].Contains("-"))
                            {
                                String[] load = prod[k].Split('-');

                                if (stnid == load[0])
                                {
                                    int load1 = int.Parse(load[1]);
                                    load1 = load1 + count;
                                    prod[k] = load[0] + "-" + load1;
                                }
                            }
                        }
                    }

                    Close_Connection();
                }

                return (prod);
            }
            catch (Exception ex)
            {
                radLabel5.Text = "Total Unloading : " + ex.Message;
                tmrproduction.Stop();
                tmrproduction.Enabled = false;
            }

            String[] ex1 = { "" };

            return ex1;
        }

        public String[] Total_WIP()
        {
            try
            {
                //get all the line
                SqlDataAdapter sda1 = new SqlDataAdapter("select distinct V_PROD_LINE from PROD_LINE_DB where V_CONTROLLER_ENABLED='TRUE' Order by V_PROD_LINE", dc.con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                sda1.Dispose();
                String prodline = "";

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    prodline = prodline + dt1.Rows[i][0].ToString() + "-0,";
                }

                if (prodline.Length > 0)
                {
                    prodline = prodline.Remove(prodline.Length - 1, 1);
                }

                String[] prod = prodline.Split(',');

                //get the cluster ipaddress
                sda1 = new SqlDataAdapter("select distinct C.V_CLUSTER_IP_ADDRESS from PROD_LINE_DB P, CLUSTER_DB C where P.V_CLUSTER_DB=C.V_CLUSTER_ID", dc.con);
                dt1 = new DataTable();
                sda1.Fill(dt1);
                sda1.Dispose();
                for (int p = 0; p < dt1.Rows.Count; p++)
                {
                    String status = OpenMYSQLConnection(dt1.Rows[i][0].ToString());   //op connection

                    if (status == "UNABLE")
                    {
                        tmrproduction.Enabled = false;
                        SqlCommand cmd = new SqlCommand("update Setup set V_CONTROLLER = '--SELECT--'", dc.con);
                        cmd.ExecuteNonQuery();

                        cmbcontroller.Items.Clear();
                        cmbcontroller.Text = "--SELECT--";
                        break;
                    }

                    //get the station wip for each line
                    MySqlDataAdapter sda = new MySqlDataAdapter("select sd.INFEED_LINENO,SUM(sh.PC_COUNT) from balancehangers sh,stationdata sd where sh.STN_ID=sd.STN_ID group by sd.INFEED_LINENO ", conn1);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        String stnid = dt.Rows[i][0].ToString();
                        int count = int.Parse(dt.Rows[i][1].ToString());

                        for (int k = 0; k < prod.Length; k++)
                        {
                            if (prod[k].Contains("-"))
                            {
                                String[] load = prod[k].Split('-');

                                if (stnid == load[0])
                                {
                                    int load1 = int.Parse(load[1]);
                                    load1 = load1 + count;
                                    prod[k] = load[0] + "-" + load1;
                                }
                            }
                        }
                    }
                    Close_Connection();
                }

                return (prod);
            }
            catch (Exception ex)
            {
                radLabel5.Text = "Total WIP : " + ex.Message;
                tmrproduction.Stop();
                tmrproduction.Enabled = false;
            }

            String[] ex1 = { "" };

            return ex1;

        }

        //open buffer
        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Buffer ss = new Buffer();
            ss.MdiParent = this;
            ss.Show();
        }

        //close connection on form close
        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            //DebugLog("Home_FormClosed() - App closed");
            EmergencyFlag();
            dc.Close_Connection();
            this.Dispose();
        }

        //open masters
        private void menuqcmain_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("QCMAIN");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void menuqcsub_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("QCSUB");
            ms.Hide_Menu(lblusergroup.Text);
        }

        private void picrefreshcontroller_Click(object sender, EventArgs e)
        {
            //thread to refresh controller
            Thread thr_Controller = new Thread(ControllerRefresh);
            thr_Controller.Start();
            Refresh_Controller();
        }

        private void cmbcontroller_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            select_controller();  //get the selected controller
        }

        public void select_controller()
        {
            String ipaddress = "";
            //String port = "";

            //get the ip address and port number of the selected controller
            SqlCommand cmd = new SqlCommand("select V_CLUSTER_IP_ADDRESS from CLUSTER_DB where V_CLUSTER_ID='" + cmbcontroller.Text + "'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                ipaddress = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            if (ipaddress == "")
            {
                return;
            }

            //connect to controller database
            dc.Close_Connection();   //close connection if open
            String status = dc.OpenMYSQLConnection(ipaddress);   //open connection
            if (status == "UNABLE")
            {
                //update setup
                cmd = new SqlCommand("update Setup set V_CONTROLLER='--SELECT--'", dc.con);
                cmd.ExecuteNonQuery();

                cmbcontroller.Items.Clear();
                cmbcontroller.Text = "--SELECT--";

                return;
            }

            //update setup
            cmd = new SqlCommand("update Setup set V_CONTROLLER='" + cmbcontroller.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }
        }

        //open mysql connection
        public String OpenMYSQLConnection(String server)
        {
            try
            {
                conn1 = new MySqlConnection("SERVER=" + server + ";" + "DATABASE=mrt_local;UID=GUI;PASSWORD=octorite!;Connection Timeout=5;");
                conn1.Open();
                return ("");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ("UNABLE");
            }
        }

        //close mysql connection
        public void Close_Connection()
        {
            try
            {
                conn1.Close();
            }
            catch (Exception ex)
            {
                radLabel5.Text = "Closing Connection : " + ex.Message;
            }
        }

        //open current production
        private void menucurrentproduction_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Current_Production ms = new Current_Production();
            ms.MdiParent = this;
            ms.Show();
        }

        //open production planning
        private void pageproductionplanning_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Production_Planning ms = new Production_Planning();
            ms.MdiParent = this;
            ms.Show();
        }

        //opem mo production report
        private void menumoproduction_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            MO_Production_Report ms = new MO_Production_Report();
            ms.MdiParent = this;
            ms.Show();
        }

        //open employee skill
        private void menuemployeeskill_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Employee_Skill ms = new Employee_Skill();
            ms.MdiParent = this;
            ms.Show();
        }

        //open operation skill
        private void menuoperationskill_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Operation_Skill ms = new Operation_Skill();
            ms.MdiParent = this;
            ms.Show();
        }

        //open masters
        private void menumachine_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Machine");
            ms.Hide_Menu(lblusergroup.Text);
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            //open controller setup
            Controller_Setup bh = new Controller_Setup();
            bh.Show();
        }

        //open station production report
        private void menustationproduction_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Station_Production ms = new Station_Production();
            ms.MdiParent = this;
            ms.Show();
        }

        //open employee qc report
        private void menuempqcreports_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            QC ms = new QC();
            ms.MdiParent = this;
            ms.Show();
        }

        //open operation qc report
        private void menuopqcreports_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            QC_Operations ms = new QC_Operations();
            ms.MdiParent = this;
            ms.Show();
        }

        //open payroll
        private void menupayroll_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Payroll ms = new Payroll();
            ms.MdiParent = this;
            ms.Show();
        }

        //open dashboard
        private void radMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Dashboard ms = new Dashboard();
            ms.MdiParent = this;
            ms.Show();
        }

        //open masters
        private void menumachinedetails_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Machine Details");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void menumbmain_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Machine Repair Main");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void menumbsub_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Machine Repair Sub");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void menuempgroupcategory_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Employee Group Category");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void menuempgroups_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Employee Groups");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void menuskill_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Employee Skill");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open current production
        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Current_Production ms = new Current_Production();
            ms.MdiParent = this;
            ms.Show();
        }

        //open employee login
        private void menuemployeelogin_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Employee_Logs ms = new Employee_Logs();
            ms.MdiParent = this;
            ms.Show();
        }

        //open mo qc report
        private void menuqcmoreport_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            QC_MO ms = new QC_MO();
            ms.MdiParent = this;
            ms.Show();
        }

        //open station qc report
        private void menuqcstationreport_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            QC_STATION ms = new QC_STATION();
            ms.MdiParent = this;
            ms.Show();
        }

        //open dashboard
        private void radButton1_Click_1(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Dashboard ms = new Dashboard();
            ms.MdiParent = this;
            ms.Show();
        }

        //open machine assign report
        private void menumachineassignreport_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Machine_Assigned_Report ms = new Machine_Assigned_Report();
            ms.MdiParent = this;
            ms.Show();
        }

        //open machine report
        private void menumachinereport_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Machine_Report ms = new Machine_Report();
            ms.MdiParent = this;
            ms.Show();
        }

        //open machine repair report
        private void menumachinerepairreport_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Machine_Repair_Report ms = new Machine_Repair_Report();
            ms.MdiParent = this;
            ms.Show();
        }

        //open top defects
        private void menutopdefects_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Top_Defects ms = new Top_Defects();
            ms.MdiParent = this;
            ms.Show();
        }

        //open station wip
        private void menubalancehangers_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Balance_Hanger ms = new Balance_Hanger();
            ms.MdiParent = this;
            ms.Show();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            try
            {
                if (led_flag == 0)
                {
                    panel5.Visible = true;
                    radListView1.Items.Clear();
                    radListView2.Items.Clear();

                    //get controller details
                    SqlDataAdapter sda = new SqlDataAdapter("select Distinct V_CONTROLLER,V_IP_ADDRESS from PROD_LINE_DB where V_CONTROLLER_ENABLED='TRUE' order by V_CONTROLLER", dc.con);
                    DataTable dt2 = new DataTable();
                    sda.Fill(dt2);
                    sda.Dispose();
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        String strController = dt2.Rows[i][0].ToString();
                        radListView1.Items.Add(strController);
                    }

                    //get the prod lines
                    sda = new SqlDataAdapter("select V_PROD_LINE from PROD_LINE_DB", dc.con);
                    DataTable dt1 = new DataTable();
                    sda.Fill(dt1);
                    sda.Dispose();
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        String strProdLine = dt1.Rows[i][0].ToString();
                        radListView2.Items.Add(strProdLine);
                    }

                    radListView3.Items.Add("PMS Server");
                    led_flag = 1;
                }

                //thread to get the line controller status
                Thread t2 = new Thread(LineStatusThread);
                t2.Start();

                //thread to get the cluster status
                Thread line_status = new Thread(Cluster_Status);
                line_status.Start();

                //thread to get pms server status
                Thread services = new Thread(Services_Thread);
                services.Start();

                Thread thr_Controller1 = new Thread(ControllerRefresh1);
                thr_Controller1.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void LineStatusThread()
        {
            try
            {
                int controller_flag = 0;

                //get the controller details
                SqlDataAdapter sda = new SqlDataAdapter("select Distinct V_CONTROLLER,V_IP_ADDRESS from PROD_LINE_DB where V_CONTROLLER_ENABLED='TRUE' order by V_CONTROLLER", dc.con);
                DataTable dt2 = new DataTable();
                sda.Fill(dt2);
                sda.Dispose();
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    //ping to the controller
                    Ping myPing = new Ping();
                    String strIP = dt2.Rows[j][1].ToString();
                    PingReply reply = myPing.Send(strIP, 1000);

                    if (reply != null)
                    {
                        //check if the controller is connected
                        if (reply.Status.ToString() == "Success")
                        {
                            //check if funtion is running on diffrent thread other than the main thread
                            if (radListView1.InvokeRequired)
                            {
                                radListView1.Invoke((Action)(() => radListView1.Items[j].Image = img));
                            }
                            else
                            {
                                radListView1.Items[j].Image = img;
                            }
                        }
                        else
                        {
                            controller_flag = 1;

                            //check if funtion is running on diffrent thread other than the main thread
                            if (radListView1.InvokeRequired)
                            {
                                radListView1.Invoke((Action)(() => radListView1.Items[j].Image = off));
                            }
                            else
                            {
                                radListView1.Items[j].Image = off;
                            }
                        }
                    }
                    else
                    {
                        controller_flag = 1;

                        //check if funtion is running on diffrent thread other than the main thread
                        if (radListView1.InvokeRequired)
                        {
                            radListView1.Invoke((Action)(() => radListView1.Items[j].Image = off));
                        }
                        else
                        {
                            radListView1.Items[j].Image = off;
                        }
                    }

                    //get the prodline for the controller
                    String strController = dt2.Rows[j][0].ToString();
                    sda = new SqlDataAdapter("select V_PROD_LINE from PROD_LINE_DB where V_CONTROLLER='" + strController + "'", dc.con);
                    DataTable dt1 = new DataTable();
                    sda.Fill(dt1);
                    sda.Dispose();
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        if (controller_flag == 0)
                        {
                            String ipaddress = "";
                            String port = "";

                            //get the ipaddress and port
                            String strProdLine = dt1.Rows[i][0].ToString();
                            sda = new SqlDataAdapter("select V_IP_ADDRESS,I_PORT from PROD_LINE_DB where V_PROD_LINE='" + strProdLine + "'", dc.con);
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            sda.Dispose();
                            for (int k = 0; k < dt.Rows.Count; k++)
                            {
                                ipaddress = dt.Rows[k][0].ToString();
                                port = dt.Rows[k][1].ToString();
                            }

                            //get the headline status
                            if (LineStatus(ipaddress, port) == "TRUE")
                            {
                                //check if funtion is running on diffrent thread other than the main thread
                                if (radListView2.InvokeRequired)
                                {
                                    radListView2.Invoke((Action)(() => radListView2.Items[i].Image = img));
                                }
                                else
                                {
                                    radListView2.Items[i].Image = img;
                                }
                                continue;
                            }

                            //get the head linestatus
                            if (LineStatus(ipaddress, port) == "FALSE")
                            {
                                //check if funtion is running on diffrent thread other than the main thread
                                if (radListView2.InvokeRequired)
                                {
                                    radListView2.Invoke((Action)(() => radListView2.Items[i].Image = img));
                                }
                                else
                                {
                                    radListView2.Items[i].Image = img;
                                }
                                continue;
                            }

                            //check if funtion is running on diffrent thread other than the main thread
                            if (radListView2.InvokeRequired)
                            {
                                radListView2.Invoke((Action)(() => radListView2.Items[i].Image = off));
                            }
                            else
                            {
                                radListView2.Items[i].Image = off;
                            }
                        }
                        else
                        {
                            //check if funtion is running on diffrent thread other than the main thread
                            if (radListView2.InvokeRequired)
                            {
                                radListView2.Invoke((Action)(() => radListView2.Items[i].Image = off));
                            }
                            else
                            {
                                radListView2.Items[i].Image = off;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Cluster_Status()
        {
            try
            {
                //get the cluster ipaddress
                SqlDataAdapter sda3 = new SqlDataAdapter("select V_CLUSTER_IP_ADDRESS from CLUSTER_DB where V_CLUSTER_ID='" + cmbcontroller.Text + "'", dc.con);
                DataTable dt2 = new DataTable();
                sda3.Fill(dt2);
                sda3.Dispose();
                for (int j = 0; j < dt2.Rows.Count; j++)
                {
                    //ping to the comtroller
                    Ping myPing = new Ping();
                    PingReply reply = myPing.Send(dt2.Rows[j][0].ToString(), 1000);

                    if (reply != null)
                    {
                        //check if the controller is connected 
                        if (reply.Status.ToString() != "Success")
                        {
                            cmbcontroller.Text = "--SELECT--";
                            if (ActiveMdiChild != null)
                            {
                                ActiveMdiChild.Close();
                            }
                            RadMessageBox.Show("Unable to Connect to Cluster : " + cmbcontroller.Text, "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                        }
                    }
                    else
                    {
                        cmbcontroller.Text = "--SELECT--";
                        if (ActiveMdiChild != null)
                        {
                            ActiveMdiChild.Close();
                        }
                        RadMessageBox.Show("Unable to Connect to Cluster : " + cmbcontroller.Text, "SmartMRT", MessageBoxButtons.OK, RadMessageIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void Services_Thread()
        {
            try
            {
                String ip = Database_Connection.GET_SERVER_IP;
                String res = ServiceStatus(ip);

                //get the pms server status
                if (res == "TRUE")
                {
                    //check if funtion is running on diffrent thread other than the main thread
                    if (radListView3.InvokeRequired)
                    {
                        radListView3.Invoke((Action)(() => radListView3.Items[0].Image = img));
                    }
                    else
                    {
                        radListView3.Items[0].Image = img;
                    }
                }
                else
                {
                    //check if funtion is running on diffrent thread other than the main thread
                    if (radListView3.InvokeRequired)
                    {
                        radListView3.Invoke((Action)(() => radListView3.Items[0].Image = off));
                    }
                    else
                    {
                        radListView3.Items[0].Image = img;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //http request to check the pms server status
        private String ServiceStatus(String IP)
        {
            try
            {
                string postData = "";
                string URL = "http://" + IP + ":8091/CURRENT_SHIFT";
                var data = "";
                data = webGetMethod(postData, URL);
                if (data.Contains("'SHIFT'"))
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

        System.Windows.Forms.Timer Reconnect_Timer = new System.Windows.Forms.Timer();

        //open performance reoprt
        private void menudailyproduction_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Daily_Production ms = new Daily_Production();
            ms.MdiParent = this;
            ms.Show();
        }

        //open masters
        private void menudesignsequence_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Design Sequence");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open diffrential line balancing
        private void menulinebalancing_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Line_Balancing ms = new Line_Balancing();
            ms.MdiParent = this;
            ms.Show();
        }

        //reconnect to pms server
        private void Reconnect_Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                //check if its already trying to re connect
                if (reconnect_flag == 0)
                {
                    reconnect_flag = 1;
                    //check if the connection is closed
                    if (dc.con.State == ConnectionState.Closed)
                    {
                        dc.con.Dispose();
                        //open the connection
                        String status = dc.OpenConnection();
                        if (status == "UNABLE")
                        {
                            Reconnect_Timer.Stop();
                            Reconnect_Timer.Enabled = false;
                        }
                    }
                    reconnect_flag = 0;
                }
            }
            catch (Exception ex)
            {
                radLabel5.Text = "Re-Connect : " + ex.Message;
            }
        }

        //open masters
        private void menusparesub_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Spare Sub");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open masters
        private void menusparemain_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Masters ms = new Masters();
            ms.MdiParent = this;
            ms.Show();
            ms.Form_Location("Spare Main");
            ms.Hide_Menu(lblusergroup.Text);
        }

        //open spare report
        private void menusparereport_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Spare_Report ms = new Spare_Report();
            ms.MdiParent = this;
            ms.Show();
        }

        //open spare inventory report
        private void menuspareinventory_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Spare_Inventory ms = new Spare_Inventory();
            ms.MdiParent = this;
            ms.Show();
        }

        //open mo operation report
        private void menumoopreport_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            MO_Operation_Report ms = new MO_Operation_Report();
            ms.MdiParent = this;
            ms.Show();
        }

        private void radDesktopAlert1_Closed(object sender, Telerik.WinControls.UI.RadPopupClosedEventArgs args)
        {
            //DebugLog("radDesktopAlert1_Closed - exit");
            EmergencyFlag();
            
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

        private void mnuEmpInspect_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Emp_Hanger_Inspect ms = new Emp_Hanger_Inspect();
            ms.MdiParent = this;
            ms.Show();
        }

        private void menuStnIdle_Click(object sender, EventArgs e)
        {
            if (ActiveMdiChild != null)
            {
                ActiveMdiChild.Close();
            }

            Station_Idle ss = new Station_Idle();
            ss.MdiParent = this;
            ss.Show();
        }
    }
}
