using System;
using Catharsis.Commons;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Entry point to access Yandex.Translator web service.</para>
  /// </summary>
  /// <seealso cref="http://api.yandex.ru/translate"/>
  public static class Yandex
  {
    /// <summary>
    ///   <para>Configures instance of client translator to be used for making requests to Yandex.Translator web service.</para>
    /// </summary>
    /// <param name="configurator">Delegate that generic translator's parameters and options to use.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="configurator"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="configurator"/> is <see cref="string.Empty"/> string.</exception>
    public static IYandexTranslator Translator(Action<IApiConfigurator> configurator)
    {
      Assertion.NotNull(configurator);

      var apiConfigurator = new ApiConfigurator();
      configurator(apiConfigurator);

      if (apiConfigurator.GetApiKey().IsEmpty())
      {
        throw new InvalidOperationException("API Key was not specified when configuring Yandex Translator");
      }

      return new YandexTranslator(apiConfigurator.GetFormat(), apiConfigurator.GetApiKey());
    }
  }
}