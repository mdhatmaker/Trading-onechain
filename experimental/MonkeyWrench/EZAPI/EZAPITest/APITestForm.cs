using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using EZAPI;
using EZAPI.Framework;
using EZAPI.Framework.Base;

namespace EZAPITest
{
    public partial class APITestForm : Form
    {        
        private APIMain api;
        private Thread _apiThread;
        
        public APITestForm()
        {
            InitializeComponent();

            api = APIMain.Instance;

            bool autoSubscribeInstruments = false;
            bool subscribeMarketDepth = true;
            bool subscribeTimeAndSales = false;
            // Change the following line to use LoginType.Universal
            // for Universal Login to API:
            UserInteractionAPIStart(autoSubscribeInstruments, subscribeMarketDepth, subscribeTimeAndSales);
        }

        // AppStart is called for you AFTER all API initialization, so
        // put your startup code here. This is essentially the beginning
        // of your application.
        void AppStart()
        {

        }

        // Call the AppStop method when you are exiting your app
        void AppStop()
        {
            api.Dispose();
        }


        #region EZAPI STARTUP AND CALLBACKS
        #region EZAPI STARTUP
        void UserInteractionAPIStart(bool autoSubscribeInstruments, bool subscribeMarketDepth, bool subscribeTimeAndSales)
        {
            //api = new APIMain(autoSubscribeInstruments, subscribeMarketDepth, subscribeTimeAndSales, ttUsername, ttPassword);
            api.OnInitialize += new InitializeHandler(api_OnInitialize);
            //api.OnInstrumentFound += new InstrumentFoundHandler(api_OnInstrumentFound);
            //api.OnInsideMarketUpdate += new InsideMarketHandler(api_OnInsideMarketUpdate);
            //api.OnMarketDepthUpdate += new MarketDepthHandler(api_OnMarketDepth);
            //api.OnFill += new FillHandler(api_OnFill);
            //api.OnOrder += new OrderHandler(api_OnOrder);
            //api.OnTimeAndSales += new TimeAndSalesHandler(api_OnTimeAndSales);
            //api.OnSystemMessage += new SystemMessageHandler(api_OnSystemMessage);

            api.StartAPIUserInteraction();
            //_apiThread = new Thread(api.StartUserInteraction);
            //_apiThread.Name = "UILogin CTSAPI Thread";
            //_apiThread.Start();
        }

        void UnattendedAPIStart(bool autoSubscribeInstruments, bool subscribeMarketDepth, bool subscribeTimeAndSales, string username, string password)
        {
            //api = new APIMain(autoSubscribeInstruments, subscribeMarketDepth, subscribeTimeAndSales);
            api.OnInitialize += new InitializeHandler(api_OnInitialize);
            //api.OnInstrumentFound += new InstrumentFoundHandler(api_OnInstrumentFound);
            //api.OnInsideMarketUpdate += new InsideMarketHandler(api_OnInsideMarketUpdate);
            //api.OnMarketDepthUpdate += new MarketDepthHandler(api_OnMarketDepth);
            //api.OnFill += new FillHandler(api_OnFill);
            //api.OnOrder += new OrderHandler(api_OnOrder);
            //api.OnTimeAndSales += new TimeAndSalesHandler(api_OnTimeAndSales);
            //api.OnSystemMessage += new SystemMessageHandler(api_OnSystemMessage);

            api.StartAPIUnattended(username, password, "CTS");
            //_apiThread = new Thread(api.StartUnattended);
            //_apiThread.Name = "UnattendedLogin CTSAPI Thread";
            //_apiThread.Start();
        }
        #endregion

        void api_OnInitialize(bool success, string message)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnInitialize(success, message);
                });
                return;
            }
            #endregion

            if (success == true)
            {
                Console.WriteLine("TTAPI initialized successfully.");
                AppStart();
            }
            else
            {
                MessageBox.Show(message, "TTAPI Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void api_OnInstrumentFound(EZInstrument ttInstrument, bool success)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnInstrumentFound(ttInstrument, success);
                });
                return;
            }
            #endregion

            if (success)
            {
            }
            else
            {
                string errorMsg = "";

                // Instrument not found and TTAPI has given up looking for it
                MessageBox.Show(this, "Could not find instrument:\n\n" + ttInstrument.Name + "\n\n" + errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void api_OnSpreadFound(TTSpread spread, bool success)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnSpreadFound(spread, success);
                });
                return;
            }
            #endregion

            if (success)
            {
            }
            else
            {
                string errorMsg = "";

                /*if (e.Error != null)
                    errorMsg = e.Error.Message;*/

                // Instrument not found and TTAPI has given up looking for it
                //MessageBox.Show(this, "Could not find spread:\n\n" + spread.Name + "\n\n" + errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                MessageBox.Show(this, "An error occurred attempting to find the spread.\n\n" + errorMsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void api_OnInsideMarketUpdate(EZInstrument instrument)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnInsideMarketUpdate(instrument);
                });
                return;
            }
            #endregion

        }

        void api_OnMarketDepth(EZInstrument ttInstrument, EZMarketDepth marketDepth)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnMarketDepth(ttInstrument, marketDepth);
                });
                return;
            }
            #endregion

        }

        void api_OnFill(FillOriginator originator, FillAction action, EZInstrument ttInstrument, EZFill fill, EZFill newFill)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnFill(originator, action, ttInstrument, fill, newFill);
                });
                return;
            }
            #endregion

        }

        void api_OnOrder(EZOrderStatus status, EZInstrument ttInstrument, EZOrder order, EZOrder newOrder)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnOrder(status, ttInstrument, order, newOrder);
                });
                return;
            }
            #endregion


        }

        void api_OnTimeAndSales(EZInstrument ttInstrument, DateTime timeStamp, Price LTP, Quantity LTQ, TradeDirection direction, bool isOTC)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnTimeAndSales(ttInstrument, timeStamp, LTP, LTQ, direction, isOTC);
                });
                return;
            }
            #endregion

        }

        void api_OnSystemMessage(SystemMessage systemMessage)
        {
            #region Thread Safety
            //cross thread - so you don't get the cross theading exception
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    api_OnSystemMessage(systemMessage);
                });
                return;
            }
            #endregion

        }

        #endregion

        #region DRAG/DROP HANDLING
        /*
        // In this method, you deal with the instruments that have been
        // dropped onto your app.
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetInstrumentKeys().Count > 0)
            {
                foreach (InstrumentKey ik in e.Data.GetInstrumentKeys())
                {
                    // Deal with each instrument dropped onto the form
                }
            }
        }

        // This method changes the mouse cursor when instruments are dragged
        // over your form.
        private void Form1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetInstrumentKeys().Count > 0)
                e.Effect = DragDropEffects.Copy;
        }
        */
        #endregion
      
    } // class
} // namespace
