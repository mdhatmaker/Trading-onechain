using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Framework.Base;
using EZAPI.Framework;

namespace EZAPI.Framework.Spread
{
    public class ezSpreadDefinition
    {
        public List<ezSpreadLeg> Legs { get { return spreadLegs; } }

        private List<ezSpreadLeg> spreadLegs;

        public ezSpreadDefinition()
        {
            spreadLegs = new List<ezSpreadLeg>();
        }
       
        public void AddSpreadLeg(ezSpreadLeg leg)
        {
            spreadLegs.Add(leg);
        }


    } // class
} // namespace
