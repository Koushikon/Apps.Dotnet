using Microsoft.EntityFrameworkCore;
using WebApi.Data.Models;

namespace WebApi.Data;

public class OrdersDbContext(DbContextOptions<OrdersDbContext> options)
    : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
}