using Microsoft.EntityFrameworkCore;
using Auth.Core.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Security.Principal;

namespace Auth.Api.Configuration
{
    public static class ContainerConfigurationExtension
    {
        public static IServiceCollection AddApi(this IServiceCollection serviceCollection)
            => serviceCollection
                    .AddHttpContextAccessor()
                    .AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>()!.HttpContext!.User);
    }
}
