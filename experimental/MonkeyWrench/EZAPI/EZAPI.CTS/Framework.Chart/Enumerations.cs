using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Chart
{
    public delegate void ezDataLoadCompleteEventHandler(object sender, ezDataLoadCompleteEventArgs e);
    public delegate void ezDataSeriesUpdatedEventHandler(object sender, ezDataSeriesUpdatedEventArgs e);

    public enum zChartInterval { Tick, Volume, Second, Minute, Hour, Day, Week, Month, Year };

    public enum zDataLoadStatus { Success, Failed };

    public enum zChartLineStyle { Dash, DashDot, DashDotDot, Dot, Solid, NotSet };

    /*
     * EXAMPLE OF "ENUM" WITH IMPLICIT CONVERSION
     * 
    public sealed class zChartLineStyle
    {
        public static readonly zChartLineStyle Dash = new zChartLineStyle(1);
        public static readonly zChartLineStyle DashDot = new zChartLineStyle(2);
        public static readonly zChartLineStyle DashDotDot = new zChartLineStyle(3);
        public static readonly zChartLineStyle Dot = new zChartLineStyle(4);
        public static readonly zChartLineStyle NotSet = new zChartLineStyle(5);
        public static readonly zChartLineStyle Solid = new zChartLineStyle(6);

        public static readonly SortedList<byte, zChartLineStyle> Values = new SortedList<byte, zChartLineStyle>();
        private readonly byte Value;

        private zChartLineStyle(byte value)
        {
            this.Value = value;
            Values.Add(value, this);
        }

        public static implicit operator zChartLineStyle(byte value)
        {
            return Values[byte];
        }

        public static implicit operator byte(zChartLineStyle value)
        {
            return value.Value;
        }
    }*/

} // namespace
