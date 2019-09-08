using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Toolbox.Serialization;

namespace MonkeyLightning.DataProvider
{
    public class XmlSaveDataProvider
    {
        public XmlSaveDataProvider()
        {
        }

        public string Name { get; set; }
        public object CurrentValue { get; set; }
        public SerializableDictionary<string, ValueString> KeyValuePairs { get; set; }

    } // class
} // namespace
