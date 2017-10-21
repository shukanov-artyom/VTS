using System;
using System.IO;
using System.Xml.Linq;
using Common.Transport.Compression;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Analyser.Connector.Export.Test.Compression
{
    [TestClass]
    public class ExportDataCompressorTest
    {
        [TestMethod]
        public void XmlCompressionDecompressionTest()
        {
            FileStream stream = new FileStream("./compression/XmlFile.xml", FileMode.Open);
            ExportDataCompressor compressor = new ExportDataCompressor(stream);
            MemoryStream mems = new MemoryStream();
            
            compressor.Compress(mems);
            mems.Position = 0;

            try
            {
                XDocument doc = XDocument.Load(mems);
                doc.ToString();
                Assert.IsFalse(true);
            }
            catch(Exception)
            {
                mems.Position = 0;
            }
            MemoryStream mems2 = new MemoryStream();
            mems.CopyTo(mems2);
            mems2.Flush();
            mems2.Position = 0;
            ExportDataCompressor compressor2 = new ExportDataCompressor(mems2);
            MemoryStream decompressed = new MemoryStream();
            compressor2.Decompress(decompressed);
            decompressed.Position = 0;
            XDocument doc2 = XDocument.Load(decompressed);
            XElement root = doc2.Root;
            Assert.IsTrue(root.HasElements);
            XElement xtag1 = root.Element("XmlTag1");
            Assert.IsTrue(xtag1.Value == "XML Value 1");
        }
    }
}
