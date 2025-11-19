using Catharsis.Extensions;

namespace Yandex.Detector;

/// <summary>
///   <para>A set of extension methods for the <see cref="IMobileDetector"/> interface.</para>
/// </summary>
/// <seealso cref="IMobileDetector"/>
public static class IMobileDetectorExtensions
{
  /// <param name="detector"></param>
  extension(IMobileDetector detector)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="cancellation"></param>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="detector"/> or <paramref name="headers"/> is <see langword="null"/>.</exception>
    public Task<IMobileDevice> DetectAsync(CancellationToken cancellation = default, params (string Name, object Value)[] headers)
    {
      if (detector is null) throw new ArgumentNullException(nameof(detector));
      if (headers is null) throw new ArgumentNullException(nameof(headers));

      return detector.DetectAsync(headers.ToReadOnlyDictionary(), cancellation);
    }

    /// <summary>
    ///   <para>Performs request to Yandex.Detector web service and determines capabilities of mobile client device.</para>
    /// </summary>
    /// <param name="action">Delegate that performs configuration of mobile device's HTTP headers to be send with request.</param>
    /// <param name="cancellation"></param>
    /// <returns>Instance of <see cref="IMobileDevice"/> object, describing capabilities of identified mobile device.</returns>
    /// <exception cref="DetectorException">If there was error either during the request to Yandex.Detector web service, or mobile device cannot be identified based on a set of provided HTTP headers.</exception>
    public Task<IMobileDevice> DetectAsync(Action<IDetectorRequest> action, CancellationToken cancellation = default)
    {
      if (detector is null) throw new ArgumentNullException(nameof(detector));
      if (action is null) throw new ArgumentNullException(nameof(action));

      var builder = new DetectorRequest();
    
      action(builder);

      return detector.DetectAsync(builder.Headers, cancellation);
    }

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="headers"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If <paramref name="detector"/> is <see langword="null"/>.</exception>
    public IMobileDevice Detect(params (string Name, object Value)[] headers) => detector.DetectAsync(CancellationToken.None, headers).Result;

    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="detector"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public IMobileDevice Detect(Action<IDetectorRequest> action) => detector.DetectAsync(action).Result;
  }
}