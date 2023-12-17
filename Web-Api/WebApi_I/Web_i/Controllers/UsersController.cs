using Microsoft.AspNetCore.Mvc;

namespace Web_i.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    /**
     * GET, POST, PUT, PATCH, DELETE
     */

    // GET the whole data
    // GET: api/Users
    [HttpGet]
    public IEnumerable<string> Get()
    {
        List<string> result = new();
        for (int i = 0; i < Random.Shared.Next(2, 10); i++)
        {
            result.Add($"Value #{i + 2}");
        }
        return result;
    }

    // GET the part of a data based on id or something
    // GET api/Users/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return $"Value #{id * 2}";
    }

    // POST create a new record
    // POST api/Users
    [HttpPost]
    public void Post([FromBody] string value)
    {

    }

    // PUT updates a whole record (or possible creates)
    // PUT api/Users/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // PATCH updates a part of a record
    // PATCH api/Users/5
    [HttpPatch("{id}/{up}")]
    public void Patch(int id, string up, [FromBody] string email)
    {

    }

    // DELETE deleted a record
    // DELETE api/Users/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
