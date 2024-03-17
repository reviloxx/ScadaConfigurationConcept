using Microsoft.Extensions.DependencyInjection;
using Scada.Component.Configuration.Interfaces;

namespace Scada.Component.TemperatureSensor;
public static class DependencyInjection
{
    public static void RegisterTemperatureSensor(this IServiceCollection services, IConfigurationService configurationService)
    {
        var configurationContainer = new TemperatureSensorConfigurationContainer();
        configurationService.RegisterConfigurationContainer(configurationContainer);
        services.AddSingleton(configurationContainer);
    }
}
