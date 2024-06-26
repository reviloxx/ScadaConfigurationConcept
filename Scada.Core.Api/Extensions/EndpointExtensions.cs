﻿using Scada.Component.Configuration.Interfaces;
using Scada.Core.Api.Models;

namespace Scada.Core.Api.Extensions;

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api");

        group.MapPut("/update-configuration", (IConfigurationService configService, UpdateConfigurationModel requestModel)
            => configService.UpdateConfigurationAsync(requestModel.ComponentId, requestModel.Configuration))
            .ProducesValidationProblem();

        group.MapGet("/configurations", (IConfigurationService configService) =>
        {
            var containers = configService.GetAllConfigurationsAsync();
            return containers.Select(x => new ConfigurationModel { ComponentId = x.ComponentId, ComponentName = x.ComponentName, Configuration = x.GetConfiguration() });
        });
    }         
}
