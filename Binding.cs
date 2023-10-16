using Microsoft.AspNetCore.Mvc.ModelBinding;

/// <summary>
/// Our custom attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
public class FromMiddlewareAttribute : Attribute, IBindingSourceMetadata, IModelNameProvider {
  public BindingSource? BindingSource => new(
    "Middleware",
    "BindingSource_Middleware",
    isGreedy: true,
    isFromRequest: false
);

  public string? Name => null;
}

/// <summary>
/// The value provider factor which will extract the value from context and pass it
/// into the value provider.
/// </summary>
public class MiddlewareValueProviderFactory : IValueProviderFactory {
  public Task CreateValueProviderAsync(
    ValueProviderFactoryContext context
) {
    Console.WriteLine(context.ActionContext.HttpContext.Items["profile"]);

    if (context.ActionContext.HttpContext.Items["profile"] is not Profile profile) {
      return Task.CompletedTask;
    }

    context.ValueProviders.Add(new MiddlewareValueProvider(profile));

    return Task.CompletedTask;
  }
}

/// <summary>
/// The value provider only provides single string values based on the keys of
/// the entity like "profile.Id", "profile.Name" so we need to extract the values
/// from the entity instance we are holding on to.  To support multiple parameters,
/// we need to make use of the prefix and cache the entities by the prefix.
/// </summary>
public class MiddlewareValueProvider : IValueProvider {
  private readonly Profile _profile;

  public MiddlewareValueProvider(Profile profile) {
    _profile = profile;
  }

  /// <summary>
  /// The prefix is simply the name of the parameter in the action like "profile".
  /// This is useful if we want to support multiple entities?
  /// </summary>
  public bool ContainsPrefix(string prefix) {
    Console.WriteLine($" Prefix = {prefix}");
    return true;
  }

  /// <summary>
  /// This gets called one time for each key like "profile.Id", "profile.Name".
  /// </summary>
  public ValueProviderResult GetValue(string key) {
    Console.WriteLine($" GetValue = {key}");

    var value = "";

    // TODO: Write a better extractor here since this is quite naive
    if (key.EndsWith("Id")) {
      value = _profile.Id;
    } else if (key.EndsWith("Name")) {
      value = _profile.Name;
    }

    return new ValueProviderResult(value);
  }
}
