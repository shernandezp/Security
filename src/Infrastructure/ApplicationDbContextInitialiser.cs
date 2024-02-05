using Common.Domain.Enums;
using Common.Domain.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Security.Infrastructure;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
{
    public async Task InitialiseAsync()
    {
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!context.Profiles.Any())
        {
            context.Profiles.Add(new Profile
            {
                Name = "Administrador",
                Description = string.Empty
            });

            await context.SaveChangesAsync();
        }

        if (!context.Users.Any())
        {
            var account = new Account
            {
                Name = "Administrador",
                Description = string.Empty,
                Type = AccountType.Business,
                Active = true
            };
            context.Accounts.Add(account);
            await context.SaveChangesAsync();

            var password = "123456".HashPassword();
            context.Users.Add(new User(
                "Administrator",
                password,
                "email@mail.com",
                "Admin",
                "Admin",
                "",
                null,
                null,
                account.AccountId));

            await context.SaveChangesAsync();
        }
    }
}
