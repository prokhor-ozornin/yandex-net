namespace Yandex.Translator
{
  using Catharsis.Commons;
  using RestSharp.Serializers;

  internal sealed class YandexTranslatorXmlSerializer : IXmlSerializer
  {
    public string Serialize(object subject)
    {
      return subject.ToXml();
    }

    public string RootElement { get; set; }

    public string Namespace { get; set; }

    public string DateFormat { get; set; }

    public string ContentType { get; set; }
  }
}