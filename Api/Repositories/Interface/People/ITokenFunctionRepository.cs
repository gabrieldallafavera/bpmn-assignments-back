using Api.Database.Entities.People;
using Api.Repository.Interface;

namespace Api.Repositories.Interface.People
{
    public interface ITokenFunctionRepository : IBaseRepository<TokenFunction>
    {
        Task<TokenFunction?> Find(string token, int type);
    }
}
