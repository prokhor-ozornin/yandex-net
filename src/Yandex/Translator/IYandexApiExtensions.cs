namespace Yandex.Translator;

/// <summary>
///   <para>Entry point to access Yandex.Translator web service.</para>
/// </summary>
/// <seealso href="http://api.yandex.ru/translate"/>
public static class IYandexApiExtensions
{
  /// <param name="api"></param>
  extension(IYandexApi api)
  {
    /// <summary>
    ///   <para>Configures instance of client translator to be used for making requests to Yandex.Translator web service.</para>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="api"/> is <see langword="null"/>.</exception>
    public ITranslator Translator() => api is not null ? new Translator() : throw new ArgumentNullException(nameof(api));
  }
}