using RestSharp;
using RestSharp.Serializers;

namespace Yandex.Translator;

internal sealed class JsonRestSerializer : IRestSerializer, ISerializer, IDeserializer
{
  public string? Serialize(object? subject) => subject?.AsJson();

  public string? Serialize(Parameter parameter) => Serialize(parameter.Value);

  public T? Deserialize<T>(RestResponse response) => response.Content != null ? response.Content.AsJson<T>() : default;
  
  public ISerializer Serializer => this;

  public IDeserializer Deserializer => this;

  public DataFormat DataFormat => DataFormat.Json;

  public string ContentType { get; set; } = "application/json";

  public string[] AcceptedContentTypes => RestSharp.Serializers.ContentType.JsonAccept;

  public SupportsContentType SupportsContentType => contentType => contentType.EndsWith("json", StringComparison.InvariantCultureIgnoreCase);
}