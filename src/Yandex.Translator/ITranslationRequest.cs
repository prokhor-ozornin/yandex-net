namespace Yandex.Translator
{
  using System;

  /// <summary>
  ///   <para>Representation of a text translation request to Yandex.Translator web service.</para>
  /// </summary>
  public interface ITranslationRequest : IRequest
  {
    /// <summary>
    ///   <para>Format of translatable text to use.</para>
    /// </summary>
    /// <param name="format">Text format.</param>
    /// <returns>Back reference to the current translation request.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="format"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="format"/> is <see cref="string.Empty"/> string.</exception>
    ITranslationRequest Format(string format);

    /// <summary>
    ///   <para>Source language from which a text fragment should be translated.</para>
    /// </summary>
    /// <param name="language">Source language.</param>
    /// <returns>Back reference to the current translation request.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="language"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="language"/> is <see cref="string.Empty"/> string.</exception>
    ITranslationRequest From(string language);

    /// <summary>
    ///   <para>Text in souce language to be translated.</para>
    /// </summary>
    /// <param name="text">Text fragment.</param>
    /// <returns>Back reference to the current translation request.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="text"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="text"/> is <see cref="string.Empty"/> string.</exception>
    ITranslationRequest Text(string text);

    /// <summary>
    ///   <para>Target language to which a text fragment should be translated.</para>
    /// </summary>
    /// <param name="language">Target language.</param>
    /// <returns>Back reference to the current translation request.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="language"/> is a <c>null</c> reference.</exception>
    /// <exception cref="ArgumentException">If <paramref name="language"/> is <see cref="string.Empty"/> string.</exception>
    ITranslationRequest To(string language);
  }
}