using CargoFlow.BuildingBlocks.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CargoFlow.BuildingBlocks.Application.DependencyInjection;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddBuildingBlocksApplication(this IServiceCollection services)
    {
        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));
        return services;
    }
}
