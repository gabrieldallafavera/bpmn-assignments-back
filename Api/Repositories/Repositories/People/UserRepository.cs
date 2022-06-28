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
            return await (from U in _context.User
                          where U.Username == username || U.Email == email
                          select U)
                          .Include(x => x.UserRoles)
                          .Include(x => x.RefreshToken)
                          .Include(x => x.ResetPassword)
                          .Include(x => x.VerifyEmail)
                          .FirstOrDefaultAsync();

        }
    }
}
