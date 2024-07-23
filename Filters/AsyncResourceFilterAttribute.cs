using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreFilterPipeline.Filters;

public class AsyncResourceFilterAttribute(string contextName) : Attribute, IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        Console.WriteLine($"Before: Executing local resource filter Context: {contextName}");
        await next();
        Console.WriteLine($"After: Executing local resource filter Context: {contextName}");
    }
}

public class GlobalResourceFilter : IResourceFilter
{
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        Console.WriteLine($"After: Executing global resource filter");
    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        Console.WriteLine($"Before: Executing global resource filter");
    }
}
