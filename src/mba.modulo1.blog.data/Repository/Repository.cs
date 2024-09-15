using MBA.Modulo1.Blog.Data.Context;
using MBA.Modulo1.Blog.Domain.Entities;
using MBA.Modulo1.Blog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MBA.Modulo1.Blog.Data.Repository;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
{
    protected readonly BlogDbContext Db;
    protected readonly DbSet<TEntity> DbSet;

    protected Repository(BlogDbContext db)
    {
        Db = db;
        DbSet = db.Set<TEntity>();
    }

    public async Task AddAsync(TEntity entity)
    {
        DbSet.Add(entity);
        await SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        DbSet.Remove(new TEntity { Id = id });
        await SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        DbSet.Update(entity);
        await SaveChangesAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await Db.SaveChangesAsync();
    }

    public void Dispose()
    {
        Db.Dispose();
    }
}