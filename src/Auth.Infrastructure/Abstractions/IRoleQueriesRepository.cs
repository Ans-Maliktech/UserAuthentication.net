using Auth.Core.Common; // This finds your new Result class
using Auth.Infrastructure.UseCases.User.Entities; // This finds RoleEntity
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Auth.Infrastructure.Abstractions
{
    public interface IRoleQueriesRepository
    {
        // 1. Must return Task<Result<RoleEntity>> to match your Repository
        Task<Result<RoleEntity>> GetRoleEntity(string roleName, CancellationToken cancellationToken);

        // 2. Must be present because your Repository implements it
        Task<IEnumerable<RoleEntity>> GetRoleEntities(CancellationToken cancellationToken);
    }
}