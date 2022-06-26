using Api.Database.Entities.People;
using Api.Repository.Interface;

namespace Api.Repositories.Interface.People
{
    public interface IVerifyEmailRepository : IBaseRepository<VerifyEmail>
    {
        VerifyEmail? Find(string token);
    }
}
