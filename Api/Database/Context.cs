using Api.Services.Interface.Auth;

namespace Api.Database
{
    partial class Context : DbContext
    {
        private readonly IPasswordHashService _passwordHashService;

        public Context(DbContextOptions<Context> options, IPasswordHashService passwordHashService) : base(options)
        {
            _passwordHashService = passwordHashService;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Adicionar chamadas
            PeopleConfiguration(modelBuilder);
        }

        // Adicionar configurações
        partial void PeopleConfiguration(ModelBuilder modelBuilder);
    }
}