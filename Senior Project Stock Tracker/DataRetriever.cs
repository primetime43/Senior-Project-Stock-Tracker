using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

//API Key: X0REJIV6R6ROZS3T
/*
 * This class is used for retrieving any data that is not already local, meaning retrieving data from the web using web clients
 * Class is going to be used as the top of the hierarchy 
 */

/*Notes
 * Syncronously - will freeze UI
 * Asynchronously - will not freeze UI
*/

namespace Senior_Project_Stock_Tracker
{
    public class DataRetriever : Form
    {
        public List<string> marketSectors = new List<string>();

        public class companyInfo
        {
            public string Symbol { get; set; }
            public string Name { get; set; }
            public string IPOyear { get; set; }
            public string Sector { get; set; }
            public string industry { get; set; }//check (possible bug because idustry's I may sometimes be lowercase)
        }

        //NASDAQ
        public static Dictionary<string, companyInfo> stockMarketCompanies = new Dictionary<string, companyInfo>();//key is symbol
        public static Dictionary<string, List<string>> mapSymbolToSector = new Dictionary<string, List<string>>();//contains array of sectors, each key is a sector and values are the company names in the sector
        public static Dictionary<string, List<string>> mapNameToSymbol = new Dictionary<string, List<string>>();//key is company names, and value is company's symbol (may have more than one symbol)
        public void loadNASDAQCompanies()//load the NASDAQ companies from the csv file
        {
            IEnumerable<companyInfo> records;
            using (var reader = new StreamReader("NASDAQcompanylist.csv"))
            using (var csv = new CsvReader(reader))
            {
                csv.Configuration.PrepareHeaderForMatch = (string header, int index) => header.ToLower();
                records = csv.GetRecords<companyInfo>();//contains each row of data from csv

                foreach (companyInfo record in records)
                {
                    //store list of sectors for nasdaq without duplicates in arraylist
                    if (!marketSectors.Contains(record.Sector))
                        marketSectors.Add(record.Sector);

                    //create the objects for each Company (row) and store them in the NASDAQ_DATA dictionary as values, using their symbol's as the keys
                    //symbol, object containing data
                    //NASDAQ_Data.Add(record.Symbol, new NASDAQ_CSV_Data(/*record.Symbol, record.Name, record.IPOyear, record.Sector, record.industry*/));
                    stockMarketCompanies.Add(record.Symbol, record);


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
        }

        //NYSE companies
        public void loadNYSECompanies()//load the NYSE companies from the csv file
        {
            IEnumerable<companyInfo> records;
            using (var reader = new StreamReader("NYSEcompanylist.csv"))
            using (var csv = new CsvReader(reader))
            {
                records = csv.GetRecords<companyInfo>();//contains each row of data from csv

                foreach (companyInfo record in records)
                {
                    if (!marketSectors.Contains(record.Sector))
                        marketSectors.Add(record.Sector);

                    if (!stockMarketCompanies.ContainsKey(record.Symbol))//check if it doesn't contain key. Some companies are listed on both NASDAQ and NYSE
                        stockMarketCompanies.Add(record.Symbol, record);


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
        }

        private static string programPath = Directory.GetCurrentDirectory();
        private static HttpClient httpClient = new HttpClient();
        public string timeSeriesFlag;

        protected static async Task<string> retrieveSymbolData(string timeSeries, string stockSymbol, string interval)
        {
            string urlString = "https://www.alphavantage.co/query?function=" + timeSeries + "&symbol=" + stockSymbol + "&interval=" + interval + "&apikey=X0REJIV6R6ROZS3T";
            Console.WriteLine(urlString);
            var json = await httpClient.GetAsync(urlString);
            return await json.Content.ReadAsStringAsync();
        }


        //retrieving local data from csv stuff, not web
        protected class NASDAQ_CSV_Data
        {
            //For reading NASDAQ csv file
            public string Symbol { get; set; }
            public string Name { get; set; }
            public string IPOyear { get; set; }
            public string Sector { get; set; }
            public string industry { get; set; }//check (possible bug because idustry's I may sometimes be lowercase)
        }

        protected class NYSE_CSV_Data
        {
            //For reading NYSE csv file
            public string Symbol { get; set; }
            public string Name { get; set; }
            public string IPOyear { get; set; }
            public string Sector { get; set; }
            public string industry { get; set; }
        }

        //Below is parsing the json string data

        //testing for json to Object --> load into chart
        //need to create parsers for each time series and create a throw message for limit reach

        public class RootIntraday
        {
            [JsonProperty("Meta Data", NullValueHandling = NullValueHandling.Ignore)]
            public MetaData metaData { get; set; }
            [JsonProperty("Time Series (1min)")]
            public Dictionary<string, Time_Series_Normal> oneMin { get; set; }//key is date, then object contains the data
            [JsonProperty("Time Series (5min)")]
            public Dictionary<string, Time_Series_Normal> fiveMin { get; set; }
            [JsonProperty("Time Series (15min)")]
            public Dictionary<string, Time_Series_Normal> fifteenMin { get; set; }
            [JsonProperty("Time Series (30min)")]
            public Dictionary<string, Time_Series_Normal> thirtyMin { get; set; }
            [JsonProperty("Time Series (60min)")]
            public Dictionary<string, Time_Series_Normal> sixtyMin { get; set; }
        }

        public class RootDaily
        {
            [JsonProperty("Meta Data", NullValueHandling = NullValueHandling.Ignore)]
            public MetaData metaData { get; set; }
            [JsonProperty("Time Series (Daily)")]
            public Dictionary<string, Time_Series_Normal> data { get; set; }
        }

        public class RootDailyAdj
        {
            [JsonProperty("Meta Data", NullValueHandling = NullValueHandling.Ignore)]
            public MetaData metaData { get; set; }
            [JsonProperty("Time Series (Daily)")]
            public Dictionary<string, Time_Series_Adjusted> data { get; set; }
        }

        public class RootWeekly
        {
            [JsonProperty("Meta Data", NullValueHandling = NullValueHandling.Ignore)]
            public MetaData metaData { get; set; }
            [JsonProperty("Weekly Time Series")]
            public Dictionary<string, Time_Series_Normal> data { get; set; }
        }

        public class RootWeeklyAdj
        {
            [JsonProperty("Meta Data", NullValueHandling = NullValueHandling.Ignore)]
            public MetaData metaData { get; set; }
            [JsonProperty("Weekly Adjusted Time Series")]
            public Dictionary<string, Time_Series_Adjusted> data { get; set; }
        }

        public class RootMonthly
        {
            [JsonProperty("Meta Data", NullValueHandling = NullValueHandling.Ignore)]
            public MetaData metaData { get; set; }
            [JsonProperty("Monthly Time Series")]
            public Dictionary<string, Time_Series_Normal> data { get; set; }
        }

        public class RootMonthlyAdj
        {
            [JsonProperty("Meta Data", NullValueHandling = NullValueHandling.Ignore)]
            public MetaData metaData { get; set; }
            [JsonProperty("Monthly Adjusted Time Series")]
            public Dictionary<string, Time_Series_Adjusted> data { get; set; }
        }

        public class MetaData//numbering is different for some, but should be fine
        {
            [JsonProperty("1. Information", NullValueHandling = NullValueHandling.Ignore)]
            public string Information { get; set; }
            [JsonProperty("2. Symbol", NullValueHandling = NullValueHandling.Ignore)]
            public string Symbol { get; set; }
            [JsonProperty("3. Last Refreshed", NullValueHandling = NullValueHandling.Ignore)]
            public string Last_Refreshed { get; set; }
            [JsonProperty("4. Interval", NullValueHandling = NullValueHandling.Ignore)]
            public string Interval { get; set; }
            [JsonProperty("5. Output Size", NullValueHandling = NullValueHandling.Ignore)]
            public string Output_Size { get; set; }
            [JsonProperty("6. Time Zone", NullValueHandling = NullValueHandling.Ignore)]
            public string Time_Zone { get; set; }
        }

        public class Time_Series_Normal//intraday time series, daily, weekly, monthly
        {
            [JsonProperty("1. open", NullValueHandling = NullValueHandling.Ignore)]
            public double open { get; set; }
            [JsonProperty("2. high", NullValueHandling = NullValueHandling.Ignore)]
            public double high { get; set; }
            [JsonProperty("3. low", NullValueHandling = NullValueHandling.Ignore)]
            public double low { get; set; }
            [JsonProperty("4. close", NullValueHandling = NullValueHandling.Ignore)]
            public double close { get; set; }
            [JsonProperty("5. volume", NullValueHandling = NullValueHandling.Ignore)]
            public double volume { get; set; }
        }

        public class Time_Series_Adjusted//daily adjusted, weekly adjusted, monthly adjusted
        {
            [JsonProperty("1. open", NullValueHandling = NullValueHandling.Ignore)]
            public double open { get; set; }
            [JsonProperty("2. high", NullValueHandling = NullValueHandling.Ignore)]
            public double high { get; set; }
            [JsonProperty("3. low", NullValueHandling = NullValueHandling.Ignore)]
            public double low { get; set; }
            [JsonProperty("4. close", NullValueHandling = NullValueHandling.Ignore)]
            public double close { get; set; }
            [JsonProperty("5. adjusted close", NullValueHandling = NullValueHandling.Ignore)]
            public double adjusted_close { get; set; }
            [JsonProperty("6. volume", NullValueHandling = NullValueHandling.Ignore)]
            public double volume { get; set; }
            [JsonProperty("7. dividend amount", NullValueHandling = NullValueHandling.Ignore)]
            public double dividend_amount { get; set; }
            [JsonProperty("8. split coefficient", NullValueHandling = NullValueHandling.Ignore)]
            public double split_coefficient { get; set; }
        }

        //For when the json data is null because of 5 request per min is exceeded
        public class ExceededReq
        {
            public string Note { get; set; }
        }
    }
}
