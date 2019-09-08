//using PureSocketCluster;
//using PureWebSockets;
//using WebSocketSharp;
//using WebSocketX;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebSocketTest
{
    class Program
    {
        // https://bitfinex.readme.io/v1/reference

        static void Main(string[] args)
        {
            Console.WriteLine("\n*** WELCOME TO CRYPTO_RISK_MANAGER ***\n");

            BinanceArbs.InitializeApi();

            BinanceArbs.GetBalances(out decimal initTotalBtc, out decimal initTotalUsdt, true);
            Console.WriteLine("\n{0}    {1} BTC   {2} USDT\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), initTotalBtc, initTotalUsdt);

            int n = 0;
            while (true)
            {
                Thread.Sleep(30000);

                ++n;

                decimal totalBtc, totalUsdt;
                if (n % 10 == 0)
                    BinanceArbs.GetBalances(out totalBtc, out totalUsdt, true);
                else
                    BinanceArbs.GetBalances(out totalBtc, out totalUsdt);

                Console.WriteLine("{0}    {1} BTC   {2} USDT            change: {3} BTC   {4} USDT", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), totalBtc, totalUsdt, totalBtc-initTotalBtc, totalUsdt-initTotalUsdt);
            }

            // [LocalTime, exch1UpdateTime, exch1BitCoinPrice, exch2UpdateTime, exch2BitCoinPrice]

            /*
            //Binance.BinanceTickers("trade", new string[] { "btcusdt", "ethusdt", "ethbtc" });
            //Binance.BinanceTickers("ticker", new string[] { "btcusdt", "ethusdt", "ethbtc" });
            List<string> symbols = new List<string>();
            symbols.AddRange(new List<string>() { "ethusdt", "btcusdt" });
            symbols.AddRange( new List<string>() { "neousdt", "neoeth", "neobtc" });
            symbols.AddRange(new List<string>() { "bnbusdt", "bnbeth", "bnbbtc" });
            symbols.AddRange(new List<string>() { "qtumusdt", "qtumeth", "qtumbtc" });
            symbols.AddRange(new List<string>() { "ltcusdt", "ltceth", "ltcbtc" });
            symbols.AddRange(new List<string>() { "bccusdt", "bcceth", "bccbtc" });
            
            List<string> streams = new List<string>();
            streams.Add("ticker");
            streams.Add("aggTrade");
            BinanceArbs.BinanceStreams(streams.ToArray(), symbols.ToArray());
            */

            //GdaxTickers();
            //GeminiTickers();
            //BinanceTickers();
            //BitfinexTickers();
        }



        /*private static void Ws_OnOpen(object sender, EventArgs e)
        {
            Console.WriteLine("OnOpen: ");
        }

        private static void Ws_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("OnError: " + e.Message);
        }

        private static void Ws_OnClose(object sender, CloseEventArgs e)
        {
            Console.WriteLine("OnClose: " + e.Reason);
        }

        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("OnMessage: " + e.Data);
        }*/



        /*private static PureSocketClusterSocket _scc;

        static void TestPureSocketCluster()
        {
            // input credentials if used, different systems use different auth systems this however is the most common (passing 'auth' event with your credentials)
            var creds = new Creds
            {
                apiKey = "your apikey if used",
                apiSecret = "your api secret if used"
            };

            // initialize the client
            //_scc = new PureSocketClusterSocket("wss://yoursocketclusterserver.com/socketcluster/", new ReconnectStrategy(4000, 60000), creds);
            _scc = new PureSocketClusterSocket("wss://ws-feed.gdax.com/", new ReconnectStrategy(4000, 60000), creds);
            

            // hook up to some events
            _scc.OnOpened += Scc_OnOpened;
            _scc.OnMessage += _scc_OnMessage;
            _scc.OnStateChanged += _scc_OnStateChanged;
            _scc.OnSendFailed += _scc_OnSendFailed;
            _scc.OnError += _scc_OnError;
            _scc.OnClosed += _scc_OnClosed;
            _scc.OnData += _scc_OnData;
            _scc.OnFatality += _scc_OnFatality;
            _scc.Connect();

            // subscribe to some channels
            var cn = _scc.CreateChannel("TRADE-PLNX--BTC--ETC").Subscribe();
            cn.OnMessage(TradeData);
            var cn0 = _scc.CreateChannel("TRADE-PLNX--BTC--ETH").Subscribe();
            cn0.OnMessage(TradeData);
            var cn1 = _scc.CreateChannel("TRADE-OK--BTC--USD").Subscribe();
            cn1.OnMessage(TradeData);

            Console.ReadLine();
        }

        private static void _scc_OnFatality(string reason)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Fatality: {reason}");
            Console.ResetColor();
            Console.WriteLine("");
        }

        private static void _scc_OnData(byte[] data)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Binary: {data}");
            Console.ResetColor();
            Console.WriteLine("");
        }

        //private static void _scc_OnClosed(WebSocketCloseStatus reason)
        private static void _scc_OnClosed(System.Net.WebSockets.WebSocketCloseStatus reason)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Socket Closed: {reason}");
            Console.ResetColor();
            Console.WriteLine("");
        }

        private static void _scc_OnError(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"Error: {ex}");
            Console.ResetColor();
            Console.WriteLine("");
        }

        private static void _scc_OnSendFailed(string data, Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"send failed: {data} Ex: {ex}");
            Console.ResetColor();
            Console.WriteLine("");
        }

        //private static void _scc_OnStateChanged(WebSocketState newState, WebSocketState prevState)
        private static void _scc_OnStateChanged(System.Net.WebSockets.WebSocketState newState, System.Net.WebSockets.WebSocketState prevState)
        {
        Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"State changed from {prevState} to {newState}");
            Console.ResetColor();
            Console.WriteLine("");
        }

        private static void TradeData(string name, object data)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(name + ": " + data);
            Console.ResetColor();
            Console.WriteLine("");
        }

        private static void _scc_OnMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine("");
        }

        private static void Scc_OnOpened()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Opened");
            Console.ResetColor();
            Console.WriteLine("");
        }*/

    } // end of class Program

} // end of namespace
