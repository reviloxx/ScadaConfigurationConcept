using Scada.Component.Configuration;
using Scada.Component.TemperatureSensor;
using Scada.Component.PressureSensor;
using Scada.Core.Infrastructure.Repositories;
using Scada.Core.Api.Models;
using Scada.Component.Configuration.Interfaces;
using Scada.Core.Api.Extensions;


// create configuration service
var configurationService = new ConfigurationService(new ConfigurationRepository());

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IConfigurationService>(configurationService);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register sensor components
builder.Services.RegisterTemperatureSensor(configurationService);
builder.Services.RegisterPressureSensor(configurationService);

// push configurations to all registered components
await configurationService.PushAllConfigurationsAsync();

// start sensor services
builder.Services.AddHostedService<TemperatureSensorService>();
builder.Services.AddHostedService<PressureSensorService>();

var app = builder.Build();

// add api to update configs
app.UseSwagger();
app.UseSwaggerUI();

app.MapEndpoints();

app.Run();