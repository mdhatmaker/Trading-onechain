using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonkeyLightning
{
    public class TradeRule
    {
        public bool Active { get; set; }
        public string Name { get; set; }

        public TradeRule(string ruleName)
        {
            Name = ruleName;
        }


    } // class
} // namespace
