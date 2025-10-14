using Catharsis.Fixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="TranslationApiRequest"/>.</para>
/// </summary>
/// <seealso cref="TranslationApiRequest"/>
public sealed class TranslationApiRequestTest : Test
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="TranslationApiRequest()"/>
  [Fact]
  public void Constructors()
  {
    typeof(TranslationApiRequest).Should().BeDerivedFrom<ApiRequest>().And.Implement<ITranslationApiRequest>();

    using (new AssertionScope())
    {
      var request = new TranslationApiRequest();

      request.Parameters.Should().BeEmpty();
    }
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationApiRequest.Format(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Format_Method()
  {
    using (new AssertionScope())
    {
      Test(null, Fixture<ITranslationApiRequest>.Create());
      Test("html", Fixture<ITranslationApiRequest>.Create());
    }

    return;

    static void Test(string format, ITranslationApiRequest request) => request.Format(format).Should().BeSameAs(request).And.BeOfType<TranslationApiRequest>().Which.Parameters["format"].Should().Be(format);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationApiRequest.From(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void From_Method()
  {
    using (new AssertionScope())
    {
      Test(null, Fixture<ITranslationApiRequest>.Create());
      Test("en", Fixture<ITranslationApiRequest>.Create());
    }

    return;

    static void Test(string language, ITranslationApiRequest request) => request.From(language).Should().BeSameAs(request).And.BeOfType<TranslationApiRequest>().Which.Parameters["lang"].Should().Be(language);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationApiRequest.To(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void To_Method()
  {
    using (new AssertionScope())
    {
      Test(null, Fixture<ITranslationApiRequest>.Create());
      Test("en", Fixture<ITranslationApiRequest>.Create());
    }

    return;

    static void Test(string language, ITranslationApiRequest request) => request.To(language).Should().BeSameAs(request).And.BeOfType<TranslationApiRequest>().Which.Parameters["lang"].Should().Be(language);
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationApiRequest.Text(string)"/> method.</para>
  /// </summary>
  [Fact]
  public void Text_Method()
  {
    using (new AssertionScope())
    {
      Test(null, Fixture<ITranslationApiRequest>.Create());
      Test("text", Fixture<ITranslationApiRequest>.Create());
    }

    return;

    static void Test(string text, ITranslationApiRequest request) => request.Text(text).Should().BeSameAs(request).And.BeOfType<TranslationApiRequest>().Which.Parameters["text"].Should().Be(text);
  }
}