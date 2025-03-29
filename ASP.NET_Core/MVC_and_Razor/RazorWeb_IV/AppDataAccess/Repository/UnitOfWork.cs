using AppDataAccess.Data;
using AppDataAccess.Repository.IRepository;

namespace AppDataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _context;

    #region Repository Services

    public ICategoryRepository Category { get; private set; }

    public IFoodTypeRepository FoodType { get; private set; }

    public IMenuItemRepository MenuItem { get; private set; }

    #endregion

    public UnitOfWork(ApplicationDBContext context)
    {
        _context = context;

        // Setting up the Services

        Category = new CategoryRepository(_context);
        FoodType = new FoodTypeRepository(_context);
        MenuItem = new MenuItemRepository(_context);
    }

    public void Dispose() => _context.Dispose();

    public void Save()
    {
        _context.SaveChanges();
    }
}