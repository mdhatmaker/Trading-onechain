using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Toolbox.Serialization
{
    public class Memento
    {
        SerializableDictionary<string, string> storeObjects;

        public Memento()
        {
            storeObjects = new SerializableDictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            storeObjects[key] = value;
        }

        public string this[string key]
        {
            get { return storeObjects[key]; }
        }

    } // class
} // namespace
