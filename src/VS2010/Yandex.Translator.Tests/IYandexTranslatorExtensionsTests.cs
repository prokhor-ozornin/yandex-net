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
    ///   <para>Performs testing of following methods :</para>
    ///   <list type="bullet">
    ///     <item><description><see cref="IYandexTranslatorExtensions.Detect(IYandexTranslator, string)"/></description></item>
    ///     <item><description><see cref="IYandexTranslatorExtensions.Detect(IYandexTranslator, string, out string)"/></description></item>
    ///   </list>
    /// </summary>
    [Fact]
    public void Detect_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IYandexTranslatorExtensions.Detect(null, "text"));
      Assert.Throws<ArgumentNullException>(() => this.xmlTranslator.Detect(null));
      Assert.Throws<ArgumentException>(() => this.xmlTranslator.Detect(string.Empty));

      string language;
      Assert.Throws<ArgumentNullException>(() => IYandexTranslatorExtensions.Detect(null, "text", out language));
      Assert.Throws<ArgumentNullException>(() => this.xmlTranslator.Detect(null, out language));
      Assert.Throws<ArgumentException>(() => this.xmlTranslator.Detect(string.Empty, out language));

      Assert.Equal("en", this.xmlTranslator.Detect("Hello, world"));
      Assert.Equal("ru", this.xmlTranslator.Detect("Привет, мир"));
      Assert.True(this.xmlTranslator.Detect("Hello, world", out language));
      Assert.Equal("en", language);
      Assert.True(this.xmlTranslator.Detect("Привет, мир", out language));
      Assert.Equal("ru", language);

      Assert.Equal("en", this.jsonTranslator.Detect("Hello, world"));
      Assert.Equal("ru", this.jsonTranslator.Detect("Привет, мир"));
      Assert.True(this.jsonTranslator.Detect("Hello, world", out language));
      Assert.Equal("en", language);
      Assert.True(this.jsonTranslator.Detect("Привет, мир", out language));
      Assert.Equal("ru", language);
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

      ITranslation translation;
      Assert.Throws<ArgumentNullException>(() => IYandexTranslatorExtensions.Translate(null, request => { }, out translation));
      Assert.Throws<ArgumentNullException>(() => this.xmlTranslator.Translate(null, out translation));

      this.TestTranslationResult(this.xmlTranslator.Translate("ru-en", "Привет, мир"));
      this.TestTranslationResult(this.xmlTranslator.Translate(t => t.From("ru").To("en").Text("Привет, мир")));
      Assert.True(this.xmlTranslator.Translate(t => t.From("ru").To("en").Text("Привет, мир"), out translation));
      this.TestTranslationResult(translation);

      this.TestTranslationResult(this.jsonTranslator.Translate("ru-en", "Привет, мир"));
      this.TestTranslationResult(this.jsonTranslator.Translate(t => t.From("ru").To("en").Text("Привет, мир")));
      Assert.True(this.jsonTranslator.Translate(t => t.From("ru").To("en").Text("Привет, мир"), out translation));
      this.TestTranslationResult(translation);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="IYandexTranslatorExtensions.TranslationPairs(IYandexTranslator)"/> method.</para>
    /// </summary>
    [Fact]
    public void TranslationPairs_Method()
    {
      Assert.Throws<ArgumentNullException>(() => IYandexTranslatorExtensions.TranslationPairs(null));

      IEnumerable<ITranslationPair> pairs;
      Assert.Throws<ArgumentNullException>(() => IYandexTranslatorExtensions.TranslationPairs(null, out pairs));


      this.TestTranslationPairsResult(this.xmlTranslator.TranslationPairs());
      Assert.True(this.xmlTranslator.TranslationPairs(out pairs));
      this.TestTranslationPairsResult(pairs);

      this.TestTranslationPairsResult(this.jsonTranslator.TranslationPairs());
      Assert.True(this.jsonTranslator.TranslationPairs(out pairs));
      this.TestTranslationPairsResult(pairs);
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