using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZAPI.Financial.MarketData
{
    public interface IMarketData
    {
        MarketDataStock FetchQuote(string symbol);
    } // interface
} // namespace
