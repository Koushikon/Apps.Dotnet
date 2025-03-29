using AppDataAccess.Data;
using AppDataAccess.Repository;
using AppDataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();

// Database Configure
builder.Services.AddDbContext<ApplicationDBContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("Type1")!)
);

/***
 * (*) Register CategoryRepository using Unit of Work Pattern
 * When we are working with Database its best to use AddScoped
 * Scoped objects are the same within a request, but different across different requests.
 */
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFoodTypeRepository, FoodTypeRepository>();

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

app.Run();