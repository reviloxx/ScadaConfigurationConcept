using Microsoft.Extensions.Configuration;
using Scada.Component.Configuration;
using Scada.Component.Configuration.Interfaces;
using System.Text.Json;

namespace Scada.Component.TemperatureSensor;

public class TemperatureSensorConfigurationContainer : IConfigurationContainer
{
    public string ComponentId => "d1574589-8764-4cbf-a79d-f5701f30c6df";
    public string ComponentName => "Temperature Sensor";
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

    public string GetConfiguration()
    {
        return JsonSerializer.Serialize(Configuration);
    }
}

public class TemperatureSensorConfiguration
{
    public string TemperatureUnit { get; set; } = string.Empty;
    public int MeasurementIntervalMs { get; set; }
}
