using Catharsis.Commons;

namespace Yandex.Translator
{
  internal sealed class ApiConfigurator : IApiConfigurator
  {
    private ApiDataFormat format = ApiDataFormat.Json;
    private string apiKey;

    public IApiConfigurator Format(ApiDataFormat format)
    {
      this.format = format;
      return this;
    }

    public IApiConfigurator ApiKey(string key)
    {
      Assertion.NotEmpty(key);

      this.apiKey = key;
      return this;
    }

    public string GetApiKey()
    {
      return this.apiKey;
    }

    public ApiDataFormat GetFormat()
    {
      return this.format;
    }
  }
}