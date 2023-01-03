namespace Yandex.Translator;

/// <summary>
///   <para></para>
/// </summary>
public interface IApi : IDisposable
{
  /// <summary>
  ///   <para>Makes a request to Yandex.Translator web service to return collection of supported languages pairs (translations directions).</para>
  /// </summary>
  /// <param name="cancellation"></param>
  /// <returns>Collection of supported language pairs (directions).</returns>
  /// <seealso cref="http://api.yandex.ru/translate/doc/dg/reference/getLangs.xml"/>
  IAsyncEnumerable<ITranslationPair> PairsAsync(CancellationToken cancellation = default);

  /// <summary>
  ///   <para>Makes a language detection request to Yandex.Translator web service.</para>
  /// </summary>
  /// <param name="text">Text fragment which language is to be detected.</param>
  /// <param name="cancellation"></param>
  /// <returns>Language of the provided text fragment.</returns>
  /// <exception cref="TranslatorException">If error occurs during the processing of web request, or if language of given text fragment cannot be reliably determined.</exception>
  /// <seealso cref="http://api.yandex.ru/translate/doc/dg/reference/detect.xml"/>
  Task<string> DetectAsync(string text, CancellationToken cancellation = default);

  /// <summary>
  ///   <para>Makes a translation request to Yandex.Translator web service.</para>
  /// </summary>
  /// <param name="request">Parameters of request.</param>
  /// <param name="cancellation"></param>
  /// <returns><see cref="ITranslation"/> instance that represents result of text's translation.</returns>
  /// <exception cref="TranslatorException">If error occurs during the processing of web request, or if given text cannot be translated by web service.</exception>
  /// <seealso cref="http://api.yandex.ru/translate/doc/dg/reference/translate.xml"/>
  Task<ITranslation> TranslateAsync(ITranslationApiRequest request, CancellationToken cancellation = default);
}