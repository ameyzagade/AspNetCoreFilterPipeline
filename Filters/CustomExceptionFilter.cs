using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreFilterPipeline.Filters;

public class CustomExceptionFilter(string contextName) : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        Console.WriteLine($"Exception caught in action {contextName}");
        context.ExceptionHandled = true;
        context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

        return Task.CompletedTask;
    }
}
