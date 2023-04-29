using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIMonitor.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private ILogger<UsersController> _logger { get; }

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    // GET: api/Users
    [HttpGet]
    public IEnumerable<string> Get()
    {
        //throw new Exception("something bad happend here.");
        return new string[] { "value1", "value2" };
    }

    // GET api/Users/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        if(id < 0 || id > 100)
        {
            // Best not to use $ String interpolation inside log meesage
            _logger.LogWarning("The index is out of Range {Id} was invalid", id);
            return BadRequest("The index is out of Range.");
        }

        // Not gonna show because of APIMonitor.Controllers Log level
        _logger.LogInformation("The value api/Users/{Id} is Perfect.", id);
        return Ok($"Value {id}");
    }

    // GET api/Users/guess/5
    [HttpGet("guess/{id}")]
    public IActionResult GetGuess(int id)
    {
        try
        {
            if (id < 0 || id > 100)
            {
               throw new ArgumentOutOfRangeException(nameof(id));
            }

            // Not gonna show because of APIMonitor.Controllers Log level
            _logger.LogInformation("The value api/Users/{Id} is Perfect.", id);
            return Ok($"Value {id}");
        }
        catch (Exception ex)
        {
            // Best not to use $ String interpolation inside log meesage
            _logger.LogError(ex, "The index is out of Range {Id} was invalid", id);
            return BadRequest("The index is out of Range.");
        }
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
