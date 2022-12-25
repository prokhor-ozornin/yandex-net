namespace Yandex.Detector;

/// <summary>
///   <para></para>
/// </summary>
public interface IError : IEquatable<IError>
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  string Text { get; }
}