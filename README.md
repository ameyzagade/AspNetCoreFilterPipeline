## Filter pipeline overview
The ASP.NET Core MVC Action (or filter) pipeline runs after the end of middleware pipeline.
It provides hooks to inspect/modify/short-circuit the filter pipeline.


#### Request response cycle
        Outgoing response        Incoming request
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
- ActionFilterAttribute

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


### ActionFilterAttribute
- Deriving from this attribute exposes:
- 2 sync methods and 1 async method to hook into action filter: OnActionExecuting, OnActionExecuted, OnActionExecutionAsync
- 2 sync methods and 1 async method to hook into result filter: OnResultExecuted, OnResultExecuting, OnResultExecutionAsync
- Either override sync methods or async methods, but not both.
- The implementing class can be used as an attribute.


### Scope of usage
- __Global__: 
  By configuring options while registering AddControllers()
  Global filters run for all controllers and their action methods by default
- __On Controller__:
  Filters applied at controller level run for all action methods by default
- __On specific action methods__


### Filter interface implementation usage
- __Using ServiceFilter__
  [ServiceFilter(type: typeof(T), Order = N)]
  Type resolved from DI container, should be registered with DI container
- __Using TypeFilter__
  [TypeFilter(type: typeof(T), Order = N)]
  Type resolved?
- __Inherit Attribute class__ 
  Inherit attribute class in the implementation type and use it as an attribute
  Type resolved?
- __Pass as an option registering controllers with DI container__
  service.AddControllers(o => o.Filters.Add<T>(order: N));
  Type resolved from DI container, should be registered with DI container


### Order of execution
- The default order stays for different variety of filters unless overridden explicitly.
- If multiple filters are added at global level or at controller/action method level, 
  they run in the order in which they are defined.
- Order can be overridden by explicity assigning an order value.