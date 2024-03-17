using Microsoft.Extensions.Configuration;

namespace Scada.Component.Configuration.Interfaces;
public interface IConfigurationContainer
{
    public string ComponentId { get; }
    public string ComponentName { get; }
    public ValidationResult ValidateConfiguration(IConfiguration configuration);
    public void AcceptConfiguration(IConfiguration configuration);
    public string GetConfiguration();
}
