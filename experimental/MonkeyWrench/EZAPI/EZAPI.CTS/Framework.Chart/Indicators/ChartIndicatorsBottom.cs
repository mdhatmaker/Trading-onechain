using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using MSChart = System.Windows.Forms.DataVisualization.Charting;
using EZAPI.Toolbox.DataStructures;
using EZAPI.Toolbox.Debug;

namespace EZAPI.Framework.Chart.Indicators
{
    public enum PadSide { PadLeft, PadRight }

    public class ChartIndicatorsBottom
    {
        public event Action<double> SlopeChanged;

        public double Slope { get { return trendLine.Slope; } }

        public IndicatorMap Indicators { get; private set; }


        private TrendLine trendLine;

        public ChartIndicatorsBottom()
        {
            Indicators = new IndicatorMap();

            Indicators.Add(new ChaikinOscillator("Chaikin Oscillator"));
            Indicators.Add(new AccumulationDistribution("Accumulation Distribution"));
            Indicators.Add(new AverageTrueRange("Average True Range"));
            Indicators.Add(new CommodityChannelIndex("Commodity Channel Index"));
            Indicators.Add(new DetrendedPriceOscillator("Detrended Price Oscillator"));
            Indicators.Add(new EaseOfMovement("Ease Of Movement"));
            Indicators.Add(new MassIndex("Mass Index"));
            Indicators.Add(new MoneyFlow("Money Flow"));
            Indicators.Add(new MACD("MACD"));
            Indicators.Add(new NegativeVolumeIndex("Negative Volume Index"));
            Indicators.Add(new PositiveVolumeIndex("Positive Volume Index"));
            Indicators.Add(new OnBalanceVolume("On Balance Volume"));
            Indicators.Add(new Performance("Performance"));
            Indicators.Add(new PriceVolumeTrend("Price Volume Trend"));
            Indicators.Add(new RateOfChange("Rate of Change"));
            Indicators.Add(new VolumeRateOfChange("Volume Rate of Change"));
            Indicators.Add(new RelativeStrengthIndex("Relative Strength Index"));
            Indicators.Add(new StandardDeviation("Standard Deviation"));
            Indicators.Add(new StochasticIndicator("Stochastic Indicator"));
            Indicators.Add(new VolatilityChaikins("Volatility Chaikins"));
            Indicators.Add(new VolumeOscillator("Volume Oscillator"));
            Indicators.Add(new WilliamsR("Williams %R"));
            trendLine = new TrendLine("Trend Line");
            Indicators.Add(trendLine);
            trendLine.SlopeChanged += trendLine_SlopeChanged;
        }

        void trendLine_SlopeChanged(double slope)
        {
            if (SlopeChanged != null) SlopeChanged(slope);
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
