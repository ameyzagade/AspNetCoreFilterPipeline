using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreFilterPipeline.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class LoggingAsyncActionFilterAttribute(string contextName) : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine($"Before: Executing logging async action filter from context: {contextName}");
        await next();
        Console.WriteLine($"After: Executing logging async action filter from context: {contextName}");
    }
}
