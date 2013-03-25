using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFQMWeb.Common.Util
{
    public class Utils
    {
        public static bool IsNull(object input)
        {
            if ((input == null) || (input.GetType() == typeof(DBNull)))
                return true;
            else
                return false;
        }

        public static string NullToEmpty(object input)
        {
            if (IsNull(input))
                return string.Empty;
            else
                return input.ToString();
        }

        public static int? DBInt(object input)
        {
            if ((input == null) || (input.GetType() == typeof(DBNull)))
                return null;
            else
                return (int)input;
        }

        public static string ToJavascriptBool(bool input)
        {
            if (input) return "true";
            else return "false";
        }
    }
}