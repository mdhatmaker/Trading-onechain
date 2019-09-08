using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using TradingTechnologies.TTAPI;
using TradingTechnologies.TTAPI.Tradebook;
using System.Configuration;
using System.Media;

namespace EZAPI
{
    public partial class MainForm : Form
    {
        private const string VERSION = "v1.0";
        private const bool DEBUG = true;
        private UniversalLoginTTAPI apiInstance = null;
        //private bool apiLoginSuccess = false;

        private Dictionary<InstrumentKey, InstrumentInfo> instruments = new Dictionary<InstrumentKey, InstrumentInfo>();
        private Dictionary<InstrumentKey, InstrumentInfo> managedHedges = new Dictionary<InstrumentKey, InstrumentInfo>();
        private List<InstrumentInfo> managedHedgeList = new List<InstrumentInfo>();
        private Dictionary<string, List<Fill>> hashSymbols = new Dictionary<string, List<Fill>>();

        private TradeSubscription ts;

        public MainForm()
        {
            InitializeComponent();

            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            this.Text = "FillsDeluxe " + VERSION;
        }

        private void CreateFillsSubscription()
        {
            Message("Creating FillsSubscription...");

            FillsSubscription fs = new FillsSubscription(apiInstance.Session, Dispatcher.Current);
            fs.FillListStart += new EventHandler<FillListEventArgs>(fs_FillListStart);
            fs.FillBookDownload += new EventHandler<FillBookDownloadEventArgs>(fs_FillBookDownload);
            fs.FillListEnd += new EventHandler<FillListEventArgs>(fs_FillListEnd);
            fs.FillAdded += new EventHandler<FillAddedEventArgs>(fs_FillAdded);
            fs.FillDeleted += new EventHandler<FillDeletedEventArgs>(fs_FillDeleted);
            fs.FillAmended += new EventHandler<FillAmendedEventArgs>(fs_FillAmended);
            fs.Start();

            Message("FillsSubscription created.");
        }

        private void CreateTradeSubscription()
        {
            Message("Creating TradeSubscription...");
            /*
            InstrumentLookupSubscription req = new InstrumentLookupSubscription(apiInstance.Session, Dispatcher.Current, new ProductKey(MarketKey.Cme, ProductType.Future, "HG"), "Dec13");
            req.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(req_Update);
            req.Start();
            // We now have a more generic method to subscribe to instruments:
            SubscribeToInstrument(MarketKey.Cme, ProductType.Future, "HG", "Dec13");
            */

            SubscribeToInstrument("CME", "FUTURE", "HG", "Dec12");

            ts = new TradeSubscription(apiInstance.Session, Dispatcher.Current);
            ts.FillBookDownload += new EventHandler<FillBookDownloadEventArgs>(ts_FillBookDownload);
            ts.FillListEnd += new EventHandler<FillListEventArgs>(ts_FillListEnd);
            ts.FillListStart += new EventHandler<FillListEventArgs>(ts_FillListStart);
            ts.OrderBookDownload += new EventHandler<OrderBookDownloadEventArgs>(ts_OrderBookDownload);
            ts.OrderAdded += new EventHandler<OrderAddedEventArgs>(ts_OrderAdded);
            ts.OrderUpdated += new EventHandler<OrderUpdatedEventArgs>(ts_OrderUpdated);
            ts.OrderDeleted += new EventHandler<OrderDeletedEventArgs>(ts_OrderDeleted);
            ts.OrderFilled += new EventHandler<OrderFilledEventArgs>(ts_OrderFilled);
            ts.FillAmended += new EventHandler<FillAmendedEventArgs>(ts_FillAmended);
            ts.FillRecordAdded += new EventHandler<FillAddedEventArgs>(ts_FillRecordAdded);
            ts.AdminFillAdded += new EventHandler<FillAddedEventArgs>(ts_AdminFillAdded);
            ts.AdminFillDeleted += new EventHandler<FillDeletedEventArgs>(ts_AdminFillDeleted);
            ts.OrderStatusUnknown += new EventHandler<OrderStatusUnknownEventArgs>(ts_OrderStatusUnknown);
            ts.OrderRejected += new EventHandler<OrderRejectedEventArgs>(ts_OrderRejected);
            ts.Rollover += new EventHandler<RolloverEventArgs>(ts_Rollover);
            ts.TradeSubscriptionReset += new EventHandler<TradeSubscriptionResetEventArgs>(ts_TradeSubscriptionReset);
            //ts.EnablePNL = true;
            //ts.ProfitLossChanged += new EventHandler<ProfitLossChangedEventArgs>(ts_ProfitLossChanged);
            ts.Start();

            Message("TradeSubscription created.");
        }
        
        // InstrumentDescriptor is my name for a string with all the instrument info jammed together
        // (using '|' as a separator).
        // SubscribeToInstrument("CME|FUTURE|HG|Dec13");
        private InstrumentInfo SubscribeToInstrument(string instrumentDescriptor)
        {
            string[] parts = instrumentDescriptor.Split(new char[] { '|' });
            string marketKeyStr = parts[0];
            string productTypeStr = parts[1];
            string productStr = parts[2];
            string contract = parts[3];
            ProductKey productKey = new ProductKey(marketKeyStr, productTypeStr, productStr);
            SubscribeToInstrument(productKey, contract);

            InstrumentInfo info = new InstrumentInfo(instrumentDescriptor);
            info.ProductKey = productKey;
            info.Contract = contract;
            
            return info;
        }

        // SubscribeToInstrument("CME", "FUTURE", "HG", "Dec13");
        private void SubscribeToInstrument(string marketKeyStr, string productTypeStr, string productStr, string contract)
        {
            ProductKey productKey = new ProductKey(marketKeyStr, productTypeStr, productStr);
            SubscribeToInstrument(productKey, contract);
        }

        // SubscribeToInstrument(MarketKey.Cme, ProductType.Future, "HG", "Dec13");
        private void SubscribeToInstrument(MarketKey marketKey, ProductType productType, string product, string contract)
        {
            ProductKey productKey = new ProductKey(marketKey, productType, product);
            SubscribeToInstrument(productKey, contract);
        }

        private void SubscribeToInstrument(ProductKey productKey, string contract)
        {
            InstrumentLookupSubscription req = new InstrumentLookupSubscription(apiInstance.Session, Dispatcher.Current, productKey, contract);

            req.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(req_Update);
            req.Start();
        }

        #region TRADE SUSCRIPTION P&L
        void ts_ProfitLossChanged(object sender, ProfitLossChangedEventArgs e)
        {
            ProfitLossValue openPL = e.TradeSubscription.OpenProfitLoss;
            ProfitLossValue realizedPL = e.TradeSubscription.RealizedProfitLoss;
            ProfitLossValue PL = e.TradeSubscription.ProfitLoss;
            //ProfitLossStatistics ProfitLossStatistics = e.TradeSubscription.ProfitLossStatistics;
            // NOTE: Only use ProfitLossStatistics for a tradesubscription that deals with one
            // instrument (i.e. InstrumentTradeSubscription or one TradeSubscriptionInstrumentFilter)
        }
        #endregion

        #region TRADE SUBSCRIPTION STATUS
        void ts_TradeSubscriptionReset(object sender, TradeSubscriptionResetEventArgs e)
        {
            throw new NotImplementedException();
        }

        void ts_Rollover(object sender, RolloverEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region TRADE SUBSCRIPTION ORDERS
        void ts_OrderRejected(object sender, OrderRejectedEventArgs e)
        {
            Console.WriteLine("OrderRejected");
            Console.WriteLine(OrderToString(e.Order));
        }

        void ts_OrderStatusUnknown(object sender, OrderStatusUnknownEventArgs e)
        {
            throw new NotImplementedException();
        }

        void ts_OrderFilled(object sender, OrderFilledEventArgs e)
        {
            //Console.WriteLine("OrderFilled");
        }

        void ts_OrderDeleted(object sender, OrderDeletedEventArgs e)
        {
            //Console.WriteLine("OrderDeleted");
        }

        void ts_OrderUpdated(object sender, OrderUpdatedEventArgs e)
        {
            //Console.WriteLine("OrderUpdated");
            ManageHedgedOrders(e.NewOrder);
        }

        void ts_OrderAdded(object sender, OrderAddedEventArgs e)
        {
            //Console.WriteLine("OrderAdded");
            ManageHedgedOrders(e.Order);
        }

        void ts_OrderBookDownload(object sender, OrderBookDownloadEventArgs e)
        {
            //Console.WriteLine("OrderBookDownload");
        }
        #endregion

        #region TRADE SUBSCRIPTION FILLS
        void ts_FillListStart(object sender, FillListEventArgs e)
        {
            //Console.WriteLine("FillListStart");
            //processFill("FillListStart");
        }

        void ts_FillListEnd(object sender, FillListEventArgs e)
        {
            //Console.WriteLine("FillListEnd");
            //processFill("FillListEnd");
        }

        void ts_FillBookDownload(object sender, FillBookDownloadEventArgs e)
        {
            //Console.WriteLine("FillBookDownload");
            //processFill("FillBookDownload");
        }

        void ts_AdminFillDeleted(object sender, FillDeletedEventArgs e)
        {
            Console.WriteLine("AdminFillDeleted");
            processFill(FillOriginator.ADMIN, FillAction.ADD, e.Fill);
        }

        void ts_AdminFillAdded(object sender, FillAddedEventArgs e)
        {
            Console.WriteLine("AdminFillAdded");
            //Console.WriteLine(FillToString(e.Fill));
            processFill(FillOriginator.ADMIN, FillAction.ADD, e.Fill);
        }

        void ts_FillRecordAdded(object sender, FillAddedEventArgs e)
        {
            //Console.WriteLine("FillRecordAdded");
            //Console.WriteLine(FillToString(e.Fill));
            processFill(FillOriginator.TRADER, FillAction.ADD, e.Fill);
        }

        void ts_FillAmended(object sender, FillAmendedEventArgs e)
        {
            //Console.WriteLine("FillAmended");
            //Console.WriteLine(FillToString(e.NewFill));
            processFill(FillOriginator.TRADER, FillAction.AMEND, e.NewFill);
        }
        #endregion

        void req_Update(object sender, InstrumentLookupSubscriptionEventArgs e)
        {
            Console.WriteLine("req_Update");
            if (e.Instrument != null && e.Error == null)
            {
                tslblInfo.Text = "SUBSCRIBED: " + e.Instrument.Name;
                Message("SUBSCRIBED: " + e.Instrument.Name);
                // if this instrument is one of our managed hedges, then add it
                // to the managed hedge dictionary
                foreach (InstrumentInfo info in managedHedgeList)
                {
                    if (info.ProductKey == e.Instrument.Product.Key && e.Instrument.Name.Contains(info.Contract)) // && info.Contract.Equals(e.Instrument.Name))
                    {
                        info.Instrument = e.Instrument;
                        info.InstrumentKey = e.Instrument.Key;
                        managedHedges.Add(e.Instrument.Key, info);
                    }
                }

                // iterate order feeds enabled for each instrument
                foreach (OrderFeed oFeed in e.Instrument.GetValidOrderFeeds())
                {
                    if (oFeed.IsTradingEnabled)
                    {
                        Console.WriteLine("Order feed {0} is enabled for trading", oFeed.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Order feed {0} is NOT enabled for trading", oFeed.ToString());
                    }
                }
                // successfully subscribed to an instrument so request prices

                PriceSubscription priceSub = new PriceSubscription(e.Instrument, Dispatcher.Current);
                priceSub.Settings = new PriceSubscriptionSettings(PriceSubscriptionType.InsideMarket);
                priceSub.FieldsUpdated += new FieldsUpdatedEventHandler(priceSub_FieldsUpdated);
                priceSub.Start();

                InstrumentTradeSubscription its = new InstrumentTradeSubscription(apiInstance.Session, Dispatcher.Current, e.Instrument);
                its.Start();
            }
            else if (e.IsFinal)
            {
                // Instrument was not found and TTAPI has given up looking for it
                tslblInfo.Text = "ERROR: " + e.Error.Message;
            }
        }

        #region FILLS SUBSCRIPTION CALLBACKS
        void fs_FillAmended(object sender, FillAmendedEventArgs e)
        {
            //Console.WriteLine("fs_FillAmended");
            //Console.WriteLine(FillToString(e.NewFill));
            processFill(FillOriginator.TRADER, FillAction.AMEND, e.NewFill);
        }

        void fs_FillDeleted(object sender, FillDeletedEventArgs e)
        {
            //Console.WriteLine("fs_FillDeleted");
            //Console.WriteLine(FillToString(e.Fill));
            processFill(FillOriginator.TRADER, FillAction.DELETE, e.Fill);
        }

        void fs_FillAdded(object sender, FillAddedEventArgs e)
        {
            //Console.WriteLine("fs_FillAdded");
            //Console.WriteLine(FillToString(e.Fill));
            processFill(FillOriginator.TRADER, FillAction.ADD, e.Fill);
        }

        void fs_FillListEnd(object sender, FillListEventArgs e)
        {
            //Console.WriteLine("fs_FillListEnd");
            //processFill("fs_FillListEnd");
        }

        void fs_FillBookDownload(object sender, FillBookDownloadEventArgs e)
        {
            //Console.WriteLine("fs_FillBookDownload");
            //processFill("fs_FillBookDownload");
        }

        void fs_FillListStart(object sender, FillListEventArgs e)
        {
            //Console.WriteLine("fs_FillListStart");
            //processFill("fs_FillListStart");
        }
        #endregion

        void priceSub_FieldsUpdated(object sender, FieldsUpdatedEventArgs e)
        {
            if (e.Error == null)
            {
                // Initial price info is sent as a snapshot
                if (e.UpdateType == UpdateType.Snapshot)
                {
                    foreach (FieldId id in e.Fields.GetFieldIds())
                    {
                        Field f = e.Fields[id];
                        // put code to process the field here
                        //Console.WriteLine(f.FieldId.ToString() + " : " + f.FormattedValue);
                    }
                }
                else
                {
                    // only some fields have changed (not snapshot like initial price update)
                    foreach (FieldId id in e.Fields.GetChangedFieldIds())
                    {
                        Field f = e.Fields[id];
                        // put code to process the field here
                        //Console.WriteLine("***" + f.FieldId.ToString() + " : " + f.FormattedValue);
                    }

                    // you can access specified field values directly...
                    Price bid = e.Fields.GetDirectBidPriceField().Value;
                    Quantity bidQty = e.Fields.GetBidMarketQuantityField().Value;

                }
            }
        }

        OrderProfile CreateOrderCopy(Order order, Instrument instrument)
        {
            ReadOnlyCollection<OrderFeed> feeds = instrument.GetValidOrderFeeds();
            OrderFeed feed = feeds[1];
            OrderProfile profile = new OrderProfile(feed, instrument);
            profile.AccountName = order.AccountName;
            profile.AccountType = order.AccountType;
            profile.BuySell = order.BuySell;
            profile.Destination = order.Destination;
            profile.FFT2 = order.FFT2;
            profile.FFT3 = order.FFT3;
            profile.GiveUp = order.GiveUp;
            profile.IsAutomated = order.IsAutomated;
            profile.LimitPrice = order.LimitPrice;
            profile.MinimumQuantity = order.MinimumQuantity;
            profile.Modifiers = order.Modifiers;
            profile.OpenClose = order.OpenClose;
            profile.OrderQuantity = order.OrderQuantity;
            profile.OrderTag = order.OrderTag;
            profile.OrderType = order.OrderType;
            profile.PriceCheck = order.PriceCheck;
            profile.Restriction = order.Restriction;
            profile.StopPrice = order.StopPrice;
            profile.StopTriggerQuantity = order.StopTriggerQuantity;
            profile.SubUserId = order.SubUserId;
            profile.TimeInForce = order.TimeInForce;
            profile.UserName = order.UserName;
            profile.UserTag = order.UserTag;

            return profile;
        }

        #region HELPER METHODS
        void DisplayOrderSuccess(bool b)
        {
            if (b == true)
            {
                //listOrders.Items.Insert(0, "Success!!!");
            }
            else
            {
                //listOrders.Items.Insert(0, "ERROR: SendOrder failed.");
            }
        }

        string FillToString(Fill fill)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" " + fill.BuySell.ToString() + " ");
            sb.Append(" " + fill.Quantity.ToInt().ToString() + " ");
            sb.Append(" " + fill.InstrumentKey + " ");
            sb.Append("@ " + fill.MatchPrice + " ");
            return (sb.ToString());
        }

        string OrderToString(Order order)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{" + order.BuySell.ToString() + "}");
            sb.Append("{" + order.OrderQuantity.ToString() + "}");
            sb.Append("{" + order.InstrumentKey + "}");
            sb.Append("{" + order.LimitPrice + "}");
            if (order.IsSynthetic)
                sb.Append(" SYNTHETIC ");
            if (order.IsParent)
                sb.Append("  PARENT  ");
            if (order.IsChild)
                sb.Append("  CHILD  ");
            return (sb.ToString());
        }

        void Message(string msg)
        {
            tslblInfo.Text = msg;
            if (DEBUG) Console.WriteLine(msg);
        }

        void ErrorMessage(string msg)
        {
            tslblInfo.Text = "ERROR: " + msg;
            if (DEBUG) Console.WriteLine("ERROR: " + msg);
        }
        #endregion

        #region DISPLAY FILLS and ORDERS
        void displayFill(DataGridView grid, FillBasic fb)
        {
            int rowIndex = grid.Rows.Add(fb.ShortTime, fb.Identifiers, fb.BuySell, fb.Quantity, fb.InstrumentName, fb.Price);
            if (tsitemAutoScroll.Checked)
                grid.FirstDisplayedScrollingRowIndex = grid.RowCount - 1;
            DataGridViewRow row = grid.Rows[rowIndex];
            if (fb.BuySell == BuySell.Buy)
            {
                row.DefaultCellStyle.BackColor = Color.Blue;
                row.DefaultCellStyle.ForeColor = Color.White;
            }
            else if (fb.BuySell == BuySell.Sell)
            {
                row.DefaultCellStyle.BackColor = Color.Red;
                row.DefaultCellStyle.ForeColor = Color.Black;
            }
            Message("FILL: " + fb);
        }

        void displayOrder(string orderMessage)
        {
            //listOrders.Items.Insert(0, orderMessage);
        }

        void displayOrder(Order order)
        {
            //listOrders.Items.Insert(0, OrderToString(order));
        }

        void displayOrder(string orderMessage, Order order)
        {
            //listOrders.Items.Insert(0, string.Format("{0} {1}", orderMessage, OrderToString(order)));
        }
        #endregion

        void ManageHedgedOrders(Order order)
        {
            if (!tsitemEnableHedging.Checked)
                return;

            // See if this order's instrument is in our managed hedges list
            foreach (InstrumentKey key in managedHedges.Keys)
            {
                InstrumentInfo info = managedHedges[key];
                if (order.InstrumentKey == info.InstrumentKey)
                {

                    //listOrders.Items.Insert(0, OrderToString(order));
                    if (order.IsChild && !order.IsSynthetic)
                    {
                        Instrument instrument = info.Instrument;
                        OrderProfile profile = CreateOrderCopy(order, instrument);
                        // try to delete the order and re-insert it
                        if (order.Delete())
                        {
                            DisplayOrderSuccess(instrument.Session.SendOrder(profile));
                        }
                    }
                    break;
                }

            }
        }
        
        void processFill(FillOriginator originator, FillAction action, Fill fill)
        {
            /*
            fill.BuySell;
            fill.FFT2;
            fill.FFT3;
            fill.FillKey;
            fill.FillType;
            fill.InstrumentKey;
            fill.IsHedge;   // autospreader hedge order
            fill.IsQuoting; // autospreader quoting order
            fill.MatchPrice;
            fill.Quantity;
            fill.SpreadId;
            fill.TransactionDateTime;
            */

            // If this is the first fill for this instrument, then add it to our list and
            // subscribe to the instrument updates.
            if (!instruments.ContainsKey(fill.InstrumentKey))
            {
                InstrumentLookupSubscription ils = new InstrumentLookupSubscription(apiInstance.Session, Dispatcher.Current, fill.InstrumentKey);
                ils.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(ils_Update);
                ils.Start();

                InstrumentInfo info = new InstrumentInfo(fill.InstrumentKey);
                instruments.Add(fill.InstrumentKey, info);
            }

            // Display the fill in the grid and play and sounds and/or send any messages related to
            // this fill.
            FillBasic fb = new FillBasic(fill);
            displayFill(fillGrid, fb);
            UpdateFillCount(fillGrid.Rows.Count);
            UpdateProfit(0);

            // See if this fill contains our '#' hashtag in either FFT field.
            string hashField = null;
            if (fill.FFT2.StartsWith("#"))
                hashField = fill.FFT2;
            if (fill.FFT3.StartsWith("#"))
                hashField = fill.FFT3;

            // If we found a valid '#' hashtag value, then put it in our dropdown combo box.
            if (hashField != null)
            {
                // Add this fill to our list of fills with this same hashsymbol
                if (!hashSymbols.ContainsKey(hashField))
                    hashSymbols.Add(hashField, new List<Fill>());
                List<Fill> fills = hashSymbols[hashField];
                fills.Add(fill);

                // Add this hashsymbol to our dropdown list if it is not already there
                if (!tscomboFFT.Items.Contains(hashField))
                {
                    tscomboFFT.Items.Add(hashField);
                }

                // If the user is filtering by this hash symbol then put this fill in the filter grid also
                string selectedHash = tscomboFFT.SelectedItem as string;
                if (selectedHash != null && selectedHash.Equals(hashField))
                {
                    displayFill(filteredGrid, fb);
                }
            }
        }

        void ils_Update(object sender, InstrumentLookupSubscriptionEventArgs e)
        {
            Console.WriteLine("ils_Update");
            if (e.Instrument != null && e.Error == null)
            {
                Message("SUBSCRIBED: " + e.Instrument.Name);

                // iterate order feeds enabled for each instrument
                foreach (OrderFeed oFeed in e.Instrument.GetValidOrderFeeds())
                {
                    if (oFeed.IsTradingEnabled)
                    {
                        Console.WriteLine("Order feed {0} is enabled for trading", oFeed.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Order feed {0} is NOT enabled for trading", oFeed.ToString());
                    }
                }
                // successfully subscribed to an instrument so request prices

                PriceSubscription priceSub = new PriceSubscription(e.Instrument, Dispatcher.Current);
                priceSub.Settings = new PriceSubscriptionSettings(PriceSubscriptionType.InsideMarket);
                priceSub.FieldsUpdated += new FieldsUpdatedEventHandler(priceSub_FieldsUpdated);
                priceSub.Start();

                InstrumentTradeSubscription its = new InstrumentTradeSubscription(apiInstance.Session, Dispatcher.Current, e.Instrument);
                its.EnablePNL = true;
                its.ProfitLossChanged += new EventHandler<ProfitLossChangedEventArgs>(its_ProfitLossChanged); 
                its.Start();

                instruments[e.Instrument.Key].Instrument = e.Instrument;
                instruments[e.Instrument.Key].TradeSubscription = its;
            }
            else if (e.IsFinal)
            {
                // Instrument was not found and TTAPI has given up looking for it
                ErrorMessage(e.Error.Message);
            }
        }

        void its_ProfitLossChanged(object sender, ProfitLossChangedEventArgs e)
        {
            Message("PROFIT: " + string.Format("{0}", e.TradeSubscription.ProfitLoss));
        }

        void UpdateFillCount(int count)
        {
            tslblFillCount.Text = "Fill count: " + count.ToString();
        }

        void UpdateProfit(double profit)
        {
            tslblProfit.Text = "Profit: $" + profit.ToString();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fillGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            filteredGrid.Rows.Clear();
            Console.WriteLine(fillGrid.Rows[e.RowIndex].Cells["ProductColumn"].Value);
            string instrumentKey = fillGrid.Rows[e.RowIndex].Cells["ProductColumn"].Value as string;
            foreach (DataGridViewRow row in fillGrid.Rows)
            {
                if (row.Cells["ProductColumn"].Value != null && row.Cells["ProductColumn"].Value.Equals(instrumentKey))
                {
                    filteredGrid.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, row.Cells[4].Value, row.Cells[5].Value);
                    filteredGrid.DefaultCellStyle.BackColor = row.DefaultCellStyle.BackColor;
                    filteredGrid.DefaultCellStyle.ForeColor = row.DefaultCellStyle.ForeColor;
                }
            }
        }

        #region TOOLSTRIP MENU ITEM CLICKS
        private void tsitemTextMessageAlert_Click(object sender, EventArgs e)
        {
            /*if (tsitemTextMessageAlert.Checked)
                tsitemTextMessageAlert.Checked = false;
            else
                tsitemTextMessageAlert.Checked = true;*/
        }

        private void tsitemiPhoneAlert_Click(object sender, EventArgs e)
        {
            if (tsitemiPhoneAlert.Checked || tsitemAndroidAlert.Checked)
                tsitemTextMessageBackup.Enabled = true;
            else
                tsitemTextMessageBackup.Enabled = false;
        }

        private void tsitemAndroidAlert_Click(object sender, EventArgs e)
        {
            if (tsitemiPhoneAlert.Checked || tsitemAndroidAlert.Checked)
                tsitemTextMessageBackup.Enabled = true;
            else
                tsitemTextMessageBackup.Enabled = false;
        }

        private void tsitemSendTestMessage_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> settings = messageSettingsForm.GetSettings(); 

            string host = settings["HOST"];
            int port = int.Parse(settings["PORT"]); // should be 465 or 587
            string username = settings["EMAIL_USERNAME"];
            string password = settings["EMAIL_PASSWORD"];
            string fromEmail = settings["EMAIL_ADDRESS"];

            Messaging msg = new Messaging(host, port, username, password, fromEmail);
            
            string toEmail = fromEmail.Clone() as string;
            string body = "This is a test from Fills Deluxe";
            string subject = "Fills Deluxe Test";
        
            msg.SendMail(toEmail, body, subject);

            string carrier = settings["CARRIER"];
            string phoneNumber = settings["PHONE_NUMBER"];
            CellularRecipient recipient = new CellularRecipient(carrier, phoneNumber);
            msg.SendTextMessage(recipient, "This is a test from Fills Deluxe", "Fills Deluxe Test");
        }

        private void tsitemAutoScroll_Click(object sender, EventArgs e)
        {

        }

        private void tsitemEnableSounds_Click(object sender, EventArgs e)
        {

        }

        private void tscomboFFT_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tag = tscomboFFT.SelectedItem as string;
            if (tag == null)
                return;

            Console.WriteLine("TAG: " + tag);

            filteredGrid.Rows.Clear();
            foreach (Fill fill in hashSymbols[tag])
            {
                FillBasic fb = new FillBasic(fill);
                displayFill(filteredGrid, fb);
            }
        }
        #endregion

        private void tsbtnInfo_Click(object sender, EventArgs e)
        {
            (new AboutBox()).ShowDialog(this);
        }

        private void tsbtnHelp_Click(object sender, EventArgs e)
        {

        }

    } // class
} // namespace
