using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using static CryptoTools.General.General;

namespace CryptoRestApis.ExchangeX.CoinMarketCap
{
    public static class CoinMarketCapModels
    {
    }

    // A List of CoinMarketCapTicker objects (that implements NullableObject)
    public class CoinMarketCapTickerList : List<CoinMarketCapTicker>, CryptoTools.Net.NullableObject
    {
        public bool IsNull => (this.Count == 0);

        public void RemoveZeroMarketCap()
        {
            this.RemoveAll(t => t.market_cap_usd == 0);
        }
    }

    public class CoinMarketCapTicker : CryptoTools.Net.NullableObject
    {        
        public string id { get; set; }                  // "bitcoin"
        public string name { get; set; }                // "Bitcoin"
        public string symbol { get; set; }              // "BTC"
        public int rank { get; set; }                   // "1", "2", "3", ...
        public decimal price_usd { get; set; }          // "9343.13"
        public decimal price_btc { get; set; }          // "1.0"
        [JsonProperty(PropertyName = "24h_volume_usd")]
        public decimal _24h_volume_usd { get; set; }    // "8278080000.0"
        public decimal market_cap_usd { get; set; }     // "158904217788"
        public decimal available_supply { get; set; }   // "17007600.0"
        public decimal total_supply { get; set; }       // "17007600.0"
        public decimal max_supply { get; set; }         // "21000000.0"
        public decimal percent_change_1h { get; set; }  // "0.21"
        public decimal percent_change_24h { get; set; } // "0.16"
        public decimal percent_change_7d { get; set; }  // "4.84"
        public long last_updated { get; set; }          // "1525112971"

        public bool IsNull => (id == null);

        public override string ToString()
        {
            string updateTime = FromTimestampSeconds((int)last_updated).ToString("yyyy-MM-dd HH:mm:ss");
            return string.Format("{0} {1,4} {2,-5} [{3}] '{4}' ${5} B{6} vol:{7} cap:${8:0,000} supply:[{9} / {10} / {11}] %chg:[{12} {13} {14}]", updateTime, rank, symbol, id, name, price_usd, price_btc, _24h_volume_usd, market_cap_usd, available_supply, total_supply, max_supply, percent_change_1h, percent_change_24h, percent_change_7d);
        }
    } // end of CoinMarketCapTicker

    public class CoinMarketCapGlobalData : CryptoTools.Net.NullableObject
    {
        public decimal total_market_cap_usd { get; set; }
        public decimal total_24h_volume_usd { get; set; }
        public decimal bitcoin_percentage_of_market_cap { get; set; }
        public int active_currencies { get; set; }
        public int active_assets { get; set; }
        public int active_markets { get; set; }
        public long last_updated { get; set; }

        public bool IsNull => false;

        public override string ToString()
        {
            string updateTime = FromTimestampSeconds((int)last_updated).ToString("yyyy-MM-dd HH:mm:ss");
            return string.Format("{0} GLOBAL_TOTALS cap:${1:#,###} vol:{2:#,###}  btc%:{3}  currencies:{4:#,###} assets:{5:#,###} markets:{6:#,###}", updateTime, total_market_cap_usd, total_24h_volume_usd, bitcoin_percentage_of_market_cap, active_currencies, active_assets, active_markets);
        }
    } // end of CoinMarketCapGlobalData

} // end of namespace
