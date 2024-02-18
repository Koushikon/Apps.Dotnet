using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using WebApi.Data;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.Options;
using WebApi.Repositories;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Telling how to resolve our database options
builder.Services.ConfigureOptions<DatabaseOptionsSetup>();

builder.Services.AddDbContext<ApplicationDBContext>((sp, dcob) =>
{
    var dbOptions = sp.GetService<IOptions<DatabaseOptions>>()!.Value;

    // We could pass the Hardcoded values or dbOptions values coming from appsettings.json
    //string connectionString = builder.Configuration.GetConnectionString("Database") ?? string.Empty;
    // "Database": "Server=(localdb)\\ProjectModels,Database=DotnetDTOs,Trusted_Connection=True",

    dcob.UseSqlServer(dbOptions.ConnectionString, sqlOpts =>
    {
        sqlOpts.EnableRetryOnFailure(dbOptions.MaxRetryCount);
        sqlOpts.CommandTimeout(dbOptions.CommandTimeout);
    });
    dcob.EnableDetailedErrors(dbOptions.EnableDetailedErrors);
    dcob.EnableSensitiveDataLogging(dbOptions.EnableSensetiveDataLoggging);
});


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/**
 * This sets a serialization option to handle reference loops. Reference loops can occur when there are
 * circular references in the object graph being serialized. In this case, the code instructs the JSON
 * serializer to ignore reference loops, which means it will not attempt to serialize properties
 * that would create circular references.
 */
//builder.Services.AddControllers().AddNewtonsoftJson(opts =>
//{
//    opts.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
//});


builder.Services.AddIdentity<AppUser, IdentityRole>(opts =>
{
    opts.Password.RequireDigit = true;
    opts.Password.RequireLowercase = true;
    opts.Password.RequireUppercase = true;
    opts.Password.RequireNonAlphanumeric = true;
    opts.Password.RequiredLength = 10;
}).AddEntityFrameworkStores<ApplicationDBContext>();

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = 
    opts.DefaultChallengeScheme =
    opts.DefaultForbidScheme = 
    opts.DefaultScheme =
    opts.DefaultSignInScheme =
    opts.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]!))
    };
});

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
