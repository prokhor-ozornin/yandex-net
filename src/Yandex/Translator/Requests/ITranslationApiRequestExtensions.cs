using System.Globalization;

namespace Yandex.Translator;

/// <summary>
///   <para>Set of extension methods for interface <see cref="ITranslationApiRequest"/>.</para>
/// </summary>
/// <seealso cref="ITranslationApiRequest"/>
public static class ITranslationApiRequestExtensions
{
  /// <summary>
  ///   <para>Specifies that translatable text fragment is in HTML format.</para>
  /// </summary>
  /// <param name="request">Translation request instance.</param>
  /// <returns>Back reference to the provided translation <paramref name="request"/>.</returns>
  /// <seealso cref="ITranslationApiRequest.Format(string)"/>
  public static ITranslationApiRequest AsHtml(this ITranslationApiRequest request) => request.Format("html");

  /// <summary>
  ///   <para>Specifies that translatable text fragment is in plain text format.</para>
  /// </summary>
  /// <param name="request">Translation request instance.</param>
  /// <returns>Back reference to the provided translation <paramref name="request"/>.</returns>
  /// <seealso cref="ITranslationApiRequest.Format(string)"/>
  public static ITranslationApiRequest AsText(this ITranslationApiRequest request) => request.Format("plain");

  /// <summary>
  ///   <para>Specifies source language from which a text fragment should be translated.</para>
  /// </summary>
  /// <param name="request">Translation request instance.</param>
  /// <param name="culture">Culture that contains a language to be used.</param>
  /// <returns>Back reference to the provided translation <paramref name="request"/>.</returns>
  /// <seealso cref="ITranslationApiRequest.From(string)"/>
  public static ITranslationApiRequest From(this ITranslationApiRequest request, CultureInfo culture) => request.From(culture?.TwoLetterISOLanguageName);

  /// <summary>
  ///   <para>Specifies target language to which a text fragment should be translated.</para>
  /// </summary>
  /// <param name="request">Translation request instance.</param>
  /// <param name="culture">Culture that contains a language to be used.</param>
  /// <returns>Back reference to the provided translation <paramref name="request"/>.</returns>
  /// <seealso cref="ITranslationApiRequest.To(string)"/>
  public static ITranslationApiRequest To(this ITranslationApiRequest request, CultureInfo culture) => request.To(culture?.TwoLetterISOLanguageName);
}