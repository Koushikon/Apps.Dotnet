using APISecurity.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APISecurity.Controllers;

[Route("api/[controller]")]
[ApiController]
// With globaly authorization we don't need to add [Authorize] attribute to every single Controller, Because authorization default now for everybody
// [Authorize]
public class UsersController : ControllerBase
{
    // secrets.json have greater prioruty than the appsettings.json
    // Run time secrets.json connection string get overwritten inside appsettings.json
    private readonly IConfiguration _config;

    public UsersController(IConfiguration config)
    {
        _config = config;
    }

    // GET: api/Users
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET: api/Users/5
    [HttpGet("{id}")]
    [Authorize(Policy = PolicyConstants.MustHaveEmployeeId)]
    [Authorize(Policy = PolicyConstants.MustBeOwner)]
    public string Get(int id)
    {
        return _config.GetConnectionString("Default")!;
    }

    // GET: api/Users/order/5
    [HttpGet("order/{id}")]
    [Authorize(Policy = PolicyConstants.MustHaveEmployeeId)]
    [Authorize(Policy = PolicyConstants.MustBeAVeterantEmployee)]
    public string GetOrder(int id)
    {
        return "From the Order #1001";
    }

    // POST: api/Users
    [HttpPost]
    [Authorize(Policy = PolicyConstants.MustBeAdmin)]
    public void Post([FromBody] string value)
    {
    }

    // PUT: api/Users/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // PATCH: api/Users/5/7
    [HttpPatch("{id}/{oid}")]
    public void Patch(int id, string oid, [FromBody] string email)
    {
    }

    // DELETE: api/Users/5
    [HttpDelete("{id}")]
    // Add [Authorize] attribute to add authorization to a particular route
    // [Authorize]
    public void Delete(int id)
    {
    }
}
