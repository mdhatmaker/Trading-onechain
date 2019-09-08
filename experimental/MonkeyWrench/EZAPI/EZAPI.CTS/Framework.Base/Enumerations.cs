using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Base
{
    public enum zAccountType
    {
        Agent1, Agent2, Agent3, Agent4, Agent5, Agent6, Agent7, Agent8, Agent9,
        GiveUp1, GiveUp2, GiveUp3,
        MarketMaker1, MarketMaker2, MarketMaker3,
        None,
        Principal1, Principal2, Principal3,
        Unallocated1, Unallocated2, Unallocated3
    }

    public enum zOrderModifiers
    {
        Auction, AutoAgress, AwayMarket, BestOnly, IfTouched, LimitMarketToLimit, MarketToLimit, None, NonLeave, Passive, SellShortExempt, Stop, Timed, Trailing, TTRelayOrigin
    }

    public enum zOrderType
    {
        BestLimit, CMO, Cross, Limit, Market, MarketToLimit, None, OCO, Quote
    }

    public enum zOpenClose
    {
        Close, ComboCloseOpen, ComboOpenClose, FIFO, Manual, Open, Rollover, StartOfDay, StartOfDayFill, XRiskAdmin
    }

    public enum zBuySell
    {
        Buy, Sell, Unknown
    }

    public enum zFillType
    {
        Full, Partial
    }

} // namespace
