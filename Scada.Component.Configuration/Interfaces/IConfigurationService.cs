namespace Scada.Component.Configuration.Interfaces;
public interface IConfigurationService
{
    void RegisterConfigurationContainer(IConfigurationContainer configurationContainer);
    IEnumerable<IConfigurationContainer> GetConfigurationContainersAsync();
    Task UpdateConfigurationAsync(string key, string jsonBody, CancellationToken cancellationToken = default);
    Task PushAllComponentConfigurationsAsync(CancellationToken cancellationToken = default);
}
