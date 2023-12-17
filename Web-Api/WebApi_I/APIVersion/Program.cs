using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add API Versioning with default Version setup
builder.Services.AddApiVersioning(opts =>
{
    opts.AssumeDefaultVersionWhenUnspecified = true;
    //opts.DefaultApiVersion = new ApiVersion(1, 0); // (major_version, minor_version)
    opts.DefaultApiVersion = new(1, 0); // Same as previous line
    opts.ReportApiVersions = true; // This will inform which versions available for given End-point when we call
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    var title = "Version API";
    var description = "Version API project demonstrates versioning of an API.";
    var terms = new Uri("https://localhost:7246");
    var contact = new OpenApiContact()
    {
        Name = "Koushik Helpdesk",
        Email = "koushiksahaask@yahoo.com",
        Url = new Uri("https://www.google.com")
    };
    var license = new OpenApiLicense()
    {
        Name = "Full License Information",
        Url = new Uri("https://localhost:7246")
    };

    opts.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = $"{title} v1 (Deprecated)",
        Description = "Deprecated on Apr-20-2023",
        TermsOfService = terms,
        Contact = contact,
        License = license
    });

    // The information can be different for different API versions.
    opts.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = $"{title} v2",
        Description = description,
        TermsOfService = terms,
        Contact = contact,
        License = license
    });
});

builder.Services.AddVersionedApiExplorer(opts =>
{
    opts.GroupNameFormat = "'v'VVV";
    opts.SubstituteApiVersionInUrl = true; // Use the Top Tight Dropdown instead of Text Version Field to Select the Version
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts =>
    {
        opts.SwaggerEndpoint("/swagger/v2/swagger.json", "v2"); // Latest API version selected first
        opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1 (Deprecated)");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
