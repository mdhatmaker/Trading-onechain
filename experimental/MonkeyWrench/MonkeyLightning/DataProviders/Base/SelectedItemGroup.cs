using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonkeyLightning.DataProvider.Base
{
    /// <summary>
    /// This class represents a GROUP of user-selectable choices. An example
    /// would be all the choices the user can pick for MarketData (i.e. BidPrice,
    /// AskPrice, LastQty, etc).
    /// </summary>
    public class SelectedItemGroup
    {
        public string GroupName { get { return groupName; } }
        public List<SelectedItemDetail> Items { get { return items.Values.ToList<SelectedItemDetail>(); } }

        private string groupName;
        private Dictionary<string, SelectedItemDetail> items;

        public SelectedItemGroup(string groupName)
        {
            this.groupName = groupName;
            items = new Dictionary<string, SelectedItemDetail>();
        }

        public SelectedItemDetail this[string tag]
        {
            get { return items[tag]; }
            set { items[tag] = value; }
        }

        public void AddItem(SelectedItemDetail item)
        {
            items.Add(item.Tag, item);
        }

    } // class
} // namespace
