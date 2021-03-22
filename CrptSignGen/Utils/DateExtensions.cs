using System;

namespace CRPT.SharedLib.Extensions
{
    public static class DateExtensions
    {
        public static DateTime FromUnixTimestamp(this long timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            return origin.AddSeconds(timestamp);
        }
        public static long ToUnixTimestamp(this DateTime timestamp)
        {
            return ((DateTimeOffset)timestamp).ToUnixTimeSeconds();
        }
    }
}
