using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public static class TryConvert
    {
        /// <summary>
        /// Attempts to convert a date.  Strips the timezone at the end since it will cause the conversion to fail.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(string s)
        {
            var d = ToDateTimeNullable(s);
            if (d != null)
                return (DateTime)d;
            else
                return DateTime.MinValue;
        }

        public static DateTime? ToDateTimeNullable(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            // strip the timezone
            // TODO: don't strip the timezone, fix it!
            s = Regex.Replace(s, @"\s[A-Z]+$", string.Empty, RegexOptions.IgnoreCase);

            DateTime date;
            if (DateTime.TryParse(s, out date))
                return date;
            else
                return null;
        }
    }
}
