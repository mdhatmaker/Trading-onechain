using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace EZAPI.Toolbox
{
    public enum FormRelativePosition { CENTER, LEFT, RIGHT, TOP, BOTTOM }

    public static class WinForms
    {
        public static void SetFormRelativePosition(Form child, Control parent, FormRelativePosition position = FormRelativePosition.CENTER)
        {
            int padding = 10;

            int childX = 0;
            int childY = 0;

            switch (position)
            {
                case FormRelativePosition.CENTER:
                    int midX = parent.Location.X + (parent.Width / 2);
                    int midY = parent.Location.Y + (parent.Height / 2);
                    childX = midX - child.Width / 2;
                    childY = midY - child.Height / 2;
                    break;
                case FormRelativePosition.LEFT:
                    childX = parent.Location.X - child.Width - padding;
                    childY = parent.Location.Y + (parent.Height - child.Height) / 2;
                    break;
                case FormRelativePosition.RIGHT:
                    childX = parent.Location.X + parent.Width + padding;
                    childY = parent.Location.Y + (parent.Height - child.Height) / 2;
                    break;
                case FormRelativePosition.TOP:
                    childX = parent.Location.X + (parent.Width - child.Width) / 2;
                    childY = parent.Location.Y - child.Height - padding;
                    break;
                case FormRelativePosition.BOTTOM:
                    childX = parent.Location.X + (parent.Width - child.Width) / 2;
                    childY = parent.Location.Y + parent.Height + padding;
                    break;
            }

            // Try to ensure we are on the screen.
            childX = Math.Min(childX, Screen.GetWorkingArea(child).Width - child.Width);
            childY = Math.Min(childY, Screen.GetWorkingArea(child).Height - child.Height);
            childX = Math.Max(childX, 0);
            childY = Math.Max(childY, 0);

            child.StartPosition = FormStartPosition.Manual;
            child.SetDesktopLocation(childX, childY);
        }

        // Try to ensure that a Form is viewable on the screen.
        public static void EnsureFormOnScreen(Form child)
        {
            if (!Screen.GetWorkingArea(child).Contains(child.Bounds))
            {
                int childX = child.Bounds.X;
                int childY = child.Bounds.Y;

                childX = Math.Min(childX, Screen.GetWorkingArea(child).Width - child.Width);
                childY = Math.Min(childY, Screen.GetWorkingArea(child).Height - child.Height);
                childX = Math.Max(childX, 0);
                childY = Math.Max(childY, 0);

                child.StartPosition = FormStartPosition.Manual;
                child.SetDesktopLocation(childX, childY);
            }
        }

        /// <summary>
        /// Set "wait" cursor for the entire application (or restore default cursor if showWaitCursor is false).
        /// </summary>
        /// <param name="showWaitCursor">true to show the Wait cursor; false to show the default cursor</param>
        public static void SetWaitCursor(bool showWaitCursor)
        {
            if (showWaitCursor == true)
            {
                Cursor.Current = Cursors.WaitCursor;
                Cursor.Show();
            }
            else
            {
                Cursor.Current = Cursors.Default;
                Cursor.Show();
            }
        }

        public static void SetWindowState(string processName, Win32.WindowState windowState)
        {
            int hWnd;
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning)
            {
                Console.WriteLine(pr.ProcessName);
                if (pr.ProcessName == processName)
                {
                    hWnd = pr.MainWindowHandle.ToInt32();
                    SetWindowState(hWnd, windowState);
                }
            }
        }

        public static void SetWindowState(int hWnd, Win32.WindowState windowState)
        {
            Win32.ShowWindow(hWnd, (int)windowState);
        }

        public static void OutlineControlRect(Control control, Color color, double lineWidth)
        {
            Graphics gfx = control.CreateGraphics();
            Rectangle rect = control.Bounds;
            float width = (float)lineWidth;
            Pen pen = new Pen(color, width);
            gfx.DrawRectangle(pen, rect);
            gfx.Dispose();
        }

        // Lighten an image using alpha masking (new - lighter - image is returned).
        public static Image LightenImage(Image imgLight, int alpha, Color colorToFadeTo)
        {
            int nRed = colorToFadeTo.R;
            int nGreen = colorToFadeTo.G;
            int nBlue = colorToFadeTo.B;

            //convert image to graphics object
            Graphics graphics = Graphics.FromImage(imgLight);
            int conversion = alpha;     // (5 * (level - 50)); //calculate new alpha value
            //create mask with blended alpha value and chosen color as pen 
            Pen pLight = new Pen(Color.FromArgb(conversion, nRed, nGreen, nBlue), imgLight.Width * 2);
            //apply created mask to graphics object
            graphics.DrawLine(pLight, -1, -1, imgLight.Width, imgLight.Height);
            //save created graphics object and modify image object by that
            graphics.Save();
            graphics.Dispose(); //dispose graphics object
            return imgLight; //return modified image
        }

        // This can be called in the constructor of your Form, but beware: Make sure 
        // InitializeComponents is run first.
        // Also, we should dispose graphics...perhaps put it in using statement.
        public static void SetTabWidth(TextBox textbox, int tabWidth)
        {
            using (Graphics graphics = textbox.CreateGraphics())
            {
                var characterWidth = (int)graphics.MeasureString("M", textbox.Font).Width;
                Win32.SendMessage(textbox.Handle, Win32.EM_SETTABSTOPS, 1,
                            new int[] { tabWidth * characterWidth });
            }
        }

        public static Cursor ActuallyLoadCursor(String path)
        {
            return new Cursor(Win32.LoadCursorFromFile(path));
        }

        /// <summary>
        /// Center the mouse pointer on a specified control (i.e. Button)
        /// </summary>
        /// <param name="control">Control on which to center mouse pointer</param>
        public static void CenterMousePointer(Control control)
        {
            Win32.Point p = new Win32.Point();
            p.x = control.Left + (control.Width / 2);
            p.y = control.Top + (control.Height / 2);

            Win32.ClientToScreen(control.FindForm().Handle, ref p);
            Win32.SetCursorPos(p.x, p.y);
        }

        public static void SetBits(Form form, Bitmap bitmap)
        {
            //if (!haveHandle) return;
            if (!Bitmap.IsCanonicalPixelFormat(bitmap.PixelFormat) ||
                !Bitmap.IsAlphaPixelFormat(bitmap.PixelFormat))
                throw new ApplicationException("The picture must be " +
                          "32bit picture with alpha channel");
            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.CreateCompatibleDC(screenDC);
            try
            {
                
                Win32.Point topLoc = new Win32.Point(form.Left, form.Top);
                Win32.Size bitMapSize = new Win32.Size(bitmap.Width, bitmap.Height);
                Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();
                Win32.Point srcLoc = new Win32.Point(0, 0);
                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32.SelectObject(memDc, hBitmap);
                blendFunc.BlendOp = Win32.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = 255;
                blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;
                Win32.UpdateLayeredWindow(form.Handle, screenDC, ref topLoc, ref bitMapSize,
                                 memDc, ref srcLoc, 0, ref blendFunc, Win32.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBits);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.ReleaseDC(IntPtr.Zero, screenDC);
                Win32.DeleteDC(memDc);
            }


        }
    } // class

    public class TextBoxEx : TextBox
    {
        public TextBoxEx()
        { }

        public void GoTo(int line, int column)
        {
            if (line < 1 || column < 1 || this.Lines.Length < line)
                return;

            this.SelectionStart = this.GetFirstCharIndexFromLine(line - 1) + column - 1;
            this.SelectionLength = 0;
        }

        public int CurrentColumn
        {
            get { return this.SelectionStart - this.GetFirstCharIndexOfCurrentLine() + 1; }
        }

        public int CurrentLine
        {
            get { return this.GetLineFromCharIndex(this.SelectionStart) + 1; }
        }
    }
} // namespace
