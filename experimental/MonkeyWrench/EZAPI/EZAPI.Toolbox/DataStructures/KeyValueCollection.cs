using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Toolbox.DataStructures
{
    public class KeyValueCollection
    {
        private Dictionary<string, string> keyValuePairs;

        // Once the keyValuePairs Dictionary is created in the constructor, its set of valid
        // keys cannot be changed.
        //
        // This collection will compensate for capitalization (always using ALL CAPS for the property name lookup).
        //
        public KeyValueCollection(string[] propertyNames)
        {
            keyValuePairs = new Dictionary<string, string>();

            if (propertyNames != null)
            {
                foreach (string property in propertyNames)
                {
                    keyValuePairs[FormatKey(property)] = null;
                }
            }
        }

        public string this[string key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException("key", "The key cannot be null for this ControlValues dictionary.");
                else if (keyValuePairs.ContainsKey(FormatKey(key)))
                    return keyValuePairs[FormatKey(key)];
                else
                    throw new ArgumentOutOfRangeException(key, "The key is not valid for this ControlValues dictionary.");
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException("key", "The key cannot be null for this ControlValues dictionary.");
                else if (keyValuePairs.ContainsKey(FormatKey(key)))
                    keyValuePairs[FormatKey(key)] = value;
                else
                    throw new ArgumentOutOfRangeException(key, "The key is not valid for this ControlValues dictionary.");
            }
        }

        string FormatKey(string key)
        {
            return key.ToUpper().Trim();
        }
    } // class
} // namespace
