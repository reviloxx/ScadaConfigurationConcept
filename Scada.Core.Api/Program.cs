using Scada.Component.Configuration;
using Scada.Component.TemperatureSensor;
using Scada.Component.PressureSensor;
using Scada.Core.Infrastructure.Repositories;
using Scada.Core.Api;


// create configuration service
var configurationService = new ScadaConfigurationService(new ConfigurationRepository());

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(configurationService);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// register sensor components
builder.Services.RegisterTemperatureSensor(configurationService);
builder.Services.RegisterPressureSensor(configurationService);

// push configurations to all registered components
await configurationService.PushAllComponentConfigurations();

// start sensor services
builder.Services.AddHostedService<TemperatureSensorService>();
builder.Services.AddHostedService<PressureSensorService>();

var app = builder.Build();

// add api to update configs
app.UseSwagger();
app.UseSwaggerUI();

app.MapPut("/config", (ScadaConfigurationService configService, ConfigRequestModel requestModel) 
    => configService.UpdateConfiguration(requestModel.ConfigurationKey, requestModel.Configuration))
    .ProducesValidationProblem();

app.Run();