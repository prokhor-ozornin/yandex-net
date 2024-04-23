using System.Runtime.Serialization;

namespace Yandex.Detector;

/// <summary>
///   <para></para>
/// </summary>
[DataContract(Name = "yandex-mobile-info-error")]
public sealed record Error : IError
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  [DataMember(Name = "Text", IsRequired = true)]
  public string Text { get; init; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  public Error()
  {
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="text"></param>
  public Error(string text) => Text = text;

  /// <summary>
  ///   <para>Returns a <see cref="string"/> that represents the current <see cref="Error"/> instance.</para>
  /// </summary>
  /// <returns>A string that represents the current <see cref="Error"/>.</returns>
  public override string ToString() => Text ?? string.Empty;
}