#define LOGENABLED
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EZAPI.Toolbox.Debug
{
    public static class Log
    {
        [Conditional("LOGENABLED")]
        public static void Print(string msg, params object[] values)
        {
            Console.WriteLine("**LOG** " + msg, values);
        }
    } // class
} // namespace
