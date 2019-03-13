using CsvHelper;
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Linq;
using System.Net;

//API Key: X0REJIV6R6ROZS3T
//News API Key: 94b9e25568ca4ee3bef44fc4c7ae335e

//Need to figure out how to dl new csv file after each day (problem: cant dl from url for some reason, some type of protection maybe. Creates file with 0 bytes)
//csv file download links: https://www.nasdaq.com/screening/company-list.aspx

namespace Senior_Project_Stock_Tracker
{
    public partial class StockTrackMain : DataRetriever
    {
        public StockTrackMain()
        {
            InitializeComponent();
            loadNASDAQCompanies();
            loadNYSECompanies();
            timeSeriesComboBox.SelectedItem = "Intraday";
            timeSeriesIntervalComboBox.SelectedItem = "1 Minute";
            desiredDataComboBox.SelectedItem = "Open";
        }

        private List<string> marketSectors = new List<string>();

        public class companyInfo
        {
            public string Symbol { get; set; }
            public string Name { get; set; }
            public string IPOyear { get; set; }
            public string Sector { get; set; }
            public string industry { get; set; }
        }

        //NASDAQ
        private Dictionary<string, companyInfo> NASDAQ_CompanyData = new Dictionary<string, companyInfo>();//key is symbol
        private Dictionary<string, List<string>> mapSymbolToSector = new Dictionary<string, List<string>>();//contains array of sectors, each key is a sector and values are the company names in the sector
        private Dictionary<string, List<string>> mapNameToSymbol = new Dictionary<string, List<string>>();//key is company names, and value is company's symbol (may have more than one symbol)
        private void loadNASDAQCompanies()//load the NASDAQ companies from the csv file
        {
            IEnumerable<companyInfo> records;
            using (var reader = new StreamReader("NASDAQcompanylist.csv"))
            using (var csv = new CsvReader(reader))
            {
                records = csv.GetRecords<companyInfo>();//contains each row of data from csv

                foreach (companyInfo record in records)
                {
                    //store list of sectors for nasdaq without duplicates in arraylist
                    if (!marketSectors.Contains(record.Sector))
                        marketSectors.Add(record.Sector);

                    //create the objects for each Company (row) and store them in the NASDAQ_DATA dictionary as values, using their symbol's as the keys
                    //symbol, object containing data
                    //NASDAQ_Data.Add(record.Symbol, new NASDAQ_CSV_Data(/*record.Symbol, record.Name, record.IPOyear, record.Sector, record.industry*/));
                    NASDAQ_CompanyData.Add(record.Symbol, record);


                    //stuff for mapping companies to symbols (solves problem for when a company has multiple symbols)
                    if (mapNameToSymbol.ContainsKey(record.Name))//contains the company name (key), so add symbol to list
                    {
                        List<string> list = mapNameToSymbol[record.Name];//find sector and add the company to the list according to appropriate sector
                        list.Add(record.Symbol);
                    }
                    else//add the new company name and a new list to go with it storing symbols (only hits here the first time a new company is found)
                    {
                        var list = new List<string>() { record.Symbol };
                        mapNameToSymbol.Add(record.Name, list);
                    }

                    //stuff for sectors and loading companies in combobox
                    if (mapSymbolToSector.ContainsKey(record.Sector))//contains the key, so just update the list to the appropriate key
                    {
                        List<string> list = mapSymbolToSector[record.Sector];//find sector and add the company to the list according to appropriate sector
                        list.Add(record.Name);
                    }
                    else//add the new sector and a new list to go with the sector (only hits here the first time a new sector is found)
                    {
                        //stuff for adding sectors and list the companies in the combobox
                        var list = new List<string>() { record.Name };
                        mapSymbolToSector.Add(record.Sector, list);
                    }
                }
            }

            //load sectors into the combobox (NYSE and NASDAQ have the same sectors)
            foreach (string sector in marketSectors)
                sectorsComboBox.Items.Add(sector);
        }

        //add, fix and update. Change code below eventually...
        //NYSE
        //symbol, name
        private Dictionary<string, string> NYSE_Name = new Dictionary<string, string>();
        //symbol, ipoyear
        private Dictionary<string, string> NYSE_IPOyear = new Dictionary<string, string>();
        //symbol, sector
        private Dictionary<string, string> NYSE_Sector = new Dictionary<string, string>();
        //symbolr, industry
        private Dictionary<string, string> NYSE_Industry = new Dictionary<string, string>();
        private void loadNYSECompanies()//load the NYSE companies from the csv file
        {
            IEnumerable<NYSE_CSV_Data> records;
            using (var reader = new StreamReader("NYSEcompanylist.csv"))
            using (var csv = new CsvReader(reader))
            {
                records = csv.GetRecords<NYSE_CSV_Data>();

                foreach (NYSE_CSV_Data record in records)
                {
                    NYSE_Name.Add(record.Symbol, record.Name);
                    NYSE_IPOyear.Add(record.Symbol, record.IPOyear);
                    NYSE_Sector.Add(record.Symbol, record.Sector);
                    NYSE_Industry.Add(record.Symbol, record.industry);
                }
            }
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

        private async void processSelectedCompany(string selectedCompanyName)//takes in the company the user selected from the companylistbox
        {
            if (mapNameToSymbol[selectedCompanyName].Count > 1)//selected company has more than one symbol
            {
                companyListingslistBox.Items.Clear();//clear the companies listbox to load the symbols
                foreach (var listMember in mapNameToSymbol[selectedCompanyName])//mapped symbol to sector because symbols are unique values and some companies have multiple symbols in different sectors
                {
                    companyListingslistBox.Items.Add(listMember);//load symbols into the listbox
                    flag = false;//set flag to false to 
                }
            }
            else//one symbol, search mapNameToSymbol to find symbol for company name
                //richTextBox1.Text = await retrieveSymbolData(selectedTimeSeries, mapNameToSymbol[selectedCompanyName][0], selectedTimeSeriesInterval);
                symbolJSONreturn = await retrieveSymbolData(selectedTimeSeries, mapNameToSymbol[selectedCompanyName][0], selectedTimeSeriesInterval);
            test();
        }

        private static Boolean flag = true;
        private void companyListingslistBox_SelectedIndexChanged(object sender, EventArgs e)//load the companies into the listbox
        {
            if (companyListingslistBox.SelectedIndex == -1)//no item selected
                updateChartBtn.Enabled = false;
            else
                updateChartBtn.Enabled = true;
            checkForMultipleSymbols();
        }

        private static string selectedCompany;
        private async void checkForMultipleSymbols()
        {
            selectedCompany = companyListingslistBox.GetItemText(companyListingslistBox.SelectedItem);
            richTextBox1.Clear();
            if (flag)//load the companies names into listbox
            {
                processSelectedCompany(selectedCompany);
            }
            else//load the symbols into the listbox for user to select one
            {
                symbolJSONreturn = await retrieveSymbolData(selectedTimeSeries, selectedCompany, selectedTimeSeriesInterval);
                flag = true;
                loadCompaniesIntoListBox();//load companies back into the listbox after user selects desired symbol
                test(selectedCompany);
            }
        }

        private void test(string symbol = null)
        {
            if (symbol == null)//one company symbol route
            {
                IndustryLbl.Text = NASDAQ_CompanyData[mapNameToSymbol[selectedCompany][0]].industry;
                symbolLbl.Text = NASDAQ_CompanyData[mapNameToSymbol[selectedCompany][0]].Symbol;
                IPOyearLbl.Text = NASDAQ_CompanyData[mapNameToSymbol[selectedCompany][0]].IPOyear;
            }
            else//two symbol route
            {
                IndustryLbl.Text = NASDAQ_CompanyData[symbol].industry;
                symbolLbl.Text = NASDAQ_CompanyData[symbol].Symbol;
                IPOyearLbl.Text = NASDAQ_CompanyData[symbol].IPOyear;
                selectedCompany = NASDAQ_CompanyData[symbol].Name;
            }
            groupBox2.Text = selectedCompany + " Stock Info";
            SummaryQuoteLbl.Text = "https://www.nasdaq.com/symbol/" + symbolLbl.Text;
            IndustryLbl.Visible = true;
            symbolLbl.Visible = true;
            IPOyearLbl.Visible = true;
            SummaryQuoteLbl.Visible = true;
        }

        private void loadCompaniesIntoListBox()
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
            if (selectedCompany != null)
                checkForMultipleSymbols();
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
            if (selectedCompany != null)
                checkForMultipleSymbols();
        }

        //back bone for retrieving and loading data to the chart
        private string symbolJSONreturn = "";
        private void updateChartBtn_Click(object sender, EventArgs e)
        {
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();

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
            //Console.WriteLine("Here");
        }

        //Intraday daily data
        private void loadIntradayToChart(RootIntraday data)
        {
            string[] keys = new string[data.oneMin.Keys.Count];
            double[] values = new double[data.oneMin.Keys.Count];
            int counter = 0;
            if (data.oneMin != null)
            {
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
                        values[counter] = data.oneMin[key].volume;
                    counter++;
                }
            }
            else if (data.fiveMin != null)
            {
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
                        values[counter] = data.fiveMin[key].volume;
                    counter++;
                }
            }
            else if (data.fifteenMin != null)
            {
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
                        values[counter] = data.fifteenMin[key].volume;
                    counter++;
                }
            }
            else if (data.thirtyMin != null)
            {
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
                        values[counter] = data.thirtyMin[key].volume;
                    counter++;
                }
            }
            else if (data.sixtyMin != null)
            {
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
                        values[counter] = data.sixtyMin[key].volume;
                    counter++;
                }
            }
            else
                Console.WriteLine("Error, all are null. Testing...");

            loadDataToChart(data.metaData.Symbol, values, keys);
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
                        values[counter] = data.data[key].volume;
                    counter++;
                }
            }
            else
                Console.WriteLine("Error, all are null. Testing...");

            loadDataToChart(data.metaData.Symbol, values, keys);
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
                        values[counter] = data.data[key].volume;
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

            loadDataToChart(data.metaData.Symbol, values, keys);
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
                        values[counter] = data.data[key].volume;
                    counter++;
                }
            }
            else
                Console.WriteLine("Error, all are null. Testing...");

            loadDataToChart(data.metaData.Symbol, values, keys);
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
                        values[counter] = data.data[key].volume;
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

            loadDataToChart(data.metaData.Symbol, values, keys);
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
                        values[counter] = data.data[key].volume;
                    counter++;
                }
            }
            else
                Console.WriteLine("Error, all are null. Testing...");

            loadDataToChart(data.metaData.Symbol, values, keys);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WebClient webClient = new WebClient();
            string url = "https://www.nasdaq.com/screening/companies-by-industry.aspx?exchange=NASDAQ&render=download";
            string path = @"C:/Users/Mike/Downloads/companylist.csv";


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
                        values[counter] = data.data[key].volume;
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

            loadDataToChart(data.metaData.Symbol, values, keys);
        }

        private void loadDataToChart(string symbol, double[] values, string[] keys)
        {
            cartesianChart1.Series = new SeriesCollection
            {
                //lines and their values
                new LineSeries
                {
                    Title = selectedCompany + " (" + symbol + ")",
                    Values = new ChartValues<double> (values)
                }
            };

            //bottom x axis labels
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Time",
                Labels = keys
            });

            //left side y axis labels
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Values",
                LabelFormatter = value => value.ToString("C")
            });

            //right side legend
            cartesianChart1.LegendLocation = LegendLocation.Right;
        }
    }
}
