using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EZAPI.Framework;
using EZAPI.Financial.Historical;
using EZAPI.Financial.MarketData;
using EZAPI.Framework.Chart;

namespace EZAPITestA
{
    public partial class ChartTestForm : Form
    {
        private IChartDataProvider chartProvider;

        public ChartTestForm()
        {
            InitializeComponent();

            dateTimePicker1.Value = dateTimePicker1.Value.Subtract(TimeSpan.FromDays(365));
        }

        void TestYahooHistorical(string symbol, DateTime startDate)
        {
            List<HistoricalStock> data = YahooHistoricalDownloader.Instance.DownloadData(symbol, startDate);
 
            foreach (HistoricalStock stock in data)
            {
                Console.WriteLine(string.Format("Date={0:M/dd/yyyy} High={1} Low={2} Open={3} Close={4}",stock.Date,stock.High,stock.Low,stock.Open,stock.Close));
            }
        }


        void TestYahooChart(string symbol, DateTime startDate)
        {
            chartProvider = new ChartDataProviderYahoo(symbol, null, null);
            uiControlChart1.SetChartDataProvider(chartProvider);
            // We don't need to handle these events ourselves because the UIControlChart takes
            // care of it. (But we could if we wanted to.)
            //chartProvider.DataLoadComplete += chartProvider_DataLoadComplete;
            //chartProvider.DataSeriesUpdated += chartProvider_DataSeriesUpdated;
            chartProvider.LoadRealTimeChartData(symbol, startDate);
        }

        void TestYahooQuote(string symbol)
        {
            ObservableCollection<Quote> quotes = YahooStockAPI.Instance.Fetch(symbol);
            foreach (Quote quote in quotes)
            {
                Console.WriteLine(quote.Symbol, quote.Bid, quote.Ask, quote.LastTradePrice);
            }
        }

        void TestGoogle(string symbol)
        {
            MarketDataStock stock = GoogleStockAPI.Instance.FetchQuote(symbol);
            Console.WriteLine(stock);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            status("Loading data...");

            //dataPoints = new List<ezBarDataPoint>();
            /*foreach (HistoricalQuote hq in historical)
            {
                ezBarDataPoint dp = new ezBarDataPoint(hq.Open, hq.High, hq.Low, hq.Close, 0, hq.Volume);
                dataPoints.Add(dp);
            }*/

            TestYahooChart(txtSymbol.Text, dateTimePicker1.Value);
            //TestGoogle(txtSymbol.Text);
            //TestYahooQuote(txtSymbol.Text);

            status("Loading data...Done.");
        }

        void status(string text)
        {
            statusLabel.Text = text;
            Application.DoEvents();
        }

        private void chkMainChart_CheckedChanged(object sender, EventArgs e)
        {

        }

    } // class
} // namespace
