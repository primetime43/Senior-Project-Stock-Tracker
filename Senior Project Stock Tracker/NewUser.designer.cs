namespace Senior_Project_Stock_Tracker
{
    partial class NewUserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewUserForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.moneyAmount = new System.Windows.Forms.TextBox();
            this.userNameTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.existingUsersListBox = new System.Windows.Forms.ListBox();
            this.loadExistingUserBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.moneyAmount);
            this.groupBox1.Controls.Add(this.userNameTxtBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(364, 167);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New User Info";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(75, 99);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(211, 59);
            this.button1.TabIndex = 4;
            this.button1.Text = "Begin Trading";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // moneyAmount
            // 
            this.moneyAmount.Location = new System.Drawing.Point(112, 69);
            this.moneyAmount.MaxLength = 1000000000;
            this.moneyAmount.Name = "moneyAmount";
            this.moneyAmount.Size = new System.Drawing.Size(223, 21);
            this.moneyAmount.TabIndex = 3;
            // 
            // userNameTxtBox
            // 
            this.userNameTxtBox.Location = new System.Drawing.Point(112, 37);
            this.userNameTxtBox.Name = "userNameTxtBox";
            this.userNameTxtBox.Size = new System.Drawing.Size(223, 21);
            this.userNameTxtBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Starting Money: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // existingUsersListBox
            // 
            this.existingUsersListBox.FormattingEnabled = true;
            this.existingUsersListBox.ItemHeight = 15;
            this.existingUsersListBox.Location = new System.Drawing.Point(23, 20);
            this.existingUsersListBox.Name = "existingUsersListBox";
            this.existingUsersListBox.Size = new System.Drawing.Size(235, 94);
            this.existingUsersListBox.TabIndex = 1;
            // 
            // loadExistingUserBtn
            // 
            this.loadExistingUserBtn.BackColor = System.Drawing.Color.White;
            this.loadExistingUserBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadExistingUserBtn.Location = new System.Drawing.Point(64, 120);
            this.loadExistingUserBtn.Name = "loadExistingUserBtn";
            this.loadExistingUserBtn.Size = new System.Drawing.Size(159, 38);
            this.loadExistingUserBtn.TabIndex = 5;
            this.loadExistingUserBtn.Text = "Load User Profile";
            this.loadExistingUserBtn.UseVisualStyleBackColor = false;
            this.loadExistingUserBtn.Click += new System.EventHandler(this.loadExistingUserBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.existingUsersListBox);
            this.groupBox2.Controls.Add(this.loadExistingUserBtn);
            this.groupBox2.Location = new System.Drawing.Point(384, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(276, 167);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Existing Users";
            // 
            // NewUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(679, 195);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewUserForm";
            this.Text = "New User";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox moneyAmount;
        private System.Windows.Forms.TextBox userNameTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox existingUsersListBox;
        private System.Windows.Forms.Button loadExistingUserBtn;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}