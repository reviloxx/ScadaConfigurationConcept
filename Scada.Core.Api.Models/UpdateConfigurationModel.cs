namespace Scada.Core.Api.Models;
public class UpdateConfigurationModel
{
    public string ComponentId { get; set; } = string.Empty;
    public string Configuration { get; set; } = string.Empty;
}
