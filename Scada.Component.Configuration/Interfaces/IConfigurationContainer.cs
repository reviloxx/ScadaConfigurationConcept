using Microsoft.Extensions.Configuration;

namespace Scada.Component.Configuration.Interfaces;
public interface IConfigurationContainer
{
    public string ConfigurationKey { get; }
    public ValidationResult ValidateConfiguration(IConfiguration configuration);
    public void AcceptConfiguration(IConfiguration configuration);
}
