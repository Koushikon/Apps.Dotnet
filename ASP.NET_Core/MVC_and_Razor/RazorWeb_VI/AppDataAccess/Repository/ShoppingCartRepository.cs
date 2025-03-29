using AppDataAccess.Data;
using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.EntityFrameworkCore;
using ppDataAccess.Repository;

namespace AppDataAccess.Repository;

public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
{
    private readonly ApplicationDBContext _context;

    public ShoppingCartRepository(ApplicationDBContext context) : base(context)
    {
        this._context = context;
    }

    public int DecrementCount(ShoppingCart shoppingCart, int count)
    {
        shoppingCart.Count -= count;

        _context.Entry(shoppingCart).State = EntityState.Detached;
        _context.Update(shoppingCart);
        //_context.SaveChanges();
        return shoppingCart.Count;
    }

    public int IncrementCount(ShoppingCart shoppingCart, int count)
    {
        shoppingCart.Count += count;

        _context.Entry(shoppingCart).State = EntityState.Detached;
        _context.Update(shoppingCart);
        //_context.SaveChanges();
        return shoppingCart.Count;
    }
}