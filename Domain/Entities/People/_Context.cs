using Domain.Configurations.People;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.People
{
    public partial class Context : DbContext
    {
        partial void PeopleConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public DbSet<User> User { get; set; }
    }
}
