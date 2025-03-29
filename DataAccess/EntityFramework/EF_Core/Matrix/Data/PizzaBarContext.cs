using Microsoft.EntityFrameworkCore;
using Matrix.Models;

namespace Matrix.Data;

// Connection Strings Info link: https://learn.microsoft.com/en-us/ef/core/miscellaneous/connection-strings

public class PizzaBarContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Data Source=PC2;Initial Catalog=PizzaBar;Integrated Security=True;Trust Server Certificate=True");
    }
}