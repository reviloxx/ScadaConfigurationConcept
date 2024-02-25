using Microsoft.Extensions.Configuration;

namespace Scada.Components.Configuration.Interfaces;
public interface IConfigurationContainer
{
    public string ConfigurationSectionKey { get; }
    public ValidationResult AcceptConfigurationSection(IConfigurationSection configurationSection);
}
