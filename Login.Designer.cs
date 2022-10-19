namespace SMARTMRT
{
    partial class Login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.fluentDarkTheme1 = new Telerik.WinControls.Themes.FluentDarkTheme();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.txtusername = new Telerik.WinControls.UI.RadTextBox();
            this.txtpassword = new Telerik.WinControls.UI.RadTextBox();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radPanel2 = new System.Windows.Forms.Panel();
            this.txtuser = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.txtpass = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.tmrdongle = new System.Windows.Forms.Timer(this.components);
            this.btncancel = new Bunifu.Framework.UI.BunifuThinButton2();
            this.btnlogin = new Bunifu.Framework.UI.BunifuThinButton2();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtusername)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpassword)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            this.panel1.SuspendLayout();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel4
            // 
            this.radLabel4.BackColor = System.Drawing.Color.Transparent;
            this.radLabel4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel4.ForeColor = System.Drawing.Color.DodgerBlue;
            this.radLabel4.Location = new System.Drawing.Point(377, 3);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(55, 25);
            this.radLabel4.TabIndex = 8;
            this.radLabel4.Text = "Color :";
            this.radLabel4.ThemeName = "FluentDark";
            this.radLabel4.TextChanged += new System.EventHandler(this.radLabel4_TextChanged);
            // 
            // radLabel1
            // 
            this.radLabel1.BackColor = System.Drawing.Color.Transparent;
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(3, 17);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(89, 25);
            this.radLabel1.TabIndex = 31;
            this.radLabel1.Text = "Username :";
            this.radLabel1.ThemeName = "FluentDark";
            // 
            // radLabel2
            // 
            this.radLabel2.BackColor = System.Drawing.Color.Transparent;
            this.radLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(3, 48);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(85, 25);
            this.radLabel2.TabIndex = 32;
            this.radLabel2.Text = "Password :";
            this.radLabel2.ThemeName = "FluentDark";
            // 
            // txtusername
            // 
            this.txtusername.Location = new System.Drawing.Point(98, 17);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(205, 24);
            this.txtusername.TabIndex = 33;
            this.txtusername.ThemeName = "FluentDark";
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(98, 47);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Size = new System.Drawing.Size(205, 24);
            this.txtpassword.TabIndex = 34;
            this.txtpassword.ThemeName = "FluentDark";
            // 
            // radButton1
            // 
            this.radButton1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.radButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radButton1.Location = new System.Drawing.Point(98, 77);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(94, 24);
            this.radButton1.TabIndex = 35;
            this.radButton1.Text = "Login";
            this.radButton1.ThemeName = "FluentDark";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click_1);
            // 
            // radButton2
            // 
            this.radButton2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.radButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radButton2.Location = new System.Drawing.Point(210, 77);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(93, 24);
            this.radButton2.TabIndex = 36;
            this.radButton2.Text = "Cancel";
            this.radButton2.ThemeName = "FluentDark";
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click_1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.radLabel1);
            this.panel1.Controls.Add(this.radLabel2);
            this.panel1.Controls.Add(this.txtusername);
            this.panel1.Controls.Add(this.txtpassword);
            this.panel1.Controls.Add(this.radButton1);
            this.panel1.Controls.Add(this.radButton2);
            this.panel1.Location = new System.Drawing.Point(14, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(50, 35);
            this.panel1.TabIndex = 41;
            // 
            // radPanel2
            // 
            this.radPanel2.BackColor = System.Drawing.Color.Transparent;
            this.radPanel2.Controls.Add(this.radLabel4);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel2.Location = new System.Drawing.Point(0, 342);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(939, 28);
            this.radPanel2.TabIndex = 50;
            this.radPanel2.Visible = false;
            // 
            // txtuser
            // 
            this.txtuser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.txtuser.BorderColorFocused = System.Drawing.Color.DeepSkyBlue;
            this.txtuser.BorderColorIdle = System.Drawing.Color.DodgerBlue;
            this.txtuser.BorderColorMouseHover = System.Drawing.Color.DeepSkyBlue;
            this.txtuser.BorderThickness = 3;
            this.txtuser.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtuser.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtuser.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtuser.isPassword = false;
            this.txtuser.Location = new System.Drawing.Point(17, 211);
            this.txtuser.Margin = new System.Windows.Forms.Padding(4);
            this.txtuser.Name = "txtuser";
            this.txtuser.Size = new System.Drawing.Size(241, 27);
            this.txtuser.TabIndex = 0;
            this.txtuser.Text = "Username";
            this.txtuser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtuser.Enter += new System.EventHandler(this.txtuser_Enter);
            this.txtuser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtuser_KeyDown);
            // 
            // txtpass
            // 
            this.txtpass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.txtpass.BorderColorFocused = System.Drawing.Color.DeepSkyBlue;
            this.txtpass.BorderColorIdle = System.Drawing.Color.DodgerBlue;
            this.txtpass.BorderColorMouseHover = System.Drawing.Color.DeepSkyBlue;
            this.txtpass.BorderThickness = 3;
            this.txtpass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtpass.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.txtpass.ForeColor = System.Drawing.Color.DodgerBlue;
            this.txtpass.isPassword = false;
            this.txtpass.Location = new System.Drawing.Point(17, 246);
            this.txtpass.Margin = new System.Windows.Forms.Padding(4);
            this.txtpass.Name = "txtpass";
            this.txtpass.Size = new System.Drawing.Size(241, 27);
            this.txtpass.TabIndex = 1;
            this.txtpass.Text = "Password";
            this.txtpass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtpass.Enter += new System.EventHandler(this.txtpass_Enter);
            this.txtpass.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpass_KeyDown_1);
            // 
            // tmrdongle
            // 
            this.tmrdongle.Interval = 600000;
            this.tmrdongle.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btncancel
            // 
            this.btncancel.ActiveBorderThickness = 1;
            this.btncancel.ActiveCornerRadius = 20;
            this.btncancel.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.btncancel.ActiveForecolor = System.Drawing.Color.DeepSkyBlue;
            this.btncancel.ActiveLineColor = System.Drawing.Color.DeepSkyBlue;
            this.btncancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.btncancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btncancel.BackgroundImage")));
            this.btncancel.ButtonText = "Cancel";
            this.btncancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncancel.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.ForeColor = System.Drawing.Color.SeaGreen;
            this.btncancel.IdleBorderThickness = 1;
            this.btncancel.IdleCornerRadius = 20;
            this.btncancel.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.btncancel.IdleForecolor = System.Drawing.Color.DodgerBlue;
            this.btncancel.IdleLineColor = System.Drawing.Color.DodgerBlue;
            this.btncancel.Location = new System.Drawing.Point(17, 307);
            this.btncancel.Margin = new System.Windows.Forms.Padding(5);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(241, 34);
            this.btncancel.TabIndex = 3;
            this.btncancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click_1);
            // 
            // btnlogin
            // 
            this.btnlogin.ActiveBorderThickness = 1;
            this.btnlogin.ActiveCornerRadius = 20;
            this.btnlogin.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.btnlogin.ActiveForecolor = System.Drawing.Color.DeepSkyBlue;
            this.btnlogin.ActiveLineColor = System.Drawing.Color.DeepSkyBlue;
            this.btnlogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.btnlogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnlogin.BackgroundImage")));
            this.btnlogin.ButtonText = "Login";
            this.btnlogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnlogin.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogin.ForeColor = System.Drawing.Color.SeaGreen;
            this.btnlogin.IdleBorderThickness = 1;
            this.btnlogin.IdleCornerRadius = 20;
            this.btnlogin.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.btnlogin.IdleForecolor = System.Drawing.Color.DodgerBlue;
            this.btnlogin.IdleLineColor = System.Drawing.Color.DodgerBlue;
            this.btnlogin.Location = new System.Drawing.Point(17, 275);
            this.btnlogin.Margin = new System.Windows.Forms.Padding(5);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(241, 34);
            this.btnlogin.TabIndex = 2;
            this.btnlogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click_1);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::SMARTMRT.Properties.Resources.SMART_PWRDBY112;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(939, 370);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnlogin);
            this.Controls.Add(this.txtpass);
            this.Controls.Add(this.txtuser);
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "SmartMRT";
            this.ThemeName = "FluentDark";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtusername)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpassword)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.FluentDarkTheme fluentDarkTheme1;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private Telerik.WinControls.UI.RadButton radButton2;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadTextBox txtpassword;
        private Telerik.WinControls.UI.RadTextBox txtusername;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel radPanel2;
        private Bunifu.Framework.UI.BunifuMetroTextbox txtuser;
        private Bunifu.Framework.UI.BunifuMetroTextbox txtpass;
        private Bunifu.Framework.UI.BunifuThinButton2 btnlogin;
        private Bunifu.Framework.UI.BunifuThinButton2 btncancel;
        private System.Windows.Forms.Timer tmrdongle;
    }
}
