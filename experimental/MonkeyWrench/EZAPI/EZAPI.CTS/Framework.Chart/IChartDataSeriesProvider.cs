using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Chart
{
    public interface IChartDataProvider
    {
        event ezDataLoadCompleteEventHandler DataLoadComplete;
        event ezDataSeriesUpdatedEventHandler DataSeriesUpdated;
        string Name { get; }
        EZChartDataSeries ChartData { get; }
        object ChartIdentifier { get; }
        ezBarInterval BarInterval { get; }
        DateTime StartDate { get; }
        DateTime EndDate { get; }
        ezSessionTimeRange Session { get; }
        void LoadRealTimeChartData(object chartIdentifier, DateTime startDate);
        void LoadHistoricalChartData(object chartIdentifier, DateTime startDate, DateTime endDate);
        void Lock();
        void Unlock();

    } // interface
} // namespace
