using System.Globalization;

namespace Yandex.Translator;

/// <summary>
///   <para>A set of extension methods for the <see cref="ITranslationApiRequest"/> interface.</para>
/// </summary>
/// <seealso cref="ITranslationApiRequest"/>
public static class ITranslationApiRequestExtensions
{
  /// <param name="request">Translation request instance.</param>
  extension(ITranslationApiRequest request)
  {
    /// <summary>
    ///   <para>Specifies that translatable text fragment is in HTML format.</para>
    /// </summary>
    /// <returns>Back reference to the provided translation <paramref name="request"/>.</returns>
    /// <seealso cref="ITranslationApiRequest.Format(string)"/>
    public ITranslationApiRequest AsHtml() => request is not null ? request.Format("html") : throw new ArgumentNullException(nameof(request));

    /// <summary>
    ///   <para>Specifies that translatable text fragment is in plain text format.</para>
    /// </summary>
    /// <returns>Back reference to the provided translation <paramref name="request"/>.</returns>
    /// <seealso cref="ITranslationApiRequest.Format(string)"/>
    public ITranslationApiRequest AsText() => request is not null ? request.Format("plain") : throw new ArgumentNullException(nameof(request));

    /// <summary>
    ///   <para>Specifies source language from which a text fragment should be translated.</para>
    /// </summary>
    /// <param name="culture">Culture that contains a language to be used.</param>
    /// <returns>Back reference to the provided translation <paramref name="request"/>.</returns>
    /// <seealso cref="ITranslationApiRequest.From(string)"/>
    public ITranslationApiRequest From(CultureInfo culture) => request is not null ? request.From(culture?.TwoLetterISOLanguageName) : throw new ArgumentNullException(nameof(request));

    /// <summary>
    ///   <para>Specifies target language to which a text fragment should be translated.</para>
    /// </summary>
    /// <param name="culture">Culture that contains a language to be used.</param>
    /// <returns>Back reference to the provided translation <paramref name="request"/>.</returns>
    /// <seealso cref="ITranslationApiRequest.To(string)"/>
    public ITranslationApiRequest To(CultureInfo culture) => request is not null ? request.To(culture?.TwoLetterISOLanguageName) : throw new ArgumentNullException(nameof(request));
  }
}