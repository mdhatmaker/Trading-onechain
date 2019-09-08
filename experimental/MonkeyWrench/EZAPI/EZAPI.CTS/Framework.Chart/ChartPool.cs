using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Framework.Chart
{
    public class ChartPool
    {
        private static readonly ChartPool instance = new ChartPool();

        public static ChartPool Instance { get { return instance; } }

        public Dictionary<ChartDescriptor, UIControlChart> Items { get; private set; }

        private ChartPool()
        {
            Items = new Dictionary<ChartDescriptor, UIControlChart>();
        }

        public void Add(ChartDescriptor desc)
        {
            Items[desc] = null;
        }

        public void Add(ChartDescriptor desc, UIControlChart uic)
        {
            Items[desc] = uic;
        }

        public UIControlChart this[ChartDescriptor desc]
        {
            get { return Items[desc]; }
        }

        public bool Contains(ChartDescriptor desc)
        {
            return Items.ContainsKey(desc);
        }
    } // class
} // namespace
