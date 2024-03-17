using Microsoft.Extensions.Configuration;
using Scada.Component.Configuration;
using Scada.Component.Configuration.Interfaces;
using System.Text.Json;

namespace Scada.Component.PressureSensor;

public class PressureSensorConfigurationContainer : IConfigurationContainer
{
    public string ComponentId => "344f923d-9cf3-4d79-8e25-565d95a4b602";
    public string ComponentName => "Pressure Sensor";

    public PressureSensorConfiguration Configuration { get; private set; } = new();

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

    public class PressureSensorConfiguration
    {
        public string PressureUnit { get; set; } = string.Empty;
        public int MeasurementIntervalMs { get; set; }
    }
}
