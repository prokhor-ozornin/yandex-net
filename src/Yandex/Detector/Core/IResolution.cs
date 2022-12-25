namespace Yandex.Detector;

/// <summary>
///   <para></para>
/// </summary>
public interface IResolution : IEquatable<IResolution>
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  short Height { get; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  short Width { get; }
}