using Microsoft.Extensions.DependencyInjection;
using Scada.Component.Configuration.Interfaces;

namespace Scada.Component.PressureSensor;

public static class DependencyInjection
{
    public static void RegisterPressureSensor(this IServiceCollection services, IConfigurationService configurationService)
    {
        var configurationContainer = new PressureSensorConfigurationContainer();
        configurationService.RegisterConfigurationContainer(configurationContainer);
        services.AddSingleton(configurationContainer);
    }
}