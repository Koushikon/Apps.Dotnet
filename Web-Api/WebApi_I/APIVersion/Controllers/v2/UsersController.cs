using Microsoft.AspNetCore.Mvc;

namespace APIVersion.Controllers.v2;

// api/v2/Users
// We could supports Both multiple version inside Same Controller file
// By adding another [ApiVersion("x.x")] attribute with different version
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("2.0")]
public class UsersController : ControllerBase
{
    // GET: api/v2/Users
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "Version 2", "value 2" };
    }

    // GET api/v2/Users/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/v2/Users
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/v2/Users/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/v2/Users/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
