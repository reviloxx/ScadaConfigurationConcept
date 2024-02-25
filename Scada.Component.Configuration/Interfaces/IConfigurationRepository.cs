using Microsoft.Extensions.Configuration;

namespace Scada.Component.Configuration.Interfaces;
public interface IConfigurationRepository
{
    Task UpdateConfigurationSectionAsync(string key, IConfigurationSection configurationSection, CancellationToken cancellationToken);
    Task<IConfigurationSection?> GetConfigurationSectionAsync(string key, CancellationToken cancellationToken);
}
