using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MonkeyLightning.DataProvider;

namespace MonkeyLightning.Framework
{
    public class RuleValue
    {
        public event ValueUpdateEventHandler ValueUpdated;

        public object Value { get { return value; } }
        public RuleValueType ValueType { get { return dataProvider.ValueType; } }
        public IDataProvider DataProvider { get { return dataProvider; } }

        private object value;
        private IDataProvider dataProvider;


        private RuleValue()
        {
        }

        /// <summary>
        /// To get a RuleValue that is a constant, just pass the "constant" value
        /// to the constructor.
        /// </summary>
        /// <param name="valueType">type of value we are storing (RuleValueType enum)</param>
        /// <param name="initialValue">value to set this RuleValue</param>
        public RuleValue(IDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
            dataProvider.DataUpdate += dataProvider_DataUpdate;
        }

        void dataProvider_DataUpdate(object sender, ValueUpdateEventArgs e)
        {
            UpdateValue(e.UpdatedValue);
        }

        public void UpdateValue(object updatedValue)
        {
            value = updatedValue;
            if (ValueUpdated != null) ValueUpdated(this, new ValueUpdateEventArgs(updatedValue));
        }

        public override string ToString()
        {
            return string.Format(dataProvider.DisplayFormat, value);
        }

        public bool IsNumeric
        {
            get
            {
                if (dataProvider.ValueType == RuleValueType.DOUBLE || dataProvider.ValueType == RuleValueType.INT)
                    return true;
                else
                    return false;
            }
        }

        public bool IsText { get { return (dataProvider.ValueType == RuleValueType.TEXT); } }
        public bool IsTime { get { return (dataProvider.ValueType == RuleValueType.TIME); } }
        public bool IsTimeRange { get { return (dataProvider.ValueType == RuleValueType.TIME_RANGE); } }
        public bool IsTimeSpan { get { return (dataProvider.ValueType == RuleValueType.TIME_SPAN); } }

        #region implicit conversions
        static public implicit operator string(RuleValue v)
        {
            return v.Value.ToString();
        }

        static public implicit operator decimal(RuleValue v)
        {
            return Convert.ToDecimal(v.Value);
        }

        static public implicit operator double(RuleValue v)
        {
            if (v.Value is double)
                return (double)v.Value;
            else
                return Convert.ToDouble(v.Value);
        }

        static public implicit operator DateTime(RuleValue v)
        {
            return Convert.ToDateTime(v.Value);
        }

        static public implicit operator TimeSpan(RuleValue v)
        {
            return (TimeSpan)v.Value;
        }
        
        /*static public implicit operator SimpleTime(RuleValue v)
        {
            return v.Value as SimpleTime;
        }*/
        #endregion


    } // class

} // namespace
