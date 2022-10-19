namespace SMARTMRT
{
    partial class Station_Idle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Station_Idle));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlDgv = new System.Windows.Forms.Panel();
            this.dgvempreport = new Telerik.WinControls.UI.RadGridView();
            this.pnlRpt = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRptView = new Telerik.WinControls.UI.RadButton();
            this.cmbStnId = new Telerik.WinControls.UI.RadDropDownList();
            this.cmbLineNo = new Telerik.WinControls.UI.RadDropDownList();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.dtpTo = new Telerik.WinControls.UI.RadDateTimePicker();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.lblempid = new Telerik.WinControls.UI.RadLabel();
            this.dtpFrom = new Telerik.WinControls.UI.RadDateTimePicker();
            this.lbldate = new Telerik.WinControls.UI.RadLabel();
            this.btnSearch = new Telerik.WinControls.UI.RadButton();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlDgv.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvempreport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvempreport.MasterTemplate)).BeginInit();
            this.pnlRpt.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRptView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStnId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLineNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblempid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbldate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1428, 838);
            this.panel2.TabIndex = 93;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlDgv);
            this.panel3.Controls.Add(this.pnlRpt);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 89);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1428, 749);
            this.panel3.TabIndex = 95;
            // 
            // pnlDgv
            // 
            this.pnlDgv.Controls.Add(this.dgvempreport);
            this.pnlDgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDgv.Location = new System.Drawing.Point(0, 0);
            this.pnlDgv.Name = "pnlDgv";
            this.pnlDgv.Size = new System.Drawing.Size(1428, 749);
            this.pnlDgv.TabIndex = 94;
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
            this.dgvempreport.Size = new System.Drawing.Size(1428, 749);
            this.dgvempreport.TabIndex = 91;
            this.dgvempreport.ThemeName = "CrystalDark";
            // 
            // pnlRpt
            // 
            this.pnlRpt.Controls.Add(this.reportViewer1);
            this.pnlRpt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRpt.Location = new System.Drawing.Point(0, 0);
            this.pnlRpt.Name = "pnlRpt";
            this.pnlRpt.Size = new System.Drawing.Size(1428, 749);
            this.pnlRpt.TabIndex = 95;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1428, 749);
            this.reportViewer1.TabIndex = 98;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRptView);
            this.panel1.Controls.Add(this.cmbStnId);
            this.panel1.Controls.Add(this.cmbLineNo);
            this.panel1.Controls.Add(this.radLabel2);
            this.panel1.Controls.Add(this.dtpTo);
            this.panel1.Controls.Add(this.radLabel1);
            this.panel1.Controls.Add(this.lblempid);
            this.panel1.Controls.Add(this.dtpFrom);
            this.panel1.Controls.Add(this.lbldate);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1428, 89);
            this.panel1.TabIndex = 89;
            // 
            // btnRptView
            // 
            this.btnRptView.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnRptView.Image = ((System.Drawing.Image)(resources.GetObject("btnRptView.Image")));
            this.btnRptView.Location = new System.Drawing.Point(15, 59);
            this.btnRptView.Name = "btnRptView";
            this.btnRptView.Size = new System.Drawing.Size(141, 24);
            this.btnRptView.TabIndex = 104;
            this.btnRptView.Text = "Report View";
            this.btnRptView.ThemeName = "FluentDark";
            this.btnRptView.Click += new System.EventHandler(this.btnRptView_Click);
            // 
            // cmbStnId
            // 
            this.cmbStnId.DropDownAnimationEnabled = true;
            this.cmbStnId.Location = new System.Drawing.Point(274, 14);
            this.cmbStnId.Name = "cmbStnId";
            this.cmbStnId.Size = new System.Drawing.Size(79, 24);
            this.cmbStnId.TabIndex = 98;
            this.cmbStnId.ThemeName = "FluentDark";
            // 
            // cmbLineNo
            // 
            this.cmbLineNo.DropDownAnimationEnabled = true;
            this.cmbLineNo.Location = new System.Drawing.Point(92, 14);
            this.cmbLineNo.Name = "cmbLineNo";
            this.cmbLineNo.Size = new System.Drawing.Size(79, 24);
            this.cmbLineNo.TabIndex = 97;
            this.cmbLineNo.Text = "All";
            this.cmbLineNo.ThemeName = "FluentDark";
            this.cmbLineNo.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbLineNo_SelectedIndexChanged);
            // 
            // radLabel2
            // 
            this.radLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel2.Location = new System.Drawing.Point(15, 12);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(76, 25);
            this.radLabel2.TabIndex = 96;
            this.radLabel2.Text = "Line No. :";
            this.radLabel2.ThemeName = "FluentDark";
            // 
            // dtpTo
            // 
            this.dtpTo.CalendarSize = new System.Drawing.Size(290, 320);
            this.dtpTo.Location = new System.Drawing.Point(628, 13);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(161, 24);
            this.dtpTo.TabIndex = 94;
            this.dtpTo.TabStop = false;
            this.dtpTo.Text = "Wednesday, July 1, 2020";
            this.dtpTo.ThemeName = "FluentDark";
            this.dtpTo.Value = new System.DateTime(2020, 7, 1, 17, 31, 13, 314);
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(597, 13);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(34, 25);
            this.radLabel1.TabIndex = 95;
            this.radLabel1.Text = "To :";
            this.radLabel1.ThemeName = "FluentDark";
            // 
            // lblempid
            // 
            this.lblempid.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblempid.Location = new System.Drawing.Point(187, 13);
            this.lblempid.Name = "lblempid";
            this.lblempid.Size = new System.Drawing.Size(88, 25);
            this.lblempid.TabIndex = 1;
            this.lblempid.Text = "Station ID :";
            this.lblempid.ThemeName = "FluentDark";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CalendarSize = new System.Drawing.Size(290, 320);
            this.dtpFrom.Location = new System.Drawing.Point(425, 13);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(158, 24);
            this.dtpFrom.TabIndex = 2;
            this.dtpFrom.TabStop = false;
            this.dtpFrom.Text = "Wednesday, July 1, 2020";
            this.dtpFrom.ThemeName = "FluentDark";
            this.dtpFrom.Value = new System.DateTime(2020, 7, 1, 17, 31, 13, 314);
            // 
            // lbldate
            // 
            this.lbldate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldate.Location = new System.Drawing.Point(370, 13);
            this.lbldate.Name = "lbldate";
            this.lbldate.Size = new System.Drawing.Size(54, 25);
            this.lbldate.TabIndex = 3;
            this.lbldate.Text = "From :";
            this.lbldate.ThemeName = "FluentDark";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnSearch.Location = new System.Drawing.Point(853, 14);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(141, 24);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.ThemeName = "FluentDark";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click_1);
            // 
            // Station_Idle
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(1452, 890);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.Color.White;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Station_Idle";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Station Function Report";
            this.ThemeName = "FluentDark";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Initialized += new System.EventHandler(this.Station_Idle_Initialized);
            this.Load += new System.EventHandler(this.Station_Idle_Load);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.pnlDgv.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvempreport.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvempreport)).EndInit();
            this.pnlRpt.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRptView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbStnId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLineNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblempid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbldate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel pnlDgv;
        private Telerik.WinControls.UI.RadGridView dgvempreport;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadDropDownList cmbStnId;
        private Telerik.WinControls.UI.RadDropDownList cmbLineNo;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadDateTimePicker dtpTo;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadLabel lblempid;
        private Telerik.WinControls.UI.RadDateTimePicker dtpFrom;
        private Telerik.WinControls.UI.RadLabel lbldate;
        private Telerik.WinControls.UI.RadButton btnSearch;
        private System.Windows.Forms.Panel pnlRpt;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private Telerik.WinControls.UI.RadButton btnRptView;
    }
}
