namespace Api.Scopes
{
    internal static partial class Scope
    {
        internal static void OnScopeCreating(IServiceCollection services)
        {
            // Adicionar chamadas
            ScopeAuth(services);
        }

        static partial void ScopeAuth(IServiceCollection services);
    }
}
