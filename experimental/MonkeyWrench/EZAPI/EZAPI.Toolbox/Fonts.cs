using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace EZAPI.Toolbox
{
    public static class Fonts
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
        
        static FontFamily ff;
        static Font font;

        static PrivateFontCollection private_fonts = new PrivateFontCollection();

        public static PrivateFontCollection PrivateFonts { get { return private_fonts; } }

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

        // To use this font after loading:
        // label1.Font = new Font(private_fonts.Families[0], 22)
        //
        // Also:
        // To use the memory font, text on a control must be rendered with GDI+. Use the
        // SetCompatibleTextRenderingDefault method, passing true, to set GDI+ rendering
        // on the application, or on individual controls by setting the control's
        // UseCompatibleTextRendering property to true.
        //
        // Something like this *could* work:
        // Application.SetCompatibleTextRenderingDefault(true);
        // ...but it can affect other controls in the program. Some control fonts could look ugly. So
        // better to specify GDI+ rendering only for chosen controls:
        // label1.UseCompatibleTextRendering = true;
        public static void LoadResourceFont(string fontName)
        {
            // specify embedded resource name
            //string resource = "embedded_font.PAGAP___.TTF";
            string resource = "TT.Hatmaker." + fontName;

            /*Assembly assembly = Assembly.GetExecutingAssembly();
            foreach (string name in assembly.GetManifestResourceNames().ToList<string>())
                Console.WriteLine(name);*/

            // receive resource stream
            Stream fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource);

            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);

            // create a buffer to read in to
            byte[] fontdata = new byte[fontStream.Length];

            // read the font data from the resource
            fontStream.Read(fontdata, 0, (int)fontStream.Length);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, (int)fontStream.Length);

            // pass the font to the font collection
            private_fonts.AddMemoryFont(data, (int)fontStream.Length);

            // close the resource stream
            fontStream.Close();

            // free up the unsafe memory
            Marshal.FreeCoTaskMem(data);
        }

    } // class
} // namespace
