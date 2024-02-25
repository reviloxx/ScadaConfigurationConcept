using Microsoft.Extensions.DependencyInjection;
using Scada.Component.Configuration;

namespace Scada.Component.TemperatureSensor;
public static class DependencyInjection
{
    public static void RegisterTemperatureSensor(this IServiceCollection services, ScadaConfigurationService configurationProvider)
    {
        var configurationContainer = new TemperatureSensorConfigurationContainer();
        services.AddSingleton(configurationContainer);
        configurationProvider.AddConfigurationContainer(configurationContainer);        
    }
}
