namespace Yandex.Detector;

/// <summary>
///   <para>Entry point for Yandex.Detector web service's access.</para>
/// </summary>
/// <seealso href="http://api.yandex.ru/detector"/>
public static class IYandexApiExtensions
{
  /// <summary>
  ///   <para>Returns detector's instance to query Yandex.Detector service.</para>
  /// </summary>
  /// <param name="api"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException">If <paramref name="api"/> is <see langword="null"/>.</exception>
  public static IMobileDetector Detector(this IYandexApi api) => api is not null ? new MobileDetector() : throw new ArgumentNullException(nameof(api));
}