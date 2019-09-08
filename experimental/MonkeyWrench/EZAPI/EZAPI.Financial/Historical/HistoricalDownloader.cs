using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZAPI.Financial.Historical
{
    public abstract class HistoricalDownloader
    {
        public abstract List<HistoricalStock> DownloadData(string ticker, DateTime startDate);
        public abstract List<HistoricalStock> DownloadData(string ticker, DateTime startDate, DateTime endDate);
    } // class
} // namespace
