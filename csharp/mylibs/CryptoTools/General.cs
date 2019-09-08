using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoTools.General
{
    public static class General
    {
        private static DateTime m_unixTimestampZero = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static string GetByteString(byte[] array)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                sb.AppendFormat("{0:x2}", array[i]);
            }
            return sb.ToString();
        }


        public static int ToTimestampSeconds(DateTime dt)
        {
            return (int)(dt.ToUniversalTime() - m_unixTimestampZero).TotalSeconds;
        }

        public static long ToTimestampMilliseconds(DateTime dt)
        {
            return (long)(dt.ToUniversalTime() - m_unixTimestampZero).TotalMilliseconds;
        }

        // TODO: Convert from UTC to local DateTime
        public static DateTime FromTimestampSeconds(int unixtimeSeconds)
        {
            return m_unixTimestampZero.AddSeconds(unixtimeSeconds).ToLocalTime();
        }

        public static DateTime FromTimestampMilliseconds(long unixtimeMilliseconds)
        {
            return m_unixTimestampZero.AddMilliseconds(unixtimeMilliseconds).ToLocalTime();
        }


        // Print the byte array in a readable format.
        public static void PrintByteArray(byte[] array)
        {
            int i;
            for (i = 0; i < array.Length; i++)
            {
                Console.Write(String.Format("{0:X2}", array[i]));
                if ((i % 4) == 3) Console.Write(" ");
            }
            Console.WriteLine();
        }

        // Print the byte array in a readable format.
        public static void PrintByteArrayPretty(byte[] array)
        {
            int i;
            for (i = 0; i < array.Length; i++)
            {
                Console.Write(String.Format("{0:X2}", array[i]));
                if ((i % 4) == 3) Console.Write(" ");
            }
            Console.WriteLine();
        }

        // Get path of folder containinng the currently executing assembly
        public static string GetExeDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        // Determine whether or not the specified decimal represents an integer number
        public static bool IsInteger(decimal d)
        {
            return ((d % 1) == 0);
        }


        //----- EXTENSION METHODS ---------------------------------------------------------------------
        public static string ToDisplay(this DateTime dt)
		{
			return dt.ToString("yyyy-MM-dd HH:mm:ss");
		}
        //---------------------------------------------------------------------------------------------

    } // end of class
} // end of namespace
