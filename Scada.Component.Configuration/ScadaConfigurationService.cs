using Microsoft.Extensions.Configuration;
using Scada.Component.Configuration.Interfaces;

namespace Scada.Component.Configuration;

public class ScadaConfigurationService(IConfigurationRoot initialConfigurationRoot, IConfigurationRepository repository)
{
    private readonly IConfigurationRoot _initialConfigurationRoot = initialConfigurationRoot;
    private readonly IConfigurationRepository _repository = repository;
    private readonly List<IConfigurationContainer> _configurationContainers = [];

    public void AddConfigurationContainer(IConfigurationContainer configurationContainer)
    {
        if (_configurationContainers.Any(x => x.ConfigurationSectionKey == configurationContainer.ConfigurationSectionKey))
            return;

        _configurationContainers.Add(configurationContainer);        
    }

    // TODO: add mechanism to change configurations at runtime
    public async Task PushAllComponentConfigurations()
    {
        foreach (var container in _configurationContainers) 
        {
            var configurationSection = await GetLatestConfigurationSection(container.ConfigurationSectionKey);
            var validationResult = container.ValidateConfigurationSection(configurationSection);

            if (!validationResult.IsValidConfiguration)
            {
                // TODO: handle invalid component configuration
                // Does it make sense to continue execution in certain cases?
                // Allow fixing the issue at runtime via API to update configs?
            }

            container.AcceptConfigurationSection(configurationSection);
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
