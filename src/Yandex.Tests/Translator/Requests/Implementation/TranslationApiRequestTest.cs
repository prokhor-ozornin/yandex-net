using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="TranslationApiRequest"/>.</para>
/// </summary>
public sealed class TranslationApiRequestTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="TranslationApiRequest()"/>
  [Fact]
  public void Constructors()
  {
    typeof(TranslationApiRequest).Should().BeDerivedFrom<ApiRequest>().And.Implement<ITranslationApiRequest>();

    var request = new TranslationApiRequest();
    request.Parameters.Should().BeEmpty();
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationApiRequest.Format(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Format_Method()
  {
    using (new AssertionScope())
    {
      Validate(null, new TranslationApiRequest());
      Validate("html", new TranslationApiRequest());
    }

    return;

    static void Validate(string format, ITranslationApiRequest request) => request.Format(format).Should().BeSameAs(request).And.BeOfType<TranslationApiRequest>().Which.Parameters["format"].Should().Be(format);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationApiRequest.From(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void From_Method()
  {
    using (new AssertionScope())
    {
      Validate(null, new TranslationApiRequest());
      Validate("en", new TranslationApiRequest());
    }

    return;

    static void Validate(string language, ITranslationApiRequest request) => request.From(language).Should().BeSameAs(request).And.BeOfType<TranslationApiRequest>().Which.Parameters["lang"].Should().Be(language);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationApiRequest.To(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void To_Method()
  {
    using (new AssertionScope())
    {
      Validate(null, new TranslationApiRequest());
      Validate("en", new TranslationApiRequest());
    }

    return;

    static void Validate(string language, ITranslationApiRequest request) => request.To(language).Should().BeSameAs(request).And.BeOfType<TranslationApiRequest>().Which.Parameters["lang"].Should().Be(language);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationApiRequest.Text(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Text_Method()
  {
    using (new AssertionScope())
    {
      Validate(null, new TranslationApiRequest());
      Validate("text", new TranslationApiRequest());
    }

    return;

    static void Validate(string text, ITranslationApiRequest request) => request.Text(text).Should().BeSameAs(request).And.BeOfType<TranslationApiRequest>().Which.Parameters["text"].Should().Be(text);
  }
}