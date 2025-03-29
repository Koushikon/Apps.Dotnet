using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi.Database;
using UserApi.Models;

namespace UserApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UsersDatabase _database;

    public UsersController(UsersDatabase database)
    {
        _database = database;
    }

    [HttpPost, Authorize]
    public IActionResult CreateUser(UserModel user)
    {
        _database.Add(user);

        return new ObjectResult(user)
        {
            StatusCode = StatusCodes.Status201Created
        };
    }


    [HttpGet, Authorize]
    public IActionResult GetUsers()
    {
        return Ok(_database);
    }


    [HttpGet("{id}"), Authorize]
    public IActionResult GetUserById(int id)
    {
        var user = _database.FirstOrDefault(u => u.Id == id);

        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }
}