using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Framework;

namespace EZAPI.Execution
{
    public interface IExecutionEngine
    {
        event Action<EZFill> PartialFill;
        event Action<EZFill> CompleteFill;
        void Buy(EZInstrument instrument, int quantity, string strategyName);
        void Sell(EZInstrument instrument, int quantity, string strategyName);
        ExecutionStatus GetExecutionStatus(string strategyName);
    } // interface
} // namespace
