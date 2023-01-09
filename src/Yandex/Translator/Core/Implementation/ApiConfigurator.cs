namespace Yandex.Translator;

internal sealed class ApiConfigurator : IApiConfigurator
{
  public IApiConfigurator ApiKey(string key)
  {
    ApiKeyValue = key;

    return this;
  }

  public string ApiKeyValue { get; private set; }
}