using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// register localization support with specified path for localize files
builder.Services.AddLocalization(opts => opts.ResourcesPath = "Resources");

// Adds Localization Views and DataAnnotation support
builder.Services
    .AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

// Configuring the supported cultures for our application:
builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
    const string defaultCulture = "en-US";
    var supportedCulture = new[]
    {
        new CultureInfo(defaultCulture),
        new CultureInfo("es"),
        new CultureInfo("bn-IN")
    };

    opts.DefaultRequestCulture = new RequestCulture(defaultCulture);
    opts.SupportedCultures = supportedCulture;
    opts.SupportedUICultures = supportedCulture;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// we need to tell our application to use these supported cultures.
app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Localization}/{action=Index}/{id?}");

app.Run();
