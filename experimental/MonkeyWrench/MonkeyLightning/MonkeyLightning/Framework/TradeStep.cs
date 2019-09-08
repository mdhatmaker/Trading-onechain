using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonkeyLightning.Framework
{
    public class TradeStep
    {
        public event EventHandler ValueChanged;
        public event EventHandler StateChanged;

        public TradeStepState State
        {
            get { return state; }
            set { state = value; if (StateChanged != null) StateChanged(this, EventArgs.Empty); }
        }

        public BooleanRuleCombination CombineRules { get; set; }

        public List<TradeRule> Rules { get { return rules; } }

        private TradeStepState state;
        private TradeStepType tradeStepType;
        private List<TradeRule> rules;

        public TradeStep(TradeStepType tradeStepType, BooleanRuleCombination combine = BooleanRuleCombination.OR)
        {
            this.tradeStepType = tradeStepType;
            this.CombineRules = combine;

            this.State = TradeStepState.INACTIVE;

            rules = new List<TradeRule>();
        }

        public void AddRule(TradeRule rule)
        {
            rules.Add(rule);
            rule.ValueChanged += rule_ValueChanged;
        }

        public void AddRules(List<TradeRule> rules)
        {
            foreach (TradeRule rule in rules)
            {
                AddRule(rule);
            }
        }

        public bool Evaluate()
        {
            bool result = false;

            // As an aside, an AND collection of TradeRules will result in
            // a TRUE value if no rules exist while an OR collection of
            // TradeRules will result in a FALSE value if no rules exist.
            // This is correct since a PRECONDITIONS step with no rules
            // should be considered TRUE and a STOP step, for instance, with no
            // rules should be considered FALSE.

            // Evaluate differently depending on if we are using
            // AND or OR to combine the rules.
            if (CombineRules == BooleanRuleCombination.AND)
            {
                result = true;
                foreach (TradeRule rule in rules)
                {
                    if (rule.Evaluate() == false)
                    {
                        result = false;
                        break;
                    }
                }
            }
            else if (CombineRules == BooleanRuleCombination.OR)
            {
                result = false;
                foreach (TradeRule rule in rules)
                {
                    if (rule.Evaluate() == true)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        void rule_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null) ValueChanged(this, EventArgs.Empty);
        }


    } // class
} // namespace
