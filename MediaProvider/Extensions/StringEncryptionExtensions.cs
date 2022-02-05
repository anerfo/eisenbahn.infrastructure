using System;
using System.Security.Cryptography;
using System.Text;

namespace MediaProvider.Extensions
{
    public static class StringEncryptionExtensions
    {
        private static readonly byte[] entropy = Encoding.Unicode.GetBytes("N#v^aeu9sDn7-8&0P/zKY-.pFG0DGThMpNe4R%");

        public static string EncryptString(this string input)
        {
            if (input == null)
            {
                return null;
            }

            byte[] encryptedData = ProtectedData.Protect(Encoding.Unicode.GetBytes(input), entropy, DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(encryptedData);
        }

        public static string DecryptString(this string encryptedData)
        {
            if (encryptedData == null)
            {
                return null;
            }

            try
            {
                byte[] decryptedData = ProtectedData.Unprotect(Convert.FromBase64String(encryptedData), entropy, DataProtectionScope.CurrentUser);

                return Encoding.Unicode.GetString(decryptedData);
            }
            catch
            {
                return null;
            }
        }
    }
}
