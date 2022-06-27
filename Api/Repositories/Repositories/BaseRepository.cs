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

        public TEntity? FindById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IList<TEntity> List()
        {
            return _context.Set<TEntity>().ToList();
        }

        public object Paginate(int itemsPerPage, int page)
        {
            return PaginationBuilder<TEntity>.ToPagination(_context.Set<TEntity>().ToList(), itemsPerPage, page);
        }

        public TEntity Insert(TEntity data)
        {
            _context.Set<TEntity>().Add(data);
            _context.SaveChanges();

            return data;
        }

        public TEntity Update(TEntity data)
        {
            _context.Set<TEntity>().Update(data);
            _context.SaveChanges();

            return data;
        }

        public bool Delete(int id)
        {
            var entidade = _context.Set<TEntity>().Find(id);

            if (entidade == null)
                throw new KeyNotFoundException("Objeto não encontrado.");

            _context.Set<TEntity>().Remove(entidade);

            _context.SaveChanges();

            return true;
        }
    }
}