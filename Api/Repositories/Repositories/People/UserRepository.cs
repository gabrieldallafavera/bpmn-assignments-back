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
                          .Include(x => x.UserRole)
                          .Include(x => x.TokenFunction)
                          .FirstOrDefaultAsync();

        }

        public async Task<User> Insert(User user, TokenFunction tokenFunction, List<UserRole>? listUserRole)
        {
            using (var dbContextTransaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Set<User>().AddAsync(user);

                    await _context.SaveChangesAsync();

                    tokenFunction.UserId = user.Id;

                    await _context.Set<TokenFunction>().AddAsync(tokenFunction);

                    await _context.SaveChangesAsync();

                    if (listUserRole != null && listUserRole.Count() > 0)
                    {
                        foreach (var item in listUserRole)
                        {
                            item.UserId = user.Id;
                        }

                        await _context.Set<UserRole>().AddRangeAsync(listUserRole);

                        await _context.SaveChangesAsync();
                    }

                    await dbContextTransaction.CommitAsync();

                    return user;
                }
                catch (Exception)
                {
                    await dbContextTransaction.RollbackAsync();
    
                    throw;
                }
            }
        }
    }
}
