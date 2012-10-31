using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class DateTimeExtensions
    {
        //public static bool IsToday(this DateTime date, string timeZone)
        //{
        //    return date
        //}

        /// <summary>
        /// Returns a readable string, such as Fri Sept 10
        /// </summary>
        public static string ToFriendlyString(this DateTime date)
        {
            return date.ToString("ddd MMM dd");
        }

        public static DateTime ToLocalTime(this DateTime utcTime, string timezone)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZone);
        }

        public static TimeSpan Elapsed(this DateTime date)
        {
            return DateTime.Now - date;
        }
        
        public static TimeSpan UtcElapsed(this DateTime date)
        {
            return DateTime.UtcNow - date;
        }

        public static string ToElapsedString(this TimeSpan elapsed)
        {
            Func<double, string, string> message = (unit, word) =>
            {
                var wholeUnit = Convert.ToInt32(unit);
                return "{0} {1}{2}".Format1(wholeUnit, word, wholeUnit.Plural());
            };

            if (elapsed.TotalSeconds < 60)
                return message(elapsed.TotalSeconds, "second");
            else if (elapsed.TotalMinutes < 60)
                return message(elapsed.TotalMinutes, "minute");
            else if (elapsed.TotalHours < 24)
                return message(elapsed.TotalHours, "hour");
            else if (elapsed.TotalDays < 30)
                return message(elapsed.TotalDays, "day");
            else if (elapsed.TotalDays < 365)
                return message(elapsed.TotalDays / 30, "month"); // assume 30 days in a month
            else
                return message(elapsed.TotalDays / 365, "year");
        }

        /// <summary>
        /// Returns "s" if the number is not equal to one. 
        /// </summary>
        public static string Plural(this int i)
        {
            return i == 1 ? string.Empty : "s";
        }

        /// <summary>
        /// Returns "s" if the number is not equal to one. 
        /// </summary>
        public static string Plural(this short i)
        {
            return i == 1 ? string.Empty : "s";
        }
    }
}
