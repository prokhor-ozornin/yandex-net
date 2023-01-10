using Catharsis.Extensions;

namespace Yandex.Translator;

internal abstract class ApiRequest : IApiRequest
{
  private readonly Dictionary<string, object> parameters = new();

  public IReadOnlyDictionary<string, object> Parameters => parameters;

  public IApiRequest WithParameter(string name, object value)
  {
    if (name is null) throw new ArgumentNullException(nameof(name));
    if (name.IsEmpty()) throw new ArgumentException(nameof(name));

    parameters[name] = value;

    return this;
  }
}