using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Toolbox.Serialization;
using MonkeyLightning.DataProvider;

namespace MonkeyLightning.Framework.Xml
{
    public class XmlSaveRuleCondition
    {
        public XmlSaveRuleCondition()
        {
        }

        public XmlSaveDataProvider Value1 { get; set; }
        public XmlSaveDataProvider Value2 { get; set; }
        public XmlSaveRuleComparison Compare { get; set; }

    } // class
} // namespace
