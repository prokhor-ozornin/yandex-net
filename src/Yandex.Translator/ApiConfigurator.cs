namespace Yandex.Translator
{
  using Catharsis.Commons;

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
      return apiKey;
    }

    public ApiDataFormat GetFormat()
    {
      return format;
    }
  }
}