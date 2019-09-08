#define DEEPDEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZAPI;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Framework.Chart;
using EZAPI.Toolbox.Debug;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public class DataProviderMomentum : DataProviderBase
    {
        public override string Name { get { return "MOMENTUM"; } }
        public override string DisplayFormat { get { return "{0}"; } }
        public override string Description { get { return "Tracks the 'momentum' of a particular trading market by examining volume and price action. Markets moving UP will have a POSITIVE momentum number. Markets moving DOWN will have a NEGATIVE momentum number. The higher the magnitude of the momentum number, the more movement/trading in the market."; } }
        public override string EnglishDescription { get { return "'Momentum' of a market (based on price movement AND volume)"; } }
        public override string[] PropertyNames { get { return new string[] {
            "SimpleDisplayFormat",  // display format to use when printing this data provider's value to the DPDisplay
            "InstrumentKey",    // instrument key for the trading instrument to use to calculate momentum
            "SeedTime",         // amount of time in minutes (decimal is accepted) that the indicator must "seed" before it is ready
            "AverageVolumePerTimePeriod",   // average volume traded in a typical time period that is...
            "TimePeriodLengthMinutes",      // # of minutes length of our "average" time period.
        }; } }

        //public override event ValueUpdateEventHandler DataUpdate;

        DPDisplaySimpleValue uiControl;
        DPModifyMomentum uiModifyControl;
        APIMain api;
        string selectedMarketData = null;
        EZInstrument currentInstrument = null;

        double momentum;  // calculate our momentum indicator in this variable.
        int averageVolumePerTimePeriod; // average volume in each of the time periods
        double timePeriodLength;        // length of our "average" time period in minutes (can use fractional minutes - it's a double)
        ezPrice lastPrint;
        ezQuantity lastPrintQty;
        int lastPrintTotalVolume;
        VolumeTimeFrames timeFrames;

        public DataProviderMomentum() : base()
        {
            // For the momentum indicator, we will start off assuming we are in the
            // "collecting data" state (which will change to "READY" when the DataProvider
            // has enough data that it can begin returning calculations.
            //dataProviderState = DataProviderState.COLLECTING_DATA;

            // Start off with ZERO momentum. As the indicator changes, POSITIVE values means market moving UP with
            // momentum. NEGATIVE values means market moving DOWN with momentum.
            momentum = 0.0;

            uiControl = null;
            uiModifyControl = null;
            DataUpdate += DataProviderMarketData_DataUpdate;

            lastPrint = null;
            lastPrintQty = null;
            lastPrintTotalVolume = 0;

            timeFrames = new VolumeTimeFrames();

            api = APIMain.Instance;
        }

        void DataProviderMarketData_DataUpdate(object sender, ValueUpdateEventArgs e)
        {
            if (uiControl != null) uiControl.Value = e.UpdatedValue;
        }

        private void DisconnectDisplayUIEvents()
        {
            DataUpdate -= DataProviderMarketData_DataUpdate;
        }

        public override void ClosingDisplayUI()
        {
            DisconnectDisplayUIEvents();
        }

        public override DPDisplayControl GetDisplayUserInterface()
        {
            if (uiControl == null)
                uiControl = new DPDisplaySimpleValue(PropertyValues);

            uiControl.InitializeControl();

            return uiControl;
        }

        public override DPModifyControl GetModifyUserInterface()
        {
            if (uiModifyControl == null)
                uiModifyControl = new DPModifyMomentum(PropertyValues);

            uiModifyControl.InitializeControl();

            return uiModifyControl;
        }

        public override void UpdateProviderFromPropertyValues()
        {
            //selectedMarketData = prop["QuoteItem"];
            ezInstrumentKey instrumentKey = APIFactory.InstrumentKeyFromString(prop["InstrumentKey"]);

            // Get historical data for 1-hour "bars".
            EZInstrument instrument = APIMain.InstrumentFromKey(instrumentKey);
            zChartInterval interval = zChartInterval.Hour;
            int period = 1;

            // TODO analyze different time periods (ex: different hours of the trading session) to 
            // get a better "AverageVolumePerTimePeriod" calculation.
            EZChartDataSeries historicalData = api.RequestHistoricalData(instrument, interval, period);

            if (historicalData == null)
            {
                averageVolumePerTimePeriod = prop["AverageVolumePerTimePeriod"] ?? 0;
                timePeriodLength = prop["TimePeriodLengthMinutes"] ?? 0.0;
            }
            else
            {
                int totalVolume = 0;
                int volumeCount = 0;
                foreach (ezBarDataPoint dp in historicalData.TradeBars)
                {
                    totalVolume += dp.Volume;
                    ++volumeCount;
                }
                double averageVolumePerHour = (double)totalVolume / (double)volumeCount;

                timePeriodLength = 5.0; // five minutes
                averageVolumePerTimePeriod = (int) Math.Round(averageVolumePerHour / 20.0);
            }

            currentInstrument = APIMain.InstrumentFromKey(instrumentKey);
            //api.SubscribeToInstrument(instrument.Key);
            api.OnInsideMarketUpdate += api_OnInsideMarketUpdate;
            api_OnInsideMarketUpdate(currentInstrument);
        }

        void api_OnInsideMarketUpdate(EZInstrument instrument)
        {
            if (instrument == null || currentInstrument == null || instrument.Key != currentInstrument.Key)
                return;

            /*if (selectedMarketData.Equals("Bid Price"))
                UpdateDataValue(instrument.Bid.ToString());
            else if (selectedMarketData.Equals("Bid Quantity"))
                UpdateDataValue(instrument.BidQty.ToString());
            else if (selectedMarketData.Equals("Offer Price"))
                UpdateDataValue(instrument.Ask.ToString());
            else if (selectedMarketData.Equals("Offer Quantity"))
                UpdateDataValue(instrument.AskQty.ToString());*/
            
            ezPrice price = instrument.Last;
            ezQuantity qty = instrument.LastQty;

            // Check if the price has moved.
            if (price != lastPrint)
            {
                Spy.Print("{0} : {1}", price, lastPrint);
                //StoreLastPrice(price);
                lastPrint = price;
                timeFrames.StoreTradeVolume(qty);
                UpdateDataValue(timeFrames.VolumeInTimePeriod(TimeSpan.FromMinutes(5)));
                lastPrintTotalVolume = instrument.LastTotalVolume;
            }
            else // last price is the same - check to see if another trade occurred at the same price
            {
                if (instrument.LastTotalVolume > lastPrintTotalVolume)
                {
                    timeFrames.StoreTradeVolume(qty);
                    UpdateDataValue(timeFrames.VolumeInTimePeriod(TimeSpan.FromMinutes(5)));
                    lastPrintTotalVolume = instrument.LastTotalVolume;
                }
            }


            //UpdateDataValue(instrument.LastQty.ToString());

            /*this.Invoke((MethodInvoker)delegate
            {
                lblBid1.Text = instrument.Bid.ToString();
                lblBidVol1.Text = instrument.BidQty.ToString();
                lblOffer1.Text = instrument.Ask.ToString();
                lblOfferVol1.Text = instrument.AskQty.ToString();
                lblLast1.Text = instrument.Last.ToString();
                lblLastVol1.Text = instrument.LastQty.ToString();
                lblTotalVol1.Text = instrument.Volume.ToString();
                lblNet1.Text = instrument.NetPos.ToString();
                lblBuys1.Text = instrument.NetBuys.ToString();
                lblSells1.Text = instrument.NetSells.ToString();
            });*/
        }

        public override RuleValueType ValueType
        {
            get { return RuleValueType.INT; }
        }

        private void UpdateDataValue(object updatedValue)
        {
            currentValue = updatedValue;
            FireValueUpdateEvent();
            //uiControl.Value = updatedValue;
        }

        public override object DataValue
        {
            get { return currentValue; }
        }
    } // class

    class VolumeTimeFrames
    {
        List<VolumeDataPoint> VolumeDataPoints;
        DateTime startTime;

        public VolumeTimeFrames()
        {
            VolumeDataPoints = new List<VolumeDataPoint>();
            startTime = DateTime.Now;
        }

        public void StoreTradeVolume(ezQuantity quantity)
        {
            var vdp = new VolumeDataPoint(DateTime.Now, quantity.ToInt());
            // Insert the newest data points at the "top" of the list.
            VolumeDataPoints.Insert(0, vdp);
        }

        /// <summary>
        /// Returns the volume traded in the last "time period" - where time period is 
        /// supplied as a parameter. Ex: If the method is called with a TimeSpan of one hour, 
        /// then it returns the total quantity traded in the past hour.
        /// </summary>
        /// <param name="timePeriod">TimeSpan representing the time period for which we will sum the volume traded</param>
        /// <returns>total quantity traded during the requested TimeSpan</returns>
        public int VolumeInTimePeriod(TimeSpan timePeriod)
        {
            DateTime beginTimePeriod = DateTime.Now.Subtract(timePeriod);

            int acc = 0; // accumulated volume traded
            for (int i = 0; i < VolumeDataPoints.Count; i++)
            {
                if (VolumeDataPoints[i].Time < beginTimePeriod)
                    break;
                else
                    acc += VolumeDataPoints[i].Quantity;                
            }

            return acc;
        }
    }

    class VolumeDataPoint
    {
        public DateTime Time { get; set; }
        public int Quantity { get; set; }

        public VolumeDataPoint(DateTime time, int qty)
        {
            Time = time;
            Quantity = qty;
        }
    }

} // namespace
