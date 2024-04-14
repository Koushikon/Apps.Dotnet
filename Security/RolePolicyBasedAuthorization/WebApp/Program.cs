using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using WebApp.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("CookieAuthentication")
    .AddCookie("CookieAuthentication", opts =>
    {
        opts.Cookie.Name = "UserLoginCookie";   // Name of cookie
        opts.LoginPath = "/Login/Index";        // Path for the redirect to the Login Page
        opts.AccessDeniedPath = "/Login/UserAccessDenied";  // Path for the User Access Denied Page
    });

builder.Services.AddAuthorization(opts =>
{
    // Set the Policy which Checks Two data Email and DateOfBirth
    opts.AddPolicy("UserPolicy", opts =>
    {
        opts.UserRequireCustomClaim(ClaimTypes.Email);
        opts.UserRequireCustomClaim(ClaimTypes.DateOfBirth);
    });
});

builder.Services.AddScoped<IAuthorizationHandler, PoliciesAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler, RolesAuthorizationHandler>();

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

// who are you?
app.UseAuthentication();

// are you allowed?
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
