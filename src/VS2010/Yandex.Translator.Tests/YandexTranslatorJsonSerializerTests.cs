using System;
using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="YandexTranslatorJsonSerializer"/>.</para>
  /// </summary>
  public sealed class RuLawJsonSerializerTests
  {
    /// <summary>
    ///   <para>Performs testing of class constructor(s).</para>
    /// </summary>
    /// <seealso cref="YandexTranslatorJsonSerializer()"/>
    [Fact]
    public void Constructors()
    {
      var serializer = new YandexTranslatorJsonSerializer();
      Assert.Null(serializer.ContentType);
      Assert.Null(serializer.DateFormat);
      Assert.Null(serializer.Namespace);
      Assert.Null(serializer.RootElement);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="YandexTranslatorJsonSerializer.Serialize(object)"/> method.</para>
    /// </summary>
    [Fact]
    public void Serialize_Method()
    {
      var subject = Guid.NewGuid();
      Assert.Equal(subject.Json(), new YandexTranslatorJsonSerializer().Serialize(subject));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="YandexTranslatorJsonSerializer.RootElement"/> property.</para>
    /// </summary>
    [Fact]
    public void RootElement_Property()
    {
      Assert.Equal("rootElement", new YandexTranslatorJsonSerializer { RootElement = "rootElement" }.RootElement);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="YandexTranslatorJsonSerializer.Namespace"/> property.</para>
    /// </summary>
    [Fact]
    public void Namespace_Property()
    {
      Assert.Equal("namespace", new YandexTranslatorJsonSerializer { Namespace = "namespace" }.Namespace);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="YandexTranslatorJsonSerializer.DateFormat"/> property.</para>
    /// </summary>
    [Fact]
    public void DateFormat_Property()
    {
      Assert.Equal("dateFormat", new YandexTranslatorJsonSerializer { DateFormat = "dateFormat" }.DateFormat);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="YandexTranslatorJsonSerializer.ContentType"/> property.</para>
    /// </summary>
    [Fact]
    public void ContentType_Property()
    {
      Assert.Equal("contentType", new YandexTranslatorJsonSerializer { ContentType = "contentType" }.ContentType);
    }
  }
}