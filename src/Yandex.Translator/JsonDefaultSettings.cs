using Newtonsoft.Json;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Default settings for <see cref="JsonConverter"/>.</para>
  /// </summary>
  /// <seealso cref="JsonConverter"/>
  public static class JsonDefaultSettings
  {
    private static JsonSerializerSettings deserializer;
    private static JsonSerializerSettings serializer;

    /// <summary>
    ///   <para>Default JSON deserializer settings.</para>
    /// </summary>
    public static JsonSerializerSettings Deserializer
    {
      get
      {
        return deserializer ?? (deserializer = new JsonSerializerSettings
        {
          ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
          DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
          NullValueHandling = NullValueHandling.Ignore,
          ObjectCreationHandling = ObjectCreationHandling.Auto
        });
      }
    }

    /// <summary>
    ///   <para>Default JSON serializer settings.</para>
    /// </summary>
    public static JsonSerializerSettings Serializer
    {
      get
      {
        return serializer ?? (serializer = new JsonSerializerSettings
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
      }
    }
  }
}