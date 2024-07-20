using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreFilterPipeline.Filters;

public class ControllerAsyncActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine($"Before: Executing controller async action filter");
        await next();
        Console.WriteLine($"After: Executing controller async action filter");
    }
}