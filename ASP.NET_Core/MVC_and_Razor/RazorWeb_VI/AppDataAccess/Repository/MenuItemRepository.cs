using AppDataAccess.Data;
using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.EntityFrameworkCore;
using ppDataAccess.Repository;

namespace AppDataAccess.Repository;

public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
{
    private readonly ApplicationDBContext _context;

    public MenuItemRepository(ApplicationDBContext context) : base(context)
    {
        this._context = context;
    }

    public void Update(MenuItem menuItem)
    {
        var objFromDb = _context.MenuItem.FirstOrDefault(u => u.Id == menuItem.Id);

        if (objFromDb == null)
        {
            throw new Exception($"Id: {menuItem.Id} data not found inside MenuItem.");
        }

        /***
         * Depending on the what we want to update we can comment out the properties we don't want to Update.
         * This lets us to update only the needed part or we can check and update the value
         */

        if (!string.IsNullOrWhiteSpace(menuItem.Name))
        {
            objFromDb.Name = menuItem.Name;
        }
        if (!string.IsNullOrWhiteSpace(menuItem.Description))
        {
            objFromDb.Description = menuItem.Description;
        }
        if (!string.IsNullOrWhiteSpace(menuItem.Image))
        {
            objFromDb.Image = menuItem.Image;
        }
        objFromDb.CategoryId = menuItem.CategoryId;
        objFromDb.FoodTypeId = menuItem.FoodTypeId;

        _context.Entry(objFromDb).State = EntityState.Detached;
        _context.Update(objFromDb);
    }
}