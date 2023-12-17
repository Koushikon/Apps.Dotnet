using AppDataAccess.Data;
using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.EntityFrameworkCore;
using ppDataAccess.Repository;

namespace AppDataAccess.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDBContext _context;

    public CategoryRepository(ApplicationDBContext context) : base(context)
    {
        this._context = context;
    }

    public void Update(Category category)
    {
        var objFromDb = _context.Category.FirstOrDefault(u => u.Id == category.Id);

        if (objFromDb == null)
        {
            throw new Exception($"Id: {category.Id} data not found inside Category.");
        }

        /***
         * Depending on the what we want to update we can comment out the properties we don't want to Update.
         * This lets us to update only the needed part or we can check and update the value
         */

        objFromDb.Name = category.Name;
        objFromDb.Description = category.Description;
        objFromDb.DisplayOrder = category.DisplayOrder;

        _context.Entry(objFromDb).State = EntityState.Detached;
        _context.Update(objFromDb);
    }
}
