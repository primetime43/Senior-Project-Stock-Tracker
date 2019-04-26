using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Senior_Project_Stock_Tracker
{
    public partial class NewUserForm : DataRetriever
    {
        public static string usersPath = Directory.GetCurrentDirectory() + "\\Users";
        public NewUserForm()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            if (!Directory.Exists(usersPath))//Users directory doesnt exists, create it
            {
                Directory.CreateDirectory(usersPath);
            }
            else//read the files from the Users folder directory and load file names into the listbox
            {
                string[] files = Directory.GetFiles(usersPath);
                foreach (string fileName in files)
                    if (Path.GetFileName(fileName).Length > 10)
                        existingUsersListBox.Items.Add(Path.GetFileName(fileName).Substring(0, Path.GetFileName(fileName).IndexOf('.')));
            }
        }
        public static string name = "";
        public static long money = 0;
        public static bool newUser = false;

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
            {
                MessageBox.Show("Max value is 1 billion, setting starting amount to max");
                money = 1000000000;
            }

            newUser = true;

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

        public static string existingUserFileName;
        private void loadExistingUserBtn_Click(object sender, EventArgs e)
        {
            newUser = false;
            existingUserFileName = existingUsersListBox.SelectedItem.ToString();

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
    }
}
