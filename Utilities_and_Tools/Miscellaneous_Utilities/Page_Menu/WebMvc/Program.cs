using Microsoft.EntityFrameworkCore;
using WebMvc.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<WebMvcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebMvcContext") ?? throw new InvalidOperationException("Connection string 'WebMvcContext' not found.")));

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
