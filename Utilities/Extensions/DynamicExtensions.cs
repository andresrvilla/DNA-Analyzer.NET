using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Extensions
{
    public class DynamicExtensions
    {
        public static bool HasProperty(dynamic obj, string name)
        {
            if (obj == null)
            {
                return false;
            }

            Type objType = obj.GetType();

            if (objType == typeof(ExpandoObject))
            {
                return ((IDictionary<string, object>)obj).ContainsKey(name);
            }

            return objType.GetProperty(name) != null;
        }

        public static int AsInt(dynamic obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch
            {
                return 0;
            }
        }
    }
}
