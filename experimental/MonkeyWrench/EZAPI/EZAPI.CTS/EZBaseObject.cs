using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI
{
    public abstract class EZBaseObject
    {
        public string Tag { get; set; }
        public Dictionary<string, object> Tags;

        public EZBaseObject()
        {
            Tags = new Dictionary<string, object>();
        }

        public object this[string key]
        {
            get
            {
                return Tags[key];
            }
            set
            {
                Tags[key] = value;
            }
        }

    } // class
} // namespace
