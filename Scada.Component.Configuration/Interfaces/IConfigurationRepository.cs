using Microsoft.Extensions.Configuration;

namespace Scada.Component.Configuration.Interfaces;
public interface IConfigurationRepository
{
    Task UpdateConfigurationAsync(string key, IConfiguration configuration, CancellationToken cancellationToken);
    Task<IConfiguration?> GetConfigurationAsync(string key, CancellationToken cancellationToken);
}
