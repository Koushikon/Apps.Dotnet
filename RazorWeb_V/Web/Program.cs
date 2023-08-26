using AppDataAccess.Data;
using AppDataAccess.Repository;
using AppDataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity.UI.Services;
using AppUtility;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();

// Configure TinyMice key
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("TinyMice"));

// Database Configure
builder.Services.AddDbContext<ApplicationDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("ShopperV"),
    sqlServerOptionsAction =>
    {
        sqlServerOptionsAction.EnableRetryOnFailure(maxRetryCount: 4, maxRetryDelay: TimeSpan.FromSeconds(1), errorNumbersToAdd: new int[] { });
    });

    // Turn off query Tracking behavior
    opts.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

    // In development we may be able to log additional info that can help us
    if (builder.Environment.IsDevelopment())
    {
        opts.EnableDetailedErrors();    // To get field-level error details
        opts.EnableSensitiveDataLogging();  // To get parameter values - don't do this in production!
        opts.ConfigureWarnings(warningAction =>
        {
            warningAction.Log(new EventId[]
            {
                CoreEventId.FirstWithoutOrderByAndFilterWarning,
                CoreEventId.RowLimitingOperationWithoutOrderByWarning
            });
        });
    }
});

/**
 * If we want to use Roles
 * We have to change it from AddDefaultIdentity to CustomIdentity
 * 
 * Means: with AddIdentity with Passing IdentityRole
 * Also we're AddDefaultTokenProviders
 */
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

// Register Email Sending Service
builder.Services.AddSingleton<IEmailSender, EmailSender>();

/***
 * (*) Register IUnitOfWork which implements Different Repositories
 * When we are working with Database its best to use AddScoped
 * Scoped objects are the same within a request, but different across different requests.
 */
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

/***
 * Here we can set the default cookies Path of our Application
 * Because we're using Custom Identity as ApplicationUser
 * We have to provide some authrization and usefull Paths
 */
builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.LoginPath = "/Identity/Account/Login";
    opts.LogoutPath = "/Identity/Account/Logout";
    opts.AccessDeniedPath = "/Identity/Account/AccessDenied";
});

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