namespace Senior_Project_Stock_Tracker
{
    partial class StockTrackMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockTrackMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fakeStockTrainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadUpdatedCompaniesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockMarketStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.companyNewsBtn = new System.Windows.Forms.Button();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sectorsComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.companyListingslistBox = new System.Windows.Forms.ListBox();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.label4 = new System.Windows.Forms.Label();
            this.timeSeriesComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.timeSeriesIntervalComboBox = new System.Windows.Forms.ComboBox();
            this.updateChartBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.desiredDataComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonLocateCompany = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SummaryQuoteLbl = new System.Windows.Forms.LinkLabel();
            this.IndustryLbl = new System.Windows.Forms.Label();
            this.IPOyearLbl = new System.Windows.Forms.Label();
            this.symbolLbl = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.addTrackCompanyButton = new System.Windows.Forms.Button();
            this.removeTrackCompanyButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.trackedCompaniesComboBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModeToolStripMenuItem,
            this.stockMarketStatusToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1853, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ModeToolStripMenuItem
            // 
            this.ModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fakeStockTrainerToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.ModeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ModeToolStripMenuItem.Name = "ModeToolStripMenuItem";
            this.ModeToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.ModeToolStripMenuItem.Text = "Mode";
            // 
            // fakeStockTrainerToolStripMenuItem
            // 
            this.fakeStockTrainerToolStripMenuItem.Name = "fakeStockTrainerToolStripMenuItem";
            this.fakeStockTrainerToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.fakeStockTrainerToolStripMenuItem.Text = "Fake Stock Trader";
            this.fakeStockTrainerToolStripMenuItem.Click += new System.EventHandler(this.fakeStockTrainerToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadUpdatedCompaniesToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(193, 24);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // downloadUpdatedCompaniesToolStripMenuItem
            // 
            this.downloadUpdatedCompaniesToolStripMenuItem.Name = "downloadUpdatedCompaniesToolStripMenuItem";
            this.downloadUpdatedCompaniesToolStripMenuItem.Size = new System.Drawing.Size(287, 24);
            this.downloadUpdatedCompaniesToolStripMenuItem.Text = "Download Updated Companies";
            this.downloadUpdatedCompaniesToolStripMenuItem.Click += new System.EventHandler(this.downloadUpdatedCompaniesToolStripMenuItem_Click);
            // 
            // stockMarketStatusToolStripMenuItem
            // 
            this.stockMarketStatusToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stockMarketStatusToolStripMenuItem.Name = "stockMarketStatusToolStripMenuItem";
            this.stockMarketStatusToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.stockMarketStatusToolStripMenuItem.Text = "Stock Market Status";
            // 
            // companyNewsBtn
            // 
            this.companyNewsBtn.BackColor = System.Drawing.Color.White;
            this.companyNewsBtn.Location = new System.Drawing.Point(192, 328);
            this.companyNewsBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.companyNewsBtn.Name = "companyNewsBtn";
            this.companyNewsBtn.Size = new System.Drawing.Size(171, 67);
            this.companyNewsBtn.TabIndex = 6;
            this.companyNewsBtn.Text = "Company News";
            this.companyNewsBtn.UseVisualStyleBackColor = false;
            this.companyNewsBtn.Click += new System.EventHandler(this.companyNewsBtn_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.searchTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.searchTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.searchTextBox.Location = new System.Drawing.Point(10, 296);
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(353, 24);
            this.searchTextBox.TabIndex = 4;
            this.toolTip1.SetToolTip(this.searchTextBox, "Text is case sensitive!\r\nSelect text from drop down or type it identically");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(170, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Search Stock Company:";
            // 
            // sectorsComboBox
            // 
            this.sectorsComboBox.BackColor = System.Drawing.Color.White;
            this.sectorsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sectorsComboBox.FormattingEnabled = true;
            this.sectorsComboBox.Location = new System.Drawing.Point(7, 50);
            this.sectorsComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sectorsComboBox.MaxDropDownItems = 50;
            this.sectorsComboBox.Name = "sectorsComboBox";
            this.sectorsComboBox.Size = new System.Drawing.Size(222, 26);
            this.sectorsComboBox.Sorted = true;
            this.sectorsComboBox.TabIndex = 1;
            this.sectorsComboBox.SelectedIndexChanged += new System.EventHandler(this.sectorsComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 18);
            this.label3.TabIndex = 7;
            this.label3.Text = "Company Sector:";
            // 
            // companyListingslistBox
            // 
            this.companyListingslistBox.FormattingEnabled = true;
            this.companyListingslistBox.ItemHeight = 18;
            this.companyListingslistBox.Location = new System.Drawing.Point(251, 29);
            this.companyListingslistBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.companyListingslistBox.Name = "companyListingslistBox";
            this.companyListingslistBox.Size = new System.Drawing.Size(300, 238);
            this.companyListingslistBox.Sorted = true;
            this.companyListingslistBox.TabIndex = 8;
            this.companyListingslistBox.SelectedIndexChanged += new System.EventHandler(this.companyListingslistBox_SelectedIndexChanged);
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(605, 31);
            this.cartesianChart1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(1234, 671);
            this.cartesianChart1.TabIndex = 9;
            this.cartesianChart1.Text = "cartesianChart1";
            this.cartesianChart1.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "Time Series:";
            // 
            // timeSeriesComboBox
            // 
            this.timeSeriesComboBox.BackColor = System.Drawing.Color.White;
            this.timeSeriesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeSeriesComboBox.FormattingEnabled = true;
            this.timeSeriesComboBox.Items.AddRange(new object[] {
            "Intraday",
            "Daily",
            "Daily Adjusted",
            "Weekly",
            "Weekly Adjusted",
            "Monthly",
            "Monthly Adjusted"});
            this.timeSeriesComboBox.Location = new System.Drawing.Point(7, 112);
            this.timeSeriesComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.timeSeriesComboBox.Name = "timeSeriesComboBox";
            this.timeSeriesComboBox.Size = new System.Drawing.Size(222, 26);
            this.timeSeriesComboBox.TabIndex = 11;
            this.toolTip1.SetToolTip(this.timeSeriesComboBox, resources.GetString("timeSeriesComboBox.ToolTip"));
            this.timeSeriesComboBox.SelectedIndexChanged += new System.EventHandler(this.timeSeriesComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 18);
            this.label5.TabIndex = 12;
            this.label5.Text = "Time Series Interval:";
            // 
            // timeSeriesIntervalComboBox
            // 
            this.timeSeriesIntervalComboBox.BackColor = System.Drawing.Color.White;
            this.timeSeriesIntervalComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeSeriesIntervalComboBox.FormattingEnabled = true;
            this.timeSeriesIntervalComboBox.Items.AddRange(new object[] {
            "1 Minute",
            "5 Minutes",
            "15 Minutes",
            "30 Minutes",
            "60 Minutes"});
            this.timeSeriesIntervalComboBox.Location = new System.Drawing.Point(7, 176);
            this.timeSeriesIntervalComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.timeSeriesIntervalComboBox.Name = "timeSeriesIntervalComboBox";
            this.timeSeriesIntervalComboBox.Size = new System.Drawing.Size(222, 26);
            this.timeSeriesIntervalComboBox.TabIndex = 13;
            this.timeSeriesIntervalComboBox.SelectedIndexChanged += new System.EventHandler(this.timeSeriesIntervalComboBox_SelectedIndexChanged);
            // 
            // updateChartBtn
            // 
            this.updateChartBtn.BackColor = System.Drawing.Color.White;
            this.updateChartBtn.Enabled = false;
            this.updateChartBtn.Location = new System.Drawing.Point(7, 328);
            this.updateChartBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.updateChartBtn.Name = "updateChartBtn";
            this.updateChartBtn.Size = new System.Drawing.Size(182, 67);
            this.updateChartBtn.TabIndex = 14;
            this.updateChartBtn.Text = "Display Chart";
            this.updateChartBtn.UseVisualStyleBackColor = false;
            this.updateChartBtn.Click += new System.EventHandler(this.updateChartBtn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 217);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(179, 18);
            this.label6.TabIndex = 15;
            this.label6.Text = "Price Measurement Point:";
            // 
            // desiredDataComboBox
            // 
            this.desiredDataComboBox.BackColor = System.Drawing.Color.White;
            this.desiredDataComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.desiredDataComboBox.FormattingEnabled = true;
            this.desiredDataComboBox.Items.AddRange(new object[] {
            "Open",
            "Close",
            "High",
            "Low",
            "Volume"});
            this.desiredDataComboBox.Location = new System.Drawing.Point(7, 238);
            this.desiredDataComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.desiredDataComboBox.Name = "desiredDataComboBox";
            this.desiredDataComboBox.Size = new System.Drawing.Size(222, 26);
            this.desiredDataComboBox.TabIndex = 16;
            this.toolTip1.SetToolTip(this.desiredDataComboBox, resources.GetString("desiredDataComboBox.ToolTip"));
            this.desiredDataComboBox.SelectedIndexChanged += new System.EventHandler(this.desiredDataComboBox_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonLocateCompany);
            this.groupBox1.Controls.Add(this.desiredDataComboBox);
            this.groupBox1.Controls.Add(this.updateChartBtn);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.searchTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.timeSeriesIntervalComboBox);
            this.groupBox1.Controls.Add(this.companyListingslistBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.timeSeriesComboBox);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.companyNewsBtn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.sectorsComboBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(570, 403);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parameters";
            // 
            // buttonLocateCompany
            // 
            this.buttonLocateCompany.BackColor = System.Drawing.Color.White;
            this.buttonLocateCompany.Location = new System.Drawing.Point(369, 292);
            this.buttonLocateCompany.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonLocateCompany.Name = "buttonLocateCompany";
            this.buttonLocateCompany.Size = new System.Drawing.Size(176, 33);
            this.buttonLocateCompany.TabIndex = 17;
            this.buttonLocateCompany.Text = "Locate Company";
            this.buttonLocateCompany.UseVisualStyleBackColor = false;
            this.buttonLocateCompany.Click += new System.EventHandler(this.buttonLocateCompany_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.SummaryQuoteLbl);
            this.groupBox2.Controls.Add(this.IndustryLbl);
            this.groupBox2.Controls.Add(this.IPOyearLbl);
            this.groupBox2.Controls.Add(this.symbolLbl);
            this.groupBox2.Location = new System.Drawing.Point(12, 447);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(570, 103);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Additional Stock Info";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 76);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 18);
            this.label10.TabIndex = 7;
            this.label10.Text = "Summary Quote:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 18);
            this.label9.TabIndex = 6;
            this.label9.Text = "Industry:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 18);
            this.label8.TabIndex = 5;
            this.label8.Text = "IPO Year:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 18);
            this.label7.TabIndex = 4;
            this.label7.Text = "Symbol:";
            // 
            // SummaryQuoteLbl
            // 
            this.SummaryQuoteLbl.AutoSize = true;
            this.SummaryQuoteLbl.Location = new System.Drawing.Point(134, 74);
            this.SummaryQuoteLbl.Name = "SummaryQuoteLbl";
            this.SummaryQuoteLbl.Size = new System.Drawing.Size(132, 18);
            this.SummaryQuoteLbl.TabIndex = 3;
            this.SummaryQuoteLbl.TabStop = true;
            this.SummaryQuoteLbl.Text = "SummaryQuoteLbl";
            this.SummaryQuoteLbl.Visible = false;
            this.SummaryQuoteLbl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SummaryQuoteLbl_LinkClicked);
            // 
            // IndustryLbl
            // 
            this.IndustryLbl.AutoSize = true;
            this.IndustryLbl.Location = new System.Drawing.Point(72, 56);
            this.IndustryLbl.Name = "IndustryLbl";
            this.IndustryLbl.Size = new System.Drawing.Size(78, 18);
            this.IndustryLbl.TabIndex = 2;
            this.IndustryLbl.Text = "industryLbl";
            this.IndustryLbl.Visible = false;
            // 
            // IPOyearLbl
            // 
            this.IPOyearLbl.AutoSize = true;
            this.IPOyearLbl.Location = new System.Drawing.Point(80, 38);
            this.IPOyearLbl.Name = "IPOyearLbl";
            this.IPOyearLbl.Size = new System.Drawing.Size(80, 18);
            this.IPOyearLbl.TabIndex = 1;
            this.IPOyearLbl.Text = "IPOyearLbl";
            this.IPOyearLbl.Visible = false;
            // 
            // symbolLbl
            // 
            this.symbolLbl.AutoSize = true;
            this.symbolLbl.Location = new System.Drawing.Point(78, 20);
            this.symbolLbl.Name = "symbolLbl";
            this.symbolLbl.Size = new System.Drawing.Size(75, 18);
            this.symbolLbl.TabIndex = 0;
            this.symbolLbl.Text = "symbolLbl";
            this.symbolLbl.Visible = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(20, 53);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(103, 22);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Company 2";
            this.toolTip1.SetToolTip(this.checkBox2, "Check the box to lock the company name you wish to compare");
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(20, 23);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(103, 22);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Company 1";
            this.toolTip1.SetToolTip(this.checkBox1, "Check the box to lock the company name you wish to compare");
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // addTrackCompanyButton
            // 
            this.addTrackCompanyButton.BackColor = System.Drawing.Color.White;
            this.addTrackCompanyButton.Location = new System.Drawing.Point(7, 62);
            this.addTrackCompanyButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.addTrackCompanyButton.Name = "addTrackCompanyButton";
            this.addTrackCompanyButton.Size = new System.Drawing.Size(124, 55);
            this.addTrackCompanyButton.TabIndex = 18;
            this.addTrackCompanyButton.Text = "Add Company";
            this.toolTip1.SetToolTip(this.addTrackCompanyButton, "Add the company to track that is selected in the company listing box");
            this.addTrackCompanyButton.UseVisualStyleBackColor = false;
            this.addTrackCompanyButton.Click += new System.EventHandler(this.addTrackCompanyButton_Click);
            // 
            // removeTrackCompanyButton
            // 
            this.removeTrackCompanyButton.BackColor = System.Drawing.Color.White;
            this.removeTrackCompanyButton.Location = new System.Drawing.Point(142, 62);
            this.removeTrackCompanyButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.removeTrackCompanyButton.Name = "removeTrackCompanyButton";
            this.removeTrackCompanyButton.Size = new System.Drawing.Size(124, 55);
            this.removeTrackCompanyButton.TabIndex = 20;
            this.removeTrackCompanyButton.Text = "Remove Company";
            this.toolTip1.SetToolTip(this.removeTrackCompanyButton, "Select the company in the box above to remove the selected company from tracking");
            this.removeTrackCompanyButton.UseVisualStyleBackColor = false;
            this.removeTrackCompanyButton.Click += new System.EventHandler(this.removeTrackCompanyButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Location = new System.Drawing.Point(12, 558);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(290, 83);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Compare Stocks";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.removeTrackCompanyButton);
            this.groupBox4.Controls.Add(this.addTrackCompanyButton);
            this.groupBox4.Controls.Add(this.trackedCompaniesComboBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 648);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(289, 131);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tracked Stocks";
            // 
            // trackedCompaniesComboBox
            // 
            this.trackedCompaniesComboBox.BackColor = System.Drawing.Color.White;
            this.trackedCompaniesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trackedCompaniesComboBox.FormattingEnabled = true;
            this.trackedCompaniesComboBox.Location = new System.Drawing.Point(10, 28);
            this.trackedCompaniesComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.trackedCompaniesComboBox.MaxDropDownItems = 50;
            this.trackedCompaniesComboBox.Name = "trackedCompaniesComboBox";
            this.trackedCompaniesComboBox.Size = new System.Drawing.Size(256, 26);
            this.trackedCompaniesComboBox.Sorted = true;
            this.trackedCompaniesComboBox.TabIndex = 19;
            this.trackedCompaniesComboBox.SelectedIndexChanged += new System.EventHandler(this.trackedCompaniesComboBox_SelectedIndexChanged);
            // 
            // StockTrackMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1853, 791);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cartesianChart1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StockTrackMain";
            this.Text = "Stock Market Tracker";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fakeStockTrainerToolStripMenuItem;
        private System.Windows.Forms.Button companyNewsBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox sectorsComboBox;
        public System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox companyListingslistBox;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox timeSeriesComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox timeSeriesIntervalComboBox;
        private System.Windows.Forms.Button updateChartBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox desiredDataComboBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonLocateCompany;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel SummaryQuoteLbl;
        private System.Windows.Forms.Label IndustryLbl;
        private System.Windows.Forms.Label IPOyearLbl;
        private System.Windows.Forms.Label symbolLbl;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ToolStripMenuItem stockMarketStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downloadUpdatedCompaniesToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button addTrackCompanyButton;
        private System.Windows.Forms.ComboBox trackedCompaniesComboBox;
        private System.Windows.Forms.Button removeTrackCompanyButton;
    }
}

