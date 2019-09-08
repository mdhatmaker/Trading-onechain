using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EZAPI.Toolbox;
using EZAPI.Toolbox.Debug;
using EZAPI.Toolbox.Serialization;
using MonkeyLightning.DataProvider;
using MonkeyLightning.Framework.Comparison;
using MonkeyLightning.Framework.Xml;

namespace MonkeyLightning.Framework.IO
{
    public static class MonkeyIO
    {
        static string appPath;
        static string rulePath;

        static MonkeyIO()
        {
            appPath = Application.StartupPath;
            rulePath = appPath + @"\rules\";
            DirectoryInfo df = new DirectoryInfo(rulePath);

            // Create the directory if it doesn't exist.
            if (!df.Exists)
            {
                DirectoryInfo di = Directory.CreateDirectory(rulePath);
            }

        }

        public static void SaveCondition(RuleCondition condition)
        {
            throw new NotImplementedException();
            //Objects.SerializeObject<RuleCondition>(condition, rulePath + "testcondition.mrl");
        }

        public static bool RenameRule(string originalName, string updatedName)
        {
            bool result = true;

            string filename1 = rulePath + originalName + ".mrl";
            string filename2 = rulePath + updatedName + ".mrl";

            try
            {
                File.Move(filename1, filename2);
            }
            catch (Exception ex)
            {
                result = false;
                ExceptionHandler.TraceException(ex);
            }

            return result;
        }

        public static void SaveRule(TradeRule rule)
        {
            string filename = rulePath + rule.Name + ".mrl";
                        
            EZAPI.Toolbox.Serialization.Xml.Serialize(rule.SaveData, typeof(XmlSaveRule), filename);
            //Objects.SerializeObject<TradeRule>(rule, filename);
        }

        public static void SaveRules(List<TradeRule> rules)
        {
            foreach (TradeRule rule in rules)
            {
                SaveRule(rule);
            }
        }

        public static TradeRule LoadRule(string ruleFileName)
        {
            TradeRule rule = null;

            try
            {
                XmlSaveRule xsr = (XmlSaveRule)EZAPI.Toolbox.Serialization.Xml.Deserialize(typeof(XmlSaveRule), ruleFileName);

                rule = new TradeRule(xsr.Name, xsr.RuleCombination, xsr.RuleType);
                rule.Active = xsr.Active;

                foreach (var xsrc in xsr.RuleConditions)
                {   
                    IDataProvider dataProvider1 = MonkeyFactory.CreateDataProviderInstance(xsrc.Value1.Name);
                    IDataProvider dataProvider2 = MonkeyFactory.CreateDataProviderInstance(xsrc.Value2.Name);
                    dataProvider1.SaveData = xsrc.Value1;
                    dataProvider2.SaveData = xsrc.Value2;

                    RuleValue value1 = new RuleValue(dataProvider1);
                    RuleValue value2 = new RuleValue(dataProvider2);
                    IRuleComparison compare = MonkeyFactory.CreateRuleComparisonInstance(xsrc.Compare.Name);
                    RuleCondition condition = new RuleCondition(value1, compare, value2);

                    rule.AddRuleCondition(condition);
                }
            }
            catch (Exception ex)
            {
                // If there is a problem loading the rule, then we return null (not some partial rule).
                rule = null;
                ExceptionHandler.TraceException(ex);
            }

            return rule;
        }

        public static List<TradeRule> LoadAllRules()
        {
            List<TradeRule> result = new List<TradeRule>();

            string[] filePaths = Directory.GetFiles(rulePath);
            foreach (string pathname in filePaths)
            {
                TradeRule rule = LoadRule(pathname);
                if (rule != null) result.Add(rule);
            }

            return result;
        }


    } // class
} // namespace
