using AppDataAccess.Data;
using AppDataAccess.Repository.IRepository;
using AppModels;
using Microsoft.EntityFrameworkCore;
using ppDataAccess.Repository;

namespace AppDataAccess.Repository;

public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
{
    private readonly ApplicationDBContext _context;

    public OrderDetailsRepository(ApplicationDBContext context) : base(context)
    {
        this._context = context;
    }

    public void Update(OrderDetails orderDetails)
    {
        /***
         * This will modify all the properties inside the existings OrderDetails
         * based on the Id, which do the Update automatically 
         */
        _context.Entry(orderDetails).State = EntityState.Detached;
        _context.OrderDetails.Update(orderDetails);
    }
}