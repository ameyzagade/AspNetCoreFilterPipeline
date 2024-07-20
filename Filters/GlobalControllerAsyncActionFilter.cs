using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreFilterPipeline.Filters;

public class GlobalControllerAsyncActionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("Before: Executing global async action filter");
        await next();
        Console.WriteLine("After: Executing global async action filter");
    }
}