namespace Scada.Component.Configuration.Interfaces;
public interface IConfigurationService
{
    void RegisterConfiguration(IConfigurationContainer configurationContainer);
    IEnumerable<IConfigurationContainer> GetAllConfigurationsAsync();
    Task UpdateConfigurationAsync(string key, string jsonBody, CancellationToken cancellationToken = default);
    Task PushAllConfigurationsAsync(CancellationToken cancellationToken = default);
}
