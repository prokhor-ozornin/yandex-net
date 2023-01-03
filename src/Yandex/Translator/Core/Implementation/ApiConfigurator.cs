namespace Yandex.Translator;

internal sealed class ApiConfigurator : IApiConfigurator
{
  public IApiConfigurator ApiKey(string apiKey)
  {
    ApiKeyValue = apiKey;

    return this;
  }

  public string ApiKeyValue { get; private set; }
}