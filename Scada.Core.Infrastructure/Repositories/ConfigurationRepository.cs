using Microsoft.Extensions.Configuration;
using Scada.Component.Configuration.Interfaces;
using System.Collections.Concurrent;

namespace Scada.Core.Infrastructure.Repositories;
public class ConfigurationRepository : IConfigurationRepository
{
    private ConcurrentDictionary<string, IConfiguration> _configurations = [];

    public Task<IConfiguration?> GetConfigurationAsync(string key, CancellationToken cancellationToken)
    {
        _configurations.TryGetValue(key, out var config);
        return Task.FromResult(config);
    }

    public Task UpdateConfigurationAsync(string key, IConfiguration configuration, CancellationToken cancellationToken)
    {
        _configurations.AddOrUpdate(key, configuration, (key, oldvalue) => oldvalue = configuration);
        return Task.CompletedTask;
    }
}
