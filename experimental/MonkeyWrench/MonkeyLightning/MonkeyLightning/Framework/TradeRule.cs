#define DEEPDEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using MonkeyLightning.Framework.Xml;
using EZAPI.Toolbox.Debug;

namespace MonkeyLightning.Framework
{
    public enum BooleanRuleCombination { AND, OR };
    public enum TradeRuleType { VALUE, TRIGGER };

    public class TradeRule
    {
        public event EventHandler ValueChanged;

        public bool Active { get; set; }
        public string Name { get; set; }
        public BooleanRuleCombination CombineRules { get; set; }
        public TradeRuleType RuleType { get; set; }
        public bool TriggerValue
        {
            get { return triggerValue; }
        }

        public List<RuleCondition> Conditions { get { return conditions; } }

        private List<RuleCondition> conditions;
        private bool triggerValue;

        private TradeRule()
        {
        }

        public TradeRule(string ruleName, BooleanRuleCombination combine, TradeRuleType ruleType = TradeRuleType.VALUE)
        {
            this.Name = ruleName;
            this.CombineRules = combine;
            this.RuleType = ruleType;
            this.triggerValue = false;

            conditions = new List<RuleCondition>();
        }

        public void AddRuleCondition(RuleCondition condition)
        {
            conditions.Add(condition);
            condition.ValueChanged += condition_ValueChanged;
        }

        public bool Evaluate()
        {
            bool result = false;

            //Console.WriteLine("Condition count: {0}", conditions.Count);
            if (conditions.Count > 0)
            {
                // Evaluate differently depending on if we are using
                // AND or OR to combine the rules.
                if (CombineRules == BooleanRuleCombination.AND)
                {
                    result = true;
                    foreach (RuleCondition condition in conditions)
                    {
                        Spy.Print(condition);

                        if (condition.Value == false)
                        {
                            result = false;
                            break;
                        }
                    }
                }
                else if (CombineRules == BooleanRuleCombination.OR)
                {
                    result = false;
                    foreach (RuleCondition condition in conditions)
                    {
                        Spy.Print(condition);

                        if (condition.Value == true)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }

            // If this is a TRIGGER, then we save the TRUE result as the trigger value.
            if (RuleType == TradeRuleType.TRIGGER && result == true)
            {
                triggerValue = true;
            }

            return result;
        }

        public void ResetTrigger()
        {
            triggerValue = false;
        }

        void condition_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
        }

        public XmlSaveRule SaveData
        {
            get
            {
                XmlSaveRule save = new XmlSaveRule();
                save.Name = Name;
                save.Active = Active;
                save.RuleCombination = CombineRules;
                save.RuleType = RuleType;

                foreach (RuleCondition condition in conditions)
                {
                    XmlSaveRuleCondition src = condition.SaveData;
                    save.RuleConditions.Add(src);
                }

                return save;
            }
        }


    } // class
} // namespace
