using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIVersion.Controllers.v2;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("2.0")]
public class EmployeesController : ControllerBase
{
    // GET api/v2/Employees/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return $"Employees #{id * 5}";
    }
}
