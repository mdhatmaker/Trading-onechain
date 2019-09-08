using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonkeyLightning.Framework.Comparison
{
    public interface IRuleComparison
    {
        string Name { get; }
        string Symbol { get; }
        string EnglishDescription { get; }
        bool CompareTo(RuleValue value1, RuleValue value2);
        bool IsValid(RuleValue value1, RuleValue value2);
    } // interface
} // namespace
