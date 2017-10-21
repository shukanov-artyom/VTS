using System;
using System.IO;
using System.Text;
using Agent.Common.Data;

namespace Common.Transport.Encryption
{
    internal class ExportDataEncryptor
    {
        private readonly Stream source;
        private readonly string pwd;

        public ExportDataEncryptor(Stream source)
        {
            this.source = source;
            // 1234567812345678
            pwd = Cipher.Decrypt("2vaxrtk3lb0Pl43E69mEF3k1CFMt0YiG3od90xEJ75M=", "System");
        }

        public void Encrypt(Stream output)
        {
            byte[] bytes = new byte[source.Length];
            source.Read(bytes, 0, (int)source.Length);
            string s = Convert.ToBase64String(bytes);
            string ciphered = Cipher2.Encrypt(s, pwd);
            byte[] cipheredBytes = Encoding.UTF8.GetBytes(ciphered);
            for (int i = 0; i < cipheredBytes.Length; i++)
            {
                output.WriteByte(cipheredBytes[i]);
            }
        }

        public void Decrypt(Stream output)
        {
            byte[] buffer = new byte[source.Length];
            source.Read(buffer, 0, (int)source.Length);
            string ciphered = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            string base64Encoded = Cipher2.Decrypt(ciphered, pwd);
            byte[] bytes = Convert.FromBase64String(base64Encoded);
            for (int i = 0; i < bytes.Length; i++)
            {
                output.WriteByte(bytes[i]);
            }
        }
    }
}
