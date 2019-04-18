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
        public static String name = "";
        public static long money = 0;

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Empty box");
                return;
            }
            name = textBox1.Text;
            money = long.Parse(Regex.Replace(textBox2.Text, @"^[$]|%$", string.Empty));

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
