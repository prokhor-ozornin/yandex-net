namespace Yandex.Translator;

/// <summary>
///   <para>Set of extension methods for interface <see cref="ITranslator"/>.</para>
/// </summary>
/// <seealso cref="ITranslator"/>
public static class ITranslatorExtensions
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="translator"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static IApi Configure(this ITranslator translator, Action<IApiConfigurator> action)
  {
    var configurator = new ApiConfigurator();

    action(configurator);

    return translator.Configure(configurator);
  }
}