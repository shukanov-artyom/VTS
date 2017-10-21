using System;
using System.IO;
using System.Text;

namespace Tester.DataImport
{
    public class ExportDataEncryptor
    {
        private Stream source;
        private string pwd = "1234567812345678";

        public ExportDataEncryptor(Stream source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            this.source = source;
        }

        public void Encrypt(Stream output)
        {
            /*StreamReader rdr = new StreamReader(source);
            string s = rdr.ReadToEnd();
            SimpleAES aes = new SimpleAES();
            byte[] res = aes.Encrypt(s);
            for (int i = 0; i < res.Length; i++)
            {
                output.WriteByte(res[i]);
            }*/
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
