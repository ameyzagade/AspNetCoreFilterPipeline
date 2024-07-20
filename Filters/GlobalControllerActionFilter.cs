using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreFilterPipeline.Filters;

public class GlobalControllerActionFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context) => Console.WriteLine("After: Executing global action filter");

    public void OnActionExecuting(ActionExecutingContext context) => Console.WriteLine("Before: Executing global action filter");
}