namespace Yandex.Translator;

/// <summary>
///   <para>Represents a translation of a text fragment from one language to another.</para>
/// </summary>
public interface ITranslation : IEquatable<ITranslation>
{
  /// <summary>
  ///   <para>Source language of the translation.</para>
  /// </summary>
  string FromLanguage { get; }

  /// <summary>
  ///   <para>Target language of the translation.</para>
  /// </summary>
  string ToLanguage { get; }

  /// <summary>
  ///   <para>Translated text fragment in target language.</para>
  /// </summary>
  string Text { get; }
}