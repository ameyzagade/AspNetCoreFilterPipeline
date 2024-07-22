## Filter pipeline overview
The ASP.NET Core MVC Action (or filter) pipeline runs after the end of middleware pipeline.
It provides hooks to inspect/modify/short-circuit the filter pipeline.


#### Request response cycle
        Outgoing request        Incoming request
                ||                      ||
                ||              Endpoint middleware 
                ||                      || 
                ||             Authorisation Filter(s) 
                ||                      || 
            Resource                Resource 
            Filter(s)               Filter(s)
            AFTER                    BEFORE
                ||                      |
                ||              **Model Binding**
                ||                      ||
                ||                Action Filter(s)    <==>    **Action Method execution**
                ||                      ||
                ||              Exception Filter(s)
                ||                      ||
                ==============    Result Filter(s)    <==>    **Result Execution**


### Available Interfaces
1. Authorisation Filter
- IAuthorizationFilter
- IAsyncAuthorizationFilter

2. Resource Filter
- IResourceFilter
- IAsyncResourceFilter

3. Action Filter
- IActionFilter
- IAsyncActionFilter

4. Exception Filter
- IExceptionFilter
- IAsyncExceptionFilter

5. Result Filter
- IResultFilter
- IAsyncResultFilter
- IAlwaysRunResultFilter
- IAsyncAlwaysRunResultFilter

6. Page Filter (In case of Razor pages)
- IPageFilter
- IAsyncPageFilter

7. Ordered Filter
- IOrderedFilter


### Filter interface implementation usage
- [ServiceFilter(type: typeof(T), Order = N)]
- [TypeFilter(type: typeof(T), Order = N)]
- Inherit Attribute class in the implementation type and use it as an attribute
- Pass as options while registering controllers with DI container
  service.AddControllers(o => o.Filters.Add<T>(order: N));


### Scope of usage
- Can be used globally by configuring options while registering AddControllers()
  Global filters run for all controllers and their action methods by default
- Can be used on controller or specific action methods
  Filters applied at controller level run for all action methods by default


### Order of execution
- The default order stays for different variety of filters unless overridden 
- If multiple filters are added at global level or at controller/action method level, 
  they run in the order in which they are defined
- Order by be overridden by explicity assigning an order value