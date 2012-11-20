using System;
using System.Security.Cryptography;

namespace BonTemps
{
    class MD5Encryption
    {
        /// <summary>
        /// Converting a string to a hashed Byte[].
        /// </summary>
        /// <param name="value">Enter a string that you want to be hashed.</param>
        /// <returns></returns>
        public static byte[] CreateMD5Hash(string value)
        {
            byte[] buffer = new byte[100];
            int buffersize = 0;
            byte[] summary;

            foreach (char c in value)
            {
                int i = c.GetHashCode();
                buffer[buffersize] = (byte)i;
                buffersize++;
            }
            summary = new byte[buffersize];
            for (int i = 0; i < buffersize; i++)
            {
                summary[i] = buffer[i];
            }
            byte[] result = MD5.Create().ComputeHash(summary);

            return result;
        }
        public static string MD5HashToString(byte[] value)
        {
            string stringResult = String.Empty;

            foreach (byte b in value)
            {
                stringResult += b.ToString();
            }

            return stringResult;
        }
    }
}
