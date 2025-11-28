using Ardalis.GuardClauses;
using Auth.Infrastructure.Database.EFContext;
using Auth.Infrastructure.UseCases.User.Entities;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using CoreInterfaces = Auth.Core.Abstractions.Repositories;
using InfraInterfaces = Auth.Infrastructure.Abstractions;

namespace Auth.Infrastructure.UseCases.User.Repositories
{
    internal sealed class RoleQueriesRepository : 
        CoreInterfaces.IRoleQueriesRepository, 
        InfraInterfaces.IRoleQueriesRepository
    {
        private readonly UserContext _context;

        public RoleQueriesRepository(UserContext context)
        {
            _context = Guard.Against.Null(context);
        }

        public async Task<Result<RoleEntity>> GetRoleEntity(string roleName, CancellationToken cancellationToken)
        {
            var role = await _context.Roles
                .Where(r => r.Role == roleName)
                .SingleOrDefaultAsync(cancellationToken);

            if (role is null)
            {
                return Result.Fail($"Role '{roleName}' not found");
            }

            return Result.Ok(role);
        }

        public async Task<IEnumerable<RoleEntity>> GetRoleEntities(CancellationToken cancellationToken)
        {
            return await _context.Roles.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<string>> GetRoles(CancellationToken cancellationToken)
        {
            return await _context.Roles
                .Select(r => r.Role)
                .ToListAsync(cancellationToken);
        }
    }
}