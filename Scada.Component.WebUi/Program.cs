using MudBlazor.Services;
using Scada.Component.WebUi.Components;
using Scada.Component.WebUi.Interfaces;
using Scada.Component.WebUi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<IConfigurationWebService, ConfigurationWebService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5093");
});

builder.Services.AddMudServices();

var app = builder.Build();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
