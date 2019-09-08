using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using EZAPI.Data;

namespace MonkeyLightning.DataProvider
{
    public abstract class DataProviderBase : IDataProvider  
    {
        private ValueUpdateEventHandler _myEvent;
        protected void FireValueUpdateEvent()
        {
            if (_myEvent != null) _myEvent(this, new ValueUpdateEventArgs(currentValue));
        }

        public virtual event ValueUpdateEventHandler DataUpdate
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            add
            {
                _myEvent = (ValueUpdateEventHandler)Delegate.Combine(_myEvent, value);
                FireValueUpdateEvent();
            }
            [MethodImpl(MethodImplOptions.Synchronized)]
            remove
            {
                _myEvent = (ValueUpdateEventHandler)Delegate.Remove(_myEvent, value);
            }
        }

        /// <summary>
        /// Event to indicate that the DataProvider's State has changed (ex: from COLLECTING_DATA to READY).
        /// </summary>
        public virtual event Action DataProviderStateChanged;
        protected void FireDataProviderStateChangeEvent()
        {
            if (DataProviderStateChanged != null) DataProviderStateChanged();
        }

        public virtual string[] PropertyNames { get { return null; } }

        public KeyValueCollection PropertyValues
        {
            get { return prop; }
            set { prop = value; }
        }        

        protected KeyValueCollection prop;

        /*public ValueString this[string key]
        {
            get
            {
                return prop[key];
            }
            set
            {
                prop[key] = value;
            }
        }*/

        protected object currentValue;
        protected TradeCycleDetail tradeCycle;
        protected TradeCycleDetail previousTradeCycle;
        protected SessionDetail sessionDetail;
        protected DataProviderState dataProviderState;  // current state of our DataProvider ('READY' means the DataProvider is ready to use).

        protected DataProviderBase()
        {
            prop = new KeyValueCollection(PropertyNames);

            tradeCycle = null;
            previousTradeCycle = null;
            sessionDetail = null;

            // Assume we are in the READY state. Any data provider that is not in the READY state
            // when it starts up should change the dataProviderState value in its constructor.
            dataProviderState = DataProviderState.READY;
        }

        public abstract string Name { get; }
        public abstract string DisplayFormat { get; }
        public abstract string Description { get; }
        public abstract string EnglishDescription { get; }

        /// <summary>
        /// For our DataProvider State, we will use READY by default. If a DataProvider
        /// is collecting data or in an error condition (or other non-ready state), it
        /// is up to the DataProvider to indicate this by overriding the State property.
        /// </summary>
        public virtual DataProviderState State
        {
            get { return dataProviderState; }
        }

        public virtual TradeCycleDetail TradeDetail
        {
            set
            {
                if (tradeCycle != null) tradeCycle.TradeCycleUpdated -= TradeCycleUpdated;
                previousTradeCycle = tradeCycle;
                tradeCycle = value;
                if (tradeCycle != null) tradeCycle.TradeCycleUpdated += TradeCycleUpdated;
                TradeDetailInitialized();
            }
        }

        public virtual SessionDetail SessionDetail
        {
            set
            {
                sessionDetail = value;
                sessionDetail.SessionDetailUpdated += SessionDetailUpdated;
                SessionDetailInitialized();
            }
        }

        protected virtual void TradeCycleUpdated()
        {            
        }

        protected virtual void SessionDetailUpdated()
        {            
        }

        public virtual void TradeDetailInitialized()
        {
        }

        public virtual void SessionDetailInitialized()
        {
        }

        public virtual DPDisplayControl GetDisplayUserInterface()
        {
            return null;
        }

        public virtual DPModifyControl GetModifyUserInterface()
        {
            return null;
        }

        public virtual void UpdateProviderFromPropertyValues()
        {
        }

        public virtual void ClosingDisplayUI()
        {
        }

        public abstract RuleValueType ValueType { get; }
        public abstract object DataValue { get; }

        /// <summary>
        /// This property was created for instances where the type of
        /// the "currentValue" object will not serialize correctly with
        /// the XmlSerializer (such as the TimeSpan object, for example).
        /// For those instances, the DataProvider just needs to override
        /// this this SerializeCurrentValue property.
        /// </summary>
        public virtual object SerializeCurrentValue
        {
            get { return currentValue; }
            set { currentValue = value; }
        }

        public virtual XmlSaveDataProvider SaveData
        {
            get
            {
                XmlSaveDataProvider save = new XmlSaveDataProvider();
                save.Name = Name;
                save.CurrentValue = SerializeCurrentValue;
                save.KeyValuePairs = prop.GetSerializableDictionary();
                return save;
            }
            set
            {
                prop.SetSerializableDictionary(value.KeyValuePairs);
                SerializeCurrentValue = value.CurrentValue;
                UpdateProviderFromPropertyValues();
            }
        }

    } // class
} // namespace
