namespace SMARTMRT
{
    partial class Restore_DB
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Restore_DB));
            this.fluentDarkTheme1 = new Telerik.WinControls.Themes.FluentDarkTheme();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radButton4 = new Telerik.WinControls.UI.RadButton();
            this.cmbmonth = new Telerik.WinControls.UI.RadDropDownList();
            this.cmbyear = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.lblshift = new Telerik.WinControls.UI.RadLabel();
            this.dgvbackup = new System.Windows.Forms.DataGridView();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radButton2 = new Telerik.WinControls.UI.RadButton();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButton4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbmonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbyear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblshift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvbackup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radLabel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 384);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(287, 31);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radLabel3);
            this.panel2.Controls.Add(this.radButton2);
            this.panel2.Controls.Add(this.radButton1);
            this.panel2.Controls.Add(this.radButton4);
            this.panel2.Controls.Add(this.cmbmonth);
            this.panel2.Controls.Add(this.cmbyear);
            this.panel2.Controls.Add(this.radLabel1);
            this.panel2.Controls.Add(this.lblshift);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(287, 162);
            this.panel2.TabIndex = 1;
            // 
            // radButton4
            // 
            this.radButton4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.radButton4.Image = ((System.Drawing.Image)(resources.GetObject("radButton4.Image")));
            this.radButton4.Location = new System.Drawing.Point(83, 70);
            this.radButton4.Name = "radButton4";
            this.radButton4.Size = new System.Drawing.Size(147, 24);
            this.radButton4.TabIndex = 120;
            this.radButton4.Text = "Restore";
            this.radButton4.ThemeName = "FluentDark";
            this.radButton4.Click += new System.EventHandler(this.radButton4_Click);
            // 
            // cmbmonth
            // 
            this.cmbmonth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbmonth.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.cmbmonth.Location = new System.Drawing.Point(83, 40);
            this.cmbmonth.Name = "cmbmonth";
            this.cmbmonth.Size = new System.Drawing.Size(147, 24);
            this.cmbmonth.TabIndex = 119;
            this.cmbmonth.ThemeName = "FluentDark";
            this.cmbmonth.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbmonth_SelectedIndexChanged);
            // 
            // cmbyear
            // 
            this.cmbyear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbyear.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.cmbyear.Location = new System.Drawing.Point(83, 10);
            this.cmbyear.Name = "cmbyear";
            this.cmbyear.Size = new System.Drawing.Size(147, 24);
            this.cmbyear.TabIndex = 118;
            this.cmbyear.ThemeName = "FluentDark";
            this.cmbyear.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbyear_SelectedIndexChanged);
            // 
            // radLabel1
            // 
            this.radLabel1.BackColor = System.Drawing.Color.Transparent;
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(12, 40);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(65, 25);
            this.radLabel1.TabIndex = 117;
            this.radLabel1.Text = "Month :";
            this.radLabel1.ThemeName = "FluentDark";
            // 
            // lblshift
            // 
            this.lblshift.BackColor = System.Drawing.Color.Transparent;
            this.lblshift.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblshift.Location = new System.Drawing.Point(12, 9);
            this.lblshift.Name = "lblshift";
            this.lblshift.Size = new System.Drawing.Size(48, 25);
            this.lblshift.TabIndex = 117;
            this.lblshift.Text = "Year :";
            this.lblshift.ThemeName = "FluentDark";
            // 
            // dgvbackup
            // 
            this.dgvbackup.AllowUserToAddRows = false;
            this.dgvbackup.AllowUserToDeleteRows = false;
            this.dgvbackup.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.dgvbackup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvbackup.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvbackup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvbackup.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvbackup.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvbackup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvbackup.Location = new System.Drawing.Point(0, 162);
            this.dgvbackup.MultiSelect = false;
            this.dgvbackup.Name = "dgvbackup";
            this.dgvbackup.ReadOnly = true;
            this.dgvbackup.RowHeadersVisible = false;
            this.dgvbackup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvbackup.Size = new System.Drawing.Size(287, 222);
            this.dgvbackup.TabIndex = 121;
            // 
            // radButton1
            // 
            this.radButton1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.radButton1.Image = ((System.Drawing.Image)(resources.GetObject("radButton1.Image")));
            this.radButton1.Location = new System.Drawing.Point(12, 131);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(127, 24);
            this.radButton1.TabIndex = 121;
            this.radButton1.Text = "Direct";
            this.radButton1.ThemeName = "FluentDark";
            this.radButton1.Click += new System.EventHandler(this.radButton1_Click);
            // 
            // radLabel2
            // 
            this.radLabel2.BackColor = System.Drawing.Color.Transparent;
            this.radLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(3, 4);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(65, 25);
            this.radLabel2.TabIndex = 118;
            this.radLabel2.Text = "Month :";
            this.radLabel2.ThemeName = "FluentDark";
            this.radLabel2.TextChanged += new System.EventHandler(this.radLabel2_TextChanged);
            // 
            // radButton2
            // 
            this.radButton2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.radButton2.Image = ((System.Drawing.Image)(resources.GetObject("radButton2.Image")));
            this.radButton2.Location = new System.Drawing.Point(145, 131);
            this.radButton2.Name = "radButton2";
            this.radButton2.Size = new System.Drawing.Size(130, 24);
            this.radButton2.TabIndex = 122;
            this.radButton2.Text = "Via Browser";
            this.radButton2.ThemeName = "FluentDark";
            this.radButton2.Click += new System.EventHandler(this.radButton2_Click);
            // 
            // radLabel3
            // 
            this.radLabel3.BackColor = System.Drawing.Color.Transparent;
            this.radLabel3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel3.Location = new System.Drawing.Point(12, 100);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(115, 25);
            this.radLabel3.TabIndex = 123;
            this.radLabel3.Text = "Save on Local :";
            this.radLabel3.ThemeName = "FluentDark";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "FILES";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 283;
            // 
            // Restore_DB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(287, 415);
            this.Controls.Add(this.dgvbackup);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Restore_DB";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Restore Database";
            this.ThemeName = "FluentDark";
            this.Initialized += new System.EventHandler(this.Restore_DB_Initialized);
            this.Load += new System.EventHandler(this.Restore_DB_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radButton4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbmonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbyear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblshift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvbackup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.FluentDarkTheme fluentDarkTheme1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvbackup;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel lblshift;
        private Telerik.WinControls.UI.RadDropDownList cmbmonth;
        private Telerik.WinControls.UI.RadDropDownList cmbyear;
        private Telerik.WinControls.UI.RadButton radButton4;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadButton radButton2;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}
