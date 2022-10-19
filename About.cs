using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.Reflection;
using System.Diagnostics;

namespace SMARTMRT
{
    public partial class About : Telerik.WinControls.UI.RadForm
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            radLabel6.UseMnemonic = false;
            radLabel6.Text = "Elite Square, #65 & 66, 3rd Floor";

            
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion;
            radLabel7.Text = "Version : " + version;

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Open SmartMRT website on Browser
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Open Octorite website on browser
            System.Diagnostics.Process.Start(linkLabel2.Text);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Open mail to Octorite
           // System.Diagnostics.Process.Start("mailto:" + linkLabel3.Text + "");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Open mail to SmartMRT
            System.Diagnostics.Process.Start("mailto:" + linkLabel4.Text + "");
        }
    }
}
