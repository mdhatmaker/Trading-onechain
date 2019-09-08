using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Framework.Base;

namespace EZAPI.Framework.Spread
{
    public class ezAutospreaderInstrument : ezInstrument
    {
        public zLaunchReturnCode LaunchToOrderFeed(ezOrderFeed feed)
        {
            return zLaunchReturnCode.CommunicationError;
        }

    } // class
} // namespace
