using Scada.Components.Configuration;
using Scada.Components.TemperatureSensor;
using Scada.Core.Infrastructure.Repositories;

var configRoot = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("initialComponentSettings.json", optional: false)
    .Build();

var configurationProvider = new ScadaConfigurationProvider(configRoot, new ConfigurationRepository());

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterTemperatureSensor(configurationProvider);
await configurationProvider.PushComponentConfigurations();
builder.Services.AddHostedService<TemperatureSensorService>();

var app = builder.Build();
app.Run();