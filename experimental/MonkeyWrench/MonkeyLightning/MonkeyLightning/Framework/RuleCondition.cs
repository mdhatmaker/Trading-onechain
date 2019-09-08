using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using MonkeyLightning.DataProvider;
using MonkeyLightning.Framework.Comparison;
using MonkeyLightning.Framework.Xml;

namespace MonkeyLightning.Framework
{
    public class RuleCondition //: ISerializable
    {
        public event EventHandler ValueChanged;

        public bool Active { get; set; }

        public bool Value { get { return compare.CompareTo(value1, value2); } }
        public string RuleValue1 { get { return value1.ToString(); } }
        public string RuleComparison { get { return compare.Name; } }
        public string RuleValue2 { get { return value2.ToString(); } }

        public RuleValue Value1 { get { return value1; } }
        public IRuleComparison Comparison { get { return compare; } }
        public RuleValue Value2 { get { return value2; } }

        private RuleValue value1;
        private IRuleComparison compare;
        private RuleValue value2;

        private RuleCondition()
        {

        }

        public RuleCondition(RuleValue value1, IRuleComparison compare, RuleValue value2)
        {
            this.value1 = value1;
            this.compare = compare;
            this.value2 = value2;

            value1.ValueUpdated += value_ValueUpdated;
            value2.ValueUpdated += value_ValueUpdated;
        }

        void value_ValueUpdated(object sender, ValueUpdateEventArgs e)
        {
            if (ValueChanged != null) ValueChanged(this, new EventArgs());
        }

        public bool IsValid
        {
            get
            {
                bool result = true;

                if (value1 == null || value2 == null || compare == null)
                    result = false;
                else if (compare.IsValid(value1, value2) == false)
                    result = false;

                return result;
            }
        }

        public override string ToString()
        {
            return value1.Value.ToString() + " " + compare.Symbol + " " + value2.Value.ToString();
        }

        public XmlSaveRuleCondition SaveData
        {
            get
            {
                XmlSaveRuleCondition save = new XmlSaveRuleCondition();
                save.Compare = new XmlSaveRuleComparison();
                save.Compare.Name = compare.Name;
                save.Compare.Symbol = compare.Symbol;

                save.Value1 = value1.DataProvider.SaveData;
                save.Value2 = value2.DataProvider.SaveData;

                return save;
            }
        }

    } // class
} // namespace
