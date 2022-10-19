namespace SMARTMRT
{
    partial class Dashboard
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
            Telerik.WinControls.UI.CartesianArea cartesianArea1 = new Telerik.WinControls.UI.CartesianArea();
            Telerik.WinControls.UI.RadListDataItem radListDataItem1 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem2 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem3 = new Telerik.WinControls.UI.RadListDataItem();
            Telerik.WinControls.UI.RadListDataItem radListDataItem4 = new Telerik.WinControls.UI.RadListDataItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            this.fluentDarkTheme1 = new Telerik.WinControls.Themes.FluentDarkTheme();
            this.radChartView4 = new Telerik.WinControls.UI.RadChartView();
            this.radChartView2 = new Telerik.WinControls.UI.RadChartView();
            this.radChartView3 = new Telerik.WinControls.UI.RadChartView();
            this.radChartView5 = new Telerik.WinControls.UI.RadChartView();
            this.radChartView1 = new Telerik.WinControls.UI.RadChartView();
            this.cmbcustomer = new Telerik.WinControls.UI.RadDropDownList();
            this.lblcustomer = new Telerik.WinControls.UI.RadLabel();
            this.radDateTimePicker1 = new Telerik.WinControls.UI.RadDateTimePicker();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbselect = new Telerik.WinControls.UI.RadDropDownList();
            this.lbltotalrepair = new Telerik.WinControls.UI.RadLabel();
            this.lbltotalunloaded = new Telerik.WinControls.UI.RadLabel();
            this.lbltotalloaded = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbcustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblcustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDateTimePicker1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbselect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbltotalrepair)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbltotalunloaded)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbltotalloaded)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // radChartView4
            // 
            this.radChartView4.AreaType = Telerik.WinControls.UI.ChartAreaType.Pie;
            this.radChartView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radChartView4.LegendTitle = "MO";
            this.radChartView4.Location = new System.Drawing.Point(3, 413);
            this.radChartView4.Name = "radChartView4";
            this.radChartView4.SelectionMode = Telerik.WinControls.UI.ChartSelectionMode.SingleDataPoint;
            this.radChartView4.ShowGrid = false;
            this.radChartView4.ShowLegend = true;
            this.radChartView4.ShowPanZoom = true;
            this.radChartView4.ShowTitle = true;
            this.radChartView4.ShowToolTip = true;
            this.radChartView4.ShowTrackBall = true;
            this.radChartView4.Size = new System.Drawing.Size(477, 404);
            this.radChartView4.TabIndex = 3;
            this.radChartView4.ThemeName = "FluentDark";
            this.radChartView4.Title = "MO Loaded";
            ((Telerik.WinControls.UI.RadChartElement)(this.radChartView4.GetChildAt(0))).BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            ((Telerik.WinControls.UI.RadChartElement)(this.radChartView4.GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView4.GetChildAt(0).GetChildAt(0).GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView4.GetChildAt(0).GetChildAt(0).GetChildAt(0))).Text = "MO Loaded";
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView4.GetChildAt(0).GetChildAt(0).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView4.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.Black;
            ((Telerik.WinControls.UI.ChartLegendElement)(this.radChartView4.GetChildAt(0).GetChildAt(0).GetChildAt(2))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.LegendTitleElement)(this.radChartView4.GetChildAt(0).GetChildAt(0).GetChildAt(2).GetChildAt(0))).Text = "MO";
            ((Telerik.WinControls.UI.LegendTitleElement)(this.radChartView4.GetChildAt(0).GetChildAt(0).GetChildAt(2).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            // 
            // radChartView2
            // 
            this.radChartView2.AreaType = Telerik.WinControls.UI.ChartAreaType.Pie;
            this.radChartView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radChartView2.LegendTitle = "MO";
            this.radChartView2.Location = new System.Drawing.Point(970, 3);
            this.radChartView2.Name = "radChartView2";
            this.radChartView2.SelectionMode = Telerik.WinControls.UI.ChartSelectionMode.SingleDataPoint;
            this.radChartView2.ShowGrid = false;
            this.radChartView2.ShowLegend = true;
            this.radChartView2.ShowPanZoom = true;
            this.radChartView2.ShowTitle = true;
            this.radChartView2.ShowToolTip = true;
            this.radChartView2.ShowTrackBall = true;
            this.radChartView2.Size = new System.Drawing.Size(479, 404);
            this.radChartView2.TabIndex = 1;
            this.radChartView2.ThemeName = "FluentDark";
            this.radChartView2.Title = "MO Rework";
            ((Telerik.WinControls.UI.RadChartElement)(this.radChartView2.GetChildAt(0))).BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            ((Telerik.WinControls.UI.RadChartElement)(this.radChartView2.GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView2.GetChildAt(0).GetChildAt(0).GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView2.GetChildAt(0).GetChildAt(0).GetChildAt(0))).Text = "MO Rework";
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView2.GetChildAt(0).GetChildAt(0).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView2.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.Black;
            ((Telerik.WinControls.UI.ChartLegendElement)(this.radChartView2.GetChildAt(0).GetChildAt(0).GetChildAt(2))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.LegendTitleElement)(this.radChartView2.GetChildAt(0).GetChildAt(0).GetChildAt(2).GetChildAt(0))).Text = "MO";
            ((Telerik.WinControls.UI.LegendTitleElement)(this.radChartView2.GetChildAt(0).GetChildAt(0).GetChildAt(2).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            // 
            // radChartView3
            // 
            this.radChartView3.AreaType = Telerik.WinControls.UI.ChartAreaType.Pie;
            this.radChartView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radChartView3.LegendTitle = "MO";
            this.radChartView3.Location = new System.Drawing.Point(486, 413);
            this.radChartView3.Name = "radChartView3";
            this.radChartView3.SelectionMode = Telerik.WinControls.UI.ChartSelectionMode.SingleDataPoint;
            this.radChartView3.ShowGrid = false;
            this.radChartView3.ShowLegend = true;
            this.radChartView3.ShowPanZoom = true;
            this.radChartView3.ShowTitle = true;
            this.radChartView3.ShowToolTip = true;
            this.radChartView3.ShowTrackBall = true;
            this.radChartView3.Size = new System.Drawing.Size(478, 404);
            this.radChartView3.TabIndex = 2;
            this.radChartView3.ThemeName = "FluentDark";
            this.radChartView3.Title = "MO Production";
            ((Telerik.WinControls.UI.RadChartElement)(this.radChartView3.GetChildAt(0))).BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            ((Telerik.WinControls.UI.RadChartElement)(this.radChartView3.GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView3.GetChildAt(0).GetChildAt(0).GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView3.GetChildAt(0).GetChildAt(0).GetChildAt(0))).Text = "MO Production";
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView3.GetChildAt(0).GetChildAt(0).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView3.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.Black;
            ((Telerik.WinControls.UI.ChartLegendElement)(this.radChartView3.GetChildAt(0).GetChildAt(0).GetChildAt(2))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.LegendTitleElement)(this.radChartView3.GetChildAt(0).GetChildAt(0).GetChildAt(2).GetChildAt(0))).Text = "MO";
            ((Telerik.WinControls.UI.LegendTitleElement)(this.radChartView3.GetChildAt(0).GetChildAt(0).GetChildAt(2).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            // 
            // radChartView5
            // 
            this.radChartView5.AreaType = Telerik.WinControls.UI.ChartAreaType.Pie;
            this.radChartView5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radChartView5.LegendTitle = "MO";
            this.radChartView5.Location = new System.Drawing.Point(970, 413);
            this.radChartView5.Name = "radChartView5";
            this.radChartView5.SelectionMode = Telerik.WinControls.UI.ChartSelectionMode.SingleDataPoint;
            this.radChartView5.ShowGrid = false;
            this.radChartView5.ShowLegend = true;
            this.radChartView5.ShowPanZoom = true;
            this.radChartView5.ShowTitle = true;
            this.radChartView5.ShowToolTip = true;
            this.radChartView5.ShowTrackBall = true;
            this.radChartView5.Size = new System.Drawing.Size(479, 404);
            this.radChartView5.TabIndex = 3;
            this.radChartView5.ThemeName = "FluentDark";
            this.radChartView5.Title = "MO Efficiency";
            ((Telerik.WinControls.UI.RadChartElement)(this.radChartView5.GetChildAt(0))).BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            ((Telerik.WinControls.UI.RadChartElement)(this.radChartView5.GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView5.GetChildAt(0).GetChildAt(0).GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView5.GetChildAt(0).GetChildAt(0).GetChildAt(0))).Text = "MO Efficiency";
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView5.GetChildAt(0).GetChildAt(0).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView5.GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.Black;
            ((Telerik.WinControls.UI.ChartLegendElement)(this.radChartView5.GetChildAt(0).GetChildAt(0).GetChildAt(2))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.LegendTitleElement)(this.radChartView5.GetChildAt(0).GetChildAt(0).GetChildAt(2).GetChildAt(0))).Text = "MO";
            ((Telerik.WinControls.UI.LegendTitleElement)(this.radChartView5.GetChildAt(0).GetChildAt(0).GetChildAt(2).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            // 
            // radChartView1
            // 
            cartesianArea1.GridDesign.AlternatingHorizontalColor = false;
            cartesianArea1.GridDesign.AlternatingVerticalColor = false;
            cartesianArea1.GridDesign.DrawHorizontalFills = false;
            cartesianArea1.GridDesign.DrawHorizontalStripes = false;
            cartesianArea1.GridDesign.DrawVerticalFills = false;
            cartesianArea1.ShowGrid = true;
            this.radChartView1.AreaDesign = cartesianArea1;
            this.radChartView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radChartView1.LegendTitle = "MO";
            this.radChartView1.Location = new System.Drawing.Point(3, 3);
            this.radChartView1.Name = "radChartView1";
            this.radChartView1.ShowLegend = true;
            this.radChartView1.ShowPanZoom = true;
            this.radChartView1.ShowTitle = true;
            this.radChartView1.ShowToolTip = true;
            this.radChartView1.ShowTrackBall = true;
            this.radChartView1.Size = new System.Drawing.Size(477, 404);
            this.radChartView1.TabIndex = 1;
            this.radChartView1.ThemeName = "FluentDark";
            this.radChartView1.Title = "Global Hourly Production";
            ((Telerik.WinControls.UI.RadChartElement)(this.radChartView1.GetChildAt(0))).BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            ((Telerik.WinControls.UI.RadChartElement)(this.radChartView1.GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            ((Telerik.WinControls.UI.RadChartElement)(this.radChartView1.GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView1.GetChildAt(0).GetChildAt(0).GetChildAt(0))).TextAlignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView1.GetChildAt(0).GetChildAt(0).GetChildAt(0))).Text = "Global Hourly Production";
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView1.GetChildAt(0).GetChildAt(0).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.ChartTitleElement)(this.radChartView1.GetChildAt(0).GetChildAt(0).GetChildAt(0))).Alignment = System.Drawing.ContentAlignment.TopCenter;
            ((Telerik.WinControls.UI.ChartLegendElement)(this.radChartView1.GetChildAt(0).GetChildAt(0).GetChildAt(2))).Alignment = System.Drawing.ContentAlignment.MiddleCenter;
            ((Telerik.WinControls.UI.LegendTitleElement)(this.radChartView1.GetChildAt(0).GetChildAt(0).GetChildAt(2).GetChildAt(0))).Text = "MO";
            ((Telerik.WinControls.UI.LegendTitleElement)(this.radChartView1.GetChildAt(0).GetChildAt(0).GetChildAt(2).GetChildAt(0))).ForeColor = System.Drawing.Color.White;
            // 
            // cmbcustomer
            // 
            this.cmbcustomer.DropDownSizingMode = Telerik.WinControls.UI.SizingMode.RightBottom;
            this.cmbcustomer.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.cmbcustomer.Location = new System.Drawing.Point(417, 3);
            this.cmbcustomer.Name = "cmbcustomer";
            this.cmbcustomer.Size = new System.Drawing.Size(199, 24);
            this.cmbcustomer.TabIndex = 2;
            this.cmbcustomer.Text = "All";
            this.cmbcustomer.ThemeName = "FluentDark";
            this.cmbcustomer.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbcustomer_SelectedIndexChanged);
            // 
            // lblcustomer
            // 
            this.lblcustomer.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcustomer.Location = new System.Drawing.Point(330, 3);
            this.lblcustomer.Name = "lblcustomer";
            this.lblcustomer.Size = new System.Drawing.Size(81, 24);
            this.lblcustomer.TabIndex = 1;
            this.lblcustomer.Text = "Customer :";
            this.lblcustomer.ThemeName = "FluentDark";
            // 
            // radDateTimePicker1
            // 
            this.radDateTimePicker1.CalendarSize = new System.Drawing.Size(290, 320);
            this.radDateTimePicker1.Location = new System.Drawing.Point(72, 3);
            this.radDateTimePicker1.Name = "radDateTimePicker1";
            this.radDateTimePicker1.Size = new System.Drawing.Size(209, 24);
            this.radDateTimePicker1.TabIndex = 0;
            this.radDateTimePicker1.TabStop = false;
            this.radDateTimePicker1.Text = "23 October 2020";
            this.radDateTimePicker1.ThemeName = "FluentDark";
            this.radDateTimePicker1.Value = new System.DateTime(2020, 10, 23, 9, 50, 7, 245);
            this.radDateTimePicker1.ValueChanged += new System.EventHandler(this.radDateTimePicker1_ValueChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33223F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33555F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33223F));
            this.tableLayoutPanel2.Controls.Add(this.radChartView4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.radChartView3, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.radChartView1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.radChartView5, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.radChartView2, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1452, 820);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbselect);
            this.panel1.Controls.Add(this.lbltotalrepair);
            this.panel1.Controls.Add(this.lbltotalunloaded);
            this.panel1.Controls.Add(this.lbltotalloaded);
            this.panel1.Controls.Add(this.radLabel1);
            this.panel1.Controls.Add(this.cmbcustomer);
            this.panel1.Controls.Add(this.radDateTimePicker1);
            this.panel1.Controls.Add(this.lblcustomer);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1452, 67);
            this.panel1.TabIndex = 2;
            // 
            // cmbselect
            // 
            radListDataItem1.Text = "Dash Board";
            radListDataItem2.Text = "Hourly Production Report";
            radListDataItem3.Text = "Hourly Operation Report";
            radListDataItem4.Text = "Hourly MO Operation Report";
            this.cmbselect.Items.Add(radListDataItem1);
            this.cmbselect.Items.Add(radListDataItem2);
            this.cmbselect.Items.Add(radListDataItem3);
            this.cmbselect.Items.Add(radListDataItem4);
            this.cmbselect.Location = new System.Drawing.Point(668, 3);
            this.cmbselect.Name = "cmbselect";
            this.cmbselect.Size = new System.Drawing.Size(205, 24);
            this.cmbselect.TabIndex = 93;
            this.cmbselect.Text = "Dash Board";
            this.cmbselect.ThemeName = "FluentDark";
            this.cmbselect.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.cmbselect_SelectedIndexChanged);
            // 
            // lbltotalrepair
            // 
            this.lbltotalrepair.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalrepair.Location = new System.Drawing.Point(807, 31);
            this.lbltotalrepair.Name = "lbltotalrepair";
            this.lbltotalrepair.Size = new System.Drawing.Size(209, 30);
            this.lbltotalrepair.TabIndex = 21;
            this.lbltotalrepair.Text = "Total Repair/Rework : 0";
            this.lbltotalrepair.ThemeName = "FluentDark";
            // 
            // lbltotalunloaded
            // 
            this.lbltotalunloaded.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalunloaded.Location = new System.Drawing.Point(513, 31);
            this.lbltotalunloaded.Name = "lbltotalunloaded";
            this.lbltotalunloaded.Size = new System.Drawing.Size(178, 30);
            this.lbltotalunloaded.TabIndex = 20;
            this.lbltotalunloaded.Text = "Total Production : 0";
            this.lbltotalunloaded.ThemeName = "FluentDark";
            // 
            // lbltotalloaded
            // 
            this.lbltotalloaded.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalloaded.Location = new System.Drawing.Point(264, 31);
            this.lbltotalloaded.Name = "lbltotalloaded";
            this.lbltotalloaded.Size = new System.Drawing.Size(147, 30);
            this.lbltotalloaded.TabIndex = 19;
            this.lbltotalloaded.Text = "Total Loaded : 0";
            this.lbltotalloaded.ThemeName = "FluentDark";
            // 
            // radLabel1
            // 
            this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radLabel1.Location = new System.Drawing.Point(3, 3);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(47, 24);
            this.radLabel1.TabIndex = 3;
            this.radLabel1.Text = "Date :";
            this.radLabel1.ThemeName = "FluentDark";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.reportViewer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 67);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1452, 820);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1452, 820);
            this.panel3.TabIndex = 1;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.DocumentMapWidth = 57;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1452, 820);
            this.reportViewer1.TabIndex = 0;
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1452, 887);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dashboard";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.Text = "Dashboard";
            this.ThemeName = "FluentDark";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Initialized += new System.EventHandler(this.Dashboard_Initialized);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Dashboard_FormClosed);
            this.Load += new System.EventHandler(this.Dashboard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radChartView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radChartView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbcustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblcustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radDateTimePicker1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbselect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbltotalrepair)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbltotalunloaded)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbltotalloaded)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.Themes.FluentDarkTheme fluentDarkTheme1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Telerik.WinControls.UI.RadChartView radChartView1;
        private System.Windows.Forms.Timer timer1;
        private Telerik.WinControls.UI.RadChartView radChartView2;
        private Telerik.WinControls.UI.RadChartView radChartView3;
        private Telerik.WinControls.UI.RadChartView radChartView4;
        private Telerik.WinControls.UI.RadChartView radChartView5;
        private Telerik.WinControls.UI.RadDateTimePicker radDateTimePicker1;
        private Telerik.WinControls.UI.RadDropDownList cmbcustomer;
        private Telerik.WinControls.UI.RadLabel lblcustomer;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private System.Windows.Forms.Panel panel2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Panel panel3;
        private Telerik.WinControls.UI.RadLabel lbltotalrepair;
        private Telerik.WinControls.UI.RadLabel lbltotalunloaded;
        private Telerik.WinControls.UI.RadLabel lbltotalloaded;
        private Telerik.WinControls.UI.RadDropDownList cmbselect;
    }
}
