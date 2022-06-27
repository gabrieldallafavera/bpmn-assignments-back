using Api.Database.Entities.People;
using Api.Repositories.Interface.People;
using Repository.Repository;

namespace Api.Repositories.Repositories.People
{
    public class ResetPasswordRepository : BaseRepository<ResetPassword>, IResetPasswordRepository
    {
        public ResetPasswordRepository(Context context) : base(context) { }

        public ResetPassword? Find(string token)
        {
            return (from RP in _context.ResetPassword
                    where RP.Token == token
                    select RP)
                    .Include(x => x.User)
                    .FirstOrDefault();
        }
    }
}
