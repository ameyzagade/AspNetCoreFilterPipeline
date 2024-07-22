using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreFilterPipeline.Filters;

public class SecondLoggingAsyncActionFilterAttribute(string contextName) : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine($"Before: Executing logging 2 async action filter from context: {contextName}");
        await next();
        Console.WriteLine($"After: Executing logging 2 async action filter from context: {contextName}");
    }

    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        Console.WriteLine($"Before: Executing logging 2 async result filter from context: {contextName}");
        await next();
        Console.WriteLine($"After: Executing logging 2 async result filter from context: {contextName}");
    }

    // Alternatively, sync versions of above methods can be implemented too
    // public override void OnActionExecuting(ActionExecutingContext context)
    // {
    //     base.OnActionExecuting(context);
    // }

    // public override void OnActionExecuted(ActionExecutedContext context)
    // {
    //     base.OnActionExecuted(context);
    // }

    // public override void OnResultExecuted(ResultExecutedContext context)
    // {
    //     base.OnResultExecuted(context);
    // }

    // public override void OnResultExecuting(ResultExecutingContext context)
    // {
    //     base.OnResultExecuting(context);
    // }
}