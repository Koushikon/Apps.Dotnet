using Microsoft.AspNetCore.Mvc;

namespace SwaggerApi.Controllers;

/***
 * Use Xml Comment or Documentation Comment to descript Endpoints
 * Add three slashes / this will generate comment style now work further.
 * This show us the info about calling method
 * 
 * To add Project wise we need to add inside *.csproj
 * <GenerateDocumentationFile>true</GenerateDocumentationFile>
 * Then, Add xml doc attach code in the Progra.cs
 */

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    // GET: api/Users
    /// <summary>
    /// Gets a list of all users in the system.
    /// </summary>
    /// <remarks>
    /// Sample Request: GET /users
    /// Sample Response:
    /// [
    ///     {
    ///         "id": 1,
    ///         "name": "Boby deol"
    ///     },
    ///     {
    ///         "id": 2,
    ///         "name": "Jimmy Harp"
    ///     }
    /// ]
    /// </remarks>
    /// <returns>A list of users.</returns>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/Users/5
    /// <summary>
    /// Gets only one user data based on Id
    /// </summary>
    /// <param name="id">User Id</param>
    /// <returns>A single user data</returns>
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/Users
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/Users/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/Users/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
