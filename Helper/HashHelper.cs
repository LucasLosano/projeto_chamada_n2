using System;
using System.Text;
using System.Security.Cryptography;

namespace Volare.Helper
{
    public static class HashHelper
    {
        public static string HashedString(string text)
        {
            using (var sha = new SHA256Managed())
            {
                byte[] textBytes = Encoding.UTF8.GetBytes(text);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                return hash;
            }
        }
    }
}