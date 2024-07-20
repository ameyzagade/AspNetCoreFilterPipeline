using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreFilterPipeline.Filters;

public class ActionMethodActionFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context) => Console.WriteLine("After: Executing action method filter");

    public void OnActionExecuting(ActionExecutingContext context) => Console.WriteLine("Before: Executing action method filter");
}