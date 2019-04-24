using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Globalization;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Senior_Project_Stock_Tracker
{
    public partial class FakeStockPurchaser : DataRetriever
    {
        private SQLiteConnection m_dbConnection;
        private string sql = "create table userDatabase (transID text, name text, companyName text, stockSymbol text, quantityPurchased long, purchasePrice text, totalCost text, purchaseDate text, startingMoney text, availableFunds text)";
        public FakeStockPurchaser()
        {
            InitializeComponent();
            string[] myArray = mapNameToSymbol.Keys.ToArray();//creates the array for searching
            searchTxtBox.AutoCompleteCustomSource.AddRange(myArray);//textbox for searching the array above and showing predictions

            if (NewUserForm.newUser)
                createUserDB();
            else
                readExistingUserDB();
        }

        public static double startingCash;
        public static double currentCash;
        public static double currentStockVal;
        private static string companyName;

        Dictionary<string, StockItem> stocksOwned = new Dictionary<string, StockItem>();
        private void FakeStockPurchaser_Load(object sender, EventArgs e)
        {
            if (NewUserForm.newUser)
                groupBox1.Text = NewUserForm.name + "'s Wallet";
            else
                groupBox1.Text = "Test";//name of user read from existing file (will pass it down)
            strCashLbl.Text = NewUserForm.money.ToString("C2", CultureInfo.CurrentCulture);
            currentCashLbl.Text = NewUserForm.money.ToString("C2", CultureInfo.CurrentCulture);
            portfolioValLbl.Text = "" + 0;
            startingCash = NewUserForm.money;
            companiesListBox.Items.Clear();
            foreach(KeyValuePair<string, List<string>> entry in mapNameToSymbol)
            {
                string value = entry.Key.ToString();
                if (!companiesListBox.Items.Contains(value))//so the company doesnt get added multiple times to listbox (when there are multiple symbols for a company)
                    companiesListBox.Items.Add(value);
            }
        }
            
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (mapNameToSymbol.ContainsKey(searchTxtBox.Text))
            {
                companyName = searchTxtBox.Text;
                companiesListBox.SelectedItem = searchTxtBox.Text;
                companiesListBox.Focus();
            }
            else
                MessageBox.Show("Unable to locate company!");
        }

        private void buyBtn_Click(object sender, EventArgs e)
        {
            m_dbConnection.Open();
            string nameOfSelectedCompany = companiesListBox.SelectedItem.ToString();
            StockItem current = new StockItem();
            current.name = nameOfSelectedCompany;
            current.symbol = stockSymbolLbl.Text;
            current.purchasePrice = intraday.oneMin[keys[0]].open;//crashing here
            current.quantity = Convert.ToInt32(numericUpDown1.Value); 
            current.value = Convert.ToInt32(numericUpDown1.Value) * currentStockPrice;
            currentStockVal += Convert.ToInt32(numericUpDown1.Value) * currentStockPrice;
            currentCash = startingCash - Convert.ToInt32(numericUpDown1.Value) * currentStockPrice;

            currentCashLbl.Text = currentCash.ToString("C2", CultureInfo.CurrentCulture);
            portfolioValLbl.Text = currentStockVal.ToString("C2", CultureInfo.CurrentCulture);

            stocksOwned.Add(current.symbol, current);
            stockOwnedListBox.Items.Add(current.name);

            double purchasePrice = intraday.oneMin[keys[0]].open;
            string todaysDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff",CultureInfo.InvariantCulture);

            //sql stuff below here testing
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            string idTest = string.Format(@"{0}", DateTime.Now.Ticks);
            double totalCost = Convert.ToInt32(numericUpDown1.Value) * currentStockPrice;

            sql = "insert into userDatabase (transID, name, companyName, stockSymbol, quantityPurchased, purchasePrice, totalCost, purchaseDate, startingMoney, availableFunds) values (" + idTest + ",'" +  NewUserForm.name + "','" + nameOfSelectedCompany + "','" + stockSymbolLbl.Text + "'," + Convert.ToInt32(numericUpDown1.Value) + ",'" + purchasePrice.ToString("C2", CultureInfo.CurrentCulture) + "','" + totalCost.ToString("C2", CultureInfo.CurrentCulture) + "','" + todaysDate + "','" + startingCash.ToString("C2", CultureInfo.CurrentCulture) + "','" + currentCash.ToString("C2", CultureInfo.CurrentCulture) + "')";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        //pick up here
        private void stockOwnedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            companyName = stockOwnedListBox.SelectedItem.ToString();

            if (stocksOwned.ContainsKey(mapNameToSymbol[companyName][0]))
            {
                quantOwnLbl.Text = stocksOwned[companyName].quantity.ToString();
                purchasePriceLbl.Text = stocksOwned[companyName].purchasePrice.ToString();//will be data from 
                curPriceLbl.Text = intraday.oneMin[keys[0]].open.ToString();
                currValLbl.Text = "";
            }
            else
                MessageBox.Show("Error");
        }

        private static double currentStockPrice;
        private RootIntraday intraday;
        string[] keys;
        private async void companiesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = 1;
            intraday = null;
            keys = null;
            stockSymbolLbl.Text = mapNameToSymbol[companiesListBox.SelectedItem.ToString()][0];

            string symbolJSONreturn = await retrieveSymbolData("TIME_SERIES_INTRADAY", mapNameToSymbol[companiesListBox.SelectedItem.ToString()][0], "1min");

            intraday = new RootIntraday();//contains the 1,5,15,30,60 min objs
            intraday = JsonConvert.DeserializeObject<RootIntraday>(symbolJSONreturn);

            //need to add a throw so when user reaches max api requests
            keys = new string[intraday.oneMin.Keys.Count];
            keys = intraday.oneMin.Keys.ToArray();
            currentStockPrice = intraday.oneMin[keys[0]].open;
            stockPriceLbl.Text = intraday.oneMin[keys[0]].open.ToString("C2", CultureInfo.CurrentCulture);

            double tempCost = Convert.ToInt32(numericUpDown1.Value) * currentStockPrice;
            totCostLbl.Text = tempCost.ToString("C2", CultureInfo.CurrentCulture);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double tempCost = Convert.ToInt32(numericUpDown1.Value) * currentStockPrice;
            totCostLbl.Text = tempCost.ToString("C2", CultureInfo.CurrentCulture);
        }

        private void createUserDB()
        {
            var uniqueFileName = string.Format(@"{0}", DateTime.Now.Ticks);
            string sqlDBFileName = NewUserForm.name + "_" + uniqueFileName + ".sqlite";

            //create the actual file
            SQLiteConnection.CreateFile(NewUserForm.usersPath + "\\" + sqlDBFileName);

            //open connection to the file
            m_dbConnection = new SQLiteConnection("Data Source=" + NewUserForm.usersPath + "\\" + sqlDBFileName + "; Version=3;");
            m_dbConnection.Open();

            //create the db table
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        private void readExistingUserDB()
        {
            MessageBox.Show(NewUserForm.usersPath + "\\" + NewUserForm.existingUserFileName + ".sqlite");

            m_dbConnection = new SQLiteConnection("Data Source=" + NewUserForm.usersPath + "\\" + NewUserForm.existingUserFileName + ".sqlite" + "; Version=3;");
            m_dbConnection.Open();

            sql = "select * from userDatabase";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
                Console.WriteLine("transID: " + reader["transID"] + " name: " + reader["name"] + " companyName: " + reader["companyName"] + " stockSymbol: " + reader["stockSymbol"] + " quantityPurchased: " + reader["quantityPurchased"] + " purchasePrice: " + reader["purchasePrice"] + " totalCost: " + reader["totalCost"] + " purchaseDate: " + reader["purchaseDate"] + " startingMoney: " + reader["startingMoney"] + " availableFunds: " + reader["availableFunds"]);
            m_dbConnection.Close();
        }
    }
}
