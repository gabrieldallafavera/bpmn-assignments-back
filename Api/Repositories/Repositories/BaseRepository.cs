using Api.Database;
using Api.Helpers.Pagination;
using Api.Repository.Interface;

namespace Repository.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public readonly Context _context;

        public BaseRepository(Context context)
        {
            _context = context;
        }

        public async Task<TEntity?> FindByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IList<TEntity>> ListAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<object> PaginateAsync(int itemsPerPage, int page)
        {
            return PaginationBuilder<TEntity>.ToPagination(await _context.Set<TEntity>().ToListAsync(), itemsPerPage, page);
        }

        public async Task<TEntity> InsertAsync(TEntity data)
        {
            try
            {
                _context.Set<TEntity>().Add(data);
                _context.SaveChanges();

                return data;
            } catch (Exception e)
            {
                throw;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity data)
        {
            _context.Set<TEntity>().Update(data);
            _context.SaveChanges();

            return data;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entidade = await _context.Set<TEntity>().FindAsync(id);

            if (entidade == null)
                throw new KeyNotFoundException("Objeto não encontrado.");

            _context.Set<TEntity>().Remove(entidade);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}