using Domain.Configurations.People;
using Domain.Entities.People;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    public partial class Context : DbContext
    {
        partial void PeopleConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
        }

        public DbSet<User> User => Set<User>();
    }
}
