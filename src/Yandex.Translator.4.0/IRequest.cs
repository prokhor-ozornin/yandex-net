using System.Collections.Generic;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Representation of a custom web request to Yandex.Translator web service.</para>
  /// </summary>
  public interface IRequest
  {
    /// <summary>
    ///   <para>Map of parameters names/values to be used in a web request.</para>
    /// </summary>
    IDictionary<string, object> Parameters { get; }
  }
}