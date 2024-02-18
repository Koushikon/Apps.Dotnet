using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filters;

public class CacheResourceFilter : IResourceFilter
{
    private static readonly Dictionary<string, object?> _cache = new();
    private string _cacheKey = string.Empty;

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        _cacheKey = context.HttpContext.Request.Path.ToString();

        if (!_cache.TryGetValue(_cacheKey, out object? value))
            return;
        
        string? cachedContent = value as string;
        if (cachedContent != null)
        {
            context.Result = new ContentResult { Content = cachedContent };
        }        
    }


    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        if (!string.IsNullOrWhiteSpace(_cacheKey) && !_cache.ContainsKey(_cacheKey))
        {
            var result = context.Result as ContentResult;

            if (result == null)
                return;

            _cache.Add(_cacheKey, result.Content);
        }
    }
}