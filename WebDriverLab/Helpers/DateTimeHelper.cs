using System;

namespace WebDriverLab.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime TruncateSecond(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, date.Millisecond, date.Kind);
        }

        public static DateTime TruncateMillisecond(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind);
        }
    }
}
