using System;

namespace Agent.Common.Presentation.Controls.ScaleControllers
{
    public abstract class ChartController
    {
        private readonly IParametersSetSettingsSource settings;
        private readonly IParametersSetGraphView view;

        public ChartController(
            IParametersSetSettingsSource settings,
            IParametersSetGraphView view)
        {
            settings.GraphAdded += SettingsOnGraphAdded;
            settings.GraphRemoved += SettingsOnGraphRemoved;
            settings.GraphColorChanged += SettingsOnGraphColorChanged;
            this.settings = settings;
            this.view = view;
        }

        protected abstract void SettingsOnGraphColorChanged(object sender, EventArgs eventArgs);

        protected abstract void SettingsOnGraphRemoved(object sender, EventArgs eventArgs);

        protected abstract void SettingsOnGraphAdded(object sender, EventArgs eventArgs);

        protected IParametersSetSettingsSource Settings
        {
            get
            {
                return settings;
            }
        }

        protected IParametersSetGraphView View
        {
            get
            {
                return view;
            }
        }
    }
}
