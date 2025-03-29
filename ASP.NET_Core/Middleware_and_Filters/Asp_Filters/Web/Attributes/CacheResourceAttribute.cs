using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Attributes;

/***
 * Creating a class which is used as TypeFilterAttribute that will be used with CacheResourceFilter inside a Controller or Action
 */

public class CacheResourceAttribute : TypeFilterAttribute
{
    public CacheResourceAttribute() :
        base(typeof(CacheResourceFilter))
    { }
}