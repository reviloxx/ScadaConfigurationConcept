namespace Scada.Core.Api;

public class ConfigRequestModel
{
    public string ConfigurationKey { get; set; } = string.Empty;
    public string Configuration { get; set; } = string.Empty;
}
