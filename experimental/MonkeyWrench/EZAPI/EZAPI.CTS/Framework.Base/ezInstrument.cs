using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Base
{
    public class ezInstrument
    {
        public ezProduct Product
        {
            get { return null; }
        }

        public ezInstrumentKey Key
        {
            get { return null; }
        }
        
        public ReadOnlyCollection<ezOrderFeed> GetValidOrderFeeds()
        {            
            return null;
        }

        public string Name
        {
            get { return "instrument"; }
        }

        public ezSession Session
        {
            get { return null; }
        }

    } // class
} // namespace
