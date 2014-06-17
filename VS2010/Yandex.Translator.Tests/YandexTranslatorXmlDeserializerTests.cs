using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="YandexTranslatorXmlDeserializer"/>.</para>
  /// </summary>
  public sealed class RuLawXmlDeserializerTests
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    /// </summary>
    /// <seealso cref="YandexTranslatorXmlDeserializer()"/>
    [Fact]
    public void Constructors()
    {
      var deserializer = new YandexTranslatorXmlDeserializer();
      Assert.Null(deserializer.DateFormat);
      Assert.Null(deserializer.Namespace);
      Assert.Null(deserializer.RootElement);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="YandexTranslatorXmlDeserializer.RootElement"/> property.</para>
    /// </summary>
    [Fact]
    public void RootElement_Property()
    {
      Assert.Equal("rootElement", new YandexTranslatorXmlDeserializer { RootElement = "rootElement" }.RootElement);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="YandexTranslatorXmlDeserializer.Namespace"/> property.</para>
    /// </summary>
    [Fact]
    public void Namespace_Property()
    {
      Assert.Equal("namespace", new YandexTranslatorXmlDeserializer { Namespace = "namespace" }.Namespace);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="YandexTranslatorXmlDeserializer.DateFormat"/> property.</para>
    /// </summary>
    [Fact]
    public void DateFormat_Property()
    {
      Assert.Equal("dateFormat", new YandexTranslatorXmlDeserializer { DateFormat = "dateFormat" }.DateFormat);
    }
  }
}