using System.Security.Cryptography;
using System.Text;

namespace VTS.Shared
{
    public class Sha256Hash
    {
        public static string Calculate(string input)
        {
            SHA256 hasher = new SHA256Managed();
            byte[] inputBytes = UTF8Encoding.UTF8.GetBytes(input);
            byte[] hash = hasher.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
