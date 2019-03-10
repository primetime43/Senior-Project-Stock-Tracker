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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fakeStockTrainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkBrokerAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.robinhoodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sectorsComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.companyListingslistBox = new System.Windows.Forms.ListBox();
            this.cartesianChart1 = new LiveCharts.WinForms.CartesianChart();
            this.label4 = new System.Windows.Forms.Label();
            this.timeSeriesComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.timeSeriesIntervalComboBox = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1647, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem1,
            this.fakeStockTrainerToolStripMenuItem,
            this.linkBrokerAccountToolStripMenuItem});
            this.testToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(55, 21);
            this.testToolStripMenuItem.Text = "Mode";
            // 
            // testToolStripMenuItem1
            // 
            this.testToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.testToolStripMenuItem1.Name = "testToolStripMenuItem1";
            this.testToolStripMenuItem1.Size = new System.Drawing.Size(181, 22);
            this.testToolStripMenuItem1.Text = "Stock Tracker";
            // 
            // fakeStockTrainerToolStripMenuItem
            // 
            this.fakeStockTrainerToolStripMenuItem.Name = "fakeStockTrainerToolStripMenuItem";
            this.fakeStockTrainerToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.fakeStockTrainerToolStripMenuItem.Text = "Fake Stock Trainer";
            // 
            // linkBrokerAccountToolStripMenuItem
            // 
            this.linkBrokerAccountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.robinhoodToolStripMenuItem});
            this.linkBrokerAccountToolStripMenuItem.Name = "linkBrokerAccountToolStripMenuItem";
            this.linkBrokerAccountToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.linkBrokerAccountToolStripMenuItem.Text = "Broker Account";
            // 
            // robinhoodToolStripMenuItem
            // 
            this.robinhoodToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buyToolStripMenuItem,
            this.sellToolStripMenuItem});
            this.robinhoodToolStripMenuItem.Name = "robinhoodToolStripMenuItem";
            this.robinhoodToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.robinhoodToolStripMenuItem.Text = "Robinhood";
            // 
            // buyToolStripMenuItem
            // 
            this.buyToolStripMenuItem.Name = "buyToolStripMenuItem";
            this.buyToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.buyToolStripMenuItem.Text = "Buy";
            // 
            // sellToolStripMenuItem
            // 
            this.sellToolStripMenuItem.Name = "sellToolStripMenuItem";
            this.sellToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
            this.sellToolStripMenuItem.Text = "Sell";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(37, 443);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(355, 104);
            this.button1.TabIndex = 1;
            this.button1.Text = "Show News";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(585, 68);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(472, 351);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(860, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "JSON Output Testing:";
            // 
            // textBox1
            // 
            this.textBox1.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.textBox1.Location = new System.Drawing.Point(401, 506);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(176, 22);
            this.textBox1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(398, 487);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Search Stock Symbol:";
            // 
            // sectorsComboBox
            // 
            this.sectorsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sectorsComboBox.FormattingEnabled = true;
            this.sectorsComboBox.Location = new System.Drawing.Point(12, 54);
            this.sectorsComboBox.MaxDropDownItems = 50;
            this.sectorsComboBox.Name = "sectorsComboBox";
            this.sectorsComboBox.Size = new System.Drawing.Size(198, 24);
            this.sectorsComboBox.TabIndex = 6;
            this.sectorsComboBox.SelectedIndexChanged += new System.EventHandler(this.sectorsComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Sector:";
            // 
            // companyListingslistBox
            // 
            this.companyListingslistBox.FormattingEnabled = true;
            this.companyListingslistBox.ItemHeight = 16;
            this.companyListingslistBox.Location = new System.Drawing.Point(270, 35);
            this.companyListingslistBox.Name = "companyListingslistBox";
            this.companyListingslistBox.Size = new System.Drawing.Size(267, 276);
            this.companyListingslistBox.TabIndex = 8;
            this.companyListingslistBox.SelectedIndexChanged += new System.EventHandler(this.companyListingslistBox_SelectedIndexChanged);
            // 
            // cartesianChart1
            // 
            this.cartesianChart1.Location = new System.Drawing.Point(1063, 96);
            this.cartesianChart1.Name = "cartesianChart1";
            this.cartesianChart1.Size = new System.Drawing.Size(552, 407);
            this.cartesianChart1.TabIndex = 9;
            this.cartesianChart1.Text = "cartesianChart1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "Time Series:";
            // 
            // timeSeriesComboBox
            // 
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
            this.timeSeriesComboBox.Location = new System.Drawing.Point(12, 133);
            this.timeSeriesComboBox.Name = "timeSeriesComboBox";
            this.timeSeriesComboBox.Size = new System.Drawing.Size(198, 24);
            this.timeSeriesComboBox.TabIndex = 11;
            this.timeSeriesComboBox.SelectedIndexChanged += new System.EventHandler(this.timeSeriesComboBox_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "Time Series Interval:";
            // 
            // timeSeriesIntervalComboBox
            // 
            this.timeSeriesIntervalComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.timeSeriesIntervalComboBox.FormattingEnabled = true;
            this.timeSeriesIntervalComboBox.Items.AddRange(new object[] {
            "1 Minute",
            "5 Minutes",
            "15 Minutes",
            "30 Minutes",
            "60 Minutes"});
            this.timeSeriesIntervalComboBox.Location = new System.Drawing.Point(12, 238);
            this.timeSeriesIntervalComboBox.Name = "timeSeriesIntervalComboBox";
            this.timeSeriesIntervalComboBox.Size = new System.Drawing.Size(198, 24);
            this.timeSeriesIntervalComboBox.TabIndex = 13;
            this.timeSeriesIntervalComboBox.SelectedIndexChanged += new System.EventHandler(this.timeSeriesIntervalComboBox_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(37, 564);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(355, 104);
            this.button2.TabIndex = 14;
            this.button2.Text = "Test Chart";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // StockTrackMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1647, 703);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.timeSeriesIntervalComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.timeSeriesComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cartesianChart1);
            this.Controls.Add(this.companyListingslistBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sectorsComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StockTrackMain";
            this.Text = "Stock Market Tracker";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fakeStockTrainerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkBrokerAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem robinhoodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sellToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox sectorsComboBox;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox companyListingslistBox;
        private LiveCharts.WinForms.CartesianChart cartesianChart1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox timeSeriesComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox timeSeriesIntervalComboBox;
        private System.Windows.Forms.Button button2;
    }
}

