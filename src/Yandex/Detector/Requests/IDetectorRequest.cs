namespace Yandex.Detector;

/// <summary>
///   <para>Represents a configurable request to the Yandex.Detector service.</para>
/// </summary>
public interface IDetectorRequest
{
  /// <summary>
  ///   <para>Add new mobile device header for HTTP request, or replaces an existing one.</para>
  /// </summary>
  /// <param name="name">Name of the header.</param>
  /// <param name="value">Value of the header.</param>
  /// <returns>Back reference to the current request instance.</returns>
  IDetectorRequest Header(string name, object? value);
}