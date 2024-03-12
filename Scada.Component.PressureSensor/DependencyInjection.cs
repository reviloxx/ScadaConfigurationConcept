using Microsoft.Extensions.DependencyInjection;
using Scada.Component.Configuration;

namespace Scada.Component.PressureSensor;

public static class DependencyInjection
{
    public static void RegisterPressureSensor(this IServiceCollection services, ScadaConfigurationService configurationService)
    {
        var configurationContainer = new PressureSensorConfigurationContainer();
        services.AddSingleton(configurationContainer);
        configurationService.AddConfigurationContainer(configurationContainer);
    }
}