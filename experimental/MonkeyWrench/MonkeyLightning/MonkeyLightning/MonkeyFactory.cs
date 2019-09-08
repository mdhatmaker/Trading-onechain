using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MonkeyLightning.DataProvider;
using MonkeyLightning.Framework.Comparison;

namespace MonkeyLightning
{
    public static class MonkeyFactory
    {
        static Dictionary<string, Type> dataProviderTypes;
        static Dictionary<string, Type> ruleComparisonTypes;

        static MonkeyFactory()
        {
            dataProviderTypes = new Dictionary<string, Type>();
            ruleComparisonTypes = new Dictionary<string, Type>();

            // Find the Assembly location for the IDataProvider classes.
            string location = Assembly.GetExecutingAssembly().Location;
            string projectFolder = @"MonkeyLightning\";
            int pathIndex = location.IndexOf(projectFolder);
#if DEBUG
            string assemblyPath = location.Substring(0, pathIndex + projectFolder.Length) + @"DataProvider.Providers.Basic\bin\Debug\DataProvider.Providers.Basic.dll";
#else
            string assemblyPath = location.Substring(0, pathIndex + projectFolder.Length) + @"DataProvider.Providers.Basic\bin\Release\DataProvider.Providers.Basic.dll";
#endif
            var dpAssembly = Assembly.LoadFrom(assemblyPath);

            // Find all types that implement IDataProvider.
            var dpTypes = from t in dpAssembly.GetTypes()
                          where t.GetInterfaces().Contains(typeof(IDataProvider))
                              && t.GetConstructor(Type.EmptyTypes) != null
                          select t;

            // Store the DataProvider names AND types.
            foreach (Type type in dpTypes)
            {
                object dp = Activator.CreateInstance(type);
                var dataProviderName = (string)type.GetProperty("Name").GetValue(dp, null);
                dataProviderTypes[dataProviderName] = type;
            }

            // Find all types that implement IRuleComparison.
            var rcTypes = from t in Assembly.GetExecutingAssembly().GetTypes()
                                      where t.GetInterfaces().Contains(typeof(IRuleComparison))
                                          && t.GetConstructor(Type.EmptyTypes) != null
                                      select t;

            // Store the RuleComparison names AND types.
            foreach (Type type in rcTypes)
            {
                object rc = Activator.CreateInstance(type);
                var ruleComparisonName = (string)type.GetProperty("Name").GetValue(rc, null);
                ruleComparisonTypes[ruleComparisonName] = type;
            }
        }

        public static IDataProvider CreateDataProviderInstance(string dpName)
        {
            return Activator.CreateInstance(dataProviderTypes[dpName]) as IDataProvider;
        }

        public static IRuleComparison CreateRuleComparisonInstance(string rcName)
        {
            return Activator.CreateInstance(ruleComparisonTypes[rcName]) as IRuleComparison;
        }

    } // class
} // namespace
