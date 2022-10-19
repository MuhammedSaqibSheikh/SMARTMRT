using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace SMARTMRT
{
    public partial class Masters : RadForm
    {
        public Masters()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);
        }

        Database_Connection dc = new Database_Connection();

        String u1_id = "";
        String u1_desc = "";
        String u2_id = "";
        String u2_desc = "";
        String u3_id = "";
        String u3_desc = "";
        String u4_id = "";
        String u4_desc = "";
        String u5_id = "";
        String u5_desc = "";
        String u6_id = "";
        String u6_desc = "";
        String u7_id = "";
        String u7_desc = "";
        String u8_id = "";
        String u8_desc = "";
        String u9_id = "";
        String u9_desc = "";
        String u10_id = "";
        String u10_desc = "";
        String update = "";

        String save = "";
        String colordesc = "";
        String articledesc = "";
        String sizedesc = "";
        String contractordesc = "";
        String customerdesc = "";
        String operationdesc = "";
        String machinedesc = "";
        String machineserialno = "";
        String u1desc = "";
        String u2desc = "";
        String u3desc = "";
        String u4desc = "";
        String u5desc = "";
        String u6desc = "";
        String u7desc = "";
        String u8desc = "";
        String u9desc = "";
        String u10desc = "";
        String subdesc = "";
        String maindesc = "";
        String mbmaindesc = "";
        String mbsubdesc = "";
        String groupdesc = "";
        String sparemaindesc = "";
        String sparesubdesc = "";

        String rfid = "";

        public void Form_Location(String Form_Name)
        {
            //open selected tabs
            if (Form_Name == "Color")
            {
                vpagemasters.SelectedPage = pagecolormaster;
            }
            else if (Form_Name == "Article")
            {
                vpagemasters.SelectedPage = pagearticlemaster;
            }
            else if (Form_Name == "Size")
            {
                vpagemasters.SelectedPage = pagesizemaster;
            }
            else if (Form_Name == "Design Sequence")
            {
                vpagemasters.SelectedPage = pagedesignsequence;
            }
            else if (Form_Name == "Contractor")
            {
                vpagemasters.SelectedPage = pagecontractormaster;
            }
            else if (Form_Name == "Employee")
            {
                vpagemasters.SelectedPage = pageemployeemaster;
            }
            else if (Form_Name == "Customer")
            {
                vpagemasters.SelectedPage = pagecustomermaster;
            }
            else if (Form_Name == "Operation")
            {
                vpagemasters.SelectedPage = pageoperationmaster;
            }
            else if (Form_Name == "User1")
            {
                vpagemasters.SelectedPage = pageuser1master;
            }
            else if (Form_Name == "User2")
            {
                vpagemasters.SelectedPage = pageuser2master;
            }
            else if (Form_Name == "User3")
            {
                vpagemasters.SelectedPage = pageuser3master;
            }
            else if (Form_Name == "User4")
            {
                vpagemasters.SelectedPage = pageuser4master;
            }
            else if (Form_Name == "User5")
            {
                vpagemasters.SelectedPage = pageuser5master;
            }
            else if (Form_Name == "User6")
            {
                vpagemasters.SelectedPage = pageuser6master;
            }
            else if (Form_Name == "User7")
            {
                vpagemasters.SelectedPage = pageuser7master;
            }
            else if (Form_Name == "User8")
            {
                vpagemasters.SelectedPage = pageuser8master;
            }
            else if (Form_Name == "User9")
            {
                vpagemasters.SelectedPage = pageuser9master;
            }
            else if (Form_Name == "User10")
            {
                vpagemasters.SelectedPage = pageuser10master;
            }
            else if (Form_Name == "QCMAIN")
            {
                vpagemasters.SelectedPage = pageqcmain;
            }
            else if (Form_Name == "QCSUB")
            {
                vpagemasters.SelectedPage = pageqcsub;
            }
            else if (Form_Name == "Machine")
            {
                vpagemasters.SelectedPage = pagemachinemaster;
            }
            else if (Form_Name == "Machine Details")
            {
                vpagemasters.SelectedPage = pagemachinedetails;
            }
            else if (Form_Name == "Machine Repair Main")
            {
                vpagemasters.SelectedPage = pagembmain;
            }
            else if (Form_Name == "Machine Repair Sub")
            {
                vpagemasters.SelectedPage = pagembsub;
            }
            else if (Form_Name == "Employee Group Category")
            {
                vpagemasters.SelectedPage = pageemployeegroupcategory;
            }
            else if (Form_Name == "Employee Groups")
            {
                vpagemasters.SelectedPage = pageemployeegroup;
            }
            else if (Form_Name == "Employee Skill")
            {
                vpagemasters.SelectedPage = pageskill;
            }
            else if (Form_Name == "Spare Main")
            {
                vpagemasters.SelectedPage = pagesparemain;
            }
            else if (Form_Name == "Spare Sub")
            {
                vpagemasters.SelectedPage = pagesparesub;
            }

            WindowState = FormWindowState.Maximized;
        }

        public void Form_Location1(String Form_Name)
        {
            //show and open only the selected tabs
            if (Form_Name == "Color")
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                vpagemasters.SelectedPage = pagecolormaster;
                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "Article")
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                vpagemasters.SelectedPage = pagearticlemaster;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "Size")
            {
                vpagemasters.SelectedPage = pagesizemaster;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "Contractor")
            {
                vpagemasters.SelectedPage = pagecontractormaster;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "Employee")
            {
                vpagemasters.SelectedPage = pageemployeemaster;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "Customer")
            {
                vpagemasters.SelectedPage = pagecustomermaster;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "User1")
            {
                vpagemasters.SelectedPage = pageuser1master;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "User2")
            {
                vpagemasters.SelectedPage = pageuser2master;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "User3")
            {
                vpagemasters.SelectedPage = pageuser3master;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "User4")
            {
                vpagemasters.SelectedPage = pageuser4master;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "User5")
            {
                vpagemasters.SelectedPage = pageuser5master;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "User6")
            {
                vpagemasters.SelectedPage = pageuser6master;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "User7")
            {
                vpagemasters.SelectedPage = pageuser7master;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "User8")
            {
                vpagemasters.SelectedPage = pageuser8master;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "User9")
            {
                vpagemasters.SelectedPage = pageuser9master;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "User10")
            {
                vpagemasters.SelectedPage = pageuser10master;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
            else if (Form_Name == "Skill")
            {
                vpagemasters.SelectedPage = pageskill;
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(390, 370);
                this.Size = new Size(900, 485);

                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
            }
        }

        private void Masters_Load(object sender, EventArgs e)
        {
            RadMessageBox.SetThemeName("FluentDark");   //set message box theme

            //disable close button on search in grid
            dgvcolor.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvarticle.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvsize.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvcustomer.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvdesignoperation.MasterView.TableSearchRow.ShowCloseButton = false;
            dvgcontractor.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvemployee.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvemployeegroup.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvemployeeselect.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvgroup.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvskill.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvmachines.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvmachinedetails.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvmbmain.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvmbsub.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvqcmain.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvqcsub.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvuser1.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvuser2.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvuser3.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvuser4.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvuser5.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvuser6.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvuser7.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvuser8.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvuser9.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvuser10.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvsparemain.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvsparesub.MasterView.TableSearchRow.ShowCloseButton = false;
            dgvoperation.MasterView.TableSearchRow.ShowCloseButton = false;

            dgvcolor.MasterTemplate.SelectLastAddedRow = false;
            dgvarticle.MasterTemplate.SelectLastAddedRow = false;
            dgvsize.MasterTemplate.SelectLastAddedRow = false;
            dgvcustomer.MasterTemplate.SelectLastAddedRow = false;
            dgvdesignoperation.MasterTemplate.SelectLastAddedRow = false;
            dvgcontractor.MasterTemplate.SelectLastAddedRow = false;
            dgvemployee.MasterTemplate.SelectLastAddedRow = false;
            dgvemployeegroup.MasterTemplate.SelectLastAddedRow = false;
            dgvemployeeselect.MasterTemplate.SelectLastAddedRow = false;
            dgvgroup.MasterTemplate.SelectLastAddedRow = false;
            dgvskill.MasterTemplate.SelectLastAddedRow = false;
            dgvmachines.MasterTemplate.SelectLastAddedRow = false;
            dgvmachinedetails.MasterTemplate.SelectLastAddedRow = false;
            dgvmbmain.MasterTemplate.SelectLastAddedRow = false;
            dgvmbsub.MasterTemplate.SelectLastAddedRow = false;
            dgvqcmain.MasterTemplate.SelectLastAddedRow = false;
            dgvqcsub.MasterTemplate.SelectLastAddedRow = false;
            dgvuser1.MasterTemplate.SelectLastAddedRow = false;
            dgvuser2.MasterTemplate.SelectLastAddedRow = false;
            dgvuser3.MasterTemplate.SelectLastAddedRow = false;
            dgvuser4.MasterTemplate.SelectLastAddedRow = false;
            dgvuser5.MasterTemplate.SelectLastAddedRow = false;
            dgvuser6.MasterTemplate.SelectLastAddedRow = false;
            dgvuser7.MasterTemplate.SelectLastAddedRow = false;
            dgvuser8.MasterTemplate.SelectLastAddedRow = false;
            dgvuser9.MasterTemplate.SelectLastAddedRow = false;
            dgvuser10.MasterTemplate.SelectLastAddedRow = false;
            dgvsparemain.MasterTemplate.SelectLastAddedRow = false;
            dgvsparesub.MasterTemplate.SelectLastAddedRow = false;
            dgvoperation.MasterTemplate.SelectLastAddedRow = false;

            //disable system color tab
            colorbox.ColorDialog.ColorDialogForm.ShowSystemColors = false;
            btndeletecolor.Enabled = false;

            dc.OpenConnection();    //open connection
            RefereshGrid_Color();   //get color master

            //get article master
            btndeletearticle.Enabled = false;
            RefereshGrid_Article();
            btnsetsequence.Enabled = false;

            //get size master
            btndeletesize.Enabled = false;
            RefereshGrid_Size();

            //get contractor master
            btndeletecontractor.Enabled = false;
            RefereshGrid_Contractor();

            //get employee master
            btndeleteemp.Enabled = false;
            RefereshGrid_Employee();

            //get machine details master
            btndeletemachinedetails.Enabled = false;
            RefereshGrid_MachineDetails();

            //get employee group master
            RefereshGrid_EmployeeGroup();

            //get skill master
            btndeleteskill.Enabled = false;
            RefereshGrid_Skill();

            //get all contractor name
            SqlDataAdapter sda = new SqlDataAdapter("Select V_CONTRACTOR_NAME from CONTRACTOR_DB", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbcontractor.Items.Add(dt.Rows[i][0].ToString());
            }

            //get customer master
            btndeletecustomer.Enabled = false;
            RefereshGrid_Customer();

            //get employee group details master
            btndeletegroup.Enabled = false;
            RefereshGrid_GroupCategory();

            //get operation master
            btndeleteoperation.Enabled = false;
            RefereshGrid_Operation();

            //get qc main category master
            btndeleteqcmain.Enabled = false;
            RefereshGrid_QCmain();

            //get qc sub category master
            btndeleteqcsub.Enabled = false;
            RefereshGrid_QCsub();

            //get machine breakdown main category master
            btndeletembmain.Enabled = false;
            RefereshGrid_MBmain();

            //get machine breakdown sub category master
            btndeletembsub.Enabled = false;
            RefereshGrid_MBsub();

            //get machine master
            btndeletemachine.Enabled = false;
            RefereshGrid_Machines();

            //get spare main catgory master
            btndeletesparemain.Enabled = false;
            RefereshGrid_Sparemain();

            //get spare sub category master
            btndeletesparesub.Enabled = false;
            RefereshGrid_Sparesub();

            //get the language
            String Lang = "";
            SqlCommand cmd = new SqlCommand("SELECT * FROM Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Lang = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get id and desc in selected language
            sda = new SqlDataAdapter("select " + Lang + " from Language where Form='User' order by Item_No", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            String id = "";
            String desc = "";
            if (dt.Rows.Count > 0)
            {
                id = dt.Rows[0][0].ToString();
                desc = dt.Rows[1][0].ToString();
            }

            //get special field name and master
            btndeleteuser1.Enabled = false;
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF1' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser1id.Text = sdr.GetValue(0).ToString() + id + " :";
                lbluser1desc.Text = sdr.GetValue(0).ToString() + desc + " :";
                u1_id = sdr.GetValue(0).ToString() + id;
                u1_desc = sdr.GetValue(0).ToString() + desc;
                pageuser1master.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            RefereshGrid_User1();

            //get special field name and master
            btndeleteuser2.Enabled = false;
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser2id.Text = sdr.GetValue(0).ToString() + id + " :";
                lbluser2desc.Text = sdr.GetValue(0).ToString() + desc + " :";
                u2_id = sdr.GetValue(0).ToString() + id;
                u2_desc = sdr.GetValue(0).ToString() + desc;
                pageuser2master.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            RefereshGrid_User2();

            //get special field name and master
            btndeleteuser3.Enabled = false;
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser3id.Text = sdr.GetValue(0).ToString() + id + " :";
                lbluser3desc.Text = sdr.GetValue(0).ToString() + desc + " :";
                u3_id = sdr.GetValue(0).ToString() + id;
                u3_desc = sdr.GetValue(0).ToString() + desc;
                pageuser3master.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            RefereshGrid_User3();

            //get special field name and master
            btndeleteuser4.Enabled = false;
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser4id.Text = sdr.GetValue(0).ToString() + id + " :";
                lbluser4desc.Text = sdr.GetValue(0).ToString() + desc + " :";
                u4_id = sdr.GetValue(0).ToString() + id;
                u4_desc = sdr.GetValue(0).ToString() + desc;
                pageuser4master.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            RefereshGrid_User4();

            //get special field name and master
            btndeleteuser5.Enabled = false;
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser5id.Text = sdr.GetValue(0).ToString() + id + " :";
                lbluser5desc.Text = sdr.GetValue(0).ToString() + desc + " :";
                u5_id = sdr.GetValue(0).ToString() + id;
                u5_desc = sdr.GetValue(0).ToString() + desc;
                pageuser5master.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            RefereshGrid_User5();

            //get special field name and master
            btndeleteuser6.Enabled = false;
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF6' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser6id.Text = sdr.GetValue(0).ToString() + id + " :";
                lbluser6desc.Text = sdr.GetValue(0).ToString() + desc + " :";
                u6_id = sdr.GetValue(0).ToString() + id;
                u6_desc = sdr.GetValue(0).ToString() + desc;
                pageuser6master.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            RefereshGrid_User6();

            //get special field name and master
            btndeleteuser7.Enabled = false;
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF7' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser7id.Text = sdr.GetValue(0).ToString() + id + " :";
                lbluser7desc.Text = sdr.GetValue(0).ToString() + desc + " :";
                u7_id = sdr.GetValue(0).ToString() + id;
                u7_desc = sdr.GetValue(0).ToString() + desc;
                pageuser7master.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            RefereshGrid_User7();

            //get special field name and master
            btndeleteuser8.Enabled = false;
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF8 ' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser8id.Text = sdr.GetValue(0).ToString() + id + " :";
                lbluser8desc.Text = sdr.GetValue(0).ToString() + desc + " :";
                u8_id = sdr.GetValue(0).ToString() + id;
                u8_desc = sdr.GetValue(0).ToString() + desc;
                pageuser8master.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            RefereshGrid_User8();

            //get special field name and master
            btndeleteuser9.Enabled = false;
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF9' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser9id.Text = sdr.GetValue(0).ToString() + id + " :";
                lbluser9desc.Text = sdr.GetValue(0).ToString() + desc + " :";
                u9_id = sdr.GetValue(0).ToString() + id;
                u9_desc = sdr.GetValue(0).ToString() + desc;
                pageuser9master.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            RefereshGrid_User9();

            //get special field name and master
            btndeleteuser10.Enabled = false;
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF10' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                lbluser10id.Text = sdr.GetValue(0).ToString() + id + " :";
                lbluser10desc.Text = sdr.GetValue(0).ToString() + desc + " :";
                u10_id = sdr.GetValue(0).ToString() + id;
                u10_desc = sdr.GetValue(0).ToString() + desc;
                pageuser10master.Text = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            RefereshGrid_User10();

            pnlerror.Visible = false;
            RefereshGrid_Groups();   //get employee group details
            Refrech_DesignSequence();   //get all operations
            Design_Selected();     //get selected operation for the artcile
        }

        public void Refrech_DesignSequence()
        {
            //get operation details and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_OPERATION_CODE,V_OPERATION_DESC,D_PIECERATE,D_SAM FROM OPERATION_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "OPERATION_DB");

            dgvdesignoperation.DataSource = ds.Tables["OPERATION_DB"].DefaultView;
            dgvdesignoperation.Columns["V_OPERATION_CODE"].HeaderText = "Operation Code ";
            dgvdesignoperation.Columns["V_OPERATION_DESC"].HeaderText = "Operation Decsription";
            dgvdesignoperation.Columns["D_PIECERATE"].HeaderText = "Piece Rate";
            dgvdesignoperation.Columns["D_SAM"].HeaderText = "SAM";
            dgvdesignoperation.Visible = false;

            if (dgvdesignoperation.Rows.Count > 0)
            {
                dgvdesignoperation.Visible = true;
            }

            dgvdesignsequence.Visible = false;
            da.Dispose();
            dgvdesignsequence.Columns[4].IsVisible = false;
        }

        public void RowSelected()
        {
            //check if user selected any operation
            if (dgvdesignoperation.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                dgvdesignsequence.Visible = true;

                //get the selected operation
                String opcode = dgvdesignoperation.SelectedRows[0].Cells[0].Value + string.Empty;
                String opdesc = dgvdesignoperation.SelectedRows[0].Cells[1].Value + string.Empty;
                String piecerate = dgvdesignoperation.SelectedRows[0].Cells[2].Value + string.Empty;
                String sam = dgvdesignoperation.SelectedRows[0].Cells[3].Value + string.Empty;

                //check if the selected opeartion already exists in the design sequence
                for (int i = 0; i < dgvdesignsequence.Rows.Count; i++)
                {
                    if (dgvdesignsequence.Rows[i].Cells[2].Value.ToString().Equals(opcode))// && dataGridView1.Rows[i].Cells[1].Value.ToString().Equals(cmbarticle.Text) && dataGridView1.Rows[i].Cells[2].Value.ToString().Equals(cmbsize.Text) && dataGridView1.Rows[i].Cells[3].Value.ToString().Equals(user1) && dataGridView1.Rows[i].Cells[4].Value.ToString().Equals(user2) && dataGridView1.Rows[i].Cells[5].Value.ToString().Equals(user3) && dataGridView1.Rows[i].Cells[6].Value.ToString().Equals(user4) && dataGridView1.Rows[i].Cells[7].Value.ToString().Equals(user5) && dataGridView1.Rows[i].Cells[8].Value.ToString().Equals(user6) && dataGridView1.Rows[i].Cells[9].Value.ToString().Equals(user7) && dataGridView1.Rows[i].Cells[10].Value.ToString().Equals(user8) && dataGridView1.Rows[i].Cells[11].Value.ToString().Equals(user9) && dataGridView1.Rows[i].Cells[12].Value.ToString().Equals(user10))
                    {
                        dgvdesignsequence.Rows[i].IsSelected = true;
                        dgvdesignsequence.Rows[i].IsCurrent = true;
                        lblmsg.Text = "Row Already Exists";
                        return;
                    }
                }

                //add to grid
                int k = dgvdesignsequence.Rows.Count + 1;
                dgvdesignsequence.Rows.Add(k, k, opcode, opdesc, 'Y', piecerate, sam);
                btnsavesequence.ForeColor = Color.Red;
            }

            DesignSummary();   //get design summary
        }

        public void DesignSummary()
        {
            decimal total_piece = 0;
            int total_sam = 0;

            //calculate total piece rate and sam for the selected operations
            for (int i = 0; i < dgvdesignsequence.Rows.Count; i++)
            {
                total_piece += Convert.ToDecimal(dgvdesignsequence.Rows[i].Cells[5].Value.ToString());
                total_sam += int.Parse(dgvdesignsequence.Rows[i].Cells[6].Value.ToString());
            }

            lbldesignsummary.Text = "Total Operations : " + dgvdesignsequence.Rows.Count + "            Total Piece Rate : " + total_piece.ToString("0.##") + "             Total SAM : " + total_sam;
        }

        //hide menu for the user access privelages
        public void Hide_Menu(String user)
        {
            String color = "N";
            String article = "N";
            String size = "N";
            String prodline = "N";
            String emp = "N";
            String contractor = "N";
            String customer = "N";
            String operation = "N";
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
            String qcmain = "N";
            String qcsub = "N";
            String groupcategory = "N";
            String empgroup = "N";
            String machines = "N";
            String machinedetails = "N";
            String mbmain = "N";
            String mbsub = "N";
            String empskill_level = "N";
            String sparemain = "N";
            String sparesub = "N";
            String designseq = "N";

            //get the user access previlages
            SqlDataAdapter sda = new SqlDataAdapter("select * from USER_LOGIN_DETAILS where USER_GROUP='" + user + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                color = dt.Rows[i][9].ToString();
                size = dt.Rows[i][10].ToString();
                article = dt.Rows[i][11].ToString();
                customer = dt.Rows[i][12].ToString();
                emp = dt.Rows[i][13].ToString();
                contractor = dt.Rows[i][14].ToString();
                prodline = dt.Rows[i][15].ToString();
                operation = dt.Rows[i][16].ToString();
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
                qcmain = dt.Rows[i][35].ToString();
                qcsub = dt.Rows[i][36].ToString();
                groupcategory = dt.Rows[i][37].ToString();
                empgroup = dt.Rows[i][38].ToString();
                empskill_level = dt.Rows[i][39].ToString();
                machines = dt.Rows[i][40].ToString();
                machinedetails = dt.Rows[i][41].ToString();
                mbmain = dt.Rows[i][42].ToString();
                mbsub = dt.Rows[i][43].ToString();
                sparemain = dt.Rows[i][67].ToString();
                sparesub = dt.Rows[i][68].ToString();
                designseq = dt.Rows[i][61].ToString();
            }

            //check if super user in logged in
            if (user == "Super User")
            {
                color = "Y";
                article = "Y";
                size = "Y";
                prodline = "Y";
                emp = "Y";
                contractor = "Y";
                customer = "Y";
                operation = "Y";
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
                qcmain = "Y";
                qcsub = "Y";
                groupcategory = "Y";
                empgroup = "Y";
                machines = "Y";
                machinedetails = "Y";
                mbmain = "Y";
                mbsub = "Y";
                empskill_level = "Y";
                sparemain = "Y";
                sparesub = "Y";
                designseq = "Y";
            }

            //get if basic version of pms client is running
            if (Database_Connection.SET_PMSCIENT == "1")
            {
                //machines = "N";
                //machinedetails = "N";
                mbmain = "N";
                mbsub = "N";
                sparemain = "N";
                sparesub = "N";
                qcmain = "N";
                qcsub = "N";
            }

            //hide the tab if the access previlages is not set for the user
            if (color == "Y")
            {
                pagecolormaster.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pagecolormaster.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (article == "Y")
            {
                pagearticlemaster.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pagearticlemaster.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (size == "Y")
            {
                pagesizemaster.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pagesizemaster.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (customer == "Y")
            {
                pagecustomermaster.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pagecustomermaster.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (operation == "Y")
            {
                pageoperationmaster.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageoperationmaster.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (emp == "Y")
            {
                pageemployeemaster.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageemployeemaster.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (contractor == "Y")
            {
                pagecontractormaster.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pagecontractormaster.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (user1 == "Y")
            {
                pageuser1master.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (user2 == "Y")
            {
                pageuser2master.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (user3 == "Y")
            {
                pageuser3master.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (user4 == "Y")
            {
                pageuser4master.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (user5 == "Y")
            {
                pageuser5master.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (user6 == "Y")
            {
                pageuser6master.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (user7 == "Y")
            {
                pageuser7master.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (user8 == "Y")
            {
                pageuser8master.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (user9 == "Y")
            {
                pageuser9master.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (user10 == "Y")
            {
                pageuser10master.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (qcmain == "Y")
            {
                pageqcmain.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageqcmain.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (qcsub == "Y")
            {
                pageqcsub.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageqcsub.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (groupcategory == "Y")
            {
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageemployeegroupcategory.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (empgroup == "Y")
            {
                pageemployeegroup.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageemployeegroup.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (empskill_level == "Y")
            {
                pageskill.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pageskill.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (machines == "Y")
            {
                pagemachinemaster.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pagemachinemaster.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (machinedetails == "Y")
            {
                pagemachinedetails.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pagemachinedetails.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (mbmain == "Y")
            {
                pagembmain.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pagembmain.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (mbsub == "Y")
            {
                pagembsub.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pagembsub.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (sparemain == "Y")
            {
                pagesparemain.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pagesparemain.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (sparesub == "Y")
            {
                pagesparesub.Item.Visibility = ElementVisibility.Visible;
            }
            else
            {
                pagesparesub.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (designseq == "Y")
            {
                pagedesignsequence.Item.Visibility = ElementVisibility.Visible;
                btnsetsequence.Visible = true;
            }
            else
            {
                pagedesignsequence.Item.Visibility = ElementVisibility.Collapsed;
                btnsetsequence.Visible = false;
            }

            //get special field name
            SqlCommand cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF1' and V_ENABLED='TRUE'", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                u1_id = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF2' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                u2_id = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF3' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                u3_id = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF4' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                u4_id = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF5' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                u5_id = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF6' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                u6_id = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF7' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                u7_id = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF8 ' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                u8_id = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF9' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                u9_id = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get special field name
            cmd = new SqlCommand("SELECT V_USER FROM USER_COLUMN_NAMES where V_MRT = 'USER_DEF10' and V_ENABLED='TRUE'", dc.con);
            sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                u10_id = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //hide the tab if the access previlages is not set for the user
            if (u1_id == "")
            {
                pageuser1master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (u2_id == "")
            {
                pageuser2master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (u3_id == "")
            {
                pageuser3master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (u4_id == "")
            {
                pageuser4master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (u5_id == "")
            {
                pageuser5master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (u6_id == "")
            {
                pageuser6master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (u7_id == "")
            {
                pageuser7master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (u8_id == "")
            {
                pageuser8master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (u9_id == "")
            {
                pageuser9master.Item.Visibility = ElementVisibility.Collapsed;
            }

            //hide the tab if the access previlages is not set for the user
            if (u10_id == "")
            {
                pageuser10master.Item.Visibility = ElementVisibility.Collapsed;
            }
        }

        public void RefereshGrid_User10()
        {
            //get all the special field and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_USER_ID,V_DESC FROM USER_DEF10_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "USER_DEF10_DB");
            dgvuser10.DataSource = ds.Tables["USER_DEF10_DB"].DefaultView;
            dgvuser10.Columns["V_USER_ID"].HeaderText = u10_id;
            dgvuser10.Columns["V_DESC"].HeaderText = u10_desc;
            dgvuser10.Visible = false;

            if (dgvuser10.Rows.Count > 0)
            {
                dgvuser10.Visible = true;
            }
        }

        public void RefereshGrid_User9()
        {
            //get all the special field and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_USER_ID,V_DESC FROM USER_DEF9_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "USER_DEF9_DB");
            dgvuser9.DataSource = ds.Tables["USER_DEF9_DB"].DefaultView;
            dgvuser9.Columns["V_USER_ID"].HeaderText = u9_id;
            dgvuser9.Columns["V_DESC"].HeaderText = u9_desc;
            dgvuser9.Visible = false;

            if (dgvuser9.Rows.Count > 0)
            {
                dgvuser9.Visible = true;
            }
        }

        public void RefereshGrid_User8()
        {
            //get all the special field and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_USER_ID,V_DESC FROM USER_DEF8_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "USER_DEF8_DB");
            dgvuser8.DataSource = ds.Tables["USER_DEF8_DB"].DefaultView;
            dgvuser8.Columns["V_USER_ID"].HeaderText = u8_id;
            dgvuser8.Columns["V_DESC"].HeaderText = u8_desc;
            dgvuser8.Visible = false;

            if (dgvuser8.Rows.Count > 0)
            {
                dgvuser8.Visible = true;
            }
        }

        public void RefereshGrid_User7()
        {
            //get all the special field and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_USER_ID,V_DESC FROM USER_DEF7_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "USER_DEF7_DB");
            dgvuser7.DataSource = ds.Tables["USER_DEF7_DB"].DefaultView;
            dgvuser7.Columns["V_USER_ID"].HeaderText = u7_id;
            dgvuser7.Columns["V_DESC"].HeaderText = u7_desc;
            dgvuser7.Visible = false;

            if (dgvuser7.Rows.Count > 0)
            {
                dgvuser7.Visible = true;
            }
        }

        public void RefereshGrid_User6()
        {
            //get all the special field and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_USER_ID,V_DESC FROM USER_DEF6_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "USER_DEF6_DB");
            dgvuser6.DataSource = ds.Tables["USER_DEF6_DB"].DefaultView;
            dgvuser6.Columns["V_USER_ID"].HeaderText = u6_id;
            dgvuser6.Columns["V_DESC"].HeaderText = u6_desc;
            dgvuser6.Visible = false;

            if (dgvuser6.Rows.Count > 0)
            {
                dgvuser6.Visible = true;
            }
        }

        public void RefereshGrid_User5()
        {
            //get all the special field and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_USER_ID,V_DESC FROM USER_DEF5_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "USER_DEF5_DB");
            dgvuser5.DataSource = ds.Tables["USER_DEF5_DB"].DefaultView;
            dgvuser5.Columns["V_USER_ID"].HeaderText = u5_id;
            dgvuser5.Columns["V_DESC"].HeaderText = u5_desc;
            dgvuser5.Visible = false;

            if (dgvuser5.Rows.Count > 0)
            {
                dgvuser5.Visible = true;
            }
        }

        public void RefereshGrid_User4()
        {
            //get all the special field and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_USER_ID,V_DESC FROM USER_DEF4_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "USER_DEF4_DB");
            dgvuser4.DataSource = ds.Tables["USER_DEF4_DB"].DefaultView;
            dgvuser4.Columns["V_USER_ID"].HeaderText = u4_id;
            dgvuser4.Columns["V_DESC"].HeaderText = u4_desc;
            dgvuser4.Visible = false;

            if (dgvuser4.Rows.Count > 0)
            {
                dgvuser4.Visible = true;
            }
        }

        public void RefereshGrid_User3()
        {
            //get all the special field and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_USER_ID,V_DESC FROM USER_DEF3_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "USER_DEF3_DB");
            dgvuser3.DataSource = ds.Tables["USER_DEF3_DB"].DefaultView;
            dgvuser3.Columns["V_USER_ID"].HeaderText = u3_desc;
            dgvuser3.Columns["V_DESC"].HeaderText = u3_desc;
            dgvuser3.Visible = false;

            if (dgvuser3.Rows.Count > 0)
            {
                dgvuser3.Visible = true;
            }
        }

        public void RefereshGrid_User2()
        {
            //get all the special field and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_USER_ID,V_DESC FROM USER_DEF2_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "USER_DEF2_DB");
            dgvuser2.DataSource = ds.Tables["USER_DEF2_DB"].DefaultView;
            dgvuser2.Columns["V_USER_ID"].HeaderText = u2_id;
            dgvuser2.Columns["V_DESC"].HeaderText = u2_desc;
            dgvuser2.Visible = false;

            if (dgvuser2.Rows.Count > 0)
            {
                dgvuser2.Visible = true;
            }
        }

        public void RefereshGrid_User1()
        {
            //get all the special field and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_USER_ID,V_DESC FROM USER_DEF1_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "USER_DEF1_DB");
            dgvuser1.DataSource = ds.Tables["USER_DEF1_DB"].DefaultView;
            dgvuser1.Columns["V_USER_ID"].HeaderText = u1_id;
            dgvuser1.Columns["V_DESC"].HeaderText = u1_desc;
            dgvuser1.Visible = false;

            if (dgvuser1.Rows.Count > 0)
            {
                dgvuser1.Visible = true;
            }
        }

        public void RefereshGrid_Operation()
        {
            //get all the operation and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT op.V_OPERATION_CODE,op.V_OPERATION_DESC,op.D_PIECERATE,op.D_OVERTIME_RATE,op.D_SAM,mc.V_MACHINE_DESC FROM OPERATION_DB op ,MACHINE_DB mc where op.V_MACHINE_ID=mc.V_MACHINE_ID ORDER BY V_OPERATION_CODE", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "OPERATION_DB");
            dgvoperation.DataSource = ds.Tables["OPERATION_DB"].DefaultView;
            dgvoperation.Columns["V_OPERATION_CODE"].HeaderText = "Operation Code ";
            dgvoperation.Columns["V_OPERATION_DESC"].HeaderText = "Operation Decsription";
            dgvoperation.Columns["D_PIECERATE"].HeaderText = "Piece Rate";
            dgvoperation.Columns["D_SAM"].HeaderText = "SAM";
            dgvoperation.Columns["V_MACHINE_DESC"].HeaderText = "Machine";
            dgvoperation.Columns["D_OVERTIME_RATE"].HeaderText = "OverTime Rate";
            dgvoperation.Visible = false;

            if (dgvoperation.Rows.Count > 0)
            {
                dgvoperation.Visible = true;
            }

            //get all the machines
            da = new SqlDataAdapter("select distinct V_MACHINE_DESC from MACHINE_DB", dc.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cmbmachine.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbmachine.Items.Add(dt.Rows[i][0].ToString());
            }

            cmbmachine.Text = "--SELECT--";
        }

        public void RefereshGrid_MachineDetails()
        {
            //get all the machine details and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT op.V_MACHINE_SERIAL_NO,op.D_PURCHASE_DATE,op.V_RFID,mc.V_MACHINE_DESC FROM MACHINE_DETAILS op ,MACHINE_DB mc where op.V_MACHINE_ID=mc.V_MACHINE_ID", dc.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            dgvmachinedetails.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvmachinedetails.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
            }

            //get all the machines
            da = new SqlDataAdapter("select distinct V_MACHINE_DESC from MACHINE_DB", dc.con);
            dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmbmachinedetails.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbmachinedetails.Items.Add(dt.Rows[i][0].ToString());
            }

            cmbmachinedetails.Text = "--SELECT--";
        }

        public void RefereshGrid_EmployeeGroup()
        {
            //get all the employee and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_EMP_ID,V_FIRST_NAME,V_LAST_NAME,V_LED_NAME,V_SEX,D_DOB FROM EMPLOYEE", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "EMPLOYEE");
            dgvemployeeselect.DataSource = ds.Tables["EMPLOYEE"].DefaultView;
            dgvemployeeselect.Columns["V_EMP_ID"].HeaderText = "Employee ID";
            dgvemployeeselect.Columns["V_FIRST_NAME"].HeaderText = "First Name";
            dgvemployeeselect.Columns["V_LAST_NAME"].HeaderText = "Last Name";
            dgvemployeeselect.Columns["V_LED_NAME"].HeaderText = "LED Name";
            dgvemployeeselect.Columns["V_SEX"].HeaderText = "Sex";
            dgvemployeeselect.Columns["D_DOB"].HeaderText = "DOB";
            dgvemployeeselect.Visible = false;

            if (dgvemployeeselect.Rows.Count > 0)
            {
                dgvemployeeselect.Visible = true;
            }
        }

        public void RefereshGrid_Skill()
        {
            //get all the skill and add to grid
            SqlDataAdapter sda = new SqlDataAdapter("Select V_SKILL_LEVEL,I_EFFICIENCY,D_SKILL_RATE from SKILL_RATE", dc.con);
            DataSet ds = new DataSet();
            sda.Fill(ds, "SKILL_RATE");
            dgvskill.DataSource = ds.Tables["SKILL_RATE"].DefaultView;
            dgvskill.Columns["V_SKILL_LEVEL"].HeaderText = "Skill Level";
            dgvskill.Columns["I_EFFICIENCY"].HeaderText = "Efficiency";
            dgvskill.Columns["D_SKILL_RATE"].HeaderText = "Skill Rate";
            dgvskill.Visible = false;

            if (dgvskill.Rows.Count > 0)
            {
                dgvskill.Visible = true;
            }
        }

        public void RefereshGrid_QCmain()
        {
            //get all the qc main category and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_QC_MAIN_ID,V_QC_MAIN_DESC FROM QC_MAIN_CATEGORY", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "QC_MAIN_CATEGORY");
            dgvqcmain.DataSource = ds.Tables["QC_MAIN_CATEGORY"].DefaultView;
            dgvqcmain.Columns["V_QC_MAIN_ID"].HeaderText = "QC Main ID ";
            dgvqcmain.Columns["V_QC_MAIN_DESC"].HeaderText = "QC Main Desc";
            dgvqcmain.Visible = false;

            if (dgvqcmain.Rows.Count > 0)
            {
                dgvqcmain.Visible = true;
            }
        }

        public void RefereshGrid_MBmain()
        {
            //get all the machine breakdown main category and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_MB_MAIN_ID,V_MB_MAIN_DESC FROM MB_MAIN_CATEGORY", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "MB_MAIN_CATEGORY");
            dgvmbmain.DataSource = ds.Tables["MB_MAIN_CATEGORY"].DefaultView;
            dgvmbmain.Columns["V_MB_MAIN_ID"].HeaderText = "Main ID ";
            dgvmbmain.Columns["V_MB_MAIN_DESC"].HeaderText = "Main Desc";
            dgvmbmain.Visible = false;

            if (dgvmbmain.Rows.Count > 0)
            {
                dgvmbmain.Visible = true;
            }
        }

        public void RefereshGrid_Sparemain()
        {
            //get all the spare main category and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_SPARE_MAIN_ID,V_SPARE_MAIN_DESC FROM SPARE_MAIN_CATEGORY", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "SPARE_MAIN_CATEGORY");
            dgvsparemain.DataSource = ds.Tables["SPARE_MAIN_CATEGORY"].DefaultView;
            dgvsparemain.Columns["V_SPARE_MAIN_ID"].HeaderText = "Main ID ";
            dgvsparemain.Columns["V_SPARE_MAIN_DESC"].HeaderText = "Main Desc";
            dgvsparemain.Visible = false;

            if (dgvsparemain.Rows.Count > 0)
            {
                dgvsparemain.Visible = true;
            }
        }

        public void RefereshGrid_Machines()
        {
            //get all the machine and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_MACHINE_ID,V_MACHINE_DESC,V_MODEL,V_ATTACHMENT1,V_ATTACHMENT2 FROM MACHINE_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "MACHINE_DB");
            dgvmachines.DataSource = ds.Tables["MACHINE_DB"].DefaultView;
            dgvmachines.Columns["V_MACHINE_ID"].HeaderText = "Machine ID ";
            dgvmachines.Columns["V_MACHINE_DESC"].HeaderText = "Machine Desc";
            dgvmachines.Columns["V_MODEL"].HeaderText = "Model";
            dgvmachines.Columns["V_ATTACHMENT1"].HeaderText = "Attachment 1";
            dgvmachines.Columns["V_ATTACHMENT2"].HeaderText = "Attachment 2";
            dgvmachines.Visible = false;

            if (dgvmachines.Rows.Count > 0)
            {
                dgvmachines.Visible = true;
            }
        }

        public void RefereshGrid_QCsub()
        {
            //get all the all the qc sub category and add to grid
            for (int i = 0; i < dgvqcsub.Rows.Count; i++)
            {
                dgvqcsub.Rows[i].IsVisible = true;
            }
            dgvqcsub.Rows.Clear();

            SqlDataAdapter da = new SqlDataAdapter("select QCMAIN.V_QC_MAIN_DESC,QCSUB.V_QC_SUB_ID,QCSUB.V_QC_SUB_DESC from QC_SUB_CATEGORY QCSUB,QC_MAIN_CATEGORY QCMAIN where QCMAIN.V_QC_MAIN_ID=QCSUB.V_QC_MAIN_ID", dc.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvqcsub.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
            }

            dgvqcsub.Visible = false;
            if (dgvqcsub.Rows.Count > 0)
            {
                dgvqcsub.Visible = true;
            }

            //get all the qc main category
            da = new SqlDataAdapter("select distinct V_QC_MAIN_DESC from QC_MAIN_CATEGORY", dc.con);
            dt = new DataTable();
            da.Fill(dt);
            cmbqcmaindesc.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbqcmaindesc.Items.Add(dt.Rows[i][0].ToString());
            }

            cmbqcmaindesc.Text = "--SELECT--";
        }

        public void RefereshGrid_MBsub()
        {
            for (int i = 0; i < dgvmbsub.Rows.Count; i++)
            {
                dgvmbsub.Rows[i].IsVisible = true;
            }

            //get all the machine breakdown sub category and add to grid
            dgvmbsub.Rows.Clear();
            SqlDataAdapter da = new SqlDataAdapter("select QCMAIN.V_MB_MAIN_DESC,QCSUB.V_MB_SUB_ID,QCSUB.V_MB_SUB_DESC from MB_SUB_CATEGORY QCSUB,MB_MAIN_CATEGORY QCMAIN where QCMAIN.V_MB_MAIN_ID=QCSUB.V_MB_MAIN_ID", dc.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvmbsub.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
            }

            dgvmbsub.Visible = false;
            if (dgvmbsub.Rows.Count > 0)
            {
                dgvmbsub.Visible = true;
            }

            //get all the machine details
            da = new SqlDataAdapter("select distinct V_MB_MAIN_DESC from MB_MAIN_CATEGORY", dc.con);
            dt = new DataTable();
            da.Fill(dt);
            cmbmbmaindesc.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbmbmaindesc.Items.Add(dt.Rows[i][0].ToString());
            }

            cmbmbmaindesc.Text = "--SELECT--";
        }

        public void RefereshGrid_Sparesub()
        {
            for (int i = 0; i < dgvsparesub.Rows.Count; i++)
            {
                dgvsparesub.Rows[i].IsVisible = true;
            }

            //get all the spare sub category and add to grid
            dgvsparesub.Rows.Clear();
            SqlDataAdapter da = new SqlDataAdapter("select QCMAIN.V_SPARE_MAIN_DESC,QCSUB.V_SPARE_SUB_ID,QCSUB.V_SPARE_SUB_DESC,QCSUB.I_QUANTITY,QCSUB.D_COST from SPARE_SUB_CATEGORY QCSUB,SPARE_MAIN_CATEGORY QCMAIN where QCMAIN.V_SPARE_MAIN_ID=QCSUB.V_SPARE_MAIN_ID", dc.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvsparesub.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][3].ToString());
            }

            dgvsparesub.Visible = false;
            if (dgvsparesub.Rows.Count > 0)
            {
                dgvsparesub.Visible = true;
            }

            //get all the spare main category
            da = new SqlDataAdapter("select distinct V_SPARE_MAIN_DESC from SPARE_MAIN_CATEGORY", dc.con);
            dt = new DataTable();
            da.Fill(dt);
            cmbsparemaindesc.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbsparemaindesc.Items.Add(dt.Rows[i][0].ToString());
            }

            cmbsparemaindesc.Text = "--SELECT--";
        }

        public void RefereshGrid_Customer()
        {
            //get all the customer and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_CUSTOMER_ID,V_CUSTOMER_NAME,V_CUSTOMER_ORIGIN FROM CUSTOMER_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "CUSTOMER_DB");
            dgvcustomer.DataSource = ds.Tables["CUSTOMER_DB"].DefaultView;
            dgvcustomer.Columns["V_CUSTOMER_ID"].HeaderText = "Customer ID";
            dgvcustomer.Columns["V_CUSTOMER_NAME"].HeaderText = "Customer Name";
            dgvcustomer.Columns["V_CUSTOMER_ORIGIN"].HeaderText = "Customer Destination";
            dgvcustomer.Visible = false;

            if (dgvcustomer.Rows.Count > 0)
            {
                dgvcustomer.Visible = true;
            }
        }

        public void RefereshGrid_GroupCategory()
        {
            //get all the employee group category and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_GROUP_ID,V_GROUP_DESC,V_GROUP_LED_NAME,V_GROUP_RFID,V_STATUS FROM EMPLOYEE_GROUP_CATEGORY", dc.con);
            DataSet ds = new DataSet();

            da.Fill(ds, "EMPLOYEE_GROUP_CATEGORY");
            dgvgroup.DataSource = ds.Tables["EMPLOYEE_GROUP_CATEGORY"].DefaultView;
            dgvgroup.Columns["V_GROUP_ID"].HeaderText = "Group ID";
            dgvgroup.Columns["V_GROUP_DESC"].HeaderText = "Group Desc";
            dgvgroup.Columns["V_GROUP_LED_NAME"].HeaderText = "Group LED Name";
            dgvgroup.Columns["V_GROUP_RFID"].HeaderText = "Group RFID";
            dgvgroup.Columns["V_STATUS"].HeaderText = "Status";
            dgvgroup.Visible = false;

            if (dgvgroup.Rows.Count > 0)
            {
                dgvgroup.Visible = true;
            }
        }

        public void RefereshGrid_Groups()
        {
            //get all the employee group details and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_GROUP_DESC FROM EMPLOYEE_GROUP_CATEGORY", dc.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            cmbgroupdesc.Items.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbgroupdesc.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        public void RefereshGrid_Employee()
        {
            //get all the employee and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_EMP_ID,V_FIRST_NAME,V_LAST_NAME,V_LED_NAME,V_SEX,D_DOB,V_LOGIN_STATUS FROM EMPLOYEE", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "EMPLOYEE");
            dgvemployee.DataSource = ds.Tables["EMPLOYEE"].DefaultView;
            dgvemployee.Columns["V_EMP_ID"].HeaderText = "Employee ID";
            dgvemployee.Columns["V_FIRST_NAME"].HeaderText = "First Name";
            dgvemployee.Columns["V_LAST_NAME"].HeaderText = "Last Name";
            dgvemployee.Columns["V_LED_NAME"].HeaderText = "LED Name";
            dgvemployee.Columns["V_SEX"].HeaderText = "Sex";
            dgvemployee.Columns["D_DOB"].HeaderText = "DOB";
            dgvemployee.Columns["V_LOGIN_STATUS"].HeaderText = "Status";
            dgvemployee.Visible = false;

            if (dgvemployee.Rows.Count > 0)
            {
                dgvemployee.Visible = true;
            }

            cmbskill.Items.Clear();
            //get all the skill
            da = new SqlDataAdapter("Select V_SKILL_LEVEL from SKILL_RATE", dc.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cmbskill.Items.Add(dt.Rows[i][0].ToString());
            }
        }

        public void RefereshGrid_Contractor()
        {
            //get all the contractor and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_CONTRACTOR_ID,V_CONTRACTOR_NAME FROM CONTRACTOR_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "CONTRACTOR_DB");
            dvgcontractor.DataSource = ds.Tables["CONTRACTOR_DB"].DefaultView;
            dvgcontractor.Columns["V_CONTRACTOR_ID"].HeaderText = "Contractor ID";
            dvgcontractor.Columns["V_CONTRACTOR_NAME"].HeaderText = "Contractor NAME";
            dvgcontractor.Visible = false;

            if (dvgcontractor.Rows.Count > 0)
            {
                dvgcontractor.Visible = true;
            }
        }

        public void RefereshGrid_Size()
        {
            //get all the sizes and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_SIZE_ID,V_SIZE_DESC FROM SIZE_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "SIZE_DB");
            dgvsize.DataSource = ds.Tables["SIZE_DB"].DefaultView;
            dgvsize.Columns["V_SIZE_ID"].HeaderText = "Size ID";
            dgvsize.Columns["V_SIZE_DESC"].HeaderText = "Size Decsription";
            dgvsize.Visible = false;

            if (dgvsize.Rows.Count > 0)
            {
                dgvsize.Visible = true;
            }
        }

        public void RefereshGrid_Article()
        {
            //get all the article and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_ARTICLE_ID,V_ARTICLE_DESC FROM ARTICLE_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "ARTICLE_DB");
            dgvarticle.DataSource = ds.Tables["ARTICLE_DB"].DefaultView;
            dgvarticle.Columns["V_ARTICLE_ID"].HeaderText = "Article ID";
            dgvarticle.Columns["V_ARTICLE_DESC"].HeaderText = "Article Description";
            dgvarticle.Visible = false;

            if (dgvarticle.Rows.Count > 0)
            {
                dgvarticle.Visible = true;
            }

            //cmbdesignarticle.Items.Clear();
            ////get all the articles
            //da = new SqlDataAdapter("Select V_ARTICLE_DESC from ARTICLE_DB", dc.con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    cmbdesignarticle.Items.Add(dt.Rows[i][0].ToString());
            //    cmbdesignarticle.SelectedIndex = 0;
            //}

            //Hanafi|21/7/2021|Changes - populate article ID and Desc
            try
            {
                //cmbdesignarticle.Items.Clear();
                //SqlDataAdapter da2 = new SqlDataAdapter("SELECT V_ARTICLE_ID, (+'['+ V_ARTICLE_ID + '] ' + V_ARTICLE_DESC) as ArtDesc FROM ARTICLE_DB", dc.con);
                SqlDataAdapter da2 = new SqlDataAdapter("SELECT V_ARTICLE_ID, (V_ARTICLE_ID + ' : ' + V_ARTICLE_DESC) as ArtDesc FROM ARTICLE_DB ORDER BY V_ARTICLE_ID", dc.con);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "ARTICLE_DB");
                DataTable dt = ds2.Tables["ARTICLE_DB"];
                DataRow row = dt.NewRow();
                //dt.Rows.Add(0, 0, "--SELECT--");

                row["V_ARTICLE_ID"] = 0;
                row["ArtDesc"] = "--SELECT--";
                dt.Rows.InsertAt(row, 0);

                cmbdesignarticle.DataSource = dt;
                cmbdesignarticle.DisplayMember = "ArtDesc";
                cmbdesignarticle.ValueMember = "V_ARTICLE_ID";
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
            
        }

        public void RefereshGrid_Color()
        {
            //get all the color and add to grid
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_COLOR_ID,V_COLOR_DESC,V_RGB FROM COLOR_DB", dc.con);
            DataSet ds = new DataSet();
            da.Fill(ds, "COLOR_DB");
            dgvcolor.DataSource = ds.Tables["COLOR_DB"].DefaultView;
            dgvcolor.Columns["V_COLOR_ID"].HeaderText = "Color ID";
            dgvcolor.Columns["V_COLOR_DESC"].HeaderText = "Color Description";
            dgvcolor.Columns["V_RGB"].HeaderText = "Color RGB";
            dgvcolor.Visible = false;

            if (dgvcolor.Rows.Count > 0)
            {
                dgvcolor.Visible = true;
            }
        }

        private void colorbox_ValueChanged(object sender, EventArgs e)
        {
            pnlrgb.BackColor = colorbox.Value;   //change the panel color with the selected color
        }

        private void btnsavecolor_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtcolorid.Text != "" && txtcolordesc.Text != "")
                {
                    int clr = colorbox.Value.ToArgb();
                    String ColorHex = String.Format("{0:x6}", clr);
                    btndeletecolor.Enabled = false;

                    //check if save button id clicked
                    if (btnsavecolor.Text == save)
                    {
                        //get the color id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from COLOR_DB where V_COLOR_ID='" + txtcolorid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get the color desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from COLOR_DB where V_COLOR_DESC='" + txtcolordesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc already exists
                        if (i == 0 && k == 0)
                        {
                            //insert into color_db
                            SqlCommand cmd = new SqlCommand("insert into COLOR_DB values('" + txtcolorid.Text + "','" + txtcolordesc.Text + "','" + ColorHex + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_Color();   //get color master

                            txtcolorid.ReadOnly = false;
                            ClearData_Color();   //clear all fields
                        }
                        else
                        {
                            //select in grid if already exists
                            for (int j = 0; j < dgvcolor.Rows.Count; j++)
                            {
                                if (dgvcolor.Rows[j].Cells[0].Value.ToString().Equals(txtcolorid.Text) || dgvcolor.Rows[j].Cells[1].Value.ToString().Equals(txtcolordesc.Text))
                                {
                                    dgvcolor.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    //check update button is clicked
                    if (btnsavecolor.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from COLOR_DB where V_COLOR_DESC='" + txtcolordesc.Text + "' and V_RGB='" + ColorHex + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc already exists or the same desc
                        if (k == 0 || colordesc == txtcolordesc.Text)
                        {
                            //update color db
                            SqlCommand cmd = new SqlCommand("Update COLOR_DB set V_COLOR_DESC='" + txtcolordesc.Text + "',V_RGB='" + ColorHex + "' where V_COLOR_ID='" + txtcolorid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_Color();   //get color master

                            txtcolorid.ReadOnly = false;
                            btnsavecolor.Text = save;

                            ClearData_Color();   //clear all fields
                        }
                        else
                        {
                            //select the row if already exists
                            for (int j = 0; j < dgvcolor.Rows.Count; j++)
                            {
                                if (dgvcolor.Rows[j].Cells[1].Value.ToString().Equals(txtcolordesc.Text) && dgvcolor.Rows[j].Cells[2].Value.ToString().Equals(ColorHex))
                                {
                                    dgvcolor.Rows[j].IsSelected = true;
                                    lblmsg.Text = "COLOR Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    btnsavecolor.ForeColor = Color.Lime;
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear fields
        public void ClearData_Color()
        {
            txtcolorid.Text = "";
            txtcolordesc.Text = "";
            pnlrgb.BackColor = Color.Transparent;
        }

        private void btneditcolor_Click(object sender, EventArgs e)
        {
            RowSelected_Color();   //get selected color
        }

        public void RowSelected_Color()
        {
            if (dgvcolor.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                //get selected color grom the grid
                String colorId = dgvcolor.SelectedRows[0].Cells[0].Value + string.Empty;
                String colorDesc = dgvcolor.SelectedRows[0].Cells[1].Value + string.Empty;
                String RGB = "#" + dgvcolor.SelectedRows[0].Cells[2].Value + string.Empty;

                txtcolorid.Text = colorId;
                txtcolordesc.Text = colorDesc;
                Color color = ColorTranslator.FromHtml(RGB);
                colorbox.Value = color;

                txtqcsubid.ReadOnly = true;
                btnsavecolor.Text = update;
                btndeletecolor.Enabled = true;
                btnsavecolor.ForeColor = Color.Red;
                colordesc = colorDesc;
            }
        }

        private void btndeletecolor_Click(object sender, EventArgs e)
        {
            try
            {
                //get the selected color 
                SqlCommand cmd = new SqlCommand("Delete from COLOR_DB where V_COLOR_ID='" + txtcolorid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                lblmsg.Text = "Record Deleted";
                RefereshGrid_Color();  //get color master

                txtcolorid.ReadOnly = false;
                btnsavecolor.Text = save;
                ClearData_Color();   //clear all fields

                btndeletecolor.Enabled = false;
                btnsavecolor.ForeColor = Color.Lime;
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Is in Use";
                Console.WriteLine(ex.Message);
            }
        }

        private void dgvcolor_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Color();   //get the selected color
        }

        private void btnsavearticle_Click(object sender, EventArgs e)
        {
            DialogResult result = RadMessageBox.Show("Are you sure to save this record", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
            if (result.Equals(DialogResult.No))
            {
                return;
            }

            try
            {
                //check if all the fields are inserted
                if (txtarticleid.Text != "" && txtarticledesc.Text != "")
                {
                    btndeletearticle.Enabled = false;
                    if (btnsavearticle.Text == save)
                    {

                        //get count of id
                        SqlCommand cmd1 = new SqlCommand("select count(*) from ARTICLE_DB where V_ARTICLE_ID='" + txtarticleid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get count desc
                        SqlCommand cmd2 = new SqlCommand("select count(*) from ARTICLE_DB where V_ARTICLE_DESC='" + txtarticledesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc already exists
                        if (i == 0 && k == 0)
                        {
                            //insert into article db
                            SqlCommand cmd = new SqlCommand("insert into ARTICLE_DB values('" + txtarticleid.Text + "','" + txtarticledesc.Text + "','" + txtarticledesc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_Article();   //get article master

                            txtarticleid.ReadOnly = false;
                            ClearData_Article();   //clear all fields
                        }
                        else
                        {
                            //select the row if exists
                            for (int j = 0; j < dgvarticle.Rows.Count; j++)
                            {
                                if (dgvarticle.Rows[j].Cells[0].Value.ToString().Equals(txtarticleid.Text) || dgvarticle.Rows[j].Cells[1].Value.ToString().Equals(txtarticledesc.Text))
                                {
                                    dgvarticle.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Article is already in use";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsavearticle.Text == update)
                    {
                        //get dec count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from ARTICLE_DB where V_ARTICLE_DESC='" + txtarticledesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc already exists or same desc
                        if (k == 0 || articledesc == txtarticledesc.Text)
                        {
                            //update article db
                            SqlCommand cmd = new SqlCommand("Update ARTICLE_DB set V_ARTICLE_SIZE='" + txtarticledesc.Text + "',V_ARTICLE_DESC='" + txtarticledesc.Text + "' where V_ARTICLE_ID='" + txtarticleid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_Article();   //get article master

                            txtarticleid.ReadOnly = false;
                            btnsavearticle.Text = save;
                            ClearData_Article();  //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvarticle.Rows.Count; j++)
                            {
                                if (dgvarticle.Rows[j].Cells[1].Value.ToString().Equals(txtarticledesc.Text))
                                {
                                    dgvarticle.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Article is already in use";
                                    return;
                                }
                            }
                        }

                        //String strSql = "";
                        ////Hanafi|21-07-2021| allow save record even if duplicate article desc
                        //strSql = "select count(*) from ARTICLE_DB where V_ARTICLE_ID='" + txtarticleid.Text + "'";
                        //SqlCommand cmd2 = new SqlCommand(strSql, dc.con);
                        //Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        ////check if desc already exists or same desc
                        //if (k != 0 )
                        //{
                        //    //update article db
                        //    SqlCommand cmd = new SqlCommand("Update ARTICLE_DB set V_ARTICLE_SIZE='" + txtarticledesc.Text + "',V_ARTICLE_DESC='" + txtarticledesc.Text + "' where V_ARTICLE_ID='" + txtarticleid.Text + "'", dc.con);
                        //    cmd.ExecuteNonQuery();

                        //    lblmsg.Text = "Records Updated";
                        //    RefereshGrid_Article();   //get article master

                        //    txtarticleid.ReadOnly = false;
                        //    btnsavearticle.Text = save;
                        //    ClearData_Article();  //clear all fields
                        //}
                        //else
                        //{
                        //    //select row if exists
                        //    for (int j = 0; j < dgvarticle.Rows.Count; j++)
                        //    {
                        //        if (dgvarticle.Rows[j].Cells[1].Value.ToString().Equals(txtarticleid.Text))
                        //        {
                        //            dgvarticle.Rows[j].IsSelected = true;
                        //            lblmsg.Text = "Article is already in use";
                        //            return;
                        //        }
                        //    }
                        //}
                    }
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }

                btnsavearticle.ForeColor = Color.Lime;
            }
            catch (Exception ex)
            {
                lblcolorrgb.Text = ex.Message;
            }
            btnsetsequence.Enabled = false;
        }

        //clear all fields
        public void ClearData_Article()
        {
            txtarticleid.Text = "";
            txtarticledesc.Text = "";
        }

        private void btndeletearticle_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected article
                SqlCommand cmd = new SqlCommand("Delete from ARTICLE_DB where V_ARTICLE_ID='" + txtarticleid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                lblmsg.Text = "Record Deleted";
                RefereshGrid_Article();   //get article master

                txtarticleid.ReadOnly = false;
                btnsavearticle.Text = save;
                ClearData_Article();   //clear all fields

                btndeletearticle.Enabled = false;
                btnsetsequence.Enabled = false;
                btnsavearticle.ForeColor = Color.Lime;
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Article Id is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        private void btneditarticle_Click(object sender, EventArgs e)
        {
            RowSelected_Article();    //get the selected article
        }

        public void RowSelected_Article()
        {
            //get the selected article
            if (dgvarticle.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String articleId = dgvarticle.SelectedRows[0].Cells[0].Value + string.Empty;
                String articleDesc = dgvarticle.SelectedRows[0].Cells[1].Value + string.Empty;

                txtarticleid.Text = articleId;
                txtarticledesc.Text = articleDesc;

                txtarticleid.ReadOnly = true;
                btnsavearticle.Text = update;
                btndeletearticle.Enabled = true;
                btnsetsequence.Enabled = true;
                btnsavearticle.ForeColor = Color.Red;
                articledesc = articleDesc;
            }
        }

        private void dgvarticle_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Article();   //get the selected article
        }

        private void btnsetsequence_Click(object sender, EventArgs e)
        {
            //cmbdesignarticle.Text = txtarticledesc.Text;
            cmbdesignarticle.SelectedValue = txtarticleid.Text;
            vpagemasters.SelectedPage = pagedesignsequence;
            Design_Selected();    //show design sequence for the article
        }

        private void btnsavesize_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtsizeid.Text != "" && txtsizedesc.Text != "")
                {
                    btndeletesize.Enabled = false;
                    if (btnsavesize.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from SIZE_DB where V_SIZE_ID='" + txtsizeid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from SIZE_DB where V_SIZE_DESC='" + txtsizedesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc already exists
                        if (i == 0 && k == 0)
                        {
                            //insert into size db
                            SqlCommand cmd = new SqlCommand("insert into SIZE_DB values('" + txtsizeid.Text + "','" + txtsizedesc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_Size();   //get size master

                            txtsizeid.ReadOnly = false;
                            ClearData_Size();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvsize.Rows.Count; j++)
                            {
                                if (dgvsize.Rows[j].Cells[0].Value.ToString().Equals(txtsizeid.Text) || dgvsize.Rows[j].Cells[1].Value.ToString().Equals(txtsizedesc.Text))
                                {
                                    dgvsize.Rows[j].IsSelected = true;
                                    lblmsg.Text = "SIZE Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsavesize.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from SIZE_DB where V_SIZE_DESC='" + txtsizedesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || sizedesc == txtsizedesc.Text)
                        {
                            //update size db
                            SqlCommand cmd = new SqlCommand("Update SIZE_DB set V_SIZE_DESC='" + txtsizedesc.Text + "' where V_SIZE_ID='" + txtsizeid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_Size();   //get size master

                            txtsizeid.ReadOnly = false;
                            btnsavesize.Text = save;
                            ClearData_Size();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvsize.Rows.Count; j++)
                            {
                                if (dgvsize.Rows[j].Cells[1].Value.ToString().Equals(txtsizedesc.Text))
                                {
                                    dgvsize.Rows[j].IsSelected = true;
                                    lblmsg.Text = "SIZE Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    btnsavesize.ForeColor = Color.Lime;
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear all fields
        public void ClearData_Size()
        {
            txtsizeid.Text = "";
            txtsizedesc.Text = "";
        }

        private void btndeletesize_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected size
                SqlCommand cmd = new SqlCommand("Delete from SIZE_DB where V_SIZE_ID='" + txtsizeid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                lblmsg.Text = "Record Deleted";
                RefereshGrid_Size();  //get size master

                txtsizeid.ReadOnly = false;
                btnsavesize.Text = save;
                ClearData_Size();   //clear all fields

                btndeletesize.Enabled = false;
                btnsavesize.ForeColor = Color.Lime;
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Size ID is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        private void btneditsize_Click(object sender, EventArgs e)
        {
            RowSelected_Size();   //get the selected size
        }

        public void RowSelected_Size()
        {
            //get the selected size
            if (dgvsize.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String sizeId = dgvsize.SelectedRows[0].Cells[0].Value + string.Empty;
                String sizeDesc = dgvsize.SelectedRows[0].Cells[1].Value + string.Empty;

                txtsizeid.Text = sizeId;
                txtsizedesc.Text = sizeDesc;

                txtsizeid.ReadOnly = true;
                btnsavesize.Text = update;
                btndeletesize.Enabled = true;
                btnsavesize.ForeColor = Color.Red;
                sizedesc = sizeDesc;
            }
        }

        private void dgvsize_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Size();   //get the selected size
        }

        private void btnsavecontractor_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtcontractorid.Text != "" && txtcontractorname.Text != "")
                {
                    btndeletecontractor.Enabled = false;
                    if (btnsavecontractor.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from CONTRACTOR_DB where V_CONTRACTOR_ID='" + txtcontractorid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from CONTRACTOR_DB where V_CONTRACTOR_NAME='" + txtcontractorname.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc already exists
                        if (i == 0 && k == 0)
                        {
                            //insert into contractor db
                            SqlCommand cmd = new SqlCommand("insert into CONTRACTOR_DB values('" + txtcontractorid.Text + "','" + txtcontractorname.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_Contractor();    //get contractor master

                            txtcontractorid.ReadOnly = false;
                            ClearData_Contractor();   //clear all fields
                        } 
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dvgcontractor.Rows.Count; j++)
                            {
                                if (dvgcontractor.Rows[j].Cells[0].Value.ToString().Equals(txtcontractorid.Text) || dvgcontractor.Rows[j].Cells[1].Value.ToString().Equals(txtcontractorname.Text))
                                {
                                    dvgcontractor.Rows[j].IsSelected = true;
                                    lblmsg.Text = "CONTRACTOR Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsavecontractor.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from CONTRACTOR_DB where V_CONTRACTOR_NAME='" + txtcontractorname.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check id desc already exists or same desc
                        if (k == 0 || contractordesc == txtcontractorname.Text)
                        {
                            //update contractor db
                            SqlCommand cmd = new SqlCommand("Update CONTRACTOR_DB set V_CONTRACTOR_NAME='" + txtcontractorname.Text + "' where V_CONTRACTOR_ID='" + txtcontractorid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_Contractor();   //get contractor master

                            txtcontractorid.ReadOnly = false;
                            btnsavecontractor.Text = save;
                            ClearData_Contractor();   //clear all fields
                        } 
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dvgcontractor.Rows.Count; j++)
                            {
                                if (dvgcontractor.Rows[j].Cells[1].Value.ToString().Equals(txtcontractorname.Text))
                                {
                                    dvgcontractor.Rows[j].IsSelected = true;
                                    lblmsg.Text = "CONTRACTOR Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    btnsavecontractor.ForeColor = Color.Lime;
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear all fields
        public void ClearData_Contractor()
        {
            txtcontractorid.Text = "";
            txtcontractorname.Text = "";
        }

        private void btndeletecontractor_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected contractor
                SqlCommand cmd = new SqlCommand("Delete from CONTRACTOR_DB where V_CONTRACTOR_ID='" + txtcontractorid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                lblmsg.Text = "Record Deleted";
                RefereshGrid_Contractor();   //get contractor master

                txtcontractorid.ReadOnly = false;
                btnsavecontractor.Text = save;
                ClearData_Contractor();   //clear all fiedls

                btndeletecontractor.Enabled = false;
                btnsavecontractor.ForeColor = Color.Lime;
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Contractor Id is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        private void btneditcontractor_Click(object sender, EventArgs e)
        {
            RowSelected_Contractor();    //get the selected contractor
        }

        public void RowSelected_Contractor()
        {
            //get th selected contractor
            if (dvgcontractor.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String contractorId = dvgcontractor.SelectedRows[0].Cells[0].Value + string.Empty;
                String contractorName = dvgcontractor.SelectedRows[0].Cells[1].Value + string.Empty;

                txtcontractorid.Text = contractorId;
                txtcontractorname.Text = contractorName;
                txtcontractorid.ReadOnly = true;

                btnsavecontractor.Text = update;
                btndeletecontractor.Enabled = true;
                btnsavecontractor.ForeColor = Color.Red;
                contractordesc = contractorName;
            }
        }

        private void dvgcontractor_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Contractor();   //get the selected contractor
        }

        private void btnsaveemp_Click(object sender, EventArgs e)
        {
            try
            {
                //check if basic pay is integer
                Regex r = new Regex("^[0-9]*$");
                if (!r.IsMatch(txtbasicpay.Text))
                {
                    lblmsg.Text = "Invalid Basic Pay value. Example : 15000";
                    txtbasicpay.Text = "";
                    return;
                }

                //check if all the fields are inserted
                if (txtempid.Text != "" && txtfirstname.Text != "" && txtledname.Text != "" && txtaddress.Text != "" && txtposition.Text != "" && txtrfid.Text != "" && cmbsex.Text != "--SELECT--" && cmbskill.Text != "--SELECT--" && cmbcontractor.Text != "--SELECT--" && txtbasicpay.Text != "" && cmbstatus.Text != "--SELECT--")
                {
                    btndeleteemp.Enabled = false;

                    //convert image to byte[]
                    MemoryStream ms = new MemoryStream();
                    pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                    byte[] photo_aray = new byte[ms.Length];
                    ms.Position = 0;
                    ms.Read(photo_aray, 0, photo_aray.Length);

                    //get the contractor id
                    SqlDataAdapter sda = new SqlDataAdapter("Select V_CONTRACTOR_ID from CONTRACTOR_DB where V_CONTRACTOR_NAME='" + cmbcontractor.Text + "'", dc.con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    String contractor = "";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        contractor = dt.Rows[i][0].ToString();
                    }

                    if (btnsaveemp.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("Select count(*) from EMPLOYEE where V_EMP_ID='" + txtempid.Text + "'", dc.con);
                        int count = int.Parse(cmd1.ExecuteScalar() + "");

                        //get rfid count
                        cmd1 = new SqlCommand("Select count(*) from EMPLOYEE where V_RFID='" + txtrfid.Text + "'", dc.con);
                        int count1 = int.Parse(cmd1.ExecuteScalar() + "");

                        //check if id and rfid already exists
                        if (count != 0 || count1 != 0)
                        {
                            for (int i = 0; i < dgvemployee.Rows.Count; i++)
                            {
                                if (dgvemployee.Rows[i].Cells[0].Value.ToString().Equals(txtempid.Text))
                                {
                                    dgvemployee.Rows[i].IsSelected = true;
                                    lblmsg.Text = "Employee Already Exists";
                                    return;
                                }
                            }
                        }

                        //get employee group count
                        cmd1 = new SqlCommand("Select count(*) from EMPLOYEE_GROUP_CATEGORY where V_GROUP_ID='" + txtempid.Text + "'", dc.con);
                        count = int.Parse(cmd1.ExecuteScalar() + "");
                        if (count != 0)
                        {
                            lblmsg.Text = "ID Already used for Employee Groups";
                            return;
                        }

                        //insert into employee
                        SqlCommand cmd = new SqlCommand("insert into Employee values(@empid,@firstname,@midname,@lastname,@ledname,@add,@zip,@pos,@tel,@sex,@dob,@img,@skill,@rfid,@login,@contractor,@basicpay)", dc.con);
                        cmd.Parameters.AddWithValue("@empid", txtempid.Text);
                        cmd.Parameters.AddWithValue("@firstname", txtfirstname.Text);
                        cmd.Parameters.AddWithValue("@midname", txtmidname.Text);
                        cmd.Parameters.AddWithValue("@lastname", txtlastname.Text);
                        cmd.Parameters.AddWithValue("@ledname", txtledname.Text);
                        cmd.Parameters.AddWithValue("@add", txtaddress.Text);
                        cmd.Parameters.AddWithValue("@zip", txtzipcode.Text);
                        cmd.Parameters.AddWithValue("@pos", txtposition.Text);
                        cmd.Parameters.AddWithValue("@tel", txttelno.Text);
                        cmd.Parameters.AddWithValue("@sex", cmbsex.Text);
                        cmd.Parameters.AddWithValue("@dob", dtdob.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@img", photo_aray);
                        cmd.Parameters.AddWithValue("@skill", cmbskill.Text);
                        cmd.Parameters.AddWithValue("@rfid", txtrfid.Text);
                        cmd.Parameters.AddWithValue("@login", cmbstatus.Text);
                        cmd.Parameters.AddWithValue("@contractor", contractor);
                        cmd.Parameters.AddWithValue("@basicpay", txtbasicpay.Text);
                        cmd.ExecuteNonQuery();

                        lblmsg.Text = "Records Saved";

                        RefereshGrid_Employee();   //get employee master
                    }
                    if (btnsaveemp.Text == update)
                    {
                        //get rfid count
                        SqlCommand cmd1 = new SqlCommand("Select count(*) from EMPLOYEE where V_RFID='" + txtrfid.Text + "'", dc.con);
                        int count = int.Parse(cmd1.ExecuteScalar() + "");
                        if (count != 0 && rfid != txtrfid.Text)
                        {
                            lblmsg.Text = "Employee RFID already Exists";
                            return;
                        }

                        //update employee
                        SqlCommand cmd = new SqlCommand("Update Employee set V_FIRST_NAME=@firstname,V_MIDDLE_NAME=@midname,V_LAST_NAME=@lastname,V_LED_NAME=@ledname,V_ADDRESS=@add,V_ZIP=@zip,V_POSITION=@pos,V_TEL_NO=@tel,V_SEX=@sex,D_DOB=@dob,IMG_IMAGE=@img,V_SKILL_LEVEL=@skill,V_RFID=@rfid,V_CONTRACTOR_ID=@contractor,I_BASIC_PAY=@basicpay,V_LOGIN_STATUS=@login where V_EMP_ID='" + txtempid.Text + "'", dc.con);
                        cmd.Parameters.AddWithValue("@firstname", txtfirstname.Text);
                        cmd.Parameters.AddWithValue("@midname", txtmidname.Text);
                        cmd.Parameters.AddWithValue("@lastname", txtlastname.Text);
                        cmd.Parameters.AddWithValue("@ledname", txtledname.Text);
                        cmd.Parameters.AddWithValue("@add", txtaddress.Text);
                        cmd.Parameters.AddWithValue("@zip", txtzipcode.Text);
                        cmd.Parameters.AddWithValue("@pos", txtposition.Text);
                        cmd.Parameters.AddWithValue("@tel", txttelno.Text);
                        cmd.Parameters.AddWithValue("@sex", cmbsex.Text);
                        cmd.Parameters.AddWithValue("@dob", dtdob.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@img", photo_aray);
                        cmd.Parameters.AddWithValue("@skill", cmbskill.Text);
                        cmd.Parameters.AddWithValue("@rfid", txtrfid.Text);
                        cmd.Parameters.AddWithValue("@contractor", contractor);
                        cmd.Parameters.AddWithValue("@basicpay", txtbasicpay.Text);
                        cmd.Parameters.AddWithValue("@login", cmbstatus.Text);
                        cmd.ExecuteNonQuery();

                        lblmsg.Text = "Records Updated";
                        RefereshGrid_Employee();   //get employee master
                        btnsaveemp.Text = save;
                    }

                    txtempid.ReadOnly = false;
                    ClearData_Employee();   //clear all fields

                    btnsaveemp.ForeColor = Color.Lime;
                    RefereshGrid_EmployeeGroup();   //get employee group
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear all fields
        public void ClearData_Employee()
        {
            txtempid.Text = "";
            txtfirstname.Text = "";
            txtmidname.Text = "";
            txtlastname.Text = "";
            txtledname.Text = "";
            txtaddress.Text = "";
            txtzipcode.Text = "";
            txttelno.Text = "";
            cmbsex.Text = "--SELECT--";
            dtdob.Text = "";
            txtrfid.Text = "";
            txtposition.Text = "";
            txtbasicpay.Text = "";
            cmbskill.Text = "--SELECT--";
            cmbcontractor.Text = "--SELECT--";
            cmbstatus.Text = "--SELECT--";

            btnsaveemp.ForeColor = Color.Lime;
            pictureBox1.Image = null;
            pictureBox1.Image = Properties.Resources.Empty;
        }

        private void btndeleteemp_Click(object sender, EventArgs e)
        {
            try
            {
                //check if employee already has production details
                 SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM HANGER_HISTORY where EMP_ID='" + txtempid.Text + "'", dc.con);
                int count = int.Parse(cmd.ExecuteScalar() + "");
                if (count == 0)
                {
                    //delete the selected employee
                    cmd = new SqlCommand("Delete from EMPLOYEE where V_EMP_ID='" + txtempid.Text + "'", dc.con);
                    cmd.ExecuteNonQuery();
                    lblmsg.Text = "Record Deleted";
                }
                else
                {
                    string empStatus = "";
                    SqlCommand cmd2 = new SqlCommand("SELECT V_LOGIN_STATUS FROM EMPLOYEE WHERE V_EMP_ID = '" + txtempid.Text + "'", dc.con);
                    SqlDataReader sdr2 = cmd2.ExecuteReader();
                    if (sdr2.Read())
                    {                       
                        empStatus = sdr2.GetValue(0).ToString();             
                    }

                    if (empStatus == "Retired")
                    {
                        //delete the selected employee
                        cmd = new SqlCommand("Delete from EMPLOYEE where V_EMP_ID='" + txtempid.Text + "'", dc.con);
                        cmd.ExecuteNonQuery();
                        lblmsg.Text = "Record Deleted";
                    }
                    else
                    {
                        lblmsg.Text = "Employee Already Used for Production";
                    }

                    
                }

                RefereshGrid_Employee();   //get employee master
                txtempid.ReadOnly = false;
                btnsaveemp.Text = save;
                btndeleteemp.Enabled = false;

                ClearData_Employee();   //clear all fields
                RefereshGrid_EmployeeGroup();   //get employee group master
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Employee Id is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        private void btneditemp_Click(object sender, EventArgs e)
        {
            RowSelected_Employee();    //get the selected employee
        }

        public void RowSelected_Employee()
        {
            try
            {
                //get the selected employee
                if (dgvemployee.SelectedRows.Count > 0) // make sure user select at least 1 row 
                {
                    String contractor = "";
                    String empid = dgvemployee.SelectedRows[0].Cells[0].Value + string.Empty;

                    //get all the details of the employee
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Employee where V_EMP_ID = '" + empid + "'", dc.con);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        byte[] a = null;
                        txtempid.Text = sdr.GetValue(1).ToString();
                        txtfirstname.Text = sdr.GetValue(2).ToString();
                        txtmidname.Text = sdr.GetValue(3).ToString();
                        txtlastname.Text = sdr.GetValue(4).ToString();
                        txtledname.Text = sdr.GetValue(5).ToString();
                        txtaddress.Text = sdr.GetValue(6).ToString();
                        txtzipcode.Text = sdr.GetValue(7).ToString();
                        txtposition.Text = sdr.GetValue(8).ToString();
                        txttelno.Text = sdr.GetValue(9).ToString();
                        cmbsex.Text = sdr.GetValue(10).ToString();
                        dtdob.Text = sdr.GetValue(11).ToString();
                        if (sdr.GetValue(12).ToString() != "")
                        {
                            a = (byte[])(sdr.GetValue(12));
                        }
                        cmbskill.Text = sdr.GetValue(13).ToString();
                        txtrfid.Text = sdr.GetValue(14).ToString();
                        cmbstatus.Text = sdr.GetValue(15).ToString();
                        contractor = sdr.GetValue(16).ToString();
                        txtbasicpay.Text = sdr.GetValue(17).ToString();
                        rfid = txtrfid.Text;
                        sdr.Close();

                        if (a != null)
                        {
                            ////memory stream to read the image from the byte array and display it in the picture box
                            MemoryStream ms = new MemoryStream(a);
                            pictureBox1.Image = Image.FromStream(ms);
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
                        }
                        btnsaveemp.Text = update;
                        btndeleteemp.Enabled = true;
                        txtempid.ReadOnly = true;
                        btnsaveemp.ForeColor = Color.Red;
                    }

                    //get the contractor name
                    SqlDataAdapter sda = new SqlDataAdapter("Select V_CONTRACTOR_NAME from CONTRACTOR_DB where V_CONTRACTOR_ID='" + contractor + "'", dc.con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    sda.Dispose();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        contractor = dt.Rows[i][0].ToString();
                    }

                    cmbcontractor.Text = contractor;
                }
            }
            catch (Exception ex)
            {
                RadMessageBox.Show(ex + "");
            }
        }

        private void dgvemployee_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Employee();  //get the selected employee
        }

        private void btnsavecustomer_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtcustomerid.Text != "" && txtcustomername.Text != "" && txtcustomerdest.Text != "")
                {
                    btndeletecustomer.Enabled = false;
                    if (btnsavecustomer.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from CUSTOMER_DB where V_CUSTOMER_ID='" + txtcustomerid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from CUSTOMER_DB where V_CUSTOMER_NAME='" + txtcustomername.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc already exists
                        if (i == 0 && k == 0)
                        {
                            //insert into cuatomer db
                            SqlCommand cmd = new SqlCommand("insert into CUSTOMER_DB values('" + txtcustomerid.Text + "','" + txtcustomername.Text + "','" + txtcustomerdest.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_Customer();    //get contomer master

                            txtcustomerid.ReadOnly = false;
                            ClearData_Customer();   //clear all fields
                        }
                        else
                        {
                            //select the row if exists
                            for (int j = 0; j < dgvcustomer.Rows.Count; j++)
                            {
                                if (dgvcustomer.Rows[j].Cells[0].Value.ToString().Equals(txtcustomerid.Text) || dgvcustomer.Rows[j].Cells[1].Value.ToString().Equals(txtcustomername.Text))
                                {
                                    dgvcustomer.Rows[j].IsSelected = true;
                                    lblmsg.Text = "CUSTOMER Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsavecustomer.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from CUSTOMER_DB where V_CUSTOMER_NAME='" + txtcustomername.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || customerdesc == txtcustomername.Text)
                        {
                            //update customer
                            SqlCommand cmd = new SqlCommand("Update CUSTOMER_DB set V_CUSTOMER_NAME='" + txtcustomername.Text + "',V_CUSTOMER_ORIGIN='" + txtcustomerdest.Text + "' where V_CUSTOMER_ID='" + txtcustomerid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_Customer();    //get customer master

                            txtcustomerid.ReadOnly = false;
                            btnsavecustomer.Text = save;
                            ClearData_Customer();   //clear all fields
                        }
                        else
                        {
                            //select the row if exists
                            for (int j = 0; j < dgvcustomer.Rows.Count; j++)
                            {
                                if (dgvcustomer.Rows[j].Cells[0].Value.ToString().Equals(txtcustomerid.Text) || dgvcustomer.Rows[j].Cells[1].Value.ToString().Equals(txtcustomername.Text))
                                {
                                    dgvcustomer.Rows[j].IsSelected = true;
                                    lblmsg.Text = "CUSTOMER Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    btnsavecustomer.ForeColor = Color.Lime;
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear all fields
        public void ClearData_Customer()
        {
            txtcustomerid.Text = "";
            txtcustomername.Text = "";
            txtcustomerdest.Text = "";
            btnsavecustomer.ForeColor = Color.Lime;
        }

        private void btneditcustomer_Click(object sender, EventArgs e)
        {
            RowSelected_Customer();     //get the selected customer
        }

        public void RowSelected_Customer()
        {
            if (dgvcustomer.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String customerId = dgvcustomer.SelectedRows[0].Cells[0].Value + string.Empty;
                String customerName = dgvcustomer.SelectedRows[0].Cells[1].Value + string.Empty;
                String customerOrigin = dgvcustomer.SelectedRows[0].Cells[2].Value + string.Empty;

                txtcustomerid.Text = customerId;
                txtcustomername.Text = customerName;
                txtcustomerdest.Text = customerOrigin;

                txtcustomerid.ReadOnly = true;
                btnsavecustomer.Text = update;
                btndeletecustomer.Enabled = true;
                btnsavecustomer.ForeColor = Color.Red;
                customerdesc = customerName;
            }
        }

        private void btndeletecustomer_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected customer
                SqlCommand cmd = new SqlCommand("Delete from CUSTOMER_DB where V_CUSTOMER_ID='" + txtcustomerid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                lblmsg.Text = "Record Deleted";
                RefereshGrid_Customer();    //get customer master

                txtcustomerid.ReadOnly = false;
                btnsavecustomer.Text = save;
                ClearData_Customer();    //clear all fields

                btndeletecustomer.Enabled = false;
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Customer Id is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        private void dgvcustomer_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Customer();    //get the selected customer
        }

        private void btnsaveoperation_Click(object sender, EventArgs e)
        {
            try
            {
                Regex r = new Regex("^[0-9]*$");
                if (!r.IsMatch(txtsam.Text))
                {
                    lblmsg.Text = "Invalid SAM value. Example : 35";
                    return;
                }

                //check if piecerate is in correct format
                r = new Regex("^[0-9]{1,4}([.][0-9]{1,4})?$");
                if (!r.IsMatch(txtpiecerate.Text))
                {
                    lblmsg.Text = "Invalid Piece Rate value.  Example : 1.2000";
                    txtpiecerate.Text = "";
                    return;
                }

                //check if overtime is in correct format
                if (!r.IsMatch(txtovertime.Text))
                {
                    lblmsg.Text = "Invalid OverTime Rate value.  Example : 1.5000";
                    txtovertime.Text = "";
                    return;
                }

                //check if sam is 0
                if (txtsam.Text == "0")
                {
                    lblmsg.Text = "Invalid SAM value. Example : 35";
                    txtsam.Text = "";
                    return;
                }

                //check if piecerate is 0
                if (txtpiecerate.Text == "0")
                {
                    lblmsg.Text = "Invalid Piece Rate value.  Example : 1.2000";
                    txtpiecerate.Text = "";
                    return;
                }

                //check if overtime rate is 0
                if (txtovertime.Text == "0")
                {
                    lblmsg.Text = "Invalid OverTime Rate value.  Example : 1.5000";
                    txtovertime.Text = "";
                    return;
                }

                //check if all the fields are inserted
                if (txtoperationid.Text != "" && txtoperationdesc.Text != "" && txtpiecerate.Text != "" && txtsam.Text != "" && txtovertime.Text != "" && cmbmachine.Text != "--SELECT--")
                {
                    String machine = "";
                    //get machine id
                    SqlCommand cmd3 = new SqlCommand("select V_MACHINE_ID from MACHINE_DB where V_MACHINE_DESC='" + cmbmachine.Text + "'", dc.con);
                    SqlDataReader sdr = cmd3.ExecuteReader();
                    if (sdr.Read())
                    {
                        machine = sdr.GetValue(0).ToString();
                    }
                    sdr.Close();

                    btndeleteoperation.Enabled = false;
                    if (btnsaveoperation.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from OPERATION_DB where V_OPERATION_CODE='" + txtoperationid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from OPERATION_DB where V_OPERATION_DESC='" + txtcustomername.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc already exists
                        if (i == 0 && k == 0)
                        {
                            //insert into operation db
                            SqlCommand cmd = new SqlCommand("insert into OPERATION_DB values('" + txtoperationid.Text + "','" + txtoperationdesc.Text + "','" + txtpiecerate.Text + "','" + txtsam.Text + "','" + machine + "','" + txtovertime.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_Operation();   //get operation master

                            txtoperationid.ReadOnly = false;
                            ClearData_Operation();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvoperation.Rows.Count; j++)
                            {
                                if (dgvoperation.Rows[j].Cells[0].Value.ToString().Equals(txtoperationid.Text) || dgvoperation.Rows[j].Cells[1].Value.ToString().Equals(txtoperationdesc.Text))
                                {
                                    dgvoperation.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Operation Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsaveoperation.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from OPERATION_DB where V_OPERATION_DESC='" + txtoperationdesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //chekc desc exists or same desc
                        if (k == 0 || operationdesc == txtoperationdesc.Text)
                        {
                            //update operation
                            SqlCommand cmd = new SqlCommand("Update OPERATION_DB set V_OPERATION_DESC='" + txtoperationdesc.Text + "',D_PIECERATE='" + txtpiecerate.Text + "',D_SAM='" + txtsam.Text + "',V_MACHINE_ID='" + machine + "',D_OVERTIME_RATE='" + txtovertime.Text + "' where V_OPERATION_CODE='" + txtoperationid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_Operation();  //get operation master

                            txtoperationid.ReadOnly = false;
                            btnsaveoperation.Text = save;
                            ClearData_Operation();    //clear all fields
                        }
                        else
                        {
                            //select roe if exists
                            for (int j = 0; j < dgvoperation.Rows.Count; j++)
                            {
                                if (dgvoperation.Rows[j].Cells[1].Value.ToString().Equals(txtoperationdesc.Text))
                                {
                                    dgvoperation.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Operation Already Exists";
                                    return;
                                }
                            }
                        }
                    }

                    btnsaveoperation.ForeColor = Color.Lime;
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear all fields
        public void ClearData_Operation()
        {
            txtoperationid.Text = "";
            txtoperationdesc.Text = "";
            txtpiecerate.Text = "";
            txtsam.Text = "";
            txtovertime.Text = "";
            btnsaveoperation.ForeColor = Color.Lime;
        }

        private void btndeleteoperation_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected opretaion
                SqlCommand cmd = new SqlCommand("Delete from OPERATION_DB where V_OPERATION_CODE='" + txtoperationid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                lblmsg.Text = "Record Deleted";
                RefereshGrid_Operation();   //get the operation master

                txtoperationid.ReadOnly = false;
                btnsaveoperation.Text = save;
                ClearData_Operation();   //clear all fields

                btndeleteoperation.Enabled = false;
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Operation Code is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        private void btneditoperation_Click(object sender, EventArgs e)
        {
            RowSelected_Operation();  //get the selected operation
        }
        public void RowSelected_Operation()
        {
            //get the selected operation
            if (dgvoperation.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                txtoperationid.Text = dgvoperation.SelectedRows[0].Cells[0].Value + string.Empty;
                txtoperationdesc.Text = dgvoperation.SelectedRows[0].Cells[1].Value + string.Empty;
                txtpiecerate.Text = dgvoperation.SelectedRows[0].Cells[2].Value + string.Empty;
                txtovertime.Text = dgvoperation.SelectedRows[0].Cells[3].Value + string.Empty;
                txtsam.Text = dgvoperation.SelectedRows[0].Cells[4].Value + string.Empty;
                cmbmachine.Text = dgvoperation.SelectedRows[0].Cells[5].Value + string.Empty;

                txtoperationid.ReadOnly = true;
                btnsaveoperation.Text = update;
                btndeleteoperation.Enabled = true;
                operationdesc = txtoperationdesc.Text;
                btnsaveoperation.ForeColor = Color.Red;
            }
        }

        private void dgvoperation_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Operation();// get the selected operation
        }

        private void btnsaveuser1_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtuser1id.Text != "" && txtuser1desc.Text != "")
                {
                    btndeleteuser1.Enabled = false;
                    if (btnsaveuser1.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from USER_DEF1_DB where V_USER_ID='" + txtuser1id.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF1_DB where V_DESC='" + txtuser1desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check id and desc already exists
                        if (i == 0 && k == 0)
                        {
                            //insert into user def1
                            SqlCommand cmd = new SqlCommand("insert into USER_DEF1_DB values('" + txtuser1id.Text + "','" + txtuser1desc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_User1();    //get the master

                            txtuser1id.ReadOnly = false;
                            ClearData_User1();   //clear fields
                        }
                        else
                        {
                            //select the row if exists
                            for (int j = 0; j < dgvuser1.Rows.Count; j++)
                            {
                                if (dgvuser1.Rows[j].Cells[0].Value.ToString().Equals(txtuser1id.Text) || dgvuser1.Rows[j].Cells[1].Value.ToString().Equals(txtuser1desc.Text))
                                {
                                    dgvuser1.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser1master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsaveuser1.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF1_DB where V_DESC='" + txtuser1desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || u1desc == txtuser1desc.Text)
                        {
                            //update userdef1
                            SqlCommand cmd = new SqlCommand("Update USER_DEF1_DB set V_DESC='" + txtuser1desc.Text + "' where V_USER_ID='" + txtuser1id.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_User1();   //get the master

                            txtuser1id.ReadOnly = false;
                            btnsaveuser1.Text = save;
                            ClearData_User1();   //clear fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser1.Rows.Count; j++)
                            {
                                if (dgvuser1.Rows[j].Cells[1].Value.ToString().Equals(txtuser1desc.Text))
                                {
                                    dgvuser1.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser1master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    btnsaveuser1.ForeColor = Color.Lime;
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear fields
        public void ClearData_User1()
        {
            txtuser1id.Text = "";
            txtuser1desc.Text = "";
            btnsaveuser1.ForeColor = Color.Lime;
        }

        private void btndeleteuser1_Click(object sender, EventArgs e)
        {
            //delete the selected userdef1
            SqlCommand cmd = new SqlCommand("Delete from USER_DEF1_DB where V_USER_ID='" + txtuser1id.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            lblmsg.Text = "Record Deleted";
            RefereshGrid_User1();   //get the master

            txtuser1id.ReadOnly = false;
            btnsaveuser1.Text = save;
            ClearData_User1();   //clear fields

            btndeleteuser1.Enabled = false;
        }

        private void btnedituser1_Click(object sender, EventArgs e)
        {
            RowSelected_User1();   //get the selected row
        } 

        public void RowSelected_User1()
        {
            //get the selected row
            if (dgvuser1.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String Id = dgvuser1.SelectedRows[0].Cells[0].Value + string.Empty;
                String Desc = dgvuser1.SelectedRows[0].Cells[1].Value + string.Empty;

                txtuser1id.Text = Id;
                txtuser1desc.Text = Desc;

                txtuser1id.ReadOnly = true;
                btnsaveuser1.Text = update;
                btndeleteuser1.Enabled = true;
                u1desc = Desc;
            }
        }

        private void dgvuser1_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_User1();   //get the selected row
        }

        private void btnsaveuser2_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtuser2id.Text != "" && txtuser2desc.Text != "")
                {
                    btndeleteuser2.Enabled = false;
                    if (btnsaveuser2.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from USER_DEF2_DB where V_USER_ID='" + txtuser2id.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF2_DB where V_DESC='" + txtuser2desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //chekc id and desc alreadt exists
                        if (i == 0 && k == 0)
                        {
                            //insert into userdef2
                            SqlCommand cmd = new SqlCommand("insert into USER_DEF2_DB values('" + txtuser2id.Text + "','" + txtuser2desc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_User2();   //get the master

                            txtuser2id.ReadOnly = false;
                            ClearData_User2();   //clear fields
                        }
                        else
                        {
                            //select roe if exists
                            for (int j = 0; j < dgvuser2.Rows.Count; j++)
                            {
                                if (dgvuser2.Rows[j].Cells[0].Value.ToString().Equals(txtuser2id.Text) || dgvuser2.Rows[j].Cells[1].Value.ToString().Equals(txtuser2desc.Text))
                                {
                                    dgvuser2.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser2master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsaveuser2.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF2_DB where V_DESC='" + txtuser2desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check desc exists or same desc
                        if (k == 0 || u2desc == txtuser2desc.Text)
                        {
                            //update userdef2
                            SqlCommand cmd = new SqlCommand("Update USER_DEF2_DB set V_DESC='" + txtuser2desc.Text + "' where V_USER_ID='" + txtuser2id.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_User2();   //get the master

                            txtuser2id.ReadOnly = false;
                            btnsaveuser2.Text = save;
                            ClearData_User2();   //clear the fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser2.Rows.Count; j++)
                            {
                                if (dgvuser2.Rows[j].Cells[1].Value.ToString().Equals(txtuser2desc.Text))
                                {
                                    dgvuser2.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser2master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    btnsaveuser2.ForeColor = Color.Lime;
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear fields
        public void ClearData_User2()
        {
            txtuser2id.Text = "";
            txtuser2desc.Text = "";
            btnsaveuser2.ForeColor = Color.Lime;
        }

        private void btndeleteuser2_Click(object sender, EventArgs e)
        {
            //delete the selected row
            SqlCommand cmd = new SqlCommand("Delete from USER_DEF2_DB where V_USER_ID='" + txtuser2id.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            lblmsg.Text = "Record Deleted";
            RefereshGrid_User2();    //get the master

            txtuser2id.ReadOnly = false;
            btnsaveuser2.Text = save;
            ClearData_User2();   //clear fields

            btndeleteuser2.Enabled = false;
        }

        private void btnedituser2_Click(object sender, EventArgs e)
        {
            RowSelected_User2();   //get the selected row
        }

        public void RowSelected_User2()
        {
            if (dgvuser2.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String Id = dgvuser2.SelectedRows[0].Cells[0].Value + string.Empty;
                String Desc = dgvuser2.SelectedRows[0].Cells[1].Value + string.Empty;

                txtuser2id.Text = Id;
                txtuser2desc.Text = Desc;

                txtuser2id.ReadOnly = true;
                btnsaveuser2.Text = update;
                btndeleteuser2.Enabled = true;
                u2desc = Desc;
            }
        }

        private void dgvuser2_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_User2();    //get the selectec row
        } 

        private void btnsaveuser3_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtuser3id.Text != "" && txtuser3desc.Text != "")
                {
                    btndeleteuser3.Enabled = false;
                    if (btnsaveuser3.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from USER_DEF3_DB where V_USER_ID='" + txtuser3id.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //gte desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF3_DB where V_DESC='" + txtuser3desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc already exists
                        if (i == 0 && k == 0)
                        {
                            //insert into userdef3
                            SqlCommand cmd = new SqlCommand("insert into USER_DEF3_DB values('" + txtuser3id.Text + "','" + txtuser3desc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_User3();   //get the master

                            txtuser3id.ReadOnly = false;
                            ClearData_User3();   //clear allfields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser3.Rows.Count; j++)
                            {
                                if (dgvuser3.Rows[j].Cells[0].Value.ToString().Equals(txtuser3id.Text) || dgvuser3.Rows[j].Cells[1].Value.ToString().Equals(txtuser3desc.Text))
                                {
                                    dgvuser3.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser3master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsaveuser3.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF3_DB where V_DESC='" + txtuser3desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || u3desc == txtuser3desc.Text)
                        {
                            //update userdef3
                            SqlCommand cmd = new SqlCommand("Update USER_DEF3_DB set V_DESC='" + txtuser3desc.Text + "' where V_USER_ID='" + txtuser3id.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_User3();   //get the master

                            txtuser3id.ReadOnly = false;
                            btnsaveuser3.Text = save; 
                            ClearData_User3();   //clear all fields
                        }
                        else
                        {
                            //select roe if exists
                            for (int j = 0; j < dgvuser3.Rows.Count; j++)
                            {
                                if (dgvuser3.Rows[j].Cells[1].Value.ToString().Equals(txtuser3desc.Text))
                                {
                                    dgvuser3.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser3master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear all fields
        public void ClearData_User3()
        {
            txtuser3id.Text = "";
            txtuser3desc.Text = "";
            btnsaveuser3.ForeColor = Color.Lime;
        }

        private void btndeleteuser3_Click(object sender, EventArgs e)
        {
            //delete the selected row
            SqlCommand cmd = new SqlCommand("Delete from USER_DEF3_DB where V_USER_ID='" + txtuser3id.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            lblmsg.Text = "Record Deleted";
            RefereshGrid_User3();  //get the master

            txtuser3id.ReadOnly = false;
            btnsaveuser3.Text = save;
            ClearData_User3();   //clear all fields

            btndeleteuser3.Enabled = false;
        }

        private void btnedituser3_Click(object sender, EventArgs e)
        {
            RowSelected_User3();    //get the selected row
        }

        public void RowSelected_User3()
        {
            if (dgvuser3.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String Id = dgvuser3.SelectedRows[0].Cells[0].Value + string.Empty;
                String Desc = dgvuser3.SelectedRows[0].Cells[1].Value + string.Empty;

                txtuser3id.Text = Id;
                txtuser3desc.Text = Desc;

                txtuser3id.ReadOnly = true;
                btnsaveuser3.Text = update;
                btndeleteuser3.Enabled = true;
                u3desc = Desc;
            }
        }

        private void dgvuser3_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_User3();   //get the selected row
        }

        private void btnsaveuser4_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtuser4id.Text != "" && txtuser4desc.Text != "")
                {
                    btndeleteuser4.Enabled = false;
                    if (btnsaveuser4.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from USER_DEF4_DB where V_USER_ID='" + txtuser4id.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF4_DB where V_DESC='" + txtuser4desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc already exists
                        if (i == 0 && k == 0)
                        {
                            //insert into userdef4
                            SqlCommand cmd = new SqlCommand("insert into USER_DEF4_DB values('" + txtuser4id.Text + "','" + txtuser4desc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_User4();   //get the master

                            txtuser4id.ReadOnly = false;
                            ClearData_User4();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser4.Rows.Count; j++)
                            {
                                if (dgvuser4.Rows[j].Cells[0].Value.ToString().Equals(txtuser4id.Text) || dgvuser4.Rows[j].Cells[1].Value.ToString().Equals(txtuser4desc.Text))
                                {
                                    dgvuser4.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser4master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsaveuser4.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF4_DB where V_DESC='" + txtuser4desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check desc exists or same desc
                        if (k == 0 || u4desc == txtuser4desc.Text)
                        {
                            //update userdef4
                            SqlCommand cmd = new SqlCommand("Update USER_DEF4_DB set V_DESC='" + txtuser4desc.Text + "' where V_USER_ID='" + txtuser4id.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_User4();   //get the master

                            txtuser4id.ReadOnly = false;
                            btnsaveuser4.Text = save;
                            ClearData_User4();   //clear fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser4.Rows.Count; j++)
                            {
                                if (dgvuser4.Rows[j].Cells[1].Value.ToString().Equals(txtuser4desc.Text))
                                {
                                    dgvuser4.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser4master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear fields
        public void ClearData_User4()
        {
            txtuser4id.Text = "";
            txtuser4desc.Text = "";
            btnsaveuser4.ForeColor = Color.Lime;
        }

        private void btndeleteuser4_Click(object sender, EventArgs e)
        {
            //delete the selected row
            SqlCommand cmd = new SqlCommand("Delete from USER_DEF4_DB where V_USER_ID='" + txtuser4id.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            lblmsg.Text = "Record Deleted";
            RefereshGrid_User4();   //get the master

            txtuser4id.ReadOnly = false;
            btnsaveuser4.Text = save;
            ClearData_User4();    //clear the fields

            btndeleteuser4.Enabled = false;
        }

        private void btnedituser4_Click(object sender, EventArgs e)
        {
            RowSelected_User4();   //get the selected row
        }

        public void RowSelected_User4()
        {
            if (dgvuser4.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String Id = dgvuser4.SelectedRows[0].Cells[0].Value + string.Empty;
                String Desc = dgvuser4.SelectedRows[0].Cells[1].Value + string.Empty;

                txtuser4id.Text = Id;
                txtuser4desc.Text = Desc;

                txtuser4id.ReadOnly = true;
                btnsaveuser4.Text = update;
                btndeleteuser4.Enabled = true;
                u4desc = Desc;
            }
        }

        private void dgvuser4_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_User4();    //get the selected row
        }

        private void btnsaveuser5_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtuser5id.Text != "" && txtuser5desc.Text != "")
                {
                    btndeleteuser5.Enabled = false;
                    if (btnsaveuser5.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from USER_DEF5_DB where V_USER_ID='" + txtuser5id.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF5_DB where V_DESC='" + txtuser5desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check id and desc already exists
                        if (i == 0 && k == 0)
                        {
                            //insert into userdef5
                            SqlCommand cmd = new SqlCommand("insert into USER_DEF5_DB values('" + txtuser5id.Text + "','" + txtuser5desc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_User5();   //get master

                            txtuser5id.ReadOnly = false;
                            ClearData_User5();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser5.Rows.Count; j++)
                            {
                                if (dgvuser5.Rows[j].Cells[0].Value.ToString().Equals(txtuser5id.Text) || dgvuser5.Rows[j].Cells[1].Value.ToString().Equals(txtuser5desc.Text))
                                {
                                    dgvuser5.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser5master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsaveuser5.Text == update)
                    {
                        //get decs count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF5_DB where V_DESC='" + txtuser5desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //chekc desc exists or same desc
                        if (k == 0 || u5desc == txtuser5desc.Text)
                        {
                            //update userdef5
                            SqlCommand cmd = new SqlCommand("Update USER_DEF5_DB set V_DESC='" + txtuser5desc.Text + "' where V_USER_ID='" + txtuser5id.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_User5();   //get the master

                            txtuser5id.ReadOnly = false;
                            btnsaveuser5.Text = save;
                            ClearData_User5();   //clear all fields
                        }
                        else
                        {
                            //select row is exists
                            for (int j = 0; j < dgvuser5.Rows.Count; j++)
                            {
                                if (dgvuser5.Rows[j].Cells[1].Value.ToString().Equals(txtuser5desc.Text))
                                {
                                    dgvuser5.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser5master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear all fieilds
        public void ClearData_User5()
        {
            txtuser5id.Text = "";
            txtuser5desc.Text = "";
            btnsaveuser5.ForeColor = Color.Lime;
        }

        private void btndeleteuser5_Click(object sender, EventArgs e)
        {
            //delete the selected row
            SqlCommand cmd = new SqlCommand("Delete from USER_DEF5_DB where V_USER_ID='" + txtuser5id.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            lblmsg.Text = "Record Deleted";
            RefereshGrid_User5();   //get the master

            txtuser5id.ReadOnly = false;
            btnsaveuser5.Text = save;
            ClearData_User5();   //clear all fields

            btndeleteuser5.Enabled = false;
        }

        private void btnedituser5_Click(object sender, EventArgs e)
        {
            RowSelected_User5();   //get the selected row
        }

        public void RowSelected_User5()
        {
            if (dgvuser5.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String Id = dgvuser5.SelectedRows[0].Cells[0].Value + string.Empty;
                String Desc = dgvuser5.SelectedRows[0].Cells[1].Value + string.Empty;

                txtuser5id.Text = Id;
                txtuser5desc.Text = Desc;

                txtuser5id.ReadOnly = true;
                btnsaveuser5.Text = update;
                btndeleteuser5.Enabled = true;
                u5desc = Desc;
            }
        }

        private void dgvuser5_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_User5();   //get the selected row
        }

        private void btnsaveuser6_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtuser6id.Text != "" && txtuser6desc.Text != "")
                {
                    btndeleteuser6.Enabled = false;
                    if (btnsaveuser6.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from USER_DEF6_DB where V_USER_ID='" + txtuser6id.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF6_DB where V_DESC='" + txtuser6desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //insert into userdef6
                            SqlCommand cmd = new SqlCommand("insert into USER_DEF6_DB values('" + txtuser6id.Text + "','" + txtuser6desc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_User6();   //get the master

                            txtuser6id.ReadOnly = false;
                            ClearData_User6();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser6.Rows.Count; j++)
                            {
                                if (dgvuser6.Rows[j].Cells[0].Value.ToString().Equals(txtuser6id.Text) || dgvuser6.Rows[j].Cells[1].Value.ToString().Equals(txtuser6desc.Text))
                                {
                                    dgvuser6.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser6master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsaveuser6.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF6_DB where V_DESC='" + txtuser6desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check desc exists or same desc
                        if (k == 0 || u6desc == txtuser6desc.Text)
                        {
                            //update uderdef6
                            SqlCommand cmd = new SqlCommand("Update USER_DEF6_DB set V_DESC='" + txtuser6desc.Text + "' where V_USER_ID='" + txtuser6id.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_User6();   //get the master

                            txtuser6id.ReadOnly = false;
                            btnsaveuser6.Text = save;
                            ClearData_User6();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser6.Rows.Count; j++)
                            {
                                if (dgvuser6.Rows[j].Cells[1].Value.ToString().Equals(txtuser6desc.Text))
                                {
                                    dgvuser6.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser6master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear all fields
        public void ClearData_User6()
        {
            txtuser6id.Text = "";
            txtuser6desc.Text = "";
            btnsaveuser6.ForeColor = Color.Lime;
        }

        private void btndeleteuser6_Click(object sender, EventArgs e)
        {
            //delete the selected row
            SqlCommand cmd = new SqlCommand("Delete from USER_DEF6_DB where V_USER_ID='" + txtuser6id.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            lblmsg.Text = "Record Deleted";
            RefereshGrid_User6();    //get the master

            txtuser6id.ReadOnly = false;
            btnsaveuser6.Text = save;
            ClearData_User6();   //clear all fields

            btndeleteuser6.Enabled = false;
        }

        private void btnedituser6_Click(object sender, EventArgs e)
        {
            RowSelected_User6();   //get the selected row
        }

        public void RowSelected_User6()
        {
            if (dgvuser6.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String Id = dgvuser6.SelectedRows[0].Cells[0].Value + string.Empty;
                String Desc = dgvuser6.SelectedRows[0].Cells[1].Value + string.Empty;

                txtuser6id.Text = Id;
                txtuser6desc.Text = Desc;

                txtuser6id.ReadOnly = true;
                btnsaveuser6.Text = update;
                btndeleteuser6.Enabled = true;
                u6desc = Desc;
            }
        }

        private void dgvuser6_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_User6();   //get the selected row
        }

        private void btnsaveuser7_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtuser7id.Text != "" && txtuser7desc.Text != "")
                {
                    btndeleteuser7.Enabled = false;
                    if (btnsaveuser7.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from USER_DEF7_DB where V_USER_ID='" + txtuser7id.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF7_DB where V_DESC='" + txtuser7desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //insert into userdef7
                            SqlCommand cmd = new SqlCommand("insert into USER_DEF7_DB values('" + txtuser7id.Text + "','" + txtuser7desc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_User7();   //get the masters

                            txtuser7id.ReadOnly = false;
                            ClearData_User7();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser7.Rows.Count; j++)
                            {
                                if (dgvuser7.Rows[j].Cells[0].Value.ToString().Equals(txtuser7id.Text) || dgvuser7.Rows[j].Cells[1].Value.ToString().Equals(txtuser7desc.Text))
                                {
                                    dgvuser7.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser7master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsaveuser7.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF7_DB where V_DESC='" + txtuser7desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || u7desc == txtuser7desc.Text)
                        {
                            //update userdef7
                            SqlCommand cmd = new SqlCommand("Update USER_DEF7_DB set V_DESC='" + txtuser7desc.Text + "' where V_USER_ID='" + txtuser7id.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_User7();   //get the master

                            txtuser7id.ReadOnly = false;
                            btnsaveuser7.Text = save;
                            ClearData_User7();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser7.Rows.Count; j++)
                            {
                                if (dgvuser7.Rows[j].Cells[1].Value.ToString().Equals(txtuser7desc.Text))
                                {
                                    dgvuser7.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser7master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear all fields
        public void ClearData_User7()
        {
            txtuser7id.Text = "";
            txtuser7desc.Text = "";
            btnsaveuser7.ForeColor = Color.Lime;
        }

        private void btndeleteuser7_Click(object sender, EventArgs e)
        {
            //delete the selected row
            SqlCommand cmd = new SqlCommand("Delete from USER_DEF7_DB where V_USER_ID='" + txtuser7id.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            lblmsg.Text = "Record Deleted";
            RefereshGrid_User7();  //get the master

            txtuser7id.ReadOnly = false;
            btnsaveuser7.Text = save;
            ClearData_User7();   //clear the fields

            btndeleteuser7.Enabled = false;
        }

        private void btnedituser7_Click(object sender, EventArgs e)
        {
            RowSelected_User7();   //get the selected row
        }

        public void RowSelected_User7()
        {
            if (dgvuser7.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String Id = dgvuser7.SelectedRows[0].Cells[0].Value + string.Empty;
                String Desc = dgvuser7.SelectedRows[0].Cells[1].Value + string.Empty;

                txtuser7id.Text = Id;
                txtuser7desc.Text = Desc;

                txtuser7id.ReadOnly = true;
                btnsaveuser7.Text = update;
                btndeleteuser7.Enabled = true;
                u7desc = Desc;
            }
        }

        private void dgvuser7_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_User7();   //get the selected row
        }

        private void btnsaveuser8_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtuser8id.Text != "" && txtuser8desc.Text != "")
                {
                    btndeleteuser8.Enabled = false;
                    if (btnsaveuser8.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from USER_DEF8_DB where V_USER_ID='" + txtuser8id.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF8_DB where V_DESC='" + txtuser8desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //insert into userdef8
                            SqlCommand cmd = new SqlCommand("insert into USER_DEF8_DB values('" + txtuser8id.Text + "','" + txtuser8desc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_User8();   //get the master

                            txtuser8id.ReadOnly = false;
                            ClearData_User8();  //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser8.Rows.Count; j++)
                            {
                                if (dgvuser8.Rows[j].Cells[0].Value.ToString().Equals(txtuser8id.Text) || dgvuser8.Rows[j].Cells[1].Value.ToString().Equals(txtuser8desc.Text))
                                {
                                    dgvuser8.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser8master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsaveuser8.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF8_DB where V_DESC='" + txtuser8desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if des exists or same desc
                        if (k == 0 || u8desc == txtuser8desc.Text)
                        {
                            //update userdef8
                            SqlCommand cmd = new SqlCommand("Update USER_DEF8_DB set V_DESC='" + txtuser8desc.Text + "' where V_USER_ID='" + txtuser8id.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_User8();    //get the master

                            txtuser8id.ReadOnly = false;
                            btnsaveuser8.Text = save;
                            ClearData_User8();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser8.Rows.Count; j++)
                            {
                                if (dgvuser8.Rows[j].Cells[1].Value.ToString().Equals(txtuser8desc.Text))
                                {
                                    dgvuser8.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser8master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear all fields
        public void ClearData_User8()
        {
            txtuser8id.Text = "";
            txtuser8desc.Text = "";
            btnsaveuser8.ForeColor = Color.Lime;
        }

        private void btndeleteuser8_Click(object sender, EventArgs e)
        {
            //delete the selected row
            SqlCommand cmd = new SqlCommand("Delete from USER_DEF8_DB where V_USER_ID='" + txtuser8id.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            lblmsg.Text = "Record Deleted";
            RefereshGrid_User8();    //get the master

            txtuser8id.ReadOnly = false;
            btnsaveuser8.Text = save;
            ClearData_User8();   //clear all fields

            btndeleteuser8.Enabled = false;
        }

        private void btnedituser8_Click(object sender, EventArgs e)
        {
            RowSelected_User8();   //get the selected row
        }

        public void RowSelected_User8()
        {
            if (dgvuser8.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String Id = dgvuser8.SelectedRows[0].Cells[0].Value + string.Empty;
                String Desc = dgvuser8.SelectedRows[0].Cells[1].Value + string.Empty;

                txtuser8id.Text = Id;
                txtuser8desc.Text = Desc;

                txtuser8id.ReadOnly = true;
                btnsaveuser8.Text = update;
                btndeleteuser8.Enabled = true;
                u8desc = Desc;
            }
        }

        private void dgvuser8_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_User8();   //get the selected row
        }

        private void btnsaveuser9_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtuser9id.Text != "" && txtuser9desc.Text != "")
                {
                    btndeleteuser9.Enabled = false;
                    if (btnsaveuser9.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from USER_DEF9_DB where V_USER_ID='" + txtuser9id.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF9_DB where V_DESC='" + txtuser9desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //insert into userdef9
                            SqlCommand cmd = new SqlCommand("insert into USER_DEF9_DB values('" + txtuser9id.Text + "','" + txtuser9desc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_User9();   //get the master

                            txtuser9id.ReadOnly = false;
                            ClearData_User9();    //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser9.Rows.Count; j++)
                            {
                                if (dgvuser9.Rows[j].Cells[0].Value.ToString().Equals(txtuser9id.Text) || dgvuser9.Rows[j].Cells[1].Value.ToString().Equals(txtuser9desc.Text))
                                {
                                    dgvuser9.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser9master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsaveuser9.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF9_DB where V_DESC='" + txtuser9desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || u9desc == txtuser9desc.Text)
                        {
                            //update userdef9
                            SqlCommand cmd = new SqlCommand("Update USER_DEF9_DB set V_DESC='" + txtuser9desc.Text + "' where V_USER_ID='" + txtuser9id.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_User9(); //get the master

                            txtuser9id.ReadOnly = false;
                            btnsaveuser9.Text = save;
                            ClearData_User9();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser9.Rows.Count; j++)
                            {
                                if (dgvuser9.Rows[j].Cells[1].Value.ToString().Equals(txtuser9desc.Text))
                                {
                                    dgvuser9.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser9master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear all fields
        public void ClearData_User9()
        {
            txtuser9id.Text = "";
            txtuser9desc.Text = "";
            btnsaveuser9.ForeColor = Color.Lime;
        }

        private void btndeleteuser9_Click(object sender, EventArgs e)
        {
            //delete the selected row
            SqlCommand cmd = new SqlCommand("Delete from USER_DEF9_DB where V_USER_ID='" + txtuser9id.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            lblmsg.Text = "Record Deleted";
            RefereshGrid_User9();    //get the master

            txtuser9id.ReadOnly = false;
            btnsaveuser9.Text = save;
            ClearData_User9();    //clear the fields
            btndeleteuser9.Enabled = false;
        }

        private void btnedituser9_Click(object sender, EventArgs e)
        {
            RowSelected_User9();    //get the selected row
        }

        public void RowSelected_User9()
        {
            if (dgvuser9.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String Id = dgvuser9.SelectedRows[0].Cells[0].Value + string.Empty;
                String Desc = dgvuser9.SelectedRows[0].Cells[1].Value + string.Empty;

                txtuser9id.Text = Id;
                txtuser9desc.Text = Desc;

                txtuser9id.ReadOnly = true;
                btnsaveuser9.Text = update;
                btndeleteuser9.Enabled = true;
                u9desc = Desc;
            }
        }

        private void dgvuser9_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_User9();    //get the selected row
        }

        private void btnsaveuser10_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtuser10id.Text != "" && txtuser10desc.Text != "")
                {
                    btndeleteuser10.Enabled = false;
                    if (btnsaveuser10.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from USER_DEF10_DB where V_USER_ID='" + txtuser10id.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF10_DB where V_DESC='" + txtuser10desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //insert into userdef10
                            SqlCommand cmd = new SqlCommand("insert into USER_DEF10_DB values('" + txtuser10id.Text + "','" + txtuser10desc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_User10();   //get the master

                            txtuser10id.ReadOnly = false;
                            ClearData_User10();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser10.Rows.Count; j++)
                            {
                                if (dgvuser10.Rows[j].Cells[0].Value.ToString().Equals(txtuser10id.Text) || dgvuser10.Rows[j].Cells[1].Value.ToString().Equals(txtuser10desc.Text))
                                {
                                    dgvuser10.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser10master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsaveuser10.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from USER_DEF10_DB where V_DESC='" + txtuser10desc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || u10desc == txtuser10desc.Text)
                        {
                            //update
                            SqlCommand cmd = new SqlCommand("Update USER_DEF10_DB set V_DESC='" + txtuser10desc.Text + "' where V_USER_ID='" + txtuser10id.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_User10();   //get the master

                            txtuser10id.ReadOnly = false;
                            btnsaveuser10.Text = save;
                            ClearData_User10();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvuser10.Rows.Count; j++)
                            {
                                if (dgvuser10.Rows[j].Cells[1].Value.ToString().Equals(txtuser10desc.Text))
                                {
                                    dgvuser10.Rows[j].IsSelected = true;
                                    lblmsg.Text = pageuser10master.Text + " Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //clear all fields
        public void ClearData_User10()
        {
            txtuser10id.Text = "";
            txtuser10desc.Text = "";
            btnsaveuser10.ForeColor = Color.Lime;
        }

        private void btndeleteuser10_Click(object sender, EventArgs e)
        {
            //delete the selected row
            SqlCommand cmd = new SqlCommand("Delete from USER_DEF10_DB where V_USER_ID='" + txtuser10id.Text + "'", dc.con);
            cmd.ExecuteNonQuery();

            lblmsg.Text = "Record Deleted";
            RefereshGrid_User10();   //get the master

            txtuser10id.ReadOnly = false;
            btnsaveuser10.Text = save;
            ClearData_User10();    //clear all fields

            btndeleteuser10.Enabled = false;
        }

        private void btnedituser10_Click(object sender, EventArgs e)
        {
            RowSelected_User10();   //get the selected row
        }

        public void RowSelected_User10()
        {
            if (dgvuser10.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String Id = dgvuser10.SelectedRows[0].Cells[0].Value + string.Empty;
                String Desc = dgvuser10.SelectedRows[0].Cells[1].Value + string.Empty;

                txtuser10id.Text = Id;
                txtuser10desc.Text = Desc;

                txtuser10id.ReadOnly = true;
                btnsaveuser10.Text = update;
                btndeleteuser10.Enabled = true;
                u10desc = Desc;
            }
        }

        private void dgvuser10_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_User10();    //get the selected row
        }

        private void btnsearchimage_Click(object sender, EventArgs e)
        {
            //browsing the image and displaying it in the picture box
            OpenFileDialog img = new OpenFileDialog();
            img.InitialDirectory = "C:/Picture/";
            img.Filter = "All Files|*.*|JPEGs|*.jpg|Bitmaps|*.bmp|GIFs|*.gif";
            img.FilterIndex = 2;

            if (img.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(img.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private void lblmsg_TextChanged_1(object sender, EventArgs e)
        {
            MyTimer.Interval = 5000; //5 Sec
            MyTimer.Tick += new EventHandler(MyTimer_Tick);
            pnlerror.Visible = true;
            MyTimer.Start();
        }

        Timer MyTimer = new Timer();

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            lblmsg.Text = "";
            pnlerror.Visible = false;
            MyTimer.Stop();
        }

        String theme = "";
        private void Masters_Initialized(object sender, EventArgs e)
        {
            dc.OpenConnection();    //open connection

            //get the language and theme
            String Lang = "";
            SqlCommand cmd = new SqlCommand("SELECT Language,ThemeName FROM Setup", dc.con);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Lang = sdr.GetValue(0).ToString();
                theme = sdr.GetValue(1).ToString();
            }
            sdr.Close();

            //change the form language
            SqlDataAdapter sda = new SqlDataAdapter("select " + Lang + " from Language where Form='Color' order by Item_No", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            if (dt.Rows.Count > 0)
            {
                lblcolorid.Text = dt.Rows[0][0].ToString() + " :";
                lblcolordesc.Text = dt.Rows[1][0].ToString() + " :";
                lblcolorrgb.Text = dt.Rows[2][0].ToString() + " :";
                btndeletecolor.Text = dt.Rows[3][0].ToString();
                btnsavecolor.Text = dt.Rows[4][0].ToString();
                save = dt.Rows[4][0].ToString();
                update = dt.Rows[5][0].ToString();
            }

            btnsaveqcmain.Text = save;
            btnsaveqcsub.Text = save;
            btnsavemachine.Text = save;
            btndeletemachine.Text = dt.Rows[3][0].ToString();
            btndeleteqcmain.Text = dt.Rows[3][0].ToString();
            btndeleteqcsub.Text = dt.Rows[3][0].ToString();

            sda = new SqlDataAdapter("select " + Lang + " from Language where Form='Article' order by Item_No", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            if (dt.Rows.Count > 0)
            {
                lblarticleid.Text = dt.Rows[0][0].ToString() + " :";
                lblarticledesc.Text = dt.Rows[1][0].ToString() + " :";
                btndeletearticle.Text = dt.Rows[2][0].ToString();
                btnsavearticle.Text = dt.Rows[3][0].ToString();
                btnsetsequence.Text = dt.Rows[4][0].ToString();
            }

            sda = new SqlDataAdapter("select " + Lang + " from Language where Form='Size' order by Item_No", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            if (dt.Rows.Count > 0)
            {
                lblsizeid.Text = dt.Rows[0][0].ToString() + " :";
                lblsizedesc.Text = dt.Rows[1][0].ToString() + " :";
                btndeletesize.Text = dt.Rows[2][0].ToString();
                btnsavesize.Text = dt.Rows[3][0].ToString();
            }

            sda = new SqlDataAdapter("select " + Lang + " from Language where Form='Contractor' order by Item_No", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            if (dt.Rows.Count > 0)
            {
                lblcontractorid.Text = dt.Rows[0][0].ToString() + " :";
                lblcontractorname.Text = dt.Rows[1][0].ToString() + " :";
                btndeletecontractor.Text = dt.Rows[2][0].ToString();
                btnsavecontractor.Text = dt.Rows[3][0].ToString();
            }

            sda = new SqlDataAdapter("select " + Lang + " from Language where Form='Employee' order by Item_No", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            if (dt.Rows.Count > 0)
            {
                lblempid.Text = "*" + dt.Rows[0][0].ToString() + " :";
                lblfname.Text = "*" + dt.Rows[1][0].ToString() + " :";
                lblmname.Text = dt.Rows[2][0].ToString() + " :";
                lbllname.Text = dt.Rows[3][0].ToString() + " :";
                lblledname.Text = "*" + dt.Rows[4][0].ToString() + " :";
                lbladdress.Text = "*" + dt.Rows[5][0].ToString() + " :";
                lblzip.Text = dt.Rows[6][0].ToString() + " :";
                lblposition.Text = "*" + dt.Rows[7][0].ToString() + " :";
                lbltelno.Text = dt.Rows[8][0].ToString() + " :";
                lblsex.Text = "*" + dt.Rows[9][0].ToString() + " :";
                lbldob.Text = dt.Rows[10][0].ToString() + " :";
                lblskill.Text = "*" + dt.Rows[11][0].ToString() + " :";
                lblrfid.Text = "*" + dt.Rows[12][0].ToString() + " :";
                lblempcontractor.Text = "*" + dt.Rows[13][0].ToString() + " :";
                btnsearchimage.Text = dt.Rows[14][0].ToString();
                btndeleteemp.Text = dt.Rows[15][0].ToString();
                btnsaveemp.Text = dt.Rows[16][0].ToString();
            }

            sda = new SqlDataAdapter("select " + Lang + " from Language where Form='Customer' order by Item_No", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            if (dt.Rows.Count > 0)
            {
                lblcustomerid.Text = dt.Rows[0][0].ToString() + " :";
                lblcustomername.Text = dt.Rows[1][0].ToString() + " :";
                lblcustomerdest.Text = dt.Rows[2][0].ToString() + " :";
                btndeletecustomer.Text = dt.Rows[3][0].ToString();
                btnsavecustomer.Text = dt.Rows[4][0].ToString();
            }

            sda = new SqlDataAdapter("select " + Lang + " from Language where Form='Operation' order by Item_No", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            if (dt.Rows.Count > 0)
            {
                lbloperationid.Text = dt.Rows[0][0].ToString() + " :";
                lbloperationdesc.Text = dt.Rows[1][0].ToString() + " :";
                lblpiecerate.Text = dt.Rows[2][0].ToString() + " :";
                lblsam.Text = dt.Rows[3][0].ToString() + " :";
                btndeleteoperation.Text = dt.Rows[4][0].ToString();
                btnsaveoperation.Text = dt.Rows[5][0].ToString();
            }

            sda = new SqlDataAdapter("select " + Lang + " from Language where Form='User' order by Item_No", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            if (dt.Rows.Count > 0)
            {
                btndeleteuser1.Text = dt.Rows[2][0].ToString();
                btnsaveuser1.Text = dt.Rows[3][0].ToString();
                btndeleteuser2.Text = dt.Rows[2][0].ToString();
                btnsaveuser2.Text = dt.Rows[3][0].ToString();
                btndeleteuser3.Text = dt.Rows[2][0].ToString();
                btnsaveuser3.Text = dt.Rows[3][0].ToString();
                btndeleteuser4.Text = dt.Rows[2][0].ToString();
                btnsaveuser4.Text = dt.Rows[3][0].ToString();
                btndeleteuser5.Text = dt.Rows[2][0].ToString();
                btnsaveuser5.Text = dt.Rows[3][0].ToString();
                btndeleteuser6.Text = dt.Rows[2][0].ToString();
                btnsaveuser6.Text = dt.Rows[3][0].ToString();
                btndeleteuser7.Text = dt.Rows[2][0].ToString();
                btnsaveuser7.Text = dt.Rows[3][0].ToString();
                btndeleteuser8.Text = dt.Rows[2][0].ToString();
                btnsaveuser8.Text = dt.Rows[3][0].ToString();
                btndeleteuser9.Text = dt.Rows[2][0].ToString();
                btnsaveuser9.Text = dt.Rows[3][0].ToString();
                btndeleteuser10.Text = dt.Rows[2][0].ToString();
                btnsaveuser10.Text = dt.Rows[3][0].ToString();
            }

            sda = new SqlDataAdapter("select " + Lang + " from Language where Form='Masters' order by Item_No", dc.con);
            dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            if (dt.Rows.Count > 0)
            {
                pagecolormaster.Text = dt.Rows[0][0].ToString();
                pagearticlemaster.Text = dt.Rows[1][0].ToString();
                pagesizemaster.Text = dt.Rows[2][0].ToString();
                pagecontractormaster.Text = dt.Rows[3][0].ToString();
                pageemployeemaster.Text = dt.Rows[4][0].ToString();
                pagecustomermaster.Text = dt.Rows[5][0].ToString();
                pageoperationmaster.Text = dt.Rows[7][0].ToString();
                pageqcmain.Text = dt.Rows[8][0].ToString();
                pageqcsub.Text = dt.Rows[9][0].ToString();
                lblqcmainid.Text = dt.Rows[10][0].ToString() + " :";
                lblqcmaindesc.Text = dt.Rows[11][0].ToString() + " :";
                lblqcmaindesc1.Text = dt.Rows[11][0].ToString() + " :";
                lblqcsubid.Text = dt.Rows[12][0].ToString() + " :";
                lblqcsubdesc.Text = dt.Rows[13][0].ToString() + " :";
            }

            //change grid theme
            GridTheme(theme);
        }

        //set grid theme
        public void GridTheme(String theme)
        {
            dgvcolor.ThemeName = theme;
            dgvarticle.ThemeName = theme;
            dgvsize.ThemeName = theme;
            dgvcustomer.ThemeName = theme;
            dgvoperation.ThemeName = theme;
            dvgcontractor.ThemeName = theme;
            dgvemployee.ThemeName = theme;
            dgvgroup.ThemeName = theme;
            dgvemployeegroup.ThemeName = theme;
            dgvemployeeselect.ThemeName = theme;
            dgvskill.ThemeName = theme;
            dgvmachines.ThemeName = theme;
            dgvmachinedetails.ThemeName = theme;
            dgvmbmain.ThemeName = theme;
            dgvmbsub.ThemeName = theme;
            dgvqcmain.ThemeName = theme;
            dgvqcsub.ThemeName = theme;
            dgvuser1.ThemeName = theme;
            dgvuser2.ThemeName = theme;
            dgvuser3.ThemeName = theme;
            dgvuser4.ThemeName = theme;
            dgvuser5.ThemeName = theme;
            dgvuser6.ThemeName = theme;
            dgvuser7.ThemeName = theme;
            dgvuser8.ThemeName = theme;
            dgvuser9.ThemeName = theme;
            dgvuser10.ThemeName = theme;
            dgvdesignoperation.ThemeName = theme;
            dgvdesignsequence.ThemeName = theme;
            dgvsparemain.ThemeName = theme;
            dgvsparesub.ThemeName = theme;
        }

        private void btnaddcontractor_Click(object sender, EventArgs e)
        {
            //open master
            Masters cm = new Masters();
            cm.Show();
            cm.Form_Location1("Contractor");
        }

        private void txtcolorid_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtcolorid.Text == "" && txtcolordesc.Text == "")
            {
                btnsavecolor.ForeColor = Color.Lime;
            }
            else
            {
                btnsavecolor.ForeColor = Color.Red;
            }
        }

        private void txtcolordesc_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtcolorid.Text == "" && txtcolordesc.Text == "")
            {
                btnsavecolor.ForeColor = Color.Lime;
            }
            else
            {
                btnsavecolor.ForeColor = Color.Red;
            }
        }

        private void Masters_FormClosing(object sender, FormClosingEventArgs e)
        {
            //confirm box before closing the form to save
            if (btnsavecolor.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pagecolormaster;

                DialogResult result = RadMessageBox.Show("Unsaved Color. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavecolor.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsavearticle.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pagearticlemaster;

                DialogResult result = RadMessageBox.Show("Unsaved Article. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavearticle.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsavesize.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pagesizemaster;

                DialogResult result = RadMessageBox.Show("Unsaved Size. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavesize.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsavecontractor.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pagecontractormaster;

                DialogResult result = RadMessageBox.Show("Unsaved Contractor. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavecontractor.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveemp.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageemployeemaster;

                DialogResult result = RadMessageBox.Show("Unsaved Employee. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveemp.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsavecustomer.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pagecustomermaster;

                DialogResult result = RadMessageBox.Show("Unsaved Customer. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavecustomer.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveoperation.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageoperationmaster;

                DialogResult result = RadMessageBox.Show("Unsaved Operation. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveoperation.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveuser1.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageuser1master;

                DialogResult result = RadMessageBox.Show("Unsaved " + pageuser1master.Text + ". Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveuser1.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveuser2.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageuser2master;

                DialogResult result = RadMessageBox.Show("Unsaved " + pageuser2master.Text + ". Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveuser2.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveuser3.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageuser3master;

                DialogResult result = RadMessageBox.Show("Unsaved " + pageuser3master.Text + ". Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveuser3.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveuser4.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageuser4master;

                DialogResult result = RadMessageBox.Show("Unsaved " + pageuser4master.Text + ". Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveuser4.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveuser5.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageuser5master;

                DialogResult result = RadMessageBox.Show("Unsaved " + pageuser5master.Text + ". Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveuser5.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveuser6.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageuser6master;

                DialogResult result = RadMessageBox.Show("Unsaved " + pageuser6master.Text + ". Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveuser6.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveuser7.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageuser7master;

                DialogResult result = RadMessageBox.Show("Unsaved " + pageuser7master.Text + ". Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveuser7.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveuser8.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageuser8master;

                DialogResult result = RadMessageBox.Show("Unsaved " + pageuser8master.Text + ". Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveuser8.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveuser9.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageuser9master;

                DialogResult result = RadMessageBox.Show("Unsaved " + pageuser9master.Text + ". Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveuser9.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveuser10.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageuser10master;

                DialogResult result = RadMessageBox.Show("Unsaved " + pageuser10master.Text + ". Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveuser10.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveqcsub.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageqcsub;

                DialogResult result = RadMessageBox.Show("Unsaved QC Sub Category. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveqcsub.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveqcmain.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageqcmain;

                DialogResult result = RadMessageBox.Show("Unsaved QC Main Category. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveqcmain.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsavegroup.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageemployeegroupcategory;

                DialogResult result = RadMessageBox.Show("Unsaved Employee Group. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavegroup.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsavemachine.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pagemachinemaster;

                DialogResult result = RadMessageBox.Show("Unsaved Machine. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavemachine.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsavemachinedetails.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pagemachinedetails;

                DialogResult result = RadMessageBox.Show("Unsaved Machine Details. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavemachinedetails.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsavembmain.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pagembmain;

                DialogResult result = RadMessageBox.Show("Unsaved Machine Repair Main Category. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavembmain.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsavembsub.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pagembsub;

                DialogResult result = RadMessageBox.Show("Unsaved Machine Repair Sub Category. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavembsub.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsaveskill.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pageskill;

                DialogResult result = RadMessageBox.Show("Unsaved Skills. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsaveskill.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsavesparemain.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pagesparemain;

                DialogResult result = RadMessageBox.Show("Unsaved Spare Main Category. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavesparemain.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsavesparesub.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pagesparesub;

                DialogResult result = RadMessageBox.Show("Unsaved Spare Sub Category. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavesparesub.PerformClick();
                    e.Cancel = true;
                }
            }
            else if (btnsavesequence.ForeColor == Color.Red)
            {
                vpagemasters.SelectedPage = pagedesignsequence;

                DialogResult result = RadMessageBox.Show("Unsaved Design Sequence. Do you want Save", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnsavesequence.PerformClick();
                    e.Cancel = true;
                }
            }
        }

        private void txtarticleid_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtarticleid.Text == "" && txtarticledesc.Text == "")
            {
                btnsavearticle.ForeColor = Color.Lime;
            }
            else
            {
                btnsavearticle.ForeColor = Color.Red;
            }
        }

        private void txtarticledesc_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtarticleid.Text == "" && txtarticledesc.Text == "")
            {
                btnsavearticle.ForeColor = Color.Lime;
            }
            else
            {
                btnsavearticle.ForeColor = Color.Red;
            }
        }

        private void txtsizeid_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtsizeid.Text == "" && txtsizedesc.Text == "")
            {
                btnsavesize.ForeColor = Color.Lime;
            }
            else
            {
                btnsavesize.ForeColor = Color.Red;
            }
        }

        private void txtsizedesc_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtsizeid.Text == "" && txtsizedesc.Text == "")
            {
                btnsavesize.ForeColor = Color.Lime;
            }
            else
            {
                btnsavesize.ForeColor = Color.Red;
            }
        }

        private void txtcontractorid_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtcontractorid.Text == "" && txtcontractorname.Text == "")
            {
                btnsavecontractor.ForeColor = Color.Lime;
            }
            else
            {
                btnsavecontractor.ForeColor = Color.Red;
            }
        }

        private void txtcontractorname_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtcontractorid.Text == "" && txtcontractorname.Text == "")
            {
                btnsavecontractor.ForeColor = Color.Lime;
            }
            else
            {
                btnsavecontractor.ForeColor = Color.Red;
            }
        }

        private void txtempid_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtempid.Text == "" && txtfirstname.Text == "" && txtlastname.Text == "" && txtledname.Text == "" && txtaddress.Text == "" && txtzipcode.Text == "" && txtposition.Text == "" && txttelno.Text == "" && txtrfid.Text == "")
            {
                btnsaveemp.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveemp.ForeColor = Color.Red;
            }
        }

        private void txtfirstname_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtempid.Text == "" && txtfirstname.Text == "" && txtlastname.Text == "" && txtledname.Text == "" && txtaddress.Text == "" && txtzipcode.Text == "" && txtposition.Text == "" && txttelno.Text == "" && txtrfid.Text == "")
            {
                btnsaveemp.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveemp.ForeColor = Color.Red;
            }
        }

        private void txtlastname_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtempid.Text == "" && txtfirstname.Text == "" && txtlastname.Text == "" && txtledname.Text == "" && txtaddress.Text == "" && txtzipcode.Text == "" && txtposition.Text == "" && txttelno.Text == "" && txtrfid.Text == "")
            {
                btnsaveemp.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveemp.ForeColor = Color.Red;
            }
        }

        private void txtledname_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtempid.Text == "" && txtfirstname.Text == "" && txtlastname.Text == "" && txtledname.Text == "" && txtaddress.Text == "" && txtzipcode.Text == "" && txtposition.Text == "" && txttelno.Text == "" && txtrfid.Text == "")
            {
                btnsaveemp.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveemp.ForeColor = Color.Red;
            }
        }

        private void txtaddress_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtempid.Text == "" && txtfirstname.Text == "" && txtlastname.Text == "" && txtledname.Text == "" && txtaddress.Text == "" && txtzipcode.Text == "" && txtposition.Text == "" && txttelno.Text == "" && txtrfid.Text == "")
            {
                btnsaveemp.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveemp.ForeColor = Color.Red;
            }
        }

        private void txtzipcode_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtempid.Text == "" && txtfirstname.Text == "" && txtlastname.Text == "" && txtledname.Text == "" && txtaddress.Text == "" && txtzipcode.Text == "" && txtposition.Text == "" && txttelno.Text == "" && txtrfid.Text == "")
            {
                btnsaveemp.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveemp.ForeColor = Color.Red;
            }
        }

        private void txtposition_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtempid.Text == "" && txtfirstname.Text == "" && txtlastname.Text == "" && txtledname.Text == "" && txtaddress.Text == "" && txtzipcode.Text == "" && txtposition.Text == "" && txttelno.Text == "" && txtrfid.Text == "")
            {
                btnsaveemp.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveemp.ForeColor = Color.Red;
            }
        }

        private void txttelno_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtempid.Text == "" && txtfirstname.Text == "" && txtlastname.Text == "" && txtledname.Text == "" && txtaddress.Text == "" && txtzipcode.Text == "" && txtposition.Text == "" && txttelno.Text == "" && txtrfid.Text == "")
            {
                btnsaveemp.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveemp.ForeColor = Color.Red;
            }
        }

        private void txtrfid_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtempid.Text == "" && txtfirstname.Text == "" && txtlastname.Text == "" && txtledname.Text == "" && txtaddress.Text == "" && txtzipcode.Text == "" && txtposition.Text == "" && txttelno.Text == "" && txtrfid.Text == "")
            {
                btnsaveemp.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveemp.ForeColor = Color.Red;
            }
        }

        private void txtcustomerid_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtcustomerid.Text == "" && txtcustomername.Text == "" && txtcustomerdest.Text == "")
            {
                btnsavecustomer.ForeColor = Color.Lime;
            }
            else
            {
                btnsavecustomer.ForeColor = Color.Red;
            }
        }

        private void txtcustomername_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtcustomerid.Text == "" && txtcustomername.Text == "" && txtcustomerdest.Text == "")
            {
                btnsavecustomer.ForeColor = Color.Lime;
            }
            else
            {
                btnsavecustomer.ForeColor = Color.Red;
            }
        }

        private void txtcustomerdest_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtcustomerid.Text == "" && txtcustomername.Text == "" && txtcustomerdest.Text == "")
            {
                btnsavecustomer.ForeColor = Color.Lime;
            }
            else
            {
                btnsavecustomer.ForeColor = Color.Red;
            }
        }

        private void txtoperationid_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtoperationid.Text == "" && txtoperationdesc.Text == "" && txtpiecerate.Text == "" && txtsam.Text == "")
            {
                btnsaveoperation.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveoperation.ForeColor = Color.Red;
            }
        }

        private void txtoperationdesc_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtoperationid.Text == "" && txtoperationdesc.Text == "" && txtpiecerate.Text == "" && txtsam.Text == "")
            {
                btnsaveoperation.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveoperation.ForeColor = Color.Red;
            }
        }

        private void txtpiecerate_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtoperationid.Text == "" && txtoperationdesc.Text == "" && txtpiecerate.Text == "" && txtsam.Text == "")
            {
                btnsaveoperation.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveoperation.ForeColor = Color.Red;
            }
        }

        private void txtsam_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtoperationid.Text == "" && txtoperationdesc.Text == "" && txtpiecerate.Text == "" && txtsam.Text == "")
            {
                btnsaveoperation.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveoperation.ForeColor = Color.Red;
            }

            try
            {
                decimal sam = Convert.ToDecimal(txtsam.Text);
                txtsammin.Text = ((decimal)sam / (decimal)60).ToString("0.##");
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Invalid SAM value. Example 30";
                Console.WriteLine(ex);
            }
        }

        private void txtuser1id_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser1id.Text == "" && txtuser1desc.Text == "")
            {
                btnsaveuser1.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser1.ForeColor = Color.Red;
            }
        }

        private void txtuser1desc_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser1id.Text == "" && txtuser1desc.Text == "")
            {
                btnsaveuser1.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser1.ForeColor = Color.Red;
            }
        }

        private void txtuser2id_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser2id.Text == "" && txtuser2desc.Text == "")
            {
                btnsaveuser2.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser2.ForeColor = Color.Red;
            }
        }

        private void txtuser2desc_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser2id.Text == "" && txtuser2desc.Text == "")
            {
                btnsaveuser2.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser2.ForeColor = Color.Red;
            }
        }

        private void txtuser3id_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser3id.Text == "" && txtuser3desc.Text == "")
            {
                btnsaveuser3.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser3.ForeColor = Color.Red;
            }
        }

        private void txtuser3desc_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser3id.Text == "" && txtuser3desc.Text == "")
            {
                btnsaveuser3.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser3.ForeColor = Color.Red;
            }
        }

        private void txtuser4id_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser4id.Text == "" && txtuser4desc.Text == "")
            {
                btnsaveuser4.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser4.ForeColor = Color.Red;
            }
        }

        private void txtuser4desc_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser4id.Text == "" && txtuser4desc.Text == "")
            {
                btnsaveuser4.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser4.ForeColor = Color.Red;
            }
        }

        private void txtuser5id_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser5id.Text == "" && txtuser5desc.Text == "")
            {
                btnsaveuser5.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser5.ForeColor = Color.Red;
            }
        }

        private void txtuser5desc_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser5id.Text == "" && txtuser5desc.Text == "")
            {
                btnsaveuser5.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser5.ForeColor = Color.Red;
            }
        }

        private void txtuser6id_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser6id.Text == "" && txtuser6desc.Text == "")
            {
                btnsaveuser6.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser6.ForeColor = Color.Red;
            }
        }

        private void txtuser6desc_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser6id.Text == "" && txtuser6desc.Text == "")
            {
                btnsaveuser6.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser6.ForeColor = Color.Red;
            }
        }

        private void txtuser7id_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser7id.Text == "" && txtuser7desc.Text == "")
            {
                btnsaveuser7.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser7.ForeColor = Color.Red;
            }
        }

        private void txtuser7desc_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser7id.Text == "" && txtuser7desc.Text == "")
            {
                btnsaveuser7.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser7.ForeColor = Color.Red;
            }
        }

        private void txtuser8id_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser8id.Text == "" && txtuser8desc.Text == "")
            {
                btnsaveuser8.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser8.ForeColor = Color.Red;
            }
        }

        private void txtuser8desc_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser8id.Text == "" && txtuser8desc.Text == "")
            {
                btnsaveuser8.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser8.ForeColor = Color.Red;
            }
        }

        private void txtuser9id_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser9id.Text == "" && txtuser9desc.Text == "")
            {
                btnsaveuser9.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser9.ForeColor = Color.Red;
            }
        }

        private void txtuser9desc_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser9id.Text == "" && txtuser9desc.Text == "")
            {
                btnsaveuser9.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser9.ForeColor = Color.Red;
            }
        }

        private void txtuser10id_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser10id.Text == "" && txtuser10desc.Text == "")
            {
                btnsaveuser10.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser10.ForeColor = Color.Red;
            }
        }

        private void txtuser10desc_TextChanged(object sender, EventArgs e)
        {
            //check if fields changed
            if (txtuser10id.Text == "" && txtuser10desc.Text == "")
            {
                btnsaveuser10.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveuser10.ForeColor = Color.Red;
            }
        }

        private void lblempid_Click(object sender, EventArgs e)
        {

        }

        private void btneditqcsub_Click(object sender, EventArgs e)
        {
            RowSelected_QCsub();     //get the selected row
        }

        public void RowSelected_QCsub()
        {
            if (dgvqcsub.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String subId = dgvqcsub.SelectedRows[0].Cells[1].Value + string.Empty;
                String subDesc = dgvqcsub.SelectedRows[0].Cells[2].Value + string.Empty;
                String mainDesc = dgvqcsub.SelectedRows[0].Cells[0].Value + string.Empty;

                txtqcsubid.Text = subId;
                txtqcsubdesc.Text = subDesc;
                cmbqcmaindesc.Text = mainDesc;

                txtqcsubid.ReadOnly = true;
                btnsaveqcsub.Text = update;
                btndeleteqcsub.Enabled = true;
                btnsaveqcsub.ForeColor = Color.Red;
                subdesc = subDesc;
                HideSelected();
            }
        }

        private void dgvqcsub_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_QCsub();   //get the selected row
        }

        private void btndeleteqcsub_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from QC_SUB_CATEGORY where V_QC_SUB_ID='" + txtqcsubid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                lblmsg.Text = "Record Deleted";
                RefereshGrid_QCsub();   //get the master

                txtqcsubid.ReadOnly = false;
                btnsaveqcsub.Text = save;
                ClearData_QCsub();   //clear all fields

                btndeleteqcsub.Enabled = false;
            }
            catch (Exception ex)
            {
                lblmsg.Text = "QC Sub Category is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        //clear all fields
        public void ClearData_QCsub()
        {
            txtqcsubid.Text = "";
            txtqcsubdesc.Text = "";
            btnsaveqcsub.ForeColor = Color.Lime;
        }

        private void btnsaveqcsub_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtqcsubid.Text != "" && txtqcsubdesc.Text != "" && cmbqcmaindesc.Text != "" && cmbqcmaindesc.Text != "--SELECT--")
                {
                    btndeleteqcsub.Enabled = false;
                    if (btnsaveqcsub.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from QC_SUB_CATEGORY where V_QC_SUB_ID='" + txtqcsubid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from QC_SUB_CATEGORY where V_QC_SUB_DESC='" + txtqcsubdesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //get the qc main id
                            SqlCommand cmd = new SqlCommand("select V_QC_MAIN_ID from QC_MAIN_CATEGORY where V_QC_MAIN_DESC='" + cmbqcmaindesc.Text + "'", dc.con);
                            String maincode = "";
                            SqlDataReader sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                maincode = sdr.GetValue(0).ToString();
                            }
                            sdr.Close();

                            //insert
                            cmd = new SqlCommand("insert into QC_SUB_CATEGORY values('" + txtqcsubid.Text + "','" + txtqcsubdesc.Text + "','" + maincode + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_QCsub();   //get the master

                            txtqcsubid.ReadOnly = false;
                            ClearData_QCsub();  //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvqcsub.Rows.Count; j++)
                            {
                                if (dgvqcsub.Rows[j].Cells[0].Value.ToString().Equals(txtqcsubid.Text) || dgvqcsub.Rows[j].Cells[1].Value.ToString().Equals(txtqcsubdesc.Text))
                                {
                                    dgvqcsub.Rows[j].IsSelected = true;
                                    lblmsg.Text = "QC Sub Category Already Exists";
                                    return;
                                }
                            }
                        }
                    }

                    if (btnsaveqcsub.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from QC_SUB_CATEGORY where V_QC_SUB_DESC='" + txtqcsubdesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || subdesc == txtqcsubdesc.Text)
                        {
                            //get the qc main id
                            SqlCommand cmd = new SqlCommand("select V_QC_MAIN_ID from QC_MAIN_CATEGORY where V_QC_MAIN_DESC='" + cmbqcmaindesc.Text + "'", dc.con);
                            String maincode = "";
                            SqlDataReader sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                maincode = sdr.GetValue(0).ToString();
                            }
                            sdr.Close();

                            //update
                            cmd = new SqlCommand("Update QC_SUB_CATEGORY set V_QC_SUB_DESC='" + txtqcsubdesc.Text + "',V_QC_MAIN_ID='" + maincode + "' where V_QC_SUB_ID='" + txtqcsubid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_QCsub();   //get the master

                            txtqcsubid.ReadOnly = false;
                            btnsaveqcsub.Text = save;
                            ClearData_QCsub();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvqcsub.Rows.Count; j++)
                            {
                                if (dgvqcsub.Rows[j].Cells[1].Value.ToString().Equals(txtqcsubdesc.Text))
                                {
                                    dgvqcsub.Rows[j].IsSelected = true;
                                    lblmsg.Text = "QC SUB Category Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    btnsaveqcsub.ForeColor = Color.Lime;
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        private void pageoperationmaster_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btneditqcmain_Click(object sender, EventArgs e)
        {
            RowSelected_QCmain();    //get the selected row
        }

        public void RowSelected_QCmain()
        {
            if (dgvqcmain.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String mainId = dgvqcmain.SelectedRows[0].Cells[0].Value + string.Empty;
                String mainDesc = dgvqcmain.SelectedRows[0].Cells[1].Value + string.Empty;

                txtqcmainid.Text = mainId;
                txtqcmaindesc.Text = mainDesc;

                txtqcmainid.ReadOnly = true;
                btnsaveqcmain.Text = update;
                btndeleteqcmain.Enabled = true;
                btnsaveqcmain.ForeColor = Color.Red;
                maindesc = mainDesc;
            }
        }

        private void dgvqcmain_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_QCmain();   //get the selected row
        }

        private void btndeleteqcmain_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from QC_MAIN_CATEGORY where V_QC_MAIN_ID='" + txtqcmainid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                //delete the selected row
                cmd = new SqlCommand("DELETE FROM QC_SUB_CATEGORY WHERE V_QC_MAIN_ID NOT IN(SELECT D.V_QC_MAIN_ID FROM QC_MAIN_CATEGORY D)", dc.con);
                cmd.ExecuteNonQuery();

                lblmsg.Text = "Record Deleted";
                RefereshGrid_QCmain();  //get the master

                txtqcmainid.ReadOnly = false;
                btnsaveqcmain.Text = save;
                ClearData_QCmain();  //clear all fields

                btndeleteqcmain.Enabled = false;
                RefereshGrid_QCsub();   //get the master
            }
            catch (Exception ex)
            {
                lblmsg.Text = "QC Main is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        //clear all fields
        public void ClearData_QCmain()
        {
            txtqcmainid.Text = "";
            txtqcmaindesc.Text = "";
            btnsaveqcmain.ForeColor = Color.Lime;
        }

        private void btnsaveqcmain_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtqcmainid.Text != "" && txtqcmaindesc.Text != "")
                {
                    btndeleteqcmain.Enabled = false;
                    if (btnsaveqcmain.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from QC_MAIN_CATEGORY where V_QC_MAIN_ID='" + txtqcmainid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from QC_MAIN_CATEGORY where V_QC_MAIN_DESC='" + txtqcmaindesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //insert
                            SqlCommand cmd = new SqlCommand("insert into QC_MAIN_CATEGORY values('" + txtqcmainid.Text + "','" + txtqcmaindesc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_QCmain();   //get the master

                            txtqcmainid.ReadOnly = false;
                            ClearData_QCmain();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvqcmain.Rows.Count; j++)
                            {
                                if (dgvqcmain.Rows[j].Cells[0].Value.ToString().Equals(txtqcmainid.Text) || dgvqcmain.Rows[j].Cells[1].Value.ToString().Equals(txtqcmaindesc.Text))
                                {
                                    dgvqcmain.Rows[j].IsSelected = true;
                                    lblmsg.Text = "QC Main Category Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsaveqcmain.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from QC_MAIN_CATEGORY where V_QC_MAIN_DESC='" + txtqcmaindesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || maindesc == txtqcmaindesc.Text)
                        {
                            //update
                            SqlCommand cmd = new SqlCommand("Update QC_MAIN_CATEGORY set V_QC_MAIN_DESC='" + txtqcmaindesc.Text + "' where V_QC_MAIN_ID='" + txtqcmainid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_QCmain();   //get the master

                            txtqcmainid.ReadOnly = false;
                            btnsaveqcmain.Text = save;
                            ClearData_QCmain();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvqcmain.Rows.Count; j++)
                            {
                                if (dgvqcmain.Rows[j].Cells[1].Value.ToString().Equals(txtqcmaindesc.Text))
                                {
                                    dgvqcmain.Rows[j].IsSelected = true;
                                    lblmsg.Text = "QC Main Category Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    btnsaveqcmain.ForeColor = Color.Lime;
                    RefereshGrid_QCsub();
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        private void txtqcmainid_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtqcmainid.Text == "" && txtqcmaindesc.Text == "")
            {
                btnsaveqcmain.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveqcmain.ForeColor = Color.Red;
            }
        }

        private void txtqcmaindesc_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtqcmainid.Text == "" && txtqcmaindesc.Text == "")
            {
                btnsaveqcmain.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveqcmain.ForeColor = Color.Red;
            }
        }

        private void txtqcsubid_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtqcsubid.Text == "" && txtqcsubdesc.Text == "")
            {
                btnsaveqcsub.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveqcsub.ForeColor = Color.Red;
            }
        }

        private void txtqcsubdesc_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtqcsubid.Text == "" && txtqcsubdesc.Text == "")
            {
                btnsaveqcsub.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveqcsub.ForeColor = Color.Red;
            }
        }

        private void cmbqcmaindesc_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            HideSelected();    //hide the other row
        }

        public void HideSelected()
        {
            try
            {
                //show all the rows
                for (int i = 0; i < dgvqcsub.Rows.Count; i++)
                {
                    dgvqcsub.Rows[i].IsVisible = true;
                }

                //hide other row if the not the selected qc main
                for (int i = 0; i < dgvqcsub.Rows.Count; i++)
                {
                    if (dgvqcsub.Rows[i].Cells[0].Value.ToString() != cmbqcmaindesc.Text && cmbqcmaindesc.Text != "")
                    {
                        dgvqcsub.Rows[i].IsVisible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        private void btneditmachine_Click(object sender, EventArgs e)
        {
            RowSelected_Machine();    //get the selected row
        }

        public void RowSelected_Machine()
        {
            if (dgvmachines.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                txtmachineid.Text = dgvmachines.SelectedRows[0].Cells[0].Value + string.Empty;
                txtmachinedesc.Text = dgvmachines.SelectedRows[0].Cells[1].Value + string.Empty;
                txtmodel.Text = dgvmachines.SelectedRows[0].Cells[2].Value + string.Empty;
                txtattachment1.Text = dgvmachines.SelectedRows[0].Cells[3].Value + string.Empty;
                txtattachment2.Text = dgvmachines.SelectedRows[0].Cells[4].Value + string.Empty;

                txtmachineid.ReadOnly = true;
                btnsavemachine.Text = update;
                btndeletemachine.Enabled = true;
                machinedesc = txtmachinedesc.Text;
                btnsavemachine.ForeColor = Color.Red;
            }
        }

        private void dgvmachines_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Machine();   //get the selected row
        }

        private void btndeletemachine_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from MACHINE_DB where V_MACHINE_ID='" + txtmachineid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand("Delete from MACHINE_DETAILS where V_MACHINE_ID='" + txtmachineid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();   //delete the selected row

                lblmsg.Text = "Record Deleted";
                RefereshGrid_Machines();    //get the master

                txtmachineid.ReadOnly = false;
                btnsavemachine.Text = save;
                ClearData_Machine();       //clear all fields

                btndeletemachine.Enabled = false;
                RefereshGrid_Operation();    //get the master

                RefereshGrid_MachineDetails();   //get the master
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Machine Code is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        //clear all fields
        public void ClearData_Machine()
        {
            txtmachineid.Text = "";
            txtmachinedesc.Text = "";
            txtmodel.Text = "";
            txtattachment1.Text = "";
            txtattachment2.Text = "";
            btnsavemachine.ForeColor = Color.Lime;
        }

        private void btnsavemachine_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtmachineid.Text != "" && txtmachinedesc.Text != "" && txtmodel.Text != "")
                {
                    btndeletemachine.Enabled = false;
                    if (btnsavemachine.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from MACHINE_DB where V_MACHINE_ID='" + txtmachineid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from MACHINE_DB where V_MACHINE_DESC='" + txtmachinedesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //insert
                            SqlCommand cmd = new SqlCommand("insert into MACHINE_DB values('" + txtmachineid.Text + "','" + txtmachinedesc.Text + "','" + txtmodel.Text + "','" + txtattachment1.Text + "','" + txtattachment2.Text + "','0')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_Machines();   //get the master

                            txtmachineid.ReadOnly = false;
                            ClearData_Machine();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvmachines.Rows.Count; j++)
                            {
                                if (dgvmachines.Rows[j].Cells[0].Value.ToString().Equals(txtmachineid.Text) || dgvmachines.Rows[j].Cells[1].Value.ToString().Equals(txtmachinedesc.Text))
                                {
                                    dgvmachines.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Machine Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsavemachine.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from MACHINE_DB where V_MACHINE_DESC='" + txtmachinedesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        if (k == 0 || machinedesc == txtmachinedesc.Text)
                        {
                            //update
                            SqlCommand cmd = new SqlCommand("Update MACHINE_DB set V_MACHINE_DESC='" + txtmachinedesc.Text + "',V_MODEL='" + txtmodel.Text + "',V_ATTACHMENT1='" + txtattachment1.Text + "' ,V_ATTACHMENT2='" + txtattachment2.Text + "' where V_MACHINE_ID='" + txtmachineid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_Machines();   //get the master

                            txtmachineid.ReadOnly = false;
                            btnsavemachine.Text = save;
                            ClearData_Machine();    //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvmachines.Rows.Count; j++)
                            {
                                if (dgvmachines.Rows[j].Cells[1].Value.ToString().Equals(txtmachinedesc.Text))
                                {
                                    dgvmachines.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Machine Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    btnsavemachine.ForeColor = Color.Lime;
                    RefereshGrid_Operation();   //get the master

                    RefereshGrid_MachineDetails();   //get the master
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        private void btneditmachinedetails_Click(object sender, EventArgs e)
        {
            RowSelected_MachineDetails();     //get the selected row
        }

        public void RowSelected_MachineDetails()
        {
            if (dgvmachinedetails.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                cmbmachinedetails.Text = dgvmachinedetails.SelectedRows[0].Cells[3].Value + string.Empty;
                txtmachineserialno.Text = dgvmachinedetails.SelectedRows[0].Cells[0].Value + string.Empty;
                dtpmachinedate.Text = dgvmachinedetails.SelectedRows[0].Cells[1].Value + string.Empty;
                txtmachinerfid.Text = dgvmachinedetails.SelectedRows[0].Cells[2].Value + string.Empty;

                btnsavemachinedetails.Text = update;
                btndeletemachinedetails.Enabled = true;
                machineserialno = txtmachineserialno.Text;
                btnsavemachinedetails.ForeColor = Color.Red;
                Hide_Machine();
            }
        }

        private void dgvmachinedetails_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_MachineDetails();    //get the selected row
        }

        private void btndeletemachinedetails_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from MACHINE_DETAILS where V_MACHINE_SERIAL_NO='" + txtmachineserialno.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                lblmsg.Text = "Record Deleted";
                RefereshGrid_MachineDetails();   //get the master

                btnsavemachinedetails.Text = save;
                ClearData_MachineDetails();    //clear all fields

                btndeletemachinedetails.Enabled = false;
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Machine is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        //clear all fields
        public void ClearData_MachineDetails()
        {
            cmbmachinedetails.Text = "--SELECT--";
            txtmachineserialno.Text = "";
            txtmachinerfid.Text = "";
            btnsavemachine.ForeColor = Color.Lime;
        }

        private void cmbmachinedetails_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Hide_Machine();   //hide the other row
            // check if fields changed
            if (cmbmachinedetails.Text == "" && txtmachineserialno.Text == "" && txtmachinerfid.Text == "")
            {
                btnsavemachinedetails.ForeColor = Color.Lime;
            }
            else
            {
                btnsavemachinedetails.ForeColor = Color.Red;
            }
        }

        public void Hide_Machine()
        {
            //show all the rows
            for (int i = 0; i < dgvmachinedetails.Rows.Count; i++)
            {
                dgvmachinedetails.Rows[i].IsVisible = true;
            }

            //hide the rows other than the selected machine 
            for (int i = 0; i < dgvmachinedetails.Rows.Count; i++)
            {
                if (dgvmachinedetails.Rows[i].Cells[3].Value.ToString() == cmbmachinedetails.Text)
                {
                    dgvmachinedetails.Rows[i].IsVisible = true;
                }
                else
                {
                    dgvmachinedetails.Rows[i].IsVisible = false;
                }
            }
        }

        private void btneditmbmain_Click(object sender, EventArgs e)
        {
            RowSelected_MBMain();    //get the selected row
        }

        public void RowSelected_MBMain()
        {
            if (dgvmbmain.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String mainId = dgvmbmain.SelectedRows[0].Cells[0].Value + string.Empty;
                String mainDesc = dgvmbmain.SelectedRows[0].Cells[1].Value + string.Empty;

                txtmbmainid.Text = mainId;
                txtmbmaindesc.Text = mainDesc;

                txtmbmainid.ReadOnly = true;
                btnsavembmain.Text = update;
                btndeletembmain.Enabled = true;
                btnsavembmain.ForeColor = Color.Red;
                mbmaindesc = mainDesc;
            }
        }

        private void dgvmbmain_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_MBMain();   //get the selected row
        }

        private void btndeletembmain_Click(object sender, EventArgs e)
        {
            try
            {

                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from MB_MAIN_CATEGORY where V_MB_MAIN_ID='" + txtmbmainid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                //delete the selected row
                cmd = new SqlCommand("DELETE FROM MB_SUB_CATEGORY WHERE V_MB_MAIN_ID NOT IN(SELECT D.V_MB_MAIN_ID FROM MB_MAIN_CATEGORY D)", dc.con);
                cmd.ExecuteNonQuery();   

                lblmsg.Text = "Record Deleted";
                RefereshGrid_MBmain();   //get the master

                txtmbmainid.ReadOnly = false;
                btnsavembmain.Text = save;
                ClearData_Sparemain();   //clear all fields

                btndeletembmain.Enabled = false;
                RefereshGrid_MBsub();   //get the master
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Machine Repair Main is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        //clear all fields
        public void ClearData_MBmain()
        {
            txtmbmainid.Text = "";
            txtmbmaindesc.Text = "";
            btnsavembmain.ForeColor = Color.Lime;
        }

        //clear all fields
        public void ClearData_Sparemain()
        {
            txtsparemainid.Text = "";
            txtsparemaindesc.Text = "";
            btnsavesparemain.ForeColor = Color.Lime;
        }

        private void btnsavembmain_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtmbmainid.Text != "" && txtmbmaindesc.Text != "")
                {
                    btndeletembmain.Enabled = false;
                    if (btnsavembmain.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from MB_MAIN_CATEGORY where V_MB_MAIN_ID='" + txtmbmainid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from MB_MAIN_CATEGORY where V_MB_MAIN_DESC='" + txtmbmaindesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //insert
                            SqlCommand cmd = new SqlCommand("insert into MB_MAIN_CATEGORY values('" + txtmbmainid.Text + "','" + txtmbmaindesc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_MBmain();   //get the master

                            txtmbmainid.ReadOnly = false;
                            ClearData_MBmain();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvmbmain.Rows.Count; j++)
                            {
                                if (dgvmbmain.Rows[j].Cells[0].Value.ToString().Equals(txtmbmainid.Text) || dgvmbmain.Rows[j].Cells[1].Value.ToString().Equals(txtmbmaindesc.Text))
                                {
                                    dgvmbmain.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Machine Repair Main Category Already Exists";
                                    return;
                                }
                            }
                        }
                    }

                    if (btnsavembmain.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from MB_MAIN_CATEGORY where V_MB_MAIN_DESC='" + txtmbmaindesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || mbmaindesc == txtmbmaindesc.Text)
                        {
                            //update
                            SqlCommand cmd = new SqlCommand("Update MB_MAIN_CATEGORY set V_MB_MAIN_DESC='" + txtmbmaindesc.Text + "' where V_MB_MAIN_ID='" + txtmbmainid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_MBmain();   //get the master

                            txtmbmainid.ReadOnly = false;
                            btnsavembmain.Text = save;
                            ClearData_MBmain();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvmbmain.Rows.Count; j++)
                            {
                                if (dgvmbmain.Rows[j].Cells[1].Value.ToString().Equals(txtmbmaindesc.Text))
                                {
                                    dgvmbmain.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Machine Repair Main Category Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    RefereshGrid_MBsub();   //get the master
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        private void btneditmbsub_Click(object sender, EventArgs e)
        {
            RowSelected_MBsub();    //get the selected row
        }

        public void RowSelected_MBsub()
        {
            if (dgvmbsub.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String subId = dgvmbsub.SelectedRows[0].Cells[1].Value + string.Empty;
                String subDesc = dgvmbsub.SelectedRows[0].Cells[2].Value + string.Empty;
                String mainDesc = dgvmbsub.SelectedRows[0].Cells[0].Value + string.Empty;

                txtmbsubid.Text = subId;
                txtmbsubdesc.Text = subDesc;
                cmbmbmaindesc.Text = mainDesc;

                txtmbsubid.ReadOnly = true;
                btnsavembsub.Text = update;
                btndeletembsub.Enabled = true;
                btnsavembsub.ForeColor = Color.Red;
                mbsubdesc = subDesc;
                Hide_MBSUB();
            }
        }

        private void cmbmbmaindesc_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Hide_MBSUB();   //hide the rows

            // check if fields changed
            if (txtmbsubid.Text == "" && txtmbsubdesc.Text == "")
            {
                btnsavembsub.ForeColor = Color.Lime;
            }
            else
            {
                btnsavembsub.ForeColor = Color.Red;
            }
        }

        public void Hide_MBSUB()
        {
            //show all the rows
            for (int i = 0; i < dgvmbsub.Rows.Count; i++)
            {
                dgvmbsub.Rows[i].IsVisible = true;
            }

            //hide all the rows other than the selected machine breakdowm main category
            for (int i = 0; i < dgvmbsub.Rows.Count; i++)
            {
                if (dgvmbsub.Rows[i].Cells[0].Value.ToString() != cmbmbmaindesc.Text && cmbmbmaindesc.Text != "")
                {
                    dgvmbsub.Rows[i].IsVisible = false;
                }
            }
        }

        public void Hide_SpareSUB()
        {
            //show all the rows
            for (int i = 0; i < dgvsparesub.Rows.Count; i++)
            {
                dgvsparesub.Rows[i].IsVisible = true;
            }

            //hide all the rows other than the selected spare main category
            for (int i = 0; i < dgvsparesub.Rows.Count; i++)
            {
                if (dgvsparesub.Rows[i].Cells[0].Value.ToString() != cmbsparemaindesc.Text && cmbsparemaindesc.Text != "")
                {
                    dgvsparesub.Rows[i].IsVisible = false;
                }
            }
        }

        private void dgvmbsub_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_MBsub();    //get the selected row
        }

        private void btndeletembsub_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from MB_SUB_CATEGORY where V_MB_SUB_ID='" + txtmbsubid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                lblmsg.Text = "Record Deleted";
                RefereshGrid_MBsub();   //get the master

                txtmbsubid.ReadOnly = false;
                btnsavembsub.Text = save;
                ClearData_MBsub();      //clear all fields

                btndeletembsub.Enabled = false;
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Machine Repair Sub Category is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        //clear all fields
        public void ClearData_MBsub()
        {
            txtmbsubid.Text = "";
            txtmbsubdesc.Text = "";
            btnsavembsub.ForeColor = Color.Lime;
        }

        //clear all fields
        public void ClearData_Sparesub()
        {
            txtsparesubid.Text = "";
            txtsparesubdesc.Text = "";
            txtsparequantity.Text = "";
            txtsparecost.Text = "";
            btnsavesparesub.ForeColor = Color.Lime;
        }

        private void btnsavembsub_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtmbsubid.Text != "" && txtmbsubdesc.Text != "" && cmbmbmaindesc.Text != "" && cmbmbmaindesc.Text != "--SELECT--")
                {
                    btndeletembsub.Enabled = false;
                    if (btnsavembsub.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from MB_SUB_CATEGORY where V_MB_SUB_ID='" + txtmbsubid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from MB_SUB_CATEGORY where V_MB_SUB_DESC='" + txtmbsubdesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //get the machine breakdown id
                            SqlCommand cmd = new SqlCommand("select V_MB_MAIN_ID from MB_MAIN_CATEGORY where V_MB_MAIN_DESC='" + cmbmbmaindesc.Text + "'", dc.con);
                            String maincode = "";
                            SqlDataReader sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                maincode = sdr.GetValue(0).ToString();
                            }
                            sdr.Close();

                            //insert
                            cmd = new SqlCommand("insert into MB_SUB_CATEGORY values('" + txtmbsubid.Text + "','" + txtmbsubdesc.Text + "','" + maincode + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_MBsub();   //get the master

                            txtmbsubid.ReadOnly = false;
                            ClearData_MBsub();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvmbsub.Rows.Count; j++)
                            {
                                if (dgvmbsub.Rows[j].Cells[0].Value.ToString().Equals(txtmbsubid.Text) || dgvmbsub.Rows[j].Cells[1].Value.ToString().Equals(txtmbsubdesc.Text))
                                {
                                    dgvmbsub.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Machine Repair Sub Category Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsavembsub.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from MB_SUB_CATEGORY where V_MB_SUB_DESC='" + txtmbsubdesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || mbsubdesc == txtmbsubdesc.Text)
                        {
                            //get the id
                            SqlCommand cmd = new SqlCommand("select V_MB_MAIN_ID from MB_MAIN_CATEGORY where V_MB_MAIN_DESC='" + cmbmbmaindesc.Text + "'", dc.con);
                            String maincode = "";
                            SqlDataReader sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                maincode = sdr.GetValue(0).ToString();
                            }
                            sdr.Close();

                            //update
                            cmd = new SqlCommand("Update MB_SUB_CATEGORY set V_MB_SUB_DESC='" + txtmbsubdesc.Text + "',V_MB_MAIN_ID='" + maincode + "' where V_MB_SUB_ID='" + txtmbsubid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_MBsub();   //get the master

                            txtmbsubid.ReadOnly = false;
                            btnsavembsub.Text = save;
                            ClearData_MBsub();    //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvmbsub.Rows.Count; j++)
                            {
                                if (dgvmbsub.Rows[j].Cells[1].Value.ToString().Equals(txtmbsubdesc.Text))
                                {
                                    dgvmbsub.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Machine Repair SUB Category Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        private void btnsavemachinedetails_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (cmbmachinedetails.Text != "--SELECT--" && cmbmachinedetails.Text != "" && txtmachineserialno.Text != "" && txtmachinerfid.Text != "")
                {
                    btndeletemachinedetails.Enabled = false;
                    if (btnsavemachinedetails.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from MACHINE_DETAILS where V_MACHINE_SERIAL_NO='" + txtmachineserialno.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from MACHINE_DETAILS where V_RFID='" + txtmachinerfid.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //get the id
                            SqlCommand cmd = new SqlCommand("select V_MACHINE_ID from MACHINE_DB where V_MACHINE_DESC='" + cmbmachinedetails.Text + "'", dc.con);
                            String maincode = "";
                            SqlDataReader sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                maincode = sdr.GetValue(0).ToString();
                            }
                            sdr.Close();

                            //insert
                            cmd = new SqlCommand("insert into MACHINE_DETAILS values('" + maincode + "','" + txtmachineserialno.Text + "','" + dtpmachinedate.Value.ToString("yyyy-MM-dd") + "','" + txtmachinerfid.Text + "','FALSE')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_MachineDetails();   //get the master

                            ClearData_MachineDetails();    //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvmachinedetails.Rows.Count; j++)
                            {
                                if (dgvmachinedetails.Rows[j].Cells[0].Value.ToString().Equals(txtmachineserialno.Text) || dgvmachinedetails.Rows[j].Cells[2].Value.ToString().Equals(txtmachinerfid.Text))
                                {
                                    dgvmachinedetails.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Machine Details Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsavemachinedetails.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from MACHINE_DETAILS where V_MACHINE_SERIAL_NO='" + txtmachineserialno.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || machineserialno == txtmachineserialno.Text)
                        {
                            //get the id
                            SqlCommand cmd = new SqlCommand("select V_MACHINE_ID from MACHINE_DB where V_MACHINE_DESC='" + cmbmachinedetails.Text + "'", dc.con);
                            String maincode = "";
                            SqlDataReader sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                maincode = sdr.GetValue(0).ToString();
                            }
                            sdr.Close();

                            //update
                            cmd = new SqlCommand("Update MACHINE_DETAILS set V_MACHINE_SERIAL_NO='" + txtmachineserialno.Text + "',V_MACHINE_ID='" + maincode + "',D_PURCHASE_DATE='" + dtpmachinedate.Value.ToString("yyyy-MM-dd") + "' ,V_RFID='" + txtmachinerfid.Text + "' where V_MACHINE_SERIAL_NO='" + machineserialno + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_MachineDetails();   //get the master

                            btnsavemachinedetails.Text = save;
                            ClearData_MachineDetails();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvmachinedetails.Rows.Count; j++)
                            {
                                if (dgvmachinedetails.Rows[j].Cells[0].Value.ToString().Equals(txtmachineserialno.Text))
                                {
                                    dgvmachinedetails.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Machine Details Already Exists";
                                    return;
                                }
                            }
                        }
                    }

                    //show all the rows
                    btnsavemachinedetails.ForeColor = Color.Lime;
                    for (int i = 0; i < dgvmachinedetails.Rows.Count; i++)
                    {
                        dgvmachinedetails.Rows[i].IsVisible = true;
                    }
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        private void btneditgroup_Click(object sender, EventArgs e)
        {
            RowSelected_GroupCategory();    //get the selected row
        }

        public void RowSelected_GroupCategory()
        {
            if (dgvgroup.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String Id = dgvgroup.SelectedRows[0].Cells[0].Value + string.Empty;
                String desc = dgvgroup.SelectedRows[0].Cells[1].Value + string.Empty;
                String ledname = dgvgroup.SelectedRows[0].Cells[2].Value + string.Empty;

                txtgroupid.Text = Id;
                txtgroupdesc.Text = desc;
                txtgroupledname.Text = ledname;

                txtgrouprfid.Text = dgvgroup.SelectedRows[0].Cells[3].Value + string.Empty;
                cmbgroupstatus.Text = dgvgroup.SelectedRows[0].Cells[4].Value + string.Empty;

                txtgroupid.ReadOnly = true;
                btnsavegroup.Text = update;
                btndeletegroup.Enabled = true;
                btnsavegroup.ForeColor = Color.Red;
                groupdesc = desc;
            }
        }

        private void dgvgroup_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_GroupCategory();    //get the selected row
        }

        private void btndeletegroup_Click(object sender, EventArgs e)
        {
            try
            {
                //get the employee group has production details
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM HANGER_HISTORY where EMP_ID='" + txtgroupid.Text + "'", dc.con);
                int count = int.Parse(cmd.ExecuteScalar() + "");

                if (count == 0)
                {
                    //delete the selected row
                    cmd = new SqlCommand("Delete from EMPLOYEE_GROUP_CATEGORY where V_GROUP_ID='" + txtgroupid.Text + "'", dc.con);
                    cmd.ExecuteNonQuery();

                    //delete the selected row
                    cmd = new SqlCommand("Delete from EMPLOYEE_GROUPS where V_GROUP_ID='" + txtgroupid.Text + "'", dc.con);
                    cmd.ExecuteNonQuery();

                    lblmsg.Text = "Record Deleted";
                }
                else
                {
                    lblmsg.Text = "Employee Already Used for Production";
                }
                RefereshGrid_GroupCategory();  //get the master

                RefereshGrid_Groups();  //get the master

                txtgroupid.ReadOnly = false;
                btnsavegroup.Text = save;
                ClearData_GroupCategory();   //clear all fields

                btndeletegroup.Enabled = false;
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Group ID is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        //clear all fields
        public void ClearData_GroupCategory()
        {
            txtgroupid.Text = "";
            txtgroupdesc.Text = "";
            txtgroupledname.Text = "";
            txtgrouprfid.Text = "";
            cmbgroupstatus.Text = "--SELECT--";
            btnsavegroup.ForeColor = Color.Lime;
        }

        private void btnsavegroup_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtgroupid.Text != "" && txtgroupdesc.Text != "" && txtgroupledname.Text != "" && txtgrouprfid.Text != "" && cmbgroupstatus.Text != "--SELECT--")
                {
                    btndeletegroup.Enabled = false;
                    if (btnsavegroup.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from EMPLOYEE_GROUP_CATEGORY where V_GROUP_ID='" + txtgroupid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from EMPLOYEE_GROUP_CATEGORY where V_GROUP_DESC='" + txtgroupdesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if group id already used for employee
                        cmd1 = new SqlCommand("select count(*) from EMPLOYEE where V_EMP_ID='" + txtgroupid.Text + "'", dc.con);
                        Int32 c = int.Parse(cmd1.ExecuteScalar().ToString());
                        if (c != 0)
                        {
                            lblmsg.Text = "Group ID already Assigned for an Employee";
                            return;
                        }

                        //check if id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //insert
                            SqlCommand cmd = new SqlCommand("insert into EMPLOYEE_GROUP_CATEGORY values('" + txtgroupid.Text + "','" + txtgroupdesc.Text + "','" + txtgroupledname.Text + "','" + txtgrouprfid.Text + "','" + cmbgroupstatus.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_GroupCategory();    //get the master

                            txtgroupid.ReadOnly = false;
                            ClearData_GroupCategory();   //clear all fields

                            RefereshGrid_Groups();   //get the master
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvgroup.Rows.Count; j++)
                            {
                                if (dgvgroup.Rows[j].Cells[0].Value.ToString().Equals(txtgroupid.Text) || dgvgroup.Rows[j].Cells[1].Value.ToString().Equals(txtgroupdesc.Text))
                                {
                                    dgvgroup.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Group Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsavegroup.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from EMPLOYEE_GROUP_CATEGORY where V_GROUP_DESC='" + txtgroupdesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || groupdesc == txtgroupdesc.Text)
                        {
                            //update
                            SqlCommand cmd = new SqlCommand("Update EMPLOYEE_GROUP_CATEGORY set V_GROUP_DESC='" + txtgroupdesc.Text + "',V_GROUP_LED_NAME='" + txtgroupledname.Text + "',V_GROUP_RFID='" + txtgrouprfid.Text + "',V_STATUS='" + cmbgroupstatus.Text + "' where V_GROUP_ID='" + txtgroupid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_GroupCategory();    //get the master

                            txtgroupid.ReadOnly = false;
                            ClearData_GroupCategory();   //clear all fields

                            btnsavegroup.Text = save;
                            RefereshGrid_Groups();   //get the master
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvgroup.Rows.Count; j++)
                            {
                                if (dgvgroup.Rows[j].Cells[0].Value.ToString().Equals(txtgroupid.Text) || dgvgroup.Rows[j].Cells[1].Value.ToString().Equals(txtgroupdesc.Text))
                                {
                                    dgvgroup.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Group Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        private void cmbgroupid_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Refresh_Group();    //get the selected row
        }

        public void Refresh_Group()
        {
            //get group id
            SqlCommand cmd = new SqlCommand("select V_GROUP_ID from EMPLOYEE_GROUP_CATEGORY where V_GROUP_DESC='" + cmbgroupdesc.Text + "'", dc.con);
            String groupid = "";
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                groupid = sdr.GetValue(0).ToString();
            }
            sdr.Close();

            //get all the employees for the group
            SqlDataAdapter sda = new SqlDataAdapter("select eg.V_GROUP_DESC,e.V_EMP_ID,e.V_FIRST_NAME from EMPLOYEE_GROUP_CATEGORY eg,EMPLOYEE e,EMPLOYEE_GROUPS es where eg.V_GROUP_ID=es.V_GROUP_ID and es.V_EMP_ID=e.V_EMP_ID and es.V_GROUP_ID='" + groupid + "'", dc.con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            sda.Dispose();
            dgvemployeegroup.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dgvemployeegroup.Rows.Add(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString());
            }
        }

        private void btnaddemployee_Click(object sender, EventArgs e)
        {
            RowSeleted_EmployeeGroup();    //get the selected row
        }

        public void RowSeleted_EmployeeGroup()
        {
            if (dgvemployeeselect.SelectedRows.Count > 0)
            {
                if (cmbgroupdesc.Text == "")
                {
                    lblmsg.Text = "Please Select a Group";
                    return;
                }

                String empid = dgvemployeeselect.SelectedRows[0].Cells[0].Value.ToString();

                //get group id
                SqlCommand cmd = new SqlCommand("select V_GROUP_ID from EMPLOYEE_GROUP_CATEGORY where V_GROUP_DESC='" + cmbgroupdesc.Text + "'", dc.con);
                String groupid = "";
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    groupid = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //check if employee already exists in the group
                cmd = new SqlCommand("select count(*) from EMPLOYEE_GROUPS where V_EMP_ID='" + empid + "' and V_GROUP_ID='" + groupid + "'", dc.con);
                Int32 k = int.Parse(cmd.ExecuteScalar().ToString());

                if (k != 0)
                {
                    //select row if exists
                    lblmsg.Text = "Employee Already Exists in this Group";
                    for (int i = 0; i < dgvemployeegroup.Rows.Count; i++)
                    {
                        if (dgvemployeegroup.Rows[i].Cells[1].Value.ToString() == empid)
                        {
                            dgvemployeegroup.Rows[i].IsSelected = true;
                            break;
                        }
                    }
                    return;
                }

                //insert
                cmd = new SqlCommand("insert into EMPLOYEE_GROUPS values('" + groupid + "','" + empid + "')", dc.con);
                cmd.ExecuteNonQuery();


                Refresh_Group();    //get the selected row
                lblmsg.Text = "Employee Added to Group";
            }
        }

        private void btndeleteemployee_Click(object sender, EventArgs e)
        {
            if (dgvemployeegroup.SelectedRows.Count > 0)
            {
                String empid = dgvemployeegroup.SelectedRows[0].Cells[1].Value.ToString();

                //get the gorup id
                SqlCommand cmd = new SqlCommand("select V_GROUP_ID from EMPLOYEE_GROUP_CATEGORY where V_GROUP_DESC='" + cmbgroupdesc.Text + "'", dc.con);
                String groupid = "";
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.Read())
                {
                    groupid = sdr.GetValue(0).ToString();
                }
                sdr.Close();

                //delete the employee from the gorup
                cmd = new SqlCommand("delete from EMPLOYEE_GROUPS where V_EMP_ID='" + empid + "' and V_GROUP_ID='" + groupid + "'", dc.con);
                cmd.ExecuteNonQuery();

                Refresh_Group();    //get the selected row

                lblmsg.Text = "Employee Removed from Group";
            }
        }

        private void dgvemployeeselect_DoubleClick(object sender, EventArgs e)
        {
            RowSeleted_EmployeeGroup();    //get the selected row
        }

        private void btneditskill_Click(object sender, EventArgs e)
        {
            RowSelected_Skill();     //get the selected row
        }

        public void RowSelected_Skill()
        {
            if (dgvskill.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                txtskilllevel.Text = dgvskill.SelectedRows[0].Cells[0].Value + string.Empty;
                txtefficiency.Text = dgvskill.SelectedRows[0].Cells[1].Value + string.Empty;
                txtskillrate.Text = dgvskill.SelectedRows[0].Cells[2].Value + string.Empty;

                txtskilllevel.ReadOnly = true;
                btnsaveskill.Text = update;
                btndeleteskill.Enabled = true;
                btnsaveskill.ForeColor = Color.Red;
            }
        }

        private void dgvskill_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Skill();    //get the selected row
        }

        private void btndeleteskill_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from SKILL_RATE where V_SKILL_LEVEL='" + txtskilllevel.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                lblmsg.Text = "Record Deleted";
                RefereshGrid_Skill();   //get the master

                txtskilllevel.ReadOnly = false;
                btnsaveskill.Text = save;
                ClearData_Skill();   //clear all fields

                btndeleteskill.Enabled = false;
                RefereshGrid_Employee();   //get the master
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Skill Level is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        //clear all fields
        public void ClearData_Skill()
        {
            txtskilllevel.Text = "";
            txtefficiency.Text = "";
            txtskillrate.Text = "";
            btnsaveskill.ForeColor = Color.Lime;
        }

        private void btnsaveskill_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtskilllevel.Text != "" && txtefficiency.Text != "" && txtskillrate.Text != "")
                {
                    btndeleteskill.Enabled = false;
                    if (btnsaveskill.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from SKILL_RATE where V_SKILL_LEVEL='" + txtskilllevel.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //check if id  exists
                        if (i == 0)
                        {
                            //insert
                            SqlCommand cmd = new SqlCommand("insert into SKILL_RATE values('" + txtskilllevel.Text + "','" + txtefficiency.Text + "','" + txtskillrate.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_Skill();   //get the master

                            txtskilllevel.ReadOnly = false;
                            ClearData_Skill();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvskill.Rows.Count; j++)
                            {
                                if (dgvskill.Rows[j].Cells[0].Value.ToString().Equals(txtskilllevel.Text))
                                {
                                    dgvskill.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Skill Level Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsaveskill.Text == update)
                    {
                        //update
                        SqlCommand cmd = new SqlCommand("Update SKILL_RATE set I_EFFICIENCY='" + txtefficiency.Text + "',D_SKILL_RATE='" + txtskillrate.Text + "' where V_SKILL_LEVEL='" + txtskilllevel.Text + "'", dc.con);
                        cmd.ExecuteNonQuery();

                        lblmsg.Text = "Records Updated";
                        RefereshGrid_Skill();   //get the master

                        txtskilllevel.ReadOnly = false;
                        ClearData_Skill();   //clear all fields

                        btnsaveskill.Text = save;
                    }
                    RefereshGrid_Employee();   //get the master
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        private void radButton1_Click_1(object sender, EventArgs e)
        {
            //open master
            Masters cm = new Masters();
            cm.Show();
            cm.Form_Location1("Skill");
        }

        private void txtgroupid_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtgroupid.Text == "" && txtgroupdesc.Text == "" && txtgroupledname.Text == "" && txtgrouprfid.Text == "")
            {
                btnsavegroup.ForeColor = Color.Lime;
            }
            else
            {
                btnsavegroup.ForeColor = Color.Red;
            }
        }

        private void txtgroupdesc_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtgroupid.Text == "" && txtgroupdesc.Text == "" && txtgroupledname.Text == "" && txtgrouprfid.Text == "")
            {
                btnsavegroup.ForeColor = Color.Lime;
            }
            else
            {
                btnsavegroup.ForeColor = Color.Red;
            }
        }

        private void txtgroupledname_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtgroupid.Text == "" && txtgroupdesc.Text == "" && txtgroupledname.Text == "" && txtgrouprfid.Text == "")
            {
                btnsavegroup.ForeColor = Color.Lime;
            }
            else
            {
                btnsavegroup.ForeColor = Color.Red;
            }
        }

        private void txtgrouprfid_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtgroupid.Text == "" && txtgroupdesc.Text == "" && txtgroupledname.Text == "" && txtgrouprfid.Text == "")
            {
                btnsavegroup.ForeColor = Color.Lime;
            }
            else
            {
                btnsavegroup.ForeColor = Color.Red;
            }
        }

        private void txtskilllevel_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtskilllevel.Text == "" && txtskillrate.Text == "" && txtefficiency.Text == "")
            {
                btnsaveskill.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveskill.ForeColor = Color.Red;
            }
        }

        private void txtefficiency_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtskilllevel.Text == "" && txtskillrate.Text == "" && txtefficiency.Text == "")
            {
                btnsaveskill.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveskill.ForeColor = Color.Red;
            }
        }

        private void txtskillrate_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtskilllevel.Text == "" && txtskillrate.Text == "" && txtefficiency.Text == "")
            {
                btnsaveskill.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveskill.ForeColor = Color.Red;
            }
        }

        private void txtmachineid_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtmachineid.Text == "" && txtmachinedesc.Text == "" && txtmodel.Text == "" && txtattachment1.Text == "" && txtattachment2.Text == "")
            {
                btnsavemachine.ForeColor = Color.Lime;
            }
            else
            {
                btnsavemachine.ForeColor = Color.Red;
            }
        }

        private void txtmachinedesc_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtmachineid.Text == "" && txtmachinedesc.Text == "" && txtmodel.Text == "" && txtattachment1.Text == "" && txtattachment2.Text == "")
            {
                btnsavemachine.ForeColor = Color.Lime;
            }
            else
            {
                btnsavemachine.ForeColor = Color.Red;
            }
        }

        private void txtmodel_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtmachineid.Text == "" && txtmachinedesc.Text == "" && txtmodel.Text == "" && txtattachment1.Text == "" && txtattachment2.Text == "")
            {
                btnsavemachine.ForeColor = Color.Lime;
            }
            else
            {
                btnsavemachine.ForeColor = Color.Red;
            }
        }

        private void txtattachment1_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtmachineid.Text == "" && txtmachinedesc.Text == "" && txtmodel.Text == "" && txtattachment1.Text == "" && txtattachment2.Text == "")
            {
                btnsavemachine.ForeColor = Color.Lime;
            }
            else
            {
                btnsavemachine.ForeColor = Color.Red;
            }
        }

        private void txtattachment2_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtmachineid.Text == "" && txtmachinedesc.Text == "" && txtmodel.Text == "" && txtattachment1.Text == "" && txtattachment2.Text == "")
            {
                btnsavemachine.ForeColor = Color.Lime;
            }
            else
            {
                btnsavemachine.ForeColor = Color.Red;
            }
        }

        private void txtquantity_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtmachineid.Text == "" && txtmachinedesc.Text == "" && txtmodel.Text == "" && txtattachment1.Text == "" && txtattachment2.Text == "")
            {
                btnsavemachine.ForeColor = Color.Lime;
            }
            else
            {
                btnsavemachine.ForeColor = Color.Red;
            }
        }

        private void txtmachineserialno_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (cmbmachinedetails.Text == "" && txtmachineserialno.Text == "" && txtmachinerfid.Text == "")
            {
                btnsavemachinedetails.ForeColor = Color.Lime;
            }
            else
            {
                btnsavemachinedetails.ForeColor = Color.Red;
            }
        }

        private void txtmachinerfid_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (cmbmachinedetails.Text == "" && txtmachineserialno.Text == "" && txtmachinerfid.Text == "")
            {
                btnsavemachinedetails.ForeColor = Color.Lime;
            }
            else
            {
                btnsavemachinedetails.ForeColor = Color.Red;
            }
        }

        private void txtmbmainid_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtmbmainid.Text == "" && txtmbmaindesc.Text == "")
            {
                btnsavembmain.ForeColor = Color.Lime;
            }
            else
            {
                btnsavembmain.ForeColor = Color.Red;
            }
        }

        private void txtmbmaindesc_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtmbmainid.Text == "" && txtmbmaindesc.Text == "")
            {
                btnsavembmain.ForeColor = Color.Lime;
            }
            else
            {
                btnsavembmain.ForeColor = Color.Red;
            }
        }

        private void txtmbsubid_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtmbsubid.Text == "" && txtmbsubdesc.Text == "")
            {
                btnsavembsub.ForeColor = Color.Lime;
            }
            else
            {
                btnsavembsub.ForeColor = Color.Red;
            }
        }

        private void txtmbsubdesc_TextChanged(object sender, EventArgs e)
        {
            // check if fields changed
            if (txtmbsubid.Text == "" && txtmbsubdesc.Text == "")
            {
                btnsavembsub.ForeColor = Color.Lime;
            }
            else
            {
                btnsavembsub.ForeColor = Color.Red;
            }
        }

        private void dgvcolor_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvcolor.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvcolor.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvcolor.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvcolor.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvarticle_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvarticle.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvarticle.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvarticle.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvarticle.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvsize_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvsize.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvsize.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvsize.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvsize.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvcustomer_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvcustomer.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvcustomer.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvcustomer.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvcustomer.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvoperation_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvoperation.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvoperation.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvoperation.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvoperation.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dvgcontractor_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dvgcontractor.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dvgcontractor.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dvgcontractor.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dvgcontractor.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvemployee_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvemployee.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvemployee.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvemployee.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvemployee.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvgroup_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvgroup.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvgroup.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvgroup.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvgroup.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvemployeegroup_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvemployeegroup.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvemployeegroup.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvemployeegroup.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvemployeegroup.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvemployeeselect_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvemployeeselect.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvemployeeselect.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvemployeeselect.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvemployeeselect.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvskill_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvskill.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvskill.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvskill.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvskill.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvmachines_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvmachines.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvmachines.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvmachines.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvmachines.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvmachinedetails_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvmachinedetails.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvmachinedetails.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvmachinedetails.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvmachinedetails.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvmbmain_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvmbmain.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvmbmain.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvmbmain.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvmbmain.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvmbsub_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvmbsub.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvmbsub.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvmbsub.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvmbsub.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvqcmain_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvqcmain.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvqcmain.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvqcmain.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvqcmain.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvqcsub_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvqcsub.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvqcsub.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvqcsub.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvqcsub.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvuser1_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvuser1.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvuser1.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvuser1.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvuser1.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvuser2_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvuser2.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvuser2.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvuser2.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvuser2.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvuser3_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvuser3.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvuser3.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvuser3.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvuser3.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvuser4_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvuser4.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvuser4.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvuser4.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvuser4.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvuser5_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvuser5.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvuser5.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvuser5.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvuser5.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvuser6_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvuser6.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvuser6.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvuser6.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvuser6.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvuser7_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvuser7.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvuser7.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvuser7.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvuser7.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvuser8_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvuser8.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvuser8.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvuser8.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvuser8.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvuser9_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvuser9.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvuser9.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvuser9.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvuser9.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvuser10_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvuser10.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvuser10.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvuser10.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvuser10.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvdesignoperation_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvdesignoperation.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvdesignoperation.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvdesignoperation.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvdesignoperation.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvdesignsequence_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //change grid fore color if these theme are selected
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvdesignsequence.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvdesignsequence.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvdesignsequence.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvdesignsequence.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void btnaddsequence_Click(object sender, EventArgs e)
        {
            RowSelected();    //get the selected row
        }

        private void dgvdesignoperation_DoubleClick(object sender, EventArgs e)
        {
            RowSelected();    //get the selected row
        }

        private void btndeletesequence_Click(object sender, EventArgs e)
        {
            if (dgvdesignsequence.SelectedRows.Count > 0)
            {
                dgvdesignsequence.Rows.RemoveAt(dgvdesignsequence.SelectedRows[0].Index);
                btnsavesequence.ForeColor = Color.Red;
            }
            UpdateSeqno();   //reorder the sequence
            DesignSummary();   //get the design summary
        }

        public void UpdateSeqno()
        {
            //reorder the sequence
            for (int i = 0; i < dgvdesignsequence.Rows.Count; i++)
            {
                dgvdesignsequence.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void btnsavesequence_Click(object sender, EventArgs e)
        {
            DialogResult result = RadMessageBox.Show("Are you sure to save this record", "SmartMRT", MessageBoxButtons.YesNo, RadMessageIcon.Question);
            if (result.Equals(DialogResult.No))
            {
                return;
            }

            try
            {
                ////get article id
                //String article = "";
                //SqlCommand cmd = new SqlCommand("SELECT V_ARTICLE_ID from ARTICLE_DB where V_ARTICLE_DESC='" + cmbdesignarticle.Text + "'", dc.con);
                //article = cmd.ExecuteScalar() + "";

                //hanafi|21/7/2021| get article id from cmbdesignarticle selectedvalue
                String ArtID = cmbdesignarticle.SelectedValue.ToString(); //get article id from cmbdesignarticle value

                if (ArtID == "0")
                {
                    return;
                }

                UpdateSeqno();   //reporder the sequence

                dgvdesignsequence.Rows[0].Cells[1].Value = "1";
                String sequence_no = "1";
                int sequence_no1 = 1;

                if (dgvdesignsequence.Rows.Count == 0)
                {
                    return;
                }

                for (int i = 0; i < dgvdesignsequence.Rows.Count; i++)
                {
                    if (sequence_no != "")
                    {
                        sequence_no1 = Int32.Parse(sequence_no);
                    }

                    //check if the sequence is valid and starts with 1
                    sequence_no = dgvdesignsequence.Rows[i].Cells[1].Value + string.Empty;
                    Regex r = new Regex("^[0-9]*$");
                    if (!(r.IsMatch(sequence_no)) || sequence_no == "" || sequence_no == "0")
                    {
                        sequence_no = "1";
                    }

                    int n = sequence_no1 + 1;

                    //corfirm box to reorder sequence
                    if (sequence_no != sequence_no1.ToString() && sequence_no != n.ToString())
                    {
                        DialogResult result1 = MessageBox.Show("Sequence is not in Order. Do you want to make it in Order", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (result1.Equals(DialogResult.OK))
                        {
                            SequenceOrdering();
                        }
                    }
                }

                //check if the article has station assign
                SqlCommand cmd = new SqlCommand("select count(*) from STATION_ASSIGN where V_ARTICLE_ID='" + ArtID + "'", dc.con);
                int count = int.Parse(cmd.ExecuteScalar() + "");

                if (count != 0)
                {
                    //corfirm box before updating the design sequence for the article
                    DialogResult result2 = RadMessageBox.Show("Changes to this Article will Affect all Station Assigns?", "SmartMRT", MessageBoxButtons.OKCancel, RadMessageIcon.Question);
                    if (result2.Equals(DialogResult.Cancel))
                    {
                        return;
                    }
                }

                //delete the previous design sequence
                cmd = new SqlCommand("delete from DESIGN_SEQUENCE where V_ARTICLE_ID='" + ArtID + "'", dc.con);
                cmd.ExecuteNonQuery();

                for (int i = 0; i < dgvdesignsequence.Rows.Count; i++)
                {
                    String opcode = dgvdesignsequence.Rows[i].Cells[2].Value.ToString();
                    sequence_no = dgvdesignsequence.Rows[i].Cells[1].Value.ToString();
                    String op_seq = dgvdesignsequence.Rows[i].Cells[0].Value.ToString();

                    //insert
                    cmd = new SqlCommand("insert into DESIGN_SEQUENCE values('" + opcode + "','" + ArtID + "','" + op_seq + "','" + sequence_no + "')", dc.con);
                    cmd.ExecuteNonQuery();
                }

                //get the sequence from the station which are not in design sequence for the article
                cmd = new SqlCommand("DELETE FROM STATION_ASSIGN WHERE V_ARTICLE_ID='" + ArtID + "' and I_SEQUENCE_NO NOT IN(SELECT D.I_SEQUENCE_NO FROM DESIGN_SEQUENCE D WHERE V_ARTICLE_ID='" + ArtID + "')", dc.con);
                cmd.ExecuteNonQuery();

                for (int i = 0; i < dgvdesignsequence.Rows.Count; i++)
                {
                    String edit = dgvdesignsequence.Rows[i].Cells[4].Value.ToString();
                    if (edit == "Y")
                    {
                        //get the sequence
                        SqlDataAdapter sda = new SqlDataAdapter("select I_SEQUENCE_NO from STATION_ASSIGN where I_SEQUENCE_NO>='" + i + "' and V_ARTICLE_ID='" + ArtID + "' order by I_SEQUENCE_NO desc", dc.con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        sda.Dispose();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            int seq = int.Parse(dt.Rows[j][0].ToString());
                            int seq1 = seq + 1;

                            //update
                            cmd = new SqlCommand("update STATION_ASSIGN set I_SEQUENCE_NO='" + seq1 + "' where I_SEQUENCE_NO='" + seq + "'", dc.con);
                            cmd.ExecuteNonQuery();
                        }

                        //get all the mo details for the article
                        sda = new SqlDataAdapter("select distinct V_MO_NO,V_MO_LINE,I_ROW_NO from STATION_ASSIGN where  V_ARTICLE_ID='" + ArtID + "'", dc.con);
                        dt = new DataTable();
                        sda.Fill(dt);
                        sda.Dispose();
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            String MO = dt.Rows[j][0].ToString();
                            String MOLINE = dt.Rows[j][1].ToString();
                            String ROWNO = dt.Rows[j][2].ToString();

                            ////insert
                            cmd = new SqlCommand("insert into STATION_ASSIGN values('" + MO + "','" + MOLINE + "','" + i + "','0','" + ROWNO + "','0','" + ArtID + "','0')", dc.con);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                lblmsg.Text = "Records Saved";
                btnsavesequence.ForeColor = Color.Lime;
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        //reorder the sequence
        public void SequenceOrdering()
        {
            String sequence_no = "1";
            int n = 1;
            dgvdesignsequence.Rows[0].Cells[1].Value = "1";

            for (int i = 0; i < dgvdesignsequence.Rows.Count; i++)
            {
                sequence_no = dgvdesignsequence.Rows[i].Cells[1].Value + string.Empty;
                if (sequence_no == n.ToString())

                {
                    continue;
                }
                n = n + 1;

                dgvdesignsequence.Rows[i].Cells[1].Value = n;
            }
        }

        private void cmbdesignarticle_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Design_Selected();    //get the selected row
        }

        public void Design_Selected()
        {
            dgvdesignsequence.Rows.Clear();
            //String article = "";

            ////get article id
            //SqlCommand cmd = new SqlCommand("SELECT V_ARTICLE_ID from ARTICLE_DB where V_ARTICLE_DESC='" + cmbdesignarticle.Text + "'", dc.con);
            //article = cmd.ExecuteScalar() + "";

            //Hanafi|21/7/2021| get article id from cmbdesignarticle selectedvalue
            if (cmbdesignarticle.SelectedValue == null)
            {
                return;
            }

            String ArtID = cmbdesignarticle .SelectedValue.ToString(); 

            if (ArtID == "0")
            {
                return;
            }

            //get operation for the article
            decimal total_piece = 0;
            int total_sam = 0;
            SqlDataAdapter da = new SqlDataAdapter("SELECT V_OPERATION_CODE,I_SEQUENCE_NO,I_OPERATION_SEQUENCE_NO FROM DESIGN_SEQUENCE WHERE V_ARTICLE_ID='" + ArtID + "' ORDER BY I_SEQUENCE_NO", dc.con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            da.Dispose();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String opcode = dt.Rows[i][0].ToString();
                String opdesc = "";
                String seqno = dt.Rows[i][1].ToString();
                String op_seq_no = dt.Rows[i][2].ToString();
                String piecerate = "0";
                String sam = "0";

                //get piecerate and sam for the article
                da = new SqlDataAdapter("SELECT V_OPERATION_DESC,D_PIECERATE,D_SAM FROM OPERATION_DB WHERE V_OPERATION_CODE='" + opcode + "'", dc.con);
                DataTable dt1 = new DataTable();
                da.Fill(dt1);
                da.Dispose();
                for (int k = 0; k < dt1.Rows.Count; k++)
                {
                    opdesc = dt1.Rows[k][0].ToString();
                    piecerate = dt1.Rows[k][1].ToString();
                    sam = dt1.Rows[k][2].ToString();
                }

                total_piece += Convert.ToDecimal(piecerate);
                total_sam += int.Parse(sam);

                //add to grid
                dgvdesignsequence.Rows.Add(op_seq_no, seqno, opcode, opdesc, 'N', piecerate, sam);
                dgvdesignsequence.Visible = true;
            }

            lbldesignsummary.Text = "Total Operations : " + dgvdesignsequence.Rows.Count + "            Total Piece Rate : " + total_piece.ToString("0.##") + "             Total SAM : " + total_sam;
        }

        public void RowSelected_SapreMain()
        {
            if (dgvsparemain.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String mainId = dgvsparemain.SelectedRows[0].Cells[0].Value + string.Empty;
                String mainDesc = dgvsparemain.SelectedRows[0].Cells[1].Value + string.Empty;

                txtsparemainid.Text = mainId;
                txtsparemaindesc.Text = mainDesc;

                txtsparemainid.ReadOnly = true;
                btnsavesparemain.Text = update;
                btndeletesparemain.Enabled = true;
                btnsavesparemain.ForeColor = Color.Red;
                sparemaindesc = mainDesc;
            }
        }

        private void btneditsparemain_Click(object sender, EventArgs e)
        {
            RowSelected_SapreMain();    //get the selected row
        }

        private void dgvsparemain_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_SapreMain();   //get the selected row
        }

        private void btndeletesparemain_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from SPARE_MAIN_CATEGORY where V_SPARE_MAIN_ID='" + txtsparemainid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                //delete the selected row
                cmd = new SqlCommand("DELETE FROM SPARE_SUB_CATEGORY WHERE V_SPARE_MAIN_ID NOT IN(SELECT D.V_SPARE_MAIN_ID FROM SPARE_MAIN_CATEGORY D)", dc.con);
                cmd.ExecuteNonQuery();

                lblmsg.Text = "Record Deleted";
                RefereshGrid_Sparemain();   //get the master

                txtsparemainid.ReadOnly = false;
                btnsavesparemain.Text = save;
                ClearData_Sparemain();   //clear all fields

                btndeletesparemain.Enabled = false;
                RefereshGrid_Sparesub();   //get the master
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Spare Parts Main is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        private void btnsavesparemain_Click(object sender, EventArgs e)
        {
            try
            {
                //check if all the fields are inserted
                if (txtsparemainid.Text != "" && txtsparemaindesc.Text != "")
                {
                    btndeletesparemain.Enabled = false;
                    if (btnsavesparemain.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from SPARE_MAIN_CATEGORY where V_SPARE_MAIN_ID='" + txtsparemainid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from SPARE_MAIN_CATEGORY where V_SPARE_MAIN_DESC='" + txtsparemaindesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //insert
                            SqlCommand cmd = new SqlCommand("insert into SPARE_MAIN_CATEGORY values('" + txtsparemainid.Text + "','" + txtsparemaindesc.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_Sparemain();   //get the master

                            txtsparemainid.ReadOnly = false;
                            ClearData_Sparemain();   //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvsparemain.Rows.Count; j++)
                            {
                                if (dgvsparemain.Rows[j].Cells[0].Value.ToString().Equals(txtsparemainid.Text) || dgvsparemain.Rows[j].Cells[1].Value.ToString().Equals(txtsparemaindesc.Text))
                                {
                                    dgvsparemain.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Spare Parts Main Category Already Exists";
                                    return;
                                }
                            }
                        }
                    }

                    if (btnsavesparemain.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from SPARE_MAIN_CATEGORY where V_SPARE_MAIN_DESC='" + txtsparemaindesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || sparemaindesc == txtsparemaindesc.Text)
                        {
                            //update
                            SqlCommand cmd = new SqlCommand("Update SPARE_MAIN_CATEGORY set V_SPARE_MAIN_DESC='" + txtsparemaindesc.Text + "' where V_SPARE_MAIN_ID='" + txtsparemainid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_Sparemain();   //get the master

                            txtsparemainid.ReadOnly = false;
                            btnsavesparemain.Text = save;
                            ClearData_Sparemain();    //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvsparemain.Rows.Count; j++)
                            {
                                if (dgvsparemain.Rows[j].Cells[1].Value.ToString().Equals(txtsparemaindesc.Text))
                                {
                                    dgvsparemain.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Spare Parts Main Category Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        public void RowSelected_Sparesub()
        {
            if (dgvsparesub.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                String subId = dgvsparesub.SelectedRows[0].Cells[1].Value + string.Empty;
                String subDesc = dgvsparesub.SelectedRows[0].Cells[2].Value + string.Empty;
                String mainDesc = dgvsparesub.SelectedRows[0].Cells[0].Value + string.Empty;
                String cost = dgvsparesub.SelectedRows[0].Cells[3].Value + string.Empty;
                String qty = dgvsparesub.SelectedRows[0].Cells[4].Value + string.Empty;

                txtsparesubid.Text = subId;
                txtsparesubdesc.Text = subDesc;
                cmbsparemaindesc.Text = mainDesc;
                txtsparequantity.Text = qty;
                txtsparecost.Text = cost;

                txtsparesubid.ReadOnly = true;
                btnsavesparesub.Text = update;
                btndeletesparesub.Enabled = true;
                btnsavesparesub.ForeColor = Color.Red;
                sparesubdesc = subDesc;
                Hide_SpareSUB();
            }
        }

        private void btneditsparesub_Click(object sender, EventArgs e)
        {
            RowSelected_Sparesub();    //get the selected row
        }

        private void dgvsparesub_DoubleClick(object sender, EventArgs e)
        {
            RowSelected_Sparesub();     //get the selected row
        }

        private void btndeletesparesub_Click(object sender, EventArgs e)
        {
            try
            {
                //delete the selected row
                SqlCommand cmd = new SqlCommand("Delete from SPARE_SUB_CATEGORY where V_SPARE_SUB_ID='" + txtsparesubid.Text + "'", dc.con);
                cmd.ExecuteNonQuery();

                lblmsg.Text = "Record Deleted";
                RefereshGrid_Sparesub();   //get the master

                txtsparesubid.ReadOnly = false;
                btnsavesparesub.Text = save;
                ClearData_Sparesub();    //clear all fields

                btndeletesparesub.Enabled = false;
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Spare Parts Sub Category is already in use";
                Console.WriteLine(ex.Message);
            }
        }

        private void btnsavesparesub_Click(object sender, EventArgs e)
        {
            try
            {
                //chekc if quantity is valid
                Regex r = new Regex("^[0-9]*$");
                if (!r.IsMatch(txtsparequantity.Text))
                {
                    lblmsg.Text = "Invalid Quantity value. Example : 20";
                    txtsparequantity.Text = "";
                    return;
                }

                //chekc if spare cost is valid
                r = new Regex("^[0-9]{1,4}([.][0-9]{1,4})?$");
                if (!r.IsMatch(txtsparecost.Text))
                {
                    lblmsg.Text = "Invalid Cost value.  Example : 10.5";
                    txtsparecost.Text = "";
                    return;
                }

                //check if all the fields are inserted
                if (txtsparesubid.Text != "" && txtsparesubdesc.Text != "" && cmbsparemaindesc.Text != "" && cmbsparemaindesc.Text != "--SELECT--")
                {
                    btndeletesparesub.Enabled = false;
                    if (btnsavesparesub.Text == save)
                    {
                        //get id count
                        SqlCommand cmd1 = new SqlCommand("select count(*) from SPARE_SUB_CATEGORY where V_SPARE_SUB_ID='" + txtsparesubid.Text + "'", dc.con);
                        Int32 i = int.Parse(cmd1.ExecuteScalar().ToString());

                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from SPARE_SUB_CATEGORY where V_SPARE_SUB_DESC='" + txtsparesubdesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if id and desc exists
                        if (i == 0 && k == 0)
                        {
                            //get the id
                            SqlCommand cmd = new SqlCommand("select V_SPARE_MAIN_ID from SPARE_MAIN_CATEGORY where V_SPARE_MAIN_DESC='" + cmbsparemaindesc.Text + "'", dc.con);
                            String maincode = "";
                            SqlDataReader sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                maincode = sdr.GetValue(0).ToString();
                            }
                            sdr.Close();

                            //insert
                            cmd = new SqlCommand("insert into SPARE_SUB_CATEGORY values('" + txtsparesubid.Text + "','" + txtsparesubdesc.Text + "','" + maincode + "','" + txtsparequantity.Text + "','" + txtsparecost.Text + "')", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Saved";
                            RefereshGrid_Sparesub();    //get the master

                            txtsparesubid.ReadOnly = false;
                            ClearData_Sparesub();    //clear all fields
                        }
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvsparesub.Rows.Count; j++)
                            {
                                if (dgvsparesub.Rows[j].Cells[0].Value.ToString().Equals(txtsparesubid.Text) || dgvsparesub.Rows[j].Cells[1].Value.ToString().Equals(txtsparesubdesc.Text))
                                {
                                    dgvsparesub.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Spare Parts Sub Category Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                    if (btnsavesparesub.Text == update)
                    {
                        //get desc count
                        SqlCommand cmd2 = new SqlCommand("select count(*) from SPARE_SUB_CATEGORY where V_SPARE_SUB_DESC='" + txtsparesubdesc.Text + "'", dc.con);
                        Int32 k = int.Parse(cmd2.ExecuteScalar().ToString());

                        //check if desc exists or same desc
                        if (k == 0 || sparesubdesc == txtsparesubdesc.Text)
                        {
                            //get the id
                            SqlCommand cmd = new SqlCommand("select V_SPARE_MAIN_ID from SPARE_MAIN_CATEGORY where V_SPARE_MAIN_DESC='" + cmbsparemaindesc.Text + "'", dc.con);
                            String maincode = "";
                            SqlDataReader sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                maincode = sdr.GetValue(0).ToString();
                            }
                            sdr.Close();

                            //update
                            cmd = new SqlCommand("Update SPARE_SUB_CATEGORY set V_SPARE_SUB_DESC='" + txtsparesubdesc.Text + "',V_SPARE_MAIN_ID='" + maincode + "',I_QUANTITY='" + txtsparequantity.Text + "',D_COST='" + txtsparecost.Text + "' where V_SPARE_SUB_ID='" + txtsparesubid.Text + "'", dc.con);
                            cmd.ExecuteNonQuery();

                            lblmsg.Text = "Records Updated";
                            RefereshGrid_Sparesub();    //get the master

                            txtsparesubid.ReadOnly = false;
                            btnsavesparesub.Text = save;
                            ClearData_Sparesub();    //clear all fields
                        } 
                        else
                        {
                            //select row if exists
                            for (int j = 0; j < dgvsparesub.Rows.Count; j++)
                            {
                                if (dgvsparesub.Rows[j].Cells[1].Value.ToString().Equals(txtsparesubdesc.Text))
                                {
                                    dgvsparesub.Rows[j].IsSelected = true;
                                    lblmsg.Text = "Spare Parts SUB Category Already Exists";
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblmsg.Text = "Fill all the Fields";
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
            }
        }

        private void cmbsparemaindesc_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            Hide_SpareSUB();   //hide all the rows other than the selected machine breakdowm main category

            // check if fields changed
            if (txtsparesubid.Text == "" && txtsparesubdesc.Text == "")
            {
                btnsavesparesub.ForeColor = Color.Lime;
            }
            else
            {
                btnsavesparesub.ForeColor = Color.Red;
            }
        }

        private void dgvsparemain_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //hide all the rows other than the selected machine breakdowm main category
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvsparemain.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvsparemain.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvsparemain.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvsparemain.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void dgvsparesub_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            //hide all the rows other than the selected machine breakdowm main category
            if (theme == "CrystalDark" || theme == "FluentDark" || theme == "HighContrastBlack" || theme == "VisualStudio2012Dark")
            {
                e.CellElement.ForeColor = Color.White;
                dgvsparesub.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.White;
                dgvsparesub.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
            else
            {
                e.CellElement.ForeColor = Color.Black;
                dgvsparesub.TableElement.GridViewElement.GroupPanelElement.ForeColor = Color.Black;
                dgvsparesub.TableElement.GridViewElement.GroupPanelElement.GradientStyle = GradientStyles.Solid;
            }
        }

        private void btnreport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt_op = new DataTable();
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

                //get article id
                String article = "";
                SqlCommand cmd = new SqlCommand("select V_ARTICLE_ID from ARTICLE_DB where V_ARTICLE_DESC='" + cmbdesignarticle.Text + "'", dc.con);
                if (cmd.ExecuteScalar() + "" != "")
                {
                    article = cmd.ExecuteScalar() + "";
                }

                //get all the mo details for the article
                SqlDataAdapter sda = new SqlDataAdapter("select distinct V_MO_NO,V_MO_LINE from MO_DETAILS where V_ARTICLE_ID='" + article + "'", dc.con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sda.Dispose();
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    String mo = dt.Rows[j][0].ToString();
                    String moline = dt.Rows[j][1].ToString();

                    int seq1 = 1;
                    int nextseq = 1;
                    int prevseq = 1;
                    int curseq = 1;

                    //get the sequence for the mo
                    sda = new SqlDataAdapter("select ds.I_SEQUENCE_NO,ds.V_OPERATION_CODE,op.V_OPERATION_DESC,op.D_SAM,op.D_PIECERATE,op.D_OVERTIME_RATE from DESIGN_SEQUENCE ds,OPERATION_DB op where ds.V_ARTICLE_ID='" + article + "' and ds.V_OPERATION_CODE=op.V_OPERATION_CODE and ds.I_SEQUENCE_NO IN(select distinct I_SEQUENCE_NO from STATION_ASSIGN where V_MO_NO='" + mo + "' and V_MO_LINE='" + moline + "' and I_STATION_ID!=0) order by ds.I_SEQUENCE_NO", dc.con);
                    DataTable dt1 = new DataTable();
                    sda.Fill(dt1);
                    sda.Dispose();
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        //reorder the sequence
                        prevseq = seq1;
                        seq1 = int.Parse(dt1.Rows[i][0].ToString());
                        if (prevseq == seq1)
                        {
                            nextseq = curseq;
                        }
                        else
                        {
                            nextseq = nextseq + 1;
                        }
                        curseq = nextseq;

                        String opcode = dt1.Rows[i][1].ToString();
                        String opdesc = dt1.Rows[i][2].ToString();
                        int sam = int.Parse(dt1.Rows[i][3].ToString());
                        decimal piecerate = Convert.ToDecimal(dt1.Rows[i][4].ToString());
                        decimal overtime = Convert.ToDecimal(dt1.Rows[i][5].ToString());

                        //get the workduration
                        int workduration = 0;
                        sda = new SqlDataAdapter("select (datediff(SECOND, MIN(time), MAX(TIME))) AS TotalSec,CONVERT(VARCHAR(10), TIME, 111) from HANGER_HISTORY where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and SEQ_NO='" + curseq + "' group by CONVERT(VARCHAR(10), TIME, 111)", dc.con);
                        DataTable dt2 = new DataTable();
                        sda.Fill(dt2);
                        sda.Dispose();
                        for (int k = 0; k < dt2.Rows.Count; k++)
                        {
                            workduration += int.Parse(dt2.Rows[k][0].ToString());
                        }

                        //get the normal piece count
                        int normal_count = 0;
                        cmd = new SqlCommand("select sum(PC_COUNT) from HANGER_HISTORY where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and SEQ_NO='" + curseq + "' and WORKTYPE='0'", dc.con);
                        String temp = cmd.ExecuteScalar() + "";
                        if (temp != "")
                        {
                            normal_count = int.Parse(temp);
                        }

                        //get the overtime piece count
                        int overtime_count = 0;
                        cmd = new SqlCommand("select sum(PC_COUNT) from HANGER_HISTORY where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and SEQ_NO='" + curseq + "' and WORKTYPE='1'", dc.con);
                        temp = cmd.ExecuteScalar() + "";
                        if (temp != "")
                        {
                            overtime_count = int.Parse(temp);
                        }

                        //get employee used for the operation
                        cmd = new SqlCommand("select count(distinct EMP_ID) from HANGER_HISTORY where MO_NO='" + mo + "' and MO_LINE='" + moline + "' and SEQ_NO='" + curseq + "'", dc.con);
                        int emp = int.Parse(cmd.ExecuteScalar() + "");

                        int flag = 0;
                        for (int k = 0; k < dt_op.Rows.Count; k++)
                        {
                            //check if the operation already exists in the grid
                            if (opcode == dt_op.Rows[k][1].ToString())
                            {
                                flag = 1;

                                //add workduration
                                workduration += int.Parse(dt_op.Rows[k][14].ToString());

                                //calculate cost
                                decimal total_cost_normal = (decimal)(normal_count * piecerate);
                                decimal total_cost_overtime = (decimal)(overtime_count * overtime);
                                int total = normal_count + overtime_count;

                                total += int.Parse(dt_op.Rows[k][7].ToString());
                                total += int.Parse(dt_op.Rows[k][8].ToString());
                                total_cost_normal += Convert.ToDecimal(dt_op.Rows[k][11].ToString());
                                total_cost_overtime += Convert.ToDecimal(dt_op.Rows[k][12].ToString());

                                normal_count += int.Parse(dt_op.Rows[k][7].ToString());
                                overtime_count += int.Parse(dt_op.Rows[k][8].ToString());
                                emp += int.Parse(dt_op.Rows[k][10].ToString());

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

                                //update values
                                dt_op.Rows[k][7] = normal_count;
                                dt_op.Rows[k][8] = overtime_count;
                                dt_op.Rows[k][6] = actual_sam.ToString("0.##");
                                dt_op.Rows[k][9] = efficiency.ToString("0.##");
                                dt_op.Rows[k][10] = emp;
                                dt_op.Rows[k][11] = total_cost_normal.ToString("0.##");
                                dt_op.Rows[k][12] = total_cost_overtime.ToString("0.##");
                                dt_op.Rows[k][13] = costperpiece.ToString("0.##");
                                dt_op.Rows[k][14] = workduration / 60;

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
                            dt_op.Rows.Add(seq1, opcode, opdesc, piecerate, overtime, sam, actual_sam.ToString("0.##"), normal_count, overtime_count, efficiency.ToString("0.##") + "%", emp, total_cost_normal.ToString("0.##"), total_cost_overtime.ToString("0.##"), costperpiece.ToString("0.##"), workduration / 60, mo, moline);
                        }
                    }
                }

                //check if report button is clicked
                if (btnreport.Text == "Report View")
                {
                    panel62.Visible = false;
                    DataView view = new DataView(dt_op);

                    //get logo
                    DataTable dt_image = new DataTable();
                    dt_image.Columns.Add("image", typeof(byte[]));
                    dt_image.Rows.Add(dc.GetImage());
                    DataView dv_image = new DataView(dt_image);

                    reportViewer1.LocalReport.ReportEmbeddedResource = "SMARTMRT.Article_Production_Report.rdlc";
                    reportViewer1.LocalReport.DataSources.Clear();

                    //add views to dataset
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", view));
                    reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dv_image));
                    reportViewer1.RefreshReport();
                    btnreport.Text = "Table View";
                }
                else
                {
                    btnreport.Text = "Report View";
                    panel62.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblmsg.Text = ex.Message;
                MessageBox.Show(ex + "");
            }
        }

        private void vpagemasters_SelectedPageChanged(object sender, EventArgs e)
        {
            if (vpagemasters.SelectedPage == pagedesignsequence)
            {
                Refrech_DesignSequence();   //get all operations
                Design_Selected();    //get selected design sequence
            }
        }

        private void btnmoveup_Click(object sender, EventArgs e)
        {

        }

        private void txtsammin_KeyUp(object sender, KeyEventArgs e)
        {
            //check if fields changed
            if (txtoperationid.Text == "" && txtoperationdesc.Text == "" && txtpiecerate.Text == "" && txtsam.Text == "")
            {
                btnsaveoperation.ForeColor = Color.Lime;
            }
            else
            {
                btnsaveoperation.ForeColor = Color.Red;
            }

            try
            {
                double min = Convert.ToDouble(txtsammin.Text);
                min *= 60;
                int sam = (int)min;
                txtsam.Text = sam.ToString();
            }
            catch (Exception ex)
            {
                lblmsg.Text = "Invalid SAM value. Example : 3";
                txtsammin.Text = "";
                Console.WriteLine(ex);
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