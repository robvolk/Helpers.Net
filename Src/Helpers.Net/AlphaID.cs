using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace System
{
    public static class AlphaID
    {
        /// <summary>
        /// Generates a new five-digit lowercase alpha-numeric string
        /// </summary>
        /// <returns></returns>
        public static string AlphaNumericString(this Random rand)
        {
            return rand.AlphaNumericString(5);
        }

        /// <summary>
        /// Generates a upper and lower case alpha-numeric string
        /// </summary>
        /// <returns></returns>
        public static string AlphaNumericString(this Random rand, int size)
        {
            //var sb = new StringBuilder(size);
            //string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            //for (int i = 0; i < size; i++)
            //    sb.Append(chars[(int)(rand.NextDouble() * chars.Length)]);

            //return sb.ToString();
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }

            return result.ToString();
        }


        /// <summary>
        /// Generates a random hexedecimal string
        /// </summary>
        /// <returns></returns>
        public static string HexString(this Random rand, int size)
        {
            var sb = new StringBuilder(size);
            string chars = "abcdef1234567890";
            for (int i = 0; i < size; i++)
                sb.Append(chars[(int)(rand.NextDouble() * chars.Length)]);

            return sb.ToString();
        }
    }
}
