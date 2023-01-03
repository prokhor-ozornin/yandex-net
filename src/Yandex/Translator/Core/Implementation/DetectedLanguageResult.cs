using System.Runtime.Serialization;
using Catharsis.Commons;

namespace Yandex.Translator;

/// <summary>
///   <para>Represents a result of call to Yandex.Translator service's operation of language detection.</para>
/// </summary>
public sealed class DetectedLanguageResult
{
  /// <summary>
  ///   <para>HTTP result status code.</para>
  /// </summary>
  public int Code { get; }

  /// <summary>
  ///   <para>Detected text's language.</para>
  /// </summary>
  public string Language { get; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="code"></param>
  /// <param name="language"></param>
  public DetectedLanguageResult(int code, string language)
  {
    Code = code;
    Language = language;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="info"></param>
  public DetectedLanguageResult(Info info)
  {
    Code = info.Code ?? 0;
    Language = info.Language ?? string.Empty;
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="info"></param>
  public DetectedLanguageResult(object info) : this(new Info().SetState(info)) {}

  /// <summary>
  ///   <para></para>
  /// </summary>
  [DataContract(Name = "DetectedLang")]
  public sealed record Info : IResultable<DetectedLanguageResult>
  {
    /// <summary>
    ///   <para>HTTP result status code.</para>
    /// </summary>
    [DataMember(Name = "code", IsRequired = true)]
    public int? Code { get; init; }

    /// <summary>
    ///   <para>Detected text's language.</para>
    /// </summary>
    [DataMember(Name = "lang", IsRequired = true)]
    public string Language { get; init; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public DetectedLanguageResult Result() => new(this);
  }
}