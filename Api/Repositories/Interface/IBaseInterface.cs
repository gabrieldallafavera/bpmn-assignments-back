namespace Repository.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> FindByIdAsync(int id);

        Task<IList<TEntity>> ListAsync();
        
        Task<object> PaginateAsync(int itemsPerPage, int page);
        
        Task<TEntity> InsertAsync(TEntity data);
        
        Task<TEntity> UpdateAsync(TEntity data);
        
        Task<TEntity> DeleteAsync(int id);
    }
}
