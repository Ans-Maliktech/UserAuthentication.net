using System.Collections.Generic; // Ensure this is at the top

namespace Auth.Infrastructure.UseCases.User.Entities
{
    public class RoleEntity
    {
        public int Id { get; init; }
        public string Role { get; set; } = "user";

        // *** ADD THIS LINE ***
        // This allows EF Core to track which users belong to this role
        public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    }
}