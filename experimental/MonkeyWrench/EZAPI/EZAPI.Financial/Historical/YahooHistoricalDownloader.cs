using System;
using System.Collections.Generic;
using System.Net;

namespace EZAPI.Financial.Historical
{
    public class YahooHistoricalDownloader : HistoricalDownloader
    {
        public static YahooHistoricalDownloader Instance
        {
            get
            {
                if (instance == null)
                    instance = new YahooHistoricalDownloader();
                return instance;
            }
        }

        private static YahooHistoricalDownloader instance;

        private YahooHistoricalDownloader()
        {
            instance = null;
        }

        /// <summary>
        /// Not supplying an end date will use today's date as the end date.
        /// </summary>
        public override List<HistoricalStock> DownloadData(string ticker, DateTime startDate)
        {
            return DownloadData(ticker, startDate, DateTime.Now);
        }

        public override List<HistoricalStock> DownloadData(string ticker, DateTime startDate, DateTime endDate)
        {
            List<HistoricalStock> retval = new List<HistoricalStock>();

            int startYear = startDate.Year;
            int startMonth = startDate.Month - 1;   // have to subtract 1 from the month to get the right data from Yahoo
            int startDay = startDate.Day;
            int endYear = endDate.Year;
            int endMonth = endDate.Month - 1;       // have to subtract 1 from the month to get the right data from Yahoo
            int endDay = endDate.Day;

            using (WebClient web = new WebClient())
            {
                // Now you have to add the interval of the trading periods (d=Daily, w=Weekly, m=Monthly).
                // http://ichart.yahoo.com/table.csv?s=GOOG&a=0&b=1&c=2000&d=0&e=31&f=2010&g=w

                string data = web.DownloadString(string.Format("http://ichart.finance.yahoo.com/table.csv?s={0}&a={1}&b={2}&c={3}&d={4}&e={5}&f={6}", ticker, startMonth, startDay, startYear, endMonth, endDay, endYear));

                data = data.Replace("\r", "");

                string[] rows = data.Split('\n');

                //First row is headers so Ignore it
                for (int i = 1; i < rows.Length; i++)
                {
                    if (rows[i].Replace("\n", "").Trim() == "") continue;

                    string[] cols = rows[i].Split(',');

                    HistoricalStock hs = new HistoricalStock();
                    hs.Date = Convert.ToDateTime(cols[0]);
                    hs.Open = Convert.ToDouble(cols[1]);
                    hs.High = Convert.ToDouble(cols[2]);
                    hs.Low = Convert.ToDouble(cols[3]);
                    hs.Close = Convert.ToDouble(cols[4]);
                    hs.Volume = Convert.ToInt32(cols[5]);
                    hs.AdjClose = Convert.ToDouble(cols[6]);

                    retval.Add(hs);
                }

                return retval;
            }
        }
    } // class
} // namespace