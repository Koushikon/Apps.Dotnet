using Microsoft.EntityFrameworkCore;
using WebApi.Contracts;
using WebApi.Data;
using WebApi.Data.Repositories;
using WebApi.Data.Repositories.Abstraction;
using WebApi.Services;
using WebApi.Services.Abstraction;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrdersDbContext>(opts =>
{
    opts.UseInMemoryDatabase("Orders");
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


// Api Endpoints
app.MapGet("/orders", async (IOrderService service) =>
{
    var orders = await service.GetAllAsync();
    return Results.Ok(orders);
}).WithOpenApi();

app.MapGet("/orders/{id:guid}", async (Guid id, IOrderService service) =>
{
    var order = await service.GetByIdAsync(id);
    return Results.Ok(order);
}).WithOpenApi();

app.MapPost("/orders", async (OrderForCreateDto dto, IOrderService service) =>
{
    var order = await service.CreateAsync(dto);
    return Results.Created($"/orders/{order.Id}", order);
}).WithOpenApi();

app.MapPut("/orders/{id:guid}", async (Guid id, OrderForUpdateDto dto, IOrderService service) =>
{
    await service.UpdateAsync(id, dto);
    return Results.NoContent();
}).WithOpenApi();

app.MapDelete("/orders/{id:guid}", async (Guid id, IOrderService service) =>
{
    await service.DeleteAsync(id);
    return Results.NoContent();
}).WithOpenApi();


app.Run();
