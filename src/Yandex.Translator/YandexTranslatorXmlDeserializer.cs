namespace Yandex.Translator
{
  using Catharsis.Commons;
  using RestSharp;
  using RestSharp.Deserializers;

  internal sealed class YandexTranslatorXmlDeserializer : IDeserializer
  {
    public T Deserialize<T>(IRestResponse response)
    {
      return response.Content.AsXml<T>();
    }

    public string RootElement { get; set; }

    public string Namespace { get; set; }
    
    public string DateFormat { get; set; }
  }
}