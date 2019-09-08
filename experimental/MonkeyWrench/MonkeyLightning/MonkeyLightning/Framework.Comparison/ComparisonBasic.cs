using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonkeyLightning.Framework.Comparison
{
    public class CompareGreaterThan : IRuleComparison
    {
        public string Name { get { return "greater than"; } }
        public string Symbol { get { return ">"; } }
        public string EnglishDescription { get { return "is greater than"; } }

        public bool CompareTo(RuleValue value1, RuleValue value2)
        {
            if (value1.IsNumeric && value2.IsNumeric)
                return (double)value1 > (double)value2;
            else if (value1.IsText && value2.IsText)
                return value1.ToString().CompareTo(value2.ToString()) > 0;
            else if (value1.IsTime && value2.IsTime)
                return ((DateTime)value1).CompareTo((DateTime)value2) > 0;
            else if (value1.IsTimeSpan && value2.IsTimeSpan)
            {
                TimeSpan ts1 = (TimeSpan)value1;
                TimeSpan ts2 = (TimeSpan)value2;
                return ts1.CompareTo(ts2) > 0;
            }
            else
                throw new ArgumentException("CompareTo does not work with the value types provided.");
        }

        public bool IsValid(RuleValue value1, RuleValue value2)
        {
            bool result = false;

            if (value1.IsNumeric && value2.IsNumeric)
                result = true;
            else if (value1.IsText && value2.IsText)
                result = true;
            else if (value1.IsTime && value2.IsTime)
                result = true;
            else if (value1.IsTimeSpan && value2.IsTimeSpan)
                result = true;

            return result;
        }
    }

    public class CompareLessThan : IRuleComparison
    {
        public string Name { get { return "less than"; } }
        public string Symbol { get { return "<"; } }
        public string EnglishDescription { get { return "is less than"; } }

        public bool CompareTo(RuleValue value1, RuleValue value2)
        {
            if (value1.IsNumeric && value2.IsNumeric)
                return (double)value1 < (double)value2;
            else if (value1.IsText && value2.IsText)
                return value1.ToString().CompareTo(value2.ToString()) < 0;
            else if (value1.IsTime && value2.IsTime)
                return ((DateTime)value1).CompareTo((DateTime)value2) < 0;
            else if (value1.IsTimeSpan && value2.IsTimeSpan)
            {
                TimeSpan ts1 = (TimeSpan)value1;
                TimeSpan ts2 = (TimeSpan)value2;
                return ts1.CompareTo(ts2) < 0;
            }
            else
                throw new ArgumentException("CompareTo does not work with the value types provided.");
        }

        public bool IsValid(RuleValue value1, RuleValue value2)
        {
            bool result = false;

            if (value1.IsNumeric && value2.IsNumeric)
                result = true;
            else if (value1.IsText && value2.IsText)
                result = true;
            else if (value1.IsTime && value2.IsTime)
                result = true;
            else if (value1.IsTimeSpan && value2.IsTimeSpan)
                result = true;

            return result;
        }
    }

    public class CompareEqualTo : IRuleComparison
    {
        public string Name { get { return "equal"; } }
        public string Symbol { get { return "="; } }
        public string EnglishDescription { get { return "is equal to"; } }

        public bool CompareTo(RuleValue value1, RuleValue value2)
        {
            if (value1.IsNumeric && value2.IsNumeric)
                return (double)value1 == (double)value2;
            else if (value1.IsText && value2.IsText)
                return value1.ToString().CompareTo(value2.ToString()) == 0;
            else if (value1.IsTime && value2.IsTime)
                return ((DateTime)value1).CompareTo((DateTime)value2.Value) == 0;
            else if (value1.IsTimeSpan && value2.IsTimeSpan)
            {
                TimeSpan ts1 = (TimeSpan)value1;
                TimeSpan ts2 = (TimeSpan)value2;
                return ts1.CompareTo(ts2) == 0;
            }
            else
                throw new ArgumentException("CompareTo does not work with the value types provided.");
        }

        public bool IsValid(RuleValue value1, RuleValue value2)
        {
            bool result = false;

            if (value1.IsNumeric && value2.IsNumeric)
                result = true;
            else if (value1.IsText && value2.IsText)
                result = true;
            else if (value1.IsTime && value2.IsTime)
                result = true;
            else if (value1.IsTimeSpan && value2.IsTimeSpan)
                result = true;

            return result;
        }
    }

    public class CompareLessThanEqualTo : IRuleComparison
    {
        public string Name { get { return "less than or equal"; } }
        public string Symbol { get { return "<="; } }
        public string EnglishDescription { get { return "is less than or equal to"; } }

        public bool CompareTo(RuleValue value1, RuleValue value2)
        {
            if (value1.IsNumeric && value2.IsNumeric)
                return (double)value1 <= (double)value2;
            else if (value1.IsText && value2.IsText)
                return value1.ToString().CompareTo(value2.ToString()) <= 0;
            else if (value1.IsTime && value2.IsTime)
                return ((DateTime)value1).CompareTo((DateTime)value2) <= 0;
            else if (value1.IsTimeSpan && value2.IsTimeSpan)
            {
                TimeSpan ts1 = (TimeSpan)value1;
                TimeSpan ts2 = (TimeSpan)value2;
                return ts1.CompareTo(ts2) <= 0;
            }
            else
                throw new ArgumentException("CompareTo does not work with the value types provided.");
        }

        public bool IsValid(RuleValue value1, RuleValue value2)
        {
            bool result = false;

            if (value1.IsNumeric && value2.IsNumeric)
                result = true;
            else if (value1.IsText && value2.IsText)
                result = true;
            else if (value1.IsTime && value2.IsTime)
                result = true;
            else if (value1.IsTimeSpan && value2.IsTimeSpan)
                result = true;

            return result;
        }
    }

    public class CompareGreaterThanEqualTo : IRuleComparison
    {
        public string Name { get { return "greater than or equal"; } }
        public string Symbol { get { return ">="; } }
        public string EnglishDescription { get { return "is greater than or equal to"; } }

        public bool CompareTo(RuleValue value1, RuleValue value2)
        {
            if (value1.IsNumeric && value2.IsNumeric)
                return (double)value1 >= (double)value2;
            else if (value1.IsText && value2.IsText)
                return value1.ToString().CompareTo(value2.ToString()) >= 0;
            else if (value1.IsTime && value2.IsTime)
                return ((DateTime)value1).CompareTo((DateTime)value2) >= 0;
            else if (value1.IsTimeSpan && value2.IsTimeSpan)
            {
                TimeSpan ts1 = (TimeSpan)value1;
                TimeSpan ts2 = (TimeSpan)value2;
                return ts1.CompareTo(ts2) >= 0;
            }
            else
                throw new ArgumentException("CompareTo does not work with the value types provided.");
        }

        public bool IsValid(RuleValue value1, RuleValue value2)
        {
            bool result = false;

            if (value1.IsNumeric && value2.IsNumeric)
                result = true;
            else if (value1.IsText && value2.IsText)
                result = true;
            else if (value1.IsTime && value2.IsTime)
                result = true;
            else if (value1.IsTimeSpan && value2.IsTimeSpan)
                result = true;

            return result;
        }
    }

} // namespace
