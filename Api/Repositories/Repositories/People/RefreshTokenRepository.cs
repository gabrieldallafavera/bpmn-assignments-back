﻿using Api.Database;
using Api.Database.Entities.People;
using Api.Repositories.Interface.People;
using Repository.Repository;

namespace Api.Repositories.Repositories.People
{
    public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(Context context) : base(context) { }

        public async Task<RefreshToken?> Find(string token)
        {
            return await (from RT in _context.RefreshToken
                         where RT.Token == token
                         select RT)
                         .Include(x => x.User)
                         .FirstOrDefaultAsync();
        }
    }
}