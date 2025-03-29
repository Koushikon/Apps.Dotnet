using ActionFilterApi.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace ActionFilterApi.Controllers;

[ServiceFilter(typeof(ControllerActionFilterExample))]
[Route("api/Test")]
[ApiController]
public class TestController : ControllerBase
{
    [ServiceFilter(typeof(ActionFilterExample))]
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return ["value 1", "value 2"];
    }

    [ServiceFilter(typeof(AsyncActionFilterExample))]
    [HttpGet("GetAsync")]
    public IEnumerable<string> GetAsync()
    {
        return ["value 1", "value 2"];
    }
}