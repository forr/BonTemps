using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public static Byte[] CreateMD5Hash(string value)
        {
            Byte[] buffer = new Byte[100];
            int buffersize = 0;
            Byte[] summary;

            foreach (char c in value)
            {
                int i = c.GetHashCode();
                buffer[buffersize] = (Byte)i;
                buffersize++;
            }
            summary = new Byte[buffersize];
            for (int i = 0; i < buffersize; i++)
            {
                summary[i] = buffer[i];
            }
            Byte[] result = MD5.Create().ComputeHash(summary);

            return result;
        }

        public static string MD5HashToString(Byte[] value)
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
