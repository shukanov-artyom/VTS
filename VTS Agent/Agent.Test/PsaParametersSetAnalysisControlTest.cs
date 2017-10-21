using System;
using System.Windows.Media;
using Agent.Common.Presentation.Controls;
using Agent.Common.Presentation.Data;
using Agent.Common.Presentation.Lexia;
using Agent.Workspace.ViewModels;
using Agent.Workspace.Views;
using DevExpress.Xpf.Charts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using VTS.Shared;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.Test
{
    [TestClass]
    public class PsaParametersSetAnalysisControlTest
    {
        [TestMethod]
        public void TestCreateSeries()
        {
            Mock<IParametersSetSettingsSource> mock = new Mock<IParametersSetSettingsSource>();
            PsaParametersSetAnalysisControl control = new PsaParametersSetAnalysisControl(mock.Object);
            PsaParametersSet set = GenerateParametersSet();
            IPsaParametersSetViewModel psvm = new PsaParametersSetViewModel(set);
            Series s = control.CreateSeries(psvm.Parameters[0], new Color(), new ChartScaleViewModel(new ChartScale()));
            Series sp = control.CreateSeries(psvm.Parameters[1], new Color(), new ChartScaleViewModel(new ChartScale()));
            control.AddSeries(s);
            control.AddSeries(sp);

            Assert.IsNotNull(s, "Series is null");
            Assert.IsNotNull(control.BaseParameter, "Base parameter is null");
            Assert.AreEqual(control.BaseParameter.Length, 8, "Base parameter length is not expected.");
        }

        [TestMethod]
        public void TestfindSeries()
        {
            Mock<IParametersSetSettingsSource> mock = new Mock<IParametersSetSettingsSource>();
            PsaParametersSetAnalysisControl control = new PsaParametersSetAnalysisControl(mock.Object);
            PsaParametersSet set = GenerateParametersSet();
            IPsaParametersSetViewModel psvm = new PsaParametersSetViewModel(set);
            PsaParameterDataViewModel first =  psvm.Parameters[0];
            var second = psvm.Parameters[1];
            Series s = control.CreateSeries(psvm.Parameters[0], new Color(), new ChartScaleViewModel(new ChartScale()));
            Series sp = control.CreateSeries(psvm.Parameters[1], new Color(), new ChartScaleViewModel(new ChartScale()));
            control.AddSeries(s);
            control.AddSeries(sp);

            Series found = control.FindSeries(second);
            Assert.IsNotNull(found);
            Assert.IsTrue(ReferenceEquals(found.DataContext, second));
        }

        [TestMethod]
        public void TestRemoveSeries()
        {
            Mock<IParametersSetSettingsSource> mock = new Mock<IParametersSetSettingsSource>();
            PsaParametersSetAnalysisControl control = new PsaParametersSetAnalysisControl(mock.Object);
            PsaParametersSet set = GenerateParametersSet();
            IPsaParametersSetViewModel psvm = new PsaParametersSetViewModel(set);
            Series s = control.CreateSeries(psvm.Parameters[0], new Color(), new ChartScaleViewModel(new ChartScale()));
            Series sp = control.CreateSeries(psvm.Parameters[1], new Color(), new ChartScaleViewModel(new ChartScale()));
            control.AddSeries(s);
            control.AddSeries(sp);

            control.RemoveSeries(sp);
            Assert.IsNotNull(control.BaseParameter);
            Assert.AreEqual(control.BaseParameter.Length, 8, "Base parameter length is not expected.");
            Series find = control.FindSeries(psvm.Parameters[1]);
            Assert.IsNull(find);
        }

        [TestMethod]
        public void TestRemoveBase()
        {
            Mock<IParametersSetSettingsSource> mock = new Mock<IParametersSetSettingsSource>();
            PsaParametersSetAnalysisControl control = new PsaParametersSetAnalysisControl(mock.Object);
            PsaParametersSet set = GenerateParametersSet();
            IPsaParametersSetViewModel psvm = new PsaParametersSetViewModel(set);
            Series s = control.CreateSeries(psvm.Parameters[0], new Color(), new ChartScaleViewModel(new ChartScale()));
            Series sp = control.CreateSeries(psvm.Parameters[1], new Color(), new ChartScaleViewModel(new ChartScale()));
            control.AddSeries(s);
            Series baseS = control.FindSeries(psvm.Parameters[0]);
            Assert.IsNotNull(baseS);
            control.RemoveSeries(baseS);
            Assert.IsTrue(control.BaseParameter == null);
        }

        private PsaParametersSet GenerateParametersSet()
        {
            var result = new PsaParametersSet()
                                      {
                                          AdditionalSourceInfo = "SourceInfo",
                                          EcuLabel = "ECU LABEL",
                                          EcuName = "ESCU1",
                                          Id=21,
                                          OriginalName = "Orig Name",
                                          OriginalTypeId = "@123-GG",
                                          PsaTraceId = 23, 
                                          Type = PsaParametersSetType.Mixed
                                      };
            var rpmData = new PsaParameterData("Engine RPM")
                          {
                              AdditionalSourceInfo = "Add source info",
                              HasTimestamps = false,
                              Id= 34,
                              OriginalName = "Engine RPM",
                              OriginalTypeId = "Eng RPMMM",
                              PsaParametersSetId = 21,
                              Type = PsaParameterType.EngineRpm,
                              Units = Unit.Rpm
                          };
            rpmData.Values.Clear();
            rpmData.Values.Add("720");
            rpmData.Values.Add("720");
            rpmData.Values.Add("1200");
            rpmData.Values.Add("1300");
            rpmData.Values.Add("1300");
            rpmData.Values.Add("1400");
            rpmData.Values.Add("1200");
            rpmData.Values.Add("720");
            result.Parameters.Add(rpmData);
            var otherData = new PsaParameterData("EngineRpm")
                            {
                                AdditionalSourceInfo = "Add source info",
                                HasTimestamps = false,
                                Id = 34,
                                OriginalName = "Engine RPM",
                                OriginalTypeId = "Eng RPMMM",
                                PsaParametersSetId = 21,
                                Timestamps = { },
                                Type = PsaParameterType.OilPressure,
                                Units = Unit.Bar
                            };
            otherData.Values.Clear();
            otherData.Values.Add("72");
            otherData.Values.Add("72");
            otherData.Values.Add("120");
            otherData.Values.Add("130");
            otherData.Values.Add("130");
            otherData.Values.Add("140");
            otherData.Values.Add("120");
            otherData.Values.Add("72");
            result.Parameters.Add(otherData);
            return result;
        }
    }
}
