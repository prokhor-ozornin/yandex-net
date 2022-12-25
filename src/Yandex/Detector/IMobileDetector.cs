namespace Yandex.Detector;

/// <summary>
///   <para>Provides access to functionality of Yandex.Detector mobile service.</para>
/// </summary>
public interface IMobileDetector : IDisposable
{
  /// <summary>
  ///   <para>Performs request to Yandex.Detector web service and determines capabilities of mobile client device, using provided HTTP headers for its identification.</para>
  /// </summary>
  /// <param name="headers">Mobile device's HTTP headers with values.</param>
  /// <param name="cancellation"></param>
  /// <returns>Instance of <see cref="IMobileDevice"/> object, describing capabilities of identified mobile device.</returns>
  /// <exception cref="DetectorException">If there was error either during the request to Yandex.Detector web service, or mobile device cannot be identified based on a set of provided HTTP headers.</exception>
  Task<IMobileDevice> Detect(IDictionary<string, object?> headers, CancellationToken cancellation = default);
}