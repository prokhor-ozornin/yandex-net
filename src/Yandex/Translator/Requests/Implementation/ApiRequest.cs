namespace Yandex.Translator;

internal abstract class ApiRequest : IApiRequest
{
  private readonly Dictionary<string, object> parameters = new();

  public IReadOnlyDictionary<string, object> Parameters => parameters;

  public IApiRequest WithParameter(string name, object value)
  {
    parameters[name] = value;
    return this;
  }
}