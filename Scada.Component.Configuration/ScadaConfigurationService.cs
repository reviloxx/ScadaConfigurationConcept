using Microsoft.Extensions.Configuration;
using Scada.Component.Configuration.Interfaces;
using System.Text;

namespace Scada.Component.Configuration;

public class ScadaConfigurationService(IConfigurationRepository repository)
{
    private readonly IConfigurationRepository _repository = repository;    
    private readonly List<IConfigurationContainer> _configurationContainers = [];
    private readonly IConfigurationRoot _initialConfiguration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("initialComponentConfigs.json", optional: false)
            .Build();

    public void AddConfigurationContainer(IConfigurationContainer configurationContainer)
    {
        if (_configurationContainers.Any(x => x.ConfigurationKey == configurationContainer.ConfigurationKey))
            return;

        _configurationContainers.Add(configurationContainer);        
    }

    public async Task UpdateConfiguration(string key, string jsonBody, CancellationToken cancellationToken = default)
    {
        var container = _configurationContainers.FirstOrDefault(x => x.ConfigurationKey == key);

        if (container == null)
            return;

        var configuration = new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(jsonBody))).Build();

        await _repository.UpdateConfigurationAsync(key, configuration, cancellationToken);
        await PushComponentConfiguration(container, cancellationToken);
    }

    public async Task PushAllComponentConfigurations(CancellationToken cancellationToken = default)
    {
        foreach (var container in _configurationContainers) 
        {
            await PushComponentConfiguration(container, cancellationToken);
        }
    }

    private async Task PushComponentConfiguration(IConfigurationContainer container, CancellationToken cancellationToken = default)
    {
        var configurationSection = await GetLatestConfigurationSection(container.ConfigurationKey, cancellationToken);
        var validationResult = container.ValidateConfiguration(configurationSection);

        if (!validationResult.IsValidConfiguration)
        {
            // TODO: Handling of invalid component config
            // Can copmponents be disabled if no valid config is present?
            // Should it be possible to update an invalid config and enable such a component at runtime?
            // How should the user be notified if A) a config is invalid at startup and B) he tries to push an invalid config?
        }

        container.AcceptConfiguration(configurationSection);
    }

    private async Task<IConfiguration> GetLatestConfigurationSection(string key, CancellationToken cancellationToken = default)
    {
        var config = await _repository.GetConfigurationAsync(key, cancellationToken);

        if (config != null)
            return config;

        config = _initialConfiguration.GetSection(key);

        // TODO: handling of missing config
        // Should it be possible to continue execution and push a missing config at runtime?
        if (config == null)
            throw new ApplicationException($"No configuration for key {key} available!");

        await _repository.UpdateConfigurationAsync(key, config, cancellationToken);
        return config;
    }
}
