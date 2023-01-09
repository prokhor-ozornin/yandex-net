using System.Globalization;
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
    AssertionExtensions.Should(() => ITranslationApiRequestExtensions.AsHtml(null)).ThrowExactly<ArgumentNullException>().WithParameterName("request");

    var request = new TranslationApiRequest();

    request.Parameters.Should().BeEmpty();

    request.AsHtml().Should().NotBeNull().And.BeSameAs(request);
    request.Parameters["format"].Should().Be("html");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ITranslationApiRequestExtensions.AsText(ITranslationApiRequest)"/> method.</para>
  /// </summary>
  [Fact]
  public void AsText_Method()
  {
    AssertionExtensions.Should(() => ITranslationApiRequestExtensions.AsText(null)).ThrowExactly<ArgumentNullException>().WithParameterName("request");

    var request = new TranslationApiRequest();

    request.Parameters.Should().BeEmpty();

    request.AsText().Should().NotBeNull().And.BeSameAs(request);
    request.Parameters["format"].Should().Be("plain");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ITranslationApiRequestExtensions.From(ITranslationApiRequest, CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void From_Method()
  {
    static void Validate(CultureInfo culture)
    {
      var request = new TranslationApiRequest();

      request.Parameters.Should().BeEmpty();

      request.From(culture).Should().NotBeNull().And.BeSameAs(request);
      request.Parameters["lang"].Should().Be(culture?.TwoLetterISOLanguageName);
      request.To(culture).Should().NotBeNull().And.BeSameAs(request);

      if (culture != null)
      {
        request.Parameters["lang"].Should().Be($"{culture.TwoLetterISOLanguageName}-{culture.TwoLetterISOLanguageName}");
      }
      else
      {
        request.Parameters["lang"].Should().BeNull();
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ITranslationApiRequestExtensions.From(null, CultureInfo.InvariantCulture)).ThrowExactly<ArgumentNullException>().WithParameterName("request");

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="ITranslationApiRequestExtensions.To(ITranslationApiRequest, CultureInfo)"/> method.</para>
  /// </summary>
  [Fact]
  public void To_Method()
  {
    static void Validate(CultureInfo culture)
    {
      var request = new TranslationApiRequest();

      request.Parameters.Should().BeEmpty();

      request.To(culture).Should().NotBeNull().And.BeSameAs(request);
      request.Parameters["lang"].Should().Be(culture?.TwoLetterISOLanguageName);
      request.To(culture).Should().NotBeNull().And.BeSameAs(request);

      if (culture != null)
      {
        request.Parameters["lang"].Should().Be($"{culture.TwoLetterISOLanguageName}-{culture.TwoLetterISOLanguageName}");
      }
      else
      {
        request.Parameters["lang"].Should().BeNull();
      }
    }

    using (new AssertionScope())
    {
      AssertionExtensions.Should(() => ITranslationApiRequestExtensions.To(null, CultureInfo.InvariantCulture)).ThrowExactly<ArgumentNullException>().WithParameterName("request");

      Validate(null);
      CultureInfo.GetCultures(CultureTypes.AllCultures).ForEach(Validate);
    }
  }
}