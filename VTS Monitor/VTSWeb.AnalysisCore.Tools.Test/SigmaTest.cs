using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VTSWeb.AnalysisCore.Tools.Statistics;

namespace VTSWeb.AnalysisCore.Tools.Test
{
    [TestClass]
    public class SigmaTest
    {
        [TestMethod]
        public void TestSigmaGeneration()
        {
            IList<double> data = new List<double>()
            { 3.2, 4.1, 5.3, 5.1, 3.825, 4.8, 2.7, 3.1, 2.3 };
            double sigma = Sigma.Get(data);
            Assert.IsTrue(sigma > 1.01);
            Assert.IsTrue(sigma < 1.02);
        }
    }
}
