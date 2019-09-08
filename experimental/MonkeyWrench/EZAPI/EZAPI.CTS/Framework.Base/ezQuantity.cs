using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Base
{
    public class ezQuantity
    {

        int intValue = int.MinValue;

        private ezQuantity()
        {

        }

        public int ToInt()
        {
            return intValue;
        }

        static public ezQuantity FromInt(object instrument, int qty)
        {
            var quantity = new ezQuantity();
            quantity.intValue = qty;
            return quantity;
        }

        static public ezQuantity Invalid
        {
            get { return new ezQuantity(); }
        }

        public bool IsValid
        {
            get { return intValue != int.MinValue; }
        }

        public override string ToString()
        {
            return intValue.ToString();
        }

        public static implicit operator int(ezQuantity quantity)
        {
            return quantity.ToInt();
        }



    } // class
} // namespace
