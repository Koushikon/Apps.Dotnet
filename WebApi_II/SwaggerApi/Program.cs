using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

/***
 * Swagger Api Versions Customize
 */
builder.Services.AddSwaggerGen(opts =>
{
    opts.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Big Bang",
        Description = "When the Api starts.",
        TermsOfService = new Uri("https://www.google.com"),
        License = new OpenApiLicense()
        {
            Name = "Testing License",
            Url = new Uri("https://www.google.com")
        },
        Contact = new OpenApiContact()
        {
            Name = "Testing Info",
            Url = new Uri("https://www.google.com")
        },
    });

    // Attaches the xml document file 
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
});
//builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    /***
     * We're currently using Swagger version 3
     * But we can use previous version if we want to
     * Default is Microsoft's adopted latest version for this Template
     */

    //app.UseSwagger(opts =>
    //{
    //    opts.SerializeAsV2 = true;
    //});
    app.UseSwagger();

    app.UseSwaggerUI(opts =>
    {
        // Sets the Swagger Josn Path | Same Default Path
        opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

        // Sets api root path to Swagger and also set "LaunchUrl" in Launch.Json
        opts.RoutePrefix = string.Empty;

        // Add Different Themes Style for Swagger
        opts.InjectStylesheet("css/theme-newspaper.css");
    });
}

app.UseHttpsRedirection();

/***
 * We're adding UseStaticFiles(); to use wwwroot/ files
 * For this case Swagger Themes
 */
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
