using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreFilterPipeline.Filters;

public class ControllerActionFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context) => Console.WriteLine($"After: Executing controller action filter");

    public void OnActionExecuting(ActionExecutingContext context) => Console.WriteLine($"Before: Executing controller action filter");
}