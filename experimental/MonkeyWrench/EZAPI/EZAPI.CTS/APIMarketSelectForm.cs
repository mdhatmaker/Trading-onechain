using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using T4;
using T4.API;
using EZAPI.Toolbox.Debug;

namespace EZAPI
{
    public enum MarketSelectViewType { EXCHANGES, CONTRACTS, MARKETS };

    public partial class APIMarketSelectForm : Form
    {
        public Market SelectedMarket { get { return selectedMarket; } }
        public bool IsClosing { get; set; }
        
        private MarketSelectViewType currentlyViewing;
        private Exchange selectedExchange;
        private Contract selectedContract;
        private Market selectedMarket;
        private APIMain api;

        private Dictionary<string, Market> markets;
        private Dictionary<string, Contract> contracts;
        private Dictionary<string, Exchange> exchanges;

        private Dictionary<string, Market> favoritesMarkets;
        private List<Market> favoritesList;

        private ToolStripMenuItem previouslySelectedMenuItem;

        public APIMarketSelectForm()
        {
            try
            {
                InitializeComponent();

                api = APIMain.Instance;

                exchanges = api.GetExchangeDictionary();

                selectedExchange = null;
                selectedContract = null;
                selectedMarket = null;
                DialogResult = DialogResult.Abort;

                // Start off in List view.
                toolStripMenuItem4.Checked = true;
                previouslySelectedMenuItem = toolStripMenuItem4;
                marketListView.View = View.List;

                // Load Favorites from Xml file.
                favoritesList = api.LoadXmlMarketList(APIMain.GetAPIDirectory("XML") + "MarketListFavorites.xml");
                // Store these Favorites for easy use and access.
                favoritesMarkets = new Dictionary<string, Market>();
                foreach (Market market in favoritesList)
                {
                    favoritesMarkets[market.Description] = market;
                }
                // And display the favorites on the FavoritesListView.
                PopulateListView(favoritesListView, favoritesList);

                currentlyViewing = MarketSelectViewType.EXCHANGES;
                UpdateDisplay();
            }
            catch (Exception ex)
            {
                ExceptionHandler.TraceException(ex);
            }
        }

        void UpdateDisplay()
        {
            marketListView.Clear();

            switch (currentlyViewing)
            {
                case MarketSelectViewType.EXCHANGES:
                    this.Text = "Market Selection : Exchanges";
                    foreach (Exchange exchange in exchanges.Values)
                    {
                        var item = new ListViewItem(exchange.Description);
                        item.Name = exchange.Description;
                        item.ImageKey = "exchange";
                        marketListView.Items.Add(item);
                    }
                    break;
                case MarketSelectViewType.CONTRACTS:
                    this.Text = "Market Selection : Contracts (" + selectedExchange.Description + ")";
                    foreach (Contract contract in contracts.Values)
                    {
                        var item = new ListViewItem(contract.Description);
                        item.Name = contract.Description;
                        item.ImageKey = "contract";
                        marketListView.Items.Add(item);
                    }
                    break;
                case MarketSelectViewType.MARKETS:
                    this.Text = "Market Selection : Markets (" + selectedContract.Description + ")";
                    foreach (Market market in markets.Values)
                    {
                        var item = new ListViewItem(market.Description);
                        item.Name = market.Description;
                        item.ImageKey = "market";
                        marketListView.Items.Add(item);
                    }
                    break;
            }
        }

        private void marketListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (marketListView.SelectedItems.Count > 0)
            {
                status.Text = marketListView.SelectedItems[0].Name;
            }
        }

        private void marketListView_ItemActivate(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                ListViewItem selectedItem = marketListView.SelectedItems[0];

                switch (currentlyViewing)
                {
                    case MarketSelectViewType.EXCHANGES:
                        selectedExchange = exchanges[selectedItem.Name];
                        contracts = api.GetContractDictionary(selectedExchange);
                        currentlyViewing = MarketSelectViewType.CONTRACTS;
                        UpdateDisplay();
                        break;
                    case MarketSelectViewType.CONTRACTS:
                        selectedContract = contracts[selectedItem.Name];
                        markets = api.GetMarketDictionary(selectedContract);
                        currentlyViewing = MarketSelectViewType.MARKETS;
                        UpdateDisplay();
                        break;
                    case MarketSelectViewType.MARKETS:
                        selectedMarket = markets[selectedItem.Name];

                        // Add to recents.
                        var item = new ListViewItem(selectedMarket.Description);
                        item.Name = selectedMarket.Description;
                        item.ImageKey = "market";
                        recentListView.Items.Add(item);

                        DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        
        }

        private void toolStripDropDownButtonViewType_Click(object sender, EventArgs e)
        {
            /*return;

            switch (marketListView.View)
            {
                case View.LargeIcon:
                    marketListView.View = View.SmallIcon;
                    break;
                case View.SmallIcon:
                    marketListView.View = View.Tile;
                    break;
                case View.Tile:
                    marketListView.View = View.List;
                    break;
                case View.List:
                    marketListView.View = View.Details;
                    break;
                case View.Details:
                    marketListView.View = View.LargeIcon;
                    break;
            }*/
        }

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            previouslySelectedMenuItem.Checked = false;

            ToolStripMenuItem clicked = (ToolStripMenuItem)sender;

            if (clicked == toolStripMenuItem1)
                marketListView.View = View.LargeIcon;
            else if (clicked == toolStripMenuItem2)
                marketListView.View = View.SmallIcon;
            else if (clicked == toolStripMenuItem3)
                marketListView.View = View.Tile;
            else if (clicked == toolStripMenuItem4)
                marketListView.View = View.List;
            else if (clicked == toolStripMenuItem5)
                marketListView.View = View.Details;

            previouslySelectedMenuItem = (ToolStripMenuItem)sender;
        }

        private void toolStripButtonBack_Click(object sender, EventArgs e)
        {
            switch (currentlyViewing)
            {
                case MarketSelectViewType.EXCHANGES:
                    break;
                case MarketSelectViewType.CONTRACTS:
                    currentlyViewing = MarketSelectViewType.EXCHANGES;
                    UpdateDisplay();
                    break;
                case MarketSelectViewType.MARKETS:
                    currentlyViewing = MarketSelectViewType.CONTRACTS;
                    UpdateDisplay();
                    break;
            }
        }

        private void toolStripButtonHome_Click(object sender, EventArgs e)
        {
            //tabPageMarkets.Select();
            tabControl1.SelectedTab = tabPageMarkets;
            currentlyViewing = MarketSelectViewType.EXCHANGES;
            UpdateDisplay();
        }

        private void APIMarketSelectForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsClosing == false)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void recentListView_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem selectedItem = recentListView.SelectedItems[0];
            selectedMarket = markets[selectedItem.Name];

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void favoritesListView_ItemActivate(object sender, EventArgs e)
        {
            ListViewItem selectedItem = favoritesListView.SelectedItems[0];
            selectedMarket = favoritesMarkets[selectedItem.Name];

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void marketListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && currentlyViewing == MarketSelectViewType.MARKETS)
            {
                if (marketListView.FocusedItem.Bounds.Contains(e.Location) == true)
                {
                    contextMenuStrip1.Show(Cursor.Position);
                }
            } 
        }

        private void itemAddToFavorites_Click(object sender, EventArgs e)
        {
            //ListViewItem selectedItem = marketListView.SelectedItems[0];
            Market rightClickMarket = markets[marketListView.FocusedItem.Name];

            // Add to favorites.
            var item = new ListViewItem(rightClickMarket.Description);
            item.Name = rightClickMarket.Description;
            item.ImageKey = "market";

            favoritesListView.Items.Add(item);

            // Add to the Favorites List we maintain behind the scenes.
            favoritesList.Add(rightClickMarket);

            //api.SaveXmlMarketList(MarketListFromListView(favoritesListView), APIMain.GetAPIDirectory("XML") + "MarketListFavorites.xml");
            api.SaveXmlMarketList(favoritesList, APIMain.GetAPIDirectory("XML") + "MarketListFavorites.xml");
        }

        private List<Market> MarketListFromListView(ListView listView)
        {
            List<Market> marketList = new List<Market>();

            foreach (ListViewItem item in listView.Items)
            {
                marketList.Add(markets[item.Name]);
            }

            return marketList;
        }

        private void PopulateListView(ListView listView, List<Market> marketList)
        {
            foreach (Market market in marketList)
            {
                var item = new ListViewItem(market.Description);
                item.Name = market.Description;
                item.ImageKey = "market";
                listView.Items.Add(item);
            }

        }

        private void APIMarketSelectForm_VisibleChanged(object sender, EventArgs e)
        {
            // Reset the selected market each time we show the form.
            if (this.Visible == true)
            {
                status.Text = "";
                selectedMarket = null;

                // Change the cursor back to the "normal" cursor (in case we set the
                // WaitCursor before displaying this form).
                Cursor.Current = Cursors.Default;
                Cursor.Show();
            }
            //DialogResult = DialogResult.Abort;
        }

        private void APIMarketSelectForm_Load(object sender, EventArgs e)
        {
            // Change the cursor back to the "normal" cursor (in case we set the
            // WaitCursor before displaying this form).
            Cursor.Current = Cursors.Default;
            Cursor.Show();
        }


    } // class
} // namespace
