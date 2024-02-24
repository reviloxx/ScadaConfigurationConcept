using Microsoft.Extensions.DependencyInjection;
using Scada.Components.Configuration;

namespace Scada.Components.DummySensor;
public static class DependencyInjection
{
    public static void RegisterDummySensor(this IServiceCollection services, ScadaConfigurationProvider configurationProvider)
    {
        var dummySensorConfig = new ConfigurationContainer();
        configurationProvider.AddConfigurationContainer(dummySensorConfig);
        services.AddSingleton(dummySensorConfig);
    }
}
