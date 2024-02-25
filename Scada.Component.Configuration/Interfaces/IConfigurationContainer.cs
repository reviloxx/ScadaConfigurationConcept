using Microsoft.Extensions.Configuration;

namespace Scada.Component.Configuration.Interfaces;
public interface IConfigurationContainer
{
    public string ConfigurationSectionKey { get; }
    public ValidationResult ValidateConfigurationSection(IConfigurationSection configurationSection);
    public void AcceptConfigurationSection(IConfigurationSection configurationSection);
}
