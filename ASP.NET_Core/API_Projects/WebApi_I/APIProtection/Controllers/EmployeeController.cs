using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIProtection.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    // GET: api/Employee
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { Random.Shared.Next(1, 101).ToString() };
    }

    // GET api/Employee/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return $"Random Number: {Random.Shared.Next(1, 101)} for Id {id}";
    }
}
