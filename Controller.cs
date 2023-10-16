using Microsoft.AspNetCore.Mvc;

[ApiController]
public class Controller : ControllerBase {
  [AuthFilter]
  [HttpGet("/")]
  public async Task Index(
    [FromMiddleware] Profile profile
  ) {
    Console.WriteLine("Index Action");
    Console.WriteLine(profile == null);

    if (profile != null) {
      Console.WriteLine($"  ID = {profile.Id}");
      Console.WriteLine($"  Name = {profile.Name}");
    }

    await Task.CompletedTask;
  }
}
