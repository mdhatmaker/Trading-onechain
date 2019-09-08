using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Resources;
using EZAPI.Framework.Base;

namespace EZAPI.Framework
{
    /// <summary>
    /// This class holds the information for OrderFeed, AccountName and AccountType
    /// </summary>
    public class EZOrderRoute
    {
        /// <summary>
        /// OrderFeed object for this order route
        /// </summary>
        public ezOrderFeed OrderFeed { get; set; }
        /// <summary>
        /// Account name to use when sending an order on this route
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// Account type to use when sending an order on this route
        /// </summary>
        public zAccountType AccountType { get; set; }

        /// <summary>
        /// Construct an empty OrderRoute object
        /// </summary>
        public EZOrderRoute()
        {
        }

        /// <summary>
        /// Construct an OrderRoute object populated with the necessary route information
        /// </summary>
        /// <param name="feed">OrderFeed to use with this order route</param>
        /// <param name="accountType">AccountType to use with this order route</param>
        /// <param name="accountName">AccountName to use with this order route</param>
        public EZOrderRoute(ezOrderFeed feed,  zAccountType accountType, string accountName)
        {
            OrderFeed = feed;
            AccountType = accountType;
            AccountName = accountName;
        }

        /// <summary>
        /// Static method to return an OrderRoute for a given instrument and market
        /// </summary>
        /// <param name="instrument">TTInstrument for this order route</param>
        /// <param name="marketName">Market for this order route (i.e. "CME", "CBOT", etc.)</param>
        /// <returns>OrderRoute object to use when sending orders for the specified instrument</returns>
        public static EZOrderRoute GetOrderRoute(EZInstrument instrument, string marketName)
        {
            string accountInfoString = null;
            IResourceReader reader = null;
            Dictionary<string, string> loginResources = new Dictionary<string, string>();
            try
            {
                reader = new ResourceReader("Account.resources");
                IDictionaryEnumerator dict = reader.GetEnumerator();
                while (dict.MoveNext())
                {
                    string key = dict.Key.ToString();
                    if (key.Equals(marketName))
                    {
                        accountInfoString = dict.Value.ToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            EZOrderRoute route = null;

            if (accountInfoString != null)
            {
                route = new EZOrderRoute();

                string[] values = accountInfoString.Split(',');
                string feedName = values[0];
                string accountName = values[1];
                string accountType = values[2];
                ReadOnlyCollection<ezOrderFeed> feeds = instrument.EnabledOrderFeeds;
                foreach (ezOrderFeed feed in feeds)
                {
                    if (feed.Name.Equals(feedName))
                    {
                        route.OrderFeed = feed;
                        break;
                    }
                }
                route.AccountName = accountName;
                route.AccountType = (zAccountType)Enum.Parse(typeof(zAccountType), accountType);
            }

            return route;
        }

    } // class

    public class EZOrderRouteInfo
    {
        public string MarketName { get; set; }
        public string OrderFeedName { get; set; }
        public string AccountTypeName { get; set; }
        public string AccountName { get; set; }

        public EZOrderRouteInfo()
        {
        }
    } // class
} // namespace
