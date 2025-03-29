using APIProtection.Models;
using Microsoft.AspNetCore.Mvc;


namespace APIProtection.Controllers;

/**
 * When we need more control over caching
 * We can use Deris Caching
 * 
 * When we are Updating the data POST we shouldn't cache for that
 * When we are asking for the data frequently we can cache that
 */


// We can use caching at Controller level

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    // GET: api/Users
    [HttpGet]
    // Add caching to this Endpoint, duration is 30sec or 60sec is enough
    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
    public IEnumerable<string> Get()
    {
        return new string[] { Random.Shared.Next(1, 101).ToString() };
    }

    // GET api/Users/5
    [HttpGet("{id}")]
    // We can cache per Id andg duration for 1day is "60 * 60 * 24"
    [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, NoStore = false)]
    public string Get(int id)
    {
        return $"Random Number: { Random.Shared.Next(1, 101) } for Id { id }";
    }

    // POST api/Users
    [HttpPost]
    public IActionResult Post([FromBody] Users users)
    {
        if (ModelState.IsValid) // Check all the validation inside our Model and return wheather it is valid or not
        {
            return Ok($"Model is valid. {users.Id}");
        }

        return BadRequest(ModelState);
    }

    // PUT api/Users/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
        Console.WriteLine($"PUT Endpoint {id}, {value}");
    }

    // DELETE api/Users/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        Console.WriteLine($"PUT Endpoint {id}");
    }
}
