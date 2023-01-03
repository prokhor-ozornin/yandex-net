using System.Runtime.Serialization;
using Catharsis.Commons;

namespace Yandex.Translator;

/// <summary>
///   <para>Represents a result of call to Yandex.Translator service's operation of retrieving supported translations pairs.</para>
/// </summary>
public sealed class TranslationPairsResult
{
  /// <summary>
  ///   <para>Collection of supported translations pairs (directions).</para>
  /// </summary>
  public IEnumerable<string> Pairs { get; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="pairs"></param>
  public TranslationPairsResult(IEnumerable<string> pairs) => Pairs = pairs;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="info"></param>
  public TranslationPairsResult(Info info) => Pairs = info.Pairs ?? new List<string>();

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="info"></param>
  public TranslationPairsResult(object info) : this(new Info().SetState(info)) {}

  /// <summary>
  ///   <para></para>
  /// </summary>
  [DataContract(Name = "Langs")]
  public sealed record Info : IResultable<TranslationPairsResult>
  {
    /// <summary>
    ///   <para>Collection of supported translations pairs (directions).</para>
    /// </summary>
    [DataMember(Name = "dirs", IsRequired = true)]
    public List<string> Pairs { get; init; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public TranslationPairsResult ToResult() => new(this);
  }
}