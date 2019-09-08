using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Framework;
using EZAPI.Framework.Base;

namespace EZAPI.Execution
{
    public class ExecutionStatus
    {
        public string StrategyName { get { return strategyName; } }
        public int QuantitySubmitted { get { return quantitySubmitted; } }
        public int QuantityFilled { get { return quantityFilled; } }
        public int QuantityRemaining { get { return (quantitySubmitted - quantityFilled); } }
        public decimal AverageBuyPrice { get { return averageBuyPrice; } }
        public decimal AverageSellPrice { get { return averageSellPrice; } }

        internal int quantitySubmitted;
        internal int quantityFilled;

        private decimal averageBuyPrice;
        private decimal averageSellPrice;
        private string strategyName;
        private List<EZFill> buyFills;
        private List<EZFill> sellFills;

        public ExecutionStatus(string strategyName)
        {
            this.strategyName = strategyName;

            averageBuyPrice = 0.0M;
            averageSellPrice = 0.0M;

            buyFills = new List<EZFill>();
            sellFills = new List<EZFill>();
        }

        public void AddFill(EZFill fill)
        {
            if (fill.BuySell == zBuySell.Buy)
            {
                buyFills.Add(fill);
                UpdateAverageBuyPrice();
            }
            else if (fill.BuySell == zBuySell.Sell)
            {
                sellFills.Add(fill);
                UpdateAverageSellPrice();
            }
        }

        void UpdateAverageBuyPrice()
        {
            int fillQty = 0;
            decimal totalFillPrice = 0.0M;

            foreach (EZFill fill in buyFills)
            {
                fillQty += fill.Quantity;
                totalFillPrice += fill.Price;
            }

            averageBuyPrice = totalFillPrice / (decimal)fillQty;
        }

        void UpdateAverageSellPrice()
        {
            int fillQty = 0;
            decimal totalFillPrice = 0.0M;

            foreach (EZFill fill in sellFills)
            {
                fillQty += fill.Quantity;
                totalFillPrice += fill.Price;
            }

            averageSellPrice = totalFillPrice / (decimal)fillQty;
        }

    } // class
} // namespace
