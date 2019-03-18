namespace Senior_Project_Stock_Tracker
{
    partial class News
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
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.newsDisplayListView = new System.Windows.Forms.ListView();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(12, 12);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(962, 786);
            this.webBrowser1.TabIndex = 2;
            // 
            // newsDisplayListView
            // 
            this.newsDisplayListView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.newsDisplayListView.Location = new System.Drawing.Point(993, 12);
            this.newsDisplayListView.Name = "newsDisplayListView";
            this.newsDisplayListView.Size = new System.Drawing.Size(634, 618);
            this.newsDisplayListView.TabIndex = 1;
            this.newsDisplayListView.UseCompatibleStateImageBehavior = false;
            this.newsDisplayListView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1399, 654);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(203, 144);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // News
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1639, 828);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.newsDisplayListView);
            this.Controls.Add(this.webBrowser1);
            this.Name = "News";
            this.Text = "News";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView newsDisplayListView;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}