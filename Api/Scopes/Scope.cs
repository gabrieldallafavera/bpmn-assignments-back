namespace Api.Scopes
{
    public static partial class Scope
    {
        public static void OnScopeCreating(IServiceCollection services)
        {
            // Adicionar chamadas
            ScopeServices(services);
            ScopeRepositories(services);
        }

        static partial void ScopeServices(IServiceCollection services);
        static partial void ScopeRepositories(IServiceCollection services);
    }
}
