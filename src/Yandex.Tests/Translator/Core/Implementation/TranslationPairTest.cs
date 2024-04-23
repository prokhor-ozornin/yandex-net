using Catharsis.Commons;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Yandex.Translator;

namespace Yandex.Tests.Translator;

/// <summary>
///   <para>Tests set for class <see cref="TranslationPair"/>.</para>
/// </summary>
public sealed class TranslationPairTest : UnitTest
{
  /// <summary>
  ///   <para>Performs testing of class constructor(s).</para>
  /// </summary>
  /// <seealso cref="TranslationPair(string, string)"/>
  [Fact]
  public void Constructors()
  {
    AssertionExtensions.Should(() => new TranslationPair(null, "to")).ThrowExactly<ArgumentNullException>().WithParameterName("fromLanguage");
    AssertionExtensions.Should(() => new TranslationPair("from", null)).ThrowExactly<ArgumentNullException>().WithParameterName("toLanguage");
    AssertionExtensions.Should(() => new TranslationPair(string.Empty, "ro")).ThrowExactly<ArgumentException>().WithMessage("fromLanguage");
    AssertionExtensions.Should(() => new TranslationPair("from", string.Empty)).ThrowExactly<ArgumentException>().WithMessage("toLanguage");

    typeof(TranslationPair).Should().BeDerivedFrom<object>().And.Implement<ITranslationPair>();

    var pair = new TranslationPair("en", "ru");
    pair.FromLanguage.Should().Be("en");
    pair.ToLanguage.Should().Be("ru");
  }

  /// <summary>
  ///   <para>Performs testing of <see cref="TranslationPair.ToString()"/> method.</para>
  /// </summary>
  [Fact]
  public void ToString_Method()
  {
    using (new AssertionScope())
    {
      Validate("en-ru", new TranslationPair("en", "ru"));
    }

    return;

    static void Validate(string value, object instance) => instance.ToString().Should().Be(value);
  }
}