using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Base
{
    public class ezContract
    {
        public DateTime GetTradeDate(DateTime pdTime)
        {
            return DateTime.Now;
        }

        public string Description
        {
            get { return "Contract Description"; }
        }

    } // class
} // namesapce
