namespace ShopSphere.Data.Repositories.Interfaces;

public interface ICrudRepository<TEntity, ID> where TEntity : class {
    Task<List<TEntity>> FindAllAsync();

    Task<TEntity> FindByIdAsync(ID id);

    Task<TEntity> SaveAsync(TEntity entity);
    
    Task<TEntity> DeleteAsync(TEntity entity);
}