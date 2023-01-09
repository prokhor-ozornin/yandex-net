namespace Yandex.Translator;

/// <summary>
///   <para>Configurator of basic parameters that are used when making requests to Yandex.Translator web service.</para>
/// </summary>
public interface IApiConfigurator
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  string ApiKeyValue { get; }

  /// <summary>
  ///   <para>Specifies API key to use.</para>
  /// </summary>
  /// <param name="key">API key.</param>
  /// <returns>Back reference to the current configurator instance.</returns>
  IApiConfigurator ApiKey(string key);
}