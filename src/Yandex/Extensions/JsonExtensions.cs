using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Yandex;

/// <summary>
///   <para></para>
/// </summary>
public static class JsonExtensions
{
  /// <summary>
  ///   <para>Serializes specified object into JSON string.</para>
  /// </summary>
  /// <param name="instance">Target object to be serialized.</param>
  /// <param name="settings">Serialization settings. If not specified, default settings set will be used.</param>
  /// <returns>JSON serialized version of <paramref name="instance"/> instance.</returns>
  /// <seealso cref="JsonConvert"/>
  public static string SerializeAsJson(this object instance, JsonSerializerSettings settings = null) =>
    JsonConvert.SerializeObject(instance, settings ?? new JsonSerializerSettings
    {
      ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
      DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
      NullValueHandling = NullValueHandling.Ignore,
      ObjectCreationHandling = ObjectCreationHandling.Auto
    });

  /// <summary>
  ///   <para>Deserializes object from JSON string.</para>
  /// </summary>
  /// <typeparam name="T">Type of object to be instantiated during deserialization.</typeparam>
  /// <param name="json">Serialized JSON object of type <typeparamref name="T"/>.</param>
  /// <param name="settings">Deserialization settings. If not specified, default settings set will be used.</param>
  /// <returns>Instance of <typeparamref name="T"/> type, deserialized from <paramref name="json"/> string.</returns>
  /// <seealso cref="JsonConvert"/>
  public static T DeserializeAsJson<T>(this string json, JsonSerializerSettings settings = null) =>
    JsonConvert.DeserializeObject<T>(json, settings ?? new JsonSerializerSettings
    {
      ContractResolver = new JsonEntityOrderedContractResolver(),
      Formatting = Formatting.None,
      DateFormatString = "o",
      DateFormatHandling = DateFormatHandling.IsoDateFormat,
      DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
      DefaultValueHandling = DefaultValueHandling.Include,
      NullValueHandling = NullValueHandling.Ignore,
      PreserveReferencesHandling = PreserveReferencesHandling.None,
      ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    });

  private sealed class JsonEntityOrderedContractResolver : DefaultContractResolver
  {
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
      var properties = base.CreateProperties(type, memberSerialization).OrderBy(property => property.PropertyName).ToList();

      var id = properties.FirstOrDefault(property => property.PropertyName.ToLowerInvariant() == "id");

      if (id != null)
      {
        properties.Remove(id);
        properties.Insert(0, id);
      }

      return properties;
    }
  }
}