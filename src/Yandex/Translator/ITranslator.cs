namespace Yandex.Translator;

/// <summary>
///   <para>Represents a client for making requests to Yandex.Translator web service.</para>
/// </summary>
public interface ITranslator
{
  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="configurator"></param>
  /// <returns></returns>
  IApi Configure(IApiConfigurator configurator);
}