using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using EZAPI.Containers;

namespace EZAPI
{
    class EventFills
    {
        public event FillHandler OnFill;
        public event SystemMessageHandler OnSystemMessage;

        public static EventFills Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EventFills();
                return _instance;
            }
        }
        public bool AutoSubscribeInstruments { get; set; }
        public Session APISession { get { return _apiSession; } set { _apiSession = value; } }
        public Dictionary<InstrumentKey, TTInstrument> TTInstruments { set { _ttInstruments = value; } }
        public Dictionary<string, TTFill> TTFills { set { _ttFills = value; } }
        public Dictionary<string, TTFill> CurrentTTFills { set { _currentTTFills = value; } }
        public Dictionary<string, List<Fill>> HashSymbols { set { _hashSymbols = value; } }
        public Dispatcher Dispatcher { get { return _dispatcher; } }

        private Dictionary<InstrumentKey, TTInstrument> _ttInstruments = new Dictionary<InstrumentKey, TTInstrument>();
        private Dictionary<string, TTFill> _ttFills = new Dictionary<string, TTFill>();
        private Dictionary<string, TTFill> _currentTTFills = new Dictionary<string, TTFill>();
        private Dictionary<string, List<Fill>> _hashSymbols = new Dictionary<string, List<Fill>>();

        private Session _apiSession;
        private WorkerDispatcher _dispatcher = null;

        private FillsSubscription _fs;
        private TradeSubscription _ts;

        private static EventFills _instance;

        private EventFills()
        {
        }

        public void Start()
        {
            Console.WriteLine("Starting fill event thread...");

            // Attach a WorkerDispatcher to the current thread
            _dispatcher = Dispatcher.AttachWorkerDispatcher();
            _dispatcher.Run();
        }

        public void CreateFillSubscription()
        {
            if (_dispatcher.InvokeRequired())
            {
                _dispatcher.BeginInvoke(() =>
                {
                    CreateFillSubscription();
                });
                return;
            }

            Dispatcher dispatcher = Dispatcher.Current;

            Console.WriteLine("Creating Fill Subscription...");

            _fs = new FillsSubscription(_apiSession, dispatcher);
            _fs.FillListStart += new EventHandler<FillListEventArgs>(fs_FillListStart);
            _fs.FillBookDownload += new EventHandler<FillBookDownloadEventArgs>(fs_FillBookDownload);
            _fs.FillListEnd += new EventHandler<FillListEventArgs>(fs_FillListEnd);
            _fs.FillAdded += new EventHandler<FillAddedEventArgs>(fs_FillAdded);
            _fs.FillDeleted += new EventHandler<FillDeletedEventArgs>(fs_FillDeleted);
            _fs.FillAmended += new EventHandler<FillAmendedEventArgs>(fs_FillAmended);
            _fs.Start();

            _ts = new TradeSubscription(_apiSession, dispatcher);
            _ts.FillBookDownload += new EventHandler<FillBookDownloadEventArgs>(ts_FillBookDownload);
            _ts.FillListEnd += new EventHandler<FillListEventArgs>(ts_FillListEnd);
            _ts.FillListStart += new EventHandler<FillListEventArgs>(ts_FillListStart);
            _ts.FillAmended += new EventHandler<FillAmendedEventArgs>(ts_FillAmended);
            _ts.FillRecordAdded += new EventHandler<FillAddedEventArgs>(ts_FillRecordAdded);
            _ts.AdminFillAdded += new EventHandler<FillAddedEventArgs>(ts_AdminFillAdded);
            _ts.AdminFillDeleted += new EventHandler<FillDeletedEventArgs>(ts_AdminFillDeleted);
            _ts.Rollover += new EventHandler<RolloverEventArgs>(ts_Rollover);
            _ts.TradeSubscriptionReset += new EventHandler<TradeSubscriptionResetEventArgs>(ts_TradeSubscriptionReset);
            //ts.EnablePNL = true;
            //ts.ProfitLossChanged += new EventHandler<ProfitLossChangedEventArgs>(ts_ProfitLossChanged);
            _ts.Start();
            
            Console.WriteLine("Fill Subscription created.");
        }

        #region TRADE SUBSCRIPTION FILLS
        void ts_FillListStart(object sender, FillListEventArgs e)
        {
            processFill(FillOriginator.BOOK, FillAction.LIST_START);
        }

        void ts_FillListEnd(object sender, FillListEventArgs e)
        {
            processFill(FillOriginator.BOOK, FillAction.LIST_END);
        }

        void ts_FillBookDownload(object sender, FillBookDownloadEventArgs e)
        {
            processFill(FillOriginator.BOOK, FillAction.BOOK_DOWNLOAD, e.Fills);
        }

        void ts_AdminFillDeleted(object sender, FillDeletedEventArgs e)
        {
            processFill(FillOriginator.ADMIN, FillAction.ADD, e.Fill);
        }

        void ts_AdminFillAdded(object sender, FillAddedEventArgs e)
        {
            processFill(FillOriginator.ADMIN, FillAction.ADD, e.Fill);
        }

        void ts_FillRecordAdded(object sender, FillAddedEventArgs e)
        {
            processFill(FillOriginator.TRADER, FillAction.ADD, e.Fill);
        }

        void ts_FillAmended(object sender, FillAmendedEventArgs e)
        {
            processFill(FillOriginator.TRADER, FillAction.AMEND, e.OldFill, e.NewFill);
        }
        #endregion

        #region FILLS SUBSCRIPTION CALLBACKS
        void fs_FillAmended(object sender, FillAmendedEventArgs e)
        {
            processFill(FillOriginator.TRADER, FillAction.AMEND, e.OldFill, e.NewFill);
        }

        void fs_FillDeleted(object sender, FillDeletedEventArgs e)
        {
            processFill(FillOriginator.TRADER, FillAction.DELETE, e.Fill);
        }

        void fs_FillAdded(object sender, FillAddedEventArgs e)
        {
            processFill(FillOriginator.TRADER, FillAction.ADD, e.Fill);
        }

        void fs_FillListEnd(object sender, FillListEventArgs e)
        {
            processFill(FillOriginator.BOOK, FillAction.LIST_END);
        }

        void fs_FillBookDownload(object sender, FillBookDownloadEventArgs e)
        {
            processFill(FillOriginator.BOOK, FillAction.BOOK_DOWNLOAD, e.Fills);
        }

        void fs_FillListStart(object sender, FillListEventArgs e)
        {
            processFill(FillOriginator.BOOK, FillAction.LIST_START);
        }
        #endregion

        #region TRADE SUBSCRIPTION STATUS
        void ts_TradeSubscriptionReset(object sender, TradeSubscriptionResetEventArgs e)
        {
            processSystemMessage(SystemMessage.TRADE_SUBSCRIPTION_RESET);
        }

        void ts_Rollover(object sender, RolloverEventArgs e)
        {
            processSystemMessage(SystemMessage.ROLLOVER);
        }
        #endregion

        #region PROCESS FILLS
        void processFill(FillOriginator originator, FillAction action)
        {
            if (action == FillAction.LIST_START)
                processSystemMessage(SystemMessage.FILL_LIST_START);
            else if (action == FillAction.LIST_END)
                processSystemMessage(SystemMessage.FILL_LIST_END);
        }

        void processFill(FillOriginator originator, FillAction action, ReadOnlyCollection<Fill> fills)
        {
            foreach (Fill fill in fills)
            {
                processFill(FillOriginator.TRADER, FillAction.ADD, fill);
                Thread.Sleep(5);
            }

            processSystemMessage(SystemMessage.FILL_BOOK_DOWNLOAD);
        }

        void processFill(FillOriginator originator, FillAction action, Fill fill, params Fill[] fills)
        {
            TTFill newFill = null;

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
            // subscribe to the instrument updates if AutoSubscribeInstruments is true.
            if (!_ttInstruments.ContainsKey(fill.InstrumentKey))
            {
                if (AutoSubscribeInstruments)
                {
                    //TODO: Make the subscribe to instrument work
                    //SubscribeToInstrument(fill.InstrumentKey);
                    /*InstrumentLookupSubscription ils = new InstrumentLookupSubscription(apiSession, Dispatcher.Current, fill.InstrumentKey);
                    ils.Update += new EventHandler<InstrumentLookupSubscriptionEventArgs>(ils_Update);
                    ils.Start();*/
                }

                TTInstrument tti = new TTInstrument(fill.InstrumentKey);
                _ttInstruments.Add(fill.InstrumentKey, tti);
            }

            TTFill ttf = NewTTFill(fill);

            #region LOOK FOR '#' IN THE FFT FIELDS
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
                if (!_hashSymbols.ContainsKey(hashField))
                    _hashSymbols.Add(hashField, new List<Fill>());
                List<Fill> hashFills = _hashSymbols[hashField];
                hashFills.Add(fill);
            }
            #endregion

            // There are definitely times when the fill key already exists in the dictionary. In those
            // cases I am just overwriting the previous fill with the new one (asuming it is an updated
            // fill or whatever).
            _currentTTFills[fill.FillKey] = ttf;

            if (OnFill != null) OnFill(originator, action, _ttInstruments[fill.InstrumentKey], ttf, newFill);
        }
        #endregion

        #region CREATE NEW TTObjects
        private TTFill NewTTFill(Fill fill)
        {
            // Create a simpler TTFill object from the Fill object.
            TTFill ttf;
            string key = fill.FillKey;
            if (_ttFills.ContainsKey(key))
                ttf = _ttFills[key];
            else
            {
                ttf = new TTFill(fill);
                _ttFills.Add(key, ttf);
            }
            // Initialize any TTFill fields here:
            ttf.Instrument = _ttInstruments[fill.InstrumentKey];
            ttf.Key = key;

            return ttf;
        }
        #endregion

        #region PROCESS SYSTEM MESSAGES
        void processSystemMessage(SystemMessage systemMessage)
        {
            if (OnSystemMessage != null) OnSystemMessage(systemMessage);
        }
        #endregion


    } // class
} // namespace
