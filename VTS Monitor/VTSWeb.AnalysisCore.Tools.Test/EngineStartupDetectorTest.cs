using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VTSWeb.AnalysisCore.Tools.Test
{
    [TestClass]
    public class EngineStartupDetectorTest
    {
        [TestMethod]
        public void TestEngineStartupDetection()
        {
            //taken from my c4 real data
            IList<double> initialLine = new List<double>() { 736, 736, 736, 736, 736, 736, 736, 736, 736, 736, 736, 736, 736, 736, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1280, 832, 768, 672, 672, 768, 736, 736, 736, 1536, 960, 832, 768, 736, 864, 1280, 1376, 1408, 896, 800, 736, 736, 736, 1952, 2208, 2240, 2464, 2656, 2688, 2720, 2848, 2912, 3008, 3104, 3136, 3136, 2944, 2784, 2656, 2208, 1856, 1728, 736, 0, 0, 0, 0, 0, 640, 864, 0, 0, 0, 0, 256, 1024, 800, 640, 768, 736, 1152, 1184, 1088, 896, 800, 1536, 1920, 1504, 896, 768, 736, 736, 736, 736, 736, 736, 736 };
            EngineStartupDetector detector = new EngineStartupDetector(initialLine);
            Assert.IsTrue(detector.EngineStartupDetected());

            IList<int> startupValues = detector.GetEngineStartupPointIndexes();
            Assert.IsTrue(startupValues.Count == 3);
            Assert.IsTrue(startupValues.Contains(22));
            Assert.IsTrue(startupValues.Contains(70));
            Assert.IsTrue(startupValues.Contains(76));
        }
    }
}
