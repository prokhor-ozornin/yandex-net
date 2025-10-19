using Yandex.Translator;

namespace Yandex.Tests;

/// <summary>
///   <para></para>
/// </summary>
public class IntegrationTest : Test
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  protected IApi Api { get; } = null; //Yandex.Api.Translator().Configure(configurator => configurator.ApiKey(ConfigurationManager.AppSettings["ApiKey"]));

  /// <summary>
  ///   <para></para>
  /// </summary>
  public override void Dispose()
  {
    base.Dispose();
    Api.Dispose();
  }
}