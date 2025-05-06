using Yandex.Translator;

namespace Yandex.Tests;

public class IntegrationTest : Test
{
  protected IApi Api { get; } = null; //Yandex.Api.Translator().Configure(configurator => configurator.ApiKey(ConfigurationManager.AppSettings["ApiKey"]));

  public override void Dispose()
  {
    base.Dispose();
    Api.Dispose();
  }
}