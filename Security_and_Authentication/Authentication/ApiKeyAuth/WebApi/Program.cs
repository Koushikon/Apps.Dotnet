using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.CustomFilters;
//using WebApi.CustomMiddleware;
using WebApi.Interfaces;
using WebApi.Logics;
using WebApi.PolicyBased;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register ApiKey Handler for Authentication & Authorization
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer();

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("ApiKeyPolicy", policy =>
    {
        policy.AddAuthenticationSchemes(new[] { JwtBearerDefaults.AuthenticationScheme });
        policy.Requirements.Add(new ApiKeyRequirement());
    });
});

// Register ApiKey Attribute Filter
builder.Services.AddScoped<ApiKeyAuthFilter>();

// Register ApiKeyValidation Logic
builder.Services.AddTransient<IApiKeyValidation, ApiKeyValidation>();

// Register ApiKeyHandler for Authorization
builder.Services.AddScoped<IAuthorizationHandler, ApiKeyHandler>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// IMPORTANT: When uncommenting this line also add directive using WebApi.CustomMiddleware;
//app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


/***
 * .AddEndpointFilter<ApiKeyEndpointFilter>()
 * Desc: Endpoint filters provide a way to apply authentication and authorization logic at the
 * endpoint level in ASP.NET Core. With endpoint filters, we can intercept requests before they reach the
 * 
 * Use: action methods and perform API key authentication checks.
 * The AddEndpointFilter extension method allows us to apply an endpoint filter
 * to the specific endpoint.
 * 
 * 200 Successful: https://localhost:44326/api/product
 * X-API-KEY: 6CBxzdYcEgNDrRhMbDpkBF7e4d4Kib46dwL9ZE5egiL0iL5Y3dzREUBSUYVUwUkN
 * 
 * 401 Unauthorized: https://localhost:44326/api/product
 * X-API-KEY: test
 * 
 * 400 Bad Request: https://localhost:44326/api/product
 * X-API-KEY: 
 */
app.MapGet("api/product", () =>
{
    return Results.Ok();
}).AddEndpointFilter<ApiKeyEndpointFilter>();


app.Run();