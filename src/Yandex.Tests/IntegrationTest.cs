using System.Configuration;
using Catharsis.Commons;
using Yandex.Translator;

namespace Yandex.Tests;

public class IntegrationTest<T> : ClassTest<T>
{
  protected IApi Api { get; } = Yandex.Api.Translator().Configure(configurator => configurator.ApiKey(ConfigurationManager.AppSettings["ApiKey"]));

  public override void Dispose()
  {
    base.Dispose();
    Api.Dispose();
  }
}

public class IntegrationTest : IntegrationTest<object>
{
}