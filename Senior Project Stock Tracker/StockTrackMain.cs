using CsvHelper;
using LiveCharts;
using LiveCharts.Wpf;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;

//API Key: X0REJIV6R6ROZS3T
//News API Key: 94b9e25568ca4ee3bef44fc4c7ae335e

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
        }

        private List<string> marketSectors = new List<string>();

        //NASDAQ
        private Dictionary<string, NASDAQ_CSV_Data> NASDAQ_Data = new Dictionary<string, NASDAQ_CSV_Data>();
        private Dictionary<string, List<string>> mapSymbolToSector = new Dictionary<string, List<string>>();
        private Dictionary<string, List<string>> mapNameToSymbol = new Dictionary<string, List<string>>();
        private void loadNASDAQCompanies()//load the NASDAQ companies from the csv file
        {
            IEnumerable<NASDAQ_CSV_Data> records;
            using (var reader = new StreamReader("NASDAQcompanylist.csv"))
            using (var csv = new CsvReader(reader))
            {
                records = csv.GetRecords<NASDAQ_CSV_Data>();

                foreach (NASDAQ_CSV_Data record in records)
                {
                    //store list of sectors for nasdaq without duplicates in arraylist
                    if (!marketSectors.Contains(record.Sector))
                        marketSectors.Add(record.Sector);

                    //create the objects for each Company (row) and store them in the NASDAQ_DATA dictionary as values, using their symbol's as the keys
                    //symbol, object containing data
                    NASDAQ_Data.Add(record.Symbol, new NASDAQ_CSV_Data(/*record.Symbol, record.Name, record.IPOyear, record.Sector, record.industry*/));

                    //test
                    //problem, storing company names as keys, although some companies have multiple symbols

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

        //private async void button1_Click(object sender, EventArgs e)
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            News updateCoordsWindow = new News(selectedCompany);
            try
            {
                updateCoordsWindow.ShowDialog();
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
        }

        private static Boolean flag = true;
        private void companyListingslistBox_SelectedIndexChanged(object sender, EventArgs e)//load the companies into the listbox
        {
            checkForMultipleSymbols();
        }

        private static string selectedCompany;
        private async void checkForMultipleSymbols()
        {
            selectedCompany = companyListingslistBox.SelectedItem.ToString();
            richTextBox1.Clear();
            if (flag)//load the companies names into listbox
                processSelectedCompany(selectedCompany);
            else//load the symbols into the listbox for user to select one
            {
                symbolJSONreturn = await retrieveSymbolData(selectedTimeSeries, selectedCompany, selectedTimeSeriesInterval);
                flag = true;
                loadCompaniesIntoListBox();//load companies back into the listbox after user selects desired symbol
            }
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

        private string symbolJSONreturn = "";
        private void button2_Click(object sender, EventArgs e)//testing chart stuff
        {
            //richTextBox1.Clear();
            //richTextBox1.Text = symbolJSONreturn;
            cartesianChart1.Series.Clear();
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisY.Clear();
            //testing
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
                case "Weekly Adjusted":
                    RootWeekly weekly = new RootWeekly();//contains the weekly & weekly adj objs
                    weekly = JsonConvert.DeserializeObject<RootWeekly>(symbolJSONreturn);
                    break;
                case "Monthly":
                case "Monthly Adjusted":
                    RootMonthly monthly = new RootMonthly();//contains the monthly & monthly adj objs
                    monthly = JsonConvert.DeserializeObject<RootMonthly>(symbolJSONreturn);
                    break;
            }
            Console.WriteLine("Here");
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
            if(selectedCompany != null)
                checkForMultipleSymbols();
        }

        private void timeSeriesIntervalComboBox_SelectedIndexChanged(object sender, EventArgs e)//combobox that lists the time series intervals
        {
            selectedTimeSeriesInterval = timeSeriesIntervalComboBox.SelectedItem.ToString();
            switch(selectedTimeSeriesInterval)
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

        private void loadIntradayToChart(RootIntraday data)
        {
            double[] values = new double[100];
            int counter = 0;
            if (data.oneMin != null)
            {
                //change from open data. Not sure what data to use here for intraday
                foreach (var key in data.oneMin.Keys)
                {
                    Console.WriteLine("Key: " + key + "\nOpen: " + data.oneMin[key].open);
                    values[counter] = data.oneMin[key].open;
                    counter++;
                }
            }
            else if (data.fiveMin != null)
            {
                foreach (var key in data.fiveMin.Keys)
                {
                    //Console.WriteLine("Key: " + key + "\nOpen: " + data.fiveMin[key].open);
                }
            }
            else if (data.fifteenMin != null)
            {
                foreach (var key in data.fifteenMin.Keys)
                {
                    //Console.WriteLine("Key: " + key + "\nOpen: " + data.fifteenMin[key].open);
                }
            }
            else if (data.thirtyMin != null)
            {
                foreach (var key in data.thirtyMin.Keys)
                {
                    //Console.WriteLine("Key: " + key + "\nOpen: " + data.thirtyMin[key].open);
                }
            }
            else if (data.sixtyMin != null)
            {
                foreach (var key in data.sixtyMin.Keys)
                {
                    //Console.WriteLine("Key: " + key + "\nOpen: " + data.sixtyMin[key].open);
                }
            }
            else
                Console.WriteLine("Error, all are null. Testing...");

            cartesianChart1.Series = new SeriesCollection
            {
                //lines and their values
                new LineSeries
                {
                    Title = selectedCompany + " (" + data.metaData.Symbol + ")",
                    Values = new ChartValues<double> (values)
                }
            };

            //bottom x axis labels
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Time",
                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" }
            });

            //left side y axis labels
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Values",
                LabelFormatter = value => value.ToString("C")
            });

            //right side legend
            cartesianChart1.LegendLocation = LegendLocation.Bottom;
        }

        private void loadDailyToChart(RootDaily data)
        {
            double[] values = new double[100];
            int counter = 0;
            if (data.data != null)
            {
                foreach (var key in data.data.Keys)
                {
                    values[counter] = data.data[key].open;
                    counter++;
                }
            }
            else
                Console.WriteLine("Error, all are null. Testing...");

            cartesianChart1.Series = new SeriesCollection
            {
                //lines and their values
                new LineSeries
                {
                    Title = selectedCompany + " (" + data.metaData.Symbol + ")",
                    Values = new ChartValues<double> (values)
                }
            };

            //bottom x axis labels
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Time",
                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" }
            });

            //left side y axis labels
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Values",
                LabelFormatter = value => value.ToString("C")
            });

            //right side legend
            cartesianChart1.LegendLocation = LegendLocation.Bottom;
        }

        private void loadDailyToChart(RootDailyAdj data)
        {
            double[] values = new double[100];
            int counter = 0;
            if (data.data != null)
            {
                foreach (var key in data.data.Keys)
                {
                    values[counter] = data.data[key].open;
                    counter++;
                }
            }
            else
                Console.WriteLine("Error, all are null. Testing...");
            
            //stuff below is for chart. Needs fixed/updated

            cartesianChart1.Series = new SeriesCollection
            {
                //lines and their values
                new LineSeries
                {
                    Title = selectedCompany + " (" + data.metaData.Symbol + ")",
                    Values = new ChartValues<double> (values)
                }
            };

            //bottom x axis labels
            cartesianChart1.AxisX.Add(new Axis
            {
                Title = "Time",
                Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" }
            });

            //left side y axis labels
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "Values",
                LabelFormatter = value => value.ToString("C")
            });

            //right side legend
            cartesianChart1.LegendLocation = LegendLocation.Bottom;
        }
    }
}
