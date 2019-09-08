using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Drawing;
using System.Resources;

namespace EZAPI.Settings
{
    public static class Fonts
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
        
        static FontFamily ff;
        static Font font;

        public static void LoadFontCollection()
        {
            // Create the byte array and get its length
            byte[] fontArray = Properties.Resources.DroidSansMono;
            int dataLength = Properties.Resources.DroidSansMono.Length;

            // Assign memory and copy byte[] on that memory address
            IntPtr ptrData = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontArray, 0, ptrData, dataLength);

            uint cFonts = 0;
            AddFontMemResourceEx(ptrData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            PrivateFontCollection pfc = new PrivateFontCollection();
            // Pass the font to the PrivateFontCollection object
            pfc.AddMemoryFont(ptrData, dataLength);

            // Free the "unsafe" memory
            Marshal.FreeCoTaskMem(ptrData);

            ff = pfc.Families[0];
            font = new Font(ff, 15f, FontStyle.Bold);
        }

        private static Font GetFont()
        {
            float size = 11f;
            FontStyle fontStyle = FontStyle.Regular;

            return new Font(ff, size, fontStyle);
        }



    } // class
} // namespace
