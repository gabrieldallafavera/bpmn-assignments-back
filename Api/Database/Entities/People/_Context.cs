using Api.Database.Configurations.People;
using Api.Database.Entities.People;
using Api.Services.Interface.Auth;

namespace Api.Database
{
    public partial class Context : DbContext
    {
        partial void PeopleConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());

            _passwordHashService.CreatePasswordHash("My@Admin@Password", out byte[] passwordHash, out byte[] passwordSalt);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "My Admin User", Username = "MyAdminUser", Email = "myadminuser@email.com", PasswordHash = passwordHash, PasswordSalt = passwordSalt, Role = "Admin" } // Adicionar Refresh token
            );
        }

        public DbSet<User> User => Set<User>();
    }
}
