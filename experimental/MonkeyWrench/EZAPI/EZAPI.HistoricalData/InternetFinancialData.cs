using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using EZAPI.Framework.Chart;

namespace EZAPI.HistoricalData
{
    public static class InternetFinancialData
    {

        public static ReadOnlyCollection<string> GetStockSymbols()
        {
            List<string> symbolList = new List<string>();

            string url = "http://www.batstrading.com/market_data/symbol_listing/xml/";

            XDocument doc = XDocument.Load(url);

            var symbols = from s in doc.Root.Element("symbols").Elements("symbol")
                          select new { Name = s.Attribute("name").Value };

            foreach (var symbol in symbols)
            {
                string tmp = Regex.Replace(symbol.ToString(), "[{} ]+", "");
                if (!tmp.StartsWith("Name="))
                    throw new ArgumentException("Incorrect formatting of stock symbol - 'Name=SYMBOL' expected.");

                symbolList.Add(tmp.Substring(5));
            }

            return new ReadOnlyCollection<string>(symbolList);
        }

        public static void GetChartData(string symbol, DateTime startDate)
        {
            DateTime endDate = DateTime.Now;
            var historical = InternetFinancialData.GetYahooHistorical(symbol, startDate, endDate);

            List<ezBarDataPoint> dataPoints = new List<ezBarDataPoint>();
            foreach (HistoricalQuote hq in historical)
            {
                ezBarDataPoint dp = new ezBarDataPoint(hq.Open, hq.High, hq.Low, hq.Close, hq.Volume);
                dataPoints.Add(dp);
            }

            Console.WriteLine("load realtime chart data - complete");
            /*if (DataLoadComplete != null)
            {
                ezDateRange requested = new ezDateRange();
                ezDateRange processed = new ezDateRange();
                DataLoadComplete(this, new ezDataLoadCompleteEventArgs(zDataLoadStatus.Success, requested, processed));
            }*/
        }

        public static ReadOnlyCollection<HistoricalQuote> GetYahooHistorical(string symbol, DateTime startDate, DateTime endDate)
        {
            List<HistoricalQuote> historical = new List<HistoricalQuote>();

            // Create the URL for the request.
            StringBuilder theWebAddress = new StringBuilder();
            theWebAddress.Append("http://query.yahooapis.com/v1/public/yql?");
            string start = "2013-01-01";
            string end = "2013-03-05";
            theWebAddress.Append("q=" + System.Web.HttpUtility.UrlEncode(string.Format("select * from yahoo.finance.historicaldata where symbol=\"{0}\" and startDate = \"{1}\" and endDate = \"{2}\"", symbol, start, end)));
            //theWebAddress.Append("q=" + System.Web.HttpUtility.UrlEncode("select * from local.search where location='Nashville ,TN' and query='Fast Food'"));
            theWebAddress.Append("&format=json");
            theWebAddress.Append("&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");
            theWebAddress.Append("&callback=");
            theWebAddress.Append("&diagnostics=false");

            //string address = "http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.historicaldata%20where%20symbol%3D%22AAPL%22%20and%20startDate%20%3D%20%222013-01-01%22%20and%20endDate%20%3D%20%222013-03-05%22&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";
            //string address = "http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.historicaldata%20where%20symbol%3D%22AAPL%22%20and%20startDate%20%3D%20%222013-01-01%22%20and%20endDate%20%3D%20%222013-03-05%22&format=json&diagnostics=false";

            // Retrieve the results from Yahoo.
            JArray jsonArray = null;
            int retryCount = 0;
            Console.WriteLine("Trying to retrieve results from Yahoo...Attempt #{0}", retryCount+1);
            while (retryCount < 5)
            {
                try
                {
                    string results = "";
                    using (WebClient wc = new WebClient())
                    {
                        results = wc.DownloadString(theWebAddress.ToString());
                    }

                    // Parse the results into objects (from JSON).
                    JObject dataObject = JObject.Parse(results);
                    jsonArray = (JArray)dataObject["query"]["results"]["quote"];
                }
                catch (Exception)
                {
                }
                finally
                {
                    Console.WriteLine("Waiting before retry...");
                    Thread.Sleep(3000);                    
                    retryCount++;
                }
            }


            // Display the field values.
            foreach (var quote in jsonArray)
            {                
                HistoricalQuote hq = new HistoricalQuote();
                
                DateTime dateValue;
                if (DateTime.TryParse(quote["date"].ToString(), out dateValue))
                    hq.date = dateValue;
                else
                    throw new ArgumentException("Cannot convert date from HistoricalQuote object to DateTime.");

                if (DateTime.TryParse(quote["Date"].ToString(), out dateValue))
                    hq.Date = dateValue;
                else
                    throw new ArgumentException("Cannot convert Date from HistoricalQuote object to DateTime.");

                hq.Open = Convert.ToDouble(quote["Open"].ToString());
                hq.High = Convert.ToDouble(quote["High"].ToString());
                hq.Low = Convert.ToDouble(quote["Low"].ToString());
                hq.Close = Convert.ToDouble(quote["Close"].ToString());
                hq.Volume = Convert.ToInt32(quote["Volume"].ToString());
                historical.Add(hq);
            }

            return new ReadOnlyCollection<HistoricalQuote>(historical);
        }
    } // class


} // namespace
