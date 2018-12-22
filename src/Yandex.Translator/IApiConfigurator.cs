using System;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Configurator of basic parameters that are used when making requests to Yandex.Translator web service.</para>
  /// </summary>
  public interface IApiConfigurator
  {
    /// <summary>
    ///   <para>Specifies data exchange format to use.</para>
    /// </summary>
    /// <param name="format">Data format.</param>
    /// <returns>Back reference to the current configurator instance.</returns>
    IApiConfigurator Format(ApiDataFormat format);

    /// <summary>
    ///   <para>Specifies API key to use.</para>
    /// </summary>
    /// <param name="key">API key.</param>
    /// <returns>Back reference to the current configurator instance.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="key"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="key"/> is <see cref="string.Empty"/> string.</exception>
    IApiConfigurator ApiKey(string key);
  }
}