using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Windows;
using System.Globalization;

namespace Senior_Project_Stock_Tracker
{
    public partial class FakeStockPurchaser : DataRetriever
    {
        
        public FakeStockPurchaser()
        {
            InitializeComponent();
            string[] myArray = mapNameToSymbol.Keys.ToArray();//testing
            searchTxtBox.AutoCompleteCustomSource.AddRange(myArray);//test for search

        }

        public static double startingCash;
        public static double currentCash;
        public static double currentStockVal;
        private string companyName;

        Dictionary<string, StockItem> stocks = new Dictionary<string, StockItem>();
        private void FakeStockPurchaser_Load(object sender, EventArgs e)
        {

            groupBox1.Text = NewUserForm.name + "'s Wallet";
            strCashLbl.Text = NewUserForm.money.ToString("C2", CultureInfo.CurrentCulture);
            currentCashLbl.Text = NewUserForm.money.ToString("C2", CultureInfo.CurrentCulture);
            stockValLbl.Text = "" + 0;
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
            StockItem current = new StockItem();
            current.name = companiesListBox.SelectedItem.ToString();
            current.symbol = stockNameLbl.Text;
            current.purchasePrice = intraday.oneMin[keys[0]].open;//crashing here
            current.quantity = Convert.ToInt32(numericUpDown1.Value); 
            current.value = Convert.ToInt32(numericUpDown1.Value) * currentStockPrice;
            currentStockVal += Convert.ToInt32(numericUpDown1.Value) * currentStockPrice;
            currentCash = startingCash - Convert.ToInt32(numericUpDown1.Value) * currentStockPrice;

            currentCashLbl.Text = currentCash.ToString("C2", CultureInfo.CurrentCulture);
            stockValLbl.Text = currentStockVal.ToString("C2", CultureInfo.CurrentCulture);

            /*stocks.Add(current.symbol, current);
            StockOwnedListBox.Items.Add(current.name);*/
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            companyName = StockOwnedListBox.SelectedItem.ToString();
            quantOwnLbl.Text = "";
            purchPriceLbl.Text = "";
            curPriceLbl.Text = "";
            currValLbl.Text = "";
        }

        private static double currentStockPrice;
        private RootIntraday intraday;
        string[] keys;
        private async void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = 1;
            intraday = null;
            keys = null;
            stockNameLbl.Text = mapNameToSymbol[companiesListBox.SelectedItem.ToString()][0];

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
    }
}
