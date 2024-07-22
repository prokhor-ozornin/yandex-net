namespace Yandex.Translator;

/// <summary>
///   <para>A set of extension methods for the <see cref="ITranslator"/> interface.</para>
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
    if (translator is null) throw new ArgumentNullException(nameof(translator));
    if (action is null) throw new ArgumentNullException(nameof(action));

    var configurator = new ApiConfigurator();

    action(configurator);

    return translator.Configure(configurator);
  }
}