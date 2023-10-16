using Microsoft.AspNetCore.Mvc.Filters;

/// <summary>
/// A custom authorization filter which will check if the profile is valid.
/// </summary>
public class AuthFilter : IAuthorizationFilter {
  public void OnAuthorization(
    AuthorizationFilterContext context
  ) {
    // Imagine that we make an EF/Redis/cache lookup for the profile.
    var profile = new Profile {
      Id = "1234",
      Name = "Charizard"
    };

    // Still need to put it into context since there's no obvious way to
    // pass it to our value provider otherwise.
    context.HttpContext.Items["profile"] = profile;

    Console.WriteLine("Auth Filtered! Injected Charizard");
  }
}
