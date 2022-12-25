using Catharsis.Commons;

namespace Yandex.Translator;

/// <summary>
///   <para>Set of extension methods for interface <see cref="IApi"/>.</para>
/// </summary>
/// <seealso cref="IApi"/>
public static class IApiExtensions
{
  /// <summary>
  ///   <para>Makes a request to Yandex.Translator web service to return collection of supported languages pairs (translations directions).</para>
  /// </summary>
  /// <param name="api">Translator instance to be used.</param>
  /// <param name="pairs">Collection of supported language pairs (directions).</param>
  /// <param name="cancellation"></param>
  /// <returns><c>true</c> if request was successfull and <paramref name="pairs"/> output parameter contains supported language pairs, or <c>false</c> if request failed and <paramref name="pairs"/> is a <c>null</c> reference.</returns>
  /// <seealso cref="http://api.yandex.ru/translate/doc/dg/reference/getLangs.xml"/>
  public static bool Pairs(this IApi api, out IEnumerable<ITranslationPair>? pairs, CancellationToken cancellation = default)
  {
    try
    {
      pairs = api.Pairs(cancellation).ToList(cancellation).Result;

      return true;
    }
    catch
    {
      pairs = null;

      return false;
    }
  }

  /// <summary>
  ///   <para>Makes a language detection request to Yandex.Translator web service.</para>
  /// </summary>
  /// <param name="api">Translator instance to be used.</param>
  /// <param name="language">Language of the provided text fragment.</param>
  /// <param name="text">Text fragment which language is to be detected.</param>
  /// <param name="cancellation"></param>
  /// <returns><c>true</c> if language was successfully determined and <paramref name="language"/> output parameter contains its ISO code, or <c>false</c> if detection failed and <paramref name="language"/> is a <c>null</c> reference.</returns>
  /// <seealso cref="http://api.yandex.ru/translate/doc/dg/reference/detect.xml"/>
  public static bool Detect(this IApi api, out string? language, string text, CancellationToken cancellation = default)
  {
    try
    {
      language = api.Detect(text, cancellation).Result;

      return true;
    }
    catch
    {
      language = null;

      return false;
    }
  }

  /// <summary>
  ///   <para>Makes a translation request to Yandex.Translator web service.</para>
  /// </summary>
  /// <param name="api">Translator instance to be used.</param>
  /// <param name="action">Delegate that specifies text for translation, source/target languages and addional options.</param>
  /// <param name="cancellation"></param>
  /// <returns><see cref="ITranslation"/> instance that represents result of text's translation.</returns>
  /// <exception cref="TranslatorException">If error occurs during the processing of web request, or if given text cannot be translated by web service.</exception>
  /// <seealso cref="Translate(IYandexTranslator, string, string, string)"/>
  /// <seealso cref="http://api.yandex.ru/translate/doc/dg/reference/translate.xml"/>
  public static Task<ITranslation> Translate(this IApi api, Action<ITranslationApiRequest> action, CancellationToken cancellation = default)
  {
    var request = new TranslationApiRequest();

    action(request);

    return api.Translate(request, cancellation);
  }

  /// <summary>
  ///   <para>Makes a translation request to Yandex.Translator web service.</para>
  /// </summary>
  /// <param name="api">Translator instance to be used.</param>
  /// <param name="action">Delegate that specifies text for translation, source/target languages and addional options.</param>
  /// <param name="translation"><see cref="ITranslation"/> instance that represents result of text's translation.</param>
  /// <returns><c>true</c> if translation was successful and <paramref name="translation"/> output parameter contains translated text, or <c>false</c> if translation failed and <paramref name="translation"/> is a <c>null</c> reference.</returns>
  /// <seealso cref="http://api.yandex.ru/translate/doc/dg/reference/translate.xml"/>
  public static bool Translate(this IApi api, Action<ITranslationApiRequest> action, out ITranslation? translation, CancellationToken cancellation = default)
  {
    try
    {
      translation = api.Translate(action, cancellation).Result;

      return true;
    }
    catch
    {
      translation = null;

      return false;
    }
  }
}