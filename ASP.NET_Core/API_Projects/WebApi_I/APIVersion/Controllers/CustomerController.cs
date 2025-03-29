using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIVersion.Controllers;

/**
 * * Best Standard is Put the different version of Controller inside different version folder
 */

// api/<V1/V2>/Customer
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
[ApiVersion("1.0", Deprecated = true)] // To support single version specify one [ApiVersion("x.x")] attribute for Controller
[ApiVersion("2.0")] // This Controller supports Both version 1.0 and 2.0
public class CustomerController : ControllerBase
{
    // GET: api/<V1/V2>/Customer
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "Customer", "700" };
    }

    // GET: api/v1/Customer/5
    [HttpGet("{id}")]
    [MapToApiVersion("1.0")] // This specifies this endpoint suppport the v1
    public string Getv1(int id)
    {
        return "From Version 1 Get by Id";
    }

    // GET: api/v1/Customer/5
    [HttpGet("{id}")]
    [MapToApiVersion("2.0")] // This specifies this endpoint suppport the v2
    public string Getv2(int id)
    {
        return "From Version 2 Get by Id";
    }
}
