#define DEEPDEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZAPI;
using EZAPI.Data;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Toolbox;
using EZAPI.Toolbox.Debug;
using EZAPI.Toolbox.DataStructures;
using MonkeyLightning.DataProvider.Base;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public class DataProviderTradeDetail : DataProviderBase
    {
        public override string DisplayFormat { get { return "{0}"; } }
        public override string Name { get { return "TRADE DETAIL"; } }
        public override string Description { get { return "Allows you to select trade details such as P&L, price change since entry, time since entry, etc."; } }
        public override string EnglishDescription { get { return "TODO english description"; } }
        public override string[] PropertyNames { get { return new string[] { "TradeDetailTag", "SimpleDisplay", "InstrumentKey", "QuoteItem" }; } }

        DPDisplaySimpleDescription uiControl;
        DPModifyTradeDetail uiModifyControl;

        SelectedItemGroup itemGroup;
        string selectedTradeDetailTag = null;
        string selectedMarketQuoteItem = null;
        string selectedMarketInstrumentKey = null;
        RuleValueType dynamicRuleValueType = RuleValueType.INT;
        EZInstrument currentInstrument = null;
        APIMain api;

        public DataProviderTradeDetail() : base()
        {
            uiControl = null;            
            uiModifyControl = null;
            DataUpdate += DataProviderTradeDetail_DataUpdate;

            // Create the ItemGroup that will represent the choices of
            // Trade Detail available to the user.
            itemGroup = new SelectedItemGroup("TradeDetail");
            InitializeItemGroup();

            api = APIMain.Instance;
        }

        void DataProviderTradeDetail_DataUpdate(object sender, ValueUpdateEventArgs e)
        {
            if (uiControl != null) uiControl.Value = e.UpdatedValue;
        }

        void InitializeItemGroup()
        {
            itemGroup.AddItem(new SelectedItemDetail("FillVsLast", "Fill vs current Last Price", "", "LastPrice", "DOUBLE"));
            itemGroup.AddItem(new SelectedItemDetail("FillVsBid", "Fill vs current Bid Price", "", "BidPrice", "DOUBLE"));
            itemGroup.AddItem(new SelectedItemDetail("FillVsAsk", "Fill vs current Offer Price", "", "AskPrice", "DOUBLE"));
            itemGroup.AddItem(new SelectedItemDetail("FillVsMid", "Fill vs current Mid Price", "", "MidPrice", "DOUBLE"));
            itemGroup.AddItem(new SelectedItemDetail("ElapsedTime", "Elapsed Time since fill", "", "CurrentTime", "TIME_SPAN"));
        }

        public override void ClosingDisplayUI()
        {
            // Disconnect display UI events.
            DataUpdate -= DataProviderTradeDetail_DataUpdate;
        }

        public override DPDisplayControl GetDisplayUserInterface()
        {
            if (uiControl == null)
                uiControl = new DPDisplaySimpleDescription(PropertyValues);

            uiControl.InitializeControl();

            return uiControl;
        }

        public override DPModifyControl GetModifyUserInterface()
        {
            if (uiModifyControl == null)
                uiModifyControl = new DPModifyTradeDetail(PropertyValues);

            uiModifyControl.InitializeControl();

            return uiModifyControl;
        }

        public override void UpdateProviderFromPropertyValues()
        {
            selectedTradeDetailTag = prop["TradeDetailTag"];
            prop["SimpleDisplay"] = itemGroup[selectedTradeDetailTag].DisplayName;

            // Identify which piece of MarketData we will need to evaluate this
            // trade detail request (i.e. "LastPrice", "BidPrice", etc).
            selectedMarketQuoteItem = itemGroup[selectedTradeDetailTag].Text1;
            dynamicRuleValueType = Parse.ParseEnum<RuleValueType>(itemGroup[selectedTradeDetailTag].Text2);
            switch (dynamicRuleValueType)
            {
                case RuleValueType.DOUBLE:
                    currentValue = 0.0;
                    break;
                case RuleValueType.TIME_SPAN:
                    currentValue = new TimeSpan(0, 0, 0);
                    break;
                default:
                    break;
            }

            //selectedMarketQuoteItem = PropertyValues["QuoteItem"];
            //selectedMarketInstrumentKey = PropertyValues["InstrumentKey"];
            //ezInstrumentKey instrumentKey = APIFactory.InstrumentKeyFromString(uiModifyControl["InstrumentKey"]);
        }

        void SubscribeToInstrument(EZInstrument instrument)
        {
            // Look at our TradeDetailTag and determine the MarketQuoteItem that we are
            // interested in (i.e. LastPrice, BidPrice, AskPrice, etc.)
            //selectedMarketQuoteItem = quoteItem;

            //ezInstrumentKey instrumentKey = APIFactory.InstrumentKeyFromString(instrumentKeyStr);
            //currentInstrument = APIMain.InstrumentFromKey(instrumentKey);
            currentInstrument = instrument;
            api.OnInsideMarketUpdate += api_OnInsideMarketUpdate;
            api_OnInsideMarketUpdate(currentInstrument);
        }

        void api_OnInsideMarketUpdate(EZInstrument instrument)
        {
            if (instrument == null || currentInstrument == null || instrument.Key != currentInstrument.Key)
                return;

            if (selectedMarketQuoteItem == null)
                return;

            if (selectedMarketQuoteItem.Equals("BidPrice"))
                UpdateDataValuePrice(instrument.Bid.ToString());
            else if (selectedMarketQuoteItem.Equals("BidQty"))
                UpdateDataValuePrice(instrument.BidQty.ToString());
            else if (selectedMarketQuoteItem.Equals("AskPrice"))
                UpdateDataValuePrice(instrument.Ask.ToString());
            else if (selectedMarketQuoteItem.Equals("AskQuantity"))
                UpdateDataValuePrice(instrument.AskQty.ToString());
            else if (selectedMarketQuoteItem.Equals("LastPrice"))
                UpdateDataValuePrice(instrument.Last.ToString());
            else if (selectedMarketQuoteItem.Equals("LastQty"))
                UpdateDataValuePrice(instrument.LastQty.ToString());
            else if (selectedMarketQuoteItem.Equals("MidPrice"))
                UpdateDataValuePrice(instrument.MidPrice.ToString());
            else if (selectedMarketQuoteItem.Equals("CurrentTime"))
                UpdateDataValueTime();
        }

        public override void TradeDetailInitialized()
        {
            Spy.Print("***TradeDetail INITIALIZED***");
            
            if (currentInstrument == null && tradeCycle.TradeInstrument != null)
                SubscribeToInstrument(tradeCycle.TradeInstrument);
        }

        protected override void TradeCycleUpdated()
        {
            Spy.Print("***TradeDetail UPDATED***");

            if (currentInstrument == null && tradeCycle.TradeInstrument != null)
                SubscribeToInstrument(tradeCycle.TradeInstrument);            
        }

        private void UpdateDataValueTime()
        {
            TimeSpan elapsedTime;

            if (tradeCycle == null || tradeCycle.EntryStartTime == DateTime.MinValue)
                elapsedTime = new TimeSpan(0, 0, 0);
            else
                elapsedTime = DateTime.Now.Subtract(tradeCycle.EntryStartTime);

#if DEEPDEBUG
            if (tradeCycle == null)
                Spy.Print("TradeDetail is null");
            else
                Spy.Print("EntryStartTime = {0:h:mm:ss tt}", tradeCycle.EntryStartTime);
#endif

            //dynamicRuleValueType = RuleValueType.TIME_SPAN;
            currentValue = elapsedTime;
            FireValueUpdateEvent();
        }

        private void UpdateDataValuePrice(object updatedValue)
        {
            decimal price = Convert.ToDecimal(updatedValue);
            decimal priceChange;
            if (tradeCycle == null || tradeCycle.EntryPriceAverage == 0M)
                priceChange = 0.0M;
            else
                priceChange = price - tradeCycle.EntryPriceAverage;

#if DEEPDEBUG
            if (tradeCycle == null)
                Spy.Print("TradeDetail is null");
            else
                Spy.Print("EntryPriceAverage = {0}", tradeCycle.EntryPriceAverage);
#endif

            //dynamicRuleValueType = RuleValueType.DOUBLE;
            currentValue = priceChange.ToString();
            FireValueUpdateEvent();
        }

        public override RuleValueType ValueType
        {
            get
            {
                return dynamicRuleValueType;
            }
        }

        public override object DataValue
        {
            get { return currentValue; }
        }

        /// <summary>
        /// Since TimeSpan does not serialize with the XmlSerializer, we do a little
        /// conversion where we will save the Ticks of the TimeSpan (a long value) and
        /// restore by creating a TimeSpan FROM the ticks that we saved.
        /// </summary>
        public override object SerializeCurrentValue
        {
            get
            {
                if (currentValue is TimeSpan)
                {
                    TimeSpan span = (TimeSpan)currentValue;
                    return span.Ticks;
                }
                else
                {
                    return currentValue;
                }
            }
            set
            {
                if (value is long)
                {
                    long ticks = (long)value;
                    currentValue = new TimeSpan(ticks);
                }
                else
                {
                    currentValue = value;
                }
            }
        }



    } // class
} // namespace
