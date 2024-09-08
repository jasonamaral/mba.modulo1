using MBA.Modulo1.Blog.Domain.Entities;
using System.Linq.Expressions;

namespace MBA.Modulo1.Blog.Domain.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task AddAsync(TEntity entity);

    Task<TEntity> GetByIdAsync(Guid id);

    Task<List<TEntity>> GetAll();

    Task UpdateAsync(TEntity entity);

    Task DeleteByIdAsync(Guid id);

    Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);

    Task<int> SaveChanges();
}