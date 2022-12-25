using System.Runtime.Serialization;
using Catharsis.Commons;

namespace Yandex.Translator;

/// <summary>
///   <para>Represents a result of call to Yandex.Translator service's operation of retrieving supported translations pairs.</para>
/// </summary>
public sealed class TranslationResult
{
  /// <summary>
  ///   <para>HTTP result status code.</para>
  /// </summary>
  public int Code { get; }

  /// <summary>
  ///   <para>Languages pair (for example "en-ru"), representing text's translation direction.</para>
  /// </summary>
  public string Language { get; }

  /// <summary>
  ///   <para>Collection of translated text fragments.</para>
  /// </summary>
  public IEnumerable<string> Lines { get; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="code"></param>
  /// <param name="language"></param>
  /// <param name="lines"></param>
  public TranslationResult(int code, string language, IEnumerable<string> lines)
  {
    Code = code;
    Language = language;
    Lines = lines;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="info"></param>
  public TranslationResult(Info info)
  {
    Code = info.Code ?? 0;
    Language = info.Language ?? string.Empty;
    Lines = info.Lines ?? new List<string>();
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="info"></param>
  public TranslationResult(object info) : this(new Info().Properties(info)) {}

  /// <summary>
  ///   <para>Returns a <see cref="string"/> that represents the current <see cref="TranslationResult"/> instance.</para>
  /// </summary>
  /// <returns>A string that represents the current <see cref="TranslationResult"/>.</returns>
  public override string ToString() => Lines.Join(string.Empty);

  /// <summary>
  ///   <para></para>
  /// </summary>
  [DataContract(Name = "Translation")]
  public sealed record Info : IResultable<TranslationResult>
  {
    /// <summary>
    ///   <para>HTTP result status code.</para>
    /// </summary>
    [DataMember(Name = "code", IsRequired = true)]
    public int? Code { get; init; }

    /// <summary>
    ///   <para>Languages pair (for example "en-ru"), representing text's translation direction.</para>
    /// </summary>
    [DataMember(Name = "lang", IsRequired = true)]
    public string? Language { get; init; }

    /// <summary>
    ///   <para>Collection of translated text fragments.</para>
    /// </summary>
    [DataMember(Name = "text", IsRequired = true)]
    public List<string>? Lines { get; init; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public TranslationResult Result() => new(this);
  }
}