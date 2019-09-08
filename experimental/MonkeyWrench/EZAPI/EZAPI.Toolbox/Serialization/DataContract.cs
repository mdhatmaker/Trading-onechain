using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using EZAPI.Toolbox.Debug;

namespace EZAPI.Toolbox.Serialization
{
    public static class DataContract
    {
        public static bool Serialize(object saveObject, Type type, string filename)
        {
            bool result = false;

            try
            {
                DataContractSerializer serializer = new DataContractSerializer(type);
                using (XmlWriter writer = XmlWriter.Create(filename))
                {
                    serializer.WriteObject(writer, saveObject);
                    writer.Close();
                }
                serializer = null;
                result = true;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TraceExceptionDetail(ex);
            }

            return result;
        }

        public static object Deserialize(Type type, string filename)
        {
            object result = null;

            try
            {
                DataContractSerializer serializer = new DataContractSerializer(type);
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    result = serializer.ReadObject(reader);
                    reader.Close();
                }
                serializer = null;
            }
            catch (Exception ex)
            {
                ExceptionHandler.TraceExceptionDetail(ex);
            }

            return result;
        }

    } // class
} // namespace
