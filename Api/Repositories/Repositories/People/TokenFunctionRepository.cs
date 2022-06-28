using Api.Database.Entities.People;
using Api.Repositories.Interface.People;
using Repository.Repository;

namespace Api.Repositories.Repositories.People
{
    public class TokenFunctionRepository : BaseRepository<TokenFunction>, ITokenFunctionRepository
    {
        public TokenFunctionRepository(Context context) : base(context) { }

        public async Task<TokenFunction?> Find(string token, int type)
        {
            return await (from TF in _context.TokenFunction
                          where TF.Token == token && TF.Type == type
                          select TF)
                          .Include(x => x.User)
                          .ThenInclude(x => x!.UserRole)
                          .FirstOrDefaultAsync();
        }
    }
}
