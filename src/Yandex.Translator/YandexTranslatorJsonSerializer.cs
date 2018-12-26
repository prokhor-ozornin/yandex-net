namespace Yandex.Translator
{
  using RestSharp.Serializers;

  internal sealed class YandexTranslatorJsonSerializer : ISerializer
  {
    public string Serialize(object subject)
    {
      return subject.Json();
    }

    public string RootElement { get; set; }

    public string Namespace { get; set; }

    public string DateFormat { get; set; }

    public string ContentType { get; set; }
  }
}