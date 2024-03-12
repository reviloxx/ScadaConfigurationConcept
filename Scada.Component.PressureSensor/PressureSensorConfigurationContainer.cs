using Microsoft.Extensions.Configuration;
using Scada.Component.Configuration;
using Scada.Component.Configuration.Interfaces;

namespace Scada.Component.PressureSensor;

public class PressureSensorConfigurationContainer : IConfigurationContainer
{
    public string ConfigurationKey { get; } = "PressureSensor";
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

    public class PressureSensorConfiguration
    {
        public string PressureUnit { get; set; } = string.Empty;
        public int MeasurementIntervalMs { get; set; }
    }
}
