using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonkeyLightning.Framework
{
    public enum TradeState { NOT_STARTED, WAITING, RUNNING, ENTERING, ENTERED, EXITING, EXITED, STOPPING, STOPPED, ABORTED }

    public enum TradeStepType { PRECONDITIONS, ENTRY, EXIT, STOP };

    public enum TradeStepState { INACTIVE, ACTIVE, WORKING, COMPLETED, ABORTED };


} // namespace
