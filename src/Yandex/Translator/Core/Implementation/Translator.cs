namespace Yandex.Translator;

internal sealed class Translator : ITranslator
{
  public IApi Configure(IApiConfigurator configurator) => new Api(configurator.ApiKeyValue);
}