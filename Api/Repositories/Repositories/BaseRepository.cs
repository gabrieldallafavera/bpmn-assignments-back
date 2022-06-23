using Api.Database;
using Repository.Interface;

namespace Repository.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
        }

        public async Task<TEntity?> FindByIdAsync(int id)
        {
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }
            catch (Exception) { throw; }
        }

        public async Task<IList<TEntity>> ListAsync()
        {
            try
            {
                return await _context.Set<TEntity>().ToListAsync();
            }
            catch (Exception) { throw; }
        }

        public async Task<object> PaginateAsync(int itemsPerPage, int page)
        {
            try
            {
                var totalitems = _context.Set<TEntity>().ToList().Count();

                var totalPages = Math.Ceiling(totalitems / (float)itemsPerPage);

                var items = await _context.Set<TEntity>()
                    .Skip((page - 1) * itemsPerPage)
                    .Take(itemsPerPage)
                    .ToListAsync();

                return new
                {
                    items,
                    currentPage = page,
                    totalitems,
                    totalPages
                };
            }
            catch (Exception) { throw; }
        }

        public Task<TEntity> InsertAsync(TEntity data)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateAsync(TEntity data)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}