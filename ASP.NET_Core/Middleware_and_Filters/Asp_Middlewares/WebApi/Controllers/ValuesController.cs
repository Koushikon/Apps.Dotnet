using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/Values")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return ["values 1", "values 2"];
    }
}