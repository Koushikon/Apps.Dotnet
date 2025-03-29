using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Product> Products { get; set; }
}