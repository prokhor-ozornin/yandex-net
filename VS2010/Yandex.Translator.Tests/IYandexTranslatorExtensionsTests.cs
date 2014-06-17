using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Catharsis.Commons;
using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="IYandexTranslatorExtensions"/>.</para>
  /// </summary>
  public sealed class IYandexTranslatorExtensionsTests
  {
    private readonly IYandexTranslator xmlTranslator = Yandex.Translator(api => api.ApiKey(ConfigurationManager.AppSettings["ApiKey"]).Format(ApiDataFormat.Xml));
    private readonly IYandexTranslator jsonTranslator = Yandex.Translator(api => api.ApiKey(ConfigurationManager.AppSettings["ApiKey"]).Format(ApiDataFormat.Json));

    /// <summary>
    ///   <para>Performs testing of <see cref="IYandexTranslatorExtensions.Detect(IYandexTranslator, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Detect_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IYandexTranslatorExtensions.Detect(null, "text"));
      Assert.Throws<ArgumentNullException>(() => this.xmlTranslator.Detect(null));
      Assert.Throws<ArgumentException>(() => this.xmlTranslator.Detect(string.Empty));

      Assert.Equal("en", this.xmlTranslator.Detect("Hello, world"));
      Assert.Equal("ru", this.xmlTranslator.Detect("Привет, мир"));

      Assert.Equal("en", this.jsonTranslator.Detect("Hello, world"));
      Assert.Equal("ru", this.jsonTranslator.Detect("Привет, мир"));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IYandexTranslatorExtensions.Translate(IYandexTranslator, string, string, string)"/> method.</para>
    /// </summary>
    [Fact]
    public void Translate_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IYandexTranslatorExtensions.Translate(null, "language", "text"));
      Assert.Throws<ArgumentNullException>(() => this.xmlTranslator.Translate(null, "text"));
      Assert.Throws<ArgumentNullException>(() => this.xmlTranslator.Translate("language", null));
      Assert.Throws<ArgumentException>(() => this.xmlTranslator.Translate(string.Empty, "text"));
      Assert.Throws<ArgumentException>(() => this.xmlTranslator.Translate("language", string.Empty));

      this.TestTranslationResult(this.xmlTranslator.Translate("ru-en", "Привет, мир"));
      this.TestTranslationResult(this.xmlTranslator.Translate(translation => translation.From("ru").To("en").Text("Привет, мир")));
      this.TestTranslationResult(this.jsonTranslator.Translate("ru-en", "Привет, мир"));
      this.TestTranslationResult(this.jsonTranslator.Translate(translation => translation.From("ru").To("en").Text("Привет, мир")));
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IYandexTranslatorExtensions.TranslationPairs(IYandexTranslator)"/> method.</para>
    /// </summary>
    [Fact]
    public void TranslationPairs_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IYandexTranslatorExtensions.TranslationPairs(null));

      this.TestTranslationPairsResult(this.xmlTranslator.TranslationPairs());
      this.TestTranslationPairsResult(this.jsonTranslator.TranslationPairs());
    }

    private void TestTranslationResult(ITranslation translation)
    {
      Assert.NotNull(translation);

      Assert.Equal("ru", translation.FromLanguage);
      Assert.Equal("en", translation.ToLanguage);
      Assert.Equal("Hello, world", translation.Text);
    }
    
    private void TestTranslationPairsResult(IEnumerable<ITranslationPair> result)
    {
      Assertion.NotNull(result);

      Assert.True(result.Any());
      Assert.True(result.Any(pair => pair.FromLanguage == "en" && pair.ToLanguage == "ru"));
      Assert.True(result.Any(pair => pair.FromLanguage == "ru" && pair.ToLanguage == "en"));
    }
  }
}