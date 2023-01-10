using Catharsis.Extensions;

namespace Yandex.Translator;

/// <summary>
///   <para>Set of extension methods for interface <see cref="IApi"/>.</para>
/// </summary>
/// <seealso cref="IApi"/>
public static class IApiExtensions
{
  /// <summary>
  ///   <para>Makes a translation request to Yandex.Translator web service.</para>
  /// </summary>
  /// <param name="api">Translator instance to be used.</param>
  /// <param name="action">Delegate that specifies text for translation, source/target languages and addional options.</param>
  /// <param name="cancellation"></param>
  /// <returns><see cref="ITranslation"/> instance that represents result of text's translation.</returns>
  /// <exception cref="TranslatorException">If error occurs during the processing of web request, or if given text cannot be translated by web service.</exception>
  /// <seealso cref="http://api.yandex.ru/translate/doc/dg/reference/translate.xml"/>
  public static Task<ITranslation> TranslateAsync(this IApi api, Action<ITranslationApiRequest> action, CancellationToken cancellation = default)
  {
    if (api is null) throw new ArgumentNullException(nameof(api));
    if (action is null) throw new ArgumentNullException(nameof(action));

    var request = new TranslationApiRequest();

    action(request);

    return api.TranslateAsync(request, cancellation);
  }
  
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="api"></param>
  /// <returns></returns>
  public static IEnumerable<ITranslationPair> Pairs(this IApi api) => api is not null ? api.PairsAsync().ToListAsync().Result : throw new ArgumentNullException(nameof(api));

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="api"></param>
  /// <param name="text"></param>
  /// <returns></returns>
  public static string Detect(this IApi api, string text)
  {
    if (api is null) throw new ArgumentNullException(nameof(api));
    if (text is null) throw new ArgumentNullException(nameof(text));
    if (text.IsEmpty()) throw new ArgumentException(nameof(text));

    return api.DetectAsync(text).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="api"></param>
  /// <param name="request"></param>
  /// <returns></returns>
  public static ITranslation Translate(this IApi api, ITranslationApiRequest request)
  {
    if (api is null) throw new ArgumentNullException(nameof(api));
    if (request is null) throw new ArgumentNullException(nameof(request));

    return api.TranslateAsync(request).Result;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="api"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static ITranslation Translate(this IApi api, Action<ITranslationApiRequest> action) => api.TranslateAsync(action).Result;
}