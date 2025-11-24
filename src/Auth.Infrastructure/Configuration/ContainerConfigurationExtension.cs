using Microsoft.EntityFrameworkCore;
using Auth.Core.Abstractions.Repositories;
using Auth.Core.Abstractions.Services;
using Auth.Domain.UseCases.User.Commands;
using Auth.Domain.UseCases.User.Dto;
using Auth.Infrastructure.Database.EFContext;
using Auth.Infrastructure.Extensions;
using Auth.Infrastructure.Options;
using Auth.Infrastructure.Services;
using Auth.Infrastructure.UseCases.User.Entities;
using Auth.Infrastructure.UseCases.User.Repositories;
using Mapster;
// using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure.Configuration
{
    public static class ContainerConfigurationExtension
    {
        // Must accept IConfiguration to get the connection string
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // Configure JWT Options
            serviceCollection.Configure<TokenOptions>(configuration.GetSection("jwt"));

            return serviceCollection
                .RegisterMapsterConfiguration()
                .AddDatabase(configuration) // Pass configuration to AddDatabase
                .AddServices();
        }

        private static IServiceCollection AddServices(this IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton<ITokenService, TokenService>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IUserService, UserService>();

        private static IServiceCollection RegisterMapsterConfiguration(this IServiceCollection serviceCollection)
        {
            TypeAdapterConfig<RegisterCommand, UserEntity>
            .NewConfig()
            .Map(dest => dest.Password, src => PasswordHasher.HashPassword(src.Password));

            TypeAdapterConfig<UserEntity, UserDto>
            .NewConfig()
            .Map(dest => dest.Role, src => src.Role.Role);

            return serviceCollection;
        }

        // Now accepts IConfiguration
        private static IServiceCollection AddDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // Read the connection string "DefaultConnection" from appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            return serviceCollection
                // Configure DbContext to use SQL Server with the connection string
                .AddDbContext<UserContext>(opt => opt.UseSqlServer(connectionString))
                .AddScoped<Abstractions.IUserQueriesRepository, UserQueriesRepository>()
                .AddScoped<Core.Abstractions.Repositories.IUserQueriesRepository, UserQueriesRepository>()
                .AddScoped<Abstractions.IUserCommandsRepository, UserCommandsRepository>()
                .AddScoped<Abstractions.IRoleQueriesRepository, RoleQueriesRepository>()
                .AddScoped<Core.Abstractions.Repositories.IRoleQueriesRepository, RoleQueriesRepository>()
                .AddScoped<Abstractions.IRoleCommandRepository, RoleCommandRepository>()
                .AddScoped<Abstractions.IUserCommandsRepository, UserCommandsRepository>();
        }
    }
}