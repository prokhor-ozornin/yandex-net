namespace Yandex.Detector;

/// <summary>
///   <para>Represents a configurable request to the Yandex.Detector service.</para>
/// </summary>
public interface IDetectorRequest
{
  /// <summary>
  ///   <para>Map of parameters names/values to be used in a web request.</para>
  /// </summary>
  IReadOnlyDictionary<string, object> Headers { get; }

  /// <summary>
  ///   <para>Add new mobile device header for HTTP request, or replaces an existing one.</para>
  /// </summary>
  /// <param name="name">Name of the header.</param>
  /// <param name="value">Value of the header.</param>
  /// <returns>Back reference to the current request instance.</returns>
  IDetectorRequest WithHeader(string name, object value);
}