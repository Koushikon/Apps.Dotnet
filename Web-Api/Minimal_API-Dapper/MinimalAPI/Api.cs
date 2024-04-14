using DataAccess.Data;
namespace MinimalAPI;

public static class Api
{
    public static void ConfigureApi(this WebApplication app)
    {
        // All of API End point mappings
        app.MapGet(pattern: "/Users", GetUsers);        // Get all Data
        app.MapGet(pattern: "/Users/{id}", GetUser);    // Get One Data
        app.MapPost(pattern: "/Users", InsertUser);     // For Insert
        app.MapPut(pattern: "/Users", UpdateUser);      // For Update
        app.MapDelete(pattern: "/Users", DeleteUser);   // Delete Data
    }

    private static async Task<IResult> GetUsers(IUserData data)
    {
        try
        {
            return Results.Ok(await data.GetUsers());
        }
        catch(Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    private static async Task<IResult> GetUser(int id, IUserData data)
    {
        try
        {
            var results = await data.GetUser(id);
            if (results == null) return Results.NotFound();
            return Results.Ok(results);
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public static async Task<IResult> InsertUser(UserModel user, IUserData data)
    {
        try
        {
            await data.InsertUser(user);
            return Results.Ok();
        }
        catch(Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public static async Task<IResult> UpdateUser(UserModel user, IUserData data)
    {
        try
        {
            await data.UpdateUser(user);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }

    public static async Task<IResult> DeleteUser(int id, IUserData data)
    {
        try
        {
            await data.DeleteUser(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}
