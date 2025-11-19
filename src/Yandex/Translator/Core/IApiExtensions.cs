using Catharsis.Extensions;

namespace Yandex.Translator;

/// <summary>
///   <para>A set of extension methods for the <see cref="IApi"/> interface.</para>
/// </summary>
/// <seealso cref="IApi"/>
public static class IApiExtensions
{
  /// <param name="api">Translator instance to be used.</param>
  extension(IApi api)
  {
    /// <summary>
    ///   <para>Makes a translation request to Yandex.Translator web service.</para>
    /// </summary>
    /// <param name="action">Delegate that specifies text for translation, source/target languages and addional options.</param>
    /// <param name="cancellation"></param>
    /// <returns><see cref="ITranslation"/> instance that represents result of text's translation.</returns>
    /// <exception cref="TranslatorException">If error occurs during the processing of web request, or if given text cannot be translated by web service.</exception>
    /// <exception cref="ArgumentNullException">If either <paramref name="api"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    /// <seealso href="http://api.yandex.ru/translate/doc/dg/reference/translate.xml"/>
    public Task<ITranslation> TranslateAsync(Action<ITranslationApiRequest> action, CancellationToken cancellation = default)
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
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="api"/> is <see langword="null"/>.</exception>
    public IEnumerable<ITranslationPair> Pairs() => api is not null ? api.PairsAsync().ToListAsync().Result : throw new ArgumentNullException(nameof(api));

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="api"/> or <paramref name="text"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">If <paramref name="text"/> is invalid string.</exception>
    public string Detect(string text)
    {
      if (api is null) throw new ArgumentNullException(nameof(api));
      if (text is null) throw new ArgumentNullException(nameof(text));
      if (text.IsEmpty()) throw new ArgumentException(nameof(text));

      return api.DetectAsync(text).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="api"/> or <paramref name="request"/> is <see langword="null"/>.</exception>
    public ITranslation Translate(ITranslationApiRequest request)
    {
      if (api is null) throw new ArgumentNullException(nameof(api));
      if (request is null) throw new ArgumentNullException(nameof(request));

      return api.TranslateAsync(request).Result;
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="api"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public ITranslation Translate(Action<ITranslationApiRequest> action) => api.TranslateAsync(action).Result;
  }
}