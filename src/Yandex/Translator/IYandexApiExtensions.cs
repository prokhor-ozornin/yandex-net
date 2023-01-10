namespace Yandex.Translator;

/// <summary>
///   <para>Entry point to access Yandex.Translator web service.</para>
/// </summary>
/// <seealso cref="http://api.yandex.ru/translate"/>
public static class IYandexApiExtensions
{
  /// <summary>
  ///   <para>Configures instance of client translator to be used for making requests to Yandex.Translator web service.</para>
  /// </summary>
  /// <param name="api"></param>
  /// <returns></returns>
  public static ITranslator Translator(this IYandexApi api) => api is not null ? new Translator() : throw new ArgumentNullException(nameof(api));
}