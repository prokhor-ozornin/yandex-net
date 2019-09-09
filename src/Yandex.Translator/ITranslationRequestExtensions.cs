namespace Yandex.Translator
{
  using System;
  using System.Globalization;
  using Catharsis.Commons;

  /// <summary>
  ///   <para>Set of extension methods for interface <see cref="ITranslationRequest"/>.</para>
  /// </summary>
  /// <seealso cref="ITranslationRequest"/>
  public static class ITranslationRequestExtensions
  {
    /// <summary>
    ///   <para>Specifies source language from which a text fragment should be translated.</para>
    /// </summary>
    /// <param name="request">Translation request instance.</param>
    /// <param name="culture">Culture that contains a language to be used.</param>
    /// <returns>Back reference to the provided translation <paramref name="request"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="request"/> or <paramref name="culture"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="ITranslationRequest.From(string)"/>
    public static ITranslationRequest From(this ITranslationRequest request, CultureInfo culture)
    {
      Assertion.NotNull(request);
      Assertion.NotNull(culture);

      return request.From(culture.TwoLetterISOLanguageName);
    }

    /// <summary>
    ///   <para>Specifies that translatable text fragment is in HTML format.</para>
    /// </summary>
    /// <param name="request">Translation request instance.</param>
    /// <returns>Back reference to the provided translation <paramref name="request"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="request"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="ITranslationRequest.Format(string)"/>
    public static ITranslationRequest Html(this ITranslationRequest request)
    {
      Assertion.NotNull(request);

      return request.Format("html");
    }

    /// <summary>
    ///   <para>Specifies that translatable text fragment is in plain text format.</para>
    /// </summary>
    /// <param name="request">Translation request instance.</param>
    /// <returns>Back reference to the provided translation <paramref name="request"/>.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="request"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="ITranslationRequest.Format(string)"/>
    public static ITranslationRequest PlainText(this ITranslationRequest request)
    {
      Assertion.NotNull(request);

      return request.Format("plain");
    }

    /// <summary>
    ///   <para>Specifies target language to which a text fragment should be translated.</para>
    /// </summary>
    /// <param name="request">Translation request instance.</param>
    /// <param name="culture">Culture that contains a language to be used.</param>
    /// <returns>Back reference to the provided translation <paramref name="request"/>.</returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="request"/> or <paramref name="culture"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="ITranslationRequest.To(string)"/>
    public static ITranslationRequest To(this ITranslationRequest request, CultureInfo culture)
    {
      Assertion.NotNull(request);
      Assertion.NotNull(culture);

      return request.To(culture.TwoLetterISOLanguageName);
    }
  }
}