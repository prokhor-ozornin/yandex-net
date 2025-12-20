namespace Yandex.Translator;

/// <summary>
///   <para>A set of extension methods for the <see cref="ITranslator"/> interface.</para>
/// </summary>
/// <seealso cref="ITranslator"/>
public static class ITranslatorExtensions
{
  /// <param name="translator"></param>
  extension(ITranslator translator)
  {
    /// <summary>
    ///   <para></para>
    /// </summary>
    /// <param name="action"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">If either <paramref name="translator"/> or <paramref name="action"/> is <see langword="null"/>.</exception>
    public IApi Configure(Action<IApiConfigurator> action)
    {
      if (translator is null) throw new ArgumentNullException(nameof(translator));
      if (action is null) throw new ArgumentNullException(nameof(action));

      var configurator = new ApiConfigurator();

      action(configurator);

      return translator.Configure(configurator);
    }
  }
}