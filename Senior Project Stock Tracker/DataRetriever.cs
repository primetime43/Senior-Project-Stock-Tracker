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
 * Asynchronously - will not freeze UI (same as backgroundworkers but newer)
*/

namespace Senior_Project_Stock_Tracker
{
    public class DataRetriever : Form
    {
        private static String programPath = Directory.GetCurrentDirectory();
        private static HttpClient httpClient = new HttpClient();
        public string timeSeriesFlag;

        protected static async Task<string> retrieveSymbolData(string timeSeries, string stockSymbol, string interval)
        {
            String urlStringTest = "https://www.alphavantage.co/query?function=" + timeSeries + "&symbol=" + stockSymbol + "&interval=" + interval + "&apikey=X0REJIV6R6ROZS3T";
            //String urlStringTest = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=MSFT&interval=5min&apikey=X0REJIV6R6ROZS3T";
            //Console.WriteLine("URL: " + urlStringTest);
            var json = await httpClient.GetAsync(urlStringTest);
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
            public string industry { get; set; }
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
    }
}
