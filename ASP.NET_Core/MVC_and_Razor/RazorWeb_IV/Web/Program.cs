using AppDataAccess.Data;
using AppDataAccess.Repository;
using AppDataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Web.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();

// Configure TinyMice key
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("TinyMice"));

// Database Configure
builder.Services.AddDbContext<ApplicationDBContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("Type1")!)
);

/***
 * (*) Register IUnitOfWork which implements Different Repositories
 * When we are working with Database its best to use AddScoped
 * Scoped objects are the same within a request, but different across different requests.
 */
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

/**
 * With this code we're adding support for Controller in Razor page Web Project
 * ASP.Net Core is the base of it. That allows multiple architechture in one Project
 */
app.MapControllers();

app.Run();