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
using EZAPI.Financial.Historical;
using EZAPI.Financial.MarketData;

namespace EZAPITest
{
    public partial class TestHistoricalForm : Form
    {
        public TestHistoricalForm()
        {
            InitializeComponent();
        }

        void TestYahoo(string symbol, DateTime startDate)
        {
            List<HistoricalStock> data = YahooHistoricalDownloader.Instance.DownloadData(symbol, startDate);
 
            foreach (HistoricalStock stock in data)
            {
                Console.WriteLine(string.Format("Date={0:M/dd/yyyy} High={1} Low={2} Open={3} Close={4}",stock.Date,stock.High,stock.Low,stock.Open,stock.Close));
            }
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

            TestYahoo(txtSymbol.Text, dateTimePicker1.Value);
            //TestGoogle(txtSymbol.Text);
            //TestYahooQuote(txtSymbol.Text);

            status("Loading data...Done.");
        }

        void status(string text)
        {
            statusLabel.Text = text;
            Application.DoEvents();
        }

    } // class
} // namespace
