using Catharsis.Commons;

namespace Yandex.Detector;

/// <summary>
///   <para>Set of extension methods for interface <see cref="IMobileDetector"/>.</para>
/// </summary>
/// <seealso cref="IMobileDetector"/>
public static class IMobileDetectorExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="detector"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static Task<IMobileDevice> Detect(this IMobileDetector detector, CancellationToken cancellation = default, params (string Name, object? Value)[] headers) => detector.Detect(headers.ToDictionary(), cancellation);

  /// <summary>
  ///   <para>Performs request to Yandex.Detector web service and determines capabilities of mobile client device.</para>
  /// </summary>
  /// <param name="detector">Instance of client for Yandex.Detector web service.</param>
  /// <param name="action">Delegate that performs configuration of mobile device's HTTP headers to be send with request.</param>
  /// <param name="cancellation"></param>
  /// <returns>Instance of <see cref="IMobileDevice"/> object, describing capabilities of identified mobile device.</returns>
  /// <exception cref="DetectorException">If there was error either during the request to Yandex.Detector web service, or mobile device cannot be identified based on a set of provided HTTP headers.</exception>
  public static Task<IMobileDevice> Detect(this IMobileDetector detector, Action<IDetectorRequest> action, CancellationToken cancellation = default)
  {
    var builder = new DetectorRequest();
    action(builder);

    return detector.Detect(builder.Headers, cancellation);
  }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="detector"></param>
  /// <param name="device"></param>
  /// <param name="cancellation"></param>
  /// <param name="headers"></param>
  /// <returns></returns>
  public static bool Detect(this IMobileDetector detector, out IMobileDevice? device, CancellationToken cancellation = default, params (string Name, object? Value)[] headers)
  {
    try
    {
      device = detector.Detect(cancellation, headers).Result;
      return true;
    }
    catch
    {
      device = null;
      return false;
    }
  }

  /// <summary>
  ///   <para>Performs request to Yandex.Detector web service and determines capabilities of mobile client device.</para>
  /// </summary>
  /// <param name="detector">Instance of client for Yandex.Detector web service.</param>
  /// <param name="device">Instance of <see cref="IMobileDevice"/> object, describing capabilities of identified mobile device.</param>
  /// <param name="action">Delegate that performs configuration of mobile device's HTTP headers to be send with request.</param>
  /// <param name="cancellation"></param>
  /// <returns><c>true</c> if call was successful and <paramref name="device"/> output parameter contains detected mobile device, or <c>false</c> if call failed and <paramref name="device"/> output parameter is a <c>null</c> reference.</returns>
  /// <exception cref="DetectorException">If there was error either during the request to Yandex.Detector web service, or mobile device cannot be identified based on a set of provided HTTP headers.</exception>
  public static bool Detect(this IMobileDetector detector, out IMobileDevice? device, Action<IDetectorRequest> action, CancellationToken cancellation = default)
  {
    try
    {
      device = detector.Detect(action, cancellation).Result;
      return true;
    }
    catch
    {
      device = null;
      return false;
    }
  }
}