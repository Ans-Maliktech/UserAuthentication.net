using Auth.Infrastructure.Database.EFContext;
using Auth.Infrastructure.UseCases.User.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
// using SmallApiToolkit.Core.Response; // Required for 'Result<T>'
using Auth.Core.Common;
// Aliases to handle both interfaces clearly
using CoreInterfaces = Auth.Core.Abstractions.Repositories;
using InfraInterfaces = Auth.Infrastructure.Abstractions;

namespace Auth.Infrastructure.UseCases.User.Repositories
{
    public class RoleQueriesRepository : 
        CoreInterfaces.IRoleQueriesRepository, 
            InfraInterfaces.IRoleQueriesRepository
    {
        private readonly UserContext _context;

        public RoleQueriesRepository(UserContext context)
        {
            _context = context;
        }

        // 1. GetRoleEntity (Fixed Return Type)
        // The interface demands 'Task<Result<RoleEntity>>', not 'Task<RoleEntity?>'
        public async Task<Result<RoleEntity>> GetRoleEntity(string roleName, CancellationToken cancellationToken)
        {
            var role = await _context.Roles
                .Where(r => r.Role == roleName)
                .SingleOrDefaultAsync(cancellationToken);

            if (role == null)
            {
                return Result<RoleEntity>.Failure($"Role '{roleName}' not found");
            }

            return Result<RoleEntity>.Success(role);
        }

        // 2. GetRoleEntities (The Missing Method)
        // The error CS0535 said this was missing.
        public async Task<IEnumerable<RoleEntity>> GetRoleEntities(CancellationToken cancellationToken)
        {
            return await _context.Roles.ToListAsync(cancellationToken);
        }

        // 3. GetRoles (For the Core Interface)
        // We keep this to satisfy the other interface that wants just the names.
        public async Task<IEnumerable<string>> GetRoles(CancellationToken cancellationToken)
        {
            return await _context.Roles
                .Select(r => r.Role)
                .ToListAsync(cancellationToken);
        }
    }
}