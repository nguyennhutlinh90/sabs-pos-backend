using System;
using System.Text;
using System.Text.RegularExpressions;

namespace sabs_pos_backend_api
{
    public static class StringExtension
    {
        public static bool IsEmail(this string value)
        {
            var pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            return Regex.IsMatch(value, pattern);
        }

        public static bool IsNull(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static T ConvertTo<T>(this string value, T defaultValue = default(T))
        {
            try
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        public static byte[] ToBytes(this string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }
    }
}
