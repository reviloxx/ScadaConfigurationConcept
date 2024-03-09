using Scada.Component.Configuration;
using Scada.Component.TemperatureSensor;
using Scada.Component.PressureSensor;
using Scada.Core.Infrastructure.Repositories;
using Scada.Core.Api;

var configRoot = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("initialComponentSettings.json", optional: false)
    .Build();

// create configuration service
var configurationService = new ScadaConfigurationService(configRoot, new ConfigurationRepository());

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(configurationService);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register sensor components
builder.Services.RegisterTemperatureSensor(configurationService);
builder.Services.RegisterPressureSensor(configurationService);

// push configurations to all registered components
await configurationService.PushAllComponentConfigurations();

// start services
builder.Services.AddHostedService<TemperatureSensorService>();
builder.Services.AddHostedService<PressureSensorService>();

// TODO: register configuration api
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapPost("/config", (ScadaConfigurationService configService, ConfigRequestModel requestModel) => configService.UpdateConfiguration(requestModel.ConfigurationKey, requestModel.Configuration))
           .ProducesValidationProblem();

app.Run();