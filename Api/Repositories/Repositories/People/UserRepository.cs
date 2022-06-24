using Api.Database;
using Api.Database.Entities.People;
using Api.Repositories.Interface.People;
using Repository.Repository;

namespace Api.Repositories.Repositories.People
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context) { }

        public async Task<User?> Find(string? username, string? email)
        {
            try
            {
                var item = (from U in _context.User
                              where U.Username == username || U.Email == email
                              select U)
                          .Include(x => x.RefreshToken)
                          .FirstOrDefault();

                return item;
            }
            catch (Exception e)
            {

                throw;
            }
            
        }
    }
}
