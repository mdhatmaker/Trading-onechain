using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoTools;
using CryptoTools.General;
using ExchangeSharp;

namespace CryptoRestApis.RestApi
{
	// https://github.com/jjxtra/ExchangeSharp

	public class ExchangeSharpRestApi
	{
		public ICollection<string> Exchanges => m_apiMap.Keys;

		private IDictionary<string, IExchangeAPI> m_apiMap;
		private Credentials m_creds;

        public ExchangeSharpRestApi()
		{
			m_apiMap = GetPrimaryExchangeApis();
		}

        // where encryptedFile is the full pathname of the encrypted CSV file like "/Users/david/Documents/myapis.csv.enc"
        // where password is 8-char password like "12345678"
        public ExchangeSharpRestApi(string encryptedFile, string password)
        {
			m_creds = Credentials.LoadEncryptedCsv(encryptedFile, password);
			//m_apiMap = GetAllExchangeApis();
			m_apiMap = GetPrimaryExchangeApis();
        }

		private IDictionary<string, IExchangeAPI> GetPrimaryExchangeApis(bool forceUpdate = false)
        {
            if (m_apiMap == null || forceUpdate)
            {
                m_apiMap = new SortedDictionary<string, IExchangeAPI>();
                
                m_apiMap.Add("Kraken", new ExchangeKrakenAPI());
                m_apiMap.Add("Gemini", new ExchangeGeminiAPI());
                m_apiMap.Add("Bittrex", new ExchangeBittrexAPI());
                m_apiMap.Add("Binance", new ExchangeBinanceAPI());
                m_apiMap.Add("Gdax", new ExchangeGdaxAPI());
                //m_apiMap.Add("Bithumb", new ExchangeBithumbAPI());  // TODO: What's wrong with Bithumb?
                m_apiMap.Add("Bitfinex", new ExchangeBitfinexAPI());
                m_apiMap.Add("Bitstamp", new ExchangeBitstampAPI());
                m_apiMap.Add("Poloniex", new ExchangePoloniexAPI());
            }
            return m_apiMap;
        }

		private IDictionary<string, IExchangeAPI> GetAllExchangeApis(bool forceUpdate = false)
        {
            if (m_apiMap == null || forceUpdate)
            {
                m_apiMap = new SortedDictionary<string, IExchangeAPI>();

                m_apiMap.Add("Kraken", new ExchangeKrakenAPI());
                m_apiMap.Add("Gemini", new ExchangeGeminiAPI());
                m_apiMap.Add("Abucoins", new ExchangeAbucoinsAPI());
                m_apiMap.Add("Hitbtc", new ExchangeHitbtcAPI());
                m_apiMap.Add("Bittrex", new ExchangeBittrexAPI());
                m_apiMap.Add("Binance", new ExchangeBinanceAPI());
                m_apiMap.Add("Okex", new ExchangeOkexAPI());
                m_apiMap.Add("Huobi", new ExchangeHuobiAPI());
                m_apiMap.Add("Yobit", new ExchangeYobitAPI());
                m_apiMap.Add("Gdax", new ExchangeGdaxAPI());
                m_apiMap.Add("Kucoin", new ExchangeKucoinAPI());
                //m_apiMap.Add("Bithumb", new ExchangeBithumbAPI());  // TODO: What's wrong with Bithumb?
                m_apiMap.Add("Bitfinex", new ExchangeBitfinexAPI());
                m_apiMap.Add("Bitstamp", new ExchangeBitstampAPI());
                m_apiMap.Add("Livecoin", new ExchangeLivecoinAPI());
                m_apiMap.Add("Poloniex", new ExchangePoloniexAPI());
                m_apiMap.Add("Bleutrade", new ExchangeBleutradeAPI());
                m_apiMap.Add("Cryptopia", new ExchangeCryptopiaAPI());
                m_apiMap.Add("Tux", new ExchangeTuxExchangeAPI());
            }
            return m_apiMap;
        }

		/*public static string[] GetArgs(string commandLineArgs = null)
		{
			if (commandLineArgs == null)
				commandLineArgs = "bitfinex BTCUSDT 5 /Users/michael/Documents/hat_apis.csv.enc mywookie";

			string[] args = commandLineArgs.Split(' ');
			// Pass FIVE arguments: <exchange> <symbol> <minutes> <encrypted_api_file> <8_char_password>
			if (args.Length < 5)
			{
				Console.WriteLine("usage: dotnet vwap.dll <exchange> <symbol> <minutes> <encrypted_api_file> <8_char_password>");
				Console.WriteLine("\n   ex: dotnet vwap.dll binance BTCUSDT 60 /Users/david/apis.csv.enc A5s78rQz\n");
				return null;
			}
			return args;
		}*/

		public void Test()   //string exchange, string symbol)
		{
            m_apiMap = GetAllExchangeApis(true);
            var api = m_apiMap["Hitbtc"];
            var tsymbols = GetSymbols(api);
            tsymbols.Wait();
            var symbols = tsymbols.Result;
            var global_symbols = symbols.Select(s => api.ExchangeSymbolToGlobalSymbol(s));
            global_symbols.ToList().ForEach(s => Console.WriteLine(s));
            //Candles(api, "BTC-USD");

            //TestWebsocketsBinance();
            //TestWebsocketsBittrex();
            //TestWebsocketsPoloniex();
            //TestWebsocketsGdax();
            //TestWebsocketsBitfinex();

            //BalancesForAllExchanges();

            //Gator("BTC-USD", 20, 125, 20);
            //Gator("ETH-USD", 200, 125, 20);
            //Gator("ETH-USD", 200, 125);

            //OrderBookForAllExchanges("BTC-USDT");
            //OrderBookForAllExchanges("ETH-USD");
            //OrderBookForAllExchanges("ETH-USDT");

            return;

			RecentTradesForAllExchanges("BTC-USD");
            //RecentTradesForAllExchanges("BTC-USDT");
            //RecentTradesForAllExchanges("ETH-USD");
            //RecentTradesForAllExchanges("ETH-USDT");
            //RecentTradesForAllExchanges("BTC-ETH");

			//TickerForAllExchanges("BTC-USD");
			//TickerForAllExchanges("BTC-USDT");
			TickerForAllExchanges("ETH-BTC");
			//TickerForAllExchanges("XMR-ETH");
			//TickerForAllExchanges("XRP-ETH");

            // -----GetHistoricalTrades-----
			//var sinceDateTime = trades.First().Timestamp;
			//api.GetHistoricalTrades(callback, symbol, sinceDateTime);
		}

        /*private bool callback(IEnumerable<ExchangeTrade> trades)
		{
			trades.ToList().ForEach(t => t.Print());
			return true;
		}*/

        public void Candles(IExchangeAPI api, string global_symbol)
        {
            var symbol = api.GlobalSymbolToExchangeSymbol(global_symbol);
            int? limit = null;
            DateTime? startDate = null;
            DateTime? endDate = null;
            var candles = api.GetCandles(symbol, 60, startDate, endDate, limit);
            foreach (var c in candles)
            {
                Console.WriteLine("{0} {1} [{2} {3}] o:{4} h:{5} l:{6} c:{7} vol:{8}", c.Timestamp, c.PeriodSeconds, c.ExchangeName, c.Name, c.OpenPrice, c.HighPrice, c.LowPrice, c.ClosePrice, c.BaseVolume);
            }
        }

        public void TestWebsocketsBitfinex()
        {
            // create a web socket connection to the exchange. Note you can Dispose the socket anytime to shut it down.
            // the web socket will handle disconnects and attempt to re-connect automatically.
            IExchangeAPI b = new ExchangeBitfinexAPI();
            using (var socket = b.GetTickersWebSocket((tickers) =>
            {
                Console.WriteLine("{0} tickers, first: {1}", tickers.Count, tickers.First());
            }))
            {
                Console.WriteLine("Press ENTER to shutdown.");
                Console.ReadLine();
            }

        }

        public void TestWebsocketsGdax()
        {
            // create a web socket connection to the exchange. Note you can Dispose the socket anytime to shut it down.
            // the web socket will handle disconnects and attempt to re-connect automatically.
            IExchangeAPI b = new ExchangeGdaxAPI();
            using (var socket = b.GetTickersWebSocket((tickers) =>
            {
                Console.WriteLine("{0} tickers, first: {1}", tickers.Count, tickers.First());
            }))
            {
                Console.WriteLine("Press ENTER to shutdown.");
                Console.ReadLine();
            }

        }

        public void TestWebsocketsPoloniex()
        {
            // create a web socket connection to the exchange. Note you can Dispose the socket anytime to shut it down.
            // the web socket will handle disconnects and attempt to re-connect automatically.
            IExchangeAPI b = new ExchangePoloniexAPI();
            using (var socket = b.GetTickersWebSocket((tickers) =>
            {
                Console.WriteLine("{0} tickers, first: {1}", tickers.Count, tickers.First());
            }))
            {
                Console.WriteLine("Press ENTER to shutdown.");
                Console.ReadLine();
            }

        }

        public void TestWebsocketsBinance()
        {
            // create a web socket connection to the exchange. Note you can Dispose the socket anytime to shut it down.
            // the web socket will handle disconnects and attempt to re-connect automatically.
            IExchangeAPI b = new ExchangeBinanceAPI();
            using (var socket = b.GetTickersWebSocket((tickers) =>
            {
                Console.WriteLine("{0} tickers, first: {1}", tickers.Count, tickers.First());
            }))
            {
                Console.WriteLine("Press ENTER to shutdown.");
                Console.ReadLine();
            }
        }

        public void TestWebsocketsBittrex()
        {
            // create a web socket connection to the exchange. Note you can Dispose the socket anytime to shut it down.
            // the web socket will handle disconnects and attempt to re-connect automatically.
            IExchangeAPI b = new ExchangeBittrexAPI();
            using (var socket = b.GetTickersWebSocket((tickers) =>
            {
                Console.WriteLine("{0} tickers, first: {1}", tickers.Count, tickers.First());
            }))
            {
                Console.WriteLine("Press ENTER to shutdown.");
                Console.ReadLine();
            }
        }

        public void BalancesForAllExchanges()
        {
            Console.WriteLine();

            List<Task> taskList = new List<Task>();
            foreach (var kv in m_apiMap)
            {
                var exchange = kv.Key;
                var api = kv.Value;
                var tb = Balance(api, exchange);
                taskList.Add(tb);
            }
            Task.WaitAll(taskList.ToArray());
        }

        private async Task Balance(IExchangeAPI api, string exchange)
        {
            try
            {
                var tamounts = await api.GetAmountsAsync();
                //var ticker = await api.GetTickerAsync(symbol);
                foreach (var kv in tamounts)
                {
                    var asset = kv.Key;
                    var amount = kv.Value;
                    Console.WriteLine("[{0,-10}]  {1}  {2:0.00000000}", exchange, asset, amount);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("***[{0}] ERROR: {1}***", exchange, ex.Message);
            }
        }


        public void Gator(string global_symbol, decimal amountRequested, decimal bips, int displayBest = 0)
		{
			ConsolidatedOrderBook ob;
            ob = OrderBookForAllExchanges(global_symbol);

			if (displayBest > 0)
			{
				Console.WriteLine("\n---BIDS---");
				for (int i = 0; i < displayBest; ++i)
					Console.WriteLine("{0,3} {1}", i, ob.Bids[i]);
				Console.WriteLine("\n---ASKS---");
				for (int i = 0; i < displayBest; ++i)
					Console.WriteLine("{0,3} {1}", i, ob.Asks[i]);
			}

			decimal mybid = 0.00M, myask = 0.00M;

			Console.WriteLine(new string('-', 120));
			Console.WriteLine("For amount {0} {1}:", amountRequested, global_symbol);
			decimal atotal = 0.0M;
            for (int i = 0; i < ob.AskCount; ++i)
            {
                atotal += ob.Asks[i].Amount;
                if (atotal >= amountRequested)
                {
                    var range = ob.Asks.GetRange(0, i + 1);
                    var sumPQ = range.Sum(x => x.Price * x.Amount);
                    var sumQ = range.Sum(x => x.Amount);
                    var avgPrice = sumPQ / sumQ;
					var excess = sumQ - amountRequested;
					myask = avgPrice + (avgPrice * bips / 10000);
                    Console.WriteLine("\nasks[{0}]   avg_price:{1:0.00}   {2}", i, avgPrice, PlusBipsString(avgPrice));  // ob.Asks[i].Price);
                    var aexchanges = ob.Asks.GetRange(0, i + 1).Select(x => x.Exchange).Distinct();
					Console.WriteLine("CUSTOMER BUYS");
					foreach (var e in aexchanges)
					{
						var exch = range.Where(x => x.Exchange == e);
						var price = exch.Max(x => x.Price);
						var amount = exch.Sum(x => x.Amount);
						if (e == range.Last().Exchange)
                            amount -= excess;
						Console.WriteLine("{0,-10} BUY  {1} at price {2}", e, amount, price);
					}
                    break;
                }
            }
			decimal btotal = 0.0M;
			for (int i = 0; i < ob.BidCount; ++i)
			{
				btotal += ob.Bids[i].Amount;
				if (btotal >= amountRequested)
				{
					var range = ob.Bids.GetRange(0, i + 1);
					var sumPQ = range.Sum(x => x.Price * x.Amount);
					var sumQ = range.Sum(x => x.Amount);
					var avgPrice = sumPQ / sumQ;
					var excess = sumQ - amountRequested;
					mybid = avgPrice - (avgPrice * bips / 10000M);
					Console.WriteLine("\nbids[{0}]   avg_price:{1:0.00}   {2}", i, avgPrice, MinusBipsString(avgPrice));   // ob.Bids[i].Price);
					var bexchanges = range.Select(x => x.Exchange).Distinct();
					Console.WriteLine("CUSTOMER SELLS");
					foreach (var e in bexchanges)
                    {
						var exch = range.Where(x => x.Exchange == e);
                        var price = exch.Min(x => x.Price);
                        var amount = exch.Sum(x => x.Amount);
						if (e == range.Last().Exchange)
							amount -= excess;
						Console.WriteLine("{0,-10} SELL {1} at price {2}", e, amount, price);
                    }
					break;
				}
			}
			Console.WriteLine();
			Console.WriteLine("at {0} bips:\n   MY ASK={1:0.00}\n   MY BID={2:0.00}", bips, myask, mybid);
			Console.WriteLine();
		}
        
		private string MinusBipsString(decimal amount)
		{
			return string.Format("-25={0:0.00}  -50={1:0.00}  -75={2:0.00}  -100={3:0.00}  -125={4:0.00}  -150={5:0.00}  -175={6:0.00}", amount - (amount * 0.0025M), amount - (amount * 0.0050M), amount - (amount * 0.0075M), amount - (amount * 0.0100M), amount - (amount * 0.0125M), amount - (amount * 0.0150M), amount - (amount * 0.0175M));
		}
        
        private string PlusBipsString(decimal amount)
        {
			return string.Format("+25={0:0.00}  +50={1:0.00}  +75={2:0.00}  +100={3:0.00}  +125={4:0.00}  +150={5:0.00}  +175={6:0.00}", amount + (amount * 0.0025M), amount + (amount * 0.0050M), amount + (amount * 0.0075M), amount + (amount * 0.0100M), amount + (amount * 0.0125M), amount + (amount * 0.0150M), amount + (amount * 0.0175M));
        }
        
		public void RecentTradesForAllExchanges(string global_symbol)
		{
			Console.WriteLine();
			foreach (var kv in m_apiMap)
			{
				var exchange = kv.Key;
				var api = kv.Value;
				RecentTrades(api, exchange, global_symbol);
			}
		}
        
		private void RecentTrades(IExchangeAPI api, string exchange, string global_symbol, bool displayTrades = false)
		{
			try
			{
				var symbol = api.GlobalSymbolToExchangeSymbol(global_symbol);       // "BTC-KRW" for Bithumb?

				var trades = api.GetRecentTrades(symbol);
				if (displayTrades) trades.ToList().ForEach(t => t.Print());
				int tradeCount = trades.Count();
				if (tradeCount == 0)
				{
					//Console.WriteLine("[{0,-10} {1,9}]  {2,5} trades", exchange, symbol, tradeCount);
					return;
				}
				Console.WriteLine("[{0,-10} {1,9}]  {2,5} trades", exchange, symbol, tradeCount);
			}
			catch (Exception ex)
			{
				//Console.WriteLine("***[{0} {1}] ERROR: {2}***", exchange, global_symbol, ""); //ex.Message);
			}
		}

        public void SymbolsForAllExchanges()
		{
			List<Task> taskList = new List<Task>();
            foreach (var kv in m_apiMap)
            {
                var exchange = kv.Key;
                var api = kv.Value;
                var s = GetSymbols(api);
                taskList.Add(s);
            }
            Task.WaitAll(taskList.ToArray());
			Console.WriteLine("here");
		}
        
		private async Task<IEnumerable<string>> GetSymbols(IExchangeAPI api)
        {
            try
            {
				var symbols = await api.GetSymbolsAsync();
				return symbols;
            }
            catch (Exception ex)
            {
				//Console.WriteLine("***[{0} {1}] ERROR: {2}***", exchange, global_symbol, ""); //ex.Message);
				return null;
			}
        }

		public void PrintSymbols(IExchangeAPI api)
		{
			api.GetSymbols().ToList().ForEach(s => Console.WriteLine(s));
		}

		public void TickerForAllExchanges(string global_symbol)
		{
			Console.WriteLine();

			List<Task> taskList = new List<Task>();
			foreach (var kv in m_apiMap)
			{
				var exchange = kv.Key;
				var api = kv.Value;
				var t = Ticker(api, exchange, global_symbol);
				taskList.Add(t);
			}
			Task.WaitAll(taskList.ToArray());
		}
        
		private async Task Ticker(IExchangeAPI api, string exchange, string global_symbol)
		{
			try
			{
				var symbol = api.GlobalSymbolToExchangeSymbol(global_symbol);
				var ticker = await api.GetTickerAsync(symbol);
				//var ticker = api.GetTicker(symbol);
				Console.WriteLine("[{0,-10} {1,9}]      b:{2:0.00000000}      a:{3:0.00000000}", exchange, symbol, ticker.Bid, ticker.Ask);
			}
			catch (Exception ex)
			{
				//Console.WriteLine("***[{0} {1}] ERROR: {2}***", exchange, global_symbol, ""); //ex.Message);
			}
		}
           
		public ConsolidatedOrderBook OrderBookForAllExchanges(string global_symbol)
        {
            //Console.WriteLine();

			var tasks = new Dictionary<string, Task<ExchangeOrderBook>>();
			//var tasks = new List<Task<ExchangeOrderBook>>();
            foreach (var kv in m_apiMap)
            {
                var exchange = kv.Key;
                var api = kv.Value;
                var tob = OrderBook(api, exchange, global_symbol);
				tasks[exchange] = tob;
                //tasks.Add(tob);
                //tob.Result.
            }
			Task.WaitAll(tasks.Values.ToArray());
			//Task.WaitAll(tasks.ToArray());
			var bids = new List<OrderBookEntry>();
			var asks = new List<OrderBookEntry>();
			foreach (var kv in tasks)
			{
				var exchange = kv.Key;
				var book = kv.Value.Result;
				if (book == null || book.Bids == null || book.Asks == null) continue;
				book.Bids.ForEach(b => bids.Add(new OrderBookEntry(exchange, b.Price, b.Amount)));
				book.Asks.ForEach(a => asks.Add(new OrderBookEntry(exchange, a.Price, a.Amount)));
			}
			Console.WriteLine("{0} bids  {1} asks", bids.Count, asks.Count);
			return new ConsolidatedOrderBook(bids, asks);
        }
        
        private async Task<ExchangeOrderBook> OrderBook(IExchangeAPI api, string exchange, string global_symbol)
        {
            try
            {
                var symbol = api.GlobalSymbolToExchangeSymbol(global_symbol);
				var ob = await api.GetOrderBookAsync(symbol);
				Console.WriteLine("[{0,-10} {1,9}]    {2,5} bids: {3:0.00000000}    {4,5} asks: {5:0.00000000} ", exchange, symbol, ob.Bids.Count, ob.Bids.First().Price, ob.Asks.Count, ob.Asks.First().Price);
				return ob;
			}
            catch (Exception ex)
            {
                //Console.WriteLine("***[{0} {1}] ERROR: {2}***", exchange, global_symbol, ""); //ex.Message);
            }
			return null;
        }

	} // end of class ExchangeSharpRestApi


    public class OrderBookEntry
	{
		public string Exchange { get; private set; }
		public decimal Price { get; private set; }
		public decimal Amount { get; private set; }

        public OrderBookEntry(string exchange, decimal price, decimal amount)
		{
			Exchange = exchange;
			Price = price;
			Amount = amount;
		}

		public override string ToString()
		{
			return string.Format("{0,-9}  price:{1:0.00######}  amount:{2:0.0#######}", Exchange, Price, Amount);
		}
	}

    public class ConsolidatedOrderBook
	{
		public List<OrderBookEntry> Bids { get; private set; }
		public List<OrderBookEntry> Asks { get; private set; }
		public int BidCount => Bids.Count;
		public int AskCount => Asks.Count;

		public ConsolidatedOrderBook(List<OrderBookEntry> bids, List<OrderBookEntry> asks)
		{
			Bids = bids.OrderByDescending(b => b.Price).ToList();
			Asks = asks.OrderBy(a => a.Price).ToList();
		}
	}
    //================================================================================================
    public static class ExchangeSharpExtensionMethods
	{
		// Extension Method: Display contents of an ExchangeTrade object
        public static void Print(this ExchangeTrade t)
        {
            Console.WriteLine("{0} {1} {2}", t.Timestamp.ToDisplay(), t.Price, t.Amount);
        }
	} // end of class ExchangeSharpExtensionMethods
	//================================================================================================

} // end of namespace
