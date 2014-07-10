using Catharsis.Commons;
using RestSharp.Serializers;

namespace Yandex.Translator
{
  internal sealed class YandexTranslatorXmlSerializer : ISerializer
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