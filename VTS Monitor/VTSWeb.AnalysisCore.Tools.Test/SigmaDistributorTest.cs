using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VTSWeb.AnalysisCore.Tools.Statistics;

namespace VTSWeb.AnalysisCore.Tools.Test
{
    [TestClass]
    public class SigmaDistributorTest
    {
        [TestMethod]
        public void TestDistribution()
        {
            IList<double> values = new List<double>()
                {3.2, 4.1, 5.3, 5.1, 3.825, 4.8, 2.7, 3.1, 2.3};
            double m = values.Average();
            Assert.IsTrue(m > 3.824);
            Assert.IsTrue(m<3.825);
            double sigma = Sigma.Get(values); // approx 1.02
            Assert.IsTrue(sigma>1.01);
            Assert.IsTrue(sigma<1.02);
            SigmaDistributor distributor = 
                new SigmaDistributor(values, (float)0.5);
            IDictionary<string, long> distribution = distributor.Distribute();
            Assert.IsTrue(distribution != null);
            Assert.IsTrue(distribution.Count == 6);
            Assert.IsTrue(distribution["-1,5σ"] == 2);
            Assert.IsTrue(distribution["-1σ"] == 2);
            Assert.IsTrue(distribution["-0,5σ"] == 0);
            Assert.IsTrue(distribution["+0,5σ"] == 2);
            Assert.IsTrue(distribution["+1σ"] == 1);
            Assert.IsTrue(distribution["+1,5σ"] == 2);
        }
    }
}
