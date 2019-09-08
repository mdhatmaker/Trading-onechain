using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prowl;

namespace EZAPI.Messaging
{
    public static class ProwlAPI
    {
        //public static string API_KEY = "75fcd3a230ac062616e14c15ed5f79bdb5d0d199";
        public static string API_KEY = "";
        
        public enum Priority
        {
            VERY_LOW = ProwlNotificationPriority.VeryLow, MODERATE = ProwlNotificationPriority.Moderate,
            NORMAL = ProwlNotificationPriority.Normal, HIGH = ProwlNotificationPriority.High, EMERGENCY = ProwlNotificationPriority.Emergency
        };

        //public enum Priority { ProwlNotificationPriority.
        public static void Send(string msgDescription, string msgEvent, Priority msgPriority)
        {
            // Before posting a notification, 
            // check out the [app.config] file to configure the Prowl client.
            try
            {
                // Create a notification.
                var notification = new ProwlNotification();

                notification.Description = msgDescription;
                notification.Event = msgEvent;
                notification.Priority = (ProwlNotificationPriority)msgPriority;
                // Create the Prowl client.
                // By default, the Prowl client will attempt to load configuration
                // from the configuration file (app.config).  You can use an overloaded constructor
                // to configure the client directly and bypass the configuration file.
                var clientCfg = new ProwlClientConfiguration();
                clientCfg.ApiKeychain = API_KEY;
                clientCfg.ApplicationName = "Copper Hedge";

                var prowlClient = new ProwlClient(clientCfg);

                // Post the notification.
                prowlClient.PostNotification(notification);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    } // class
} // namespace
