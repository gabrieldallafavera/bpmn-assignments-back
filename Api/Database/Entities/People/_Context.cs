using Api.Database.Configurations.People;
using Api.Database.Entities.People;
using Api.Services.Interface.Auth;

namespace Api.Database
{
    public partial class Context : DbContext
    {
        partial void PeopleConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
            modelBuilder.ApplyConfiguration(new ResetPasswordConfiguration());
            modelBuilder.ApplyConfiguration(new VerifyEmailConfiguration());

            _passwordHashService.CreatePasswordHash("My@Admin@Password", out byte[] passwordHash, out byte[] passwordSalt);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "My Admin User", Username = "MyAdminUser", Email = "myadminuser@email.com", PasswordHash = passwordHash, PasswordSalt = passwordSalt, Role = "Admin", VerifiedAt = DateTime.Now }
            );
        }

        public DbSet<User> User => Set<User>();
        public DbSet<RefreshToken> RefreshToken => Set<RefreshToken>();
        public DbSet<ResetPassword> ResetPassword => Set<ResetPassword>();
        public DbSet<VerifyEmail> VerifyEmail => Set<VerifyEmail>();
    }
}
