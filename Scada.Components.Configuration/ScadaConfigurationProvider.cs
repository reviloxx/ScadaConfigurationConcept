using Microsoft.Extensions.Configuration;

namespace Scada.Components.Configuration;

public class ScadaConfigurationProvider(IConfigurationRoot configurationRoot)
{
    private IConfigurationRoot _configurationRoot = configurationRoot;

    private List<IConfigurationContainer> _configurationContainers = [];

    public void AddConfigurationContainer(IConfigurationContainer configurationContainer)
    {
        _configurationContainers.Add(configurationContainer);        
    }

    // TODO: add mechanism to change configurations at runtime
    public void PushComponentConfigurations()
    {
        foreach (var container in _configurationContainers) 
        {
            // TODO: check if config exists in DB and only use values from initial json if not
            var configurationSection = _configurationRoot.GetSection(container.ConfigurationSectionKey);
            var validationResult = container.AcceptConfigurationSection(configurationSection);

            if (!validationResult.IsValid)
            {
                // TODO: handle invalid component configuration
            }
        }
    }
}
