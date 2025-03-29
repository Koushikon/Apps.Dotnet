using AppDataAccess.Data;
using AppDataAccess.Repository.IRepository;
using AppModels;
using ppDataAccess.Repository;

namespace AppDataAccess.Repository;

public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
{
    private readonly ApplicationDBContext _context;

    public FoodTypeRepository(ApplicationDBContext context) : base(context)
    {
        this._context = context;
    }

    public void Update(FoodType foodType)
    {
        var objFromDb = _context.FoodType.FirstOrDefault(u => u.Id == foodType.Id);

        if (objFromDb == null)
        {
            throw new Exception($"Id: {foodType.Id} data not found inside FoodType.");
        }

        /***
         * Depending on the what we want to update we can comment out the properties we don't want to Update.
         * This lets us to update only the needed part or we can check and update the value
         */

        objFromDb.Name = foodType.Name;
        _context.Update(objFromDb);
    }
}
