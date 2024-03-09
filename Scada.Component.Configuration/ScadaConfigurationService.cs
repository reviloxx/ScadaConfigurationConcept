using Microsoft.Extensions.Configuration;
using Scada.Component.Configuration.Interfaces;
using System.Text;

namespace Scada.Component.Configuration;

public class ScadaConfigurationService(IConfigurationRoot initialConfiguration, IConfigurationRepository repository)
{
    private readonly IConfigurationRoot _initialConfiguration = initialConfiguration;
    private readonly IConfigurationRepository _repository = repository;
    private readonly List<IConfigurationContainer> _configurationContainers = [];

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
        {
            await Console.Out.WriteLineAsync("No config found to update!");
            return;
        }

        var configuration = new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.ASCII.GetBytes(jsonBody))).Build();

        var validationResult = container.ValidateConfiguration(configuration);
        if (!validationResult.IsValidConfiguration)
        {
            // TODO: expose validation errors to API response
            await Console.Out.WriteLineAsync("Config not valid!");
            return;
        }

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
            // TODO: handle invalid component configuration
            // Does it make sense to continue execution in certain cases?
            // Allow fixing the issue at runtime via API to update configs?
        }

        container.AcceptConfiguration(configurationSection);
    }

    private async Task<IConfiguration> GetLatestConfigurationSection(string key, CancellationToken cancellationToken = default)
    {
        var section = await _repository.GetConfigurationAsync(key, cancellationToken);

        if (section != null)
            return section;

        section = _initialConfiguration.GetSection(key);

        if (section == null)
            throw new ApplicationException($"No configuration for key {key} available!");

        await _repository.UpdateConfigurationAsync(key, section, cancellationToken);
        return section;
    }
}
