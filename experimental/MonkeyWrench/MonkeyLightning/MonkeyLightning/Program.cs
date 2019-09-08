//#define TESTING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonkeyLightning.UI.Forms;
using EZAPI.Toolbox.Debug;

namespace MonkeyLightning
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            Spy.Print("Starting Main() program method...");
#if TESTING
            Application.Run(new TestSpreadsForm());
#else
            Spy.Print("Creating new LightningLoaderForm...");
            Application.Run(new LightningLoaderForm());
#endif
        }
    } // class
} // namespace
