using AppDataAccess.Data;
using AppDataAccess.Repository.IRepository;

namespace AppDataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDBContext _context;

    #region Repository Fields

    public ICategoryRepository Category { get; private set; }

    public IFoodTypeRepository FoodType { get; private set; }

    public IMenuItemRepository MenuItem { get; private set; }

    public IShoppingCartRepository ShoppingCart { get; private set; }

    public IOrderHeaderRepository OrderHeader { get; private set; }

    public IOrderDetailsRepository OrderDetails { get; private set; }

    public IApplicationUserRepository ApplicationUser { get; private set; }

    #endregion

    public UnitOfWork(ApplicationDBContext context)
    {
        _context = context;

        // Setting up the Services

        Category = new CategoryRepository(_context);
        FoodType = new FoodTypeRepository(_context);
        MenuItem = new MenuItemRepository(_context);
        ShoppingCart = new ShoppingCartRepository(_context);
        OrderHeader = new OrderHeaderRepository(_context);
        OrderDetails = new OrderDetailsRepository(_context);
        ApplicationUser = new ApplicationUserRepository(_context);
    }

    public void Dispose() => _context.Dispose();

    public void Save()
    {
        _context.SaveChanges();
    }
}