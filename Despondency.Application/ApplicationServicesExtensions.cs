using System.Reflection;
using Despondency.Application.Common.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Despondency.Application;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudService<>), typeof(CrudService<>));
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}