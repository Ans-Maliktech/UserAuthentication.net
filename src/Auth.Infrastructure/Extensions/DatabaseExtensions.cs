using Auth.Infrastructure.Database.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Auth.Infrastructure.Extensions
{
    public static class DatabaseExtensions
    {
        // Changed input from IApplicationBuilder (Web) to IServiceProvider (Generic)
        public static void ApplyDatabaseMigrations(IServiceProvider serviceProvider)
        {
            try
            {
                var context = serviceProvider.GetRequiredService<UserContext>();
                
                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                var logger = serviceProvider.GetRequiredService<ILogger<UserContext>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
    }
}