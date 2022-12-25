namespace Yandex;

/// <summary>
///   <para>Entry point to access web services.</para>
/// </summary>
public static class Yandex
{
  /// <summary>
  ///   <para>Configures instance of client translator to be used for making requests to Yandex.Translator web service.</para>
  /// </summary>
  public static IYandexApi Api { get; } = new YandexApi();

  private sealed class YandexApi : IYandexApi
  {
  }
}