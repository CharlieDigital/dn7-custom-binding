# Intro

Do you have middleware in your .NET Web API that is performing some auth check or other filtering that loads an entity from the database/cache/Redis?

Wouldn't it be nice if you could just pass that entity down into your action without having to reload it or explicitly grabbing it from `HttpContext`?

Have you ever wanted to do this in your code?

```cs
[ApiController]
public class Controller : ControllerBase {
  [AuthFilter]
  [HttpGet("/")]
  public async Task Index(
    [FromMiddleware] Profile profile
  ) {
    // Note how profile is injected from the filter.
  }
```

This project shows how it's done!

## Resources

- https://www.davidkaya.com/p/custom-from-attribute-for-controller-actions-in-asp-net-core
- https://github.com/dotnet/aspnetcore/blob/main/src/Mvc/Mvc.Core/src/ModelBinding/FormFileValueProvider.cs