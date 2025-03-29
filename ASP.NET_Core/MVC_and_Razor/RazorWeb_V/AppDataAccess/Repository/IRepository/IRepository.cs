using System.Linq.Expressions;

namespace AppDataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    /***
     * Common methods of database are GetAll, GetById (FirstOrDefault), Add, Remove, RemoveRange
     */

    IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null,
        string ? includeProperties = null);

    T GetById(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

    void Add(T entity);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entity);
}