using System;
using System.Linq;
using Catharsis.Commons;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="YandexTranslator"/>.</para>
  /// </summary>
  public sealed class YandexTranslatorTests
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    /// </summary>
    /// <seealso cref="YandexTranslator(ApiDataFormat, string)"/>
    [Fact]
    public void Constructors()
    {
      Assert.Throws<ArgumentNullException>(() => new YandexTranslator(ApiDataFormat.Xml, null));
      Assert.Throws<ArgumentException>(() => new YandexTranslator(ApiDataFormat.Xml, string.Empty));

      var translator = new YandexTranslator(ApiDataFormat.Xml, "apiKey");
      Assert.Equal("apiKey", translator.ApiKey);
      Assert.Equal(ApiDataFormat.Xml, translator.Format);
      Assert.True(translator.Field("jsonSerializer").To<ISerializer>() is YandexTranslatorJsonSerializer);
      Assert.True(translator.Field("jsonDeserializer").To<IDeserializer>() is YandexTranslatorJsonDeserializer);
      Assert.True(translator.Field("xmlSerializer").To<ISerializer>() is YandexTranslatorXmlSerializer);
      Assert.True(translator.Field("xmlDeserializer").To<IDeserializer>() is YandexTranslatorXmlDeserializer);

      var client = translator.Field("restClient").To<RestClient>();
      Assert.Equal("https://translate.yandex.net/api/v1.5/tr", client.BaseUrl);
      var key = client.DefaultParameters.FirstOrDefault(x => x.Name == "key");
      Assert.NotNull(key);
      Assert.Equal("apiKey", key.Value);
    }
  }
}