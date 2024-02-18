using Microsoft.AspNetCore.Mvc;
using Web.Filters;

namespace Web.Attributes;

public class AddHeaderAttribute : TypeFilterAttribute
{
    public AddHeaderAttribute()
        : base(typeof(AddHeaderFilter))
    { }
}