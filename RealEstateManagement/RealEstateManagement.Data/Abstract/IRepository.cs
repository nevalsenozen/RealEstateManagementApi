using System.Linq.Expressions;

namespace RealEstateManagement.Data.Abstract;

public interface IRepository<T> where T : class
{
    Task<T?> GetAsync(int id);

    Task<T?> GetAsync(
        Expression<Func<T, bool>> predicate,
        bool asNoTracking = true,
        params Expression<Func<T, object>>[] includes
    );

    Task<List<T>> GetAllAsync();

    Task<List<T>> GetAllAsync(
        Expression<Func<T, bool>> predicate,
        bool asNoTracking = true,
        params Expression<Func<T, object>>[] includes
    );

    Task<int> CountAsync();

    Task<int> CountAsync(Expression<Func<T, bool>> predicate);

    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);
}
