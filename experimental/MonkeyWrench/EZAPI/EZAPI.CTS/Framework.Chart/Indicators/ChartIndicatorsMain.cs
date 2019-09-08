using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;

namespace EZAPI.Framework.Chart.Indicators
{
    public class ChartIndicatorsMain
    {
        public IndicatorMap Indicators { get; private set; }

        /// <summary>
        /// Define the available indicators and store them in a dictionary with the indicator name as the key.
        /// </summary>
        public ChartIndicatorsMain()
        {
            Indicators = new IndicatorMap();

            Indicators.Add(new KeltnerChannels("Keltner Channel"));
            Indicators.Add(new MovingAverage("Moving Average"));
            Indicators.Add(new ExponentialMovingAverage("Exponential Moving Average"));
            Indicators.Add(new TriangularMovingAverage("Triangular Moving Average"));
            Indicators.Add(new WeightedMovingAverage("Weighted Moving Average"));
            Indicators.Add(new TripleExponentialMovingAverage("Triple Exponential Moving Average"));
            Indicators.Add(new BollingerBands("Bollinger Bands"));
            Indicators.Add(new Envelopes("Envelopes"));
            Indicators.Add(new MedianPrice("Median Price"));
            Indicators.Add(new TypicalPrice("Typical Price"));
            Indicators.Add(new WeightedClose("Weighted Close"));
            //Indicators.Add(new Forecasting("Forecasting"));

        }

        public Indicator this[string indicatorName]
        {
            get
            {
                return Indicators[indicatorName];
            }
        }

    } // class
} // namespace
