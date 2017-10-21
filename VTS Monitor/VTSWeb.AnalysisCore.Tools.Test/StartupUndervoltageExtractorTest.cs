using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VTSWeb.AnalysisCore.Tools.StartupUndervoltage;

namespace VTSWeb.AnalysisCore.Tools.Test
{
    [TestClass]
    public class StartupUndervoltageExtractorTest
    {
        [TestMethod]
        public void TestUndervoltageExtraction()
        {
            IList<double> voltages = new List<double>() { 14.1, 14.1, 14.1, 14.1, 14.1, 14.1, 14.1, 14.1, 14.1, 14.1, 14.1, 14.1, 14.1, 14.1, 13.6, 13.6, 13.5, 13.4, 12.5, 12.5, 12.4, 12.4, 10.3, 12.2, 12.3, 12.8, 13.5, 13.9, 14.1, 14.0, 14.1, 14.1, 14.1, 14.1, 14.1, 14.1, 14.1, 14.1, 14.1, 14.1, 14.2, 14.1, 14.2, 14.1, 14.1, 14.1, 14.2, 14.2, 14.2, 14.2, 14.2, 14.2, 14.2, 14.2, 14.1, 14.1, 14.2, 14.1, 14.2, 14.2, 14.1, 14.2, 14.1, 14.2, 14.1, 14.1, 12.6, 12.6, 12.6, 12.6, 12.6, 13.9, 14.1, 12.6, 12.6, 12.6, 12.5, 11.4, 12.3, 12.6, 13.2, 14.1, 14.0, 14.1, 14.2, 14.2, 14.2, 14.2, 14.2, 14.2, 14.2, 14.2, 14.2, 14.1, 14.2, 14.1, 14.1, 14.1, 14.1, 14.1 };
            int startIndex = 22; // has 22, 70 and 76
            StartupUndervoltageExtractor extractor = new StartupUndervoltageExtractor(startIndex, voltages);
            Assert.IsTrue(extractor.Extract() == 10.3);
        }
    }
}
