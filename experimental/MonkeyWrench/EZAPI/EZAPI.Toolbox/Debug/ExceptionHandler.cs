using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EZAPI.Toolbox.Debug
{
    public static class ExceptionHandler
    {
        public static void TraceException(Exception ex)
        {
            Trace.WriteLine(string.Format("EXCEPTION HANDLER: {0}", ex.Message));
        }

        public static void TraceExceptionDetail(Exception ex)
        {
            Trace.WriteLine(string.Format("EXCEPTION HANDLER: {0}", ex.Message));
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                Trace.WriteLine(string.Format("EXCEPTION HANDLER: {0}", ex.Message));
            }
        }

    } // class
} // namespace
