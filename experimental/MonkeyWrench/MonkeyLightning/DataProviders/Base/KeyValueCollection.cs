using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZAPI.Framework.Base;
using EZAPI.Toolbox.Serialization;
using EZAPI.Toolbox.Time;

namespace MonkeyLightning.DataProvider
{
    public class KeyValueCollection
    {
        private SerializableDictionary<string, ValueString> keyValuePairs;

        private KeyValueCollection()
        {
        }

        // Once the keyValuePairs Dictionary is created in the constructor, its set of valid
        // keys cannot be changed.
        //
        // This collection will compensate for capitalization (always using ALL CAPS for the property name lookup).
        //
        public KeyValueCollection(string[] propertyNames)
        {
            keyValuePairs = new SerializableDictionary<string, ValueString>();

            if (propertyNames != null)
            {
                foreach (string property in propertyNames)
                {
                    keyValuePairs[FormatKey(property)] = null;
                }
            }
        }

        public ValueString this[string key]
        {
            get
            {
                if (key == null)
                    throw new ArgumentNullException("key", "The key cannot be null for this ControlValues dictionary.");
                else if (keyValuePairs.ContainsKey(FormatKey(key)))
                    return keyValuePairs[FormatKey(key)];
                else
                    throw new ArgumentOutOfRangeException(key, "The key is not valid for this ControlValues dictionary.");
            }
            set
            {
                if (key == null)
                    throw new ArgumentNullException("key", "The key cannot be null for this ControlValues dictionary.");
                else if (keyValuePairs.ContainsKey(FormatKey(key)))
                    keyValuePairs[FormatKey(key)] = value;
                else
                    throw new ArgumentOutOfRangeException(key, "The key is not valid for this ControlValues dictionary.");
            }
        }

        string FormatKey(string key)
        {
            return key.ToUpper().Trim();
        }

        public SerializableDictionary<string, ValueString> GetSerializableDictionary()
        {
            return keyValuePairs;
        }

        public void SetSerializableDictionary(SerializableDictionary<string, ValueString> dict)
        {
            keyValuePairs = dict;
        }

    } // class

    public class ValueString : IConvertible 
    {
        public string Value { get; set; }

        private ValueString()
        {
        }

        private ValueString(string str)
        {
            Value = str;
        }

        public override string ToString()
        {
            return Value;
        }

        // CONVERT TO/FROM STRING
        public static implicit operator string(ValueString vs)
        {
            if (vs == null)
                return null;
            else
                return vs.Value;
        }

        public static implicit operator ValueString(string str)
        {
            return new ValueString(str);
        }

        // CONVERT TO/FROM INT
        public static implicit operator int(ValueString vs)
        {
            return Convert.ToInt32(vs.Value);
        }

        public static implicit operator ValueString(int i)
        {
            return new ValueString(i.ToString());
        }

        // CONVERT TO/FROM DECIMAL
        public static implicit operator decimal(ValueString vs)
        {
            return Convert.ToDecimal(vs.Value);
        }

        public static implicit operator ValueString(decimal d)
        {
            return new ValueString(d.ToString());
        }

        // CONVERT TO/FROM DOUBLE
        public static implicit operator double(ValueString vs)
        {
            return Convert.ToDouble(vs.Value);
        }

        public static implicit operator ValueString(double d)
        {
            return new ValueString(d.ToString());
        }

        // CONVERT TO/FROM BOOL
        public static implicit operator bool(ValueString vs)
        {
            return Convert.ToBoolean(vs.Value);
        }

        public static implicit operator ValueString(bool b)
        {
            return new ValueString(b.ToString());
        }

        // CONVERT TO/FROM zDAYOFWEEK ENUM
        public static implicit operator zDayOfWeek(ValueString vs)
        {
            string str = vs.Value;
            return (zDayOfWeek)Enum.Parse(typeof(zDayOfWeek), str, true);
        }

        public static implicit operator ValueString(zDayOfWeek weekDay)
        {
            return weekDay.ToString();
        }

        // CONVERT TO/FROM DATETIME
        public static implicit operator DateTime(ValueString vs)
        {
            return Convert.ToDateTime(vs.Value);
        }

        public static implicit operator ValueString(DateTime dt)
        {
            return new ValueString(dt.ToString());
        }

        // CONVERT TO/FROM DAILYTIMERANGE
        public static implicit operator DailyTimeRange(ValueString vs)
        {
            if (vs == null)
                return null;
            else
                return Convertz.ToDailyTimeRange(vs.Value);
        }

        public static implicit operator ValueString(DailyTimeRange dtr)
        {
            if (dtr == null)
                return new ValueString();
            else
                return new ValueString(dtr.ToString());
        }

        #region IConvertible IMPLEMENTATION
        public TypeCode GetTypeCode()
        {
            throw new NotImplementedException();
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public byte ToByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public double ToDouble(IFormatProvider provider)
        {
            return (double)this;
        }

        public short ToInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public int ToInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public long ToInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public float ToSingle(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }
        #endregion

    } // class

} // namespace
