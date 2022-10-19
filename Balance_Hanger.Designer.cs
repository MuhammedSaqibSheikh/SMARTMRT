namespace SMARTMRT
{
    partial class Balance_Hanger
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
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem5 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem6 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem7 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.CartesianArea cartesianArea1 = new Telerik.WinControls.UI.CartesianArea();
            this.fluentDarkTheme1 = new Telerik.WinControls.Themes.FluentDarkTheme();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.cmbautorefresh = new Telerik.WinControls.UI.RadDropDownList();
            this.lblprodline = new Telerik.WinControls.UI.RadLabel();
            this.cmbline = new Telerik.WinControls.UI.RadDropDownList();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radChartView3 = new Telerik.WinControls.UI.RadChartView();
            this.lbltotalwip = new Telerik.WinControls.UI.RadLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbautorefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblprodline)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbline)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView3)).BeginInit();
            this.radChartView3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbltotalwip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radLabel5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 858);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1452, 29);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            // 
            // radLabel5
            // 
            this.radLabel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.radLabel5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel5.Location = new System.Drawing.Point(0, 0);
            this.radLabel5.Name = "radLabel5";
            this.radLabel5.Size = new System.Drawing.Size(46, 29);
            this.radLabel5.TabIndex = 80;
            this.radLabel5.Text = "Line :";
            this.radLabel5.ThemeName = "FluentDark";
            this.radLabel5.TextChanged += new System.EventHandler(this.radLabel5_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radLabel1);
            this.panel2.Controls.Add(this.cmbautorefresh);
            this.panel2.Controls.Add(this.lblprodline);
            this.panel2.Controls.Add(this.cmbline);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1452, 41);
            this.panel2.TabIndex = 1;
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(271, 10);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(110, 25);
            this.radLabel1.TabIndex = 78;
            this.radLabel1.Text = "Auto Refresh :";
            this.radLabel1.ThemeName = "FluentDark";
            // 
            // cmbautorefresh
            // 
            this.cmbautorefresh.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            radListDataItem1.Text = "5";
            radListDataItem2.Text = "10";
            radListDataItem3.Text = "15";
            radListDataItem4.Text = "20";
            radListDataItem5.Text = "25";
            radListDataItem6.Text = "30";
            radListDataItem7.Text = "60";
            this.cmbautorefresh.Items.Add(radListDataItem1);
            this.cmbautorefresh.Items.Add(radListDataItem2);
            this.cmbautorefresh.Items.Add(radListDataItem3);
            this.cmbautorefresh.Items.Add(radListDataItem4);
            this.cmbautorefresh.Items.Add(radListDataItem5);
            this.cmbautorefresh.Items.Add(radListDataItem6);
            this.cmbautorefresh.Items.Add(radListDataItem7);
            this.cmbautorefresh.Location = new System.Drawing.Point(387, 10);
            this.cmbautorefresh.Name = "cmbautorefresh";
            this.cmbautorefresh.Size = new System.Drawing.Size(125, 24);
            this.cmbautorefresh.TabIndex = 77;
            this.cmbautorefresh.Text = "60";
            this.cmbautorefresh.ThemeName = "FluentDark";
            this.cmbautorefresh.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbautorefresh_SelectedIndexChanged);
            // 
            // lblprodline
            // 
            this.lblprodline.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprodline.Location = new System.Drawing.Point(12, 12);
            this.lblprodline.Name = "lblprodline";
            this.lblprodline.Size = new System.Drawing.Size(46, 25);
            this.lblprodline.TabIndex = 76;
            this.lblprodline.Text = "Line :";
            this.lblprodline.ThemeName = "FluentDark";
            // 
            // cmbline
            // 
            this.cmbline.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.cmbline.Location = new System.Drawing.Point(64, 11);
            this.cmbline.Name = "cmbline";
            this.cmbline.Size = new System.Drawing.Size(125, 24);
            this.cmbline.TabIndex = 0;
            this.cmbline.ThemeName = "FluentDark";
            this.cmbline.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbline_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radChartView3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 41);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1452, 817);
            this.panel3.TabIndex = 2;
            // 
            // radChartView3
            // 
            this.radChartView3.AreaDesign = cartesianArea1;
            this.radChartView3.Controls.Add(this.lbltotalwip);
            this.radChartView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radChartView3.LegendTitle = "MO";
            this.radChartView3.Location = new System.Drawing.Point(0, 0);
            this.radChartView3.Name = "radChartView3";
            this.radChartView3.SelectionMode = Telerik.WinControls.UI.ChartSelectionMode.SingleDataPoint;
            this.radChartView3.ShowGrid = false;
            this.radChartView3.ShowPanZoom = true;
            this.radChartView3.ShowTitle = true;
            this.radChartView3.ShowToolTip = true;
            this.radChartView3.Size = new System.Drawing.Size(1452, 817);
            this.radChartView3.TabIndex = 3;
            this.radChartView3.ThemeName = "FluentDark";
            this.radChartView3.Title = "Station WIP";
            ((Telerik.WinControls.UI.RadChartElement)(this.radChartView3.GetChildAt(0))).BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            ((Telerik.WinControls.UI.RadChartElement)(this.radChartView3.GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView3.GetChildAt(0).GetChildAt(0).GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView3.GetChildAt(0).GetChildAt(0).GetChildAt(0))).Text = "Station WIP";
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView3.GetChildAt(0).GetChildAt(0).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView3.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.Black;
            // 
            // lbltotalwip
            // 
            this.lbltotalwip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalwip.Location = new System.Drawing.Point(12, 19);
            this.lbltotalwip.Name = "lbltotalwip";
            this.lbltotalwip.Size = new System.Drawing.Size(86, 25);
            this.lbltotalwip.TabIndex = 79;
            this.lbltotalwip.Text = "Total WIP :";
            this.lbltotalwip.ThemeName = "FluentDark";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Balance_Hanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1452, 887);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Balance_Hanger";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Station WIP";
            this.ThemeName = "FluentDark";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Initialized += new System.EventHandler(this.Balance_Hanger_Initialized);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Balance_Hanger_FormClosed);
            this.Load += new System.EventHandler(this.Balance_Hanger_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel5)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbautorefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblprodline)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbline)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radChartView3)).EndInit();
            this.radChartView3.ResumeLayout(false);
            this.radChartView3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbltotalwip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.FluentDarkTheme fluentDarkTheme1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private Telerik.WinControls.UI.RadDropDownList cmbline;
        private Telerik.WinControls.UI.RadLabel lblprodline;
        private Telerik.WinControls.UI.RadChartView radChartView3;
        private System.Windows.Forms.Timer timer1;
        private Telerik.WinControls.UI.RadLabel radLabel5;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDropDownList cmbautorefresh;
        private Telerik.WinControls.UI.RadLabel lbltotalwip;
    }
}
