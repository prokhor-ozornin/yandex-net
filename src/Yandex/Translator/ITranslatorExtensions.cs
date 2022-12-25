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
  /// <param name="self"></param>
  /// <param name="action"></param>
  /// <returns></returns>
  public static IApi Configure(this ITranslator self, Action<IApiConfigurator> action)
  {
    var configurator = new ApiConfigurator();

    action(configurator);

    return self.Configure(configurator);
  }
}