namespace SMARTMRT
{
    partial class Buffer_Sorting
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
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Buffer_Sorting));
            this.fluentDarkTheme1 = new Telerik.WinControls.Themes.FluentDarkTheme();
            this.radPanel2 = new Telerik.WinControls.UI.RadPanel();
            this.radLabel8 = new Telerik.WinControls.UI.RadLabel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvbufferout = new Telerik.WinControls.UI.RadGridView();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnmoveup = new Telerik.WinControls.UI.RadButton();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtquantity = new Telerik.WinControls.UI.RadTextBox();
            this.btnbuffercallout = new Telerik.WinControls.UI.RadButton();
            this.lblqty = new Telerik.WinControls.UI.RadLabel();
            this.btnmovedown = new Telerik.WinControls.UI.RadButton();
            this.visualStudio2012DarkTheme1 = new Telerik.WinControls.Themes.VisualStudio2012DarkTheme();
            this.visualStudio2012LightTheme1 = new Telerik.WinControls.Themes.VisualStudio2012LightTheme();
            this.windows7Theme1 = new Telerik.WinControls.Themes.Windows7Theme();
            this.windows8Theme1 = new Telerik.WinControls.Themes.Windows8Theme();
            this.aquaTheme1 = new Telerik.WinControls.Themes.AquaTheme();
            this.breezeTheme1 = new Telerik.WinControls.Themes.BreezeTheme();
            this.crystalTheme2 = new Telerik.WinControls.Themes.CrystalTheme();
            this.desertTheme1 = new Telerik.WinControls.Themes.DesertTheme();
            this.crystalDarkTheme1 = new Telerik.WinControls.Themes.CrystalDarkTheme();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).BeginInit();
            this.radPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvbufferout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvbufferout.MasterTemplate)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnmoveup)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtquantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnbuffercallout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblqty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnmovedown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radPanel2
            // 
            this.radPanel2.Controls.Add(this.radLabel8);
            this.radPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radPanel2.Location = new System.Drawing.Point(0, 367);
            this.radPanel2.Name = "radPanel2";
            this.radPanel2.Size = new System.Drawing.Size(641, 23);
            this.radPanel2.TabIndex = 98;
            // 
            // radLabel8
            // 
            this.radLabel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radLabel8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel8.Location = new System.Drawing.Point(0, 0);
            this.radLabel8.Name = "radLabel8";
            this.radLabel8.Size = new System.Drawing.Size(641, 23);
            this.radLabel8.TabIndex = 8;
            this.radLabel8.Text = "Color :";
            this.radLabel8.ThemeName = "FluentDark";
            this.radLabel8.TextChanged += new System.EventHandler(this.radLabel8_TextChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvbufferout);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(641, 367);
            this.panel4.TabIndex = 101;
            // 
            // dgvbufferout
            // 
            this.dgvbufferout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvbufferout.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.dgvbufferout.MasterTemplate.AllowAddNewRow = false;
            this.dgvbufferout.MasterTemplate.AllowSearchRow = true;
            this.dgvbufferout.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvbufferout.MasterTemplate.EnableFiltering = true;
            this.dgvbufferout.MasterTemplate.ShowFilteringRow = false;
            this.dgvbufferout.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvbufferout.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvbufferout.Name = "dgvbufferout";
            this.dgvbufferout.ReadOnly = true;
            this.dgvbufferout.ShowHeaderCellButtons = true;
            this.dgvbufferout.Size = new System.Drawing.Size(441, 367);
            this.dgvbufferout.TabIndex = 1;
            this.dgvbufferout.ThemeName = "CrystalDark";
            this.dgvbufferout.ViewCellFormatting += new Telerik.WinControls.UI.CellFormattingEventHandler(this.dgvbufferout_ViewCellFormatting);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btnmoveup);
            this.panel5.Controls.Add(this.panel7);
            this.panel5.Controls.Add(this.btnmovedown);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(441, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(200, 367);
            this.panel5.TabIndex = 0;
            // 
            // btnmoveup
            // 
            this.btnmoveup.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnmoveup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnmoveup.Image = ((System.Drawing.Image)(resources.GetObject("btnmoveup.Image")));
            this.btnmoveup.Location = new System.Drawing.Point(35, 219);
            this.btnmoveup.Name = "btnmoveup";
            this.btnmoveup.Size = new System.Drawing.Size(138, 24);
            this.btnmoveup.TabIndex = 96;
            this.btnmoveup.Text = "▲";
            this.btnmoveup.ThemeName = "FluentDark";
            this.btnmoveup.Click += new System.EventHandler(this.btnmoveup_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.txtquantity);
            this.panel7.Controls.Add(this.btnbuffercallout);
            this.panel7.Controls.Add(this.lblqty);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(200, 213);
            this.panel7.TabIndex = 100;
            // 
            // txtquantity
            // 
            this.txtquantity.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtquantity.Location = new System.Drawing.Point(33, 43);
            this.txtquantity.Name = "txtquantity";
            this.txtquantity.Size = new System.Drawing.Size(138, 24);
            this.txtquantity.TabIndex = 99;
            this.txtquantity.ThemeName = "FluentDark";
            this.txtquantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtquantity_KeyDown);
            // 
            // btnbuffercallout
            // 
            this.btnbuffercallout.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnbuffercallout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnbuffercallout.Image = ((System.Drawing.Image)(resources.GetObject("btnbuffercallout.Image")));
            this.btnbuffercallout.Location = new System.Drawing.Point(33, 73);
            this.btnbuffercallout.Name = "btnbuffercallout";
            this.btnbuffercallout.Size = new System.Drawing.Size(138, 24);
            this.btnbuffercallout.TabIndex = 95;
            this.btnbuffercallout.Text = "Buffer Call Out";
            this.btnbuffercallout.ThemeName = "FluentDark";
            this.btnbuffercallout.Click += new System.EventHandler(this.btnbuffercallout_Click);
            // 
            // lblqty
            // 
            this.lblqty.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblqty.Location = new System.Drawing.Point(46, 12);
            this.lblqty.Name = "lblqty";
            this.lblqty.Size = new System.Drawing.Size(105, 25);
            this.lblqty.TabIndex = 98;
            this.lblqty.Text = "Sort Quantity";
            this.lblqty.ThemeName = "FluentDark";
            // 
            // btnmovedown
            // 
            this.btnmovedown.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnmovedown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnmovedown.Image = ((System.Drawing.Image)(resources.GetObject("btnmovedown.Image")));
            this.btnmovedown.Location = new System.Drawing.Point(35, 249);
            this.btnmovedown.Name = "btnmovedown";
            this.btnmovedown.Size = new System.Drawing.Size(138, 24);
            this.btnmovedown.TabIndex = 97;
            this.btnmovedown.Text = "▼";
            this.btnmovedown.ThemeName = "FluentDark";
            this.btnmovedown.Click += new System.EventHandler(this.btnmovedown_Click);
            // 
            // Buffer_Sorting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 390);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.radPanel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Buffer_Sorting";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Buffer Sorting";
            this.ThemeName = "FluentDark";
            this.Initialized += new System.EventHandler(this.Buffer_Sorting_Initialized);
            this.Load += new System.EventHandler(this.Buffer_Sorting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radPanel2)).EndInit();
            this.radPanel2.ResumeLayout(false);
            this.radPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel8)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvbufferout.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvbufferout)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnmoveup)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtquantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnbuffercallout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblqty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnmovedown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.FluentDarkTheme fluentDarkTheme1;
        private Telerik.WinControls.UI.RadPanel radPanel2;
        private Telerik.WinControls.UI.RadLabel radLabel8;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel7;
        private Telerik.WinControls.UI.RadButton btnmoveup;
        private Telerik.WinControls.UI.RadTextBox txtquantity;
        private Telerik.WinControls.UI.RadButton btnbuffercallout;
        private Telerik.WinControls.UI.RadLabel lblqty;
        private Telerik.WinControls.UI.RadButton btnmovedown;
        private Telerik.WinControls.UI.RadGridView dgvbufferout;
        private Telerik.WinControls.Themes.VisualStudio2012DarkTheme visualStudio2012DarkTheme1;
        private Telerik.WinControls.Themes.VisualStudio2012LightTheme visualStudio2012LightTheme1;
        private Telerik.WinControls.Themes.Windows7Theme windows7Theme1;
        private Telerik.WinControls.Themes.Windows8Theme windows8Theme1;
        private Telerik.WinControls.Themes.AquaTheme aquaTheme1;
        private Telerik.WinControls.Themes.BreezeTheme breezeTheme1;
        private Telerik.WinControls.Themes.CrystalTheme crystalTheme2;
        private Telerik.WinControls.Themes.DesertTheme desertTheme1;
        private Telerik.WinControls.Themes.CrystalDarkTheme crystalDarkTheme1;
    }
}
