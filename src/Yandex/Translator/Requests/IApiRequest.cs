namespace Yandex.Translator;

/// <summary>
///   <para>Representation of a custom web request to Yandex.Translator web service.</para>
/// </summary>
public interface IApiRequest
{
  /// <summary>
  ///   <para>Map of parameters names/values to be used in a web request.</para>
  /// </summary>
  IReadOnlyDictionary<string, object> Parameters { get; }

  /// <summary>
  ///   <para></para>
  /// </summary>
  /// <param name="name"></param>
  /// <param name="value"></param>
  /// <returns></returns>
  IApiRequest WithParameter(string name, object value);
}