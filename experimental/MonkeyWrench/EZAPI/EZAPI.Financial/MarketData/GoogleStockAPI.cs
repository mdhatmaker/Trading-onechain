using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace EZAPI.Financial.MarketData
{
    public class GoogleStockAPI : IMarketData
    {
        public static GoogleStockAPI Instance
        {
            get
            {
                if (instance == null)
                    instance = new GoogleStockAPI();
                return instance;
            }
        }

        private static GoogleStockAPI instance;

        private GoogleStockAPI()
        {
            instance = null;
        }

        public MarketDataStock FetchQuote(string symbol)
        {
            string url = "http://www.google.com/ig/api?stock=" + HttpUtility.UrlEncode(symbol);
            XDocument doc = XDocument.Load(url);

            return new MarketDataStock(doc);
            //Company = GetData(doc, "company");
            //Exchange = GetData(doc, "exchange");
            //Last = Convert.ToDouble(GetData(doc, "last"));
            //High = Convert.ToDouble(GetData(doc, "high"));
            //Low = Convert.ToDouble(GetData(doc, "low"));
        }

        private string GetData(XDocument doc, string name)
        {
            return doc.Root.Element("finance").Element(name).Attribute("data").Value;
        }



    } // class
} // namespace
