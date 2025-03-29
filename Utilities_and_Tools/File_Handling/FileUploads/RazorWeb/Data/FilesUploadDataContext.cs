using Microsoft.EntityFrameworkCore;
using RazorWeb.Models;

namespace RazorWeb.Data;

public class FilesUploadDataContext : DbContext
{
    public FilesUploadDataContext(DbContextOptions options)
        : base(options)
    { }

    public DbSet<AppFile> File { get; set; }
}