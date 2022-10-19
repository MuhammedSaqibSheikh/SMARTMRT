namespace SMARTMRT
{
    partial class Emp_Hanger_Inspect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Emp_Hanger_Inspect));
            Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.cmbshift = new Telerik.WinControls.UI.RadDropDownList();
            this.lblshift = new Telerik.WinControls.UI.RadLabel();
            this.lblempid = new Telerik.WinControls.UI.RadLabel();
            this.txtempid = new Telerik.WinControls.UI.RadTextBox();
            this.dtpFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lbldate = new Telerik.WinControls.UI.RadLabel();
            this.btnsearch = new Telerik.WinControls.UI.RadButton();
            this.radSeparator1 = new Telerik.WinControls.UI.RadSeparator();
            this.radSeparator2 = new Telerik.WinControls.UI.RadSeparator();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnreport = new Telerik.WinControls.UI.RadButton();
            this.lblfname = new Telerik.WinControls.UI.RadLabel();
            this.txtempfirstname = new Telerik.WinControls.UI.RadTextBox();
            this.txtpiececnt = new Telerik.WinControls.UI.RadTextBox();
            this.lblpiecerate = new Telerik.WinControls.UI.RadLabel();
            this.lbllname = new Telerik.WinControls.UI.RadLabel();
            this.txtemplastname = new Telerik.WinControls.UI.RadTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvempreport = new Telerik.WinControls.UI.RadGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbshift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblshift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblempid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtempid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbldate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnsearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnreport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblfname)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtempfirstname)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpiececnt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblpiecerate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbllname)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtemplastname)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvempreport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvempreport.MasterTemplate)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpTo);
            this.panel1.Controls.Add(this.radLabel1);
            this.panel1.Controls.Add(this.cmbshift);
            this.panel1.Controls.Add(this.lblshift);
            this.panel1.Controls.Add(this.lblempid);
            this.panel1.Controls.Add(this.txtempid);
            this.panel1.Controls.Add(this.dtpFrom);
            this.panel1.Controls.Add(this.lbldate);
            this.panel1.Controls.Add(this.btnsearch);
            this.panel1.Controls.Add(this.radSeparator1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1452, 49);
            this.panel1.TabIndex = 88;
            // 
            // dtpTo
            // 
            this.dtpTo.CalendarSize = new System.Drawing.Size(290, 320);
            this.dtpTo.Location = new System.Drawing.Point(555, 13);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(161, 20);
            this.dtpTo.TabIndex = 94;
            this.dtpTo.TabStop = false;
            this.dtpTo.Text = "Wednesday, July 1, 2020";
            this.dtpTo.ThemeName = "FluentDark";
            this.dtpTo.Value = new System.DateTime(2020, 7, 1, 17, 31, 13, 314);
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(524, 13);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(34, 25);
            this.radLabel1.TabIndex = 95;
            this.radLabel1.Text = "To :";
            this.radLabel1.ThemeName = "FluentDark";
            // 
            // cmbshift
            // 
            this.cmbshift.DropDownAnimationEnabled = true;
            this.cmbshift.Location = new System.Drawing.Point(772, 13);
            this.cmbshift.Name = "cmbshift";
            this.cmbshift.Size = new System.Drawing.Size(125, 20);
            this.cmbshift.TabIndex = 82;
            this.cmbshift.Text = "All";
            this.cmbshift.ThemeName = "FluentDark";
            // 
            // lblshift
            // 
            this.lblshift.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblshift.Location = new System.Drawing.Point(726, 14);
            this.lblshift.Name = "lblshift";
            this.lblshift.Size = new System.Drawing.Size(46, 24);
            this.lblshift.TabIndex = 81;
            this.lblshift.Text = "Shift :";
            this.lblshift.ThemeName = "FluentDark";
            // 
            // lblempid
            // 
            this.lblempid.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblempid.Location = new System.Drawing.Point(11, 12);
            this.lblempid.Name = "lblempid";
            this.lblempid.Size = new System.Drawing.Size(107, 25);
            this.lblempid.TabIndex = 1;
            this.lblempid.Text = "Employee ID :";
            this.lblempid.ThemeName = "FluentDark";
            // 
            // txtempid
            // 
            this.txtempid.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtempid.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtempid.Location = new System.Drawing.Point(125, 13);
            this.txtempid.Name = "txtempid";
            this.txtempid.Size = new System.Drawing.Size(149, 20);
            this.txtempid.TabIndex = 0;
            this.txtempid.ThemeName = "FluentDark";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CalendarSize = new System.Drawing.Size(290, 320);
            this.dtpFrom.Location = new System.Drawing.Point(352, 13);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(158, 20);
            this.dtpFrom.TabIndex = 2;
            this.dtpFrom.TabStop = false;
            this.dtpFrom.Text = "Wednesday, July 1, 2020";
            this.dtpFrom.ThemeName = "FluentDark";
            this.dtpFrom.Value = new System.DateTime(2020, 7, 1, 17, 31, 13, 314);
            // 
            // lbldate
            // 
            this.lbldate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldate.Location = new System.Drawing.Point(297, 13);
            this.lbldate.Name = "lbldate";
            this.lbldate.Size = new System.Drawing.Size(54, 25);
            this.lbldate.TabIndex = 3;
            this.lbldate.Text = "From :";
            this.lbldate.ThemeName = "FluentDark";
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnsearch.Location = new System.Drawing.Point(929, 13);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(141, 24);
            this.btnsearch.TabIndex = 6;
            this.btnsearch.Text = "Search";
            this.btnsearch.ThemeName = "FluentDark";
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // radSeparator1
            // 
            this.radSeparator1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radSeparator1.Location = new System.Drawing.Point(0, 39);
            this.radSeparator1.Name = "radSeparator1";
            this.radSeparator1.Size = new System.Drawing.Size(1452, 10);
            this.radSeparator1.TabIndex = 80;
            this.radSeparator1.ThemeName = "FluentDark";
            // 
            // radSeparator2
            // 
            this.radSeparator2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.radSeparator2.Location = new System.Drawing.Point(0, 82);
            this.radSeparator2.Name = "radSeparator2";
            this.radSeparator2.Size = new System.Drawing.Size(1452, 10);
            this.radSeparator2.TabIndex = 86;
            this.radSeparator2.ThemeName = "FluentDark";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnreport);
            this.panel2.Controls.Add(this.lblfname);
            this.panel2.Controls.Add(this.txtempfirstname);
            this.panel2.Controls.Add(this.txtpiececnt);
            this.panel2.Controls.Add(this.lblpiecerate);
            this.panel2.Controls.Add(this.lbllname);
            this.panel2.Controls.Add(this.txtemplastname);
            this.panel2.Controls.Add(this.radSeparator2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1452, 92);
            this.panel2.TabIndex = 89;
            // 
            // btnreport
            // 
            this.btnreport.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnreport.Image = ((System.Drawing.Image)(resources.GetObject("btnreport.Image")));
            this.btnreport.Location = new System.Drawing.Point(12, 52);
            this.btnreport.Name = "btnreport";
            this.btnreport.Size = new System.Drawing.Size(141, 24);
            this.btnreport.TabIndex = 102;
            this.btnreport.Text = "Report View";
            this.btnreport.ThemeName = "FluentDark";
            this.btnreport.Click += new System.EventHandler(this.btnreport_Click);
            // 
            // lblfname
            // 
            this.lblfname.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfname.Location = new System.Drawing.Point(12, 11);
            this.lblfname.Name = "lblfname";
            this.lblfname.Size = new System.Drawing.Size(170, 25);
            this.lblfname.TabIndex = 94;
            this.lblfname.Text = "Employee First Name :";
            this.lblfname.ThemeName = "FluentDark";
            // 
            // txtempfirstname
            // 
            this.txtempfirstname.Location = new System.Drawing.Point(180, 13);
            this.txtempfirstname.Name = "txtempfirstname";
            this.txtempfirstname.Size = new System.Drawing.Size(125, 20);
            this.txtempfirstname.TabIndex = 95;
            this.txtempfirstname.ThemeName = "FluentDark";
            // 
            // txtpiececnt
            // 
            this.txtpiececnt.Location = new System.Drawing.Point(824, 13);
            this.txtpiececnt.Name = "txtpiececnt";
            this.txtpiececnt.Size = new System.Drawing.Size(125, 20);
            this.txtpiececnt.TabIndex = 98;
            this.txtpiececnt.ThemeName = "FluentDark";
            // 
            // lblpiecerate
            // 
            this.lblpiecerate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpiecerate.Location = new System.Drawing.Point(675, 11);
            this.lblpiecerate.Name = "lblpiecerate";
            this.lblpiecerate.Size = new System.Drawing.Size(143, 25);
            this.lblpiecerate.TabIndex = 99;
            this.lblpiecerate.Text = "Total Piece Count :";
            this.lblpiecerate.ThemeName = "FluentDark";
            // 
            // lbllname
            // 
            this.lbllname.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbllname.Location = new System.Drawing.Point(352, 11);
            this.lbllname.Name = "lbllname";
            this.lbllname.Size = new System.Drawing.Size(168, 25);
            this.lbllname.TabIndex = 96;
            this.lbllname.Text = "Employee Last Name :";
            this.lbllname.ThemeName = "FluentDark";
            // 
            // txtemplastname
            // 
            this.txtemplastname.Location = new System.Drawing.Point(522, 12);
            this.txtemplastname.Name = "txtemplastname";
            this.txtemplastname.Size = new System.Drawing.Size(119, 20);
            this.txtemplastname.TabIndex = 97;
            this.txtemplastname.ThemeName = "FluentDark";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.dgvempreport);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 141);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1452, 749);
            this.panel4.TabIndex = 97;
            // 
            // dgvempreport
            // 
            this.dgvempreport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvempreport.Location = new System.Drawing.Point(0, 0);
            // 
            // 
            // 
            this.dgvempreport.MasterTemplate.AllowAddNewRow = false;
            this.dgvempreport.MasterTemplate.AllowColumnReorder = false;
            this.dgvempreport.MasterTemplate.AllowSearchRow = true;
            this.dgvempreport.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.dgvempreport.MasterTemplate.EnableAlternatingRowColor = true;
            this.dgvempreport.MasterTemplate.EnableFiltering = true;
            this.dgvempreport.MasterTemplate.ShowFilteringRow = false;
            this.dgvempreport.MasterTemplate.ShowHeaderCellButtons = true;
            this.dgvempreport.MasterTemplate.ViewDefinition = tableViewDefinition1;
            this.dgvempreport.Name = "dgvempreport";
            this.dgvempreport.ReadOnly = true;
            this.dgvempreport.ShowHeaderCellButtons = true;
            this.dgvempreport.Size = new System.Drawing.Size(1452, 749);
            this.dgvempreport.TabIndex = 91;
            this.dgvempreport.ThemeName = "CrystalDark";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.reportViewer1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 141);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1452, 749);
            this.panel3.TabIndex = 95;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1452, 749);
            this.reportViewer1.TabIndex = 97;
            // 
            // Emp_Hanger_Inspect
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(1452, 890);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Emp_Hanger_Inspect";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Employee Hanger Inspection";
            this.ThemeName = "FluentDark";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Initialized += new System.EventHandler(this.Emp_Hanger_Inspect_Initialized);
            this.Load += new System.EventHandler(this.Emp_Hanger_Inspect_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbshift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblshift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblempid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtempid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbldate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnsearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSeparator2)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnreport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblfname)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtempfirstname)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtpiececnt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblpiecerate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbllname)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtemplastname)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvempreport.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvempreport)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadDropDownList cmbshift;
        private Telerik.WinControls.UI.RadLabel lblshift;
        private Telerik.WinControls.UI.RadLabel lblempid;
        private Telerik.WinControls.UI.RadTextBox txtempid;
        private Telerik.WinControls.UI.RadDateTimePicker dtpFrom;
        private Telerik.WinControls.UI.RadLabel lbldate;
        private Telerik.WinControls.UI.RadButton btnsearch;
        private Telerik.WinControls.UI.RadSeparator radSeparator1;
        private Telerik.WinControls.UI.RadDateTimePicker dtpTo;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadSeparator radSeparator2;
        private System.Windows.Forms.Panel panel2;
        private Telerik.WinControls.UI.RadLabel lblfname;
        private Telerik.WinControls.UI.RadTextBox txtempfirstname;
        private Telerik.WinControls.UI.RadTextBox txtpiececnt;
        private Telerik.WinControls.UI.RadLabel lblpiecerate;
        private Telerik.WinControls.UI.RadLabel lbllname;
        private Telerik.WinControls.UI.RadTextBox txtemplastname;
        private System.Windows.Forms.Panel panel4;
        private Telerik.WinControls.UI.RadGridView dgvempreport;
        private System.Windows.Forms.Panel panel3;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Telerik.WinControls.UI.RadButton btnreport;
    }
}
