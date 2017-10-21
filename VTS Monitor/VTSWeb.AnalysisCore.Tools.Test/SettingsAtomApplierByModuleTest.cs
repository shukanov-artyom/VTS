using Microsoft.VisualStudio.TestTools.UnitTesting;
using VTSWeb.AnalysisCore.Models.Settings;

namespace VTSWeb.AnalysisCore.Tools.Test
{
    [TestClass]
    public class SettingsAtomApplierByModuleTest
    {
        [TestMethod]
        public void Test()
        {
            SettingsAtom atom = new SettingsAtom();
            atom.MinOptimal = 0.5;
            atom.MaxOptimal = 1.5;
            atom.MaxAcceptable = 3.1;
            SettingsAtomApplierByAbsoluteValue applier = 
                new SettingsAtomApplierByAbsoluteValue(atom);
            Assert.IsTrue(applier.GetMarkForValue(1.5) == 9);
            Assert.IsTrue(applier.GetMarkForValue(0.5) == 9);
            Assert.IsTrue(applier.GetMarkForValue(0.75) == 10);
            Assert.IsTrue(applier.GetMarkForValue(0.91) == 10);
            Assert.IsTrue(applier.GetMarkForValue(2.5) > 2);
            Assert.IsTrue(applier.GetMarkForValue(2.5) < 8);
            Assert.IsTrue(applier.GetMarkForValue(3.1) == 2);
            Assert.IsTrue(applier.GetMarkForValue(4) == 1);
            double d = applier.GetMarkForValue(2.5);
        }
    }
}
