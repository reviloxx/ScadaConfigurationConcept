using Scada.Components.Configuration;
using Scada.Components.DummySensor;

var configRoot = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("initialComponentSettings.json", optional: false)
    .Build();

var configurationProvider = new ScadaConfigurationProvider(configRoot);

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterDummySensor(configurationProvider);
configurationProvider.PushComponentConfigurations();
builder.Services.AddHostedService<DummySensorService>();

var app = builder.Build();
app.Run();