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

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public class DataProviderExcel : DataProviderBase
    {
        public override string DisplayFormat { get { return "{0}"; } }
        public override string Name { get { return "EXCEL WORKSHEET"; } }
        public override string Description { get { return "Allows pull data from a cell in an Excel worksheet. You can choose an existing Excel document or create a new one."; } }
        public override string EnglishDescription { get { return "TODO english description"; } }
        public override string[] PropertyNames { get { return new string[] { "Workbook", "Worksheet", "Cell" }; } }

        DPDisplaySimpleDescription uiControl;
        DPModifyExcel uiModifyControl;

        string selectedTradeDetailTag = null;
        string selectedMarketQuoteItem = null;
        string selectedMarketInstrumentKey = null;
        EZInstrument currentInstrument = null;
        APIMain api;

        public DataProviderExcel()
            : base()
        {
            uiControl = null;
            uiModifyControl = null;
            DataUpdate += DataProviderTradeDetail_DataUpdate;

            api = APIMain.Instance;
        }

        void DataProviderTradeDetail_DataUpdate(object sender, ValueUpdateEventArgs e)
        {
            if (uiControl != null) uiControl.Value = e.UpdatedValue;
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
                uiModifyControl = new DPModifyExcel(PropertyValues);

            uiModifyControl.InitializeControl();

            return uiModifyControl;
        }

        public override void UpdateProviderFromPropertyValues()
        {
            selectedTradeDetailTag = prop["TradeDetailTag"];

            //TODO look at TradeDetail to pick out the correct QuoteItem
            selectedMarketQuoteItem = "Last Price";

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
            if (instrument.Key != currentInstrument.Key)
                return;

            if (selectedMarketQuoteItem == null)
                return;

            if (selectedMarketQuoteItem.Equals("Bid Price"))
                UpdateDataValue(instrument.Bid.ToString());
            else if (selectedMarketQuoteItem.Equals("Bid Quantity"))
                UpdateDataValue(instrument.BidQty.ToString());
            else if (selectedMarketQuoteItem.Equals("Offer Price"))
                UpdateDataValue(instrument.Ask.ToString());
            else if (selectedMarketQuoteItem.Equals("Offer Quantity"))
                UpdateDataValue(instrument.AskQty.ToString());
            else if (selectedMarketQuoteItem.Equals("Last Price"))
                UpdateDataValue(instrument.Last.ToString());
            else if (selectedMarketQuoteItem.Equals("Last Quantity"))
                UpdateDataValue(instrument.LastQty.ToString());
        }

        public override void TradeDetailInitialized()
        {
            Console.WriteLine("***TRADE_DETAIL_UPDATED***");

            if (currentInstrument == null)
                SubscribeToInstrument(tradeCycle.TradeInstrument);
        }

        private void UpdateDataValue(object updatedValue)
        {
            decimal price = Convert.ToDecimal(updatedValue);
            decimal priceChange;
            if (tradeCycle == null || tradeCycle.EntryPriceAverage == 0M)
                priceChange = 0.0M;
            else
                priceChange = price - tradeCycle.EntryPriceAverage;

            currentValue = priceChange.ToString();
            FireValueUpdateEvent();
            //uiControl.Value = updatedValue;
        }

        public override RuleValueType ValueType
        {
            get
            {
                // TODO: look at this - maybe we need to update the value type dynamically depending on the trade detail data we are providing
                return RuleValueType.INT;
            }
        }

        public override object DataValue
        {
            get { return currentValue; }
        }


    } // class
} // namespace
