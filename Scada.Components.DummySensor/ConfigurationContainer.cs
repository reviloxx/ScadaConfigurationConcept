using Microsoft.Extensions.Configuration;
using Scada.Components.Configuration;

namespace Scada.Components.DummySensor;

public class ConfigurationContainer : IConfigurationContainer
{
    public string ConfigurationSectionKey { get; } = "DummySensor";
    public Configuration Configuration { get; private set; } = new();

    public ValidationResult AcceptConfigurationSection(IConfigurationSection configurationSection)
    {
        configurationSection.Bind(Configuration);

        // TODO: validate configuration and return proper result
        return new ValidationResult() { IsValid = true };
    }
}

public class Configuration
{
    public string TemperatureUnit { get; set; } = string.Empty;
    public int MeasurementIntervalMs { get; set; }
}
