using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonkeyLightning.DataProvider.Base
{
    /// <summary>
    /// When a DataProvider offers a choice among various items, this
    /// class provides the detail for these items.
    /// This class would be useful for, as an example, the TradeDetail
    /// DataProvider. This class will maintain a "Tag" for each item of
    /// TradeDetail (i.e. "ElapsedTime") and a "DisplayName" for each
    /// item of (i.e. "Fill vs Current Bid") along with a detailed description
    /// and other information about each item that the user can select.
    /// </summary>
    public class SelectedItemDetail
    {
        public string Tag { get { return tag; } } // a short identifier for this choice (that we can reference in the code)
        public string DisplayName { get; set; } // we can display this to the user to describe their choice (terse description)
        public string Description { get; set; } // detailed description of this choice (may even display a simple example of use)
        public string Text1 { get; set; } // Text fields can hold any text information.
        public string Text2 { get; set; }
        public string Text3 { get; set; }

        private string tag;

        public SelectedItemDetail(string tag, string displayName, string description = "", string text1 = "", string text2 = "", string text3 = "")
        {
            this.tag = tag;
            this.DisplayName = displayName;
            this.Description = description;
            this.Text1 = text1;
            this.Text2 = text2;
            this.Text3 = text3;
        }

    } // class
} // namespace
