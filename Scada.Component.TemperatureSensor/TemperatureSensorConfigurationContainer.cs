using Microsoft.Extensions.Configuration;
using Scada.Component.Configuration;
using Scada.Component.Configuration.Interfaces;

namespace Scada.Component.TemperatureSensor;

public class TemperatureSensorConfigurationContainer : IConfigurationContainer
{
    public string ConfigurationKey { get; } = "TemperatureSensor";
    public TemperatureSensorConfiguration Configuration { get; private set; } = new();

    public ValidationResult ValidateConfiguration(IConfiguration configuration)
    {
        // TODO: validate configuration and return proper result
        return new ValidationResult() { IsValidConfiguration = true };
    }

    public void AcceptConfiguration(IConfiguration configuration)
    {
        configuration.Bind(Configuration);
    }
}

public class TemperatureSensorConfiguration
{
    public string TemperatureUnit { get; set; } = string.Empty;
    public int MeasurementIntervalMs { get; set; }
}
