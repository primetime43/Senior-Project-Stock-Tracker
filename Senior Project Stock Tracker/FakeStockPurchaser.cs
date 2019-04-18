using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Senior_Project_Stock_Tracker
{
    public partial class FakeStockPurchaser : DataRetriever
    {
        
        public FakeStockPurchaser()
        {
            InitializeComponent();
            

        }
        
        public static long startingCash = 0;
        public static long currentCash = 0;
        public static long currentStockVal = 0;

        Dictionary<String, StockItem> stocks = new Dictionary<String, StockItem>();
        private void FakeStockPurchaser_Load(object sender, EventArgs e)
        {

            groupBox1.Text = NewUserForm.name + "'s Wallet";
            strCashLbl.Text = NewUserForm.money.ToString();
            currentCashLbl.Text = NewUserForm.money.ToString();
            stockValLbl.Text = "" + 0;
            startingCash = NewUserForm.money;
            listBox2.Items.Clear();
            foreach(KeyValuePair<string, List<string>> entry in mapNameToSymbol)
            {
                string value = entry.Key.ToString();
                if (!listBox2.Items.Contains(value))//so the company doesnt get added multiple times to listbox (when there are multiple symbols for a company)
                    listBox2.Items.Add(value);
            }

            listBox2.Sorted = true;

            
            
        }
            
        
        private void button1_Click(object sender, EventArgs e)
        {
            String company = textBox1.Text;




        }

        private void buyBtn_Click(object sender, EventArgs e)
        {
            StockItem current = new StockItem();
            current.name = listBox2.SelectedItem.ToString();
            current.symbol = stockNameLbl.Text;
            current.purchasePrice = long.Parse(stockPriceLbl.Text);
            current.quantity = int.Parse(quantity.Text);
            current.value = current.quantity * current.purchasePrice;
            currentStockVal += current.value;
            currentCash -= current.value;

            currentCashLbl.Text = currentCash.ToString();
            stockValLbl.Text = currentStockVal.ToString();

            stocks.Add(current.symbol, current);
            listBox1.Items.Add(current.name);
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String companyName = listBox1.SelectedItem.ToString();
            quantOwnLbl.Text = "";
            purchPriceLbl.Text = "";
            curPriceLbl.Text = "";
            currValLbl.Text = "";
        }

        private async void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            stockNameLbl.Text = mapNameToSymbol[listBox2.SelectedItem.ToString()][0];


            String symbolJSONreturn = await retrieveSymbolData("TIME_SERIES_INTRADAY", mapNameToSymbol[listBox2.SelectedItem.ToString()][0], "1min");

            RootIntraday intraday = new RootIntraday();//contains the 1,5,15,30,60 min objs
            intraday = JsonConvert.DeserializeObject<RootIntraday>(symbolJSONreturn);
            Console.WriteLine(intraday);
            String[] keys = null;
            keys = new string[intraday.oneMin.Keys.Count];
            keys = intraday.oneMin.Keys.ToArray();
            stockPriceLbl.Text = intraday.oneMin[keys[0]].open.ToString();

            if (quantity.Text.Equals("0")){
                totCostLbl.Text = "0";
            }
            



        }

  

    
      

        private void button2_Click_1(object sender, EventArgs e)
        {
            long temporaryVal = long.Parse(quantity.Text) * long.Parse(stockPriceLbl.Text);
            totCostLbl.Text = (temporaryVal).ToString();
        }
    }
}
