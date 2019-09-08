using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Toolbox.Finance
{
    public static class EconomicNumbers
    {
        // Either specify a specific URL of a CSV file containing econ numbers, or pass
        // null and this method will try to grab the latest data.
        public static List<EconomicNumber> DownloadDailyFXCalendar(string urlToDownload)
        {
            List<EconomicNumber> results = new List<EconomicNumber>();

            string url = null;

            if (urlToDownload == null)
            {
                //url = "http://www.dailyfx.com/files/Calendar-03-24-2013.csv";

                // Start with today's date, and loop backwards until we find a Sunday
                // (which is the day the online calendars are released).
                url = "http://www.dailyfx.com/files/Calendar-";
                DateTime lookForSunday = DateTime.Now;
                while (lookForSunday.DayOfWeek != DayOfWeek.Sunday)
                {
                    lookForSunday = lookForSunday.Subtract(TimeSpan.FromDays(1));
                }
                string date = lookForSunday.ToString("MM-dd-yyyy");
                url = url + date + ".csv";                
            }
            else
            {
                url = urlToDownload;
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            
            // Read the first line (column headers) - we will discard it.
            string line = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                line = PreParseCSV(line);

                string[] split = line.Split(',');

                EconomicNumber econ = new EconomicNumber();
                econ.Date = Convert.ToDateTime(split[0]);
                econ.Time = ConvertToDateTime(split[1]);
                econ.TimeZone = Parse.ParseEnum<zTimeZone>(split[2]);
                econ.Currency = Parse.ParseEnum<zCurrency>(split[3]);
                econ.Event = split[4];
                econ.Importance = Parse.ParseEnum<zImportance>(split[5]);
                econ.Actual = split[6];
                econ.Forecast = split[7];
                econ.Previous = split[8];

                econ.Event = PostParseCSV(econ.Event);

                results.Add(econ);
            }

            reader.Close();

            return results;
        }

        private const char commaSubstitute = '~';

        private static string PreParseCSV(string csv)
        {
            StringBuilder sb = new StringBuilder(csv);

            // Find the quotes, and take them out (and remove any commas within them).
            int i1 = sb.ToString().IndexOf('"');
            while (i1 >= 0)
            {
                // Find the second (closing) quote.
                int i2 = sb.ToString().IndexOf('"', i1 + 1);
                // Within the quoted string, replace all commas with '&'.
                sb.Replace(',', commaSubstitute, i1, i2 - i1 + 1);
                // Remove the ending and starting quotes.
                sb.Remove(i2, 1);
                sb.Remove(i1, 1);

                i1 = sb.ToString().IndexOf('"');
            }
            return sb.ToString();
        }

        private static string PostParseCSV(string csv)
        {
            StringBuilder sb = new StringBuilder(csv);

            sb.Replace(commaSubstitute, ',');

            return sb.ToString();
        }

        private static DateTime ConvertToDateTime(string str)
        {
            DateTime result;

            if (str.Trim() == "")
            {
                result = DateTime.MinValue;
            }
            else
            {
                result = Convert.ToDateTime(str);
            }

            return result;
        }

    } // class
} // namespace
