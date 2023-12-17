using Api_I.Endpoints;
using Library.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDBAccessService, DBAccessService>();
builder.Services.AddSingleton<ITodoService, TodoService>();

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

// For authorization
builder.Services.AddAuthorization(opts =>
{
    opts.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

/***
 * From the end users perspective nothing changed,
 * We can call just the way we used to
 * Instead of calling Controller we're using
 * 
 * * In Minimal Api we use "return Results.Ok(output)"
 * * In Normal Web Api we use "return Ok(output)"
 * 
 * app.MapGet("<url_path>", () => {
 *  // Do some work
 * });
 * 
 * Add Endpoints is the same file or create a separate file for that both works.
 */

app.MapGet("/api/Task", () => new string[] { "value1", "value2" }).AllowAnonymous();

app.MapGet("/api/Task/{id}", (int id) => $"Your Id: {id}").RequireAuthorization();

// Add separate file endpoints
app.AddAuthenticationEndpoint();
app.AddTodosEndpoint();

app.Run();