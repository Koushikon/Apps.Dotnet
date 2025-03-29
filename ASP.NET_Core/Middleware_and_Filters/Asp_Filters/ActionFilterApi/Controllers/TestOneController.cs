using ActionFilterApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ActionFilterApi.Controllers;

/***
 * In Filter we can change the order of the execution using Order parameter:
 */

[ServiceFilter(typeof(ControllerActionFilterExample), Order = 2)]
[Route("api/TestOne")]
[ApiController]
public class TestOneController : ControllerBase
{
    [ServiceFilter(typeof(ActionFilterExample), Order = 3)]
    [ServiceFilter(typeof(AsyncActionFilterExample), Order = 1)]
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return ["value 1", "value 2"];
    }
}