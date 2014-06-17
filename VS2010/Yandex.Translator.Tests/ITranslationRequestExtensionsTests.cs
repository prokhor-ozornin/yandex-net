using System;
using System.Globalization;
using Xunit;

namespace Yandex.Translator
{
  /// <summary>
  ///   <para>Tests set for class <see cref="ITranslationRequestExtensions"/>.</para>
  /// </summary>
  public sealed class ITranslationRequestExtensionsTests
  {
    /// <summary>
    ///   <para>Performs testing of <see cref="ITranslationRequestExtensions.From(ITranslationRequest, CultureInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void From_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ITranslationRequestExtensions.From(null, CultureInfo.InvariantCulture));
      Assert.Throws<ArgumentNullException>(() => ITranslationRequestExtensions.From(new TranslationRequest(), null));

      var request = new TranslationRequest();
      Assert.False(request.Parameters.ContainsKey("lang"));
      Assert.True(ReferenceEquals(request, request.From(CultureInfo.GetCultureInfo("ru"))));
      Assert.Equal("ru", request.Parameters["lang"]);
      Assert.Equal("ru-en", request.To(CultureInfo.GetCultureInfo("en")).Parameters["lang"]);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ITranslationRequestExtensions.Html(ITranslationRequest)"/> method.</para>
    /// </summary>
    [Fact]
    public void Html_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ITranslationRequestExtensions.Html(null));

      var request = new TranslationRequest();
      Assert.False(request.Parameters.ContainsKey("format"));
      Assert.True(ReferenceEquals(request, request.Html()));
      Assert.Equal("html", request.Parameters["format"]);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ITranslationRequestExtensions.PlainText(ITranslationRequest)"/> method.</para>
    /// </summary>
    [Fact]
    public void PlainText_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ITranslationRequestExtensions.PlainText(null));

      var request = new TranslationRequest();
      Assert.False(request.Parameters.ContainsKey("format"));
      Assert.True(ReferenceEquals(request, request.PlainText()));
      Assert.Equal("plain", request.Parameters["format"]);
    }

    /// <summary>
    ///   <para>Performs testing of <see cref="ITranslationRequestExtensions.To(ITranslationRequest, CultureInfo)"/> method.</para>
    /// </summary>
    [Fact]
    public void To_Method()
    {
      Assert.Throws<ArgumentNullException>(() => ITranslationRequestExtensions.To(null, CultureInfo.InvariantCulture));

      var request = new TranslationRequest();
      Assert.False(request.Parameters.ContainsKey("lang"));
      Assert.True(ReferenceEquals(request, request.To(CultureInfo.GetCultureInfo("ru"))));
      Assert.Equal("ru", request.Parameters["lang"]);
      Assert.Equal("en-ru", request.From(CultureInfo.GetCultureInfo("en")).Parameters["lang"]);
    }
  }
}