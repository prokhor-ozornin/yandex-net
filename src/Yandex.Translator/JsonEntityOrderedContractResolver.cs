namespace Yandex.Translator
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Serialization;

  /// <summary>
  ///   <para>Custom JSON <see cref="IContractResolver"/> that orders serialized properties alphabetically, as well as placing property named "id" (case-insensitive), if it exists, at the top.</para>
  /// </summary>
  public sealed class JsonEntityOrderedContractResolver : DefaultContractResolver
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