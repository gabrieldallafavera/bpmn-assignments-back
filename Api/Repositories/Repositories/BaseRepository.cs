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

        public async Task<TEntity?> FindById(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IList<TEntity>> List()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<object> Paginate(int itemsPerPage, int page)
        {
            return PaginationBuilder<TEntity>.ToPagination(await _context.Set<TEntity>().ToListAsync(), itemsPerPage, page);
        }

        public async Task<TEntity> Insert(TEntity data)
        {
            await _context.Set<TEntity>().AddAsync(data);
            await _context.SaveChangesAsync();

            return data;
        }

        public async Task<TEntity> Update(TEntity data)
        {
            _context.Set<TEntity>().Update(data);
            await _context.SaveChangesAsync();

            return data;
        }

        public async Task Delete(int id)
        {
            var entidade = _context.Set<TEntity>().Find(id);

            if (entidade == null)
                throw new KeyNotFoundException("Objeto não encontrado.");

            _context.Set<TEntity>().Remove(entidade);

            await _context.SaveChangesAsync();
        }
    }
}