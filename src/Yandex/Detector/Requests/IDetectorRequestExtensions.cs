﻿namespace Yandex.Detector;

/// <summary>
///   <para>Set of extension methods for interface <see cref="IDetectorRequest"/>.</para>
/// </summary>
/// <seealso cref="IDetectorRequest"/>
public static class IDetectorRequestExtensions
{
  /// <summary>
  ///   <para>Adds new header for HTTP request that identifies target mobile device as having an Opera Mini browser installed.</para>
  /// </summary>
  /// <param name="request">Instance of request to Yandex.Detector service.</param>
  /// <param name="version">Version of installed Opera Mini.</param>
  /// <returns>Back reference to the provided <paramref name="request"/> instance.</returns>
  /// <seealso cref="IDetectorRequest.WithHeader"/>
  public static IDetectorRequest OperaMini(this IDetectorRequest request, string version) => request.WithHeader("x-operamini-phone-ua", version);

  /// <summary>
  ///   <para>Adds set of headers for HTTP request that indicates a mobile profile of the target device.</para>
  ///   <para>The following HTTP headers are set : "profile", "wap-profile", "x-wap-profile".</para>
  /// </summary>
  /// <param name="request">Instance of request to Yandex.Detector service.</param>
  /// <param name="profile">Value of HTTP mobile profile headers.</param>
  /// <returns>Back reference to the provided <paramref name="request"/> instance.</returns>
  /// <seealso cref="IDetectorRequest.WithHeader"/>
  public static IDetectorRequest Profile(this IDetectorRequest request, string profile)
  {
    request.WithHeader("profile", profile);
    request.WithHeader("wap-profile", profile);
    request.WithHeader("x-wap-profile", profile);

    return request;
  }

  /// <summary>
  ///   <para>Adds new "user-agent" header for HTTP request that identifies a target mobile device.</para>
  /// </summary>
  /// <param name="request">Instance of request to Yandex.Detector service.</param>
  /// <param name="userAgent">Value of User-agent header.</param>
  /// <returns>Back reference to the provided <paramref name="request"/> instance.</returns>
  /// <seealso cref="IDetectorRequest.WithHeader"/>
  public static IDetectorRequest UserAgent(this IDetectorRequest request, string userAgent) => request.WithHeader("user-agent", userAgent);
}