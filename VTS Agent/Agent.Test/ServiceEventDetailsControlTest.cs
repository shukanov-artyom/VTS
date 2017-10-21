using System;
using System.Diagnostics;
using Agent.Workspace.ViewModels.Maintenance;
using Agent.Workspace.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VTS.Shared.DomainObjects;

namespace Agent.Workspace.Test
{
    [TestClass]
    public class ServiceEventDetailsControlTest
    {
        [TestMethod]
        public void TestBinding()
        {
            var control = new ServiceEventDetailsControl();
            var viewModel = new VehicleEventViewModel(new VehicleEvent(), new Vehicle());
            control.DataContext = viewModel;

            control.ComboBoxType.SelectedValue = VehicleEventType.EngineRepair;

            Assert.IsTrue(viewModel.SelectedType.Model == VehicleEventType.EngineRepair);
        }
    }
}
