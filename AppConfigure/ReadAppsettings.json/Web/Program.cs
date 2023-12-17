var builder = WebApplication.CreateBuilder(args);


/**
 * * To Get the values from "appsettings.json"
 * * Don't need for ASP.Net application
 */
//builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

/**
 * * Reading ConnectionString Different Way possible
 */

// Read Connection String
var ProductsDb = app.Configuration.GetConnectionString("ProductsDb")?? string.Empty;

// Read Connection Strings From a Custom Configuration Section
var prodDb = app.Configuration.GetSection("Modules:Products").GetConnectionString("Database") ?? string.Empty;
var userDb = app.Configuration.GetSection("Modules:Users").GetConnectionString("Database") ?? string.Empty;

// In case we do not want to use a ConnectionString property, we can get configuration this way
var usersDbTwo = app.Configuration["ModulesTwo:Users:Database"];
var usersDbTwoOr = app.Configuration.GetValue<string>("ModulesTwo:Users:Database"); // Or, This way

// Read Data from Windows Enviroment Variables
var MyMail = Environment.GetEnvironmentVariable("MailFrom");

/**
 * * Reading ConnectionString Different Way possible
 */

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
