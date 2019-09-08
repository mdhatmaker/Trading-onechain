using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CryptoRestApis.RestApi.Models;
using Binance.Net;
using Binance.Net.Objects;
using CryptoTools.Net;
using static CryptoTools.General.General;
using CryptoTools.Models;

namespace CryptoRestApis.RestApi
{
    // https://www.nuget.org/packages/Binance.Net/

    public class BinanceRestApi : ICryptoRestApi
    {
        private CryptoTools.SymbolManager m_symbolManager;

        private BinanceClient m_client;

        public BinanceRestApi(string apiKey, string apiSecret)
        {
            var options = new BinanceClientOptions();
            options.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials(apiKey, apiSecret);
            m_client = new BinanceClient(options);
        }

        // This TEST method should show sample code for each of the following:
        // method: Get Account Balances (for each currency), Get Deposit Addresses (for each currency)
        // method: Get Deposit History, Get Withdrawal History
        // method: Withdraw (to cryptoAddress)
        public static void Test()
        {
            // API test code goes here
        }

        public async Task<XTickerMap> GetTickers()
        {
            var res = await m_client.GetAllBookPricesAsync();
            return new XTickerMap(res.Data);
        }

        public async Task<BinancePlacedOrder> Sell(string symbolId, decimal qty, decimal price)
        {
            string symbol = GetSymbol(symbolId);
            var side = OrderSide.Sell;
            var type = OrderType.Limit;
            string oid = null;
            var tif = TimeInForce.GoodTillCancel;
            var res = await m_client.PlaceOrderAsync(symbol, side, type, qty, oid, price, tif);
            if (res.Error != null) Console.WriteLine("Binance::Sell ERROR: {0} {1}", res.Error.Code, res.Error.Message);
            return res.Data;
        }

        public async Task<BinanceExchangeInfo> GetExchangeInfo()
        {
            var res = await m_client.GetExchangeInfoAsync();
            if (res.Error != null) Console.WriteLine("Binace::GetExchangeInfo ERROR: {0} {1}", res.Error.Code, res.Error.Message);
            return res.Data;
        }

        public XSymbol GetXSymbol(string symbolId)
        {
            if (m_symbolManager == null) m_symbolManager = new CryptoTools.SymbolManager();
            var symbol = m_symbolManager.GetXSymbol(Exchange, symbolId);
            if (symbol != null)
                return symbol;
            else
            {
                Console.WriteLine("ERROR: Symbol ID not found.");
                return null;
            }
        }

        #region ---------- ICryptoApi ---------------------------------------------------------------
        public string Exchange { get { return "BINANCE"; } }

        public List<string> GetAllSymbols()
        {
            var result = new List<string>();
            var res = m_client.GetAllPricesAsync();
            res.Wait();
            foreach (var p in res.Result.Data)
            {
                result.Add(p.Symbol);
            }
            return result;
        }

        public string GetSymbol(string symbolId)
        {
            var xs = GetXSymbol(symbolId);
            if (xs != null)
                return xs.Symbol;
            else
                return null;
        }

        public async Task<XTicker> GetTicker(string symbolId)
        {
            string symbol = GetSymbol(symbolId);
            var res = await m_client.GetBookPriceAsync(symbol);
            return new XTicker(res.Data);
        }

        public async Task<XBalanceMap> GetBalances()
        {
            var res = await m_client.GetAccountInfoAsync();
            return new XBalanceMap(res.Data);
        }
        #endregion ----------------------------------------------------------------------------------


    } // end of class BinanceRestApi
} // end of namespace

