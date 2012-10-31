using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Security.Cryptography;

namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// Formats a string for web display.  Replaces line breaks with <br />'s and replaces URLs with hyperlinks.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HtmlFormat(this string s)
        {
            // replace line breaks with <br /> tags
            s = Regex.Replace(s, @"\n\r?", "<br/>");

            // turn URLs into hyperlinks
            return s.Linkify();
        }

        /// <summary>
        /// Turns URLs into hyperlinks
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Linkify(this string s)
        {
            Regex r = new Regex("(https?://[^ ]+)");
            string html = r.Replace(s, "<a href=\"$1\" target=\"_blank\">$1</a>");
            return html;
            // TODO: strip off any trailing punctuation that isn't supposed to be part of the url
        }

        /// <summary>
        /// Reverse the string
        /// from http://en.wikipedia.org/wiki/Extension_method
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Reverse(this string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new String(chars);
        }

        public static string StripSpaces(this string s)
        {
            if (s.IsNullOrEmptyTrimmed())
                return s;

            return s.Replace(" ", string.Empty);
        }

        /// <summary>
        /// Splists a CSV string into a string array, or an empty array if string is null or empty
        /// </summary>
        public static string[] SplitCsv(this string s)
        {
            if (s.IsNullOrEmptyTrimmed())
                return new string[0];

            return s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Returns an abbreviated number, i.e. 1.5M, 15k, or 500
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToAbbreviatedString(this int number)
        {
            if (number > 1000000)
                return "{0:#,0.0}M".Format(number / 1000000.0);
            else if (number > 1000)
                return "{0}k".Format(number / 1000);
            else
                return number.ToString();
        }

        /// <summary>
        /// Hash an input string and return the hash as a 32 character hexadecimal string  
        /// </summary> 
        public static string MD5HashHex(this string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.  
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.  
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes  
            // and create a string.  
            var sb = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string.  
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }

            return sb.ToString();  // Return the hexadecimal string.  
        }  

        /// <summary>
        /// Create an md5 hash of a string and returns the base-64 encoding of it
        /// </summary>
        static public string MD5HashBase64(this string str)
        {
            // First we need to convert the string into bytes, which
            // means using a text encoder.
            Encoder enc = System.Text.Encoding.Unicode.GetEncoder();

            // Create a buffer large enough to hold the string
            byte[] unicodeText = new byte[str.Length * 2];
            enc.GetBytes(str.ToCharArray(), 0, str.Length, unicodeText, 0, true);

            // Now that we have a byte array we can ask the CSP to hash it
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(unicodeText);

            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// Serializes a string to JSON
        /// </summary>
        public static string ToJson(this object o)
        {
            var jss = new JavaScriptSerializer();
            return jss.Serialize(o);
        }

        /// <summary>
        /// Deserializes a JSON string
        /// </summary>
        public static T FromJson<T>(this string json)
        {
            var jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(json);
        }

        public static string Format(this string format, object arg)
        {
            return string.Format(format, arg);
        }

        public static string Format1(this string format, object arg0, object arg1)
        {
            return string.Format(format, arg0, arg1);
        }

        public static string Format1(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static string FirstWord(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return s.Split(' ')[0];
        }

        /// <summary>
        /// Returns the longest word in a sentence
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string LongestWord(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            var words = s.Split(' ');
            return words
                .OrderByDescending(word=>word.Length)
                .First();
        }

        /// <summary>
        /// Determines whether a string contains a phrase.  Matches only complete word matches of the phrase.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool ContainsPhrase(this string s, string phrase)
        {
            s.EnsureNotNullOrEmptyTrimmed("s");
            phrase.EnsureNotNullOrEmptyTrimmed("phrase");

            // TODO: escape special characters in phrase before applying the regex

            return Regex.IsMatch(s, @"(\W|^)" + phrase + @"(\W|$)", RegexOptions.IgnoreCase);
        }

        public static bool IsNullOrEmptyTrimmed(this string s)
        {
            // TODO: write unit tests
            return (s == null || s.Trim() == string.Empty);
        }

        /// <summary>
        /// Generates a unique URL for an feed group based on its title.
        /// Strips all special characters and replaces whitespace with hyphens.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static string AsUrlFriendly(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            // replace spaces with hyphens
            var url = s.Trim();
            url = Regex.Replace(url, @" - ", "-");
            url = Regex.Replace(url, @"\s+", "-");

            // strip all characters that aren't letters, numbers or hyphens
            url = Regex.Replace(url, @"[^-\w]+", string.Empty);
            return url;
        }

        /// <summary>
        /// Generates a string suitable for use as a HTML DOM ID.
        /// Strips spaces and special characters except for hyphens or underscores.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public static string AsHtmlID(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            // strip all characters that aren't letters, numbers or hyphens
            return Regex.Replace(s, @"[^-_\w]+", string.Empty);
        }

        

        public static string DefaultDelimiter = ", ";


        /// <summary>
        /// Splists a CSV string into a string array, or an empty array if string is null or empty
        /// </summary>
        public static string[] FromDelimitedString(this string s, string delimiter)
        {
            if (s.IsNullOrEmptyTrimmed())
                return new string[0];

            return s.Split( delimiter.ToCharArray() , StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// Convert a sequence of items to a delimited string. By default, ToString() will be called on each item in the sequence to formulate the result. The default delimiter of ', ' will be used
        /// </summary>
        public static string ToDelimitedString<T>(this IEnumerable<T> source)
        {
            return source.ToDelimitedString(x => x.ToString(), DefaultDelimiter);
        }

        /// <summary>
        /// Convert a sequence of items to a delimited string. By default, ToString() will be called on each item in the sequence to formulate the result
        /// </summary>
        /// <param name="delimiter">The delimiter to separate each item with</param>
        public static string ToDelimitedString<T>(this IEnumerable<T> source, string delimiter)
        {
            return source.ToDelimitedString(x => x.ToString(), delimiter);
        }

        /// <summary>
        /// Convert a sequence of items to a delimited string. The default delimiter of ', ' will be used
        /// </summary>
        /// <param name="selector">A lambda expression to select a string property of <typeparamref name="T"/></param>
        public static string ToDelimitedString<T>(this IEnumerable<T> source, Func<T, string> selector)
        {
            return source.ToDelimitedString(selector, DefaultDelimiter);
        }

        /// <summary>
        /// Convert a sequence of items to a delimited string.
        /// </summary>
        /// <param name="selector">A lambda expression to select a string property of <typeparamref name="T"/></param>
        /// <param name="delimiter">The delimiter to separate each item with</param>
        public static string ToDelimitedString<T>(this IEnumerable<T> source, Func<T, string> selector, string delimiter)
        {
            if (source == null || source.Count() == 0)
                return string.Empty;

            if (selector == null)
                throw new ArgumentNullException("selector", "Must provide a valid property selector");

            if (string.IsNullOrEmpty(delimiter))
                delimiter = DefaultDelimiter;

            var sb = new StringBuilder();
            foreach (var item in source.Select(selector))
            {
                sb.Append(item);
                sb.Append(delimiter);
            }
            sb.Remove(sb.Length - delimiter.Length, delimiter.Length);
            return sb.ToString();
        }

        /// <summary>
        /// Parses the domain from a URL string or returns the string if no URL was found
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string AsDomain(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            var match = Regex.Match(url, @"^http[s]?[:/]+[^/]+");
            if (match.Success)
                return match.Captures[0].Value;
            else
                return url;
        }

        /// <summary>
        /// Parses the domain from a URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string AsDomain(this Uri url)
        {
            if (url == null)
                return null;

            return url.ToString().AsDomain();
        }

        /// <summary>
        /// Get file extension from a URL, or an empty string if there is no '.'
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetExtension(this string url)
        {
            if (url.IsNullOrEmptyTrimmed())
                return string.Empty;

            var pos = url.LastIndexOf('.');
            if (pos < 0)
                return string.Empty;

            return url.Substring(pos + 1).Trim();
        }
    }
}
