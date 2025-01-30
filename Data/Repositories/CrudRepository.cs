using Microsoft.EntityFrameworkCore;
using ShopSphere.Data.Repositories.Interfaces;

namespace ShopSphere.Data.Repositories;

public class CrudRepository<TEntity, ID> : ICrudRepository<TEntity, ID> where TEntity : class {
    private readonly ShopSphereContext dbContext;
    private readonly DbSet<TEntity> dbSet;
    public virtual string NotFoundErrorMessage => "No entity found with this id";

    public CrudRepository(ShopSphereContext shopSphereContext){
        dbContext = shopSphereContext;
        dbSet = dbContext.Set<TEntity>();
    }

    public virtual async Task<TEntity> DeleteAsync(TEntity entity) {
        dbSet.Remove(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<List<TEntity>> FindAllAsync() {
        return await dbSet.ToListAsync();
    }

    public virtual async Task<TEntity> FindByIdAsync(ID id) {
        return await dbSet.FindAsync(id) ?? throw new KeyNotFoundException(NotFoundErrorMessage);
    }

    public virtual async Task<TEntity> SaveAsync(TEntity entity) {
        if (dbSet.Contains(entity)) {
            dbContext.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        } else await dbContext.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }
}