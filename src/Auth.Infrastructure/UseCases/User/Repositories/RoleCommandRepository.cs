using Ardalis.GuardClauses;
using Auth.Infrastructure.Abstractions;
using Auth.Infrastructure.Database.EFContext;
using Auth.Infrastructure.UseCases.User.Entities;

namespace Auth.Infrastructure.UseCases.User.Repositories
{
    internal sealed class RoleCommandRepository : IRoleCommandRepository
    {
        private readonly UserContext _context;

        public RoleCommandRepository(UserContext userContext)
        {
            _context = Guard.Against.Null(userContext);
        }

        public async Task AddRoles(IEnumerable<string> roles, CancellationToken cancellationToken)
        {
            foreach (var role in roles)
            {
                await _context.Roles.AddAsync(new RoleEntity { Role = role }, cancellationToken);
            }
            
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}