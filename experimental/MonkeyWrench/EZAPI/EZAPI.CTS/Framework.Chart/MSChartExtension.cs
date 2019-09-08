using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;

namespace EZAPI.Framework.Chart
{
    public static class MSChartExtension
    {
        public static void RemoveLastPoint(this MSChart.Series series)
        {
            series.Points.RemoveAt(series.Points.Count - 1);
        }

        public static void RemoveFirstPoint(this MSChart.Series series)
        {
            series.Points.RemoveAt(0);
        }

        public static MSChart.DataPoint GetFirstPoint(this MSChart.Series series)
        {
            return series.Points[0];
        }

        public static MSChart.DataPoint GetLastPoint(this MSChart.Series series)
        {
            return series.Points[series.Points.Count - 1];
        }

        public static void InsertFirst(this MSChart.Series series, MSChart.DataPoint dp)
        {
            series.Points.Insert(0, dp);
        }

        public static void InsertLast(this MSChart.Series series, MSChart.DataPoint dp)
        {
            series.Points.Add(dp);
        }

        /*public static MSChart.CustomLabel GetFirstLabel(this MSChart.Axis axis)
        {
            if (axis.CustomLabels.Count > 0)
                return axis.CustomLabels[0];
            else
                return null;
        }

        public static MSChart.CustomLabel GetLastLabel(this MSChart.Axis axis)
        {
            if (axis.CustomLabels.Count > 0)
                return axis.CustomLabels[axis.CustomLabels.Count - 1];
            else
                return null;
        }*/

    } // class
} // namespace
