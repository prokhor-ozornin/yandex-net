using RestSharp;
using RestSharp.Serializers;

namespace Yandex.Translator;

internal sealed class JsonRestSerializer : IRestSerializer, ISerializer, IDeserializer
{
  public string Serialize(object subject) => subject?.SerializeAsJson();

  public ContentType ContentType { get; set; }

  public string Serialize(Parameter parameter) => Serialize(parameter.Value);

  public T Deserialize<T>(RestResponse response) => response.Content is not null ? response.Content.DeserializeAsJson<T>() : default;
  
  public ISerializer Serializer => this;

  public IDeserializer Deserializer => this;
  
  public string[] AcceptedContentTypes => ContentType.JsonAccept;

  public SupportsContentType SupportsContentType => contentType => contentType.Equals(ContentType.Json);

  public DataFormat DataFormat => DataFormat.Json;
}