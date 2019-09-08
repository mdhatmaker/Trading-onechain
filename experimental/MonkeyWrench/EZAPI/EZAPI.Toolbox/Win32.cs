using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace EZAPI.Toolbox
{
    public static class Win32
    {
        // constants for alpha blending
        public const byte AC_SRC_OVER = 0;
        public const Int32 ULW_ALPHA = 2;
        public const byte AC_SRC_ALPHA = 1;

        // set tab stops to a width of 4
        public const int EM_SETTABSTOPS = 0x00CB;

        [Flags]
        public enum AnimateWindowFlags
        {
             AW_HOR_POSITIVE = 0x00000001,
             AW_HOR_NEGATIVE = 0x00000002,
             AW_VER_POSITIVE = 0x00000004,
             AW_VER_NEGATIVE = 0x00000008,
             AW_CENTER = 0x00000010,
             AW_HIDE = 0x00010000,
             AW_ACTIVATE = 0x00020000,
             AW_SLIDE = 0x00040000,
             AW_BLEND = 0x00080000
        }

        public enum WindowState
        {
            SW_HIDE =            0,
            SW_SHOWNORMAL =      1,
            SW_NORMAL =          1,
            SW_SHOWMINIMIZED =   2,
            SW_SHOWMAXIMIZED =   3,
            SW_MAXIMIZE =        3,
            SW_SHOWNOACTIVATE =  4,
            SW_SHOW =            5,
            SW_MINIMIZE =        6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA =          8,
            SW_RESTORE =         9,
            SW_SHOWDEFAULT =     10,
            SW_FORCEMINIMIZE =   11,
            SW_MAX =             11
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Size
        {
            public Int32 cx;
            public Int32 cy;

            public Size(Int32 x, Int32 y)
            {
                cx = x;
                cy = y;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public Int32 x;
            public Int32 y;

            public Point(Int32 x, Int32 y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr h, int msg, int wParam, int[] lParam);

        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("User32.Dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref Point point);
        
        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObj);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int DeleteObject(IntPtr hObj);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern int UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pptSrc, Int32 crKey, ref BLENDFUNCTION pblend, Int32 dwFlags);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr ExtCreateRegion(IntPtr lpXform, uint nCount, IntPtr rgnData);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string fileName);

        [DllImport("user32.dll")]
        public static extern int GetKeyboardState(byte[] keystate);

        [DllImport("User32.dll")]
        public static extern int SetClipboardViewer(int hWndNewViewer);
        
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool AnimateWindow(IntPtr hWnd, int time, AnimateWindowFlags flags);

        [DllImport("User32")]
        public static extern int ShowWindow(int hwnd, int nCmdShow);

        [DllImport("winmm.dll")]
        public static extern int WaveOutGetVolume(IntPtr hwo, out uint dwVolume);

        [DllImport("winmm.dll")]
        public static extern int WaveOutSetVolume(IntPtr hwo, uint dwVolume);

/* Example usage:
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            byte[] keys = new byte[255];

            GetKeyboardState(keys);

            if (keys[(int)Keys.Up] == 129 && keys[(int)Keys.Right] == 129)
            {
                Console.WriteLine("Up Arrow key and Right Arrow key down.");
            }
        }
*/

    } // class
} // namespace
