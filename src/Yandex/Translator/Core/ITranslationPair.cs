namespace Yandex.Translator;

/// <summary>
///   <para>Represents a pair of souce and target languages (translation direction).</para>
/// </summary>
public interface ITranslationPair : IEquatable<ITranslationPair>
{
  /// <summary>
  ///   <para>Source translation language.</para>
  /// </summary>
  string FromLanguage { get; }

  /// <summary>
  ///   <para>Target translation language.</para>
  /// </summary>
  string ToLanguage { get; }
}