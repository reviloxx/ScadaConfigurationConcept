using Scada.Component.WebUi.Interfaces;
using Scada.Core.Api.Models;

namespace Scada.Component.WebUi.Services;

public class ConfigurationWebService(HttpClient httpClient) : IConfigurationWebService
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IEnumerable<ConfigurationModel>> GetConfigurationsAsync()
    {
        return await _httpClient.GetFromJsonAsync<ConfigurationModel[]>("api/configurations") ?? [];
    }

    public async Task UpdateConfigurationAsync(UpdateConfigurationModel configuration)
    {
        await _httpClient.PutAsJsonAsync("api/update-configuration", configuration);
    }
}
