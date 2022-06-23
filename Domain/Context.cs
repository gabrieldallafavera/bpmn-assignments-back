using Microsoft.EntityFrameworkCore;

namespace Domain
{
    partial class Context : DbContext
    {
        public Context() { }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Database=[BANCO];Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Adicionar chamadas
            PeopleConfiguration(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        // Adicionar configurações
        partial void PeopleConfiguration(ModelBuilder modelBuilder);
    }
}