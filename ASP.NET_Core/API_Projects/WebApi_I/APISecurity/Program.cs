

using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using APISecurity.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Authorize by default and globaly
builder.Services.AddAuthorization(opts =>
{
    // Add new Custom Claim Policy
    opts.AddPolicy(PolicyConstants.MustHaveEmployeeId, policy => policy.RequireClaim("employeeId"));

    opts.AddPolicy(PolicyConstants.MustBeOwner, policy =>
    {
        // We can also provide valueto check
        // policy.RequireUserName("koushik");

        policy.RequireClaim("title", "Owner");
    });

    opts.AddPolicy(PolicyConstants.MustBeAdmin, policy => policy.RequireClaim("title", "Admin"));

    opts.AddPolicy(PolicyConstants.MustBeAVeterantEmployee,
        policy => policy.RequireClaim("employeeId", "Emp001", "Emp002", "Emp003"));

    // If no policies apply atleast user needs to be authentication allways
    opts.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
            ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(
                    builder.Configuration.GetValue<string>("Authentication:SecretKey")!
                )
            )
        };
    });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
    