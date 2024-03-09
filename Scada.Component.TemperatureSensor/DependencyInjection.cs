using Microsoft.Extensions.DependencyInjection;
using Scada.Component.Configuration;

namespace Scada.Component.TemperatureSensor;
public static class DependencyInjection
{
    public static void RegisterTemperatureSensor(this IServiceCollection services, ScadaConfigurationService configurationService)
    {
        var configurationContainer = new TemperatureSensorConfigurationContainer();
        services.AddSingleton(configurationContainer);
        configurationService.AddConfigurationContainer(configurationContainer);        
    }
}
