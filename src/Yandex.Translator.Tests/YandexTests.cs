using System;
using Catharsis.Commons;
using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="Yandex"/>.</para>
  /// </summary>
  public sealed class YandexTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="Yandex.Translator(Action{IApiConfigurator})"/> method.</para>
    /// </summary>
    [Fact]
    public void Translator_Method()
    {
      Assert.Throws<ArgumentNullException>(() => Yandex.Translator(null));
      Assert.Throws<InvalidOperationException>(() => Yandex.Translator(x => { }));

      var translator = Yandex.Translator(x => x.ApiKey("apiKey"));
      Assert.Equal("apiKey", translator.ApiKey);
      Assert.Equal(ApiDataFormat.Json, translator.Property("Format").To<ApiDataFormat>());

      translator = Yandex.Translator(x => x.ApiKey("apiKey").Format(ApiDataFormat.Xml));
      Assert.Equal("apiKey", translator.ApiKey);
      Assert.Equal(ApiDataFormat.Xml, translator.Property("Format").To<ApiDataFormat>());
    }
  }
}