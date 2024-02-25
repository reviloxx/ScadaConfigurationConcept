using Microsoft.Extensions.DependencyInjection;
using Scada.Components.Configuration;

namespace Scada.Components.TemperatureSensor;
public static class DependencyInjection
{
    public static void RegisterTemperatureSensor(this IServiceCollection services, ScadaConfigurationProvider configurationProvider)
    {
        var temperatureSensorConfig = new ConfigurationContainer();
        configurationProvider.AddConfigurationContainer(temperatureSensorConfig);
        services.AddSingleton(temperatureSensorConfig);
    }
}
