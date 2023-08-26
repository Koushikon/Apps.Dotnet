using Microsoft.AspNetCore.Mvc;

namespace APIVersion.Controllers.v1;

// api/v1/Users
// We could supports Both multiple version inside Same Controller file
// By adding another [ApiVersion("x.x")] attribute with different version
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0", Deprecated = true)]
public class UsersController : ControllerBase
{
    // GET: api/v1/Users
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "Version 1", "value 1" };
    }
}
