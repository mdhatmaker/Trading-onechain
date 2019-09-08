using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZAPI.Toolbox.DataStructures
{
    public static class Parse
    {
        public static TEnum ParseEnum<TEnum>(string stringToParse)
            where TEnum : struct
        {
            TEnum enumValue;
            bool parseOk = Enum.TryParse<TEnum>(stringToParse, true, out enumValue);

            return enumValue;
        }

        public static T To<T>(this IConvertible obj)
        {
            Type t = typeof(T);
            Type u = Nullable.GetUnderlyingType(t);

            if (u != null)
            {
                if (obj == null)
                    return default(T);

                return (T)Convert.ChangeType(obj, u);
            }
            else
            {
                return (T)Convert.ChangeType(obj, t);
            }
        }

    } // class
} // namespace
