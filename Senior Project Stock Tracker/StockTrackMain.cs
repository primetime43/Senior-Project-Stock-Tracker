using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;

//API Key: X0REJIV6R6ROZS3T
//News API Key: 94b9e25568ca4ee3bef44fc4c7ae335e

namespace Senior_Project_Stock_Tracker
{
    public partial class StockTrackMain : DataRetriever
    {
        public StockTrackMain()
        {
            InitializeComponent();
            if (!File.Exists(Directory.GetCurrentDirectory() + "\\NASDAQcompanylist.csv") || !File.Exists(Directory.GetCurrentDirectory() + "\\NYSEcompanylist.csv"))//if one of the two files don't exist, download them
            {
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Important Files Are Missing\nDownload them now?", "Missing Files", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);

                if (result == System.Windows.Forms.DialogResult.Yes)
                    downloadRequiredFiles();
            }
            else
            {
                File.SetAttributes("NASDAQcompanylist.csv", FileAttributes.Hidden);
                File.SetAttributes("NYSEcompanylist.csv", FileAttributes.Hidden);
                loadNASDAQCompanies();
                loadNYSECompanies();
                String[] myArray = mapNameToSymbol.Keys.ToArray();//creates this array to use in the searching for companies
                searchTextBox.AutoCompleteCustomSource.AddRange(myArray);//for searching for companies
                loadSectors();
                timeSeriesComboBox.SelectedItem = "Intraday";
                timeSeriesIntervalComboBox.SelectedItem = "1 Minute";
                desiredDataComboBox.SelectedItem = "Open";
                sectorsComboBox.SelectedIndex = 0;
                readTrackedStocks();
                CheckTime();
            }
        }

        private void CheckTime()
        {
            DateTime localTime = DateTime.Now;
            TimeZoneInfo USEasternTime = TimeZoneInfo.FindSystemTimeZoneById("US Eastern Standard Time");
            DateTime currentTime = TimeZoneInfo.ConvertTime(localTime, USEasternTime);

            int hour = int.Parse(currentTime.ToString("HH"));
            int minutes = int.Parse(currentTime.ToString("mm"));
            bool status = false;
            if ((hour > 9 && hour < 16) && (currentTime.DayOfWeek.ToString() != "Saturday" && currentTime.DayOfWeek.ToString() != "Sunday"))//open hours
            {
                if (hour == 9 && minutes >= 30 || hour > 9)//check if its 9 and if its 9:30 or greater OR past 9 so open
                {
                    this.BackColor = System.Drawing.Color.White;//open
                    status = true;
                }
                else//is 9 but not past 9:30 yet
                    this.BackColor = System.Drawing.SystemColors.Control;
            }
            else
                this.BackColor = System.Drawing.SystemColors.Control;

            if (status)
            {
                stockMarketStatusToolStripMenuItem.Text = "Stock Market: Open";
                stockMarketStatusToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                stockMarketStatusToolStripMenuItem.Text = "Stock Market: Closed";
                stockMarketStatusToolStripMenuItem.ForeColor = System.Drawing.Color.Red;
            }
        }

        public void loadSectors()
        {
            foreach (string sector in marketSectors)
                sectorsComboBox.Items.Add(sector);
        }

        private void companyNewsBtn_Click(object sender, EventArgs e)//open and display News form
        {
            this.Hide();
            News newsWindow = new News(selectedCompany);
            try
            {
                newsWindow.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Unable to perform this action");
            }
            this.Show();
        }

        private static Boolean moreThanOneSymbolflag = true;
        private void companyListingslistBox_SelectedIndexChanged(object sender, EventArgs e)//load the companies into the listbox
        {
            if (companyListingslistBox.SelectedIndex == -1)//no item selected
                updateChartBtn.Enabled = false;
            else
                updateChartBtn.Enabled = true;
            checkForMultipleSymbols(); //testing here
        }

        private static string selectedCompany;
        private async void checkForMultipleSymbols()//testing
        {
            selectedCompany = companyListingslistBox.GetItemText(companyListingslistBox.SelectedItem);
            if (moreThanOneSymbolflag)//load the companies names into listbox (enter here if only one symbol)
            {
                processSelectedCompany(selectedCompany);//processes the company to check if the company has one or more symbols
            }
            else//load the symbols into the listbox for user to select one
            {
                symbolJSONreturn = await retrieveSymbolData(selectedTimeSeries, selectedCompany, selectedTimeSeriesInterval);
                moreThanOneSymbolflag = true;
                loadCompaniesIntoListBox();//load companies back into the listbox after user selects desired symbol
                displayAdditionalInfo(selectedCompany);
            }

            setComparedStocks();
        }

        private async void processSelectedCompany(string selectedCompanyName)//takes in the company the user selected from the companylistbox
        {
            //problem is that its losing the selected company
            selectedCompany = companyListingslistBox.GetItemText(companyListingslistBox.SelectedItem);//testing
            if (mapNameToSymbol[selectedCompanyName].Count > 1)//selected company has more than one symbol (iterate through to check for more than 1 symbol)
            {
                companyListingslistBox.Items.Clear();//clear the companies listbox to load the symbols
                foreach (var listMember in mapNameToSymbol[selectedCompanyName])//mapped symbol to sector because symbols are unique values and some companies have multiple symbols in different sectors
                {
                    MessageBox.Show(selectedCompany);
                    companyListingslistBox.Items.Add(listMember);//load symbols into the listbox
                    moreThanOneSymbolflag = false;//set flag to false to get into the else statement in checkForMultipleSymbols method 
                }
            }
            else//one symbol, search mapNameToSymbol to find symbol for company name
            {
                //this is needed for the more than one symbol route!
                symbolJSONreturn = await retrieveSymbolData(selectedTimeSeries, mapNameToSymbol[selectedCompanyName][0], selectedTimeSeriesInterval);//testing
            }//from here, the json data is somehow lost (data is null without break point for some reason) (may be fixed, not 100% sure yet)
            displayAdditionalInfo();
        }

        private void displayAdditionalInfo(string symbol = null)
        {
            if (symbol == null)//one company symbol route
            {
                IndustryLbl.Text = stockMarketCompanies[mapNameToSymbol[selectedCompany][0]].industry;
                symbolLbl.Text = stockMarketCompanies[mapNameToSymbol[selectedCompany][0]].Symbol;
                IPOyearLbl.Text = stockMarketCompanies[mapNameToSymbol[selectedCompany][0]].IPOyear;
            }
            else//two symbol route
            {
                IndustryLbl.Text = stockMarketCompanies[symbol].industry;
                symbolLbl.Text = stockMarketCompanies[symbol].Symbol;
                IPOyearLbl.Text = stockMarketCompanies[symbol].IPOyear;
                selectedCompany = stockMarketCompanies[symbol].Name;
            }
            groupBox2.Text = selectedCompany + " Stock Info";
            SummaryQuoteLbl.Text = "https://www.nasdaq.com/symbol/" + symbolLbl.Text;
            IndustryLbl.Visible = true;
            symbolLbl.Visible = true;
            IPOyearLbl.Visible = true;
            SummaryQuoteLbl.Visible = true;
        }

        private void loadCompaniesIntoListBox()//populate the listbox containing the company names
        {
            companyListingslistBox.Items.Clear();//clear the box after selecting a different sector from combobox
            List<string> listValues = mapSymbolToSector[selectedSector];
            foreach (string value in listValues)
            {
                if (!companyListingslistBox.Items.Contains(value))//so the company doesnt get added multiple times to listbox (when there are multiple symbols for a company)
                    companyListingslistBox.Items.Add(value);
            }
        }

        private static string selectedSector, selectedTimeSeries, selectedTimeSeriesInterval;
        private void sectorsComboBox_SelectedIndexChanged(object sender, EventArgs e)//combobox that lists the sectors
        {
            selectedSector = sectorsComboBox.SelectedItem.ToString();
            loadCompaniesIntoListBox();
        }

        private static string desiredData;
        private void desiredDataComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            desiredData = desiredDataComboBox.SelectedItem.ToString();
        }

        private void updateDataComboBox(string selectedTimeSeries)
        {
            switch (selectedTimeSeries)
            {
                //add additional options for adjusted time series
                case "TIME_SERIES_DAILY_ADJUSTED":
                case "TIME_SERIES_WEEKLY_ADJUSTED":
                case "TIME_SERIES_MONTHLY_ADJUSTED":
                    if (!desiredDataComboBox.Items.Contains("Adjusted Close"))
                    {
                        desiredDataComboBox.Items.Add("Adjusted Close");
                        desiredDataComboBox.Items.Add("Dividend Amount");
                        desiredDataComboBox.Items.Add("Split Coefficient");
                    }
                    break;
                //remove them when adjusted time series isn't selected
                default:
                    desiredDataComboBox.Items.Remove("Adjusted Close");
                    desiredDataComboBox.Items.Remove("Dividend Amount");
                    desiredDataComboBox.Items.Remove("Split Coefficient");
                    break;

            }
        }

        //disable the time series interval combobox because the interval only applies to intraday, and is non existant in other time series json strings
        private void timeSeriesComboBox_SelectedIndexChanged(object sender, EventArgs e)//combobox that lists the time series
        {
            selectedTimeSeries = timeSeriesComboBox.SelectedItem.ToString();
            switch (selectedTimeSeries)
            {
                case "Intraday":
                    selectedTimeSeries = "TIME_SERIES_INTRADAY";
                    timeSeriesIntervalComboBox.Enabled = true;
                    timeSeriesFlag = "Intraday";
                    break;
                case "Daily":
                    selectedTimeSeries = "TIME_SERIES_DAILY";
                    timeSeriesIntervalComboBox.Enabled = false;
                    timeSeriesFlag = "Daily";
                    break;
                case "Daily Adjusted":
                    selectedTimeSeries = "TIME_SERIES_DAILY_ADJUSTED";
                    timeSeriesIntervalComboBox.Enabled = false;
                    timeSeriesFlag = "Daily Adjusted";
                    break;
                case "Weekly":
                    selectedTimeSeries = "TIME_SERIES_WEEKLY";
                    timeSeriesIntervalComboBox.Enabled = false;
                    timeSeriesFlag = "Weekly";
                    break;
                case "Weekly Adjusted":
                    selectedTimeSeries = "TIME_SERIES_WEEKLY_ADJUSTED";
                    timeSeriesIntervalComboBox.Enabled = false;
                    timeSeriesFlag = "Weekly Adjusted";
                    break;
                case "Monthly":
                    selectedTimeSeries = "TIME_SERIES_MONTHLY";
                    timeSeriesIntervalComboBox.Enabled = false;
                    timeSeriesFlag = "Monthly";
                    break;
                case "Monthly Adjusted":
                    selectedTimeSeries = "TIME_SERIES_MONTHLY_ADJUSTED";
                    timeSeriesIntervalComboBox.Enabled = false;
                    timeSeriesFlag = "Monthly Adjusted";
                    break;
            }
            updateDataComboBox(selectedTimeSeries);
            //not sure if this is needed below, will cause user to lose selected company and crash
            /*if (selectedCompany != null)
                checkForMultipleSymbols();*/
        }

        private void timeSeriesIntervalComboBox_SelectedIndexChanged(object sender, EventArgs e)//combobox that lists the time series intervals
        {
            selectedTimeSeriesInterval = timeSeriesIntervalComboBox.SelectedItem.ToString();
            switch (selectedTimeSeriesInterval)
            {
                case "1 Minute":
                    selectedTimeSeriesInterval = "1min";
                    break;
                case "5 Minutes":
                    selectedTimeSeriesInterval = "5min";
                    break;
                case "15 Minutes":
                    selectedTimeSeriesInterval = "15min";
                    break;
                case "30 Minutes":
                    selectedTimeSeriesInterval = "30min";
                    break;
                case "60 Minutes":
                    selectedTimeSeriesInterval = "60min";
                    break;
            }
            /*if (selectedCompany != null)
                checkForMultipleSymbols();*/
        }

        //back bone for retrieving and loading data to the chart
        private string symbolJSONreturn = "";
        private async void updateChartBtn_Click(object sender, EventArgs e)//for loading a single company to the chart
        {
            if ((!checkBox1.Checked && checkBox2.Checked) || (checkBox1.Checked && !checkBox2.Checked))//if one checkbox is checked, notify user
            {
                MessageBox.Show("Both check boxes must be checked to compare companies");
                return;
            }

            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();
            cartesianChart1.Visible = true;

            if (checkBox1.Checked && checkBox2.Checked)//both are checked, user wants to compare the two stocks
                compareStocks();
            else//no comparing, just load the single stock on the graph
            {
                symbolJSONreturn = await retrieveSymbolData(selectedTimeSeries, mapNameToSymbol[selectedCompany][0], selectedTimeSeriesInterval);
                ExceededReq notify = new ExceededReq();
                notify = JsonConvert.DeserializeObject<ExceededReq>(symbolJSONreturn);
                if (notify != null && notify.Note != null || notify.Note == "")//for when the api notifies user must wait to make more request due to free api restrictions (bug, crashing here sometimes)
                {
                    MessageBox.Show(notify.Note.ToString());
                    return;
                }
                else
                {
                    loadSingleCompanyToChart();
                }
            }
        }

        private void loadSingleCompanyToChart()
        {
            switch (timeSeriesFlag)
            {
                case "Intraday":
                    RootIntraday intraday = new RootIntraday();//contains the 1,5,15,30,60 min objs
                    intraday = JsonConvert.DeserializeObject<RootIntraday>(symbolJSONreturn);
                    loadIntradayToChart(intraday);
                    break;
                case "Daily":
                    RootDaily daily = new RootDaily();//contains the daily & daily adj objs
                    daily = JsonConvert.DeserializeObject<RootDaily>(symbolJSONreturn);
                    loadDailyToChart(daily);
                    break;
                case "Daily Adjusted":
                    RootDailyAdj dailyAdj = new RootDailyAdj();//contains the daily & daily adj objs
                    dailyAdj = JsonConvert.DeserializeObject<RootDailyAdj>(symbolJSONreturn);
                    loadDailyToChart(dailyAdj);
                    break;
                case "Weekly":
                    RootWeekly weekly = new RootWeekly();//contains the weekly & weekly adj objs
                    weekly = JsonConvert.DeserializeObject<RootWeekly>(symbolJSONreturn);
                    loadWeeklyToChart(weekly);
                    break;
                case "Weekly Adjusted":
                    RootWeeklyAdj weeklyAdj = new RootWeeklyAdj();//contains the weekly & weekly adj objs
                    weeklyAdj = JsonConvert.DeserializeObject<RootWeeklyAdj>(symbolJSONreturn);
                    loadWeeklyToChart(weeklyAdj);
                    break;
                case "Monthly":
                    RootMonthly monthly = new RootMonthly();//contains the monthly & monthly adj objs
                    monthly = JsonConvert.DeserializeObject<RootMonthly>(symbolJSONreturn);
                    loadMonthlyToChart(monthly);
                    break;
                case "Monthly Adjusted":
                    RootMonthlyAdj monthlyAdj = new RootMonthlyAdj();//contains the monthly & monthly adj objs
                    monthlyAdj = JsonConvert.DeserializeObject<RootMonthlyAdj>(symbolJSONreturn);
                    loadMonthlyToChart(monthlyAdj);
                    break;
            }
        }

        private Boolean isComparing = false;
        public Dictionary<int, CompareStocksObj> testingComp = new Dictionary<int, CompareStocksObj>();//key is symbol 
        private async void compareStocks()//for comparing stocks
        {
            isComparing = true;
            int numOfStocksToCompare = 0;
            if (checkBox1.Checked)
                numOfStocksToCompare++;
            if (checkBox2.Checked)
                numOfStocksToCompare++;

            string[] stockTest = new string[numOfStocksToCompare];

            for (int i = 0; i < numOfStocksToCompare; i++)
            {
                if (i == 0)
                    stockTest[i] = checkBox1.Text;
                if (i == 1)
                    stockTest[i] = checkBox2.Text;
            }

            for (int k = 0; k < stockTest.Length; k++)
            {
                //MessageBox.Show(stockTest[k] + ": " + mapNameToSymbol[stockTest[k]][0]);
                symbolJSONreturn = await retrieveSymbolData(selectedTimeSeries, mapNameToSymbol[stockTest[k]][0], selectedTimeSeriesInterval);

                ExceededReq notify = new ExceededReq();
                notify = JsonConvert.DeserializeObject<ExceededReq>(symbolJSONreturn);
                if (notify.Note != null)//temp for testing. Notifies user that API limit reached
                {
                    MessageBox.Show(notify.Note.ToString());
                    return;
                }
                else
                {
                    switch (timeSeriesFlag)
                    {
                        case "Intraday":
                            RootIntraday intraday = new RootIntraday();//contains the 1,5,15,30,60 min objs
                            intraday = JsonConvert.DeserializeObject<RootIntraday>(symbolJSONreturn);
                            loadIntradayToChart(intraday);
                            break;
                        case "Daily":
                            RootDaily daily = new RootDaily();//contains the daily & daily adj objs
                            daily = JsonConvert.DeserializeObject<RootDaily>(symbolJSONreturn);
                            loadDailyToChart(daily);
                            break;
                        case "Daily Adjusted":
                            RootDailyAdj dailyAdj = new RootDailyAdj();//contains the daily & daily adj objs
                            dailyAdj = JsonConvert.DeserializeObject<RootDailyAdj>(symbolJSONreturn);
                            loadDailyToChart(dailyAdj);
                            break;
                        case "Weekly":
                            RootWeekly weekly = new RootWeekly();//contains the weekly & weekly adj objs
                            weekly = JsonConvert.DeserializeObject<RootWeekly>(symbolJSONreturn);
                            loadWeeklyToChart(weekly);
                            break;
                        case "Weekly Adjusted":
                            RootWeeklyAdj weeklyAdj = new RootWeeklyAdj();//contains the weekly & weekly adj objs
                            weeklyAdj = JsonConvert.DeserializeObject<RootWeeklyAdj>(symbolJSONreturn);
                            loadWeeklyToChart(weeklyAdj);
                            break;
                        case "Monthly":
                            RootMonthly monthly = new RootMonthly();//contains the monthly & monthly adj objs
                            monthly = JsonConvert.DeserializeObject<RootMonthly>(symbolJSONreturn);
                            loadMonthlyToChart(monthly);
                            break;
                        case "Monthly Adjusted":
                            RootMonthlyAdj monthlyAdj = new RootMonthlyAdj();//contains the monthly & monthly adj objs
                            monthlyAdj = JsonConvert.DeserializeObject<RootMonthlyAdj>(symbolJSONreturn);
                            loadMonthlyToChart(monthlyAdj);
                            break;
                    }
                }
                symbolJSONreturn = null;
            }
            loadDataToChartComparing();
            comparingIndex = 0;
            isComparing = false;
            testingComp.Clear();
        }

        private void setComparedStocks()
        {
            //testing
            if (!checkBox1.Checked)//not checked, keep changing company name
                checkBox1.Text = selectedCompany;
            else if (!checkBox2.Checked)
            {
                checkBox2.Text = selectedCompany;
                checkBox2.Enabled = true;
            }
        }

        public class CompareStocksObj
        {
            public string symbol { get; set; }
            public double[] values { get; set; }
            public string[] keys { get; set; }
        }

        private static int comparingIndex = 0;
        private void storeComparingStockData(string symbol, double[] values, string[] keys)//testStore(data.metaData.Symbol, values, keys)
        {
            CompareStocksObj comp1 = new CompareStocksObj();
            comp1.symbol = symbol;
            comp1.values = values;
            comp1.keys = keys;
            testingComp.Add(comparingIndex, comp1);
            comparingIndex++;
        }

        private static Boolean isVolume = false;
        //Intraday daily data
        private void loadIntradayToChart(RootIntraday data)
        {
            string[] keys = null;
            double[] values = null;
            int counter = 0;
            if (data.oneMin != null)
            {
                keys = new string[data.oneMin.Keys.Count];//keys are x axis (time/dates)
                values = new double[data.oneMin.Keys.Count];//values are y axis (numerical values of stock)
                keys = data.oneMin.Keys.ToArray();
                foreach (var key in data.oneMin.Keys)
                {
                    if (desiredData == "Open")
                        values[counter] = data.oneMin[key].open;
                    else if (desiredData == "Close")
                        values[counter] = data.oneMin[key].close;
                    else if (desiredData == "High")
                        values[counter] = data.oneMin[key].high;
                    else if (desiredData == "Low")
                        values[counter] = data.oneMin[key].low;
                    else if (desiredData == "Volume")
                    {
                        values[counter] = data.oneMin[key].volume;
                        isVolume = true;
                    }
                    counter++;
                }
            }
            else if (data.fiveMin != null)
            {
                keys = new string[data.fiveMin.Keys.Count];//keys are x axis (time/dates)
                values = new double[data.fiveMin.Keys.Count];//values are y axis (numerical values of stock)
                keys = data.fiveMin.Keys.ToArray();
                foreach (var key in data.fiveMin.Keys)
                {
                    if (desiredData == "Open")
                        values[counter] = data.fiveMin[key].open;
                    else if (desiredData == "Close")
                        values[counter] = data.fiveMin[key].close;
                    else if (desiredData == "High")
                        values[counter] = data.fiveMin[key].high;
                    else if (desiredData == "Low")
                        values[counter] = data.fiveMin[key].low;
                    else if (desiredData == "Volume")
                    {
                        values[counter] = data.fiveMin[key].volume;
                        isVolume = true;
                    }
                    counter++;
                }
            }
            else if (data.fifteenMin != null)
            {
                keys = new string[data.fifteenMin.Keys.Count];//keys are x axis (time/dates)
                values = new double[data.fifteenMin.Keys.Count];//values are y axis (numerical values of stock)
                keys = data.fifteenMin.Keys.ToArray();
                foreach (var key in data.fifteenMin.Keys)
                {
                    if (desiredData == "Open")
                        values[counter] = data.fifteenMin[key].open;
                    else if (desiredData == "Close")
                        values[counter] = data.fifteenMin[key].close;
                    else if (desiredData == "High")
                        values[counter] = data.fifteenMin[key].high;
                    else if (desiredData == "Low")
                        values[counter] = data.fifteenMin[key].low;
                    else if (desiredData == "Volume")
                    {
                        values[counter] = data.fifteenMin[key].volume;
                        isVolume = true;
                    }
                    counter++;
                }
            }
            else if (data.thirtyMin != null)
            {
                keys = new string[data.thirtyMin.Keys.Count];//keys are x axis (time/dates)
                values = new double[data.thirtyMin.Keys.Count];//values are y axis (numerical values of stock)
                keys = data.thirtyMin.Keys.ToArray();
                foreach (var key in data.thirtyMin.Keys)
                {
                    if (desiredData == "Open")
                        values[counter] = data.thirtyMin[key].open;
                    else if (desiredData == "Close")
                        values[counter] = data.thirtyMin[key].close;
                    else if (desiredData == "High")
                        values[counter] = data.thirtyMin[key].high;
                    else if (desiredData == "Low")
                        values[counter] = data.thirtyMin[key].low;
                    else if (desiredData == "Volume")
                    {
                        values[counter] = data.thirtyMin[key].volume;
                        isVolume = true;
                    }
                    counter++;
                }
            }
            else if (data.sixtyMin != null)
            {
                keys = new string[data.sixtyMin.Keys.Count];//keys are x axis (time/dates)
                values = new double[data.sixtyMin.Keys.Count];//values are y axis (numerical values of stock)
                keys = data.sixtyMin.Keys.ToArray();
                foreach (var key in data.sixtyMin.Keys)
                {
                    if (desiredData == "Open")
                        values[counter] = data.sixtyMin[key].open;
                    else if (desiredData == "Close")
                        values[counter] = data.sixtyMin[key].close;
                    else if (desiredData == "High")
                        values[counter] = data.sixtyMin[key].high;
                    else if (desiredData == "Low")
                        values[counter] = data.sixtyMin[key].low;
                    else if (desiredData == "Volume")
                    {
                        values[counter] = data.sixtyMin[key].volume;
                        isVolume = true;
                    }
                    counter++;
                }
            }
            else
                Console.WriteLine("Error, all are null. Testing...");

            if (!isComparing)
                loadDataToChart(data.metaData.Symbol, values, keys);
            else
                storeComparingStockData(data.metaData.Symbol, values, keys);
        }

        //normal daily data
        private void loadDailyToChart(RootDaily data)
        {
            string[] keys = data.data.Keys.ToArray();
            double[] values = new double[data.data.Keys.Count];
            int counter = 0;
            if (data.data != null)
            {
                foreach (var key in data.data.Keys)
                {
                    if (desiredData == "Open")
                        values[counter] = data.data[key].open;
                    else if (desiredData == "Close")
                        values[counter] = data.data[key].close;
                    else if (desiredData == "High")
                        values[counter] = data.data[key].high;
                    else if (desiredData == "Low")
                        values[counter] = data.data[key].low;
                    else if (desiredData == "Volume")
                    {
                        values[counter] = data.data[key].volume;
                        isVolume = true;
                    }
                    counter++;
                }
            }
            else
                Console.WriteLine("Error, all are null. Testing...");

            if (!isComparing)
                loadDataToChart(data.metaData.Symbol, values, keys);
            else
                storeComparingStockData(data.metaData.Symbol, values, keys);
        }
        //adjusted daily data
        private void loadDailyToChart(RootDailyAdj data)
        {
            string[] keys = data.data.Keys.ToArray();
            double[] values = new double[data.data.Keys.Count];
            int counter = 0;
            if (data.data != null)
            {
                foreach (var key in data.data.Keys)
                {
                    if (desiredData == "Open")
                        values[counter] = data.data[key].open;
                    else if (desiredData == "Close")
                        values[counter] = data.data[key].close;
                    else if (desiredData == "High")
                        values[counter] = data.data[key].high;
                    else if (desiredData == "Low")
                        values[counter] = data.data[key].low;
                    else if (desiredData == "Volume")
                    {
                        values[counter] = data.data[key].volume;
                        isVolume = true;
                    }
                    else if (desiredData == "Adjusted Close")
                        values[counter] = data.data[key].volume;
                    else if (desiredData == "Dividend Amount")
                        values[counter] = data.data[key].volume;
                    else if (desiredData == "Split Coefficient")
                        values[counter] = data.data[key].volume;
                    counter++;
                }
            }
            else
                Console.WriteLine("Error, all are null. Testing...");

            if (!isComparing)
                loadDataToChart(data.metaData.Symbol, values, keys);
            else
                storeComparingStockData(data.metaData.Symbol, values, keys);
        }
        //normal weekly data
        private void loadWeeklyToChart(RootWeekly data)
        {
            string[] keys = data.data.Keys.ToArray();
            double[] values = new double[data.data.Keys.Count];
            int counter = 0;
            if (data.data != null)
            {
                foreach (var key in data.data.Keys)
                {
                    if (desiredData == "Open")
                        values[counter] = data.data[key].open;
                    else if (desiredData == "Close")
                        values[counter] = data.data[key].close;
                    else if (desiredData == "High")
                        values[counter] = data.data[key].high;
                    else if (desiredData == "Low")
                        values[counter] = data.data[key].low;
                    else if (desiredData == "Volume")
                    {
                        values[counter] = data.data[key].volume;
                        isVolume = true;
                    }
                    counter++;
                }
            }
            else
                Console.WriteLine("Error, all are null. Testing...");

            if (!isComparing)
                loadDataToChart(data.metaData.Symbol, values, keys);
            else
                storeComparingStockData(data.metaData.Symbol, values, keys);
        }
        //adjusted weekly data
        private void loadWeeklyToChart(RootWeeklyAdj data)
        {
            string[] keys = data.data.Keys.ToArray();
            double[] values = new double[data.data.Keys.Count];
            int counter = 0;
            if (data.data != null)
            {
                foreach (var key in data.data.Keys)
                {
                    if (desiredData == "Open")
                        values[counter] = data.data[key].open;
                    else if (desiredData == "Close")
                        values[counter] = data.data[key].close;
                    else if (desiredData == "High")
                        values[counter] = data.data[key].high;
                    else if (desiredData == "Low")
                        values[counter] = data.data[key].low;
                    else if (desiredData == "Volume")
                    {
                        values[counter] = data.data[key].volume;
                        isVolume = true;
                    }
                    else if (desiredData == "Adjusted Close")
                        values[counter] = data.data[key].volume;
                    else if (desiredData == "Dividend Amount")
                        values[counter] = data.data[key].volume;
                    else if (desiredData == "Split Coefficient")
                        values[counter] = data.data[key].volume;
                    counter++;
                }
            }
            else
                Console.WriteLine("Error, all are null. Testing...");

            if (!isComparing)
                loadDataToChart(data.metaData.Symbol, values, keys);
            else
                storeComparingStockData(data.metaData.Symbol, values, keys);
        }
        //normal monthly data
        private void loadMonthlyToChart(RootMonthly data)
        {
            string[] keys = data.data.Keys.ToArray();
            double[] values = new double[data.data.Keys.Count];
            int counter = 0;
            if (data.data != null)
            {
                foreach (var key in data.data.Keys)
                {
                    if (desiredData == "Open")
                        values[counter] = data.data[key].open;
                    else if (desiredData == "Close")
                        values[counter] = data.data[key].close;
                    else if (desiredData == "High")
                        values[counter] = data.data[key].high;
                    else if (desiredData == "Low")
                        values[counter] = data.data[key].low;
                    else if (desiredData == "Volume")
                    {
                        values[counter] = data.data[key].volume;
                        isVolume = true;
                    }
                    counter++;
                }
            }
            else
                Console.WriteLine("Error, all are null. Testing...");

            if (!isComparing)
                loadDataToChart(data.metaData.Symbol, values, keys);
            else
                storeComparingStockData(data.metaData.Symbol, values, keys);
        }

        private void buttonLocateCompany_Click(object sender, EventArgs e)
        {
            if (mapNameToSymbol.ContainsKey(searchTextBox.Text))
            {
                selectedCompany = searchTextBox.Text;
                sectorsComboBox.SelectedItem = stockMarketCompanies[mapNameToSymbol[selectedCompany][0]].Sector;
                companyListingslistBox.SelectedItem = searchTextBox.Text;
                displayAdditionalInfo();
                companyListingslistBox.Focus();
            }
            else
                MessageBox.Show("Unable to locate company!");
        }

        private void SummaryQuoteLbl_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(SummaryQuoteLbl.Text);
        }

        //adjusted monthly data
        private void loadMonthlyToChart(RootMonthlyAdj data)
        {
            string[] keys = data.data.Keys.ToArray();
            double[] values = new double[data.data.Keys.Count];
            int counter = 0;
            if (data.data != null)
            {
                foreach (var key in data.data.Keys)
                {
                    if (desiredData == "Open")
                        values[counter] = data.data[key].open;
                    else if (desiredData == "Close")
                        values[counter] = data.data[key].close;
                    else if (desiredData == "High")
                        values[counter] = data.data[key].high;
                    else if (desiredData == "Low")
                        values[counter] = data.data[key].low;
                    else if (desiredData == "Volume")
                    {
                        values[counter] = data.data[key].volume;
                        isVolume = true;
                    }
                    else if (desiredData == "Adjusted Close")
                        values[counter] = data.data[key].volume;
                    else if (desiredData == "Dividend Amount")
                        values[counter] = data.data[key].volume;
                    else if (desiredData == "Split Coefficient")
                        values[counter] = data.data[key].volume;
                    counter++;
                }
            }
            else
                Console.WriteLine("Error, all are null. Testing...");

            if (!isComparing)
                loadDataToChart(data.metaData.Symbol, values, keys);
            else
                storeComparingStockData(data.metaData.Symbol, values, keys);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if ((checkBox1.Checked && !checkBox2.Checked) || (!checkBox1.Checked && checkBox2.Checked))//used to make sure the user doesnt try to compare the stocks with two different time series
            {
                timeSeriesComboBox.Enabled = false;
                timeSeriesIntervalComboBox.Enabled = false;
            }
            else
                timeSeriesComboBox.Enabled = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if ((checkBox1.Checked && !checkBox2.Checked) || (!checkBox1.Checked && checkBox2.Checked))//used to make sure the user doesnt try to compare the stocks with two different time series
            {
                timeSeriesComboBox.Enabled = false;
                timeSeriesIntervalComboBox.Enabled = false;
            }
            else
                timeSeriesComboBox.Enabled = true;
        }

        private void loadDataToChart(string symbol, double[] values, string[] keys)
        {//the bug is the user must reselect the time series again or else the dates will not show (user must select new items in combobox to fix the bug.)

            cartesianChart1.Series.Add(new LineSeries
            {
                Title = selectedCompany + " (" + symbol + ")",
                Values = new ChartValues<double>(values),
            });

            foreach (var item in keys)
            {
                Console.WriteLine("Keys: " + item.ToString());
            }

            //bottom x axis labels
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Time",
                Labels = keys
            });

            if (isVolume)
            {
                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Volume",
                    LabelFormatter = value => value.ToString("N")
                });
            }
            else
            {//left side y axis labels
                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Values",
                    LabelFormatter = value => value.ToString("C")
                });
            }
            //right side legend
            cartesianChart1.LegendLocation = LegendLocation.Bottom;
            isVolume = false;
            
        }

        private void loadDataToChartComparing()//Axis and data labels are using array indexes instead of dates for some reason (same as above for intraday. Works for daily tho)
        {
            string[] keys1 = testingComp[0].keys;
            string[] keys2 = testingComp[1].keys;
            /*foreach (var item in keys1)
            {
                Console.WriteLine("Keys1: " + item.ToString());
            }
            foreach (var item in keys2)
            {
                Console.WriteLine("Keys2: " + item.ToString());
            }*/

            cartesianChart1.Series.Add(new LineSeries
            {
                Title = stockMarketCompanies[testingComp[0].symbol].Name + " (" + testingComp[0].symbol + ")",
                Values = new ChartValues<double>(testingComp[0].values),
            });

            cartesianChart1.Series.Add(new LineSeries
            {
                Title = stockMarketCompanies[testingComp[1].symbol].Name + " (" + testingComp[1].symbol + ")",
                Values = new ChartValues<double>(testingComp[1].values),
            });
            //I believe the problem is that its too much data (month is less), so its unable to show all the keys
            if (keys1.Length > keys2.Length)//works with months but not others
            {
                cartesianChart1.AxisX.Add(new Axis
                {
                    Labels = keys1,
                });
            }
            else
            {
                cartesianChart1.AxisX.Add(new Axis
                {
                    Labels = keys2,
                });
            }
            //bottom x axis labels
            /*cartesianChart1.AxisX.Add(new Axis
            {
                Title = stockMarketCompanies[testingComp[0].symbol].Name + " Axis",
                //Labels = keys1,
            });

            cartesianChart1.AxisX.Add(new Axis
            {
                Title = stockMarketCompanies[testingComp[1].symbol].Name + " Axis",
                //Labels = keys2,
            });*/

            /*if (isVolume)
            {
                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = "Volume",
                    LabelFormatter = values => values.ToString("C")
                });
            }
            else
            {//left side y axis labels
                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = stockMarketCompanies[testingComp[0].symbol].Name + " Values",
                    LabelFormatter = values => values.ToString("C")
                });

                cartesianChart1.AxisY.Add(new Axis
                {
                    Title = stockMarketCompanies[testingComp[1].symbol].Name + " Values",
                    LabelFormatter = values => values.ToString("C"),
                });
            }*/
            //right side legend
            cartesianChart1.LegendLocation = LegendLocation.Bottom;
            isVolume = false;
        }

        private void addTrackCompanyButton_Click(object sender, EventArgs e)
        {
            List<string> linesList = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\trackedStocks.txt").ToList();
            if (!linesList.Contains(selectedCompany))
            {
                trackedCompaniesComboBox.Items.Add(selectedCompany);
                using (var tw = new StreamWriter(Directory.GetCurrentDirectory() + "\\trackedStocks.txt", true))
                {
                    tw.WriteLine(selectedCompany);
                }
                MessageBox.Show(selectedCompany + " added");
            }
            else
                MessageBox.Show(selectedCompany + " is already being tracked");
        }

        private void readTrackedStocks()//read the stocks from the file that the user is tracking
        {
            string filePath = Directory.GetCurrentDirectory() + "\\trackedStocks.txt";
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
                File.SetAttributes(filePath, FileAttributes.Hidden);
            }
            else
            {
                List<string> linesList = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\trackedStocks.txt").ToList();
                foreach (string t in linesList)
                    trackedCompaniesComboBox.Items.Add(t);
            }
        }

        private void removeTrackCompanyButton_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory() + "\\trackedStocks.txt";
            File.SetAttributes(path, FileAttributes.Normal);
            string companyToRemove = trackedCompaniesComboBox.Text;
            List<string> linesList = File.ReadAllLines(path).ToList();
            linesList.Remove(companyToRemove);//remove it from list, to later rewrite the new list to txt file updated without the removed item
            trackedCompaniesComboBox.Items.Remove(companyToRemove);//remove the selected company
            File.WriteAllText(path, String.Empty);
            using (var tw = new StreamWriter(File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite)))
            {
                foreach (string t in linesList)//rewrite new list to txt file and overwrite the old one
                    tw.WriteLine(t);
            }
            File.SetAttributes(path, FileAttributes.Hidden);
            MessageBox.Show(companyToRemove + " removed");
        }

        private void fakeStockTrainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            NewUserForm frm1 = new NewUserForm();
            try
            {
                frm1.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Unable to perform this action");
            }
            this.Show();
        }

        private void trackedCompaniesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = trackedCompaniesComboBox.GetItemText(trackedCompaniesComboBox.SelectedItem);
            if (mapNameToSymbol.ContainsKey(selected))
            {
                selectedCompany = selected;
                sectorsComboBox.SelectedItem = stockMarketCompanies[mapNameToSymbol[selectedCompany][0]].Sector;
                companyListingslistBox.SelectedItem = selected;
                displayAdditionalInfo();
                companyListingslistBox.Focus();
            }
            else
                MessageBox.Show("Unable to locate company!");
        }

        private void downloadUpdatedCompaniesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            downloadRequiredFiles();
        }

        private async void downloadRequiredFiles()
        {
            var http = new HttpClient();
            http.DefaultRequestHeaders.Add("Host", "www.nasdaq.com");
            http.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            http.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");

            //NASDAQ csv file download
            var response = await http.GetAsync("https://www.nasdaq.com/screening/companies-by-name.aspx?letter=0&exchange=nasdaq&render=download");// by calling .Result you are synchronously reading the result
            var bytes = await response.Content.ReadAsByteArrayAsync();
            using (var fs = File.Open("NASDAQcompanylist.csv", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                await fs.WriteAsync(bytes, 0, bytes.Length);
            }

            //NYSE csv file download
            response = await http.GetAsync("https://www.nasdaq.com/screening/companies-by-name.aspx?letter=0&exchange=nyse&render=download");
            bytes = await response.Content.ReadAsByteArrayAsync();
            using (var fs = File.Open("NYSEcompanylist.csv", FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                await fs.WriteAsync(bytes, 0, bytes.Length);
            }
            //could program it to reload the new data, but is easier just to close and reopen the program
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Close and reopen the program for changes to take place\nClose now?", "Files Downloaded", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);

            if (result == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }
    }
}
/*
 * TODO:
 * add ability to compare stocks on chart (needs fixed)
 * add daily high & low and 52 weeks high & low (not sure where to get this data from, may scratch idea)
 * */

//super weird bug having to do with searching a company then trying to display chart without focusing on another object on form. Doesn't seem to occur always.
//there is a bug where if while trying to compare stocks and an api limit is reached, it will lose the second companying being compared and will compare the first company to itself. Can be fixed by unchecking and rechecking company box two