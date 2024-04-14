using System.Text.Json.Serialization;
using WebApi_II.Middlewares;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

/***
 * The UseWhen method takes two parameters. The first parameter is a delegate of type Func<T,bool>,
 * which returns a Boolean value indicating whether the middleware should be executed. If the delegate returns true,
 * the middleware will be applied to the request, otherwise, it will be skipped.
 * 
 * To apply middleware that starts with request path /api
 */
app.UseWhen(context =>
    context.Request.Path.StartsWithSegments("/api"),
    builder => builder.UseMiddleware<ExceptionMiddleware>()
);


var sampleTodos = new Todo[] {
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
};

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos);
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
