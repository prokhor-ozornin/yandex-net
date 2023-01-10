namespace Yandex.Detector;

/// <summary>
///   <para></para>
/// </summary>
public sealed record Resolution : IResolution
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
  ///   <para>Returns a <see cref="string"/> that represents the current <see cref="Resolution"/> instance.</para>
  /// </summary>
  /// <returns>A string that represents the current <see cref="Resolution"/>.</returns>
  public override string ToString() => $"{Width}x{Height}";
}