using System;
using System.Collections.Generic;

namespace sabs_pos_backend_api
{
    public static class DictionaryExtension
    {
        public static T GetValue<T>(this Dictionary<string, object> sources, string name)
        {
            try
            {
                var value = sources[name];
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch { return default; }
        }

        public static T GetValue<T>(this Dictionary<string, T> sources, string name)
        {
            try
            {
                return sources[name];
            }
            catch { return default; }
        }

        public static void SetValue(this Dictionary<string, object> sources, string name, object value)
        {
            if (sources.ContainsKey(name))
                sources[name] = value;
        }

        public static void SetValue<T>(this Dictionary<string, T> sources, string name, T value)
        {
            if (sources.ContainsKey(name))
                sources[name] = value;
        }
    }
}
