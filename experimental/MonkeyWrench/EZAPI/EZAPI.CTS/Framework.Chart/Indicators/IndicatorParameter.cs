using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Charting = System.Windows.Forms.DataVisualization.Charting;

namespace EZAPI.Framework.Chart.Indicators
{
    public interface IIndicatorParameter
    {
        string Name { get; }
        Type DataType { get; }
        string Description { get; }
        object Value { get; set; }
    }

    public interface IIndicatorParameter<T> : IIndicatorParameter
    {
        new T Value { get; }
    }

    //public delegate void DrawIndicator(Charting.Chart chart, IndicatorParameterList parameters);

    /*/// <summary>
    /// We need this abstract class so that we can create a List of IndicatorParameter generic objects
    /// </summary>
    public abstract class IndicatorParameter
    {
        public abstract string Name { get; }
        public abstract Type DataType { get; }
        public abstract object Value { get; }
    }*/

    public class IndicatorParameter<T> : IIndicatorParameter<T>
        where T : struct
    {
        public string Name { get { return parameterName; } }
        public Type DataType { get { return typeof(T); } }
        public string Description { get { return description; } }
        public T DefaultValue { get { return defaultValue; } }
        public T Value { get; set; }
        object IIndicatorParameter.Value
        {
            get { return Value; }
            set { Value = (T)value; }
        }
        string parameterName;
        string description;
        T defaultValue;

        public IndicatorParameter(string name, string description, T defaultValue)
        {
            this.parameterName = name;
            this.description = description;
            this.defaultValue = defaultValue;
            this.Value = defaultValue;
        }

        #region Equals and == overrides
        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ Name.Length;
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            IndicatorParameter<T> ip = obj as IndicatorParameter<T>;
            if ((System.Object)ip == null)
                return false;

            return Equals(ip);
        }

        public bool Equals(IndicatorParameter<T> ip)
        {
            bool result = true;

            if ((object)ip == null)
                return false;

            if (Name != ip.Name)
                result = false;
            else if (DataType != ip.DataType)
                result = false;
            else if (Description != ip.Description)
                result = false;
            else if (!DefaultValue.Equals(ip.DefaultValue))
                result = false;
            else if (!Value.Equals(ip.Value))
                result = false;

            return result;
        }

        public static bool operator ==(IndicatorParameter<T> ip1, IndicatorParameter<T> ip2)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(ip1, ip2))
                return true;

            // If one is null, but not both, return false.
            if (((object)ip1 == null) || ((object)ip2 == null))
                return false;

            return ip1.Equals(ip2);
        }

        public static bool operator !=(IndicatorParameter<T> ip1, IndicatorParameter<T> ip2)
        {
            return !(ip1 == ip2);
        }
        #endregion

    } // class (IndicatorParameter<T>)

    public class IndicatorParameterList
    {
        public int Count { get { return parameters.Count; } }

        Dictionary<string, IIndicatorParameter> parameters;

        public IndicatorParameterList()
        {
            parameters = new Dictionary<string, IIndicatorParameter>();
        }

        public void Add(IIndicatorParameter parameter)
        {
            parameters[parameter.Name] = parameter;
        }

        public IIndicatorParameter this[string name]
        {
            get { return parameters[name]; }
        }

        public IIndicatorParameter this[int index]
        {
            get { return parameters.Values.ElementAt(index); }
        }

        public bool IsValid
        {
            get { return true; }
        }

        #region Equals and == overrides
        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ Count;
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            IndicatorParameterList ipl = obj as IndicatorParameterList;
            if ((System.Object)ipl == null)
                return false;

            return Equals(ipl);
        }

        public bool Equals(IndicatorParameterList ipl)
        {
            bool result = true;

            if ((object)ipl == null)
                return false;

            if (parameters.Count != ipl.parameters.Count)
                result = false;
            else
            {
                foreach (string key in parameters.Keys)
                {
                    if (!ipl.parameters.ContainsKey(key) && parameters[key] != ipl.parameters[key])
                    {
                        result = false;
                        break;
                    }
                }
            }

            return result;
        }

        public static bool operator ==(IndicatorParameterList ipl1, IndicatorParameterList ipl2)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(ipl1, ipl2))
                return true;

            // If one is null, but not both, return false.
            if (((object)ipl1 == null) || ((object)ipl2 == null))
                return false;

            return ipl1.Equals(ipl2);
        }

        public static bool operator !=(IndicatorParameterList ipl1, IndicatorParameterList ipl2)
        {
            return !(ipl1 == ipl2);
        }
        #endregion

    } // class (IndicatorParameterList)


} // namespace
