namespace Api.Repository.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> FindById(int id);

        Task<IList<TEntity>> List();
        
        Task<object> Paginate(int itemsPerPage, int page);
        
        Task<TEntity> Insert(TEntity data);
        
        Task<TEntity> Update(TEntity data);
        
        Task Delete(int id);
    }
}
