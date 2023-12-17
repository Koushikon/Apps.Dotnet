using AppDataAccess.Data;
using AppDataAccess.Repository;
using AppDataAccess.Repository.IRepository;
using AppUtility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Stripe;
using Web.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();

// Configure TinyMice Rich Text Editor key
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("TinyMice"));

// EF.Core Database Configure
builder.Services.AddDbContext<ApplicationDBContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("ShopperVI"),
    sqlServerOptionsAction =>
    {
        sqlServerOptionsAction.EnableRetryOnFailure(maxRetryCount: 4, maxRetryDelay: TimeSpan.FromSeconds(1), errorNumbersToAdd: Array.Empty<int>());
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

/***
 * (*) Register IUnitOfWork which implements Different Repositories
 * When we are working with Database its best to use AddScoped
 * Scoped objects are the same within a request, but different across different requests.
 */
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

/***
 * If we want to use Roles
 * We have to change it from AddDefaultIdentity to CustomIdentity
 * 
 * Means: with AddIdentity with Passing IdentityRole
 * Also we're AddDefaultTokenProviders
 */
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

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

// Register Email Sending Service
builder.Services.AddSingleton<IEmailSender, EmailSender>();

// Bind Stripe Credentials
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Add the Stripe Secret key to the Stripe Confihuration
string stripeSecretKey = builder.Configuration.GetValue<string>("Stripe:SecretKey") ?? string.Empty;
StripeConfiguration.ApiKey = stripeSecretKey;

app.UseAuthorization();

app.MapRazorPages();

/**
 * With this code we're adding support for Controller in Razor page Web Project
 * ASP.Net Core is the base of it. That allows multiple architechture in one Project
 */
app.MapControllers();

app.Run();