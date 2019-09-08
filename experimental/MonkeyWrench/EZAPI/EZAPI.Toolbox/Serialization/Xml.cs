using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using EZAPI.Toolbox.Debug;

namespace EZAPI.Toolbox.Serialization
{
    public static class Xml
    {
        public static bool Serialize(object saveObject, Type type, string filename)
        {
            bool result = false;

            try
            {
                XmlSerializer serializer = new XmlSerializer(type);
                Stream stream = new FileStream(filename, FileMode.Create);
                serializer.Serialize(stream, saveObject);
                stream.Flush();
                stream.Close();
                stream.Dispose();
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
                XmlSerializer serializer = new XmlSerializer(type);
                Stream stream = new FileStream(filename, FileMode.Open);
                result = serializer.Deserialize(stream);
                stream.Flush();
                stream.Close();
                stream.Dispose();
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


/************************************************
 *** EXAMPLE OF IMPLEMENTING IXmlSerializable ***
 ************************************************
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "WeeklyTimeRange")
            {
                dailyTimeRanges.ReadXml(reader);
                _name = reader["Name"];
                _enabled = Boolean.Parse(reader["Enabled"]);
                _color = Color.FromArgb(Int32.Parse(reader["Color"]));

                if (reader.ReadToDescendant("MyEvent"))
                {
                    while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "MyEvent")
                    {
                        MyEvent evt = new MyEvent();
                        evt.ReadXml(reader);
                        _events.Add(evt);
                    }
                }
                reader.Read();
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            dailyTimeRanges.WriteXml(writer);
            writer.WriteAttributeString("Name",    _name);
            writer.WriteAttributeString("Enabled", _enabled.ToString());
            writer.WriteAttributeString("Color",   _color.ToArgb().ToString());

            foreach (MyEvent evt in _events)
            {
                writer.WriteStartElement("MyEvent");
                evt.WriteXml(writer);
                writer.WriteEndElement();
            }
        }


public class MyEvent : IXmlSerializable
{
    private string _title;
    private DateTime _start;
    private DateTime _stop;


    public XmlSchema GetSchema() { return null; }

    public void ReadXml(XmlReader reader)
    {
        if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "MyEvent")
        {
            _title = reader["Title"];
            _start = DateTime.FromBinary(Int64.Parse(reader["Start"]));
            _stop  = DateTime.FromBinary(Int64.Parse(reader["Stop"]));
            reader.Read();
        }
    }

    public void WriteXml(XmlWriter writer)
    {
        writer.WriteAttributeString("Title", _title);
        writer.WriteAttributeString("Start", _start.ToBinary().ToString());
        writer.WriteAttributeString("Stop",  _stop.ToBinary().ToString());
    }
}
*/
