using Scada.Core.Api.Models;

namespace Scada.Component.WebUi.Interfaces;

public interface IConfigurationWebService
{
    Task<IEnumerable<ConfigurationModel>> GetConfigurationsAsync();
    Task UpdateConfigurationAsync(UpdateConfigurationModel configuration);
}
