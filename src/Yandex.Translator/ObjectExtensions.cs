using System;
using Catharsis.Commons;
using Newtonsoft.Json;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Set of extension methods for class <see cref="object"/>.</para>
  /// </summary>
  /// <seealso cref="object"/>
  public static class ObjectExtensions
  {
    /// <summary>
    ///   <para>Serializes specified object into JSON string.</para>
    /// </summary>
    /// <param name="subject">Target object to be serialized.</param>
    /// <param name="settings">Serialization settings. If not specified, default settings set (<see cref="JsonDefaultSettings.Serializer"/>) will be used.</param>
    /// <returns>JSON serialized version of <paramref name="subject"/> instance.</returns>
    /// <exception cref="ArgumentNullException">If <paramref name="subject"/> is a <c>null</c> reference.</exception>
    /// <seealso cref="JsonConvert"/>
    /// <seealso cref="JsonDefaultSettings"/>
    public static string Json(this object subject, JsonSerializerSettings settings = null)
    {
      Assertion.NotNull(subject);

      return JsonConvert.SerializeObject(subject, settings ?? JsonDefaultSettings.Serializer);
    }
  }
}