namespace SMARTMRT
{
    partial class MessageBox_loading
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageBox_loading));
            this.fluentDarkTheme1 = new Telerik.WinControls.Themes.FluentDarkTheme();
            this.lblmessage1 = new Telerik.WinControls.UI.RadLabel();
            this.btnstoploading = new Telerik.WinControls.UI.RadButton();
            this.btnchangeloading = new Telerik.WinControls.UI.RadButton();
            this.txtloading = new Telerik.WinControls.UI.RadTextBox();
            this.lblloading = new Telerik.WinControls.UI.RadLabel();
            this.btncancel = new Telerik.WinControls.UI.RadButton();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.radLabel15 = new Telerik.WinControls.UI.RadLabel();
            this.lblmessage2 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.lblmessage1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnstoploading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnchangeloading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtloading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblloading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btncancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblmessage2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblmessage1
            // 
            this.lblmessage1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmessage1.Location = new System.Drawing.Point(25, 25);
            this.lblmessage1.Name = "lblmessage1";
            this.lblmessage1.Size = new System.Drawing.Size(65, 21);
            this.lblmessage1.TabIndex = 0;
            this.lblmessage1.Text = "radLabel1";
            this.lblmessage1.ThemeName = "FluentDark";
            // 
            // btnstoploading
            // 
            this.btnstoploading.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnstoploading.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnstoploading.Image = ((System.Drawing.Image)(resources.GetObject("btnstoploading.Image")));
            this.btnstoploading.Location = new System.Drawing.Point(14, 120);
            this.btnstoploading.Name = "btnstoploading";
            this.btnstoploading.Size = new System.Drawing.Size(163, 24);
            this.btnstoploading.TabIndex = 89;
            this.btnstoploading.Text = "Stop Loading";
            this.btnstoploading.ThemeName = "FluentDark";
            this.btnstoploading.Click += new System.EventHandler(this.btnstoploading_Click);
            // 
            // btnchangeloading
            // 
            this.btnchangeloading.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnchangeloading.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnchangeloading.Image = ((System.Drawing.Image)(resources.GetObject("btnchangeloading.Image")));
            this.btnchangeloading.Location = new System.Drawing.Point(200, 120);
            this.btnchangeloading.Name = "btnchangeloading";
            this.btnchangeloading.Size = new System.Drawing.Size(169, 24);
            this.btnchangeloading.TabIndex = 88;
            this.btnchangeloading.Text = "Change Loading Station";
            this.btnchangeloading.ThemeName = "FluentDark";
            this.btnchangeloading.Click += new System.EventHandler(this.btnchangeloading_Click);
            // 
            // txtloading
            // 
            this.txtloading.Location = new System.Drawing.Point(137, 88);
            this.txtloading.Name = "txtloading";
            this.txtloading.Size = new System.Drawing.Size(138, 24);
            this.txtloading.TabIndex = 90;
            this.txtloading.ThemeName = "FluentDark";
            this.txtloading.Visible = false;
            // 
            // lblloading
            // 
            this.lblloading.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblloading.Location = new System.Drawing.Point(25, 91);
            this.lblloading.Name = "lblloading";
            this.lblloading.Size = new System.Drawing.Size(106, 21);
            this.lblloading.TabIndex = 91;
            this.lblloading.Text = "Loading Station :";
            this.lblloading.ThemeName = "FluentDark";
            this.lblloading.Visible = false;
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btncancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btncancel.Image = ((System.Drawing.Image)(resources.GetObject("btncancel.Image")));
            this.btncancel.Location = new System.Drawing.Point(394, 120);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(157, 24);
            this.btncancel.TabIndex = 89;
            this.btncancel.Text = "Cancel";
            this.btncancel.ThemeName = "FluentDark";
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.radLabel15);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel2.Location = new System.Drawing.Point(0, 150);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(567, 23);
            this.radPanel2.TabIndex = 92;
            this.radPanel2.Visible = false;
            // 
            // radLabel15
            // 
            this.radLabel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radLabel15.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel15.Location = new System.Drawing.Point(0, 0);
            this.radLabel15.Name = "radLabel15";
            this.radLabel15.Size = new System.Drawing.Size(567, 23);
            this.radLabel15.TabIndex = 8;
            this.radLabel15.Text = "Color :";
            this.radLabel15.ThemeName = "FluentDark";
            this.radLabel15.TextChanged += new System.EventHandler(this.radLabel15_TextChanged);
            // 
            // lblmessage2
            // 
            this.lblmessage2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmessage2.Location = new System.Drawing.Point(25, 52);
            this.lblmessage2.Name = "lblmessage2";
            this.lblmessage2.Size = new System.Drawing.Size(65, 21);
            this.lblmessage2.TabIndex = 93;
            this.lblmessage2.Text = "radLabel1";
            this.lblmessage2.ThemeName = "FluentDark";
            // 
            // MessageBox_loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 173);
            this.Controls.Add(this.lblmessage2);
            this.Controls.Add(this.radPanel2);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.lblloading);
            this.Controls.Add(this.txtloading);
            this.Controls.Add(this.btnstoploading);
            this.Controls.Add(this.btnchangeloading);
            this.Controls.Add(this.lblmessage1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MessageBox_loading";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "SmartMRT";
            this.ThemeName = "FluentDark";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MessageBox_loading_FormClosed);
            this.Load += new System.EventHandler(this.MessageBox_loading_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lblmessage1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnstoploading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnchangeloading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtloading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblloading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btncancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblmessage2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.Themes.FluentDarkTheme fluentDarkTheme1;
        public Telerik.WinControls.UI.RadLabel lblmessage1;
        private Telerik.WinControls.UI.RadButton btnstoploading;
        private Telerik.WinControls.UI.RadButton btnchangeloading;
        private Telerik.WinControls.UI.RadTextBox txtloading;
        public Telerik.WinControls.UI.RadLabel lblloading;
        private Telerik.WinControls.UI.RadButton btncancel;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadLabel radLabel15;
        public Telerik.WinControls.UI.RadLabel lblmessage2;
    }
}
