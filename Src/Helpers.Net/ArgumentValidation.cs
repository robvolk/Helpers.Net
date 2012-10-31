using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Provides extension methods to validate arguments passed into a method
    /// </summary>
    public static class ArgumentValidation
    {
        /// <summary>
        /// Throws an ArgumentNullException if the object is null
        /// </summary>
        /// <param name="o"></param>
        public static void EnsureNotNull(this object o, string argumentName)
        {
            if (o == null)
                throw new ArgumentNullException(argumentName);
        }

        /// <summary>
        /// Throws an ArgumentOutOfRangeException if the integer is not greater than zero
        /// </summary>
        public static void EnsureGreaterThanZero(this double i, string argumentName)
        {
            if (i <= 0)
                throw new ArgumentOutOfRangeException(argumentName, "Argument must be greater than zero");
        }

        /// <summary>
        /// Throws an ArgumentOutOfRangeException if the integer is not greater than zero
        /// </summary>
        public static void EnsureGreaterThanZero(this int i, string argumentName)
        {
            if (i <= 0)
                throw new ArgumentOutOfRangeException(argumentName, "Argument must be greater than zero");
        }

        /// <summary>
        /// Throws an ArgumentOutOfRangeException if the string is null or empty.  Trims the string for whitespace.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="argumentName"></param>
        public static void EnsureNotNullOrEmptyTrimmed(this string s, string argumentName)
        {
            if (s.IsNullOrEmptyTrimmed())
                throw new ArgumentOutOfRangeException(argumentName, "String must contain a value other than whitespace");
        }
    }
}
