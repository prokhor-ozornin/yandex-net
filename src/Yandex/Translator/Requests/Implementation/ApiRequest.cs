namespace Yandex.Translator;

internal abstract class ApiRequest : IApiRequest
{
  public IDictionary<string, object?> Parameters { get; } = new Dictionary<string, object?>();
}