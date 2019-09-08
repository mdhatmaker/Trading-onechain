using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using Tools;
using static Tools.G;
using PubnubApi;

namespace CryptoMania
{
    class Program
    {
        static public Pubnub pubnub;

        static void Main(string[] args)
        {
            //Dictionary<string, string> settings;

            if (Debugger.IsAttached)
            {
                //args = new string[] { "symbols", "neo" };
            }

            // Look for some specific tools that can be run from the command line
            if (args.Length > 0 && args[0].ToLower() == "symbols")
            {
                if (args.Length == 1)
                {
                    CryptoAPIs.ExchangeSymbols.DisplaySymbols();
                }
                else
                {
                    CryptoAPIs.ExchangeSymbols.DisplaySymbols(args[1]);
                }
            }
            else    // OTHERWISE, we are attempting to run a "typical" CryptoMania session, which requires a SETTINGS file to operate
            {
                /*string settings_filename = "CryptoMania.settings.txt";
                Console.WriteLine("\nLooking for '{0}' in this directory...", settings_filename);
                string dir = AppDomain.CurrentDomain.BaseDirectory;
                settings = ReadSettingsFile(Path.Combine(dir, settings_filename));
                if (settings.Count == 0)
                {
                    Console.WriteLine("\nEmpty settings file or settings file not found.");
                }
                else
                {
                    CryptoAPIs.Crypto.Test(settings);
                }*/

                /*if (Settings.Instance.Count > 0)
                {
                    //CryptoAPIs.Crypto.Test();
                    TestPubSub();
                }*/

                TestPubSub();
            }

            if (Debugger.IsAttached)
            {
                Console.Write("Press any key to exit... ");
                Console.ReadKey();
            }
        }

        // Test publish/subscribe using Pubnub
        static void TestPubSub()
        {
            PNConfiguration config = new PNConfiguration();
            config.SubscribeKey = "demo";
            config.PublishKey = "demo";

            pubnub = new Pubnub(config);

            pubnub.AddListener(new SubscribeCallbackExt(
                (pubnubObj, message) =>
                {
                    // Handle new message stored in message.Message
                    if (message != null)
                    {
                        if (message.Channel != null)
                        {
                            // Message has been received on channel group stored in message.Channel
                            cout("message received on Channel {0}: {1}", message.Channel, message.Message);
                        }
                        else
                        {
                            // Message has been received on channel stored in message.Subscription
                            cout("message received on Subscription {0}: {1}", message.Subscription, message.Message);
                        }

                        ErrorMessage("Program::TestPubSub=> Pubnub message is null");
                        /*
                            log the following items with your favorite logger
                                - message.Message()
                                - message.Subscription()
                                - message.Timetoken()
                         */
                    }
                },
                (pubnubObj, presence) => { },
                (pubnubObj, status) =>
                {
                    if (status.Category == PNStatusCategory.PNUnexpectedDisconnectCategory)
                    {
                        // This even happens when radio / connectivity is lost
                    }
                    else if (status.Category == PNStatusCategory.PNConnectedCategory)
                    {
                        // Connect event. You can do stuff like publish, and know you'll get it.
                        // Or just use the connected event to confirm you are subscribed for UI/internal notifications, etc.

                        pubnub.Publish()
                        .Channel("awesomeChannel")
                        .Message("hello!!")
                        .Async(new PNPublishResultExt((publishResult, publishStatus) =>
                        {
                            // Check whether request successfully completed or not.
                            if (!publishStatus.Error)
                            {
                                // Message successfully published to specific channel.
                            }
                            else
                            {
                                // Request processing failed.

                                ErrorMessage("Program::TestPubSub=> Pubnub request processing failed: {0}", publishStatus.Category);
                                // Handle message publish error. Check 'Category' property to find out possible issue
                                // because of which request failed.
                            }
                        }));
                    }
                    else if (status.Category == PNStatusCategory.PNReconnectedCategory)
                    {
                        // Happens as part of our regular operation. This event happens when
                        // radio / connectivity is lost, then regained.
                    }
                    else if (status.Category == PNStatusCategory.PNDecryptionErrorCategory)
                    {
                        ErrorMessage("Program::TestPubSub=> Pubnub decryption error");
                        // Handle message decryption error. Probably client configured to
                        // encrypt messages and on live data feed it received plain text.
                    }
                }
                ));

            pubnub.Subscribe<string>()
                .Channels(new string[]
                {
                    "awesomeChannel"
                })
                .Execute();
        }


        /*static Dictionary<string, string> ReadSettingsFile(string filename)
        {
            var result = new Dictionary<string, string>();
            try
            {
                using (var reader = new StreamReader(filename))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;      // skip blank lines
                        int idx = line.IndexOf('=');
                        if (idx < 0)
                        {
                            Console.WriteLine("All lines in settings file should have format NAME=VALUE. Skipping the following line:\n{0}\n", line);
                            continue;
                        }
                        Console.WriteLine(line);
                        string name = line.Substring(0, idx);
                        string value = line.Substring(idx + 1);
                        result[name] = value;
                        //Console.WriteLine("{0} {1}", name, value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nERROR: Could not read from the settings file: {0}", ex.Message);
                Console.WriteLine("Try again and specify the correct settings filename on the command line.");
            }
            return result;
        }*/


    } // end of class Program
} // end of namespace
