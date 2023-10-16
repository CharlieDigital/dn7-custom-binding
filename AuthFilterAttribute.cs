using Microsoft.AspNetCore.Mvc;

/// <summary>
/// The custom TypeFilterAttribute to load our auth filter.
/// </summary>
public class AuthFilterAttribute : TypeFilterAttribute {
  public AuthFilterAttribute() : base(typeof(AuthFilter)) { }
}
