using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Toolbox.Serialization;
using MonkeyLightning.DataProvider;

namespace MonkeyLightning.Framework.Xml
{
    public class XmlSaveRule
    {
        public XmlSaveRule()
        {
            RuleConditions = new List<XmlSaveRuleCondition>();
        }

        public string Name { get; set; }
        public bool Active { get; set; }
        public BooleanRuleCombination RuleCombination { get; set; }
        public TradeRuleType RuleType { get; set; }

        public List<XmlSaveRuleCondition> RuleConditions { get; set; }

    } // class
} // namespace
