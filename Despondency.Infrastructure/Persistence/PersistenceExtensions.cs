using Despondency.Domain.Repositories;
using Despondency.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Despondency.Infrastructure.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString, string? migrationsAssembly = "")
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString, sql =>
        {
            if (!string.IsNullOrEmpty(migrationsAssembly))
            {
                sql.MigrationsAssembly(migrationsAssembly);
            }
        }));

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        services.AddScoped(typeof(IUnitOfWork), serviceProvider =>
            serviceProvider.GetRequiredService<AppDbContext>());
        
        return services;
    }
    
    public static void MigrateAdsDb(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
        serviceScope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
    }
}