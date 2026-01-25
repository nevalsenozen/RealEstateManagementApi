using System.Linq.Expressions;

namespace RealEstateManagement.Data.Abstract;

public interface IRepository<T> where T : class
{
    Task<T> GetAsync(int id);

    Task<T> GetAsync(
        Expression<Func<T, bool>> predicate,
        bool showIsDeleted = false,
        bool asExpanded = false,
        params Func<IQueryable<T>, IQueryable<T>>[] includes
    );

    Task<IEnumerable<T>> GetAllAsync();

    Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>> predicate = null!,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
        bool showIsDeleted = false,
        bool asExpanded = false,
        params Func<IQueryable<T>, IQueryable<T>>[] includes
    );

    Task<int> CountAsync();

    Task<int> CountAsync(Expression<Func<T, bool>> predicate);

    Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

    Task AddAsync(T entity);

    void Update(T entity);

    void BatchUpdate(IEnumerable<T> entities);

    void Remove(T entity);
}

