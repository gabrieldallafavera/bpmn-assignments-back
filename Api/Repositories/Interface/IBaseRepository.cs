namespace Api.Repository.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        TEntity? FindById(int id);

        IList<TEntity> List();
        
        object Paginate(int itemsPerPage, int page);
        
        TEntity Insert(TEntity data);
        
        TEntity Update(TEntity data);
        
        bool Delete(int id);
    }
}
