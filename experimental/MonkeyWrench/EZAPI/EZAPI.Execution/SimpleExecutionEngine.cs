#define DEEPDEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI;
using EZAPI.Framework;
using EZAPI.Framework.Base;
using EZAPI.Toolbox.Debug;

namespace EZAPI.Execution
{
    public class SimpleExecutionEngine : IExecutionEngine
    {
        public event Action<EZFill> PartialFill;
        public event Action<EZFill> CompleteFill;

        private Dictionary<string, ExecutionStatus> executionStatus;
        private Dictionary<string, List<EZOrder>> orders;
        private Dictionary<string, List<EZFill>> fills;

        APIMain api;

        public SimpleExecutionEngine()
        {
            api = APIMain.Instance;

            api.OnFill += api_OnFill;
            
            executionStatus = new Dictionary<string, ExecutionStatus>();
            orders = new Dictionary<string,List<EZOrder>>();
            fills = new Dictionary<string,List<EZFill>>();
        }

        public void Buy(EZInstrument instrument, int quantity, string strategyName)
        {
            if (strategyName != null)
            {
                executionStatus[strategyName] = new ExecutionStatus(strategyName);
                executionStatus[strategyName].quantitySubmitted = quantity;
            }

            ezPrice ask = instrument.Ask;

            EZOrder ezo = api.BuyLimit(instrument, quantity, ask, strategyName);
            StoreOrder(strategyName, ezo);

            Spy.Print("BUY: {0} @ {1}", quantity, ask);
        }

        public void Sell(EZInstrument instrument, int quantity, string strategyName)
        {
            if (strategyName != null)
            {
                executionStatus[strategyName] = new ExecutionStatus(strategyName);
            }

            ezPrice bid = instrument.Bid;

            EZOrder ezo = api.SellLimit(instrument, quantity, bid, strategyName);
            StoreOrder(strategyName, ezo);

            Spy.Print("SELL: {0} @ {1}", quantity, bid);
        }

        public ExecutionStatus GetExecutionStatus(string strategyName)
        {
            ExecutionStatus status = null;

            if (strategyName != null && executionStatus.ContainsKey(strategyName))
            {
                status = executionStatus[strategyName];
            }

            return status;
        }

        void StoreOrder(string strategyName, EZOrder order)
        {
            if (!orders.ContainsKey(strategyName) || orders[strategyName] == null)
                orders[strategyName] = new List<EZOrder>();

            orders[strategyName].Add(order);
        }

        void StoreFill(string strategyName, EZFill fill)
        {
            if (!fills.ContainsKey(strategyName) || fills[strategyName] == null)
                fills[strategyName] = new List<EZFill>();

            fills[strategyName].Add(fill);
        }

        void api_OnFill(EZFill fill)
        {
            StoreFill(fill.Tag, fill);

            string strategyName = fill.Tag;

            if (fill.FillType == zFillType.Full)
            {
                executionStatus[strategyName].AddFill(fill);
                if (CompleteFill != null) CompleteFill(fill);
            }
            else if (fill.FillType == zFillType.Partial)
            {
                executionStatus[strategyName].AddFill(fill);
                if (PartialFill != null) PartialFill(fill);
            }

            Spy.Print("FILL: {0} {1} {2} {3}", fill.FillType, fill.BuySell, fill.Quantity, fill.Price);
        }


    } // class
} // namespace
