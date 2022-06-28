using Api.Database.Configurations.People;
using Api.Database.Entities.People;

namespace Api.Database
{
    public partial class Context : DbContext
    {
        partial void PeopleConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new TokenFunctionConfiguration());

            _passwordHashService.CreatePasswordHash("My@Admin@Password", out byte[] passwordHash, out byte[] passwordSalt);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "My Admin User", Username = "MyAdminUser", Email = "myadminuser@email.com", PasswordHash = passwordHash, PasswordSalt = passwordSalt, VerifiedAt = DateTime.Now }
            );
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, UserId = 1, Role = "Admin" },
                new UserRole { Id = 2, UserId = 1, Role = "User" }
            );
        }

        public DbSet<User> User => Set<User>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<TokenFunction> TokenFunction => Set<TokenFunction>();
    }
}
