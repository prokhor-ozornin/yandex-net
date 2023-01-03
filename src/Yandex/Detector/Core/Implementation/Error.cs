using System.Runtime.Serialization;
using Catharsis.Commons;

namespace Yandex.Detector;

/// <summary>
///   <para></para>
/// </summary>
public sealed class Error : IError
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
  ///   <para>Determines whether two <see cref="Error"/> instances are equal.</para>
  /// </summary>
  /// <param name="other">The instance to compare with the current one.</param>
  /// <returns><c>true</c> if specified instance is equal to the current, <c>false</c> otherwise.</returns>
  public bool Equals(IError other) => this.Equality(other, error => error.Text);

  /// <summary>
  ///   <para>Determines whether the specified <see cref="object"/> is equal to the current <see cref="object"/>.</para>
  /// </summary>
  /// <param name="other">The object to compare with the current object.</param>
  /// <returns><c>true</c> if the specified object is equal to the current object, <c>false</c>.</returns>
  public override bool Equals(object other) => Equals(other as Error);

  /// <summary>
  ///   <para>Returns hash code for the current object.</para>
  /// </summary>
  /// <returns>Hash code of current instance.</returns>
  public override int GetHashCode() => this.HashCode(nameof(Text));

  /// <summary>
  ///   <para>Returns a <see cref="string"/> that represents the current <see cref="Error"/> instance.</para>
  /// </summary>
  /// <returns>A string that represents the current <see cref="Error"/>.</returns>
  public override string ToString() => Text;

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
    public IError Result() => new Error(this);
  }
}