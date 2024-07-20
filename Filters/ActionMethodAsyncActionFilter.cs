using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreFilterPipeline.Filters;

public class ActionMethodAsyncActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("Before: Executing async action method filter");
        await next();
        Console.WriteLine("After: Executing async action method filter");
    }
}
