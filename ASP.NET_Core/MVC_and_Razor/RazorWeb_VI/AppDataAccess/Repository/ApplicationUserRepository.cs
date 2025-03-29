using AppDataAccess.Data;
using AppDataAccess.Repository.IRepository;
using AppModels;
using ppDataAccess.Repository;

namespace AppDataAccess.Repository;

public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
{
    private readonly ApplicationDBContext _context;

    public ApplicationUserRepository(ApplicationDBContext context) : base(context)
    {
        this._context = context;
    }
}
