using Microsoft.Extensions.Configuration;

namespace Scada.Components.Configuration;
public interface IConfigurationContainer
{
    public string ConfigurationSectionKey { get; }
    public ValidationResult AcceptConfigurationSection(IConfigurationSection configurationSection);
}
