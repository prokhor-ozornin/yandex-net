using System.Globalization;
using Catharsis.Commons;
using Catharsis.Extensions;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="ITranslationApiRequestExtensions"/>.</para>
/// </summary>
public sealed class ITranslationApiRequestExtensionsTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of <see cref="ITranslationApiRequestExtensions.AsHtml(ITranslationApiRequest)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsHtml_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ITranslationApiRequestExtensions.AsHtml(null)).ThrowExactly<ArgumentNullException>().WithParameterName("request");

      Validate(new TranslationApiRequest());
    }

    return;

    static void Validate(ITranslationApiRequest request) => request.AsHtml().Should().BeSameAs(request).And.BeOfType<TranslationApiRequest>().Which.Parameters["format"].Should().Be("html");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ITranslationApiRequestExtensions.AsText(ITranslationApiRequest)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsText_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ITranslationApiRequestExtensions.AsText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("request");

      Validate(new TranslationApiRequest());
    }

    return;

    static void Validate(ITranslationApiRequest request) => request.AsText().Should().BeSameAs(request).And.BeOfType<TranslationApiRequest>().Which.Parameters["format"].Should().Be("plain");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ITranslationApiRequestExtensions.From(ITranslationApiRequest, CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void From_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ITranslationApiRequestExtensions.From(null, CultureInfo.InvariantCulture)).ThrowExactly<ArgumentNullException>().WithParameterName("request");

      Validate(null, new TranslationApiRequest());
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture => Validate(culture, new TranslationApiRequest()));
    }
    
    return;

    static void Validate(CultureInfo culture, ITranslationApiRequest request) => request.From(culture).Should().BeSameAs(request).And.BeOfType<TranslationApiRequest>().Which.Parameters["lang"].Should().Be(culture?.TwoLetterISOLanguageName);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ITranslationApiRequestExtensions.To(ITranslationApiRequest, CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void To_Method()
  {
    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ITranslationApiRequestExtensions.To(null, CultureInfo.InvariantCulture)).ThrowExactly<ArgumentNullException>().WithParameterName("request");

      Validate(null, new TranslationApiRequest());
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(culture => Validate(culture, new TranslationApiRequest()));
    }

    return;

    static void Validate(CultureInfo culture, ITranslationApiRequest request) => request.To(culture).Should().BeSameAs(request).And.BeOfType<TranslationApiRequest>().Which.Parameters["lang"].Should().Be(culture?.TwoLetterISOLanguageName);
  }
}