using System;
using System.IO;
using System.Security.Cryptography;

namespace System.Security
{

    /// <summary>
    /// String extension methods for Encrypting/Decrypting string values
    /// This class is coded to be very straight forward and easy to read and not oo, so you will
    /// see duplication of code
    /// </summary>
    public static class StringExtensionMethods
    {

        /// <summary>
        /// Encrypts the specified string value using RSA
        /// NOTE: For users that that just want to run the code and not worry about generating a RSA private/public key
        /// use this overload and it's corresponding decrypt method. RSA will handle generating the keys for you all you
        /// need to do is pass in any string value you like for the key name.
        /// </summary>
        /// <param name="encryptValue"><see cref="System.String"/> value to encrypt</param>
        /// <param name="publicKey"><see cref="System.String"/> registry key name that contains the public key</param>
        /// <returns></returns>
        public static string Encrypt(this string encryptValue, string publicKey)
        {
            // This is the variable that will be returned to the user
            string encryptedValue = string.Empty;

            // Make sure user supplied a value for the registry key
            if (string.IsNullOrEmpty(publicKey))
                throw new ArgumentNullException("You must provide the name of the registry key for the public key");

            // Create the CspParameters object which is used to create the RSA provider
            // without it generating a new private/public key.
            // Parameter value of 1 indicates RSA provider type - 13 would indicate DSA provider
            CspParameters csp = new CspParameters(1);

            // Registry key name containing the RSA private/public key
            csp.KeyContainerName = publicKey;

            // Supply the provider name
            csp.ProviderName = "Microsoft Strong Cryptographic Provider";

            //Create new RSA object passing our key info
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);

            // Before encrypting the value we must convert it over to byte array
            byte[] bytesToEncrypt = System.Text.Encoding.UTF8.GetBytes(encryptValue);

            // Encrypt our byte array. The false parameter has to do with 
            // padding (not to clear on this point but you can look it up and decide which is better for your use)
            byte[] bytesEncrypted = rsa.Encrypt(bytesToEncrypt, false);

            // Extract our encrypted byte array into a string value to return to our user
            encryptedValue = Convert.ToBase64String(bytesEncrypted);

            return encryptedValue;

        }

        /// <summary>
        /// Encrypts the specified string value using RSA encryption
        /// </summary>
        /// <param name="encryptValue"><see cref="System.String"/> value to encrypt</param>
        /// <param name="publicKeyPath"><see cref="System.String"/> path to XML file that contains the public key</param>
        /// <returns></returns>
        public static string EncryptStringUsingXMLFile(this string encryptValue, string publicKeyPath)
        {

            // This is the variable that will be returned to the user
            string encryptedValue = string.Empty;

            // Variable to hold contents of public key xml
            string pubKey;

            // Make sure user supplied a value for the path to a xml file that contains the public key
            if (string.IsNullOrEmpty(publicKeyPath))
                throw new ArgumentNullException("You must provide the path to a xml file for the public key");

            // Read public key from xml file
            using (StreamReader reader = new StreamReader(publicKeyPath))
            {
                pubKey = reader.ReadToEnd();
            }

            // Create the CspParameters object which is used to create the RSA provider
            // without it generating a new private/public key.
            // Parameter value of 1 indicates RSA provider type - 13 would indicate DSA provider
            CspParameters csp = new CspParameters(1);

            //Create new RSA object passing our key info
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);

            // Load our public key data
            rsa.FromXmlString(pubKey);

            // Before encrypting the value we must convert it over to byte array
            byte[] bytesToEncrypt = System.Text.Encoding.UTF8.GetBytes(encryptValue);

            // Encrypt our byte array. The false parameter has to do with 
            // padding (not to clear on this point but you can look it up and decide which is better for your use)
            byte[] bytesEncrypted = rsa.Encrypt(bytesToEncrypt, false);

            // Extract our encrypted byte array into a string value to return to our user
            encryptedValue = Convert.ToBase64String(bytesEncrypted);

            return encryptedValue;

        }

        /// <summary>
        /// Decrypts the passed in string value using RSA
        /// </summary>
        /// <param name="decryptValue"><see cref="System.String"/> value to decrypt</param>
        /// <param name="publicKey"><see cref="System.String"/> registry key name that contains the public key</param>
        /// <returns></returns>
        public static string DecryptStringUsingRegistryKey(this string decryptValue, string privateKey)
        {
            // This is the variable that will be returned to the user
            string decryptedValue = string.Empty;

            // Make sure user supplied a value for the registry key
            if (string.IsNullOrEmpty(privateKey))
                throw new ArgumentNullException("You must provide the name of the registry key for the public key");

            // Create the CspParameters object which is used to create the RSA provider
            // without it generating a new private/public key.
            // Parameter value of 1 indicates RSA provider type - 13 would indicate DSA provider
            CspParameters csp = new CspParameters(1);

            // Registry key name containing the RSA private/public key
            csp.KeyContainerName = privateKey;

            // Supply the provider name
            csp.ProviderName = "Microsoft Strong Cryptographic Provider";

            //Create new RSA object passing our key info
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);

            // Before decryption we must convert this ugly string into a byte array
            byte[] valueToDecrypt = Convert.FromBase64String(decryptValue);

            // Decrypt the passed in string value - Again the false value has to do with padding
            byte[] plainTextValue = rsa.Decrypt(valueToDecrypt, false);

            // Extract our decrypted byte array into a string value to return to our user
            decryptedValue = System.Text.Encoding.UTF8.GetString(plainTextValue);

            return decryptedValue;
        }

        /// <summary>
        /// Decrypts the passed in string value using RSA
        /// </summary>
        /// <param name="decryptValue"><see cref="System.String"/> value to decrypt</param>
        /// <param name="publicKeyPath"><see cref="System.String"/> path to XML file that contains the public key</param>
        /// <returns></returns>
        public static string DecryptStringUsingXMLFile(this string decryptValue, string privateKeyPath)
        {
            // This is the variable that will be returned to the user
            string decryptedValue = string.Empty;

            // Variable to hold contents of private key xml
            string privateKey;

            // Make sure user supplied a value for the registry key
            if (string.IsNullOrEmpty(privateKeyPath))
                throw new ArgumentNullException("You must provide the name of the registry key for the public key");

            // Read public key from xml file
            using (StreamReader reader = new StreamReader(privateKeyPath))
            {
                privateKey = reader.ReadToEnd();
            }

            // Create the CspParameters object which is used to create the RSA provider
            // without it generating a new private/public key.
            // Parameter value of 1 indicates RSA provider type - 13 would indicate DSA provider
            CspParameters csp = new CspParameters(1);

            // Supply the provider name
            csp.ProviderName = "Microsoft Strong Cryptographic Provider";

            //Create new RSA object passing our key info
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);
            rsa.FromXmlString(privateKey);

            // Before decryption we must convert this ugly string into a byte array
            byte[] valueToDecrypt = Convert.FromBase64String(decryptValue);

            // Decrypt the passed in string value - Again the false value has to do with padding
            byte[] plainTextValue = rsa.Decrypt(valueToDecrypt, false);

            // Extract our decrypted byte array into a string value to return to our user
            decryptedValue = System.Text.Encoding.UTF8.GetString(plainTextValue);

            return decryptedValue;
        }

    }
}
