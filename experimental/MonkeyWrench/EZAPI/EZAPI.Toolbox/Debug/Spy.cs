#define DEEPDEBUG
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EZAPI.Toolbox.Debug
{
    public enum ThreadStates { AutoDiscovery, Opening, Running, Stopping, Stopped }
    
    public class Spy
    {
        public static Spy Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Spy();
                    instance.Start("SpyForm");
                }
                return instance;
            }
        }

        private static Spy instance = null;

        private volatile static ThreadStates state = ThreadStates.Stopped;        
        private static Thread thrd;
        private static SpyForm comForm = null;
        private static string lasterror = null;

        private Spy()
        {
        }

        public void Start(string threadName)
        {
            //...
            thrd = new Thread(new ThreadStart(run));
            thrd.IsBackground = false;
            thrd.Priority = ThreadPriority.BelowNormal;
            thrd.Name = threadName;
            thrd.SetApartmentState(ApartmentState.STA); //a single-threaded Appartment is required for COM objects of secondary thread
            thrd.Start();            
        }

        //create a Form object to be run in its own thread
        private static void run()
        {
            try
            {
                Console.WriteLine("===Creating thread Form...");
                comForm = new SpyForm();

                Console.WriteLine("===checking thread state ({0})...", state);
                if (state == ThreadStates.Stopped)
                    ; // return; //bad action in constructor (can't continue)
                //state changed in SerialForm constructor to States.Running using a public setter
                Console.WriteLine("===Running thread form with Application.Run...");
                Application.Run(comForm); //returns when form exits
                //Form has closed
            }
            catch (Exception ex)
            { /*form may be disposed on run if bad com port*/
                Log.Print(String.Format("Error in run method: {0}\nStack Trace:{1}", ex.Message, ex.StackTrace));
                SpyForm.bClosing = false;
                state = ThreadStates.Stopped;
                lasterror = ex.Message;
            }
            //MessageBox.Show("Thread stopping");
        }

        private void PrintMessage(string msg, params object[] values)
        {
            string timeStamp = string.Format("{0:h:mm:ss tt}   ", DateTime.Now);
            string output = timeStamp + string.Format(msg, values);
            if (comForm == null)
                Console.WriteLine(output);
            else
                comForm.Print(output);
        }

        private void PrintObject(object obj)
        {
            string timeStamp = string.Format("{0:h:mm:ss tt}   ", DateTime.Now);
            string output = timeStamp + obj.ToString();
            if (comForm == null)
                Console.WriteLine(output);
            else                
                comForm.Print(output);
        }

        [Conditional("DEEPDEBUG")]
        public static void Print(string msg, params object[] values)
        {
            Instance.PrintMessage(msg, values);

            //string dtStr = string.Format("{0:h:mm:ss tt}   ", DateTime.Now);
            //Console.WriteLine(dtStr + string.Format(msg, values));
            /*traceForm.Invoke((MethodInvoker)delegate
            {
                traceList.Items.Insert(0, dtStr + string.Format(msg, values));
                traceForm.Refresh();
            });*/
        }

        [Conditional("DEEPDEBUG")]
        public static void Print(object obj)
        {
            Instance.PrintObject(obj);
            
            //string dtStr = string.Format("{0:h:mm:ss tt}   ", DateTime.Now);
            //Console.WriteLine(dtStr + obj.ToString());
            /*traceForm.Invoke((MethodInvoker)delegate
            {
                traceList.Items.Insert(0, dtStr + obj.ToString());
                traceForm.Refresh();
            });*/
        }

    } // class



} // namespace
