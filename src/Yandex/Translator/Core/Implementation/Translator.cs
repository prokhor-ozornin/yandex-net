namespace Yandex.Translator;

internal sealed class Translator : ITranslator
{
  public IApi Configure(IApiConfigurator configurator) => configurator is not null ? new Api(configurator.ApiKeyValue) : throw new ArgumentNullException(nameof(configurator));
}