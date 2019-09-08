using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonkeyLightning.DataProvider;
using EZAPI.Toolbox.Time;

namespace MonkeyLightning.Framework.Comparison
{
    [Serializable]
    public class CompareRangeContainsTime : IRuleComparison
    {
        public string Name { get { return "time range contains"; } }
        public string Symbol { get { return "<t>"; } }
        public string EnglishDescription { get { return "time range contains"; } }

        public bool CompareTo(RuleValue value1, RuleValue value2)
        {
            if (value1.IsTimeRange && value2.IsTime)
            {
                ITimeRange tr = value1.Value as ITimeRange;
                DateTime dt = Convert.ToDateTime(value2.Value);
                SimpleTime st = SimpleTime.FromDateTime(dt);
                return tr.ContainsTime(st);
            }
            else
                throw new ArgumentException("CompareTo does not work with the value types provided.");
        }

        public bool IsValid(RuleValue value1, RuleValue value2)
        {
            bool result = false;

            if (value1.IsTimeRange && value2.IsTime)
                result = true;

            return result;
        }
    } // class

    [Serializable]
    public class CompareTimeInRange : IRuleComparison
    {
        public string Name { get { return "time in range"; } }
        public string Symbol { get { return ">t<"; } }
        public string EnglishDescription { get { return "time is within range"; } }

        public bool CompareTo(RuleValue value1, RuleValue value2)
        {
            if (value1.IsTime && value2.IsTimeRange)
            {
                ITimeRange tr = value2.Value as ITimeRange;
                DateTime dt = Convert.ToDateTime(value1.Value);
                SimpleTime st = SimpleTime.FromDateTime(dt);
                return tr.ContainsTime(st);
            }
            else
                throw new ArgumentException("CompareTo does not work with the value types provided.");
        }

        public bool IsValid(RuleValue value1, RuleValue value2)
        {
            bool result = false;

            if (value1.IsTime && value2.IsTimeRange)
                result = true;

            return result;
        }
    } // class


} // namespace
