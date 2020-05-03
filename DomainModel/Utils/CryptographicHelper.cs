using System;
using System.Security.Cryptography;
using System.Text;

namespace DomainModel.Utils
{
    internal sealed class CryptographicHelper
    {
        internal static string HashValues(string s)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(s));

                StringBuilder stringBuilder = new StringBuilder();
                for(int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2")); // "x2" means hexadecimal, i.e. number 13 becomes 0D
                }

                return stringBuilder.ToString();
            }
        }
    }
}
