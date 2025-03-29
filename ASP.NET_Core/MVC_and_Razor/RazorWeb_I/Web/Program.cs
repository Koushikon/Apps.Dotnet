using Microsoft.EntityFrameworkCore;
using Web.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();

// Database Configure
builder.Services.AddDbContext<ApplicationDBContext>(opts =>
    opts.UseSqlServer(builder.Configuration.GetConnectionString("Type1")!)
);

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
