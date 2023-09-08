using Microsoft.AspNetCore.Mvc;

namespace ReadQueryString.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }


    // GET: ~/Home/GetScalarValues?FirstName=Koushik&LastName=Saha
    // Explain: GetQueryStringsAsScalarValues
    public IActionResult GetScalarValues([FromQuery] string firstName, [FromQuery] string lastName)
    {
        return Ok(new { FullName = $"{firstName} {lastName}" });
    }


    // GET: ~/Home/GetArrayValues?ids=1&ids=2&ids=3
    // Explain: GetMultipleQueryStringsAsArray
    public IActionResult GetArrayValues([FromQuery] int[] ids)
    {
        return Ok(new { ProductIds = ids });
    }


    // GET: ~/Home/GetAsObject?FirstName=Sourav&LastName=Das
    // Explain: GetMultipleQueryStringsAsObject
    public IActionResult GetAsObject([FromQuery] Person queryStringParameters)
    {
        return Ok(new { FullName = $"{queryStringParameters.FirstName} {queryStringParameters.LastName}" });
    }
}

public record Person(string FirstName, string LastName);