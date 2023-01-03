using Catharsis.Commons;

namespace Yandex.Detector;

/// <summary>
///   <para></para>
/// </summary>
public sealed class Resolution : IResolution
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  public short Height { get; init; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  public short Width { get; init; }

  /// <summary>
  ///   <para>Determines whether two <see cref="Resolution"/> instances are equal.</para>
  /// </summary>
  /// <param name="other">The instance to compare with the current one.</param>
  /// <returns><c>true</c> if specified instance is equal to the current, <c>false</c> otherwise.</returns>
  public bool Equals(IResolution other) => this.Equality(other, nameof(Height), nameof(Width));

  /// <summary>
  ///   <para>Determines whether the specified <see cref="object"/> is equal to the current <see cref="object"/>.</para>
  /// </summary>
  /// <param name="other">The object to compare with the current object.</param>
  /// <returns><c>true</c> if the specified object is equal to the current object, <c>false</c>.</returns>
  public override bool Equals(object other) => Equals(other as Resolution);

  /// <summary>
  ///   <para>Returns hash code for the current object.</para>
  /// </summary>
  /// <returns>Hash code of current instance.</returns>
  public override int GetHashCode() => this.HashCode(nameof(Height), nameof(Width));

  /// <summary>
  ///   <para>Returns a <see cref="string"/> that represents the current <see cref="Resolution"/> instance.</para>
  /// </summary>
  /// <returns>A string that represents the current <see cref="Resolution"/>.</returns>
  public override string ToString() => $"{Width}x{Height}";
}