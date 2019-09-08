using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace EZAPI.Financial.MarketData
{
    public class MarketDataStock
    {
        public string Symbol { get; private set; }
        public string PrettySymbol { get; private set; }
        public string Company { get; private set; }
        public string Exchange { get; private set; }
        public string ExchangeTimeZone { get; private set; }
        public string ExchangeUTCOffset { get; private set; }

        public double Last { get; private set; }
        public double High { get; private set; }
        public double Low { get; private set; }

        /*<symbol data="AAPL"/>
        <pretty_symbol data="AAPL"/>
        <symbol_lookup_url data="/finance?client=ig&amp;q=AAPL"/>
        <company data="Apple Inc."/>
        <exchange data="Nasdaq"/>
        <exchange_timezone data="ET"/>
        <exchange_utc_offset data="+05:00"/>
        <exchange_closing data="960"/>
        <divisor data="2"/>
        <currency data="USD"/>
        <last data="321.90"/>
        <high data="323.48"/>
        <low data="321.33"/>
        <volume data="3753391"/>
        <avg_volume data="12682"/>
        <market_cap data="295281.14"/>
        <open data="323.00"/>
        <y_close data="323.66"/>
        <change data="-1.76"/>
        <perc_change data="-0.54"/>
        <delay data="0"/>
        <trade_timestamp data="54 seconds ago"/>
        <trade_date_utc data="20101231"/>
        <trade_time_utc data="184724"/>
        <current_date_utc data="20101231"/>
        <current_time_utc data="184818"/>
        <symbol_url data="/finance?client=ig&amp;q=AAPL"/>
        <chart_url data="/finance/chart?q=NASDAQ:AAPL&amp;tlf=12"/>
        <disclaimer_url data="/help/stock_disclaimer.html"/>
        <ecn_url data=""/>
        <isld_last data="323.00"/>
        <isld_trade_date_utc data="20101231"/>
        <isld_trade_time_utc data="143001"/>
        <brut_last data=""/>
        <brut_trade_date_utc data=""/>
        <brut_trade_time_utc data=""/>
        <daylight_savings data="false"/>*/

        public MarketDataStock(XDocument doc)
        {
            Company = GetData(doc, "company");
            Exchange = GetData(doc, "exchange");
            Last = Convert.ToDouble(GetData(doc, "last"));
            High = Convert.ToDouble(GetData(doc, "high"));
            Low = Convert.ToDouble(GetData(doc, "low"));
        }

        private string GetData(XDocument doc, string name)
        {
            return doc.Root.Element("finance").Element(name).Attribute("data").Value;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4} {5}", Symbol, PrettySymbol, Company, High, Low, Last);
        }
    } // class
} // namespace
