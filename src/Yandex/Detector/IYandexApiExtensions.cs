namespace Yandex.Detector;

/// <summary>
///   <para>Entry point for Yandex.Detector web service's access.</para>
/// </summary>
/// <seealso cref="http://api.yandex.ru/detector"/>
public static class IYandexApiExtensions
{
  /// <summary>
  ///   <para>Returns detector's instance to query Yandex.Detector service.</para>
  /// </summary>
  /// <param name="api"></param>
  /// <returns></returns>
  public static IMobileDetector Detector(this IYandexApi api) => new MobileDetector();
}