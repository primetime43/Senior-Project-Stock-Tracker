using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Globalization;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Senior_Project_Stock_Tracker
{
    public partial class FakeStockPurchaser : DataRetriever
    {
        private SQLiteConnection m_dbConnection;
        private string sql = "create table userDatabase (transID text, name text, companyName text, stockSymbol text, quantityPurchased long, purchasePrice text, totalCost text, lastTransDate text, startingMoney text, availableFunds text)";
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
            {
                groupBox1.Text = NewUserForm.name + "'s Wallet";
                strCash.Text = NewUserForm.money.ToString("C2", CultureInfo.CurrentCulture);
                availFunds.Text = NewUserForm.money.ToString("C2", CultureInfo.CurrentCulture);
                startingCash = NewUserForm.money;
            }



            companiesListBox.Items.Clear();
            foreach (KeyValuePair<string, List<string>> entry in mapNameToSymbol)
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
            current.purchasePrice = intraday.oneMin[keys[0]].open;
            current.quantity = Convert.ToInt32(numericUpDown1.Value);
            current.value = Convert.ToInt32(numericUpDown1.Value) * currentStockPrice;
            currentStockVal += (Convert.ToInt32(numericUpDown1.Value) * currentStockPrice);
            currentCash = startingCash - (Convert.ToInt32(numericUpDown1.Value) * currentStockPrice);

            availFunds.ResetText();
            availFunds.Text = currentCash.ToString("C2", CultureInfo.CurrentCulture);


            stocksOwned.Add(current.symbol, current);
            stockOwnedListBox.Items.Add(current.name);

            double purchasePrice = intraday.oneMin[keys[0]].open;
            string todaysDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            //sql stuff below here testing
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            string idTest = string.Format(@"{0}", DateTime.Now.Ticks);
            double totalCost = Convert.ToInt32(numericUpDown1.Value) * currentStockPrice;

            sql = "insert into userDatabase (transID, name, companyName, stockSymbol, quantityPurchased, purchasePrice, totalCost, lastTransDate, startingMoney, availableFunds) values (" + idTest + ",'" + NewUserForm.name + "','" + nameOfSelectedCompany + "','" + stockSymbolLbl.Text + "'," + Convert.ToInt32(numericUpDown1.Value) + ",'" + purchasePrice.ToString("C2", CultureInfo.CurrentCulture) + "','" + totalCost.ToString("C2", CultureInfo.CurrentCulture) + "','" + todaysDate + "','" + startingCash.ToString("C2", CultureInfo.CurrentCulture) + "','" + currentCash.ToString("C2", CultureInfo.CurrentCulture) + "')";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        private async void stockOwnedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (stockOwnedListBox.SelectedItem != null)
            {
                companyName = stockOwnedListBox.SelectedItem.ToString();
                String key = mapNameToSymbol[companyName][0];

                if (stocksOwned.ContainsKey(mapNameToSymbol[companyName][0]))
                {
                    numericUpDown1.Value = 1;
                    intraday = null;
                    keys = null;


                    string symbolJSONreturn = await retrieveSymbolData("TIME_SERIES_INTRADAY", mapNameToSymbol[companyName][0], "1min");

                    intraday = new RootIntraday();//contains the 1,5,15,30,60 min objs
                    intraday = JsonConvert.DeserializeObject<RootIntraday>(symbolJSONreturn);

                    //need to add a throw so when user reaches max api requests
                    keys = new string[intraday.oneMin.Keys.Count];
                    keys = intraday.oneMin.Keys.ToArray();
                    quantOwnLbl.Text = stocksOwned[key].quantity.ToString();
                    purchasePriceLbl.Text = stocksOwned[key].purchasePrice.ToString("C2", CultureInfo.CurrentCulture);//will be data fr
                    curPriceLbl.Text = intraday.oneMin[keys[0]].open.ToString("C2", CultureInfo.CurrentCulture);
                    currValLbl.Text = (stocksOwned[key].quantity * intraday.oneMin[keys[0]].open).ToString("C2", CultureInfo.CurrentCulture);
                    soldPriceLbl.Text = curPriceLbl.Text;
                    initialValLbl.Text = stocksOwned[key].value.ToString("C2", CultureInfo.CurrentCulture);
                    gainLossLbl.Text = (stocksOwned[key].value - (stocksOwned[key].quantity * intraday.oneMin[keys[0]].open)).ToString("C2", CultureInfo.CurrentCulture);
                }
                else
                    MessageBox.Show("Error");
            }

            quantOwnLbl.Visible = true;
            purchasePriceLbl.Visible = true;
            curPriceLbl.Visible = true;
            initialValLbl.Visible = true;
            currValLbl.Visible = true;
            gainLossLbl.Visible = true;
            soldPriceLbl.Visible = true;
        }

        private static double currentStockPrice;
        private RootIntraday intraday;
        string[] keys;
        private async void companiesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string symbolJSONreturn = await retrieveSymbolData("TIME_SERIES_INTRADAY", mapNameToSymbol[companiesListBox.SelectedItem.ToString()][0], "1min");

            //for when the api notifies user must wait to make more request due to free api restrictions
            ExceededReq notify = new ExceededReq();
            notify = JsonConvert.DeserializeObject<ExceededReq>(symbolJSONreturn);
            if (notify != null && notify.Note != null || notify.Note == "")
            {
                MessageBox.Show(notify.Note.ToString());
                return;
            }

            numericUpDown1.Value = 1;
            intraday = null;
            keys = null;
            stockSymbolLbl.Text = mapNameToSymbol[companiesListBox.SelectedItem.ToString()][0];

            intraday = new RootIntraday();//contains the 1,5,15,30,60 min objs
            intraday = JsonConvert.DeserializeObject<RootIntraday>(symbolJSONreturn);

            //need to add a throw so when user reaches max api requests
            keys = new string[intraday.oneMin.Keys.Count];
            keys = intraday.oneMin.Keys.ToArray();
            currentStockPrice = intraday.oneMin[keys[0]].open;
            stockPriceLbl.Text = intraday.oneMin[keys[0]].open.ToString("C2", CultureInfo.CurrentCulture);

            double tempCost = Convert.ToInt32(numericUpDown1.Value) * currentStockPrice;
            totCostLbl.Text = tempCost.ToString("C2", CultureInfo.CurrentCulture);

            stockSymbolLbl.Visible = true;
            stockPriceLbl.Visible = true;
            totCostLbl.Visible = true;
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
            m_dbConnection = new SQLiteConnection("Data Source=" + NewUserForm.usersPath + "\\" + NewUserForm.existingUserFileName + ".sqlite" + "; Version=3;");
            m_dbConnection.Open();

            sql = "select * from userDatabase";

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

            SQLiteDataReader reader = command.ExecuteReader();
            string startCash = "";
            string availableCash = "";
            string name = "";

            while (reader.Read())
            {

                Console.WriteLine("transID: " + reader["transID"] + " name: " + reader["name"] + " companyName: " + reader["companyName"] + " stockSymbol: " + reader["stockSymbol"] + " quantityPurchased: " + reader["quantityPurchased"] + " purchasePrice: " + reader["purchasePrice"] + " totalCost: " + reader["totalCost"] + " lastTransDate: " + reader["lastTransDate"] + " startingMoney: " + reader["startingMoney"] + " availableFunds: " + reader["availableFunds"]);

                StockItem current = new StockItem();
                name = reader["name"].ToString();
                current.name = reader["companyName"].ToString();
                current.purchasePrice = double.Parse(Regex.Replace(reader["purchasePrice"].ToString(), @"^[$]|%$", string.Empty));
                current.quantity = Convert.ToInt32(reader["quantityPurchased"]);
                current.symbol = reader["stockSymbol"].ToString();
                current.value = double.Parse(Regex.Replace((reader["totalCost"].ToString()), @"^[$]|%$", string.Empty));

                stockOwnedListBox.Items.Add(current.name);
                stocksOwned.Add(current.symbol, current);
                startCash = reader["startingMoney"].ToString();
                startingCash = double.Parse(Regex.Replace(startCash, @"^[$]|%$", string.Empty));

                availableCash = reader["availableFunds"].ToString();
            }
            strCash.ResetText();
            availFunds.ResetText();

            groupBox1.Text = name;
            strCash.Text = startCash.ToString();
            availFunds.Text = availableCash.ToString();

            m_dbConnection.Close();

            //testing for deleting records after reading them all in
            m_dbConnection = new SQLiteConnection("Data Source=" + NewUserForm.usersPath + "\\" + NewUserForm.existingUserFileName + ".sqlite" + "; Version=3;");
            m_dbConnection.Open();

            sql = "delete from userDatabase";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        private void sellStock_Click(object sender, EventArgs e)
        {
            m_dbConnection = new SQLiteConnection("Data Source=" + NewUserForm.usersPath + "\\" + NewUserForm.existingUserFileName + ".sqlite" + "; Version=3;");
            m_dbConnection.Open();
            string companyNameSell = stockOwnedListBox.SelectedItem.ToString();
            string key = mapNameToSymbol[companyNameSell][0];

            StockItem updatedStock = new StockItem();
            StockItem oldStock = stocksOwned[key];

            updatedStock.name = oldStock.name;
            updatedStock.symbol = oldStock.symbol;
            updatedStock.purchasePrice = oldStock.purchasePrice;
            updatedStock.quantity = oldStock.quantity - int.Parse(quantityStockSell.Value.ToString());
            updatedStock.value = updatedStock.quantity * oldStock.purchasePrice;
            decimal currentMone = decimal.Parse(Regex.Replace(availFunds.Text, @"^[$]|%$", string.Empty));

            availFunds.ResetText();
            availFunds.Text = (currentMone + (quantityStockSell.Value * decimal.Parse(Regex.Replace(soldPriceLbl.Text, @"^[$]|%$", string.Empty)))).ToString("C2", CultureInfo.CurrentCulture);

            stocksOwned.Remove(key);
            if (updatedStock.quantity > 0)
                stocksOwned.Add(key, updatedStock);
            else
            {
                int currIn = stockOwnedListBox.SelectedIndex;
                stockOwnedListBox.Items.Remove(companyNameSell);
                if (stockOwnedListBox.Items.Count == 0)
                {
                    stockOwnedListBox.SelectedIndex = -1;
                }
                else if (currIn == (stockOwnedListBox.Items.Count - 1))
                {
                    stockOwnedListBox.SelectedIndex = currIn - 1;
                }
                else
                {
                    stockOwnedListBox.SelectedIndex = currIn + 1;
                }
            }
            this.Refresh();
            
            //testing for updating DB file after selling
            string todaysDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);

            //sql stuff below here testing
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            string idTest = string.Format(@"{0}", DateTime.Now.Ticks);
            double totalCost = Convert.ToInt32(quantityStockSell.Value) * oldStock.purchasePrice;//need to subtract the value of say 1 stock from value of totalCost of 3 so you get the value of only 2, should be same price when purchased

            sql = "insert into userDatabase (transID, name, companyName, stockSymbol, quantityPurchased, purchasePrice, totalCost, lastTransDate, startingMoney, availableFunds) values (" + idTest + ",'" + NewUserForm.name + "','" + updatedStock.name + "','" + stockSymbolLbl.Text + "'," + Convert.ToInt32(numericUpDown1.Value) + ",'" + oldStock.purchasePrice.ToString("C2", CultureInfo.CurrentCulture) + "','" + totalCost.ToString("C2", CultureInfo.CurrentCulture) + "','" + todaysDate + "','" + startingCash.ToString("C2", CultureInfo.CurrentCulture) + "','" + currentCash.ToString("C2", CultureInfo.CurrentCulture) + "')";

            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }

        private void quantityStockSell_ValueChanged(object sender, EventArgs e)
        {
            if (quantityStockSell.Value > decimal.Parse(Regex.Replace((quantOwnLbl.Text), @"^[$]|%$", string.Empty)))
            {
                MessageBox.Show("You do not own this many stocks!");
                quantityStockSell.Value = decimal.Parse(Regex.Replace((quantOwnLbl.Text), @"^[$]|%$", string.Empty));
                totalSoldValLbl.Text = (quantityStockSell.Value * decimal.Parse(Regex.Replace((soldPriceLbl.Text.ToString()), @"^[$]|%$", string.Empty))).ToString("C2", CultureInfo.CurrentCulture);
            }
            else
                totalSoldValLbl.Text = (quantityStockSell.Value * decimal.Parse(Regex.Replace((soldPriceLbl.Text.ToString()), @"^[$]|%$", string.Empty))).ToString("C2", CultureInfo.CurrentCulture);
            totalSoldValLbl.Visible = true;
        }
    }


}
