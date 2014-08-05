using System;
using RestSharp;
using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="YandexTranslatorJsonDeserializer"/>.</para>
  /// </summary>
  public sealed class YandexTranslatorJsonDeserializerTests
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    /// </summary>
    /// <seealso cref="YandexTranslatorJsonDeserializer()"/>
    [Fact]
    public void Constructors()
    {
      var deserializer = new YandexTranslatorJsonDeserializer();
      Assert.Null(deserializer.DateFormat);
      Assert.Null(deserializer.Namespace);
      Assert.Null(deserializer.RootElement);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="YandexTranslatorJsonDeserializer.Deserialize{T}(IRestResponse)"/> method.</para>
    /// </summary>
    [Fact]
    public void Deserialize_Method()
    {
      var subject = Guid.NewGuid();
      Assert.Equal(subject, new YandexTranslatorJsonDeserializer().Deserialize<Guid>(new RestResponse { Content = subject.Json() }));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="YandexTranslatorJsonDeserializer.RootElement"/> property.</para>
    /// </summary>
    [Fact]
    public void RootElement_Property()
    {
      Assert.Equal("rootElement", new YandexTranslatorJsonDeserializer { RootElement = "rootElement" }.RootElement);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="YandexTranslatorJsonDeserializer.Namespace"/> property.</para>
    /// </summary>
    [Fact]
    public void Namespace_Property()
    {
      Assert.Equal("namespace", new YandexTranslatorJsonDeserializer { Namespace = "namespace" }.Namespace);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="YandexTranslatorJsonDeserializer.DateFormat"/> property.</para>
    /// </summary>
    [Fact]
    public void DateFormat_Property()
    {
      Assert.Equal("dateFormat", new YandexTranslatorJsonDeserializer { DateFormat = "dateFormat" }.DateFormat);
    }
  }
}