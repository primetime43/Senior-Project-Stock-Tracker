using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Senior_Project_Stock_Tracker
{
    public partial class NewUserForm : DataRetriever
    {
        public NewUserForm()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }
        public static string name = "";
        public static long money = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userNameTxtBox.Text) || string.IsNullOrEmpty(moneyAmount.Text))
            {
                MessageBox.Show("You must fill out both boxes!");
                return;
            }

            name = userNameTxtBox.Text;
            money = long.Parse(Regex.Replace(moneyAmount.Text, @"^[$]|%$", string.Empty));

            if (money > 1000000000)//limit set to 1 billion
                MessageBox.Show("Max value is 1 billion, setting starting amount to max");

            this.Hide();
            FakeStockPurchaser frm2 = new FakeStockPurchaser();
            try
            {
                frm2.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Unable to perform this action");
            }
        }

        private void NewUser_Load_FormClosing(object sender, FormClosedEventArgs e)
        {
            StockTrackMain main = new StockTrackMain();
            main.ShowDialog();
        }
    }
}
