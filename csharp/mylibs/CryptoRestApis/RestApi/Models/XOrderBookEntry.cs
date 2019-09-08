using System;

namespace CryptoRestApis.RestApi.Models
{
    public class XOrderBookEntry
    {
        public decimal Price { get; private set; }
        public decimal Amount { get; private set; }

        public XOrderBookEntry(decimal price, decimal amount)
        {
            this.Price = price;
            this.Amount = amount;
        }
    } // end of class XOrderBookEntry

} // end of namespace
