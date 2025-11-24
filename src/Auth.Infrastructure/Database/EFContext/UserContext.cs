using Auth.Infrastructure.UseCases.User.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Database.EFContext
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // --- Role Configuration (for SQL Server) ---

            // Set Role name to be unique (crucial for a real DB)
            modelBuilder.Entity<RoleEntity>()
                .HasIndex(r => r.Role)
                .IsUnique();

            // Seed Data: Add initial roles so the app works from the start
            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity { Id = 1, Role = "User" },
                new RoleEntity { Id = 2, Role = "Admin" }
            );

            // --- User Configuration (Relationships) ---

            // Define the one-to-many relationship between Role and User
            modelBuilder.Entity<UserEntity>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}