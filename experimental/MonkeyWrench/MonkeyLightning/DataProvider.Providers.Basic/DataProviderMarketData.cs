using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZAPI;
using EZAPI.Framework;
using EZAPI.Framework.Base;

namespace MonkeyLightning.DataProvider.Providers.Basic
{
    public class DataProviderMarketData : DataProviderBase
    {
        //public static readonly string Name = "MARKET DATA";
        public override string DisplayFormat { get { return "{0}"; } }
        public override string Name { get { return "MARKET DATA"; } }
        public override string Description { get { return "Allows you to choose a piece of market data (i.e. BidPrice, AskQuantity, LastPrice, etc.)"; } }
        public override string EnglishDescription { get { return prop["InstrumentKey"] + ":" + prop["QuoteItem"]; } }
        public override string[] PropertyNames { get { return new string[] { "SimpleDisplayFormat", "InstrumentKey", "QuoteItem" }; } }

        //public override event ValueUpdateEventHandler DataUpdate;

        DPDisplaySimpleValue uiControl;
        DPModifyMarketData uiModifyControl;
        APIMain api;                
        string selectedMarketQuoteItem = null;
        EZInstrument currentInstrument = null;

        public DataProviderMarketData() : base()
        {
            uiControl = null;            
            uiModifyControl = null;
            DataUpdate += DataProviderMarketData_DataUpdate;

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
                uiModifyControl = new DPModifyMarketData(PropertyValues);

            uiModifyControl.InitializeControl();

            return uiModifyControl;
        }

        public override void UpdateProviderFromPropertyValues()
        {
            selectedMarketQuoteItem = prop["QuoteItem"];
            ezInstrumentKey instrumentKey = APIFactory.InstrumentKeyFromString(prop["InstrumentKey"]);
            currentInstrument = APIMain.InstrumentFromKey(instrumentKey);
            //api.SubscribeToInstrument(instrument.Key);
            api.OnInsideMarketUpdate += api_OnInsideMarketUpdate;
            api_OnInsideMarketUpdate(currentInstrument);
        }

        void api_OnInsideMarketUpdate(EZInstrument instrument)
        {
            if (instrument == null || currentInstrument == null || instrument.Key != currentInstrument.Key)
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
            get { return RuleValueType.DOUBLE; }
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
} // namespace
