
using Library.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Web_Api.StartupConfig;

public static class DependencyExtensions
{
    public static void AddStandardServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // If we don't want Authorization in SwaggerUI use this
        //builder.Services.AddSwaggerGen();

        // Otherwise, Using this to Swagger gen code separation
        builder.AddSwaggerServices();
    }


    /***
     * This code adds Authtication with Swagger UI
     */
    private static void AddSwaggerServices(this WebApplicationBuilder builder)
    {
        // This works for v3 of Swagger
        var securityScheme = new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Description = "JWT authorization header info using bearer tokens",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        };

        var securityRequirement = new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "bearerAuth"
                    }
                },
                new string[] {}
            }
        };

        builder.Services.AddSwaggerGen(opts =>
        {
            opts.AddSecurityDefinition("bearerAuth", securityScheme);
            opts.AddSecurityRequirement(securityRequirement);

        });
    }

    public static void AddCustomServices(this WebApplicationBuilder builder)
    {
        // DataAccess service
        builder.Services.AddSingleton<IDBAccessService, DBAccessService>();

        // Todo data service
        builder.Services.AddSingleton<ITodoService, TodoService>();

        // Logging service
        builder.Services.AddTransient(typeof(IUtilityLogger<>), typeof(UtilityLogger<>));
    }

    public static void AddAuthServices(this WebApplicationBuilder builder)
    {
        // For authorization
        builder.Services.AddAuthorization(opts =>
        {
            opts.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        // For authentication
        builder.Services.AddAuthentication("Bearer")
            .AddJwtBearer(opts =>
            {
                opts.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
                    ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("Authentication:SecretKey")!)
                    )
                };
            });
    }

    public static void AddHealthCheckServices(this WebApplicationBuilder builder)
    {
        // For Healthchecks
        builder.Services.AddHealthChecks()
            .AddSqlServer(builder.Configuration.GetValue<string>("ConnectionStrings:Default")!);
    }
}