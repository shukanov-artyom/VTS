using System;

namespace Agent.Common.Presentation.Controls
{
    public interface IParametersSetSettingsSource
    {
        event EventHandler GraphAdded;

        event EventHandler GraphRemoved;

        event EventHandler GraphColorChanged;
    }
}
