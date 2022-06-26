using Api.Database.Entities.People;
using Api.Repository.Interface;

namespace Api.Repositories.Interface.People
{
    public interface IResetPasswordRepository : IBaseRepository<ResetPassword>
    {
        ResetPassword? Find(string token);
    }
}
