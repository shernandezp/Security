using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Security.Domain.Interfaces;
using Security.Infrastructure;
using Security.Infrastructure.Interfaces;
using Security.Infrastructure.Readers;
using Security.Infrastructure.Writers;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Security");

        Guard.Against.Null(connectionString, message: "Connection string 'Security' not found.");

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUserWriter, UserWriter>();
        services.AddScoped<IUserReader, UserReader>();

        services.AddScoped<ApplicationDbContextInitialiser>();

        return services;
    }
}
