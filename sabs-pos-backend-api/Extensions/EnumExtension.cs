using System;
using System.ComponentModel;
using System.Linq;

namespace sabs_pos_backend_api
{
    public static class EnumExtension
    {
        public static bool ExistedIn<T>(this int value) where T : Enum
        {
            try
            {
                var name = Enum.GetName(typeof(T), value);
                return !string.IsNullOrEmpty(name);
            }
            catch
            {
                return false;
            }
        }

        public static string GetDecription<T>(this T input, params object[] args) where T : Enum
        {
            var description = input.attributeValue<DescriptionAttribute, string>(arg => arg.Description);
            if (string.IsNullOrEmpty(description))
                return description;
            return string.Format(description, args);
        }

        static TValue attributeValue<T, TValue>(this Enum input, Func<T, TValue> expression) where T : Attribute
        {
            var memberInfo = input.GetType().GetMember(input.ToString());

            if (!memberInfo.Any())
                return default;

            var attribute = Attribute.GetCustomAttribute(memberInfo[0], typeof(T));
            if (attribute == null)
                return default;

            return expression((T)attribute);
        }
    }
}
