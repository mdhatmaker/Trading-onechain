using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Data
{
    public class SessionDetail
    {
        public event Action SessionDetailUpdated;

        public double ProfitLoss { get { return profitLoss; } }

        private double profitLoss;

        public SessionDetail()
        {

        }

        void FireUpdateEvent()
        {
            if (SessionDetailUpdated != null) SessionDetailUpdated();
        }

        public void SetProfitLoss(double pl)
        {
            profitLoss = pl;
            FireUpdateEvent();
        }

    } // class
} // namespace
