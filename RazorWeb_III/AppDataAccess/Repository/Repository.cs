using AppDataAccess.Data;
using AppDataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ppDataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    internal readonly ApplicationDBContext context;
    internal DbSet<T> dbSet;


    public Repository(ApplicationDBContext context)
    {
        this.context = context;
        this.dbSet = context.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return query.ToList();
    }

    public T GetById(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null)
    {
        IQueryable<T> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return query.FirstOrDefault();
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entites)
    {
        dbSet.RemoveRange(entites);
    }
}
