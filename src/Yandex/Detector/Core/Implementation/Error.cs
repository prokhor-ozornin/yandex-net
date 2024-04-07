using System.Runtime.Serialization;
using Catharsis.Extensions;

namespace Yandex.Detector;

/// <summary>
///   <para></para>
/// </summary>
public sealed record Error : IError
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public string Text { get; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  public Error(string text) => Text = text;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="info"></param>
  public Error(Info info) => Text = info.Text ?? string.Empty;

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="info"></param>
  public Error(object info) : this(new Info().SetState(info))
  {
  }

  /// <summary>
  ///   <para>Returns a <see cref="string"/> that represents the current <see cref="Error"/> instance.</para>
  /// </summary>
  /// <returns>A string that represents the current <see cref="Error"/>.</returns>
  public override string ToString() => Text ?? string.Empty;

  /// <summary>
  ///   <para></para>
  /// </summary>
  [DataContract(Name = "yandex-mobile-info-error")]
  public sealed record Info : IResultable<IError>
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    [DataMember(Name = "Text", IsRequired = true)]
    public string Text { get; init; }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <returns></returns>
    public IError ToResult() => new Error(this);
  }
}