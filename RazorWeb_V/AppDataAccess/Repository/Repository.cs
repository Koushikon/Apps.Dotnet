using AppDataAccess.Data;
using AppDataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ppDataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    internal readonly ApplicationDBContext context;
    internal DbSet<T> dbSet;


    public Repository(ApplicationDBContext context)
    {
        this.context = context;
        this.dbSet = context.Set<T>();

        /**
         * When we're using ApplicationDBContext directly to use "Include" property like this:
         * When we're using ApplicationDBContext directly to use "OrderBy" property like this:
         */
        //context.MenuItem.Include(u => u.FoodType).Include(u => u.Category);
        //context.ShoppingCart.Include(u => u.MenuItem).ThenInclude(u => u.Category);
        //context.MenuItem.OrderBy(u => u.Name);
    }

	public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderby = null,
        string ? includeProperties = null)
    {
        IQueryable<T> query = dbSet;

		if (filter != null)
		{
			query = query.Where(filter);
		}

		if (includeProperties != null)
        {
            foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }

        // If orderby is not null we return the ordered list
        if(orderby != null)
        {
            return orderby(query).ToList();
        }

        return query.ToList();
    }

    public T GetById(Expression<Func<T, bool>>? filter = null, string ? includeProperties = null)
    {
        IQueryable<T> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

		if (!string.IsNullOrWhiteSpace(includeProperties))
		{
			foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(property);
			}
		}

		return query.FirstOrDefault()!;
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
