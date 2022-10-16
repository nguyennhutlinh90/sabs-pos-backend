using System;

namespace sabs_pos_backend_api
{
    public static class DateTimeExtension
    {
        public static string Format(this DateTime date, string format = SystemFormat.DATE_TIME)
        {
            return date.ToString(format);
        }

        public static string Format(this DateTime? date, string format = SystemFormat.DATE_TIME)
        {
            return date.HasValue ? date.Value.ToString(format) : string.Empty;
        }
    }
}
