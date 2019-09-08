using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI;
using EZAPI.Framework;

namespace EZAPI.Framework.Base
{
    public class ezPrice
    {
        double doubleValue = double.NaN;
        EZOrder order = null;
        EZInstrument instrument = null;
        
        private ezPrice()
        {
        }

        private ezPrice(double price)
        {
            doubleValue = price;
        }

        static public ezPrice FromDouble(EZOrder order, double price)
        {
            var ezPrice = new ezPrice(price);
            ezPrice.order = order;
            return ezPrice;
        }

        static public ezPrice FromDouble(EZInstrument instrument, double price)
        {
            var ezPrice = new ezPrice(price);
            ezPrice.instrument = instrument;
            return ezPrice;
        }

        public double ToDouble()
        {
            return doubleValue;
        }

        public decimal ToDecimal()
        {
            return Convert.ToDecimal(doubleValue);
        }

        static public ezPrice Invalid
        {
            get { return new ezPrice(); }
        }

        public bool IsValid
        {
            get { return doubleValue != double.NaN; }
        }

        public override string ToString()
        {
            return doubleValue.ToString();
        }

        public static ezPrice operator ++(ezPrice price)
        {
            ezPrice ezp = APIMain.IncrementTick(price.instrument, price);
            return ezp;
        }

        public static ezPrice operator --(ezPrice price)
        {
            ezPrice ezp = APIMain.DecrementTick(price.instrument, price);
            return ezp;
        }

        public static implicit operator double(ezPrice price)
        {
            return price.ToDouble();
        }

        public static implicit operator decimal(ezPrice price)
        {
            return price.ToDecimal();
        }

    } // class
} // namespace
