using Microsoft.AspNetCore.Mvc;
using WebApi.CustomFilters;

namespace WebApi.CustomAttributes;

public class ApiKeyAttribute : ServiceFilterAttribute
{
    public ApiKeyAttribute() : base(typeof(ApiKeyAuthFilter))
    {
    }
}