using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Toolbox.Serialization;

namespace EZAPI.Framework.Xml
{
    public class XmlSaveMarketList
    {
        public XmlSaveMarketList()
        {
            MarketKeyStrings = new List<string>();
        }

        public List<string> MarketKeyStrings { get; set; }

    } // class
} // namespace
