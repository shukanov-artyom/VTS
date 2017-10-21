using System;
using System.IO;
using Common.Transport.Encryption;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Analyser.Connector.Export.Test.Encryption
{
    [TestClass]
    public class ExportDataEncryptorTest
    {
        [TestMethod]
        public void TestEncryptionDecryption()
        {
            FileStream inFile = new FileStream("Encryption/TextFile.txt",
            FileMode.Open);
            ExportDataEncryptor encryptor = new ExportDataEncryptor(inFile);

            MemoryStream encrypted = new MemoryStream();
            encryptor.Encrypt(encrypted);

            encrypted.Flush();
            encrypted.Position = 0;

            BinaryReader rdrEncr = new BinaryReader(encrypted);
            byte[] encrBytes = new byte[encrypted.Length];

            for (int i=0; i<encrypted.Length; i++)
            {
                encrBytes[i] = rdrEncr.ReadByte();
            }
            //Assert.IsFalse( == "secret service");

            encrypted.Position = 0;

            ExportDataEncryptor decryptor = new ExportDataEncryptor(encrypted);
            MemoryStream decrypted = new MemoryStream();
            decryptor.Decrypt(decrypted);

            decrypted.Position = 0;

            StreamReader rdrDecr = new StreamReader(decrypted);
            string decrStr = rdrDecr.ReadToEnd();
            Assert.IsTrue(decrStr.Contains("<tag1>vaue1</tag1>"));
        }
    }
}
