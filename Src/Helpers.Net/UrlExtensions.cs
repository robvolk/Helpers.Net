using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Helpers
{
    public static class UrlExtensions
    {
        /// <summary>
        /// Strips the querystring from a url and returns the result as a string
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string StripQueryString(this Uri uri)
        {
            return Regex.Replace(uri.ToString(), @"\?.*$", string.Empty);
        }
    }
}
