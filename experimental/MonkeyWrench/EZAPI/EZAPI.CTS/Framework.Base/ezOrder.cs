using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Base
{
    public class ezOrder
    {
        public string AccountName
        {
            get { return "order"; }
        }

        public zAccountType AccountType
        {
            get { return zAccountType.Agent1; }
        }

        public string FFT2
        {
            get { return "fft2"; }
        }

        public string FFT3
        {
            get { return "fft3"; }
        }

        public string Destination
        {
            get { return "destination"; }
        }

        public string OrderTag
        {
            get { return "orderTag"; }
        }

        public zBuySell BuySell
        {
            get { return zBuySell.Unknown; }
        }

        public ezPrice LimitPrice
        {
            get { return ezPrice.Invalid; }
        }

        public ezInstrumentKey InstrumentKey
        {
            get { return null; }
        }

        public ezQuantity OrderQuantity
        {
            get { return ezQuantity.Invalid; }
        }

        public void Hold()
        {

        }

        public void Delete()
        {

        }

        public void RemoveFromHold()
        {

        }

        public string GiveUp
        {
            get { return "giveup"; }
        }

        public bool IsAutomated
        {
            get { return false; }
        }

        public ezQuantity MinimumQuantity
        {
            get { return ezQuantity.Invalid; }
        }

        public zOrderModifiers Modifiers
        {
            get { return zOrderModifiers.None; }
        }

        public zOpenClose OpenClose
        {
            get { return zOpenClose.Close; }
        }

        public bool PriceCheck
        {
            get { return false; }
        }

        public zOrderType OrderType
        {
            get { return zOrderType.BestLimit; }
        }

        public ezOrderRestriction Restriction
        {
            get { return null; }
        }

        public ezPrice StopPrice
        {
            get { return ezPrice.Invalid; }
        }

        public ezQuantity StopTriggerQuantity
        {
            get { return ezQuantity.Invalid; }
        }

        public string SubUserId
        {
            get { return "subUserId"; }
        }

        public ezTimeInForce TimeInForce
        {
            get { return null; }
        }

        public string UserName
        {
            get { return "userName"; }
        }

        public string UserTag
        {
            get { return "userTag"; }
        }

        public bool IsSynthetic
        {
            get { return false; }
        }

        public bool IsParent
        {
            get { return false; }
        }

        public bool IsChild
        {
            get { return false; }
        }

        public void ModifyPrice(ezPrice updatedPrice)
        {

        }

        public void ModifyQuantity(ezQuantity updatedQuantity)
        {

        }

    } // class
} // namespace
