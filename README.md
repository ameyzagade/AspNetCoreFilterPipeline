### Filter pipeline overview
The ASP.NET Core MVC Action (or filter) pipeline runs before appropriate action method is to be executed at end of middleware pipeline.

It provides hooks into the request response cycle to inspect/modify/short-circuit.

##### Request response cycle
        Outgoing request   Incoming request
                ||              ||
                ||      Endpoint middleware 
                ||              || 
                ||    Authorisation Filter(s) 
                ||              || 
                Resource        Resource 
                Filter(s)       Filter(s)
                AFTER          BEFORE
                ||              |
                ||      **Model Binding**
                ||              ||
                ||          Action Filter(s)    <==>    **Action Method execution**
                ||              ||
                ||      Exception Filter(s)
                ||              ||
                ========Result Filter(s)    <==>    **Result Execution**
                        