using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyLightning.DataProvider
{
    //public enum RuleValueProvider { USER_NUMBER, USER_TEXT, MARKET_DATA, CHART, RANDOM };
    public enum RuleValueType { INT, DOUBLE, TEXT, TIME, TIME_RANGE, TIME_SPAN };

    public delegate void ValueUpdateEventHandler(object sender, ValueUpdateEventArgs e);

    public class ValueUpdateEventArgs
    {
        public object UpdatedValue { get { return updatedValue; } }

        private object updatedValue;

        public ValueUpdateEventArgs(object updatedValue)
        {
            this.updatedValue = updatedValue;
        }
    } // class
} // namespace
