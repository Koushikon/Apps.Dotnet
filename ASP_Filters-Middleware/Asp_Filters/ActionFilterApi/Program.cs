using ActionFilterApi.Filters;
using ActionFilterApi.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


/***
 * If we want to use our filter globally, we need to register it inside the AddControllers() method:
 */
builder.Services.AddControllers(opts =>
{
    opts.Filters.Add(typeof(GlobalActionFilterExample));
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/***
 * But if we want to use our filter as a service type on the Action or Controller level,
 * we need to register it in the same ConfigureServices method but as a service in the IoC container:
 */
builder.Services.AddScoped<ActionFilterExample>();
builder.Services.AddScoped<AsyncActionFilterExample>();
builder.Services.AddScoped<ControllerActionFilterExample>();


// Adding a Action Filter to validate the parameter model data
builder.Services.AddScoped<ValidateModelFilter>();

// Validating movie data not found in database used as a Filter
builder.Services.AddScoped<ValidateEntityExistsFilter<Movie>>();


/***
 * Before we test this validation filter, we have to suppress validation from the [ApiController] attribute.
 * If we don’t do it, it will overtake the validation from our action filter and always return 400 (BadRequest) for all validation errors.'
 * But as you’ve seen, if our model is invalid, we want to return the UnprocessableEntity result (422).
 */
builder.Services.Configure<ApiBehaviorOptions>(opts =>
{
    opts.SuppressModelStateInvalidFilter = true;
});

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
