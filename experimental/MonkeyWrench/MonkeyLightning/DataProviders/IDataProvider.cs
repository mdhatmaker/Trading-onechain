using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZAPI.Data;

namespace MonkeyLightning.DataProvider
{
    public enum DataProviderState { READY, COLLECTING_DATA, ERROR }

    public interface IDataProvider
    {
        event ValueUpdateEventHandler DataUpdate;
        string Name { get; }
        RuleValueType ValueType { get; }
        object DataValue { get; }
        DataProviderState State { get; }
        string DisplayFormat { get; }
        string Description { get; }
        string EnglishDescription { get; }
        KeyValueCollection PropertyValues { get; set; }
        XmlSaveDataProvider SaveData { get; set; }
        TradeCycleDetail TradeDetail { set; }
        SessionDetail SessionDetail { set; }
        DPDisplayControl GetDisplayUserInterface();
        DPModifyControl GetModifyUserInterface();
        void UpdateProviderFromPropertyValues();
        void ClosingDisplayUI();
        //string Serialize();
    } // interface
} // namespace
