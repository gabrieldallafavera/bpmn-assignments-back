using Api.Database.Entities.People;
using Api.Repository.Interface;

namespace Api.Repositories.Interface.People
{
    public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
    {
        RefreshToken? Find(string token);
    }
}
