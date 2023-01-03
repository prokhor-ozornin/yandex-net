namespace Yandex.Translator;

/// <summary>
///   <para>Representation of a text translation request to Yandex.Translator web service.</para>
/// </summary>
public interface ITranslationApiRequest : IApiRequest
{
  /// <summary>
  ///   <para>Format of translatable text to use.</para>
  /// </summary>
  /// <param name="format">Text format.</param>
  /// <returns>Back reference to the current translation request.</returns>
  ITranslationApiRequest Format(string format);

  /// <summary>
  ///   <para>Source language from which a text fragment should be translated.</para>
  /// </summary>
  /// <param name="language">Source language.</param>
  /// <returns>Back reference to the current translation request.</returns>
  ITranslationApiRequest From(string language);

  /// <summary>
  ///   <para>Target language to which a text fragment should be translated.</para>
  /// </summary>
  /// <param name="language">Target language.</param>
  /// <returns>Back reference to the current translation request.</returns>
  ITranslationApiRequest To(string language);

  /// <summary>
  ///   <para>Text in source language to be translated.</para>
  /// </summary>
  /// <param name="text">Text fragment.</param>
  /// <returns>Back reference to the current translation request.</returns>
  ITranslationApiRequest Text(string text);
}