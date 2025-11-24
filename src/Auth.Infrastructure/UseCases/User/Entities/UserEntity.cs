using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Infrastructure.UseCases.User.Entities
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        
        // *** ADDED THESE 2 PROPERTIES ***
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public int RoleId { get; set; }

        // initialized with 'null!' to silence the "Non-nullable" warning
        public RoleEntity Role { get; set; } = null!; 
    }
}