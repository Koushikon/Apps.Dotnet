using Microsoft.AspNetCore.Mvc;
using Web.Attributes;

namespace Web.Controllers;

/***
 * Now, After implementing the cache for the result content the users consequtive requests will get the cached result.
 */

[CacheResource]
public class CachedController : Controller
{
    public IActionResult Index()
    {
        return Content("This content was generated at " + DateTime.Now);
    }
}