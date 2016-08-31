using System;
using System.Security.Cryptography;

namespace CapitalCoffee.Data.Access
{
    public static class SaltGenerator
    {
        private static RNGCryptoServiceProvider m_cryptoServiceProvider = null;
        private const int SALT_SIZE = 32;

        static SaltGenerator()
        {
            m_cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public static string GetSaltString()
        {
            byte[] saltBytes = new byte[SALT_SIZE];

            m_cryptoServiceProvider.GetNonZeroBytes(saltBytes);

            string saltString = BitConverter.ToString(saltBytes);
            saltString = saltString.Replace("-", "");
            return saltString;
        }
    }
}
