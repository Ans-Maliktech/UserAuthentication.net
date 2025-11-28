using Auth.Infrastructure.UseCases.User.Entities;
using FluentResults;

namespace Auth.Infrastructure.Abstractions
{
    internal interface IRoleQueriesRepository
    {
        // These methods return the actual Database Entity (which Core layer cannot see)
        Task<Result<RoleEntity>> GetRoleEntity(string roleName, CancellationToken cancellationToken);
        Task<IEnumerable<RoleEntity>> GetRoleEntities(CancellationToken cancellationToken);
    }
}