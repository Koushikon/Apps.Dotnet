using Microsoft.EntityFrameworkCore;
using WebMvc.Models;

namespace WebMvc.Data;

public class WebMvcContext : DbContext
{
    public WebMvcContext(DbContextOptions<WebMvcContext> options)
        : base(options)
    {
    }

    public DbSet<Menu> Menu { get; set; } = default!;

    public DbSet<MenuItem> MenuItem { get; set; } = default!;
}