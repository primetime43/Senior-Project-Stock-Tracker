﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Windows.Forms;

//API key: 94b9e25568ca4ee3bef44fc4c7ae335e

namespace Senior_Project_Stock_Tracker
{
    public partial class News : Form
    {
        private static HttpClient httpClient = new HttpClient();
        private string selectedCompany = "";
        public News(string company)
        {
            selectedCompany = company;
            InitializeComponent();
            if (selectedCompany != null)
                PopulateListView();
            else
            {//temp for testing, remove if/add alt later
                selectedCompany = "NASDAQ";
                PopulateListView();
            }
            PopulateListView();
        }

        RootObject test1 = null;
        private async void PopulateListView()
        {
            //var json = await httpClient.GetAsync("https://newsapi.org/v2/top-headlines?country=us&apiKey=94b9e25568ca4ee3bef44fc4c7ae335e");
            var json = await httpClient.GetAsync("https://newsapi.org/v2/everything?q=" + selectedCompany + "&language=en&sortBy=popularity&apiKey=94b9e25568ca4ee3bef44fc4c7ae335e");
            var jsonAsString = await json.Content.ReadAsStringAsync();

            test1 = JsonConvert.DeserializeObject<RootObject>(jsonAsString);
            ImageList images = new ImageList();
            int articleIndex = test1.articles.Count;
            for (int i = 0; i < articleIndex; i++)
            {
                if (test1.articles[i].url != null)
                {
                    listBox1.Items.Add(test1.articles[i].title);
                    if (test1.articles[i].urlToImage != null)
                    {
                        images.Images.Add(LoadImage(test1.articles[i].urlToImage));
                        images.ImageSize = new Size(220, 165);
                    }
                }
            }
        }

        private Image LoadImage(string url)
        {
            System.Net.WebRequest request = System.Net.WebRequest.Create(url);
            System.Net.WebResponse response = request.GetResponse();
            System.IO.Stream responseStream = response.GetResponseStream();
            Bitmap bmp = new Bitmap(responseStream);
            responseStream.Dispose();
            return bmp;
        }

        public class RootObject
        {
            public string status { get; set; }
            public int totalResults { get; set; }
            public List<Article> articles { get; set; }
        }

        public class Source
        {
            public string id { get; set; }
            public string name { get; set; }
        }

        public class Article
        {
            public Source source { get; set; }
            public string author { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string url { get; set; }
            public string urlToImage { get; set; }
            public DateTime publishedAt { get; set; }
            public string content { get; set; }
        }

        private void News_Load_FormClosing(object sender, FormClosedEventArgs e)
        {
            StockTrackMain main = new StockTrackMain();
            main.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < test1.articles.Count; i++)
            {
                try
                {
                    if (test1.articles[i].title == listBox1.SelectedItem.ToString())
                    {
                        webBrowser1.Visible = true;
                        pictureBox1.Load(test1.articles[i].urlToImage);
                        webBrowser1.Navigate(new Uri(test1.articles[i].url));
                        break;
                    }
                }
                catch (IndexOutOfRangeException t)
                {
                    Console.WriteLine("Error: " + t.Message);
                }
            }
        }
    }
}
