using Api.Database;
using Api.Database.Entities.People;
using Api.Repositories.Interface.People;
using Repository.Repository;

namespace Api.Repositories.Repositories.People
{
    public class VerifyEmailRepository : BaseRepository<VerifyEmail>, IVerifyEmailRepository
    {
        public VerifyEmailRepository(Context context) : base(context) { }

        public VerifyEmail? Find(string token)
        {
            return (from VE in _context.VerifyEmail
                    where VE.Token == token
                    select VE)
                    .Include(x => x.User)
                    .FirstOrDefault();
        }
    }
}
