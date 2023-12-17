using AppDataAccess.Data;
using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.EntityFrameworkCore;
using ppDataAccess.Repository;

namespace AppDataAccess.Repository;

public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
{
    private readonly ApplicationDBContext _context;

    public OrderHeaderRepository(ApplicationDBContext context) : base(context)
    {
        this._context = context;
    }

    public void Update(OrderHeader orderHeader)
    {
        /***
         * This will modify all the properties inside the existings OrderHeader
         * based on the Id, which do the Update automatically 
         */
        _context.Entry(orderHeader).State = EntityState.Detached;
        _context.OrderHeader.Update(orderHeader);
    }
}