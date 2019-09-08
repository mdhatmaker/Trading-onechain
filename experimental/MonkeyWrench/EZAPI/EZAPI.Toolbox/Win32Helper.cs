using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EZAPI.Toolbox
{
    public static class Win32Helper
    {
        static IntPtr nextClipboardViewer;

        public static bool IsKeyDown(Keys keyToTest)
        {
            byte[] keys = new byte[255];
            Win32.GetKeyboardState(keys);

            if (keys[(int)keyToTest] == 129 || keys[(int)keyToTest] == 128)
                return true;
            else
                return false;
        }

        /// <summary>
        /// This effect is good for a Form's load event handler.
        /// </summary>
        /// <param name="form">Form to fade in</param>
        /// <param name="milliseconds">time (in milliseconds) for the transition</param>
        public static void FadeInForm(Form form, int milliseconds = 500)
        {
            Win32.AnimateWindow(form.Handle, milliseconds, Win32.AnimateWindowFlags.AW_BLEND);
        }

        /// <summary>
        /// This effect is good for a Form's "Form_closing" event handler.
        /// </summary>
        /// <param name="form">Form to fade out.</param>
        /// <param name="milliseconds">time (in milliseconds) for the transition</param>
        public static void FadeOutForm(Form form, int milliseconds = 1000)
        {
            Win32.AnimateWindow(form.Handle, milliseconds, Win32.AnimateWindowFlags.AW_BLEND | Win32.AnimateWindowFlags.AW_HIDE);
        }

        #region ClipboardMonitor
        /*
        Within the Form whose handle you pass to SetClipboardViewer, you must override the
        WndProc method as follows:

        protected override void
                  WndProc(ref System.Windows.Forms.Message m)
        {
          // defined in winuser.h
          const int WM_DRAWCLIPBOARD = 0x308;
          const int WM_CHANGECBCHAIN = 0x030D;
 
          switch(m.Msg)
          {
            case WM_DRAWCLIPBOARD:
              DisplayClipboardData();
              SendMessage(nextClipboardViewer, m.Msg, m.WParam,
                          m.LParam);
            break;
 
            case WM_CHANGECBCHAIN:
              if (m.WParam == nextClipboardViewer)
                nextClipboardViewer = m.LParam;
              else
                SendMessage(nextClipboardViewer, m.Msg, m.WParam,
                            m.LParam);
            break;
 
            default:
              base.WndProc(ref m);
            break;
          }
        }        
        */

        public static string WndProcOverride(ref System.Windows.Forms.Message m)
        {
            // defined in winuser.h
            const int WM_DRAWCLIPBOARD = 0x308;
            const int WM_CHANGECBCHAIN = 0x030D;

            string clipboardData = null;

            switch (m.Msg)
            {
                case WM_DRAWCLIPBOARD:
                    clipboardData = GetClipboardText();
                    Win32.SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;

                case WM_CHANGECBCHAIN:
                    if (m.WParam == nextClipboardViewer)
                        nextClipboardViewer = m.LParam;
                    else
                        Win32.SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
                    break;
            }

            return clipboardData;
        }

        public static string GetClipboardText()
        {
            string clipboardText = null;

            try
            {
                IDataObject iData = new DataObject();
                iData = Clipboard.GetDataObject();

                if (iData.GetDataPresent(DataFormats.Rtf))
                    clipboardText = (string)iData.GetData(DataFormats.Rtf);
                else if (iData.GetDataPresent(DataFormats.Text))
                    clipboardText = (string)iData.GetData(DataFormats.Text);
                else
                    //clipboardTExt = "[Clipboard data is not RTF or ASCII Text]";
                    throw new FormatException("[Clipboard data is not RTF or ASCII Text]");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.ToString());
                //MessageBox.Show(e.ToString());
            }

            return clipboardText;
        }

        /// <summary>
        /// Start monitoring the clipboard using Win32 calls
        /// </summary>
        /// <param name="handle">the windows Form that will receive the clipboard messages</param>
        public static void StartClipboardMonitor(IntPtr handle)
        {
            nextClipboardViewer = (IntPtr)Win32.SetClipboardViewer((int)handle);
        }

        public static void StopClipboardMonitor(IntPtr handle)
        {
            Win32.ChangeClipboardChain(handle, nextClipboardViewer);
        }
        #endregion

    } // class
} // namespace
