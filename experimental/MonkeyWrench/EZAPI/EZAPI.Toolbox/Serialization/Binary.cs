using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using EZAPI.Toolbox.Debug;

namespace EZAPI.Toolbox.Serialization
{
    public static class Binary
    {
        public static bool Serialize(object saveObject, string filename)
        {
            bool result = false;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename, FileMode.Create);
                formatter.Serialize(stream, saveObject);
                stream.Flush();
                stream.Close();
                stream.Dispose();
                formatter = null;
                result = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TraceExceptionDetail(ex);
            }

            return result;
        }

        public static object Deserialize(string filename)
        {
            object result = null;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename, FileMode.Create);
                result = formatter.Deserialize(stream);
                stream.Flush();
                stream.Close();
                stream.Dispose();
                formatter = null;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TraceExceptionDetail(ex);
            }

            return result;
        }

    } // class
} // namespace
