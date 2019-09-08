using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Toolbox.Time
{
    public interface ITimeRange
    {
        bool ContainsTime(SimpleTime timeToCheck);
    } // interface
} // namespace
