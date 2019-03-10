using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
        //Use one static HttpClient for the whole app to use
        private static String programPath = Directory.GetCurrentDirectory();
        private static HttpClient httpClient = new HttpClient();

        protected static async Task<string> retrieveSymbolData(string timeSeries, string stockSymbol, string interval)
        {
            String urlStringTest = "https://www.alphavantage.co/query?function=" + timeSeries + "&symbol=" + stockSymbol + "&interval=" + interval + "&apikey=X0REJIV6R6ROZS3T";
            //String urlStringTest = "https://www.alphavantage.co/query?function=TIME_SERIES_INTRADAY&symbol=MSFT&interval=5min&apikey=X0REJIV6R6ROZS3T";
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

            /*public NASDAQ_CSV_Data(string Symbol, string Name, string IPOyear, string Sector, string industry)
            {
                this.Symbol = Symbol;
                this.Name = Name;
                this.IPOyear = IPOyear;
                this.Sector = Sector;
                this.industry = industry;
            }*/
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

        //testing for json to Object --> load into chart
        //fix
        public class RootData
        {
            [JsonProperty(PropertyName = "Meta Data")]
            public MetaData metaData { get; set; }
            //[JsonProperty]
            //public List<Time_Series> data { get; set; }
        }

        public class MetaData
        {
            public string Information { get; set; }
            public string Symbol { get; set; }
            [JsonProperty(PropertyName = "Last Refreshed")]
            public string Last_Refreshed { get; set; }
            public string Interval { get; set; }
            [JsonProperty(PropertyName = "Output Size")]
            public string Output_Size { get; set; }
            [JsonProperty(PropertyName = "Time Zone")]
            public string Time_Zone { get; set; }
        }

        public class Time_Series
        {
            public string open { get; set; }
            public string high { get; set; }
            public string low { get; set; }
            public string close { get; set; }
            public string volume { get; set; }
        }
    }
}
