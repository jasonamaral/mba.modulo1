using MBA.Modulo1.Blog.Domain.Entities;
using System.Linq.Expressions;

namespace MBA.Modulo1.Blog.Domain.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task AddAsync(TEntity entity);

    Task<TEntity> GetByIdAsync(Guid id);

    Task<List<TEntity>> GetAllAsync();

    Task UpdateAsync(TEntity entity);

    Task DeleteByIdAsync(Guid id);

    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);

    Task<int> SaveChangesAsync();
}