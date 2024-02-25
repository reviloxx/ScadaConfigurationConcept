using Microsoft.Extensions.Configuration;
using Scada.Component.Configuration.Interfaces;
using Scada.Components.Configuration.Interfaces;

namespace Scada.Components.Configuration;

public class ScadaConfigurationProvider(IConfigurationRoot initialConfigurationRoot, IConfigurationRepository repository)
{
    private IConfigurationRoot _initialConfigurationRoot = initialConfigurationRoot;
    private readonly IConfigurationRepository _repository = repository;
    private List<IConfigurationContainer> _configurationContainers = [];

    public void AddConfigurationContainer(IConfigurationContainer configurationContainer)
    {
        _configurationContainers.Add(configurationContainer);        
    }

    // TODO: add mechanism to change configurations at runtime
    public async Task PushComponentConfigurations()
    {
        foreach (var container in _configurationContainers) 
        {
            var configurationSection = await GetLatestConfigurationSection(container.ConfigurationSectionKey);
            var validationResult = container.AcceptConfigurationSection(configurationSection);

            if (!validationResult.IsValid)
            {
                // TODO: handle invalid component configuration
            }
        }
    }

    private async Task<IConfigurationSection> GetLatestConfigurationSection(string key, CancellationToken cancellationToken = default)
    {
        var section = await _repository.GetConfigurationSectionAsync(key, cancellationToken);

        if (section != null)
            return section;

        section = _initialConfigurationRoot.GetSection(key);

        if (section == null)
            throw new ApplicationException($"No configuration for key {key} available!");

        await _repository.UpdateConfigurationSectionAsync(key, section, cancellationToken);
        return section;
    }
}
