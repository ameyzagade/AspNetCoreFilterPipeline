## Filter pipeline overview
- ASP.NET Core Action Invocation or filter pipeline
- Runs after the end of middleware pipeline once action method is selected
- It provides granular approach and hooks to inspect/modify/short-circuit the requests
- It helps make action method logic be highly cohesive


### Filter pipeline flow
  Outgoing response        Incoming request
          ||                      ||
          ||              Endpoint middleware 
          ||                      || 
          ||             Authorisation Filter(s)
          ||                      || 
      Resource                Resource 
      Filter(s)               Filter(s)
      AFTER                    BEFORE
          ||                      ||
          ||               **Model Binding**
          ||              **Model validation**
          ||                      ||
          ||                Action Filter(s)    <==>    **Action Method execution**
          ||                      ||
          ||              Exception Filter(s)
          ||                      ||
          ==============    Result Filter(s)    <==>    **Result Execution**


### Use cases
1. __Authorisation Filter__:
- Define custom auth scheme

2. __Resource Filter__:
- Generic validations
- Return cached value

3. __Action Filter__:
- Data validations on the incoming data after model binding and validations

4. __Exception Filter__:
- Catching exceptions in model binding and validations, action methods, action filters, controller or razor page
  instance creation

5. __Result Filter__:
- Executes result produced by action method or action filter


### Scope
1. __Global__: 
  - Configure options while registering services.AddControllers() with the DI container
  - Runs for all controllers and their action methods by default

2. __On Controller__:
  - Runs for all action methods by default

3. __On specific action methods__
  - Runs for the specific action methods where applied by default


### Order of execution
1. The order of execution is defined for different types of filters

2. For a given type, the order if execution is global scope -> controller scope -> action method scope

3. If multiple filters of same type are defined at a scope,  
  they run in the order defined unless overidden by order argument value

4. Order can be overridden by assigning an order argument value
  Lower integer value -> execution prioritised
  Higher integer value -> execution deprioritised

5. Implement IOrderedFilter interface to add order property to a custom filter implementation
  and a value to override order of execution


### Filter interface implementation usage
1. __ServiceFilter attribute__
  - [ServiceFilter(type: typeof(T), Order = N)]
  - Type T will be resolved from the DI container and thus should be registered with DI container
  - Type T's dependencies will be resolved from the DI container as well.

2. __TypeFilter attribute__
  - [TypeFilter(type: typeof(T), Order = N)]
  - Can pass arguments to the type's ctor with this usage
  - Type instantiated by Activator Utilities
  - Type's dependencies resolved from the DI container

3. __Inherit Attribute class__ 
  - Inherit attribute class in the implementation type and use it as an attribute
  - Type instantiated by Activator Utilities

4. __Pass as an option while registering controllers with the DI container__
  - service.AddControllers(o => o.Filters.Add<T>(order: N));
    Type T instantiated by Activator Utilities
    Type T's dependencies resolved from the DI container

  - service.AddControllers(o => o.Filters.AddService<T>(order: N));
    Type T resolved from DI container and thus should be registered with DI container
    Type T's dependencies resolved from the DI container


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


#### ActionFilterAttribute
- Deriving from this attribute exposes:
- 2 sync methods and 1 async method to hook into action filter: OnActionExecuting, OnActionExecuted, OnActionExecutionAsync
- 2 sync methods and 1 async method to hook into result filter: OnResultExecuted, OnResultExecuting, OnResultExecutionAsync
- Either override sync methods or async methods, but not both.
- The implementing class can be used as an attribute.


#### Exception Filter
- Catches exceptions in controllers or razor page creation, action methods, model binding, action filters.
  Does not catch exceptions in resource filter, result filter, result execution.
- Not as flexible as global error handling middleware. Use only when action method response would be different
  e.g., view action method -> HTML, api action method -> json
- Set exception handled property to true or write response to stop exception propagation.


#### Result Filter
- __IResultFilterAsync or IAsyncResultFilterAsync__
  Only executed when action result is produced by action filter or action method.

- __Use IAlwaysRunResultFilter or IAsyncAlwaysRunResultFilter__ 
  Executed when action result is produced by action method or action filter or auth filter 
  or resource filter or exception filter