using Library.DataAccess;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api_I.Endpoints;

public static class TodosEndpoints
{
    public static void AddTodosEndpoint(this WebApplication app)
    {
        /***
         * Now, To authorize endpoint we can use "[Authorize]" before hander function defination
         * Or, we could use an Extension method "RequireAuthorization()" after Map Route Handler
         */

        app.MapGet("/api/Todos", GetAllTodos);
        app.MapGet("/api/Todos/{id}", GetTodo);
        app.MapPost("/api/Todos", CreateTodo).AllowAnonymous();
        app.MapPut("/api/Todos/{id}", UpdateTodo).RequireAuthorization();

        app.MapPut("/api/Todos/{id}/Complete", async (ITodoService db, int id) =>
        {
            await db.CompleteTodo(id, 2);

            return Results.Ok();
        }).RequireAuthorization();

        app.MapDelete("/api/Todos/{id}", async (ITodoService db, int id) =>
        {
            await db.DeleteTodo(id, 2);

            return Results.Ok();
        }).RequireAuthorization();

        /***
         * For Endpoints we can call function as handler like GetAllTodos, GetTodo etc.
         * Or, We can define the handler right on the endpoint section like for Complete, MapDelete.
         */
    }

    // We can allows anyone to use this endpoint with [AllowAnonymous]
    [AllowAnonymous]
    private async static Task<IResult> GetAllTodos(ITodoService db)
    {
        var output = await db.GetTodos(new Todo { AssignTo = 2 });
        return Results.Ok(output);
    }

    // This means this endpoint needs authorization for access [Authorize]
    [Authorize]
    private async static Task<IResult> GetTodo(ITodoService db, int id)
    {
        var output = await db.GetSingleTodo(new Todo { Id = id });
        return Results.Ok(output);
    }

    private async static Task<IResult> CreateTodo(ITodoService db, [FromBody] string task)
    {
        var output = await db.CreateTodo(2, task);
        return Results.Ok(output);
    }

    private async static Task<IResult> UpdateTodo(ITodoService db, int id, [FromBody] string task)
    {
        await db.UpdateTodo(id, 2, task);
        return Results.Ok();
    }
}